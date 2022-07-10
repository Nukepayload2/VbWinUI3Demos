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
Imports Microsoft.UI.Xaml.Documents

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class ImagePage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub ImageStretch_Checked(sender As Object, e As RoutedEventArgs)
            If StretchImage IsNot Nothing Then
                Dim strStretch = TryCast(sender, RadioButton).Content.ToString()
                Dim stretch1 = CType([Enum].Parse(GetType(Stretch), strStretch), Stretch)
                StretchImage.Stretch = stretch1
            End If
        End Sub
    End Class
End Namespace
