@echo off
set FFMPEG_HOME=%programfiles%\ffmpeg\bin
set PATH=%PATH%;%FFMPEG_HOME%

ffmpeg -i "%~1" -c:v libsvtav1 -qp 18 -still-picture 1 "%~2"
