# ========================================================================
# RH Plus Gestion - Script de Build PowerShell
# ========================================================================

Write-Host ""
Write-Host "========================================================================"  -ForegroundColor Cyan
Write-Host "  GESTION MODERNE RH - Build de l'Installateur v1.0"  -ForegroundColor Cyan
Write-Host "========================================================================" -ForegroundColor Cyan
Write-Host ""

# Aller dans le dossier du script
Set-Location $PSScriptRoot
Write-Host "Dossier de travail : $PSScriptRoot" -ForegroundColor Yellow
Write-Host ""

# ========================================================================
# ETAPE 1 : Verification des outils
# ========================================================================
Write-Host "ETAPE 1 : Verification des outils" -ForegroundColor Green
Write-Host "------------------------------------------------------------------------"
Write-Host ""

$errors = 0

# Chercher MSBuild
Write-Host "Recherche de MSBuild..." -ForegroundColor Yellow
$msbuildPaths = @(
    "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\MSBuild.exe"
)

$msbuild = $null
foreach ($path in $msbuildPaths) {
    if (Test-Path $path) {
        $msbuild = $path
        Write-Host "[OK] MSBuild trouve : $path" -ForegroundColor Green
        break
    }
}

if (-not $msbuild) {
    Write-Host "[ERREUR] MSBuild non trouve !" -ForegroundColor Red
    Write-Host ""
    Write-Host "Veuillez installer Visual Studio ou Build Tools :" -ForegroundColor Yellow
    Write-Host "https://visualstudio.microsoft.com/downloads/" -ForegroundColor Cyan
    Write-Host ""
    $errors++
}
Write-Host ""

# Chercher ou telecharger NuGet
Write-Host "Recherche de NuGet..." -ForegroundColor Yellow
$nuget = Get-Command nuget.exe -ErrorAction SilentlyContinue

