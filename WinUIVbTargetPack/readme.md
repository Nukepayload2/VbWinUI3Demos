# Nukepayload2.UI.VBWinUI3

WinUI 3 build support for Visual Basic projects with automatic MSBuild target integration.

https://github.com/Nukepayload2/VbWinUI3Demos

## Description

This NuGet package provides WinUI 3 build support for Visual Basic .NET projects by automatically importing the necessary MSBuild targets and support files. When you add this package to your VB WinUI 3 project, you no longer need to manually edit your `.vbproj` file to include WinUI 3 workarounds.

## Features

- **Automatic Target Integration**: Automatically imports `WinUI3.VisualBasic.targets` when package is added
- **RegFree WinRT Support**: Includes `UndockedRegFreeWinRTAutoInitializer.vb` for self-contained applications
- **Development Dependency**: Configured as a development-only dependency that won't flow to consumers
- **Zero Runtime Dependencies**: Pure build-time asset package with no runtime assemblies

## Installation

```powershell
dotnet add package Nukepayload2.UI.VBWinUI3
```

## Usage

The package will automatically:

1. Import WinUI 3 specific MSBuild targets for VB projects
2. Apply workarounds for WinUI 3 + VB compatibility issues
3. Include necessary support files for RegFree WinRT initialization

## What's Included

### MSBuild Targets
- `WinUI3.VisualBasic.targets` - Contains WinUI 3 workarounds for VB projects:
  - WPF conflict workarounds
  - XAML generation fixes
  - RegFree WinRT initialization support

### Support Files
- `UndockedRegFreeWinRTAutoInitializer.vb` - Runtime initializer for self-contained WinUI 3 applications

## Requirements

- Visual Basic projects that `<UseWinUI>true</UseWinUI>`
- Microsoft.WindowsAppSDK (consumed projects should reference this)

## Configuration

The package automatically applies WinUI 3 VB workarounds through the imported targets file. No additional configuration is required.

## Package Structure

```
buildTransitive/
├── Nukepayload2.UI.VBWinUI3.targets    # Package entry point (auto-imported)
├── WinUI3.VisualBasic.targets          # Actual VB WinUI 3 targets
└── include/
    └── UndockedRegFreeWinRTAutoInitializer.vb  # RegFree WinRT support
```

## Contributing

This is a build integration package. Issues related to WinUI 3 itself should be reported to the official Windows App SDK repository.