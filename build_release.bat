@echo off
echo ========================================
echo BUILD GESTION MODERNE RH v1.1.8
echo ========================================
echo.

cd /d "%~dp0"

set MSBUILD="C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\amd64\MSBuild.exe"
set PROJECT=RH_GRH\RH_GRH.csproj

echo Nettoyage du projet...
%MSBUILD% %PROJECT% /t:Clean /p:Configuration=Release /v:minimal /nologo
if errorlevel 1 goto error

echo.
echo Compilation en mode Release...
%MSBUILD% %PROJECT% /t:Rebuild /p:Configuration=Release /v:minimal /nologo
if errorlevel 1 goto error

echo.
echo ========================================
echo BUILD TERMINÉ AVEC SUCCÈS !
echo ========================================
echo Fichier exe généré : RH_GRH\bin\Release\RH_GRH.exe
echo.
goto end

:error
echo.
echo ========================================
echo ERREUR DURANT LE BUILD !
echo ========================================
pause
exit /b 1

:end
pause
