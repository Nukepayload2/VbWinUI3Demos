' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Navigation

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class InfoBarPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            DisplayMessage.Value = "A long essential app message..."
            DisplayButton.Value = String.Empty
        End Sub
        Private Sub SeverityComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            Dim severityName As String = e.AddedItems(0).ToString()

            Select Case severityName
                Case "Error"
                    TestInfoBar1.Severity = InfoBarSeverity.[Error]

                Case "Warning"
                    TestInfoBar1.Severity = InfoBarSeverity.Warning

                Case "Success"
                    TestInfoBar1.Severity = InfoBarSeverity.Success
                Case Else
                    TestInfoBar1.Severity = InfoBarSeverity.Informational
            End Select
        End Sub
        Private Sub MessageComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            If TestInfoBar2 Is Nothing Then
                Return
            End If

            If MessageComboBox.SelectedIndex = 0 Then
                ' short
                Dim shortMessage As String = "A short essential app message."
                TestInfoBar2.Message = shortMessage
                DisplayMessage.Value = shortMessage
            ElseIf MessageComboBox.SelectedIndex = 1 Then
                TestInfoBar2.Message = "A long essential app message for your users to be informed of, acknowledge, or take action on. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin dapibus dolor vitae justo rutrum, ut lobortis nibh mattis. Aenean id elit commodo, semper felis nec."
                DisplayMessage.Value = "A long essential app message..."
            End If
        End Sub
        Private Sub ActionButtonComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
            If TestInfoBar2 Is Nothing Then
                Return
            End If

            If ActionButtonComboBox.SelectedIndex = 0 Then
                ' none
                TestInfoBar2.ActionButton = Nothing
                DisplayButton.Value = String.Empty
            ElseIf ActionButtonComboBox.SelectedIndex = 1 Then
                Dim button1 As New Button
                button1.Content = "Action"
                TestInfoBar2.ActionButton = button1
                DisplayButton.Value = "<InfoBar.ActionButton>
            <Button Content=""Action"" Click=""InfoBarButton_Click"" />
    </InfoBar.ActionButton> "
            ElseIf ActionButtonComboBox.SelectedIndex = 2 Then
                Dim link As New HyperlinkButton
                link.NavigateUri = New Uri("http://www.microsoft.com/")
                link.Content = "Informational link"
                TestInfoBar2.ActionButton = link
                DisplayButton.Value = "<InfoBar.ActionButton>
            <HyperlinkButton Content=""Informational link"" NavigateUri=""https://www.example.com"" />
    </InfoBar.ActionButton>"
            End If
        End Sub
    End Class
End Namespace
