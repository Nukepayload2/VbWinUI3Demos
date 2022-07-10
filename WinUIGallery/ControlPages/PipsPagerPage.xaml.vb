' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports System.Collections.Generic
Imports Microsoft.UI.Xaml.Controls

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class PipsPagerPage
        Inherits Page
        Public Pictures As New List(Of String) From { _
            "ms-appx:///Assets/SampleMedia/LandscapeImage1.jpg",
            "ms-appx:///Assets/SampleMedia/LandscapeImage2.jpg",
            "ms-appx:///Assets/SampleMedia/LandscapeImage3.jpg",
            "ms-appx:///Assets/SampleMedia/LandscapeImage4.jpg",
            "ms-appx:///Assets/SampleMedia/LandscapeImage5.jpg",
            "ms-appx:///Assets/SampleMedia/LandscapeImage6.jpg",
            "ms-appx:///Assets/SampleMedia/LandscapeImage7.jpg",
            "ms-appx:///Assets/SampleMedia/LandscapeImage8.jpg"
        }
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub OrientationComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim orientation As String = e.AddedItems(0).ToString()

            Select Case orientation
                Case "Vertical"
                    TestPipsPager2.Orientation = Orientation.Vertical
                Case Else
                    TestPipsPager2.Orientation = Orientation.Horizontal
            End Select
        End Sub
        Private Sub PrevButtonComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim prevButtonVisibility As String = e.AddedItems(0).ToString()

            Select Case prevButtonVisibility
                Case "Visible"
                    TestPipsPager2.PreviousButtonVisibility = PipsPagerButtonVisibility.Visible

                Case "VisibleOnPointerOver"
                    TestPipsPager2.PreviousButtonVisibility = PipsPagerButtonVisibility.VisibleOnPointerOver
                Case Else
                    TestPipsPager2.PreviousButtonVisibility = PipsPagerButtonVisibility.Collapsed
            End Select
        End Sub
        Private Sub NextButtonComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim nextButtonVisibility As String = e.AddedItems(0).ToString()

            Select Case nextButtonVisibility
                Case "Visible"
                    TestPipsPager2.NextButtonVisibility = PipsPagerButtonVisibility.Visible

                Case "VisibleOnPointerOver"
                    TestPipsPager2.NextButtonVisibility = PipsPagerButtonVisibility.VisibleOnPointerOver
                Case Else
                    TestPipsPager2.NextButtonVisibility = PipsPagerButtonVisibility.Collapsed
            End Select
        End Sub
    End Class
End Namespace
