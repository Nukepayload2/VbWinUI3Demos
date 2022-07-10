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

Imports AppUIBasics.Common
Imports AppUIBasics.Data
Imports AppUIBasics.Helper
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Navigation

#If Not UNIVERSAL

Imports System.ComponentModel
#Else

using Microsoft.UI.Xaml.Data;

#End If

Namespace AppUIBasics
    ''' <summary>
    ''' This page displays search results when a global search is directed to this application.
    ''' </summary>
    Public NotInheritable Partial Class SearchResultsPage
        Inherits ItemsPageBase
        Private _filters As IEnumerable(Of Filter)
        Private _selectedFilter As Filter
        Private _queryText As String
        Public Property Filters As IEnumerable(Of Filter)
            Get
                Return _filters
            End Get

            Set(value As IEnumerable(Of Filter))
                Me.SetProperty(_filters, value)
            End Set
        End Property

        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            MyBase.OnNavigatedTo(e)

            Dim args As NavigationRootPageArgs = CType(e.Parameter, NavigationRootPageArgs)
            Dim queryText = args.Parameter?.ToString().ToLower()

            BuildFilterList(queryText)

            args.NavigationRootPage.NavigationView.Header = "Search"
        End Sub
        Protected Overrides Sub OnNavigatingFrom(e As NavigatingCancelEventArgs)
            MyBase.OnNavigatingFrom(e)

            _selectedFilter = CType(resultsNavView.SelectedItem, Filter)
        End Sub
        Private Sub OnResultsNavViewLoaded(sender As Object, e As RoutedEventArgs)
            resultsNavView.Focus(FocusState.Programmatic)
        End Sub
        Private Sub OnResultsNavViewSelectionChanged(sender As Object, e As NavigationViewSelectionChangedEventArgs)
            If e.SelectedItem IsNot Nothing Then
                _selectedFilter = CType(e.SelectedItem, Filter)
            End If
        End Sub
        Private Sub BuildFilterList(queryText As String)
            If Not String.IsNullOrEmpty(queryText) Then
                ' Application-specific searching logic.  The search process is responsible for
                ' creating a list of user-selectable result categories:
                Dim filterList As Collections.Generic.List(Of AppUIBasics.Filter) = New List(Of Filter)

                ' Query is already lowercase
                Dim querySplit As String() = queryText.ToLower().Split(" ")
                For Each group In ControlInfoDataSource.Instance.Groups
                    Dim matchingItems = group.Items.Where(Function(item) As Boolean
                                                              ' Idea: check for every word entered (separated by space) if it is in the name,
                                                              ' e.g. for query "split button" the only result should "SplitButton" since its the only query to contain "split" and "button"
                                                              ' If any of the sub tokens is not in the string, we ignore the item. So the search gets more precise with more words
                                                              Dim flag As Boolean = True
                                                              For Each queryToken As String In querySplit
                                                                  ' Check if token is in title or subtitle
                                                                  If Not item.Title.ToLower().Contains(queryToken) AndAlso Not item.Subtitle.ToLower().Contains(queryToken) Then
                                                                      ' Neither title nor sub title contain one of the tokens so we discard this item!
                                                                      flag = False
                                                                  End If
                                                              Next
                                                              Return flag
                                                          End Function).ToList()
                    Dim numberOfMatchingItems As Integer = matchingItems.Count()

                    If numberOfMatchingItems > 0 Then
                        filterList.Add(New Filter(group.Title, numberOfMatchingItems, matchingItems))
                    End If
                Next

                If filterList.Count = 0 Then
                    ' Display informational text when there are no search results.
                    VisualStateManager.GoToState(Me, "NoResultsFound", False)
                    Dim textbox = NavigationRootPage.GetForElement(Me)?.PageHeader?.GetDescendantsOfType(Of AutoSuggestBox)().FirstOrDefault()
                    textbox?.Focus(FocusState.Programmatic)
                Else
                    ' When there are search results, set Filters
                    Dim allControls As Collections.Generic.List(Of ControlInfoDataItem) = filterList.SelectMany(Function(s) s.Items).ToList()
                    filterList.Insert(0, New Filter("All", allControls.Count, allControls, True))
                    Filters = filterList

                    ' Check to see if the current query matches the last
                    If _queryText = queryText AndAlso _selectedFilter IsNot Nothing Then
                        ' If so try to restore any previously selected nav view item
                        resultsNavView.SelectedItem = Filters.Where(Function(f) f.Name = _selectedFilter.Name).SingleOrDefault()
                    Else
                        ' Otherwise reset query text and nav view filter
                        _queryText = queryText
                        resultsNavView.SelectedItem = Filters.FirstOrDefault()
                    End If

                    VisualStateManager.GoToState(Me, "ResultsFound", False)
                End If
            End If
        End Sub
        Protected Overrides Function GetIsNarrowLayoutState() As Boolean
            Return LayoutVisualStates.CurrentState = NarrowLayout
        End Function
    End Class

    ''' <summary>
    ''' View model describing one of the filters available for viewing search results.
    ''' </summary>
    Public NotInheritable Class Filter
        Implements INotifyPropertyChanged
        Private _name As String
        Private _count As Integer
        Private _active As Boolean?
        Private _items As List(Of ControlInfoDataItem)

        Public Sub New(name As String, count As Integer, controlInfoList As List(Of ControlInfoDataItem), Optional active As Boolean = False)
            Me.Name = name
            Me.Count = count
            Me.Active = active
            Me.Items = controlInfoList
        End Sub
        Public Overrides Function ToString() As String
            Return Description
        End Function
        Public Property Items As List(Of ControlInfoDataItem)
            Get
                Return _items
            End Get

            Set(value As List(Of ControlInfoDataItem))
                Me.SetProperty(_items, value)
            End Set
        End Property
        Public Property Name As String
            Get
                Return _name
            End Get

            Set(value As String)
                If Me.SetProperty(_name, value) Then
                    Me.NotifyPropertyChanged(NameOf(Description))
                End If
            End Set
        End Property
        Public Property Count As Integer
            Get
                Return _count
            End Get

            Set(value As Integer)
                If Me.SetProperty(_count, value) Then
                    Me.NotifyPropertyChanged(NameOf(Description))
                End If
            End Set
        End Property
        Public Property Active As Boolean?
            Get
                Return _active
            End Get

            Set(value As Boolean?)
                Me.SetProperty(_active, value)
            End Set
        End Property
        Public ReadOnly Property Description As String
            Get
                Return String.Format("{0} ({1})", _name, _count)
            End Get
        End Property

        ''' <summary>
        ''' Multicast event for property change notifications.
        ''' </summary>
        Public Event PropertyChanged As PropertyChangedEventHandler Implements ComponentModel.INotifyPropertyChanged.PropertyChanged
        ''' <summary>
        ''' Checks if a property already matches a desired value.  Sets the property and
        ''' notifies listeners only when necessary.
        ''' </summary>
        ''' <typeparam name="T">Type of the property.</typeparam>
        ''' <param name="storage">Reference to a property with both getter and setter.</param>
        ''' <param name="value">Desired value for the property.</param>
        ''' <param name="propertyName">Name of the property used to notify listeners.  This
        ''' value is optional and can be provided automatically when invoked from compilers that
        ''' support CallerMemberName.</param>
        ''' <returns>True if the value was changed, false if the existing value matched the
        ''' desired value.</returns>
        Private Function SetProperty(Of T)(ByRef storage As T, value As T, <CallerMemberName> Optional propertyName As String = Nothing) As Boolean
            If Object.Equals(storage, value) Then
                Return False
            End If

            storage = value
            Me.NotifyPropertyChanged(propertyName)
            Return True
        End Function
        ''' <summary>
        ''' Notifies listeners that a property value has changed.
        ''' </summary>
        ''' <param name="propertyName">Name of the property used to notify listeners.  This
        ''' value is optional and can be provided automatically when invoked from compilers
        ''' that support <see cref="CallerMemberNameAttribute"/>.</param>
        Private Sub NotifyPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
            Me.PropertyChanged?.Invoke(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class
End Namespace
