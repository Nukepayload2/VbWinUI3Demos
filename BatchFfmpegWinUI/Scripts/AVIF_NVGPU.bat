@echo off
set FFMPEG_HOME=%programfiles%\ffmpeg\bin
set PATH=%PATH%;%FFMPEG_HOME%

ffmpeg -i "%~1" -c:v av1_nvenc -qp 18 -still-picture 1 "%~2"
