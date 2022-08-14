' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports System.Collections.Generic
Imports System.Linq
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Media
Imports Windows.Storage
Imports System.Runtime.CompilerServices

Namespace AppUIBasics.Helper
    Public Module UIHelper
        Public Property IsScreenshotMode As Boolean
#If Not UNPACKAGED

        Public Property ScreenshotStorageFolder As StorageFolder = ApplicationData.Current.LocalFolder
#Else

        public static StorageFolder ScreenshotStorageFolder { get; set; } = Task.Run(async () => await StorageFolder.GetFolderFromPathAsync(System.AppContext.BaseDirectory)).Result;

#End If

        <Extension()>
        Public Function GetDescendantsOfType(Of T As DependencyObject
)(start As DependencyObject) As IEnumerable(Of T)
            Return start.GetDescendants().OfType(Of T)()
        End Function

        <Extension()>
        Public Iterator Function GetDescendants(start As DependencyObject) As IEnumerable(Of DependencyObject)
            Dim queue1 As Collections.Generic.Queue(Of DependencyObject) = New Queue(Of DependencyObject)
            Dim count1 = VisualTreeHelper.GetChildrenCount(start)

            For i As Integer = 0 To count1 - 1
                Dim child = VisualTreeHelper.GetChild(start, i)
                Yield child
                queue1.Enqueue(child)
            Next

            While queue1.Count > 0
                Dim parent = queue1.Dequeue()
                Dim count2 = VisualTreeHelper.GetChildrenCount(parent)

                For i As Integer = 0 To count2 - 1
                    Dim child = VisualTreeHelper.GetChild(parent, i)
                    Yield child
                    queue1.Enqueue(child)
                Next
            End While
        End Function
    End Module
End Namespace
