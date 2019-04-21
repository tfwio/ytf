#! cmd.exe /c
:: '#!' is not recognized as an internal or external command, operable program or batch file.
@echo off
set path1=%PROGRAMFILES(X86)%\msbuild\14.0\bin
set path2=%PROGRAMFILES(X86)%\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin
set PATH=%PATH%;%path2%
msbuild %~dp0..\.solution\youtube-dl-winforms.sln /m /t:YouTubeDownloadUtil "/p:Configuration=Debug;Platform=Any CPU"
