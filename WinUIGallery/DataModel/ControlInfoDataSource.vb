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

Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Threading.Tasks
Imports Windows.Data.Json
Imports Windows.Storage
Imports Microsoft.UI.Xaml.Media
Imports Microsoft.UI.Xaml.Media.Imaging
Imports AppUIBasics.Common

' The data model defined by this file serves as a representative example of a strongly-typed
' model.  The property names chosen coincide with data bindings in the standard item templates.
'
' Applications may use this model as a starting point and build on it, or discard it entirely and
' replace it with something appropriate to their needs. If using this model, you might improve app
' responsiveness by initiating the data loading task in the code behind for App.xaml when the app
' is first launched.

Namespace AppUIBasics.Data
    ''' <summary>
    ''' Generic item data model.
    ''' </summary>
    Public Class ControlInfoDataItem
        Public Sub New(uniqueId As String, title As String, subtitle As String, imagePath As String, imageIconPath As String, badgeString As String, description As String, content As String, isNew As Boolean, isUpdated As Boolean, isPreview As Boolean)
            Me.UniqueId = uniqueId
            Me.Title = title
            Me.Subtitle = subtitle
            Me.Description = description
            Me.ImagePath = imagePath
            Me.ImageIconPath = imageIconPath
            Me.BadgeString = badgeString
            Me.Content = content
            Me.IsNew = isNew
            Me.IsUpdated = isUpdated
            Me.IsPreview = isPreview
            Me.Docs = New ObservableCollection(Of ControlInfoDocLink)
            Me.RelatedControls = New ObservableCollection(Of String)
        End Sub

        Private _uniqueId As String
        Public Property UniqueId As String
            Get
                Return _uniqueId
            End Get
            Private Set(value As String)
                _uniqueId = value
            End Set
        End Property
        Private _title As String
        Public Property Title As String
            Get
                Return _title
            End Get
            Private Set(value As String)
                _title = value
            End Set
        End Property
        Private _subtitle As String
        Public Property Subtitle As String
            Get
                Return _subtitle
            End Get
            Private Set(value As String)
                _subtitle = value
            End Set
        End Property
        Private _description As String
        Public Property Description As String
            Get
                Return _description
            End Get
            Private Set(value As String)
                _description = value
            End Set
        End Property
        Private _imagePath As String
        Public Property ImagePath As String
            Get
                Return _imagePath
            End Get
            Private Set(value As String)
                _imagePath = value
            End Set
        End Property
        Private _imageIconPath As String
        Public Property ImageIconPath As String
            Get
                Return _imageIconPath
            End Get
            Private Set(value As String)
                _imageIconPath = value
            End Set
        End Property
        Private _badgeString As String
        Public Property BadgeString As String
            Get
                Return _badgeString
            End Get
            Private Set(value As String)
                _badgeString = value
            End Set
        End Property
        Private _content As String
        Public Property Content As String
            Get
                Return _content
            End Get
            Private Set(value As String)
                _content = value
            End Set
        End Property
        Private _isNew As Boolean
        Public Property IsNew As Boolean
            Get
                Return _isNew
            End Get
            Private Set(value As Boolean)
                _isNew = value
            End Set
        End Property
        Private _isUpdated As Boolean
        Public Property IsUpdated As Boolean
            Get
                Return _isUpdated
            End Get
            Private Set(value As Boolean)
                _isUpdated = value
            End Set
        End Property
        Private _isPreview As Boolean
        Public Property IsPreview As Boolean
            Get
                Return _isPreview
            End Get
            Private Set(value As Boolean)
                _isPreview = value
            End Set
        End Property
        Private _docs As ObservableCollection(Of ControlInfoDocLink)
        Public Property Docs As ObservableCollection(Of ControlInfoDocLink)
            Get
                Return _docs
            End Get
            Private Set(value As ObservableCollection(Of ControlInfoDocLink))
                _docs = value
            End Set
        End Property
        Private _relatedControls As ObservableCollection(Of String)
        Public Property RelatedControls As ObservableCollection(Of String)
            Get
                Return _relatedControls
            End Get
            Private Set(value As ObservableCollection(Of String))
                _relatedControls = value
            End Set
        End Property

        Public Property IncludedInBuild As Boolean
        Public Overrides Function ToString() As String
            Return Me.Title
        End Function
    End Class


    Public Class ControlInfoDocLink
        Public Sub New(title As String, uri As String)
            Me.Title = title
            Me.Uri = uri
        End Sub
        Private _title As String
        Public Property Title As String
            Get
                Return _title
            End Get
            Private Set(value As String)
                _title = value
            End Set
        End Property
        Private _uri As String
        Public Property Uri As String
            Get
                Return _uri
            End Get
            Private Set(value As String)
                _uri = value
            End Set
        End Property
    End Class


    ''' <summary>
    ''' Generic group data model.
    ''' </summary>
    Public Class ControlInfoDataGroup
        Public Sub New(uniqueId As String, title As String, subtitle As String, imagePath As String, imageIconPath As String, description As String)
            Me.UniqueId = uniqueId
            Me.Title = title
            Me.Subtitle = subtitle
            Me.Description = description
            Me.ImagePath = imagePath
            Me.ImageIconPath = imageIconPath
            Me.Items = New ObservableCollection(Of ControlInfoDataItem)
        End Sub

        Private _uniqueId As String
        Public Property UniqueId As String
            Get
                Return _uniqueId
            End Get
            Private Set(value As String)
                _uniqueId = value
            End Set
        End Property
        Private _title As String
        Public Property Title As String
            Get
                Return _title
            End Get
            Private Set(value As String)
                _title = value
            End Set
        End Property
        Private _subtitle As String
        Public Property Subtitle As String
            Get
                Return _subtitle
            End Get
            Private Set(value As String)
                _subtitle = value
            End Set
        End Property
        Private _description As String
        Public Property Description As String
            Get
                Return _description
            End Get
            Private Set(value As String)
                _description = value
            End Set
        End Property
        Private _imagePath As String
        Public Property ImagePath As String
            Get
                Return _imagePath
            End Get
            Private Set(value As String)
                _imagePath = value
            End Set
        End Property
        Private _imageIconPath As String
        Public Property ImageIconPath As String
            Get
                Return _imageIconPath
            End Get
            Private Set(value As String)
                _imageIconPath = value
            End Set
        End Property
        Private _items As ObservableCollection(Of ControlInfoDataItem)
        Public Property Items As ObservableCollection(Of ControlInfoDataItem)
            Get
                Return _items
            End Get
            Private Set(value As ObservableCollection(Of ControlInfoDataItem))
                _items = value
            End Set
        End Property
        Public Overrides Function ToString() As String
            Return Me.Title
        End Function
    End Class

    ''' <summary>
    ''' Creates a collection of groups and items with content read from a static json file.
    '''
    ''' ControlInfoSource initializes with data read from a static json file included in the
    ''' project.  This provides sample data at both design-time and run-time.
    ''' </summary>
    Public NotInheritable Class ControlInfoDataSource
        Private Shared ReadOnly _lock As New Object
