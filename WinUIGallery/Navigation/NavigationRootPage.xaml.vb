'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************

Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On

Imports AppUIBasics.Common
Imports AppUIBasics.Data
Imports AppUIBasics.Helper
Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Threading.Tasks
Imports Windows.ApplicationModel.Core
Imports Windows.Devices.Input
Imports Windows.Foundation
Imports Windows.Foundation.Metadata
Imports Windows.Gaming.Input
Imports Windows.System.Profile
Imports Windows.UI.ViewManagement
Imports Microsoft.UI.Dispatching
Imports Microsoft.UI
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Automation
Imports Microsoft.UI.Xaml.Input
Imports Microsoft.UI.Xaml.Media
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics
    Public NotInheritable Partial Class NavigationRootPage
        Inherits Page
        Public ArrowKey As Windows.System.VirtualKey
        Private _navHelper As RootFrameNavigationHelper
        Private _isGamePadConnected As Boolean
        Private _isKeyboardConnected As Boolean
        Private _allControlsMenuItem As Microsoft.UI.Xaml.Controls.NavigationViewItem
        Private _newControlsMenuItem As Microsoft.UI.Xaml.Controls.NavigationViewItem
        Public Shared Function GetForElement(obj As Object) As NavigationRootPage
            Dim element As UIElement = CType(obj, UIElement)
            Dim window1 As Window = WindowHelper.GetWindowForElement(element)
            If window1 IsNot Nothing Then
                Return CType(window1.Content, NavigationRootPage)
            End If
            Return Nothing
        End Function
        Public ReadOnly Property NavigationView As Microsoft.UI.Xaml.Controls.NavigationView
            Get
                Return NavigationViewControl
            End Get
        End Property

        Public Property NavigationViewLoaded As Action

        Public Property DeviceFamily As DeviceType
        Public ReadOnly Property IsFocusSupported As Boolean
            Get
                Return DeviceFamily = DeviceType.Xbox OrElse _isGamePadConnected OrElse _isKeyboardConnected
            End Get
        End Property
        Public ReadOnly Property PageHeader As PageHeader
            Get
                Return UIHelper.GetDescendantsOfType(Of PageHeader)(NavigationViewControl).FirstOrDefault()
            End Get
        End Property
        Public ReadOnly Property AppTitleText As String
            Get
#If Not UNIVERSAL And DEBUG Then
                Return "WinUI 3 Gallery Dev"
#ElseIf Not UNIVERSAL Then
                Return "WinUI 3 Gallery"
#ElseIf DEBUG
                return "WinUI 3 Gallery Dev (UWP)";
#Else
                return "WinUI 3 Gallery (UWP)";
#End If
            End Get
        End Property

        Public Sub New()
            Me.InitializeComponent()

            ' Workaround for VisualState issue that should be fixed
            ' by https://github.com/microsoft/microsoft-ui-xaml/pull/2271
            NavigationViewControl.PaneDisplayMode = NavigationViewPaneDisplayMode.Left

            _navHelper = New RootFrameNavigationHelper(rootFrame, NavigationViewControl)

            SetDeviceFamily()
            AddNavigationMenuItems()

            Me.GotFocus += Sub(sender As Object, e As RoutedEventArgs)
                               ' helpful for debugging focus problems w/ keyboard & gamepad
                               Dim TempVar1 As Boolean = TypeOf FocusManager.GetFocusedElement() Is FrameworkElement
                               Dim focus As FrameworkElement = FocusManager.GetFocusedElement()
                               If TempVar1 Then
                                   Debug.WriteLine("got focus: " & focus.Name & " (" & focus.[GetType]().ToString() & ")")
                               End If
                           End Sub
            AddHandler Gamepad.GamepadAdded, AddressOf OnGamepadAdded
            AddHandler Gamepad.GamepadRemoved, AddressOf OnGamepadRemoved

#If UNIVERSAL

            CoreApplication.GetCurrentView().TitleBar.LayoutMetricsChanged += (s, e) => UpdateAppTitle(s);

#End If

            _isKeyboardConnected = Convert.ToBoolean(New KeyboardCapabilities.KeyboardPresent)

            ' remove the solid-colored backgrounds behind the caption controls and system back button if we are in left mode
            ' This is done when the app is loaded since before that the actual theme that is used is not "determined" yet
            Loaded += Sub(sender As Object, e As RoutedEventArgs)
                          NavigationOrientationHelper.UpdateTitleBarForElement(NavigationOrientationHelper.IsLeftMode(), Me)
