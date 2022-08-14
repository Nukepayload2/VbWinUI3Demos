Option Strict On

Imports Microsoft.VisualBasic.CompilerServices
Imports Windows.UI.Popups
Imports WinRT

Module Interaction
    Public Async Function MsgBoxAsync(prompt As String, Optional title As String = Nothing) As Task(Of IUICommand)
        Dim host = HostServices.VBHost
        If host Is Nothing Then
            Throw New InvalidOperationException("Current window is unknown.")
        End If

        If title Is Nothing Then
            title = host.GetWindowTitle
        End If

        Dim dlg As New MessageDialog(prompt, title)
        Dim initWnd = dlg.As(Of IInitializeWithWindow)
        Dim parent = host.GetParentWindow
        If parent IsNot Nothing Then
            initWnd.Initialize(parent.Handle)
        End If
        Return Await dlg.ShowAsync
    End Function
End Module
