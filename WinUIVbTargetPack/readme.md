# Nukepayload2.UI.VBWinUI3

WinUI 3 build support for Visual Basic projects: the MSBuild workarounds a VB project needs, plus the
custom XAML compiler whose VB code generators are fixed.

https://github.com/Nukepayload2/VbWinUI3Demos

## Usage

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.WindowsAppSDK" Version="2.2.0" />
  <PackageReference Include="Nukepayload2.UI.VBWinUI3" Version="0.11.0-beta" />
</ItemGroup>
```

That is the whole integration. The package's props and targets are named after the package id, so
NuGet imports them from `buildTransitive\` - do not `Import` them by hand, and do not copy anything
from the repository. `Nukepayload2.UI.VBWinUI3.XamlCompiler` comes in as a dependency, so the XAML
compiler is redirected to the fork without any further reference.

## What it applies

| | |
| --- | --- |
| `ImportFrameworkWinFXTargets=true` | keeps WPF's framework targets out of the project |
| `VBRuntime=None` before `XamlPreCompile` | works around XAML pre-compile errors |
| `WindowsAppSdkAutoInitialize` / `WindowsAppSdkUndockedRegFreeWinRTInitialize` off, plus `buildTransitive\include\UndockedRegFreeWinRTAutoInitializer.vb` | unpackaged self-contained apps (`WindowsAppSDKSelfContained=true`) load the Windows App SDK runtime |

Set `VBWinUI3XamlCompilerEnabled=false` to keep the stock XAML compiler instead of the fork's.
To pin a different compiler build, reference `Nukepayload2.UI.VBWinUI3.XamlCompiler` directly - a
direct reference wins over the dependency.

## Upgrading from 0.9.4-beta

0.10.0-beta no longer defines `DISABLE_XAML_GENERATED_MAIN`: the compiler generates `Program` and
`Sub Main` itself, so delete the entry point you had to write by hand - keeping it duplicates the
entry point and fails the build. Define `DISABLE_XAML_GENERATED_MAIN` yourself if you want the old
behaviour.

## Status

Beta. Built and tested against `Microsoft.WindowsAppSDK` 2.2.0, `net10.0-windows10.0.19041.0`, with
both `dotnet build` (Core MSBuild) and Visual Studio (desktop MSBuild).

## Contributing

Issues related to WinUI 3 itself belong to the official Windows App SDK repository.
