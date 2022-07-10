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

Imports AppUIBasics.Data
Imports System.Linq
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics
    ''' <summary>
    ''' A page that displays a grouped collection of items.
    ''' </summary>
    Public NotInheritable Partial Class AllControlsPage
        Inherits ItemsPageBase
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            Dim args As NavigationRootPageArgs = CType(e.Parameter, NavigationRootPageArgs)

            Dim menuItem = CType(args.NavigationRootPage.NavigationView.MenuItems.ElementAt(1), Microsoft.UI.Xaml.Controls.NavigationViewItem)
            menuItem.IsSelected = True
            args.NavigationRootPage.NavigationView.Header = String.Empty

            Items = ControlInfoDataSource.Instance.Groups.SelectMany(Function(g) g.Items).OrderBy(Function(i) i.Title).ToList()
        End Sub
        Protected Overrides Function GetIsNarrowLayoutState() As Boolean
            Return LayoutVisualStates.CurrentState = NarrowLayout
        End Function
    End Class
End Namespace
