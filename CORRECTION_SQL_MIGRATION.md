# 🔧 Correction Script SQL - Erreur de Syntaxe Résolue

## ⚠️ Erreur Rencontrée

Vous avez obtenu cette erreur SQL :

```
SQL (1064): You have an error in your SQL syntax; check the manual that corresponds to your MySQL server version for the right syntax to use near 'IF NOT EXISTS premier_connexion BOOLEAN DEFAULT TRUE...'
```

## 📋 Explication du Problème

La syntaxe `ADD COLUMN IF NOT EXISTS` **n'est pas supportée par toutes les versions de MySQL**.

- ✅ MySQL 8.0.29+ : Supporte cette syntaxe
- ❌ MySQL 5.7 et versions antérieures : Ne supporte PAS cette syntaxe
- ❌ MariaDB < 10.5 : Ne supporte PAS cette syntaxe

## ✅ Solution Appliquée

Le script a été corrigé pour utiliser une méthode compatible avec **toutes les versions de MySQL** :

**AVANT (incompatible)** :
```sql
ALTER TABLE utilisateurs
ADD COLUMN IF NOT EXISTS premier_connexion BOOLEAN DEFAULT TRUE;
```

**APRÈS (compatible)** :
```sql
SET @sql = (
    SELECT IF(COUNT(*) = 0,
        'ALTER TABLE utilisateurs ADD COLUMN premier_connexion BOOLEAN DEFAULT TRUE',
        'SELECT ''Colonne existe déjà'' AS message')
    FROM information_schema.COLUMNS
    WHERE TABLE_SCHEMA = DATABASE()
      AND TABLE_NAME = 'utilisateurs'
      AND COLUMN_NAME = 'premier_connexion'
);

PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;
```

Cette méthode :
1. ✅ Vérifie si la colonne existe déjà
2. ✅ Crée la colonne seulement si elle n'existe pas
3. ✅ Affiche un message si la colonne existe déjà
4. ✅ Fonctionne sur **toutes les versions de MySQL**

---

## 🚀 Instructions d'Application

### Méthode 1 : Via MySQL Workbench (Recommandé)

1. **Ouvrir MySQL Workbench**

2. **Se connecter à votre base de données**

3. **Ouvrir le script SQL** :
   - Menu **File** → **Open SQL Script**
   - Sélectionner : `C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion\Database\add_premier_connexion_column.sql`

4. **Exécuter le script** :
   - Cliquer sur l'icône ⚡ **Execute** (ou **Ctrl+Shift+Enter**)

5. **Vérifier les résultats** :
   - Vous devriez voir :
     ```
     ✓ Colonne premier_connexion ajoutée
     ✓ Colonne mot_de_passe_par_defaut ajoutée
     ✓ Utilisateurs existants marqués comme déjà connectés
     ```

### Méthode 2 : Via Ligne de Commande

```bash
# Se connecter à MySQL
mysql -u votre_utilisateur -p votre_base_de_donnees

# Exécuter le script
source "C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion\Database\add_premier_connexion_column.sql"

# Ou en une seule ligne (Windows CMD)
mysql -u votre_utilisateur -p votre_base_de_donnees < "C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion\Database\add_premier_connexion_column.sql"
```

---

## ✅ Vérification Manuelle (Optionnel)

Si vous voulez vérifier manuellement que tout a fonctionné :

```sql
-- 1. Vérifier que les colonnes ont été ajoutées
SHOW COLUMNS FROM utilisateurs LIKE 'premier_connexion';
SHOW COLUMNS FROM utilisateurs LIKE 'mot_de_passe_par_defaut';

-- 2. Vérifier les valeurs des utilisateurs existants
SELECT
    id,
    nom_utilisateur,
    nom_complet,
    premier_connexion,
    mot_de_passe_par_defaut,
    actif
FROM utilisateurs;

-- Résultat attendu pour utilisateurs existants :
-- premier_connexion = 0 (FALSE)
-- mot_de_passe_par_defaut = NULL
```

**Résultat Attendu** :

| Field | Type | Null | Key | Default | Extra |
|-------|------|------|-----|---------|-------|
| premier_connexion | tinyint(1) | YES | | TRUE | |
| mot_de_passe_par_defaut | varchar(20) | YES | | NULL | |

