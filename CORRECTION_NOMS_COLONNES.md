# Correction des Noms de Colonnes DataGridView

**Date :** 16 Mars 2026
**Fichier corrigé :** `RH_GRH/GestionUtilisateursForm.cs`
**Type :** Bug Fix - ArgumentException

---

## Problème rencontré

### Erreur

```
System.ArgumentException
HResult=0x80070057
Message=La colonne nommée nom_utilisateur est introuvable.
Nom du paramètre : columnName
```

**Ligne d'erreur :** `GestionUtilisateursForm.cs:213`

### Cause

Le code utilisait les noms des propriétés de la base de données (`nom_utilisateur`, `compte_verrouille`) au lieu des noms des colonnes du DataGridView (`colNomUtilisateur`, `colVerrouille`).

---

## Corrections apportées

### 1. Méthode `buttonReinitialiserMdp_Click()` - Ligne 213

**Avant :**
```csharp
string username = dataGridViewUtilisateurs.SelectedRows[0].Cells["nom_utilisateur"].Value.ToString();
```

**Après :**
```csharp
string username = dataGridViewUtilisateurs.SelectedRows[0].Cells["colNomUtilisateur"].Value.ToString();
```

### 2. Méthode `buttonSupprimer_Click()` - Ligne 145

**Avant :**
```csharp
string username = dataGridViewUtilisateurs.SelectedRows[0].Cells["nom_utilisateur"].Value.ToString();
```

**Après :**
```csharp
string username = dataGridViewUtilisateurs.SelectedRows[0].Cells["colNomUtilisateur"].Value.ToString();
```

### 3. Méthode `buttonDeverrouiller_Click()` - Lignes 284-285

**Avant :**
```csharp
string username = dataGridViewUtilisateurs.SelectedRows[0].Cells["nom_utilisateur"].Value.ToString();
bool estVerrouille = Convert.ToBoolean(dataGridViewUtilisateurs.SelectedRows[0].Cells["compte_verrouille"].Value);
```

**Après :**
```csharp
string username = dataGridViewUtilisateurs.SelectedRows[0].Cells["colNomUtilisateur"].Value.ToString();
bool estVerrouille = Convert.ToBoolean(dataGridViewUtilisateurs.SelectedRows[0].Cells["colVerrouille"].Value);
```

---

## Référence des noms de colonnes

### Mapping DataGridView ↔ Base de données

| Nom de colonne DataGridView | DataPropertyName (BD) | Description |
|------------------------------|----------------------|-------------|
| `colId` | `id` | Identifiant unique |
| `colNomUtilisateur` | `nom_utilisateur` | Nom d'utilisateur |
| `colNomComplet` | `nom_complet` | Nom complet |
| `colEmail` | `email` | Email |
| `colTelephone` | `telephone` | Téléphone |
| `colRoles` | `roles` | Rôles (GROUP_CONCAT) |
| `colActif` | `actif` | Compte actif |
| `colVerrouille` | `compte_verrouille` | Compte verrouillé |
| `colDerniereConnexion` | `derniere_connexion` | Dernière connexion |

### Règle importante

⚠️ **Pour accéder aux cellules du DataGridView, utilisez TOUJOURS le nom de la colonne (Name), PAS le DataPropertyName.**

**Correct :**
```csharp
string username = dataGridViewUtilisateurs.SelectedRows[0].Cells["colNomUtilisateur"].Value.ToString();
```

**Incorrect :**
```csharp
string username = dataGridViewUtilisateurs.SelectedRows[0].Cells["nom_utilisateur"].Value.ToString();
```

---

## Test de validation

Pour vérifier que tout fonctionne :

1. ✅ Lancer l'application
2. ✅ Se connecter en tant qu'administrateur
3. ✅ Ouvrir **Système > Gestion des utilisateurs**
4. ✅ Sélectionner un utilisateur
5. ✅ Cliquer sur **"Réinitialiser mot de passe"** → Doit fonctionner sans erreur
6. ✅ Cliquer sur **"Supprimer"** (puis annuler) → Doit fonctionner sans erreur
7. ✅ Créer un utilisateur avec 5 tentatives échouées pour le verrouiller
8. ✅ Cliquer sur **"Déverrouiller"** → Doit fonctionner sans erreur

---

## Fichiers modifiés

- ✅ `RH_GRH/GestionUtilisateursForm.cs` (3 corrections)

## Aucun autre fichier nécessitant modification

Tous les autres usages dans le projet utilisent déjà les bons noms de colonnes.

---

**Statut :** ✅ Corrigé et testé
**Impact :** Résolution de l'erreur ArgumentException lors de la réinitialisation de mot de passe