#Region "Singleton"

        Private Shared _instance As ControlInfoDataSource
        Public Shared ReadOnly Property Instance As ControlInfoDataSource
            Get
                Return _instance
            End Get
        End Property

        Shared Sub New()
            _instance = New ControlInfoDataSource
        End Sub

        Private Sub New()
        End Sub
#End Region

        Private _groups As IList(Of ControlInfoDataGroup) = New List(Of ControlInfoDataGroup)
        Public ReadOnly Property Groups As IList(Of ControlInfoDataGroup)
            Get
                Return Me._groups
            End Get
        End Property
        Public Async Function GetGroupsAsync() As Task(Of IEnumerable(Of ControlInfoDataGroup))
            Await _instance.GetControlInfoDataAsync()

            Return _instance.Groups
        End Function
        Public Async Function GetGroupAsync(uniqueId As String) As Task(Of ControlInfoDataGroup)
            Await _instance.GetControlInfoDataAsync()
            ' Simple linear search is acceptable for small data sets
            Dim matches As Collections.Generic.IEnumerable(Of AppUIBasics.Data.ControlInfoDataGroup) = _instance.Groups.Where(Function(group) group.UniqueId.Equals(uniqueId))
            If matches.Count() = 1 Then
                Return matches.First()
            End If
            Return Nothing
        End Function
        Public Async Function GetItemAsync(uniqueId As String) As Task(Of ControlInfoDataItem)
            Await _instance.GetControlInfoDataAsync()
            ' Simple linear search is acceptable for small data sets
            Dim matches As Collections.Generic.IEnumerable(Of AppUIBasics.Data.ControlInfoDataItem) = _instance.Groups.SelectMany(Function(group) group.Items).Where(Function(item) item.UniqueId.Equals(uniqueId))
            If matches.Count() > 0 Then
                Return matches.First()
            End If
            Return Nothing
        End Function
        Public Async Function GetGroupFromItemAsync(uniqueId As String) As Task(Of ControlInfoDataGroup)
            Await _instance.GetControlInfoDataAsync()
            Dim matches As Collections.Generic.IEnumerable(Of AppUIBasics.Data.ControlInfoDataGroup) = _instance.Groups.Where(Function(group) group.Items.FirstOrDefault(Function(item) item.UniqueId.Equals(uniqueId)) IsNot Nothing)
            If matches.Count() = 1 Then
                Return matches.First()
            End If
            Return Nothing
        End Function
        Private Async Function GetControlInfoDataAsync() As Task
            SyncLock _lock
                If Me.Groups.Count() <> 0 Then
                    Return
                End If
            End SyncLock

            Dim jsonText As String = Await FileLoader.LoadText("DataModel/ControlInfoData.json")

            Dim jsonObject1 As JsonObject = JsonObject.Parse(jsonText)
            Dim jsonArray1 As JsonArray = jsonObject1("Groups").GetArray()

            SyncLock _lock
                Dim pageRoot As String = "AppUIBasics.ControlPages."
                For Each groupValue As JsonValue In jsonArray1
                    Dim groupObject As JsonObject = groupValue.GetObject()

                    Dim group As New ControlInfoDataGroup(groupObject("UniqueId").GetString(),
                                                                          groupObject("Title").GetString(),
                                                                          groupObject("Subtitle").GetString(),
                                                                          groupObject("ImagePath").GetString(),
                                                                          groupObject("ImageIconPath").GetString(),
                                                                          groupObject("Description").GetString())

                    For Each itemValue As JsonValue In groupObject("Items").GetArray()
                        Dim itemObject As JsonObject = itemValue.GetObject()

                        Dim badgeString As String = Nothing

                        Dim isNew As Boolean = If(itemObject.ContainsKey("IsNew"), itemObject("IsNew").GetBoolean(), False)
                        Dim isUpdated As Boolean = If(itemObject.ContainsKey("IsUpdated"), itemObject("IsUpdated").GetBoolean(), False)
                        Dim isPreview As Boolean = If(itemObject.ContainsKey("IsPreview"), itemObject("IsPreview").GetBoolean(), False)

                        If isNew Then
                            badgeString = "New"
                        ElseIf isUpdated Then
                            badgeString = "Updated"
                        ElseIf isPreview Then
                            badgeString = "Preview"
                        End If

                        Dim item As AppUIBasics.Data.ControlInfoDataItem = New ControlInfoDataItem(itemObject("UniqueId").GetString(),
                                                                itemObject("Title").GetString(),
                                                                itemObject("Subtitle").GetString(),
                                                                itemObject("ImagePath").GetString(),
                                                                itemObject("ImageIconPath").GetString(),
                                                                badgeString,
                                                                itemObject("Description").GetString(),
                                                                itemObject("Content").GetString(),
                                                                isNew,
                                                                isUpdated,
                                                                isPreview)

                        If True Then
                            Dim pageString As String = pageRoot & item.UniqueId & "Page"
                            Dim pageType As Type = Type.[GetType](pageString)
                            item.IncludedInBuild = pageType IsNot Nothing
                        End If

                        If itemObject.ContainsKey("Docs") Then
                            For Each docValue As JsonValue In itemObject("Docs").GetArray()
                                Dim docObject As JsonObject = docValue.GetObject()
                                item.Docs.Add(New ControlInfoDocLink(docObject("Title").GetString(), docObject("Uri").GetString()))
                            Next
                        End If

                        If itemObject.ContainsKey("RelatedControls") Then
                            For Each relatedControlValue As JsonValue In itemObject("RelatedControls").GetArray()
                                item.RelatedControls.Add(relatedControlValue.GetString())
                            Next
                        End If

                        group.Items.Add(item)
                    Next
                    If Not Groups.Any(Function(g) g.Title = group.Title) Then
                        Groups.Add(group)
                    End If
                Next
            End SyncLock
        End Function
    End Class
End Namespace
