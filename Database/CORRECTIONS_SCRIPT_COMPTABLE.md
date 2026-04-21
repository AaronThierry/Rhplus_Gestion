# Corrections du Script add_role_comptable.sql

## Date
13 février 2026

## Problèmes Rencontrés

### Erreur 1: Unknown column 'actif' in 'field list'
**Cause**: La table `roles` n'a pas de colonne `actif` ni `date_modification`

**Structure réelle de la table `roles`**:
```sql
CREATE TABLE roles (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nom_role VARCHAR(50) UNIQUE NOT NULL,
    description TEXT,
    niveau_acces INT DEFAULT 1,
    date_creation DATETIME DEFAULT CURRENT_TIMESTAMP
)
```

**Solution**: Remplacé les colonnes inexistantes par les colonnes correctes
```sql
-- AVANT (incorrect)
INSERT INTO roles (nom_role, description, actif, date_creation, date_modification)
VALUES ('Comptable', '...', 1, NOW(), NOW())

-- APRÈS (correct)
INSERT INTO roles (nom_role, description, niveau_acces, date_creation)
VALUES ('Comptable', '...', 2, NOW())
```

---

### Erreur 2: Unknown column 'p.code_permission' in 'where clause'
**Cause**: La table `permissions` utilise `nom_permission` et non `code_permission`

**Structure réelle de la table `permissions`**:
```sql
CREATE TABLE permissions (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nom_permission VARCHAR(100) UNIQUE NOT NULL,
    description TEXT,
    module VARCHAR(50) NOT NULL,
    action VARCHAR(50) NOT NULL,
    date_creation DATETIME DEFAULT CURRENT_TIMESTAMP
)
```

**Solution**: Remplacé `code_permission` par `nom_permission`
```sql
-- AVANT (incorrect)
WHERE p.code_permission IN ('SALAIRE_VIEW', ...)

-- APRÈS (correct)
WHERE p.nom_permission IN ('SALAIRE_VIEW', ...)
```

---

### Problème 3: Permissions détaillées inexistantes
**Cause**: Les permissions détaillées (SURSALAIRE_*, BULLETIN_*, etc.) n'existent que si les scripts `reset_roles_permissions.sql` ont été exécutés

**Permissions de base** (toujours présentes):
- `SALAIRE_VIEW`
- `SALAIRE_PROCESS`
- `SALAIRE_EDIT`
- `SALAIRE_EXPORT`
- `PERSONNEL_VIEW`

**Permissions détaillées** (nécessitent reset_roles_permissions.sql):
- `SURSALAIRE_*` (4 permissions)
- `SALAIRE_HORAIRE_*` (5 permissions)
- `SALAIRE_JOURNALIER_*` (5 permissions)
- `BULLETIN_*` (4 permissions)
- `EMPLOYE_VIEW` (1 permission)

**Solution**: Script adapté pour fonctionner dans les deux cas
- Si permissions détaillées existent → toutes sont ajoutées
- Si permissions détaillées n'existent pas → seules les permissions de base sont ajoutées
- Pas d'erreur dans les deux cas grâce au `WHERE IN` qui ignore les valeurs inexistantes

---

## Toutes les Corrections Appliquées

### 1. Colonnes de la table `roles`
```sql
-- Ligne 14-18
INSERT INTO roles (nom_role, description, niveau_acces, date_creation)
SELECT 'Comptable',
       'Accès complet à la gestion des salaires et bulletins de paie. Aucun accès aux autres sections.',
       2,
       NOW()
WHERE NOT EXISTS (
    SELECT 1 FROM roles WHERE nom_role = 'Comptable'
);
```

### 2. Colonnes de la table `permissions`
```sql
-- Ligne 49-89
INSERT INTO role_permissions (role_id, permission_id)
SELECT @comptable_role_id, p.id
FROM permissions p
WHERE p.nom_permission IN (
    'SALAIRE_VIEW',
    'SALAIRE_PROCESS',
    ...
);
```

### 3. Requête d'affichage des permissions
```sql
-- Ligne 92-101
SELECT
    r.nom_role as 'Rôle',
    p.nom_permission as 'Permission',
    p.description as 'Description',
    p.module as 'Module'
FROM role_permissions rp
JOIN roles r ON rp.role_id = r.id
JOIN permissions p ON rp.permission_id = p.id
WHERE r.nom_role = 'Comptable'
ORDER BY p.module, p.nom_permission;
```

