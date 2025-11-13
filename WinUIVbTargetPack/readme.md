# Nukepayload2.UI.VBWinUI3

WinUI 3 build support for Visual Basic projects with automatic MSBuild target integration.

https://github.com/Nukepayload2/VbWinUI3Demos

## Features

- **Automatic Target Integration**: Import WinUI 3 specific MSBuild targets for VB projects that applies workarounds for compatibility issues
- **RegFree WinRT Support**: Includes `UndockedRegFreeWinRTAutoInitializer.vb` for self-contained applications (`<WindowsAppSDKSelfContained>true</WindowsAppSDKSelfContained>`)

## Requirements

- Visual Basic projects that `<UseWinUI>true</UseWinUI>`
- Microsoft.WindowsAppSDK (consumed projects should reference this)

## Contributing

This is a build integration package. Issues related to WinUI 3 itself should be reported to the official Windows App SDK repository.