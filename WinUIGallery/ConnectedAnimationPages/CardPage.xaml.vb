' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports System.Collections.Generic
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media.Animation
Imports Windows.Foundation.Metadata

Namespace AppUIBasics.ConnectedAnimationPages
    Public NotInheritable Partial Class CardPage
        Inherits Page
        Private _storedItem As Integer

        Public Sub New()
            Me.InitializeComponent()

            ' Populate the collection with some items.
            Dim items As Collections.Generic.List(Of Integer) = New List(Of Integer)
            For i As Integer = 0 To 30 - 1
                items.Add(i)
            Next

            collection.ItemsSource = items
        End Sub
        Private Async Sub BackButton_Click(sender As Object, e As RoutedEventArgs)
            Dim animation As ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backwardsAnimation", destinationElement)
            SmokeGrid.Children.Remove(destinationElement)

            ' Collapse the smoke when the animation completes.
            AddHandler animation.Completed, AddressOf Animation_Completed

            ' If the connected item appears outside the viewport, scroll it into view.
            collection.ScrollIntoView(_storedItem, ScrollIntoViewAlignment.[Default])
            collection.UpdateLayout()

            ' Use the Direct configuration to go back (if the API is available). 
            If ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7) Then
                animation.Configuration = New DirectConnectedAnimationConfiguration
            End If

            ' Play the second connected animation. 
            Await collection.TryStartConnectedAnimationAsync(animation, _storedItem, "connectedElement")
        End Sub
        Private Sub Animation_Completed(sender As ConnectedAnimation, args As Object)
            SmokeGrid.Visibility = Visibility.Collapsed
            SmokeGrid.Children.Add(destinationElement)
        End Sub
        Private Sub TipsGrid_ItemClick(sender As Object, e As ItemClickEventArgs)
            Dim animation As ConnectedAnimation = Nothing

            ' Get the collection item corresponding to the clicked item.
            Dim TempVar As Boolean = TypeOf collection.ContainerFromItem(e.ClickedItem) Is GridViewItem
            Dim container As GridViewItem = collection.ContainerFromItem(e.ClickedItem)
            If TempVar Then
                ' Stash the clicked item for use later. We'll need it when we connect back from the detailpage.
                _storedItem = Convert.ToInt32(container.Content)

                ' Prepare the connected animation.
                ' Notice that the stored item is passed in, as well as the name of the connected element. 
                ' The animation will actually start on the Detailed info page.
                animation = collection.PrepareConnectedAnimation("forwardAnimation", _storedItem, "connectedElement")

            End If

            SmokeGrid.Visibility = Visibility.Visible

            animation.TryStart(destinationElement)
        End Sub
    End Class
End Namespace
