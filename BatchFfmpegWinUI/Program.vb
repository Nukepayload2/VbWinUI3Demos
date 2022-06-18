Option Strict On

Imports Microsoft.UI.Dispatching
Imports Microsoft.UI.Xaml
Imports Microsoft.VisualBasic.CompilerServices

Module Program

    Private Declare Sub XamlCheckProcessRequirements Lib "Microsoft.ui.xaml.dll" ()

    Sub Main()
        XamlCheckProcessRequirements
        WinRT.ComWrappersSupport.InitializeComWrappers()
        Microsoft.UI.Xaml.Application.Start(AddressOf OnAppInit)
    End Sub

    Private Sub OnAppInit(p As ApplicationInitializationCallbackParams)
        Dim synchronizationContext As New DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread())
        Threading.SynchronizationContext.SetSynchronizationContext(synchronizationContext)
        HostServices.VBHost = WinUIVbHost.Instance
        Dim app As New App
    End Sub
End Module
