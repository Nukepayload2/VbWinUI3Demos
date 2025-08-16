@echo off
set FFMPEG_HOME=%programfiles%\ffmpeg\bin
set PATH=%PATH%;%FFMPEG_HOME%

REM Add " -qp 18" after "av1_nvenc" to enter lossless mode.
ffmpeg -i "%~1" -c:a aac -c:v libsvtav1 -rc 0 -qp 32 -preset 5 "%~2"
