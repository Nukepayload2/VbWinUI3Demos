Imports Microsoft.Build.Framework

Public Class ConvertCSharpToVB
    Inherits Microsoft.Build.Utilities.Task

    <Required>
    Public ReadOnly Property ProjectPath As String

    <Output>
    Public ReadOnly Property CompileCodeFiles As ITaskItem()

    <Output>
    Public ReadOnly Property ConvertedCodeFiles As ITaskItem()

    Public Overrides Function Execute() As Boolean

        ' TODO: Call process files in CSharpToVB\Utilities

        Return True
    End Function
End Class