#If Not UNIVERSAL

                          WindowHelper.GetWindowForElement(Me).Title = AppTitleText
#End If

                      End Sub

            NavigationViewControl.RegisterPropertyChangedCallback(NavigationView.PaneDisplayModeProperty, New DependencyPropertyChangedCallback(AddressOf OnPaneDisplayModeChanged))

            ' Set the titlebar to be custom. This is also referenced by the TitleBarPage
            App.appTitleBar = AppTitleBar
#If Not UNIVERSAL

            Dim window1 = App.StartupWindow
            window1.ExtendsContentIntoTitleBar = True
            window1.SetTitleBar(AppTitleBar)
#End If

        End Sub
        Private Sub OnPaneDisplayModeChanged(sender As DependencyObject, dp As DependencyProperty)
            Dim navigationView1 = TryCast(sender, NavigationView)
            NavigationRootPage.GetForElement(Me).AppTitleBar.Visibility = If(navigationView1.PaneDisplayMode = NavigationViewPaneDisplayMode.Top, Visibility.Collapsed, Visibility.Visible)
        End Sub
        Private Sub UpdateAppTitle(coreTitleBar As CoreApplicationViewTitleBar)
            'ensure the custom title bar does not overlap window caption controls
            Dim currMargin As Thickness = AppTitleBar.Margin
            AppTitleBar.Margin = New Thickness() With
{
                .Left = currMargin.Left,
                .Top = currMargin.Top,
                .Right = coreTitleBar.SystemOverlayRightInset,
                .Bottom = currMargin.Bottom}
        End Sub
        Public Function CheckNewControlSelected() As Boolean
            Return _newControlsMenuItem.IsSelected
        End Function
        ' Wraps a call to rootFrame.Navigate to give the Page a way to know which NavigationRootPage is navigating.
        ' Please call this function rather than rootFrame.Navigate to navigate the rootFrame.
        Public Sub Navigate( _
            pageType As Type,
            Optional targetPageArguments As Object = Nothing,
            Optional navigationTransitionInfo1 As Microsoft.UI.Xaml.Media.Animation.NavigationTransitionInfo = Nothing)
            Dim args As New NavigationRootPageArgs
            args.NavigationRootPage = Me
            args.Parameter = targetPageArguments
            rootFrame.Navigate(pageType, args, navigationTransitionInfo1)
        End Sub
#If WINUI_PRERELEASE

        public void App_Resuming()
        {

#If UNIVERSAL

            switch (rootFrame?.Content)
            {
                case ItemPage itemPage:
                    itemPage.SetInitialVisuals();
                    break;
                case NewControlsPage newControlsPage:
                case AllControlsPage allControlsPage:
                    NavigationView.AlwaysShowHeader = false;
                    break;
            }

#End If

        }

