'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports AppUIBasics.Data
Imports AppUIBasics.Helper
Imports System.Collections.Generic
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class CreateMultipleWindowsPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub List_GotFocus(sender As Object, e As RoutedEventArgs)
            Control1.StartBringIntoView()
        End Sub
        Private Sub createNewWindow_Click(sender As Object, e As RoutedEventArgs)
            Dim newWindow = WindowHelper.CreateWindow()
            Dim rootPage As New NavigationRootPage
            rootPage.RequestedTheme = ThemeHelper.RootTheme
            newWindow.Content = rootPage
            newWindow.Activate()

            Dim targetPageType As System.Type = GetType(NewControlsPage)
            Dim targetPageArguments As String = String.Empty
            rootPage.Navigate(targetPageType, targetPageArguments)
        End Sub
    End Class
End Namespace
