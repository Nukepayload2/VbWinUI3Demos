' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Data

Namespace AppUIBasics.Common
    Class DoubleToThicknessConverter
        Inherits IValueConverter
        Public Function Convert(_value As Object, targetType As Type, parameter As Object, language As String) As Object
            If TypeOf _value Is Double? Then
                Dim val As Double = CDbl(_value)

#If Not UNIVERSAL

                Return New Thickness(val)
#Else

                return ThicknessHelper.FromUniformLength(val);

#End If

            End If
            Return False
        End Function
        Public Function ConvertBack(_value As Object, targetType As Type, parameter As Object, language As String) As Object
            Throw New NotImplementedException
        End Function
    End Class
End Namespace
