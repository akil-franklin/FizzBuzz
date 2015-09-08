@echo off
setlocal

REM Get Current Path  --------------------------------------------------------
set BUILD_DIR=%CD%


REM Get nunit-console.exe Path --------------------------------------------------------

set nunit-console.exe=
for /D %%D in ("%PROGRAMFILES(x86)%\NUnit 2*") do set nunit-console.exe=%%D\bin\nunit-console.exe

if not defined nunit-console.exe echo error: cannot find Nunit. Skipping unit tests. & goto :eof
if not exist "%nunit-console.exe%" echo error: %nunit-console.exe%: not found. Skipping unit tests & goto :eof

REM Get MSBuild Path --------------------------------------------------------

set msbuild.exe=
for /D %%D in (%SYSTEMROOT%\Microsoft.NET\Framework\v*) do set msbuild.exe=%%D\MSBuild.exe

if not defined msbuild.exe echo error: cannot find MSBuild.exe & goto :eof
if not exist "%msbuild.exe%" echo error: %msbuild.exe%: not found & goto :eof


REM Build the Test DLL ------------------------------------------------------------

echo Building Tests...

"%msbuild.exe%" "%BUILD_DIR%\FizzBuzzTests\FizzBuzzTests.csproj" /property:Configuration=Debug


REM Run Unit Tests --------------------------------------------------------

"%nunit-console.exe%" "%BUILD_DIR%\FizzBuzzTests\bin\Debug\FizzBuzzTests.dll"

REM Cleanup ------------------------------------------------------------
:eof
endlocal
