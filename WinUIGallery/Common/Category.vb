' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports Microsoft.UI.Xaml.Controls

Namespace AppUIBasics.Common


    Public Class CategoryBase
    End Class


    Public Class Category
        Inherits CategoryBase
        Public Property Name As String
        Public Property Tooltip As String
        Public Property Glyph As Symbol
        'public Type TargetType { get; set; }
    End Class


    Public Class Separator
        Inherits CategoryBase
    End Class


    Public Class Header
        Inherits CategoryBase
        Public Property Name As String
    End Class
End Namespace
