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
Imports System.Collections.Generic
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class SemanticZoomPage
        Inherits Page
        Private _groups As IEnumerable(Of ControlInfoDataGroup)

        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Public ReadOnly Property Groups As IEnumerable(Of ControlInfoDataGroup)
            Get
                Return Me._groups
            End Get
        End Property
        Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            MyBase.OnNavigatedTo(e)

            _groups = ControlInfoDataSource.Instance.Groups
        End Sub
        Private Sub List_GotFocus(sender As Object, e As RoutedEventArgs)
            Control1.StartBringIntoView()
        End Sub
    End Class
End Namespace
