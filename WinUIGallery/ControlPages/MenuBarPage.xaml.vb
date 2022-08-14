' To configure or remove Option's included in result, go to Options/Advanced Options...
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
    Public NotInheritable Partial Class MenuBarPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub OnElementClicked(sender As Object, e As Microsoft.UI.Xaml.RoutedEventArgs)
            Dim selectedFlyoutItem = TryCast(sender, MenuFlyoutItem)
            Dim exampleNumber As String = selectedFlyoutItem.Name.Substring(0, 1)
            If exampleNumber = "o" Then
                SelectedOptionText.Text = "You clicked: " & TryCast(sender, MenuFlyoutItem).Text
            ElseIf exampleNumber = "t" Then
                SelectedOptionText1.Text = "You clicked: " & TryCast(sender, MenuFlyoutItem).Text
            ElseIf exampleNumber = "z" Then
                SelectedOptionText2.Text = "You clicked: " & TryCast(sender, MenuFlyoutItem).Text
            End If
        End Sub
    End Class
End Namespace
