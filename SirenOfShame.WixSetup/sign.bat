--usage sign.bat [pfxcert] [password] [filename]
"c:\Program Files (x86)\Windows Kits\8.1\bin\x64\signtool.exe" sign /f %1 /p %2 /d %3 %3