' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Windows.ApplicationModel.Core
Imports Windows.Storage
Imports Microsoft.UI
Imports Microsoft.UI.Windowing
Imports Microsoft.UI.Xaml

#If UNIVERSAL

using Windows.UI.ViewManagement;

#End If

Namespace AppUIBasics.Helper
    Public Module NavigationOrientationHelper
        Private Const IsLeftModeKey As String = "NavigationIsOnLeftMode"
#If UNPACKAGED

        private static bool _isLeftMode = true;

#End If

        Public Function IsLeftMode() As Boolean
#If Not UNPACKAGED

            Dim valueFromSettings = ApplicationData.Current.LocalSettings.Values(IsLeftModeKey)
            If valueFromSettings Is Nothing Then
                ApplicationData.Current.LocalSettings.Values(IsLeftModeKey) = True
                valueFromSettings = True
            End If
            Return CBool(valueFromSettings)
#Else

            return _isLeftMode;

#End If

        End Function
        Public Sub IsLeftModeForElement(isLeftMode1 As Boolean, element As UIElement)
            UpdateTitleBarForElement(isLeftMode1, element)
#If Not UNPACKAGED

            ApplicationData.Current.LocalSettings.Values(IsLeftModeKey) = isLeftMode1
#Else

            _isLeftMode = isLeftMode;

#End If

        End Sub
        Public Sub UpdateTitleBarForElement(isLeftMode1 As Boolean, element As UIElement)
#If UNIVERSAL

            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = isLeftMode;

            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

#End If

            If isLeftMode1 Then
                NavigationRootPage.GetForElement(element).NavigationView.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Auto
            Else
                NavigationRootPage.GetForElement(element).NavigationView.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Top
#If UNIVERSAL

                var userSettings = new UISettings();
                titleBar.ButtonBackgroundColor = userSettings.GetColorValue(UIColorType.Accent);
                titleBar.ButtonInactiveBackgroundColor = userSettings.GetColorValue(UIColorType.Accent);

#End If

            
#If UNIVERSAL

                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

#End If

            End If
        End Sub
    End Module
End Namespace
