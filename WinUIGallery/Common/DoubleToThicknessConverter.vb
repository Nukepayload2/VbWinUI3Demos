' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Data

Namespace AppUIBasics.Common
    Class DoubleToThicknessConverter
        Implements IValueConverter
        Public Function Convert(_value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
            If TypeOf _value Is Double? Then
                Dim val As Double = CDbl(_value)

#If Not UNIVERSAL Then

                Return New Thickness(val)
#Else

                return ThicknessHelper.FromUniformLength(val);

#End If

            End If
            Return False
        End Function
        Public Function ConvertBack(_value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
            Throw New NotImplementedException
        End Function
    End Class
End Namespace
