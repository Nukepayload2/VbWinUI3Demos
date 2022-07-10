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
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Media

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class BorderPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub ThicknessSlider_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs)
#If Not UNIVERSAL

            If Control1 IsNot Nothing Then
                Control1.BorderThickness = New Thickness(e.NewValue)
            End If
#Else

            if (Control1 != null) Control1.BorderThickness = ThicknessHelper.FromUniformLength(e.NewValue);

#End If

        End Sub
        Private Sub BGRadioButton_Checked(sender As Object, e As RoutedEventArgs)
            Dim TempVar As Boolean = TypeOf sender Is RadioButton
            Dim rb As RadioButton = sender
            If TempVar AndAlso Control1 IsNot Nothing Then
                Dim colorName As String = rb.Content.ToString()
                Select Case colorName
                    Case "Yellow"
                        Control1.Background = New SolidColorBrush(Colors.Yellow)
                    Case "Green"
                        Control1.Background = New SolidColorBrush(Colors.Green)
                    Case "Blue"
                        Control1.Background = New SolidColorBrush(Colors.Blue)
                    Case "White"
                        Control1.Background = New SolidColorBrush(Colors.White)
                End Select
            End If
        End Sub
        Private Sub RadioButton_Checked(sender As Object, e As RoutedEventArgs)
            Dim TempVar1 As Boolean = TypeOf sender Is RadioButton
            Dim rb As RadioButton = sender
            If TempVar1 AndAlso Control1 IsNot Nothing Then
                Dim colorName As String = rb.Content.ToString()
                Select Case colorName
                    Case "Yellow"
                        Control1.BorderBrush = New SolidColorBrush(Colors.Gold)
                    Case "Green"
                        Control1.BorderBrush = New SolidColorBrush(Colors.DarkGreen)
                    Case "Blue"
                        Control1.BorderBrush = New SolidColorBrush(Colors.DarkBlue)
                    Case "White"
                        Control1.BorderBrush = New SolidColorBrush(Colors.White)
                End Select
            End If
        End Sub
    End Class
End Namespace
