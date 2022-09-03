Imports System.ComponentModel

Public Class ConvertibleVideo
    Implements INotifyPropertyChanged

    Public Sub New(path As String, output As String, formatName As String)
        Me.Path = path
        Me.Output = output
        Me.FormatName = formatName
    End Sub

    Public ReadOnly Property Path As String
    Public ReadOnly Property Output As String
    Public ReadOnly Property FormatName As String

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