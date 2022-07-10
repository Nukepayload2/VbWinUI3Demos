' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Input
Imports AppUIBasics.SamplePages
Imports AppUIBasics.Helper
Imports Windows.ApplicationModel.Core
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Windowing
Imports Microsoft.UI.Dispatching
Imports AppUIBasics.TabViewPages

#If Not UNIVERSAL

Imports System.Collections.ObjectModel
#End If

#If UNIVERSAL

using Windows.UI.Core;
using Windows.UI.ViewManagement;

#End If

Namespace AppUIBasics.ControlPages
    Public Class MyData
        Public Property DataHeader As String
        Public Property DataIconSource As Microsoft.UI.Xaml.Controls.IconSource
        Public Property DataContent As Object
    End Class


    Public NotInheritable Partial Class TabViewPage
        Inherits Page
        Private myDatas As ObservableCollection(Of MyData)

        Public Sub New()
            Me.InitializeComponent()

#If Not UNIVERSAL

            ' Launching isn't supported yet on Desktop
            ' Blocked on Task 27517663: DCPP Preview 2 Bug: Dragging in TabView windowing sample causes app to crash
            'this.LaunchExample.Visibility = Visibility.Collapsed;
#End If

            InitializeDataBindingSampleData()
        End Sub
#Region "SharedTabViewLogic"

        Private Sub TabView_Loaded(sender As Object, e As RoutedEventArgs)
            For i As Integer = 0 To 3 - 1
                TryCast(sender, TabView).TabItems.Add(CreateNewTab(i))
            Next
        End Sub
        Private Sub TabView_AddButtonClick(sender As TabView, args As Object)
            sender.TabItems.Add(CreateNewTab(sender.TabItems.Count))
        End Sub
        Private Sub TabView_TabCloseRequested(sender As TabView, args As TabViewTabCloseRequestedEventArgs)
            sender.TabItems.Remove(args.Tab)
        End Sub
        Private Function CreateNewTab(index As Integer) As TabViewItem
            Dim newItem As New TabViewItem With
            { _
            .Header = $"Document {index}",
            .IconSource = New Microsoft.UI.Xaml.Controls.SymbolIconSource() With
{
                .Symbol = Symbol.Document}
            }

            ' The content of the tab is often a frame that contains a page, though it could be any UIElement.
            Dim frame1 As New Frame

            Select Case index Mod 3
                Case 0
                    frame1.Navigate(GetType(SamplePage1))
                Case 1
                    frame1.Navigate(GetType(SamplePage2))
                Case 2
                    frame1.Navigate(GetType(SamplePage3))
            End Select

            newItem.Content = frame1

            Return newItem
        End Function
#End Region

#Region "ItemsSourceSample"

        Private Sub InitializeDataBindingSampleData()
            myDatas = New ObservableCollection(Of MyData)

            For index As Integer = 0 To 3 - 1
                myDatas.Add(CreateNewMyData(index))
            Next
        End Sub
        Private Function CreateNewMyData(index As Integer) As MyData
            Dim newData As AppUIBasics.ControlPages.MyData = New MyData With
            { _
            .DataHeader = $"MyData Doc {index}",
            .DataIconSource = New Microsoft.UI.Xaml.Controls.SymbolIconSource() With
{
                .Symbol = Symbol.Placeholder}
            }

            Dim frame1 As New Frame

            Select Case index Mod 3
                Case 0
                    frame1.Navigate(GetType(SamplePage1))
                Case 1
                    frame1.Navigate(GetType(SamplePage2))
                Case 2
                    frame1.Navigate(GetType(SamplePage3))
            End Select

            newData.DataContent = frame1

            Return newData
        End Function
        Private Sub TabViewItemsSourceSample_AddTabButtonClick(sender As TabView, args As Object)
            ' Add a new MyData item to the collection. TabView automatically generates a TabViewItem.
            myDatas.Add(CreateNewMyData(myDatas.Count))
        End Sub
        Private Sub TabViewItemsSourceSample_TabCloseRequested(sender As TabView, args As TabViewTabCloseRequestedEventArgs)
            ' Remove the requested MyData object from the collection.
            myDatas.Remove(TryCast(args.Item, MyData))
        End Sub
#End Region

