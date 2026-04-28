# Quick Start - Création d'une nouvelle release

**Guide rapide pour créer une release** (voir `GUIDE_RELEASE_PROCESS.md` pour version complète)

---

## 🚀 En 5 minutes

### 1. Préparer la nouvelle version

```bash
# Exemple: passer de 1.1.8 à 1.1.9
VERSION_OLD="1.1.8"
VERSION_NEW="1.1.9"
```

### 2. Mettre à jour les fichiers (3 fichiers essentiels)

**A. AssemblyInfo.cs**
```csharp
// RH_GRH\Properties\AssemblyInfo.cs (lignes 33-34)
[assembly: AssemblyVersion("1.1.9.0")]
[assembly: AssemblyFileVersion("1.1.9.0")]
```

**B. LoginFormModern.Designer.cs**
```csharp
// RH_GRH\LoginFormModern.Designer.cs (ligne ~306)
this.labelVersion.Text = "Version 1.1.9";
```

**C. setup-update.iss**
```ini
; setup-update.iss (lignes 1-2, 5, 26, 40)
; Script d'installation Inno Setup pour Gestion Moderne RH v1.1.9 - UPDATE
; Mise à jour : [VOTRE DESCRIPTION]

#define MyAppVersion "1.1.9"

InfoBeforeFile=UPDATE_NOTES_v1.1.9.txt

VersionInfoDescription=Système de Gestion des Ressources Humaines et Paie - Mise à jour v1.1.9
```

### 3. Créer les notes de version

**A. UPDATE_NOTES_v1.1.9.txt**
```text
===============================================================================
    GESTION MODERNE RH - MISE À JOUR v1.1.9
    Date de sortie : [DATE]
===============================================================================

NOUVEAUTÉS ET AMÉLIORATIONS
-------------------------------
[Vos changements ici]

CORRECTIONS CRITIQUES
----------------------
[Vos corrections ici]

...
```

**B. RESUME_v1.1.9.md** (optionnel mais recommandé)

### 4. Build et Package

```powershell
# Build
powershell -ExecutionPolicy Bypass -Command "
$msbuild = 'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\amd64\MSBuild.exe'
& $msbuild RH_GRH\RH_GRH.csproj /t:Clean /p:Configuration=Release /v:minimal /nologo
& $msbuild RH_GRH\RH_GRH.csproj /t:Rebuild /p:Configuration=Release /v:minimal /nologo
"

# Package (attendre 3 secondes)
powershell -ExecutionPolicy Bypass -Command "
Start-Sleep -Seconds 3
& 'C:\Program Files (x86)\Inno Setup 6\ISCC.exe' setup-update.iss
"
```

### 5. Commit et Push

```bash
# Commit 1: Code
git add [vos fichiers de code modifiés]
git commit -m "Release v1.1.9 - [Description courte]"

# Commit 2: Version
git add RH_GRH/Properties/AssemblyInfo.cs RH_GRH/LoginFormModern.Designer.cs
git commit -m "Update version to 1.1.9 in AssemblyInfo and LoginForm"

# Commit 3: Config
git add setup-update.iss UPDATE_NOTES_v1.1.9.txt RESUME_v1.1.9.md
git commit -m "Configuration v1.1.9 - Package d'installation"

# Push
git push
```

---

## ✅ Checklist Ultra-Rapide

- [ ] Code modifié et testé
- [ ] 3 fichiers mis à jour (AssemblyInfo, LoginForm, setup-update.iss)
- [ ] UPDATE_NOTES créé
- [ ] Build Release OK
- [ ] Package généré (~67 MB)
- [ ] Installer et tester sur machine propre
- [ ] Version affichée = 1.1.9 partout
- [ ] 3 commits + push Git
- [ ] Distribuer aux utilisateurs

---

## 📁 Fichiers à modifier (minimum)

| Fichier | Ligne | Changement |
|---------|-------|------------|
| `RH_GRH\Properties\AssemblyInfo.cs` | 33-34 | Version 1.1.9.0 |
| `RH_GRH\LoginFormModern.Designer.cs` | ~306 | "Version 1.1.9" |
| `setup-update.iss` | 1-2 | Commentaire v1.1.9 |
| `setup-update.iss` | 5 | MyAppVersion "1.1.9" |
| `setup-update.iss` | 26 | UPDATE_NOTES_v1.1.9.txt |
| `setup-update.iss` | 40 | ...v1.1.9 |
| **NOUVEAU** `UPDATE_NOTES_v1.1.9.txt` | - | Notes de version |

---

## 🐛 Erreurs fréquentes

**"FontStyle.SemiBold doesn't exist"**
→ Utiliser `FontStyle.Bold`

**"Resource update error"**
→ Attendre 3 secondes avant ISCC.exe

**Package trop petit/grand**
→ Vérifier que Source = bin\Release

**Version ne s'affiche pas**
→ Rebuild après modification AssemblyInfo

---

## 📞 Aide

Problème ? Consultez `GUIDE_RELEASE_PROCESS.md` section "Troubleshooting"

---

**Dernière mise à jour :** 27 Avril 2026
