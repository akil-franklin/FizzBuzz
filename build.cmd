@echo off
setlocal

REM Get Current Path and Ensure that we are running from the expected location ----------------------------
set BUILD_DIR=%CD%


REM Get MSBuild Path --------------------------------------------------------

set msbuild.exe=
for /D %%D in (%SYSTEMROOT%\Microsoft.NET\Framework\v*) do set msbuild.exe=%%D\MSBuild.exe

if not defined msbuild.exe echo error: cannot find MSBuild.exe & goto :eof
if not exist "%msbuild.exe%" echo error: %msbuild.exe%: not found & goto :eof


REM Build the DLL ------------------------------------------------------------

echo DEBUG Build...

"%msbuild.exe%" "%BUILD_DIR%\FizzBuzz\FizzBuzz.csproj" /property:Configuration=Debug
"%msbuild.exe%" "%BUILD_DIR%\FizzBuzzConsole\FizzBuzzConsole.csproj" /property:Configuration=Debug

echo RELEASE Build...

"%msbuild.exe%" "%BUILD_DIR%\FizzBuzz\FizzBuzz.csproj" /property:Configuration=Release

echo Copying targets to dist Folder...
robocopy /S "%BUILD_DIR%\FizzBuzz\bin" "%BUILD_DIR%\dist"


REM Run Unit Tests ------------------------------------------------------------
.\test


REM Cleanup ------------------------------------------------------------
:eof
endlocal
