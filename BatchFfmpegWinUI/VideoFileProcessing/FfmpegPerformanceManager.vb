Option Compare Text

Imports System.Collections.Concurrent
Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class FfmpegPerformanceManager
    Private ReadOnly _processes As New ConcurrentDictionary(Of Process, Integer)

    Private _priority As ProcessPriorityClass = ProcessPriorityClass.Normal
    Private _lastSnapshot As Process()
    Private ReadOnly _priorityLock As New Object
    Private ReadOnly _snapshotLock As New Object

    Public Sub Add(proc As Process)
        InvalidateSnapshot()
        ChangePriorityOnErrorResumeNext(proc, _priority)
        _processes(proc) = 0
    End Sub

    Private Sub InvalidateSnapshot()
        SyncLock _snapshotLock
            _lastSnapshot = Nothing
        End SyncLock
    End Sub

    Public Sub Remove(proc As Process)
        _processes.TryRemove(proc, Nothing)
    End Sub

    Public Sub Clear()
        _processes.Clear()
    End Sub

    Public Sub PreferPCores()
        SyncLock _priorityLock
            If _priority = ProcessPriorityClass.Normal Then Return
            _priority = ProcessPriorityClass.Normal
        End SyncLock

        InvalidateSnapshot()
        For Each proc In _processes.Keys
            ChangePriorityOnErrorResumeNext(proc, ProcessPriorityClass.Normal)
        Next
    End Sub

    Public Sub PreferECores()
        SyncLock _priorityLock
            If _priority = ProcessPriorityClass.BelowNormal Then Return
            _priority = ProcessPriorityClass.BelowNormal
        End SyncLock

        InvalidateSnapshot()
        For Each proc In _processes.Keys
            ChangePriorityOnErrorResumeNext(proc, ProcessPriorityClass.BelowNormal)
        Next
    End Sub

    Private Sub ChangePriorityOnErrorResumeNext(proc As Process, value As ProcessPriorityClass)
        If proc.HasExited Then Return
        Try
            proc.PriorityClass = value
        Catch ex As Exception
        End Try

        If proc.ProcessName = "ffmpeg" Then Return
        ' proc is cmd. Find ffmpeg.
        Dim snap = _lastSnapshot
        SyncLock _snapshotLock
            If _lastSnapshot Is Nothing Then
                snap = Aggregate p In Process.GetProcesses() Where p.ProcessName = "ffmpeg" Into ToArray
                _lastSnapshot = snap
            End If
        End SyncLock

        Dim cmdPid = proc.Id
        For Each p In snap
            Dim parentPid = GetParentProcessId(p.Handle)
            If parentPid = cmdPid Then
                p.PriorityClass = value
                Exit For
            End If
        Next
    End Sub

    <StructLayout(LayoutKind.Sequential)>
    Private Structure PROCESS_BASIC_INFORMATION
        Friend ExitStatus As Integer
        Friend PebBaseAddress As IntPtr
        Friend AffinityMask As IntPtr
        Friend BasePriority As Integer
        Friend UniqueProcessId As IntPtr
        Friend InheritedFromUniqueProcessId As IntPtr
    End Structure

    Private Declare Function NtQueryInformationProcess Lib "ntdll.dll" (
        processHandle As IntPtr, processInformationClass As Integer,
        ByRef processInformation As PROCESS_BASIC_INFORMATION,
        processInformationLength As Integer,
        ByRef returnLength As Integer) As Integer

    Private Function GetParentProcessId(handle As IntPtr) As Integer
        Dim pbi = New PROCESS_BASIC_INFORMATION
        Dim returnLength As Integer = Nothing
        Dim status As Integer = NtQueryInformationProcess(handle, 0, pbi, Marshal.SizeOf(pbi), returnLength)
        If status <> 0 Then
            Throw New Win32Exception(status)
        End If

        Return pbi.InheritedFromUniqueProcessId.ToInt32()
    End Function
End Class
