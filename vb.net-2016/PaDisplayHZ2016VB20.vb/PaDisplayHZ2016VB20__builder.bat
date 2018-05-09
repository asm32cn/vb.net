@set dir=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\
@set ProjName=PaDisplayHZ2016VB20

@echo %dir%
@echo.

@%dir%\vbc /target:winexe /win32icon:res\%ProjName%.ico /resource:res\%ProjName%.resources /optimize %ProjName%.vb

@pause
