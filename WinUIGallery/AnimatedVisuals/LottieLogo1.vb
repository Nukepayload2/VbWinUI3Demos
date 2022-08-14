Imports Microsoft.Graphics.Canvas.Geometry
Imports Microsoft.UI.Xaml.Controls
Imports System
Imports System.Numerics
Imports Microsoft.UI
Imports Microsoft.UI.Composition
Imports System.Runtime.InteropServices

Namespace AnimatedVisuals
    NotInheritable Class LottieLogo1
        Implements IAnimatedVisualSource
        Public Function TryCreateAnimatedVisual(compositor1 As Compositor, <Out> ByRef diagnostics As Object) As IAnimatedVisual Implements IAnimatedVisualSource.TryCreateAnimatedVisual
            diagnostics = Nothing
            Return New AnimatedVisual(compositor1)
        End Function

        NotInheritable Class AnimatedVisual
            Implements IAnimatedVisual
            Const c_durationTicks As Long = 59670000
            ReadOnly _c As Compositor
            ReadOnly _reusableExpressionAnimation As ExpressionAnimation
            Private _colorBrush_AlmostTeal_FF007A87 As CompositionColorBrush
            Private _colorBrush_White As CompositionColorBrush
            Private _compositionPath_00 As CompositionPath
            Private _compositionPath_01 As CompositionPath
            Private _compositionPath_02 As CompositionPath
            Private _compositionPath_03 As CompositionPath
            Private _compositionPath_04 As CompositionPath
            Private _compositionPath_05 As CompositionPath
            Private _compositionPath_06 As CompositionPath
            Private _compositionPath_07 As CompositionPath
            Private _compositionPath_08 As CompositionPath
            Private _cubicBezierEasingFunction_02 As CubicBezierEasingFunction
            Private _cubicBezierEasingFunction_03 As CubicBezierEasingFunction
            Private _cubicBezierEasingFunction_04 As CubicBezierEasingFunction
            Private _cubicBezierEasingFunction_05 As CubicBezierEasingFunction
            Private _cubicBezierEasingFunction_07 As CubicBezierEasingFunction
            Private _cubicBezierEasingFunction_08 As CubicBezierEasingFunction
            Private _cubicBezierEasingFunction_11 As CubicBezierEasingFunction
            Private _cubicBezierEasingFunction_12 As CubicBezierEasingFunction
            Private _cubicBezierEasingFunction_15 As CubicBezierEasingFunction
            Private _cubicBezierEasingFunction_16 As CubicBezierEasingFunction
            Private _cubicBezierEasingFunction_23 As CubicBezierEasingFunction
            Private _ellipse_4p7 As CompositionEllipseGeometry
            Private _holdThenStepEasingFunction As StepEasingFunction
            Private _linearEasingFunction As LinearEasingFunction
            Private _root As ContainerVisual
            Private _scalarAnimation_0_to_0p249 As ScalarKeyFrameAnimation
            Private _scalarAnimation_0_to_1_2 As ScalarKeyFrameAnimation
            Private _scalarAnimation_0p87_to_0_02 As ScalarKeyFrameAnimation
            Private _scalarAnimation_1_to_0_10 As ScalarKeyFrameAnimation
            Private _scalarAnimation_1_to_0_12 As ScalarKeyFrameAnimation
            Private _scalarAnimation_1_to_0_13 As ScalarKeyFrameAnimation
            Private _scalarAnimation_1_to_0_14 As ScalarKeyFrameAnimation
            Private _scalarAnimation_1_to_0_15 As ScalarKeyFrameAnimation
            Private _scalarAnimation_1_to_0_18 As ScalarKeyFrameAnimation
            Private _scalarAnimation_1_to_0_19 As ScalarKeyFrameAnimation
            Private _scalarAnimation_1_to_0_27 As ScalarKeyFrameAnimation
            Private _scalarAnimation_1_to_0_28 As ScalarKeyFrameAnimation
            Private _scalarAnimation_1_to_0_30 As ScalarKeyFrameAnimation
            Private _scalarAnimation_1_to_0_31 As ScalarKeyFrameAnimation
            Private _scalarAnimation_to_1_02 As ScalarKeyFrameAnimation
            Private _scalarAnimation_to_1_06 As ScalarKeyFrameAnimation
            Private _scalarExpressionAnimation As ExpressionAnimation
            Private _stepThenHoldEasingFunction As StepEasingFunction
            Private _vector2Animation_02 As Vector2KeyFrameAnimation
            Private _vector2Animation_03 As Vector2KeyFrameAnimation
            Private _vector2Animation_04 As Vector2KeyFrameAnimation
            Private _vector2Animation_05 As Vector2KeyFrameAnimation
            Private _vector2Animation_06 As Vector2KeyFrameAnimation
            Private _vector2Animation_07 As Vector2KeyFrameAnimation
            ' Rectangle Path 1
            Private Function ColorBrush_AlmostDarkTurquoise_FF00D1C1() As CompositionColorBrush
                Return _c.CreateColorBrush(Windows.UI.Color.FromArgb(&HFF, &H0, &HD1, &HC1))
            End Function
            Private Function ColorBrush_AlmostTeal_FF007A87() As CompositionColorBrush
                Return __InlineAssignHelper(_colorBrush_AlmostTeal_FF007A87, _c.CreateColorBrush(Windows.UI.Color.FromArgb(&HFF, &H0, &H7A, &H87)))
            End Function
            Private Function ColorBrush_White() As CompositionColorBrush
                Return __InlineAssignHelper(_colorBrush_White, _c.CreateColorBrush(Windows.UI.Color.FromArgb(&HFF, &HFF, &HFF, &HFF)))
            End Function
            Private Function CompositionPath_00() As CompositionPath
                Dim result = __InlineAssignHelper(_compositionPath_00, New CompositionPath(Geometry_00()))
                Return result
            End Function
            Private Function CompositionPath_01() As CompositionPath
                Dim result = __InlineAssignHelper(_compositionPath_01, New CompositionPath(Geometry_01()))
                Return result
            End Function
            Private Function CompositionPath_02() As CompositionPath
                Dim result = __InlineAssignHelper(_compositionPath_02, New CompositionPath(Geometry_02()))
                Return result
            End Function
            Private Function CompositionPath_03() As CompositionPath
                Dim result = __InlineAssignHelper(_compositionPath_03, New CompositionPath(Geometry_03()))
                Return result
            End Function
            Private Function CompositionPath_04() As CompositionPath
                Dim result = __InlineAssignHelper(_compositionPath_04, New CompositionPath(Geometry_04()))
                Return result
            End Function
            Private Function CompositionPath_05() As CompositionPath
                Dim result = __InlineAssignHelper(_compositionPath_05, New CompositionPath(Geometry_05()))
                Return result
            End Function
            Private Function CompositionPath_06() As CompositionPath
                Dim result = __InlineAssignHelper(_compositionPath_06, New CompositionPath(Geometry_06()))
                Return result
            End Function
            Private Function CompositionPath_07() As CompositionPath
                Dim result = __InlineAssignHelper(_compositionPath_07, New CompositionPath(Geometry_07()))
                Return result
            End Function
            Private Function CompositionPath_08() As CompositionPath
                Dim result = __InlineAssignHelper(_compositionPath_08, New CompositionPath(Geometry_08()))
                Return result
            End Function
            ' Layer (Shape): Dot-Y
            Private Function ContainerShape_00() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_01())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_to_1_00())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): Dot-Y
            Private Function ContainerShape_01() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 154.457F, 287.822F)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_02())
                Return result
            End Function
            ' Transforms for Bncr
            Private Function ContainerShape_02() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(164.782F, 57.473F))
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_03())
                result.StartAnimation("Position", Vector2Animation_01())
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(60,60)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Transforms for Dot-Y
            Private Function ContainerShape_03() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(43.263F, 59.75F))
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_01())
                result.StartAnimation("Position", Vector2Animation_00())
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", ScalarExpressionAnimation())
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(196.791,266.504)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): E3-Y
            Private Function ContainerShape_04() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_05())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_00())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): E3-Y
            Private Function ContainerShape_05() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 154.457F, 287.822F)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_06())
                Return result
            End Function
            ' Transforms for E3-Y
            Private Function ContainerShape_06() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(119.167F, 57.479F))
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_02())
                result.StartAnimation("Position", Vector2Animation_02())
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(345.124,261.801)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): E3-B
            Private Function ContainerShape_07() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_08())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_to_1_01())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): E3-B
            Private Function ContainerShape_08() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 154.457F, 287.822F)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_09())
                Return result
            End Function
            ' Transforms for E3-Y
            Private Function ContainerShape_09() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(119.167F, 57.479F))
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_03())
                result.StartAnimation("Position", _vector2Animation_02)
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(345.124,261.801)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): I-Y
            Private Function ContainerShape_10() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_11())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_01())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): I-Y
            Private Function ContainerShape_11() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 154.457F, 287.822F)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_12())
                Return result
            End Function
            ' Transforms for I-Y
            Private Function ContainerShape_12() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(93.594F, 62.861F))
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_04())
                result.StartAnimation("Position", Vector2Animation_03())
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(303.802,282.182)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): I-B
            Private Function ContainerShape_13() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_14())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_to_1_02())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): I-B
            Private Function ContainerShape_14() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 154.457F, 287.822F)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_15())
                Return result
            End Function
            ' Transforms for I-Y
            Private Function ContainerShape_15() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(93.594F, 62.861F))
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_05())
                result.StartAnimation("Position", _vector2Animation_03)
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(303.802,282.182)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): E2-Y
            Private Function ContainerShape_16() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_17())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_02())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): E2-Y
            Private Function ContainerShape_17() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 154.457F, 287.822F)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_18())
                Return result
            End Function
            ' Transforms for E2-Y
            Private Function ContainerShape_18() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(109.092F, 33.61F))
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_06())
                result.StartAnimation("Position", Vector2Animation_04())
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(332.05,237.932)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): E2-B
            Private Function ContainerShape_19() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_20())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_to_1_03())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): E2-B
            Private Function ContainerShape_20() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 154.457F, 287.822F)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_21())
                Return result
            End Function
            ' Transforms for E2-Y
            Private Function ContainerShape_21() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(109.092F, 33.61F))
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_07())
                result.StartAnimation("Position", _vector2Animation_04)
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(332.05,237.932)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): E1-Y
            Private Function ContainerShape_22() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_23())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_03())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): E1-Y
            Private Function ContainerShape_23() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 154.457F, 287.822F)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_24())
                Return result
            End Function
            ' Transforms for E1-Y
            Private Function ContainerShape_24() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(113.715F, 9.146F))
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_08())
                result.StartAnimation("Position", Vector2Animation_05())
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(344.672,214.842)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): E1-B
            Private Function ContainerShape_25() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_26())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_to_1_04())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): E1-B
            Private Function ContainerShape_26() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 154.457F, 287.822F)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_27())
                Return result
            End Function
            ' Transforms for E1-Y
            Private Function ContainerShape_27() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(113.715F, 9.146F))
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_09())
                result.StartAnimation("Position", _vector2Animation_05)
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(344.672,214.842)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T1a-Y
            Private Function ContainerShape_28() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_29())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_04())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T1a-Y
            Private Function ContainerShape_29() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 154.457F, 287.822F)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_30())
                Return result
            End Function
            ' Transforms for T1a-Y
            Private Function ContainerShape_30() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(39.043F, 48.678F))
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_10())
                result.StartAnimation("Position", Vector2Animation_06())
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(250,250)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T2b-Y
            Private Function ContainerShape_31() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_11())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_05())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T2a-Y
            Private Function ContainerShape_32() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_12())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_06())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T2b-B
            Private Function ContainerShape_33() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_13())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_to_1_05())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T1b-Y
            Private Function ContainerShape_34() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_14())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_07())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T1b-B
            Private Function ContainerShape_35() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_15())
                result.StartAnimation("TransformMatrix._11", _scalarAnimation_to_1_02)
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): O-Y
            Private Function ContainerShape_36() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_37())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_to_1_06())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): O-Y
            Private Function ContainerShape_37() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 154.457F, 287.822F)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_38())
                Return result
            End Function
            ' Transforms for O-Y
            Private Function ContainerShape_38() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(-62.792F, 73.057F))
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_16())
                result.StartAnimation("Position", Vector2Animation_08())
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(196.791,266.504)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): O-B
            Private Function ContainerShape_39() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_40())
                result.StartAnimation("TransformMatrix._11", _scalarAnimation_to_1_06)
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): O-B
            Private Function ContainerShape_40() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 154.457F, 287.822F)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_41())
                Return result
            End Function
            ' Transforms for O-B
            Private Function ContainerShape_41() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(-62.792F, 73.057F))
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_17())
                result.StartAnimation("Position", Vector2Animation_09())
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(196.791,266.504)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T1a-Y 2
            Private Function ContainerShape_42() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_43())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_to_1_07())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T1a-Y 2
            Private Function ContainerShape_43() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 154.457F, 287.822F)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_44())
                Return result
            End Function
            ' Transforms for T1a-Y 2
            Private Function ContainerShape_44() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(39.043F, 48.678F))
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_18())
                result.StartAnimation("Position", _vector2Animation_06)
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(250,250)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T2a-B
            Private Function ContainerShape_45() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_19())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_to_1_08())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T1a-B
            Private Function ContainerShape_46() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_47())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_to_1_09())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T1a-B
            Private Function ContainerShape_47() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 154.457F, 287.822F)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_48())
                Return result
            End Function
            ' Transforms for T1a-Y
            Private Function ContainerShape_48() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(39.043F, 48.678F))
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_20())
                result.StartAnimation("Position", _vector2Animation_06)
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(250,250)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): Dot-Y
            Private Function ContainerShape_49() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_50())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_08())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): Dot-Y
            Private Function ContainerShape_50() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 154.457F, 287.822F)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_51())
                Return result
            End Function
            ' Transforms for N
            Private Function ContainerShape_51() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(-33.667F, 8.182F))
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_52())
                result.StartAnimation("Position", Vector2Animation_11())
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(60,60)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Transforms for Dot-Y
            Private Function ContainerShape_52() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(39.875F, 60))
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_21())
                result.StartAnimation("Position", Vector2Animation_10())
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(196.791,266.504)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): L-Y
            Private Function ContainerShape_53() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_22())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_to_1_10())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): L-B
            Private Function ContainerShape_54() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_23())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_to_1_11())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): Dot1
            Private Function ContainerShape_55() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_56())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_to_0())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): Dot1
            Private Function ContainerShape_56() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 154.457F, 287.822F)
                Dim shapes1 = result.Shapes
                shapes1.Add(ContainerShape_57())
                Return result
            End Function
            ' Transforms for Dot1
            Private Function ContainerShape_57() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                Dim propertySet = result.Properties
                propertySet.InsertVector2("Position", New Vector2(295.771F, 108.994F))
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_24())
                result.StartAnimation("Position", Vector2Animation_12())
                Dim controller = result.TryGetAnimationController("Position")
                controller.Pause()
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "(_.Progress * 0.9835165) + 0.01648352"
                _reusableExpressionAnimation.SetReferenceParameter("_", _root)
                controller.StartAnimation("Progress", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.Position - Vector2(196.791,266.504)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("Offset", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S1-Y
            Private Function ContainerShape_58() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_25())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_10())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S2-Y
            Private Function ContainerShape_59() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_26())
                result.StartAnimation("TransformMatrix._11", _scalarAnimation_1_to_0_10)
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S7
            Private Function ContainerShape_60() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_27())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_13())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S8
            Private Function ContainerShape_61() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_28())
                result.StartAnimation("TransformMatrix._11", _scalarAnimation_1_to_0_13)
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S3-Y
            Private Function ContainerShape_62() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_29())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_15())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S4-Y
            Private Function ContainerShape_63() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_30())
                result.StartAnimation("TransformMatrix._11", _scalarAnimation_1_to_0_15)
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S5-Y
            Private Function ContainerShape_64() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_31())
                result.StartAnimation("TransformMatrix._11", _scalarAnimation_1_to_0_15)
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S6-Y
            Private Function ContainerShape_65() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_32())
                result.StartAnimation("TransformMatrix._11", _scalarAnimation_1_to_0_15)
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S3-Y 2
            Private Function ContainerShape_66() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_33())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_19())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S4-Y 2
            Private Function ContainerShape_67() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_34())
                result.StartAnimation("TransformMatrix._11", _scalarAnimation_1_to_0_19)
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S5-Y 2
            Private Function ContainerShape_68() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_35())
                result.StartAnimation("TransformMatrix._11", _scalarAnimation_1_to_0_19)
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S11
            Private Function ContainerShape_69() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_36())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_22())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S12
            Private Function ContainerShape_70() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_37())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_24())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S13
            Private Function ContainerShape_71() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_38())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_26())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S3-Y 3
            Private Function ContainerShape_72() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_39())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_28())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S4-Y 3
            Private Function ContainerShape_73() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_40())
                result.StartAnimation("TransformMatrix._11", _scalarAnimation_1_to_0_28)
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S5-Y 3
            Private Function ContainerShape_74() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_41())
                result.StartAnimation("TransformMatrix._11", _scalarAnimation_1_to_0_28)
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S3-Y 4
            Private Function ContainerShape_75() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_42())
                result.StartAnimation("TransformMatrix._11", ScalarAnimation_1_to_0_31())
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S4-Y 4
            Private Function ContainerShape_76() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_43())
                result.StartAnimation("TransformMatrix._11", _scalarAnimation_1_to_0_31)
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): S5-Y 4
            Private Function ContainerShape_77() As CompositionContainerShape
                Dim result = _c.CreateContainerShape()
                result.TransformMatrix = New Matrix3x2(0, 0, 0, 0, 0, 0)
                Dim shapes1 = result.Shapes
                shapes1.Add(SpriteShape_44())
                result.StartAnimation("TransformMatrix._11", _scalarAnimation_1_to_0_31)
                Dim controller = result.TryGetAnimationController("TransformMatrix._11")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "my.TransformMatrix._11"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TransformMatrix._22", _reusableExpressionAnimation)
                Return result
            End Function
            ' Transforms: Dot-Y
            '   Position
            Private Function CubicBezierEasingFunction_00() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0, 0), New Vector2(0, 0.812F))
            End Function
            ' Transforms: Dot-Y
            '   Position
            Private Function CubicBezierEasingFunction_01() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.39F, 0.707F), New Vector2(0.708F, 1))
            End Function
            Private Function CubicBezierEasingFunction_02() As CubicBezierEasingFunction
                Return __InlineAssignHelper(_cubicBezierEasingFunction_02, _c.CreateCubicBezierEasingFunction(New Vector2(0.167F, 0.167F), New Vector2(0.18F, 1)))
            End Function
            Private Function CubicBezierEasingFunction_03() As CubicBezierEasingFunction
                Return __InlineAssignHelper(_cubicBezierEasingFunction_03, _c.CreateCubicBezierEasingFunction(New Vector2(0.82F, 0), New Vector2(0.833F, 0.833F)))
            End Function
            Private Function CubicBezierEasingFunction_04() As CubicBezierEasingFunction
                Return __InlineAssignHelper(_cubicBezierEasingFunction_04, _c.CreateCubicBezierEasingFunction(New Vector2(0.167F, 0.167F), New Vector2(0.833F, 0.833F)))
            End Function
            Private Function CubicBezierEasingFunction_05() As CubicBezierEasingFunction
                Return __InlineAssignHelper(_cubicBezierEasingFunction_05, _c.CreateCubicBezierEasingFunction(New Vector2(0.167F, 0.167F), New Vector2(0.667F, 1)))
            End Function
            ' Position
            Private Function CubicBezierEasingFunction_06() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.167F, 0), New Vector2(0.667F, 1))
            End Function
            Private Function CubicBezierEasingFunction_07() As CubicBezierEasingFunction
                Return __InlineAssignHelper(_cubicBezierEasingFunction_07, _c.CreateCubicBezierEasingFunction(New Vector2(0.167F, 0.167F), New Vector2(0.12F, 1)))
            End Function
            Private Function CubicBezierEasingFunction_08() As CubicBezierEasingFunction
                Return __InlineAssignHelper(_cubicBezierEasingFunction_08, _c.CreateCubicBezierEasingFunction(New Vector2(0.167F, 0), New Vector2(0.12F, 1)))
            End Function
            ' Position
            Private Function CubicBezierEasingFunction_09() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.167F, 0.167F), New Vector2(0.12F, 0.12F))
            End Function
            ' TStart
            Private Function CubicBezierEasingFunction_10() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.301F, 0), New Vector2(0.833F, 1))
            End Function
            Private Function CubicBezierEasingFunction_11() As CubicBezierEasingFunction
                Return __InlineAssignHelper(_cubicBezierEasingFunction_11, _c.CreateCubicBezierEasingFunction(New Vector2(0.301F, 0), New Vector2(0.667F, 1)))
            End Function
            Private Function CubicBezierEasingFunction_12() As CubicBezierEasingFunction
                Return __InlineAssignHelper(_cubicBezierEasingFunction_12, _c.CreateCubicBezierEasingFunction(New Vector2(0.167F, 0.167F), New Vector2(0.06F, 1)))
            End Function
            ' Layer (Shape): T1b-B
            '   Path 1
            '     Path 1.PathGeometry
            '       TrimEnd
            Private Function CubicBezierEasingFunction_13() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.167F, 0.167F), New Vector2(0.21F, 1))
            End Function
            ' Radius
            Private Function CubicBezierEasingFunction_14() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.333F, 0), New Vector2(0.667F, 1))
            End Function
            Private Function CubicBezierEasingFunction_15() As CubicBezierEasingFunction
                Return __InlineAssignHelper(_cubicBezierEasingFunction_15, _c.CreateCubicBezierEasingFunction(New Vector2(0.18F, 0), New Vector2(0.348F, 1)))
            End Function
            Private Function CubicBezierEasingFunction_16() As CubicBezierEasingFunction
                Return __InlineAssignHelper(_cubicBezierEasingFunction_16, _c.CreateCubicBezierEasingFunction(New Vector2(0.693F, 0), New Vector2(0.27F, 1)))
            End Function
            ' Transforms: O-B
            '   Ellipse Path 1
            '     Ellipse Path 1.EllipseGeometry
            '       TrimStart
            Private Function CubicBezierEasingFunction_17() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.167F, 1), New Vector2(0.432F, 1))
            End Function
            ' Transforms: T1a-Y
            '   Path 1
            '     Path 1.PathGeometry
            '       TrimEnd
            Private Function CubicBezierEasingFunction_18() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.167F, 0.167F), New Vector2(0.673F, 1))
            End Function
            ' Transforms: N
            '   Position
            Private Function CubicBezierEasingFunction_19() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.167F, 0.167F), New Vector2(0.26F, 1))
            End Function
            ' Transforms: N
            '   Position
            Private Function CubicBezierEasingFunction_20() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.74F, 0), New Vector2(0.833F, 0.833F))
            End Function
            ' TStart
            Private Function CubicBezierEasingFunction_21() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.167F, 0.167F), New Vector2(0.703F, 0.857F))
            End Function
            ' TStart
            Private Function CubicBezierEasingFunction_22() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.333F, 0.202F), New Vector2(0.938F, 1))
            End Function
            Private Function CubicBezierEasingFunction_23() As CubicBezierEasingFunction
                Return __InlineAssignHelper(_cubicBezierEasingFunction_23, _c.CreateCubicBezierEasingFunction(New Vector2(0.167F, 0.167F), New Vector2(0.337F, 1)))
            End Function
            ' TStart
            Private Function CubicBezierEasingFunction_24() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.167F, 0.167F), New Vector2(0.703F, 0.821F))
            End Function
            ' TStart
            Private Function CubicBezierEasingFunction_25() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.037F, 0.168F), New Vector2(0.263F, 1))
            End Function
            ' Transforms: Dot1
            '   Position
            Private Function CubicBezierEasingFunction_26() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.823F, 0), New Vector2(0.833F, 0.833F))
            End Function
            Private Function CubicBezierEasingFunction_27() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.167F, 0.198F), New Vector2(0.638F, 1))
            End Function
            Private Function CubicBezierEasingFunction_28() As CubicBezierEasingFunction
                Return _c.CreateCubicBezierEasingFunction(New Vector2(0.523F, 0), New Vector2(0.795F, 1))
            End Function
            ' Transforms: O-Y
            '   Ellipse Path 1
            ' Ellipse Path 1.EllipseGeometry
            Private Function Ellipse_1p5_0() As CompositionEllipseGeometry
                Dim result = _c.CreateEllipseGeometry()
                result.Center = New Vector2(0.8F, -0.5F)
                result.Radius = New Vector2(1.5F, 1.5F)
                result.StartAnimation("Radius", Vector2Animation_07())
                Dim controller = result.TryGetAnimationController("Radius")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Transforms: O-B
            '   Ellipse Path 1
            ' Ellipse Path 1.EllipseGeometry
            Private Function Ellipse_1p5_1() As CompositionEllipseGeometry
                Dim result = _c.CreateEllipseGeometry()
                result.Center = New Vector2(0.8F, -0.5F)
                result.Radius = New Vector2(1.5F, 1.5F)
                result.StartAnimation("Radius", _vector2Animation_07)
                Dim controller = result.TryGetAnimationController("Radius")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TrimStart", ScalarAnimation_0_to_0p399())
                controller = result.TryGetAnimationController("TrimStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TrimEnd", ScalarAnimation_1_to_0p88())
                controller = result.TryGetAnimationController("TrimEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Transforms: Dot-Y
            '   Ellipse Path 1
            ' Ellipse Path 1.EllipseGeometry
            Private Function Ellipse_4p6() As CompositionEllipseGeometry
                Dim result = _c.CreateEllipseGeometry()
                result.Center = New Vector2(0.8F, -0.5F)
                result.Radius = New Vector2(4.6F, 4.6F)
                Return result
            End Function
            ' Ellipse Path 1.EllipseGeometry
            Private Function Ellipse_4p7() As CompositionEllipseGeometry
                Dim result = __InlineAssignHelper(_ellipse_4p7, _c.CreateEllipseGeometry())
                result.Center = New Vector2(0.8F, -0.5F)
                result.Radius = New Vector2(4.7F, 4.7F)
                Return result
            End Function
            Private Function Geometry_00() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(-13.664F, -0.145F))
                    builder.AddLine(New Vector2(75.663F, 0.29F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_01() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(0.859F, -21.143F))
                    builder.AddLine(New Vector2(-4.359F, 70.392F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_02() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(-26.67F, -0.283F))
                    builder.AddLine(New Vector2(99.171F, 0.066F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_03() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(-13.664F, -0.145F))
                    builder.AddLine(New Vector2(62.163F, 0.29F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_04() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(-30.72F, 63.761F))
                    builder.AddCubicBezier(New Vector2(-30.689F, 63.167F), New Vector2(-30.789F, 50.847F), New Vector2(-30.741F, 45.192F))
                    builder.AddCubicBezier(New Vector2(-30.665F, 36.214F), New Vector2(-37.343F, 27.074F), New Vector2(-37.397F, 27.014F))
                    builder.AddCubicBezier(New Vector2(-38.558F, 25.714F), New Vector2(-39.752F, 24.147F), New Vector2(-40.698F, 22.661F))
                    builder.AddCubicBezier(New Vector2(-46.637F, 13.334F), New Vector2(-47.84F, 0.933F), New Vector2(-37.873F, -7.117F))
                    builder.AddCubicBezier(New Vector2(-13.196F, -27.046F), New Vector2(8.96F, 11.559F), New Vector2(49.506F, 11.559F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_05() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(246.65F, 213.814F))
                    builder.AddLine(New Vector2(340.956F, 213.628F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_06() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(1.681F, -29.992F))
                    builder.AddLine(New Vector2(-1.681F, 29.992F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_07() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(1.768F, -25.966F))
                    builder.AddLine(New Vector2(-1.768F, 25.966F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_08() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(-8.837F, -58.229F))
                    builder.AddCubicBezier(New Vector2(-8.837F, -58.229F), New Vector2(-10.163F, 29.495F), New Vector2(-35.834F, 33.662F))
                    builder.AddCubicBezier(New Vector2(-44.058F, 34.997F), New Vector2(-50.232F, 30.05F), New Vector2(-51.688F, 23.148F))
                    builder.AddCubicBezier(New Vector2(-53.144F, 16.245F), New Vector2(-49.655F, 9.156F), New Vector2(-41.174F, 7.293F))
                    builder.AddCubicBezier(New Vector2(-17.357F, 2.06F), New Vector2(4.235F, 57.188F), New Vector2(51.797F, 44.178F))
                    builder.AddCubicBezier(New Vector2(51.957F, 44.134F), New Vector2(52.687F, 43.874F), New Vector2(53.188F, 43.741F))
                    builder.AddCubicBezier(New Vector2(53.689F, 43.608F), New Vector2(68.971F, 41.357F), New Vector2(140.394F, 43.672F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_09() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(-67.125F, -112))
                    builder.AddCubicBezier(New Vector2(-67.125F, -112), New Vector2(-73.558F, -100.719F), New Vector2(-75.458F, -89.951F))
                    builder.AddCubicBezier(New Vector2(-78.625F, -72), New Vector2(-79.375F, -58.25F), New Vector2(-80.375F, -39.25F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_10() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(-67.25F, -105.5F))
                    builder.AddCubicBezier(New Vector2(-67.25F, -105.5F), New Vector2(-70.433F, -94.969F), New Vector2(-72.333F, -84.201F))
                    builder.AddCubicBezier(New Vector2(-75.5F, -66.25F), New Vector2(-75.5F, -56.75F), New Vector2(-76.5F, -37.75F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_11() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(34.5F, -13.05F))
                    builder.AddCubicBezier(New Vector2(7.5F, -14.5F), New Vector2(-4, -37), New Vector2(-35.046F, -35.579F))
                    builder.AddCubicBezier(New Vector2(-61.472F, -34.369F), New Vector2(-62.25F, -5.75F), New Vector2(-62.25F, -5.75F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_12() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(-3, 35.95F))
                    builder.AddCubicBezier(New Vector2(-3, 35.95F), New Vector2(-1.5F, 7.5F), New Vector2(-1.352F, -6.756F))
                    builder.AddCubicBezier(New Vector2(-9.903F, -15.019F), New Vector2(-21.57F, -20.579F), New Vector2(-32.046F, -20.579F))
                    builder.AddCubicBezier(New Vector2(-53.5F, -20.579F), New Vector2(-42.25F, 4.25F), New Vector2(-42.25F, 4.25F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_13() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(16.231F, 39.073F))
                    builder.AddLine(New Vector2(-32.769F, 57.365F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_14() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(7.45F, 21.95F))
                    builder.AddLine(New Vector2(-32.75F, 55.75F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_15() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(-94.5F, 37.073F))
                    builder.AddLine(New Vector2(-48.769F, 55.365F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_16() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(-87.5F, 20.95F))
                    builder.AddLine(New Vector2(-48.75F, 54.75F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_17() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(166.731F, -7.927F))
                    builder.AddLine(New Vector2(136.731F, 7.115F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_18() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(156.45F, -23.05F))
                    builder.AddLine(New Vector2(132, 2.75F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_19() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(169.5F, 18.073F))
                    builder.AddLine(New Vector2(137.481F, 11.365F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_20() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(119.5F, -45.05F))
                    builder.AddLine(New Vector2(82.75F, -44.75F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_21() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(119.25F, -20.05F))
                    builder.AddLine(New Vector2(63.5F, -20.5F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_22() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(128, 3.65F))
                    builder.AddLine(New Vector2(78.25F, 3.5F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_23() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(149.624F, 8.244F))
                    builder.AddLine(New Vector2(136.648F, 10.156F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_24() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(144.429F, -5.397F))
                    builder.AddLine(New Vector2(132.275F, 4.731F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_25() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(145.677F, 22.22F))
                    builder.AddLine(New Vector2(134.922F, 14.749F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_26() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(147.699F, 13.025F))
                    builder.AddLine(New Vector2(133.195F, 13.21F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_27() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(142.183F, -5.112F))
                    builder.AddLine(New Vector2(130.029F, 5.016F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function Geometry_28() As CanvasGeometry
                Dim result As CanvasGeometry
                Using builder As New CanvasPathBuilder(Nothing)
                    builder.BeginFigure(New Vector2(142.038F, 29.278F))
                    builder.AddLine(New Vector2(131.282F, 21.807F))
                    builder.EndFigure(CanvasFigureLoop.Open)
                    result = CanvasGeometry.CreatePath(builder)
                End Using
                Return result
            End Function
            Private Function HoldThenStepEasingFunction() As StepEasingFunction
                Dim result = __InlineAssignHelper(_holdThenStepEasingFunction, _c.CreateStepEasingFunction())
                result.IsFinalStepSingleFrame = True
                Return result
            End Function
            Private Function LinearEasingFunction() As LinearEasingFunction
                Return __InlineAssignHelper(_linearEasingFunction, _c.CreateLinearEasingFunction())
            End Function
            ' Transforms: E3-Y
            '   Path 1
            ' Path 1.PathGeometry
            Private Function PathGeometry_00() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(CompositionPath_00())
                result.TrimEnd = 0
                result.StartAnimation("TrimEnd", ScalarAnimation_0_to_0p316_0())
                Dim controller = result.TryGetAnimationController("TrimEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Transforms: E3-Y
            '   Path 1
            ' Path 1.PathGeometry
            Private Function PathGeometry_01() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(_compositionPath_00)
                result.TrimEnd = 0
                result.StartAnimation("TrimEnd", ScalarAnimation_0_to_0p316_1())
                Dim controller = result.TryGetAnimationController("TrimEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Transforms: I-Y
            '   Path 1
            ' Path 1.PathGeometry
            Private Function PathGeometry_02() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(CompositionPath_01())
                result.TrimEnd = 0
                result.StartAnimation("TrimEnd", ScalarAnimation_0_to_0p457_0())
                Dim controller = result.TryGetAnimationController("TrimEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Transforms: I-Y
            '   Path 1
            ' Path 1.PathGeometry
            Private Function PathGeometry_03() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(_compositionPath_01)
                result.TrimEnd = 0
                result.StartAnimation("TrimEnd", ScalarAnimation_0_to_0p457_1())
                Dim controller = result.TryGetAnimationController("TrimEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Transforms: E2-Y
            '   Path 1
            ' Path 1.PathGeometry
            Private Function PathGeometry_04() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(CompositionPath_02())
                result.TrimEnd = 0
                result.StartAnimation("TrimEnd", ScalarAnimation_0_to_0p43_0())
                Dim controller = result.TryGetAnimationController("TrimEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Transforms: E2-Y
            '   Path 1
            ' Path 1.PathGeometry
            Private Function PathGeometry_05() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(_compositionPath_02)
                result.TrimEnd = 0
                result.StartAnimation("TrimEnd", ScalarAnimation_0_to_0p43_1())
                Dim controller = result.TryGetAnimationController("TrimEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Transforms: E1-Y
            '   Path 1
            ' Path 1.PathGeometry
            Private Function PathGeometry_06() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(CompositionPath_03())
                result.TrimEnd = 0
                result.StartAnimation("TrimEnd", ScalarAnimation_0_to_0p375_0())
                Dim controller = result.TryGetAnimationController("TrimEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Transforms: E1-Y
            '   Path 1
            ' Path 1.PathGeometry
            Private Function PathGeometry_07() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(_compositionPath_03)
                result.TrimEnd = 0
                result.StartAnimation("TrimEnd", ScalarAnimation_0_to_0p375_1())
                Dim controller = result.TryGetAnimationController("TrimEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_08() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(CompositionPath_04())
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0)
                propertySet.InsertScalar("TEnd", 0)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0_to_0p249())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_0_to_1_0())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T2b-Y
            '   Path 1
            ' Path 1.PathGeometry
            Private Function PathGeometry_09() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(CompositionPath_05())
                result.TrimEnd = 0.411F
                result.TrimStart = 0.29F
                result.StartAnimation("TrimStart", ScalarAnimation_0p29_to_0_0())
                Dim controller = result.TryGetAnimationController("TrimStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TrimEnd", ScalarAnimation_0p411_to_0p665_0())
                controller = result.TryGetAnimationController("TrimEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T2a-Y
            '   Path 1
            ' Path 1.PathGeometry
            Private Function PathGeometry_10() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(CompositionPath_06())
                result.TrimEnd = 0.5F
                result.TrimStart = 0.5F
                result.StartAnimation("TrimStart", ScalarAnimation_0p5_to_0_0())
                Dim controller = result.TryGetAnimationController("TrimStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TrimEnd", ScalarAnimation_0p5_to_1_0())
                controller = result.TryGetAnimationController("TrimEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T2b-B
            '   Path 1
            ' Path 1.PathGeometry
            Private Function PathGeometry_11() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(_compositionPath_05)
                result.TrimEnd = 0.411F
                result.TrimStart = 0.29F
                result.StartAnimation("TrimStart", ScalarAnimation_0p29_to_0_1())
                Dim controller = result.TryGetAnimationController("TrimStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TrimEnd", ScalarAnimation_0p411_to_0p665_1())
                controller = result.TryGetAnimationController("TrimEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T1b-Y
            '   Path 1
            ' Path 1.PathGeometry
            Private Function PathGeometry_12() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(CompositionPath_07())
                result.TrimEnd = 0.117F
                result.StartAnimation("TrimEnd", ScalarAnimation_0p117_to_1_0())
                Dim controller = result.TryGetAnimationController("TrimEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T1b-B
            '   Path 1
            ' Path 1.PathGeometry
            Private Function PathGeometry_13() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(_compositionPath_07)
                result.TrimEnd = 0.117F
                result.StartAnimation("TrimEnd", ScalarAnimation_0p117_to_1_1())
                Dim controller = result.TryGetAnimationController("TrimEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_14() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(_compositionPath_04)
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0)
                propertySet.InsertScalar("TEnd", 0)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", _scalarAnimation_0_to_0p249)
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_0_to_1_1())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Layer (Shape): T2a-B
            '   Path 1
            ' Path 1.PathGeometry
            Private Function PathGeometry_15() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(_compositionPath_06)
                result.TrimEnd = 0.5F
                result.TrimStart = 0.5F
                result.StartAnimation("TrimStart", ScalarAnimation_0p5_to_0_1())
                Dim controller = result.TryGetAnimationController("TrimStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TrimEnd", ScalarAnimation_0p5_to_1_1())
                controller = result.TryGetAnimationController("TrimEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Transforms: T1a-Y
            '   Path 1
            ' Path 1.PathGeometry
            Private Function PathGeometry_16() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(_compositionPath_04)
                result.TrimEnd = 0.249F
                result.TrimStart = 0.249F
                result.StartAnimation("TrimEnd", ScalarAnimation_0p249_to_0p891())
                Dim controller = result.TryGetAnimationController("TrimEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_17() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(CompositionPath_08())
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.8F)
                propertySet.InsertScalar("TEnd", 0.81F)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p8_to_0())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_0p81_to_0p734_0())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_18() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(_compositionPath_08)
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.8F)
                propertySet.InsertScalar("TEnd", 0.81F)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p8_to_0p3())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_0p81_to_0p734_1())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_19() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_09()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_00())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_1_to_0_09())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_20() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_10()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_01())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_1_to_0_11())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_21() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_11()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_02())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_1_to_0_12())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_22() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_12()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", _scalarAnimation_0p87_to_0_02)
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", _scalarAnimation_1_to_0_12)
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_23() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_13()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_03())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_1_to_0_14())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_24() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_14()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_04())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", _scalarAnimation_1_to_0_14)
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_25() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_15()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_05())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_1_to_0_16())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_26() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_16()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_06())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_1_to_0_17())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_27() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_17()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_07())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_1_to_0_18())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_28() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_18()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_08())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", _scalarAnimation_1_to_0_18)
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_29() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_19()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_09())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_1_to_0_20())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_30() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_20()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_10())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_1_to_0_21())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_31() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_21()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_11())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_1_to_0_23())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_32() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_22()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_12())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_1_to_0_25())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_33() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_23()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_13())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_1_to_0_27())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_34() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_24()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_14())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", _scalarAnimation_1_to_0_27)
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_35() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_25()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_15())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_1_to_0_29())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_36() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_26()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_16())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_1_to_0_30())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_37() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_27()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_17())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", _scalarAnimation_1_to_0_30)
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Path 1.PathGeometry
            Private Function PathGeometry_38() As CompositionPathGeometry
                Dim result = _c.CreatePathGeometry(New CompositionPath(Geometry_28()))
                Dim propertySet = result.Properties
                propertySet.InsertScalar("TStart", 0.87F)
                propertySet.InsertScalar("TEnd", 1)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Min(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimStart", _reusableExpressionAnimation)
                _reusableExpressionAnimation.ClearAllParameters()
                _reusableExpressionAnimation.Expression = "Max(my.TStart, my.TEnd)"
                _reusableExpressionAnimation.SetReferenceParameter("my", result)
                result.StartAnimation("TrimEnd", _reusableExpressionAnimation)
                result.StartAnimation("TStart", ScalarAnimation_0p87_to_0_18())
                Dim controller = result.TryGetAnimationController("TStart")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("TEnd", ScalarAnimation_1_to_0_32())
                controller = result.TryGetAnimationController("TEnd")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' The root of the composition.
            Private Function Root() As ContainerVisual
                Dim result = __InlineAssignHelper(_root, _c.CreateContainerVisual())
                Dim propertySet = result.Properties
                propertySet.InsertScalar("Progress", 0)
                propertySet.InsertScalar("t0", 0)
                propertySet.InsertScalar("t1", 0)
                Dim children1 = result.Children
                children1.InsertAtTop(ShapeVisual())
                result.StartAnimation("t0", ScalarAnimation_0_to_1_2())
                Dim controller = result.TryGetAnimationController("t0")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                result.StartAnimation("t1", _scalarAnimation_0_to_1_2)
                controller = result.TryGetAnimationController("t1")
                controller.Pause()
                controller.StartAnimation("Progress", _scalarExpressionAnimation)
                Return result
            End Function
            ' Rectangle Path 1
            ' Rectangle Path 1.RectangleGeometry
            Private Function RoundedRectangle_375x667() As CompositionRoundedRectangleGeometry
                Dim result = _c.CreateRoundedRectangleGeometry()
                result.CornerRadius = New Vector2(0.000001F, 0.000001F)
                result.Offset = New Vector2(-187.5F, -333.5F)
                result.Size = New Vector2(375, 667)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0_to_0p249() As ScalarKeyFrameAnimation
                Dim result = __InlineAssignHelper(_scalarAnimation_0_to_0p249, _c.CreateScalarKeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.301675975F, 0, _linearEasingFunction)
                result.InsertKeyFrame(0.391061455F, 0.249F, CubicBezierEasingFunction_10())
                Return result
            End Function
            ' Transforms: E3-Y
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimEnd
            Private Function ScalarAnimation_0_to_0p316_0() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.469273746F, 0, LinearEasingFunction())
                result.InsertKeyFrame(0.5139665F, 0.316F, CubicBezierEasingFunction_04())
                Return result
            End Function
            ' Transforms: E3-Y
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimEnd
            Private Function ScalarAnimation_0_to_0p316_1() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.5139665F, 0, _linearEasingFunction)
                result.InsertKeyFrame(0.541899443F, 0.316F, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' Transforms: E1-Y
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimEnd
            Private Function ScalarAnimation_0_to_0p375_0() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.441340774F, 0, _linearEasingFunction)
                result.InsertKeyFrame(0.491620123F, 0.375F, _cubicBezierEasingFunction_07)
                Return result
            End Function
            ' Transforms: E1-Y
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimEnd
            Private Function ScalarAnimation_0_to_0p375_1() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.469273746F, 0, _linearEasingFunction)
                result.InsertKeyFrame(0.519553065F, 0.375F, _cubicBezierEasingFunction_07)
                Return result
            End Function
            ' Transforms: O-B
            '   Ellipse Path 1
            '     Ellipse Path 1.EllipseGeometry
            ' TrimStart
            Private Function ScalarAnimation_0_to_0p399() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.301675975F, 0, _linearEasingFunction)
                result.InsertKeyFrame(0.3519553F, 0.3F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.5083799F, 0.399F, CubicBezierEasingFunction_17())
                Return result
            End Function
            ' Transforms: E2-Y
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimEnd
            Private Function ScalarAnimation_0_to_0p43_0() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.463687152F, 0, _linearEasingFunction)
                result.InsertKeyFrame(0.5139665F, 0.43F, _cubicBezierEasingFunction_07)
                Return result
            End Function
            ' Transforms: E2-Y
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimEnd
            Private Function ScalarAnimation_0_to_0p43_1() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.480446935F, 0, _linearEasingFunction)
                result.InsertKeyFrame(0.530726254F, 0.43F, _cubicBezierEasingFunction_07)
                Return result
            End Function
            ' Transforms: I-Y
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimEnd
            Private Function ScalarAnimation_0_to_0p457_0() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.43575418F, 0, _linearEasingFunction)
                result.InsertKeyFrame(0.491620123F, 0.457F, CubicBezierEasingFunction_07())
                Return result
            End Function
            ' Transforms: I-Y
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimEnd
            Private Function ScalarAnimation_0_to_0p457_1() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.452513963F, 0, _linearEasingFunction)
                result.InsertKeyFrame(0.5083799F, 0.457F, _cubicBezierEasingFunction_07)
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_0_to_1_0() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.301675975F, 0, _linearEasingFunction)
                result.InsertKeyFrame(0.413407832F, 1, CubicBezierEasingFunction_11())
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_0_to_1_1() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.301675975F, 0, _linearEasingFunction)
                result.InsertKeyFrame(0.43575418F, 1, _cubicBezierEasingFunction_11)
                Return result
            End Function
            Private Function ScalarAnimation_0_to_1_2() As ScalarKeyFrameAnimation
                Dim result = __InlineAssignHelper(_scalarAnimation_0_to_1_2, _c.CreateScalarKeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.196966588F, 0, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.245809957F, 1, CubicBezierEasingFunction_27())
                result.InsertKeyFrame(0.245810062F, 0, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.3016759F, 1, CubicBezierEasingFunction_28())
                Return result
            End Function
            ' Layer (Shape): T1b-Y
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimEnd
            Private Function ScalarAnimation_0p117_to_1_0() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.117F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.391061455F, 0.117F, _linearEasingFunction)
                result.InsertKeyFrame(0.418994427F, 1, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' Layer (Shape): T1b-B
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimEnd
            Private Function ScalarAnimation_0p117_to_1_1() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.117F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.452513963F, 0.117F, _linearEasingFunction)
                result.InsertKeyFrame(0.491620123F, 1, CubicBezierEasingFunction_13())
                Return result
            End Function
            ' Transforms: T1a-Y
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimEnd
            Private Function ScalarAnimation_0p249_to_0p891() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.249F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.391061455F, 0.249F, _linearEasingFunction)
                result.InsertKeyFrame(0.469273746F, 0.891F, CubicBezierEasingFunction_18())
                Return result
            End Function
            ' Layer (Shape): T2b-Y
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimStart
            Private Function ScalarAnimation_0p29_to_0_0() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.29F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.424581F, 0.29F, _linearEasingFunction)
                result.InsertKeyFrame(0.47486034F, 0, _cubicBezierEasingFunction_07)
                Return result
            End Function
            ' Layer (Shape): T2b-B
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimStart
            Private Function ScalarAnimation_0p29_to_0_1() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.29F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.458100557F, 0.29F, _linearEasingFunction)
                result.InsertKeyFrame(0.5083799F, 0, _cubicBezierEasingFunction_07)
                Return result
            End Function
            ' Layer (Shape): T2b-Y
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimEnd
            Private Function ScalarAnimation_0p411_to_0p665_0() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.411F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.424581F, 0.411F, _linearEasingFunction)
                result.InsertKeyFrame(0.47486034F, 0.665F, _cubicBezierEasingFunction_07)
                Return result
            End Function
            ' Layer (Shape): T2b-B
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimEnd
            Private Function ScalarAnimation_0p411_to_0p665_1() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.411F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.458100557F, 0.411F, _linearEasingFunction)
                result.InsertKeyFrame(0.5083799F, 0.665F, _cubicBezierEasingFunction_07)
                Return result
            End Function
            ' Layer (Shape): T2a-Y
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimStart
            Private Function ScalarAnimation_0p5_to_0_0() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.5F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.402234644F, 0.5F, _linearEasingFunction)
                result.InsertKeyFrame(0.458100557F, 0, CubicBezierEasingFunction_12())
                Return result
            End Function
            ' Layer (Shape): T2a-B
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimStart
            Private Function ScalarAnimation_0p5_to_0_1() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.5F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.418994427F, 0.5F, _linearEasingFunction)
                result.InsertKeyFrame(0.47486034F, 0, _cubicBezierEasingFunction_12)
                Return result
            End Function
            ' Layer (Shape): T2a-Y
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimEnd
            Private Function ScalarAnimation_0p5_to_1_0() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.5F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.402234644F, 0.5F, _linearEasingFunction)
                result.InsertKeyFrame(0.458100557F, 1, _cubicBezierEasingFunction_12)
                Return result
            End Function
            ' Layer (Shape): T2a-B
            '   Path 1
            '     Path 1.PathGeometry
            ' TrimEnd
            Private Function ScalarAnimation_0p5_to_1_1() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.5F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.418994427F, 0.5F, _linearEasingFunction)
                result.InsertKeyFrame(0.47486034F, 1, _cubicBezierEasingFunction_12)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p8_to_0() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.8F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.08938547F, 0.8F, _linearEasingFunction)
                result.InsertKeyFrame(0.111731842F, 0.5F, CubicBezierEasingFunction_21())
                result.InsertKeyFrame(0.156424582F, 0, CubicBezierEasingFunction_22())
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p8_to_0p3() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.8F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.100558661F, 0.8F, _linearEasingFunction)
                result.InsertKeyFrame(0.128491625F, 0.5F, CubicBezierEasingFunction_24())
                result.InsertKeyFrame(0.30726257F, 0.3F, CubicBezierEasingFunction_25())
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_0p81_to_0p734_0() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.81F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.08938547F, 0.81F, _linearEasingFunction)
                result.InsertKeyFrame(0.150837988F, 0.734F, CubicBezierEasingFunction_23())
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_0p81_to_0p734_1() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.81F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.100558661F, 0.81F, _linearEasingFunction)
                result.InsertKeyFrame(0.162011176F, 0.734F, _cubicBezierEasingFunction_23)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_00() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.162011176F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.184357539F, 0.37533F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.201117322F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_01() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.162011176F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.184357539F, 0.25333F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.201117322F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_02() As ScalarKeyFrameAnimation
                Dim result = __InlineAssignHelper(_scalarAnimation_0p87_to_0_02, _c.CreateScalarKeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.363128483F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.391061455F, 0.21233F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.418994427F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_03() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.301675975F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.318435758F, 0.42133F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.3575419F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_04() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.301675975F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.318435758F, 0.43833F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.3575419F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_05() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.301675975F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.318435758F, 0.50633F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.3575419F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_06() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.301675975F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.318435758F, 0.43933F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.3575419F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_07() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.541899443F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.5586592F, 0.42133F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.5977654F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_08() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.541899443F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.5586592F, 0.43833F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.5977654F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_09() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.541899443F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.5586592F, 0.50633F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.5977654F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_10() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.446927369F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.463687152F, 0.21233F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.486033529F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_11() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.469273746F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.486033529F, 0.21233F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.5083799F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_12() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.47486034F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.5027933F, 0.21233F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.5251397F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_13() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.418994427F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.43575418F, 0.42133F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.458100557F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_14() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.418994427F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.43575418F, 0.43833F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.458100557F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_15() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.418994427F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.43575418F, 0.50633F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.458100557F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_16() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.424581F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.441340774F, 0.42133F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.463687152F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_17() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.424581F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.441340774F, 0.43833F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.463687152F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TStart
            Private Function ScalarAnimation_0p87_to_0_18() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 0.87F, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.424581F, 0.87F, _linearEasingFunction)
                result.InsertKeyFrame(0.441340774F, 0.50633F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.463687152F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' Layer (Shape): E3-Y
            Private Function ScalarAnimation_1_to_0_00() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.469273746F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.5698324F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): I-Y
            Private Function ScalarAnimation_1_to_0_01() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.43575418F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.519553065F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): E2-Y
            Private Function ScalarAnimation_1_to_0_02() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.463687152F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.5363129F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): E1-Y
            Private Function ScalarAnimation_1_to_0_03() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.441340774F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.5251397F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): T1a-Y
            Private Function ScalarAnimation_1_to_0_04() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.329608947F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.87150836F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): T2b-Y
            Private Function ScalarAnimation_1_to_0_05() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.424581F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.5139665F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): T2a-Y
            Private Function ScalarAnimation_1_to_0_06() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.402234644F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.497206718F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): T1b-Y
            Private Function ScalarAnimation_1_to_0_07() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.391061455F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.899441361F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): Dot-Y
            Private Function ScalarAnimation_1_to_0_08() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.156424582F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.301675975F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_1_to_0_09() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 1, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.162011176F, 1, _linearEasingFunction)
                result.InsertKeyFrame(0.184357539F, 0.66356F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.201117322F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            Private Function ScalarAnimation_1_to_0_10() As ScalarKeyFrameAnimation
                Dim result = __InlineAssignHelper(_scalarAnimation_1_to_0_10, _c.CreateScalarKeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.167597771F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.206703916F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_1_to_0_11() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 1, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.162011176F, 1, _linearEasingFunction)
                result.InsertKeyFrame(0.184357539F, 0.69056F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.201117322F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_1_to_0_12() As ScalarKeyFrameAnimation
                Dim result = __InlineAssignHelper(_scalarAnimation_1_to_0_12, _c.CreateScalarKeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 1, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.363128483F, 1, _linearEasingFunction)
                result.InsertKeyFrame(0.391061455F, 0.66356F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.418994427F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            Private Function ScalarAnimation_1_to_0_13() As ScalarKeyFrameAnimation
                Dim result = __InlineAssignHelper(_scalarAnimation_1_to_0_13, _c.CreateScalarKeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.363128483F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.418994427F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_1_to_0_14() As ScalarKeyFrameAnimation
                Dim result = __InlineAssignHelper(_scalarAnimation_1_to_0_14, _c.CreateScalarKeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 1, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.301675975F, 1, _linearEasingFunction)
                result.InsertKeyFrame(0.318435758F, 0.66356F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.3575419F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            Private Function ScalarAnimation_1_to_0_15() As ScalarKeyFrameAnimation
                Dim result = __InlineAssignHelper(_scalarAnimation_1_to_0_15, _c.CreateScalarKeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.301675975F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.3575419F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_1_to_0_16() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 1, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.301675975F, 1, _linearEasingFunction)
                result.InsertKeyFrame(0.318435758F, 0.75856F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.3575419F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_1_to_0_17() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 1, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.301675975F, 1, _linearEasingFunction)
                result.InsertKeyFrame(0.318435758F, 0.70456F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.3575419F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_1_to_0_18() As ScalarKeyFrameAnimation
                Dim result = __InlineAssignHelper(_scalarAnimation_1_to_0_18, _c.CreateScalarKeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 1, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.541899443F, 1, _linearEasingFunction)
                result.InsertKeyFrame(0.5586592F, 0.66356F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.5977654F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            Private Function ScalarAnimation_1_to_0_19() As ScalarKeyFrameAnimation
                Dim result = __InlineAssignHelper(_scalarAnimation_1_to_0_19, _c.CreateScalarKeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.541899443F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.5977654F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_1_to_0_20() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 1, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.541899443F, 1, _linearEasingFunction)
                result.InsertKeyFrame(0.5586592F, 0.75856F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.5977654F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_1_to_0_21() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 1, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.446927369F, 1, _linearEasingFunction)
                result.InsertKeyFrame(0.463687152F, 0.66356F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.486033529F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' Layer (Shape): S11
            Private Function ScalarAnimation_1_to_0_22() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.446927369F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.5027933F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_1_to_0_23() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 1, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.469273746F, 1, _linearEasingFunction)
                result.InsertKeyFrame(0.486033529F, 0.66356F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.5083799F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' Layer (Shape): S12
            Private Function ScalarAnimation_1_to_0_24() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.469273746F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.5251397F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_1_to_0_25() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 1, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.47486034F, 1, _linearEasingFunction)
                result.InsertKeyFrame(0.5027933F, 0.66356F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.5251397F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' Layer (Shape): S13
            Private Function ScalarAnimation_1_to_0_26() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.47486034F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.530726254F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_1_to_0_27() As ScalarKeyFrameAnimation
                Dim result = __InlineAssignHelper(_scalarAnimation_1_to_0_27, _c.CreateScalarKeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 1, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.418994427F, 1, _linearEasingFunction)
                result.InsertKeyFrame(0.43575418F, 0.66356F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.458100557F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            Private Function ScalarAnimation_1_to_0_28() As ScalarKeyFrameAnimation
                Dim result = __InlineAssignHelper(_scalarAnimation_1_to_0_28, _c.CreateScalarKeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.418994427F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.463687152F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_1_to_0_29() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 1, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.418994427F, 1, _linearEasingFunction)
                result.InsertKeyFrame(0.43575418F, 0.75856F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.458100557F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_1_to_0_30() As ScalarKeyFrameAnimation
                Dim result = __InlineAssignHelper(_scalarAnimation_1_to_0_30, _c.CreateScalarKeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 1, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.424581F, 1, _linearEasingFunction)
                result.InsertKeyFrame(0.441340774F, 0.66356F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.463687152F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            Private Function ScalarAnimation_1_to_0_31() As ScalarKeyFrameAnimation
                Dim result = __InlineAssignHelper(_scalarAnimation_1_to_0_31, _c.CreateScalarKeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.424581F, 1, _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.469273746F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' TEnd
            Private Function ScalarAnimation_1_to_0_32() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 1, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.424581F, 1, _linearEasingFunction)
                result.InsertKeyFrame(0.441340774F, 0.75856F, _cubicBezierEasingFunction_04)
                result.InsertKeyFrame(0.463687152F, 0, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' Transforms: O-B
            '   Ellipse Path 1
            '     Ellipse Path 1.EllipseGeometry
            ' TrimEnd
            Private Function ScalarAnimation_1_to_0p88() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, 1, _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.301675975F, 1, _linearEasingFunction)
                result.InsertKeyFrame(0.3519553F, 0.88F, _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' Layer (Shape): Dot1
            Private Function ScalarAnimation_to_0() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.09497207F, 0, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): Dot-Y
            Private Function ScalarAnimation_to_1_00() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.5363129F, 1, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): E3-B
            Private Function ScalarAnimation_to_1_01() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.5139665F, 1, _holdThenStepEasingFunction)
                Return result
            End Function
            Private Function ScalarAnimation_to_1_02() As ScalarKeyFrameAnimation
                Dim result = __InlineAssignHelper(_scalarAnimation_to_1_02, _c.CreateScalarKeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.452513963F, 1, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): E2-B
            Private Function ScalarAnimation_to_1_03() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.480446935F, 1, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): E1-B
            Private Function ScalarAnimation_to_1_04() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.469273746F, 1, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): T2b-B
            Private Function ScalarAnimation_to_1_05() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.458100557F, 1, _holdThenStepEasingFunction)
                Return result
            End Function
            Private Function ScalarAnimation_to_1_06() As ScalarKeyFrameAnimation
                Dim result = __InlineAssignHelper(_scalarAnimation_to_1_06, _c.CreateScalarKeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.301675975F, 1, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): T1a-Y 2
            Private Function ScalarAnimation_to_1_07() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.329608947F, 1, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): T2a-B
            Private Function ScalarAnimation_to_1_08() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.418994427F, 1, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): T1a-B
            Private Function ScalarAnimation_to_1_09() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.391061455F, 1, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): L-Y
            Private Function ScalarAnimation_to_1_10() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.08938547F, 1, _holdThenStepEasingFunction)
                Return result
            End Function
            ' Layer (Shape): L-B
            Private Function ScalarAnimation_to_1_11() As ScalarKeyFrameAnimation
                Dim result = _c.CreateScalarKeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0.100558661F, 1, _holdThenStepEasingFunction)
                Return result
            End Function
            Private Function ScalarExpressionAnimation() As ExpressionAnimation
                Dim result = __InlineAssignHelper(_scalarExpressionAnimation, _c.CreateExpressionAnimation())
                result.SetReferenceParameter("_", _root)
                result.Expression = "_.Progress"
                Return result
            End Function
            Private Function ShapeVisual() As ShapeVisual
                Dim result = _c.CreateShapeVisual()
                result.Size = New Vector2(375, 667)
                Dim shapes1 = result.Shapes
                ' Rectangle Path 1
                shapes1.Add(SpriteShape_00())
                ' Layer (Shape): Dot-Y
                shapes1.Add(ContainerShape_00())
                ' Layer (Shape): E3-Y
                shapes1.Add(ContainerShape_04())
                ' Layer (Shape): E3-B
                shapes1.Add(ContainerShape_07())
                ' Layer (Shape): I-Y
                shapes1.Add(ContainerShape_10())
                ' Layer (Shape): I-B
                shapes1.Add(ContainerShape_13())
                ' Layer (Shape): E2-Y
                shapes1.Add(ContainerShape_16())
                ' Layer (Shape): E2-B
                shapes1.Add(ContainerShape_19())
                ' Layer (Shape): E1-Y
                shapes1.Add(ContainerShape_22())
                ' Layer (Shape): E1-B
                shapes1.Add(ContainerShape_25())
                ' Layer (Shape): T1a-Y
                shapes1.Add(ContainerShape_28())
                ' Layer (Shape): T2b-Y
                shapes1.Add(ContainerShape_31())
                ' Layer (Shape): T2a-Y
                shapes1.Add(ContainerShape_32())
                ' Layer (Shape): T2b-B
                shapes1.Add(ContainerShape_33())
                ' Layer (Shape): T1b-Y
                shapes1.Add(ContainerShape_34())
                ' Layer (Shape): T1b-B
                shapes1.Add(ContainerShape_35())
                ' Layer (Shape): O-Y
                shapes1.Add(ContainerShape_36())
                ' Layer (Shape): O-B
                shapes1.Add(ContainerShape_39())
                ' Layer (Shape): T1a-Y 2
                shapes1.Add(ContainerShape_42())
                ' Layer (Shape): T2a-B
                shapes1.Add(ContainerShape_45())
                ' Layer (Shape): T1a-B
                shapes1.Add(ContainerShape_46())
                ' Layer (Shape): Dot-Y
                shapes1.Add(ContainerShape_49())
                ' Layer (Shape): L-Y
                shapes1.Add(ContainerShape_53())
                ' Layer (Shape): L-B
                shapes1.Add(ContainerShape_54())
                ' Layer (Shape): Dot1
                shapes1.Add(ContainerShape_55())
                ' Layer (Shape): S1-Y
                shapes1.Add(ContainerShape_58())
                ' Layer (Shape): S2-Y
                shapes1.Add(ContainerShape_59())
                ' Layer (Shape): S7
                shapes1.Add(ContainerShape_60())
                ' Layer (Shape): S8
                shapes1.Add(ContainerShape_61())
                ' Layer (Shape): S3-Y
                shapes1.Add(ContainerShape_62())
                ' Layer (Shape): S4-Y
                shapes1.Add(ContainerShape_63())
                ' Layer (Shape): S5-Y
                shapes1.Add(ContainerShape_64())
                ' Layer (Shape): S6-Y
                shapes1.Add(ContainerShape_65())
                ' Layer (Shape): S3-Y 2
                shapes1.Add(ContainerShape_66())
                ' Layer (Shape): S4-Y 2
                shapes1.Add(ContainerShape_67())
                ' Layer (Shape): S5-Y 2
                shapes1.Add(ContainerShape_68())
                ' Layer (Shape): S11
                shapes1.Add(ContainerShape_69())
                ' Layer (Shape): S12
                shapes1.Add(ContainerShape_70())
                ' Layer (Shape): S13
                shapes1.Add(ContainerShape_71())
                ' Layer (Shape): S3-Y 3
                shapes1.Add(ContainerShape_72())
                ' Layer (Shape): S4-Y 3
                shapes1.Add(ContainerShape_73())
                ' Layer (Shape): S5-Y 3
                shapes1.Add(ContainerShape_74())
                ' Layer (Shape): S3-Y 4
                shapes1.Add(ContainerShape_75())
                ' Layer (Shape): S4-Y 4
                shapes1.Add(ContainerShape_76())
                ' Layer (Shape): S5-Y 4
                shapes1.Add(ContainerShape_77())
                Return result
            End Function
            ' Rectangle Path 1
            Private Function SpriteShape_00() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 187.5F, 333.5F)
                result.FillBrush = ColorBrush_AlmostDarkTurquoise_FF00D1C1()
                result.Geometry = RoundedRectangle_375x667()
                Return result
            End Function
            ' Transforms: Dot-Y
            ' Ellipse Path 1
            Private Function SpriteShape_01() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 196, 267)
                result.FillBrush = ColorBrush_White()
                result.Geometry = Ellipse_4p6()
                Return result
            End Function
            ' Transforms: E3-Y
            ' Path 1
            Private Function SpriteShape_02() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 344.674F, 261.877F)
                result.Geometry = PathGeometry_00()
                result.StrokeBrush = ColorBrush_AlmostTeal_FF007A87()
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 9.562F
                Return result
            End Function
            ' Transforms: E3-Y
            ' Path 1
            Private Function SpriteShape_03() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 344.739F, 261.877F)
                result.Geometry = PathGeometry_01()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 9.562F
                Return result
            End Function
            ' Transforms: I-Y
            ' Path 1
            Private Function SpriteShape_04() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 304.135F, 282.409F)
                result.Geometry = PathGeometry_02()
                result.StrokeBrush = _colorBrush_AlmostTeal_FF007A87
                result.StrokeDashCap = CompositionStrokeCap.Square
                result.StrokeEndCap = CompositionStrokeCap.Square
                result.StrokeStartCap = CompositionStrokeCap.Square
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 8.4F
                Return result
            End Function
            ' Transforms: I-Y
            ' Path 1
            Private Function SpriteShape_05() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 304.135F, 282.409F)
                result.Geometry = PathGeometry_03()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Square
                result.StrokeEndCap = CompositionStrokeCap.Square
                result.StrokeStartCap = CompositionStrokeCap.Square
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 9.194F
                Return result
            End Function
            ' Transforms: E2-Y
            ' Path 1
            Private Function SpriteShape_06() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 331.664F, 238.14F)
                result.Geometry = PathGeometry_04()
                result.StrokeBrush = _colorBrush_AlmostTeal_FF007A87
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 8.4F
                Return result
            End Function
            ' Transforms: E2-Y
            ' Path 1
            Private Function SpriteShape_07() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 331.664F, 238.14F)
                result.Geometry = PathGeometry_05()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 9.562F
                Return result
            End Function
            ' Transforms: E1-Y
            ' Path 1
            Private Function SpriteShape_08() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 344.672F, 214.842F)
                result.Geometry = PathGeometry_06()
                result.StrokeBrush = _colorBrush_AlmostTeal_FF007A87
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 8.4F
                Return result
            End Function
            ' Transforms: E1-Y
            ' Path 1
            Private Function SpriteShape_09() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 344.672F, 214.842F)
                result.Geometry = PathGeometry_07()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 9.562F
                Return result
            End Function
            ' Transforms: T1a-Y
            ' Path 1
            Private Function SpriteShape_10() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 227.677F, 234.375F)
                result.Geometry = PathGeometry_08()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 8.4F
                Return result
            End Function
            ' Layer (Shape): T2b-Y
            ' Path 1
            Private Function SpriteShape_11() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, -56.5F, 83.5F)
                result.Geometry = PathGeometry_09()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 9.194F
                Return result
            End Function
            ' Layer (Shape): T2a-Y
            ' Path 1
            Private Function SpriteShape_12() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 221.198F, 330.758F)
                result.Geometry = PathGeometry_10()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Square
                result.StrokeEndCap = CompositionStrokeCap.Square
                result.StrokeStartCap = CompositionStrokeCap.Square
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 9.194F
                Return result
            End Function
            ' Layer (Shape): T2b-B
            ' Path 1
            Private Function SpriteShape_13() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, -56.5F, 83.5F)
                result.Geometry = PathGeometry_11()
                result.StrokeBrush = _colorBrush_AlmostTeal_FF007A87
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 9.194F
                Return result
            End Function
            ' Layer (Shape): T1b-Y
            ' Path 1
            Private Function SpriteShape_14() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 186.256F, 349.081F)
                result.Geometry = PathGeometry_12()
                result.StrokeBrush = _colorBrush_AlmostTeal_FF007A87
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeLineJoin = CompositionStrokeLineJoin.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 8.4F
                Return result
            End Function
            ' Layer (Shape): T1b-B
            ' Path 1
            Private Function SpriteShape_15() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 186.256F, 349.081F)
                result.Geometry = PathGeometry_13()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeLineJoin = CompositionStrokeLineJoin.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 9.194F
                Return result
            End Function
            ' Transforms: O-Y
            ' Ellipse Path 1
            Private Function SpriteShape_16() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 196, 267)
                result.Geometry = Ellipse_1p5_0()
                result.StrokeBrush = _colorBrush_White
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 8.8F
                Return result
            End Function
            ' Transforms: O-B
            ' Ellipse Path 1
            Private Function SpriteShape_17() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 196, 267)
                result.Geometry = Ellipse_1p5_1()
                result.StrokeBrush = _colorBrush_AlmostTeal_FF007A87
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 9.194F
                Return result
            End Function
            ' Transforms: T1a-Y 2
            ' Path 1
            Private Function SpriteShape_18() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 227.677F, 234.375F)
                result.Geometry = PathGeometry_14()
                result.StrokeBrush = _colorBrush_AlmostTeal_FF007A87
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 8.4F
                Return result
            End Function
            ' Layer (Shape): T2a-B
            ' Path 1
            Private Function SpriteShape_19() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 221.198F, 330.758F)
                result.Geometry = PathGeometry_15()
                result.StrokeBrush = _colorBrush_AlmostTeal_FF007A87
                result.StrokeDashCap = CompositionStrokeCap.Square
                result.StrokeEndCap = CompositionStrokeCap.Square
                result.StrokeStartCap = CompositionStrokeCap.Square
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 9.194F
                Return result
            End Function
            ' Transforms: T1a-Y
            ' Path 1
            Private Function SpriteShape_20() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 227.677F, 234.375F)
                result.Geometry = PathGeometry_16()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 9.194F
                Return result
            End Function
            ' Transforms: Dot-Y
            ' Ellipse Path 1
            Private Function SpriteShape_21() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 196, 267)
                result.FillBrush = _colorBrush_White
                result.Geometry = Ellipse_4p7()
                Return result
            End Function
            ' Layer (Shape): L-Y
            ' Path 1
            Private Function SpriteShape_22() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 109.529007F, 354.143F)
                result.Geometry = PathGeometry_17()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 8.4F
                Return result
            End Function
            ' Layer (Shape): L-B
            ' Path 1
            Private Function SpriteShape_23() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 109.529007F, 354.143F)
                result.Geometry = PathGeometry_18()
                result.StrokeBrush = _colorBrush_AlmostTeal_FF007A87
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 10
                result.StrokeThickness = 9.194F
                Return result
            End Function
            ' Transforms: Dot1
            ' Ellipse Path 1
            Private Function SpriteShape_24() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 196, 267)
                result.FillBrush = _colorBrush_White
                result.Geometry = _ellipse_4p7
                Return result
            End Function
            ' Layer (Shape): S1-Y
            ' Path 1
            Private Function SpriteShape_25() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 179.5F, 333.5F)
                result.Geometry = PathGeometry_19()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 1.5F
                Return result
            End Function
            ' Layer (Shape): S2-Y
            ' Path 1
            Private Function SpriteShape_26() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 179.5F, 333.5F)
                result.Geometry = PathGeometry_20()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 1.5F
                Return result
            End Function
            ' Layer (Shape): S7
            ' Path 1
            Private Function SpriteShape_27() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 179.5F, 333.5F)
                result.Geometry = PathGeometry_21()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 1.5F
                Return result
            End Function
            ' Layer (Shape): S8
            ' Path 1
            Private Function SpriteShape_28() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 179.5F, 333.5F)
                result.Geometry = PathGeometry_22()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 1.5F
                Return result
            End Function
            ' Layer (Shape): S3-Y
            ' Path 1
            Private Function SpriteShape_29() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 179.5F, 333.5F)
                result.Geometry = PathGeometry_23()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 2
                Return result
            End Function
            ' Layer (Shape): S4-Y
            ' Path 1
            Private Function SpriteShape_30() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 179.5F, 333.5F)
                result.Geometry = PathGeometry_24()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 2
                Return result
            End Function
            ' Layer (Shape): S5-Y
            ' Path 1
            Private Function SpriteShape_31() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 179.5F, 333.5F)
                result.Geometry = PathGeometry_25()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 2
                Return result
            End Function
            ' Layer (Shape): S6-Y
            ' Path 1
            Private Function SpriteShape_32() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 179.5F, 333.5F)
                result.Geometry = PathGeometry_26()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 2
                Return result
            End Function
            ' Layer (Shape): S3-Y 2
            ' Path 1
            Private Function SpriteShape_33() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 179.5F, 333.5F)
                result.Geometry = PathGeometry_27()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 2
                Return result
            End Function
            ' Layer (Shape): S4-Y 2
            ' Path 1
            Private Function SpriteShape_34() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 179.5F, 333.5F)
                result.Geometry = PathGeometry_28()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 2
                Return result
            End Function
            ' Layer (Shape): S5-Y 2
            ' Path 1
            Private Function SpriteShape_35() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 179.5F, 333.5F)
                result.Geometry = PathGeometry_29()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 2
                Return result
            End Function
            ' Layer (Shape): S11
            ' Path 1
            Private Function SpriteShape_36() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 179.5F, 333.5F)
                result.Geometry = PathGeometry_30()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 1.5F
                Return result
            End Function
            ' Layer (Shape): S12
            ' Path 1
            Private Function SpriteShape_37() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 179.5F, 333.5F)
                result.Geometry = PathGeometry_31()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 1.5F
                Return result
            End Function
            ' Layer (Shape): S13
            ' Path 1
            Private Function SpriteShape_38() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(1, 0, 0, 1, 179.5F, 333.5F)
                result.Geometry = PathGeometry_32()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 1.5F
                Return result
            End Function
            ' Layer (Shape): S3-Y 3
            ' Path 1
            Private Function SpriteShape_39() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(-0.137444615F, 0.99050945F, -0.99050945F, -0.137444615F, 212.662F, 248.428F)
                result.Geometry = PathGeometry_33()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 2
                Return result
            End Function
            ' Layer (Shape): S4-Y 3
            ' Path 1
            Private Function SpriteShape_40() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(-0.137444615F, 0.99050945F, -0.99050945F, -0.137444615F, 212.662F, 248.428F)
                result.Geometry = PathGeometry_34()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 2
                Return result
            End Function
            ' Layer (Shape): S5-Y 3
            ' Path 1
            Private Function SpriteShape_41() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(-0.137444615F, 0.99050945F, -0.99050945F, -0.137444615F, 212.662F, 248.428F)
                result.Geometry = PathGeometry_35()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 2
                Return result
            End Function
            ' Layer (Shape): S3-Y 4
            ' Path 1
            Private Function SpriteShape_42() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(0.0157074F, -0.9998766F, 0.9998766F, 0.0157074F, 207.662F, 419.427979F)
                result.Geometry = PathGeometry_36()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 2
                Return result
            End Function
            ' Layer (Shape): S4-Y 4
            ' Path 1
            Private Function SpriteShape_43() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(0.0157074F, -0.9998766F, 0.9998766F, 0.0157074F, 207.662F, 419.427979F)
                result.Geometry = PathGeometry_37()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 2
                Return result
            End Function
            ' Layer (Shape): S5-Y 4
            ' Path 1
            Private Function SpriteShape_44() As CompositionSpriteShape
                Dim result = _c.CreateSpriteShape()
                result.TransformMatrix = New Matrix3x2(0.0157074F, -0.9998766F, 0.9998766F, 0.0157074F, 207.662F, 419.427979F)
                result.Geometry = PathGeometry_38()
                result.StrokeBrush = _colorBrush_White
                result.StrokeDashCap = CompositionStrokeCap.Round
                result.StrokeEndCap = CompositionStrokeCap.Round
                result.StrokeStartCap = CompositionStrokeCap.Round
                result.StrokeMiterLimit = 4
                result.StrokeThickness = 2
                Return result
            End Function
            Private Function StepThenHoldEasingFunction() As StepEasingFunction
                Dim result = __InlineAssignHelper(_stepThenHoldEasingFunction, _c.CreateStepEasingFunction())
                result.IsInitialStepSingleFrame = True
                Return result
            End Function
            ' Transforms: Dot-Y
            ' Position
            Private Function Vector2Animation_00() As Vector2KeyFrameAnimation
                Dim result = _c.CreateVector2KeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, New Vector2(43.263F, 59.75F), StepThenHoldEasingFunction())
                result.InsertKeyFrame(0.5363129F, New Vector2(43.263F, 59.75F), HoldThenStepEasingFunction())
                result.InsertKeyFrame(0.603351951F, New Vector2(62.513F, 59.75F), CubicBezierEasingFunction_00())
                result.InsertKeyFrame(0.6424581F, New Vector2(63.763F, 59.75F), CubicBezierEasingFunction_01())
                Return result
            End Function
            ' Transforms: Bncr
            ' Position
            Private Function Vector2Animation_01() As Vector2KeyFrameAnimation
                Dim result = _c.CreateVector2KeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, New Vector2(164.782F, 57.473F), _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.5363129F, New Vector2(164.782F, 57.473F), _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.553072631F, New Vector2(164.782F, 55.473F), CubicBezierEasingFunction_02())
                result.InsertKeyFrame(0.5698324F, New Vector2(164.782F, 57.473F), CubicBezierEasingFunction_03())
                result.InsertKeyFrame(0.5865922F, New Vector2(164.782F, 56.909F), _cubicBezierEasingFunction_02)
                result.InsertKeyFrame(0.603351951F, New Vector2(164.782F, 57.473F), _cubicBezierEasingFunction_03)
                Return result
            End Function
            ' Position
            Private Function Vector2Animation_02() As Vector2KeyFrameAnimation
                Dim result = __InlineAssignHelper(_vector2Animation_02, _c.CreateVector2KeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, New Vector2(119.167F, 57.479F), _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.469273746F, New Vector2(119.167F, 57.479F), _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.5139665F, New Vector2(137.167F, 57.479F), CubicBezierEasingFunction_05())
                result.InsertKeyFrame(0.5363129F, New Vector2(134.167F, 57.479F), CubicBezierEasingFunction_06())
                Return result
            End Function
            ' Position
            Private Function Vector2Animation_03() As Vector2KeyFrameAnimation
                Dim result = __InlineAssignHelper(_vector2Animation_03, _c.CreateVector2KeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, New Vector2(93.594F, 62.861F), _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.43575418F, New Vector2(93.594F, 62.861F), _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.491620123F, New Vector2(92.626F, 82.829F), _cubicBezierEasingFunction_07)
                result.InsertKeyFrame(0.5139665F, New Vector2(92.844F, 77.861F), CubicBezierEasingFunction_08())
                Return result
            End Function
            ' Position
            Private Function Vector2Animation_04() As Vector2KeyFrameAnimation
                Dim result = __InlineAssignHelper(_vector2Animation_04, _c.CreateVector2KeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, New Vector2(109.092F, 33.61F), _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.463687152F, New Vector2(109.092F, 33.61F), _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.5139665F, New Vector2(121.092F, 33.61F), _cubicBezierEasingFunction_07)
                result.InsertKeyFrame(0.5363129F, New Vector2(121.092F, 33.61F), CubicBezierEasingFunction_09())
                Return result
            End Function
            ' Position
            Private Function Vector2Animation_05() As Vector2KeyFrameAnimation
                Dim result = __InlineAssignHelper(_vector2Animation_05, _c.CreateVector2KeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, New Vector2(113.715F, 9.146F), _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.441340774F, New Vector2(113.715F, 9.146F), _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.491620123F, New Vector2(137.715F, 9.146F), _cubicBezierEasingFunction_07)
                result.InsertKeyFrame(0.5139665F, New Vector2(133.715F, 9.146F), _cubicBezierEasingFunction_08)
                Return result
            End Function
            ' Position
            Private Function Vector2Animation_06() As Vector2KeyFrameAnimation
                Dim result = __InlineAssignHelper(_vector2Animation_06, _c.CreateVector2KeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, New Vector2(39.043F, 48.678F), _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.312849164F, New Vector2(39.043F, 48.678F), _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.3575419F, New Vector2(39.043F, 45.678F), _cubicBezierEasingFunction_05)
                Return result
            End Function
            ' Radius
            Private Function Vector2Animation_07() As Vector2KeyFrameAnimation
                Dim result = __InlineAssignHelper(_vector2Animation_07, _c.CreateVector2KeyFrameAnimation())
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, New Vector2(1.5F, 1.5F), _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.301675975F, New Vector2(1.5F, 1.5F), _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.340782136F, New Vector2(22.3F, 22.3F), CubicBezierEasingFunction_14())
                Return result
            End Function
            ' Transforms: O-Y
            ' Position
            Private Function Vector2Animation_08() As Vector2KeyFrameAnimation
                Dim result = _c.CreateVector2KeyFrameAnimation()
                result.SetReferenceParameter("_", _root)
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, New Vector2(-62.792F, 73.057F), _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.17318435F, New Vector2(-62.792F, 73.057F), _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.196966484F, New Vector2(-53.792F, 7.557F), _cubicBezierEasingFunction_04)
                result.InsertExpressionKeyFrame(0.245809957F, "(Pow(1 - _.t0, 3) * Vector2((-53.792),7.557)) + (3 * Square(1 - _.t0) * _.t0 * Vector2((-53.792),7.557)) + (3 * (1 - _.t0) * Square(_.t0) * Vector2((-52.82329),(-71.07968))) + (Pow(_.t0, 3) * Vector2((-33.667),(-72.818)))", _stepThenHoldEasingFunction)
                result.InsertExpressionKeyFrame(0.3016759F, "(Pow(1 - _.t0, 3) * Vector2((-33.667),(-72.818))) + (3 * Square(1 - _.t0) * _.t0 * Vector2((-17.45947),(-74.28873))) + (3 * (1 - _.t0) * Square(_.t0) * Vector2((-14.167),102.182)) + (Pow(_.t0, 3) * Vector2((-14.167),102.182))", _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.301675975F, New Vector2(-14.167F, 102.182F), _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.3519553F, New Vector2(-14.167F, 59.182F), CubicBezierEasingFunction_15())
                result.InsertKeyFrame(0.407821238F, New Vector2(-14.167F, 62.182F), CubicBezierEasingFunction_16())
                Return result
            End Function
            ' Transforms: O-B
            ' Position
            Private Function Vector2Animation_09() As Vector2KeyFrameAnimation
                Dim result = _c.CreateVector2KeyFrameAnimation()
                result.SetReferenceParameter("_", _root)
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, New Vector2(-62.792F, 73.057F), _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.17318435F, New Vector2(-62.792F, 73.057F), _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.196966484F, New Vector2(-53.792F, 7.557F), _cubicBezierEasingFunction_04)
                result.InsertExpressionKeyFrame(0.245809957F, "(Pow(1 - _.t1, 3) * Vector2((-53.792),7.557)) + (3 * Square(1 - _.t1) * _.t1 * Vector2((-53.792),7.557)) + (3 * (1 - _.t1) * Square(_.t1) * Vector2((-52.82329),(-71.07968))) + (Pow(_.t1, 3) * Vector2((-33.667),(-72.818)))", _stepThenHoldEasingFunction)
                result.InsertExpressionKeyFrame(0.3016759F, "(Pow(1 - _.t1, 3) * Vector2((-33.667),(-72.818))) + (3 * Square(1 - _.t1) * _.t1 * Vector2((-17.45947),(-74.28873))) + (3 * (1 - _.t1) * Square(_.t1) * Vector2((-14.167),102.182)) + (Pow(_.t1, 3) * Vector2((-14.167),102.182))", _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.301675975F, New Vector2(-14.167F, 102.182F), _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.3519553F, New Vector2(-14.167F, 59.182F), _cubicBezierEasingFunction_15)
                result.InsertKeyFrame(0.407821238F, New Vector2(-14.167F, 62.182F), _cubicBezierEasingFunction_16)
                Return result
            End Function
            ' Transforms: Dot-Y
            ' Position
            Private Function Vector2Animation_10() As Vector2KeyFrameAnimation
                Dim result = _c.CreateVector2KeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, New Vector2(39.875F, 60), _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.156424582F, New Vector2(39.875F, 60), _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.301675975F, New Vector2(79.375F, 60), _cubicBezierEasingFunction_04)
                Return result
            End Function
            ' Transforms: N
            ' Position
            Private Function Vector2Animation_11() As Vector2KeyFrameAnimation
                Dim result = _c.CreateVector2KeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, New Vector2(-33.667F, 8.182F), _stepThenHoldEasingFunction)
                result.InsertKeyFrame(0.156424582F, New Vector2(-33.667F, 8.182F), _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.223463684F, New Vector2(-33.667F, -72.818F), CubicBezierEasingFunction_19())
                result.InsertKeyFrame(0.301675975F, New Vector2(-33.667F, 102.057F), CubicBezierEasingFunction_20())
                Return result
            End Function
            ' Transforms: Dot1
            ' Position
            Private Function Vector2Animation_12() As Vector2KeyFrameAnimation
                Dim result = _c.CreateVector2KeyFrameAnimation()
                result.Duration = TimeSpan.FromTicks(c_durationTicks)
                result.InsertKeyFrame(0, New Vector2(295.771F, 108.994F), _holdThenStepEasingFunction)
                result.InsertKeyFrame(0.104395606F, New Vector2(35.771F, 108.994F), CubicBezierEasingFunction_26())
                Return result
            End Function

            Friend Sub New(compositor1 As Compositor)
                _c = compositor1
                _reusableExpressionAnimation = compositor1.CreateExpressionAnimation()
                Root()
            End Sub
            ReadOnly Property IAnimatedVisual_RootVisual As Visual Implements IAnimatedVisual.RootVisual
                Get
                    Return _root
                End Get
            End Property
            ReadOnly Property IAnimatedVisual_Duration As TimeSpan Implements IAnimatedVisual.Duration
                Get
                    Return TimeSpan.FromTicks(c_durationTicks)
                End Get
            End Property
            ReadOnly Property IAnimatedVisual_Size As Vector2 Implements IAnimatedVisual.Size
                Get
                    Return New Vector2(375, 667)
                End Get
            End Property
            Private Sub Dispose() Implements System.IDisposable.Dispose
                _root?.Dispose()
            End Sub
            <Obsolete("Please refactor code that uses this function, it is a simple work-around to simulate inline assignment in VB!")>
            Private Shared Function __InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class
End Namespace