#End If

        Public Sub EnsureNavigationSelection(id As String)
            For Each rawGroup As Object In Me.NavigationView.MenuItems
                Dim TempVar2 As Boolean = TypeOf rawGroup Is NavigationViewItem
                Dim group As NavigationViewItem = rawGroup
                If TempVar2 Then
                    For Each rawItem As Object In group.MenuItems
                        Dim TempVar3 As Boolean = TypeOf rawItem Is NavigationViewItem
                        Dim item As NavigationViewItem = rawItem
                        If TempVar3 Then
                            If CStr(item.Tag) = id Then
                                group.IsExpanded = True
                                NavigationView.SelectedItem = item
                                item.IsSelected = True
                                Return
                            End If
                        End If
                    Next
                End If
            Next
        End Sub
        Private Sub AddNavigationMenuItems()
            For Each group In ControlInfoDataSource.Instance.Groups.OrderBy(Function(i) i.Title)
                Dim itemGroup As New Microsoft.UI.Xaml.Controls.NavigationViewItem() With
{
                    .Content = group.Title,
                    .Tag = group.UniqueId,
                    .DataContext = group,
                    .Icon = GetIcon(group.ImageIconPath)}

                Dim groupMenuFlyoutItem As New MenuFlyoutItem() With
{
                    .Text = $"Copy Link to {group.Title} Samples",
                    .Icon = New FontIcon() With
{
                        .Glyph = ""},
                    .Tag = group}
                groupMenuFlyoutItem.Click += Me.OnMenuFlyoutItemClick
                itemGroup.ContextFlyout = New MenuFlyout() With
{
                    .Items = New UnknownTypeTryConvertProject From {
                        groupMenuFlyoutItem}}

                AutomationProperties.SetName(itemGroup, group.Title)

                For Each item In group.Items
                    Dim itemInGroup As New Microsoft.UI.Xaml.Controls.NavigationViewItem() With
{
                        .IsEnabled = item.IncludedInBuild,
                        .Content = item.Title,
                        .Tag = item.UniqueId,
                        .DataContext = item}

                    Dim itemInGroupMenuFlyoutItem As New MenuFlyoutItem() With
{
                        .Text = $"Copy Link to {item.Title} Sample",
                        .Icon = New FontIcon() With
{
                            .Glyph = ""},
                        .Tag = item}
                    itemInGroupMenuFlyoutItem.Click += Me.OnMenuFlyoutItemClick
                    itemInGroup.ContextFlyout = New MenuFlyout() With
{
                        .Items = New UnknownTypeTryConvertProject From {
                            itemInGroupMenuFlyoutItem}}

                    itemGroup.MenuItems.Add(itemInGroup)
                    AutomationProperties.SetName(itemInGroup, item.Title)
                Next

                NavigationViewControl.MenuItems.Add(itemGroup)

                If group.UniqueId = "AllControls" Then
                    Me._allControlsMenuItem = itemGroup
                ElseIf group.UniqueId = "NewControls" Then
                    Me._newControlsMenuItem = itemGroup
                End If
            Next

            ' Move "What's New" and "All Controls" to the top of the NavigationView
            NavigationViewControl.MenuItems.Remove(_allControlsMenuItem)
            NavigationViewControl.MenuItems.Remove(_newControlsMenuItem)
            NavigationViewControl.MenuItems.Insert(0, _allControlsMenuItem)
            NavigationViewControl.MenuItems.Insert(0, _newControlsMenuItem)

            ' Separate the All/New items from the rest of the categories.
            NavigationViewControl.MenuItems.Insert(2, New Microsoft.UI.Xaml.Controls.NavigationViewItemSeparator)
            AddHandler _newControlsMenuItem.Loaded, AddressOf OnNewControlsMenuItemLoaded
        End Sub
        Private Sub OnMenuFlyoutItemClick(sender As Object, e As RoutedEventArgs)
            Select Case True
                Case TypeOf TryCast(sender, MenuFlyoutItem).Tag Is ControlInfoDataItem
                    Dim item As ControlInfoDataItem = CType(TryCast(sender, MenuFlyoutItem).Tag, ControlInfoDataItem)
                    ProtocolActivationClipboardHelper.Copy(item)
                    Return
                Case TypeOf TryCast(sender, MenuFlyoutItem).Tag Is ControlInfoDataGroup
                    Dim group As ControlInfoDataGroup = CType(TryCast(sender, MenuFlyoutItem).Tag, ControlInfoDataGroup)
                    ProtocolActivationClipboardHelper.Copy(group)
                    Return
            End Select
        End Sub
        Private Shared Function GetIcon(imagePath As String) As IconElement

            ' FontFamily = new FontFamily("Segoe MDL2 Assets"),
            Return If(imagePath.ToLowerInvariant().EndsWith(".png"),
CType(New BitmapIcon() With
{
                .UriSource = New Uri(imagePath, UriKind.RelativeOrAbsolute),
                .ShowAsMonochrome = False}, IconElement),
CType(New FontIcon() With
{ _
            .Glyph = imagePath
}, IconElement))
        End Function
        Private Sub SetDeviceFamily()
            Dim familyName = AnalyticsInfo.VersionInfo.DeviceFamily
            Dim parsedDeviceType As DeviceType = Nothing
            If Not [Enum].TryParse(familyName.Replace("Windows.", String.Empty), parsedDeviceType) Then
                parsedDeviceType = DeviceType.Other
            End If

            DeviceFamily = parsedDeviceType
        End Sub
        Private Sub OnNewControlsMenuItemLoaded(sender As Object, e As RoutedEventArgs)
            If IsFocusSupported AndAlso NavigationViewControl.DisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode.Expanded Then
                controlsSearchBox.Focus(FocusState.Keyboard)
            End If
        End Sub
        Private Sub OnGamepadRemoved(sender As Object, e As Gamepad)
            _isGamePadConnected = Gamepad.Gamepads.Any()
        End Sub
        Private Sub OnGamepadAdded(sender As Object, e As Gamepad)
            _isGamePadConnected = Gamepad.Gamepads.Any()
        End Sub
        Private Sub OnNavigationViewControlLoaded(sender As Object, e As RoutedEventArgs)
            ' Delay necessary to ensure NavigationView visual state can match navigation
            Task.Delay(500).ContinueWith(Sub() Me.NavigationViewLoaded?.Invoke(), TaskScheduler.FromCurrentSynchronizationContext())
        End Sub
        Private Sub OnNavigationViewSelectionChanged(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs)
            ' Close any open teaching tips before navigation
            CloseTeachingTips()

            If args.IsSettingsSelected Then
                If rootFrame.CurrentSourcePageType <> GetType(SettingsPage) Then
                    Navigate(GetType(SettingsPage))
                End If
            Else
                Dim selectedItem1 = args.SelectedItemContainer

                If selectedItem1 = _allControlsMenuItem Then
                    If rootFrame.CurrentSourcePageType <> GetType(AllControlsPage) Then
                        Navigate(GetType(AllControlsPage))
                    End If
                ElseIf selectedItem1 = _newControlsMenuItem Then
                    If rootFrame.CurrentSourcePageType <> GetType(NewControlsPage) Then
                        Navigate(GetType(NewControlsPage))
                    End If
                Else
                    If TypeOf selectedItem1.DataContext Is ControlInfoDataGroup Then
                        Dim itemId = CType(selectedItem1.DataContext, ControlInfoDataGroup).UniqueId
                        Navigate(GetType(SectionPage), itemId)
                    ElseIf TypeOf selectedItem1.DataContext Is ControlInfoDataItem Then
                        Dim item = CType(selectedItem1.DataContext, ControlInfoDataItem)
                        Navigate(GetType(ItemPage), item.UniqueId)
                    End If
                End If
            End If
        End Sub
        Private Sub OnRootFrameNavigated(sender As Object, e As NavigationEventArgs)
            ' Close any open teaching tips before navigation
            CloseTeachingTips()

            If e.SourcePageType = GetType(AllControlsPage) OrElse _
                e.SourcePageType = GetType(NewControlsPage) Then
                NavigationViewControl.AlwaysShowHeader = False
            Else
                NavigationViewControl.AlwaysShowHeader = True
            End If

            TestContentLoadedCheckBox.IsChecked = True
        End Sub
        Private Sub OnRootFrameNavigating(sender As Object, e As NavigatingCancelEventArgs)
            TestContentLoadedCheckBox.IsChecked = False
        End Sub
        Private Sub CloseTeachingTips()
            If PageHeader IsNot Nothing Then
                PageHeader.TeachingTip1.IsOpen = False
                PageHeader.TeachingTip3.IsOpen = False
            End If
        End Sub
        Private Sub OnControlsSearchBoxTextChanged(sender As AutoSuggestBox, args As AutoSuggestBoxTextChangedEventArgs)
            If args.Reason = AutoSuggestionBoxTextChangeReason.UserInput Then
                Dim suggestions As Collections.Generic.List(Of ControlInfoDataItem) = New List(Of ControlInfoDataItem)

                Dim querySplit = sender.Text.Split(" ")
                For Each group In ControlInfoDataSource.Instance.Groups
                    Dim matchingItems = group.Items.Where( _
        Function(item) As Boolean
                            ' Idea: check for every word entered (separated by space) if it is in the name, 
                            ' e.g. for query "split button" the only result should "SplitButton" since its the only query to contain "split" and "button"
                            ' If any of the sub tokens is not in the string, we ignore the item. So the search gets more precise with more words
                            Dim flag As Boolean = item.IncludedInBuild
            For Each queryToken As String In querySplit
                                ' Check if token is not in string
                                If item.Title.IndexOf(queryToken, StringComparison.CurrentCultureIgnoreCase) < 0 Then
                                    ' Token is not in string, so we ignore this item.
                                    flag = False
                End If
            Next
            Return flag
        End Function)
                    For Each item In matchingItems
                        suggestions.Add(item)
                    Next
                Next
                If suggestions.Count > 0 Then
                    controlsSearchBox.ItemsSource = suggestions.OrderByDescending(Function(i) i.Title.StartsWith(sender.Text, StringComparison.CurrentCultureIgnoreCase)).ThenBy(Function(i) i.Title)
                Else
                    controlsSearchBox.ItemsSource = New String() {"No results found"}
                End If
            End If
        End Sub
        Private Sub OnControlsSearchBoxQuerySubmitted(sender As AutoSuggestBox, args As AutoSuggestBoxQuerySubmittedEventArgs)
            If args.ChosenSuggestion IsNot Nothing AndAlso TypeOf args.ChosenSuggestion Is ControlInfoDataItem Then
                Dim infoDataItem = TryCast(args.ChosenSuggestion, ControlInfoDataItem)
                Dim itemId = infoDataItem.UniqueId
                EnsureItemIsVisibleInNavigation(infoDataItem.Title)
                Navigate(GetType(ItemPage), itemId)
            ElseIf Not String.IsNullOrEmpty(args.QueryText) Then
                Navigate(GetType(SearchResultsPage), args.QueryText)
            End If
        End Sub
        Public Sub EnsureItemIsVisibleInNavigation(name1 As String)
            Dim changedSelection As Boolean = False
            For Each rawItem As Object In NavigationView.MenuItems
                ' Check if we encountered the separator
                If Not (TypeOf rawItem Is NavigationViewItem) Then
                    ' Skipping this item
                    Continue For
                End If

                Dim item = TryCast(rawItem, NavigationViewItem)

                ' Check if we are this category
                If CStr(item.Content) = name1 Then
                    NavigationView.SelectedItem = item
                    changedSelection = True
                    ' We are not :/
                Else
                    ' Maybe one of our items is?
                    If item.MenuItems.Count <> 0 Then
                        For Each child As NavigationViewItem In item.MenuItems
                            If CStr(child.Content) = name1 Then
                                ' We are the item corresponding to the selected one, update selection!
                                ' Deal with differences in displaymodes
                                If NavigationView.PaneDisplayMode = NavigationViewPaneDisplayMode.Top Then
                                    ' In Topmode, the child is not visible, so set parent as selected
                                    ' Everything else does not work unfortunately
                                    NavigationView.SelectedItem = item
                                    item.StartBringIntoView()
                                Else
                                    ' Expand so we animate
                                    item.IsExpanded = True
                                    ' Ensure parent is expanded so we actually show the selection indicator
                                    NavigationView.UpdateLayout()
                                    ' Set selected item
                                    NavigationView.SelectedItem = child
                                    child.StartBringIntoView()
                                End If
                                ' Set to true to also skip out of outer for loop
                                changedSelection = True
                                ' Break out of child iteration for loop
                                Exit For
                            End If
                        Next
                    End If
                End If
                ' We updated selection, break here!
                If changedSelection Then
                    Exit For
                End If
            Next
        End Sub
        Private Sub NavigationViewControl_PaneClosing(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Microsoft.UI.Xaml.Controls.NavigationViewPaneClosingEventArgs)
            UpdateAppTitleMargin(sender)
        End Sub
        Private Sub NavigationViewControl_PaneOpening(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Object)
            UpdateAppTitleMargin(sender)
        End Sub
        Private Sub NavigationViewControl_DisplayModeChanged(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Microsoft.UI.Xaml.Controls.NavigationViewDisplayModeChangedEventArgs)
            Dim currMargin As Thickness = AppTitleBar.Margin
            If sender.DisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode.Minimal Then
                AppTitleBar.Margin = New Thickness() With
{
                    .Left = (sender.CompactPaneLength * 2),
                    .Top = currMargin.Top,
                    .Right = currMargin.Right,
                    .Bottom = currMargin.Bottom}
            Else
                AppTitleBar.Margin = New Thickness() With
{
                    .Left = sender.CompactPaneLength,
                    .Top = currMargin.Top,
                    .Right = currMargin.Right,
                    .Bottom = currMargin.Bottom}

            End If

            UpdateAppTitleMargin(sender)
            UpdateHeaderMargin(sender)
        End Sub
        Private Sub UpdateAppTitleMargin(sender As Microsoft.UI.Xaml.Controls.NavigationView)
            Const smallLeftIndent As Integer = 4, largeLeftIndent As Integer = 24

            If ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7) Then
                AppTitle.TranslationTransition = New Vector3Transition

                If (sender.DisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode.Expanded AndAlso sender.IsPaneOpen) OrElse _
                         sender.DisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode.Minimal Then
                    AppTitle.Translation = New Numerics.Vector3(smallLeftIndent, 0, 0)
                Else
                    AppTitle.Translation = New Numerics.Vector3(largeLeftIndent, 0, 0)
                End If
            Else
                Dim currMargin As Thickness = AppTitle.Margin

                If (sender.DisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode.Expanded AndAlso sender.IsPaneOpen) OrElse _
                         sender.DisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode.Minimal Then
                    AppTitle.Margin = New Thickness() With
{
                        .Left = smallLeftIndent,
                        .Top = currMargin.Top,
                        .Right = currMargin.Right,
                        .Bottom = currMargin.Bottom}
                Else
                    AppTitle.Margin = New Thickness() With
{
                        .Left = largeLeftIndent,
                        .Top = currMargin.Top,
                        .Right = currMargin.Right,
                        .Bottom = currMargin.Bottom}
                End If
            End If
        End Sub
        Private Sub UpdateHeaderMargin(sender As Microsoft.UI.Xaml.Controls.NavigationView)
            If PageHeader IsNot Nothing Then
                If sender.DisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode.Minimal Then
                    PageHeader.HeaderPadding = CType(App.Current.Resources("PageHeaderMinimalPadding"), Thickness)
                Else
                    PageHeader.HeaderPadding = CType(App.Current.Resources("PageHeaderDefaultPadding"), Thickness)
                End If
            End If
        End Sub
        Private Sub CtrlF_Invoked(sender As KeyboardAccelerator, args As KeyboardAcceleratorInvokedEventArgs)
            controlsSearchBox.Focus(FocusState.Programmatic)
        End Sub
