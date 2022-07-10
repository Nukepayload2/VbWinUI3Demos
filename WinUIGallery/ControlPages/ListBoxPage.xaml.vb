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

Imports System.Collections.Generic
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class ListBoxPage
        Inherits Page
        Private _fonts As New List(Of Tuple(Of String, FontFamily)) From { _
            New Tuple(Of String, FontFamily)("Arial", New FontFamily("Arial")),
            New Tuple(Of String, FontFamily)("Comic Sans MS", New FontFamily("Comic Sans MS")),
            New Tuple(Of String, FontFamily)("Courier New", New FontFamily("Courier New")),
            New Tuple(Of String, FontFamily)("Segoe UI", New FontFamily("Segoe UI")),
            New Tuple(Of String, FontFamily)("Times New Roman", New FontFamily("Times New Roman"))
        }
        Public ReadOnly Property Fonts As List(Of Tuple(Of String, FontFamily))
            Get
                Return _fonts
            End Get
        End Property
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub ColorListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim colorName As String = e.AddedItems(0).ToString()
            Select Case colorName
                Case "Yellow"
                    Control1Output.Fill = New SolidColorBrush(Microsoft.UI.Colors.Yellow)
                Case "Green"
                    Control1Output.Fill = New SolidColorBrush(Microsoft.UI.Colors.Green)
                Case "Blue"
                    Control1Output.Fill = New SolidColorBrush(Microsoft.UI.Colors.Blue)
                Case "Red"
                    Control1Output.Fill = New SolidColorBrush(Microsoft.UI.Colors.Red)
            End Select
        End Sub
        Private Sub ListBox2_Loaded(sender As Object, e As RoutedEventArgs)
            ListBox2.SelectedIndex = 2
        End Sub
    End Class
End Namespace
