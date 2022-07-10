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

Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Navigation
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class GridViewPage
        Inherits ItemsPageBase
        Private StyledGridIWG As ItemsWrapGrid

        Public Sub New()
            Me.InitializeComponent()
            Me.DataContext = Me
        End Sub
        Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            MyBase.OnNavigatedTo(e)

            ' Get data objects and place them into an ObservableCollection
            Dim tempList As List(Of CustomDataObject) = CustomDataObject.GetDataObjects()
            Dim Items As New ObservableCollection(Of CustomDataObject)(tempList)
            Dim Items2 As New ObservableCollection(Of CustomDataObject)(tempList)
            BasicGridView.ItemsSource = Items2
            ContentGridView.ItemsSource = Items
            StyledGrid.ItemsSource = Items

            DisplayDT.Value = "<!-- ImageTemplate: -->
<DataTemplate x:Key='ImageTemplate' x:DataType='local1: CustomDataObject'>
    <Image Stretch = 'UniformToFill' Source = '{x:Bind ImageLocation}' 
           AutomationProperties.Name = '{x:Bind Title}' Width = '190' Height = '130' 
           AutomationProperties.AccessibilityView = 'Raw'/>
</DataTemplate> "
        End Sub
        Private Sub ItemTemplate_Checked(sender As Object, e As RoutedEventArgs)
            Dim tag1 = TryCast(sender, FrameworkElement).Tag
            If tag1 IsNot Nothing Then
                Dim template As String = tag1.ToString()
                ContentGridView.ItemTemplate = CType(Me.Resources(template), DataTemplate)
                itemTemplate.Value = template

                If template = "ImageTemplate" Then
                    DisplayDT.Value = "<!-- ImageTemplate: -->
<DataTemplate x:Key='ImageTemplate' x:DataType='local1: CustomDataObject'>
    <Image Stretch = 'UniformToFill' Source = '{x:Bind ImageLocation}' 
           AutomationProperties.Name = '{x:Bind Title}' Width = '190' Height = '130' 
           AutomationProperties.AccessibilityView = 'Raw'/>
</DataTemplate> "

                ElseIf template = "IconTextTemplate" Then
                    DisplayDT.Value = "<!-- IconTextTemplate: -->
<DataTemplate x:Key='IconTextTemplate' x:DataType='local1:CustomDataObject'>
    <RelativePanel AutomationProperties.Name='{x:Bind Title}' Width='280' MinHeight='160'>
        <Image x:Name='image'
               Width='18'
               Margin='0,4,0,0'
               RelativePanel.AlignLeftWithPanel='True'
               RelativePanel.AlignTopWithPanel='True'
               Source='{x:Bind ImageLocation}'
               Stretch='Uniform' />
        <TextBlock x:Name='title' Style='{StaticResource BaseTextBlockStyle}' Margin='8,0,0,0' 
                   Text='{x:Bind Title}' RelativePanel.RightOf='image' RelativePanel.AlignTopWithPanel='True'/>
        <TextBlock Text='{x:Bind Description}' Style='{StaticResource CaptionTextBlockStyle}' 
                   TextWrapping='Wrap' Margin='0,4,8,0' RelativePanel.Below='title' TextTrimming='WordEllipsis'/>
    </RelativePanel>
</DataTemplate>"

                ElseIf template = "ImageTextTemplate" Then
                    DisplayDT.Value = "<!-- ImageTextTemplate: -->
<DataTemplate x: Key = 'ImageTextTemplate' x: DataType = 'local1:CustomDataObject'>
    <Grid AutomationProperties.Name = '{x:Bind Title}' Width = '280'>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width = 'Auto'/>
                <ColumnDefinition Width = '*'/>
        </Grid.ColumnDefinitions>
        <Image Source = '{x:Bind ImageLocation}' Height = '100' Stretch = 'Fill' VerticalAlignment = 'Top'/>
        <StackPanel Grid.Column = '1' Margin = '8,0,0,8'>
            <TextBlock Text = '{x:Bind Title}' Style = '{ThemeResource SubtitleTextBlockStyle}' Margin = '0,0,0,8'/>
            <StackPanel Orientation = 'Horizontal'>
                <TextBlock Text = '{x:Bind Views}' Style = '{ThemeResource CaptionTextBlockStyle}'/>
                    <TextBlock Text = ' Views ' Style = '{ThemeResource CaptionTextBlockStyle}'/>
            </StackPanel>
            <StackPanel Orientation = 'Horizontal'>
                    <TextBlock Text = '{x:Bind Likes}' Style = '{ThemeResource CaptionTextBlockStyle}'/> 
                    <TextBlock Text = ' Likes' Style = '{ThemeResource CaptionTextBlockStyle}'/>
            </StackPanel>
        </StackPanel>
     </Grid>
