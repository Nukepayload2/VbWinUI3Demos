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
    Public NotInheritable Partial Class PasswordBoxPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub PasswordBox_PasswordChanged(sender As Object, e As RoutedEventArgs)
            Dim TempVar As Boolean = TypeOf sender Is PasswordBox
            Dim pb As PasswordBox = sender
            If TempVar Then
                If String.IsNullOrEmpty(pb.Password) OrElse pb.Password = "Password" Then
                    Control1Output.Visibility = Visibility.Visible
                    Control1Output.Text = "'Password' is not allowed."
                    pb.Password = String.Empty
                Else
                    Control1Output.Text = String.Empty
                    Control1Output.Visibility = Visibility.Collapsed
                End If
            End If
        End Sub
        Private Sub RevealModeCheckbox_Changed(sender As Object, e As RoutedEventArgs)
            If revealModeCheckBox.IsChecked = True Then
                passworBoxWithRevealmode.PasswordRevealMode = PasswordRevealMode.Visible
            Else
                passworBoxWithRevealmode.PasswordRevealMode = PasswordRevealMode.Hidden
            End If
        End Sub
    End Class
End Namespace
