'*********************************************************
'
' Copyright (c) Microsoft. All rights reserved.
' THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
' ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
' IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
' PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'*********************************************************


Imports Microsoft.UI.Xaml.Navigation
Imports Microsoft.UI.Xaml.Controls

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class DatePickerPage
        Inherits Page
        Public Sub New()
            Me.InitializeComponent()
        End Sub
        Protected Overrides Sub OnNavigatedTo(e As NavigationEventArgs)
            ' Set the default date to 2 months from the current date.
            Control2.[Date] = DateTimeOffset.Now.AddMonths(2)

            ' Set the minimum year to the current year.
            Control2.MinYear = DateTimeOffset.Now

            ' Set the maximum year to 5 years in the future.
            Control2.MaxYear = DateTimeOffset.Now.AddYears(5)
        End Sub
    End Class
End Namespace
