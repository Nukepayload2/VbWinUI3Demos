' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media.Animation
Imports AppUIBasics.SamplePages

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class CompactSizingPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()

        End Sub
        Private Sub Example1_Loaded(sender As Object, e As RoutedEventArgs)
            ContentFrame.Navigate(GetType(SampleStandardSizingPage), Nothing, New SuppressNavigationTransitionInfo)
        End Sub
        Private Sub Standard_Checked(sender As Object, e As RoutedEventArgs)
            Dim oldPage = TryCast(ContentFrame.Content, SampleCompactSizingPage)

            ContentFrame.Navigate(GetType(SampleStandardSizingPage), Nothing, New SuppressNavigationTransitionInfo)

            If oldPage IsNot Nothing Then
                Dim page1 = TryCast(ContentFrame.Content, SampleStandardSizingPage)
                page1.CopyState(oldPage)
            End If
        End Sub
        Private Sub Compact_Checked(sender As Object, e As RoutedEventArgs)
            Dim oldPage = TryCast(ContentFrame.Content, SampleStandardSizingPage)

            ContentFrame.Navigate(GetType(SampleCompactSizingPage), Nothing, New SuppressNavigationTransitionInfo)

            If oldPage IsNot Nothing Then
                Dim page1 = TryCast(ContentFrame.Content, SampleCompactSizingPage)
                page1.CopyState(oldPage)
            End If
        End Sub
    End Class
End Namespace
