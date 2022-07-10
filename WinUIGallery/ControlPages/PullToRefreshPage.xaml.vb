' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports AppUIBasics.Helper
Imports System
Imports Windows.Foundation
Imports Windows.Foundation.Metadata
Imports Windows.UI.Core
Imports Windows.UI.ViewManagement
Imports Microsoft.UI.Composition
Imports Microsoft.UI.Dispatching
Imports Microsoft.UI.Windowing
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Hosting
Imports Microsoft.UI.Xaml.Media
Imports Microsoft.UI.Xaml.Media.Imaging
Imports Microsoft.UI.Xaml.Navigation

#If Not UNIVERSAL

Imports System.Collections.ObjectModel
#End If

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class PullToRefreshPage
        Inherits Page
        Private items1 As New ObservableCollection(Of String)
        Private items2 As New ObservableCollection(Of String)
        Private timer1 As New DispatcherTimer
        Private timer2 As New DispatcherTimer
        Private visualizerContentVisual As Visual
        Private Shared rc2 As RefreshContainer
        Private rv2 As RefreshVisualizer
        Private items1AddedCount As Integer = 0
        Private items2AddedCount As Integer = 0

        Private Property RefreshCompletionDeferral1 As Deferral
        Private Property RefreshCompletionDeferral2 As Deferral

        Public Sub New()
            Me.InitializeComponent()

            If ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 6) Then
                rc2 = New RefreshContainer
                AddHandler rc2.RefreshRequested, New TypedEventHandler(Of RefreshContainer, RefreshRequestedEventArgs)(AddressOf rc2_RefreshRequested)

                rv2 = New RefreshVisualizer
                AddHandler rv2.RefreshStateChanged, New TypedEventHandler(Of RefreshVisualizer, RefreshStateChangedEventArgs)(AddressOf rv2_RefreshStateChanged)

                Dim ptrImage As New Image
                Dim accessibilitySettings1 As New AccessibilitySettings
                ' Checking light theme
                If (ThemeHelper.RootTheme = ElementTheme.Light OrElse Application.Current.RequestedTheme = ApplicationTheme.Light) AndAlso Not accessibilitySettings1.HighContrast Then
                    ptrImage.Source = New BitmapImage(New Uri("ms-appx:///Assets/SunBlack.png"))
                    ' Checking high contrast theme
                ElseIf accessibilitySettings1.HighContrast _
                          AndAlso accessibilitySettings1.HighContrastScheme.Equals("High Contrast Black") Then
                    ptrImage.Source = New BitmapImage(New Uri("ms-appx:///Assets/SunBlack.png"))
                Else
                    ptrImage.Source = New BitmapImage(New Uri("ms-appx:///Assets/SunWhite.png"))
                End If

                ptrImage.Width = 35
                ptrImage.Height = 35

                rv2.Content = ptrImage
                rc2.Visualizer = rv2

                Dim lv2 As New ListView With
                { _
                .Width = 200,
                .Height = 200,
                .BorderThickness = New Thickness() With
{
                    .Left = 1,
                    .Top = 1,
                    .Right = 1,
                    .Bottom = 1},
                .HorizontalAlignment = HorizontalAlignment.Center,
                .BorderBrush = CType(Application.Current.Resources("TextControlBorderBrush"), Brush)
                }


                rc2.Content = lv2

                Ex2Grid.Children.Add(rc2)
                Grid.SetRow(rc2, 1)
                Grid.SetRow(lv2, 1)

                timer1.Interval = New TimeSpan(0, 0, 0, 0, 500)
                AddHandler timer1.Tick, AddressOf Timer1_Tick

                timer2.Interval = New TimeSpan(0, 0, 0, 0, 800)
                AddHandler timer2.Tick, AddressOf Timer2_Tick

                For Each c As String In "AcrylicBrush ColorPicker NavigationView ParallaxView PersonPicture PullToRefreshPage RatingsControl RevealBrush TreeView".Split(" "c)
                    items1.Add(c)
                Next
                lv.ItemsSource = items1

                For Each c As String In "Mike Ben Barbra Claire Justin Shawn Drew Lili".Split(" "c)
                    items2.Add(c)
                Next
                lv2.ItemsSource = items2
                AddHandler Me.Loaded, AddressOf PullToRefreshPage_Loaded
            End If
        End Sub
        Private Sub PullToRefreshPage_Loaded(sender As Object, e As RoutedEventArgs)
            visualizerContentVisual = ElementCompositionPreview.GetElementVisual(rv2.Content)
            RemoveHandler Me.Loaded, AddressOf PullToRefreshPage_Loaded
        End Sub
        Private Sub Timer1_Tick(sender As Object, e As Object)
            Dim disp As DispatcherQueue = rc.DispatcherQueue
            If disp.HasThreadAccess Then
                Timer1_TickImpl()
            Else
                disp.TryEnqueue(DispatcherQueuePriority.Normal, Sub()
                                                                    Timer1_TickImpl()
                                                                End Sub)
            End If
        End Sub
        Private Sub Timer2_Tick(sender As Object, e As Object)
            Dim disp As DispatcherQueue = rc2.DispatcherQueue
            If disp.HasThreadAccess Then
                Timer2_TickImpl()
            Else
                disp.TryEnqueue(DispatcherQueuePriority.Normal, Sub()
                                                                    Timer2_TickImpl()
                                                                End Sub)
            End If
        End Sub
        Private Sub Timer1_TickImpl()
            items1.Insert(0, "NewControl " & Math.Min(Threading.Interlocked.Increment(items1AddedCount), items1AddedCount - 1))
            timer1.[Stop]()
            If Me.RefreshCompletionDeferral1 IsNot Nothing Then
                Me.RefreshCompletionDeferral1.Complete()
                Me.RefreshCompletionDeferral1.Dispose()
                Me.RefreshCompletionDeferral1 = Nothing
            End If
        End Sub
        Private Sub Timer2_TickImpl()
            items2.Insert(0, "New Friend " & Math.Min(Threading.Interlocked.Increment(items2AddedCount), items2AddedCount - 1))
            timer2.[Stop]()
            If Me.RefreshCompletionDeferral2 IsNot Nothing Then
                Me.RefreshCompletionDeferral2.Complete()
                Me.RefreshCompletionDeferral2.Dispose()
                Me.RefreshCompletionDeferral2 = Nothing
            End If
        End Sub
        Protected Overrides Sub OnNavigatedFrom(e As NavigationEventArgs)
            MyBase.OnNavigatedFrom(e)
            timer1.[Stop]()
            timer2.[Stop]()
        End Sub
        Private Sub rc_RefreshRequested(sender As RefreshContainer, args As RefreshRequestedEventArgs)
            Me.RefreshCompletionDeferral1 = args.GetDeferral()
            'Do some work to show new content!
            timer1.Start()
        End Sub
        Private Sub rc2_RefreshRequested(sender As RefreshContainer, args As RefreshRequestedEventArgs)
            Me.RefreshCompletionDeferral2 = args.GetDeferral()
            'Do some work to show new content!
            timer2.Start()
        End Sub
        Private Sub rv2_RefreshStateChanged(sender As RefreshVisualizer, args As RefreshStateChangedEventArgs)
            'visualizerContentVisual.StopAnimation("RotationAngle");
        End Sub
    End Class
End Namespace
