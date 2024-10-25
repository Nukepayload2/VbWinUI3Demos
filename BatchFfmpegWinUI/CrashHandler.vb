﻿Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls

Module CrashHandler

    Async Function ShowFfmpegError(message As String) As Task
        Dim errWnd As New Window With {
            .Title = "Conversion failed. Technical details:",
            .Content = New TextBox With {
                .IsReadOnly = True,
                .AcceptsReturn = True,
                .Text = message,
                .Margin = New Thickness(4),
                .TextWrapping = TextWrapping.Wrap
            }
        }
        My.Computer.Audio.PlaySystemSound(ElementSoundKind.Show)

        Await errWnd.ShowDialogAsync(New DialogOptions With {.CenterOwner = True})
    End Function

End Module
