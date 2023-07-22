Imports System.ComponentModel
Imports Microsoft.UI.Xaml

Public Class ConvertibleVideo
    Implements INotifyPropertyChanged

    Public Sub New(path As String, output As String, formatName As String, scriptName As String)
        Me.Path = path
        Me.Output = output
        Me.FormatName = formatName
        Me.ScriptName = scriptName
    End Sub

    Public ReadOnly Property Path As String
    Public ReadOnly Property Output As String
    Public ReadOnly Property FormatName As String
    Public ReadOnly Property ScriptName As String

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

    Dim _ProgressMax As Double
    Public Property ProgressMax As Double
        Get
            Return _ProgressMax
        End Get
        Set(value As Double)
            _ProgressMax = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ProgressMax)))
        End Set
    End Property

    Dim _ProgressValue As Double
    Public Property ProgressValue As Double
        Get
            Return _ProgressValue
        End Get
        Set(value As Double)
            _ProgressValue = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ProgressValue)))
        End Set
    End Property

    Dim _ProgressVisibility As Visibility = Visibility.Collapsed
    Public Property ProgressVisibility As Visibility
        Get
            Return _ProgressVisibility
        End Get
        Set(value As Visibility)
            _ProgressVisibility = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ProgressVisibility)))
        End Set
    End Property

    Dim _ProgressIndeterminate As Boolean
    Public Property ProgressIndeterminate As Boolean
        Get
            Return _ProgressIndeterminate
        End Get
        Set(value As Boolean)
            _ProgressIndeterminate = value
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(NameOf(ProgressIndeterminate)))
        End Set
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class