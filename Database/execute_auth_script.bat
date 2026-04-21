@echo off
echo ========================================
echo Installation des tables d'authentification
echo ========================================
echo.
echo Serveur: 72.62.190.57
echo Base de donnees: rhplusCshrp
echo.

REM Chemin vers mysql.exe (à adapter selon votre installation)
set MYSQL_PATH="C:\Program Files\MySQL\MySQL Server 8.0\bin\mysql.exe"

REM Si MySQL n'est pas dans Program Files, essayer d'autres chemins communs
if not exist %MYSQL_PATH% set MYSQL_PATH="C:\xampp\mysql\bin\mysql.exe"
if not exist %MYSQL_PATH% set MYSQL_PATH="C:\wamp64\bin\mysql\mysql8.0.27\bin\mysql.exe"
if not exist %MYSQL_PATH% set MYSQL_PATH=mysql.exe

echo Execution du script SQL...
echo.

%MYSQL_PATH% -h 72.62.190.57 -P 3306 -u portail_user -pRoot@508050rh rhplusCshrp < create_auth_tables.sql

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ========================================
    echo Installation reussie!
    echo ========================================
    echo.
    echo Vous pouvez maintenant vous connecter avec:
    echo   Utilisateur: admin
    echo   Mot de passe: Admin@123
    echo.
) else (
    echo.
    echo ========================================
    echo ERREUR lors de l'installation
    echo ========================================
    echo.
    echo Verifiez que MySQL est installe et accessible.
    echo Ou utilisez MySQL Workbench pour executer le script manuellement.
    echo.
)

pause
