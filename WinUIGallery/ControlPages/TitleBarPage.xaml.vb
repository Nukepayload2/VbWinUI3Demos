'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************


Imports Microsoft
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Runtime.InteropServices.WindowsRuntime
Imports Microsoft.UI
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Data
Imports Microsoft.UI.Xaml.Input
Imports Microsoft.UI.Xaml.Media
Imports Microsoft.UI.Xaml.Navigation
Imports Windows.Foundation
Imports Windows.Foundation.Collections
Imports WinRT
Imports System.Runtime.InteropServices
Imports WinUIGallery.DesktopWap.Helper

' To learn more about WinUI, the WinUI project structure,
' and more about our project templates, see: http://aka.ms/winui-project-info.

Namespace AppUIBasics.ControlPages


    ''' <summary>
    ''' An empty page that can be used on its own or navigated to within a Frame.
    ''' </summary>
    Public NotInheritable Partial Class TitleBarPage
        Inherits Page
        Private currentBgColor As Windows.UI.Color = Colors.Transparent
        Private currentFgColor As Windows.UI.Color = Colors.Black

        Public Sub New()
            Me.InitializeComponent()
            UpdateTitleBarColor()
            UpdateButtonText()
        End Sub
        Private Sub SetTitleBar(titlebar As UIElement)
            Dim window = App.StartupWindow
            If Not window.ExtendsContentIntoTitleBar Then
                window.ExtendsContentIntoTitleBar = True
                window.SetTitleBar(titlebar)
            Else
                window.ExtendsContentIntoTitleBar = False
                window.SetTitleBar(Nothing)
            End If
            UpdateButtonText()
            UpdateTitleBarColor()
        End Sub
        Private Sub UpdateButtonText()
            Dim window = App.StartupWindow
            If window.ExtendsContentIntoTitleBar Then
                customTitleBar.Content = "Reset to system TitleBar"
                defaultTitleBar.Content = "Reset to system TitleBar"
            Else
                customTitleBar.Content = "Set Custom TitleBar"
                defaultTitleBar.Content = "Set Fallback Custom TitleBar"
            End If
        End Sub
        Private Sub BgColorButton_Click(sender As Object, e As RoutedEventArgs)
            ' Extract the color of the button that was clicked.
            Dim clickedColor As Button = CType(sender, Button)
            Dim rectangle1 = CType(clickedColor.Content, Microsoft.UI.Xaml.Shapes.Rectangle)
            Dim color1 = CType(rectangle1.Fill, Microsoft.UI.Xaml.Media.SolidColorBrush).Color

            BackgroundColorElement.Background = New SolidColorBrush(color1)

            currentBgColor = color1
            UpdateTitleBarColor()
        End Sub
        Private Sub FgColorButton_Click(sender As Object, e As RoutedEventArgs)
            ' Extract the color of the button that was clicked.
            Dim clickedColor As Button = CType(sender, Button)
            Dim rectangle1 = CType(clickedColor.Content, Microsoft.UI.Xaml.Shapes.Rectangle)
            Dim color1 = CType(rectangle1.Fill, Microsoft.UI.Xaml.Media.SolidColorBrush).Color

            ForegroundColorElement.Background = New SolidColorBrush(color1)

            currentFgColor = color1
            UpdateTitleBarColor()
        End Sub
        Private Sub UpdateTitleBarColor()
            Dim res = Microsoft.UI.Xaml.Application.Current.Resources
            res("WindowCaptionBackground") = currentBgColor
            'res["WindowCaptionBackgroundDisabled"] = currentBgColor;
            res("WindowCaptionForeground") = currentFgColor
            'res["WindowCaptionForegroundDisabled"] = currentFgColor;

            TitleBarHelper.triggerTitleBarRepaint()
        End Sub
        Private Sub customTitleBar_Click(sender As Object, e As RoutedEventArgs)
            SetTitleBar(App.appTitleBar)
        End Sub
        Private Sub defaultTitleBar_Click(sender As Object, e As RoutedEventArgs)
            SetTitleBar(Nothing)
        End Sub
    End Class
End Namespace
