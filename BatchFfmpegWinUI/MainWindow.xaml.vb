Option Strict On

Imports System.Collections.ObjectModel
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Threading
Imports Microsoft.UI
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Input
Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Foundation

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

            If _fileList Is Nothing OrElse _replaceListOnAdd Then
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
    Private _replaceListOnAdd As Boolean
    Private ReadOnly _processGroupManager As New FfmpegPerformanceManager

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
                                   DispatcherQueue, _processGroupManager)
                If succeed AndAlso Not _convSoftCancel.Value Then
                    _replaceListOnAdd = True
                End If
                DispatcherQueue.TryEnqueue(
                Sub()
                    _convertStatusCode = ConvertStatusCode.Idle
                    ConvertingFiles.CanReorderItems = True
                    _convSoftCancel = Nothing
                    _convHardCancel = Nothing
                    BtnConvertStop.Content = "Convert"
                End Sub)
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
                e.Handled = DeleteSelected()
        End Select
    End Sub

    Private Function DeleteSelected() As Boolean
        If _fileList Is Nothing Then Return False
        Dim handled = False
        For Each selItem In ConvertingFiles.SelectedItems.OfType(Of ConvertibleVideo).ToArray
            _fileList.Remove(selItem)
            handled = True
        Next
        Return handled
    End Function

    Private Sub BtnDelSelected_Click(sender As Object, e As RoutedEventArgs) Handles BtnDelSelected.Click
        DeleteSelected()
    End Sub

    Private Sub BtnInvertSelection_Click(sender As Object, e As RoutedEventArgs) Handles BtnInvertSelection.Click
        If _fileList Is Nothing Then Return
        Dim selectedItems = ConvertingFiles.SelectedItems
        Dim selectedSet = selectedItems.ToHashSet
        selectedItems.Clear()
        For Each f In _fileList
            If Not selectedSet.Contains(f) Then
                selectedItems.Add(f)
            End If
        Next
    End Sub

    Private Sub BtnPreferECores_Click(sender As Object, e As RoutedEventArgs) Handles BtnPreferECores.Click
        _processGroupManager.PreferECores()
        BtnPreferECores.Visibility = Visibility.Collapsed
        BtnPreferPCores.Visibility = Visibility.Visible
    End Sub

    Private Sub BtnPreferPCores_Click(sender As Object, e As RoutedEventArgs) Handles BtnPreferPCores.Click
        _processGroupManager.PreferPCores()
        BtnPreferPCores.Visibility = Visibility.Collapsed
        BtnPreferECores.Visibility = Visibility.Visible
    End Sub

    Private Sub BtnSelectAll_Click(sender As Object, e As RoutedEventArgs) Handles BtnSelectAll.Click
        If _fileList Is Nothing Then Return
        Dim selectedItems = ConvertingFiles.SelectedItems
        For Each f In _fileList
            selectedItems.Add(f)
        Next
    End Sub

    Private Async Sub BtnCleanConverted_Click() Handles BtnCleanConverted.Click
        Dim folderPicker As New System.Windows.Forms.FolderBrowserDialog
        Dim folder = If(folderPicker.ShowDialog = System.Windows.Forms.DialogResult.OK,
            folderPicker.SelectedPath, Nothing)
        If folder Is Nothing Then Return
        Dim currentPostfix = "_" & DirectCast(CmbCurFormat.SelectedItem, VideoFormatReference).Name & ".mp4"
        Dim filesInFolder = New DirectoryInfo(folder).GetFiles()
        Dim fileNoExtCache =
            Aggregate f In filesInFolder
            Select Path.Combine(folderPicker.SelectedPath,
                                Path.GetFileNameWithoutExtension(f.Name))
            Into ToHashSet
        Dim removeCandidate =
            (From f In filesInFolder
             Where f.Name.EndsWith(currentPostfix, StringComparison.OrdinalIgnoreCase) AndAlso f.Length > 0
             Let fullName = f.FullName,
                 candidateNoExt = fullName.Substring(0, fullName.Length - currentPostfix.Length)
             Where fileNoExtCache.Contains(candidateNoExt)
             Select candidates = (From ext In AllowedVideoFileExtensions
                                  Let candidate = candidateNoExt & ext
                                  Where File.Exists(candidate)
                                  Select candidate)
             ).SelectMany(Function(it) it).ToArray

        Dim myComputer As New Devices.Computer
        Dim recycleCount = 0, errorCount = 0
        Dim lastError As Exception = Nothing
        Dim showProgressDialog = removeCandidate.Length > 10
        Dim showDialogTask As Task = Nothing
        Dim progressDialog As ProgressDialog = Nothing
        If showProgressDialog Then
            progressDialog = New ProgressDialog With {
                .Title = $"Recycling {removeCandidate.Length} files. Sit back and relax."
            }
            showDialogTask = progressDialog.ShowDialogAsync(
                New DialogOptions With {
                    .Width = 832, .Height = 233,
                    .CenterOwner = True, .Maximizable = False,
                    .Minimizable = False, .Resizable = False
                })
        End If

        Dim processedFileCount = 0
        For Each f In removeCandidate
            If progressDialog IsNot Nothing Then
                progressDialog.DispatcherQueue.TryEnqueue(
                    Sub() progressDialog.UpdateProgress(
                        $"Recycling {processedFileCount + 1} of {removeCandidate.Length} files.
{f}", processedFileCount, removeCandidate.Length))
                Await Task.Delay(10)
            End If
            Try
                myComputer.FileSystem.DeleteFile(f, FileIO.UIOption.OnlyErrorDialogs,
                                                 FileIO.RecycleOption.SendToRecycleBin)
                recycleCount += 1
            Catch ex As Exception
                errorCount += 1
                lastError = ex
            End Try
            processedFileCount += 1
        Next

        If progressDialog IsNot Nothing Then
            progressDialog.DispatcherQueue.TryEnqueue(
                Sub() progressDialog.Close())
            Await showDialogTask
        End If

        Await MsgBoxAsync($"Recycled {recycleCount} file(s), 
Error {errorCount} file(s), 
Last error is {If(lastError Is Nothing, "nothing", lastError.Message)}.", "Recycle Result")
    End Sub
End Class
