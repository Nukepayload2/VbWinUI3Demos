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
    Public NotInheritable Partial Class VariableSizedWrapGridPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub RadioButton_Checked(sender As Object, e As RoutedEventArgs)
            Dim TempVar As Boolean = TypeOf sender Is RadioButton
            Dim rb As RadioButton = sender
            If TempVar AndAlso Control1 IsNot Nothing Then
                Dim orientationName As String = rb.Tag.ToString()

                Select Case orientationName
                    Case "Horizontal"
                        Control1.Orientation = Orientation.Horizontal
                    Case "Vertical"
                        Control1.Orientation = Orientation.Vertical
                End Select
            End If
        End Sub
    End Class
End Namespace
