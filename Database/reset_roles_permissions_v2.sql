-- ============================================================================
-- SCRIPT DE RÉINITIALISATION DES RÔLES ET PERMISSIONS - RH+ GESTION v2
-- Version: 2.0 (Corrigé pour structure existante)
-- Date: 2026-02-12
-- Description: Supprime et recrée un système complet de permissions et rôles
-- ============================================================================

SET FOREIGN_KEY_CHECKS = 0;

-- Nettoyage complet
TRUNCATE TABLE role_permissions;
TRUNCATE TABLE utilisateur_roles;
DELETE FROM roles;
DELETE FROM permissions;

SET FOREIGN_KEY_CHECKS = 1;

-- ============================================================================
-- CRÉATION DES PERMISSIONS
-- Format: (nom_permission, description, module, action)
-- ============================================================================

-- ENTREPRISES (4)
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('ENTREPRISE_VIEW', 'Consulter les entreprises', 'Entreprises', 'VIEW', NOW()),
('ENTREPRISE_CREATE', 'Créer une entreprise', 'Entreprises', 'CREATE', NOW()),
('ENTREPRISE_EDIT', 'Modifier une entreprise', 'Entreprises', 'EDIT', NOW()),
('ENTREPRISE_DELETE', 'Supprimer une entreprise', 'Entreprises', 'DELETE', NOW());

-- DIRECTIONS (4)
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('DIRECTION_VIEW', 'Consulter les directions', 'Directions', 'VIEW', NOW()),
('DIRECTION_CREATE', 'Créer une direction', 'Directions', 'CREATE', NOW()),
('DIRECTION_EDIT', 'Modifier une direction', 'Directions', 'EDIT', NOW()),
('DIRECTION_DELETE', 'Supprimer une direction', 'Directions', 'DELETE', NOW());

-- SERVICES (4)
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('SERVICE_VIEW', 'Consulter les services', 'Services', 'VIEW', NOW()),
('SERVICE_CREATE', 'Créer un service', 'Services', 'CREATE', NOW()),
('SERVICE_EDIT', 'Modifier un service', 'Services', 'EDIT', NOW()),
('SERVICE_DELETE', 'Supprimer un service', 'Services', 'DELETE', NOW());

-- CATÉGORIES (4)
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('CATEGORIE_VIEW', 'Consulter les catégories', 'Catégories', 'VIEW', NOW()),
('CATEGORIE_CREATE', 'Créer une catégorie', 'Catégories', 'CREATE', NOW()),
('CATEGORIE_EDIT', 'Modifier une catégorie', 'Catégories', 'EDIT', NOW()),
('CATEGORIE_DELETE', 'Supprimer une catégorie', 'Catégories', 'DELETE', NOW());

-- EMPLOYÉS (6)
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('EMPLOYE_VIEW', 'Consulter les employés', 'Employés', 'VIEW', NOW()),
('EMPLOYE_CREATE', 'Créer un employé', 'Employés', 'CREATE', NOW()),
('EMPLOYE_EDIT', 'Modifier un employé', 'Employés', 'EDIT', NOW()),
('EMPLOYE_DELETE', 'Supprimer un employé', 'Employés', 'DELETE', NOW()),
('EMPLOYE_IMPORT', 'Importer des employés', 'Employés', 'IMPORT', NOW()),
('EMPLOYE_EXPORT', 'Exporter des employés', 'Employés', 'EXPORT', NOW());

-- CHARGES (4)
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('CHARGE_VIEW', 'Consulter les charges', 'Charges', 'VIEW', NOW()),
('CHARGE_CREATE', 'Créer une charge', 'Charges', 'CREATE', NOW()),
('CHARGE_EDIT', 'Modifier une charge', 'Charges', 'EDIT', NOW()),
('CHARGE_DELETE', 'Supprimer une charge', 'Charges', 'DELETE', NOW());

