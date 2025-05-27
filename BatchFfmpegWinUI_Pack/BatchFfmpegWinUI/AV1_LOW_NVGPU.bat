@echo off
set FFMPEG_HOME=%programfiles%\ffmpeg\bin
set PATH=%PATH%;%FFMPEG_HOME%

REM Add " -qp 18" after "av1_nvenc" to enter lossless mode.
ffmpeg -i "%~1" -c:a libopus -b:a 64k -c:v av1_nvenc -preset p7 -rc-lookahead 40 -cq 42 -vf unsharp -bf 4 -b_ref_mode 2 "%~2"
