' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports System.Collections.Generic
Imports System.Linq
Imports Windows.Foundation.Collections
Imports System.Runtime.InteropServices

Namespace AppUIBasics.Common
    ''' <summary>
    ''' Implementation of IObservableMap that supports reentrancy for use as a default view
    ''' model.
    ''' </summary>
    Public Class ObservableDictionary
        Inherits IObservableMap(Of String, Object)
        Private Class ObservableDictionaryChangedEventArgs
            Inherits IMapChangedEventArgs(Of String)
            Public Sub New(change As CollectionChange, key As String)
                Me.CollectionChange = change
                Me.Key = key
            End Sub

            Private _collectionChange As CollectionChange
            Public Property CollectionChange As CollectionChange
                Get
                    Return _collectionChange
                End Get
                Private Set(value As CollectionChange)
                    _collectionChange = value
                End Set
            End Property
            Private _key As String
            Public Property Key As String
                Get
                    Return _key
                End Get
                Private Set(value As String)
                    _key = value
                End Set
            End Property
        End Class
        Private _dictionary As New Dictionary(Of String, Object)
        Public Event MapChanged As MapChangedEventHandler(Of String, Object)
        Private Sub InvokeMapChanged(change As CollectionChange, key As String)
            MapChanged?.Invoke(Me, New ObservableDictionaryChangedEventArgs(change, key))
        End Sub
        Public Sub Add(key As String, _value As Object)
            Me._dictionary.Add(key, _value)
            Me.InvokeMapChanged(CollectionChange.ItemInserted, key)
        End Sub
        Public Sub Add(item As KeyValuePair(Of String, Object))
            Me.Add(item.Key, item.Value)
        End Sub
        Public Function Remove(key As String) As Boolean
            If Me._dictionary.Remove(key) Then
                Me.InvokeMapChanged(CollectionChange.ItemRemoved, key)
                Return True
            End If
            Return False
        End Function
        Public Function Remove(item As KeyValuePair(Of String, Object)) As Boolean
            Dim currentValue As Object = Nothing
            If Me._dictionary.TryGetValue(item.Key, currentValue) AndAlso _
                            [Object].Equals(item.Value, currentValue) AndAlso Me._dictionary.Remove(item.Key) Then
                Me.InvokeMapChanged(CollectionChange.ItemRemoved, item.Key)
                Return True
            End If
            Return False
        End Function
        Default Public Property item(key As String) As Object
            Get
                Return Me._dictionary(key)
            End Get
            Set(value As Object)
                Me._dictionary(key) = _value
                Me.InvokeMapChanged(CollectionChange.ItemChanged, key)
            End Set
        End Property
        Public Sub Clear()
            Dim priorKeys As String() = Me._dictionary.Keys.ToArray()
            Me._dictionary.Clear()
            For Each key As String In priorKeys
                Me.InvokeMapChanged(CollectionChange.ItemRemoved, key)
            Next
        End Sub
        Public ReadOnly Property Keys As ICollection(Of String)
            Get
                Return Me._dictionary.Keys
            End Get
        End Property
        Public Function ContainsKey(key As String) As Boolean
            Return Me._dictionary.ContainsKey(key)
        End Function
        Public Function TryGetValue(key As String, <Out> ByRef _value As Object) As Boolean
            Return Me._dictionary.TryGetValue(key, _value)
        End Function
        Public ReadOnly Property Values As ICollection(Of Object)
            Get
                Return Me._dictionary.Values
            End Get
        End Property
        Public Function Contains(item As KeyValuePair(Of String, Object)) As Boolean
            Return Me._dictionary.Contains(item)
        End Function
        Public ReadOnly Property Count As Integer
            Get
                Return Me._dictionary.Count
            End Get
        End Property
        Public ReadOnly Property IsReadOnly As Boolean
            Get
                Return False
            End Get
        End Property
        Public Function GetEnumerator() As IEnumerator(Of KeyValuePair(Of String, Object))
            Return Me._dictionary.GetEnumerator()
        End Function
        Private Function GetEnumerator1() As Collections.IEnumerator Implements Collections.IEnumerable.GetEnumerator
            Return Me._dictionary.GetEnumerator()
        End Function
        Public Sub CopyTo(array As KeyValuePair(Of String, Object)(), arrayIndex As Integer)
            Dim arraySize As Integer = array.Length
            For Each pair As KeyValuePair(Of String, Object) In Me._dictionary
                If arrayIndex >= arraySize Then
                    Exit For
                End If
                array(Math.Min(Threading.Interlocked.Increment(arrayIndex), arrayIndex - 1)) = pair
            Next
        End Sub
    End Class
End Namespace
