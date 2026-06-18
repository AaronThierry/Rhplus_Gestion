# Guide de Compilation et Génération du Package v1.1.6

## ✅ État Actuel

- ✅ Code modifié et commit créé
- ✅ Version mise à jour → 1.1.6.0
- ✅ Fichiers de configuration prêts
- ⏳ Push Git en cours
- ⚠️ **Build nécessaire avant génération du package**

---

## 📦 ÉTAPE 1 : Compiler le Projet

### Option A : Avec Visual Studio (RECOMMANDÉ)

1. **Ouvrir Visual Studio 2022**
2. **Ouvrir le projet**
   - File > Open > Project/Solution
   - Naviguer vers : `C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion\RH_GRH\RH_GRH.csproj`

3. **Sélectionner le mode Release**
   - Dans la barre d'outils, changer de "Debug" à **"Release"**
   - Vérifier que la plateforme est "Any CPU" ou "x86"

4. **Nettoyer et Rebuild**
   - Menu : **Build** > **Clean Solution**
   - Attendre la fin du nettoyage
   - Menu : **Build** > **Rebuild Solution**
   - Attendre la fin de la compilation (peut prendre 2-3 minutes)

5. **Vérifier la compilation**
   - Dans la fenêtre Output, vérifier qu'il n'y a pas d'erreurs
   - Le fichier sera créé dans : `RH_GRH\bin\Release\RH_GRH.exe`
   - Vérifier que la version du fichier est **1.1.6.0** (clic droit > Propriétés > Détails)

### Option B : Avec Developer Command Prompt

1. **Ouvrir Developer Command Prompt for VS 2022**
   - Menu Démarrer > Visual Studio 2022 > Developer Command Prompt

2. **Naviguer vers le projet**
   ```cmd
   cd "C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion\RH_GRH"
   ```

3. **Nettoyer**
   ```cmd
   msbuild RH_GRH.csproj /t:Clean /p:Configuration=Release
   ```

4. **Compiler**
   ```cmd
   msbuild RH_GRH.csproj /t:Rebuild /p:Configuration=Release /v:minimal
   ```

5. **Vérifier**
   ```cmd
   dir bin\Release\RH_GRH.exe
   ```

---

## 🔧 ÉTAPE 2 : Générer le Package d'Installation

### Prérequis
- ✅ Compilation Release terminée avec succès
- ✅ Inno Setup 6 installé

### Option A : Avec Inno Setup Compiler (Interface Graphique)

1. **Ouvrir Inno Setup Compiler**
   - Menu Démarrer > Inno Setup > Inno Setup Compiler

2. **Ouvrir le script**
   - File > Open
   - Sélectionner : `C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion\setup-update.iss`

3. **Compiler**
   - Menu : **Build** > **Compile**
   - Ou appuyer sur **Ctrl+F9**

4. **Vérifier la sortie**
   - Le package sera créé dans : `Setup\Output\GestionModerneRH_v1.1.6_Update.exe`
   - Taille attendue : ~30-40 MB

### Option B : Avec Ligne de Commande

