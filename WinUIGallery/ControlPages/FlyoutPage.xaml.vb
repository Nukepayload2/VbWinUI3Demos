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

Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Input

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class FlyoutPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub DeleteConfirmation_Click(sender As Object, e As RoutedEventArgs)
            Dim TempVar As Boolean = TypeOf Me.Control1.Flyout Is Flyout
            Dim f As Flyout = Me.Control1.Flyout
            If TempVar Then
                f.Hide()
            End If
        End Sub
    End Class
End Namespace
