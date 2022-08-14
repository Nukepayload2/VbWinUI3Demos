' To configure or remove Option's included in result, go to Options/Advanced Options...
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Input

Namespace AppUIBasics.Common
    ''' <summary>
    ''' A command whose sole purpose is to relay its functionality 
    ''' to other objects by invoking delegates. 
    ''' The default return value for the CanExecute method is 'true'.
    ''' <see cref="RaiseCanExecuteChanged"/> needs to be called whenever
    ''' <see cref="CanExecute"/> is expected to return a different value.
    ''' </summary>
    Public Class RelayCommand
        Implements ICommand
        Private ReadOnly _execute As Action
        Private ReadOnly _canExecute As Func(Of Boolean)

        ''' <summary>
        ''' Raised when RaiseCanExecuteChanged is called.
        ''' </summary>
        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        ''' <summary>
        ''' Creates a new command that can always execute.
        ''' </summary>
        ''' <param name="execute">The execution logic.</param>
        Public Sub New(execute As Action)
            MyClass.New(execute, Nothing)
        End Sub

        ''' <summary>
        ''' Creates a new command.
        ''' </summary>
        ''' <param name="execute">The execution logic.</param>
        ''' <param name="canExecute">The execution status logic.</param>
        Public Sub New(execute As Action, canExecute As Func(Of Boolean))
            If execute Is Nothing Then Throw New ArgumentNullException("execute")
            _execute = execute
            _canExecute = canExecute
        End Sub
        ''' <summary>
        ''' Determines whether this <see cref="RelayCommand"/> can execute in its current state.
        ''' </summary>
        ''' <param name="parameter">
        ''' Data used by the command. If the command does not require data to be passed, this object can be set to null.
        ''' </param>
        ''' <returns>true if this command can be executed; otherwise, false.</returns>
        Public Function CanExecute1(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return If(_canExecute Is Nothing, True, _canExecute())
        End Function
        ''' <summary>
        ''' Executes the <see cref="RelayCommand"/> on the current command target.
        ''' </summary>
        ''' <param name="parameter">
        ''' Data used by the command. If the command does not require data to be passed, this object can be set to null.
        ''' </param>
        Public Sub Execute1(parameter As Object) Implements ICommand.Execute
            _execute()
        End Sub
        ''' <summary>
        ''' Method used to raise the <see cref="CanExecuteChanged"/> event
        ''' to indicate that the return value of the <see cref="CanExecute"/>
        ''' method has changed.
        ''' </summary>
        Public Sub RaiseCanExecuteChanged()
            RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
        End Sub
    End Class
End Namespace
