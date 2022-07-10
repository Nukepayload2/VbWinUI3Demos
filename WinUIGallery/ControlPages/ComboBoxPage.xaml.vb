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
Imports Windows.Foundation.Metadata
Imports Microsoft.UI
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class ComboBoxPage
        Inherits Page
        Public ReadOnly Property Fonts As List(Of Tuple(Of String, FontFamily)) = New List(Of Tuple(Of String, FontFamily)) From { _
                New Tuple(Of String, FontFamily)("Arial", New FontFamily("Arial")),
                New Tuple(Of String, FontFamily)("Comic Sans MS", New FontFamily("Comic Sans MS")),
                New Tuple(Of String, FontFamily)("Courier New", New FontFamily("Courier New")),
                New Tuple(Of String, FontFamily)("Segoe UI", New FontFamily("Segoe UI")),
                New Tuple(Of String, FontFamily)("Times New Roman", New FontFamily("Times New Roman"))
        }

        Public ReadOnly Property FontSizes As List(Of Double) = New List(Of Double) From { _
                8,
                9,
                10,
                11,
                12,
                14,
                16,
                18,
                20,
                24,
                28,
                36,
                48,
                72
        }

        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub ColorComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim colorName As String = e.AddedItems(0).ToString()
            Dim color1 As Windows.UI.Color
            Select Case colorName
                Case "Yellow"
                    color1 = Colors.Yellow
                Case "Green"
                    color1 = Colors.Green
                Case "Blue"
                    color1 = Colors.Blue
                Case "Red"
                    color1 = Colors.Red
                Case Else
                    Throw New Exception($"Invalid argument: {colorName}")
            End Select
            Control1Output.Fill = New SolidColorBrush(color1)
        End Sub
        Private Sub Combo2_Loaded(sender As Object, e As RoutedEventArgs)
            Combo2.SelectedIndex = 2
        End Sub
        Private Sub Combo3_Loaded(sender As Object, e As RoutedEventArgs)
            Combo3.SelectedIndex = 2

            If ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7) Then
                AddHandler Combo3.TextSubmitted, AddressOf Combo3_TextSubmitted
            End If
        End Sub
        Private Sub Combo3_TextSubmitted(sender As ComboBox, args As ComboBoxTextSubmittedEventArgs)
            Dim newValue As Double = Nothing
            Dim isDouble As Boolean = Double.TryParse(sender.Text, newValue)

            ' Set the selected item if:
            ' - The value successfully parsed to double AND
            ' - The value is in the list of sizes OR is a custom value between 8 and 100
            If isDouble AndAlso (FontSizes.Contains(newValue) OrElse (newValue < 100 AndAlso newValue > 8)) Then
                ' Update the SelectedItem to the new value. 
                sender.SelectedItem = newValue
            Else
                ' If the item is invalid, reject it and revert the text. 
                sender.Text = sender.SelectedValue.ToString()

                Dim dialog As New ContentDialog
                dialog.Content = "The font size must be a number between 8 and 100."
                dialog.CloseButtonText = "Close"
                dialog.DefaultButton = ContentDialogButton.Close
                dialog.XamlRoot = sender.XamlRoot
                Dim task = dialog.ShowAsync()
            End If

            ' Mark the event as handled so the framework doesn’t update the selected item automatically. 
            args.Handled = True
        End Sub
    End Class
End Namespace
