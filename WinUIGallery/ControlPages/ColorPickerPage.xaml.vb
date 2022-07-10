' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports Microsoft.UI.Xaml.Controls

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class ColorPickerPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub ColorSpectrumShapeRadioButtons_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Select Case ColorSpectrumShapeRadioButtons.SelectedItem
                Case "Box"
                    colorPicker.ColorSpectrumShape = Microsoft.UI.Xaml.Controls.ColorSpectrumShape.Box
                Case Else
                    colorPicker.ColorSpectrumShape = Microsoft.UI.Xaml.Controls.ColorSpectrumShape.Ring
            End Select
        End Sub
    End Class
End Namespace
