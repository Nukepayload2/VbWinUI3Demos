'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************


Imports System.Collections.Generic
Imports Windows.Foundation.Metadata
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Provider
Imports Microsoft.UI.Text
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Input
Imports Microsoft.UI.Xaml.Media
Imports Microsoft.UI
Imports System.Runtime.InteropServices

#If Not UNIVERSAL

Imports WinRT
#End If

Namespace AppUIBasics.ControlPages
    <ComImport, Runtime.InteropServices.Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
    Public Interface IInitializeWithWindow
        Sub Initialize(<[In]> hwnd As IntPtr)
    End Interface


    Public NotInheritable Partial Class RichEditBoxPage
        Inherits Page
        <DllImport("user32.dll", ExactSpelling:=True, CharSet:=CharSet.Auto, PreserveSig:=True, SetLastError:=False)>
        Public Shared Function GetActiveWindow() As IntPtr
        End Function
        Private currentColor As Windows.UI.Color = Microsoft.UI.Colors.Green

        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub Menu_Opening(sender As Object, e As Object)
            Dim myFlyout As CommandBarFlyout = TryCast(sender, CommandBarFlyout)
            If myFlyout IsNot Nothing AndAlso myFlyout.Target = REBCustom Then
                Dim myButton As New AppBarButton With
    { _
                .Command = New StandardUICommand(StandardUICommandKind.Share)
    }
                myFlyout.PrimaryCommands.Add(myButton)
            Else
                Dim muxFlyout As CommandBarFlyout = TryCast(sender, CommandBarFlyout)
                If muxFlyout IsNot Nothing AndAlso muxFlyout.Target = REBCustom Then
                    Dim myButton As New AppBarButton With
    { _
                    .Command = New StandardUICommand(StandardUICommandKind.Share)
    }
                    muxFlyout.PrimaryCommands.Add(myButton)
                End If
            End If
        End Sub
        Private Async Sub OpenButton_Click(sender As Object, e As RoutedEventArgs)
            ' Open a text file.
            Dim open As New FileOpenPicker
            open.SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            open.FileTypeFilter.Add(".rtf")

#If Not UNIVERSAL

            ' When running on win32, FileOpenPicker needs to know the top-level hwnd via IInitializeWithWindow::Initialize.
            If Window.Current Is Nothing Then
                Dim initializeWithWindowWrapper As IInitializeWithWindow = open.[As](Of IInitializeWithWindow)()
                Dim hwnd As IntPtr = GetActiveWindow()
                initializeWithWindowWrapper.Initialize(hwnd)
            End If
#End If

            Dim file As StorageFile = Await open.PickSingleFileAsync()

            If file IsNot Nothing Then
                Using randAccStream As Windows.Storage.Streams.IRandomAccessStream = Await file.OpenAsync(Windows.Storage.FileAccessMode.Read)
                    ' Load the file into the Document property of the RichEditBox.
                    editor.Document.LoadFromStream(TextSetOptions.FormatRtf, randAccStream)
                End Using
            End If
        End Sub
        Private Async Sub SaveButton_Click(sender As Object, e As RoutedEventArgs)
            Dim savePicker As New FileSavePicker With
            { _
            .SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            }

            ' Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Rich Text", New List(Of String) From {
                ".rtf"})

            ' Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Document"

#If Not UNIVERSAL

            ' When running on win32, FileSavePicker needs to know the top-level hwnd via IInitializeWithWindow::Initialize.
            If Window.Current Is Nothing Then
                Dim initializeWithWindowWrapper As IInitializeWithWindow = savePicker.[As](Of IInitializeWithWindow)()
                Dim hwnd As IntPtr = GetActiveWindow()
                initializeWithWindowWrapper.Initialize(hwnd)
            End If
