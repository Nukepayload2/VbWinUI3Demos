' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Numerics
Imports System.Runtime.InteropServices.WindowsRuntime
Imports Windows.Foundation
Imports Windows.Foundation.Collections
Imports Windows.System
Imports Microsoft.UI.Composition
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Data
Imports Microsoft.UI.Xaml.Input
Imports Microsoft.UI.Xaml.Media
Imports Microsoft.UI.Xaml.Navigation

' The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

Namespace AppUIBasics.ControlPages
    ''' <summary>
    ''' An empty page that can be used on its own or navigated to within a Frame.
    ''' </summary>
    Public NotInheritable Partial Class ScrollViewer2Page
        Inherits ItemsPageBase
        Private scroller2MouseIsOver As Boolean = False
        Public Sub New()
            Me.InitializeComponent()
            AddHandler Me.scroller2.StateChanged, AddressOf Scroller_StateChanged
            Me.scroller2.PointerEntered += Me.Scroller2_PointerEntered
            Me.scroller2.PointerExited += Me.Scroller2_PointerExited
        End Sub
        Private Sub Scroller2_PointerExited(sender As Object, e As PointerRoutedEventArgs)
            scroller2MouseIsOver = False
        End Sub
        Private Sub Scroller2_PointerEntered(sender As Object, e As PointerRoutedEventArgs)
            scroller2MouseIsOver = True
        End Sub
#Region "Zooming"

        Private Sub ZoomModeComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            If scroller2 IsNot Nothing AndAlso ZoomSlider IsNot Nothing Then
                Dim cb As ComboBox = TryCast(sender, ComboBox)
                If cb IsNot Nothing Then
                    Select Case cb.SelectedIndex
                        Case 0
                            scroller2.ZoomMode = muxc.ZoomMode.Enabled
                            ZoomSlider.IsEnabled = True
                        Case Else
                            scroller2.ZoomMode = muxc.ZoomMode.Disabled
                            scroller2.ZoomTo(1.0F, New Vector2)
                            ZoomSlider.Value = 1
                            ZoomSlider.IsEnabled = False
                    End Select
                End If
            End If
        End Sub
        Private Sub Scroller_StateChanged(sender As muxc.ScrollViewer, args As Object)

            ' Checking if sender is idle and the scrollviewer has the mouse over itsself 
            ' since when using slider, the sender may sometimes still be idle while
            ' user is still changing the slider
            If sender.State = muxc.InteractionState.Idle AndAlso scroller2MouseIsOver Then
                ZoomSlider.Value = Math.Round(sender.ZoomFactor, CInt(CLng(Fix((10 * ZoomSlider.StepFrequency))) Mod Integer.MaxValue))
            End If
        End Sub
        Private Sub ZoomSlider_ValueChanged(sender As Object, e As RangeBaseValueChangedEventArgs)
            If scroller2 IsNot Nothing Then
                ' Zoom based on the center point of the current viewport
                scroller2.ZoomTo(CSng(e.NewValue), Nothing)
            End If
        End Sub
#End Region
#Region "Custom Animations"
        Public Property Sample4MaximumXViewportPosition As Double
            Get
                Return CDbl(GetValue(Sample4MaximumXViewportPositionProperty))
            End Get

            Set(value As Double)
                SetValue(Sample4MaximumXViewportPositionProperty, value)
            End Set
        End Property
        Public Shared ReadOnly Sample4MaximumXViewportPositionProperty As DependencyProperty = DependencyProperty.Register( _
                NameOf(Sample4MaximumXViewportPosition),
GetType(Double),
GetType(ScrollViewer2Page),
                New PropertyMetadata(0.0))
        Public Property Sample4MaximumYViewportPosition As Double
            Get
                Return CDbl(GetValue(Sample4MaximumYViewportPositionProperty))
            End Get

            Set(value As Double)
                SetValue(Sample4MaximumYViewportPositionProperty, value)
            End Set
        End Property
        Public Shared ReadOnly Sample4MaximumYViewportPositionProperty As DependencyProperty = DependencyProperty.Register( _
                NameOf(Sample4MaximumYViewportPosition),
GetType(Double),
GetType(ScrollViewer2Page),
                New PropertyMetadata(0.0))
        Private Sub Sample4_ImageLoaded(sender As Object, e As RoutedEventArgs)
            Dim image1 = TryCast(sender, Image)
            Sample4MaximumXViewportPosition = image1.ActualWidth - CType(image1.Parent, FrameworkElement).ActualWidth
            Sample4MaximumYViewportPosition = image1.ActualHeight - CType(image1.Parent, FrameworkElement).ActualHeight
        End Sub
        Private Sub Go_Click(sender As Object, e As RoutedEventArgs)
            Dim newZoomFactor = zoomFactorSlider.Value

            Dim newX = (xposSlider.Value / xposSlider.Maximum) * (image1.ActualWidth * newZoomFactor - scroller4.ActualWidth)
            Dim newY = (yposSlider.Value / yposSlider.Maximum) * (image1.ActualHeight * newZoomFactor - scroller4.ActualHeight)

            ' Scroll
            scroller4.ScrollTo(newX, newY)

            ' Zoom
            scroller4.ZoomTo(CSng(newZoomFactor), Nothing)
        End Sub
#Region "Adjust scrolling animation"

        Private Sub Scroller4_ScrollAnimationStarting(sender As muxc.ScrollViewer, args As muxc.ScrollAnimationStartingEventArgs)
            Try
                Dim stockKeyFrameAnimation As Vector3KeyFrameAnimation = TryCast(args.Animation, Vector3KeyFrameAnimation)

                If stockKeyFrameAnimation IsNot Nothing Then
                    Dim customKeyFrameAnimation As Vector3KeyFrameAnimation = stockKeyFrameAnimation

                    If NameOf(ScrollAnimationOptions.[Default]) <> CStr(cbAnimation.SelectedItem) Then
                        Dim targetHorizontalOffset As Double = args.EndPosition.X
                        Dim targetHorizontalPosition As Single = ComputeHorizontalPositionFromOffset(sender.Content, targetHorizontalOffset, sender.ZoomFactor)

                        Dim targetVerticalOffset As Double = args.EndPosition.Y
                        Dim targetVerticalPosition As Single = ComputeVerticalPositionFromOffset(sender.Content, targetVerticalOffset, sender.ZoomFactor)

                        customKeyFrameAnimation = stockKeyFrameAnimation.Compositor.CreateVector3KeyFrameAnimation()

                        Dim deltaHorizontalPosition As Single = CSng((targetHorizontalOffset - sender.HorizontalOffset))
                        Dim deltaVerticalPosition As Single = CSng((targetVerticalOffset - sender.VerticalOffset))

                        Select Case CStr(cbAnimation.SelectedItem)
                            Case NameOf(ScrollAnimationOptions.Custom1)
                                ' "Accordion" case
                                For keyframe As Integer = 0 To 3 - 1
                                    customKeyFrameAnimation.InsertKeyFrame( _
        1.0F - (0.4F / CSng(Math.Pow(2, keyframe))),
        New Vector3(targetHorizontalPosition + 0.1F * deltaHorizontalPosition, targetVerticalPosition + 0.1F * deltaVerticalPosition, 0.0F))

                                    deltaHorizontalPosition /= -2.0F
                                    deltaVerticalPosition /= -2.0F
                                Next

                                customKeyFrameAnimation.InsertKeyFrame(1.0F, New Vector3(targetHorizontalPosition, targetVerticalPosition, 0.0F))
                            Case NameOf(ScrollAnimationOptions.Custom2)
                                ' "Teleportation" case
                                Dim cubicBezierStart As CubicBezierEasingFunction = stockKeyFrameAnimation.Compositor.CreateCubicBezierEasingFunction( _
                                    New Vector2(1.0F, 0.0F),
                                    New Vector2(1.0F, 0.0F))

                                Dim [step] As StepEasingFunction = stockKeyFrameAnimation.Compositor.CreateStepEasingFunction(1)

                                Dim cubicBezierEnd As CubicBezierEasingFunction = stockKeyFrameAnimation.Compositor.CreateCubicBezierEasingFunction( _
                                    New Vector2(0.0F, 1.0F),
                                    New Vector2(0.0F, 1.0F))

                                customKeyFrameAnimation.InsertKeyFrame( _
                                    0.499999F,
                                    New Vector3(targetHorizontalPosition - 0.75F * deltaHorizontalPosition, targetVerticalPosition - 0.75F * deltaVerticalPosition, 0.0F),
                                    cubicBezierStart)
                                customKeyFrameAnimation.InsertKeyFrame( _
                                    0.5F,
                                    New Vector3(targetHorizontalPosition - 0.25F * deltaHorizontalPosition, targetVerticalPosition - 0.25F * deltaVerticalPosition, 0.0F),
[step])
                                customKeyFrameAnimation.InsertKeyFrame( _
                                    1.0F,
                                    New Vector3(targetHorizontalPosition, targetVerticalPosition, 0.0F),
                                    cubicBezierEnd)
                            Case Else
                        End Select

                        customKeyFrameAnimation.Duration = stockKeyFrameAnimation.Duration
                    End If

                    If Not String.IsNullOrWhiteSpace(tbAnimDuration.Text) Then
                        ' Override animation duration
                        Dim durationOverride As Double = Convert.ToDouble(tbAnimDuration.Text)
                        customKeyFrameAnimation.Duration = TimeSpan.FromMilliseconds(durationOverride)
                    End If

                    args.Animation = customKeyFrameAnimation
                End If
            Catch ex As Exception
            
            End Try
        End Sub
#End Region

#Region "Adjust zooming animation"

        Private Sub Scroller4_ZoomAnimationStarting(sender As muxc.ScrollViewer, args As muxc.ZoomAnimationStartingEventArgs)
            Try
                Dim stockKeyFrameAnimation As ScalarKeyFrameAnimation = TryCast(args.Animation, ScalarKeyFrameAnimation)

                If stockKeyFrameAnimation IsNot Nothing Then
                    Dim customKeyFrameAnimation As ScalarKeyFrameAnimation = stockKeyFrameAnimation

                    If NameOf(ZoomAnimationOptions.[Default]) <> CStr(cbZoomAnimation.SelectedItem) Then
                        Dim targetZoomFactor As Single = CSng(zoomFactorSlider.Value)

                        customKeyFrameAnimation = stockKeyFrameAnimation.Compositor.CreateScalarKeyFrameAnimation()
                        Dim deltaZoomFactor As Single = CSng((targetZoomFactor - sender.ZoomFactor))

                        Select Case CStr(cbZoomAnimation.SelectedItem)

                            Case NameOf(ZoomAnimationOptions.Custom1)
                                ' "Accordion" case
                                For [step] As Integer = 0 To 3 - 1
                                    customKeyFrameAnimation.InsertKeyFrame( _
        1.0F - (0.4F / CSng(Math.Pow(2, [step]))),
        targetZoomFactor + 0.1F * deltaZoomFactor)
                                    deltaZoomFactor /= -2.0F
                                Next

                                customKeyFrameAnimation.InsertKeyFrame(1.0F, targetZoomFactor)
                            Case NameOf(ZoomAnimationOptions.Custom2)
                                ' "Teleportation" case

                                Dim cubicBezierStart As CubicBezierEasingFunction = stockKeyFrameAnimation.Compositor.CreateCubicBezierEasingFunction( _
                                    New Vector2(1.0F, 0.0F),
                                    New Vector2(1.0F, 0.0F))

                                Dim stepEasingFunc As StepEasingFunction = stockKeyFrameAnimation.Compositor.CreateStepEasingFunction(1)

                                Dim cubicBezierEnd As CubicBezierEasingFunction = stockKeyFrameAnimation.Compositor.CreateCubicBezierEasingFunction( _
                                    New Vector2(0.0F, 1.0F),
                                    New Vector2(0.0F, 1.0F))

                                customKeyFrameAnimation.InsertKeyFrame( _
                                    0.499999F,
                                    targetZoomFactor - 0.75F * deltaZoomFactor,
                                    cubicBezierStart)
                                customKeyFrameAnimation.InsertKeyFrame( _
                                    0.5F,
                                    targetZoomFactor - 0.25F * deltaZoomFactor,
                                    stepEasingFunc)
                                customKeyFrameAnimation.InsertKeyFrame( _
                                    1.0F,
                                    targetZoomFactor,
                                    cubicBezierEnd)
                            Case Else
                        End Select

                        customKeyFrameAnimation.Duration = stockKeyFrameAnimation.Duration
                        args.Animation = customKeyFrameAnimation
                    End If

                    If Not String.IsNullOrWhiteSpace(tbZoomDuration.Text) Then
                        Dim durationOverride As Double = Convert.ToDouble(tbZoomDuration.Text)
                        customKeyFrameAnimation.Duration = TimeSpan.FromMilliseconds(durationOverride)
                    End If
                End If
            Catch ex As Exception
            
            End Try
        End Sub
#End Region

#Region "Helper methods"

        Private Function ComputeHorizontalPositionFromOffset(content1 As UIElement, offset As Double, zoomFactor1 As Single) As Single
            Return CSng((offset + ComputeMinHorizontalPosition(content1, zoomFactor1)))
        End Function
        Private Function ComputeVerticalPositionFromOffset(content1 As UIElement, offset As Double, zoomFactor1 As Single) As Single
            Return CSng((offset + ComputeMinVerticalPosition(content1, zoomFactor1)))
        End Function
        Private Function ComputeMinHorizontalPosition(content1 As UIElement, zoomFactor1 As Single) As Single
            If content1 Is Nothing Then
                Return 0
            End If

            Dim contentAsFE As FrameworkElement = TryCast(content, FrameworkElement)

            If contentAsFE Is Nothing Then
                Return 0
            End If

            Dim childMargin As Thickness = contentAsFE.Margin
            Dim scrollerVisual As Visual = Microsoft.UI.Xaml.Hosting.ElementCompositionPreview.GetElementVisual(scroller4)
            Dim childWidth As Double = If(Double.IsNaN(contentAsFE.Width), contentAsFE.ActualWidth, contentAsFE.Width)
            Dim minPosX As Single = 0.0F
            Dim extentWidth As Single = Math.Max(0.0F, CSng((childWidth + childMargin.Left + childMargin.Right)))

            If contentAsFE.HorizontalAlignment = HorizontalAlignment.Center OrElse _
                contentAsFE.HorizontalAlignment = HorizontalAlignment.Stretch Then
                minPosX = Math.Min(0.0F, (extentWidth * zoomFactor1 - scrollerVisual.Size.X) / 2.0F)
            ElseIf contentAsFE.HorizontalAlignment = HorizontalAlignment.Right Then
                minPosX = Math.Min(0.0F, extentWidth * zoomFactor1 - scrollerVisual.Size.X)
            End If

            Return minPosX
        End Function
        Private Function ComputeMinVerticalPosition(content1 As UIElement, zoomFactor1 As Single) As Single
            If content1 Is Nothing Then
                Return 0
            End If

            Dim contentAsFE As FrameworkElement = TryCast(content, FrameworkElement)

            If contentAsFE Is Nothing Then
                Return 0
            End If

            Dim childMargin As Thickness = contentAsFE.Margin
            Dim scrollerVisual As Visual = Microsoft.UI.Xaml.Hosting.ElementCompositionPreview.GetElementVisual(scroller4)
            Dim childHeight As Double = If(Double.IsNaN(contentAsFE.Height), contentAsFE.ActualHeight, contentAsFE.Height)
            Dim minPosY As Single = 0.0F
            Dim extentHeight As Single = Math.Max(0.0F, CSng((childHeight + childMargin.Top + childMargin.Bottom)))

            If contentAsFE.VerticalAlignment = VerticalAlignment.Center OrElse _
                contentAsFE.VerticalAlignment = VerticalAlignment.Stretch Then
                minPosY = Math.Min(0.0F, (extentHeight * zoomFactor1 - scrollerVisual.Size.Y) / 2.0F)
            ElseIf contentAsFE.VerticalAlignment = VerticalAlignment.Bottom Then
                minPosY = Math.Min(0.0F, extentHeight * zoomFactor1 - scrollerVisual.Size.Y)
            End If

            Return minPosY
        End Function
        Public Function FormatDouble(value As Double) As String
            Return value.ToString("G2")
        End Function
#End Region

#End Region

#Region "x:Bind Converter Helpers"

        Private Function ObjectToScrollControllerVisibility(value As Object) As muxc.ScrollBarVisibility
            Dim output As muxc.ScrollBarVisibility = Nothing
            [Enum].TryParse(Of muxc.ScrollBarVisibility)(TryCast(value, String), output)
            Return output
        End Function
        Private Function ObjectToScrollMode(value As Object) As muxc.ScrollMode
            Dim output As muxc.ScrollMode = Nothing
            [Enum].TryParse(Of muxc.ScrollMode)(TryCast(value, String), output)
            Return output
        End Function
        Private Function ObjectToContentOrientation(value As Object) As muxc.ContentOrientation
            Dim output As muxc.ContentOrientation = Nothing
            [Enum].TryParse(Of muxc.ContentOrientation)(TryCast(value, String), output)
            Return output
        End Function
        Private Sub LvIgnoredInputKinds_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim listView1 = TryCast(sender, ListView)

            Dim stringified As String = [String].Join(","c, listView1.SelectedItems)
            Dim output As muxc.InputKind = Nothing
            [Enum].TryParse(Of muxc.InputKind)(stringified, output)

            scroller3.IgnoredInputKind = output
        End Sub
#End Region

#Region "Property values for binding available options"

        Private Enum ScrollAnimationOptions
            [Default]
            Custom1
            Custom2
        End Enum
        Public Enum ZoomAnimationOptions
            [Default]
            Custom1
            Custom2
        End Enum
        Public ReadOnly Property ZoomModes As String()
            Get
                Return [Enum].GetNames(GetType(muxc.ZoomMode))
            End Get
        End Property
        Public ReadOnly Property ScrollModes As String()
            Get
                Return [Enum].GetNames(GetType(muxc.ScrollMode))
            End Get
        End Property
        Public ReadOnly Property ScrollBarVisibility As String()
            Get
                Return [Enum].GetNames(GetType(muxc.ScrollBarVisibility))
            End Get
        End Property
        Public ReadOnly Property ContentOrientation As String()
            Get
                Return [Enum].GetNames(GetType(muxc.ContentOrientation))
            End Get
        End Property
        Public ReadOnly Property ScrollingAnimations As String()
            Get
                Return [Enum].GetNames(GetType(ScrollAnimationOptions))
            End Get
        End Property
        Public ReadOnly Property ZoomingAnimations As String()
            Get
                Return [Enum].GetNames(GetType(ZoomAnimationOptions))
            End Get
        End Property
        Public ReadOnly Property InputKinds As String()
            Get
                Return [Enum].GetNames(GetType(muxc.InputKind))
            End Get
        End Property
#End Region

        Private Sub Scroller_HandleKeyDown(sender As Object, e As KeyRoutedEventArgs)
            ' Swallow up and down for gamepad / keyboard input when focused to prevent the Page's ScrollViewer from scrollling
            Select Case e.Key
                Case VirtualKey.Up, VirtualKey.Down
                    e.Handled = True
            End Select
        End Sub
    End Class
End Namespace
