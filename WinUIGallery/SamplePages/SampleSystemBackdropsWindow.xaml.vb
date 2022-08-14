' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports Windows.Foundation.Metadata
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media.Animation
Imports Microsoft.UI.Xaml.Navigation
Imports System.Runtime.InteropServices ' For DllImport
Imports WinRT ' required to support Window.As<ICompositionSupportsSystemBackdrop>()

Namespace AppUIBasics.SamplePages
    Class WindowsSystemDispatcherQueueHelper
        <StructLayout(LayoutKind.Sequential)>
        Structure DispatcherQueueOptions
            Friend dwSize As Integer
            Friend threadType As Integer
            Friend apartmentType As Integer
        End Structure
        <DllImport("CoreMessaging.dll")>
        Private Shared Function CreateDispatcherQueueController(<[In]> options As DispatcherQueueOptions, <[In], Out, MarshalAs(UnmanagedType.IUnknown)> ByRef dispatcherQueueController As Object) As Integer
        End Function
        Private m_dispatcherQueueController As Object = Nothing
        Public Sub EnsureWindowsSystemDispatcherQueueController()
            If Windows.System.DispatcherQueue.GetForCurrentThread() IsNot Nothing Then
                ' one already exists, so we'll just use it.
                Return
            End If

            If m_dispatcherQueueController Is Nothing Then
                Dim options As DispatcherQueueOptions
                options.dwSize = Marshal.SizeOf(GetType(DispatcherQueueOptions))
                options.threadType = 2    ' DQTYPE_THREAD_CURRENT
                options.apartmentType = 2 ' DQTAT_COM_STA

                CreateDispatcherQueueController(options, m_dispatcherQueueController)
            End If
        End Sub
    End Class


    Public NotInheritable Partial Class SampleSystemBackdropsWindow
        Inherits Window
        Public Sub New()
            Me.InitializeComponent()
            CType(Me.Content, FrameworkElement).RequestedTheme = AppUIBasics.Helper.ThemeHelper.RootTheme

            m_wsdqHelper = New WindowsSystemDispatcherQueueHelper
            m_wsdqHelper.EnsureWindowsSystemDispatcherQueueController()

            SetBackdrop(BackdropType.Mica)
        End Sub
        Public Enum BackdropType
            Mica
            DesktopAcrylic
            DefaultColor
        End Enum
        Private m_wsdqHelper As WindowsSystemDispatcherQueueHelper
        Private m_currentBackdrop As BackdropType
        Private m_micaController As Microsoft.UI.Composition.SystemBackdrops.MicaController
        Private m_acrylicController As Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController
        Private m_configurationSource As Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration
        Public Sub SetBackdrop(type As BackdropType)
            ' Reset to default color. If the requested type is supported, we'll update to that.
            ' Note: This sample completely removes any previous controller to reset to the default
            '       state. This is done so this sample can show what is expected to be the most
            '       common pattern of an app simply choosing one controller type which it sets at
            '       startup. If an app wants to toggle between Mica and Acrylic it could simply
            '       call RemoveSystemBackdropTarget() on the old controller and then setup the new
            '       controller, reusing any existing m_configurationSource and Activated/Closed
            '       event handlers.
            m_currentBackdrop = BackdropType.DefaultColor
            tbCurrentBackdrop.Text = "None (default theme color)"
            tbChangeStatus.Text = ""
            If m_micaController IsNot Nothing Then
                m_micaController.Dispose()
                m_micaController = Nothing
            End If
            If m_acrylicController IsNot Nothing Then
                m_acrylicController.Dispose()
                m_acrylicController = Nothing
            End If
            RemoveHandler Me.Activated, AddressOf Window_Activated
            RemoveHandler Me.Closed, AddressOf Window_Closed
            RemoveHandler CType(Me.Content, FrameworkElement).ActualThemeChanged, AddressOf Window_ThemeChanged
            m_configurationSource = Nothing

            If type = BackdropType.Mica Then
                If TrySetMicaBackdrop() Then
                    tbCurrentBackdrop.Text = "Mica"
                    m_currentBackdrop = type
                Else
                    ' Mica isn't supported. Try Acrylic.
                    type = BackdropType.DesktopAcrylic
                    tbChangeStatus.Text += "  Mica isn't supported. Trying Acrylic."
                End If
            End If
            If type = BackdropType.DesktopAcrylic Then
                If TrySetAcrylicBackdrop() Then
                    tbCurrentBackdrop.Text = "Acrylic"
                    m_currentBackdrop = type
                Else
                    ' Acrylic isn't supported, so take the next option, which is DefaultColor, which is already set.
                    tbChangeStatus.Text += "  Acrylic isn't supported. Switching to default color."
                End If
            End If
        End Sub
        Private Function TrySetMicaBackdrop() As Boolean
            If Microsoft.UI.Composition.SystemBackdrops.MicaController.IsSupported() Then
                ' Hooking up the policy object
                m_configurationSource = New Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration
                AddHandler Me.Activated, AddressOf Window_Activated
                AddHandler Me.Closed, AddressOf Window_Closed
                AddHandler CType(Me.Content, FrameworkElement).ActualThemeChanged, AddressOf Window_ThemeChanged

                ' Initial configuration state.
                m_configurationSource.IsInputActive = True
                SetConfigurationSourceTheme()

                m_micaController = New Microsoft.UI.Composition.SystemBackdrops.MicaController

                ' Enable the system backdrop.
                ' Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
                m_micaController.AddSystemBackdropTarget(Me.[As](Of Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop)())
                m_micaController.SetSystemBackdropConfiguration(m_configurationSource)
                Return True ' succeeded
            End If

            Return False ' Mica is not supported on this system
        End Function
        Private Function TrySetAcrylicBackdrop() As Boolean
            If Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController.IsSupported() Then
                ' Hooking up the policy object
                m_configurationSource = New Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration
                AddHandler Me.Activated, AddressOf Window_Activated
                AddHandler Me.Closed, AddressOf Window_Closed
                AddHandler CType(Me.Content, FrameworkElement).ActualThemeChanged, AddressOf Window_ThemeChanged

                ' Initial configuration state.
                m_configurationSource.IsInputActive = True
                SetConfigurationSourceTheme()

                m_acrylicController = New Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController

                ' Enable the system backdrop.
                ' Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
                m_acrylicController.AddSystemBackdropTarget(Me.[As](Of Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop)())
                m_acrylicController.SetSystemBackdropConfiguration(m_configurationSource)
                Return True ' succeeded
            End If

            Return False ' Acrylic is not supported on this system
        End Function
        Private Sub Window_Activated(sender As Object, args As WindowActivatedEventArgs)
            m_configurationSource.IsInputActive = args.WindowActivationState <> WindowActivationState.Deactivated
        End Sub
        Private Sub Window_Closed(sender As Object, args As WindowEventArgs)
            ' Make sure any Mica/Acrylic controller is disposed so it doesn't try to
            ' use this closed window.
            If m_micaController IsNot Nothing Then
                m_micaController.Dispose()
                m_micaController = Nothing
            End If
            If m_acrylicController IsNot Nothing Then
                m_acrylicController.Dispose()
                m_acrylicController = Nothing
            End If
            RemoveHandler Me.Activated, AddressOf Window_Activated
            m_configurationSource = Nothing
        End Sub
        Private Sub Window_ThemeChanged(sender As FrameworkElement, args As Object)
            If m_configurationSource IsNot Nothing Then
                SetConfigurationSourceTheme()
            End If
        End Sub
        Private Sub SetConfigurationSourceTheme()
            Select Case CType(Me.Content, FrameworkElement).ActualTheme
                Case ElementTheme.Dark
                    m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Dark
                Case ElementTheme.Light
                    m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Light
                Case ElementTheme.[Default]
                    m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.[Default]
            End Select
        End Sub
        Private Sub ChangeBackdropButton_Click(sender As Object, e As RoutedEventArgs)
            Dim newType As BackdropType
            Select Case m_currentBackdrop
                Case BackdropType.Mica
                    newType = BackdropType.DesktopAcrylic
                Case BackdropType.DesktopAcrylic
                    newType = BackdropType.DefaultColor
                Case Else
                    newType = BackdropType.Mica
            End Select
            SetBackdrop(newType)
        End Sub
    End Class
End Namespace
