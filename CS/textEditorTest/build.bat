@echo off 
chcp 1252 
setlocal EnableDelayedExpansion 
tasklist | find /i "program.exe" && taskkill /im program.exe /F || echo process "program.exe" is not running. 
IF EXIST program.exe (del "%CD%"\program.exe) ELSE ( echo file unavailable ) 
timeout /t 1 
set "cur_dir=%CD%" 
set "fileList=" 
cd src 
for %%f in (*) do ( 
   if defined fileList ( 
       set "fileList=!fileList! "%CD%\%%~nxf"" 
   ) else ( 
       set "fileList="%CD%\%%~nxf"" 
) 
) 
cd C:\Windows\Microsoft.NET\Framework64\v4.0.30319 
csc -out:"%cur_dir%\program.exe" %fileList% 
cd %cur_dir% 
IF EXIST program.exe (start "program.exe" "program.exe") ELSE ( echo build failed ) 
exit 
