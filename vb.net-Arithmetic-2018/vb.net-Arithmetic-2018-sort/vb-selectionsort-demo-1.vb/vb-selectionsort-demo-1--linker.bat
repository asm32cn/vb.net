@echo off

set strLinker=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\vbc.exe
set strAppName=vb-selectionsort-demo-1
echo %strLinker%
echo.

set strCmd=%strLinker% /target:exe /optimize %strAppName%.vb

echo #%strCmd%
%strCmd%

pause