' Copyright (c) Microsoft Corporation. All rights reserved.
' Licensed under the MIT License. See LICENSE in the project root for license information.

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports System.Runtime.InteropServices
Imports System.Threading
Imports Windows.ApplicationModel.Core
Imports Windows.Foundation
Imports Microsoft.UI.Dispatching
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Media

Namespace AppUIBasics
    Class IdleSynchronizer
        Const s_idleTimeoutMs As UInteger = 100000
        Const s_defaultWaitForEventMs As Integer = 10000
        Const s_hasAnimationsHandleName As String = "HasAnimations"
        Const s_animationsCompleteHandleName As String = "AnimationsComplete"
        Const s_hasDeferredAnimationOperationsHandleName As String = "HasDeferredAnimationOperations"
        Const s_deferredAnimationOperationsCompleteHandleName As String = "DeferredAnimationOperationsComplete"
        Const s_rootVisualResetHandleName As String = "RootVisualReset"
        Const s_imageDecodingIdleHandleName As String = "ImageDecodingIdle"
        Const s_fontDownloadsIdleHandleName As String = "FontDownloadsIdle"
        Const s_hasBuildTreeWorksHandleName As String = "HasBuildTreeWorks"
        Const s_buildTreeServiceDrainedHandleName As String = "BuildTreeServiceDrained"
        Private m_dispatcherQueue As DispatcherQueue = Nothing
        Private m_hasAnimationsHandle As Handle
        Private m_animationsCompleteHandle As Handle
        Private m_hasDeferredAnimationOperationsHandle As Handle
        Private m_deferredAnimationOperationsCompleteHandle As Handle
        Private m_rootVisualResetHandle As Handle
        Private m_imageDecodingIdleHandle As Handle
        Private m_fontDownloadsIdleHandle As Handle
        Private m_hasBuildTreeWorksHandle As Handle
        Private m_buildTreeServiceDrainedHandle As Handle
        Private m_waitForAnimationsIsDisabled As Boolean = False
        Private m_isRS2OrHigherInitialized As Boolean = False
        Private m_isRS2OrHigher As Boolean = False
        Private Function OpenNamedEvent(processId As UInteger, threadId As UInteger, eventNamePrefix As String) As Handle
            Dim eventName As String = String.Format("{0}.{1}.{2}", eventNamePrefix, processId, threadId)
            Dim handle1 As New Handle( _
                NativeMethods.OpenEvent( _
                    CUInt((SyncObjectAccess.EVENT_MODIFY_STATE Or SyncObjectAccess.SYNCHRONIZE)),
                    False _ ' inherit handle 
,
                    eventName))

            If Not handle1.IsValid Then
                ' Warning: Opening a session wide event handle, test may listen for events coming from the wrong process
                handle1 = New Handle( _
                    NativeMethods.OpenEvent( _
                        CUInt((SyncObjectAccess.EVENT_MODIFY_STATE Or SyncObjectAccess.SYNCHRONIZE)),
                        False _ ' inherit handle 
,
                        eventNamePrefix))
            End If

            If Not handle1.IsValid Then
                Throw New Exception("Failed to open " & eventName & " handle.")
            End If

            Return handle1
        End Function
        Private Function OpenNamedEvent(threadId As UInteger, eventNamePrefix As String) As Handle
            Return OpenNamedEvent(NativeMethods.GetCurrentProcessId(), threadId, eventNamePrefix)
        End Function
        Private Function OpenNamedEvent(dispatcherQueue1 As DispatcherQueue, eventNamePrefix As String) As Handle
            Return OpenNamedEvent(NativeMethods.GetCurrentProcessId(), GetUIThreadId(dispatcherQueue1), eventNamePrefix)
        End Function
        Private Function GetUIThreadId(dispatcherQueue1 As DispatcherQueue) As UInteger
            Dim threadId As UInteger = 0
            If dispatcherQueue1.HasThreadAccess Then
                threadId = NativeMethods.GetCurrentThreadId()
            Else
                Dim threadIdReceivedEvent As New AutoResetEvent(False)

                dispatcherQueue1.TryEnqueue( _
                    DispatcherQueuePriority.Normal,
                    New DispatcherQueueHandler(Sub()
                                                   threadId = NativeMethods.GetCurrentThreadId()
                                                   threadIdReceivedEvent.[Set]()
                                               End Sub))

                threadIdReceivedEvent.WaitOne(s_defaultWaitForEventMs)
            End If

            Return threadId
        End Function
        Private Shared instance As IdleSynchronizer = Nothing
        Public Shared ReadOnly Property Instance As IdleSynchronizer
            Get
                If instance Is Nothing Then
                    Throw New Exception("Init() must be called on the UI thread before retrieving Instance.")
                End If

                Return Me.instance
            End Get
        End Property

        Public Property Log As String
        Public Property TickCountBegin As Integer

        Private Sub New(dispatcherQueue1 As DispatcherQueue)
            m_dispatcherQueue = dispatcherQueue1
            m_hasAnimationsHandle = OpenNamedEvent(m_dispatcherQueue, s_hasAnimationsHandleName)
            m_animationsCompleteHandle = OpenNamedEvent(m_dispatcherQueue, s_animationsCompleteHandleName)
            m_hasDeferredAnimationOperationsHandle = OpenNamedEvent(m_dispatcherQueue, s_hasDeferredAnimationOperationsHandleName)
            m_deferredAnimationOperationsCompleteHandle = OpenNamedEvent(m_dispatcherQueue, s_deferredAnimationOperationsCompleteHandleName)
            m_rootVisualResetHandle = OpenNamedEvent(m_dispatcherQueue, s_rootVisualResetHandleName)
            m_imageDecodingIdleHandle = OpenNamedEvent(m_dispatcherQueue, s_imageDecodingIdleHandleName)
            m_fontDownloadsIdleHandle = OpenNamedEvent(m_dispatcherQueue, s_fontDownloadsIdleHandleName)
            m_hasBuildTreeWorksHandle = OpenNamedEvent(m_dispatcherQueue, s_hasBuildTreeWorksHandleName)
            m_buildTreeServiceDrainedHandle = OpenNamedEvent(m_dispatcherQueue, s_buildTreeServiceDrainedHandleName)
        End Sub
        Public Shared Sub Init()
            Dim dispatcherQueue1 As DispatcherQueue = DispatcherQueue.GetForCurrentThread()

            If dispatcherQueue1 Is Nothing Then
                Throw New Exception("Init() must be called on the UI thread.")
            End If

            instance = New IdleSynchronizer(dispatcherQueue1)
        End Sub
        Public Shared Sub Wait()
            Dim logMessage As String
            Wait(logMessage)
        End Sub
        Public Shared Sub Wait(<Out> ByRef logMessage As String)
            Dim errorString As String = Instance.WaitInternal(logMessage)

            If errorString.Length > 0 Then
                Throw New Exception(errorString)
            End If
        End Sub
        Public Shared Function TryWait() As String
            Dim logMessage As String
            Return Instance.WaitInternal(logMessage)
        End Function
        Public Shared Function TryWait(<Out> ByRef logMessage As String) As String
            Return Instance.WaitInternal(logMessage)
        End Function
        Public Sub AddLog(message As String)
            Diagnostics.Debug.WriteLine(message)

            If Log IsNot Nothing AndAlso Log <> "LOG: " Then
                Log &= "; "
            End If

            Log &= (Environment.TickCount - TickCountBegin).ToString() & ": "
            Log &= message
        End Sub
        Private Function WaitInternal(<Out> ByRef logMessage As String) As String
            logMessage = String.Empty
            Dim errorString As String = String.Empty

            If m_dispatcherQueue.HasThreadAccess Then
                Return "Cannot wait for UI thread idle from the UI thread."
            End If

            Log = "LOG: "
            TickCountBegin = Environment.TickCount

            Dim isIdle As Boolean = False
            While Not isIdle
                Dim hadAnimations As Boolean = True
                Dim hadDeferredAnimationOperations As Boolean = True
                Dim hadBuildTreeWork As Boolean = False

                errorString = WaitForRootVisualReset()
                If errorString.Length > 0 Then
                    Return errorString
                End If
                AddLog("After WaitForRootVisualReset")

                errorString = WaitForImageDecodingIdle()
                If errorString.Length > 0 Then
                    Return errorString
                End If
                AddLog("After WaitForImageDecodingIdle")

                ' SynchronouslyTickUIThread(1);
                ' AddLog("After SynchronouslyTickUIThread(1)");

                errorString = WaitForFontDownloadsIdle()
                If errorString.Length > 0 Then
                    Return errorString
                End If
                AddLog("After WaitForFontDownloadsIdle")

                WaitForIdleDispatcher()
                AddLog("After WaitForIdleDispatcher")

                ' At this point, we know that the UI thread is idle - now we need to make sure
                ' that XAML isn't animating anything.
                ' TODO 27870237: Remove this #if once BuildTreeServiceDrained is properly signaled in WinUI desktop apps.