-- INDEMNITÉS (4)
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('INDEMNITE_VIEW', 'Consulter les indemnités', 'Indemnités', 'VIEW', NOW()),
('INDEMNITE_CREATE', 'Créer une indemnité', 'Indemnités', 'CREATE', NOW()),
('INDEMNITE_EDIT', 'Modifier une indemnité', 'Indemnités', 'EDIT', NOW()),
('INDEMNITE_DELETE', 'Supprimer une indemnité', 'Indemnités', 'DELETE', NOW());

-- SURSALAIRES (4)
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('SURSALAIRE_VIEW', 'Consulter les sursalaires', 'Sursalaires', 'VIEW', NOW()),
('SURSALAIRE_CREATE', 'Créer un sursalaire', 'Sursalaires', 'CREATE', NOW()),
('SURSALAIRE_EDIT', 'Modifier un sursalaire', 'Sursalaires', 'EDIT', NOW()),
('SURSALAIRE_DELETE', 'Supprimer un sursalaire', 'Sursalaires', 'DELETE', NOW());

-- ABONNEMENTS (4)
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('ABONNEMENT_VIEW', 'Consulter les abonnements', 'Abonnements', 'VIEW', NOW()),
('ABONNEMENT_CREATE', 'Créer un abonnement', 'Abonnements', 'CREATE', NOW()),
('ABONNEMENT_EDIT', 'Modifier un abonnement', 'Abonnements', 'EDIT', NOW()),
('ABONNEMENT_DELETE', 'Supprimer un abonnement', 'Abonnements', 'DELETE', NOW());

-- SALAIRES HORAIRES (5)
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('SALAIRE_HORAIRE_VIEW', 'Consulter les salaires horaires', 'Salaires', 'VIEW', NOW()),
('SALAIRE_HORAIRE_CREATE', 'Créer un salaire horaire', 'Salaires', 'CREATE', NOW()),
('SALAIRE_HORAIRE_EDIT', 'Modifier un salaire horaire', 'Salaires', 'EDIT', NOW()),
('SALAIRE_HORAIRE_DELETE', 'Supprimer un salaire horaire', 'Salaires', 'DELETE', NOW()),
('SALAIRE_HORAIRE_VALIDATE', 'Valider un salaire horaire', 'Salaires', 'VALIDATE', NOW());

-- SALAIRES JOURNALIERS (5)
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('SALAIRE_JOURNALIER_VIEW', 'Consulter les salaires journaliers', 'Salaires', 'VIEW', NOW()),
('SALAIRE_JOURNALIER_CREATE', 'Créer un salaire journalier', 'Salaires', 'CREATE', NOW()),
('SALAIRE_JOURNALIER_EDIT', 'Modifier un salaire journalier', 'Salaires', 'EDIT', NOW()),
('SALAIRE_JOURNALIER_DELETE', 'Supprimer un salaire journalier', 'Salaires', 'DELETE', NOW()),
('SALAIRE_JOURNALIER_VALIDATE', 'Valider un salaire journalier', 'Salaires', 'VALIDATE', NOW());

-- BULLETINS (4)
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('BULLETIN_VIEW', 'Consulter les bulletins', 'Bulletins', 'VIEW', NOW()),
('BULLETIN_PRINT', 'Imprimer les bulletins', 'Bulletins', 'PRINT', NOW()),
('BULLETIN_PRINT_BATCH', 'Impression en lot', 'Bulletins', 'PRINT_BATCH', NOW()),
('BULLETIN_EXPORT', 'Exporter les bulletins', 'Bulletins', 'EXPORT', NOW());

-- SYSTÈME - UTILISATEURS (3)
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('SYSTEM_USERS', 'Gérer les utilisateurs', 'Système', 'MANAGE', NOW()),
('SYSTEM_USER_RESET_PASSWORD', 'Réinitialiser les mots de passe', 'Système', 'RESET_PWD', NOW()),
('SYSTEM_USER_UNLOCK', 'Déverrouiller les comptes', 'Système', 'UNLOCK', NOW());

-- SYSTÈME - RÔLES (2)
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('SYSTEM_ROLES', 'Gérer les rôles', 'Système', 'MANAGE', NOW()),
('SYSTEM_PERMISSIONS', 'Gérer les permissions', 'Système', 'MANAGE', NOW());

