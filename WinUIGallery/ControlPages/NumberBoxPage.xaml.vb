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

Imports Windows.Globalization.NumberFormatting
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class NumberBoxPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
            SetNumberBoxNumberFormatter()
        End Sub
        Private Sub SetNumberBoxNumberFormatter()
            Dim rounder As New IncrementNumberRounder With
            { _
            .Increment = 0.25,
            .RoundingAlgorithm = RoundingAlgorithm.RoundHalfUp
            }

            Dim formatter As New DecimalFormatter With
            { _
            .IntegerDigits = 1,
            .FractionDigits = 2,
            .NumberRounder = rounder
            }
            FormattedNumberBox.NumberFormatter = formatter
        End Sub
        Private Sub RadioButton_Checked(sender As Object, e As RoutedEventArgs)
            Dim TempVar As Boolean = TypeOf sender Is RadioButton
            Dim rb As RadioButton = sender
            If TempVar AndAlso NumberBoxSpinButtonPlacementExample IsNot Nothing Then
                Dim spinButtonPlacementModeName As String = rb.Tag.ToString()

                Select Case spinButtonPlacementModeName
                    Case "Inline"
                        NumberBoxSpinButtonPlacementExample.SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Inline
                    Case "Compact"
                        NumberBoxSpinButtonPlacementExample.SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact
                End Select
            End If
        End Sub
    End Class
End Namespace
