Imports System.Runtime.CompilerServices
Imports Microsoft.UI.Windowing
Imports Microsoft.UI.Xaml
Imports WinRT

Module DialogHelper

    Private Declare Function EnableWindow Lib "user32.dll" (hWnd As IntPtr, enable As Boolean) As Boolean

    Private Declare Unicode Function SetWindowLong32 Lib "user32" Alias "SetWindowLongW" (
        hWnd As IntPtr, nIndex As Integer, dwNewLong As Integer) As Integer

    Private Declare Unicode Function SetWindowLongPtr64 Lib "user32" Alias "SetWindowLongPtrW" (
        hWnd As IntPtr, nIndex As Integer, dwNewLong As IntPtr) As IntPtr

    Private Function SetWindowLong(hWnd As IntPtr, nIndex As Integer, dwNewLong As IntPtr) As IntPtr
        If IntPtr.Size = 8 Then
            Return SetWindowLongPtr64(hWnd, nIndex, dwNewLong)
        Else
            Return New IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32))
        End If
    End Function

    Private Const GWLP_HWNDPARENT = -8

    <Extension>
    Async Function ShowDialogAsync(errWnd As Window, Optional dialogOptions As DialogOptions = Nothing) As Task
        Dim curWnd = WinUIVbHost.Instance.CurrentWindow
        Dim hWndCurWnd = CastExtensions.As(Of IWindowNative)(curWnd).WindowHandle
        Dim curWindowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWndCurWnd)
        Dim curAppWindow = AppWindow.GetFromWindowId(curWindowId)

        Dim waitTaskSrc As New TaskCompletionSource
        Dim windowClosed =
            Sub()
                EnableWindow(hWndCurWnd, True)
                curWnd.Activate()
                waitTaskSrc.SetResult()
            End Sub

        Dim loaded = False
        Dim windowActivated =
            Sub()
                If loaded Then Return
                loaded = True
                Dim hWndErrWnd = CastExtensions.As(Of IWindowNative)(errWnd).WindowHandle
                SetWindowLong(hWndErrWnd, GWLP_HWNDPARENT, hWndCurWnd)
                EnableWindow(hWndCurWnd, False)
                If dialogOptions IsNot Nothing Then
                    Dim windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWndErrWnd)
                    Dim appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId)
                    If dialogOptions.Width IsNot Nothing OrElse
                       dialogOptions.Height IsNot Nothing Then
                        Dim dlgSize = appWindow.Size
                        appWindow.Resize(New Windows.Graphics.SizeInt32 With {
                            .Width = If(dialogOptions.Width, dlgSize.Width),
                            .Height = If(dialogOptions.Height, dlgSize.Height)
                        })
                    End If
                    If dialogOptions.CenterOwner Then
                        Dim curPos = curAppWindow.Position
                        Dim curSize = curAppWindow.Size
                        Dim dlgSize = appWindow.Size
                        Dim newX = curPos.X + (curSize.Width - dlgSize.Width) \ 2
                        Dim newY = curPos.Y + (curSize.Height - dlgSize.Height) \ 2
                        appWindow.Move(New Windows.Graphics.PointInt32 With {
                                           .X = newX, .Y = newY
                                       })
                    End If
                    Dim presenter = TryCast(appWindow.Presenter, OverlappedPresenter)
                    If presenter IsNot Nothing Then
                        If Not dialogOptions.Resizable Then
                            presenter.IsResizable = False
                        End If
                        If Not dialogOptions.Minimizable Then
                            presenter.IsMinimizable = False
                        End If
                        If Not dialogOptions.Maximizable Then
                            presenter.IsMaximizable = False
                        End If
                    End If
                End If
            End Sub

        AddHandler errWnd.Closed, windowClosed
        AddHandler errWnd.Activated, windowActivated
        errWnd.Activate()

        Await waitTaskSrc.Task
    End Function
End Module

Class DialogOptions
    Public Property Width As Integer?
    Public Property Height As Integer?
    Public Property CenterOwner As Boolean
    Public Property Resizable As Boolean = True
    Public Property Minimizable As Boolean = True
    Public Property Maximizable As Boolean = True
End Class
