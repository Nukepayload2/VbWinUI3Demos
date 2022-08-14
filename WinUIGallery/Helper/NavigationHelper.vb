' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Input
Imports Windows.ApplicationModel.Core
Imports Windows.Foundation.Metadata
Imports Windows.System
Imports Microsoft.UI.Dispatching
Imports Microsoft.UI.Windowing
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Navigation
Imports AppUIBasics.Common

#If UNIVERSAL

using Windows.UI.Core;
using Windows.UI.ViewManagement;

#End If

Namespace AppUIBasics.Helper
    ''' <summary>
    ''' NavigationHelper aids in navigation between pages. It manages
    ''' the backstack and integrates SuspensionManager to handle process
    ''' lifetime management and state management when navigating between pages.
    ''' </summary>
    ''' <example>
    ''' To make use of NavigationHelper, follow these two steps or
    ''' start with a BasicPage or any other Page item template other than BlankPage.
    '''
    ''' 1) Create an instance of the NavigationHelper somewhere such as in the
    '''     constructor for the page and register a callback for the LoadState and
    '''     SaveState events.
    ''' <code>
    '''     public MyPage()
    '''     {
    '''         this.InitializeComponent();
    '''         this.navigationHelper = new NavigationHelper(this);
    '''         this.navigationHelper.LoadState += navigationHelper_LoadState;
    '''         this.navigationHelper.SaveState += navigationHelper_SaveState;
    '''     }
    '''
    '''     private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
    '''     { }
    '''     private void navigationHelper_SaveState(object sender, LoadStateEventArgs e)
    '''     { }
    ''' </code>
    '''
    ''' 2) Register the page to call into the NavigationManager whenever the page participates
    '''     in navigation by overriding the <see cref="Microsoft.UI.Xaml.Controls.Page.OnNavigatedTo"/>
    '''     and <see cref="Microsoft.UI.Xaml.Controls.Page.OnNavigatedFrom"/> events.
    ''' <code>
    '''     protected override void OnNavigatedTo(NavigationEventArgs e)
    '''     {
    '''         navigationHelper.OnNavigatedTo(e);
    '''     }
    '''
    '''     protected override void OnNavigatedFrom(NavigationEventArgs e)
    '''     {
    '''         navigationHelper.OnNavigatedFrom(e);
    '''     }
    ''' </code>
    ''' </example>
    <Windows.Foundation.Metadata.WebHostHidden>
    Public Class NavigationHelper
        Inherits DependencyObject
        Private Property Page As Page
        Private ReadOnly Property Frame As Frame
            Get
                Return Me.Page.Frame
            End Get
        End Property

        ''' <summary>
        ''' Initializes a new instance of the <see cref="NavigationHelper"/> class.
        ''' </summary>
        ''' <param name="page">A reference to the current page used for navigation.
        ''' This reference allows for frame manipulation.</param>
        Public Sub New(page1 As Page)
            Me.Page = page1
        End Sub
#Region "Process lifetime management"

        Private _pageKey As String

        ''' <summary>
        ''' Handle this event to populate the page using content passed
        ''' during navigation as well as any state that was saved by
        ''' the SaveState event handler.
        ''' </summary>
        Public Event LoadState As LoadStateEventHandler
        ''' <summary>
        ''' Handle this event to save state that can be used by
        ''' the LoadState event handler. Save the state in case
        ''' the application is suspended or the page is discarded
        ''' from the navigation cache.
        ''' </summary>
        Public Event SaveState As SaveStateEventHandler
        ''' <summary>
        ''' Invoked when this page is about to be displayed in a Frame.
        ''' This method calls <see cref="LoadState"/>, where all page specific
        ''' navigation and process lifetime management logic should be placed.
        ''' </summary>
        ''' <param name="e">Event data that describes how this page was reached.  The Parameter
        ''' property provides the group to be displayed.</param>
        Public Sub OnNavigatedTo(e As NavigationEventArgs)
            Dim frameState = SuspensionManager.SessionStateForFrame(Me.Frame)
            Me._pageKey = "Page-" & Me.Frame.BackStackDepth

            If e.NavigationMode = NavigationMode.[New] Then
                ' Clear existing state for forward navigation when adding a new page to the
                ' navigation stack
                Dim nextPageKey As String = Me._pageKey
                Dim nextPageIndex As Integer = Me.Frame.BackStackDepth
                While frameState.Remove(nextPageKey)
                    nextPageIndex += 1
                    nextPageKey = "Page-" & nextPageIndex
                End While

                ' Pass the navigation parameter to the new page
                RaiseEvent LoadState(Me, New LoadStateEventArgs(e.Parameter, Nothing))
            Else
                ' Pass the navigation parameter and preserved page state to the page, using
                ' the same strategy for loading suspended state and recreating pages discarded
                ' from cache
                RaiseEvent LoadState(Me, New LoadStateEventArgs(e.Parameter, CType(frameState(Me._pageKey), Dictionary(Of String, Object))))
            End If
        End Sub
        ''' <summary>
        ''' Invoked when this page will no longer be displayed in a Frame.
        ''' This method calls <see cref="SaveState"/>, where all page specific
        ''' navigation and process lifetime management logic should be placed.
        ''' </summary>
        ''' <param name="e">Event data that describes how this page was reached.  The Parameter
        ''' property provides the group to be displayed.</param>
        Public Sub OnNavigatedFrom(e As NavigationEventArgs)
            Dim frameState = SuspensionManager.SessionStateForFrame(Me.Frame)
            Dim pageState As Collections.Generic.Dictionary(Of String, Object) = New Dictionary(Of String, Object)
            RaiseEvent SaveState(Me, New SaveStateEventArgs(pageState))
            frameState(_pageKey) = pageState
        End Sub

