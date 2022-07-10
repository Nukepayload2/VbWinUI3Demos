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

Imports AppUIBasics.Data
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Storage
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Navigation
Imports AppUIBasics.Common

#If Not UNIVERSAL

Imports System.Collections.ObjectModel
#End If

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class ListViewPage
        Inherits ItemsPageBase
        Private contacts1 As New ObservableCollection(Of Contact)
        Private contacts2 As New ObservableCollection(Of Contact)
        Private contacts3 As New ObservableCollection(Of Contact)
        Private contacts3Filtered As New ObservableCollection(Of Contact)
        Private stackPanelObj As ItemsStackPanel
        Private messageNumber As Integer

        Public Sub New()
            Me.InitializeComponent()
            ' Add first item to inverted list so it's not empty
            AddItemToEnd()
            AddHandler BaseExample.Loaded, AddressOf BaseExample_Loaded
        End Sub
        Private Sub BaseExample_Loaded(sender As Object, e As RoutedEventArgs)
            ' Set focus so the first item of the listview has focus
            ' instead of some item which is not visible on page load
            BaseExample.Focus(FocusState.Programmatic)
        End Sub
        Protected Overrides Async Sub OnNavigatedTo(e As NavigationEventArgs)
            Items = ControlInfoDataSource.Instance.Groups.Take(3).SelectMany(Function(g) g.Items).ToList()
            BaseExample.ItemsSource = Await Contact.GetContactsAsync()
            Control2.ItemsSource = Await Contact.GetContactsAsync()
            contacts1 = Await Contact.GetContactsAsync()

            DragDropListView.ItemsSource = contacts1

            contacts2.Add(New Contact("John", "Doe", "ABC Printers"))
            contacts2.Add(New Contact("Jane", "Doe", "XYZ Refrigerators"))
            contacts2.Add(New Contact("Santa", "Claus", "North Pole Toy Factory Inc."))
            DragDropListView2.ItemsSource = contacts2

            Control4.ItemsSource = AppUIBasics.ControlPages.CustomDataObject.GetDataObjects()
            ContactsCVS.Source = Await Contact.GetContactsGroupedAsync()

            ' Initialize list of contacts to be filtered
            contacts3 = Await Contact.GetContactsAsync()
            contacts3Filtered = New ObservableCollection(Of Contact)(contacts3)

            FilteredListView.ItemsSource = contacts3Filtered
        End Sub
        '===================================================================================================================
        ' Selection Modes Example
        '===================================================================================================================
        Private Sub SelectionModeComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            If Control2 IsNot Nothing Then
                Dim selectionMode As String = e.AddedItems(0).ToString()
                Select Case selectionMode
                    Case "None"
                        Control2.SelectionMode = ListViewSelectionMode.None
                    Case "Single"
                        Control2.SelectionMode = ListViewSelectionMode.[Single]
                    Case "Multiple"
                        Control2.SelectionMode = ListViewSelectionMode.Multiple
                    Case "Extended"
                        Control2.SelectionMode = ListViewSelectionMode.Extended
                End Select
            End If
        End Sub
        '===================================================================================================================
        ' Drag/Drop Example
        '===================================================================================================================
        Private Sub Source_DragItemsStarting(sender As Object, e As DragItemsStartingEventArgs)
            ' Prepare a string with one dragged item per line
            Dim items1 As New StringBuilder
            For Each item As Contact In e.Items
                If items1.Length > 0 Then
                    items1.AppendLine()
                End If
                If item.ToString() IsNot Nothing Then
                    ' Append name from contact object onto data string
                    items1.Append(item.ToString() & " " & item.Company)
                End If
            Next
            ' Set the content of the DataPackage
            e.Data.SetText(items1.ToString())

            e.Data.RequestedOperation = DataPackageOperation.Move
        End Sub
        Private Sub Target_DragOver(sender As Object, e As DragEventArgs)
            e.AcceptedOperation = DataPackageOperation.Move
        End Sub
        Private Sub Source_DragOver(sender As Object, e As DragEventArgs)
            e.AcceptedOperation = DataPackageOperation.Move
        End Sub
        Private Async Sub ListView_Drop(sender As Object, e As DragEventArgs)
            Dim target As ListView = CType(sender, ListView)

            If e.DataView.Contains(StandardDataFormats.Text) Then
                Dim def As DragOperationDeferral = e.GetDeferral()
                Dim s As String = Await e.DataView.GetTextAsync()
                Dim items1 As String() = s.Split(vbLf)
                For Each item As String In items1
                    ' Create Contact object from string, add to existing target ListView
                    Dim info As String() = item.Split(" ", 3)
                    Dim temp As New Contact(info(0), info(1), info(2))

                    ' Find the insertion index:
                    Dim pos As Windows.Foundation.Point = e.GetPosition(target.ItemsPanelRoot)

                    ' If the target ListView has items in it, use the heigh of the first item
                    '      to find the insertion index.
                    Dim index As Integer = 0
                    If target.Items.Count <> 0 Then
                        ' Get a reference to the first item in the ListView
                        Dim sampleItem As ListViewItem = CType(target.ContainerFromIndex(0), ListViewItem)

                        ' Adjust itemHeight for margins
                        Dim itemHeight As Double = sampleItem.ActualHeight + sampleItem.Margin.Top + sampleItem.Margin.Bottom

                        ' Find index based on dividing number of items by height of each item
                        index = Math.Min(target.Items.Count - 1, CInt(CLng(Fix((pos.Y / itemHeight))) Mod Integer.MaxValue))

                        ' Find the item being dropped on top of.
                        Dim targetItem As ListViewItem = CType(target.ContainerFromIndex(index), ListViewItem)

                        ' If the drop position is more than half-way down the item being dropped on
                        '      top of, increment the insertion index so the dropped item is inserted
                        '      below instead of above the item being dropped on top of.
                        Dim positionInItem As Windows.Foundation.Point = e.GetPosition(targetItem)
                        If positionInItem.Y > itemHeight / 2 Then
                            index += 1
                        End If

                        ' Don't go out of bounds
                        index = Math.Min(target.Items.Count, index)
                    End If
                    ' Only other case is if the target ListView has no items (the dropped item will be
                    '      the first). In that case, the insertion index will remain zero.

                    ' Find correct source list
                    If target.Name = "DragDropListView" Then
                        ' Find the ItemsSource for the target ListView and insert
                        contacts1.Insert(index, temp)
                        'Go through source list and remove the items that are being moved
                        For Each contact1 As Contact In DragDropListView2.Items
                            If contact1.FirstName = temp.FirstName AndAlso contact1.LastName = temp.LastName AndAlso contact1.Company = temp.Company Then
                                contacts2.Remove(contact1)
                                Exit For
                            End If
                        Next
                    ElseIf target.Name = "DragDropListView2" Then
                        contacts2.Insert(index, temp)
                        For Each contact1 As Contact In DragDropListView.Items
                            If contact1.FirstName = temp.FirstName AndAlso contact1.LastName = temp.LastName AndAlso contact1.Company = temp.Company Then
                                contacts1.Remove(contact1)
                                Exit For
                            End If
                        Next
                    End If
                Next

                e.AcceptedOperation = DataPackageOperation.Move
                def.Complete()
            End If
        End Sub
        Private Sub Target_DragItemsStarting(sender As Object, e As DragItemsStartingEventArgs)
            If e.Items.Count = 1 Then
                ' Prepare ListViewItem to be moved
                Dim tmp As Contact = CType(e.Items(0), Contact)

                e.Data.SetText(tmp.FirstName & " " & tmp.LastName & " " & tmp.Company)
                e.Data.RequestedOperation = DataPackageOperation.Move
            End If
        End Sub
        Private Sub Target_DragEnter(sender As Object, e As DragEventArgs)
            ' We don't want to show the Move icon
            e.DragUIOverride.IsGlyphVisible = False
        End Sub
        '===================================================================================================================
        ' Grouped Headers Example
        '===================================================================================================================
        Private Sub ToggleSwitch_Toggled(sender As Object, e As RoutedEventArgs)
            If StickySwitch IsNot Nothing Then
                If StickySwitch.IsOn = True Then
                    stackPanelObj.AreStickyGroupHeadersEnabled = True
                Else
                    stackPanelObj.AreStickyGroupHeadersEnabled = False
                End If
            End If
        End Sub
        Private Sub StackPanel_loaded(sender As Object, e As RoutedEventArgs)
            stackPanelObj = TryCast(sender, ItemsStackPanel)
        End Sub
        '===================================================================================================================
        ' Filtered List Example
        '===================================================================================================================
        Private Sub Remove_NonMatching(filteredData As IEnumerable(Of Contact))
            For i As Integer = contacts3Filtered.Count - 1 To 0 Step -1
                Dim item As AppUIBasics.ControlPages.Contact = contacts3Filtered(i)
                ' If contact is not in the filtered argument list, remove it from the ListView's source.
                If Not filteredData.Contains(item) Then
                    contacts3Filtered.Remove(item)
                End If
            Next
        End Sub
        Private Sub AddBack_Contacts(filteredData As IEnumerable(Of Contact))
            ' When a user hits backspace, more contacts may need to be added back into the list
            For Each item As AppUIBasics.ControlPages.Contact In filteredData
                ' If item in filtered list is not currently in ListView's source collection, add it back in
                If Not contacts3Filtered.Contains(item) Then
                    contacts3Filtered.Add(item)
                End If
            Next
        End Sub
        Private Sub OnFilterChanged(sender As Object, args As TextChangedEventArgs)
            ' Linq query that selects only items that return True after being passed through Filter function
            Dim filtered As Collections.Generic.IEnumerable(Of AppUIBasics.ControlPages.Contact) = contacts3.Where(Function(contact1) Filter(contact1))
            Remove_NonMatching(filtered)
            AddBack_Contacts(filtered)
        End Sub
        Private Function Filter(contact1 As Contact) As Boolean
            ' When the text in any filter is changed, contact list is ran through all three filters to make sure
            ' they can properly interact with each other (i.e. they can all be applied at the same time).

            Return contact1.FirstName.Contains(FilterByFirstName.Text, StringComparison.InvariantCultureIgnoreCase) AndAlso _
                   contact1.LastName.Contains(FilterByLastName.Text, StringComparison.InvariantCultureIgnoreCase) AndAlso _
                   contact1.Company.Contains(FilterByCompany.Text, StringComparison.InvariantCultureIgnoreCase)
        End Function
        '===================================================================================================================
        ' Inverted List Example
        '===================================================================================================================
        Private Sub AddItemToEnd()
            InvertedListView.Items.Add( _
                New Message("Message " & Threading.Interlocked.Increment(messageNumber), DateTime.Now, HorizontalAlignment.Right) _
                )
        End Sub
        Private Sub MessageReceived(sender As Object, e As RoutedEventArgs)
            InvertedListView.Items.Add( _
                New Message("Message " & Threading.Interlocked.Increment(messageNumber), DateTime.Now, HorizontalAlignment.Left) _
                )
        End Sub
    End Class


    Public Class Message
        Private _msgText As String
        Public Property MsgText As String
            Get
                Return _msgText
            End Get
            Private Set(value As String)
                _msgText = value
            End Set
        End Property
        Private _msgDateTime As DateTime
        Public Property MsgDateTime As DateTime
            Get
                Return _msgDateTime
            End Get
            Private Set(value As DateTime)
                _msgDateTime = value
            End Set
        End Property
        Public Property MsgAlignment As HorizontalAlignment
        Public Sub New(text As String, dateTime1 As DateTime, align As HorizontalAlignment)
            MsgText = text
            MsgDateTime = dateTime1
            MsgAlignment = align
        End Sub
        Public Overrides Function ToString() As String
            Return MsgDateTime.ToString() & " " & MsgText
        End Function
    End Class


    Public Class Contact