</DataTemplate>"

                Else
                    DisplayDT.Value = "<!-- TextTemplate: -->
<DataTemplate x:Key='TextTemplate' x:DataType='local1: CustomDataObject'>
    <StackPanel Width = '240' Orientation = 'Horizontal'>
        <TextBlock Style = '{StaticResource TitleTextBlockStyle}' Margin = '8,0,0,0' Text = '{x:Bind Title}'/>
            </StackPanel>
</DataTemplate>"
                End If
            End If
        End Sub
        Private Sub ContentGridView_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim TempVar As Boolean = TypeOf sender Is GridView1
            Dim gridView As GridView1 = sender
            If TempVar Then
                SelectionOutput.Text = String.Format("You have selected {0} item(s).", gridView.SelectedItems.Count)
            End If
        End Sub
        Private Sub ContentGridView_ItemClick(sender As Object, e As ItemClickEventArgs)
            ClickOutput.Text = "You clicked " & TryCast(e.ClickedItem, CustomDataObject).Title & "."
        End Sub
        Private Sub BasicGridView_ItemClick(sender As Object, e As ItemClickEventArgs)
            ClickOutput0.Text = "You clicked " & TryCast(e.ClickedItem, CustomDataObject).Title & "."
        End Sub
        Private Sub ItemClickCheckBox_Click(sender As Object, e As RoutedEventArgs)
            ClickOutput.Text = String.Empty
        End Sub
        Private Sub FlowDirectionCheckBox_Click(sender As Object, e As RoutedEventArgs)
            If ContentGridView.FlowDirection = FlowDirection.LeftToRight Then
                ContentGridView.FlowDirection = FlowDirection.RightToLeft
            Else
                ContentGridView.FlowDirection = FlowDirection.LeftToRight
            End If
        End Sub
        Private Sub SelectionModeComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            If ContentGridView IsNot Nothing Then
                Dim colorName As String = e.AddedItems(0).ToString()
                Select Case colorName
                    Case "None"
                        ContentGridView.SelectionMode = ListViewSelectionMode.None
                        SelectionOutput.Text = String.Empty
                    Case "Single"
                        ContentGridView.SelectionMode = ListViewSelectionMode.[Single]
                    Case "Multiple"
                        ContentGridView.SelectionMode = ListViewSelectionMode.Multiple
                    Case "Extended"
                        ContentGridView.SelectionMode = ListViewSelectionMode.Extended
                End Select
            End If
        End Sub
        Private Sub StyledGrid_InitWrapGrid(sender As Object, e As RoutedEventArgs)
            ' Update ItemsWrapGrid object created on page load by assigning it to StyledGrid's ItemWrapGrid
            StyledGridIWG = TryCast(sender, ItemsWrapGrid)

            ' Now we can change StyledGrid's MaximumRowsorColumns property within its ItemsPanel>ItemsPanelTemplate>ItemsWrapGrid.
            StyledGridIWG.MaximumRowsOrColumns = 3
        End Sub
        Private Sub NumberBox_ValueChanged(sender As Microsoft.UI.Xaml.Controls.NumberBox, args As Microsoft.UI.Xaml.Controls.NumberBoxValueChangedEventArgs)
            If StyledGridIWG Is Nothing Then
                Return
            End If

            ' Only update either max-row value or margins
            If sender = WrapItemCount Then
                StyledGridIWG.MaximumRowsOrColumns = CInt(CLng(Fix(WrapItemCount.Value)) Mod Integer.MaxValue)
                Return
            End If

            Dim rowSpace1 As Integer = CInt(CLng(Fix(RowSpace.Value)) Mod Integer.MaxValue)
            Dim columnSpace1 As Integer = CInt(CLng(Fix(ColumnSpace.Value)) Mod Integer.MaxValue)
            For i As Integer = 0 To StyledGrid.Items.Count - 1
                Dim item As GridViewItem = TryCast(StyledGrid.ContainerFromIndex(i), GridViewItem)

                Dim NewMargin As Thickness = item.Margin
                NewMargin.Left = columnSpace1
                NewMargin.Top = rowSpace1
                NewMargin.Right = columnSpace1
                NewMargin.Bottom = rowSpace1

                item.Margin = NewMargin
            Next
        End Sub
    End Class
End Namespace
