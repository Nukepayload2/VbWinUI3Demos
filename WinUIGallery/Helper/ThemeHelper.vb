' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports Windows.Storage
Imports Microsoft.UI
Imports Microsoft.UI.Windowing
Imports Microsoft.UI.Xaml

#If UNIVERSAL

using Windows.UI.ViewManagement;
using Windows.System;

#End If

Namespace AppUIBasics.Helper
    ''' <summary>
    ''' Class providing functionality around switching and restoring theme settings
    ''' </summary>
    Public Module ThemeHelper
        Private Const SelectedAppThemeKey As String = "SelectedAppTheme"
        Private CurrentApplicationWindow As Window
        ' Keep reference so it does not get optimized/garbage collected
#If UNIVERSAL
        private static UISettings uiSettings;
#End If
        ''' <summary>
        ''' Gets the current actual theme of the app based on the requested theme of the
        ''' root element, or if that value is Default, the requested theme of the Application.
        ''' </summary>
        Public ReadOnly Property ActualTheme As ElementTheme
            Get
                For Each window1 As Window In WindowHelper.ActiveWindows
                    Dim TempVar As Boolean = TypeOf window1.Content Is FrameworkElement
                    Dim rootElement As FrameworkElement = window1.Content
                    If TempVar Then
                        If rootElement.RequestedTheme <> ElementTheme.[Default] Then
                            Return rootElement.RequestedTheme
                        End If
                    End If
                Next

                Return AppUIBasics.App.GetEnum(Of ElementTheme)(App.Current.RequestedTheme.ToString())
            End Get
        End Property
        ''' <summary>
        ''' Gets or sets (with LocalSettings persistence) the RequestedTheme of the root element.
        ''' </summary>
        Public Property RootTheme As ElementTheme
            Get
                For Each window1 As Window In WindowHelper.ActiveWindows
                    Dim TempVar1 As Boolean = TypeOf window1.Content Is FrameworkElement
                    Dim rootElement As FrameworkElement = window1.Content
                    If TempVar1 Then
                        Return rootElement.RequestedTheme
                    End If
                Next

                Return ElementTheme.[Default]
            End Get

            Set(value As ElementTheme)
                For Each window1 As Window In WindowHelper.ActiveWindows
                    Dim TempVar2 As Boolean = TypeOf window1.Content Is FrameworkElement
                    Dim rootElement As FrameworkElement = window1.Content
                    If TempVar2 Then
                        rootElement.RequestedTheme = value
                    End If
                Next

#If Not UNPACKAGED
                ApplicationData.Current.LocalSettings.Values(SelectedAppThemeKey) = value.ToString()
#End If
                UpdateSystemCaptionButtonColors()
            End Set
        End Property
        Public Sub Initialize()
#If Not UNPACKAGED

            ' Save reference as this might be null when the user is in another app
            CurrentApplicationWindow = App.StartupWindow
            Dim savedTheme As String = ApplicationData.Current.LocalSettings.Values(SelectedAppThemeKey)?.ToString()

            If savedTheme IsNot Nothing Then
                RootTheme = AppUIBasics.App.GetEnum(Of ElementTheme)(savedTheme)
            End If
#End If

#If UNIVERSAL

            // Registering to color changes, thus we notice when user changes theme system wide
            uiSettings = new UISettings();
            uiSettings.ColorValuesChanged += UiSettings_ColorValuesChanged;

#End If

        End Sub
#If UNIVERSAL

        private static void UiSettings_ColorValuesChanged(UISettings sender, object args)
        {
            // Make sure we have a reference to our window so we dispatch a UI change
            if (CurrentApplicationWindow != null)
            {
                // Dispatch on UI thread so that we have a current appbar to access and change
                _ = CurrentApplicationWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                        {
                            UpdateSystemCaptionButtonColors();
                        });
            }
        }

#End If

        Public Function IsDarkTheme() As Boolean
            If RootTheme = ElementTheme.[Default] Then
                Return Application.Current.RequestedTheme = ApplicationTheme.Dark
            End If
            Return RootTheme = ElementTheme.Dark
        End Function
        Public Sub UpdateSystemCaptionButtonColors()
#If UNIVERSAL

            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

            if (ThemeHelper.IsDarkTheme())
            {
                titleBar.ButtonForegroundColor = Colors.White;
            }
            else
            {
                titleBar.ButtonForegroundColor = Colors.Black;
            }

#End If

        End Sub
    End Module
End Namespace