#Region "KeyboardAcceleratorSample"

        Private Sub NewTabKeyboardAccelerator_Invoked(sender As KeyboardAccelerator, args As KeyboardAcceleratorInvokedEventArgs)
            Dim senderTabView = TryCast(args.Element, TabView)
            senderTabView.TabItems.Add(CreateNewTab(senderTabView.TabItems.Count))

            args.Handled = True
        End Sub
        Private Sub CloseSelectedTabKeyboardAccelerator_Invoked(sender As KeyboardAccelerator, args As KeyboardAcceleratorInvokedEventArgs)
            Dim InvokedTabView = TryCast(args.Element, TabView)

            ' Only close the selected tab if it is closeable
            If CType(InvokedTabView.SelectedItem, TabViewItem).IsClosable Then
                InvokedTabView.TabItems.Remove(InvokedTabView.SelectedItem)
            End If

            args.Handled = True
        End Sub
        Private Sub NavigateToNumberedTabKeyboardAccelerator_Invoked(sender As KeyboardAccelerator, args As KeyboardAcceleratorInvokedEventArgs)
            Dim InvokedTabView = TryCast(args.Element, TabView)

            Dim tabToSelect As Integer = 0

            Select Case sender.Key
                Case Windows.System.VirtualKey.Number1
                    tabToSelect = 0
                Case Windows.System.VirtualKey.Number2
                    tabToSelect = 1
                Case Windows.System.VirtualKey.Number3
                    tabToSelect = 2
                Case Windows.System.VirtualKey.Number4
                    tabToSelect = 3
                Case Windows.System.VirtualKey.Number5
                    tabToSelect = 4
                Case Windows.System.VirtualKey.Number6
                    tabToSelect = 5
                Case Windows.System.VirtualKey.Number7
                    tabToSelect = 6
                Case Windows.System.VirtualKey.Number8
                    tabToSelect = 7
                Case Windows.System.VirtualKey.Number9
                    ' Select the last tab
                    tabToSelect = InvokedTabView.TabItems.Count - 1
            End Select

            ' Only select the tab if it is in the list
            If tabToSelect < InvokedTabView.TabItems.Count Then
                InvokedTabView.SelectedIndex = tabToSelect
            End If

            args.Handled = True
        End Sub
#End Region

        Private Sub TabWidthBehaviorComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim widthModeString As String = TryCast(e.AddedItems(0), ComboBoxItem).Content.ToString()
            Dim widthMode As TabViewWidthMode = TabViewWidthMode.Equal
            Select Case widthModeString
                Case "Equal"
                    widthMode = TabViewWidthMode.Equal
                Case "SizeToContent"
                    widthMode = TabViewWidthMode.SizeToContent
                Case "Compact"
                    widthMode = TabViewWidthMode.Compact
            End Select
            TabView3.TabWidthMode = widthMode
        End Sub
        Private Sub TabCloseButtonOverlayModeComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim overlayModeString As String = TryCast(e.AddedItems(0), ComboBoxItem).Content.ToString()
            Dim overlayMode As TabViewCloseButtonOverlayMode = TabViewCloseButtonOverlayMode.Auto
            Select Case overlayModeString
                Case "Auto"
                    overlayMode = TabViewCloseButtonOverlayMode.Auto
                Case "OnHover"
                    overlayMode = TabViewCloseButtonOverlayMode.OnPointerOver
                Case "Always"
                    overlayMode = TabViewCloseButtonOverlayMode.Always
            End Select
            TabView4.CloseButtonOverlayMode = overlayMode
        End Sub
#If UNIVERSAL

        private async void TabViewWindowingButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            CoreApplicationView newView = CoreApplication.CreateNewView();
            int newViewId = 0;
            await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame frame = new Frame();
                frame.Navigate(typeof(TabViewWindowingSamplePage), null);
                Window.Current.Content = frame;
                // You have to activate the window in order to show it later.
                Window.Current.Activate();

                newViewId = ApplicationView.GetForCurrentView().Id;
            });
            bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
        }

#Else

        Private Sub TabViewWindowingButton_Click(sender As Object, e As Microsoft.UI.Xaml.RoutedEventArgs)
            Dim newWindow = WindowHelper.CreateWindow()

            Dim frame1 As New Frame
            frame1.RequestedTheme = ThemeHelper.RootTheme
            frame1.Navigate(GetType(TabViewWindowingSamplePage), Nothing)
            newWindow.Content = frame1
            newWindow.Activate()
        End Sub
#End If

    End Class
End Namespace
