' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Markup
Imports System

Namespace AppUIBasics.Common
    <ContentProperty(Name:="ItemTemplate")>
    Class MenuItemTemplateSelector
        Inherits DataTemplateSelector
        Public Property ItemTemplate As DataTemplate
        'public string PaneTitle { get; set; }
        Protected Overrides Function SelectTemplateCore(item As Object) As DataTemplate
            Return If(TypeOf item Is Separator, SeparatorTemplate, If(TypeOf item Is Header, HeaderTemplate, ItemTemplate))
        End Function
        Protected Overrides Function SelectTemplateCore(item As Object, container As DependencyObject) As DataTemplate
            Return If(TypeOf item Is Separator, SeparatorTemplate, If(TypeOf item Is Header, HeaderTemplate, ItemTemplate))
        End Function
        Friend HeaderTemplate As DataTemplate = CType(XamlReader.Load( _
            "<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
                   <NavigationViewItemHeader Content='{Binding Name}' />
                  </DataTemplate>"), DataTemplate)
        Friend SeparatorTemplate As DataTemplate = CType(XamlReader.Load( _
            "<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>
                    <NavigationViewItemSeparator />
                  </DataTemplate>"), DataTemplate)
    End Class
End Namespace
