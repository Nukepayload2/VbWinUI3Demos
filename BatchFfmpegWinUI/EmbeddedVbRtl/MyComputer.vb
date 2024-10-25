Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports BatchFfmpegWinUI
Imports Microsoft.UI.Xaml

Namespace My
    Module MyComputer
        Public ReadOnly Property Computer As New Global.Microsoft.VisualBasic.Devices.Computer
    End Module

End Namespace

Namespace Global.Microsoft.VisualBasic.Devices
    Class Computer
        Public ReadOnly Property Audio As New Audio
        Public ReadOnly Property FileSystem As New FileIO.FileSystem
    End Class

    Class Audio
        Public Sub PlaySystemSound(kind As ElementSoundKind)
            ElementSoundPlayer.Play(kind)
        End Sub
    End Class

End Namespace

Namespace Global.Microsoft.VisualBasic.FileIO

    Class FileSystem
        Private Declare Unicode Function SHFileOperation Lib "shell32.dll" Alias "SHFileOperationW" (
            ByRef lpFileOp As SHFILEOPSTRUCT64W) As Integer

        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
        Public Structure SHFILEOPSTRUCT64W
            Public hwnd As IntPtr
            Public wFunc As SHFileOperationType
            <MarshalAs(UnmanagedType.LPWStr)>
            Public pFrom As String
            <MarshalAs(UnmanagedType.LPWStr)>
            Public pTo As String
            Public fFlags As ShFileOperationFlags
            Public fAnyOperationsAborted As Integer
            Public hNameMappings As IntPtr
            <MarshalAs(UnmanagedType.LPWStr)>
            Public lpszProgressTitle As String
        End Structure

        ''' <summary>
        ''' Values that indicate which file operation to perform. Used in SHFILEOPSTRUCT
        ''' </summary>
        Friend Enum SHFileOperationType As UInteger
            FO_MOVE = &H1
            FO_COPY = &H2
            FO_DELETE = &H3
            FO_RENAME = &H4
        End Enum

        ''' <summary>
        ''' Flags that control the file operation. Used in SHFILEOPSTRUCT.
        ''' </summary>
        <Flags>
        Friend Enum ShFileOperationFlags As UShort
            ' The pTo member specifies multiple destination files (one for each source file)
            ' rather than one directory where all source files are to be deposited.
            FOF_MULTIDESTFILES = &H1
            ' Not currently used.
            FOF_CONFIRMMOUSE = &H2
            ' Do not display a progress dialog box.
            FOF_SILENT = &H4
            ' Give the file being operated on a new name in a move, copy, or rename operation
            ' if a file with the target name already exists.
            FOF_RENAMEONCOLLISION = &H8
            ' Respond with "Yes to All" for any dialog box that is displayed.
            FOF_NOCONFIRMATION = &H10
            ' If FOF_RENAMEONCOLLISION is specified and any files were renamed,
            ' assign a name mapping object containing their old and new names to the hNameMappings member.
            FOF_WANTMAPPINGHANDLE = &H20
            ' Preserve Undo information, if possible. Undone can only be done from the same process.
            ' If pFrom does not contain fully qualified path and file names, this flag is ignored.
            ' NOTE: Not setting this flag will let the file be deleted permanently, unlike the doc says.
            FOF_ALLOWUNDO = &H40
            ' Perform the operation on files only if a wildcard file name (*.*) is specified.
            FOF_FILESONLY = &H80
            ' Display a progress dialog box but do not show the file names.
            FOF_SIMPLEPROGRESS = &H100
            ' Do not confirm the creation of a new directory if the operation requires one to be created.
            FOF_NOCONFIRMMKDIR = &H200
            ' Do not display a user interface if an error occurs.
            FOF_NOERRORUI = &H400
            ' Do not copy the security attributes of the file.
            FOF_NOCOPYSECURITYATTRIBS = &H800
            ' Only operate in the local directory. Don't operate recursively into subdirectories.
            FOF_NORECURSION = &H1000
            ' Do not move connected files as a group. Only move the specified files.
            FOF_NO_CONNECTED_ELEMENTS = &H2000
            ' Send a warning if a file is being destroyed during a delete operation rather than recycled.
            ' This flag partially overrides FOF_NOCONFIRMATION.
            FOF_WANTNUKEWARNING = &H4000
            ' Treat reparse points as objects, not containers.
            FOF_NORECURSEREPARSE = &H8000
        End Enum

        Public Sub RecycleFile(filePath As String)
            Dim fileOp As New SHFILEOPSTRUCT64W With {
                .wFunc = SHFileOperationType.FO_DELETE,
                .pFrom = filePath & (ChrW(0) & ChrW(0)),
                .fFlags = ShFileOperationFlags.FOF_ALLOWUNDO Or ShFileOperationFlags.FOF_SILENT Or
                          ShFileOperationFlags.FOF_NOCONFIRMATION Or ShFileOperationFlags.FOF_NOERRORUI
            }

            Dim result = SHFileOperation(fileOp)
            If result <> 0 Then
                Throw New Win32Exception("Failed to recycle file. " & Marshal.GetPInvokeErrorMessage(result))
            End If
        End Sub

    End Class
End Namespace
