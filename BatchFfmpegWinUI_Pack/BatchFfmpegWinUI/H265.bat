@echo off
set FFMPEG_HOME=%programfiles%\ffmpeg\bin
set PATH=%PATH%;%FFMPEG_HOME%
ffmpeg -i "%~1" -vcodec hevc_nvenc "%~2" -f mp4
