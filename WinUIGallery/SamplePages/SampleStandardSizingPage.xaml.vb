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

Namespace AppUIBasics.SamplePages
    Public NotInheritable Partial Class SampleStandardSizingPage
        Inherits Page
        Public ReadOnly Property FirstName As TextBox
            Get
                Return firstName1
            End Get
        End Property
        Public ReadOnly Property LastName As TextBox
            Get
                Return lastName1
            End Get
        End Property
        Public ReadOnly Property Password As PasswordBox
            Get
                Return password1
            End Get
        End Property
        Public ReadOnly Property ConfirmPassword As PasswordBox
            Get
                Return confirmPassword1
            End Get
        End Property
        Public ReadOnly Property ChosenDate As DatePicker
            Get
                Return chosenDate1
            End Get
        End Property

        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Public Sub CopyState(page1 As SampleCompactSizingPage)
            FirstName.Text = page1.FirstName.Text
            LastName.Text = page1.LastName.Text
            Password.Password = page1.Password.Password
            ConfirmPassword.Password = page1.ConfirmPassword.Password
            ChosenDate.[Date] = page1.ChosenDate.[Date]
        End Sub
    End Class
End Namespace