if (-not $nuget) {
    Write-Host "[INFO] NuGet non trouve dans PATH" -ForegroundColor Yellow

    if (Test-Path ".\nuget.exe") {
        Write-Host "[OK] NuGet.exe trouve localement" -ForegroundColor Green
        $nuget = ".\nuget.exe"
    } else {
        Write-Host "Telechargement de NuGet..." -ForegroundColor Yellow
        try {
            [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
            Invoke-WebRequest -Uri "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" -OutFile "nuget.exe"
            Write-Host "[OK] NuGet telecharge avec succes" -ForegroundColor Green
            $nuget = ".\nuget.exe"
        } catch {
            Write-Host "[ERREUR] Impossible de telecharger NuGet : $_" -ForegroundColor Red
            Write-Host "Telechargez manuellement : https://www.nuget.org/downloads" -ForegroundColor Yellow
            $errors++
        }
    }
} else {
    Write-Host "[OK] NuGet trouve : $($nuget.Source)" -ForegroundColor Green
    $nuget = "nuget.exe"
}
Write-Host ""

# Chercher Inno Setup
Write-Host "Recherche d'Inno Setup..." -ForegroundColor Yellow
$isccPaths = @(
    "C:\Program Files (x86)\Inno Setup 6\ISCC.exe",
    "C:\Program Files\Inno Setup 6\ISCC.exe"
)

$iscc = $null
foreach ($path in $isccPaths) {
    if (Test-Path $path) {
        $iscc = $path
        Write-Host "[OK] Inno Setup trouve : $path" -ForegroundColor Green
        break
    }
}

if (-not $iscc) {
    Write-Host "[AVERTISSEMENT] Inno Setup non trouve" -ForegroundColor Yellow
    Write-Host "L'application sera compilee mais le setup ne sera pas cree" -ForegroundColor Yellow
    Write-Host "Telechargez Inno Setup : https://jrsoftware.org/isdl.php" -ForegroundColor Cyan
}
Write-Host ""

if ($errors -gt 0) {
    Write-Host "========================================================================"  -ForegroundColor Red
    Write-Host "  Des outils requis sont manquants !" -ForegroundColor Red
    Write-Host "========================================================================" -ForegroundColor Red
    Write-Host ""
    Write-Host "Appuyez sur une touche pour quitter..."
    $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
    exit 1
}

# ========================================================================
# ETAPE 2 : Restauration des packages NuGet
# ========================================================================
Write-Host "ETAPE 2 : Restauration des packages NuGet" -ForegroundColor Green
Write-Host "------------------------------------------------------------------------"
Write-Host ""

if (Test-Path "packages") {
    Write-Host "Nettoyage de l'ancien dossier packages..." -ForegroundColor Yellow
    Remove-Item "packages" -Recurse -Force -ErrorAction SilentlyContinue
}

Write-Host "Restauration des packages (peut prendre quelques minutes)..." -ForegroundColor Yellow
& $nuget restore RH_GRH.sln -NonInteractive

if ($LASTEXITCODE -ne 0) {
    Write-Host "[ERREUR] Echec de la restauration des packages" -ForegroundColor Red
    Write-Host "Appuyez sur une touche pour quitter..."
    $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
    exit 1
}

Write-Host "[OK] Packages restaures avec succes" -ForegroundColor Green
Write-Host ""

# ========================================================================
# ETAPE 3 : Nettoyage des builds precedents
# ========================================================================
Write-Host "ETAPE 3 : Nettoyage des builds precedents" -ForegroundColor Green
Write-Host "------------------------------------------------------------------------"
Write-Host ""

if (Test-Path "RH_GRH\bin\Release") {
    Write-Host "Suppression de RH_GRH\bin\Release..." -ForegroundColor Yellow
    Remove-Item "RH_GRH\bin\Release" -Recurse -Force -ErrorAction SilentlyContinue
}

if (Test-Path "RH_GRH\obj\Release") {
    Write-Host "Suppression de RH_GRH\obj\Release..." -ForegroundColor Yellow
    Remove-Item "RH_GRH\obj\Release" -Recurse -Force -ErrorAction SilentlyContinue
}

Write-Host "[OK] Nettoyage termine" -ForegroundColor Green
Write-Host ""

# ========================================================================
# ETAPE 4 : Compilation en mode Release
# ========================================================================
Write-Host "ETAPE 4 : Compilation en mode Release" -ForegroundColor Green
Write-Host "------------------------------------------------------------------------"
Write-Host ""

Write-Host "Compilation en cours (peut prendre quelques minutes)..." -ForegroundColor Yellow
Write-Host "Plateforme cible : x86 (requis par QuestPDF)" -ForegroundColor Cyan
Write-Host ""

& $msbuild RH_GRH.sln /p:Configuration=Release /p:Platform="Any CPU" /p:PlatformTarget=x86 /t:Rebuild /m /v:minimal /nologo

if ($LASTEXITCODE -ne 0) {
    Write-Host ""
    Write-Host "[ERREUR] Echec de la compilation" -ForegroundColor Red
    Write-Host "Appuyez sur une touche pour quitter..."
    $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
    exit 1
}

Write-Host ""
Write-Host "[OK] Compilation reussie" -ForegroundColor Green
Write-Host ""

# ========================================================================
# ETAPE 5 : Verification des fichiers compiles
# ========================================================================
Write-Host "ETAPE 5 : Verification des fichiers compiles" -ForegroundColor Green
Write-Host "------------------------------------------------------------------------"
Write-Host ""

if (-not (Test-Path "RH_GRH\bin\Release\RH_GRH.exe")) {
    Write-Host "[ERREUR] Le fichier RH_GRH.exe n'a pas ete trouve !" -ForegroundColor Red
    Write-Host "Appuyez sur une touche pour quitter..."
    $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
    exit 1
}

$exeFile = Get-Item "RH_GRH\bin\Release\RH_GRH.exe"
$exeSizeMB = [math]::Round($exeFile.Length / 1MB, 2)
Write-Host "[OK] RH_GRH.exe trouve - Taille: $exeSizeMB MB" -ForegroundColor Green

$dllCount = (Get-ChildItem "RH_GRH\bin\Release\*.dll" -ErrorAction SilentlyContinue).Count
Write-Host "[OK] $dllCount fichiers DLL trouves" -ForegroundColor Green
Write-Host ""

# ========================================================================
# ETAPE 6 : Creation de l'installateur
# ========================================================================
Write-Host "ETAPE 6 : Creation de l'installateur" -ForegroundColor Green
Write-Host "------------------------------------------------------------------------"
Write-Host ""

if (-not $iscc) {
    Write-Host "[INFO] Inno Setup non installe - installateur non cree" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "L'application est prete dans : RH_GRH\bin\Release\" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Pour creer l'installateur :" -ForegroundColor Yellow
    Write-Host "1. Installez Inno Setup : https://jrsoftware.org/isdl.php" -ForegroundColor Cyan
    Write-Host "2. Relancez ce script" -ForegroundColor Cyan
    Write-Host ""
    explorer "RH_GRH\bin\Release"
} else {
    if (-not (Test-Path "Setup\Output")) {
        New-Item -ItemType Directory -Path "Setup\Output" | Out-Null
    }

    Write-Host "Creation de l'installateur avec Inno Setup..." -ForegroundColor Yellow
    Write-Host ""

    & $iscc /Q setup.iss

    if ($LASTEXITCODE -ne 0) {
        Write-Host "[ERREUR] Echec de la creation de l'installateur" -ForegroundColor Red
        Write-Host "Appuyez sur une touche pour quitter..."
        $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
        exit 1
    }

    Write-Host "[OK] Installateur cree avec succes !" -ForegroundColor Green
    Write-Host ""

    if (Test-Path "Setup\Output\GestionModerneRH_v1.0.0_Setup.exe") {
        $setupFile = Get-Item "Setup\Output\GestionModerneRH_v1.0.0_Setup.exe"
        $setupSizeMB = [math]::Round($setupFile.Length / 1MB, 2)
        Write-Host "Fichier : GestionModerneRH_v1.0.0_Setup.exe" -ForegroundColor Cyan
        Write-Host "Taille : $setupSizeMB MB" -ForegroundColor Cyan
        Write-Host ""
    }

    Write-Host "========================================================================"  -ForegroundColor Green
    Write-Host "  BUILD TERMINE AVEC SUCCES !" -ForegroundColor Green
    Write-Host "========================================================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "L'installateur est pret :" -ForegroundColor Cyan
    Write-Host "  Setup\Output\GestionModerneRH_v1.0.0_Setup.exe" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Ouverture du dossier..." -ForegroundColor Yellow
    explorer "Setup\Output"
}

Write-Host ""
Write-Host "========================================================================"  -ForegroundColor Cyan
Write-Host "Appuyez sur une touche pour quitter..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
