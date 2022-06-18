Imports System.Runtime.InteropServices
Imports Microsoft.UI.Dispatching

' Copied from https://github.com/microsoft/WinUI-Gallery

''' <summary>
''' Advanced version of DispatcherQueueController .
''' </summary>
Public Class WindowsSystemDispatcherQueueHelper
    <StructLayout(LayoutKind.Sequential)>
    Private Structure DispatcherQueueOptions
        Dim Size As Integer
        Dim ThreadType As ThreadType
        Dim ApartmentType As ApartmentType
    End Structure

    Private Enum ThreadType
        Dedicated = 1
        Current = 2
    End Enum

    Private Enum ApartmentType
        None = 0
        ASTA = 1
        STA = 2
    End Enum

    Private Declare Function CreateDispatcherQueueController Lib "CoreMessaging.dll" (
         <[In]> options As DispatcherQueueOptions,
         <[In], Out(), MarshalAs(UnmanagedType.IUnknown)>
         ByRef dispatcherQueueController As Object) As Integer

    Private m_dispatcherQueueController As Object = Nothing
    Public Sub EnsureWindowsSystemDispatcherQueueController()
        If Windows.System.DispatcherQueue.GetForCurrentThread() IsNot Nothing Then
            Return
        End If

        'DispatcherQueueController.CreateOnCurrentThread()

        If m_dispatcherQueueController Is Nothing Then
            Dim options As DispatcherQueueOptions
            With options
                options.Size = Marshal.SizeOf(GetType(DispatcherQueueOptions))
                options.ThreadType = ThreadType.Current
                options.ApartmentType = ApartmentType.STA
            End With

            CreateDispatcherQueueController(options, m_dispatcherQueueController)
        End If
    End Sub
End Class