#If UNIVERSAL

                errorString = WaitForBuildTreeServiceWork(out hadBuildTreeWork);
                if (errorString.Length > 0) { return errorString; }
                AddLog("After WaitForBuildTreeServiceWork");

#End If

                ' The AnimationsComplete handle sometimes is never set in RS1,
                ' so we'll skip waiting for animations to complete
                ' if we've timed out once while waiting for animations in RS1.
                If Not m_waitForAnimationsIsDisabled Then
                    errorString = WaitForAnimationsComplete(hadAnimations)
                    If errorString.Length > 0 Then
                        Return errorString
                    End If
                    AddLog("After WaitForAnimationsComplete")
                Else
                    hadAnimations = False
                End If

                errorString = WaitForDeferredAnimationOperationsComplete(hadDeferredAnimationOperations)
                If errorString.Length > 0 Then
                    Return errorString
                End If
                AddLog("After WaitForDeferredAnimationOperationsComplete")

                ' In the case where we waited for an animation to complete there's a possibility that
                ' XAML, at the completion of the animation, scheduled a new tick. We will loop
                ' for as long as needed until we complete an idle dispatcher callback without
                ' waiting for a pending animation to complete.
                isIdle = Not hadAnimations AndAlso Not hadDeferredAnimationOperations AndAlso Not hadBuildTreeWork

                AddLog("IsIdle? " & isIdle)
            End While

            AddLog("End")

            logMessage = Log
            Return String.Empty
        End Function
        Private Function WaitForRootVisualReset() As String
            Dim waitResult As UInteger = NativeMethods.WaitForSingleObject(m_rootVisualResetHandle.NativeHandle, 5000)

            If waitResult <> NativeMethods.WAIT_OBJECT_0 AndAlso waitResult <> NativeMethods.WAIT_TIMEOUT Then
                Return "Waiting for root visual reset handle returned an invalid value."
            End If

            Return String.Empty
        End Function
        Private Function WaitForImageDecodingIdle() As String
            Dim waitResult As UInteger = NativeMethods.WaitForSingleObject(m_imageDecodingIdleHandle.NativeHandle, 5000)

            If waitResult <> NativeMethods.WAIT_OBJECT_0 AndAlso waitResult <> NativeMethods.WAIT_TIMEOUT Then
                Return "Waiting for image decoding idle handle returned an invalid value."
            End If

            Return String.Empty
        End Function
        Private Function WaitForFontDownloadsIdle() As String
            Dim waitResult As UInteger = NativeMethods.WaitForSingleObject(m_fontDownloadsIdleHandle.NativeHandle, 5000)

            If waitResult <> NativeMethods.WAIT_OBJECT_0 AndAlso waitResult <> NativeMethods.WAIT_TIMEOUT Then
                Return "Waiting for font downloads handle returned an invalid value."
            End If

            Return String.Empty
        End Function
        Private Sub WaitForIdleDispatcher()
            Dim shouldContinueEvent As New AutoResetEvent(False)

            ' DispatcherQueueTimer runs at below idle priority, so we can use it to ensure that we only raise the event when we're idle.
            Dim timer = m_dispatcherQueue.CreateTimer()
            timer.Interval = TimeSpan.FromMilliseconds(0)
            timer.IsRepeating = False

            Dim tickHandler As TypedEventHandler(Of DispatcherQueueTimer, Object) = Nothing

            tickHandler = Sub(sender, args)
                              timer.Tick -= tickHandler
                              shouldContinueEvent.[Set]()
                          End Sub

            timer.Tick += tickHandler

            timer.Start()
            shouldContinueEvent.WaitOne(s_defaultWaitForEventMs)
        End Sub
        Private Function WaitForBuildTreeServiceWork(<Out> ByRef hadBuildTreeWork As Boolean) As String
            hadBuildTreeWork = False
            Dim hasBuildTreeWork As Boolean = True

            ' We want to avoid an infinite loop, so we'll iterate 20 times before concluding that
            ' we probably are never going to become idle.
            Dim waitCount As Integer = 20

            While hasBuildTreeWork AndAlso Math.Max(Threading.Interlocked.Decrement(waitCount), waitCount + 1) > 0
                If Not NativeMethods.ResetEvent(m_buildTreeServiceDrainedHandle.NativeHandle) Then
                    Return "Failed to reset BuildTreeServiceDrained handle."
                End If

                Dim layoutUpdatedEvent As New AutoResetEvent(False)

                m_dispatcherQueue.TryEnqueue( _
                    DispatcherQueuePriority.Normal,
                    New DispatcherQueueHandler(Sub()
                                                   For Each window As Window In WindowHelper.ActiveWindows
                                                       If window.Content IsNot Nothing Then
                                                           window.Content.UpdateLayout()
                                                       End If
                                                   Next

                                                   layoutUpdatedEvent.[Set]()
                                               End Sub))

                layoutUpdatedEvent.WaitOne(s_defaultWaitForEventMs)

                ' This will be signaled if and only if Jupiter plans to at some point in the near
                ' future set the BuildTreeServiceDrained event.
                Dim waitResult As UInteger = NativeMethods.WaitForSingleObject(m_hasBuildTreeWorksHandle.NativeHandle, 0)

                If waitResult <> NativeMethods.WAIT_OBJECT_0 AndAlso waitResult <> NativeMethods.WAIT_TIMEOUT Then
                    Return "HasBuildTreeWork handle wait returned an invalid value."
                End If

                hasBuildTreeWork = (waitResult = NativeMethods.WAIT_OBJECT_0)
                AddLog("HasBuildTreeWork? " & hasBuildTreeWork)

                If hasBuildTreeWork Then
                    AddLog("Waiting for BuildTreeService to finish...")
                    waitResult = NativeMethods.WaitForSingleObject(m_buildTreeServiceDrainedHandle.NativeHandle, 10000)

                    If waitResult <> NativeMethods.WAIT_OBJECT_0 AndAlso waitResult <> NativeMethods.WAIT_TIMEOUT Then
                        Return "Wait for build tree service failed"
                    End If
                    AddLog("BuildTreeService drained")
                End If
            End While

            hadBuildTreeWork = hasBuildTreeWork
            Return String.Empty
        End Function
        Private Function WaitForAnimationsComplete(<Out> ByRef hadAnimations As Boolean) As String
            hadAnimations = False

            If Not NativeMethods.ResetEvent(m_animationsCompleteHandle.NativeHandle) Then
                Return "Failed to reset AnimationsComplete handle."
            End If

            AddLog("WaitForAnimationsComplete: After ResetEvent")

            ' This will be signaled if and only if XAML plans to at some point in the near
            ' future set the animations complete event.
            Dim waitResult As UInteger = NativeMethods.WaitForSingleObject(m_hasAnimationsHandle.NativeHandle, 0)

            If waitResult <> NativeMethods.WAIT_OBJECT_0 AndAlso waitResult <> NativeMethods.WAIT_TIMEOUT Then
                Return "HasAnimations handle wait returned an invalid value."
            End If

            AddLog("WaitForAnimationsComplete: After Wait(m_hasAnimationsHandle)")

            Dim hasAnimations As Boolean = (waitResult = NativeMethods.WAIT_OBJECT_0)

            If hasAnimations Then
                Dim animationCompleteWaitResult As UInteger = NativeMethods.WaitForSingleObject(m_animationsCompleteHandle.NativeHandle, s_idleTimeoutMs)

                AddLog("WaitForAnimationsComplete: HasAnimations, After Wait(m_animationsCompleteHandle)")

                If animationCompleteWaitResult <> NativeMethods.WAIT_OBJECT_0 Then
                    If Not IsRS2OrHigher() Then
                        ' The AnimationsComplete handle is sometimes just never signaled on RS1, ever.
                        ' If we run into this problem, we'll just disable waiting for animations to complete
                        ' and continue execution.  When the current test completes, we'll then close and reopen
                        ' the test app to minimize the effects of this problem.
                        m_waitForAnimationsIsDisabled = True

                        hadAnimations = False
                    End If

                    Return "Animation complete wait took longer than idle timeout."
                End If
            End If

            hadAnimations = hasAnimations
            Return String.Empty
        End Function
        Private Function WaitForDeferredAnimationOperationsComplete(<Out> ByRef hadDeferredAnimationOperations As Boolean) As String
            hadDeferredAnimationOperations = False

            If Not NativeMethods.ResetEvent(m_deferredAnimationOperationsCompleteHandle.NativeHandle) Then
                Return "Failed to reset DeferredAnimationOperations handle."
            End If

            ' This will be signaled if and only if XAML plans to at some point in the near
            ' future set the animations complete event.
            Dim waitResult As UInteger = NativeMethods.WaitForSingleObject(m_hasDeferredAnimationOperationsHandle.NativeHandle, 0)

            If waitResult <> NativeMethods.WAIT_OBJECT_0 AndAlso waitResult <> NativeMethods.WAIT_TIMEOUT Then
                Return "HasDeferredAnimationOperations handle wait returned an invalid value."
            End If

            Dim hasDeferredAnimationOperations As Boolean = (waitResult = NativeMethods.WAIT_OBJECT_0)

            If hasDeferredAnimationOperations Then
                Dim animationCompleteWaitResult As UInteger = NativeMethods.WaitForSingleObject(m_deferredAnimationOperationsCompleteHandle.NativeHandle, s_idleTimeoutMs)

                If animationCompleteWaitResult <> NativeMethods.WAIT_OBJECT_0 AndAlso animationCompleteWaitResult <> NativeMethods.WAIT_TIMEOUT Then
                    Return "Deferred animation operations complete wait took longer than idle timeout."
                End If
            End If

            hadDeferredAnimationOperations = hasDeferredAnimationOperations
            Return String.Empty
        End Function
        Private Sub SynchronouslyTickUIThread(ticks As UInteger)
            For i As UInteger = 0 To ticks - 1
                Dim tickCompleteEvent As New AutoResetEvent(False)

                m_dispatcherQueue.TryEnqueue( _
                    DispatcherQueuePriority.Normal,
                    New DispatcherQueueHandler(Sub()
                                                   Dim renderingHandler As EventHandler(Of Object) = Nothing

                                                   renderingHandler = Sub(sender As Object, args As Object)
                                                                          RemoveHandler CompositionTarget.Rendering, renderingHandler
                                                                          tickCompleteEvent.[Set]()
                                                                      End Sub

                                                   AddHandler CompositionTarget.Rendering, renderingHandler
                                               End Sub))

                tickCompleteEvent.WaitOne(s_defaultWaitForEventMs)
            Next
        End Sub
        Private Function IsRS2OrHigher() As Boolean
            If Not m_isRS2OrHigherInitialized Then
                m_isRS2OrHigherInitialized = True
                m_isRS2OrHigher = Windows.Foundation.Metadata.ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 4)
            End If

            Return m_isRS2OrHigher
        End Function
    End Class


    Friend Class Handle
        Private _nativeHandle As IntPtr
        Public Property NativeHandle As IntPtr
            Get
                Return _nativeHandle
            End Get
            Private Set(value As IntPtr)
                _nativeHandle = value
            End Set
        End Property
        Public ReadOnly Property IsValid As Boolean
            Get
                Return NativeHandle <> IntPtr.Zero
            End Get
        End Property

        Public Sub New(nativeHandle1 As IntPtr)
            Attach(nativeHandle1)
        End Sub
        Protected Overrides Sub Finalize()
            Release()
        End Sub
        Public Sub Attach(nativeHandle1 As IntPtr)
            Release()
            NativeHandle = nativeHandle1
        End Sub
        Public Function Detach() As IntPtr
            Dim returnValue As IntPtr = NativeHandle
            NativeHandle = IntPtr.Zero
            Return returnValue
        End Function
        Public Sub Release()
            NativeMethods.CloseHandle(NativeHandle)
            NativeHandle = IntPtr.Zero
        End Sub
    End Class


    Friend Module NativeMethods
        <DllImport("Kernel32.dll", SetLastError:=True)>
        Public Function OpenEvent(dwDesiredAccess As UInteger, bInheritHandle As Boolean, lpName As String) As IntPtr
        End Function
        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Function WaitForSingleObject(hHandle As IntPtr, dwMilliseconds As UInt32) As UInt32
        End Function
        Public Const INFINITE As UInt32 = &HFFFFFFFFUI
        Public Const WAIT_ABANDONED As UInt32 = &H00000080
        Public Const WAIT_OBJECT_0 As UInt32 = &H00000000
        Public Const WAIT_TIMEOUT As UInt32 = &H00000102
        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Function ResetEvent(hEvent As IntPtr) As Boolean
        End Function
        <DllImport("kernel32.dll", SetLastError:=True)>
        Public Function CloseHandle(hObject As IntPtr) As Boolean
        End Function
        <DllImport("kernel32.dll")>
        Public Function GetCurrentProcessId() As UInteger
        End Function
        <DllImport("kernel32.dll")>
        Public Function GetCurrentThreadId() As UInteger
        End Function
    End Module
    <Flags>
    Public Enum SyncObjectAccess As UInteger
        DELETE = &H00010000
        READ_CONTROL = &H00020000
        WRITE_DAC = &H00040000
        WRITE_OWNER = &H00080000
        SYNCHRONIZE = &H00100000
        EVENT_ALL_ACCESS = &H001F0003
        EVENT_MODIFY_STATE = &H00000002
        MUTEX_ALL_ACCESS = &H001F0001
        MUTEX_MODIFY_STATE = &H00000001
        SEMAPHORE_ALL_ACCESS = &H001F0003
        SEMAPHORE_MODIFY_STATE = &H00000002
        TIMER_ALL_ACCESS = &H001F0003
        TIMER_MODIFY_STATE = &H00000002
        TIMER_QUERY_STATE = &H00000001
    End Enum
End Namespace
