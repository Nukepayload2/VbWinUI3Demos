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
Imports Microsoft.UI.Xaml.Shapes

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class ThemeTransitionPage
        Inherits Page
        Private _itemCount As Integer = 10
        Public Sub New()
            Me.InitializeComponent()

            For i As Integer = 0 To _itemCount - 1
                AddRemoveListView.Items.Add(New ListViewItem() With
{
                    .Content = "Item " & i})
            Next

            AddItemsToContentListView()
        End Sub
        Private Sub ShowPopupButton_Click(sender As Object, e As RoutedEventArgs)
            ExamplePopup.IsOpen = True
            ClosePopupButton.Focus(FocusState.Programmatic)
        End Sub
        Private Sub ClosePopupButton_Click(sender As Object, e As RoutedEventArgs)
            ExamplePopup.IsOpen = False
            ShowPopupButton.Focus(FocusState.Programmatic)
        End Sub
        Private Sub ContentRefreshButton_Click(sender As Object, e As RoutedEventArgs)
            AddItemsToContentListView(True)
        End Sub
        Private Sub AddItemsToContentListView(Optional ShowDifferentContent As Boolean = False)
            Dim items1 As Collections.Generic.List(Of String) = New List(Of String)
            For i As Integer = 0 To 5 - 1
                items1.Add(If(ShowDifferentContent, "Updated content " & i, "Item " & i))
            Next

            ContentList.ItemsSource = items1
        End Sub
        Private Sub AddButton_Click(sender As Object, e As RoutedEventArgs)
            AddRemoveListView.Items.Add(New ListViewItem() With
{
                .Content = "New Item " & _itemCount.ToString()})
            _itemCount += 1
        End Sub
        Private Sub DeleteButton_Click(sender As Object, e As RoutedEventArgs)
            If AddRemoveListView.Items.Count > 0 Then
                AddRemoveListView.Items.RemoveAt(0)
            End If
        End Sub
        Private Sub RepositionButton_Click(sender As Object, e As RoutedEventArgs)
            MiddleElement.Visibility = If(MiddleElement.Visibility = Visibility.Visible, Visibility.Collapsed, Visibility.Visible)
        End Sub
        Private Sub EntranceAddButton_Click(sender As Object, e As RoutedEventArgs)
            Dim _value As Integer = Convert.ToInt32(TryCast(sender, Button).Tag)

            For i As Integer = 0 To _value - 1
#If Not UNIVERSAL

                Dim thickness1 As New Thickness(5.0)
#Else

                Thickness thickness = ThicknessHelper.FromUniformLength(5.0);

#End If

                EntranceStackPanel.Children.Add(New Rectangle() With
{
                    .Width = 50,
                    .Height = 50,
                    .Margin = thickness1,
                    .Fill = New SolidColorBrush(Microsoft.UI.Colors.LightBlue)})
            Next
        End Sub
        Private Sub EntranceClearButton_Click(sender As Object, e As RoutedEventArgs)
            EntranceStackPanel.Children.Clear()
        End Sub
        Private Sub AddDeleteButton_Click(sender As Object, e As RoutedEventArgs)
            AddButton_Click(sender, e)
            DeleteButton_Click(sender, e)
        End Sub
    End Class
End Namespace
