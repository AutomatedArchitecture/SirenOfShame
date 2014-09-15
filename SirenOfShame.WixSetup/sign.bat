@echo off
:: usage siren.bat [filename]

if "%1" == "" (
  echo usage: siren.bat [filename]
  goto :end
)

rename SirenOfShame.WixSetup.msi %1
"c:\Program Files (x86)\Windows Kits\8.1\bin\x64\signtool.exe" sign /f "C:\Temp\Cert.pfx" /p password /d %1 %1

:end