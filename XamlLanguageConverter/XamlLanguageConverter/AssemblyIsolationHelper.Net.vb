Imports System.Runtime.Loader

Partial Public Class AssemblyIsolationHelper

    Private ReadOnly _asmContext As New AssemblyLoadContext(DomainName)
    Public Function CreateInstance(Of TInterface, TClass As TInterface)() As TInterface
        Dim asm = _asmContext.LoadFromAssemblyPath(GetType(TClass).Assembly.Location)
        Dim clsType = asm.GetType(GetType(TClass).FullName)
        Return Activator.CreateInstance(clsType)
    End Function

End Class
