' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports Windows.Foundation.Metadata
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Input

#If Not UNIVERSAL

Imports System.Collections.ObjectModel
Imports System.Windows.Input
Imports ICommand = System.Windows.Input.ICommand
#End If

Namespace AppUIBasics.ControlPages
    Public Class ListItemData
        Public Property Text As String
        Public Property Command As ICommand
    End Class


    Public NotInheritable Partial Class StandardUICommandPage
        Inherits Page
        Private collection As New ObservableCollection(Of ListItemData)

        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub DeleteCommand_ExecuteRequested(sender As XamlUICommand, args As ExecuteRequestedEventArgs)
            If args.Parameter IsNot Nothing Then
                For Each i As AppUIBasics.ControlPages.ListItemData In collection
                    If i.Text = TryCast(args.Parameter, String) Then
                        collection.Remove(i)
                        Return
                    End If
                Next
            End If
            If ListViewRight.SelectedIndex <> -1 Then
                collection.RemoveAt(ListViewRight.SelectedIndex)
            End If
        End Sub
        Private Sub ListView_Loaded(sender As Object, e As RoutedEventArgs)
            Dim listView1 = CType(sender, ListView)
            listView1.ItemsSource = collection
        End Sub
        Private Sub ListView_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            If ListViewRight.SelectedIndex <> -1 Then
                Dim item As AppUIBasics.ControlPages.ListItemData = collection(ListViewRight.SelectedIndex)
            End If
        End Sub
        Private Sub ListViewSwipeContainer_PointerEntered(sender As Object, e As PointerRoutedEventArgs)
            If e.Pointer.PointerDeviceType = Microsoft.UI.Input.PointerDeviceType.Mouse OrElse e.Pointer.PointerDeviceType = Microsoft.UI.Input.PointerDeviceType.Pen Then
                VisualStateManager.GoToState(TryCast(sender, Control), "HoverButtonsShown", True)
            End If
        End Sub
        Private Sub ListViewSwipeContainer_PointerExited(sender As Object, e As PointerRoutedEventArgs)
            VisualStateManager.GoToState(TryCast(sender, Control), "HoverButtonsHidden", True)
        End Sub
        Private Sub ControlExample_Loaded(sender As Object, e As RoutedEventArgs)
            If ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7) Then
                Dim deleteCommand As New StandardUICommand(StandardUICommandKind.Delete)
                AddHandler deleteCommand.ExecuteRequested, AddressOf DeleteCommand_ExecuteRequested

                DeleteFlyoutItem.Command = deleteCommand

                For i = 0 To 15 - 1
                    collection.Add(New ListItemData With
{
                        .Text = "List item " & i.ToString(),
                        .Command = deleteCommand})
                Next
            Else
                For i = 0 To 15 - 1
                    collection.Add(New ListItemData With
{
                        .Text = "List item " & i.ToString(),
                        .Command = Nothing})
                Next
            End If
        End Sub
        Private Sub ListViewRight_ContainerContentChanging(sender As ListViewBase, args As ContainerContentChangingEventArgs)
            Dim flyout As New MenuFlyout
            Dim data As ListItemData = CType(args.Item, ListItemData)
            Dim item As New MenuFlyoutItem() With
{
                .Command = data.Command}
            flyout.Opened += Sub(element As Object, e As Object)
                                 Dim flyoutElement As MenuFlyout = TryCast(element, MenuFlyout)
                                 Dim elementToHighlight As ListViewItem = TryCast(flyoutElement.Target, ListViewItem)
                                 elementToHighlight.IsSelected = True
                             End Sub
            flyout.Items.Add(item)
            args.ItemContainer.ContextFlyout = flyout
        End Sub
    End Class
End Namespace
