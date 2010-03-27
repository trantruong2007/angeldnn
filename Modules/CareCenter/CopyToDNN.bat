@ECHO OFF
REM   xcopy reference:  http://www.microsoft.com/resources/documentation/windows/xp/all/proddocs/en-us/ntcmds.mspx?mfr=true
REM   Put this into your Visual Studio Build Event			CALL "$(ProjectDir)CopyToDNN.bat"

REM SET DnnDirectory=C:\PATH_TO_YOUR_LOCAL_DNN\
SET DnnDirectory=C:\Inetpub\wwwroot\DNN\
SET ModuleFolder=Angel-CareCenter

ECHO.
ECHO - Copying DLLs
xcopy /y /q "%~dp0bin\*.dll" "%DnnDirectory%bin\"

ECHO.
ECHO - Copying ASCX, ASPX, ASHX files
xcopy /s /y /q "%~dp0*.ascx" "%DnnDirectory%DesktopModules\%ModuleFolder%\"
xcopy /s /y /q "%~dp0*.aspx" "%DnnDirectory%DesktopModules\%ModuleFolder%\"
xcopy /s /y /q "%~dp0*.ashx" "%DnnDirectory%DesktopModules\%ModuleFolder%\"
xcopy /s /y /q "%~dp0Views\*.ascx" "%DnnDirectory%DesktopModules\%ModuleFolder%\Views\"

ECHO.
ECHO - Copying images
xcopy /s /y /q "%~dp0images\*.*" "%DnnDirectory%DesktopModules\%ModuleFolder%\images\"

ECHO.
ECHO - Copying CSS, JS files
xcopy /s /y /q "%~dp0*.css" "%DnnDirectory%DesktopModules\%ModuleFolder%\"
xcopy /s /y /q "%~dp0*.js" "%DnnDirectory%DesktopModules\%ModuleFolder%\"