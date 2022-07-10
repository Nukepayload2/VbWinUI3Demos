' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports Microsoft.UI
Imports Microsoft.UI.Text
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media
Imports Microsoft.UI.Xaml.Shapes
Imports System.Threading.Tasks

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class SplitButtonPage
        Inherits Page
        Private currentColor As Windows.UI.Color = Colors.Green

        Public Sub New()
            Me.InitializeComponent()

            myRichEditBox.Document.Selection.CharacterFormat.ForegroundColor = currentColor
            myRichEditBox.Document.Selection.SetText(Microsoft.UI.Text.TextSetOptions.None,
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " &
                "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Tempor commodo ullamcorper a lacus.")
        End Sub
        Private Sub GridView_ItemClick(sender As Object, e As ItemClickEventArgs)
            Dim rect = CType(e.ClickedItem, Rectangle)
            Dim color1 = CType(rect.Fill, SolidColorBrush).Color
            myRichEditBox.Document.Selection.CharacterFormat.ForegroundColor = color1
            CurrentColor.Background = New SolidColorBrush(color1)

            myRichEditBox.Focus(Microsoft.UI.Xaml.FocusState.Keyboard)
            currentColor = color1

            ' Delay required to circumvent GridView bug: https://github.com/microsoft/microsoft-ui-xaml/issues/6350
            Task.Delay(10).ContinueWith(Function() myColorButton.Flyout.Hide(), TaskScheduler.FromCurrentSynchronizationContext())
        End Sub
        Private Sub RevealColorButton_Click(sender As Object, e As RoutedEventArgs)
            myColorButtonReveal.Flyout.Hide()
        End Sub
        Private Sub myColorButton_Click(sender As Microsoft.UI.Xaml.Controls.SplitButton, args As Microsoft.UI.Xaml.Controls.SplitButtonClickEventArgs)
            Dim border1 = CType(sender.Content, Border)
            Dim color1 = CType(border1.Background, Microsoft.UI.Xaml.Media.SolidColorBrush).Color

            myRichEditBox.Document.Selection.CharacterFormat.ForegroundColor = color1
            currentColor = color1
        End Sub
        Private Sub MyRichEditBox_TextChanged(sender As Object, e As RoutedEventArgs)
            If myRichEditBox.Document.Selection.CharacterFormat.ForegroundColor <> currentColor Then
                myRichEditBox.Document.Selection.CharacterFormat.ForegroundColor = currentColor
            End If
        End Sub
    End Class
End Namespace
