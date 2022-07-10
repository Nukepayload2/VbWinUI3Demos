' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls

' The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

Namespace AppUIBasics.TabViewPages
    Public NotInheritable Partial Class MyTabContentControl
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub
    End Class
End Namespace
