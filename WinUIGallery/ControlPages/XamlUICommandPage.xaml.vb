' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports System.Collections.ObjectModel
Imports System.Windows.Input
Imports Windows.Foundation.Metadata
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Input

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class XamlUICommandPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub CustomXamlUICommand_ExecuteRequested(sender As XamlUICommand, args As ExecuteRequestedEventArgs)
            XamlUICommandOutput.Text = "You fired the custom command"
        End Sub
        Private Sub ControlExample_Loaded(sender As Object, e As RoutedEventArgs)
        End Sub
    End Class
End Namespace
