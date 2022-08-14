'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************


Imports Microsoft.UI
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media

#If Not UNIVERSAL

Imports System.Collections.ObjectModel
#End If

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class SwipeControlPage
        Inherits Page
        Private isArchived As Boolean = False
        Private isFlagged As Boolean = False
        Private isAccepted As Boolean = False

        Public Sub New()
            Me.InitializeComponent()
            Dim source As String() = "Swipe Item 1,Swipe Item 2,Swipe Item 3,Swipe Item 4".Split(","c)
            For Each item As String In source
                items.Add(item)
            Next
            lv.ItemsSource = items
        End Sub
        Private items As New ObservableCollection(Of Object)
        Private Sub DeleteOne_ItemInvoked(sender As SwipeItem, args As SwipeItemInvokedEventArgs)
            isArchived = Not isArchived

            If isArchived Then
                CType(args.SwipeControl.Content, TextBlock).Text = "Archived - Swipe Left"
            Else
                CType(args.SwipeControl.Content, TextBlock).Text = "Swipe Left"
            End If
        End Sub
        Private Sub DeleteItem_ItemInvoked(sender As SwipeItem, args As SwipeItemInvokedEventArgs)
            items.Remove(args.SwipeControl.DataContext)
        End Sub
        Private Sub Accept_ItemInvoked(sender As SwipeItem, args As SwipeItemInvokedEventArgs)
            isAccepted = Not isAccepted
            CheckAcceptFlagBool(args.SwipeControl)

            If isAccepted Then
                Dim cancelIcon As New FontIconSource() With
{
                    .Glyph = ""}
                sender.IconSource = cancelIcon
                sender.Text = "Cancel"
            Else
                Dim acceptIcon As New FontIconSource() With
{
                    .Glyph = ""}
                sender.IconSource = acceptIcon
                sender.Text = "Accept"
            End If
        End Sub
        Private Sub Flag_ItemInvoked(sender As SwipeItem, args As SwipeItemInvokedEventArgs)
            isFlagged = Not isFlagged
            CheckAcceptFlagBool(args.SwipeControl)

            If isFlagged Then
                Dim filledFlagIcon As New FontIconSource() With
{
                    .Glyph = ""}
                sender.IconSource = filledFlagIcon
                sender.Text = "Unmark"
            Else
                Dim flagIcon As New FontIconSource() With
{
                    .Glyph = ""}
                sender.IconSource = flagIcon
                sender.Text = "Flag"
            End If
        End Sub
        Private Sub CheckAcceptFlagBool(swipeCtrl As SwipeControl)
            If isAccepted AndAlso Not isFlagged Then
                CType(swipeCtrl.Content, TextBlock).Text = "Swipe Right - Accepted"
            ElseIf isAccepted AndAlso isFlagged Then
                CType(swipeCtrl.Content, TextBlock).Text = "Swipe Right - Accepted & Flagged"
            ElseIf Not isAccepted AndAlso isFlagged Then
                CType(swipeCtrl.Content, TextBlock).Text = "Swipe Right - Flagged"
            Else
                CType(swipeCtrl.Content, TextBlock).Text = "Swipe Right"
            End If
        End Sub
    End Class
End Namespace
