'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************


Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media
Imports Windows.Foundation
Imports System.Numerics

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class RadialGradientBrushPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
            AddHandler Loaded, AddressOf OnPageLoaded
        End Sub
        Private Sub OnPageLoaded(sender As Object, e As Microsoft.UI.Xaml.RoutedEventArgs)
            AddHandler MappingModeComboBox.SelectionChanged, AddressOf OnMappingModeChanged
            AddHandler SpreadMethodComboBox.SelectionChanged, AddressOf OnSpreadMethodChanged
            InitializeSliders()
        End Sub
        Private Sub OnSpreadMethodChanged(sender As Object, e As SelectionChangedEventArgs)
            RadialGradientBrushExample.SpreadMethod = [Enum].Parse(Of GradientSpreadMethod)(SpreadMethodComboBox.SelectedValue.ToString())
        End Sub
        Private Sub OnMappingModeChanged(sender As Object, e As SelectionChangedEventArgs)
            RadialGradientBrushExample.MappingMode = [Enum].Parse(Of BrushMappingMode)(MappingModeComboBox.SelectedValue.ToString())
            InitializeSliders()
        End Sub
        Private Sub InitializeSliders()
            Dim rectSize = Rect.ActualSize.ToSize()
            If RadialGradientBrushExample.MappingMode = BrushMappingMode.Absolute Then
                CenterXSlider.Maximum = __InlineAssignHelper(RadiusXSlider.Maximum, __InlineAssignHelper(OriginXSlider.Maximum, rectSize.Width))
                CenterYSlider.Maximum = __InlineAssignHelper(RadiusYSlider.Maximum, __InlineAssignHelper(OriginYSlider.Maximum, rectSize.Width))
                CenterXSlider.Value = __InlineAssignHelper(RadiusXSlider.Value, __InlineAssignHelper(OriginXSlider.Value, rectSize.Width / 2))
                CenterYSlider.Value = __InlineAssignHelper(RadiusYSlider.Value, __InlineAssignHelper(OriginYSlider.Value, rectSize.Width / 2))
                CenterXSlider.StepFrequency = __InlineAssignHelper(RadiusXSlider.StepFrequency, __InlineAssignHelper(OriginXSlider.StepFrequency, rectSize.Width / 50))
                CenterYSlider.StepFrequency = __InlineAssignHelper(RadiusYSlider.StepFrequency, __InlineAssignHelper(OriginYSlider.StepFrequency, rectSize.Height / 50))
            Else
                CenterXSlider.Maximum = __InlineAssignHelper(RadiusXSlider.Maximum, __InlineAssignHelper(OriginXSlider.Maximum, 1.0))
                CenterYSlider.Maximum = __InlineAssignHelper(RadiusYSlider.Maximum, __InlineAssignHelper(OriginYSlider.Maximum, 1.0))
                CenterXSlider.Value = __InlineAssignHelper(RadiusXSlider.Value, __InlineAssignHelper(OriginXSlider.Value, 0.5))
                CenterYSlider.Value = __InlineAssignHelper(RadiusYSlider.Value, __InlineAssignHelper(OriginYSlider.Value, 0.5))
                CenterXSlider.StepFrequency = __InlineAssignHelper(RadiusXSlider.StepFrequency, __InlineAssignHelper(OriginXSlider.StepFrequency, 0.02))
                CenterYSlider.StepFrequency = __InlineAssignHelper(RadiusYSlider.StepFrequency, __InlineAssignHelper(OriginYSlider.StepFrequency, 0.02))
            End If
        End Sub
        Private Sub OnSliderValueChanged(sender As Object, e As Microsoft.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs)
            RadialGradientBrushExample.Center = New Point(CenterXSlider.Value, CenterYSlider.Value)
            RadialGradientBrushExample.RadiusX = RadiusXSlider.Value
            RadialGradientBrushExample.RadiusY = RadiusYSlider.Value
            RadialGradientBrushExample.GradientOrigin = New Point(OriginXSlider.Value, OriginYSlider.Value)
        End Sub
        <Obsolete("Please refactor code that uses this function, it is a simple work-around to simulate inline assignment in VB!")>
        Private Shared Function __InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
End Namespace
