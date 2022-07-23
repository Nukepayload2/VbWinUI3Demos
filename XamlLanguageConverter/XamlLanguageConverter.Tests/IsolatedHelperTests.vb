Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Nukepayload2.XamlLanguageConverter

<TestClass>
Public Class IsolatedHelperTests

    <TestMethod>
    Sub ConvertCsConsoleApp()
        Dim refs = (Aggregate ref In AppDomain.CurrentDomain.GetAssemblies
                    Where Not ref.IsDynamic
                    Select ref.Location Into ToArray)
        Dim consts As New List(Of KeyValuePair(Of String, Object)) From {
            New KeyValuePair(Of String, Object)("NETCOREAPP", True)
        }

        Dim isolationHelper As New AssemblyIsolationHelper

        Dim convIsolated = isolationHelper.CreateInstance(Of ICsVbCodeConverter, CsVbCodeConverter)()

        With convIsolated
            .References = refs
            .DefinedConstants = consts
            .WarningLog = Sub(text) Console.WriteLine(text)
        End With

        Dim csCode = <![CDATA[
using System;

public class Example 
{
    public static void Main()
    {
        Console.Write("Hello ");
        Console.WriteLine("World!");
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        Console.Write("Good day, ");
        Console.Write(name);
        Console.WriteLine("!");
    }
}
]]>
        Dim vbCode = convIsolated.ConvertSource(csCode.Value)

        Assert.IsTrue(vbCode.Contains("End Class"))
    End Sub

End Class
