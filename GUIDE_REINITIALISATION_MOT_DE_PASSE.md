# Guide de Réinitialisation de Mot de Passe - Gestion Moderne RH
**Version 1.1.5 | Date: 16 Mars 2026**

---

## Table des matières
1. [Vue d'ensemble](#vue-densemble)
2. [Pour les utilisateurs](#pour-les-utilisateurs)
3. [Pour les administrateurs](#pour-les-administrateurs)
4. [Configuration technique](#configuration-technique)
5. [Dépannage](#dépannage)

---

## Vue d'ensemble

Le système de réinitialisation de mot de passe permet aux administrateurs de réinitialiser les mots de passe oubliés et force les utilisateurs à changer leur mot de passe par défaut lors de leur première connexion.

### Fonctionnalités principales

✅ **Réinitialisation par l'administrateur** - Les admins peuvent réinitialiser n'importe quel mot de passe
✅ **Changement obligatoire** - Première connexion force le changement de mot de passe
✅ **Sécurité renforcée** - Validation stricte de la complexité des mots de passe
✅ **Traçabilité** - Tous les changements sont enregistrés dans les logs
✅ **Contact administrateur** - Lien "Mot de passe oublié" affiche les coordonnées de l'admin

---

## Pour les utilisateurs

### J'ai oublié mon mot de passe

1. Sur l'écran de connexion, cliquez sur le lien **"Mot de passe oublié ?"**
2. Une fenêtre s'affiche avec les coordonnées de l'administrateur système
3. Contactez l'administrateur par email ou téléphone
4. L'administrateur réinitialisera votre mot de passe

### Première connexion

Lors de votre première connexion, vous devrez obligatoirement changer votre mot de passe :

1. Connectez-vous avec vos identifiants fournis par l'administrateur
   - **Nom d'utilisateur** : Fourni par votre admin
   - **Mot de passe par défaut** : `RHPlus2026!`

2. Un formulaire de changement de mot de passe s'affiche automatiquement

3. Saisissez votre nouveau mot de passe (il doit respecter les règles de sécurité)

4. Confirmez votre nouveau mot de passe

5. Cliquez sur **"Valider et se connecter"**

### Règles de sécurité du mot de passe

Votre nouveau mot de passe doit contenir :

- ✅ Au moins **8 caractères**
- ✅ Au moins **une lettre majuscule** (A-Z)
- ✅ Au moins **une lettre minuscule** (a-z)
- ✅ Au moins **un chiffre** (0-9)
- ✅ Au moins **un caractère spécial** (!@#$%^&*...)

❌ **Vous ne pouvez PAS** utiliser le mot de passe par défaut `RHPlus2026!`

#### Exemples de mots de passe valides

- `MonMotDePasse2026!`
- `Securite@RH2026`
- `GestionRH#2026`

---

## Pour les administrateurs

### Accéder à la gestion des utilisateurs

1. Connectez-vous avec votre compte administrateur
2. Dans le menu principal, cliquez sur **Système**
3. Sélectionnez **Gestion des utilisateurs**

### Réinitialiser le mot de passe d'un utilisateur

#### Via l'interface graphique (Recommandé)

1. Ouvrez **Système > Gestion des utilisateurs**
2. Sélectionnez l'utilisateur dans la liste
3. Cliquez sur le bouton **"Réinitialiser mot de passe"**
4. Une confirmation s'affiche avec le nouveau mot de passe par défaut : `RHPlus2026!`
5. Cliquez sur **"Oui"** pour confirmer
6. Un message de succès s'affiche avec le mot de passe
7. **IMPORTANT** : Communiquez ce mot de passe à l'utilisateur de manière sécurisée

#### Ce qui se passe lors de la réinitialisation

- ✅ Le mot de passe est défini sur `RHPlus2026!`
- ✅ Le compteur de tentatives échouées est remis à zéro
- ✅ Le compte est déverrouillé (si verrouillé)
- ✅ Le flag "première connexion" est activé
- ✅ L'utilisateur devra changer son mot de passe à la prochaine connexion
- ✅ L'action est enregistrée dans les logs d'audit

### Créer un nouvel utilisateur

Lors de la création d'un utilisateur :

1. Ouvrez **Système > Gestion des utilisateurs**
2. Cliquez sur **"Ajouter"**
3. Remplissez les informations de l'utilisateur
4. Sélectionnez au moins un rôle
5. Cliquez sur **"Enregistrer"**

Un message de succès affichera le **mot de passe par défaut** (`RHPlus2026!`) que vous devez communiquer à l'utilisateur.

### Déverrouiller un compte

Si un utilisateur a dépassé 5 tentatives de connexion échouées :

1. Ouvrez **Système > Gestion des utilisateurs**
2. Sélectionnez l'utilisateur (vous verrez un indicateur de verrouillage)
3. Cliquez sur **"Déverrouiller"**
4. Le compte est immédiatement déverrouillé

💡 **Astuce** : Vous pouvez également réinitialiser le mot de passe, ce qui déverrouillera automatiquement le compte.

### Consulter les logs de réinitialisation

1. Ouvrez **Système > Visualisation des logs**
2. Filtrez par action : **"RESET_PASSWORD"**
3. Vous verrez l'historique de toutes les réinitialisations avec :
   - Date et heure
   - Administrateur qui a effectué l'action
   - Utilisateur concerné
   - Résultat de l'opération

---

## Configuration technique

### Structure de la base de données

#### Colonnes de la table `utilisateurs`

| Colonne | Type | Description |
|---------|------|-------------|
| `premier_connexion` | BOOLEAN | TRUE si l'utilisateur doit changer son mot de passe |
| `mot_de_passe_par_defaut` | VARCHAR(20) | Stocke le mot de passe par défaut (pour référence admin) |
| `tentatives_echec` | INT | Nombre de tentatives de connexion échouées |
| `compte_verrouille` | BOOLEAN | TRUE si le compte est verrouillé |

### Migration de la base de données

Pour appliquer les modifications nécessaires, exécutez le script :

```bash
Database/migration_reset_password_fix.sql
```

Ce script :
- ✅ Ajoute les colonnes manquantes (si nécessaire)
- ✅ Met à jour les données existantes
- ✅ Affiche des statistiques
- ✅ Enregistre la migration dans les logs

### Mot de passe par défaut centralisé

Le mot de passe par défaut est défini dans :

📁 **Fichier** : `RH_GRH/Auth/PasswordGenerator.cs`

```csharp
public const string DEFAULT_PASSWORD = "RHPlus2026!";
```

Pour changer le mot de passe par défaut :
1. Modifiez la constante `DEFAULT_PASSWORD`
2. Recompilez l'application
3. Réinitialisez les utilisateurs qui utilisent l'ancien mot de passe

---

## Dépannage

### Problème : L'utilisateur ne peut pas se connecter après réinitialisation

**Cause** : Mauvaise saisie du mot de passe par défaut

**Solution** :
1. Vérifiez que l'utilisateur saisit exactement : `RHPlus2026!`
2. Attention à la casse (R et P majuscules)
3. Le point d'exclamation est obligatoire
4. Pas d'espaces avant ou après

### Problème : Le formulaire de changement de mot de passe ne s'affiche pas

**Cause** : Le flag `premier_connexion` n'est pas activé

**Solution via SQL** :
```sql
UPDATE utilisateurs
SET premier_connexion = TRUE
WHERE nom_utilisateur = 'nom_utilisateur_problematique';
```

**Solution via interface** :
1. Réinitialisez le mot de passe de l'utilisateur
2. Cela réactivera automatiquement le flag

### Problème : Le mot de passe ne respecte pas les règles de sécurité

**Erreur** : "Le mot de passe doit contenir..."

**Solution** : Assurez-vous que le mot de passe contient :
- Minimum 8 caractères
- 1 majuscule (A-Z)
- 1 minuscule (a-z)
- 1 chiffre (0-9)
- 1 caractère spécial (!@#$%^&*...)

### Problème : Le compte est verrouillé après 5 tentatives

**Solution** :
1. Utilisez le bouton **"Déverrouiller"** dans la gestion des utilisateurs
2. OU réinitialisez le mot de passe (déverrouille automatiquement)

### Problème : Le lien "Mot de passe oublié" n'affiche pas les coordonnées

**Cause** : Aucun administrateur actif avec email/téléphone

**Solution** :
1. Ajoutez un email et téléphone au compte administrateur principal
2. Vérifiez que le compte admin est actif
3. Vérifiez qu'il a bien le rôle "Administrateur"

### Problème : Erreur lors de la réinitialisation

**Erreur** : "Erreur lors de la réinitialisation : ..."

**Solutions possibles** :
1. Vérifiez la connexion à la base de données
2. Vérifiez que la table `utilisateurs` existe
3. Vérifiez que les colonnes `premier_connexion` et `mot_de_passe_par_defaut` existent
4. Exécutez le script de migration : `migration_reset_password_fix.sql`

### Vérifier la structure de la base de données

```sql
-- Vérifier que les colonnes existent
SELECT COLUMN_NAME, COLUMN_TYPE, COLUMN_DEFAULT
FROM information_schema.COLUMNS
WHERE TABLE_SCHEMA = 'gmp_rh_gestion'
  AND TABLE_NAME = 'utilisateurs'
  AND COLUMN_NAME IN ('premier_connexion', 'mot_de_passe_par_defaut');
```

---

## Support technique

Pour toute question ou problème non résolu :

1. **Consultez les logs** : Système > Visualisation des logs
2. **Contactez le support technique** : Vérifiez la documentation principale
3. **Créez un ticket** : Incluez les informations suivantes :
   - Version de l'application
   - Message d'erreur exact
   - Capture d'écran si possible
   - Étapes pour reproduire le problème

---

## Historique des modifications

| Version | Date | Modifications |
|---------|------|---------------|
| 1.1.5 | 16/03/2026 | Correction système de réinitialisation, centralisation mot de passe par défaut |
| 1.1.4 | 11/02/2026 | Ajout système de première connexion |
| 1.1.3 | 05/02/2026 | Implémentation système d'authentification |

---

**Document créé par le système Gestion Moderne RH**
**© 2026 - Tous droits réservés**
