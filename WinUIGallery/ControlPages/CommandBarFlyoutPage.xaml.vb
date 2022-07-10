' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports Windows.Foundation.Metadata
Imports Microsoft.UI
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Input
Imports Microsoft.UI.Xaml.Media

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class CommandBarFlyoutPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub OnElementClicked(sender As Object, e As Microsoft.UI.Xaml.RoutedEventArgs)
            ' Do custom logic
            SelectedOptionText.Text = "You clicked: " & TryCast(sender, AppBarButton).Label
        End Sub
        Private Sub ShowMenu(isTransient As Boolean)
            If ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7) Then
                Dim myOption As New FlyoutShowOptions With
    { _
                .ShowMode = If(isTransient, FlyoutShowMode.Transient, FlyoutShowMode.Standard),
                .Placement = FlyoutPlacementMode.RightEdgeAlignedTop
    }
                CommandBarFlyout1.ShowAt(Image1, myOption)
            Else
                CommandBarFlyout1.ShowAt(Image1)
            End If
        End Sub
        Private Sub MyImageButton_ContextRequested(sender As Microsoft.UI.Xaml.UIElement, args As ContextRequestedEventArgs)
            ' always show a context menu in standard mode
            ShowMenu(False)
        End Sub
        Private Sub MyImageButton_Click(sender As Object, e As Microsoft.UI.Xaml.RoutedEventArgs)
            ' show in Transient mode.
            ShowMenu(True)
        End Sub
    End Class
End Namespace
