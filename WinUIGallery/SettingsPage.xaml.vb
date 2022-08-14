'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************


Imports AppUIBasics.Helper
Imports System
Imports System.Linq
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.System
Imports Microsoft.UI
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Input
Imports Microsoft.UI.Xaml.Navigation
Imports WinRT
Imports System.Runtime.InteropServices
Imports WinUIGallery.DesktopWap.Helper

#If UNIVERSAL

using Windows.UI.ViewManagement;

#End If

Namespace AppUIBasics
    ''' <summary>
    ''' A page that displays the app's settings.
    ''' </summary>
    Public NotInheritable Partial Class SettingsPage
        Inherits Page
        Public ReadOnly Property Version As String
            Get
                Dim version1 = Windows.ApplicationModel.Package.Current.Id.Version
                Return String.Format("{0}.{1}.{2}.{3}", version1.Major, version1.Minor, version1.Build, version1.Revision)
            End Get
        End Property

        Public Sub New()
            Me.InitializeComponent()
            AddHandler Loaded, AddressOf OnSettingsPageLoaded

            If ElementSoundPlayer.State = ElementSoundPlayerState.[On] Then
                soundToggle.IsOn = True
            End If
            If ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.[On] Then
                spatialSoundBox.IsChecked = True
            End If

#If UNIVERSAL

            screenshotModeToggle.IsOn = UIHelper.IsScreenshotMode;
            screenshotFolderLink.Content = UIHelper.ScreenshotStorageFolder.Path;

#Else

            ScreenshotSettingsGrid.Visibility = Visibility.Collapsed
#End If

        End Sub
        Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            MyBase.OnNavigatedTo(e)
            Dim args As NavigationRootPageArgs = CType(e.Parameter, NavigationRootPageArgs)
            args.NavigationRootPage.NavigationView.Header = "Settings"
        End Sub
        Private Async Sub OnFeedbackButtonClick(sender As Object, e As RoutedEventArgs)
            Await Launcher.LaunchUriAsync(New Uri("feedback-hub:"))
        End Sub
        Private Sub OnSettingsPageLoaded(sender As Object, e As RoutedEventArgs)
            Dim currentTheme = ThemeHelper.RootTheme.ToString()
            ThemePanel.Children.Cast(Of RadioButton)().FirstOrDefault(Function(c) c?.Tag?.ToString() = currentTheme).IsChecked = True

            Dim navigationRootPage1 As NavigationRootPage = NavigationRootPage.GetForElement(Me)
            If navigationRootPage1 IsNot Nothing Then
                If navigationRootPage1.NavigationView.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Auto Then
                    navigationLocation.SelectedIndex = 0
                Else
                    navigationLocation.SelectedIndex = 1
                End If
            End If
        End Sub
        Private Sub OnThemeRadioButtonChecked(sender As Object, e As RoutedEventArgs)
            Dim selectedTheme = CType(sender, RadioButton)?.Tag?.ToString()
#If UNIVERSAL

            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            Action<Windows.UI.Color> SetTitleBarButtonForegroundColor = (Windows.UI.Color color) => { titleBar.ButtonForegroundColor = color; };

#Else

            Dim res = Microsoft.UI.Xaml.Application.Current.Resources
            Dim SetTitleBarButtonForegroundColor As Action(Of Windows.UI.Color) = Sub(color1 As Windows.UI.Color)
                                                                                      res("WindowCaptionForeground") = color1
                                                                                  End Sub
#End If

            If selectedTheme IsNot Nothing Then
                ThemeHelper.RootTheme = App.GetEnum(Of ElementTheme)(selectedTheme)
                If selectedTheme = "Dark" Then
                    SetTitleBarButtonForegroundColor(Colors.White)
                ElseIf selectedTheme = "Light" Then
                    SetTitleBarButtonForegroundColor(Colors.Black)
                Else
                    If Application.Current.RequestedTheme = ApplicationTheme.Dark Then
                        SetTitleBarButtonForegroundColor(Colors.White)
                    Else
                        SetTitleBarButtonForegroundColor(Colors.Black)
                    End If
                End If
            End If

            TitleBarHelper.triggerTitleBarRepaint()
        End Sub
        Private Sub OnThemeRadioButtonKeyDown(sender As Object, e As KeyRoutedEventArgs)
            If e.Key = VirtualKey.Up Then
                NavigationRootPage.GetForElement(Me).PageHeader.Focus(FocusState.Programmatic)
            End If
        End Sub
        Private Sub spatialSoundBox_Checked(sender As Object, e As RoutedEventArgs)
            If soundToggle.IsOn = True Then
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.[On]
            End If
        End Sub
        Private Sub soundToggle_Toggled(sender As Object, e As RoutedEventArgs)
            If soundToggle.IsOn = True Then
                spatialSoundBox.IsEnabled = True
                ElementSoundPlayer.State = ElementSoundPlayerState.[On]
            Else
                spatialSoundBox.IsEnabled = False
                spatialSoundBox.IsChecked = False

                ElementSoundPlayer.State = ElementSoundPlayerState.Off
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.Off
            End If
        End Sub
        Private Sub screenshotModeToggle_Toggled(sender As Object, e As RoutedEventArgs)
            UIHelper.IsScreenshotMode = screenshotModeToggle.IsOn
        End Sub
        Private Sub spatialSoundBox_Unchecked(sender As Object, e As RoutedEventArgs)
            If soundToggle.IsOn = True Then
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.Off
            End If
        End Sub
        Private Sub navigationLocation_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            NavigationOrientationHelper.IsLeftModeForElement(navigationLocation.SelectedIndex = 0, Me)
        End Sub
        Private Async Sub FolderButton_Click(sender As Object, e As RoutedEventArgs)
            Dim folderPicker1 As New FolderPicker
            folderPicker1.SuggestedStartLocation = PickerLocationId.PicturesLibrary
            folderPicker1.FileTypeFilter.Add(".png") ' meaningless, but you have to have something
            Dim folder As StorageFolder = Await folderPicker1.PickSingleFolderAsync()

            If folder IsNot Nothing Then
                UIHelper.ScreenshotStorageFolder = folder
                screenshotFolderLink.Content = UIHelper.ScreenshotStorageFolder.Path
            End If
        End Sub
        Private Async Sub screenshotFolderLink_Click(sender As Object, e As RoutedEventArgs)
            Await Launcher.LaunchFolderAsync(UIHelper.ScreenshotStorageFolder)
        End Sub
        Private Sub OnResetTeachingTipsButtonClick(sender As Object, e As RoutedEventArgs)
            ProtocolActivationClipboardHelper.ShowCopyLinkTeachingTip = True
        End Sub
        Private Sub soundPageHyperlink_Click(sender As Microsoft.UI.Xaml.Documents.Hyperlink, args As Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs)
            Me.Frame.Navigate(GetType(ItemPage), New NavigationRootPageArgs() With
{
                .Parameter = "Sound",
                .NavigationRootPage = NavigationRootPage.GetForElement(Me)})
        End Sub
    End Class
End Namespace
