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
    Public NotInheritable Partial Class AppBarToggleButtonPage
        Inherits Page
        Private compactButton As AppBarToggleButton = Nothing
        Private separator As AppBarSeparator = Nothing

        Public Sub New()
            Me.InitializeComponent()
            AddHandler Loaded, AddressOf AppBarButtonPage_Loaded
            AddHandler Unloaded, AddressOf AppBarToggleButtonPage_Unloaded
        End Sub
        Private Sub AppBarToggleButtonPage_Unloaded(sender As Object, e As RoutedEventArgs)
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
            Dim TempVar As Boolean = TypeOf sender Is ToggleButton
            Dim toggle As ToggleButton = sender
            If TempVar AndAlso toggle.IsChecked IsNot Nothing Then
                Button1.IsCompact = __InlineAssignHelper(Button2.IsCompact, __InlineAssignHelper(Button3.IsCompact, __InlineAssignHelper(Button4.IsCompact, CBool(toggle.IsChecked))))
            End If
        End Sub
        Private Sub AppBarButton_Click(sender As Object, e As RoutedEventArgs)
            Dim TempVar1 As Boolean = TypeOf sender Is AppBarToggleButton
            Dim b As AppBarToggleButton = sender
            If TempVar1 Then
                Dim name1 As String = b.Name

                Select Case name1
                    Case "Button1"
                        Control1Output.Text = "IsChecked = " & b.IsChecked.ToString()
                    Case "Button2"
                        Control2Output.Text = "IsChecked = " & b.IsChecked.ToString()
                    Case "Button3"
                        Control3Output.Text = "IsChecked = " & b.IsChecked.ToString()
                    Case "Button4"
                        Control4Output.Text = "IsChecked = " & b.IsChecked.ToString()
                End Select
            End If
        End Sub
        <Obsolete("Please refactor code that uses this function, it is a simple work-around to simulate inline assignment in VB!")>
        Private Shared Function __InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
End Namespace
