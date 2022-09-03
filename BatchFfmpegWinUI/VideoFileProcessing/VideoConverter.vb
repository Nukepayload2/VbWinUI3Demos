Option Strict On

Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.CompilerServices
Imports Windows.Storage
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

    Async Function ConvertAsync(fileList As IReadOnlyList(Of ConvertibleVideo),
                          statusCallback As Action(Of String),
                          cancelToken As Threading.CancellationToken,
                          softStop As StrongBox(Of Boolean),
                          formatName As String) As Task(Of Boolean)

        Dim success = False
        Try
            Dim index = 1
            Dim timer As New Stopwatch

            For Each vidFile In fileList
                statusCallback($"Converting {index}/{fileList.Count}")
                If File.Exists(vidFile.Output) Then
                    index += 1
                    vidFile.Icon = ChrW(&HE001)
                    Continue For
                End If

                vidFile.Icon = ChrW(&HE768)

                Dim procStart As New ProcessStartInfo With {
                    .UseShellExecute = False,
                    .FileName = "cmd",
                    .Arguments = $"/c {formatName}.bat ""{vidFile.Path}"" ""{vidFile.Output}""",
                    .CreateNoWindow = True,
                    .RedirectStandardOutput = True,
                    .RedirectStandardError = True
                }

                Dim proc = Process.Start(procStart)

                timer.Start()

                Dim killingEx As TaskCanceledException = Nothing
                Try
                    Dim stdErrTask = proc.StandardError.ReadToEndAsync(cancelToken)
                    Dim stdOutTask = proc.StandardOutput.ReadToEndAsync(cancelToken)
                    Await Task.WhenAll(stdErrTask, stdOutTask).WaitAsync(cancelToken)
                    ThrowForExternalException(proc, stdErrTask.Result, stdOutTask.Result)
                Catch ex As TaskCanceledException
                    killingEx = ex
                End Try

                If killingEx IsNot Nothing Then
                    statusCallback("Cleaning canceled file...")
                    proc.Kill(True)
                    Dim convertedPath = vidFile.Output
                    Await DeleteFileWithRetryAsync(convertedPath)
                    Throw killingEx
                End If

                timer.Stop()

                vidFile.Icon = ChrW(&HE001)

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

        Return success
    End Function

    Private Sub ThrowForExternalException(proc As Process, stdErr As String, stdOut As String)
        If proc.ExitCode = 0 Then
            Return
        End If

        Dim errMsg = String.Empty
        If stdErr <> Nothing Then
            errMsg = "Error: " & stdErr.Trim
        End If
        If stdOut <> Nothing Then
            If errMsg.Length > 0 Then errMsg &= Environment.NewLine
            errMsg &= "Output: " & stdOut.Trim
        End If
        Throw New Win32Exception($"Call ffmpeg failed. {Environment.NewLine}{errMsg}")
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
        files.Add(New ConvertibleVideo With {.Path = filePath, .Output = convertedPath})
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