---

## 🧪 Test Complet Après Migration

Une fois le script appliqué, testez le système complet :

### Test 1 : Créer un Nouvel Utilisateur

1. **Lancer l'application RH_GRH.exe**

2. **Se connecter** avec un compte **Super Administrateur**

3. **Menu Administration** → **Gestion des Utilisateurs**

4. **Cliquer sur Ajouter**

5. **Remplir les informations** :
   - Nom complet : `Test Utilisateur`
   - Nom d'utilisateur : `testuser`
   - Email : `test@example.com` (optionnel)
   - Téléphone : (optionnel)
   - Cocher au moins un rôle

6. **Cliquer sur Enregistrer**

**Résultat Attendu** :
```
┌─────────────────────────────────────────────┐
│  ✓ Succès                                   │
├─────────────────────────────────────────────┤
│                                             │
│  L'utilisateur 'testuser' a été créé        │
│  avec succès !                              │
│                                             │
│  Mot de passe par défaut : RHPlus2026!      │
│                                             │
│  IMPORTANT : L'utilisateur devra changer    │
│  ce mot de passe lors de sa première        │
│  connexion.                                 │
│  Veuillez communiquer ce mot de passe de    │
│  manière sécurisée.                         │
│                                             │
│               [OK]                          │
└─────────────────────────────────────────────┘
```

### Test 2 : Vérifier dans la Base de Données

```sql
SELECT
    nom_utilisateur,
    nom_complet,
    premier_connexion,
    mot_de_passe_par_defaut
FROM utilisateurs
WHERE nom_utilisateur = 'testuser';
```

**Résultat Attendu** :
| nom_utilisateur | nom_complet | premier_connexion | mot_de_passe_par_defaut |
|----------------|-------------|-------------------|-------------------------|
| testuser | Test Utilisateur | 1 (TRUE) | RHPlus2026! |

### Test 3 : Première Connexion

1. **Se déconnecter** de l'application

2. **Se reconnecter** avec :
   - Nom d'utilisateur : `testuser`
   - Mot de passe : `RHPlus2026!`

**Résultat Attendu** :
```
┌─────────────────────────────────────────────┐
│  ℹ Information                              │
├─────────────────────────────────────────────┤
│                                             │
│  Première connexion détectée.               │
│  Changement de mot de passe requis.         │
│                                             │
│               [OK]                          │
└─────────────────────────────────────────────┘
```

3. **Formulaire de changement de mot de passe s'affiche**

4. **Saisir un nouveau mot de passe** :
   - Nouveau mot de passe : `MonNouveauMdp2026!`
   - Confirmation : `MonNouveauMdp2026!`

5. **Cliquer sur Changer**

**Résultat Attendu** :
```
┌─────────────────────────────────────────────┐
│  ✓ Succès                                   │
├─────────────────────────────────────────────┤
│                                             │
│  Mot de passe changé avec succès !          │
│  Vous allez être déconnecté.                │
│  Veuillez vous reconnecter avec votre       │
│  nouveau mot de passe.                      │
│                                             │
│               [OK]                          │
└─────────────────────────────────────────────┘
```

6. **Application retourne au formulaire de connexion**

7. **Se reconnecter** avec :
   - Nom d'utilisateur : `testuser`
   - Mot de passe : `MonNouveauMdp2026!` (le nouveau)

8. **L'application s'ouvre normalement**

### Test 4 : Vérifier dans la Base de Données Après Changement

```sql
SELECT
    nom_utilisateur,
    nom_complet,
    premier_connexion,
    mot_de_passe_par_defaut
FROM utilisateurs
WHERE nom_utilisateur = 'testuser';
```

**Résultat Attendu** :
| nom_utilisateur | nom_complet | premier_connexion | mot_de_passe_par_defaut |
|----------------|-------------|-------------------|-------------------------|
| testuser | Test Utilisateur | 0 (FALSE) | NULL |

✅ `premier_connexion` est maintenant à **FALSE**
✅ `mot_de_passe_par_defaut` est maintenant **NULL**
✅ Le mot de passe hashé dans `mot_de_passe_hash` a été mis à jour

