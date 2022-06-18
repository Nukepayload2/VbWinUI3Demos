Imports System.Runtime.CompilerServices
Imports Microsoft.UI
Imports Microsoft.UI.Windowing
Imports Microsoft.UI.Xaml
Imports WinRT.Interop

Public Module AppWindowExtension

    <Extension>
    Public Function GetAppWindow(window As Window) As AppWindow
        Dim hWnd As IntPtr = WindowNative.GetWindowHandle(window)
        Dim myWndId As WindowId = Win32Interop.GetWindowIdFromWindow(hWnd)
        Return AppWindow.GetFromWindowId(myWndId)
    End Function

End Module
