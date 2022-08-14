'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************


Imports Microsoft.UI.Xaml
Imports System.Collections.Generic

Namespace AppUIBasics
    ' Helper class to allow the app to find the Window that contains an
    ' arbitrary UIElement (GetWindowForElement).  To do this, we keep track
    ' of all active Windows.  The app code must call WindowHelper.CreateWindow
    ' rather than "new Window" so we can keep track of all the relevant
    ' windows.  In the future, we would like to support this in platform APIs.
    Public NotInheritable Class WindowHelper
        Shared Public Function CreateWindow() As Window
            Dim newWindow As New Window
            TrackWindow(newWindow)
            Return newWindow
        End Function
        Shared Public Sub TrackWindow(window1 As Window)
            AddHandler window1.Closed, Sub(sender, args)
                                           _activeWindows.Remove(window1)
                                       End Sub
            _activeWindows.Add(window1)
        End Sub
        Shared Public Function GetWindowForElement(element As UIElement) As Window
            If element.XamlRoot IsNot Nothing Then
                For Each window1 As Window In _activeWindows
                    If element.XamlRoot = window1.Content.XamlRoot Then
                        Return window1
                    End If
                Next
            End If
            Return Nothing
        End Function
        Shared Public ReadOnly Property ActiveWindows As List(Of Window)
            Get
                Return _activeWindows
            End Get
        End Property
        Shared Private _activeWindows As New List(Of Window)
    End Class
End Namespace
