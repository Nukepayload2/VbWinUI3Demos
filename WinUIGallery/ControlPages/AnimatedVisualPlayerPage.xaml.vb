' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class AnimatedVisualPlayerPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub PlayButton_Click(sender As Object, e As RoutedEventArgs)
            ' Set forward playback rate.
            ' NOTE: This property is live, which means it takes effect even if the animation is playing.
            Player.PlaybackRate = 1
            EnsurePlaying()
        End Sub
        Private Sub PauseButton_Checked(sender As Object, e As RoutedEventArgs)
            ' Pause the animation, if playing.
            ' NOTE: Pausing does not cause PlayAsync to complete.
            Player.Pause()
        End Sub
        Private Sub PauseButton_Unchecked(sender As Object, e As RoutedEventArgs)
            ' Resume playing current animation.
            Player.[Resume]()
        End Sub
        Private Sub StopButton_Click(sender As Object, e As RoutedEventArgs)
            ' Stop the animation, which completes PlayAsync and resets to initial frame. 
            Player.[Stop]()
            PauseButton.IsChecked = False
        End Sub
        Private Sub ReverseButton_Click(sender As Object, e As RoutedEventArgs)
            ' Set reverse playback rate.
            ' NOTE: This property is live, which means it takes effect even if the animation is playing.
            Player.PlaybackRate = -1
            EnsurePlaying()
        End Sub
        Private Sub EnsurePlaying()
            If PauseButton.IsChecked.Value Then
                ' Resume playing the animation, if paused.
                PauseButton.IsChecked = False
            Else
                If Not Player.IsPlaying Then
                    ' Play the animation at the currently specified playback rate.
                    Dim x = Player.PlayAsync(fromProgress:=0, toProgress:=1, looped:=False)
                End If
            End If
        End Sub
    End Class
End Namespace
