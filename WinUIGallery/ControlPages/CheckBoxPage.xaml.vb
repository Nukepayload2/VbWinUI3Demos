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
    Public NotInheritable Partial Class CheckBoxPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
            AddHandler Loaded, AddressOf CheckBoxPage_Loaded
        End Sub
        Private Sub CheckBoxPage_Loaded(sender As Object, e As RoutedEventArgs)
            SetCheckedState()
        End Sub
        Private Sub Control1_Checked(sender As Object, e As RoutedEventArgs)
            Control1Output.Text = "You checked the box."
        End Sub
        Private Sub Control1_Unchecked(sender As Object, e As RoutedEventArgs)
            Control1Output.Text = "You unchecked the box."
        End Sub
        Private Sub Control2_Checked(sender As Object, e As RoutedEventArgs)
            Control2Output.Text = "CheckBox is checked."
        End Sub
        Private Sub Control2_Unchecked(sender As Object, e As RoutedEventArgs)
            Control2Output.Text = "CheckBox is unchecked."
        End Sub
        Private Sub Control2_Indeterminate(sender As Object, e As RoutedEventArgs)
            Control2Output.Text = "CheckBox state is indeterminate."
        End Sub
#Region "SelectAllMethods"

        Private Sub SelectAll_Checked(sender As Object, e As RoutedEventArgs)
            Option1CheckBox.IsChecked = __InlineAssignHelper(Option2CheckBox.IsChecked, __InlineAssignHelper(Option3CheckBox.IsChecked, True))
        End Sub
        Private Sub SelectAll_Unchecked(sender As Object, e As RoutedEventArgs)
            Option1CheckBox.IsChecked = __InlineAssignHelper(Option2CheckBox.IsChecked, __InlineAssignHelper(Option3CheckBox.IsChecked, False))
        End Sub
        Private Sub SelectAll_Indeterminate(sender As Object, e As RoutedEventArgs)
            ' If the SelectAll box is checked (all options are selected),
            ' clicking the box will change it to its indeterminate state.
            ' Instead, we want to uncheck all the boxes,
            ' so we do this programatically. The indeterminate state should
            ' only be set programatically, not by the user.

            If Option1CheckBox.IsChecked = True AndAlso _
                Option2CheckBox.IsChecked = True AndAlso _
                Option3CheckBox.IsChecked = True Then
                ' This will cause SelectAll_Unchecked to be executed, so
                ' we don't need to uncheck the other boxes here.
                OptionsAllCheckBox.IsChecked = False
            End If
        End Sub
        Private Sub SetCheckedState()
            ' Controls are null the first time this is called, so we just
            ' need to perform a null check on any one of the controls.
            If Option1CheckBox IsNot Nothing Then
                If Option1CheckBox.IsChecked = True AndAlso _
        Option2CheckBox.IsChecked = True AndAlso _
        Option3CheckBox.IsChecked = True Then
                    OptionsAllCheckBox.IsChecked = True
                ElseIf Option1CheckBox.IsChecked = False AndAlso _
                    Option2CheckBox.IsChecked = False AndAlso _
                    Option3CheckBox.IsChecked = False Then
                    OptionsAllCheckBox.IsChecked = False
                Else
                    ' Set third state (indeterminate) by setting IsChecked to null.
                    OptionsAllCheckBox.IsChecked = Nothing
                End If
            End If
        End Sub
        Private Sub Option_Checked(sender As Object, e As RoutedEventArgs)
            SetCheckedState()
        End Sub
        Private Sub Option_Unchecked(sender As Object, e As RoutedEventArgs)
            SetCheckedState()
        End Sub
        <Obsolete("Please refactor code that uses this function, it is a simple work-around to simulate inline assignment in VB!")>
        Private Shared Function __InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
#End Region

    End Class
End Namespace
