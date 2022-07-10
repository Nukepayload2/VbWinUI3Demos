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

Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Input

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class TeachingTipPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub TestButtonClick1(sender As Object, e As RoutedEventArgs)
            If NavigationRootPage.GetForElement(Me)?.PageHeader IsNot Nothing Then
                NavigationRootPage.GetForElement(Me).PageHeader.TeachingTip1.IsOpen = True
            End If
        End Sub
        Private Sub TestButtonClick2(sender As Object, e As RoutedEventArgs)
            If NavigationRootPage.GetForElement(Me)?.PageHeader IsNot Nothing Then
                NavigationRootPage.GetForElement(Me).PageHeader.TeachingTip2.IsOpen = True
            End If
        End Sub
        Private Sub TestButtonClick3(sender As Object, e As RoutedEventArgs)
            If NavigationRootPage.GetForElement(Me)?.PageHeader IsNot Nothing Then
                NavigationRootPage.GetForElement(Me).PageHeader.TeachingTip3.IsOpen = True
            End If
        End Sub
        Protected Overrides Sub OnKeyDown(e As KeyRoutedEventArgs)
            ' The non-light dismiss Teaching tips do not handle the escape key, however we do not want the page to navigate away while they are open, so we will mark these key events as

            ' handled while these tips are open.
            If e.Key = Windows.System.VirtualKey.Escape AndAlso (NavigationRootPage.GetForElement(Me).PageHeader.TeachingTip3.IsOpen OrElse NavigationRootPage.GetForElement(Me).PageHeader.TeachingTip1.IsOpen) Then
                e.Handled = True
            End If
        End Sub
    End Class
End Namespace
