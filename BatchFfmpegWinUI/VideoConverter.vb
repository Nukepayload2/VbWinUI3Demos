Option Strict On

Imports System.IO
Imports System.Runtime.CompilerServices
Imports Windows.Storage
Imports FileAttributes = Windows.Storage.FileAttributes

Module VideoConverter

    Function GetConvertibleVideos(droppedItems As IReadOnlyList(Of IStorageItem)) As List(Of ConvertibleVideo)
        Dim files As New List(Of ConvertibleVideo)
        For Each droppedItem In droppedItems
            If droppedItem.Attributes.HasFlag(FileAttributes.Directory) Then
                AddMp4FilesFromDir(files, droppedItem)
            Else
                Dim filePath = droppedItem.Path
                TryAddMp4File(files, droppedItem.Name, Path.GetDirectoryName(filePath), filePath)
            End If
        Next
        Return files
    End Function

    Async Function ConvertAsync(fileList As List(Of ConvertibleVideo),
                          statusCallback As Action(Of String),
                          cancelToken As Threading.CancellationToken,
                          softStop As StrongBox(Of Boolean)) As Task
        Try
            Dim index = 1
            Dim timer As New Stopwatch
            For Each vidFile In fileList
                statusCallback($"Converting {index}/{fileList.Count}")
                If File.Exists(vidFile.Output) Then Continue For

                Dim procStart As New ProcessStartInfo With {
                    .UseShellExecute = False,
                    .FileName = "cmd",
                    .Arguments = $"/c H265.bat ""{vidFile.Path}""",
                    .WindowStyle = ProcessWindowStyle.Minimized
                }

                Dim proc = Process.Start(procStart)

                timer.Start()

                Try
                    Await proc.WaitForExitAsync(cancelToken)
                Catch ex As TaskCanceledException
                    proc.Kill(True)
                    Dim convertedPath = vidFile.Output
                    If File.Exists(convertedPath) Then
                        File.Delete(convertedPath)
                    End If

                    Throw
                End Try

                timer.Stop()

                cancelToken = Await PreventOverheatAsync(statusCallback, timer, cancelToken, softStop)

                If softStop.Value Then
                    statusCallback("Conversion was canceled")
                    Exit For
                End If

                index += 1
            Next

            statusCallback("Done")
            Await MsgBoxAsync("Mission accomplished")
        Catch ex As TaskCanceledException
            statusCallback("Conversion was terminated")
        Catch ex As Exception
            statusCallback($"Error {ex.GetType.Name}: {ex.Message}")
        End Try
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
                If softStop.Value Then
                    Exit For
                End If
                statusCallback("Sleeping... " & i)
                Await Task.Delay(1000, cancelToken)
            Next
        Else
            cancelToken.ThrowIfCancellationRequested()
        End If

        Return cancelToken
    End Function

    Private Sub TryAddMp4File(files As List(Of ConvertibleVideo),
                              name As String, dirName As String, filePath As String)
        If Not name.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase) Then Return
        If name.EndsWith("_h265.mp4", StringComparison.OrdinalIgnoreCase) Then Return

        Dim convertedPath = GetConvertedPath(name, dirName)
        files.Add(New ConvertibleVideo With {.Path = filePath, .Output = convertedPath})
    End Sub

    Private Function GetConvertedPath(name As String, dirName As String) As String
        Dim convFileName = Path.GetFileNameWithoutExtension(name) & "_h265.mp4"
        Dim convertedPath = Path.Combine(dirName, convFileName)
        Return convertedPath
    End Function

    Private Sub AddMp4FilesFromDir(files As List(Of ConvertibleVideo), item As IStorageItem)
        Dim fileInfo = New DirectoryInfo(item.Path).GetFiles("*.mp4", SearchOption.AllDirectories)
        For Each f In fileInfo
            TryAddMp4File(files, f.Name, f.DirectoryName, f.FullName)
        Next
    End Sub
End Module
