# Guide de Build - Version 1.1.10
## Gestion Moderne RH

**Date:** 19 juin 2026
**Version:** 1.1.10

---

## 🎯 Prérequis

### Logiciels Requis
- ✅ Visual Studio 2019/2022 (Community, Professional ou Enterprise)
- ✅ .NET Framework 4.7.2 SDK minimum
- ✅ Inno Setup 6.x (pour générer l'installateur)

### Fichiers Préparés
- ✅ `UPDATE_NOTES_v1.1.10.txt` (créé)
- ✅ `setup-update.iss` (mis à jour avec version 1.1.10)
- ✅ `RESUME_v1.1.10.md` (documentation)

---

## 📋 Étapes de Build

### ÉTAPE 1 : Compiler l'Application en Release

#### Option A : Via Visual Studio (RECOMMANDÉ)

1. **Ouvrir le projet**
   ```
   Double-cliquer sur: Rhplus_Gestion.sln
   ```

2. **Configurer le mode Release**
   - En haut de Visual Studio, sélectionner **"Release"** (au lieu de "Debug")
   - Sélectionner **"Any CPU"** ou **"x86"**

3. **Nettoyer la solution**
   ```
   Menu: Build > Clean Solution
   ```
   - Attendre la fin du nettoyage (quelques secondes)

4. **Compiler la solution**
   ```
   Menu: Build > Rebuild Solution
   ```
   - ⏱️ Durée estimée : 2-5 minutes
   - ✅ Vérifier qu'il n'y a **aucune erreur** dans l'Output

5. **Vérifier l'exécutable**
   ```
   Chemin: RH_GRH\bin\Release\RH_GRH.exe
   Taille attendue: ~31-32 MB
   ```

#### Option B : Via MSBuild (Ligne de commande)

```bash
cd "C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion"

# Nettoyer
msbuild Rhplus_Gestion.sln /t:Clean /p:Configuration=Release

# Compiler
msbuild Rhplus_Gestion.sln /t:Rebuild /p:Configuration=Release
```

---

### ÉTAPE 2 : Générer l'Installateur avec Inno Setup

#### 2.1 Vérifier les Fichiers Requis

Assurez-vous que ces fichiers existent :
- ✅ `RH_GRH\bin\Release\RH_GRH.exe` (compilé à l'étape 1)
- ✅ `UPDATE_NOTES_v1.1.10.txt`
- ✅ `LICENSE.txt`
- ✅ `setup-update.iss` (version 1.1.10)

#### 2.2 Compiler l'Installateur

##### Option A : Via Inno Setup Compiler (GUI)

1. **Ouvrir Inno Setup Compiler**
   ```
   Menu Démarrer > Inno Setup > Inno Setup Compiler
   ```

2. **Ouvrir le script**
   ```
   File > Open
   Sélectionner: C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion\setup-update.iss
   ```

3. **Compiler**
   ```
   Build > Compile
   ou
   Appuyer sur F9
   ```

4. **Attendre la compilation**
   - ⏱️ Durée estimée : 1-3 minutes
   - La compression LZMA2 Ultra peut prendre du temps

5. **Vérifier la sortie**
   ```
   Chemin: Setup\Output\GestionModerneRH_v1.1.10_Update.exe
   Taille attendue: ~65-70 MB
   ```

##### Option B : Via Ligne de Commande

```bash
cd "C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion"

# Compiler l'installateur
"C:\Program Files (x86)\Inno Setup 6\ISCC.exe" setup-update.iss
```

---

### ÉTAPE 3 : Vérifier l'Installateur

1. **Vérifier l'emplacement**
   ```
   Fichier: Setup\Output\GestionModerneRH_v1.1.10_Update.exe
   ```

2. **Vérifier la taille**
   ```
   Taille attendue: ~65-70 MB
   Si beaucoup plus petit: erreur de compilation
   ```

3. **Tester l'installateur (OPTIONNEL)**
   ```
   Double-cliquer sur GestionModerneRH_v1.1.10_Update.exe
   Vérifier que:
   - Les notes de version s'affichent
   - L'installation démarre correctement
   - La version 1.1.10 apparaît dans le wizard
   ```

---

## 📦 Résultat Final

Après compilation réussie, vous aurez :

```
Setup\Output\
└── GestionModerneRH_v1.1.10_Update.exe  (~65-70 MB)
```

**Cet exécutable contient :**
- ✅ Application compilée (Release)
- ✅ Toutes les DLL et dépendances
- ✅ Notes de mise à jour v1.1.10
- ✅ Licence
- ✅ Icône de l'application

---

## 🚨 Résolution des Problèmes

### Erreur : "RH_GRH.exe not found"
**Solution :**
1. Vérifier que vous avez compilé en mode **Release** (pas Debug)
2. Vérifier le chemin : `RH_GRH\bin\Release\RH_GRH.exe`
3. Recompiler la solution si nécessaire

### Erreur : "UPDATE_NOTES_v1.1.10.txt not found"
**Solution :**
1. Vérifier que le fichier existe à la racine du projet
2. Vérifier l'orthographe exacte du nom de fichier

### Erreur : "Build failed" dans Visual Studio
**Solution :**
1. Vérifier la fenêtre "Error List" pour voir les erreurs
2. Nettoyer la solution : `Build > Clean Solution`
3. Fermer Visual Studio et rouvrir
4. Recompiler : `Build > Rebuild Solution`

### Installateur trop petit (<10 MB)
**Solution :**
1. Vérifier que l'exécutable Release existe et est récent
2. Vérifier les sections [Files] dans setup-update.iss
3. Recompiler l'installateur

---

## ✅ Checklist Finale

Avant distribution, vérifier :

- [ ] Application compilée en **Release** (pas Debug)
- [ ] Aucune erreur de compilation
- [ ] `RH_GRH.exe` taille ~31-32 MB
- [ ] Installateur généré taille ~65-70 MB
- [ ] Version affichée : **1.1.10** dans le wizard
- [ ] Notes de version v1.1.10 affichées correctement
- [ ] Test installation sur machine propre (optionnel)

---

## 📌 Commandes Rapides (Résumé)

```bash
# 1. Compiler en Release
cd "C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion"
msbuild Rhplus_Gestion.sln /t:Rebuild /p:Configuration=Release

# 2. Générer l'installateur
"C:\Program Files (x86)\Inno Setup 6\ISCC.exe" setup-update.iss

# 3. Vérifier la sortie
dir "Setup\Output\GestionModerneRH_v1.1.10_Update.exe"
```

---

## 📝 Prochaines Étapes

Après génération de l'installateur :

1. **Tester sur environnement de test**
2. **Créer un commit Git pour les fichiers de build**
3. **Créer un tag Git v1.1.10**
4. **Distribuer l'installateur**

---

**Fin du guide - Version 1.1.10**
