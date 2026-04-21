@echo off
echo ====================================
echo   FORCER RECHARGEMENT VISUAL STUDIO
echo ====================================
echo.
echo Ce script va forcer Visual Studio a recharger le projet.
echo.
echo Etape 1: Fermeture de Visual Studio...
taskkill /F /IM devenv.exe 2>nul
if %errorlevel% == 0 (
    echo Visual Studio ferme avec succes.
) else (
    echo Visual Studio n'etait pas ouvert ou deja ferme.
)
echo.

echo Etape 2: Nettoyage des fichiers cache...
cd /d "%~dp0"
if exist "RH_GRH\bin" rmdir /s /q "RH_GRH\bin"
if exist "RH_GRH\obj" rmdir /s /q "RH_GRH\obj"
if exist ".vs" rmdir /s /q ".vs"
echo Cache nettoye.
echo.

echo Etape 3: Reconstruction de la solution...
"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" RH_GRH.sln -t:Rebuild -p:Configuration=Debug -v:minimal
if %errorlevel% == 0 (
    echo.
    echo ====================================
    echo   SUCCES !
    echo ====================================
    echo.
    echo La solution a ete reconstruite avec succes.
    echo Vous pouvez maintenant rouvrir Visual Studio.
    echo.
    echo Double-cliquez sur RH_GRH.sln pour ouvrir le projet.
    echo.
) else (
    echo.
    echo ====================================
    echo   ERREUR DE COMPILATION
    echo ====================================
    echo.
    echo Des erreurs sont survenues pendant la compilation.
    echo Consultez les messages ci-dessus pour plus de details.
    echo.
)

pause
