﻿Imports System.Runtime.CompilerServices
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

    Private Declare Function GetSystemMetrics Lib "user32" (nIndex As Integer) As Integer
    Private Const SM_CYCAPTION = 4

    <Extension>
    Public Function GetTitleBarHeight(appWnd As AppWindow) As Integer
        Dim titleBarHeight = appWnd.TitleBar?.Height
        If titleBarHeight.GetValueOrDefault = 0 Then
            ' Sometimes appWnd.TitleBar.Height = 0. Use values from WinForms as fallback.
            titleBarHeight = GetSystemMetrics(SM_CYCAPTION)
        End If

        Return titleBarHeight.GetValueOrDefault
    End Function

End Module
