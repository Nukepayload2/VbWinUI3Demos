Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports WinRT

Module CrashHandler

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

    Async Function ShowFfmpegError(message As String) As Task
        Dim curWnd = WinUIVbHost.Instance.CurrentWindow
        Dim hWndCurWnd = CastExtensions.As(Of IWindowNative)(curWnd).WindowHandle

        Dim errWnd As New Window With {
            .Title = "Conversion failed. Technical details:",
            .Content = New TextBox With {
                .IsReadOnly = True,
                .AcceptsReturn = True,
                .Text = message,
                .Margin = New Thickness(4)
            }
        }

        Dim waitTaskSrc As New TaskCompletionSource
        Dim windowClosed = Sub()
                               EnableWindow(hWndCurWnd, True)
                               curWnd.Activate()
                               waitTaskSrc.SetResult()
                           End Sub

        Dim loaded = False
        Dim windowActivated = Sub()
                                  If loaded Then Return
                                  loaded = True
                                  Dim hWndErrWnd = CastExtensions.As(Of IWindowNative)(errWnd).WindowHandle
                                  SetWindowLong(hWndErrWnd, GWLP_HWNDPARENT, hWndCurWnd)
                                  EnableWindow(hWndCurWnd, False)
                                  My.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation)
                              End Sub

        AddHandler errWnd.Closed, windowClosed
        AddHandler errWnd.Activated, windowActivated
        errWnd.Activate()

        Await waitTaskSrc.Task
    End Function
End Module
