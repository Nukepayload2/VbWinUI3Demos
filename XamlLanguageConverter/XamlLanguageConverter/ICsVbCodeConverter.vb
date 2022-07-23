Public Interface ICsVbCodeConverter
    Property References As String()
    Property DefinedConstants As List(Of KeyValuePair(Of String, Object))
    Property WarningLog As Action(Of String)
    Function ConvertFile(csFile As String) As String
    Function ConvertSource(csFile As String) As String
End Interface
