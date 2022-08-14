'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************


Imports System.Linq
Imports System.Numerics
Imports System.Reflection
Imports AppUIBasics.Data
Imports AppUIBasics.Helper
Imports Windows.Foundation
Imports Windows.Foundation.Metadata
Imports Windows.System
Imports Microsoft.UI.Composition
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Hosting
Imports Microsoft.UI.Xaml.Input
Imports Microsoft.UI.Xaml.Media.Animation
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics
    ''' <summary>
    ''' A page that displays details for a single item within a group.
    ''' </summary>
    Public Partial Class ItemPage
        Inherits Page
        Private _compositor As Compositor
        Private _item As ControlInfoDataItem
        Private _currentElementTheme? As ElementTheme
        Public Property Item As ControlInfoDataItem
            Get
                Return _item
            End Get

            Set(value As ControlInfoDataItem)
                _item = value
            End Set
        End Property

        Public Sub New()
            Me.InitializeComponent()

            AddHandler LayoutVisualStates.CurrentStateChanged, Sub(s, e) UpdateSeeAlsoPanelVerticalTranslationAnimation()
            AddHandler Loaded, Sub(s, e) SetInitialVisuals()
            AddHandler Unloaded, AddressOf ItemPage_Unloaded
        End Sub
        Private Sub ItemPage_Unloaded(sender As Object, e As RoutedEventArgs)
            ' Notifying the pageheader that this Itempage was unloaded
            Dim navigationRootPage1 As NavigationRootPage = NavigationRootPage.GetForElement(Me)
            If navigationRootPage1 IsNot Nothing Then
                navigationRootPage1.PageHeader.Event_ItemPage_Unloaded(sender, e)
            End If
        End Sub
        Public Sub SetInitialVisuals()
            Dim navigationRootPage1 = NavigationRootPage.GetForElement(Me)
            If navigationRootPage1 IsNot Nothing Then
                navigationRootPage1.PageHeader.TopCommandBar.Visibility = Visibility.Visible
                navigationRootPage1.PageHeader.ToggleThemeAction = AddressOf OnToggleTheme
                navigationRootPage1.NavigationViewLoaded = AddressOf OnNavigationViewLoaded
                navigationRootPage1.PageHeader.CopyLinkAction = AddressOf OnCopyLink
                navigationRootPage1.PageHeader.ResetCopyLinkButton()

                If navigationRootPage1.IsFocusSupported Then
                    Me.Focus(FocusState.Programmatic)
                End If
            End If

            _compositor = ElementCompositionPreview.GetElementVisual(Me).Compositor

            UpdateSeeAlsoPanelVerticalTranslationAnimation()

            If UIHelper.IsScreenshotMode Then
                Dim controlExamples = TryCast(Me.contentFrame.Content, UIElement)?.GetDescendantsOfType(Of ControlExample)()

                If controlExamples IsNot Nothing Then
                    For Each controlExample1 In controlExamples
                        VisualStateManager.GoToState(controlExample1, "ScreenshotMode", False)
                    Next
                End If
            End If
        End Sub
        Private Sub UpdateSeeAlsoPanelVerticalTranslationAnimation()
            Dim isEnabled As Boolean = LayoutVisualStates.CurrentState = LargeLayout

            ElementCompositionPreview.SetIsTranslationEnabled(seeAlsoPanel, True)

            Dim targetPanelVisual = ElementCompositionPreview.GetElementVisual(seeAlsoPanel)
            targetPanelVisual.Properties.InsertVector3("Translation", Vector3.Zero)

            If isEnabled Then
                Dim scrollProperties = ElementCompositionPreview.GetScrollViewerManipulationPropertySet(svPanel)

                Dim expression = _compositor.CreateExpressionAnimation("ScrollManipulation.Translation.Y * -1")
                expression.SetReferenceParameter("ScrollManipulation", scrollProperties)
                expression.Target = "Translation.Y"
                targetPanelVisual.StartAnimation(expression.Target, expression)
            Else
                targetPanelVisual.StopAnimation("Translation.Y")
            End If
        End Sub
        Private Sub OnNavigationViewLoaded()
            NavigationRootPage.GetForElement(Me).EnsureNavigationSelection(Me.Item.UniqueId)
        End Sub
        Private Sub OnCopyLink()
            ProtocolActivationClipboardHelper.Copy(Me.Item)
        End Sub
        Private Sub OnToggleTheme()
            Dim currentElementTheme = If(((If(_currentElementTheme, ElementTheme.[Default])) = ElementTheme.[Default]), ThemeHelper.ActualTheme, _currentElementTheme.Value)
            Dim newTheme = If(currentElementTheme = ElementTheme.Dark, ElementTheme.Light, ElementTheme.Dark)
            SetControlExamplesTheme(newTheme)
        End Sub
        Private Sub SetControlExamplesTheme(theme As ElementTheme)
            Dim controlExamples = TryCast(Me.contentFrame.Content, UIElement)?.GetDescendantsOfType(Of ControlExample)()

            If controlExamples IsNot Nothing Then
                _currentElementTheme = theme
                For Each controlExample1 In controlExamples
                    Dim exampleContent = TryCast(controlExample1.Example, FrameworkElement)
                    exampleContent.RequestedTheme = theme
                    controlExample1.ExampleContainer.RequestedTheme = theme
                Next
            End If
        End Sub
        Private Sub OnRelatedControlClick(sender As Object, e As RoutedEventArgs)
            Dim b As ButtonBase = CType(sender, ButtonBase)

            NavigationRootPage.GetForElement(Me).Navigate(GetType(ItemPage), b.DataContext.ToString())
        End Sub
        Protected Overrides Async Sub OnNavigatedTo(e As NavigationEventArgs)
            Dim args As NavigationRootPageArgs = CType(e.Parameter, NavigationRootPageArgs)
            Dim item1 = Await ControlInfoDataSource.Instance.GetItemAsync(CStr(args.Parameter))

            If item1 IsNot Nothing Then
                Item = item1

                ' Load control page into frame.
                Dim pageRoot As String = "AppUIBasics.ControlPages."
                Dim pageString As String = pageRoot & item1.UniqueId & "Page"
                Dim pageType As Type = Type.[GetType](pageString)

                If pageType IsNot Nothing Then
                    ' Pagetype is not null!
                    ' So lets generate the github links and set them!
                    Dim gitHubBaseURI As String = "https://github.com/microsoft/WinUI-Gallery/tree/main/WinUIGallery/ControlPages/"
                    Dim pageName As String = pageType.Name & ".xaml"
                    PageCodeGitHubLink.NavigateUri = New Uri(gitHubBaseURI & pageName & ".cs")
                    PageMarkupGitHubLink.NavigateUri = New Uri(gitHubBaseURI & pageName)

                    Debug.WriteLine(String.Format("[ItemPage] Navigate to {0}", pageType.ToString()))
                    Me.contentFrame.Navigate(pageType)
                End If

                args.NavigationRootPage.NavigationView.Header = item1?.Title
                args.NavigationRootPage.EnsureNavigationSelection(item1?.UniqueId)
            End If

            MyBase.OnNavigatedTo(e)
        End Sub
        Protected Overrides Sub OnKeyDown(e As KeyRoutedEventArgs)
            If e.Key = Windows.System.VirtualKey.Escape Then
                Me.Item = Nothing
                If Me.contentFrame.CanGoBack Then
                    Me.contentFrame.GoBack()
                Else
                    NavigationRootPage.GetForElement(Me).Navigate(GetType(AllControlsPage))
                End If
            End If
        End Sub
        Protected Overrides Sub OnNavigatingFrom(e As NavigatingCancelEventArgs)
            SetControlExamplesTheme(ThemeHelper.ActualTheme)

            MyBase.OnNavigatingFrom(e)
        End Sub
        Protected Overrides Sub OnNavigatedFrom(e As NavigationEventArgs)
            Dim navigationRootPage1 = NavigationRootPage.GetForElement(Me)
            If navigationRootPage1 IsNot Nothing Then
                navigationRootPage1.NavigationViewLoaded = Nothing
                navigationRootPage1.PageHeader.TopCommandBar.Visibility = Visibility.Collapsed
                navigationRootPage1.PageHeader.ToggleThemeAction = Nothing
                navigationRootPage1.PageHeader.CopyLinkAction = Nothing

                ' Reverse Connected Animation
                If e.SourcePageType <> GetType(ItemPage) Then
                    Dim pageHeader1 As PageHeader = navigationRootPage1.PageHeader

                    If pageHeader1.Visibility = Visibility.Visible Then
                        ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("controlAnimation", pageHeader1.TitlePanel)
                    End If
                End If
            End If

            ' We use reflection to call the OnNavigatedFrom function the user leaves this page
            ' See this PR for more information: https://github.com/microsoft/WinUI-Gallery/pull/145
            Dim contentFrameAsFrame As Frame = TryCast(contentFrame, Frame)
            Dim innerPage As Page = TryCast(contentFrameAsFrame.Content, Page)
            If innerPage IsNot Nothing Then
                Dim dynMethod As MethodInfo = innerPage.[GetType]().GetMethod("OnNavigatedFrom",
        BindingFlags.NonPublic Or BindingFlags.Instance)
                dynMethod.Invoke(innerPage, New Object() {e})
            End If

            MyBase.OnNavigatedFrom(e)
        End Sub
        Private Sub OnContentRootSizeChanged(sender As Object, e As SizeChangedEventArgs)
            Dim targetState As String = "NormalFrameContent"

            If (contentColumn.ActualWidth) >= 1000 Then
                targetState = "WideFrameContent"
            End If

            VisualStateManager.GoToState(Me, targetState, False)
        End Sub
    End Class
End Namespace
