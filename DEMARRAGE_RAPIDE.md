# 🚀 Démarrage Rapide - Création de l'Installateur

## Pour créer l'installateur en 3 étapes simples :

---

## ⚡ ÉTAPE 1 : Installer les Prérequis (Une seule fois)

### Option A : Installation Automatique (RECOMMANDÉ) ⭐

1. **Clic droit** sur `INSTALL_PREREQUISITES.bat`
2. Choisir **"Exécuter en tant qu'administrateur"**
3. Suivre les instructions à l'écran
4. Quand Inno Setup s'installe, choisir "Standard Installation"

✅ C'est tout ! Les prérequis sont installés.

---

### Option B : Installation Manuelle

Si le script automatique ne fonctionne pas :

#### 1️⃣ Installer NuGet CLI

```batch
# Télécharger
https://www.nuget.org/downloads

# Copier nuget.exe dans C:\Windows\System32\
```

#### 2️⃣ Installer Inno Setup 6

```batch
# Télécharger
https://jrsoftware.org/isdl.php

# Installer (Standard Installation)
```

#### 3️⃣ Vérifier Visual Studio / Build Tools

Si vous avez déjà **Visual Studio 2019/2022**, c'est bon !

Sinon, installer **Build Tools** :
```
https://visualstudio.microsoft.com/downloads/
→ Build Tools for Visual Studio 2022
→ Installer ".NET Desktop Build Tools"
```

---

## ⚡ ÉTAPE 2 : Créer l'Installateur

### La Méthode Ultra Simple :

1. **Double-cliquer** sur `BUILD_INSTALLER.bat`
2. **Attendre** 5-10 minutes (compilation + création du setup)
3. **C'est terminé !**

Le dossier `Setup\Output\` s'ouvrira automatiquement avec votre installateur !

---

## ⚡ ÉTAPE 3 : Récupérer l'Installateur

Votre installateur est prêt :
```
📁 Setup\Output\RHPlusGestion_v1.0.0_Setup.exe
```

**Taille :** ~50-100 MB

**Vous pouvez maintenant le distribuer ! 🎉**

---

## 🎨 BONUS : Personnaliser l'Apparence (Optionnel)

Pour ajouter votre logo dans l'installateur :

### Créer 2 images :

**Image 1 : WizardImage.bmp**
- Dimensions : 164 x 314 pixels
- Emplacement : `Setup\Assets\WizardImage.bmp`
- Usage : Image latérale gauche du wizard

**Image 2 : WizardSmallImage.bmp**
- Dimensions : 55 x 55 pixels
- Emplacement : `Setup\Assets\WizardSmallImage.bmp`
- Usage : Petite icône en haut à droite

Puis relancer `BUILD_INSTALLER.bat`

---

## ❓ En Cas de Problème

### Le script s'arrête avec une erreur

**Consulter :**
1. Le fichier `GUIDE_CREATION_INSTALLATEUR.md` (guide détaillé)
2. La section "Résolution des problèmes"

### Besoin d'aide

- 📧 Email : support@gmp-rh.com
- 🐛 GitHub Issues : https://github.com/AaronThierry/Rhplus_Gestion/issues

---

## 📋 Résumé Ultra-Rapide

```batch
# 1. Installer les prérequis (une seule fois)
Clic droit sur INSTALL_PREREQUISITES.bat → Exécuter en admin

# 2. Créer l'installateur
Double-clic sur BUILD_INSTALLER.bat

# 3. Récupérer le fichier
Setup\Output\RHPlusGestion_v1.0.0_Setup.exe
```

**Temps total : ~15-20 minutes** (dont 10 min d'attente compilation)

---

## ✅ Checklist Rapide

- [ ] Prérequis installés (`INSTALL_PREREQUISITES.bat`)
- [ ] Build lancé (`BUILD_INSTALLER.bat`)
- [ ] Installateur créé (`Setup\Output\RHPlusGestion_v1.0.0_Setup.exe`)
- [ ] Testé l'installation sur votre PC
- [ ] Prêt à distribuer ! 🚀

---

## 🎯 Prochaines Étapes Après Création

1. **Tester l'installateur** sur votre machine
2. **Tester sur une autre machine** (si possible)
3. **Distribuer** aux utilisateurs :
   - Par email
   - Sur clé USB
   - Via GitHub Releases
   - Sur réseau partagé

---

**C'est simple, rapide et professionnel !** ✨

Pour plus de détails, voir `GUIDE_CREATION_INSTALLATEUR.md`