#End Region

    End Class
    ''' <summary>
    ''' RootFrameNavigationHelper registers for standard mouse and keyboard
    ''' shortcuts used to go back and forward. There should be only one
    ''' RootFrameNavigationHelper per view, and it should be associated with the
    ''' root frame.
    ''' </summary>
    ''' <example>
    ''' To make use of RootFrameNavigationHelper, create an instance of the
    ''' RootNavigationHelper such as in the constructor of your root page.
    ''' <code>
    '''     public MyRootPage()
    '''     {
    '''         this.InitializeComponent();
    '''         this.rootNavigationHelper = new RootNavigationHelper(MyFrame);
    '''     }
    ''' </code>
    ''' </example>
    <Windows.Foundation.Metadata.WebHostHidden>
    Public Class RootFrameNavigationHelper
        Private Property Frame As Frame
#If UNIVERSAL

        SystemNavigationManager systemNavigationManager;

#End If

        Private Property CurrentNavView As Microsoft.UI.Xaml.Controls.NavigationView

        ''' <summary>
        ''' Initializes a new instance of the <see cref="RootNavigationHelper"/> class.
        ''' </summary>
        ''' <param name="rootFrame">A reference to the top-level frame.
        ''' This reference allows for frame manipulation and to register navigation handlers.</param>
        Public Sub New(rootFrame As Frame, currentNavView1 As Microsoft.UI.Xaml.Controls.NavigationView)
            Me.Frame = rootFrame
            AddHandler Me.Frame.Navigated, Sub(s, e)
                                               ' Update the Back button whenever a navigation occurs.
                                               UpdateBackButton()
                                           End Sub
            Me.CurrentNavView = currentNavView1

#If UNIVERSAL

            // Handle keyboard and mouse navigation requests
            this.systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            systemNavigationManager.BackRequested += SystemNavigationManager_BackRequested;

#End If

            ' must register back requested on navview
            If ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 6) Then
                AddHandler CurrentNavView.BackRequested, AddressOf NavView_BackRequested
            End If

            ' #if UNIVERSAL
            '             // Listen to the window directly so we will respond to hotkeys regardless
            '             // of which element has focus.
            '             CoreWindow.GetForCurrentThread().Dispatcher.AcceleratorKeyActivated +=
            '                 CoreDispatcher_AcceleratorKeyActivated;
            '             CoreWindow.GetForCurrentThread().PointerPressed +=
            '                 this.CoreWindow_PointerPressed;
            ' #endif
        End Sub
        Private Sub NavView_BackRequested(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs)
            TryGoBack()
        End Sub
        Private Function TryGoBack() As Boolean
            ' don't go back if the nav pane is overlayed
            If Me.CurrentNavView.IsPaneOpen AndAlso (Me.CurrentNavView.DisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode.Compact OrElse Me.CurrentNavView.DisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode.Minimal) Then
                Return False
            End If

            Dim navigated1 As Boolean = False
            If Me.Frame.CanGoBack Then
                Me.Frame.GoBack()
                navigated1 = True
            End If

            Return navigated1
        End Function
        Private Function TryGoForward() As Boolean
            Dim navigated1 As Boolean = False
            If Me.Frame.CanGoForward Then
                Me.Frame.GoForward()
                navigated1 = True
            End If
            Return navigated1
        End Function
