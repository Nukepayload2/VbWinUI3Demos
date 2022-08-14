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
    Public NotInheritable Partial Class ProgressBarPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub ProgressValue_ValueChanged(sender As Microsoft.UI.Xaml.Controls.NumberBox, args As Microsoft.UI.Xaml.Controls.NumberBoxValueChangedEventArgs)
            ' Value might be NaN, which is not valid as value, thus we need to handle changes ourselves
            If Not sender.Value.IsNaN() Then
                ProgressBar2.Value = sender.Value
            Else
                sender.Value = 0
            End If
        End Sub
    End Class
End Namespace
