' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports Windows.ApplicationModel.Core
Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Foundation.Metadata
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Hosting
Imports Microsoft.UI.Xaml.Navigation
Imports Microsoft.UI.Windowing
Imports AppUIBasics.Helper

Namespace AppUIBasics.TabViewPages
    Public NotInheritable Partial Class TabViewWindowingSamplePage
        Inherits Page
        Private Const DataIdentifier As String = "MyTabItem"
        Public Sub New()
            Me.InitializeComponent()
            AddHandler Tabs.TabItemsChanged, AddressOf Tabs_TabItemsChanged

            AddHandler Loaded, AddressOf TabViewWindowingSamplePage_Loaded
        End Sub
        Private Sub TabViewWindowingSamplePage_Loaded(sender As Object, e As RoutedEventArgs)
            Dim currentWindow = WindowHelper.GetWindowForElement(Me)
            currentWindow.ExtendsContentIntoTitleBar = True
            currentWindow.SetTitleBar(CustomDragRegion)
            CustomDragRegion.MinWidth = 188
        End Sub
        Private Sub Tabs_TabItemsChanged(sender As TabView, args As Windows.Foundation.Collections.IVectorChangedEventArgs)
            ' If there are no more tabs, close the window.
            If sender.TabItems.Count = 0 Then
                WindowHelper.GetWindowForElement(Me).Close()
                ' If there is only one tab left, disable dragging and reordering of Tabs.
            ElseIf sender.TabItems.Count = 1 Then
                sender.CanReorderTabs = False
                sender.CanDragTabs = False
            Else
                sender.CanReorderTabs = True
                sender.CanDragTabs = True
            End If
        End Sub
        Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            MyBase.OnNavigatedTo(e)

            SetupWindow()
        End Sub
        Private Sub SetupWindow()

            ' Main Window -- add some default items
            For i As Integer = 0 To 3 - 1
                Tabs.TabItems.Add(New TabViewItem() With
{
                    .IconSource = New Microsoft.UI.Xaml.Controls.SymbolIconSource() With
{
                        .Symbol = Symbol.Placeholder},
                    .Header = $"Item {i}",
                    .Content = New MyTabContentControl() With
{
                        .DataContext = $"Page {i}"}})
            Next

            Tabs.SelectedIndex = 0
#If UNIVERSAL

            // Extend into the titlebar
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;

            var titleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Microsoft.UI.Colors.Transparent;

#End If

        End Sub
        Private Sub CoreTitleBar_LayoutMetricsChanged(sender As CoreApplicationViewTitleBar, args As Object)
            ' To ensure that the tabs in the titlebar are not occluded by shell
            ' content, we must ensure that we account for left and right overlays.
            ' In LTR layouts, the right inset includes the caption buttons and the
            ' drag region, which is flipped in RTL.

            ' The SystemOverlayLeftInset and SystemOverlayRightInset values are
            ' in terms of physical left and right. Therefore, we need to flip
            ' then when our flow direction is RTL.
            If FlowDirection = FlowDirection.LeftToRight Then
                CustomDragRegion.MinWidth = sender.SystemOverlayRightInset
                ShellTitleBarInset.MinWidth = sender.SystemOverlayLeftInset
            Else
                CustomDragRegion.MinWidth = sender.SystemOverlayLeftInset
                ShellTitleBarInset.MinWidth = sender.SystemOverlayRightInset
            End If

            ' Ensure that the height of the custom regions are the same as the titlebar.
            CustomDragRegion.Height = __InlineAssignHelper(ShellTitleBarInset.Height, sender.Height)
        End Sub
        Public Sub AddTabToTabs(tab As TabViewItem)
            Tabs.TabItems.Add(tab)
        End Sub
        ' Create a new Window once the Tab is dragged outside.
        Private Sub Tabs_TabDroppedOutside(sender As TabView, args As TabViewTabDroppedOutsideEventArgs)
            Dim newPage As AppUIBasics.TabViewPages.TabViewWindowingSamplePage = New TabViewWindowingSamplePage

            Tabs.TabItems.Remove(args.Tab)
            newPage.AddTabToTabs(args.Tab)

            Dim newWindow = WindowHelper.CreateWindow()
            newWindow.ExtendsContentIntoTitleBar = True
            newWindow.Content = newPage

            newWindow.Activate()
        End Sub
        Private Sub Tabs_TabDragStarting(sender As TabView, args As TabViewTabDragStartingEventArgs)
            ' We can only drag one tab at a time, so grab the first one...
            Dim firstItem = args.Tab

            ' ... set the drag data to the tab...
            args.Data.Properties.Add(DataIdentifier, firstItem)

            ' ... and indicate that we can move it
            args.Data.RequestedOperation = DataPackageOperation.Move
        End Sub
        Private Sub Tabs_TabStripDrop(sender As Object, e As DragEventArgs)
            Dim obj As Object = Nothing
            If e.DataView.Properties.TryGetValue(DataIdentifier, obj) Then
                ' Ensure that the obj property is set before continuing.
                If obj Is Nothing Then
                    Return
                End If

                Dim destinationTabView = TryCast(sender, TabView)
                Dim destinationItems = destinationTabView.TabItems

                If destinationItems IsNot Nothing Then
                    ' First we need to get the position in the List to drop to
                    Dim index As Integer = -1

                    ' Determine which items in the list our pointer is between.
                    For i As Integer = 0 To destinationTabView.TabItems.Count - 1
                        Dim item = TryCast(destinationTabView.ContainerFromIndex(i), TabViewItem)

                        If e.GetPosition(item).X - item.ActualWidth < 0 Then
                            index = i
                            Exit For
                        End If
                    Next

                    ' The TabView can only be in one tree at a time. Before moving it to the new TabView, remove it from the old.
                    Dim destinationTabViewListView = TryCast(TryCast(obj, TabViewItem).Parent, TabViewListView)
                    destinationTabViewListView.Items.Remove(obj)

                    If index < 0 Then
                        ' We didn't find a transition point, so we're at the end of the list
                        destinationItems.Add(obj)
                    ElseIf index < destinationTabView.TabItems.Count Then
                        ' Otherwise, insert at the provided index.
                        destinationItems.Insert(index, obj)
                    End If

                    ' Select the newly dragged tab
                    destinationTabView.SelectedItem = obj
                End If
            End If
        End Sub
        ' This method prevents the TabView from handling things that aren't text (ie. files, images, etc.)
        Private Sub Tabs_TabStripDragOver(sender As Object, e As DragEventArgs)
            If e.DataView.Properties.ContainsKey(DataIdentifier) Then
                e.AcceptedOperation = DataPackageOperation.Move
            End If
        End Sub
        Private Sub Tabs_AddTabButtonClick(sender As TabView, args As Object)
            sender.TabItems.Add(New TabViewItem() With
{
                .IconSource = New Microsoft.UI.Xaml.Controls.SymbolIconSource() With
{
                    .Symbol = Symbol.Placeholder},
                .Header = "New Item",
                .Content = New MyTabContentControl() With
{
                    .DataContext = "New Item"}})
        End Sub
        Private Sub Tabs_TabCloseRequested(sender As TabView, args As TabViewTabCloseRequestedEventArgs)
            sender.TabItems.Remove(args.Tab)
        End Sub
        <Obsolete("Please refactor code that uses this function, it is a simple work-around to simulate inline assignment in VB!")>
        Private Shared Function __InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
End Namespace
