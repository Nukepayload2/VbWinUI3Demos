Option Strict On

Imports System.Collections.ObjectModel
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports Microsoft.UI
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Input
Imports Windows.ApplicationModel.DataTransfer

Public Class MainWindow
    Inherits Window

    Private ReadOnly _backdrop As BackdropHelper

    Sub New()

        Title = "WinUI 3 VB Demo - mp4 converter"

        InitializeComponent()

        _backdrop = New BackdropHelper(Me)
        Dim useAcrylic = False
        _backdrop.SetBackdrop(BackdropType.Mica, useAcrylic)

        TryCustomizeTitleBar(useAcrylic)

        FileListTip.Target = ConvertingFiles
    End Sub

    Private Sub TryCustomizeTitleBar(useAcrylic As Boolean)
        Dim appWnd = GetAppWindow
        Dim titleBar = appWnd.TitleBar
        If titleBar IsNot Nothing Then
            ' Windows 11 or Windows 10
            With appWnd.TitleBar
                .ExtendsContentIntoTitleBar = True
                .ButtonBackgroundColor = Colors.Transparent
            End With
            Dim titleBarHeight = appWnd.GetTitleBarHeight
            LayoutRoot.RowDefinitions(0).Height = New GridLength(titleBarHeight + 4, GridUnitType.Pixel)
            If Not useAcrylic Then
                LayoutRoot.Background = Nothing
                ConvertingFiles.Background = Nothing
            End If
        Else
            ' Windows 10
            TblTitleText.Visibility = Visibility.Collapsed
        End If
    End Sub

    Private _loaded As Boolean
    Private Sub MainWindow_Activated(sender As Object, args As WindowActivatedEventArgs) Handles Me.Activated
        WinUIVbHost.Instance.CurrentWindow = Me

        If _loaded Then Return
        _loaded = True

        If Not _tipsCompleted Then
            FileListTip.IsOpen = True
        End If
    End Sub

    Private Sub LayoutRoot_DragEnter(sender As Object, e As DragEventArgs) Handles LayoutRoot.DragEnter
        If _convertStatusCode <> ConvertStatusCode.Idle Then
            Return
        End If

        e.AcceptedOperation = DataPackageOperation.Link
    End Sub

    Private _fileList As ObservableCollection(Of ConvertibleVideo)
    Private _convertStatusCode As ConvertStatusCode
    Private _tipsCompleted As Boolean

    Private Async Sub LayoutRoot_Drop(sender As Object, e As DragEventArgs) Handles LayoutRoot.Drop
        If _convertStatusCode <> ConvertStatusCode.Idle Then
            Return
        End If

        Dim dataView = e.DataView
        Dim def = e.GetDeferral
        Dim curErr As Exception = Nothing
        Try
            Dim droppedItems = Await dataView.GetStorageItemsAsync
            Dim files = GetConvertibleVideos(droppedItems, GetActiveFormatName)
            If files.Count > 0 AndAlso Not _tipsCompleted Then
                FileListTip.IsOpen = False
                ConvertTip.Target = BtnConvertStop
                ConvertTip.IsOpen = True
                _tipsCompleted = True
            End If

            If _fileList Is Nothing Then
                _fileList = New ObservableCollection(Of ConvertibleVideo)(files)
                ConvertingFiles.ItemsSource = _fileList
            Else
                For Each f In files
                    _fileList.Add(f)
                Next
            End If
        Catch ex As Exception
            curErr = ex
        End Try

        If curErr IsNot Nothing Then
            Await MsgBoxAsync("Error: " & curErr.Message)
        End If
        def.Complete()
    End Sub

    Private Function GetActiveFormatName() As String
        Return If(TryCast(CmbCurFormat.SelectedItem, VideoFormatReference).Name, "h265")
    End Function

    Private _convHardCancel As CancellationTokenSource
    Private _convSoftCancel As StrongBox(Of Boolean)
    Private Async Sub BtnConvertStop_Click(sender As Object, e As RoutedEventArgs) Handles BtnConvertStop.Click
        Select Case _convertStatusCode
            Case ConvertStatusCode.Idle
                If _fileList Is Nothing Then
                    ConvertStatus.Text = "Drop files and try again"
                    Return
                End If

                BtnConvertStop.Content = "Stop after this file"
                _convertStatusCode = ConvertStatusCode.Converting
                ConvertTip.IsOpen = False
                ConvertingFiles.CanReorderItems = False
                _convHardCancel = New CancellationTokenSource
                _convSoftCancel = New StrongBox(Of Boolean)
                Dim succeed = Await ConvertAsync(_fileList, Sub(status) ConvertStatus.DispatcherQueue.TryEnqueue(
                                                            Sub() ConvertStatus.Text = status),
                                   _convHardCancel.Token, _convSoftCancel, CmbMaxConverterThread.SelectedIndex + 1,
                                   DispatcherQueue)
                _convertStatusCode = ConvertStatusCode.Idle
                ConvertingFiles.CanReorderItems = True
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

    Private Sub ConvertingFiles_KeyDown(sender As Object, e As KeyRoutedEventArgs) Handles ConvertingFiles.KeyDown
        If _convertStatusCode <> ConvertStatusCode.Idle Then Return

        Select Case e.Key
            Case Windows.System.VirtualKey.Delete
                If _fileList Is Nothing Then Return
                For Each selItem In ConvertingFiles.SelectedItems.OfType(Of ConvertibleVideo).ToArray
                    _fileList.Remove(selItem)
                    e.Handled = True
                Next
        End Select
    End Sub
End Class
