# Instructions pour Générer le Package Update v1.1.6

## Étape 1 : Build du Projet

1. Ouvrir **Visual Studio 2022**
2. Ouvrir le projet : `RH_GRH\RH_GRH.csproj`
3. Sélectionner le mode **Release** (en haut)
4. Menu : **Build** > **Rebuild Solution**
5. Attendre la fin de la compilation
6. Vérifier qu'il n'y a pas d'erreurs

## Étape 2 : Générer le Package d'Installation

### Option A : Avec Inno Setup Compiler (Interface Graphique)

1. Ouvrir **Inno Setup Compiler**
2. Menu : **File** > **Open**
3. Sélectionner : `setup-update.iss`
4. Menu : **Build** > **Compile**
5. Le package sera généré dans : `Setup\Output\GestionModerneRH_v1.1.6_Update.exe`

### Option B : Avec Ligne de Commande

```cmd
cd "C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion"

"C:\Program Files (x86)\Inno Setup 6\iscc.exe" setup-update.iss
```

Le package sera créé dans : `Setup\Output\GestionModerneRH_v1.1.6_Update.exe`

## Étape 3 : Tester le Package

1. Copier `GestionModerneRH_v1.1.6_Update.exe` sur une machine de test
2. Fermer RH+ Gestion si ouvert
3. Exécuter le package en tant qu'administrateur
4. Suivre l'assistant d'installation
5. Vérifier que la version affichée est **1.1.6**
6. Générer un bulletin de paie test
7. Vérifier les logs dans Visual Studio (View > Output > Debug)

## Étape 4 : Distribution

Une fois testé avec succès :

1. Copier le fichier vers le serveur de distribution
2. Informer les utilisateurs de la mise à jour disponible
3. Fournir les notes de mise à jour : `UPDATE_NOTES_v1.1.6.txt`

## Vérification des Fichiers

Avant de distribuer, vérifier que ces fichiers sont présents dans le package :

### Exécutables et DLL
- ✅ RH_GRH.exe (version 1.1.6.0)
- ✅ Toutes les DLL tierces (Guna.UI2, MySql.Data, etc.)

### Fichiers de Configuration
- ✅ RH_GRH.exe.config
- ✅ UPDATE_NOTES_v1.1.6.txt

### Nouvelles Fonctionnalités v1.1.6
- ✅ IUTSCalculator.cs avec calcul CNSS exonérée
- ✅ Debugger détaillé pour Base IUTS
- ✅ Debugger détaillé pour indemnités déductibles

## Checklist de Validation

Avant de marquer le build comme final :

- [ ] Version dans AssemblyInfo.cs = 1.1.6.0
- [ ] Version dans setup-update.iss = 1.1.6
- [ ] Compilation sans erreurs ni warnings critiques
- [ ] Package généré avec succès
- [ ] Test d'installation réussi
- [ ] Test de génération bulletin avec logs détaillés
- [ ] Vérification calculs CNSS exonérée
- [ ] Vérification calcul abattement avec sursalaire
- [ ] Git commit et push effectués

## Troubleshooting

### Erreur "iscc n'est pas reconnu"
**Solution** : Spécifier le chemin complet vers iscc.exe
```cmd
"C:\Program Files (x86)\Inno Setup 6\iscc.exe" setup-update.iss
```

### Erreur de compilation Visual Studio
**Solution** :
1. Clean Solution
2. Rebuild Solution
3. Vérifier les packages NuGet

### Package généré mais erreur à l'installation
**Solution** :
1. Vérifier les droits administrateur
2. Désinstaller l'ancienne version si nécessaire
3. Vérifier l'antivirus

## Logs et Debug

Pour vérifier les nouveaux calculs après installation :

1. Ouvrir Visual Studio avec le projet
2. Menu : **View** > **Output**
3. Sélectionner **Debug** dans la liste
4. Lancer l'application en mode Debug (F5)
5. Générer un bulletin de paie
6. Observer les logs détaillés :
   - Calcul CNSS Exonérée 1 et 2
   - Sélection du minimum
   - Calcul Base IUTS en 6 étapes
   - Calcul indemnités déductibles

## Contact Support

Pour toute question concernant le build :
- Consulter la documentation dans le dossier du projet
- Vérifier les fichiers README et UPDATE_NOTES

---

**Date de création** : 21 Avril 2026
**Version cible** : 1.1.6
