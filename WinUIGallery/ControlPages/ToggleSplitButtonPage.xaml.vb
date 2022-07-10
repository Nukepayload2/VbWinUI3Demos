' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports Microsoft.UI.Text
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Automation
Imports Microsoft.UI.Xaml.Controls

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class ToggleSplitButtonPage
        Inherits Page
        Private _type As MarkerType = MarkerType.Bullet
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Sub BulletButton_Click(sender As Object, e As RoutedEventArgs)
            Dim clickedBullet As Button = CType(sender, Button)
            Dim symbol As SymbolIcon = CType(clickedBullet.Content, SymbolIcon)

            If symbol.Symbol = Symbol.List Then
                _type = MarkerType.Bullet
                mySymbolIcon.Symbol = Symbol.List
                myListButton.SetValue(AutomationProperties.NameProperty, "Bullets")
            ElseIf symbol.Symbol = Symbol.Bullets Then
                _type = MarkerType.UppercaseRoman
                mySymbolIcon.Symbol = Symbol.Bullets
                myListButton.SetValue(AutomationProperties.NameProperty, "Roman Numerals")
            End If
            myRichEditBox.Document.Selection.ParagraphFormat.ListType = _type

            myListButton.IsChecked = True
            myListButton.Flyout.Hide()
            myRichEditBox.Focus(FocusState.Keyboard)
        End Sub
        Private Sub MyListButton_IsCheckedChanged(sender As Microsoft.UI.Xaml.Controls.ToggleSplitButton, args As Microsoft.UI.Xaml.Controls.ToggleSplitButtonIsCheckedChangedEventArgs)
            If sender.IsChecked Then
                'add bulleted list
                myRichEditBox.Document.Selection.ParagraphFormat.ListType = _type
            Else
                'remove bulleted list
                myRichEditBox.Document.Selection.ParagraphFormat.ListType = MarkerType.None
            End If
        End Sub
    End Class
End Namespace
