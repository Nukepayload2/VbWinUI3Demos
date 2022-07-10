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

Imports Microsoft.UI
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class RadioButtonsPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub BackgroundColor_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim TempVar As Boolean = TypeOf sender Is RadioButtons
            Dim rb As RadioButtons = sender
            If ControlOutput IsNot Nothing AndAlso TempVar Then
                Dim colorName As String = TryCast(rb.SelectedItem, String)
                Select Case colorName
                    Case "Yellow"
                        ControlOutput.Background = New SolidColorBrush(Colors.Yellow)
                    Case "Green"
                        ControlOutput.Background = New SolidColorBrush(Colors.Green)
                    Case "White"
                        ControlOutput.Background = New SolidColorBrush(Colors.White)
                End Select
            End If
        End Sub
        Private Sub BorderBrush_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim TempVar1 As Boolean = TypeOf sender Is RadioButtons
            Dim rb As RadioButtons = sender
            If ControlOutput IsNot Nothing AndAlso TempVar1 Then
                Dim colorName As String = TryCast(rb.SelectedItem, String)
                Select Case colorName
                    Case "Yellow"
                        ControlOutput.BorderBrush = New SolidColorBrush(Colors.Gold)
                    Case "Green"
                        ControlOutput.BorderBrush = New SolidColorBrush(Colors.DarkGreen)
                    Case "White"
                        ControlOutput.BorderBrush = New SolidColorBrush(Colors.White)
                End Select
            End If
        End Sub
    End Class
End Namespace
