' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports System.Linq
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Media
Imports Microsoft.UI.Xaml.Shapes

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class AcrylicPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
            Loaded += AddressOf AcrylicPage_Loaded
        End Sub
        Private Sub AcrylicPage_Loaded(sender As Object, e As RoutedEventArgs)
            ColorSelectorInApp.SelectedIndex = 0
            FallbackColorSelectorInApp.SelectedIndex = 0
            OpacitySliderInApp.Value = __InlineAssignHelper(OpacitySliderLumin.Value, 0.8)
            LuminositySlider.Value = 0.8
        End Sub
        Private Sub Slider_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs)
            Dim shape As Rectangle = CustomAcrylicShapeInApp
            If CType(sender, Slider) = OpacitySliderLumin Then
                shape = CustomAcrylicShapeLumin
            End If

            CType(shape.Fill, Microsoft.UI.Xaml.Media.AcrylicBrush).TintOpacity = e.NewValue
        End Sub
        Private Sub ColorSelector_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim shape As Rectangle = CustomAcrylicShapeInApp
            CType(shape.Fill, Microsoft.UI.Xaml.Media.AcrylicBrush).TintColor = CType(e.AddedItems.First(), SolidColorBrush).Color
        End Sub
        Private Sub FallbackColorSelector_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim shape As Rectangle = CustomAcrylicShapeInApp
            CType(shape.Fill, Microsoft.UI.Xaml.Media.AcrylicBrush).FallbackColor = CType(e.AddedItems.First(), SolidColorBrush).Color
        End Sub
        Private Sub LuminositySlider_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs)
            Dim shape As Rectangle = CustomAcrylicShapeLumin
            CType(shape.Fill, Microsoft.UI.Xaml.Media.AcrylicBrush).TintLuminosityOpacity = e.NewValue
        End Sub
        <Obsolete("Please refactor code that uses this function, it is a simple work-around to simulate inline assignment in VB!")>
        Private Shared Function __InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
End Namespace
