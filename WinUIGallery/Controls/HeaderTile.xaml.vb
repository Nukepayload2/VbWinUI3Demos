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
Imports Microsoft.UI.Composition
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Data
Imports Microsoft.UI.Xaml.Input
Imports Microsoft.UI.Xaml.Media
Imports Microsoft.UI.Xaml.Navigation
Imports Windows.Foundation
Imports Windows.Foundation.Collections

' To learn more about WinUI, the WinUI project structure,
' and more about our project templates, see: http://aka.ms/winui-project-info.

Namespace AppUIBasics.Controls
    Public NotInheritable Partial Class HeaderTile
        Inherits UserControl
        Private _compositor As Compositor = Microsoft.UI.Xaml.Media.CompositionTarget.GetCompositorForCurrentThread()
        Private _springAnimation As SpringVector3NaturalMotionAnimation
        Public Property Title As String
            Get
                Return CStr(GetValue(TitleProperty))
            End Get

            Set(value As String)
                SetValue(TitleProperty, value)
            End Set
        End Property
        Public Shared ReadOnly TitleProperty As DependencyProperty = DependencyProperty.Register("Title", GetType(String), GetType(HeaderTile), New PropertyMetadata(Nothing))
        Public Property Source As String
            Get
                Return CStr(GetValue(SourceProperty))
            End Get

            Set(value As String)
                SetValue(SourceProperty, value)
            End Set
        End Property
        Public Shared ReadOnly SourceProperty As DependencyProperty = DependencyProperty.Register("Title", GetType(String), GetType(HeaderTile), New PropertyMetadata(Nothing))
        Public Property Link As String
            Get
                Return CStr(GetValue(LinkProperty))
            End Get

            Set(value As String)
                SetValue(LinkProperty, value)
            End Set
        End Property
        Public Shared ReadOnly LinkProperty As DependencyProperty = DependencyProperty.Register("Title", GetType(String), GetType(HeaderTile), New PropertyMetadata(Nothing))


        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub Element_PointerEntered(sender As Object, e As PointerRoutedEventArgs)
            CreateOrUpdateSpringAnimation(1.1F)
            TryCast(sender, UIElement).CenterPoint = New Vector3(70, 40, 1F)
            TryCast(sender, UIElement).StartAnimation(_springAnimation)
        End Sub
        Private Sub Element_PointerExited(sender As Object, e As PointerRoutedEventArgs)
            CreateOrUpdateSpringAnimation(1.0F)
            TryCast(sender, UIElement).CenterPoint = New Vector3(70, 40, 1F)
            TryCast(sender, UIElement).StartAnimation(_springAnimation)
        End Sub
        Private Sub CreateOrUpdateSpringAnimation(finalValue As Single)
            If _springAnimation Is Nothing Then
                If _compositor IsNot Nothing Then
                    _springAnimation = _compositor.CreateSpringVector3Animation()
                    _springAnimation.Target = "Scale"
                End If
            End If

            _springAnimation.FinalValue = New Vector3(finalValue)
        End Sub
    End Class
End Namespace
