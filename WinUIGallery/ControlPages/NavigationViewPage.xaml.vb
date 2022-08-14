' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports AppUIBasics.SamplePages
Imports AppUIBasics.Common
Imports Microsoft.UI.Xaml.Controls
Imports System
Imports System.Diagnostics
Imports Microsoft.UI.Xaml
Imports System.Linq
Imports Windows.System
Imports Microsoft.UI.Xaml.Automation

#If Not UNIVERSAL

Imports System.Collections.ObjectModel
#End If

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class NavigationViewPage
        Inherits Page
        Public Shared CameFromToggle As Boolean = False
        Public Shared CameFromGridChange As Boolean = False
        Public ArrowKey As VirtualKey

        Public Property Categories As ObservableCollection(Of CategoryBase)

        Public Sub New()
            Me.InitializeComponent()

            nvSample2.SelectedItem = nvSample2.MenuItems.OfType(Of Microsoft.UI.Xaml.Controls.NavigationViewItem)().First()
            nvSample5.SelectedItem = nvSample5.MenuItems.OfType(Of Microsoft.UI.Xaml.Controls.NavigationViewItem)().First()
            nvSample6.SelectedItem = nvSample6.MenuItems.OfType(Of Microsoft.UI.Xaml.Controls.NavigationViewItem)().First()
            nvSample7.SelectedItem = nvSample7.MenuItems.OfType(Of Microsoft.UI.Xaml.Controls.NavigationViewItem)().First()
            nvSample8.SelectedItem = nvSample8.MenuItems.OfType(Of Microsoft.UI.Xaml.Controls.NavigationViewItem)().First()
            nvSample9.SelectedItem = nvSample9.MenuItems.OfType(Of Microsoft.UI.Xaml.Controls.NavigationViewItem)().First()

            Categories = New ObservableCollection(Of CategoryBase)
            Dim firstCategory As New Category With
{
                .Name = "Category 1",
                .Glyph = Symbol.Home,
                .Tooltip = "This is category 1"}
            Categories.Add(firstCategory)
            Categories.Add(New Category With
{
                .Name = "Category 2",
                .Glyph = Symbol.Keyboard,
                .Tooltip = "This is category 2"})
            Categories.Add(New Category With
{
                .Name = "Category 3",
                .Glyph = Symbol.Library,
                .Tooltip = "This is category 3"})
            Categories.Add(New Category With
{
                .Name = "Category 4",
                .Glyph = Symbol.Mail,
                .Tooltip = "This is category 4"})
            nvSample4.SelectedItem = firstCategory

            setASBSubstitutionString()

            ' Fixes #218
            nvSample2.UpdateLayout()
        End Sub
        Public Function ChoosePanePosition(toggleOn As Boolean) As Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode
            If toggleOn Then
                Return Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Left
            Else
                Return Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Top
            End If
        End Function
        Private Sub NavigationView_SelectionChanged(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs)
            If args.IsSettingsSelected Then
                contentFrame.Navigate(GetType(SampleSettingsPage))
            Else
                Dim selectedItem1 = CType(args.SelectedItem, Microsoft.UI.Xaml.Controls.NavigationViewItem)
                If selectedItem1 IsNot Nothing Then
                    Dim selectedItemTag As String = (CStr(selectedItem1.Tag))
                    sender.Header = "Sample Page " & selectedItemTag.Substring(selectedItemTag.Length - 1)
                    Dim pageName As String = "AppUIBasics.SamplePages." & selectedItemTag
                    Dim pageType As Type = Type.[GetType](pageName)
                    contentFrame.Navigate(pageType)
                End If
            End If
        End Sub
        Private Sub NavigationView_SelectionChanged2(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs)
            If Not CameFromGridChange Then
                If args.IsSettingsSelected Then
                    contentFrame2.Navigate(GetType(SampleSettingsPage))
                Else
                    Dim selectedItem1 = CType(args.SelectedItem, Microsoft.UI.Xaml.Controls.NavigationViewItem)
                    Dim selectedItemTag As String = (CStr(selectedItem1.Tag))
                    Dim pageName As String = "AppUIBasics.SamplePages." & selectedItemTag
                    Dim pageType As Type = Type.[GetType](pageName)
                    contentFrame2.Navigate(pageType)
                End If
            End If

            CameFromGridChange = False
        End Sub
        Private Sub NavigationView_SelectionChanged4(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs)
            If args.IsSettingsSelected Then
                contentFrame4.Navigate(GetType(SampleSettingsPage))
            Else
                Debug.WriteLine("Before hitting sample page 1")

                Dim selectedItem1 = CType(args.SelectedItem, Category)
                Dim selectedItemTag As String = selectedItem1.Name
                sender.Header = "Sample Page " & selectedItemTag.Substring(selectedItemTag.Length - 1)
                Dim pageName As String = "AppUIBasics.SamplePages." & "SamplePage1"
                Dim pageType As Type = Type.[GetType](pageName)
                contentFrame4.Navigate(pageType)
            End If
        End Sub
        Private Sub NavigationView_SelectionChanged5(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs)
            If args.IsSettingsSelected Then
                contentFrame5.Navigate(GetType(SampleSettingsPage))
            Else
                Dim selectedItem1 = CType(args.SelectedItem, Microsoft.UI.Xaml.Controls.NavigationViewItem)
                Dim selectedItemTag As String = (CStr(selectedItem1.Tag))
                sender.Header = "Sample Page " & selectedItemTag.Substring(selectedItemTag.Length - 1)
                Dim pageName As String = "AppUIBasics.SamplePages." & selectedItemTag
                Dim pageType As Type = Type.[GetType](pageName)
                contentFrame5.Navigate(pageType)
            End If
        End Sub
        Private Sub NavigationView_SelectionChanged6(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs)
            If args.IsSettingsSelected Then
                contentFrame6.Navigate(GetType(SampleSettingsPage))
            Else
                Dim selectedItem1 = CType(args.SelectedItem, Microsoft.UI.Xaml.Controls.NavigationViewItem)
                Dim pageName As String = "AppUIBasics.SamplePages." & (CStr(selectedItem1.Tag))
                Dim pageType As Type = Type.[GetType](pageName)
                contentFrame6.Navigate(pageType)
            End If
        End Sub
        Private Sub NavigationView_SelectionChanged7(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs)
            If args.IsSettingsSelected Then
                contentFrame7.Navigate(GetType(SampleSettingsPage))
            Else
                Dim selectedItem1 = CType(args.SelectedItem, Microsoft.UI.Xaml.Controls.NavigationViewItem)
                Dim pageName As String = "AppUIBasics.SamplePages." & (CStr(selectedItem1.Tag))
                Dim pageType As Type = Type.[GetType](pageName)

                contentFrame7.Navigate(pageType, Nothing, args.RecommendedNavigationTransitionInfo)
            End If
        End Sub
        Private Sub NavigationView_SelectionChanged8(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs)
             ' TODO: Check, VB does not directly support MemberAccess off a Conditional If Expression
            Dim tempVar = sender.Name
            Dim sampleNum As String = tempVar.Substring(8)
            Debug.Print("num: " & sampleNum & Microsoft.VisualBasic.vbLf)

            If args.IsSettingsSelected Then
                contentFrame8.Navigate(GetType(SampleSettingsPage))
            Else
                Dim selectedItem1 = CType(args.SelectedItem, Microsoft.UI.Xaml.Controls.NavigationViewItem)
                Dim selectedItemTag As String = (CStr(selectedItem1.Tag))
                sender.Header = "Sample Page " & selectedItemTag.Substring(selectedItemTag.Length - 1)
                Dim pageName As String = "AppUIBasics.SamplePages." & selectedItemTag
                Dim pageType As Type = Type.[GetType](pageName)
                contentFrame8.Navigate(pageType)
            End If
        End Sub
        Private Sub NavigationView_SelectionChanged9(sender As Microsoft.UI.Xaml.Controls.NavigationView, args As Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs)
            Dim selectedItem1 = CType(args.SelectedItem, Microsoft.UI.Xaml.Controls.NavigationViewItem)
            Dim pageName As String = "AppUIBasics.SamplePages." & (CStr(selectedItem1.Tag))
            Dim pageType As Type = Type.[GetType](pageName)

            contentFrame9.Navigate(pageType, Nothing, args.RecommendedNavigationTransitionInfo)
        End Sub
        Private Sub databindHeader_Checked(sender As Object, e As RoutedEventArgs)
            Categories = New ObservableCollection(Of CategoryBase) From { _
                New Header With
{
                    .Name = "Header1 "},
                New Category With
{
                    .Name = "Category 1",
                    .Glyph = Symbol.Home,
                    .Tooltip = "This is category 1"},
                New Category With
{
                    .Name = "Category 2",
                    .Glyph = Symbol.Keyboard,
                    .Tooltip = "This is category 2"},
                New Separator,
                New Header With
{
                    .Name = "Header2 "},
                New Category With
{
                    .Name = "Category 3",
                    .Glyph = Symbol.Library,
                    .Tooltip = "This is category 3"},
                New Category With
{
                    .Name = "Category 4",
                    .Glyph = Symbol.Mail,
                    .Tooltip = "This is category 3"}
            }
        End Sub
        Private Sub databindHeader_Checked_Unchecked(sender As Object, e As RoutedEventArgs)
            Categories = New ObservableCollection(Of CategoryBase) From { _
                New Category With
{
                    .Name = "Category 1",
                    .Glyph = Symbol.Home,
                    .Tooltip = "This is category 1"},
                New Category With
{
                    .Name = "Category 2",
                    .Glyph = Symbol.Keyboard,
                    .Tooltip = "This is category 2"},
                New Category With
{
                    .Name = "Category 3",
                    .Glyph = Symbol.Library,
                    .Tooltip = "This is category 3"},
                New Category With
{
                    .Name = "Category 4",
                    .Glyph = Symbol.Mail,
                    .Tooltip = "This is category 3"}
            }
        End Sub
        Private Sub Grid_ManipulationDelta1(sender As Object, e As Microsoft.UI.Xaml.Input.ManipulationDeltaRoutedEventArgs)
            Dim grid1 = TryCast(sender, Grid)
            grid1.Width = grid1.ActualWidth + e.Delta.Translation.X
        End Sub
        Private Sub headerCheck_Click(sender As Object, e As RoutedEventArgs)
            nvSample.AlwaysShowHeader = If(TryCast(sender, CheckBox).IsChecked = True, True, False)
        End Sub
        Private Sub settingsCheck_Click(sender As Object, e As RoutedEventArgs)
            nvSample.IsSettingsVisible = If(TryCast(sender, CheckBox).IsChecked = True, True, False)
        End Sub
        Private Sub visibleCheck_Click(sender As Object, e As RoutedEventArgs)
            If TryCast(sender, CheckBox).IsChecked = True Then
                nvSample.IsBackButtonVisible = Microsoft.UI.Xaml.Controls.NavigationViewBackButtonVisible.Visible
            Else
                nvSample.IsBackButtonVisible = Microsoft.UI.Xaml.Controls.NavigationViewBackButtonVisible.Collapsed
            End If
        End Sub
        Private Sub enableCheck_Click(sender As Object, e As RoutedEventArgs)
            nvSample.IsBackEnabled = If(TryCast(sender, CheckBox).IsChecked = True, True, False)
        End Sub
        Private Sub autoSuggestCheck_Click(sender As Object, e As RoutedEventArgs)
            If TryCast(sender, CheckBox).IsChecked = True Then
                Dim asb As New AutoSuggestBox() With
{
                    .QueryIcon = New SymbolIcon(Symbol.Find)}
                asb.SetValue(AutomationProperties.NameProperty, "search")
                nvSample.AutoSuggestBox = asb

                setASBSubstitutionString()
            Else
                nvSample.AutoSuggestBox = Nothing
                navViewASB.Value = Nothing
            End If
        End Sub
        Private Sub setASBSubstitutionString()
            navViewASB.Value = vbCrLf & "    <NavigationView.AutoSuggestBox> " & vbCrLf & "        <AutoSuggestBox QueryIcon=""Find"" AutomationProperties.Name=""Search"" /> " & vbCrLf & "    <" & "/" & "NavigationView.AutoSuggestBox> " & vbCrLf
        End Sub
        Private Sub panemc_Check_Click(sender As Object, e As RoutedEventArgs)
            If TryCast(sender, CheckBox).IsChecked = True Then
                PaneHyperlink.Visibility = Visibility.Visible
            Else
                PaneHyperlink.Visibility = Visibility.Collapsed
            End If
        End Sub
        Private Sub paneFooterCheck_Click(sender As Object, e As RoutedEventArgs)
            If TryCast(sender, CheckBox).IsChecked = True Then
                FooterStackPanel.Visibility = Visibility.Visible
            Else
                FooterStackPanel.Visibility = Visibility.Collapsed
            End If
        End Sub
        Private Sub panePositionLeft_Checked(sender As Object, e As RoutedEventArgs)
            If TryCast(sender, RadioButton).IsChecked = True Then
                If TryCast(sender, RadioButton).Name = "nvSampleLeft" AndAlso nvSample IsNot Nothing Then
                    nvSample.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Left
                    nvSample.IsPaneOpen = True
                    FooterStackPanel.Orientation = Orientation.Vertical
                ElseIf TryCast(sender, RadioButton).Name = "nvSample8Left" AndAlso nvSample8 IsNot Nothing Then
                    nvSample8.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Left
                    nvSample8.IsPaneOpen = True
                ElseIf TryCast(sender, RadioButton).Name = "nvSample9Left" AndAlso nvSample9 IsNot Nothing Then
                    nvSample9.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Left
                    nvSample9.IsPaneOpen = True
                End If
            End If
        End Sub
        Private Sub panePositionTop_Checked(sender As Object, e As RoutedEventArgs)
            If TryCast(sender, RadioButton).IsChecked = True Then
                If TryCast(sender, RadioButton).Name = "nvSampleTop" AndAlso nvSample IsNot Nothing Then
                    nvSample.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Top
                    nvSample.IsPaneOpen = False
                    FooterStackPanel.Orientation = Orientation.Horizontal
                ElseIf TryCast(sender, RadioButton).Name = "nvSample8Top" AndAlso nvSample8 IsNot Nothing Then
                    nvSample8.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Top
                    nvSample8.IsPaneOpen = False
                ElseIf TryCast(sender, RadioButton).Name = "nvSample9Top" AndAlso nvSample9 IsNot Nothing Then
                    nvSample9.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Top
                    nvSample9.IsPaneOpen = False
                End If
            End If
        End Sub
        Private Sub panePositionLeftCompact_Checked(sender As Object, e As RoutedEventArgs)
            If TryCast(sender, RadioButton).IsChecked = True Then
                If TryCast(sender, RadioButton).Name = "nvSample8LeftCompact" AndAlso nvSample8 IsNot Nothing Then
                    nvSample8.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.LeftCompact
                    nvSample8.IsPaneOpen = False
                End If
            End If
        End Sub
        Private Sub sffCheck_Click(sender As Object, e As RoutedEventArgs)
            If TryCast(sender, CheckBox).IsChecked = True Then
                nvSample.SelectionFollowsFocus = Microsoft.UI.Xaml.Controls.NavigationViewSelectionFollowsFocus.Enabled
            Else
                nvSample.SelectionFollowsFocus = Microsoft.UI.Xaml.Controls.NavigationViewSelectionFollowsFocus.Disabled
            End If
        End Sub
        Private Sub suppressselectionCheck_Checked_Click(sender As Object, e As RoutedEventArgs)
            SamplePage2Item.SelectsOnInvoked = If(TryCast(sender, CheckBox).IsChecked = True, False, True)
        End Sub
    End Class
End Namespace
