' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Data

Namespace AppUIBasics.Common
    Public Class NullToVisibilityConverter
        Inherits IValueConverter
        Public Property NullValue As Visibility = Visibility.Collapsed
        Public Property NonNullValue As Visibility = Visibility.Visible
        Public Function Convert(_value As Object, targetType As Type, parameter As Object, language As String) As Object
            Return If((_value Is Nothing), NullValue, NonNullValue)
        End Function
        Public Function ConvertBack(_value As Object, targetType As Type, parameter As Object, language As String) As Object
            Throw New NotImplementedException
        End Function
    End Class
End Namespace
