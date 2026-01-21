@echo off
setlocal enabledelayedexpansion

echo.
echo ========================================================================
echo   RH PLUS GESTION - Build Simplifie
echo ========================================================================
echo.

REM Aller dans le bon dossier
cd /d "%~dp0"
echo Dossier actuel : %CD%
echo.

REM ========================================================================
echo ETAPE 1 : Verification des outils
echo ========================================================================
echo.

set ERRORS=0

REM Chercher MSBuild
echo Recherche de MSBuild...
set MSBUILD=
if exist "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" (
    set "MSBUILD=C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
    echo [OK] Trouve : Visual Studio 2022 Community
)
if exist "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe" (
    set "MSBUILD=C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe"
    echo [OK] Trouve : Visual Studio 2022 Professional
)
if exist "C:\Program Files\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" (
    set "MSBUILD=C:\Program Files\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"
    echo [OK] Trouve : Visual Studio 2019 Community
)
if exist "C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe" (
    set "MSBUILD=C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe"
    echo [OK] Trouve : Build Tools 2019
)

if "%MSBUILD%"=="" (
    echo [ERREUR] MSBuild non trouve !
    echo.
    echo Vous devez installer Visual Studio ou Build Tools :
    echo https://visualstudio.microsoft.com/downloads/
    echo.
    set ERRORS=1
) else (
    echo MSBuild : %MSBUILD%
)
echo.

REM Chercher NuGet
echo Recherche de NuGet...
set NUGET=nuget.exe
where nuget.exe >nul 2>&1
if errorlevel 1 (
    echo [AVERTISSEMENT] NuGet non trouve dans PATH
    echo Tentative de telechargement...

    REM Télécharger NuGet dans le dossier local
    powershell -Command "& {try { [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; Invoke-WebRequest -Uri 'https://dist.nuget.org/win-x86-commandline/latest/nuget.exe' -OutFile 'nuget.exe' -ErrorAction Stop; Write-Host 'NuGet telecharge avec succes' } catch { Write-Host 'Erreur de telechargement' }}"

    if exist "nuget.exe" (
        set "NUGET=%CD%\nuget.exe"
        echo [OK] NuGet telecharge localement
    ) else (
        echo [ERREUR] Impossible de telecharger NuGet
        echo Telechargez manuellement : https://www.nuget.org/downloads
        set ERRORS=1
    )
) else (
    echo [OK] NuGet trouve dans PATH
)
echo.

REM Chercher Inno Setup
echo Recherche d'Inno Setup...
set ISCC=
if exist "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" (
    set "ISCC=C:\Program Files (x86)\Inno Setup 6\ISCC.exe"
    echo [OK] Inno Setup 6 trouve
) else if exist "C:\Program Files\Inno Setup 6\ISCC.exe" (
    set "ISCC=C:\Program Files\Inno Setup 6\ISCC.exe"
    echo [OK] Inno Setup 6 trouve
) else (
    echo [AVERTISSEMENT] Inno Setup non trouve
    echo L'application sera compilee mais le setup ne sera pas cree
    echo Telechargez Inno Setup : https://jrsoftware.org/isdl.php
)
echo.

if %ERRORS% equ 1 (
    echo ========================================================================
    echo   Des outils requis sont manquants !
    echo ========================================================================
    echo.
    echo Veuillez installer les outils manquants et relancer ce script.
    echo.
    pause
    exit /b 1
)

REM ========================================================================
echo ETAPE 2 : Restauration des packages NuGet
echo ========================================================================
echo.

if exist "packages" (
    echo Nettoyage ancien dossier packages...
    rmdir /s /q "packages" 2>nul
)

echo Restauration des packages...
"%NUGET%" restore RH_GRH.sln -NonInteractive
if errorlevel 1 (
    echo [ERREUR] Echec de la restauration des packages
    pause
    exit /b 1
)
echo [OK] Packages restaures
echo.

REM ========================================================================
echo ETAPE 3 : Nettoyage des builds precedents
echo ========================================================================
echo.

if exist "RH_GRH\bin\Release" (
    echo Suppression de RH_GRH\bin\Release...
    rmdir /s /q "RH_GRH\bin\Release" 2>nul
)
if exist "RH_GRH\obj\Release" (
    echo Suppression de RH_GRH\obj\Release...
    rmdir /s /q "RH_GRH\obj\Release" 2>nul
)
echo [OK] Nettoyage termine
echo.

REM ========================================================================
echo ETAPE 4 : Compilation en mode Release
echo ========================================================================
echo.

echo Compilation en cours (peut prendre quelques minutes)...
echo.
"%MSBUILD%" RH_GRH.sln /p:Configuration=Release /p:Platform="Any CPU" /t:Rebuild /m /v:minimal /nologo
if errorlevel 1 (
    echo.
    echo [ERREUR] Echec de la compilation
    echo.
    pause
    exit /b 1
)
echo.
echo [OK] Compilation reussie
echo.

REM ========================================================================
echo ETAPE 5 : Verification des fichiers compiles
echo ========================================================================
echo.

if not exist "RH_GRH\bin\Release\RH_GRH.exe" (
    echo [ERREUR] Le fichier RH_GRH.exe n'a pas ete trouve !
    pause
    exit /b 1
)

echo [OK] RH_GRH.exe trouve
echo.

dir "RH_GRH\bin\Release\RH_GRH.exe" | find "RH_GRH.exe"
echo.

REM Compter les DLLs
set DLL_COUNT=0
for %%f in ("RH_GRH\bin\Release\*.dll") do set /a DLL_COUNT+=1
echo [OK] %DLL_COUNT% fichiers DLL trouves
echo.

REM ========================================================================
echo ETAPE 6 : Creation de l'installateur
echo ========================================================================
echo.

if "%ISCC%"=="" (
    echo [INFO] Inno Setup non installe - installateur non cree
    echo.
    echo L'application est prete dans : RH_GRH\bin\Release\
    echo.
    echo Pour creer l'installateur :
    echo 1. Installez Inno Setup : https://jrsoftware.org/isdl.php
    echo 2. Relancez ce script
    echo.
    explorer "RH_GRH\bin\Release"
    goto :end
)

if not exist "Setup\Output" mkdir "Setup\Output"

echo Creation de l'installateur avec Inno Setup...
echo.
"%ISCC%" /Q setup.iss
if errorlevel 1 (
    echo [ERREUR] Echec de la creation de l'installateur
    pause
    exit /b 1
)

echo [OK] Installateur cree avec succes !
echo.

if exist "Setup\Output\RHPlusGestion_v1.0.0_Setup.exe" (
    for %%f in ("Setup\Output\RHPlusGestion_v1.0.0_Setup.exe") do (
        set SIZE=%%~zf
        set /a SIZE_MB=!SIZE! / 1048576
        echo Fichier : RHPlusGestion_v1.0.0_Setup.exe
        echo Taille : !SIZE_MB! MB
    )
    echo.
)

REM ========================================================================
echo TERMINE !
echo ========================================================================
echo.
echo L'installateur est pret :
echo   Setup\Output\RHPlusGestion_v1.0.0_Setup.exe
echo.
echo Ouverture du dossier...
explorer "Setup\Output"

:end
echo.
echo ========================================================================
pause
