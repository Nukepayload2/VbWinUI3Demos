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

## Samples
### BatchFfmpegWinUI
A `mp4` video to `h265` transcoder.

It converts `.mp4` and `.mkv` videos to `h265` encoding by calling `H265.bat`.

#### How to use it
- Install `ffmpeg` to `%programfiles%\ffmpeg\bin`. If you have installed it in a different location, please add it to `%PATH%` or edit the path to `ffmpeg` in [H265.bat](BatchFfmpegWinUI_Pack/BatchFfmpegWinUI/H265.bat).
- Open `BatchFfmpegWinUI.sln` with Visual Studio.
- Set `BatchFfmpegWinUI_Pack` as startup project and run.
- Drag and drop `.mp4` or `.mkv` files.
- Press the "Convert" button and wait.
