Option Strict On

Imports System.Runtime.CompilerServices
Imports System.Threading
Imports Microsoft.UI.Xaml
Imports Windows.ApplicationModel.DataTransfer

Public Class MainWindow
    Inherits Window

    Private Sub LayoutRoot_DragEnter(sender As Object, e As DragEventArgs) Handles LayoutRoot.DragEnter
        If _convertStatusCode <> ConvertStatusCode.Idle Then
            Return
        End If

        e.AcceptedOperation = DataPackageOperation.Link
    End Sub

    Private _fileList As List(Of ConvertibleVideo)
    Private _convertStatusCode As ConvertStatusCode

    Private Async Sub LayoutRoot_Drop(sender As Object, e As DragEventArgs) Handles LayoutRoot.Drop
        If _convertStatusCode <> ConvertStatusCode.Idle Then
            Return
        End If

        Dim dataView = e.DataView
        Dim def = e.GetDeferral
        Dim curErr As Exception = Nothing
        Try
            Dim droppedItems = Await dataView.GetStorageItemsAsync
            _fileList = GetConvertibleVideos(droppedItems)
            ConvertingFiles.ItemsSource = _fileList
        Catch ex As Exception
            curErr = ex
        End Try

        If curErr IsNot Nothing Then
            Await MsgBoxAsync("Error: " & curErr.Message)
        End If
        def.Complete()
    End Sub

    Private _convHardCancel As CancellationTokenSource
    Private _convSoftCancel As StrongBox(Of Boolean)
    Private Async Sub BtnConvertStop_Click(sender As Object, e As RoutedEventArgs) Handles BtnConvertStop.Click
        Select Case _convertStatusCode
            Case ConvertStatusCode.Idle
                If _fileList Is Nothing Then
                    ConvertStatus.Text = "Drop files and try again"
                End If
                BtnConvertStop.Content = "Stop after this file"
                _convertStatusCode = ConvertStatusCode.Converting
                _convHardCancel = New CancellationTokenSource
                _convSoftCancel = New StrongBox(Of Boolean)
                Await ConvertAsync(_fileList, Sub(status) ConvertStatus.Text = status,
                                   _convHardCancel.Token, _convSoftCancel)
                _convertStatusCode = ConvertStatusCode.Idle
                _convSoftCancel = Nothing
                _convHardCancel = Nothing
                BtnConvertStop.Content = "Convert"
            Case ConvertStatusCode.Converting
                If _convSoftCancel IsNot Nothing Then
                    _convertStatusCode = ConvertStatusCode.StopRequested
                    _convSoftCancel.Value = True
                    _convSoftCancel = Nothing
                    BtnConvertStop.Content = "Terminate"
                End If
            Case ConvertStatusCode.StopRequested
                If _convHardCancel IsNot Nothing Then
                    _convHardCancel.Cancel()
                    _convHardCancel = Nothing
                End If
        End Select
    End Sub

End Class
