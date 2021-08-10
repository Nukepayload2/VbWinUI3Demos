@echo off
set FFMPEG_HOME=%programfiles%\ffmpeg\bin
set PATH=%PATH%;%FFMPEG_HOME%
ffmpeg -i "%~1" -vcodec hevc_nvenc "%~dpn1_h265%~x1"
