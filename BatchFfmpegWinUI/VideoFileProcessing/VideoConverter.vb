Option Strict On

Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports Microsoft.UI.Dispatching
Imports Microsoft.UI.Xaml
Imports Windows.Storage
Imports FileAttributes = Windows.Storage.FileAttributes

Module VideoConverter

    Function GetConvertibleFiles(droppedItems As IReadOnlyList(Of IStorageItem),
                                 activeFormatName As String,
                                 activeScriptName As String,
                                 fileExtension As String) As List(Of ConvertibleVideo)
        Dim files As New List(Of ConvertibleVideo)
        For Each droppedItem In droppedItems
            If droppedItem.Attributes.HasFlag(FileAttributes.Directory) Then
                AddFilesFromDir(files, droppedItem, activeFormatName, activeScriptName, fileExtension)
            Else
                Dim filePath = droppedItem.Path
                TryAddFile(files, droppedItem.Name, Path.GetDirectoryName(filePath), filePath, activeFormatName, activeScriptName, fileExtension)
            End If
        Next
        Return files
    End Function

    Private Const IconOk As Char = ChrW(&HE001)
    Private Const IconExclamation As Char = ChrW(&HE171)
    Private Const IconProcessing As Char = ChrW(&HE768)

    Private ReadOnly _regGetDuration As New Regex("Duration: (.+?)(?=,)", RegexOptions.Compiled)
    Private ReadOnly _regGetTime As New Regex("time=(.+?)(?= )", RegexOptions.Compiled)

    Async Function ConvertAsync(fileList As IReadOnlyList(Of ConvertibleVideo),
                                statusCallback As Action(Of String),
                                cancelToken As CancellationToken,
                                softStop As StrongBox(Of Boolean),
                                parallelCount As Integer,
                                dispatcherQueue As DispatcherQueue,
                                processGroupManager As FfmpegPerformanceManager) As Task(Of Boolean)

        Dim errToPrompt As New StrongBox(Of String)
        Dim success = False
        Try
            Dim index As New StrongBox(Of Integer)
            Dim timer As New Stopwatch
            Dim parallelOptions As New ParallelOptions With {
                .CancellationToken = cancelToken,
                .MaxDegreeOfParallelism = parallelCount
            }

            Await Parallel.ForEachAsync(fileList, parallelOptions,
            Function(vidFile, token)
                Return New ValueTask(
                Async Function()
                    If Volatile.Read(softStop.Value) Then Return
                    Await ConvertVideoFileAsync(vidFile, statusCallback, token, softStop, fileList.Count, index, timer, errToPrompt, dispatcherQueue, processGroupManager)
                End Function())
            End Function)

            If softStop.Value Then
                statusCallback("Stopped")
                Await MsgBoxAsync("Conversion was stopped")
                success = False
            Else
                statusCallback("Done")
                Await MsgBoxAsync("Mission accomplished")
                success = True
            End If
        Catch ex As AggregateException
            For Each inner In ex.InnerExceptions
                If TypeOf inner Is OperationCanceledException Then
                    statusCallback("Conversion was terminated")
                    Exit For
                End If
            Next
        Catch ex As OperationCanceledException
            statusCallback("Conversion was terminated")
        Catch ex As Exception
            statusCallback($"Error {ex.GetType.Name}: {ex.Message}")
        End Try

        processGroupManager.Clear()
        If errToPrompt.Value IsNot Nothing Then
            Await ShowFfmpegError(errToPrompt.Value)
        End If

        Return success
    End Function

    Private Async Function ConvertVideoFileAsync(
             vidFile As ConvertibleVideo,
             statusCallback As Action(Of String),
             cancelToken As CancellationToken,
             softStop As StrongBox(Of Boolean),
             fileListCount As Integer,
             index As StrongBox(Of Integer),
             timer As Stopwatch,
             errToPrompt As StrongBox(Of String),
             dispatcherQueue As DispatcherQueue,
             processGroupManager As FfmpegPerformanceManager) As Task

        Interlocked.Increment(index.Value)
        statusCallback($"Converting {Volatile.Read(index.Value)}/{fileListCount}")
        If File.Exists(vidFile.Output) Then
            dispatcherQueue.TryEnqueue(Sub() vidFile.Icon = IconOk)
            Return
        End If

        dispatcherQueue.TryEnqueue(
            Sub()
                vidFile.Icon = IconProcessing
                vidFile.ProgressIndeterminate = True
                vidFile.ProgressVisibility = Visibility.Visible
            End Sub)

        Dim procStart As New ProcessStartInfo With {
            .UseShellExecute = False,
            .FileName = "cmd",
            .Arguments = $"/c {vidFile.ScriptName}.bat ""{vidFile.Path}"" ""{vidFile.Output}""",
            .CreateNoWindow = True,
            .RedirectStandardOutput = True,
            .RedirectStandardError = True,
            .StandardOutputEncoding = Encoding.UTF8,
            .StandardErrorEncoding = Encoding.UTF8
        }

        Dim proc = Process.Start(procStart)
        processGroupManager.Add(proc)
        timer.Start()

        Dim killingEx As TaskCanceledException = Nothing
        Try
            Dim totalTimeLength As Double? = Nothing
            Dim handleOutput =
            Sub(curLine As String)
                If curLine = Nothing OrElse cancelToken.IsCancellationRequested Then
                    dispatcherQueue.TryEnqueue(Sub() vidFile.ProgressVisibility = Visibility.Collapsed)
                    Return
                End If

                If totalTimeLength Is Nothing Then
                    If Not curLine.Contains("Duration: ") Then Return

                    Dim timeMatch = _regGetDuration.Matches(curLine).OfType(Of Match).FirstOrDefault
                    If timeMatch IsNot Nothing Then
                        Dim timeString = timeMatch.Groups(1).Value
                        Dim durationValue As TimeSpan = Nothing
                        If TimeSpan.TryParseExact(timeString, "g", CultureInfo.InvariantCulture, durationValue) Then
                            Dim hours = durationValue.TotalHours
                            totalTimeLength = hours
                            dispatcherQueue.TryEnqueue(
                            Sub()
                                vidFile.ProgressMax = hours
                                vidFile.ProgressValue = 0
                            End Sub)
                        End If
                    End If

                Else
                    If Not curLine.Contains("time") Then Return

                    Dim timeMatch = _regGetTime.Matches(curLine).OfType(Of Match).FirstOrDefault
                    If timeMatch IsNot Nothing Then
                        Dim timeString = timeMatch.Groups(1).Value
                        Dim timeValue As TimeSpan = Nothing
                        If TimeSpan.TryParseExact(timeString, "g", CultureInfo.InvariantCulture, timeValue) Then
                            Dim hours = timeValue.TotalHours
                            totalTimeLength = hours

                            dispatcherQueue.TryEnqueue(
                            Sub()
                                vidFile.ProgressIndeterminate = False
                                vidFile.ProgressValue = hours
                            End Sub)
                        End If
                    End If
                End If
            End Sub

            Dim stdErrTask = proc.StandardError.ReadToEndWithLineReportAsync(cancelToken, handleOutput)
            Dim stdOutTask = proc.StandardOutput.ReadToEndWithLineReportAsync(cancelToken, handleOutput)

            Await Task.WhenAll(stdErrTask, stdOutTask).WaitAsync(cancelToken)

            If proc.ExitCode <> 0 Then
                Dim stdErr = stdErrTask.Result, stdOut = stdOutTask.Result
                Dim errMsg = String.Empty
                If stdErr <> Nothing Then
                    errMsg = "Error: " & stdErr.Trim
                End If
                If stdOut <> Nothing Then
                    If errMsg.Length > 0 Then errMsg &= Environment.NewLine
                    errMsg &= "Output: " & stdOut.Trim
                End If
                Interlocked.Exchange(errToPrompt.Value, errMsg)
            End If

            ThrowForExternalException(proc)
        Catch ex As TaskCanceledException
            killingEx = ex
        Catch ex As Exception
            dispatcherQueue.TryEnqueue(Sub() vidFile.Icon = IconExclamation)
            Throw
        Finally
            processGroupManager.Remove(proc)
            dispatcherQueue.TryEnqueue(Sub() vidFile.ProgressVisibility = Visibility.Collapsed)
        End Try

        If killingEx IsNot Nothing Then
            dispatcherQueue.TryEnqueue(Sub() vidFile.Icon = IconExclamation)
            statusCallback("Cleaning canceled file...")
            proc.Kill(True)
            Dim convertedPath = vidFile.Output
            Await DeleteFileWithRetryAsync(convertedPath)
            Throw killingEx
        End If

        timer.Stop()

        dispatcherQueue.TryEnqueue(Sub() vidFile.Icon = IconOk)

        If Volatile.Read(index.Value) < fileListCount - 1 Then
            cancelToken = Await PreventOverheatAsync(statusCallback, timer, cancelToken, softStop)
        End If

        If Volatile.Read(softStop.Value) Then
            statusCallback("Waiting for tasks end...")
            Return
        End If
    End Function

    Private Sub ThrowForExternalException(proc As Process)
        If proc.ExitCode = 0 Then
            Return
        End If

        Throw New Win32Exception($"Call ffmpeg failed. Exit code {proc.ExitCode}")
    End Sub

    Private Async Function DeleteFileWithRetryAsync(convertedPath As String) As Task
        If File.Exists(convertedPath) Then
            Do
                Try
                    File.Delete(convertedPath)
                    Exit Do
                Catch ex As Exception
                End Try
                Await Task.Delay(200)
            Loop
        End If
    End Function

    Private Async Function PreventOverheatAsync(statusCallback As Action(Of String), timer As Stopwatch, cancelToken As Threading.CancellationToken, softStop As StrongBox(Of Boolean)) As Task(Of Threading.CancellationToken)
        Dim waitSec As Integer

        Select Case timer.Elapsed
            Case Is < TimeSpan.FromSeconds(30)
                waitSec = 0
            Case Is < TimeSpan.FromSeconds(60)
                waitSec = 3
            Case Else
                waitSec = 5
                SyncLock timer
                    timer.Reset()
                End SyncLock
        End Select

        If waitSec > 0 Then
            For i = waitSec To 1 Step -1
                statusCallback("Sleeping... " & i)
                For j = 1 To 10
                    If Volatile.Read(softStop.Value) Then Exit For
                    Await Task.Delay(100, cancelToken)
                Next
                If Volatile.Read(softStop.Value) Then Exit For
            Next
        Else
            cancelToken.ThrowIfCancellationRequested()
        End If

        Return cancelToken
    End Function

    Private ReadOnly _allowedExt As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
        ".mp4", ".mkv", ".flv", ".avi", ".wmv", ".mpg", ".mov", ".3gp",
        ".wav", ".mp3", ".aac", ".flac", ".m4a", ".png", ".jpg", ".jpeg"
    }

    Public ReadOnly Property AllowedVideoFileExtensions As IEnumerable(Of String)
        Get
            Return _allowedExt
        End Get
    End Property

    Private Sub TryAddFile(files As List(Of ConvertibleVideo),
                           name As String, dirName As String,
                           filePath As String, activeFormatName As String,
                           activeScriptName As String, fileExtension As String)
        Dim ext = Path.GetExtension(name)
        If Not _allowedExt.Contains(ext) Then Return
        Dim nameNoExt = Path.GetFileNameWithoutExtension(name)
        If nameNoExt.EndsWith($"_{activeFormatName}", StringComparison.OrdinalIgnoreCase) Then Return

        Dim convertedPath = GetConvertedPath(name, dirName, activeFormatName, fileExtension)
        files.Add(New ConvertibleVideo(filePath, convertedPath, activeFormatName, activeScriptName))
    End Sub

    Private Function GetConvertedPath(name As String,
                                      dirName As String, activeFormatName As String, fileExtension As String) As String
        Dim convFileName = $"{Path.GetFileNameWithoutExtension(name)}_{activeFormatName.ToLowerInvariant}{fileExtension}"
        Dim convertedPath = Path.Combine(dirName, convFileName)
        Return convertedPath
    End Function

    Private Sub AddFilesFromDir(files As List(Of ConvertibleVideo),
                                item As IStorageItem, activeFormatName As String,
                                activeScriptName As String, fileExtension As String)
        Dim fileInfo = New DirectoryInfo(item.Path).GetFiles("*", SearchOption.AllDirectories)
        For Each f In fileInfo
            TryAddFile(files, f.Name, f.DirectoryName, f.FullName, activeFormatName, activeScriptName, fileExtension)
        Next
    End Sub
End Module
