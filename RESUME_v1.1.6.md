# Gestion Moderne RH - Version 1.1.6

## Date de Release: 21 Avril 2026

---

## 🎯 Résumé des Améliorations

Cette version 1.1.6 apporte des **améliorations majeures aux calculs fiscaux** avec l'intégration de la CNSS exonérée et un système de débogage détaillé pour faciliter la vérification des calculs de paie.

---

## ✨ Nouvelles Fonctionnalités

### 1. Calcul CNSS Exonérée
- **CNSS Exonérée 1** : 8% × (Salaire base + Prime ancienneté + Sursalaire)
- **CNSS Exonérée 2** : 5.5% × Salaire Brut
- Plafonnement automatique à **44 000 FCFA** pour chaque type
- Sélection automatique du **minimum** entre les deux

### 2. Nouveau Calcul Salaire Brut Social
**Ancienne formule :**
```
Salaire Brut Social = Salaire Brut - CNSS Employé
```

**Nouvelle formule :**
```
Salaire Brut Social = Salaire Brut - MIN(CNSS Exonérée 1, CNSS Exonérée 2)
```

### 3. Amélioration Calcul Abattement Fiscal
**Ancienne formule SBP :**
```
SBP = Salaire catégoriel + Prime ancienneté
```

**Nouvelle formule SBP :**
```
SBP = Salaire catégoriel + Prime ancienneté + Sursalaire
```

---

## 🔍 Système de Débogage Détaillé

### Debugger Base IUTS (6 étapes)

#### Étape 1 : CNSS Exonérée - Calcul et Sélection
- Affichage CNSS Exonérée 1 et 2
- Indication de laquelle est retenue (la plus petite)
- Signalement automatique si plafond de 44 000 FCFA appliqué

#### Étape 2 : Salaire Brut Social (BSoc)
- Détail du calcul avec CNSS exonérée retenue

#### Étape 3 : Brut Fiscal
- Calcul avec déduction des indemnités déductibles

#### Étape 4 : Détail CNSS Exonérée
- Décomposition complète CNSS Exonérée 1
  - Salaire de base
  - Prime d'ancienneté
  - Sursalaire
  - Calcul 8%
  - Application du plafond si nécessaire

- Décomposition complète CNSS Exonérée 2
  - Salaire Brut
  - Calcul 5.5%
  - Application du plafond si nécessaire

#### Étape 5 : Abattement
- Statut (cadre/non-cadre)
- Taux (20% ou 25%)
- Composants SBP (avec sursalaire)
- Montant d'abattement calculé

#### Étape 6 : Base IUTS
- Base IUTS exacte
- Base IUTS arrondie (centaine inférieure)

### Debugger Indemnités Déductibles

#### Indemnité Logement
- Type : Numéraire / Nature / Mixte
- Montants
- Plafonds :
  - 20% × Brut Social
  - 75 000 FCFA fixe
- Exonération finale

#### Indemnité Transport
- Type : Numéraire / Nature / Mixte
- Montants
- Plafonds :
  - 5% × Brut Social
  - 30 000 FCFA fixe
- Exonération finale

#### Indemnité Fonction
- Montant numéraire
- Plafonds :
  - 5% × Brut Social
  - 50 000 FCFA fixe
- Exonération finale

#### Total Déductibilité
- Récapitulatif des trois exonérations
- Total déductible

---

## 📁 Fichiers Modifiés

### Calculs Fiscaux
- `RH_GRH/IUTSCalculator.cs`
  - Ajout calcul CNSS exonérée avec plafonnement
  - Modification calcul salaire brut social
  - Ajout sursalaire dans abattement
  - Debugger détaillé en 6 étapes

- `RH_GRH/GestionSalaireHoraireForm.cs`
  - Mise à jour appel IUTSCalculator
  - Debugger indemnités déductibles

- `RH_GRH/GestionSalaireJournalierForm.cs`
  - Mise à jour appel IUTSCalculator
  - Debugger indemnités déductibles

- `RH_GRH/SaisiePayeLotForm.cs`
  - Mise à jour appels IUTSCalculator (2 appels)
  - Ajout paramètres sursalaire et salaire de base

### Version et Configuration
- `RH_GRH/Properties/AssemblyInfo.cs` → Version 1.1.6.0
- `setup-update.iss` → Configuration update v1.1.6
- `UPDATE_NOTES_v1.1.6.txt` → Notes de mise à jour

