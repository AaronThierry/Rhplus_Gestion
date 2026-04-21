# Attribution de Rôles - bamikou.traore et bibiane.traore

**Date :** 16 Mars 2026
**Utilisateurs concernés :** bamikou.traore, bibiane.traore

---

## Choix du rôle à attribuer

Vous avez deux options selon le niveau d'accès souhaité :

### Option 1 : Gestionnaire RH (Recommandé pour RH)
**Script :** `Database/add_role_gestionnaire_rh_bamikou_bibiane.sql`

✅ **Accès inclus :**
- Gestion complète du personnel (ajout, modification, consultation)
- Gestion complète de la paie (traitement, export bulletins)
- Gestion des paramètres RH (catégories, services, directions, indemnités, charges)
- Import en masse d'employés

❌ **Limitations :**
- Pas d'accès à la gestion des utilisateurs système
- Pas d'accès aux rôles et permissions
- Pas d'accès aux logs système
- Pas de suppression d'employés
- Pas de gestion des entreprises

**Idéal pour :** Responsables RH, Gestionnaires de paie

---

### Option 2 : Administrateur (Accès complet)
**Script :** `Database/add_role_administrateur_bamikou_bibiane.sql`

✅ **Accès complet à tout le système :**
- Toutes les fonctionnalités du Gestionnaire RH
- Gestion des utilisateurs (créer, modifier, supprimer, réinitialiser mots de passe)
- Gestion des rôles et permissions
- Consultation des logs système
- Configuration système
- Gestion des entreprises
- Suppression d'employés

**Idéal pour :** Administrateurs système, Directeurs RH

---

## Exécution du script

### Méthode 1 : Via MySQL Workbench (Recommandé)

1. Ouvrez **MySQL Workbench**
2. Connectez-vous à votre serveur MySQL
3. Ouvrez le script choisi :
   - Gestionnaire RH : `Database/add_role_gestionnaire_rh_bamikou_bibiane.sql`
   - Administrateur : `Database/add_role_administrateur_bamikou_bibiane.sql`
4. Cliquez sur l'icône **⚡ Execute** (éclair) ou appuyez sur `Ctrl+Shift+Enter`
5. Vérifiez les résultats dans l'onglet **Output**

### Méthode 2 : Via ligne de commande

```bash
# Pour Gestionnaire RH
mysql -u root -p gmp_rh_gestion < "Database/add_role_gestionnaire_rh_bamikou_bibiane.sql"

# OU pour Administrateur
mysql -u root -p gmp_rh_gestion < "Database/add_role_administrateur_bamikou_bibiane.sql"
```

### Méthode 3 : Via phpMyAdmin

1. Ouvrez **phpMyAdmin** dans votre navigateur
2. Sélectionnez la base de données `gmp_rh_gestion`
3. Cliquez sur l'onglet **SQL**
4. Ouvrez le fichier script dans un éditeur de texte
5. Copiez tout le contenu
6. Collez-le dans la zone SQL de phpMyAdmin
7. Cliquez sur **Exécuter**

---

## Vérification après exécution

### 1. Vérifier via SQL

```sql
SELECT
    u.nom_utilisateur,
    u.nom_complet,
    GROUP_CONCAT(r.nom_role SEPARATOR ', ') AS roles
FROM utilisateurs u
LEFT JOIN utilisateur_roles ur ON u.id = ur.utilisateur_id
LEFT JOIN roles r ON ur.role_id = r.id
WHERE u.nom_utilisateur IN ('bamikou.traore', 'bibiane.traore')
GROUP BY u.id;
```

**Résultat attendu :**

| nom_utilisateur | nom_complet | roles |
|----------------|-------------|-------|
| bamikou.traore | Bamikou Traoré | Gestionnaire RH (ou Administrateur) |
| bibiane.traore | Bibiane Traoré | Gestionnaire RH (ou Administrateur) |

### 2. Vérifier via l'application

1. Lancez l'application **Gestion Moderne RH**
2. Connectez-vous en tant qu'**admin**
3. Allez dans **Système > Gestion des utilisateurs**
4. Recherchez **bamikou.traore** et **bibiane.traore**
5. Vérifiez que la colonne **Rôles** affiche le bon rôle

### 3. Tester la connexion

1. Déconnectez-vous de l'application
2. Connectez-vous avec **bamikou.traore** ou **bibiane.traore**
3. Vérifiez que les menus correspondent au rôle attribué

---

## Permissions par rôle

### Gestionnaire RH - Menus disponibles

```
📊 Accueil / Tableau de bord

👥 Personnel
  ├─ Ajouter un employé
  ├─ Modifier un employé
  ├─ Consulter les employés
  └─ Importer des employés

💰 Salaire
  ├─ Gestion Salaire Journalier
  ├─ Gestion Salaire Horaire
  ├─ Saisie Paye en Lot
  ├─ Génération Bulletins Individuels
  ├─ Génération Bulletins Consolidés
  └─ Export Bulletins

⚙️ Administration
  ├─ Gestion Catégories
  ├─ Gestion Services
  ├─ Gestion Directions
  ├─ Gestion Charges
  └─ Gestion Indemnités
```

