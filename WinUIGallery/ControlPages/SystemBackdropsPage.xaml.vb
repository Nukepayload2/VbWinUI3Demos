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

Imports AppUIBasics.Helper
Imports System
Imports Microsoft.UI
Imports Microsoft.UI.Windowing
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class SystemBackdropsPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub createMicaWindow_Click(sender As Object, e As RoutedEventArgs)
            Dim newWindow As New AppUIBasics.SamplePages.SampleSystemBackdropsWindow
            newWindow.Activate()
        End Sub
        Private Sub createAcrylicWindow_Click(sender As Object, e As RoutedEventArgs)
            Dim newWindow As New AppUIBasics.SamplePages.SampleSystemBackdropsWindow
            newWindow.SetBackdrop(AppUIBasics.SamplePages.SampleSystemBackdropsWindow.BackdropType.DesktopAcrylic)
            newWindow.Activate()
        End Sub
    End Class
End Namespace
