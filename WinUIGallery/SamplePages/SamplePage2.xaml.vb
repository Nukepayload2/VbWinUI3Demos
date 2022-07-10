' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports Windows.Foundation.Metadata
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media.Animation
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics.SamplePages
    Public NotInheritable Partial Class SamplePage2
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Public Sub PrepareConnectedAnimation(config As ConnectedAnimationConfiguration)
            Dim anim = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("BackwardConnectedAnimation", DestinationElement)

            If config IsNot Nothing AndAlso ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7) Then
                anim.Configuration = config
            End If
        End Sub
        Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            MyBase.OnNavigatedTo(e)

            Dim anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation")
            If anim IsNot Nothing Then
                AddContentPanelAnimations()
                anim.TryStart(DestinationElement)
            End If
        End Sub
        Private Sub AddContentPanelAnimations()
            ContentPanel.Transitions = New TransitionCollection From {
                New EntranceThemeTransition}
        End Sub
    End Class
End Namespace
