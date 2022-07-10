' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports AppUIBasics.Data
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class ParallaxViewPage
        Inherits ItemsPageBase
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            MyBase.OnNavigatedTo(e)

            Items = ControlInfoDataSource.Instance.Groups.SelectMany(Function(g) g.Items).OrderBy(Function(i) i.Title).ToList()
        End Sub
    End Class
End Namespace
