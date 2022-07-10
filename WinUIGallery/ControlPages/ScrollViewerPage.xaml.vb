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
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Input

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class ScrollViewerPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub ZoomModeComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            If Control1 IsNot Nothing AndAlso ZoomSlider IsNot Nothing Then
                Dim TempVar As Boolean = TypeOf sender Is ComboBox
                Dim cb As ComboBox = sender
                If TempVar Then
                    Select Case cb.SelectedIndex
                        Case 0
                            Control1.ZoomMode = ZoomMode.Enabled
                            ZoomSlider.IsEnabled = True
                        Case 1
                            Control1.ZoomMode = ZoomMode.Disabled
                            Control1.ChangeView(Nothing, Nothing, CSng(1.0))
                            ZoomSlider.Value = 1
                            ZoomSlider.IsEnabled = False
                        Case Else
                            Control1.ZoomMode = ZoomMode.Enabled
                            ZoomSlider.IsEnabled = True
                    End Select
                End If
            End If
        End Sub
        Private Sub ZoomSlider_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs)
            If Control1 IsNot Nothing Then
                Control1.ChangeView(Nothing, Nothing, CSng(e.NewValue))
            End If
        End Sub
        Private Sub hsmCombo_SelectionChanged_1(sender As Object, e As SelectionChangedEventArgs)
            If Control1 IsNot Nothing Then
                Dim TempVar1 As Boolean = TypeOf sender Is ComboBox
                Dim cb As ComboBox = sender
                If TempVar1 Then
                    Select Case cb.SelectedIndex
                        Case 0
                            Control1.HorizontalScrollMode = ScrollMode.Auto
                        Case 1
                            Control1.HorizontalScrollMode = ScrollMode.Enabled
                        Case 2
                            Control1.HorizontalScrollMode = ScrollMode.Disabled
                        Case Else
                            Control1.HorizontalScrollMode = ScrollMode.Enabled
                    End Select
                End If
            End If
        End Sub
        Private Sub hsbvCombo_SelectionChanged_1(sender As Object, e As SelectionChangedEventArgs)
            If Control1 IsNot Nothing Then
                Dim TempVar2 As Boolean = TypeOf sender Is ComboBox
                Dim cb As ComboBox = sender
                If TempVar2 Then
                    Select Case cb.SelectedIndex
                        Case 0
                            Control1.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto
                        Case 1
                            Control1.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible
                        Case 2
                            Control1.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden
                        Case 3
                            Control1.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
                        Case Else
                            Control1.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
                    End Select
                End If
            End If
        End Sub
        Private Sub vsmCombo_SelectionChanged_1(sender As Object, e As SelectionChangedEventArgs)
            If Control1 IsNot Nothing Then
                Dim TempVar3 As Boolean = TypeOf sender Is ComboBox
                Dim cb As ComboBox = sender
                If TempVar3 Then
                    Select Case cb.SelectedIndex
                        Case 0
                            Control1.VerticalScrollMode = ScrollMode.Auto
                        Case 1
                            Control1.VerticalScrollMode = ScrollMode.Enabled
                        Case 2
                            Control1.VerticalScrollMode = ScrollMode.Disabled
                        Case Else
                            Control1.VerticalScrollMode = ScrollMode.Enabled
                    End Select
                End If
            End If
        End Sub
        Private Sub vsbvCombo_SelectionChanged_1(sender As Object, e As SelectionChangedEventArgs)
            If Control1 IsNot Nothing Then
                Dim TempVar4 As Boolean = TypeOf sender Is ComboBox
                Dim cb As ComboBox = sender
                If TempVar4 Then
                    Select Case cb.SelectedIndex
                        Case 0
                            Control1.VerticalScrollBarVisibility = ScrollBarVisibility.Auto
                        Case 1
                            Control1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible
                        Case 2
                            Control1.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden
                        Case 3
                            Control1.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled
                        Case Else
                            Control1.VerticalScrollBarVisibility = ScrollBarVisibility.Visible
                    End Select
                End If
            End If
        End Sub
        Private Sub Page_SizeChanged(sender As Object, e As SizeChangedEventArgs)
            If Grid.GetColumnSpan(Control1) = 1 Then
                Control1.Width = (e.NewSize.Width / 2) - 50
            Else
                Control1.Width = e.NewSize.Width - 50
            End If
        End Sub
        Private Sub Control1_ViewChanged(sender As Object, e As ScrollViewerViewChangedEventArgs)
            ZoomSlider.Value = Control1.ZoomFactor
        End Sub
        Private Sub Control1_ManipulationCompleted(sender As Object, e As ManipulationCompletedRoutedEventArgs)
            ZoomSlider.Value = Control1.ZoomFactor
        End Sub
    End Class
End Namespace