#Region "Properties"

        Private _firstName As String
        Public Property FirstName As String
            Get
                Return _firstName
            End Get
            Private Set(value As String)
                _firstName = value
            End Set
        End Property
        Private _lastName As String
        Public Property LastName As String
            Get
                Return _lastName
            End Get
            Private Set(value As String)
                _lastName = value
            End Set
        End Property
        Private _company As String
        Public Property Company As String
            Get
                Return _company
            End Get
            Private Set(value As String)
                _company = value
            End Set
        End Property
        Public ReadOnly Property Name As String
            Get
                Return FirstName & " " & LastName
            End Get
        End Property
#End Region

        Public Sub New(firstName1 As String, lastName1 As String, company1 As String)
            Me.FirstName = firstName1
            Me.LastName = lastName1
            Me.Company = company1
        End Sub
#Region "Public Methods"

        Public Async Shared Function GetContactsAsync() As Task(Of ObservableCollection(Of Contact))
            Dim lines As IList(Of String) = Await FileLoader.LoadLines("Assets/Contacts.txt")

            Dim contacts As New ObservableCollection(Of Contact)

            For i As Integer = 0 To lines.Count - 2 - 1 Step 3
                contacts.Add(New Contact(lines(i), lines(i + 1), lines(i + 2)))
            Next

            Return contacts
        End Function
        Public Shared Async Function GetContactsGroupedAsync() As Task(Of ObservableCollection(Of GroupInfoList))
            Dim query As Collections.Generic.IEnumerable(Of AppUIBasics.ControlPages.GroupInfoList) = From item In Await GetContactsAsync()
                                                                                                      Group item By __groupByKey0__ = item.LastName.Substring(0, 1).ToUpper() Into g Select New GroupInfoList(g) With {.Key = g.Key}

            Return New ObservableCollection(Of GroupInfoList)(query)
        End Function
        Public Overrides Function ToString() As String
            Return Name
        End Function
#End Region

    End Class


    Public Class GroupInfoList
        Inherits List(Of Object)
        Public Sub New(items As IEnumerable(Of Object))
            MyBase.New(items)
        End Sub
        Public Property Key As Object
    End Class
End Namespace
