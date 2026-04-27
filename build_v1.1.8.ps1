Write-Host "========================================" -ForegroundColor Cyan
Write-Host "BUILD GESTION MODERNE RH v1.1.8" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$msbuild = "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\amd64\MSBuild.exe"
$project = "RH_GRH\RH_GRH.csproj"

if (-not (Test-Path $msbuild)) {
    Write-Host "ERREUR: MSBuild introuvable!" -ForegroundColor Red
    exit 1
}

Write-Host "Nettoyage du projet..." -ForegroundColor Yellow
& $msbuild $project /t:Clean /p:Configuration=Release /v:minimal /nologo
if ($LASTEXITCODE -ne 0) {
    Write-Host ""
    Write-Host "ERREUR durant le nettoyage!" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Compilation en mode Release..." -ForegroundColor Yellow
& $msbuild $project /t:Rebuild /p:Configuration=Release /v:minimal /nologo
if ($LASTEXITCODE -ne 0) {
    Write-Host ""
    Write-Host "ERREUR durant la compilation!" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Green
Write-Host "BUILD TERMINÉ AVEC SUCCÈS!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host "Fichier exe: RH_GRH\bin\Release\RH_GRH.exe" -ForegroundColor White
Write-Host ""
