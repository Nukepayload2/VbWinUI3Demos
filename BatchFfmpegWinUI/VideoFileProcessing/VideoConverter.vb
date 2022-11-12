Option Strict On

Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports Windows.Storage
Imports Windows.UI.Core
Imports FileAttributes = Windows.Storage.FileAttributes

Module VideoConverter

    Function GetConvertibleVideos(droppedItems As IReadOnlyList(Of IStorageItem), activeFormatName As String) As List(Of ConvertibleVideo)
        Dim files As New List(Of ConvertibleVideo)
        For Each droppedItem In droppedItems
            If droppedItem.Attributes.HasFlag(FileAttributes.Directory) Then
                AddMp4FilesFromDir(files, droppedItem, activeFormatName)
            Else
                Dim filePath = droppedItem.Path
                TryAddMp4File(files, droppedItem.Name, Path.GetDirectoryName(filePath), filePath, activeFormatName)
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
                          cancelToken As Threading.CancellationToken,
                          softStop As StrongBox(Of Boolean)) As Task(Of Boolean)

        Dim errToPrompt As String = Nothing
        Dim success = False
        Try
            Dim index = 1
            Dim timer As New Stopwatch

            For Each vidFile In fileList
                statusCallback($"Converting {index}/{fileList.Count}")
                If File.Exists(vidFile.Output) Then
                    index += 1
                    vidFile.Icon = IconOk
                    Continue For
                End If

                vidFile.Icon = IconProcessing

                Dim procStart As New ProcessStartInfo With {
                    .UseShellExecute = False,
                    .FileName = "cmd",
                    .Arguments = $"/c {vidFile.FormatName}.bat ""{vidFile.Path}"" ""{vidFile.Output}""",
                    .CreateNoWindow = True,
                    .RedirectStandardOutput = True,
                    .RedirectStandardError = True
                }

                Dim proc = Process.Start(procStart)

                timer.Start()

                Dim killingEx As TaskCanceledException = Nothing
                Try
                    Dim totalTimeLength As Double?
                    Dim handleOutput =
                    Sub(curLine As String)
                        If curLine = Nothing OrElse cancelToken.IsCancellationRequested Then
                            vidFile.ProgressVisibility = Microsoft.UI.Xaml.Visibility.Collapsed
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

                                    vidFile.ProgressMax = hours
                                    vidFile.ProgressValue = 0
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

                                    vidFile.ProgressValue = hours
                                    vidFile.ProgressVisibility = Microsoft.UI.Xaml.Visibility.Visible
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
                        errToPrompt = errMsg
                    End If

                    ThrowForExternalException(proc)
                Catch ex As TaskCanceledException
                    killingEx = ex
                Catch ex As Exception
                    vidFile.Icon = IconExclamation
                    Throw
                Finally
                    vidFile.ProgressVisibility = Microsoft.UI.Xaml.Visibility.Collapsed
                End Try

                If killingEx IsNot Nothing Then
                    vidFile.Icon = IconExclamation
                    statusCallback("Cleaning canceled file...")
                    proc.Kill(True)
                    Dim convertedPath = vidFile.Output
                    Await DeleteFileWithRetryAsync(convertedPath)
                    Throw killingEx
                End If

                timer.Stop()

                vidFile.Icon = IconOk

                If index < fileList.Count - 1 Then
                    cancelToken = Await PreventOverheatAsync(statusCallback, timer, cancelToken, softStop)
                End If

                If softStop.Value Then
                    statusCallback("Conversion was canceled")
                    Exit For
                End If

                index += 1
            Next

            If softStop.Value Then
                statusCallback("Stopped")
                Await MsgBoxAsync("Conversion was stopped")
            Else
                statusCallback("Done")
                Await MsgBoxAsync("Mission accomplished")
            End If

            success = True
        Catch ex As TaskCanceledException
            statusCallback("Conversion was terminated")
        Catch ex As Exception
            statusCallback($"Error {ex.GetType.Name}: {ex.Message}")
        End Try

        If errToPrompt IsNot Nothing Then
            Await ShowFfmpegError(errToPrompt)
        End If

        Return success
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
                waitSec = 20
                timer.Reset()
        End Select

        If waitSec > 0 Then
            For i = waitSec To 1 Step -1
                statusCallback("Sleeping... " & i)
                For j = 1 To 10
                    If softStop.Value Then Exit For
                    Await Task.Delay(100, cancelToken)
                Next
                If softStop.Value Then Exit For
            Next
        Else
            cancelToken.ThrowIfCancellationRequested()
        End If

        Return cancelToken
    End Function

    Private ReadOnly _allowedExt As New HashSet(Of String) From {
        ".mp4", ".mkv", ".flv", ".avi", ".wmv", ".mpg", ".mov"
    }

    Private Sub TryAddMp4File(files As List(Of ConvertibleVideo),
                              name As String, dirName As String,
                              filePath As String, activeFormatName As String)
        Dim ext = Path.GetExtension(name)
        If Not _allowedExt.Contains(ext) Then Return
        Dim nameNoExt = Path.GetFileNameWithoutExtension(name)
        If nameNoExt.EndsWith($"_{activeFormatName}", StringComparison.OrdinalIgnoreCase) Then Return

        Dim convertedPath = GetConvertedPath(name, dirName, activeFormatName)
        files.Add(New ConvertibleVideo(filePath, convertedPath, activeFormatName))
    End Sub

    Private Function GetConvertedPath(name As String,
                                      dirName As String, activeFormatName As String) As String
        Dim convFileName = $"{Path.GetFileNameWithoutExtension(name)}_{activeFormatName.ToLowerInvariant}.mp4"
        Dim convertedPath = Path.Combine(dirName, convFileName)
        Return convertedPath
    End Function

    Private Sub AddMp4FilesFromDir(files As List(Of ConvertibleVideo),
                                   item As IStorageItem, activeFormatName As String)
        Dim fileInfo = New DirectoryInfo(item.Path).GetFiles("*", SearchOption.AllDirectories)
        For Each f In fileInfo
            TryAddMp4File(files, f.Name, f.DirectoryName, f.FullName, activeFormatName)
        Next
    End Sub
End Module
