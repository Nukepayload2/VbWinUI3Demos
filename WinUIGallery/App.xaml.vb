'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************

Imports AppUIBasics.Common
Imports AppUIBasics.Data
Imports AppUIBasics.Helper
Imports System
Imports System.Linq
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports Windows.ApplicationModel
Imports Windows.ApplicationModel.Activation
Imports Windows.ApplicationModel.Core
Imports Windows.Foundation.Metadata
Imports Windows.System.Profile
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics
    ''' <summary>
    ''' Provides application-specific behavior to supplement the default Application class.
    ''' </summary>
    Partial NotInheritable Class App
        Inherits Application
#If Not UNIVERSAL Then

        Private Shared s_startupWindow As Window
#End If
        ' Get the initial window created for this app
        ' On UWP, this is simply Window.Current
        ' On Desktop, multiple Windows may be created, and the StartupWindow may have already
        ' been closed.
        Public Shared ReadOnly Property StartupWindow As Window
            Get
                Return s_startupWindow
            End Get
        End Property

        Public Shared appTitleBar As UIElement = Nothing

        ''' <summary>
        ''' Initializes the singleton Application object.  This is the first line of authored code
        ''' executed, and as such is the logical equivalent of main() or WinMain().
        ''' </summary>
        Public Sub New()
            Me.InitializeComponent()

#If WINUI_PRERELEASE Then

            this.Suspending += OnSuspending;
            this.Resuming += App_Resuming;
            this.RequiresPointerMode = ApplicationRequiresPointerMode.WhenRequested;

#End If

            If ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 6) Then
                Me.FocusVisualKind = If(AnalyticsInfo.VersionInfo.DeviceFamily = "Xbox", FocusVisualKind.Reveal, FocusVisualKind.HighVisibility)
            End If
        End Sub
        Public Sub EnableSound(Optional withSpatial As Boolean = False)
            ElementSoundPlayer.State = ElementSoundPlayerState.[On]

            If Not withSpatial Then
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.Off
            Else ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.[On]
            End If
        End Sub
        Public Shared Function GetEnum(Of TEnum As Structure)(text As String) As TEnum
            If Not GetType(TEnum).GetTypeInfo().IsEnum Then
                Throw New InvalidOperationException("Generic parameter 'TEnum' must be an enum.")
            End If
            Return CType([Enum].Parse(GetType(TEnum), text), TEnum)
        End Function
#If WINUI_PRERELEASE Then

        private void App_Resuming(object sender, object e)
        {

#If UNIVERSAL

            NavigationRootPage navigationRootPage = (NavigationRootPage)Window.Current.Content;
            if (navigationRootPage != null)
            {
                navigationRootPage.App_Resuming();
            }

#End If

        }

#End If

        ''' <summary>
        ''' Invoked when the application is launched normally by the end user.  Other entry points
        ''' will be used such as when the application is launched to open a specific file.
        ''' </summary>
        ''' <param name="e">Details about the launch request and process.</param>
        Protected Overrides Sub OnLaunched(args As Microsoft.UI.Xaml.LaunchActivatedEventArgs)
            IdleSynchronizer.Init()

#If UNIVERSAL Then

            WindowHelper.TrackWindow(Window.Current);

#Else

            s_startupWindow = WindowHelper.CreateWindow()
#End If

#If DEBUGThen

            //if (System.Diagnostics.Debugger.IsAttached)
            //{
            //    this.DebugSettings.EnableFrameRateCounter = true;
            //}

            If (System.Diagnostics.Debugger.IsAttached) Then
                            {
                this.DebugSettings.BindingFailed += DebugSettings_BindingFailed();
            }

#End If

            'draw into the title bar

#If UNIVERSALThen

            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

#End If

#If Not UNIVERSALThen

            ' args.UWPLaunchActivatedEventArgs throws an InvalidCastException in desktop apps.
            EnsureWindow()
#Else

            EnsureWindow(args.UWPLaunchActivatedEventArgs);

#End If

        End Sub
        Private Sub DebugSettings_BindingFailed(sender As Object, e As BindingFailedEventArgs)
        End Sub
#If WINUI_PRERELEASE Then

        protected override void OnActivated(IActivatedEventArgs args)
        {
            EnsureWindow(args);
        }

