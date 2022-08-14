'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports AppUIBasics.Helper
Imports ColorCode
Imports ColorCode.Common
Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Storage
Imports Windows.System
Imports Windows.UI.Core
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Media

Namespace AppUIBasics.Controls
    Public NotInheritable Partial Class SampleCodePresenter
        Inherits UserControl
        Public Shared ReadOnly CodeProperty As DependencyProperty = DependencyProperty.Register("Code", GetType(String), GetType(SampleCodePresenter), New PropertyMetadata("", AddressOf OnDependencyPropertyChanged))
        Public Property Code As String
            Get
                Return CStr(GetValue(CodeProperty))
            End Get

            Set(value As String)
                SetValue(CodeProperty, value)
            End Set
        End Property
        Public Shared ReadOnly CodeSourceFileProperty As DependencyProperty = DependencyProperty.Register("CodeSourceFile", GetType(Object), GetType(SampleCodePresenter), New PropertyMetadata(Nothing, AddressOf OnDependencyPropertyChanged))
        Public Property CodeSourceFile As Uri
            Get
                Return CType(GetValue(CodeSourceFileProperty), Uri)
            End Get

            Set(value As Uri)
                SetValue(CodeSourceFileProperty, value)
            End Set
        End Property
        Public Shared ReadOnly IsCSharpSampleProperty As DependencyProperty = DependencyProperty.Register("IsCSharpSample", GetType(Boolean), GetType(SampleCodePresenter), New PropertyMetadata(False))
        Public Property IsCSharpSample As Boolean
            Get
                Return CBool(GetValue(IsCSharpSampleProperty))
            End Get

            Set(value As Boolean)
                SetValue(IsCSharpSampleProperty, value)
            End Set
        End Property
        Public Shared ReadOnly SubstitutionsProperty As DependencyProperty = DependencyProperty.Register("Substitutions", GetType(IList(Of ControlExampleSubstitution)), GetType(ControlExample), New PropertyMetadata(Nothing))
        Public Property Substitutions As IList(Of ControlExampleSubstitution)
            Get
                Return CType(GetValue(SubstitutionsProperty), IList(Of ControlExampleSubstitution))
            End Get

            Set(value As IList(Of ControlExampleSubstitution))
                SetValue(SubstitutionsProperty, value)
            End Set
        End Property
        Public ReadOnly Property IsEmpty As Boolean
            Get
                Return Code.Length = 0 AndAlso CodeSourceFile Is Nothing
            End Get
        End Property
        Private actualCode As String = ""
        Private Shared SubstitutionPattern As New Regex("\$\(([^\)]+)\)")

        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Private Shared Sub OnDependencyPropertyChanged(target As DependencyObject, args As DependencyPropertyChangedEventArgs)
            If TypeOf target Is SampleCodePresenter Then
                Dim presenter = CType(target, SampleCodePresenter)
                presenter.ReevaluateVisibility()
            End If
        End Sub
        Private Sub ReevaluateVisibility()
            If Code.Length = 0 AndAlso CodeSourceFile Is Nothing Then
                Visibility = Visibility.Collapsed
            Else
                Visibility = Visibility.Visible
            End If
        End Sub
        Private Sub SampleCodePresenter_Loaded(sender As Object, e As RoutedEventArgs)
            ReevaluateVisibility()
            VisualStateManager.GoToState(Me, If(IsCSharpSample, "CSharpSample", "XAMLSample"), False)
            For Each substitution As ControlExampleSubstitution In Substitutions
                AddHandler substitution.ValueChanged, AddressOf OnValueChanged
            Next
        End Sub
        Private Sub CodePresenter_Loaded(sender As Object, e As RoutedEventArgs)
            GenerateSyntaxHighlightedContent()
        End Sub
        Private Sub SampleCodePresenter_ActualThemeChanged(sender As FrameworkElement, args As Object)
            ' If the theme has changed after the user has already opened the app (ie. via settings), then the new locally set theme will overwrite the colors that are set during Loaded.
            ' Therefore we need to re-format the REB to use the correct colors.
            GenerateSyntaxHighlightedContent()
        End Sub
        Private Sub OnValueChanged(sender As ControlExampleSubstitution, e As Object)
            GenerateSyntaxHighlightedContent()
        End Sub
        Private Function GetDerivedSource(rawSource As Uri) As Uri
            ' Get the full path of the source string
            Dim concatString As String = ""
            For i As Integer = 2 To rawSource.Segments.Length - 1
                concatString &= rawSource.Segments(i)
            Next
            Dim derivedSource As New Uri(New Uri("ms-appx:///ControlPagesSampleCode/"), concatString)

            Return derivedSource
        End Function
        Private Sub GenerateSyntaxHighlightedContent()
            If Not String.IsNullOrEmpty(Code) Then
                FormatAndRenderSampleFromString(Code, CodePresenter, If(IsCSharpSample, Languages.CSharp, Languages.Xml))
            Else
                FormatAndRenderSampleFromFile(CodeSourceFile, CodePresenter, If(IsCSharpSample, Languages.CSharp, Languages.Xml))
            End If
        End Sub
        Private Async Sub FormatAndRenderSampleFromFile(source As Uri, presenter As ContentPresenter, highlightLanguage As ILanguage)
            If source IsNot Nothing AndAlso source.AbsolutePath.EndsWith("txt") Then
                Dim derivedSource As Uri = GetDerivedSource(source)
                Dim file = Await StorageFile.GetFileFromApplicationUriAsync(derivedSource)
                Dim sampleString As String = Await FileIO.ReadTextAsync(file)

                FormatAndRenderSampleFromString(sampleString, presenter, highlightLanguage)
            Else
                presenter.Visibility = Visibility.Collapsed
            End If
        End Sub
        Private Sub FormatAndRenderSampleFromString(sampleString As String, presenter As ContentPresenter, highlightLanguage As ILanguage)
            ' Trim out stray blank lines at start and end.
            sampleString = sampleString.TrimStart(ControlChars.Lf).TrimEnd()

            ' Also trim out spaces at the end of each line
            sampleString = String.Join(vbLf, sampleString.Split(vbLf).[Select](Function(s) s.TrimEnd()))

            ' Perform any applicable substitutions.
            sampleString = SubstitutionPattern.Replace(sampleString, Function(match) As String
                                                                         For Each substitution As ControlExampleSubstitution In Substitutions
                                                                             If substitution.Key = match.Groups(1).Value Then
                                                                                 Return substitution.ValueAsString()
                                                                             End If
                                                                         Next
                                                                         Throw New KeyNotFoundException(match.Groups(1).Value)
                                                                     End Function)

            actualCode = sampleString

            Dim sampleCodeRTB As New RichTextBlock With
{
                .FontFamily = New FontFamily("Consolas")}

            Dim formatter = GenerateRichTextFormatter()
            formatter.FormatRichTextBlock(sampleString, highlightLanguage, sampleCodeRTB)
            presenter.Content = sampleCodeRTB
        End Sub
        Private Function GenerateRichTextFormatter() As RichTextBlockFormatter
            Dim formatter As New RichTextBlockFormatter(ThemeHelper.ActualTheme)

            If ThemeHelper.ActualTheme = ElementTheme.Dark Then
                UpdateFormatterDarkThemeColors(formatter)
            End If

            Return formatter
        End Function
        Private Sub UpdateFormatterDarkThemeColors(formatter As RichTextBlockFormatter)
            ' Replace the default dark theme resources with ones that more closely align to VS Code dark theme.
            formatter.Styles.Remove(formatter.Styles(ScopeName.XmlAttribute))
            formatter.Styles.Remove(formatter.Styles(ScopeName.XmlAttributeQuotes))
            formatter.Styles.Remove(formatter.Styles(ScopeName.XmlAttributeValue))
            formatter.Styles.Remove(formatter.Styles(ScopeName.HtmlComment))
            formatter.Styles.Remove(formatter.Styles(ScopeName.XmlDelimiter))
            formatter.Styles.Remove(formatter.Styles(ScopeName.XmlName))

            formatter.Styles.Add(New ColorCode.Styling.Style(ScopeName.XmlAttribute) With
            { _
            .Foreground = "#FF87CEFA",
            .ReferenceName = "xmlAttribute"
            })
            formatter.Styles.Add(New ColorCode.Styling.Style(ScopeName.XmlAttributeQuotes) With
            { _
            .Foreground = "#FFFFA07A",
            .ReferenceName = "xmlAttributeQuotes"
            })
            formatter.Styles.Add(New ColorCode.Styling.Style(ScopeName.XmlAttributeValue) With
            { _
            .Foreground = "#FFFFA07A",
            .ReferenceName = "xmlAttributeValue"
            })
            formatter.Styles.Add(New ColorCode.Styling.Style(ScopeName.HtmlComment) With
            { _
            .Foreground = "#FF6B8E23",
            .ReferenceName = "htmlComment"
            })
            formatter.Styles.Add(New ColorCode.Styling.Style(ScopeName.XmlDelimiter) With
            { _
            .Foreground = "#FF808080",
            .ReferenceName = "xmlDelimiter"
            })
            formatter.Styles.Add(New ColorCode.Styling.Style(ScopeName.XmlName) With
            { _
            .Foreground = "#FF5F82E8",
            .ReferenceName = "xmlName"
            })
        End Sub
        Private Sub CopyCodeButton_Click(sender As Object, e As RoutedEventArgs)
            Dim package As New DataPackage
            package.SetText(actualCode)
            Clipboard.SetContent(package)

            VisualStateManager.GoToState(Me, "ConfirmationDialogVisible", False)
            Dim dispatcherQueue1 As Microsoft.UI.Dispatching.DispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread()

            ' Automatically close teachingtip after 1 seconds
            If dispatcherQueue1 IsNot Nothing Then
                dispatcherQueue1.TryEnqueue(Async Function() As System.Threading.Tasks.Task
                                                Await Task.Delay(1000)
                                                VisualStateManager.GoToState(Me, "ConfirmationDialogHidden", False)
                                            End Function)
            End If
        End Sub
    End Class
End Namespace
