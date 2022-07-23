Imports System.IO
Imports System.Runtime.CompilerServices
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
                    Return
                End If

                ' VB specific. We don't need to handle them while converting C# to VB.

                Log.LogMessage($"Ignored defined constant(s) '{definedConstant}' while converting C# to VB.")
            End Function(con.ItemSpec)

            Dim consts = constGroups.SelectMany(Function(s) s).ToList

            Dim referencesRaw = Aggregate asm In ReferenceAssemblies
                                Select asm.ItemSpec Into ToArray
            Dim deletedFilesRaw As New List(Of String)
            Dim generatedFilesRaw As New List(Of String)

            Dim convIsolated = IsolationHelper.CreateInstance(Of ICsVbCodeConverter, CsVbCodeConverter)()

            With convIsolated
                .References = referencesRaw
                .DefinedConstants = consts
                .WarningLog = Sub(text) Log.LogWarning(text)
            End With

            For Each inFile In inputFiles
                Log.LogMessage($"Converting file '{inFile}'")

                Try
                    Dim outFile = convIsolated.InvokeConverterForFile(inFile)
                    deletedFilesRaw.Add(inFile)
                    generatedFilesRaw.Add(outFile)
                Catch ex As Exception
                    Log.LogMessage($"Failed to convert file '{inFile}', see exception below.")
                    Log.LogError(PrintExceptionAndInnerException(ex))
                    Log.LogErrorFromException(ex, True, True, Nothing)
                    Return False
                End Try
            Next

            _DeletedCodeFiles = ToTaskItemArray(deletedFilesRaw)
            _GeneratedCodeFiles = ToTaskItemArray(generatedFilesRaw)

            Log.LogMessage($"Output parameter '{NameOf(DeletedCodeFiles)}'={PrintTaskItem(DeletedCodeFiles)}")
            Log.LogMessage($"Output parameter '{NameOf(GeneratedCodeFiles)}'={PrintTaskItem(GeneratedCodeFiles)}")

            Return True
        End Function

        Private Shared s_isolationHelper As AssemblyIsolationHelper
        Private Shared ReadOnly Property IsolationHelper As AssemblyIsolationHelper
            <MethodImpl(MethodImplOptions.Synchronized)>
            Get
                If s_isolationHelper Is Nothing Then
                    s_isolationHelper = New AssemblyIsolationHelper
                End If
                Return s_isolationHelper
            End Get
        End Property

    End Class

End Namespace
