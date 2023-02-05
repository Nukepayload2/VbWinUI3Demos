Option Strict On

Imports Microsoft.UI.Xaml

Public Class ProgressDialog
    Inherits Window

    Sub New()

        InitializeComponent()

    End Sub

    Public Sub UpdateProgress(progressText As String, value As Double, maxValue As Double)
        LblProgressText.Text = progressText
        PrgProgress.Maximum = maxValue
        PrgProgress.Value = value
    End Sub
End Class
