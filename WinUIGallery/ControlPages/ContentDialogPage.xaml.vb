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
    Public NotInheritable Partial Class ContentDialogPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Async Sub ShowDialog_Click(sender As Object, e As RoutedEventArgs)
            Dim dialog As New ContentDialogExample

            ' XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = Me.XamlRoot
            dialog.Style = TryCast(Application.Current.Resources("DefaultContentDialogStyle"), Style)
            dialog.Title = "Save your work?"
            dialog.PrimaryButtonText = "Save"
            dialog.SecondaryButtonText = "Don't Save"
            dialog.CloseButtonText = "Cancel"
            dialog.DefaultButton = ContentDialogButton.Primary
            dialog.Content = New ContentDialogContent

            Dim result = Await dialog.ShowAsync()

            If result = ContentDialogResult.Primary Then
                DialogResult.Text = "User saved their work"
            ElseIf result = ContentDialogResult.Secondary Then
                DialogResult.Text = "User did not save their work"
            Else
                DialogResult.Text = "User cancelled the dialog"
            End If
        End Sub
    End Class
End Namespace
