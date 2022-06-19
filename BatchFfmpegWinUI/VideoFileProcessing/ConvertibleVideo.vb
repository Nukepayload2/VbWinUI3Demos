Imports System.ComponentModel

Public Class ConvertibleVideo
    Implements INotifyPropertyChanged

    Public Property Path As String
    Public Property Output As String

    Dim _Icon As String = ChrW(&HE916)
    Public Property Icon As String
        Get
            Return _Icon
        End Get
        Set(value As String)
            If _Icon <> value Then
                _Icon = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(Icon)))
            End If
        End Set
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class