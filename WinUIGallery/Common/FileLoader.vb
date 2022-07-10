' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Text
Imports System.Threading.Tasks
Imports Windows.Storage

Namespace AppUIBasics.Common
    Friend Class FileLoader
        Public Shared Async Function LoadText(relativeFilePath As String) As Task(Of String)
#If UNPACKAGED

            var sourcePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), relativeFilePath));
            var file = await StorageFile.GetFileFromPathAsync(sourcePath);

#Else

            Dim sourceUri As New Uri("ms-appx:///" & relativeFilePath)
            Dim file = Await StorageFile.GetFileFromApplicationUriAsync(sourceUri)
#End If

            Return Await FileIO.ReadTextAsync(file)
        End Function
        Public Shared Async Function LoadLines(relativeFilePath As String) As Task(Of IList(Of String))
            Dim fileContents As String = Await LoadText(relativeFilePath)
            Return fileContents.Split(Environment.NewLine).ToList()
        End Function
    End Class
End Namespace