#If UNIVERSAL

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = TryGoBack();
            }
        }

#End If

        Private Sub UpdateBackButton()
            If ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 6) Then
                Me.CurrentNavView.IsBackEnabled = If(Me.Frame.CanGoBack, True, False)
            End If
#If UNIVERSAL

            else
            {
                systemNavigationManager.AppViewBackButtonVisibility = this.Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            }

#End If

        End Sub

        ' /// <summary>
        ' /// Invoked on every keystroke, including system keys such as Alt key combinations.
        ' /// Used to detect keyboard navigation between pages even when the page itself
        ' /// doesn't have focus.
        ' /// </summary>
        ' /// <param name="sender">Instance that triggered the event.</param>
        ' /// <param name="e">Event data describing the conditions that led to the event.</param>
        ' private void CoreDispatcher_AcceleratorKeyActivated(DispatcherQueue sender,
        '     AcceleratorKeyEventArgs e)
        ' {
        '     var virtualKey = e.VirtualKey;

        '     // Only investigate further when Left, Right, or the dedicated Previous or Next keys
        '     // are pressed
        '     if ((e.EventType == CoreAcceleratorKeyEventType.SystemKeyDown ||
        '         e.EventType == CoreAcceleratorKeyEventType.KeyDown) &&
        '         (virtualKey == VirtualKey.Left || virtualKey == VirtualKey.Right ||
        '         (int)virtualKey == 166 || (int)virtualKey == 167))
        '     {
        '         var downState = CoreVirtualKeyStates.Down;
        '         bool menuKey = (Microsoft.UI.Input.InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Menu) & downState) == downState;
        '         bool controlKey = (Microsoft.UI.Input.InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Control) & downState) == downState;
        '         bool shiftKey = (Microsoft.UI.Input.InputKeyboardSource.GetKeyStateForCurrentThread(VirtualKey.Shift) & downState) == downState;
        '         bool noModifiers = !menuKey && !controlKey && !shiftKey;
        '         bool onlyAlt = menuKey && !controlKey && !shiftKey;

        '         if (((int)virtualKey == 166 && noModifiers) ||
        '             (virtualKey == VirtualKey.Left && onlyAlt))
        '         {
        '             // When the previous key or Alt+Left are pressed navigate back
        '             e.Handled = TryGoBack();
        '         }
        '         else if (((int)virtualKey == 167 && noModifiers) ||
        '             (virtualKey == VirtualKey.Right && onlyAlt))
        '         {
        '             // When the next key or Alt+Right are pressed navigate forward
        '             e.Handled = TryGoForward();
        '         }
        '     }
        ' }

        ' /// <summary>
        ' /// Invoked on every mouse click, touch screen tap, or equivalent interaction.
        ' /// Used to detect browser-style next and previous mouse button clicks
        ' /// to navigate between pages.
        ' /// </summary>
        ' /// <param name="sender">Instance that triggered the event.</param>
        ' /// <param name="e">Event data describing the conditions that led to the event.</param>
        ' private void CoreWindow_PointerPressed(CoreWindow sender,
        '     PointerEventArgs e)
        ' {
        '     var properties = e.CurrentPoint.Properties;

        '     // Ignore button chords with the left, right, and middle buttons
        '     if (properties.IsLeftButtonPressed || properties.IsRightButtonPressed ||
        '         properties.IsMiddleButtonPressed)
        '         return;

        '     // If back or forward are pressed (but not both) navigate appropriately
        '     bool backPressed = properties.IsXButton1Pressed;
        '     bool forwardPressed = properties.IsXButton2Pressed;
        '     if (backPressed ^ forwardPressed)
        '     {
        '         e.Handled = true;
        '         if (backPressed) this.TryGoBack();
        '         if (forwardPressed) this.TryGoForward();
        '     }
        ' }
    End Class

    ''' <summary>
    ''' Represents the method that will handle the <see cref="NavigationHelper.LoadState"/>event
    ''' </summary>
    Public Delegate Sub LoadStateEventHandler(sender As Object, e As LoadStateEventArgs)
    ''' <summary>
    ''' Represents the method that will handle the <see cref="NavigationHelper.SaveState"/>event
    ''' </summary>
    Public Delegate Sub SaveStateEventHandler(sender As Object, e As SaveStateEventArgs)

    ''' <summary>
    ''' Class used to hold the event data required when a page attempts to load state.
    ''' </summary>
    Public Class LoadStateEventArgs
        Inherits EventArgs
        ''' <summary>
        ''' The parameter value passed to <see cref="Frame.Navigate(Type,Object)"/>
        ''' when this page was initially requested.
        ''' </summary>
        Private _navigationParameter As Object
        Public Property NavigationParameter As Object
            Get
                Return _navigationParameter
            End Get
            Private Set(value As Object)
                _navigationParameter = value
            End Set
        End Property
        ''' <summary>
        ''' A dictionary of state preserved by this page during an earlier
        ''' session.  This will be null the first time a page is visited.
        ''' </summary>
        Private _pageState As Dictionary(Of String, Object)
        Public Property PageState As Dictionary(Of String, Object)
            Get
                Return _pageState
            End Get
            Private Set(value As Dictionary(Of String, Object))
                _pageState = value
            End Set
        End Property

        ''' <summary>
        ''' Initializes a new instance of the <see cref="LoadStateEventArgs"/> class.
        ''' </summary>
        ''' <param name="navigationParameter">
        ''' The parameter value passed to <see cref="Frame.Navigate(Type,Object)"/>
        ''' when this page was initially requested.
        ''' </param>
        ''' <param name="pageState">
        ''' A dictionary of state preserved by this page during an earlier
        ''' session.  This will be null the first time a page is visited.
        ''' </param>
        Public Sub New(navigationParameter1 As Object, pageState1 As Dictionary(Of String, Object))
            MyBase.New()
            Me.NavigationParameter = navigationParameter1
            Me.PageState = pageState1
        End Sub
    End Class
    ''' <summary>
    ''' Class used to hold the event data required when a page attempts to save state.
    ''' </summary>
    Public Class SaveStateEventArgs
        Inherits EventArgs
        ''' <summary>
        ''' An empty dictionary to be populated with serializable state.
        ''' </summary>
        Private _pageState As Dictionary(Of String, Object)
        Public Property PageState As Dictionary(Of String, Object)
            Get
                Return _pageState
            End Get
            Private Set(value As Dictionary(Of String, Object))
                _pageState = value
            End Set
        End Property

        ''' <summary>
        ''' Initializes a new instance of the <see cref="SaveStateEventArgs"/> class.
        ''' </summary>
        ''' <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        Public Sub New(pageState1 As Dictionary(Of String, Object))
            MyBase.New()
            Me.PageState = pageState1
        End Sub
    End Class
End Namespace
