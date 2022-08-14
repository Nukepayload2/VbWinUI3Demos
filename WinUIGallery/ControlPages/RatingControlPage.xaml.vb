' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports Microsoft.UI.Xaml.Controls

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class RatingControlPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub RatingControl1_ValueChanged(sender As Microsoft.UI.Xaml.Controls.RatingControl, args As Object)
            RatingControl1.Caption = "Your rating"
        End Sub
    End Class
End Namespace
