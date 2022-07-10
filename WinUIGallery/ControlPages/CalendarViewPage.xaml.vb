' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Runtime.InteropServices.WindowsRuntime
Imports Windows.Foundation
Imports Windows.Foundation.Collections
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Data
Imports Microsoft.UI.Xaml.Input
Imports Microsoft.UI.Xaml.Media
Imports Microsoft.UI.Xaml.Navigation
Imports System.Reflection
Imports Windows.Globalization
Imports Windows.UI.Popups
Imports AppUIBasics.Common

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class CalendarViewPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()

            Dim calendarIdentifiers1 As New List(Of String) From { _
                CalendarIdentifiers.Gregorian,
                CalendarIdentifiers.Hebrew,
                CalendarIdentifiers.Hijri,
                CalendarIdentifiers.Japanese,
                CalendarIdentifiers.Julian,
                CalendarIdentifiers.Korean,
                CalendarIdentifiers.Persian,
                CalendarIdentifiers.Taiwan,
                CalendarIdentifiers.Thai,
                CalendarIdentifiers.UmAlQura
            }

            calendarIdentifier.ItemsSource = calendarIdentifiers1
            calendarIdentifier.SelectedItem = CalendarIdentifiers.Gregorian

            Dim langs As New LanguageList
            calendarLanguages.ItemsSource = langs.Languages
        End Sub
        Private Sub SelectionMode_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim selectionMode As CalendarViewSelectionMode = Nothing
            If [Enum].TryParse(Of CalendarViewSelectionMode)(TryCast(sender, ComboBox).SelectedItem.ToString(), selectionMode) Then
                Control1.SelectionMode = selectionMode
            End If
        End Sub
        Private Sub calendarLanguages_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim selectedLang As String = calendarLanguages.SelectedValue.ToString()
            If Windows.Globalization.Language.IsWellFormed(selectedLang) Then
                Control1.Language = selectedLang
            End If
        End Sub
    End Class
End Namespace
