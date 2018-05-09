@set dir=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\
@set ProjName=_CreateResource

@echo %dir%
@echo.

@%dir%\vbc /target:exe /optimize %ProjName%.vb

@pause
