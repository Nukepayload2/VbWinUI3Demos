' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Microsoft.UI.Xaml.Data

Namespace AppUIBasics.Common
    Public Class NullableBooleanToBooleanConverter
        Inherits IValueConverter
        Public Function Convert(_value As Object, targetType As Type, parameter As Object, language As String) As Object
            If TypeOf _value Is Boolean? Then
                Return CBool(_value)
            End If
            Return False
        End Function
        Public Function ConvertBack(_value As Object, targetType As Type, parameter As Object, language As String) As Object
            If TypeOf _value Is Boolean Then
                Return CBool(_value)
            End If
            Return False
        End Function
    End Class
End Namespace
