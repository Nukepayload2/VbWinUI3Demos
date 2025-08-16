Imports Microsoft.UI.Dispatching
Imports Microsoft.UI.Xaml

Partial Module Program
    Sub Main()
        Application.Start(AddressOf OnAppInit)
    End Sub

    Private Sub OnAppInit(p As ApplicationInitializationCallbackParams)
        Dim synchronizationContext As New DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread())
        Threading.SynchronizationContext.SetSynchronizationContext(synchronizationContext)
        Dim app As New App
    End Sub
End Module
