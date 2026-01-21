# RH Plus Gestion v1.0 - Guide de Création du Setup

## Vue d'ensemble

Ce document explique comment créer un installateur professionnel pour RH Plus Gestion v1.0.

## Prérequis pour créer l'installateur

### Logiciels requis

1. **Visual Studio 2019/2022** ou **Build Tools pour Visual Studio**
   - Télécharger : https://visualstudio.microsoft.com/downloads/
   - Composants nécessaires : .NET Desktop Development

2. **NuGet CLI**
   - Télécharger : https://www.nuget.org/downloads
   - Ajouter au PATH système

3. **Inno Setup 6**
   - Télécharger : https://jrsoftware.org/isdl.php
   - Version 6.0 ou supérieure recommandée
   - Installer avec les composants par défaut

### Configuration système

- Windows 10 ou supérieur
- .NET Framework 4.7.2 ou supérieur
- 4 GB RAM minimum
- 2 GB d'espace disque libre

## Structure des fichiers

```
Rhplus_Gestion/
├── setup.iss                    # Script Inno Setup principal
├── BUILD_INSTALLER.bat          # Script automatisé de build
├── LICENSE.txt                  # Licence utilisateur
├── INSTALLATION_INFO.txt        # Info avant installation
├── POST_INSTALL_INFO.txt        # Info après installation
├── Setup/
│   ├── Assets/                  # Images pour le wizard
│   │   ├── WizardImage.bmp      # Image latérale (164x314 px)
│   │   └── WizardSmallImage.bmp # Petite image (55x55 px)
│   └── Output/                  # Installateurs générés
└── Database/
    ├── schema.sql               # Structure de la base
    └── initial_data.sql         # Données initiales
```

## Méthode 1 : Compilation automatique (Recommandé)

### Étape 1 : Préparer l'environnement

```batch
# Ouvrir une invite de commande dans le dossier du projet
cd "C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion"
```

### Étape 2 : Exécuter le script de build

```batch
BUILD_INSTALLER.bat
```

Le script va automatiquement :
1. Nettoyer les builds précédents
2. Restaurer les packages NuGet
3. Compiler en mode Release
4. Créer l'installateur avec Inno Setup

### Étape 3 : Récupérer l'installateur

L'installateur sera disponible dans :
```
Setup\Output\RHPlusGestion_v1.0.0_Setup.exe
```

## Méthode 2 : Compilation manuelle

### Étape 1 : Restaurer les packages NuGet

```batch
nuget restore RH_GRH.sln
```

### Étape 2 : Compiler l'application

#### Option A : Via Visual Studio
1. Ouvrir `RH_GRH.sln` dans Visual Studio
2. Sélectionner la configuration **Release**
3. Build → Rebuild Solution (Ctrl+Shift+B)

#### Option B : Via ligne de commande
```batch
"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" RH_GRH.sln /p:Configuration=Release /p:Platform="Any CPU" /t:Rebuild
```

### Étape 3 : Créer les assets (optionnel)

Créer les images pour le wizard d'installation :
- `Setup\Assets\WizardImage.bmp` : 164x314 pixels
- `Setup\Assets\WizardSmallImage.bmp` : 55x55 pixels

### Étape 4 : Compiler le setup avec Inno Setup

#### Option A : Via l'interface graphique
1. Ouvrir Inno Setup Compiler
2. Fichier → Ouvrir → `setup.iss`
3. Build → Compile (Ctrl+F9)

#### Option B : Via ligne de commande
```batch
"C:\Program Files (x86)\Inno Setup 6\ISCC.exe" setup.iss
```

## Configuration du script setup.iss

### Personnalisation

#### Informations de l'application
```pascal
#define MyAppName "RH Plus Gestion"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "GMP - Gestion Moderne de Paie"
#define MyAppURL "https://github.com/AaronThierry/Rhplus_Gestion"
```

#### Fichiers à inclure
Le script inclut automatiquement :
- Exécutable et DLLs
- Fichiers de configuration
- Ressources et rapports
- Documentation

#### Prérequis système
Vérifie automatiquement :
- .NET Framework 4.7.2
- Droits administrateur
- Espace disque disponible

## Personnalisation avancée

### Modifier les images du wizard

1. Créer vos images aux bonnes dimensions
2. Les placer dans `Setup\Assets\`
3. Les noms doivent correspondre à ceux dans `setup.iss`

### Ajouter des composants optionnels

Dans `setup.iss`, section `[Components]` :
```pascal
[Components]
Name: "main"; Description: "Application principale"; Types: full compact custom; Flags: fixed
Name: "docs"; Description: "Documentation complète"; Types: full
Name: "samples"; Description: "Données d'exemple"; Types: full
```

### Personnaliser les tâches d'installation

Dans `setup.iss`, section `[Tasks]` :
```pascal
[Tasks]
Name: "desktopicon"; Description: "Créer une icône sur le bureau"
Name: "startupicon"; Description: "Lancer au démarrage"
```

## Signature numérique (Production)

Pour signer l'installateur avec un certificat :

```pascal
[Setup]
SignTool=mysigntool
SignedUninstaller=yes

