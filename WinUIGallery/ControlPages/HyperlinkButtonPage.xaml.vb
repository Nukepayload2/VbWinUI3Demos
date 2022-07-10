'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class HyperlinkButtonPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub GoToHyperlinkButton_Click(sender As Object, e As RoutedEventArgs)
            NavigationRootPage.GetForElement(Me).Navigate(GetType(ItemPage), "ToggleButton")
        End Sub
    End Class
End Namespace
