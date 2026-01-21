# Guide Pas à Pas - Création de l'Installateur RH Plus Gestion v1.0

## 🎯 Objectif

Créer un installateur professionnel `RHPlusGestion_v1.0.0_Setup.exe` pour distribuer l'application.

---

## 📋 Prérequis à Installer

### 1. Inno Setup 6 (OBLIGATOIRE)

**Téléchargement :**
1. Aller sur : https://jrsoftware.org/isdl.php
2. Télécharger "Inno Setup 6.x.x" (version stable)
3. Exécuter le fichier téléchargé
4. Installation :
   - Accepter la licence
   - Choisir "Standard Installation"
   - Laisser tous les composants cochés
   - Cliquer "Install"

**Vérification :**
```batch
# Vérifier que Inno Setup est installé
dir "C:\Program Files (x86)\Inno Setup 6\ISCC.exe"
```

### 2. NuGet CLI (Si pas déjà installé)

**Téléchargement :**
1. Aller sur : https://www.nuget.org/downloads
2. Télécharger "nuget.exe" (Latest recommended)
3. Placer le fichier dans : `C:\Windows\System32\` ou un dossier dans le PATH

**Vérification :**
```batch
nuget
# Devrait afficher la version et les commandes disponibles
```

### 3. Visual Studio Build Tools (Si pas déjà installé)

**Option A - Si vous avez déjà Visual Studio :**
Vous êtes prêt ! Passez à l'étape suivante.

**Option B - Si vous n'avez pas Visual Studio :**
1. Télécharger Build Tools : https://visualstudio.microsoft.com/downloads/
2. Chercher "Build Tools for Visual Studio 2022"
3. Installer avec les composants :
   - .NET Desktop Build Tools
   - C# compiler

---

## 🚀 Méthode 1 : Création Automatique (RECOMMANDÉ)

### Étape 1 : Ouvrir l'Invite de Commandes

1. Appuyer sur `Windows + R`
2. Taper `cmd`
3. Appuyer sur `Entrée`

### Étape 2 : Naviguer vers le Projet

```batch
cd "C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion"
```

### Étape 3 : Exécuter le Script de Build

```batch
BUILD_INSTALLER.bat
```

### Étape 4 : Attendre la Fin du Processus

Le script va :
1. ✅ Nettoyer les anciens builds
2. ✅ Restaurer les packages NuGet (~2-3 minutes)
3. ✅ Compiler l'application en Release (~1-2 minutes)
4. ✅ Créer l'installateur avec Inno Setup (~30 secondes)

**Sortie attendue :**
```
═══════════════════════════════════════════════════════════════════════════════
  RH PLUS GESTION - Build et Creation du Setup
═══════════════════════════════════════════════════════════════════════════════

[1/5] Nettoyage des fichiers de build precedents...
     Nettoyage termine

[2/5] Restauration des packages NuGet...
     Restauration terminee

[3/5] Compilation en mode Release...
     Compilation terminee avec succes

[4/5] Verification des fichiers compiles...
     Fichiers de sortie verifies

[5/5] Creation de l'installateur avec Inno Setup...

═══════════════════════════════════════════════════════════════════════════════
  BUILD TERMINE AVEC SUCCES !
═══════════════════════════════════════════════════════════════════════════════

Fichiers generes :
  - Application   : RH_GRH\bin\Release\RH_GRH.exe
  - Installateur  : Setup\Output\RHPlusGestion_v1.0.0_Setup.exe
