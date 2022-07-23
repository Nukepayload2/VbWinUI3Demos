Imports System.Text
Imports Microsoft.Build.Framework
Imports Microsoft.CodeAnalysis

Module LogHelper

    Function PrintExceptionAndInnerException(
        ex As Exception,
        Optional exText As StringBuilder = Nothing,
        Optional depth As Integer = 0) As String

        If ex Is Nothing Then
            Throw New ArgumentNullException(NameOf(ex))
        End If

        If exText Is Nothing Then exText = New StringBuilder
        If depth > 16 Then
            exText.AppendLine($"InnerException at depth {depth} truncated")
            Return Nothing
        End If

        exText.AppendLine(ex.GetType.FullName & " at depth " & depth)
        exText.AppendLine(ex.Message)

        If ex.InnerException IsNot Nothing Then
            PrintExceptionAndInnerException(ex.InnerException, exText, depth + 1)
        End If

        Return exText.ToString
    End Function

    Function PrintTaskItem(compileCodeFiles() As ITaskItem) As String
        Return String.Join(";"c, From itm In compileCodeFiles Select itm.ItemSpec)
    End Function

End Module