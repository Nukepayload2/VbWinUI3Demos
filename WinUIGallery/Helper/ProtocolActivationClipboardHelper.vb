' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports AppUIBasics.Data
Imports Windows.ApplicationModel
Imports Windows.ApplicationModel.DataTransfer
Imports Windows.Storage

Namespace AppUIBasics.Helper
    ''' <summary>
    ''' Class providing functionality to support generating and copying protocol activation URIs.
    ''' </summary>
    Public Module ProtocolActivationClipboardHelper
        Private Const ShowCopyLinkTeachingTipKey As String = "ShowCopyLinkTeachingTip"
#If UNPACKAGED
        private static bool _showCopyLinkTeachingTip = true;
#End If
        Public Property ShowCopyLinkTeachingTip As Boolean
            Get
#If Not UNPACKAGED
                Dim valueFromSettings As Object = ApplicationData.Current.LocalSettings.Values(ShowCopyLinkTeachingTipKey)
                If valueFromSettings Is Nothing Then
                    ApplicationData.Current.LocalSettings.Values(ShowCopyLinkTeachingTipKey) = True
                    valueFromSettings = True
                End If

                Return CBool(valueFromSettings)
#Else
                return _showCopyLinkTeachingTip;
#End If
            End Get

#If Not UNPACKAGED
            Set(value As Boolean)
                ApplicationData.Current.LocalSettings.Values(ShowCopyLinkTeachingTipKey) = value
            End Set
#Else
            set => _showCopyLinkTeachingTip = value;
#End If
        End Property
        Public Sub Copy(item As ControlInfoDataItem)
            Dim uri1 As System.Uri = New Uri($"winui3gallery://item/{item.UniqueId}", UriKind.Absolute)
            ProtocolActivationClipboardHelper.Copy(uri1, $"{Package.Current.DisplayName} - {item.Title} Sample")
        End Sub
        Public Sub Copy(group As ControlInfoDataGroup)
            Dim uri1 As System.Uri = New Uri($"winui3gallery://category/{group.UniqueId}", UriKind.Absolute)
            ProtocolActivationClipboardHelper.Copy(uri1, $"{Package.Current.DisplayName} - {group.Title} Samples")
        End Sub
        Private Sub Copy(uri1 As Uri, displayName1 As String)
            Dim htmlFormat As String = HtmlFormatHelper.CreateHtmlFormat($"<a href='{uri1}'>{displayName1}</a>")

            Dim dataPackage1 As New DataPackage
            dataPackage1.SetApplicationLink(uri1)
            dataPackage1.SetWebLink(uri1)
            dataPackage1.SetText(uri1.ToString())
            dataPackage1.SetHtmlFormat(htmlFormat)

            Clipboard.SetContentWithOptions(dataPackage1, Nothing)
        End Sub
    End Module
End Namespace
