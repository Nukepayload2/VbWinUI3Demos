'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************
Imports AppUIBasics.Helper
Imports Microsoft.UI
Imports Microsoft.UI.Composition
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Hosting
Imports System

Namespace AppUIBasics
    Public NotInheritable Partial Class PageHeader
        Inherits UserControl
        Public Property CopyLinkAction As Action
        Public Property ToggleThemeAction As Action
        Public ReadOnly Property TeachingTip1 As TeachingTip
            Get
                Return ToggleThemeTeachingTip1
            End Get
        End Property
        Public ReadOnly Property TeachingTip2 As TeachingTip
            Get
                Return ToggleThemeTeachingTip2
            End Get
        End Property
        Public ReadOnly Property TeachingTip3 As TeachingTip
            Get
                Return ToggleThemeTeachingTip3
            End Get
        End Property
        Public Property Title As Object
            Get
                Return GetValue(TitleProperty)
            End Get

            Set(value As Object)
                SetValue(TitleProperty, value)
            End Set
        End Property
        Public Shared ReadOnly TitleProperty As DependencyProperty = DependencyProperty.Register("Title", GetType(Object), GetType(PageHeader), New PropertyMetadata(Nothing))
        Public Property Subtitle As Object
            Get
                Return GetValue(SubtitleProperty)
            End Get

            Set(value As Object)
                SetValue(SubtitleProperty, value)
            End Set
        End Property
        Public Shared ReadOnly SubtitleProperty As DependencyProperty = DependencyProperty.Register("Subtitle", GetType(Object), GetType(PageHeader), New PropertyMetadata(Nothing))
        Public Property HeaderPadding As Thickness
            Get
                Return CType(GetValue(HeaderPaddingProperty), Thickness)
            End Get

            Set(value As Thickness)
                SetValue(HeaderPaddingProperty, value)
            End Set
        End Property
        ' Using a DependencyProperty as the backing store for BackgroundColorOpacity.  This enables animation, styling, binding, etc...
        Public Shared ReadOnly HeaderPaddingProperty As DependencyProperty = DependencyProperty.Register("HeaderPadding", GetType(Thickness), GetType(PageHeader), New PropertyMetadata(CType(App.Current.Resources("PageHeaderDefaultPadding"), Thickness)))
        Public Property BackgroundColorOpacity As Double
            Get
                Return CDbl(GetValue(BackgroundColorOpacityProperty))
            End Get

            Set(value As Double)
                SetValue(BackgroundColorOpacityProperty, value)
            End Set
        End Property
        ' Using a DependencyProperty as the backing store for BackgroundColorOpacity.  This enables animation, styling, binding, etc...
        Public Shared ReadOnly BackgroundColorOpacityProperty As DependencyProperty = DependencyProperty.Register("BackgroundColorOpacity", GetType(Double), GetType(PageHeader), New PropertyMetadata(0.0))
        Public Property AcrylicOpacity As Double
            Get
                Return CDbl(GetValue(AcrylicOpacityProperty))
            End Get

            Set(value As Double)
                SetValue(AcrylicOpacityProperty, value)
            End Set
        End Property
        ' Using a DependencyProperty as the backing store for BackgroundColorOpacity.  This enables animation, styling, binding, etc...
        Public Shared ReadOnly AcrylicOpacityProperty As DependencyProperty = DependencyProperty.Register("AcrylicOpacity", GetType(Double), GetType(PageHeader), New PropertyMetadata(0.3))
        Public Property ShadowOpacity As Double
            Get
                Return CDbl(GetValue(ShadowOpacityProperty))
            End Get

            Set(value As Double)
                SetValue(ShadowOpacityProperty, value)
            End Set
        End Property
        ' Using a DependencyProperty as the backing store for BackgroundColorOpacity.  This enables animation, styling, binding, etc...
        Public Shared ReadOnly ShadowOpacityProperty As DependencyProperty = DependencyProperty.Register("ShadowOpacity", GetType(Double), GetType(PageHeader), New PropertyMetadata(0.0))
        Public ReadOnly Property TopCommandBar As CommandBar
            Get
                Return topCommandBar1
            End Get
        End Property
        Public ReadOnly Property TitlePanel As UIElement
            Get
                Return pageTitle
            End Get
        End Property

        Public Sub New()
            Me.InitializeComponent()
            ' this.InitializeDropShadow(ShadowHost, TitleTextBlock.GetAlphaMask());
            Me.ResetCopyLinkButton()
        End Sub
        Private Sub OnCopyLinkButtonClick(sender As Object, e As RoutedEventArgs)
            Me.CopyLinkAction?.Invoke()

            If ProtocolActivationClipboardHelper.ShowCopyLinkTeachingTip Then
                Me.CopyLinkButtonTeachingTip.IsOpen = True
            End If

            Me.CopyLinkButton.Label = "Copied to Clipboard"
            Me.CopyLinkButtonIcon.Symbol = Symbol.Accept
        End Sub
        Public Sub OnThemeButtonClick(sender As Object, e As RoutedEventArgs)
            ToggleThemeAction?.Invoke()
        End Sub
        Public Sub ResetCopyLinkButton()
            Me.CopyLinkButtonTeachingTip.IsOpen = False
            Me.CopyLinkButton.Label = "Generate Link to Page"
            Me.CopyLinkButtonIcon.Symbol = Symbol.Link
        End Sub
        Private Sub OnCopyDontShowAgainButtonClick(sender As TeachingTip, args As Object)
            ProtocolActivationClipboardHelper.ShowCopyLinkTeachingTip = False
            Me.CopyLinkButtonTeachingTip.IsOpen = False
        End Sub
        Private Sub ToggleThemeTeachingTip2_ActionButtonClick(sender As Microsoft.UI.Xaml.Controls.TeachingTip, args As Object)
            NavigationRootPage.GetForElement(Me).PageHeader.ToggleThemeAction?.Invoke()
        End Sub
        ''' <summary>
        ''' This method will be called when a <see cref="ItemPage"/> gets unloaded. 
        ''' Put any code in here that should be done when a <see cref="ItemPage"/> gets unloaded.
        ''' </summary>
        ''' <param name="sender">The sender (the ItemPage)</param>
        ''' <param name="e">The <see cref="RoutedEventArgs"/> of the ItemPage that was unloaded.</param>
        Public Sub Event_ItemPage_Unloaded(sender As Object, e As RoutedEventArgs)
        End Sub

        ' private void InitializeDropShadow(UIElement shadowHost, CompositionBrush shadowTargetBrush)
        ' {
        '     Visual hostVisual = ElementCompositionPreview.GetElementVisual(shadowHost);
        '     Compositor compositor = hostVisual.Compositor;

        '     // Create a drop shadow
        '     var dropShadow = compositor.CreateDropShadow();
        '     dropShadow.Color = ColorHelper.FromArgb(102, 0, 0, 0);
        '     dropShadow.BlurRadius = 4.0f;
        '     // Associate the shape of the shadow with the shape of the target element
        '     dropShadow.Mask = shadowTargetBrush;

        '     // Create a Visual to hold the shadow
        '     var shadowVisual = compositor.CreateSpriteVisual();
        '     shadowVisual.Shadow = dropShadow;

        '     // Add the shadow as a child of the host in the visual tree
        '     ElementCompositionPreview.SetElementChildVisual(shadowHost, shadowVisual);

        '     // Make sure size of shadow host and shadow visual always stay in sync
        '     var bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
        '     bindSizeAnimation.SetReferenceParameter("hostVisual", hostVisual);

        '     shadowVisual.StartAnimation("Size", bindSizeAnimation);
        ' }
    End Class
End Namespace
