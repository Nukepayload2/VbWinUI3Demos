'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************


Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Runtime.InteropServices.WindowsRuntime
Imports Windows.Foundation
Imports Windows.Foundation.Collections
Imports Microsoft.UI
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Data
Imports Microsoft.UI.Xaml.Documents
Imports Microsoft.UI.Xaml.Input
Imports Microsoft.UI.Xaml.Media
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class RichTextBlockPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub HighlightColorCombobox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            ' Get color to use
            Dim selectedItem1 = TryCast(TryCast(sender, ComboBox).SelectedItem, ComboBoxItem)
            Dim color = Colors.Yellow
            Select Case TryCast(selectedItem1.Content, String)
                Case "Yellow"
                    color = Colors.Yellow
                Case "Red"
                    color = Colors.Red
                Case "Blue"
                    color = Colors.Blue
            End Select

            ' Get text range and highlighter
            Dim textRange1 As New TextRange() With
            { _
            .StartIndex = 28,
            .Length = 11
            }
            Dim highlighter As New TextHighlighter() With
            {
            .Background = New SolidColorBrush(color)
            }
            highlighter.Ranges.Add(textRange1)
            ' Switch texthighlighter
            TextHighlightingRichTextBlock.TextHighlighters.Clear()
            TextHighlightingRichTextBlock.TextHighlighters.Add(highlighter)
        End Sub
    End Class
End Namespace
