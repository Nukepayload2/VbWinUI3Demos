' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Runtime.InteropServices.WindowsRuntime
Imports Windows.Foundation
Imports Windows.Foundation.Collections
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Data
Imports Microsoft.UI.Xaml.Input
Imports Microsoft.UI.Xaml.Media
Imports Microsoft.UI.Xaml.Media.Animation
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics.ControlPages
    Public Class NamedEasingFunction
        Private _name As String
        Public Property Name As String
            Get
                Return _name
            End Get
            Private Set(value As String)
                _name = value
            End Set
        End Property
        Private _easingFunctionBase As EasingFunctionBase
        Public Property EasingFunctionBase As EasingFunctionBase
            Get
                Return _easingFunctionBase
            End Get
            Private Set(value As EasingFunctionBase)
                _easingFunctionBase = value
            End Set
        End Property
        Public Sub New(name1 As String, easingFunctionBase1 As EasingFunctionBase)
            Me.Name = name1
            Me.EasingFunctionBase = easingFunctionBase1
        End Sub
    End Class


    Public NotInheritable Partial Class EasingFunctionPage
        Inherits Page
        Private ReadOnly Property EasingFunctions As List(Of NamedEasingFunction) = New List(Of NamedEasingFunction) From { _
            New NamedEasingFunction("BackEase", New BackEase),
            New NamedEasingFunction("BounceEase", New BounceEase),
            New NamedEasingFunction("CircleEase", New CircleEase),
            New NamedEasingFunction("CubicEase", New CubicEase),
            New NamedEasingFunction("ElasticEase", New ElasticEase),
            New NamedEasingFunction("ExponentialEase", New ExponentialEase),
            New NamedEasingFunction("PowerEase", New PowerEase),
            New NamedEasingFunction("QuadraticEase", New QuadraticEase),
            New NamedEasingFunction("QuarticEase", New QuarticEase),
            New NamedEasingFunction("QuinticEase", New QuinticEase),
            New NamedEasingFunction("SineEase", New SineEase)
        }

        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub Button1_Click(sender As Object, e As RoutedEventArgs)
            Storyboard1.Children(0).SetValue(DoubleAnimation.FromProperty, Translation1.X)
            Storyboard1.Children(0).SetValue(DoubleAnimation.ToProperty, If(Translation1.X > 0, 0, 200))
            Storyboard1.Begin()
        End Sub
        Private Sub Button2_Click(sender As Object, e As RoutedEventArgs)
            Storyboard2.Children(0).SetValue(DoubleAnimation.FromProperty, Translation2.X)
            Storyboard2.Children(0).SetValue(DoubleAnimation.ToProperty, If(Translation2.X > 0, 0, 200))
            Storyboard2.Begin()
        End Sub
        Private Sub Button3_Click(sender As Object, e As RoutedEventArgs)
            Storyboard3.Children(0).SetValue(DoubleAnimation.FromProperty, Translation3.X)
            Storyboard3.Children(0).SetValue(DoubleAnimation.ToProperty, If(Translation3.X > 0, 0, 200))
            Storyboard3.Begin()
        End Sub
        Private Sub Button4_Click(sender As Object, e As RoutedEventArgs)
            Dim easingFunction = TryCast(EasingComboBox.SelectedValue, EasingFunctionBase)
            easingFunction.EasingMode = GetEaseValue()
            TryCast(Storyboard4.Children(0), DoubleAnimation).EasingFunction = easingFunction

            Storyboard4.Children(0).SetValue(DoubleAnimation.FromProperty, Translation4.X)
            Storyboard4.Children(0).SetValue(DoubleAnimation.ToProperty, If(Translation4.X > 0, 0, 200))
            Storyboard4.Begin()
        End Sub
        Private Function GetEaseValue() As EasingMode
            If easeOutRB.IsChecked = True Then
                Return EasingMode.EaseOut
            ElseIf easeInRB.IsChecked = True Then
                Return EasingMode.EaseIn
            Else Return EasingMode.EaseInOut
            End If
        End Function
        Private Sub EasingComboBox_Loaded(sender As Object, e As RoutedEventArgs)
            EasingComboBox.SelectedIndex = 0
        End Sub
    End Class
End Namespace
