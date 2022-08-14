' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports System.Runtime.InteropServices

Namespace AppUIBasics
    Friend Module Win32
        <DllImport("user32.dll", CharSet:=CharSet.Auto)>
        Public Function SendMessage(hWnd As IntPtr, Msg As Integer, wParam As Integer, lParam As IntPtr) As IntPtr
        End Function
        <DllImport("user32.dll")>
        Public Function LoadIcon(hInstance As IntPtr, lpIconName As IntPtr) As IntPtr
        End Function
        <DllImport("user32.dll")>
        Public Function GetActiveWindow() As IntPtr
        End Function
        <DllImport("kernel32.dll", CharSet:=CharSet.Auto)>
        Public Function GetModuleHandle(moduleName As IntPtr) As IntPtr
        End Function
        Public Const WM_ACTIVATE As Integer = &H0006
        Public Const WA_ACTIVE As Integer = &H01
        'static int WA_CLICKACTIVE = 0x02;
        Public Const WA_INACTIVE As Integer = &H00
        Public Const WM_SETICON As Integer = &H0080
        Public Const ICON_SMALL As Integer = 0
        Public Const ICON_BIG As Integer = 1
    End Module
End Namespace
