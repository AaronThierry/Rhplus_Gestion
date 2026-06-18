$msbuild = "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\amd64\MSBuild.exe"
$project = "RH_GRH\RH_GRH.csproj"

Write-Host "========================================"
Write-Host "BUILD GESTION MODERNE RH v1.1.7"
Write-Host "========================================"
Write-Host ""

Write-Host "Nettoyage du projet..."
& $msbuild $project /t:Clean /p:Configuration=Release /v:minimal /nologo

if ($LASTEXITCODE -ne 0) {
    Write-Host "ERREUR lors du nettoyage!"
    exit 1
}

Write-Host ""
Write-Host "Compilation en mode Release..."
& $msbuild $project /t:Rebuild /p:Configuration=Release /v:minimal /nologo

if ($LASTEXITCODE -ne 0) {
    Write-Host "ERREUR lors de la compilation!"
    exit 1
}

Write-Host ""
Write-Host "========================================"
Write-Host "BUILD TERMINÉ AVEC SUCCÈS !"
Write-Host "========================================"
Write-Host "Fichier exe généré : RH_GRH\bin\Release\RH_GRH.exe"
Write-Host ""