[Code]
procedure InitializeWizard;
begin
  // Configuration de signature
end;
```

Configuration de l'outil de signature :
1. Tools → Configure Sign Tools
2. Ajouter : `signtool=C:\path\to\signtool.exe sign /f certificate.pfx /p password $f`

## Tests recommandés

### Avant distribution

1. **Installation propre**
   - Tester sur une machine Windows vierge
   - Vérifier l'installation de .NET Framework
   - Confirmer tous les fichiers copiés

2. **Fonctionnalités**
   - Lancer l'application
   - Tester la connexion MySQL
   - Vérifier toutes les fonctions principales

3. **Désinstallation**
   - Désinstaller l'application
   - Vérifier la suppression des fichiers
   - Confirmer le nettoyage du registre

4. **Mise à jour**
   - Installer version précédente
   - Installer nouvelle version par-dessus
   - Vérifier conservation des données

### Checklist de validation

- [ ] L'installateur se lance sans erreur
- [ ] Les prérequis sont correctement vérifiés
- [ ] Tous les fichiers sont copiés
- [ ] Les raccourcis sont créés
- [ ] L'application se lance après installation
- [ ] La connexion à la base fonctionne
- [ ] La désinstallation est propre
- [ ] Le registre est correctement configuré

## Dépannage

### Erreur : MSBuild introuvable

**Solution** : Installer Build Tools pour Visual Studio
```
https://visualstudio.microsoft.com/downloads/
```

### Erreur : Packages NuGet manquants

**Solution** : Restaurer manuellement
```batch
nuget restore RH_GRH.sln -PackagesDirectory packages
```

### Erreur : Inno Setup introuvable

**Solution** : Vérifier le chemin dans `BUILD_INSTALLER.bat`
```batch
set INNO_PATH=C:\Program Files (x86)\Inno Setup 6\ISCC.exe
```

### Erreur : Fichier DLL manquant

**Solution** : Vérifier les références dans `RH_GRH.csproj`
```batch
# Nettoyer et reconstruire
rmdir /s /q RH_GRH\bin\Release
rmdir /s /q RH_GRH\obj\Release
nuget restore
msbuild /t:Rebuild /p:Configuration=Release
```

### Avertissement : Images wizard manquantes

Les images sont optionnelles. Pour les créer :
1. Créer `WizardImage.bmp` (164x314 pixels)
2. Créer `WizardSmallImage.bmp` (55x55 pixels)
3. Les placer dans `Setup\Assets\`

## Distribution

### Méthode 1 : Distribution directe

1. Copier `RHPlusGestion_v1.0.0_Setup.exe`
2. Distribuer via USB, réseau ou email
3. Inclure `README.md` et `MANUEL_UTILISATEUR.pdf`

### Méthode 2 : Hébergement web

1. Uploader sur un serveur web
2. Créer une page de téléchargement
3. Inclure sommes de contrôle (MD5/SHA256)

```batch
certutil -hashfile RHPlusGestion_v1.0.0_Setup.exe SHA256
```

### Méthode 3 : GitHub Releases

1. Créer une nouvelle release sur GitHub
2. Uploader l'installateur
3. Ajouter notes de version

```bash
git tag -a v1.0.0 -m "Version 1.0.0 - Release initiale"
git push origin v1.0.0
```

## Versioning

Pour créer une nouvelle version :

1. Modifier `MyAppVersion` dans `setup.iss`
2. Mettre à jour `AssemblyInfo.cs`
3. Recompiler l'application
4. Recréer l'installateur
5. Tester la mise à jour

## Support et ressources

### Documentation

- Manuel utilisateur : `MANUEL_UTILISATEUR.pdf`
- Guide d'installation : `GUIDE_INSTALLATION.pdf`
- Documentation API : `docs/`

### Liens utiles

- Inno Setup Documentation : https://jrsoftware.org/ishelp/
- NuGet Documentation : https://docs.microsoft.com/nuget/
- MSBuild Reference : https://docs.microsoft.com/visualstudio/msbuild/

### Support technique

- GitHub Issues : https://github.com/AaronThierry/Rhplus_Gestion/issues
- Email : support@gmp-rh.com

## Changelog

### v1.0.0 (2025-01-21)
- Release initiale
- Gestion complète des employés
- Calcul des salaires (horaire, journalier, mensuel)
- Génération de bulletins de paie
- Impression en lot
- Calculs CNSS, TPA, IUTS
- Import Excel

---

**Note** : Ce document sera mis à jour à chaque nouvelle version pour refléter les changements dans le processus de build.
