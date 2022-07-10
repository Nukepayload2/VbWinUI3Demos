'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports Windows.Foundation.Metadata
Imports Microsoft.UI
Imports Microsoft.UI.Windowing
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class RevealFocusPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()

            ' DEMO ONLY: Initialize Color rectangles
            If Spring2018 AndAlso Application.Current.FocusVisualKind = FocusVisualKind.Reveal Then
                RevealFocus.IsChecked = True
                myPrimaryColorPicker.Color = TryCast(Me.Resources("SystemControlRevealFocusVisualBrush"), SolidColorBrush).Color
                mySecondaryColorPicker.Color = TryCast(Me.Resources("SystemControlFocusVisualSecondaryBrush"), SolidColorBrush).Color
                primaryColorPickerButton.Background = New SolidColorBrush(myPrimaryColorPicker.Color)
                secondaryColorPickerButton.Background = New SolidColorBrush(mySecondaryColorPicker.Color)
            Else
                HighVisibility.IsChecked = True
                primaryColorPickerButton.Background = New SolidColorBrush(myPrimaryColorPicker.Color)
                secondaryColorPickerButton.Background = New SolidColorBrush(mySecondaryColorPicker.Color)
            End If
        End Sub
        ' DEMO ONLY: Change focus visual mode to high visibility
        Private Sub HighVisibility_Checked(sender As Object, e As RoutedEventArgs)
            If exampleButton.ActualTheme = ElementTheme.Light Then
                myPrimaryColorPicker.Color = Colors.Black
                mySecondaryColorPicker.Color = Colors.White
            ElseIf exampleButton.ActualTheme = ElementTheme.Dark Then
                myPrimaryColorPicker.Color = Colors.White
                mySecondaryColorPicker.Color = Colors.Black
            End If

            primaryColorPickerButton.Background = New SolidColorBrush(myPrimaryColorPicker.Color)
            primaryBrushText.Value = "{StaticResource SystemControlFocusVisualPrimaryBrush}"
            primaryColorKeyText.Value = "SystemControlFocusVisualPrimaryBrush"
            Application.Current.FocusVisualKind = FocusVisualKind.HighVisibility
            FocusVisualKindSubstitution.Value = "HighVisibility"
        End Sub
        ' DEMO ONLY: Change focus visual mode to reveal focus
        Private Sub RevealFocus_Checked(sender As Object, e As RoutedEventArgs)
            If Spring2018 Then
                myPrimaryColorPicker.Color = TryCast(Me.Resources("SystemControlRevealFocusVisualBrush"), SolidColorBrush).Color
                primaryColorPickerButton.Background = New SolidColorBrush(myPrimaryColorPicker.Color)
                primaryBrushText.Value = "{StaticResource SystemControlRevealFocusVisualBrush}"
                primaryColorKeyText.Value = "SystemControlRevealFocusVisualBrush"
                Application.Current.FocusVisualKind = FocusVisualKind.Reveal
                FocusVisualKindSubstitution.Value = "Reveal"
            End If
        End Sub
        Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
            ' Draw the focus visuals at the edge of the control
            ' A negative FocusVisualMargin outsets the focus visual. A positive one insets the focus visual
            marginSlider.Value = -1 * (primarySlider.Value + secondarySlider.Value)
        End Sub
        Private Sub confirmColor_Click(sender As Object, e As RoutedEventArgs)
            ' DEMO ONLY: Set colors of the buttons to the selected color in ColorPicker
            primaryColorPickerButton.Background = New SolidColorBrush(myPrimaryColorPicker.Color)
            secondaryColorPickerButton.Background = New SolidColorBrush(mySecondaryColorPicker.Color)

            ' DEMO ONLY: Close the Flyout.
            primaryColorPickerButton.Flyout.Hide()
            secondaryColorPickerButton.Flyout.Hide()
        End Sub
        ''' <summary>
        ''' A property to identify if app is running on Spring2018 version of Windows
        ''' </summary>
        Public ReadOnly Property Spring2018 As Boolean
            Get
                Return ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 6)
            End Get
        End Property
        Private Sub Example2_ActualThemeChanged(sender As FrameworkElement, args As Object)
            If Example2.ActualTheme = ElementTheme.Light Then
                myPrimaryColorPicker.Color = Colors.Black
                mySecondaryColorPicker.Color = Colors.White
            ElseIf Example2.ActualTheme = ElementTheme.Dark Then
                myPrimaryColorPicker.Color = Colors.White
                mySecondaryColorPicker.Color = Colors.Black
            End If
        End Sub
        Private Sub MoveFocusBtn_Click(sender As Object, e As RoutedEventArgs)
            ' Set focus to button for better preview/demo of customization
            exampleButton.Focus(FocusState.Keyboard)
        End Sub
    End Class


    Public Class MyConverters
        Public Shared Function IntToThickness(UniformLength As Double) As Thickness
#If Not UNIVERSAL

            Return New Thickness(UniformLength)
#Else

            return ThicknessHelper.FromUniformLength(UniformLength);

#End If

        End Function
        Public Shared Function ColorToBrush(color1 As Windows.UI.Color) As SolidColorBrush
            Return New SolidColorBrush(color1)
        End Function

    End Class
End Namespace
