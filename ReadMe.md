# VbWinUI3Demos
Demos for WinUI 3 in Visual Basic

## Solutions
### BatchFfmpegWinUI
A `mp4` video to `h265` transcoder.

It converts `.mp4` and `.mkv` videos to `h265` encoding by calling `H265.bat`.

#### How to use it
- Install `ffmpeg` to `%programfiles%\ffmpeg\bin`. If you have installed it in a different location, please add it to `%PATH%` or edit the path to `ffmpeg` in [H265.bat](BatchFfmpegWinUI_Pack/BatchFfmpegWinUI/H265.bat).
- Open `BatchFfmpegWinUI.sln` with the latest Visual Studio 2019.
- Set `BatchFfmpegWinUI_Pack` as startup project and run.
- Drag and drop `.mp4` or `.mkv` files.
- Press the "Convert" button and wait.
