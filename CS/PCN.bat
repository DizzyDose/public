@echo off
chcp 1252
cls
:beginning
set /p input="New Project Name: "
echo %input%
IF NOT EXIST %input% ( mkdir %input% ) ELSE (
echo directory %input% already exists
echo try again
goto beginning
)
cd %input%
mkdir src
mkdir res
cd src
echo: > main.cs
echo using System; > main.cs
echo: >> main.cs 
echo public class Program >> main.cs
echo { >> main.cs
echo 	public static void Main(string[] args) >> main.cs
echo 	{ >> main.cs
echo 		Console.WriteLine("Setup successful");>> main.cs
echo 		Console.ReadKey();>>main.cs
echo 	} >> main.cs
echo } >> main.cs


set "TnF=tasklist | find"
set "Pntk= /i "program.exe" && taskkill"
set "ImnE= /im program.exe /F || echo process "program.exe" is not running."
cd ../
echo: > build.bat
echo @echo off > build.bat
echo chcp 1252 >> build.bat
echo setlocal EnableDelayedExpansion >> build.bat
setlocal EnableDelayedExpansion
echo !TnF!!Pntk!!ImnE! >> build.bat
setlocal DisableDelayedExpansion
echo IF EXIST program.exe (del "%%CD%%"\program.exe) ELSE ( echo file unavailable ) >> build.bat
echo timeout /t 1 >> build.bat
echo set "cur_dir=%%CD%%" >> build.bat
echo set "fileList=" >> build.bat
echo cd src >> build.bat
echo for %%%%f in (*) do ( >> build.bat
echo    if defined fileList ( >> build.bat
echo        set "fileList=!fileList! "%%CD%%\%%%%~nxf"" >> build.bat
echo    ) else ( >> build.bat
echo        set "fileList="%%CD%%\%%%%~nxf"" >> build.bat
echo	) >> build.bat
echo ) >> build.bat
echo cd C:\Windows\Microsoft.NET\Framework64\v4.0.30319 >> build.bat
echo csc -out:"%%cur_dir%%\program.exe" %%fileList%% >> build.bat
echo cd %%cur_dir%% >> build.bat
echo IF EXIST program.exe (start "program.exe" "program.exe") ELSE ( >> build.bat
echo echo build failed >> build.bat
echo pause >> build.bat
echo ) >> build.bat
echo exit >> build.bat



timeout /t 1

start build.bat
cd src

start %SYSTEMROOT%\notepad.exe "%CD%\main.cs"
start "" "%CD%"
cd ..\
start "" "%CD%"