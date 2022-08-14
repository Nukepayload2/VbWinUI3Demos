'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************


Imports AppUIBasics.Data
Imports System.Linq
Imports Microsoft.UI.Xaml.Navigation
Imports System.Collections.Generic

#If Not UNIVERSAL

Imports System.Collections.ObjectModel
#End If

Namespace AppUIBasics
    Public NotInheritable Partial Class NewControlsPage
        Inherits ItemsPageBase
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            Dim args As NavigationRootPageArgs = CType(e.Parameter, NavigationRootPageArgs)
            Dim menuItem = CType(args.NavigationRootPage.NavigationView.MenuItems.First(), Microsoft.UI.Xaml.Controls.NavigationViewItem)
            menuItem.IsSelected = True
            args.NavigationRootPage.NavigationView.Header = String.Empty

            Items = ControlInfoDataSource.Instance.Groups.SelectMany(Function(g) g.Items.Where(Function(i) i.BadgeString IsNot Nothing)).OrderBy(Function(i) i.Title).ToList()
            itemsCVS.Source = FormatData()
        End Sub
        Private Function FormatData() As ObservableCollection(Of GroupInfoList)
            Dim query = From item In Me.Items
                        Group item By item.BadgeString Into g = Group
                        Select New GroupInfoList(g) With {.Key = BadgeString}

            Dim groupList As New ObservableCollection(Of GroupInfoList)(query)

            'Move Preview samples to the back of the list
            Dim previewGroup As AppUIBasics.GroupInfoList = groupList.ElementAt(1)
            If previewGroup?.Key.ToString() = "Preview" Then
                groupList.RemoveAt(1)
                groupList.Insert(groupList.Count, previewGroup)
            End If

            For Each item As AppUIBasics.GroupInfoList In groupList
                Select Case item.Key.ToString()
                    Case "New"
                        item.Title = "Recently Added Samples"
                    Case "Updated"
                        item.Title = "Recently Updated Samples"
                    Case "Preview"
                        item.Title = "Preview Samples"
                End Select
            Next

            Return groupList
        End Function
        Protected Overrides Function GetIsNarrowLayoutState() As Boolean
            Return LayoutVisualStates.CurrentState = NarrowLayout
        End Function
    End Class


    Public Class GroupInfoList
        Inherits List(Of Object)
        Public Sub New(items As IEnumerable(Of Object))
            MyBase.New(items)
        End Sub

        Public Property Key As Object

        Public Property Title As String
        Public Overrides Function ToString() As String
            Return Title
        End Function
    End Class
End Namespace
