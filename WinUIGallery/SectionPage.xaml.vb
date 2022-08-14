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
Imports System.Linq
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics
    ''' <summary>
    ''' A page that displays an overview of a single group, including a preview of the items
    ''' within the group.
    ''' </summary>
    Public NotInheritable Partial Class SectionPage
        Inherits ItemsPageBase
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Protected Overrides Async Sub OnNavigatedTo(e As NavigationEventArgs)
            MyBase.OnNavigatedTo(e)
            Dim args As NavigationRootPageArgs = CType(e.Parameter, NavigationRootPageArgs)
            Dim navigationRootPage1 As NavigationRootPage = args.NavigationRootPage
            Dim group = Await ControlInfoDataSource.Instance.GetGroupAsync(CStr(args.Parameter))

            Dim menuItem = CType(navigationRootPage1.NavigationView.MenuItems.[Single](Function(i) CStr(CType(i, Microsoft.UI.Xaml.Controls.NavigationViewItemBase).Tag) = group.UniqueId), Microsoft.UI.Xaml.Controls.NavigationViewItemBase)
            menuItem.IsSelected = True
            navigationRootPage1.NavigationView.Header = menuItem.Content

            Items = group.Items.OrderBy(Function(i) i.Title).ToList()
        End Sub
        Protected Overrides Function GetIsNarrowLayoutState() As Boolean
            Return LayoutVisualStates.CurrentState = NarrowLayout
        End Function
    End Class
End Namespace
