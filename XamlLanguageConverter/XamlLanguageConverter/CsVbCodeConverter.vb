Imports System.IO
Imports Microsoft.CodeAnalysis

Partial Public Class CsVbCodeConverter
    Implements ICsVbCodeConverter

    Private _References As String()

    Public Property References As String() Implements ICsVbCodeConverter.References
        Get
            Return _References
        End Get
        Set
            If Value IsNot Nothing Then
                _PeReferences = Aggregate asm In Value
                                Select MetadataReference.CreateFromFile(asm) Into ToArray
            End If
            _References = Value
        End Set
    End Property

    Property DefinedConstants As List(Of KeyValuePair(Of String, Object)) Implements ICsVbCodeConverter.DefinedConstants
    Property WarningLog As Action(Of String) Implements ICsVbCodeConverter.WarningLog

    Private ReadOnly Property PeReferences As PortableExecutableReference()

    Private ReadOnly _vbOption As New DefaultVbOptions("Binary", True, "On", True, "On", True, "On", True)

    Public Function InvokeConverterForFile(csFile As String) As String Implements ICsVbCodeConverter.InvokeConverterForFile
        Dim convRequest As New ConvertRequest(False, False,
                                              Nothing, Nothing) With {
            .SourceCode = File.ReadAllText(csFile)
        }

        Dim result = ConvertInputRequest(convRequest, _vbOption,
                         (Aggregate con In DefinedConstants Select con.Key Into ToList),
                         DefinedConstants, PeReferences,
                         Sub(err) WarningLog()($"Error '{err.GetType.FullName}' while converting C# to VB: {err.Message}"),
                          Nothing)

        Dim converted = result.ConvertedCode
        Dim outFile = String.Concat(csFile.AsSpan(0, csFile.Length - 2), "vb")
        File.Delete(csFile)
        Return outFile
    End Function

End Class
