@echo off
set FFMPEG_HOME=%programfiles%\ffmpeg\bin
set PATH=%PATH%;%FFMPEG_HOME%

ffmpeg -i "%~1" -c:a copy -c:v hevc_nvenc -profile:v main -preset slow -rc-lookahead 40 -qp 18 -bf 4 -b_ref_mode 2 "%~2"
