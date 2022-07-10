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
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class AppBarSeparatorPage
        Inherits Page
        Private compactButton As AppBarToggleButton = Nothing
        Private separator As AppBarSeparator = Nothing

        Public Sub New()
            Me.InitializeComponent()
            Loaded += AddressOf AppBarButtonPage_Loaded
            Unloaded += AddressOf AppBarSeparatorPage_Unloaded
        End Sub
        Private Sub AppBarSeparatorPage_Unloaded(sender As Object, e As RoutedEventArgs)
            Dim appBar As CommandBar = NavigationRootPage.GetForElement(Me).PageHeader.TopCommandBar
            RemoveHandler compactButton.Click, AddressOf CompactButton_Click
            appBar.PrimaryCommands.Remove(compactButton)
            appBar.PrimaryCommands.Remove(separator)
        End Sub
        Private Sub AppBarButtonPage_Loaded(sender As Object, e As RoutedEventArgs)
            ' Add compact button to the command bar. It provides functionality specific
            ' to this page, and is removed when leaving the page.

            Dim appBar As CommandBar = NavigationRootPage.GetForElement(Me).PageHeader.TopCommandBar
            separator = New AppBarSeparator
            appBar.PrimaryCommands.Insert(0, separator)

            compactButton = New AppBarToggleButton With
            { _
            .Icon = New SymbolIcon(Symbol.FontSize),
            .Label = "IsCompact"
            }
            AddHandler compactButton.Click, AddressOf CompactButton_Click
            appBar.PrimaryCommands.Insert(0, compactButton)
        End Sub
        Private Sub CompactButton_Click(sender As Object, e As RoutedEventArgs)
            If TryCast(sender, AppBarToggleButton).IsChecked = True Then
                Control1.DefaultLabelPosition = CommandBarDefaultLabelPosition.Collapsed
            Else
                Control1.DefaultLabelPosition = CommandBarDefaultLabelPosition.Bottom
            End If
        End Sub
    End Class
End Namespace
