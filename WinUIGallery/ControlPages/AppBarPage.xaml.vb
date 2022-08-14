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
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class AppBarPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub topAppBar_Opened(sender As Object, e As Object)
            Dim headerTopAppBar As CommandBar = NavigationRootPage.GetForElement(Me).PageHeader.TopCommandBar
            headerTopAppBar.IsOpen = False
        End Sub
        Private Sub OpenButton_Click(sender As Object, e As RoutedEventArgs)
            topAppBar.IsOpen = True
        End Sub
        Private Sub CloseButton_Click(sender As Object, e As RoutedEventArgs)
            topAppBar.IsOpen = False
        End Sub
        Private Sub NavBarButton_Click(sender As Object, e As RoutedEventArgs)
            Dim b As ButtonBase = CType(sender, ButtonBase)

            Dim navigationRootPage1 As NavigationRootPage = NavigationRootPage.GetForElement(Me)
            If navigationRootPage1 IsNot Nothing AndAlso b.Tag IsNot Nothing Then
                If b.Tag.ToString() = "Home" Then
                    navigationRootPage1.Navigate(GetType(AppUIBasics.AllControlsPage))
                Else
                    navigationRootPage1.Navigate(GetType(SectionPage), b.Tag.ToString())
                End If
            End If
        End Sub
        Private Sub AddButton_Click(sender As Object, e As RoutedEventArgs)
            Dim TempVar As Boolean = TypeOf AppBarContentPanel.Children(0) Is Button
            Dim homeButton As Button = AppBarContentPanel.Children(0)
            If TempVar AndAlso homeButton.Tag.ToString() <> "Home" Then
                homeButton = New Button With
    { _
                .Content = "Home",
                .Tag = "Home"
    }
                AddHandler homeButton.Click, AddressOf NavBarButton_Click

                AppBarContentPanel.Children.Insert(0, homeButton)
            End If
        End Sub
        Private Sub RemoveButton_Click(sender As Object, e As RoutedEventArgs)
            RemoveHomeButton()
        End Sub
        Protected Overrides Sub OnNavigatingFrom(e As NavigatingCancelEventArgs)
            RemoveHomeButton()
            MyBase.OnNavigatingFrom(e)
        End Sub
        Private Sub RemoveHomeButton()
            Dim TempVar1 As Boolean = TypeOf AppBarContentPanel.Children(0) Is Button
            Dim homeButton As Button = AppBarContentPanel.Children(0)
            If TempVar1 AndAlso homeButton.Tag.ToString() = "Home" Then
                RemoveHandler homeButton.Click, AddressOf NavBarButton_Click
                AppBarContentPanel.Children.RemoveAt(0)
            End If
        End Sub
    End Class
End Namespace
