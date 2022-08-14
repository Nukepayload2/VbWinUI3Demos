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
Imports Microsoft.UI.Xaml.Controls
Imports Microsoft.UI.Xaml.Navigation

#If Not UNIVERSAL

Imports System.ComponentModel
#Else

using Microsoft.UI.Xaml.Data;

#End If

Namespace AppUIBasics.ControlPages
    Public NotInheritable Partial Class CommandBarPage
        Inherits Page
        Implements INotifyPropertyChanged
        Private _multipleButtons As Boolean = False
        Public Property MultipleButtons As Boolean
            Get
                Return _multipleButtons
            End Get

            Set(value As Boolean)
                _multipleButtons = value
                OnPropertyChanged("MultipleButtons")
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements ComponentModel.INotifyPropertyChanged.PropertyChanged
        Public Sub OnPropertyChanged(PropertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(PropertyName))
        End Sub

        Public Sub New()
            Me.InitializeComponent()
            AddKeyboardAccelerators()
        End Sub
        Private Sub OpenButton_Click(sender As Object, e As RoutedEventArgs)
            PrimaryCommandBar.IsOpen = True
            PrimaryCommandBar.IsSticky = True
        End Sub
        Private Sub CloseButton_Click(sender As Object, e As RoutedEventArgs)
            PrimaryCommandBar.IsOpen = False
            PrimaryCommandBar.IsSticky = False
        End Sub
        Private Sub OnElementClicked(sender As Object, e As Microsoft.UI.Xaml.RoutedEventArgs)
            Dim selectedFlyoutItem = TryCast(sender, AppBarButton)
            SelectedOptionText.Text = "You clicked: " & TryCast(sender, AppBarButton).Label
        End Sub
        Private Sub AddSecondaryCommands_Click(sender As Object, e As RoutedEventArgs)
            ' Add compact button to the command bar. It provides functionality specific
            ' to this page, and is removed when leaving the page.

            If PrimaryCommandBar.SecondaryCommands.Count = 1 Then
                Dim newButton As New AppBarButton
                newButton.Icon = New SymbolIcon(Symbol.Add)
                newButton.Label = "Button 1"
                newButton.KeyboardAccelerators.Add(New Microsoft.UI.Xaml.Input.KeyboardAccelerator() With
                { _
                .Key = Windows.System.VirtualKey.N,
                .Modifiers = Windows.System.VirtualKeyModifiers.Control
                })
                PrimaryCommandBar.SecondaryCommands.Add(newButton)

                newButton = New AppBarButton With
                { _
                .Icon = New SymbolIcon(Symbol.Delete),
                .Label = "Button 2"
                }
                PrimaryCommandBar.SecondaryCommands.Add(newButton)
                newButton.KeyboardAccelerators.Add(New Microsoft.UI.Xaml.Input.KeyboardAccelerator() With
                { _
                .Key = Windows.System.VirtualKey.Delete
                })
                PrimaryCommandBar.SecondaryCommands.Add(New AppBarSeparator)

                newButton = New AppBarButton
                newButton.Icon = New SymbolIcon(Symbol.FontDecrease)
                newButton.Label = "Button 3"
                newButton.KeyboardAccelerators.Add(New Microsoft.UI.Xaml.Input.KeyboardAccelerator() With
                { _
                .Key = Windows.System.VirtualKey.Subtract,
                .Modifiers = Windows.System.VirtualKeyModifiers.Control
                })
                PrimaryCommandBar.SecondaryCommands.Add(newButton)

                newButton = New AppBarButton
                newButton.Icon = New SymbolIcon(Symbol.FontIncrease)
                newButton.Label = "Button 4"
                newButton.KeyboardAccelerators.Add(New Microsoft.UI.Xaml.Input.KeyboardAccelerator() With
                { _
                .Key = Windows.System.VirtualKey.Add,
                .Modifiers = Windows.System.VirtualKeyModifiers.Control
                })
                PrimaryCommandBar.SecondaryCommands.Add(newButton)

            End If
            MultipleButtons = True
        End Sub
        Private Sub RemoveSecondaryCommands_Click(sender As Object, e As RoutedEventArgs)
            RemoveSecondaryCommands()
        End Sub
        Protected Overrides Sub OnNavigatingFrom(e As NavigatingCancelEventArgs)
            RemoveSecondaryCommands()
            MyBase.OnNavigatingFrom(e)
        End Sub
        Private Sub RemoveSecondaryCommands()
            While PrimaryCommandBar.SecondaryCommands.Count > 1
                PrimaryCommandBar.SecondaryCommands.RemoveAt(PrimaryCommandBar.SecondaryCommands.Count - 1)
            End While
            MultipleButtons = False
        End Sub
        Private Sub AddKeyboardAccelerators()
            editButton.KeyboardAccelerators.Add(New Microsoft.UI.Xaml.Input.KeyboardAccelerator() With
            { _
            .Key = Windows.System.VirtualKey.E,
            .Modifiers = Windows.System.VirtualKeyModifiers.Control
            })

            shareButton.KeyboardAccelerators.Add(New Microsoft.UI.Xaml.Input.KeyboardAccelerator() With
            { _
            .Key = Windows.System.VirtualKey.F4
            })

            addButton.KeyboardAccelerators.Add(New Microsoft.UI.Xaml.Input.KeyboardAccelerator() With
            { _
            .Key = Windows.System.VirtualKey.A,
            .Modifiers = Windows.System.VirtualKeyModifiers.Control
            })
        End Sub

    End Class
End Namespace
