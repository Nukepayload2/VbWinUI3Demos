' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Navigation
Imports AppUIBasics.ControlPages
Imports Microsoft.UI.Xaml.Media.Animation

Namespace AppUIBasics.ConnectedAnimationPages
    Public NotInheritable Partial Class DetailedInfoPage
        Inherits Page
        Public Property DetailedObject As CustomDataObject
        Public Sub New()
            Me.InitializeComponent()
            AddHandler GoBackButton.Loaded, AddressOf GoBackButton_Loaded
        End Sub
        Private Sub GoBackButton_Loaded(sender As Object, e As RoutedEventArgs)
            ' When we land in page, put focus on the back button
            GoBackButton.Focus(FocusState.Programmatic)
        End Sub
        Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            MyBase.OnNavigatedTo(e)

            ' Store the item to be used in binding to UI
            DetailedObject = TryCast(e.Parameter, CustomDataObject)

            Dim imageAnimation As ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation")
            If imageAnimation IsNot Nothing Then
                ' Connected animation + coordinated animation
                imageAnimation.TryStart(detailedImage, New UIElement() {coordinatedPanel})

            End If
        End Sub
        ' Create connected animation back to collection page.
        Protected Overrides Sub OnNavigatingFrom(e As NavigatingCancelEventArgs)
            MyBase.OnNavigatingFrom(e)

            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackConnectedAnimation", detailedImage)
        End Sub
        Private Sub BackButton_Click(sender As Object, e As RoutedEventArgs)
            Frame.GoBack()
        End Sub
    End Class
End Namespace
