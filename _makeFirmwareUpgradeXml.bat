@echo off
set VERSION=%1
set HEXFILE=%2
set OUTDIR=%3

echo ^<?xml version="1.0"?^> > %OUTDIR%FirmwareUpgrade.xml
echo ^<firmwareUpgrade^> >> %OUTDIR%FirmwareUpgrade.xml
echo   ^<version^>%VERSION%^</version^> >> %OUTDIR%FirmwareUpgrade.xml
echo   ^<date^>%~t2^</date^> >> %OUTDIR%FirmwareUpgrade.xml
echo   ^<hex^> >> %OUTDIR%FirmwareUpgrade.xml
type %HEXFILE% >> %OUTDIR%FirmwareUpgrade.xml
echo   ^</hex^> >> %OUTDIR%FirmwareUpgrade.xml
echo ^</firmwareUpgrade^> >> %OUTDIR%FirmwareUpgrade.xml