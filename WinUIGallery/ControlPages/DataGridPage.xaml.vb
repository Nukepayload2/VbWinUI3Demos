' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class DataGridPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Async Sub LaunchToolkitButton_Click(sender As Object, e As RoutedEventArgs)
            ' Set the recommended app
            Dim options As New Windows.System.LauncherOptions With
            { _
            .PreferredApplicationPackageFamilyName = "Microsoft.UWPCommunityToolkitSampleApp_8wekyb3d8bbwe",
            .PreferredApplicationDisplayName = "Windows Community Toolkit"
            }

            Await Windows.System.Launcher.LaunchUriAsync(New Uri("uwpct://controls?sample=datagrid"), options)
        End Sub
    End Class
End Namespace
