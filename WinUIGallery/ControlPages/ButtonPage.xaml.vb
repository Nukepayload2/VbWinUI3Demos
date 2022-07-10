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
    Public NotInheritable Partial Class ButtonPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
            Dim TempVar As Boolean = TypeOf sender Is Button
            Dim b As Button = sender
            If TempVar Then
                Dim name1 As String = b.Name

                Select Case name1
                    Case "Button1"
                        Control1Output.Text = "You clicked: " & name1
                    Case "Button2"
                        Control2Output.Text = "You clicked: " & name1

                End Select
            End If
        End Sub
    End Class
End Namespace
