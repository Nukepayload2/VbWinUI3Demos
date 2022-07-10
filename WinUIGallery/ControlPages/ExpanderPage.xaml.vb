' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports Microsoft.UI.Xaml.Controls

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class ExpanderPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub ExpandDirectionComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim expandDirection As String = e.AddedItems(0).ToString()

            Select Case expandDirection

                Case "Up" '
                    Expander1.ExpandDirection = Microsoft.UI.Xaml.Controls.ExpandDirection.Up
                    Expander1.VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Bottom
                Case Else
                    Expander1.ExpandDirection = Microsoft.UI.Xaml.Controls.ExpandDirection.Down
                    Expander1.VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Top
            End Select
        End Sub
    End Class
End Namespace
