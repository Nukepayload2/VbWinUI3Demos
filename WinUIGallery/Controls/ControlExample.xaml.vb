'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************
Imports AppUIBasics.Common
Imports AppUIBasics.Helper
Imports ColorCode
Imports ColorCode.Common
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Runtime.InteropServices.WindowsRuntime
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports Windows.ApplicationModel.Core
Imports Windows.Foundation
Imports Windows.Foundation.Metadata
Imports Windows.Graphics.Display
Imports Windows.Graphics.Imaging
#If Not UNIVERSAL Then

Imports Windows.Media.AppRecording
#End If

Imports Windows.Storage
Imports Microsoft.UI.Xaml
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Markup
Imports Microsoft.UI.Xaml.Media
Imports Microsoft.UI.Xaml.Media.Imaging
Imports System.Reflection

Namespace AppUIBasics
    ''' <summary>
    ''' Describes a textual substitution in sample content.
    ''' If enabled (default), then $(Key) is replaced with the stringified value.
    ''' If disabled, then $(Key) is replaced with the empty string.
    ''' </summary>
    Public NotInheritable Class ControlExampleSubstitution
        Inherits DependencyObject
        Public Event ValueChanged As TypedEventHandler(Of ControlExampleSubstitution, Object)

        Public Property Key As String
        Private _value As Object = Nothing
        Public Property Value As Object
            Get
                Return _value
            End Get

            Set(value As Object)
                _value = value
                RaiseEvent ValueChanged(Me, Nothing)
            End Set
        End Property
        Private _enabled As Boolean = True
        Public Property IsEnabled As Boolean
            Get
                Return _enabled
            End Get

            Set(value As Boolean)
                _enabled = value
                RaiseEvent ValueChanged(Me, Nothing)
            End Set
        End Property
        Public Function ValueAsString() As String
            If Not IsEnabled Then
                Return String.Empty
            End If

            Dim value As Object = value

            ' For solid color brushes, use the underlying color.
            If TypeOf value Is SolidColorBrush Then
                value = CType(value, SolidColorBrush).Color
            End If

            If value Is Nothing Then
                Return String.Empty
            End If

            Return value.ToString()
        End Function
    End Class
    <ContentProperty(Name:="Example")>
    Partial Public NotInheritable Class ControlExample
        Inherits UserControl
        Public Shared ReadOnly HeaderTextProperty As DependencyProperty = DependencyProperty.Register("HeaderText", GetType(String), GetType(ControlExample), New PropertyMetadata(Nothing))
        Public Property HeaderText As String
            Get
                Return CStr(GetValue(HeaderTextProperty))
            End Get

            Set(value As String)
                SetValue(HeaderTextProperty, value)
            End Set
        End Property
        Public Shared ReadOnly ExampleProperty As DependencyProperty = DependencyProperty.Register("Example", GetType(Object), GetType(ControlExample), New PropertyMetadata(Nothing))
        Public Property Example As Object
            Get
                Return GetValue(ExampleProperty)
            End Get

            Set(value As Object)
                SetValue(ExampleProperty, value)
            End Set
        End Property
        Public Shared ReadOnly OutputProperty As DependencyProperty = DependencyProperty.Register("Output", GetType(Object), GetType(ControlExample), New PropertyMetadata(Nothing))
        Public Property Output As Object
            Get
                Return GetValue(OutputProperty)
            End Get

            Set(value As Object)
                SetValue(OutputProperty, value)
            End Set
        End Property
        Public Shared ReadOnly OptionsProperty As DependencyProperty = DependencyProperty.Register("Options", GetType(Object), GetType(ControlExample), New PropertyMetadata(Nothing))
        Public Property Options As Object
            Get
                Return GetValue(OptionsProperty)
            End Get

            Set(value As Object)
                SetValue(OptionsProperty, value)
            End Set
        End Property
        Public Shared ReadOnly XamlProperty As DependencyProperty = DependencyProperty.Register("Xaml", GetType(String), GetType(ControlExample), New PropertyMetadata(Nothing))
        Public Property Xaml As String
            Get
                Return CStr(GetValue(XamlProperty))
            End Get

            Set(value As String)
                SetValue(XamlProperty, value)
            End Set
        End Property
        Public Shared ReadOnly XamlSourceProperty As DependencyProperty = DependencyProperty.Register("XamlSource", GetType(Object), GetType(ControlExample), New PropertyMetadata(Nothing))
        Public Property XamlSource As Uri
            Get
                Return CType(GetValue(XamlSourceProperty), Uri)
            End Get

            Set(value As Uri)
                SetValue(XamlSourceProperty, value)
            End Set
        End Property
        Public Shared ReadOnly CSharpProperty As DependencyProperty = DependencyProperty.Register("CSharp", GetType(String), GetType(ControlExample), New PropertyMetadata(Nothing))
        Public Property CSharp As String
            Get
                Return CStr(GetValue(CSharpProperty))
            End Get

            Set(value As String)
                SetValue(CSharpProperty, value)
            End Set
        End Property
        Public Shared ReadOnly CSharpSourceProperty As DependencyProperty = DependencyProperty.Register("CSharpSource", GetType(Object), GetType(ControlExample), New PropertyMetadata(Nothing))
        Public Property CSharpSource As Uri
            Get
                Return CType(GetValue(CSharpSourceProperty), Uri)
            End Get

            Set(value As Uri)
                SetValue(CSharpSourceProperty, value)
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

        Private Shared ReadOnly defaultExampleHeight As New GridLength(1, GridUnitType.Star)

        Public Shared ReadOnly ExampleHeightProperty As DependencyProperty = DependencyProperty.Register("ExampleHeight", GetType(GridLength), GetType(ControlExample), New PropertyMetadata(defaultExampleHeight))
        Public Property ExampleHeight As GridLength
            Get
                Return CType(GetValue(ExampleHeightProperty), GridLength)
            End Get

            Set(value As GridLength)
                SetValue(ExampleHeightProperty, value)
            End Set
        End Property
        Public Shared ReadOnly WebViewHeightProperty As DependencyProperty = DependencyProperty.Register("WebViewHeight", GetType(Integer), GetType(ControlExample), New PropertyMetadata(400))
        Public Property WebViewHeight As Integer
            Get
                Return CInt(CLng(Fix(GetValue(WebViewHeightProperty))) Mod Integer.MaxValue)
            End Get

            Set(value As Integer)
                SetValue(WebViewHeightProperty, value)
            End Set
        End Property
        Public Shared ReadOnly WebViewWidthProperty As DependencyProperty = DependencyProperty.Register("WebViewWidth", GetType(Integer), GetType(ControlExample), New PropertyMetadata(800))
        Public Property WebViewWidth As Integer
            Get
                Return CInt(CLng(Fix(GetValue(WebViewWidthProperty))) Mod Integer.MaxValue)
            End Get

            Set(value As Integer)
                SetValue(WebViewWidthProperty, value)
            End Set
        End Property
        Public Shared Shadows ReadOnly HorizontalContentAlignmentProperty As DependencyProperty = DependencyProperty.Register("HorizontalContentAlignment", GetType(HorizontalAlignment), GetType(ControlExample), New PropertyMetadata(HorizontalAlignment.Left))
        Public Shadows Property HorizontalContentAlignment As HorizontalAlignment
            Get
                Return CType(GetValue(HorizontalContentAlignmentProperty), HorizontalAlignment)
            End Get

            Set(value As HorizontalAlignment)
                SetValue(HorizontalContentAlignmentProperty, value)
            End Set
        End Property
        Public Shared ReadOnly MinimumUniversalAPIContractProperty As DependencyProperty = DependencyProperty.Register("MinimumUniversalAPIContract", GetType(Integer), GetType(ControlExample), New PropertyMetadata(Nothing))
        Public Property MinimumUniversalAPIContract As Integer
            Get
                Return CInt(CLng(Fix(GetValue(MinimumUniversalAPIContractProperty))) Mod Integer.MaxValue)
            End Get

            Set(value As Integer)
                SetValue(MinimumUniversalAPIContractProperty, value)
            End Set
        End Property

        Public Sub New()
            Me.InitializeComponent()
            Substitutions = New List(Of ControlExampleSubstitution)

            ControlPresenter.RegisterPropertyChangedCallback(ContentPresenter.PaddingProperty, AddressOf ControlPaddingChangedCallback)
            AddHandler Me.Loaded, AddressOf ControlExample_Loaded
        End Sub
        Private Sub ControlExample_Loaded(sender As Object, e As RoutedEventArgs)
            If Not XamlPresenter.IsEmpty AndAlso Not CSharpPresenter.IsEmpty Then
                VisualStateManager.GoToState(Me, "SeparatorVisible", False)
            End If
        End Sub
        Private Sub rootGrid_Loaded(sender As Object, e As RoutedEventArgs)
            If MinimumUniversalAPIContract <> 0 AndAlso Not ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", CUShort(MinimumUniversalAPIContract)) Then
                ErrorTextBlock.Visibility = Visibility.Visible
            End If
        End Sub
        Private Enum SyntaxHighlightLanguage
            Xml
            CSharp
        End Enum
        Private Sub ScreenshotButton_Click(sender As Object, e As RoutedEventArgs)
            TakeScreenshot()
        End Sub
        Private Sub ScreenshotDelayButton_Click(sender As Object, e As RoutedEventArgs)
            TakeScreenshotWithDelay()
        End Sub
        Private Async Sub TakeScreenshot()
            ' Using RTB doesn't capture popups; but in the non-delay case, that probably isn't necessary.
            ' This method seems more robust than using AppRecordingManager and also will work on non-desktop devices.

            Dim rtb As New RenderTargetBitmap
            Await rtb.RenderAsync(ControlPresenter)

            Dim pixelBuffer = Await rtb.GetPixelsAsync()
            Dim pixels = pixelBuffer.ToArray()

            Dim file = Await UIHelper.ScreenshotStorageFolder.CreateFileAsync(GetBestScreenshotName(), CreationCollisionOption.ReplaceExisting)
            Using stream = Await file.OpenAsync(FileAccessMode.ReadWrite)
                Dim displayInformation1 = DisplayInformation.GetForCurrentView()
                Dim encoder = Await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream)
                encoder.SetPixelData(BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Premultiplied,
                    CUInt(rtb.PixelWidth),
                    CUInt(rtb.PixelHeight),
                    displayInformation1.RawDpiX,
                    displayInformation1.RawDpiY,
                    pixels)

                Await encoder.FlushAsync()
            End Using
        End Sub
        Public Async Sub TakeScreenshotWithDelay()
            ' 3 second countdown
            For i As Integer = 3 To 0 + 1 Step -1
                ScreenshotStatusTextBlock.Text = i.ToString()
                Await Task.Delay(1000)
            Next
            ScreenshotStatusTextBlock.Text = "Image captured"

            ' AppRecordingManager is desktop-only, and its use here is quite hacky,
            ' but it is able to capture popups (though not theme shadows).
            Dim isAppRecordingPresent As Boolean = ApiInformation.IsTypePresent("Windows.Media.AppRecording.AppRecordingManager")
            If Not isAppRecordingPresent Then
                ' Better than doing nothing
                TakeScreenshot()
            Else
#If Not UNIVERSAL Then

                Dim manager = AppRecordingManager.GetDefault()
                If manager.GetStatus().CanRecord Then
#If Not UNPACKAGED Then

                    Dim localFolder1 As StorageFolder = ApplicationData.Current.LocalFolder
#Else

                    StorageFolder localFolder = await StorageFolder.GetFolderFromPathAsync(System.AppContext.BaseDirectory);

#End If

                    Dim result = Await manager.SaveScreenshotToFilesAsync(
                        localFolder1,
                        "appScreenshot",
                        AppRecordingSaveScreenshotOption.HdrContentVisible,
                        manager.SupportedScreenshotMediaEncodingSubtypes)

                    If result.Succeeded Then
                        ' Open the screenshot back up
                        Dim screenshotFile = Await localFolder1.GetFileAsync("appScreenshot.png")
                        Using stream = Await screenshotFile.OpenAsync(FileAccessMode.Read)
                            Dim decoder = Await BitmapDecoder.CreateAsync(stream)

                            ' Find the control in the picture
                            Dim t As GeneralTransform = ControlPresenter.TransformToVisual(Window.Current.Content)
                            Dim pos As Point = t.TransformPoint(New Point(0, 0))

                            If Not CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar Then
                                ' Add the height of the title bar, which I really wish was programmatically available anywhere.
                                pos.Y += 32.0
                            End If

                            ' Crop the screenshot to the control area
                            Dim transform As New BitmapTransform() With
{
                                .Bounds = New BitmapBounds() With
{
                                    .X = CUInt(Math.Ceiling(pos.X)) + 1UI, ' Avoid the 1px window border
                                    .Y = CUInt(Math.Ceiling(pos.Y)) + 1UI,
                                    .Width = CUInt(ControlPresenter.ActualWidth) - 1UI, ' Rounding issues -- this avoids capturing the control border
                                    .Height = CUInt(ControlPresenter.ActualHeight) - 1UI}}

                            Dim softwareBitmap = Await decoder.GetSoftwareBitmapAsync(
                                decoder.BitmapPixelFormat,
                                BitmapAlphaMode.Ignore,
                                transform,
                                ExifOrientationMode.IgnoreExifOrientation,
                                ColorManagementMode.DoNotColorManage)

                            ' Save the cropped picture
                            Dim file = Await localFolder1.CreateFileAsync(GetBestScreenshotName(), CreationCollisionOption.ReplaceExisting)
                            Using outStream = Await file.OpenAsync(FileAccessMode.ReadWrite)
                                Dim encoder As BitmapEncoder = Await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, outStream)
                                encoder.SetSoftwareBitmap(softwareBitmap)
                                Await encoder.FlushAsync()
                            End Using
                        End Using

                        ' Delete intermediate file
                        Await screenshotFile.DeleteAsync()
                    End If
                End If
#End If
            End If

            Await Task.Delay(1000)
            ScreenshotStatusTextBlock.Text = ""
        End Sub
        Private Function GetBestScreenshotName() As String
            Dim imageName As String = "Screenshot.png"
            If XamlSource IsNot Nothing Then
                ' Most of them don't have this, but the xaml source name is a really good file name
                Dim xamlSource1 As String = New Uri("ms-appx:///" & Path.Combine("ControlPagesSampleCode", XamlSource.LocalPath)).LocalPath
                Dim fileName As String = Path.GetFileNameWithoutExtension(xamlSource1)
                If Not [String].IsNullOrWhiteSpace(fileName) Then
                    imageName = fileName & ".png"
                End If
            ElseIf Not [String].IsNullOrWhiteSpace(Name) Then
                ' Put together the page name and the control example name
                Dim uie As UIElement = Me
                While uie IsNot Nothing AndAlso Not (TypeOf uie Is Page)
                    uie = TryCast(VisualTreeHelper.GetParent(uie), UIElement)
                End While
                If uie IsNot Nothing Then
                    Dim name1 As String = Name
                    If name1.Equals("RootPanel") Then
                        ' This is the default name for the example; add an index on the end to disambiguate
                        imageName = uie.[GetType]().Name & "_" & CType(Me.Parent, Panel).Children.IndexOf(Me).ToString() & ".png"
                    Else
                        imageName = uie.[GetType]().Name & "_" & name1 & ".png"
                    End If
                End If
            End If
            Return imageName
        End Function
        Private Sub ControlPaddingChangedCallback(sender As DependencyObject, dp As DependencyProperty)
            ControlPaddingBox.Text = ControlPresenter.Padding.ToString()
        End Sub
        Private Sub ControlPaddingBox_KeyUp(sender As Object, e As Microsoft.UI.Xaml.Input.KeyRoutedEventArgs)
            If e.Key = Windows.System.VirtualKey.Enter AndAlso Not [String].IsNullOrWhiteSpace(ControlPaddingBox.Text) Then
                EvaluatePadding()
            End If
        End Sub
        Private Sub ControlPaddingBox_LostFocus(sender As Object, e As RoutedEventArgs)
            EvaluatePadding()
        End Sub
        Private Sub EvaluatePadding()
            ' Evaluate the text in the ControlPaddingBox as padding
            Dim strs As String() = ControlPaddingBox.Text.Split(New Char() {" "c, ","c})
            Dim nums As Double() = New Double(3) {}
            For i As Integer = 0 To strs.Length - 1
                If Not [Double].TryParse(strs(i), nums(i)) Then
                    '  Bad format
                    Return
                End If
            Next

            Select Case nums.Length
                Case 1
                    ControlPresenter.Padding = New Thickness() With
{
                        .Left = nums(0),
                        .Top = nums(0),
                        .Right = nums(0),
                        .Bottom = nums(0)}

                Case 2
                    ControlPresenter.Padding = New Thickness() With
{
                        .Left = nums(0),
                        .Top = nums(1),
                        .Right = nums(0),
                        .Bottom = nums(1)}

                Case 4
                    ControlPresenter.Padding = New Thickness() With
{
                        .Left = nums(0),
                        .Top = nums(1),
                        .Right = nums(2),
                        .Bottom = nums(3)}
            End Select
        End Sub
    End Class
End Namespace