---

## 🔄 Réinitialiser un Utilisateur (Pour Test)

Si vous voulez forcer un utilisateur existant à changer son mot de passe :

```sql
UPDATE utilisateurs
SET premier_connexion = TRUE,
    mot_de_passe_hash = SHA2('RHPlus2026!', 256),
    mot_de_passe_par_defaut = 'RHPlus2026!'
WHERE nom_utilisateur = 'nom_utilisateur_test';
```

⚠️ **Attention** : Cela réinitialisera le mot de passe de l'utilisateur à `RHPlus2026!` et le forcera à le changer à la prochaine connexion.

---

## ❓ FAQ - Questions Fréquentes

### Q1 : Que se passe-t-il si j'exécute le script plusieurs fois ?
**R** : ✅ Aucun problème ! Le script vérifie si les colonnes existent déjà. S'ils existent, il affiche un message et ne fait rien.

### Q2 : Les utilisateurs existants seront-ils affectés ?
**R** : ❌ Non, le script met automatiquement `premier_connexion = FALSE` pour tous les utilisateurs existants. Ils ne seront pas forcés de changer leur mot de passe.

### Q3 : Puis-je modifier le mot de passe par défaut ?
**R** : Oui, mais vous devrez modifier la valeur dans deux endroits :
1. `RH_GRH\Auth\PasswordGenerator.cs` : ligne 12 (`DEFAULT_PASSWORD`)
2. Recompiler l'application

### Q4 : Comment désactiver le changement obligatoire pour un utilisateur ?
**R** : Exécutez cette requête SQL :
```sql
UPDATE utilisateurs
SET premier_connexion = FALSE
WHERE nom_utilisateur = 'nom_utilisateur';
```

### Q5 : Le mot de passe par défaut est-il sécurisé ?
**R** : Le mot de passe par défaut `RHPlus2026!` est **temporaire**. L'utilisateur **DOIT** le changer immédiatement lors de sa première connexion. Le nouveau mot de passe doit respecter les règles strictes :
- Minimum 8 caractères
- Au moins une majuscule
- Au moins une minuscule
- Au moins un chiffre
- Au moins un caractère spécial
- Ne peut pas être le mot de passe par défaut

---

## 🎯 Checklist de Validation

Après avoir appliqué le script, vérifiez :

- [ ] ✅ Script SQL exécuté sans erreur
- [ ] ✅ Colonnes `premier_connexion` et `mot_de_passe_par_defaut` créées
- [ ] ✅ Utilisateurs existants ont `premier_connexion = FALSE`
- [ ] ✅ Application compile et se lance
- [ ] ✅ Création d'un nouvel utilisateur affiche le mot de passe par défaut
- [ ] ✅ Première connexion déclenche le formulaire de changement
- [ ] ✅ Changement de mot de passe fonctionne
- [ ] ✅ Reconnexion avec nouveau mot de passe réussit
- [ ] ✅ `premier_connexion` passe à FALSE après changement

---

## 📞 Besoin d'Aide ?

Si vous rencontrez des problèmes :

1. **Vérifier votre version MySQL** :
   ```sql
   SELECT VERSION();
   ```

2. **Vérifier que la base de données est sélectionnée** :
   ```sql
   SELECT DATABASE();
   ```
   Si NULL, sélectionnez votre base :
   ```sql
   USE nom_de_votre_base;
   ```

3. **Vérifier les privilèges** :
   ```sql
   SHOW GRANTS FOR CURRENT_USER();
   ```
   Vous devez avoir le privilège `ALTER` sur la table `utilisateurs`.

---

**Date** : 13 février 2026
**Version** : 1.1.4
**Statut** : ✅ Script SQL Corrigé et Compatible
**Action requise** : Exécuter le script SQL mis à jour

---

## 🎉 Résumé

Le script SQL a été corrigé pour être compatible avec toutes les versions de MySQL. Vous pouvez maintenant :

1. ✅ Exécuter le script sans erreur
2. ✅ Créer des utilisateurs avec mot de passe par défaut
3. ✅ Forcer le changement à la première connexion
4. ✅ Tester le workflow complet

Bonne utilisation du système ! 🚀
