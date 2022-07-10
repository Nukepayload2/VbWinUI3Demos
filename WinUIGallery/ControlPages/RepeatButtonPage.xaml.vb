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

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class RepeatButtonPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Shared _clicks As Integer = 0
        Private Sub RepeatButton_Click(sender As Object, e As RoutedEventArgs)
            _clicks += 1
            Control1Output.Text = "Number of clicks: " & _clicks
        End Sub
    End Class
End Namespace
