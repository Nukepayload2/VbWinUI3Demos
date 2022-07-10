' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports Microsoft.UI.Xaml.Data

Namespace AppUIBasics.Common
    Public NotInheritable Class BooleanToValueConverter
        Inherits IValueConverter
        Public Function Convert(_value As Object, targetType As Type, parameter As Object, language As String) As Object
            Return If((CBool(_value)), parameter, Nothing)
        End Function
        Public Function ConvertBack(_value As Object, targetType As Type, parameter As Object, language As String) As Object
            Throw New NotImplementedException
        End Function
    End Class
End Namespace
