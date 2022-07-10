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
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class SoundPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()

            If ElementSoundPlayer.State = ElementSoundPlayerState.[On] Then
                soundToggle.IsOn = True
            End If
            If ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.[On] AndAlso soundToggle.IsOn = True Then
                spatialAudioBox.IsChecked = True
            End If
        End Sub
        Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
            ElementSoundPlayer.Play(CType(soundSelection.SelectedIndex, ElementSoundKind))
        End Sub
        Private Sub spatialAudioBox_Checked(sender As Object, e As RoutedEventArgs)
            If soundToggle.IsOn = True Then
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.[On]
            End If
        End Sub
        Private Sub spatialAudioBox_Unchecked(sender As Object, e As RoutedEventArgs)
            If soundToggle.IsOn = True Then
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.Off
            End If
        End Sub
        Private Sub soundToggle_Toggled(sender As Object, e As RoutedEventArgs)
            If soundToggle.IsOn = True Then
                spatialAudioBox.IsEnabled = True
                ElementSoundPlayer.State = ElementSoundPlayerState.[On]
            Else
                spatialAudioBox.IsEnabled = False
                spatialAudioBox.IsChecked = False

                ElementSoundPlayer.State = ElementSoundPlayerState.Off
                ElementSoundPlayer.SpatialAudioMode = ElementSpatialAudioMode.Off
            End If
        End Sub
    End Class
End Namespace
