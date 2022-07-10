Imports System.IO
Imports Microsoft.Build.Framework
Imports Microsoft.CodeAnalysis

Namespace Tasks
    Public Class ConvertCSharpToVB
        Inherits Microsoft.Build.Utilities.Task

        <Required>
        Public Property CompileCodeFiles As ITaskItem()

        <Required>
        Public Property DefinedConstants As ITaskItem()

        <Required>
        Public Property ReferenceAssemblies As ITaskItem()

        <Output>
        Public ReadOnly Property DeletedCodeFiles As ITaskItem()

        <Output>
        Public ReadOnly Property GeneratedCodeFiles As ITaskItem()

        Public Overrides Function Execute() As Boolean

            Log.LogMessage($"Input parameter '{NameOf(CompileCodeFiles)}'={PrintTaskItem(CompileCodeFiles)}")
            Log.LogMessage($"Input parameter '{NameOf(DefinedConstants)}'={PrintTaskItem(DefinedConstants)}")
            Log.LogMessage($"Input parameter '{NameOf(ReferenceAssemblies)}'={PrintTaskItem(ReferenceAssemblies)}")

            Dim references = Aggregate asm In ReferenceAssemblies
                             Select MetadataReference.CreateFromFile(asm.ItemSpec) Into ToArray

            Dim inputFiles = Aggregate srcFile In CompileCodeFiles
                             Let sourceFile = srcFile.ItemSpec, fileExt = Path.GetExtension(sourceFile)
                             Where fileExt?.EndsWith(".cs", StringComparison.OrdinalIgnoreCase)
                             Select sourceFile Into ToArray

            Dim constGroups = From con In DefinedConstants
                              Select
            Iterator Function(definedConstant As String) As IEnumerable(Of KeyValuePair(Of String, Object))
                If String.IsNullOrWhiteSpace(definedConstant) Then Return

                ' Constants from .NET SDK and C#
                If Not definedConstant.Contains(","c) AndAlso Not definedConstant.Contains("="c) Then
                    Yield New KeyValuePair(Of String, Object)(definedConstant, True)
                End If

                ' VB specific. We don't need to handle them while converting C# to VB.

                Log.LogMessage($"Ignored defined constant(s) '{definedConstant}' while converting C# to VB.")
            End Function(con.ItemSpec)

            Dim consts = constGroups.SelectMany(Function(s) s).ToList

            Dim vbOption As New DefaultVbOptions("Binary", True, "On", True, "On", True, "On", True)

            Dim deletedFiles As New List(Of ITaskItem)
            Dim generatedFiles As New List(Of ITaskItem)

            For Each inFile In inputFiles

                Log.LogMessage($"Converting file '{inFile}'")

                Try
                    Dim convRequest As New ConvertRequest(False, False, Nothing, Nothing) With {
                        .SourceCode = File.ReadAllText(inFile)
                    }

                    Dim result = ConvertInputRequest(convRequest, vbOption,
                                     (Aggregate con In consts Select con.Key Into ToList),
                                     consts, references,
                                     Sub(err) Log.LogWarning($"Error '{err.GetType.FullName}' while converting C# to VB: {err.Message}"),
                                      Nothing)

                    Dim converted = result.ConvertedCode
                    Dim outFile = String.Concat(inFile.AsSpan(0, inFile.Length - 2), "vb")
                    File.Delete(inFile)
                    deletedFiles.Add(New Microsoft.Build.Utilities.TaskItem(inFile))
                    generatedFiles.Add(New Microsoft.Build.Utilities.TaskItem(outFile))
                Catch ex As Exception
                    Log.LogMessage($"Failed to convert file '{inFile}', see exception below.")
                    Log.LogErrorFromException(ex, True, True, Nothing)
                    LogLoadedAssemblies()
                    Return False
                End Try
            Next

            _DeletedCodeFiles = deletedFiles.ToArray
            _GeneratedCodeFiles = generatedFiles.ToArray

            Log.LogMessage($"Output parameter '{NameOf(DeletedCodeFiles)}'={PrintTaskItem(DeletedCodeFiles)}")
            Log.LogMessage($"Output parameter '{NameOf(GeneratedCodeFiles)}'={PrintTaskItem(GeneratedCodeFiles)}")

            Return True
        End Function

        Private Sub LogLoadedAssemblies()
            Dim loadedAsms = From asm In AppDomain.CurrentDomain.GetAssemblies
                             Select $"  Assembly '{asm.FullName}' at '{asm.Location}'"

            Log.LogMessage("Loaded assemblies")
            Log.LogMessage(String.Join(Environment.NewLine, loadedAsms))
            Log.LogMessage("End Loaded assemblies")
        End Sub

        Private Function PrintTaskItem(compileCodeFiles() As ITaskItem) As String
            Return String.Join(";"c, From itm In compileCodeFiles Select itm.ItemSpec)
        End Function
    End Class

End Namespace