#End If

        Private Async Sub EnsureWindow(Optional args As IActivatedEventArgs = Nothing)
            Dim match1 As Match
            ' TODO Check: Local function was replaced with Lambda
            Dim IsMatching As Func(Of String, String, Boolean) = Function(parent As String, expression As String) As Boolean

                                                                     match1 = Regex.Match(parent, expression)
                                                                     Return match1.Success
                                                                 End Function

            ' No matter what our destination is, we're going to need control data loaded - let's knock that out now.
            ' We'll never need to do this again.
            Await ControlInfoDataSource.Instance.GetGroupsAsync()

            Dim rootFrame As Frame = GetRootFrame()

            ThemeHelper.Initialize()

            Dim targetPageType As Type = GetType(NewControlsPage)
            Dim targetPageArguments As String = String.Empty

            If args IsNot Nothing Then
                If args.Kind = ActivationKind.Launch Then
                    If args.PreviousExecutionState = ApplicationExecutionState.Terminated Then
                        Try
                            Await SuspensionManager.RestoreAsync()
                        Catch __unusedSuspensionManagerException1__ As SuspensionManagerException

                        End Try
                    End If

                    targetPageArguments = CType(args, Windows.ApplicationModel.Activation.LaunchActivatedEventArgs).Arguments
                ElseIf args.Kind = ActivationKind.Protocol Then

                    Dim targetId As String = String.Empty

                    Select Case True
                        Case TypeOf CType(args, ProtocolActivatedEventArgs).Uri?.AbsoluteUri Is String
                            Dim s As String = CType(CType(args, ProtocolActivatedEventArgs).Uri?.AbsoluteUri, String)
                            If IsMatching(s, "(/*)category/(.*)") Then Exit Select
                            targetId = match1.Groups(2)?.ToString()
                            If targetId = "AllControls" Then
                                targetPageType = GetType(AllControlsPage)
                            ElseIf targetId = "NewControls" Then
                                targetPageType = GetType(NewControlsPage)
                            ElseIf ControlInfoDataSource.Instance.Groups.Any(Function(g) g.UniqueId = targetId) Then
                                targetPageType = GetType(SectionPage)
                            End If

                        Case TypeOf CType(args, ProtocolActivatedEventArgs).Uri?.AbsoluteUri Is String
                            Dim s As String = CType(CType(args, ProtocolActivatedEventArgs).Uri?.AbsoluteUri, String)
                            If IsMatching(s, "(/*)item/(.*)") Then Exit Select
                            targetId = match1.Groups(2)?.ToString()
                            If ControlInfoDataSource.Instance.Groups.Any(Function(g) g.Items.Any(Function(i) i.UniqueId = targetId)) Then
                                targetPageType = GetType(ItemPage)
                            End If
                    End Select

                    targetPageArguments = targetId
                End If
            End If

            Dim rootPage As NavigationRootPage = TryCast(startupWindow.Content, NavigationRootPage)
            rootPage.Navigate(targetPageType, targetPageArguments)

            If targetPageType Is GetType(NewControlsPage) Then
                CType(CType(App.startupWindow.Content, NavigationRootPage).NavigationView.MenuItems(0), Microsoft.UI.Xaml.Controls.NavigationViewItem).IsSelected = True
            ElseIf targetPageType Is GetType(ItemPage) Then
                NavigationRootPage.GetForElement(Me).EnsureNavigationSelection(targetPageArguments)
            End If

            ' Ensure the current window is active
            startupWindow.Activate()
        End Sub
        Private Function GetRootFrame() As Frame
            Dim rootFrame As Frame
            Dim rootPage As NavigationRootPage = TryCast(startupWindow.Content, NavigationRootPage)
            If rootPage Is Nothing Then
                rootPage = New NavigationRootPage
                rootFrame = CType(rootPage.FindName("rootFrame"), Frame)
                If rootFrame Is Nothing Then
                    Throw New Exception("Root frame not found")
                End If
                SuspensionManager.RegisterFrame(rootFrame, "AppFrame")
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages(0)
                AddHandler rootFrame.NavigationFailed, AddressOf OnNavigationFailed

                startupWindow.Content = rootPage
            Else
                rootFrame = CType(rootPage.FindName("rootFrame"), Frame)
            End If

            Return rootFrame
        End Function
        ''' <summary>
        ''' Invoked when Navigation to a certain page fails
        ''' </summary>
        ''' <param name="sender">The Frame which failed navigation</param>
        ''' <param name="e">Details about the navigation failure</param>
        Private Sub OnNavigationFailed(sender As Object, e As NavigationFailedEventArgs)
            Throw New Exception("Failed to load Page " & e.SourcePageType.FullName)
        End Sub

#If WINUI_PRERELEASE Then

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

#End If ' WINUI_PRERELEASE

    End Class
End Namespace
