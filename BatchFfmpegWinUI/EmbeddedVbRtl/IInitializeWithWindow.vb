Imports System.Runtime.InteropServices

<ComImport>
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
<Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")>
Interface IInitializeWithWindow
    Sub Initialize(hwnd As IntPtr)
End Interface