```

### Étape 5 : Récupérer l'Installateur

L'installateur sera dans :
```
C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion\Setup\Output\RHPlusGestion_v1.0.0_Setup.exe
```

Le dossier s'ouvrira automatiquement à la fin du build !

---

## 🔧 Méthode 2 : Création Manuelle (Alternative)

Si le script automatique ne fonctionne pas, suivez ces étapes manuelles :

### Étape 1 : Restaurer les Packages NuGet

```batch
cd "C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion"
nuget restore RH_GRH.sln
```

### Étape 2 : Compiler l'Application

**Option A : Avec Visual Studio**
1. Double-cliquer sur `RH_GRH.sln`
2. En haut, sélectionner "Release" (au lieu de "Debug")
3. Menu `Build` → `Rebuild Solution` (ou Ctrl+Shift+B)
4. Attendre la fin de la compilation

**Option B : Ligne de commande**
```batch
"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" RH_GRH.sln /p:Configuration=Release /t:Rebuild
```

### Étape 3 : Vérifier la Compilation

```batch
dir "RH_GRH\bin\Release\RH_GRH.exe"
```

Devrait afficher le fichier avec la date/heure récente.

### Étape 4 : Compiler le Setup avec Inno Setup

**Option A : Interface graphique (Plus facile)**
1. Aller dans le dossier du projet
2. Double-cliquer sur `setup.iss`
3. Inno Setup s'ouvre
4. Menu `Build` → `Compile` (ou F9)
5. Attendre la fin de la compilation

**Option B : Ligne de commande**
```batch
"C:\Program Files (x86)\Inno Setup 6\ISCC.exe" setup.iss
```

### Étape 5 : Récupérer l'Installateur

```batch
explorer "Setup\Output"
```

---

## 🎨 OPTIONNEL : Personnaliser les Images du Wizard

Pour un look encore plus professionnel :

### Créer WizardImage.bmp (Image latérale)

1. Ouvrir un éditeur d'image (Paint.NET, GIMP, Photoshop)
2. Créer une nouvelle image :
   - Largeur : 164 pixels
   - Hauteur : 314 pixels
   - Profondeur : 24 bits
3. Designer votre image (logo, gradient, etc.)
4. Sauvegarder en BMP : `Setup\Assets\WizardImage.bmp`

### Créer WizardSmallImage.bmp (Petite icône)

1. Créer une nouvelle image :
   - Largeur : 55 pixels
   - Hauteur : 55 pixels
   - Profondeur : 24 bits
2. Insérer votre logo
3. Sauvegarder en BMP : `Setup\Assets\WizardSmallImage.bmp`

### Recompiler le Setup

```batch
BUILD_INSTALLER.bat
```

Les images seront automatiquement intégrées !

---

## ✅ Tester l'Installateur

### Test sur Votre Machine

1. Localiser : `Setup\Output\RHPlusGestion_v1.0.0_Setup.exe`
2. Double-cliquer dessus
3. Suivre l'assistant d'installation
4. Vérifier que tout fonctionne

⚠️ **ATTENTION** : Cela installera vraiment l'application !

### Test sur une Machine Virtuelle (Recommandé)

1. Créer une VM Windows propre
2. Copier l'installateur dans la VM
3. Exécuter l'installation
4. Tester toutes les fonctionnalités
5. Tester la désinstallation

---

## 🐛 Résolution des Problèmes Courants

### Erreur : "MSBuild introuvable"

**Solution 1 :** Trouver MSBuild
```batch
dir "C:\Program Files\Microsoft Visual Studio" /s /b | find "MSBuild.exe"
```

Copier le chemin trouvé et modifier `BUILD_INSTALLER.bat` ligne 15.

**Solution 2 :** Installer Build Tools
https://visualstudio.microsoft.com/downloads/

---

### Erreur : "nuget n'est pas reconnu"

**Solution :**
1. Télécharger nuget.exe : https://www.nuget.org/downloads
2. Le placer dans : `C:\Windows\System32\`
3. Ou l'ajouter au PATH

---

### Erreur : "ISCC.exe introuvable"

**Solution :**
1. Vérifier l'installation : `dir "C:\Program Files (x86)\Inno Setup 6\"`
2. Si absent, réinstaller Inno Setup
3. Modifier le chemin dans `BUILD_INSTALLER.bat` ligne 80

---

### Erreur : "Packages NuGet manquants"

**Solution :**
```batch
# Nettoyer et restaurer
rmdir /s /q packages
nuget restore RH_GRH.sln -PackagesDirectory packages
```

---

### Erreur : "Fichier DLL manquant après compilation"

**Solution :**
```batch
# Nettoyer complètement
rmdir /s /q "RH_GRH\bin"
rmdir /s /q "RH_GRH\obj"
rmdir /s /q "packages"

# Restaurer et recompiler
nuget restore RH_GRH.sln
msbuild RH_GRH.sln /p:Configuration=Release /t:Rebuild
```

---

### L'installateur se compile mais ne contient pas tous les fichiers

**Vérification :**
```batch
# Lister le contenu du dossier Release
dir "RH_GRH\bin\Release" /s
```

**Solution :** S'assurer que tous les DLLs sont présents. Si manquants, recompiler.

---

## 📦 Distribuer l'Installateur

### Méthode 1 : Partage Direct

1. Copier `RHPlusGestion_v1.0.0_Setup.exe`
2. Le partager via :
   - Clé USB
   - Email (si < 25 MB)
   - Réseau local
   - Cloud (Google Drive, OneDrive, etc.)

### Méthode 2 : Créer un Hash pour Vérification

```batch
certutil -hashfile "Setup\Output\RHPlusGestion_v1.0.0_Setup.exe" SHA256
```

Partager le hash avec l'installateur pour que les utilisateurs vérifient l'intégrité.

### Méthode 3 : GitHub Release

```bash
# Créer un tag
git tag -a v1.0.0 -m "Release version 1.0.0"
git push origin v1.0.0

# Ensuite sur GitHub :
# 1. Aller dans Releases
# 2. Draft a new release
# 3. Choisir le tag v1.0.0
# 4. Uploader RHPlusGestion_v1.0.0_Setup.exe
# 5. Publier
```

---

## 📋 Checklist Finale

Avant de distribuer, vérifier :

- [ ] L'installateur se compile sans erreur
- [ ] Le fichier .exe fait environ 50-100 MB
- [ ] L'installation se déroule correctement
- [ ] L'application se lance après installation
- [ ] Toutes les fonctionnalités principales fonctionnent
- [ ] La connexion MySQL fonctionne
- [ ] Les bulletins PDF se génèrent
- [ ] La désinstallation est propre
- [ ] Le README est à jour
- [ ] Le numéro de version est correct (1.0.0)

---

## 🎉 Félicitations !

Vous avez créé votre installateur professionnel !

**Fichier généré :**
```
Setup\Output\RHPlusGestion_v1.0.0_Setup.exe
```

**Taille approximative :** 50-100 MB

**Prêt pour la distribution !**

---

## 📞 Besoin d'Aide ?

Si vous rencontrez des problèmes :
1. Consulter la section "Résolution des problèmes" ci-dessus
2. Vérifier les logs dans le terminal
3. Ouvrir une issue sur GitHub
4. Contacter le support : support@gmp-rh.com

---

**Date de création :** 21 janvier 2025
**Version du guide :** 1.0
**Auteur :** GMP - RH Plus Gestion Team