### Administrateur - Menus disponibles

```
Tous les menus du Gestionnaire RH +

🔧 Système
  ├─ Gestion des Utilisateurs
  ├─ Gestion des Rôles et Permissions
  ├─ Visualisation des Logs
  └─ Configuration Système

⚙️ Administration (accès complet)
  └─ Gestion des Entreprises

👥 Personnel (accès complet)
  └─ Supprimer un employé
```

---

## Actions post-attribution

### Si vous avez choisi Gestionnaire RH

✅ **Recommandations :**
- Informez les utilisateurs de leur nouvel accès
- Communiquez-leur leurs identifiants
- Expliquez les fonctionnalités disponibles
- Formez-les si nécessaire sur les processus de paie

✅ **Limitations à communiquer :**
- Ils ne peuvent pas créer/supprimer d'autres utilisateurs
- Ils ne peuvent pas modifier les rôles
- Ils ne peuvent pas accéder aux logs système
- Ils ne peuvent pas supprimer d'employés (seulement les désactiver)

### Si vous avez choisi Administrateur

⚠️ **Important :**
- Ces utilisateurs ont un accès COMPLET au système
- Ils peuvent créer, modifier et supprimer des utilisateurs
- Ils peuvent modifier les rôles et permissions
- Ils ont accès à toutes les données sensibles

✅ **Recommandations :**
- Assurez-vous que ces utilisateurs sont de confiance
- Informez-les de leurs responsabilités
- Documentez qui a un accès administrateur
- Surveillez les logs pour les actions critiques

---

## Retirer ou changer le rôle

### Pour changer le rôle via l'interface

1. Connectez-vous en tant qu'**admin**
2. Allez dans **Système > Gestion des utilisateurs**
3. Sélectionnez l'utilisateur
4. Cliquez sur **Modifier**
5. Décochez les rôles actuels et cochez le nouveau rôle
6. Enregistrez

### Pour changer le rôle via SQL

```sql
-- Supprimer tous les rôles actuels
DELETE ur FROM utilisateur_roles ur
INNER JOIN utilisateurs u ON ur.utilisateur_id = u.id
WHERE u.nom_utilisateur = 'bamikou.traore';

-- Attribuer le nouveau rôle (exemple: Assistant RH)
INSERT INTO utilisateur_roles (utilisateur_id, role_id)
SELECT u.id, r.id
FROM utilisateurs u
CROSS JOIN roles r
WHERE u.nom_utilisateur = 'bamikou.traore'
  AND r.nom_role = 'Assistant RH';
```

---

## En cas de problème

### L'utilisateur n'existe pas

**Erreur :** Aucun résultat dans la vérification

**Solution :** Créer d'abord les utilisateurs via l'interface ou SQL :

```sql
-- Exemple de création d'utilisateur
INSERT INTO utilisateurs (
    nom_utilisateur,
    mot_de_passe_hash,
    nom_complet,
    email,
    actif,
    premier_connexion,
    mot_de_passe_par_defaut
) VALUES (
    'bamikou.traore',
    '$2a$11$7rLSvRVyTfIapPwVu.bEy.KqV5pDfKCrRfKpG/QNVQbW7VqkN6qZG', -- RHPlus2026!
    'Bamikou Traoré',
    'bamikou.traore@entreprise.com',
    1,
    TRUE,
    'RHPlus2026!'
);
```

### Le rôle n'existe pas

**Erreur :** Rôle "Gestionnaire RH" introuvable

**Solution :** Exécuter le script de création des rôles :

```bash
mysql -u root -p gmp_rh_gestion < "Database/create_auth_tables.sql"
```

### Le script ne s'exécute pas

**Vérifications :**
1. La base de données `gmp_rh_gestion` existe
2. Les tables `utilisateurs`, `roles`, `utilisateur_roles` existent
3. Vous avez les droits d'exécution SQL
4. La syntaxe du fichier n'a pas été modifiée

---

## Logs générés

Après l'exécution, les actions suivantes sont enregistrées dans `logs_activite` :

```sql
SELECT * FROM logs_activite
WHERE action IN ('CHANGE_ROLE', 'CHANGE_ROLE_ADMIN')
ORDER BY date_action DESC
LIMIT 10;
```

Vous devriez voir :
- Attribution du rôle pour bamikou.traore
- Attribution du rôle pour bibiane.traore

---

## Support

Pour toute question ou problème :

1. Vérifiez la structure de la base de données
2. Consultez les logs d'activité
3. Testez avec le compte admin d'abord
4. Référez-vous à la documentation principale

---

**Rappel :** Choisissez le rôle approprié selon les besoins :
- **Gestionnaire RH** pour un accès RH complet mais limité au système
- **Administrateur** pour un accès total sans restriction

**Fichiers de script :**
- `Database/add_role_gestionnaire_rh_bamikou_bibiane.sql`
- `Database/add_role_administrateur_bamikou_bibiane.sql`
