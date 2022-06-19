Option Strict On

Imports System.Collections.ObjectModel
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports Microsoft.UI
Imports Microsoft.UI.Xaml
Imports Windows.ApplicationModel.DataTransfer

Public Class MainWindow
    Inherits Window

    Private ReadOnly _backdrop As BackdropHelper

    Sub New()

        Title = "WinUI 3 VB Demo - H265 mp4 converter"

        InitializeComponent()

        _backdrop = New BackdropHelper(Me)
        _backdrop.SetBackdrop(BackdropType.Mica)

        TryCustomizeTitleBar()
    End Sub

    Private Sub TryCustomizeTitleBar()
        Dim appWnd = GetAppWindow
        Dim titleBar = appWnd.TitleBar
        If titleBar IsNot Nothing Then
            ' Windows 11
            With appWnd.TitleBar
                .ExtendsContentIntoTitleBar = True
                .ButtonBackgroundColor = Colors.Transparent
            End With
            Dim titleBarHeight = appWnd.GetTitleBarHeight
            LayoutRoot.RowDefinitions(0).Height = New GridLength(titleBarHeight + 4, GridUnitType.Pixel)
        Else
            ' Windows 10
            TblTitleText.Visibility = Visibility.Collapsed
        End If
    End Sub

    Private Sub MainWindow_Activated(sender As Object, args As WindowActivatedEventArgs) Handles Me.Activated
        WinUIVbHost.Instance.CurrentWindow = Me
    End Sub

    Private Sub LayoutRoot_DragEnter(sender As Object, e As DragEventArgs) Handles LayoutRoot.DragEnter
        If _convertStatusCode <> ConvertStatusCode.Idle Then
            Return
        End If

        e.AcceptedOperation = DataPackageOperation.Link
    End Sub

    Private _fileList As ObservableCollection(Of ConvertibleVideo)
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
            Dim files = GetConvertibleVideos(droppedItems)
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
                _convHardCancel = New CancellationTokenSource
                _convSoftCancel = New StrongBox(Of Boolean)
                Dim succeed = Await ConvertAsync(_fileList, Sub(status) ConvertStatus.Text = status,
                                   _convHardCancel.Token, _convSoftCancel)
                If succeed Then
                    _fileList = Nothing
                End If
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
