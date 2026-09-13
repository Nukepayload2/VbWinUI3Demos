# VB WinUI 3 Demos
Demonstrates how to use WinUI 3 in Visual Basic .NET projects.

## Status

Works, verified with Visual Studio and with `dotnet build` (Windows App SDK 2.2.0, .NET 10):

- XAML compilation by the forked compiler, including the `Sub Main` entry point - a VB project needs no `Program.vb`.
- XBF generation, running self-contained without MSIX, Mica/Acrylic, WinForms host integration (`HostServices.VBHost`).
- `BatchFfmpegWinUI` builds warning-clean (`0 warnings, 0 errors`) and shows its window; all three of its XAML files compile.
- `BatchFfmpegWinUI` binds with `x:Bind`, including `x:DataType` templates.

## Progress
- [x] No C# or C++ projects for startup
- [x] Load `XamlControlsResources` from `Microsoft.UI.Xaml.Controls` automatically
- [x] Let the XAML compiler generate the entry point - no `Program.vb` is needed; `DISABLE_XAML_GENERATED_MAIN` remains available for taking the entry point over manually.
- [x] Allow Windows Forms integration, such as registering `HostServices.VBHost` at startup
- [x] Make sure Mica and Acrylic are working as expected on Windows 11
- [x] Workaround blocking bugs of the WinUI 3 XAML compiler - Generate Xbf files and VB source files
- [x] Run apps without MSIX packaging
- [x] Use a custom XAML compiler (Approach 3) - see [Custom XAML compiler](#custom-xaml-compiler)
- [x] Publish a NuGet package that contains VB specific build transitive files, so any VB projects will be able to use WinUI 3 without editing `*.vbproj` manually.
- [ ] Publish a VSIX to add VB WinUI 3 templates to Visual Studio.
- [ ] Write a new VB application framework for WinUI 3 - Allow users to delete `Program.vb` and use events to configure the App in the `Application` class
- [ ] Write a new VB "My extension" for WinUI 3 - Enable `My.*` for WinUI 3 specific things

## Environment
- Visual Studio 2026 with Windows Desktop and WinUI development workloads
- Windows 10 21H2+ or Windows 11

## Build
`vbxamlc` is a submodule that holds the compiler and the local NuGet feed. Its nupkg is not committed, so pack it once:

```bat
git submodule update --init --recursive
cd vbxamlc
msbuild XamlCompilerPrerequisites.sln /p:Configuration=Release /p:Platform=x64 /restore /m
build\xamlcompiler-nupkg\pack.cmd
cd ..
```

Then build `BatchFfmpegWinUI.sln` in Visual Studio, or:

```bat
dotnet build BatchFfmpegWinUI\BatchFfmpegWinUI.vbproj -c Debug -p:Platform=x64
```

If the compiler build stops with `MSB4019` about `Microsoft.MSBuildCache.Local`, run `vbxamlc\init.cmd` once (or put that package under `vbxamlc\packages\`).

To check which compiler was used: the build log prints `VBWinUI3 XamlCompiler override active: ...` with paths under the package, and generated files carry the `3.0.0.0` stamp (the stock compiler stamps `3.0.0.2606`). Repacking, version bumps and cache rules are in `vbxamlc\build\xamlcompiler-nupkg\readme.md`.

## How does it work

- **Approach 1 - VB workarounds**: two MSBuild files (`WinUI3.VisualBasic.props` / `.targets`) that `BatchFfmpegWinUI` imports through `Directory.Build.props`.
- **Approach 2 - C# compiler plus code converter**: abandoned, see `XamlLanguageConverter`.
- **Approach 3 - custom XAML compiler**: what this repository uses. Details below.

## Custom XAML compiler

The XAML compiler comes from [winui3-vbxamlc](https://github.com/Nukepayload2/winui3-vbxamlc), whose Visual Basic code generators are fixed, and is consumed as a compiler-only NuGet package. The package replaces three tool paths of the stock WinUI package and leaves its targets, `genxbf` and reference assemblies in place.

### Using it in another VB project

Reference `Microsoft.WindowsAppSDK` and `Nukepayload2.UI.VBWinUI3` - nothing else:

| Package | Purpose |
| --- | --- |
| `Nukepayload2.UI.VBWinUI3` | VB build support: `ImportFrameworkWinFXTargets`, `VBRuntime=None` for `XamlPreCompile`, reg-free WinRT initializer for self-contained apps |
| `Nukepayload2.UI.VBWinUI3.XamlCompiler` | redirects the XAML compiler to the fork; arrives as a dependency of the package above |

Everything `BatchFfmpegWinUI` imports by hand (`WinUI3.VisualBasic.props`, `WinUI3.VisualBasic.targets` and `include\UndockedRegFreeWinRTAutoInitializer.vb`) ships under `buildTransitive\`, named after the package id, so NuGet imports it automatically - there is nothing to copy from this repository and nothing to `Import`.

`BatchFfmpegWinUI` itself stays on the source-tree import instead of referencing the package: it is the development project these files and the compiler are built from.

`0.10.0-beta` no longer defines `DISABLE_XAML_GENERATED_MAIN`, so the compiler generates `Program` and `Sub Main` itself; delete the entry point you had to write for `0.9.4-beta`.

This repository's `nuget.config` adds `vbxamlc\PackageStore` as a feed: that is where the sample picks up compiler packages built from the submodule, before they are pushed.

## Samples
### BatchFfmpegWinUI
A `mp4` video to `h265` transcoder.

It converts `.mp4` and `.mkv` videos to `h265` encoding by calling `H265.bat`.

#### Status
✅
Builds and runs with Windows App SDK `2.2.0`.

#### How to use it
- Install `ffmpeg` to `%programfiles%\ffmpeg\bin`. If you have installed it in a different location, please add it to `%PATH%`.
- Open `BatchFfmpegWinUI.sln` with Visual Studio.
- Set `BatchFfmpegWinUI_Pack` as startup project and run.
- Drag and drop `.mp4` or `.mkv` files.
- Press the "Convert" button and wait.

### XamlLanguageConverter
Demonstrates how to use MSBuild extension points to invoke the C# XAML compiler and start a [code converter](https://github.com/Nukepayload2/CSharpToVB-Backports) to convert the C# output to VB.

#### Status
❌
Stopped with error. Because it has severe performance issues.