### 4. Requête de statistiques
```sql
-- Ligne 109-115
SELECT
    'Comptable' as 'Rôle créé',
    COUNT(DISTINCT rp.permission_id) as 'Nombre de permissions',
    GROUP_CONCAT(DISTINCT p.module ORDER BY p.module SEPARATOR ', ') as 'Modules'
FROM role_permissions rp
JOIN permissions p ON rp.permission_id = p.id
WHERE rp.role_id = @comptable_role_id;
```

### 5. Requête de vérification finale
```sql
-- Ligne 149-156
SELECT
    id,
    nom_role,
    description,
    niveau_acces,
    date_creation
FROM roles
WHERE nom_role = 'Comptable';
```

---

## Résultat Final

### Le script corrigé peut maintenant:
✅ S'exécuter sans erreur sur une base avec structure de base (`create_auth_tables.sql`)
✅ S'exécuter sans erreur sur une base avec permissions détaillées (`reset_roles_permissions.sql`)
✅ Attribuer automatiquement toutes les permissions disponibles liées aux salaires
✅ Ignorer les permissions qui n'existent pas encore
✅ Afficher correctement les informations du rôle créé

### Permissions minimales accordées (structure de base):
- `SALAIRE_VIEW` - Consulter les salaires
- `SALAIRE_PROCESS` - Traiter les salaires
- `SALAIRE_EDIT` - Modifier les salaires
- `SALAIRE_EXPORT` - Exporter les bulletins
- `PERSONNEL_VIEW` - Consulter les employés (lecture seule)

**Total**: 5 permissions minimum

### Permissions maximales accordées (avec reset_roles_permissions):
- 23 permissions couvrant tous les aspects de la gestion des salaires

---

## Commandes de Test

### Vérifier la structure de la table roles
```sql
SHOW COLUMNS FROM roles;
```

### Vérifier la structure de la table permissions
```sql
SHOW COLUMNS FROM permissions;
```

### Lister toutes les permissions disponibles
```sql
SELECT nom_permission, module
FROM permissions
ORDER BY module, nom_permission;
```

### Exécuter le script corrigé
```bash
mysql -u utilisateur -p nom_base < Database/add_role_comptable.sql
```

### Vérifier le résultat
```sql
-- Vérifier le rôle
SELECT * FROM roles WHERE nom_role = 'Comptable';

-- Vérifier les permissions attribuées
SELECT
    r.nom_role,
    p.nom_permission,
    p.module
FROM role_permissions rp
JOIN roles r ON rp.role_id = r.id
JOIN permissions p ON rp.permission_id = p.id
WHERE r.nom_role = 'Comptable'
ORDER BY p.module, p.nom_permission;
```

---

## Notes Importantes

1. **Niveau d'accès**: Le rôle Comptable a `niveau_acces = 2` (niveau intermédiaire)
   - Niveau 3 = Super Administrateur
   - Niveau 2 = Administrateur métier (Admin RH, Comptable)
   - Niveau 1 = Rôles standards

2. **Compatibilité**: Le script est maintenant compatible avec:
   - MySQL 5.7+
   - MySQL 8.0+
   - MariaDB 10.0+

3. **Idempotence**: Le script peut être exécuté plusieurs fois sans erreur
   - `WHERE NOT EXISTS` évite la duplication du rôle
   - `DELETE FROM role_permissions WHERE role_id = @comptable_role_id` nettoie avant insertion

4. **Extensions futures**: Si de nouvelles permissions salaire sont ajoutées, il suffit de les ajouter à la liste `WHERE p.nom_permission IN (...)` et réexécuter le script

---

## Contact et Support

Pour toute question sur ce script:
- Consulter la documentation: `ROLE_COMPTABLE.md`
- Vérifier les logs d'exécution dans MySQL Workbench
- Contacter le support: support@gmp-rh.com

---

**Version du script**: 1.0 (corrigé)
**Date**: 13 février 2026
**Testé avec**: MySQL 8.0, structure create_auth_tables.sql
