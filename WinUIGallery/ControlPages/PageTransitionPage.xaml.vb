' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Runtime.InteropServices.WindowsRuntime
Imports Windows.Foundation
Imports Windows.Foundation.Collections
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Controls.Primitives
Imports Microsoft.UI.Xaml.Data
Imports Microsoft.UI.Xaml.Input
Imports Microsoft.UI.Xaml.Media
Imports Microsoft.UI.Xaml.Media.Animation
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class PageTransitionPage
        Inherits Page
        Private _transitionInfo As NavigationTransitionInfo = Nothing

        Public Sub New()
            Me.InitializeComponent()

            ContentFrame.Navigate(GetType(SamplePages.SamplePage1))
        End Sub
        Private Sub ForwardButton1_Click(sender As Object, e As RoutedEventArgs)

            Dim pageToNavigateTo = If(ContentFrame.BackStackDepth Mod 2 = 1, GetType(SamplePages.SamplePage1), GetType(SamplePages.SamplePage2))

            If _transitionInfo Is Nothing Then
                ' Default behavior, no transition set or used.
                ContentFrame.Navigate(pageToNavigateTo, Nothing)
            Else
                ' Explicit transition info used.
                ContentFrame.Navigate(pageToNavigateTo, Nothing, _transitionInfo)
            End If
        End Sub
        Private Sub BackwardButton1_Click(sender As Object, e As RoutedEventArgs)
            If ContentFrame.BackStackDepth > 0 Then
                ContentFrame.GoBack()
            End If
        End Sub
        Private Sub TransitionRadioButton_Checked(sender As Object, e As RoutedEventArgs)
            Dim pageTransitionString As String = ""

            Dim senderTransitionString = TryCast(sender, RadioButton).Content.ToString()
            If senderTransitionString <> "Default" Then
                pageTransitionString = ", new " & senderTransitionString & "NavigationTransitionInfo()"

                If senderTransitionString = "Entrance" Then
                    _transitionInfo = New EntranceNavigationTransitionInfo
                ElseIf senderTransitionString = "DrillIn" Then
                    _transitionInfo = New DrillInNavigationTransitionInfo
                ElseIf senderTransitionString = "Suppress" Then
                    _transitionInfo = New SuppressNavigationTransitionInfo
                ElseIf senderTransitionString = "Slide from Right" Then
                    _transitionInfo = New SlideNavigationTransitionInfo() With
{
                        .Effect = SlideNavigationTransitionEffect.FromRight}
                    pageTransitionString = ", new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight }"
                ElseIf senderTransitionString = "Slide from Left" Then
                    _transitionInfo = New SlideNavigationTransitionInfo() With
{
                        .Effect = SlideNavigationTransitionEffect.FromLeft}
                    pageTransitionString = ", new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft }"
                End If
            Else
                _transitionInfo = Nothing
            End If

            If TransitionValue IsNot Nothing Then
                TransitionValue.Value = pageTransitionString
            End If
        End Sub
    End Class
End Namespace
