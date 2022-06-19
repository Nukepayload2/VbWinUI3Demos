Imports System.IO
Imports Microsoft.UI.Xaml

Public Class App
    Inherits Microsoft.UI.Xaml.Application

    WithEvents AppDebugSettings As DebugSettings
    Sub New()
        InitializeComponent()
        AppDebugSettings = DebugSettings
    End Sub

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
        Dim ex = e.Exception.ToString()
        Debug.WriteLine(ex)
        Stop
        e.Handled = True
    End Sub

    Private Sub AppDebugSettings_BindingFailed(sender As Object, e As BindingFailedEventArgs) Handles AppDebugSettings.BindingFailed
        Debug.WriteLine(e.Message)
    End Sub
End Class
