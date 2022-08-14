Imports System.Runtime.InteropServices

<ComImport>
<InterfaceType(ComInterfaceType.InterfaceIsIUnknown)>
<Guid("EECDBF0E-BAE9-4CB6-A68E-9598E1CB57BB")>
Interface IWindowNative
    ReadOnly Property WindowHandle As IntPtr
End Interface
