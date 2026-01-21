@echo off
REM ═══════════════════════════════════════════════════════════════════════════════
REM  Installation Automatique des Prerequis pour RH Plus Gestion
REM  Ce script telecharge et installe NuGet et Inno Setup
REM ═══════════════════════════════════════════════════════════════════════════════

echo.
echo ═══════════════════════════════════════════════════════════════════════════════
echo   RH PLUS GESTION - Installation des Prerequis
echo ═══════════════════════════════════════════════════════════════════════════════
echo.

REM Vérifier les droits administrateur
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo [ERREUR] Ce script necessite des droits administrateur !
    echo.
    echo Faites un clic droit sur le fichier et choisissez "Executer en tant qu'administrateur"
    echo.
    pause
    exit /b 1
)

echo [INFO] Droits administrateur confirmes
echo.

REM Créer un dossier temporaire
set TEMP_DIR=%TEMP%\RHPlusSetup
if not exist "%TEMP_DIR%" mkdir "%TEMP_DIR%"
cd /d "%TEMP_DIR%"

echo ═══════════════════════════════════════════════════════════════════════════════
echo   Etape 1/2 - Installation de NuGet CLI
echo ═══════════════════════════════════════════════════════════════════════════════
echo.

REM Vérifier si NuGet est déjà installé
where nuget >nul 2>&1
if %errorLevel% equ 0 (
    echo [OK] NuGet est deja installe !
    nuget | find "NuGet Version"
) else (
    echo [INFO] Telechargement de NuGet.exe...

    REM Télécharger NuGet
    powershell -Command "& {[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; Invoke-WebRequest -Uri 'https://dist.nuget.org/win-x86-commandline/latest/nuget.exe' -OutFile 'nuget.exe'}"

    if not exist "nuget.exe" (
        echo [ERREUR] Echec du telechargement de NuGet
        echo.
        echo Telechargez manuellement depuis : https://www.nuget.org/downloads
        pause
        exit /b 1
    )

    echo [INFO] Installation de NuGet dans System32...
    copy /Y "nuget.exe" "C:\Windows\System32\" >nul

    if exist "C:\Windows\System32\nuget.exe" (
        echo [OK] NuGet installe avec succes !
        C:\Windows\System32\nuget.exe | find "NuGet Version"
    ) else (
        echo [ERREUR] Impossible de copier NuGet dans System32
        pause
        exit /b 1
    )
)

echo.
echo ═══════════════════════════════════════════════════════════════════════════════
echo   Etape 2/2 - Installation de Inno Setup 6
echo ═══════════════════════════════════════════════════════════════════════════════
echo.

REM Vérifier si Inno Setup est déjà installé
if exist "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" (
    echo [OK] Inno Setup 6 est deja installe !
    "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" /? | find "Inno Setup"
) else (
    echo [INFO] Telechargement de Inno Setup 6...
    echo.
    echo ATTENTION : Une fenetre d'installation va s'ouvrir.
    echo Suivez les instructions et choisissez "Standard Installation"
    echo.
    pause

    REM Télécharger Inno Setup
    powershell -Command "& {[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; Invoke-WebRequest -Uri 'https://jrsoftware.org/download.php/is.exe' -OutFile 'innosetup_installer.exe'}"

    if not exist "innosetup_installer.exe" (
        echo [ERREUR] Echec du telechargement de Inno Setup
        echo.
        echo Telechargez manuellement depuis : https://jrsoftware.org/isdl.php
        echo Installez-le, puis relancez ce script
        pause
        exit /b 1
    )

    echo [INFO] Lancement de l'installation d'Inno Setup...
    echo Veuillez suivre l'assistant d'installation
    echo.

    REM Lancer l'installation
    start /wait innosetup_installer.exe

    REM Vérifier l'installation
    if exist "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" (
        echo.
        echo [OK] Inno Setup 6 installe avec succes !
    ) else (
        echo.
        echo [AVERTISSEMENT] Inno Setup ne semble pas etre installe
        echo.
        echo Si vous avez annule l'installation, veuillez :
        echo 1. Telecharger Inno Setup : https://jrsoftware.org/isdl.php
        echo 2. L'installer manuellement
        echo 3. Relancer ce script
        pause
    )
)

echo.
echo ═══════════════════════════════════════════════════════════════════════════════
echo   Verification Finale
echo ═══════════════════════════════════════════════════════════════════════════════
echo.

set ALL_OK=1

REM Vérifier NuGet
where nuget >nul 2>&1
if %errorLevel% equ 0 (
    echo [OK] NuGet : Installe et accessible
) else (
    echo [X] NuGet : NON trouve
    set ALL_OK=0
)

REM Vérifier Inno Setup
if exist "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" (
    echo [OK] Inno Setup : Installe
) else (
    echo [X] Inno Setup : NON trouve
    set ALL_OK=0
)

REM Vérifier MSBuild (Visual Studio)
set MSBUILD_PATH=C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe
if not exist "%MSBUILD_PATH%" (
    set MSBUILD_PATH=C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe
)
if not exist "%MSBUILD_PATH%" (
    set MSBUILD_PATH=C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe
)

if exist "%MSBUILD_PATH%" (
    echo [OK] MSBuild : Trouve
) else (
    echo [!] MSBuild : NON trouve
    echo.
    echo MSBuild est necessaire pour compiler l'application.
    echo.
    echo Si vous n'avez pas Visual Studio, installez Build Tools :
    echo https://visualstudio.microsoft.com/downloads/
    echo Choisissez "Build Tools for Visual Studio 2022"
    echo.
    set ALL_OK=0
)

echo.
if %ALL_OK% equ 1 (
    echo ═══════════════════════════════════════════════════════════════════════════════
    echo   INSTALLATION TERMINEE AVEC SUCCES !
    echo ═══════════════════════════════════════════════════════════════════════════════
    echo.
    echo Tous les prerequis sont installes.
    echo.
    echo Prochaine etape :
    echo   Executez BUILD_INSTALLER.bat pour creer l'installateur
    echo.
) else (
    echo ═══════════════════════════════════════════════════════════════════════════════
    echo   INSTALLATION INCOMPLETE
    echo ═══════════════════════════════════════════════════════════════════════════════
    echo.
    echo Certains prerequis sont manquants (voir ci-dessus)
    echo.
    echo Veuillez les installer manuellement, puis relancer ce script pour verification
    echo.
)

REM Nettoyer les fichiers temporaires
cd /d "%USERPROFILE%"
if exist "%TEMP_DIR%" rmdir /s /q "%TEMP_DIR%"

echo ═══════════════════════════════════════════════════════════════════════════════
echo.
pause
