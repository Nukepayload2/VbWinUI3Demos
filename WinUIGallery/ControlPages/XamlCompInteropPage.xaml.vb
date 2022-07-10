' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports System.Numerics
Imports Windows.Foundation.Metadata
Imports Microsoft.UI.Composition
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Automation
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Input

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class XamlCompInteropPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private _compositor As Compositor = Microsoft.UI.Xaml.Media.CompositionTarget.GetCompositorForCurrentThread()
        Private _springAnimation As SpringVector3NaturalMotionAnimation
        Private Sub NaturalMotionExample_Loaded(sender As Object, e As RoutedEventArgs)
            UpdateSpringAnimation(1.0F)
        End Sub
        Private Sub UpdateSpringAnimation(finalValue As Single)
            If _springAnimation Is Nothing Then
                _springAnimation = _compositor.CreateSpringVector3Animation()
                _springAnimation.Target = "Scale"
            End If

            _springAnimation.FinalValue = New Vector3(finalValue)
            _springAnimation.DampingRatio = GetDampingRatio()
            _springAnimation.Period = GetPeriod()
        End Sub
        Private Function GetDampingRatio() As Single
            If DampingStackPanel.SelectedItem IsNot Nothing Then
                Return CSng(Convert.ToDouble(TryCast(DampingStackPanel.SelectedItem, RadioButton).Content))
            End If
            Return 0.6F
        End Function
        Private Function GetPeriod() As TimeSpan
            Return TimeSpan.FromMilliseconds(PeriodSlider.Value)
        End Function
        Private Sub StartAnimationIfAPIPresent(sender As UIElement, animation As Microsoft.UI.Composition.CompositionAnimation)
            TryCast(sender, UIElement).StartAnimation(animation)
        End Sub
        Private Sub element_PointerEntered(sender As Object, e As PointerRoutedEventArgs)
            UpdateSpringAnimation(1.5F)

            StartAnimationIfAPIPresent(TryCast(sender, UIElement), _springAnimation)
        End Sub
        Private Sub element_PointerExited(sender As Object, e As PointerRoutedEventArgs)
            UpdateSpringAnimation(1F)

            StartAnimationIfAPIPresent(TryCast(sender, UIElement), _springAnimation)
        End Sub
        Private Sub ExpressionSample_Loaded(sender As Object, e As RoutedEventArgs)
            Dim anim = _compositor.CreateExpressionAnimation()
            anim.Expression = "Vector3(1/scaleElement.Scale.X, 1/scaleElement.Scale.Y, 1)"
            anim.Target = "Scale"

            anim.SetExpressionReferenceParameter("scaleElement", rectangle)

            StartAnimationIfAPIPresent(ellipse, anim)
        End Sub
        Private Sub StackedButtonsExample_Loaded(sender As Object, e As RoutedEventArgs)
            Dim anim = _compositor.CreateExpressionAnimation()
            anim.Expression = "(above.Scale.Y - 1) * 50 + above.Translation.Y % (50 * index)"
            anim.Target = "Translation.Y"

            anim.SetExpressionReferenceParameter("above", ExpressionButton1)
            anim.SetScalarParameter("index", 1)
            ExpressionButton2.StartAnimation(anim)

            anim.SetExpressionReferenceParameter("above", ExpressionButton2)
            anim.SetScalarParameter("index", 2)
            ExpressionButton3.StartAnimation(anim)

            anim.SetExpressionReferenceParameter("above", ExpressionButton3)
            anim.SetScalarParameter("index", 3)
            ExpressionButton4.StartAnimation(anim)
        End Sub
        Private Sub ActualSizeExample_Loaded(sender As Object, e As RoutedEventArgs)
            ' We will lay out some buttons in a circle.
            ' The formulas we will use are:
            '   X = radius * cos(theta) + xOffset
            '   Y = radius * sin(theta) + yOffset
            '   radius = 1/2 the width and height of the parent container
            '   theta = the angle for each element. The starting value of theta depends on both the number of elements and the relative index of each element.
            '   xOffset = The starting horizontal offset for the element. 
            '   yOffset = The starting vertical offset for the element.

            Dim radius As [String] = "(source.ActualSize.X / 2)" ' Since the layout is a circle, width and height are equivalent meaning we could use X or Y. We'll use X.
            Dim theta As [String] = ".02 * " & radius & " + ((2 * Pi)/total)*index" ' The first value is the rate of angular change based on radius. The last value spaces the buttons equally.
            Dim xOffset As [String] = radius ' We offset x by radius because the buttons naturally layout along the left edge. We need to move them to center of the circle first.
            Dim yOffset As [String] = "0" ' We don't need to offset y because the buttons naturally layout vertically centered.

            ' We combine X, Y, and Z subchannels into a single animation because we can only start a single animation on Translation.
            Dim expression1 As [String] = String.Format("Vector3({0}*cos({1})+{2}, {0}*sin({1})+{3},0)", radius, theta, xOffset, yOffset)

            Dim totalElements As Integer = 8
            For i As Integer = 0 To totalElements - 1
                Dim element As New Button() With
{
                    .Content = "Button"}
                AutomationProperties.SetName(element, "Button " & i)

                LayoutPanel.Children.Add(element)

                Dim anim = _compositor.CreateExpressionAnimation()

                anim.Expression = expression1
                anim.SetScalarParameter("index", i + 1)
                anim.SetScalarParameter("total", totalElements)
                anim.Target = "Translation"
                anim.SetExpressionReferenceParameter("source", LayoutPanel)

                element.StartAnimation(anim)
            Next
        End Sub
        Private Sub RadiusSlider_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs)
            If LayoutPanel Is Nothing Then
                Return
            End If
            LayoutPanel.Width = __InlineAssignHelper(LayoutPanel.Height, e.NewValue)
        End Sub
        Private Sub ActualOffsetExample_Loaded(sender As Object, e As RoutedEventArgs)
            ' This sample positions a popup relative to a block of text that has variable layout size based on font size.
            Dim anim = _compositor.CreateExpressionAnimation()

            anim.Expression = "Vector3(source.ActualOffset.X + source.ActualSize.X, source.ActualOffset.Y + source.ActualSize.Y / 2 - 25, 0)"
            anim.Target = "Translation"
            anim.SetExpressionReferenceParameter("source", PopupTarget)

            Popup.StartAnimation(anim)

            Popup.IsOpen = True
        End Sub
        <Obsolete("Please refactor code that uses this function, it is a simple work-around to simulate inline assignment in VB!")>
        Private Shared Function __InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
End Namespace
