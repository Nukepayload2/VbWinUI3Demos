' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports AppUIBasics.Helper
Imports Microsoft.Xaml.Interactivity
Imports System.Linq
Imports Windows.Storage
Imports Microsoft.UI
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media

Namespace AppUIBasics.Behaviors
    Public Class ImageScrollBehavior
        Inherits DependencyObject
        Implements IBehavior
        Private Const _opacityMaxValue As Integer = 250
        Private Const _alpha As Integer = 255
        Private Const _maxFontSize As Integer = 28
        Private Const _minFontSize As Integer = 10
        Private Const scrollViewerThresholdValue As Integer = 85
        Private scrollViewer1 As ScrollViewer
        Private listGridView As ListViewBase

        Private _associatedObject As DependencyObject
        Public Property AssociatedObject As DependencyObject
            Get
                Return _associatedObject
            End Get
            Private Set(value As DependencyObject)
                _associatedObject = value
            End Set
        End Property
        Public Property TargetControl As Control
            Get
                Return CType(GetValue(TargetControlProperty), Control)
            End Get

            Set(value As Control)
                SetValue(TargetControlProperty, value)
            End Set
        End Property
        ' Using a DependencyProperty as the backing store for TargetControl.  This enables animation, styling, binding, etc...
        Public Shared ReadOnly TargetControlProperty As DependencyProperty = DependencyProperty.Register("TargetControl", GetType(Control), GetType(ImageScrollBehavior), New PropertyMetadata(Nothing))
        Public Sub Attach(associatedObject1 As DependencyObject)
            AssociatedObject = associatedObject1
            If Not GetScrollViewer() Then
                AddHandler CType(associatedObject1, ListViewBase).Loaded, AddressOf ListGridView_Loaded
            End If
        End Sub
        Private Sub ListGridView_Loaded(sender As Object, e As RoutedEventArgs)
            GetScrollViewer()
            listGridView = TryCast(sender, ListViewBase)
        End Sub
        Private Function GetScrollViewer() As Boolean
            scrollViewer1 = Helper.UIHelper.GetDescendantsOfType(Of ScrollViewer)(AssociatedObject).FirstOrDefault()
            If scrollViewer1 IsNot Nothing Then
                AddHandler scrollViewer1.ViewChanging, AddressOf ScrollViewer_ViewChanging
                Return True
            End If
            Return False
        End Function
        Private Sub ScrollViewer_ViewChanging(sender As Object, e As ScrollViewerViewChangingEventArgs)
            Dim verticalOffset1 As Double = CType(sender, ScrollViewer).VerticalOffset
            Dim header = CType(TargetControl, PageHeader)
            header.BackgroundColorOpacity = verticalOffset1 / _opacityMaxValue
            header.AcrylicOpacity = 0.3 * (1 - (verticalOffset1 / _opacityMaxValue))
            If verticalOffset1 < 10 Then
                VisualStateManager.GoToState(header, "DefaultForeground", False)
                header.BackgroundColorOpacity = 0
                header.FontSize = 28
                header.AcrylicOpacity = 0.3
            ElseIf verticalOffset1 > scrollViewerThresholdValue Then
                VisualStateManager.GoToState(header, "AlternateForeground", False)
                header.FontSize = _minFontSize
            Else
                If ThemeHelper.ActualTheme <> ElementTheme.Dark Then
                    VisualStateManager.GoToState(header, "DefaultForeground", False)
                    Dim foreground As New Color() With
{
                        .A = CByte((If((verticalOffset1 > scrollViewerThresholdValue), 0, (_alpha * (1 - (verticalOffset1 / scrollViewerThresholdValue))))))}
                    foreground.R = __InlineAssignHelper(foreground.G, __InlineAssignHelper(foreground.B, 0))
                    header.Foreground = New SolidColorBrush(foreground)
                Else
                    VisualStateManager.GoToState(header, "AlternateForeground", False)
                End If

                header.FontSize = -(((verticalOffset1 / scrollViewerThresholdValue) * (_maxFontSize - _minFontSize)) - _maxFontSize)
            End If
        End Sub
        Public Sub Detach()
            RemoveHandler CType(AssociatedObject, ListViewBase).Loaded, AddressOf ListGridView_Loaded
            AssociatedObject = Nothing
        End Sub
        <Obsolete("Please refactor code that uses this function, it is a simple work-around to simulate inline assignment in VB!")>
        Private Shared Function __InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
End Namespace
