'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************


Imports AppUIBasics.Data
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports Windows.Foundation.Metadata
Imports Windows.System
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Input
Imports Microsoft.UI.Xaml.Media.Animation
#If Not UNIVERSAL

Imports System.ComponentModel
#Else

using Microsoft.UI.Xaml.Data;

#End If

Namespace AppUIBasics
    Public MustInherit Class ItemsPageBase
        Inherits Page
        Implements INotifyPropertyChanged
        Public Event PropertyChanged As PropertyChangedEventHandler Implements ComponentModel.INotifyPropertyChanged.PropertyChanged
        Private _itemId As String
        Private _items As IEnumerable(Of ControlInfoDataItem)
        Public Property Items As IEnumerable(Of ControlInfoDataItem)
            Get
                Return _items
            End Get

            Set(value As IEnumerable(Of ControlInfoDataItem))
                SetProperty(_items, value)
            End Set
        End Property
        ''' <summary>
        ''' Gets a value indicating whether the application's view is currently in "narrow" mode - i.e. on a mobile-ish device.
        ''' </summary>
        Protected Overridable Function GetIsNarrowLayoutState() As Boolean
            Throw New NotImplementedException
        End Function
        Protected Sub OnItemGridViewContainerContentChanging(sender As ListViewBase, args As ContainerContentChangingEventArgs)
            Dim TempVar As Boolean = TypeOf sender.ContainerFromItem(sender.Items.LastOrDefault()) Is GridViewItem
            Dim container As GridViewItem = sender.ContainerFromItem(sender.Items.LastOrDefault())
            If TempVar Then
                container.XYFocusDown = container
            End If

            Dim item1 = TryCast(args.Item, ControlInfoDataItem)
            If item1 IsNot Nothing Then
                args.ItemContainer.IsEnabled = item1.IncludedInBuild
            End If
        End Sub
        Protected Sub OnItemGridViewItemClick(sender As Object, e As ItemClickEventArgs)
            Dim gridView1 = CType(sender, GridView)
            Dim item1 = CType(e.ClickedItem, ControlInfoDataItem)

            _itemId = item1.UniqueId

            NavigationRootPage.GetForElement(Me).Navigate(GetType(ItemPage), _itemId, New DrillInNavigationTransitionInfo)
        End Sub
        Protected Sub OnItemGridViewKeyDown(sender As Object, e As KeyRoutedEventArgs)
            If e.Key = VirtualKey.Up Then
                Dim nextElement = FocusManager.FindNextElement(FocusNavigationDirection.Up)
                If nextElement?.[GetType]() = GetType(Microsoft.UI.Xaml.Controls.NavigationViewItem) Then
                    NavigationRootPage.GetForElement(Me).PageHeader.Focus(FocusState.Programmatic)
                Else
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Up)
                End If
            End If
        End Sub
        Protected Async Sub OnItemGridViewLoaded(sender As Object, e As RoutedEventArgs)
            If _itemId IsNot Nothing Then
                Dim gridView1 = CType(sender, GridView)
                Dim items1 As Collections.Generic.IEnumerable(Of ControlInfoDataItem) = TryCast(gridView1.ItemsSource, IEnumerable(Of ControlInfoDataItem))
                Dim item1 = items1?.FirstOrDefault(Function(s) s.UniqueId = _itemId)
                If item1 IsNot Nothing Then
                    gridView1.ScrollIntoView(item1)

                    If NavigationRootPage.GetForElement(Me).IsFocusSupported Then
                        CType(gridView1.ContainerFromItem(item1), GridViewItem)?.Focus(FocusState.Programmatic)
                    End If

                    Dim animation As ConnectedAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("controlAnimation")

                    If animation IsNot Nothing Then
                        ' Setup the "basic" configuration if the API is present.
                        If ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7) Then
                            animation.Configuration = New BasicConnectedAnimationConfiguration
                        End If

                        Await gridView1.TryStartConnectedAnimationAsync(animation, item1, "controlRoot")
                    End If
                End If
            End If
        End Sub
        Protected Sub OnItemGridViewSizeChanged(sender As Object, e As SizeChangedEventArgs)
            Dim gridView1 = CType(sender, GridView)
            Dim TempVar1 As Boolean = TypeOf gridView1.ItemsPanelRoot Is ItemsWrapGrid
            Dim wrapGrid As ItemsWrapGrid = gridView1.ItemsPanelRoot
            If TempVar1 Then
                If GetIsNarrowLayoutState() Then
                    Dim wrapGridPadding As Double = 88
                    wrapGrid.HorizontalAlignment = HorizontalAlignment.Center

                    wrapGrid.ItemWidth = gridView1.ActualWidth - gridView1.Padding.Left - gridView1.Padding.Right - wrapGridPadding
                Else
                    wrapGrid.HorizontalAlignment = HorizontalAlignment.Left
                    wrapGrid.ItemWidth = Double.NaN
                End If
            End If
        End Sub
        Protected Function SetProperty(Of T)(ByRef storage As T, value As T, <CallerMemberName> Optional propertyName As String = Nothing) As Boolean
            If Equals(storage, value) Then
                Return False
            End If

            storage = value
            NotifyPropertyChanged(propertyName)
            Return True
        End Function
        Protected Sub NotifyPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class
End Namespace
