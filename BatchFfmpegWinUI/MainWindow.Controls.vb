Option Strict Off

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

    WithEvents ConvertStatus As New TextBlock With {
        .Text = "Ready. Drag and drop mp4 files to add to conversion list."
    }

    WithEvents ConvertingFiles As New ListBox With {
        .Margin = New Thickness(0, 4, 0, 0)
    }

    WithEvents BtnConvertStop As New Button With {
        .Content = "Convert", .HorizontalAlignment = HorizontalAlignment.Right
    }

    Sub New()
        Title = "WinUI 3 VB Demo - H265 mp4 converter"
        With LayoutRoot.RowDefinitions
            .Add(New RowDefinition With {.Height = GridLength.Auto})
            .Add(New RowDefinition With {.Height = New GridLength(1, GridUnitType.Star)})
        End With
        Content = LayoutRoot

        LayoutRoot.Children.Add(ConvertStatus)
        LayoutRoot.Children.Add(BtnConvertStop)

        Grid.SetRow(ConvertingFiles, 1)
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

    Private Sub MainWindow_Activated(sender As Object, args As WindowActivatedEventArgs) Handles Me.Activated
        WinUIVbHost.Instance.CurrentWindow = Me
    End Sub
End Class