#End If

            Dim file As StorageFile = Await savePicker.PickSaveFileAsync()
            If file IsNot Nothing Then
                ' Prevent updates to the remote version of the file until we
                ' finish making changes and call CompleteUpdatesAsync.
                CachedFileManager.DeferUpdates(file)
                Using randAccStream As Windows.Storage.Streams.IRandomAccessStream = Await file.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite)
                    editor.Document.SaveToStream(TextGetOptions.FormatRtf, randAccStream)
                End Using

                ' Let Windows know that we're finished changing the file so the
                ' other app can update the remote version of the file.
                Dim status As FileUpdateStatus = Await CachedFileManager.CompleteUpdatesAsync(file)
                If status <> FileUpdateStatus.Complete Then
                    Dim errorBox As New Windows.UI.Popups.MessageDialog("File " & file.Name & " couldn't be saved.")
                    Await errorBox.ShowAsync()
                End If
            End If
        End Sub
        Private Sub BoldButton_Click(sender As Object, e As RoutedEventArgs)
            editor.Document.Selection.CharacterFormat.Bold = FormatEffect.Toggle
        End Sub
        Private Sub ItalicButton_Click(sender As Object, e As RoutedEventArgs)
            editor.Document.Selection.CharacterFormat.Italic = FormatEffect.Toggle
        End Sub
        Private Sub ColorButton_Click(sender As Object, e As RoutedEventArgs)
            ' Extract the color of the button that was clicked.
            Dim clickedColor As Button = CType(sender, Button)
            Dim rectangle1 = CType(clickedColor.Content, Microsoft.UI.Xaml.Shapes.Rectangle)
            Dim color1 = CType(rectangle1.Fill, Microsoft.UI.Xaml.Media.SolidColorBrush).Color

            editor.Document.Selection.CharacterFormat.ForegroundColor = color1

            fontColorButton.Flyout.Hide()
            editor.Focus(Microsoft.UI.Xaml.FocusState.Keyboard)
            currentColor = color1
        End Sub
        Private Sub FindBoxHighlightMatches()
            FindBoxRemoveHighlights()

            Dim highlightBackgroundColor As Windows.UI.Color = CType(App.Current.Resources("SystemColorHighlightColor"), Windows.UI.Color)
            Dim highlightForegroundColor As Windows.UI.Color = CType(App.Current.Resources("SystemColorHighlightTextColor"), Windows.UI.Color)

            Dim textToFind As String = findBox.Text
            If textToFind IsNot Nothing Then
                Dim searchRange As ITextRange = editor.Document.GetRange(0, 0)
                While searchRange.FindText(textToFind, TextConstants.MaxUnitCount, FindOptions.None) > 0
                    searchRange.CharacterFormat.BackgroundColor = highlightBackgroundColor
                    searchRange.CharacterFormat.ForegroundColor = highlightForegroundColor
                End While
            End If
        End Sub
        Private Sub FindBoxRemoveHighlights()
            Dim documentRange As ITextRange = editor.Document.GetRange(0, TextConstants.MaxUnitCount)
            Dim defaultBackground As SolidColorBrush = TryCast(editor.Background, SolidColorBrush)
            Dim defaultForeground As SolidColorBrush = TryCast(editor.Foreground, SolidColorBrush)

            documentRange.CharacterFormat.BackgroundColor = defaultBackground.Color
            documentRange.CharacterFormat.ForegroundColor = defaultForeground.Color
        End Sub
        Private Sub Editor_GotFocus(sender As Object, e As RoutedEventArgs)
            Dim currentRawText As String = Nothing
            editor.Document.GetText(TextGetOptions.UseCrlf, currentRawText)

            ' reset colors to correct defaults for Focused state
            Dim documentRange As ITextRange = editor.Document.GetRange(0, TextConstants.MaxUnitCount)
            Dim background1 As SolidColorBrush = CType(App.Current.Resources("TextControlBackgroundFocused"), SolidColorBrush)

            If background1 IsNot Nothing Then
                documentRange.CharacterFormat.BackgroundColor = background1.Color
            End If
        End Sub
        Private Sub Page_SizeChanged(sender As Object, e As SizeChangedEventArgs)
            If e.NewSize.Width <= 768 Then
                editor.Width = e.NewSize.Width - 20
            Else
                editor.Width = e.NewSize.Width - 100
            End If
        End Sub
        Private Sub REBCustom_Loaded(sender As Object, e As RoutedEventArgs)
            ' Prior to UniversalApiContract 7, RichEditBox did not have a default ContextFlyout set.
            If ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7) Then
                ' customize the menu that opens on text selection
                AddHandler REBCustom.SelectionFlyout.Opening, AddressOf Menu_Opening

                ' also customize the context menu to match selection menu
                AddHandler REBCustom.ContextFlyout.Opening, AddressOf Menu_Opening
            End If
        End Sub
        Private Sub REBCustom_Unloaded(sender As Object, e As RoutedEventArgs)
            ' Prior to UniversalApiContract 7, RichEditBox did not have a default ContextFlyout set.
            If ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7) Then
                RemoveHandler REBCustom.SelectionFlyout.Opening, AddressOf Menu_Opening
                RemoveHandler REBCustom.ContextFlyout.Opening, AddressOf Menu_Opening
            End If
        End Sub
        Private Sub Editor_TextChanged(sender As Object, e As RoutedEventArgs)
            If editor.Document.Selection.CharacterFormat.ForegroundColor <> currentColor Then
                editor.Document.Selection.CharacterFormat.ForegroundColor = currentColor
            End If
        End Sub
    End Class
End Namespace
