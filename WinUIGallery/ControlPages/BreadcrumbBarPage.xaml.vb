' To configure or remove Option's included in result, go to Options/Advanced Options...
Option Compare Text
Option Explicit On
Option Infer Off
Option Strict On
Imports System.Collections.Generic
Imports Microsoft.UI.Xaml.Controls


Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class BreadcrumbBarPage
        Inherits Page

        Public Sub New()
            Me.InitializeComponent()
            BreadcrumbBar1.ItemsSource = New String() {"Home", "Documents", "Design", "Northwind", "Images", "Folder1", "Folder2", "Folder3"}

            BreadcrumbBar2.ItemsSource = New List(Of Folder) From { _
                New Folder With
{
                    .Name = "Home"},
                New Folder With
{
                    .Name = "Folder1"},
                New Folder With
{
                    .Name = "Folder2"}
            }
        End Sub
    End Class


    Public Class Folder
        Public Property Name As String
    End Class
End Namespace
