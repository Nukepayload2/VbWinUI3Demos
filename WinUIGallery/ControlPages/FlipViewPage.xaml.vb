'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************


Imports AppUIBasics.Common
Imports AppUIBasics.Data
Imports System.Collections.Generic
Imports System.Linq
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class FlipViewPage
        Inherits ItemsPageBase
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            MyBase.OnNavigatedTo(e)

            Items = ControlInfoDataSource.Instance.Groups.Take(3).SelectMany(Function(g) g.Items).ToList()
        End Sub
    End Class
End Namespace
