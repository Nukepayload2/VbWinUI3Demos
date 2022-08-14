' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports Microsoft.UI.Xaml.Data

Namespace AppUIBasics.Common
    Public NotInheritable Class BooleanToValueConverter
        Implements IValueConverter
        Public Function Convert(_value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
            Return If((CBool(_value)), parameter, Nothing)
        End Function
        Public Function ConvertBack(_value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException
        End Function
    End Class
End Namespace
