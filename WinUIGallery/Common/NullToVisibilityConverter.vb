' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Data

Namespace AppUIBasics.Common
    Public Class NullToVisibilityConverter
        Implements IValueConverter
        Public Property NullValue As Visibility = Visibility.Collapsed
        Public Property NonNullValue As Visibility = Visibility.Visible
        Public Function Convert(_value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
            Return If((_value Is Nothing), NullValue, NonNullValue)
        End Function
        Public Function ConvertBack(_value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException
        End Function
    End Class
End Namespace
