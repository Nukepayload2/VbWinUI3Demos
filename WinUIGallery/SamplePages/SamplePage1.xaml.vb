' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports Windows.Foundation.Metadata
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media.Animation
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics.SamplePages
    Public NotInheritable Partial Class SamplePage1
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Public Sub PrepareConnectedAnimation(config As ConnectedAnimationConfiguration)
            Dim anim = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ForwardConnectedAnimation", SourceElement)

            If config IsNot Nothing AndAlso ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7) Then
                anim.Configuration = config
            End If
        End Sub
        Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            MyBase.OnNavigatedTo(e)

            Dim anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("BackwardConnectedAnimation")
            If anim IsNot Nothing Then
                anim.TryStart(SourceElement)
            End If
        End Sub
    End Class
End Namespace