-- SYSTÈME - LOGS (2)
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('SYSTEM_LOGS', 'Consulter les logs', 'Système', 'VIEW', NOW()),
('SYSTEM_LOGS_EXPORT', 'Exporter les logs', 'Système', 'EXPORT', NOW());

-- SYSTÈME - CONFIG (2)
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('SYSTEM_CONFIG', 'Configuration système', 'Système', 'CONFIG', NOW()),
('SYSTEM_BACKUP', 'Sauvegardes', 'Système', 'BACKUP', NOW());

-- ============================================================================
-- CRÉATION DES RÔLES
-- ============================================================================

-- 1. SUPER ADMINISTRATEUR
INSERT INTO roles (nom_role, description, niveau_acces, date_creation) VALUES
('Super Administrateur', 'Accès complet au système', 100, NOW());
SET @role_super_admin = LAST_INSERT_ID();

INSERT INTO role_permissions (role_id, permission_id)
SELECT @role_super_admin, id FROM permissions;

-- 2. ADMINISTRATEUR RH
INSERT INTO roles (nom_role, description, niveau_acces, date_creation) VALUES
('Administrateur RH', 'Gestion complète RH sans système critique', 80, NOW());
SET @role_admin_rh = LAST_INSERT_ID();

INSERT INTO role_permissions (role_id, permission_id)
SELECT @role_admin_rh, id FROM permissions
WHERE nom_permission NOT IN ('SYSTEM_ROLES', 'SYSTEM_PERMISSIONS', 'SYSTEM_CONFIG', 'SYSTEM_BACKUP');

-- 3. GESTIONNAIRE DE PAIE
INSERT INTO roles (nom_role, description, niveau_acces, date_creation) VALUES
('Gestionnaire de Paie', 'Gestion complète de la paie', 60, NOW());
SET @role_paie = LAST_INSERT_ID();

INSERT INTO role_permissions (role_id, permission_id)
SELECT @role_paie, id FROM permissions
WHERE nom_permission IN (
    'EMPLOYE_VIEW',
    'CHARGE_VIEW', 'CHARGE_CREATE', 'CHARGE_EDIT',
    'INDEMNITE_VIEW', 'INDEMNITE_CREATE', 'INDEMNITE_EDIT', 'INDEMNITE_DELETE',
    'SURSALAIRE_VIEW', 'SURSALAIRE_CREATE', 'SURSALAIRE_EDIT', 'SURSALAIRE_DELETE',
    'ABONNEMENT_VIEW', 'ABONNEMENT_CREATE', 'ABONNEMENT_EDIT', 'ABONNEMENT_DELETE',
    'SALAIRE_HORAIRE_VIEW', 'SALAIRE_HORAIRE_CREATE', 'SALAIRE_HORAIRE_EDIT', 'SALAIRE_HORAIRE_DELETE', 'SALAIRE_HORAIRE_VALIDATE',
    'SALAIRE_JOURNALIER_VIEW', 'SALAIRE_JOURNALIER_CREATE', 'SALAIRE_JOURNALIER_EDIT', 'SALAIRE_JOURNALIER_DELETE', 'SALAIRE_JOURNALIER_VALIDATE',
    'BULLETIN_VIEW', 'BULLETIN_PRINT', 'BULLETIN_PRINT_BATCH', 'BULLETIN_EXPORT'
);

-- 4. RESPONSABLE PERSONNEL
INSERT INTO roles (nom_role, description, niveau_acces, date_creation) VALUES
('Responsable Personnel', 'Gestion du personnel et structure', 50, NOW());
SET @role_personnel = LAST_INSERT_ID();

