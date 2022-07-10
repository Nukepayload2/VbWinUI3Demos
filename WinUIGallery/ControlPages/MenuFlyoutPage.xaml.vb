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
    Public NotInheritable Partial Class MenuFlyoutPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub MenuFlyoutItem_Click(sender As Object, e As RoutedEventArgs)
            Dim TempVar As Boolean = TypeOf sender Is MenuFlyoutItem
            Dim selectedItem As MenuFlyoutItem = sender
            If TempVar Then
                Dim sortOption As String = selectedItem.Tag.ToString()
                Select Case sortOption
                    Case "rating"
                        'SortByRating();
                        
                    Case "match"
                        'SortByMatch();
                        
                    Case "distance"
                        'SortByDistance();
                        
                End Select
                Control1Output.Text = "Sort by: " & sortOption
            End If
        End Sub
        Private Sub Example5_Loaded(sender As Object, e As RoutedEventArgs)
        End Sub
    End Class
End Namespace