1. **Ouvrir Command Prompt** (en tant qu'administrateur)

2. **Naviguer vers le projet**
   ```cmd
   cd "C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion"
   ```

3. **Compiler avec Inno Setup**
   ```cmd
   "C:\Program Files (x86)\Inno Setup 6\iscc.exe" setup-update.iss
   ```

4. **Vérifier**
   ```cmd
   dir "Setup\Output\GestionModerneRH_v1.1.6_Update.exe"
   ```

---

## ✅ ÉTAPE 3 : Tester le Package

### Test Rapide

1. **Vérifier les propriétés du fichier exe généré**
   - Clic droit sur `GestionModerneRH_v1.1.6_Update.exe`
   - Propriétés > Détails
   - Version doit être : **1.1.6**

2. **Test d'installation (optionnel mais recommandé)**
   - Copier le package sur une machine de test
   - Fermer RH+ Gestion si ouvert
   - Exécuter en tant qu'administrateur
   - Suivre l'assistant
   - Vérifier que l'application démarre
   - Dans l'application, vérifier la version affichée

### Test des Nouveaux Calculs

1. **Ouvrir Visual Studio avec le projet**
2. **Lancer en mode Debug** (F5)
3. **Ouvrir la fenêtre Output**
   - View > Output
   - Sélectionner "Debug" dans la liste

4. **Générer un bulletin de paie test**
   - Utiliser un employé avec sursalaire si possible
   - Observer les logs détaillés :

```
═══════════════════════════════════════════════════════════════════════
                    CALCUL BASE IUTS - DÉTAIL COMPLET
═══════════════════════════════════════════════════════════════════════

┌─ ÉTAPE 1: CNSS EXONÉRÉE - CALCUL ET SÉLECTION
│  CNSS Exonérée 1 (finale) :       XX,XXX.XX FCFA
│  CNSS Exonérée 2 (finale) :       XX,XXX.XX FCFA
│  ────────────────────────────────────────────
│  ✓ CNSS retenue (MIN)     :       XX,XXX.XX FCFA (...)
```

5. **Vérifier également les indemnités déductibles**

---

## 📋 Checklist de Validation

Avant de distribuer le package :

- [ ] Compilation Release sans erreurs
- [ ] Version fichier RH_GRH.exe = 1.1.6.0
- [ ] Package Inno Setup généré avec succès
- [ ] Taille package ~30-40 MB
- [ ] Test installation réussi
- [ ] Application démarre correctement
- [ ] Version affichée = 1.1.6
- [ ] Logs debugger fonctionnels
- [ ] Calcul CNSS exonérée visible dans logs
- [ ] Calcul indemnités déductibles visible dans logs
- [ ] Git push terminé avec succès

---

## 📁 Emplacement des Fichiers

### Fichiers Source
```
C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion\
├── RH_GRH\
│   ├── RH_GRH.csproj                    ← Projet à compiler
│   ├── bin\Release\RH_GRH.exe           ← Fichier compilé (généré)
│   └── Properties\AssemblyInfo.cs       ← Version 1.1.6.0 ✓
├── setup-update.iss                      ← Script Inno Setup
├── UPDATE_NOTES_v1.1.6.txt              ← Notes de version
└── Setup\Output\
    └── GestionModerneRH_v1.1.6_Update.exe  ← Package final (à générer)
```

---

## 🔍 Vérification des Modifications v1.1.6

### Fichiers Modifiés à Vérifier

1. **RH_GRH/IUTSCalculator.cs**
   - Rechercher : `cnssExonere1` et `cnssExonere2`
   - Vérifier : Plafond 44 000 FCFA
   - Vérifier : Debugger en 6 étapes

2. **RH_GRH/GestionSalaireHoraireForm.cs**
   - Rechercher : `salaireDeBase: salaireBase`
   - Rechercher : `sursalaire: sursalaire`
   - Vérifier : Debugger indemnités déductibles

3. **RH_GRH/GestionSalaireJournalierForm.cs**
   - Même vérification que ci-dessus

4. **RH_GRH/SaisiePayeLotForm.cs**
   - 2 endroits à vérifier (horaire et journalier)

---

## ⚠️ Problèmes Courants et Solutions

### Erreur : "Le fichier exe est introuvable"
**Solution** : Compiler d'abord le projet en mode Release

### Erreur : "Version incorrecte dans le package"
**Solution** :
1. Vérifier `RH_GRH\Properties\AssemblyInfo.cs` → Version 1.1.6.0
2. Recompiler en mode Release
3. Regénérer le package Inno Setup

### Erreur MSBuild : "Un seul projet peut être spécifié"
**Solution** : Utiliser Visual Studio GUI ou Developer Command Prompt

### Package trop petit (< 20 MB)
**Solution** : Les DLL ne sont pas incluses, vérifier le build Release

---

## 📞 Support Build

Si vous rencontrez des problèmes :

1. **Vérifier les logs de compilation** dans Visual Studio Output
2. **Nettoyer complètement** : Build > Clean Solution
3. **Fermer et rouvrir** Visual Studio
4. **Vérifier NuGet** : Tools > NuGet Package Manager > Manage NuGet Packages for Solution
5. **Restore packages** : Clic droit sur Solution > Restore NuGet Packages

---

## ✨ Nouveautés v1.1.6 à Tester

Une fois le package installé, vérifier :

1. **Calcul CNSS Exonérée**
   - CNSS Exo 1 : 8% × (Sal. Base + Prime Anc + Sursalaire)
   - CNSS Exo 2 : 5.5% × Salaire Brut
   - Plafond 44 000 FCFA appliqué

2. **Salaire Brut Social**
   - Nouvelle formule avec MIN(CNSS Exo 1, CNSS Exo 2)

3. **Abattement**
   - Inclut maintenant le sursalaire dans SBP

4. **Debugger**
   - 6 étapes détaillées pour Base IUTS
   - Détail complet indemnités déductibles

---

**Date de création** : 21 Avril 2026
**Version cible** : 1.1.6
**État** : Prêt pour compilation et génération du package
