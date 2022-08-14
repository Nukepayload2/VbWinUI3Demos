'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************


Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class ToggleButtonPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()

            ' Set initial output value.
            Control1Output.Text = If(CBool(Toggle1.IsChecked), "On", "Off")
        End Sub
        Private Sub ToggleButton_Checked(sender As Object, e As RoutedEventArgs)
            Control1Output.Text = "On"
        End Sub
        Private Sub ToggleButton_Unchecked(sender As Object, e As RoutedEventArgs)
            Control1Output.Text = "Off"
        End Sub
    End Class
End Namespace
