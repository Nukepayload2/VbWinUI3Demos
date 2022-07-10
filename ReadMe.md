# VB WinUI 3 Demos
Demonstrates how to use WinUI 3 in Visual Basic .NET projects.

## Progress
- [x] No C# or C++ projects for startup
- [x] Load `XamlControlsResources` from `Microsoft.UI.Xaml.Controls` automatically
- [x] Use a custom `Sub Main` - You can enable high DPI, add dynamic dependencies or add reg-free WinRT support by editing `Program.vb`.
- [x] Allow Windows Forms integration, such as registering `HostServices.VBHost` at startup
- [x] Make sure Mica and Acrylic are working as expected on Windows 11
- [x] Workaround blocking bugs of the WinUI 3 XAML compiler - Generate Xbf files and VB source files
- [ ] Workaround trivial bugs of the WinUI 3 XAML compiler - Generate 99% correct VB source files
- [ ] Publish a NuGet package that contains VB specific build transitive files, so any VB projects will be able to use WinUI 3 without editing `*.vbproj` manually.
- [ ] Publish a VSIX to add VB WinUI 3 templates to Visual Studio.
- [ ] Write a new VB application framework for WinUI 3 - Allow users to delete `Program.vb` and use events to configure the App in the `Application` class
- [ ] Write a new VB "My extension" for WinUI 3 - Enable `My.*` for WinUI 3 specific things

## Environment
- Visual Studio 2022 with Windows Desktop and UWP development workloads
- Windows 10 21H2 or Windows 11

## How does it work

### Approach 1: Use the default WinUI VB XAML compiler
The MSBuild target files of WinUI 3 need some hacking to support Visual Basic.
VB projects need to import the following target file to use WinUI 3.

**WinUI3.VisualBasic.targets**
```xml
<Project>

  <PropertyGroup>
    <!-- Workaround for conflicts with WPF -->
    <ImportFrameworkWinFXTargets>true</ImportFrameworkWinFXTargets>
    <!-- Workaround for wrong entry point code generated by WinUI 3 -->
    <DefineConstants>DISABLE_XAML_GENERATED_MAIN</DefineConstants>
  </PropertyGroup>

  <!-- Workaround for XamlPreCompile compilation errors -->
  <Target Name="SpecifyVBRuntimeForXamlPreCompile" BeforeTargets="XamlPreCompile">
    <PropertyGroup>
      <VBRuntime>None</VBRuntime>
    </PropertyGroup>
  </Target>
  
</Project>
```

### Approach 2: Use the WinUI C# XAML compiler and code converter
WIP. See [XamlLanguageConverter](#XamlLanguageConverter).

## Samples
### BatchFfmpegWinUI
A `mp4` video to `h265` transcoder.

It converts `.mp4` and `.mkv` videos to `h265` encoding by calling `H265.bat`.

#### Status
Finished. Tested with WinUI3 `v1.1.1` and .NET SDK v6.0.3.

#### How to use it
- Install `ffmpeg` to `%programfiles%\ffmpeg\bin`. If you have installed it in a different location, please add it to `%PATH%` or edit the path to `ffmpeg` in [H265.bat](BatchFfmpegWinUI_Pack/BatchFfmpegWinUI/H265.bat).
- Open `BatchFfmpegWinUI.sln` with Visual Studio.
- Set `BatchFfmpegWinUI_Pack` as startup project and run.
- Drag and drop `.mp4` or `.mkv` files.
- Press the "Convert" button and wait.

### XamlLanguageConverter
Demonstrates how to use MSBuild extension points to invoke the C# XAML compiler and start a [code converter](https://github.com/Nukepayload2/CSharpToVB-Backports) to convert the C# output to VB.

#### Status
WIP

### WinUIGallery
A VB version of WinUIGallery `v1.1`. It uses the C# XAML converter until the official VB WinUI3 XAML converter works correctly.

#### Status
WIP, depends on `XamlLanguageConverter`.
