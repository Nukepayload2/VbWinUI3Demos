Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.Build.Framework
Imports Microsoft.Build.Utilities
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

        <Required>
        Public Property Configuration As String

        <Output>
        Public ReadOnly Property DeletedCodeFiles As ITaskItem()

        <Output>
        Public ReadOnly Property GeneratedCodeFiles As ITaskItem()

        Public Overrides Function Execute() As Boolean

            Log.LogMessage($"Input parameter '{NameOf(CompileCodeFiles)}'={PrintTaskItem(CompileCodeFiles)}")
            Log.LogMessage($"Input parameter '{NameOf(DefinedConstants)}'={PrintTaskItem(DefinedConstants)}")
            Log.LogMessage($"Input parameter '{NameOf(ReferenceAssemblies)}'={PrintTaskItem(ReferenceAssemblies)}")
            Log.LogMessage($"Input parameter '{NameOf(Configuration)}'={Configuration}")

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
            If Configuration <> Nothing AndAlso
                Not (From con In consts
                     Where String.Equals(con.Key, Configuration, StringComparison.OrdinalIgnoreCase)).Any Then
                consts.Add(New KeyValuePair(Of String, Object)(Configuration, True))
            End If

            Dim referencesRaw = Aggregate asm In ReferenceAssemblies
                                Select asm.ItemSpec Into ToArray
            Dim deletedFilesRaw As New List(Of String)
            Dim generatedFilesRaw As New List(Of String)

            Dim convIsolated = IsolationHelper.CreateInstance(Of ICsVbCodeConverter, CsVbCodeConverter)()

            With convIsolated
                .References = referencesRaw
                .DefinedConstants = consts
                .WarningLog = AddressOf New RemoteWarningLogger(Log).Log
            End With

            For Each inFile In inputFiles
                Log.LogMessage($"Converting file '{inFile}'")

                Try
                    Dim outFile = convIsolated.ConvertFile(inFile)
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

        Private Function ToTaskItemArray(items As IEnumerable(Of String)) As ITaskItem()
            Return Aggregate item In items
                   Select New TaskItem(item)
                   Into ToArray
        End Function

        Private Class RemoteWarningLogger
            Inherits MarshalByRefObject

            Private ReadOnly _log As TaskLoggingHelper

            Public Sub New(log As TaskLoggingHelper)
                _log = log
            End Sub

            Sub Log(text As String)
                _log.LogWarning(text)
            End Sub

            Public Overrides Function InitializeLifetimeService() As Object
                Return Nothing
            End Function
        End Class
    End Class

End Namespace
