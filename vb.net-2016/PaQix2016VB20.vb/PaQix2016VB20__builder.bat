@set dir=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\
@set ProjName=PaQix2016VB20

@echo %dir%
@echo.

@%dir%\vbc /target:winexe /resource:res\%ProjName%.resources /win32icon:res\%ProjName%.ico /optimize %ProjName%.vb

@pause
