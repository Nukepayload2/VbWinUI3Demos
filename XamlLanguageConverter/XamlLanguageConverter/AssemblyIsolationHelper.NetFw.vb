Imports System.IO

Partial Public Class AssemblyIsolationHelper

    Private ReadOnly _domain As AppDomain
    Sub New()
        Dim domainSetup As New AppDomainSetup With {
            .ApplicationBase = Path.GetDirectoryName(GetType(AssemblyIsolationHelper).Assembly.Location)
        }
        _domain = AppDomain.CreateDomain(DomainName, Nothing, domainSetup)
    End Sub

    Public Function CreateInstance(Of TInterface, TClass As TInterface)() As TInterface
        Return _domain.CreateInstanceAndUnwrap(GetType(TClass).Assembly.FullName, GetType(TClass).FullName)
    End Function

End Class
