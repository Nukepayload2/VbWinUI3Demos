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
Imports Microsoft.UI

#If Not UNIVERSAL

Imports System.Collections.ObjectModel
#End If

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class SplitViewPage
        Inherits Page
        Private _navLinks As New ObservableCollection(Of NavLink) From { _
            New NavLink() With
{
                .Label = "People",
                .Symbol = Symbol.People},
            New NavLink() With
{
                .Label = "Globe",
                .Symbol = Symbol.Globe},
            New NavLink() With
{
                .Label = "Message",
                .Symbol = Symbol.Message},
            New NavLink() With
{
                .Label = "Mail",
                .Symbol = Symbol.Mail}
        }
        Public ReadOnly Property NavLinks As ObservableCollection(Of NavLink)
            Get
                Return _navLinks
            End Get
        End Property

        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub togglePaneButton_Click(sender As Object, e As RoutedEventArgs)
            Dim window1 As Window = WindowHelper.GetWindowForElement(Me)
            If window1.Bounds.Width >= 640 Then
                If splitView.IsPaneOpen Then
                    splitView.DisplayMode = SplitViewDisplayMode.CompactOverlay
                    splitView.IsPaneOpen = False
                Else
                    splitView.IsPaneOpen = True
                    splitView.DisplayMode = SplitViewDisplayMode.Inline
                End If
            Else
                splitView.IsPaneOpen = Not splitView.IsPaneOpen
            End If
        End Sub
        Private Sub NavLinksList_ItemClick(sender As Object, e As ItemClickEventArgs)
            content.Text = TryCast(e.ClickedItem, NavLink).Label & " Page"
        End Sub
        Private Sub PanePlacement_Toggled(sender As Object, e As RoutedEventArgs)
            Dim ts = TryCast(sender, ToggleSwitch)
            If ts.IsOn Then
                splitView.PanePlacement = SplitViewPanePlacement.Right
            Else
                splitView.PanePlacement = SplitViewPanePlacement.Left
            End If
        End Sub
        Private Sub displayModeCombobox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            splitView.DisplayMode = CType([Enum].Parse(GetType(SplitViewDisplayMode), TryCast(e.AddedItems(0), ComboBoxItem).Content.ToString()), SplitViewDisplayMode)
        End Sub
        Private Sub paneBackgroundCombobox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim colorString = TryCast(e.AddedItems(0), ComboBoxItem).Content.ToString()

            VisualStateManager.GoToState(Me, colorString, False)
        End Sub
    End Class


    Public Class NavLink
        Public Property Label As String
        Public Property Symbol As Symbol
    End Class
End Namespace
