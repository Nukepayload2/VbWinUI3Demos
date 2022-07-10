' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports Microsoft.UI.Xaml.Data
Imports Microsoft.UI.Xaml.Interop
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Collections.Specialized
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Imports NotifyCollectionChangedAction = Microsoft.UI.Xaml.Interop.NotifyCollectionChangedAction
Imports System.Runtime.InteropServices

Namespace AppUIBasics
    ' .NET collection types are tightly coupled with WUX types - e.g., ObservableCollection<T>
    ' maps to WUX.INotifyCollectionChanged, and creates WUX.NotifyCollectionChangedEventArgs
    ' when raising its INCC event.  This is a problem because we've switched everything else over
    ' to use MUX types, such that creating WUX types raises an RPC_E_WRONG_THREAD error
    ' due to DXamlCore not being initialized.  For the purposes of our tests, we're providing
    ' our own implementation of ObservableCollection<T> that implements MUX.INotifyCollectionChanged.
    Public Class ObservableCollection(Of T)
        Inherits Collection(Of T)
        Implements Microsoft.UI.Xaml.Interop.INotifyCollectionChanged, INotifyPropertyChanged
        Private reentrancyGuard1 As ReentrancyGuard = Nothing
        Private Class ReentrancyGuard
            Implements IDisposable
            Private owningCollection As ObservableCollection(Of T)

            Public Sub New(owningCollection As ObservableCollection(Of T))
                owningCollection.CheckReentrancy()
                owningCollection.reentrancyGuard = Me
                Me.owningCollection = owningCollection
            End Sub
            Public Sub Dispose() Implements IDisposable.Dispose
                owningCollection.reentrancyGuard = Nothing
            End Sub
        End Class

        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(list As IList(Of T))
            MyBase.New(list.ToList())
        End Sub
        Public Sub New(collection As IEnumerable(Of T))
            MyBase.New(collection.ToList())
        End Sub

        Public Event CollectionChanged As Microsoft.UI.Xaml.Interop.NotifyCollectionChangedEventHandler
        Public Sub Move(oldIndex As Integer, newIndex As Integer)
            MoveItem(oldIndex, newIndex)
        End Sub
        Protected Function BlockReentrancy() As IDisposable
            Return New ReentrancyGuard(Me)
        End Function
        Protected Sub CheckReentrancy()
            If reentrancyGuard IsNot Nothing Then
                Throw New InvalidOperationException("Collection cannot be modified in a collection changed handler.")
            End If
        End Sub
        Protected Overrides Sub ClearItems()
            CheckReentrancy()

            Dim oldItems As New TestBindableVector(Of T)(Me)

            MyBase.ClearItems()
            OnCollectionChanged( _
                NotifyCollectionChangedAction.Reset,
                Nothing, oldItems, 0, 0)
        End Sub
        Protected Overrides Sub InsertItem(index As Integer, item As T)
            CheckReentrancy()

            Dim newItem As New TestBindableVector(Of T)
            newItem.Add(item)

            MyBase.InsertItem(index, item)
            OnCollectionChanged( _
                NotifyCollectionChangedAction.Add,
                newItem, Nothing, index, 0)
        End Sub
        Protected Overridable Sub MoveItem(oldIndex As Integer, newIndex As Integer)
            CheckReentrancy()

            Dim oldItem As New TestBindableVector(Of T)
            oldItem.Add(Me(oldIndex))
            Dim newItem As New TestBindableVector(Of T)(oldItem)

            Dim item As T = Me(oldIndex)
            MyBase.RemoveAt(oldIndex)
            MyBase.InsertItem(newIndex, item)
            OnCollectionChanged( _
                NotifyCollectionChangedAction.Move,
                newItem, oldItem, newIndex, oldIndex)
        End Sub
        Protected Overrides Sub RemoveItem(index As Integer)
            CheckReentrancy()

            Dim oldItem As New TestBindableVector(Of T)
            oldItem.Add(Me(index))

            MyBase.RemoveItem(index)
            OnCollectionChanged( _
                NotifyCollectionChangedAction.Remove,
                Nothing, oldItem, 0, index)
        End Sub
        Protected Overrides Sub SetItem(index As Integer, item As T)
            CheckReentrancy()

            Dim oldItem As New TestBindableVector(Of T)
            oldItem.Add(Me(index))
            Dim newItem As New TestBindableVector(Of T)
            newItem.Add(item)

            MyBase.SetItem(index, item)
            OnCollectionChanged( _
                NotifyCollectionChangedAction.Replace,
                newItem, oldItem, index, index)
        End Sub
        Protected Overridable Sub OnCollectionChanged( _
            action As NotifyCollectionChangedAction,
            newItems As IBindableVector,
            oldItems As IBindableVector,
            newIndex As Integer,
            oldIndex As Integer)
            OnCollectionChanged(New Microsoft.UI.Xaml.Interop.NotifyCollectionChangedEventArgs(action, newItems, oldItems, newIndex, oldIndex))
        End Sub
        Protected Overridable Sub OnCollectionChanged(e As Microsoft.UI.Xaml.Interop.NotifyCollectionChangedEventArgs)
            Using BlockReentrancy()
                CollectionChanged?.Invoke(Me, e)
            End Using
        End Sub

        Public Event PropertyChanged As PropertyChangedEventHandler
    End Class


    Public Class TestBindableVector(Of T)
        Implements IList(Of T), IBindableVector
        Private implementation As IList(Of T)

        Public Sub New()
            implementation = New List(Of T)
        End Sub
        Public Sub New(list1 As IList(Of T))
            implementation = New List(Of T)(list1)
        End Sub
        Default Public Property item(index As Integer) As T Implements Collections.Generic.IList(Of T).item
            Get
                Return implementation(index)
            End Get
            Set(value As T)
                implementation(index) = value
            End Set
        End Property
        Public ReadOnly Property Count As Integer Implements Collections.Generic.ICollection(Of T).Count
            Get
                Return implementation.Count
            End Get
        End Property
        Public Overridable ReadOnly Property IsReadOnly As Boolean Implements Collections.Generic.ICollection(Of T).IsReadOnly
            Get
                Return implementation.IsReadOnly
            End Get
        End Property
        Public Sub Add(item As T) Implements Collections.Generic.ICollection(Of T).Add
            implementation.Add(item)
        End Sub
        Public Sub Clear() Implements Collections.Generic.ICollection(Of T).Clear
            implementation.Clear()
        End Sub
        Public Function Contains(item As T) As Boolean Implements Collections.Generic.ICollection(Of T).Contains
            Return implementation.Contains(item)
        End Function
        Public Sub CopyTo(array As T(), arrayIndex As Integer) Implements Collections.Generic.ICollection(Of T).CopyTo
            implementation.CopyTo(array, arrayIndex)
        End Sub
        Public Function GetEnumerator() As IEnumerator(Of T) Implements Collections.Generic.IEnumerable(Of T).GetEnumerator
            Return implementation.GetEnumerator()
        End Function
        Public Function IndexOf(item As T) As Integer Implements Collections.Generic.IList(Of T).IndexOf
            Return implementation.IndexOf(item)
        End Function
        Public Sub Insert(index As Integer, item As T) Implements Collections.Generic.IList(Of T).Insert
            implementation.Insert(index, item)
        End Sub
        Public Function Remove(item As T) As Boolean Implements Collections.Generic.ICollection(Of T).Remove
            Return implementation.Remove(item)
        End Function
        Public Sub RemoveAt(index As Integer) Implements Collections.Generic.IList(Of T).RemoveAt
            implementation.RemoveAt(index)
        End Sub
        Private Function GetEnumerator1() As IEnumerator Implements Collections.IEnumerable.GetEnumerator
            Return implementation.GetEnumerator()
        End Function
        Public Function GetAt(index As UInteger) As Object
            Return implementation(CInt(CLng(Fix(index)) Mod Integer.MaxValue))
        End Function
        Public Function GetView() As IBindableVectorView
            Return New TestBindableVectorView(Of T)(implementation)
        End Function
        Public Function IndexOf(value As Object, <Out> ByRef index As UInteger) As Boolean
            Dim indexOf1 As Integer = implementation.IndexOf(CType(value, T))

            If indexOf1 >= 0 Then
                index = CUInt(indexOf1)
                Return True
            Else
                index = 0
                Return False
            End If
        End Function
        Public Sub SetAt(index As UInteger, value As Object)
            implementation(CInt(CLng(Fix(index)) Mod Integer.MaxValue)) = CType(value, T)
        End Sub
        Public Sub InsertAt(index As UInteger, value As Object)
            implementation.Insert(CInt(CLng(Fix(index)) Mod Integer.MaxValue), CType(value, T))
        End Sub
        Public Sub RemoveAt(index As UInteger)
            implementation.RemoveAt(CInt(CLng(Fix(index)) Mod Integer.MaxValue))
        End Sub
        Public Sub Append(value As Object)
            implementation.Add(CType(value, T))
        End Sub
        Public Sub RemoveAtEnd()
            implementation.RemoveAt(implementation.Count - 1)
        End Sub
        Public ReadOnly Property Size As UInteger
            Get
                Return CUInt(implementation.Count)
            End Get
        End Property
        Public Function First() As IBindableIterator
            Return New TestBindableIterator(Of T)(implementation)
        End Function
    End Class


    Public Class TestBindableVectorView(Of T)
        Inherits TestBindableVector(Of T)
        Implements IBindableVectorView
        Public Sub New(list As IList(Of T))
            MyBase.New(list)
        End Sub
        Public Overrides ReadOnly Property IsReadOnly As Boolean
            Get
                Return True
            End Get
        End Property
    End Class


    Public Class TestBindableIterator(Of T)
        Inherits IBindableIterator
        Private ReadOnly enumerator As IEnumerator(Of T)

        Public Sub New(enumerable As IEnumerable(Of T))
            enumerator = enumerable.GetEnumerator()
        End Sub
        Public Function MoveNext() As Boolean
            Return enumerator.MoveNext()
        End Function
        Public ReadOnly Property Current As Object
            Get
                Return enumerator.Current
            End Get
        End Property
        Public ReadOnly Property HasCurrent As Boolean
            Get
                Return enumerator.Current IsNot Nothing
            End Get
        End Property
    End Class
End Namespace
