Imports Microsoft.UI.Composition
Imports Microsoft.UI.Composition.SystemBackdrops
Imports Microsoft.UI.Xaml
Imports Windows.Foundation
Imports WinRT

' Copied from https://github.com/microsoft/WinUI-Gallery

Public Class BackdropHelper
    Private ReadOnly window As Window
    Private ReadOnly m_wsdqHelper As WindowsSystemDispatcherQueueHelper
    Private m_currentBackdrop? As BackdropType = Nothing
    Private m_micaController As MicaController
    Private m_acrylicController As DesktopAcrylicController
    Private m_configurationSource As SystemBackdropConfiguration

    Public Event BackdropTypeChanged As TypedEventHandler(Of BackdropHelper, Object)

    Public Sub New(window As Window)
        Me.window = window
        m_wsdqHelper = New WindowsSystemDispatcherQueueHelper()
        m_wsdqHelper.EnsureWindowsSystemDispatcherQueueController()
    End Sub

    Public Sub SetBackdrop(type As BackdropType, ByRef useAcrylic As Boolean)
        If m_currentBackdrop.Equals(type) Then
            Return
        End If

        ' Reset to default color. If the requested type is supported, we'll update to that.
        ' Note: This sample completely removes any previous controller to reset to the default
        '       state. This is done so this sample can show what is expected to be the most
        '       common pattern of an app simply choosing one controller type which it sets at
        '       startup. If an app wants to toggle between Mica and Acrylic it could simply
        '       call RemoveSystemBackdropTarget() on the old controller and then setup the new
        '       controller, reusing any existing m_configurationSource and Activated/Closed
        '       event handlers.
        m_currentBackdrop = BackdropType.DefaultColor
        If m_micaController IsNot Nothing Then
            m_micaController.Dispose()
            m_micaController = Nothing
        End If
        If m_acrylicController IsNot Nothing Then
            m_acrylicController.Dispose()
            m_acrylicController = Nothing
        End If
        RemoveHandler window.Activated, AddressOf Window_Activated
        RemoveHandler window.Closed, AddressOf Window_Closed
        RemoveHandler CType(window.Content, FrameworkElement).ActualThemeChanged, AddressOf Window_ThemeChanged
        m_configurationSource = Nothing

        If type = BackdropType.Mica Then
            If TrySetMicaBackdrop() Then
                m_currentBackdrop = type
            Else
                ' Mica isn't supported. Try Acrylic.
                type = BackdropType.DesktopAcrylic
            End If
        End If
        If type = BackdropType.DesktopAcrylic Then
            If TrySetAcrylicBackdrop() Then
                m_currentBackdrop = type
                useAcrylic = True
            End If
        End If

        BackdropTypeChangedEvent?.Invoke(Me, m_currentBackdrop)
    End Sub

    Private Function TrySetMicaBackdrop() As Boolean
        If MicaController.IsSupported() Then
            ' Hooking up the policy object
            m_configurationSource = New SystemBackdropConfiguration()
            AddHandler window.Activated, AddressOf Window_Activated
            AddHandler window.Closed, AddressOf Window_Closed
            AddHandler CType(window.Content, FrameworkElement).ActualThemeChanged, AddressOf Window_ThemeChanged

            ' Initial configuration state.
            m_configurationSource.IsInputActive = True
            SetConfigurationSourceTheme()

            m_micaController = New MicaController()

            ' Enable the system backdrop.
            ' Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
            m_micaController.AddSystemBackdropTarget(CastExtensions.As(Of ICompositionSupportsSystemBackdrop)(window))
            m_micaController.SetSystemBackdropConfiguration(m_configurationSource)
            Return True ' succeeded
        End If

        Return False ' Mica is not supported on this system
    End Function

    Private Function TrySetAcrylicBackdrop() As Boolean
        If DesktopAcrylicController.IsSupported() Then
            ' Hooking up the policy object
            m_configurationSource = New SystemBackdropConfiguration()
            AddHandler window.Activated, AddressOf Window_Activated
            AddHandler window.Closed, AddressOf Window_Closed
            AddHandler CType(window.Content, FrameworkElement).ActualThemeChanged, AddressOf Window_ThemeChanged

            ' Initial configuration state.
            m_configurationSource.IsInputActive = True
            SetConfigurationSourceTheme()

            m_acrylicController = New DesktopAcrylicController()

            ' Enable the system backdrop.
            ' Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
            m_acrylicController.AddSystemBackdropTarget(CastExtensions.As(Of ICompositionSupportsSystemBackdrop)(window))
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
        RemoveHandler window.Activated, AddressOf Window_Activated
        m_configurationSource = Nothing
    End Sub

    Private Sub Window_ThemeChanged(sender As FrameworkElement, args As Object)
        If m_configurationSource IsNot Nothing Then
            SetConfigurationSourceTheme()
        End If
    End Sub

    Private Sub SetConfigurationSourceTheme()
        Select Case (CType(window.Content, FrameworkElement)).ActualTheme
            Case ElementTheme.Dark
                m_configurationSource.Theme = SystemBackdropTheme.Dark
            Case ElementTheme.Light
                m_configurationSource.Theme = SystemBackdropTheme.Light
            Case ElementTheme.Default
                m_configurationSource.Theme = SystemBackdropTheme.Default
        End Select
    End Sub
End Class
