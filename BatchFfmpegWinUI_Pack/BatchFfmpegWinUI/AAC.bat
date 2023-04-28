@echo off
set FFMPEG_HOME=%programfiles%\ffmpeg\bin
set PATH=%PATH%;%FFMPEG_HOME%
REM requires libfdk_aac
REM ffmpeg -i "%~1" -c:a libfdk_aac -profile:a aac_he_v2 -b:a 32k "%~2"
ffmpeg -i "%~1" -c:a aac -b:a 160k "%~2"
