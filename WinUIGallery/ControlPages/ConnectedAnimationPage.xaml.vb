' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports System.Collections.Generic
Imports Microsoft.UI.Xaml.Controls
Imports AppUIBasics.SamplePages
Imports Microsoft.UI.Xaml.Media.Animation
Imports Microsoft.UI.Xaml

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class ConnectedAnimationPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()

            ContentFrame.Navigate(GetType(SamplePage1))

            CollectionContentFrame.Navigate(GetType(ConnectedAnimationPages.CollectionPage))

            CardFrame.Navigate(GetType(ConnectedAnimationPages.CardPage))
        End Sub
        Private Function GetConfiguration() As ConnectedAnimationConfiguration
            If Me.ConfigurationPanel Is Nothing Then
                Return Nothing
            End If

            Dim selectedName = TryCast(ConfigurationPanel.SelectedItem, RadioButton).Content.ToString()
            Select Case selectedName
                Case "Gravity"
                    Return New GravityConnectedAnimationConfiguration
                Case "Direct"
                    Return New DirectConnectedAnimationConfiguration
                Case "Basic"
                    Return New BasicConnectedAnimationConfiguration
                Case Else
                    Return Nothing
            End Select
        End Function
        Private Sub NavigateButton_Click(sender As Object, e As RoutedEventArgs)
            Dim currentContent = ContentFrame.Content

            If TryCast(currentContent, SamplePage1) IsNot Nothing Then
                TryCast(currentContent, SamplePage1).PrepareConnectedAnimation(GetConfiguration())
                ContentFrame.Navigate(GetType(SamplePage2), Nothing, New SuppressNavigationTransitionInfo)
            ElseIf TryCast(currentContent, SamplePage2) IsNot Nothing Then
                TryCast(currentContent, SamplePage2).PrepareConnectedAnimation(GetConfiguration())
                ContentFrame.Navigate(GetType(SamplePage1), Nothing, New SuppressNavigationTransitionInfo)
            End If
        End Sub
    End Class

    ' Sample data object used to populate the collection page.
    Public Class CustomDataObject
        Public Property Title As String
        Public Property ImageLocation As String
        Public Property Views As String
        Public Property Likes As String
        Public Property Description As String

        Public Sub New()
        End Sub
        Public Shared Function GetDataObjects() As List(Of CustomDataObject)
            Dim dummyTexts As String() = { _
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer id facilisis lectus. Cras nec convallis ante, quis pulvinar tellus. Integer dictum accumsan pulvinar. Pellentesque eget enim sodales sapien vestibulum consequat.",
                "Nullam eget mattis metus. Donec pharetra, tellus in mattis tincidunt, magna ipsum gravida nibh, vitae lobortis ante odio vel quam.",
                "Quisque accumsan pretium ligula in faucibus. Mauris sollicitudin augue vitae lorem cursus condimentum quis ac mauris. Pellentesque quis turpis non nunc pretium sagittis. Nulla facilisi. Maecenas eu lectus ante. Proin eleifend vel lectus non tincidunt. Fusce condimentum luctus nisi, in elementum ante tincidunt nec.",
                "Aenean in nisl at elit venenatis blandit ut vitae lectus. Praesent in sollicitudin nunc. Pellentesque justo augue, pretium at sem lacinia, scelerisque semper erat. Ut cursus tortor at metus lacinia dapibus.",
                "Ut consequat magna luctus justo egestas vehicula. Integer pharetra risus libero, et posuere justo mattis et.",
                "Proin malesuada, libero vitae aliquam venenatis, diam est faucibus felis, vitae efficitur erat nunc non mauris. Suspendisse at sodales erat.",
                "Aenean vulputate, turpis non tincidunt ornare, metus est sagittis erat, id lobortis orci odio eget quam. Suspendisse ex purus, lobortis quis suscipit a, volutpat vitae turpis.",
                "Duis facilisis, quam ut laoreet commodo, elit ex aliquet massa, non varius tellus lectus et nunc. Donec vitae risus ut ante pretium semper. Phasellus consectetur volutpat orci, eu dapibus turpis. Fusce varius sapien eu mattis pharetra."}

            Dim rand As New Random
            Const numberOfLocations As Integer = 8
            Dim objects As New List(Of CustomDataObject)
            For i As Integer = 0 To numberOfLocations - 1
                objects.Add(New CustomDataObject() With
    { _
                .Title = $"Item {i + 1}",
                .ImageLocation = $"/Assets/SampleMedia/LandscapeImage{i + 1}.jpg",
                .Views = rand.[Next](100, 999).ToString(),
                .Likes = rand.[Next](10, 99).ToString(),
                .Description = dummyTexts(i Mod dummyTexts.Length)
    })
            Next

            Return objects
        End Function

    End Class
End Namespace