INSERT INTO role_permissions (role_id, permission_id)
SELECT @role_personnel, id FROM permissions
WHERE nom_permission IN (
    'ENTREPRISE_VIEW', 'ENTREPRISE_CREATE', 'ENTREPRISE_EDIT',
    'DIRECTION_VIEW', 'DIRECTION_CREATE', 'DIRECTION_EDIT', 'DIRECTION_DELETE',
    'SERVICE_VIEW', 'SERVICE_CREATE', 'SERVICE_EDIT', 'SERVICE_DELETE',
    'CATEGORIE_VIEW', 'CATEGORIE_CREATE', 'CATEGORIE_EDIT', 'CATEGORIE_DELETE',
    'EMPLOYE_VIEW', 'EMPLOYE_CREATE', 'EMPLOYE_EDIT', 'EMPLOYE_DELETE', 'EMPLOYE_IMPORT', 'EMPLOYE_EXPORT',
    'BULLETIN_VIEW', 'BULLETIN_PRINT'
);

-- 5. ASSISTANT RH
INSERT INTO roles (nom_role, description, niveau_acces, date_creation) VALUES
('Assistant RH', 'Consultation et saisie sans suppression', 30, NOW());
SET @role_assistant = LAST_INSERT_ID();

INSERT INTO role_permissions (role_id, permission_id)
SELECT @role_assistant, id FROM permissions
WHERE nom_permission IN (
    'ENTREPRISE_VIEW', 'DIRECTION_VIEW', 'SERVICE_VIEW', 'CATEGORIE_VIEW',
    'EMPLOYE_VIEW', 'EMPLOYE_CREATE', 'EMPLOYE_EDIT',
    'CHARGE_VIEW',
    'INDEMNITE_VIEW', 'INDEMNITE_CREATE', 'INDEMNITE_EDIT',
    'SURSALAIRE_VIEW', 'SURSALAIRE_CREATE', 'SURSALAIRE_EDIT',
    'ABONNEMENT_VIEW', 'ABONNEMENT_CREATE', 'ABONNEMENT_EDIT',
    'SALAIRE_HORAIRE_VIEW', 'SALAIRE_HORAIRE_CREATE', 'SALAIRE_HORAIRE_EDIT',
    'SALAIRE_JOURNALIER_VIEW', 'SALAIRE_JOURNALIER_CREATE', 'SALAIRE_JOURNALIER_EDIT',
    'BULLETIN_VIEW'
);

-- 6. CONSULTANT
INSERT INTO roles (nom_role, description, niveau_acces, date_creation) VALUES
('Consultant', 'Lecture seule avec exports', 10, NOW());
SET @role_consultant = LAST_INSERT_ID();

INSERT INTO role_permissions (role_id, permission_id)
SELECT @role_consultant, id FROM permissions
WHERE nom_permission LIKE '%_VIEW' OR nom_permission IN ('BULLETIN_PRINT', 'BULLETIN_EXPORT', 'EMPLOYE_EXPORT');

-- ============================================================================
-- ATTRIBUTION AU COMPTE ADMIN
-- ============================================================================

UPDATE utilisateurs
SET actif = 1, compte_verrouille = 0, tentatives_echec = 0
WHERE nom_utilisateur = 'admin';

INSERT INTO utilisateur_roles (utilisateur_id, role_id)
SELECT u.id, @role_super_admin
FROM utilisateurs u
WHERE u.nom_utilisateur = 'admin'
AND NOT EXISTS (SELECT 1 FROM utilisateur_roles ur WHERE ur.utilisateur_id = u.id AND ur.role_id = @role_super_admin);

-- ============================================================================
-- RAPPORT FINAL
-- ============================================================================

SELECT '=== RÉSUMÉ ===' as '';
SELECT
    (SELECT COUNT(*) FROM permissions) as Permissions,
    (SELECT COUNT(*) FROM roles) as Roles,
    (SELECT COUNT(*) FROM role_permissions) as Associations;

SELECT '=== RÔLES ===' as '';
SELECT r.nom_role, r.niveau_acces, COUNT(rp.permission_id) as permissions
FROM roles r
LEFT JOIN role_permissions rp ON r.id = rp.role_id
GROUP BY r.id
ORDER BY r.niveau_acces DESC;

SELECT 'Installation terminée !' as STATUT;
