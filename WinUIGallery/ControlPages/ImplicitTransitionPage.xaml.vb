' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports System.Numerics
Imports Windows.Foundation.Metadata
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Input
Imports Microsoft.UI.Xaml.Media
Imports Microsoft.UI.Xaml.Controls.Primitives

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class ImplicitTransitionPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()

            SetupImplicitTransitionsIfAPIAvailable()
        End Sub
        Private Sub SetupImplicitTransitionsIfAPIAvailable()
            OpacityRectangle.OpacityTransition = New ScalarTransition
            RotationRectangle.RotationTransition = New ScalarTransition
            ScaleRectangle.ScaleTransition = New Vector3Transition
            TranslateRectangle.TranslationTransition = New Vector3Transition
            BrushPresenter.BackgroundTransition = New BrushTransition
            ThemeExampleGrid.BackgroundTransition = New BrushTransition
        End Sub
        Private Sub OpacityButton_Click(sender As Object, e As RoutedEventArgs)
            ' If the implicit animation API is not present, simply no-op. 
            If Not ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7) Then
                Return
            End If
            Dim customValue = EnsureValueIsNumber(OpacityNumberBox)
            OpacityRectangle.Opacity = customValue
            OpacityValue.Value = customValue
        End Sub
        Private Sub RotationButton_Click(sender As Object, e As RoutedEventArgs)
            RotationRectangle.CenterPoint = New Numerics.Vector3(CSng(RotationRectangle.ActualWidth) / 2, CSng(RotationRectangle.ActualHeight) / 2, 0F)

            RotationRectangle.Rotation = EnsureValueIsNumber(RotationNumberBox)
        End Sub
        Private Sub ScaleButton_Click(sender As Object, e As RoutedEventArgs)
            Dim _scaleTransition = ScaleRectangle.ScaleTransition

            _scaleTransition.Components = (If((ScaleX.IsChecked = True), Vector3TransitionComponents.X, 0)) Or _
(If((ScaleY.IsChecked = True), Vector3TransitionComponents.Y, 0)) Or _
(If((ScaleZ.IsChecked = True), Vector3TransitionComponents.Z, 0))

            Dim customValue As Single

            If sender IsNot Nothing AndAlso TryCast(sender, Button).Tag IsNot Nothing Then
                customValue = CSng(Convert.ToDouble(TryCast(sender, Button).Tag))
            Else
                customValue = EnsureValueIsNumber(ScaleNumberBox)
            End If

            ScaleRectangle.Scale = New Vector3(customValue)
            ScaleValue.Value = customValue
        End Sub
        Private Sub TranslateButton_Click(sender As Object, e As RoutedEventArgs)
            Dim _translationTransition = TranslateRectangle.TranslationTransition

            _translationTransition.Components = (If((TranslateX.IsChecked = True), Vector3TransitionComponents.X, 0)) Or _
(If((TranslateY.IsChecked = True), Vector3TransitionComponents.Y, 0)) Or _
(If((TranslateZ.IsChecked = True), Vector3TransitionComponents.Z, 0))

            Dim customValue As Single
            If sender IsNot Nothing AndAlso TryCast(sender, Button).Tag IsNot Nothing Then
                customValue = CSng(Convert.ToDouble(TryCast(sender, Button).Tag))
            Else
                customValue = EnsureValueIsNumber(TranslationNumberBox)
            End If

            TranslateRectangle.Translation = New Vector3(customValue)
            TranslationValue.Value = customValue
        End Sub
        Private Sub NumberBox_KeyDown(sender As Object, e As KeyRoutedEventArgs)
            If e.Key = Windows.System.VirtualKey.Enter Then
                If CStr(TryCast(sender, NumberBox).Header) = "Opacity (0.0 to 1.0)" Then
                    OpacityButton_Click(Nothing, Nothing)
                End If
                If CStr(TryCast(sender, NumberBox).Header) = "Rotation (0.0 to 360.0)" Then
                    RotationButton_Click(Nothing, Nothing)
                End If
                If CStr(TryCast(sender, NumberBox).Header) = "Scale (0.0 to 5.0)" Then
                    ScaleButton_Click(Nothing, Nothing)
                End If
                If CStr(TryCast(sender, NumberBox).Header) = "Translation (0.0 to 200.0)" Then
                    TranslateButton_Click(Nothing, Nothing)
                End If
            End If
        End Sub
        Private Sub BackgroundButton_Click(sender As Object, e As RoutedEventArgs)

            If TryCast(BrushPresenter.Background, SolidColorBrush).Color = Microsoft.UI.Colors.Blue Then
                BrushPresenter.Background = New SolidColorBrush(Microsoft.UI.Colors.Yellow)
            Else
                BrushPresenter.Background = New SolidColorBrush(Microsoft.UI.Colors.Blue)
            End If
        End Sub
        Private Function EnsureValueIsNumber(numberBox1 As NumberBox) As Single
            If Double.IsNaN(numberBox1.Value) Then
                numberBox1.Value = 0
            End If
            Return CSng(numberBox1.Value)
        End Function
        Private Sub ThemeButton_Click(sender As Object, e As RoutedEventArgs)
            ThemeExampleGrid.RequestedTheme = If(ThemeExampleGrid.RequestedTheme = ElementTheme.Dark, ElementTheme.Light, ElementTheme.Dark)
        End Sub
    End Class
End Namespace
