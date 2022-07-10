' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Imports AppUIBasics.Helper
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.System
Imports Microsoft.UI
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Input
Imports Microsoft.UI.Xaml.Navigation
Imports WinRT
Imports System.Runtime.InteropServices

Namespace WinUIGallery.DesktopWap.Helper
    Friend NotInheritable Class TitleBarHelper
        Public Shared Sub triggerTitleBarRepaint()
            ' to trigger repaint tracking task id 38044406
            Dim hwnd = WinRT.Interop.WindowNative.GetWindowHandle(AppUIBasics.App.StartupWindow)
            Dim activeWindow = AppUIBasics.Win32.GetActiveWindow()
            If hwnd = activeWindow Then
                AppUIBasics.Win32.SendMessage(hwnd, AppUIBasics.Win32.WM_ACTIVATE, AppUIBasics.Win32.WA_INACTIVE, IntPtr.Zero)
                AppUIBasics.Win32.SendMessage(hwnd, AppUIBasics.Win32.WM_ACTIVATE, AppUIBasics.Win32.WA_ACTIVE, IntPtr.Zero)
            Else
                AppUIBasics.Win32.SendMessage(hwnd, AppUIBasics.Win32.WM_ACTIVATE, AppUIBasics.Win32.WA_ACTIVE, IntPtr.Zero)
                AppUIBasics.Win32.SendMessage(hwnd, AppUIBasics.Win32.WM_ACTIVATE, AppUIBasics.Win32.WA_INACTIVE, IntPtr.Zero)
            End If
        End Sub

    End Class
End Namespace
