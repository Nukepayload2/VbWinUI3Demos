Public Interface ICsVbCodeConverter
    Property References As String()
    Property DefinedConstants As List(Of KeyValuePair(Of String, Object))
    Property WarningLog As Action(Of String)
    Function InvokeConverterForFile(csFile As String) As String
End Interface
