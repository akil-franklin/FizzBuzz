@echo off
setlocal

set FizzBuzzConsole.exe=.\FizzBuzzConsole\bin\Debug\FizzBuzzConsole.exe
if not exist %FizzBuzzConsole.exe% echo error: %FizzBuzzConsole.exe%: not found. Do you need to run the build command? & goto :eof

%FizzBuzzConsole.exe% %1


REM Cleanup ------------------------------------------------------------
:eof
endlocal
