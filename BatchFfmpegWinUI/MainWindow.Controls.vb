﻿Option Strict Off

Imports Microsoft.UI
Imports Microsoft.UI.Windowing
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Markup
Imports Microsoft.UI.Xaml.Media
Imports <xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation">

Partial Class MainWindow
    WithEvents LayoutRoot As New Grid With {
        .Margin = New Thickness(4),
        .Background = New SolidColorBrush(Microsoft.UI.Colors.Transparent),
        .AllowDrop = True
    }

    WithEvents TblTitleText As New TextBlock With {
        .Margin = New Thickness(4)
    }

    WithEvents ConvertStatus As New TextBlock With {
        .Text = "Ready. Drag and drop mp4 files to add to conversion list.",
        .Margin = New Thickness(4)
    }

    WithEvents ConvertingFiles As New ListBox With {
        .Margin = New Thickness(0, 4, 0, 0)
    }

    WithEvents BtnConvertStop As New Button With {
        .Content = "Convert", .HorizontalAlignment = HorizontalAlignment.Right
    }

    Private ReadOnly _backdrop As BackdropHelper

    Sub New()

        InitializeComponents()

        _backdrop = New BackdropHelper(Me)
        _backdrop.SetBackdrop(BackdropType.Mica)

        With GetAppWindow.TitleBar
            .ExtendsContentIntoTitleBar = True
            .ButtonBackgroundColor = Colors.Transparent
        End With
    End Sub

    Private Shared Function GetTitleBarHeight(appWnd As AppWindow) As Integer
        Dim titleBarHeight = appWnd.TitleBar.Height
        If titleBarHeight = 0 Then
            ' Sometimes appWnd.TitleBar.Height = 0. Use values from WinForms as fallback.
            titleBarHeight = System.Windows.Forms.SystemInformation.CaptionHeight
        End If

        Return titleBarHeight
    End Function

    Private Sub InitializeComponents()
        Title = "WinUI 3 VB Demo - H265 mp4 converter"

        ' Crash if App.xbf doesn't exist.
        ' UseRoundCornerdUI()

        Dim appWnd = GetAppWindow
        Dim titleBarHeight = GetTitleBarHeight(appWnd)

        With LayoutRoot.RowDefinitions
            .Add(New RowDefinition With {.Height = New GridLength(titleBarHeight + 4, GridUnitType.Pixel)})
            .Add(New RowDefinition With {.Height = GridLength.Auto})
            .Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
        End With
        Content = LayoutRoot

        TblTitleText.SetBinding(TextBlock.TextProperty,
         New Data.Binding With {
            .Source = Me, .Path = New PropertyPath(NameOf(Title)), .Mode = Data.BindingMode.OneWay
        })
        LayoutRoot.Children.Add(TblTitleText)

        Grid.SetRow(ConvertStatus, 1)
        LayoutRoot.Children.Add(ConvertStatus)

        Grid.SetRow(BtnConvertStop, 1)
        LayoutRoot.Children.Add(BtnConvertStop)

        Grid.SetRow(ConvertingFiles, 2)
        Dim fileTemplate As DataTemplate = XamlReader.Load((
            <DataTemplate>
                <Grid>
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="32"/>
                        <ColumnDefinition Width="*"/>
                    </Grid.ColumnDefinitions>
                    <FontIcon FontFamily="{ThemeResource SymbolThemeFontFamily}"
                        Glyph="{Binding Path=Icon, Mode=OneWay}"/>
                    <TextBlock Grid.Column="1" Text="{Binding Path=Path, Mode=OneTime}"/>
                </Grid>
            </DataTemplate>
        ).ToString)

        ConvertingFiles.ItemTemplate = fileTemplate
        LayoutRoot.Children.Add(ConvertingFiles)
    End Sub

    Private Sub UseRoundCornerdUI()
        Dim themes As New ResourceDictionary With {
            .Source = New Uri("ms-appx:///Microsoft.UI.Xaml/Themes/themeresources.xaml")
        }

        LayoutRoot.Resources.MergedDictionaries.Add(themes)
    End Sub

    Private Sub MainWindow_Activated(sender As Object, args As WindowActivatedEventArgs) Handles Me.Activated
        WinUIVbHost.Instance.CurrentWindow = Me
    End Sub
End Class
