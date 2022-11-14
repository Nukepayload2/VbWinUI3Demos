Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Threading

Module StreamHelper

    <Extension>
    Async Function ReadToEndWithLineReportAsync(
            reader As StreamReader,
            token As CancellationToken,
            lineHandler As Action(Of String)) As Task(Of String)

        Dim result As New StringBuilder
        Do
            Dim readText = Await reader.ReadLineAsync(token).ConfigureAwait(False)
            If readText Is Nothing Then Exit Do
            lineHandler(readText)
            result.Append(readText)
        Loop

        Return result.ToString
    End Function

End Module
