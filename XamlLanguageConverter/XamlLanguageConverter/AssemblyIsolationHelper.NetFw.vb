Imports System.IO
Imports System.Reflection

Partial Public Class AssemblyIsolationHelper

    Private ReadOnly _domain As AppDomain

    Sub New()
        Dim domainSetup As New AppDomainSetup With {
            .ApplicationBase = Path.GetDirectoryName(GetType(AssemblyIsolationHelper).Assembly.Location)
        }
        _domain = AppDomain.CreateDomain(DomainName, Nothing, domainSetup)
        CreateInstance(Of Object, AppDomainEventHelper)()
    End Sub

    Public Function CreateInstance(Of TInterface, TClass As TInterface)() As TInterface
        Return _domain.CreateInstanceAndUnwrap(GetType(TClass).Assembly.FullName, GetType(TClass).FullName)
    End Function

End Class

Public Class AppDomainEventHelper
    Inherits MarshalByRefObject

    Private WithEvents Domain As AppDomain = AppDomain.CurrentDomain

    Private Function Domain_AssemblyResolve(
            sender As Object, args As ResolveEventArgs
            ) As Assembly Handles Domain.AssemblyResolve

        Dim curDomain = Domain
        Dim found As Assembly = Nothing
        Dim asmShortName = New AssemblyName(args.Name).Name
        found = (From asm In curDomain.GetAssemblies
                 Where asm.GetName.Name = asmShortName).FirstOrDefault

        If found Is Nothing Then
            found = FindFromDir(args, curDomain.SetupInformation.ApplicationBase)
        End If

        Debug.WriteLine($"Resolve {asmShortName}: {If(found Is Nothing,
                          "Failed", "Success")}")
        Return found
    End Function

    Private Shared Function FindFromDir(args As ResolveEventArgs, lookIn As String) As Assembly
        Debug.WriteLine("Find = " & lookIn & ", Find asm " & args.Name)

        If Directory.Exists(lookIn) Then
            Dim asmName = New AssemblyName(args.Name)
            If Not String.IsNullOrEmpty(lookIn) Then
                Dim fnWithoutExt = asmName.Name

                For Each ext In {".dll"}
                    Dim asmFileName = fnWithoutExt & ext
                    Dim asmFilePath = Path.Combine(lookIn, asmFileName)
                    If File.Exists(asmFilePath) Then
                        Debug.WriteLine("Found " & asmFilePath)
                        Return Assembly.LoadFile(asmFilePath)
                    End If
                Next ext
            End If
        End If

        Debug.WriteLine("Not found")
        Return Nothing
    End Function

    Public Overrides Function InitializeLifetimeService() As Object
        Return Nothing
    End Function
End Class
