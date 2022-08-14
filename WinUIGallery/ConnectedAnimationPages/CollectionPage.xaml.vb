' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media.Animation
Imports Microsoft.UI.Xaml.Navigation
Imports AppUIBasics.ControlPages
Imports Windows.Foundation.Metadata

Namespace AppUIBasics.ConnectedAnimationPages
    Public NotInheritable Partial Class CollectionPage
        Inherits Page
        Private _storeditem As CustomDataObject

        Public Sub New()
            Me.InitializeComponent()

            ' Ensure that the MainPage is only created once, and cached during navigation.
            Me.NavigationCacheMode = NavigationCacheMode.Enabled

            collection.ItemsSource = AppUIBasics.ControlPages.CustomDataObject.GetDataObjects()
        End Sub
        Private Async Sub collection_Loaded(sender As Object, e As RoutedEventArgs)
            If _storeditem IsNot Nothing Then
                ' If the connected item appears outside the viewport, scroll it into view.
                collection.ScrollIntoView(_storeditem, ScrollIntoViewAlignment.[Default])
                collection.UpdateLayout()

                ' Play the second connected animation. 
                Dim animation As ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("BackConnectedAnimation")
                If animation IsNot Nothing Then
                    ' Setup the "back" configuration if the API is present. 
                    If ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7) Then
                        animation.Configuration = New DirectConnectedAnimationConfiguration
                    End If

                    Await collection.TryStartConnectedAnimationAsync(animation, _storeditem, "connectedElement")
                End If

                ' Set focus on the list
                collection.Focus(FocusState.Programmatic)
            End If
        End Sub
        Private Sub collection_ItemClick(sender As Object, e As ItemClickEventArgs)
            ' Get the collection item corresponding to the clicked item.
            Dim TempVar As Boolean = TypeOf collection.ContainerFromItem(e.ClickedItem) Is ListViewItem
            Dim container As ListViewItem = collection.ContainerFromItem(e.ClickedItem)
            If TempVar Then
                ' Stash the clicked item for use later. We'll need it when we connect back from the detailpage.
                _storeditem = TryCast(container.Content, CustomDataObject)

                ' Prepare the connected animation.
                ' Notice that the stored item is passed in, as well as the name of the connected element. 
                ' The animation will actually start on the Detailed info page.
                Dim animation = collection.PrepareConnectedAnimation("ForwardConnectedAnimation", _storeditem, "connectedElement")

            End If

            ' Navigate to the DetailedInfoPage.
            ' Note that we suppress the default animation. 
            Frame.Navigate(GetType(DetailedInfoPage), _storeditem, New SuppressNavigationTransitionInfo)
        End Sub

    End Class
End Namespace
