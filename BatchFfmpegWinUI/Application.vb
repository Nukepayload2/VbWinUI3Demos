Imports System.IO
Imports Microsoft.UI.Xaml

Public Class Application
    Inherits Microsoft.UI.Xaml.Application

    Private _mWindow As Window

    Protected Overrides Sub OnLaunched(args As LaunchActivatedEventArgs)
        SetCurDirToAsmDir()
        _mWindow = New MainWindow
        _mWindow.Activate()
    End Sub

    Private Sub SetCurDirToAsmDir()
        Dim appDir = Path.GetDirectoryName(GetType(Program).Assembly.Location)
        Directory.SetCurrentDirectory(appDir)
    End Sub

    Private Sub Application_UnhandledException(
            sender As Object,
            e As UnhandledExceptionEventArgs) Handles Me.UnhandledException
        Stop
    End Sub
End Class
