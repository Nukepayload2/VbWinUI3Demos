Imports System.Buffers
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Threading

Module StreamHelper
    <Extension>
    Async Function ReadToEndAsync(reader As StreamReader, token As CancellationToken) As Task(Of String)
        Dim buf = ArrayPool(Of Char).Shared.Rent(4096)
        Try
            Dim bufMemory = buf.AsMemory

            Dim result As New StringBuilder
            Dim readCount As Integer
            Do
                readCount = Await reader.ReadBlockAsync(bufMemory, token).ConfigureAwait(False)
                If readCount = 0 Then Exit Do
                result.Append(buf.AsSpan(0, readCount))
            Loop

            Return result.ToString
        Finally
            ArrayPool(Of Char).Shared.Return(buf)
        End Try
    End Function
End Module
