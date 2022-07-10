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
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class AnimatedIconPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub Button_PointerEntered(sender As Object, e As PointerRoutedEventArgs)
            AnimatedIcon.SetState(Me.SearchAnimatedIcon, "PointerOver")
        End Sub
        Private Sub Button_PointerExited(sender As Object, e As PointerRoutedEventArgs)
            AnimatedIcon.SetState(Me.SearchAnimatedIcon, "Normal")
        End Sub
    End Class
End Namespace
