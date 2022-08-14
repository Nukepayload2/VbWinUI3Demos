'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************


Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class ViewBoxPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub StretchDirectionButton_Checked(sender As Object, e As RoutedEventArgs)
            Dim TempVar As Boolean = TypeOf sender Is RadioButton
            Dim rb As RadioButton = sender
            If TempVar AndAlso Control1 IsNot Nothing Then
                Dim direction As String = rb.Tag.ToString()
                Select Case direction
                    Case "UpOnly"
                        Control1.StretchDirection = StretchDirection.UpOnly
                    Case "DownOnly"
                        Control1.StretchDirection = StretchDirection.DownOnly
                    Case "Both"
                        Control1.StretchDirection = StretchDirection.Both
                End Select
            End If
        End Sub
        Private Sub StretchButton_Checked(sender As Object, e As RoutedEventArgs)
            Dim TempVar1 As Boolean = TypeOf sender Is RadioButton
            Dim rb As RadioButton = sender
            If TempVar1 AndAlso Control1 IsNot Nothing Then
                Dim stretch1 As String = rb.Tag.ToString()
                Select Case stretch1
                    Case "None"
                        Control1.Stretch = Stretch.None
                    Case "Fill"
                        Control1.Stretch = Stretch.Fill
                    Case "Uniform"
                        Control1.Stretch = Stretch.Uniform
                    Case "UniformToFill"
                        Control1.Stretch = Stretch.UniformToFill
                End Select
            End If
        End Sub
    End Class
End Namespace
