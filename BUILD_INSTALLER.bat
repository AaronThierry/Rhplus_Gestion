@echo off
REM ═══════════════════════════════════════════════════════════════════════════════
REM  Script de compilation pour RH Plus Gestion v1.0
REM  Ce script compile l'application et crée l'installateur
REM ═══════════════════════════════════════════════════════════════════════════════

echo.
echo ═══════════════════════════════════════════════════════════════════════════════
echo   RH PLUS GESTION - Build et Creation du Setup
echo ═══════════════════════════════════════════════════════════════════════════════
echo.

REM Vérifier si MSBuild est disponible
set MSBUILD_PATH=C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe
if not exist "%MSBUILD_PATH%" (
    set MSBUILD_PATH=C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe
)
if not exist "%MSBUILD_PATH%" (
    set MSBUILD_PATH=C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe
)

if not exist "%MSBUILD_PATH%" (
    echo [ERREUR] MSBuild n'a pas ete trouve !
    echo.
    echo Veuillez installer Visual Studio ou Build Tools pour Visual Studio
    echo URL: https://visualstudio.microsoft.com/downloads/
    pause
    exit /b 1
)

echo [1/5] Nettoyage des fichiers de build precedents...
echo.
if exist "RH_GRH\bin\Release" rmdir /s /q "RH_GRH\bin\Release"
if exist "RH_GRH\obj\Release" rmdir /s /q "RH_GRH\obj\Release"
if exist "Setup\Output" rmdir /s /q "Setup\Output"
echo      Nettoyage termine

echo.
echo [2/5] Restauration des packages NuGet...
echo.
nuget restore RH_GRH.sln
if errorlevel 1 (
    echo [ERREUR] Echec de la restauration des packages NuGet
    echo.
    echo Assurez-vous que NuGet est installe et accessible
    echo URL: https://www.nuget.org/downloads
    pause
    exit /b 1
)
echo      Restauration terminee

echo.
echo [3/5] Compilation en mode Release...
echo.
"%MSBUILD_PATH%" RH_GRH.sln /p:Configuration=Release /p:Platform="Any CPU" /t:Rebuild /m /v:minimal
if errorlevel 1 (
    echo [ERREUR] Echec de la compilation
    pause
    exit /b 1
)
echo      Compilation terminee avec succes

echo.
echo [4/5] Verification des fichiers compiles...
echo.
if not exist "RH_GRH\bin\Release\RH_GRH.exe" (
    echo [ERREUR] Le fichier executable n'a pas ete trouve !
    pause
    exit /b 1
)
echo      Fichiers de sortie verifies

echo.
echo [5/5] Creation de l'installateur avec Inno Setup...
echo.

REM Chercher Inno Setup
set INNO_PATH=C:\Program Files (x86)\Inno Setup 6\ISCC.exe
if not exist "%INNO_PATH%" (
    set INNO_PATH=C:\Program Files\Inno Setup 6\ISCC.exe
)

if not exist "%INNO_PATH%" (
    echo [AVERTISSEMENT] Inno Setup n'a pas ete trouve !
    echo.
    echo L'application a ete compilee avec succes, mais l'installateur n'a pas pu etre cree.
    echo.
    echo Pour creer l'installateur :
    echo 1. Installez Inno Setup 6 depuis : https://jrsoftware.org/isdl.php
    echo 2. Relancez ce script, ou
    echo 3. Ouvrez setup.iss avec Inno Setup et compilez manuellement
    echo.
    pause
    exit /b 0
)

REM Créer le dossier de sortie
if not exist "Setup\Output" mkdir "Setup\Output"

REM Créer les dossiers pour les assets (images du wizard)
if not exist "Setup\Assets" mkdir "Setup\Assets"

REM Compiler le setup
"%INNO_PATH%" setup.iss
if errorlevel 1 (
    echo [ERREUR] Echec de la creation de l'installateur
    pause
    exit /b 1
)

echo.
echo ═══════════════════════════════════════════════════════════════════════════════
echo   BUILD TERMINE AVEC SUCCES !
echo ═══════════════════════════════════════════════════════════════════════════════
echo.
echo Fichiers generes :
echo   - Application   : RH_GRH\bin\Release\RH_GRH.exe
echo   - Installateur  : Setup\Output\RHPlusGestion_v1.0.0_Setup.exe
echo.
echo Prochaines etapes :
echo   1. Testez l'installateur sur une machine de test
echo   2. Verifiez toutes les fonctionnalites
echo   3. Distribuez l'installateur aux utilisateurs
echo.
echo ═══════════════════════════════════════════════════════════════════════════════
echo.

REM Ouvrir le dossier de sortie
if exist "Setup\Output" explorer "Setup\Output"

pause
