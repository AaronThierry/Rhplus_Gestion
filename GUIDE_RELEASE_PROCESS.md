# Guide du Processus de Release - Gestion Moderne RH

**Date de création :** 27 Avril 2026
**Dernière mise à jour :** v1.1.8
**Auteur :** GMP - Gestion Moderne de Paie

---

## 📋 Table des matières

1. [Vue d'ensemble](#vue-densemble)
2. [Prérequis](#prérequis)
3. [Liste de vérification complète](#liste-de-vérification-complète)
4. [Processus étape par étape](#processus-étape-par-étape)
5. [Fichiers à mettre à jour](#fichiers-à-mettre-à-jour)
6. [Génération du package](#génération-du-package)
7. [Tests avant distribution](#tests-avant-distribution)
8. [Troubleshooting](#troubleshooting)

---

## Vue d'ensemble

Ce guide décrit le processus complet pour créer une nouvelle version (release) de **Gestion Moderne RH**, de la modification du code jusqu'à la distribution du package d'installation.

### Numérotation des versions

Format: `MAJEUR.MINEUR.PATCH`

- **MAJEUR** (1.x.x) : Changements incompatibles avec versions précédentes
- **MINEUR** (x.1.x) : Nouvelles fonctionnalités compatibles
- **PATCH** (x.x.1) : Corrections de bugs

**Exemples :**
- `1.1.6` → `1.1.7` : Correction de bugs ou petites améliorations
- `1.1.7` → `1.2.0` : Nouvelle fonctionnalité majeure
- `1.2.0` → `2.0.0` : Refonte complète ou changements incompatibles

---

## Prérequis

### Outils nécessaires

1. **Visual Studio 2022** (Community ou supérieur)
   - MSBuild.exe : `C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\amd64\MSBuild.exe`

2. **Inno Setup 6.4.3+**
   - ISCC.exe : `C:\Program Files (x86)\Inno Setup 6\ISCC.exe`

3. **Git** (pour le versioning)
   - Accès au dépôt : `https://github.com/AaronThierry/Rhplus_Gestion`

4. **PowerShell** (intégré à Windows)

### Permissions

- ✅ Droits administrateur sur la machine de build
- ✅ Accès push au dépôt GitHub
- ✅ Espace disque : minimum 500 MB libre

---

## Liste de vérification complète

### ☑️ Avant de commencer

- [ ] Toutes les modifications de code sont terminées et testées
- [ ] Aucune erreur de compilation
- [ ] Les tests manuels passent
- [ ] La base de données est compatible
- [ ] Décider du nouveau numéro de version (ex: 1.1.8 → 1.1.9)

### ☑️ Mises à jour des fichiers

- [ ] `RH_GRH\Properties\AssemblyInfo.cs` - AssemblyVersion et AssemblyFileVersion
- [ ] `RH_GRH\LoginFormModern.Designer.cs` - labelVersion.Text
- [ ] `setup-update.iss` - MyAppVersion
- [ ] `setup-update.iss` - Commentaire de description
- [ ] `setup-update.iss` - InfoBeforeFile
- [ ] `setup-update.iss` - VersionInfoDescription
- [ ] `build_release.bat` - Message d'en-tête (optionnel)
- [ ] Créer `UPDATE_NOTES_v{VERSION}.txt`
- [ ] Créer `RESUME_v{VERSION}.md`

### ☑️ Build et package

- [ ] Nettoyer la solution (Clean)
- [ ] Compiler en mode Release
- [ ] Vérifier qu'il n'y a pas d'erreurs
- [ ] Générer le package Inno Setup
- [ ] Vérifier la taille du package (≈ 67 MB)

### ☑️ Git

- [ ] Commit des modifications de code
- [ ] Commit des fichiers de configuration
- [ ] Push vers GitHub
- [ ] Tag de version (optionnel mais recommandé)

### ☑️ Tests finaux

- [ ] Installer le package sur une machine de test
- [ ] Vérifier le numéro de version affiché
- [ ] Tester les nouvelles fonctionnalités
- [ ] Vérifier qu'aucune régression n'a été introduite

---

## Processus étape par étape

### 📍 Étape 1 : Préparation

**1.1. Déterminer le numéro de version**

Décider de la nouvelle version en fonction des changements :

```
Version actuelle : 1.1.8
Changements : Correction de bugs mineurs
Nouvelle version : 1.1.9
```

**1.2. Créer une branche (optionnel mais recommandé)**

```bash
git checkout -b release-v1.1.9
```

---

### 📍 Étape 2 : Mise à jour du numéro de version

**2.1. AssemblyInfo.cs**

📁 Fichier : `RH_GRH\Properties\AssemblyInfo.cs`

```csharp
// AVANT
[assembly: AssemblyVersion("1.1.8.0")]
[assembly: AssemblyFileVersion("1.1.8.0")]

// APRÈS
[assembly: AssemblyVersion("1.1.9.0")]
[assembly: AssemblyFileVersion("1.1.9.0")]
```

**2.2. LoginFormModern.Designer.cs**

📁 Fichier : `RH_GRH\LoginFormModern.Designer.cs`

Chercher la ligne (environ ligne 306) :

```csharp
// AVANT
this.labelVersion.Text = "Version 1.1.8";

// APRÈS
this.labelVersion.Text = "Version 1.1.9";
```

> **Note :** Le `Formmain.cs` lit automatiquement la version depuis AssemblyInfo, pas besoin de le modifier.

---

### 📍 Étape 3 : Mise à jour du script Inno Setup

**3.1. Configuration de base**

📁 Fichier : `setup-update.iss`

```ini
; AVANT
; Script d'installation Inno Setup pour Gestion Moderne RH v1.1.8 - UPDATE
; Mise à jour : Améliorations UI saisie par lot et corrections adresse/téléphone

#define MyAppVersion "1.1.8"

; APRÈS
; Script d'installation Inno Setup pour Gestion Moderne RH v1.1.9 - UPDATE
; Mise à jour : [DESCRIPTION DE VOS CHANGEMENTS]

#define MyAppVersion "1.1.9"
```

**3.2. InfoBeforeFile**

```ini
; AVANT
InfoBeforeFile=UPDATE_NOTES_v1.1.8.txt

; APRÈS
InfoBeforeFile=UPDATE_NOTES_v1.1.9.txt
```

**3.3. VersionInfoDescription**

```ini
; AVANT
VersionInfoDescription=Système de Gestion des Ressources Humaines et Paie - Mise à jour v1.1.8

; APRÈS
VersionInfoDescription=Système de Gestion des Ressources Humaines et Paie - Mise à jour v1.1.9
```

---

### 📍 Étape 4 : Créer les notes de mise à jour

**4.1. UPDATE_NOTES_vX.X.X.txt**

📁 Fichier : `UPDATE_NOTES_v1.1.9.txt`

```text
===============================================================================
    GESTION MODERNE RH - MISE À JOUR v1.1.9
    Date de sortie : [DATE]
===============================================================================

NOUVEAUTÉS ET AMÉLIORATIONS
-------------------------------

1. [TITRE DE LA FONCTIONNALITÉ]
   ✓ [Description point 1]
   ✓ [Description point 2]
   ✓ [Description point 3]

CORRECTIONS CRITIQUES
----------------------

2. [TITRE DU BUG CORRIGÉ]
   ✓ [Description de la correction]
   ✓ [Impact de la correction]

DÉTAILS TECHNIQUES
------------------

Fichiers modifiés :
- [Liste des fichiers modifiés]

Améliorations :
- [Détails techniques si pertinent]

INSTALLATION
------------

Cette mise à jour :
✓ Préserve toutes vos données existantes
✓ Met à jour uniquement les fichiers nécessaires
✓ Compatible avec les versions 1.1.x
✓ Temps d'installation : ~2 minutes

PRÉREQUIS
---------

- Windows 7 SP1 ou supérieur (32-bit ou 64-bit)
- .NET Framework 4.7.2 ou supérieur
- Droits administrateur pour l'installation

SUPPORT
-------

Pour toute question ou problème :
- Email : secretariatrhplus@gmail.com
- GitHub : https://github.com/AaronThierry/Rhplus_Gestion/issues

===============================================================================
© 2026 GMP - Gestion Moderne de Paie. Tous droits réservés.
===============================================================================
```

**4.2. RESUME_vX.X.X.md**

📁 Fichier : `RESUME_v1.1.9.md`

Voir le template complet dans `RESUME_v1.1.8.md` comme référence.

Points importants à inclure :
- Date de sortie
- Résumé des changements
- Nouveautés détaillées
- Corrections de bugs
- Fichiers modifiés
- Instructions d'installation
- Statistiques (commits, lignes de code)

---

### 📍 Étape 5 : Mise à jour build_release.bat (optionnel)

📁 Fichier : `build_release.bat`

```batch
@echo off
echo ========================================
echo BUILD GESTION MODERNE RH v1.1.9
echo ========================================
```

---

### 📍 Étape 6 : Compilation du projet

**6.1. Option A : Utiliser le script PowerShell**

```powershell
# Créer un nouveau script ou utiliser build_v1.1.8.ps1 comme base
powershell.exe -ExecutionPolicy Bypass -File build_v1.1.9.ps1
```

**6.2. Option B : Utiliser MSBuild directement**

```powershell
$msbuild = "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\amd64\MSBuild.exe"
$project = "RH_GRH\RH_GRH.csproj"

# Nettoyage
& $msbuild $project /t:Clean /p:Configuration=Release /v:minimal /nologo

# Compilation
& $msbuild $project /t:Rebuild /p:Configuration=Release /v:minimal /nologo
```

**6.3. Vérifier la compilation**

✅ Vérifier qu'il n'y a **AUCUNE ERREUR** (les warnings sont OK)
✅ Vérifier que le fichier existe : `RH_GRH\bin\Release\RH_GRH.exe`
✅ Vérifier la version du fichier (clic droit > Propriétés > Détails)

---

### 📍 Étape 7 : Génération du package d'installation

**7.1. Générer avec Inno Setup**

```powershell
# Attendre quelques secondes pour éviter les conflits de fichiers
Start-Sleep -Seconds 3

# Compiler le script
& "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" setup-update.iss
```

**7.2. Vérifier le package**

✅ Fichier généré : `Setup\Output\GestionModerneRH_v1.1.9_Update.exe`
✅ Taille approximative : 67-70 MB
✅ Pas d'erreurs lors de la compilation Inno Setup

**7.3. Vérifier la version dans le package**

Clic droit sur le .exe > Propriétés > Détails :
- Version du produit : 1.1.9
- Version du fichier : 1.1.9

---

### 📍 Étape 8 : Commits Git

**8.1. Premier commit : Code et modifications**

```bash
# Ajouter les fichiers modifiés
git add RH_GRH/SaisiePayeLotForm.cs
git add RH_GRH/SaisiePayeLotForm.Designer.cs
# ... autres fichiers de code modifiés

# Commit
git commit -m "Release v1.1.9 - [Description courte des changements]

[Description détaillée]

Modifications principales:
- [Point 1]
- [Point 2]
- [Point 3]

Corrections:
- [Bug 1]
- [Bug 2]

Fichiers modifiés:
- [Liste des fichiers]

🤖 Generated with [Claude Code](https://claude.com/claude-code)

Co-Authored-By: Claude <noreply@anthropic.com>"
```

**8.2. Deuxième commit : Mise à jour de version**

```bash
# Ajouter les fichiers de version
git add RH_GRH/Properties/AssemblyInfo.cs
git add RH_GRH/LoginFormModern.Designer.cs

# Commit
git commit -m "Update version to 1.1.9 in AssemblyInfo and LoginForm

Version updates:
- AssemblyInfo: 1.1.8.0 -> 1.1.9.0
- LoginFormModern: Version 1.1.8 -> Version 1.1.9
- Formmain lit automatiquement depuis AssemblyInfo

Package rebuilt:
- GestionModerneRH_v1.1.9_Update.exe regenerated (~67 MB)
- Compilation successful with updated version

🤖 Generated with [Claude Code](https://claude.com/claude-code)

Co-Authored-By: Claude <noreply@anthropic.com>"
```

**8.3. Troisième commit : Configuration**

```bash
# Ajouter les fichiers de configuration
git add setup-update.iss
git add UPDATE_NOTES_v1.1.9.txt
git add RESUME_v1.1.9.md
git add build_release.bat
# Si créé : git add build_v1.1.9.ps1

# Commit
git commit -m "Configuration v1.1.9 - Package d'installation

Fichiers de configuration:
- setup-update.iss mis à jour pour v1.1.9
- UPDATE_NOTES_v1.1.9.txt créé
- RESUME_v1.1.9.md créé (documentation complète)
- build_release.bat mis à jour

Package généré:
- GestionModerneRH_v1.1.9_Update.exe (~67 MB)
- Compilation réussie sans erreurs
- Tests OK

🤖 Generated with [Claude Code](https://claude.com/claude-code)

Co-Authored-By: Claude <noreply@anthropic.com>"
```

**8.4. Push vers GitHub**

```bash
git push origin main
```

**8.5. Créer un tag (recommandé)**

```bash
# Créer le tag
git tag -a v1.1.9 -m "Release v1.1.9 - [Description courte]"

# Pusher le tag
git push origin v1.1.9
```

---

### 📍 Étape 9 : Tests avant distribution

**9.1. Installation sur machine de test**

1. Copier `GestionModerneRH_v1.1.9_Update.exe` sur une machine de test
2. Fermer l'application si elle est ouverte
3. Exécuter l'installateur en tant qu'administrateur
4. Suivre l'assistant d'installation

**9.2. Vérifications post-installation**

✅ **Version affichée correctement**
   - Écran de login : "Version 1.1.9"
   - Menu principal : "Version 1.1.9" (en bas)

✅ **Fonctionnalités nouvelles**
   - Tester toutes les nouvelles fonctionnalités
   - Vérifier qu'elles fonctionnent comme prévu

✅ **Non-régression**
   - Tester les fonctionnalités existantes importantes
   - S'assurer qu'aucune régression n'a été introduite

✅ **Base de données**
   - Vérifier la connexion à la base de données
   - Tester quelques opérations CRUD

✅ **Données utilisateur**
   - Vérifier que les données existantes sont préservées
   - Vérifier que les préférences sont conservées

**9.3. Tests spécifiques par module**

Selon vos changements, tester :

- 📊 **Saisie de paie** : Horaire, Journalier, Par lot
- 👥 **Gestion employés** : CRUD, filtres, exports
- 💰 **Calculs salariaux** : CNSS, IUTS, retenues
- 📄 **Génération PDF** : Bulletins, rapports
- 📈 **Exports Excel** : Tous les types d'exports
- 🔐 **Authentification** : Login, rôles, permissions

---

### 📍 Étape 10 : Distribution

**10.1. Préparer le package pour distribution**

1. Renommer si nécessaire (généralement pas besoin)
2. Créer un checksum MD5/SHA256 (optionnel)

```powershell
# Générer un hash SHA256
Get-FileHash "Setup\Output\GestionModerneRH_v1.1.9_Update.exe" -Algorithm SHA256
```

**10.2. Upload sur les canaux de distribution**

Options :
- GitHub Releases
- Google Drive / Dropbox partagé
- Serveur FTP de l'entreprise
- Email direct aux clients (petite base)

**10.3. Communication**

Informer les utilisateurs :
- Email avec lien de téléchargement
- Notes de version incluses
- Instructions d'installation
- Date d'application recommandée

---

## Fichiers à mettre à jour

### Tableau récapitulatif

| Fichier | Champ à modifier | Exemple |
|---------|------------------|---------|
| `RH_GRH\Properties\AssemblyInfo.cs` | AssemblyVersion | `[assembly: AssemblyVersion("1.1.9.0")]` |
| `RH_GRH\Properties\AssemblyInfo.cs` | AssemblyFileVersion | `[assembly: AssemblyFileVersion("1.1.9.0")]` |
| `RH_GRH\LoginFormModern.Designer.cs` | labelVersion.Text | `this.labelVersion.Text = "Version 1.1.9";` |
| `setup-update.iss` | Commentaire en-tête | `; Script d'installation Inno Setup pour Gestion Moderne RH v1.1.9` |
| `setup-update.iss` | MyAppVersion | `#define MyAppVersion "1.1.9"` |
| `setup-update.iss` | InfoBeforeFile | `InfoBeforeFile=UPDATE_NOTES_v1.1.9.txt` |
| `setup-update.iss` | VersionInfoDescription | `...Mise à jour v1.1.9` |
| `build_release.bat` | Message | `echo BUILD GESTION MODERNE RH v1.1.9` |
| **NOUVEAU** | `UPDATE_NOTES_v1.1.9.txt` | Créer le fichier avec notes de version |
| **NOUVEAU** | `RESUME_v1.1.9.md` | Créer le fichier avec résumé détaillé |

---

## Génération du package

### Script PowerShell complet

Créer `build_and_package_v1.1.9.ps1` :

```powershell
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "BUILD & PACKAGE GESTION MODERNE RH v1.1.9" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$version = "1.1.9"
$msbuild = "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\amd64\MSBuild.exe"
$iscc = "C:\Program Files (x86)\Inno Setup 6\ISCC.exe"
$project = "RH_GRH\RH_GRH.csproj"

# Vérifier MSBuild
if (-not (Test-Path $msbuild)) {
    Write-Host "ERREUR: MSBuild introuvable!" -ForegroundColor Red
    exit 1
}

# Vérifier Inno Setup
if (-not (Test-Path $iscc)) {
    Write-Host "ERREUR: Inno Setup introuvable!" -ForegroundColor Red
    exit 1
}

# Étape 1: Nettoyage
Write-Host "Étape 1/4: Nettoyage du projet..." -ForegroundColor Yellow
& $msbuild $project /t:Clean /p:Configuration=Release /v:minimal /nologo
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERREUR durant le nettoyage!" -ForegroundColor Red
    exit 1
}

# Étape 2: Compilation
Write-Host ""
Write-Host "Étape 2/4: Compilation en mode Release..." -ForegroundColor Yellow
& $msbuild $project /t:Rebuild /p:Configuration=Release /v:minimal /nologo
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERREUR durant la compilation!" -ForegroundColor Red
    exit 1
}

# Étape 3: Vérification
Write-Host ""
Write-Host "Étape 3/4: Vérification de l'exécutable..." -ForegroundColor Yellow
$exePath = "RH_GRH\bin\Release\RH_GRH.exe"
if (-not (Test-Path $exePath)) {
    Write-Host "ERREUR: Exécutable non trouvé!" -ForegroundColor Red
    exit 1
}
$fileVersion = (Get-Item $exePath).VersionInfo.FileVersion
Write-Host "Version de l'exe: $fileVersion" -ForegroundColor Cyan

# Étape 4: Génération du package
Write-Host ""
Write-Host "Étape 4/4: Génération du package d'installation..." -ForegroundColor Yellow
Start-Sleep -Seconds 3
& $iscc setup-update.iss
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERREUR durant la génération du package!" -ForegroundColor Red
    exit 1
}

# Vérification finale
$setupPath = "Setup\Output\GestionModerneRH_v${version}_Update.exe"
if (Test-Path $setupPath) {
    $fileSize = (Get-Item $setupPath).Length / 1MB
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Green
    Write-Host "BUILD & PACKAGE TERMINÉS AVEC SUCCÈS!" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Green
    Write-Host "Fichier: $setupPath" -ForegroundColor White
    Write-Host "Taille: $([math]::Round($fileSize, 2)) MB" -ForegroundColor White
    Write-Host ""
} else {
    Write-Host "ERREUR: Package non trouvé!" -ForegroundColor Red
    exit 1
}
```

**Utilisation :**

```powershell
powershell.exe -ExecutionPolicy Bypass -File build_and_package_v1.1.9.ps1
```

---

## Tests avant distribution

### Checklist de tests

#### ✅ Tests d'installation

- [ ] L'installateur démarre sans erreur
- [ ] Pas de message d'erreur Windows Defender/Antivirus
- [ ] L'installation se termine avec succès
- [ ] Tous les fichiers sont copiés
- [ ] Les raccourcis sont créés (bureau, menu démarrer)
- [ ] Le registre Windows est correctement mis à jour

#### ✅ Tests de version

- [ ] Version affichée sur écran de login : "Version 1.1.9"
- [ ] Version affichée dans le menu principal : "Version 1.1.9"
- [ ] Propriétés du fichier RH_GRH.exe : Version 1.1.9.0

#### ✅ Tests fonctionnels critiques

**Authentification :**
- [ ] Login avec utilisateur admin
- [ ] Login avec utilisateur standard
- [ ] Mot de passe oublié (si applicable)
- [ ] Changement de mot de passe

**Base de données :**
- [ ] Connexion à la base de données
- [ ] Lecture des données existantes
- [ ] Création d'un nouvel enregistrement
- [ ] Modification d'un enregistrement
- [ ] Suppression d'un enregistrement (si applicable)

**Modules principaux :**
- [ ] Gestion employés : Liste, ajout, modification
- [ ] Saisie de paie horaire : Calculs corrects
- [ ] Saisie de paie journalier : Calculs corrects
- [ ] Saisie de paie par lot : Génération PDF multiple
- [ ] Calculs CNSS : Vérifier plafonds et taux
- [ ] Calculs IUTS : Vérifier tranches et barème
- [ ] Génération bulletins PDF : Format correct
- [ ] Export Excel : Fichiers générés correctement

**Nouvelles fonctionnalités v1.1.9 :**
- [ ] [Liste des nouvelles fonctionnalités à tester]

#### ✅ Tests de non-régression

- [ ] Aucune fonctionnalité existante n'est cassée
- [ ] Les calculs donnent les mêmes résultats qu'avant
- [ ] Les exports fonctionnent toujours
- [ ] Les permissions utilisateurs sont respectées

#### ✅ Tests de performance

- [ ] L'application démarre en moins de 5 secondes
- [ ] Pas de ralentissement notable
- [ ] Les requêtes SQL restent rapides
- [ ] La génération PDF ne prend pas plus de temps

---

## Troubleshooting

### Problèmes courants et solutions

#### ❌ Erreur : "Resource update error: EndUpdateResource failed"

**Cause :** Un fichier Setup est verrouillé par un autre processus.

**Solution :**
```powershell
# Attendre quelques secondes avant de relancer
Start-Sleep -Seconds 5
& "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" setup-update.iss
```

---

#### ❌ Erreur : "CS1503: Argument 1 : conversion impossible de 'string' en 'System.Drawing.FontFamily'"

**Cause :** Problème avec la définition de police (Font).

**Solution :**
Utiliser les polices système correctement :
```csharp
// ❌ INCORRECT
Font = new System.Drawing.Font("Montserrat", 9.5F, FontStyle.SemiBold)

// ✅ CORRECT
Font = new System.Drawing.Font("Montserrat", 9.5F, FontStyle.Bold)
```

Note : `FontStyle.SemiBold` n'existe pas en .NET, utiliser `Bold` ou `Regular`.

---

#### ❌ Erreur : "CS0117: 'FontStyle' ne contient pas de définition pour 'SemiBold'"

**Cause :** Même problème que ci-dessus.

**Solution :** Utiliser `FontStyle.Bold` à la place.

---

#### ❌ Build échoue avec erreur "Project file does not exist"

**Cause :** Chemin relatif incorrect ou espace dans le chemin.

**Solution :**
```powershell
# ❌ INCORRECT (guillemets mal placés)
& $msbuild "RH_GRH\RH_GRH.csproj" /t:Rebuild

# ✅ CORRECT (guillemets autour du chemin complet)
$project = "RH_GRH\RH_GRH.csproj"
& $msbuild $project /t:Rebuild /p:Configuration=Release
```

---

#### ❌ Le numéro de version ne s'affiche pas correctement

**Vérifications :**

1. AssemblyInfo.cs est bien modifié et sauvegardé
2. Le projet a été recompilé APRÈS la modification
3. Pas de cache de compilation

**Solution :**
```powershell
# Forcer un Clean puis Rebuild
& $msbuild $project /t:Clean /p:Configuration=Release
& $msbuild $project /t:Rebuild /p:Configuration=Release
```

---

#### ❌ Git : "error: invalid path 'nul'"

**Cause :** Fichier système Windows invalide dans le dépôt.

**Solution :**
```bash
# Supprimer le fichier
rm nul

# Continuer avec git add
git add .
```

---

#### ❌ Inno Setup Warning : "OnlyBelowVersion parameter"

**Cause :** Avertissement non critique concernant Windows Vista (obsolète).

**Impact :** Aucun, peut être ignoré.

**Solution (optionnel) :** Retirer la ligne concernée dans setup-update.iss :
```ini
; À retirer ou commenter
; Name: "quicklaunchicon"; Description: ".."; OnlyBelowVersion: 6.1
```

---

#### ❌ Package généré mais taille anormale (< 50 MB ou > 80 MB)

**Cause possible :**
- Fichiers manquants (< 50 MB)
- Fichiers dupliqués ou debug inclus (> 80 MB)

**Solution :**
1. Vérifier la section [Files] de setup-update.iss
2. S'assurer que Source pointe vers `bin\Release` et non `bin\Debug`
3. Vérifier qu'aucun fichier volumineux superflu n'est inclus

---

### Logs de débogage

#### Activer les logs MSBuild détaillés

```powershell
& $msbuild $project /t:Rebuild /p:Configuration=Release /v:detailed /fl /flp:logfile=build.log
```

Fichier généré : `build.log`

#### Activer les logs Inno Setup

```powershell
& $iscc setup-update.iss /O+ /LOG=inno_setup.log
```

Fichier généré : `inno_setup.log`

---

## Template de message de commit

### Commit principal (Code)

```
Release v{VERSION} - {Titre court}

{Description détaillée des changements}

Modifications principales:
- {Point 1}
- {Point 2}
- {Point 3}

Corrections de bugs:
- {Bug 1}
- {Bug 2}

Améliorations techniques:
- {Amélioration 1}
- {Amélioration 2}

Fichiers modifiés:
- {Fichier 1}
- {Fichier 2}

🤖 Generated with [Claude Code](https://claude.com/claude-code)

Co-Authored-By: Claude <noreply@anthropic.com>
```

### Commit version

```
Update version to {VERSION} in AssemblyInfo and LoginForm

Version updates:
- AssemblyInfo: {OLD_VERSION} -> {NEW_VERSION}
- LoginFormModern: Version {OLD_VERSION} -> Version {NEW_VERSION}
- Formmain lit automatiquement depuis AssemblyInfo

Package rebuilt:
- GestionModerneRH_v{VERSION}_Update.exe regenerated (~67 MB)
- Compilation successful with updated version

🤖 Generated with [Claude Code](https://claude.com/claude-code)

Co-Authored-By: Claude <noreply@anthropic.com>
```

### Commit configuration

```
Configuration v{VERSION} - Package d'installation

Fichiers de configuration:
- setup-update.iss mis à jour pour v{VERSION}
- UPDATE_NOTES_v{VERSION}.txt créé
- RESUME_v{VERSION}.md créé (documentation complète)
- build_release.bat mis à jour

Package généré:
- GestionModerneRH_v{VERSION}_Update.exe (~67 MB)
- Compilation réussie sans erreurs
- Tests OK

🤖 Generated with [Claude Code](https://claude.com/claude-code)

Co-Authored-By: Claude <noreply@anthropic.com>
```

---

## Exemple complet : Release 1.1.8 → 1.1.9

### Contexte

**Version actuelle :** 1.1.8
**Nouvelle version :** 1.1.9
**Changements :**
- Correction d'un bug dans le calcul des congés
- Amélioration de l'export Excel des statistiques
- Ajout d'un filtre par département

### Étapes suivies

1. ✅ Code modifié et testé
2. ✅ AssemblyInfo : 1.1.8.0 → 1.1.9.0
3. ✅ LoginFormModern : "Version 1.1.8" → "Version 1.1.9"
4. ✅ setup-update.iss : MyAppVersion "1.1.9"
5. ✅ setup-update.iss : InfoBeforeFile=UPDATE_NOTES_v1.1.9.txt
6. ✅ Création UPDATE_NOTES_v1.1.9.txt
7. ✅ Création RESUME_v1.1.9.md
8. ✅ Compilation Release réussie
9. ✅ Package généré : GestionModerneRH_v1.1.9_Update.exe (67.2 MB)
10. ✅ Tests d'installation OK
11. ✅ Tests fonctionnels OK
12. ✅ Commit et push vers GitHub
13. ✅ Tag v1.1.9 créé
14. ✅ Distribution aux utilisateurs

**Temps total :** ~45 minutes

---

## Ressources utiles

### Liens

- **GitHub Repository :** https://github.com/AaronThierry/Rhplus_Gestion
- **Inno Setup Documentation :** https://jrsoftware.org/ishelp/
- **MSBuild Reference :** https://learn.microsoft.com/en-us/visualstudio/msbuild/

### Fichiers de référence

Dans le dépôt, consultez ces exemples :
- `RESUME_v1.1.8.md` - Exemple de résumé détaillé
- `UPDATE_NOTES_v1.1.8.txt` - Exemple de notes utilisateur
- `setup-update.iss` - Script Inno Setup configuré

---

## Notes importantes

### ⚠️ À NE JAMAIS FAIRE

- ❌ Modifier le numéro de version APRÈS avoir compilé
- ❌ Oublier de tester le package avant distribution
- ❌ Pusher directement vers `main` sans tester
- ❌ Distribuer un package avec des erreurs de compilation
- ❌ Oublier de créer les notes de mise à jour
- ❌ Utiliser des versions incohérentes dans différents fichiers

### ✅ Bonnes pratiques

- ✅ Toujours tester sur une machine propre avant distribution
- ✅ Garder une trace de toutes les versions (tags Git)
- ✅ Documenter TOUS les changements dans les notes
- ✅ Informer les utilisateurs AVANT de déployer
- ✅ Garder une copie de sauvegarde de la version précédente
- ✅ Vérifier la compatibilité base de données

---

## Changelog de ce guide

| Version | Date | Changements |
|---------|------|-------------|
| 1.0 | 27/04/2026 | Création initiale basée sur release v1.1.8 |

---

**Auteur :** GMP - Gestion Moderne de Paie
**Contact :** secretariatrhplus@gmail.com
**Dernière révision :** 27 Avril 2026

---

© 2026 GMP - Gestion Moderne de Paie. Tous droits réservés.
