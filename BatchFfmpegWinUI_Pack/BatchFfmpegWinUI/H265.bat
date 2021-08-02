set FFMPEG_HOME=C:\Program Files\ffmpeg\bin
set PATH=%PATH%;%FFMPEG_HOME%
ffmpeg -i "%~1" -vcodec hevc_nvenc "%~dpn1_h265%~x1"
