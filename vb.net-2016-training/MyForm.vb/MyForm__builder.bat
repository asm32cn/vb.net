@set dir=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\
@set ProjName=MyForm

@echo %dir%
@echo.

@rem %dir%\vbc /target:winexe /win32icon:%ProjName%.ico /optimize %ProjName%.vb
@%dir%\vbc /target:winexe /optimize %ProjName%.vb

@pause