---

## ⚠️ Impact sur les Calculs

Cette mise à jour modifie les calculs fiscaux de manière significative :

### 1. Salaire Brut Social
- **Avant** : Déduction de la CNSS Employé
- **Après** : Déduction du minimum entre CNSS Exo 1 et CNSS Exo 2
- **Impact** : Augmentation du salaire brut social

### 2. Abattement Fiscal
- **Avant** : Calculé sur Sal. Cat + Prime Anc
- **Après** : Calculé sur Sal. Cat + Prime Anc + Sursalaire
- **Impact** : Augmentation de l'abattement si sursalaire > 0

### 3. Base IUTS
- **Impact** : Modification due aux deux changements ci-dessus

---

## 📊 Comment Vérifier les Calculs

1. **Ouvrir Visual Studio**
2. **Menu** : View > Output
3. **Sélectionner** : "Debug" dans la liste déroulante
4. **Générer** un bulletin de paie
5. **Consulter** les logs détaillés dans la fenêtre Output

Exemple de sortie :
```
═══════════════════════════════════════════════════════════════════════
                    CALCUL BASE IUTS - DÉTAIL COMPLET
═══════════════════════════════════════════════════════════════════════

┌─ ÉTAPE 1: CNSS EXONÉRÉE - CALCUL ET SÉLECTION
│  CNSS Exonérée 1 (finale) :       34,400.00 FCFA
│  CNSS Exonérée 2 (finale) :       27,500.00 FCFA
│  ────────────────────────────────────────────
│  ✓ CNSS retenue (MIN)     :       27,500.00 FCFA (Exonérée 2)
...
```

---

## 📦 Installation

### Prérequis
- Version précédente de Gestion Moderne RH installée
- Droits administrateur Windows

### Étapes d'Installation

1. **Fermer** l'application RH+ Gestion si elle est ouverte
2. **Exécuter** `GestionModerneRH_v1.1.6_Update.exe`
3. **Suivre** les instructions de l'installateur
4. **Redémarrer** l'application

---

## 🔧 Pour les Développeurs

### Compilation du Projet

```bash
# Ouvrir le projet dans Visual Studio
# Sélectionner Release mode
# Build > Build Solution
```

### Génération du Package Update

```bash
# Utiliser Inno Setup Compiler
iscc setup-update.iss
```

Le fichier sera généré dans : `Setup/Output/GestionModerneRH_v1.1.6_Update.exe`

---

## 📝 Formules Complètes

### CNSS Exonérée
```
CNSS Exo 1 = MIN(8% × (Sal. Base + Prime Anc + Sursalaire), 44 000)
CNSS Exo 2 = MIN(5.5% × Salaire Brut, 44 000)
CNSS Retenue = MIN(CNSS Exo 1, CNSS Exo 2)
```

### Salaire Brut Social
```
BSoc = Salaire Brut - CNSS Retenue
```

### Brut Fiscal
```
Brut Fiscal = BSoc - Indemnités Déductibles
```

### Abattement
```
SBP = Salaire catégoriel + Prime ancienneté + Sursalaire
Abattement = Taux × SBP
  où Taux = 20% (cadre) ou 25% (non-cadre)
```

### Base IUTS
```
Base IUTS = Brut Fiscal - Abattement
Base IUTS Arrondie = FLOOR(Base IUTS / 100) × 100
```

---

## 🆘 Support

### Documentation
- Notes de mise à jour complètes : `UPDATE_NOTES_v1.1.6.txt`
- Documentation technique dans le dossier d'installation

### Logs de Débogage
- Visual Studio : View > Output > Debug
- Affichage détaillé de tous les calculs

### Contact
Pour toute question ou assistance technique, consultez la documentation fournie ou contactez le support.

---

## 📜 Historique des Versions

- **v1.1.6** (21/04/2026) - CNSS Exonérée + Debugger détaillé
- **v1.1.5** (Précédent) - Plafond CNSS 800K + Arrondissements
- **v1.1.4** - Corrections critiques CNSS, CDD et matricule
- **v1.1.3** - Améliorations bulletin de paie

---

**© 2025-2026 GMP - Gestion Moderne de Paie**
