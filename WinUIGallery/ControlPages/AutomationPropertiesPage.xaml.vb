'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************


Imports Microsoft.UI.Xaml.Controls

Namespace AppUIBasics.ControlPages
    ''' <summary>
    ''' Page containing sample related to XAML AutomationProperties.
    ''' </summary>
    Public NotInheritable Partial Class AutomationPropertiesPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub FontSizeNumberBox_ValueChanged(sender As Microsoft.UI.Xaml.Controls.NumberBox, args As Microsoft.UI.Xaml.Controls.NumberBoxValueChangedEventArgs)
            ' Ensure that if user clears the NumberBox, we don't pass 0 or null as fontsize
            If sender.Value >= sender.Minimum Then
                FontSizeChangingTextBlock.FontSize = sender.Value
            Else
                ' We fell below minimum, so lets restore a correct value
                sender.Value = sender.Minimum
            End If
        End Sub
    End Class
End Namespace