#Region "Helpers for test automation"

        Private Shared _error As String = String.Empty
        Private Shared _log As String = String.Empty
        Private Async Sub WaitForIdleInvokerButton_Click(sender As Object, e As RoutedEventArgs)
            _idleStateEnteredCheckBox.IsChecked = False
            Await Windows.System.Threading.ThreadPool.RunAsync(AddressOf WaitForIdleWorker)

            _logReportingTextBox.Text = _log

            If _error.Length = 0 Then
                _idleStateEnteredCheckBox.IsChecked = True
            Else
                ' Setting Text will raise a property-changed event, so even if we
                ' immediately set it back to the empty string, we'll still get the
                ' error-reported event that we can detect and handle.
                _errorReportingTextBox.Text = _error
                _errorReportingTextBox.Text = String.Empty

                _error = String.Empty
            End If
        End Sub
        Private Shared Sub WaitForIdleWorker(action1 As IAsyncAction)
            _error = IdleSynchronizer.TryWait(_log)
        End Sub
        Private Sub CloseAppInvokerButton_Click(sender As Object, e As Microsoft.UI.Xaml.RoutedEventArgs)
            Application.Current.[Exit]()
        End Sub
        Private Sub GoBackInvokerButton_Click(sender As Object, e As Microsoft.UI.Xaml.RoutedEventArgs)
            If Me.rootFrame.CanGoBack Then
                Me.rootFrame.GoBack()
            End If
        End Sub
        Private Sub WaitForDebuggerInvokerButton_Click(sender As Object, e As RoutedEventArgs)
            DebuggerAttachedCheckBox.IsChecked = False

            Dim dispatcherQueue1 = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread()

            Dim workItem As New Windows.System.Threading.WorkItemHandler(Sub()
                                                                             While Not IsDebuggerPresent()
                                                                                 Thread.Sleep(1000)
                                                                             End While

                                                                             DebugBreak()

                                                                             dispatcherQueue1.TryEnqueue( _
                                                                                 DispatcherQueuePriority.Low,
                                                                                 New DispatcherQueueHandler(Sub()
                                                                                                                DebuggerAttachedCheckBox.IsChecked = True
                                                                                                            End Sub))
                                                                         End Sub)

            Dim asyncAction = Windows.System.Threading.ThreadPool.RunAsync(workItem)
        End Sub
        <DllImport("kernel32.dll")>
        Private Shared Function IsDebuggerPresent() As Boolean
        End Function
        <DllImport("kernel32.dll")>
        Private Shared Sub DebugBreak()
        End Sub

#End Region

    End Class


    Public Class NavigationRootPageArgs
        Public NavigationRootPage As NavigationRootPage
        Public Parameter As Object
    End Class

    Public Enum DeviceType
        Desktop
        Mobile
        Other
        Xbox
    End Enum
End Namespace
