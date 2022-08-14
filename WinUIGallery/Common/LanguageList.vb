' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace AppUIBasics.Common
    Class LanguageList
        Private _languages As List(Of Language)
        Public ReadOnly Property Languages As List(Of Language)
            Get
                Return _languages
            End Get
        End Property

        Public Sub New()
            If _languages Is Nothing Then
                _languages = New List(Of Language)
            End If

            _languages.Add(New Language("English", "en"))
            _languages.Add(New Language("Arabic", "ar"))
            _languages.Add(New Language("Afrikaans", "af"))
            _languages.Add(New Language("Albanian", "sq"))
            _languages.Add(New Language("Amharic", "am"))
            _languages.Add(New Language("Armenian", "hy"))
            _languages.Add(New Language("Assamese", "as"))
            _languages.Add(New Language("Azerbaijani", "az"))
            _languages.Add(New Language("Basque ", "eu"))
            _languages.Add(New Language("Belarusian", "be"))
            _languages.Add(New Language("Bangla", "bn"))
            _languages.Add(New Language("Bosnian", "bs"))
            _languages.Add(New Language("Bulgarian", "bg"))
            _languages.Add(New Language("Catalan", "ca"))
            _languages.Add(New Language("Chinese (Simplified)", "zh"))
            _languages.Add(New Language("Croatian", "hr"))
            _languages.Add(New Language("Czech", "cs"))
            _languages.Add(New Language("Danish", "da"))
            _languages.Add(New Language("Dari", "prs"))
            _languages.Add(New Language("Dutch", "nl"))
            _languages.Add(New Language("Estonian", "et"))
            _languages.Add(New Language("Filipino", "fil"))
            _languages.Add(New Language("Finnish", "fi"))
            _languages.Add(New Language("French ", "fr"))
            _languages.Add(New Language("Galician", "gl"))
            _languages.Add(New Language("Georgian", "ka"))
            _languages.Add(New Language("German", "de"))
            _languages.Add(New Language("Greek", "el"))
            _languages.Add(New Language("Gujarati", "gu"))
            _languages.Add(New Language("Hausa", "ha"))
            _languages.Add(New Language("Hebrew", "he"))
            _languages.Add(New Language("Hindi", "hi"))
            _languages.Add(New Language("Hungarian", "hu"))
            _languages.Add(New Language("Icelandic", "is"))
            _languages.Add(New Language("Indonesian", "id"))
            _languages.Add(New Language("Irish", "ga"))
            _languages.Add(New Language("isiXhosa", "xh"))
            _languages.Add(New Language("isiZulu", "zu"))
            _languages.Add(New Language("Italian", "it"))
            _languages.Add(New Language("Japanese ", "ja"))
            _languages.Add(New Language("Kannada", "kn"))
            _languages.Add(New Language("Kazakh", "kk"))
            _languages.Add(New Language("Khmer", "km"))
            _languages.Add(New Language("Kinyarwanda", "rw"))
            _languages.Add(New Language("KiSwahili", "sw"))
            _languages.Add(New Language("Konkani", "kok"))
            _languages.Add(New Language("Korean", "ko"))
            _languages.Add(New Language("Lao", "lo"))
            _languages.Add(New Language("Latvian", "lv"))
            _languages.Add(New Language("Lithuanian", "lt"))
            _languages.Add(New Language("Luxembourgish", "lb"))
            _languages.Add(New Language("Macedonian", "mk"))
            _languages.Add(New Language("Malay", "ms"))
            _languages.Add(New Language("Malayalam", "ml"))
            _languages.Add(New Language("Maltese", "mt"))
            _languages.Add(New Language("Maori ", "mi"))
            _languages.Add(New Language("Marathi", "mr"))
            _languages.Add(New Language("Nepali", "ne"))
            _languages.Add(New Language("Norwegian", "nb"))
            _languages.Add(New Language("Odia", "or"))
            _languages.Add(New Language("Persian", "fa"))
            _languages.Add(New Language("Polish", "pl"))
            _languages.Add(New Language("Portuguese", "pt"))
            _languages.Add(New Language("Punjabi", "pa"))
            _languages.Add(New Language("Quechua", "quz"))
            _languages.Add(New Language("Romanian", "ro"))
            _languages.Add(New Language("Russian", "ru"))
            _languages.Add(New Language("Serbian (Latin)", "sr"))
            _languages.Add(New Language("Sesotho sa Leboa", "nso"))
            _languages.Add(New Language("Setswana", "tn"))
            _languages.Add(New Language("Sinhala", "si"))
            _languages.Add(New Language("Slovak ", "sk"))
            _languages.Add(New Language("Slovenian", "sl"))
            _languages.Add(New Language("Spanish", "es"))
            _languages.Add(New Language("Swedish", "sv"))
            _languages.Add(New Language("Tamil", "ta"))
            _languages.Add(New Language("Telugu", "te"))
            _languages.Add(New Language("Thai", "th"))
            _languages.Add(New Language("Tigrinya", "ti"))
            _languages.Add(New Language("Turkish", "tr"))
            _languages.Add(New Language("Ukrainian", "uk"))
            _languages.Add(New Language("Urdu", "ur"))
            _languages.Add(New Language("Uzbek (Latin)", "uz"))
            _languages.Add(New Language("Vietnamese", "vi"))
            _languages.Add(New Language("Welsh", "cy"))
            _languages.Add(New Language("Wolof", "wo"))

        End Sub
        Public Class Language
            Public Property Name As String
            Public Property Code As String

            Public Sub New(name1 As String, code1 As String)
                Me.Name = name1
                Me.Code = code1
            End Sub
        End Class
    End Class
End Namespace
