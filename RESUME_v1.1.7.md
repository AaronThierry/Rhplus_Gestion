# Version 1.1.7 - Correction calcul BSoc pour déductibilité indemnités

## Date de release
24 avril 2026

## Corrections critiques

### 1. Cohérence calcul Salaire Brut Social (BSoc)
- **Problème** : Deux valeurs différentes de BSoc utilisées dans les calculs
  - Calcul IUTS : BSoc = Brut - CNSS exonérée (correct)
  - Calcul déductibilité indemnités : BSoc = Brut - CNSS employé (incorrect)
- **Impact** : Plafond 5% BSoc pour indemnités transport/fonction calculé avec mauvaise base
- **Solution** :
  - Nouvelle méthode `IUTSCalculator.CalculerSalaireBrutSocial()` centralisée
  - Utilisation systématique de cette méthode dans tous les calculs
  - BSoc = Salaire Brut - MIN(CNSS Exo 1 [8%], CNSS Exo 2 [5.5%])

### 2. Améliorations debugger déductibilité indemnités
- Ajout ligne explicite montrant formule de calcul plafond 5%
- Affichage : `Calcul plafond 5% : 5% × [BSoc] FCFA (BSoc)`
- Meilleure traçabilité des calculs fiscaux

## Fichiers modifiés

### Calculs fiscaux
- `RH_GRH/IUTSCalculator.cs`
  - Ajout méthode `CalculerSalaireBrutSocial()` (ligne 23-44)

- `RH_GRH/GestionSalaireHoraireForm.cs`
  - Utilisation BSoc correct ligne 1308-1314
  - Debug amélioré lignes 662, 676, 699

- `RH_GRH/GestionSalaireJournalierForm.cs`
  - Utilisation BSoc correct ligne 1251-1257
  - Debug amélioré lignes 658, 672, 695

- `RH_GRH/SaisiePayeLotForm.cs`
  - Correction calcul horaire ligne 999-1006
  - Correction calcul journalier ligne 1193-1200

## Impact utilisateur
- Calculs plus précis pour plafonds indemnités déductibles
- Cohérence totale entre affichage debugger et calculs réels
- Pas de changement interface utilisateur

## Détails techniques

### Calcul CNSS Exonérée
```
CNSS Exo 1 = MIN(8% × (Sal. Base + Prime Anc. + Sursalaire), 44000)
CNSS Exo 2 = MIN(5.5% × Salaire Brut, 44000)
CNSS Retenue = MIN(CNSS Exo 1, CNSS Exo 2)
BSoc = Salaire Brut - CNSS Retenue
```

### Plafonds indemnités
- Transport : MIN(5% BSoc, 30000 FCFA)
- Fonction : MIN(5% BSoc, 50000 FCFA)
- Logement : MIN(20% BSoc, 75000 FCFA)

## Notes de migration
- Aucune migration base de données requise
- Recalcul automatique à la prochaine paie
- Compatible avec versions 1.1.x

## Validation
- Tests calcul BSoc avec différents profils salariaux
- Vérification cohérence debugger
- Validation plafonds 5% et 20%
