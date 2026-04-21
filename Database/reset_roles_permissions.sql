-- ============================================================================
-- SCRIPT DE RÉINITIALISATION DES RÔLES ET PERMISSIONS - RH+ GESTION
-- Version: 2.0
-- Date: 2026-02-12
-- Description: Supprime et recrée un système complet de permissions et rôles
--              aligné avec les fonctionnalités réelles du logiciel
-- ============================================================================

-- ============================================================================
-- ÉTAPE 1: NETTOYAGE COMPLET
-- ============================================================================

SET FOREIGN_KEY_CHECKS = 0;

-- Supprimer toutes les associations rôles-permissions
TRUNCATE TABLE role_permissions;

-- Supprimer toutes les associations utilisateurs-rôles
TRUNCATE TABLE utilisateur_roles;

-- Supprimer tous les rôles
DELETE FROM roles;

-- Supprimer toutes les permissions
DELETE FROM permissions;

SET FOREIGN_KEY_CHECKS = 1;

-- ============================================================================
-- ÉTAPE 2: CRÉATION DES PERMISSIONS PAR MODULE
-- ============================================================================

-- ----------------------------------------------------------------------------
-- MODULE: GESTION DES ENTREPRISES
-- ----------------------------------------------------------------------------
INSERT INTO permissions (nom_permission, description, module, action, date_creation) VALUES
('Consulter les entreprises', 'Permet de visualiser la liste des entreprises et leurs détails', 'Entreprises', 'VIEW', NOW()),
('Créer une entreprise', 'Permet d''ajouter de nouvelles entreprises dans le système', 'Entreprises', 'CREATE', NOW()),
('Modifier une entreprise', 'Permet de modifier les informations des entreprises existantes', 'Entreprises', 'EDIT', NOW()),
('Supprimer une entreprise', 'Permet de supprimer des entreprises du système', 'Entreprises', 'DELETE', NOW());

-- ----------------------------------------------------------------------------
-- MODULE: GESTION DES DIRECTIONS
-- ----------------------------------------------------------------------------
INSERT INTO permissions (code_permission, nom_permission, description, module, date_creation) VALUES
('DIRECTION_VIEW', 'Consulter les directions', 'Permet de visualiser la liste des directions', 'Directions', NOW()),
('DIRECTION_CREATE', 'Créer une direction', 'Permet d''ajouter de nouvelles directions', 'Directions', NOW()),
('DIRECTION_EDIT', 'Modifier une direction', 'Permet de modifier les directions existantes', 'Directions', NOW()),
('DIRECTION_DELETE', 'Supprimer une direction', 'Permet de supprimer des directions', 'Directions', NOW());

-- ----------------------------------------------------------------------------
-- MODULE: GESTION DES SERVICES
-- ----------------------------------------------------------------------------
INSERT INTO permissions (code_permission, nom_permission, description, module, date_creation) VALUES
('SERVICE_VIEW', 'Consulter les services', 'Permet de visualiser la liste des services', 'Services', NOW()),
('SERVICE_CREATE', 'Créer un service', 'Permet d''ajouter de nouveaux services', 'Services', NOW()),
('SERVICE_EDIT', 'Modifier un service', 'Permet de modifier les services existants', 'Services', NOW()),
('SERVICE_DELETE', 'Supprimer un service', 'Permet de supprimer des services', 'Services', NOW());

-- ----------------------------------------------------------------------------
-- MODULE: GESTION DES CATÉGORIES
-- ----------------------------------------------------------------------------
INSERT INTO permissions (code_permission, nom_permission, description, module, date_creation) VALUES
('CATEGORIE_VIEW', 'Consulter les catégories', 'Permet de visualiser la liste des catégories professionnelles', 'Catégories', NOW()),
('CATEGORIE_CREATE', 'Créer une catégorie', 'Permet d''ajouter de nouvelles catégories', 'Catégories', NOW()),
('CATEGORIE_EDIT', 'Modifier une catégorie', 'Permet de modifier les catégories existantes', 'Catégories', NOW()),
('CATEGORIE_DELETE', 'Supprimer une catégorie', 'Permet de supprimer des catégories', 'Catégories', NOW());

-- ----------------------------------------------------------------------------
-- MODULE: GESTION DES EMPLOYÉS
-- ----------------------------------------------------------------------------
INSERT INTO permissions (code_permission, nom_permission, description, module, date_creation) VALUES
('EMPLOYE_VIEW', 'Consulter les employés', 'Permet de visualiser la liste des employés et leurs fiches', 'Employés', NOW()),
('EMPLOYE_CREATE', 'Créer un employé', 'Permet d''ajouter de nouveaux employés au système', 'Employés', NOW()),
('EMPLOYE_EDIT', 'Modifier un employé', 'Permet de modifier les informations des employés', 'Employés', NOW()),
('EMPLOYE_DELETE', 'Supprimer un employé', 'Permet de supprimer des employés du système', 'Employés', NOW()),
('EMPLOYE_IMPORT', 'Importer des employés', 'Permet d''importer des employés en masse depuis Excel', 'Employés', NOW()),
('EMPLOYE_EXPORT', 'Exporter des employés', 'Permet d''exporter la liste des employés vers Excel', 'Employés', NOW());

-- ----------------------------------------------------------------------------
-- MODULE: GESTION DES CHARGES
-- ----------------------------------------------------------------------------
INSERT INTO permissions (code_permission, nom_permission, description, module, date_creation) VALUES
('CHARGE_VIEW', 'Consulter les charges', 'Permet de visualiser les charges sociales et fiscales', 'Charges', NOW()),
('CHARGE_CREATE', 'Créer une charge', 'Permet d''ajouter de nouvelles charges au système', 'Charges', NOW()),
('CHARGE_EDIT', 'Modifier une charge', 'Permet de modifier les charges existantes', 'Charges', NOW()),
('CHARGE_DELETE', 'Supprimer une charge', 'Permet de supprimer des charges', 'Charges', NOW());

-- ----------------------------------------------------------------------------
-- MODULE: GESTION DES INDEMNITÉS
-- ----------------------------------------------------------------------------
INSERT INTO permissions (code_permission, nom_permission, description, module, date_creation) VALUES
('INDEMNITE_VIEW', 'Consulter les indemnités', 'Permet de visualiser les indemnités (transport, logement, etc.)', 'Indemnités', NOW()),
('INDEMNITE_CREATE', 'Créer une indemnité', 'Permet d''ajouter de nouvelles indemnités', 'Indemnités', NOW()),
('INDEMNITE_EDIT', 'Modifier une indemnité', 'Permet de modifier les indemnités existantes', 'Indemnités', NOW()),
('INDEMNITE_DELETE', 'Supprimer une indemnité', 'Permet de supprimer des indemnités', 'Indemnités', NOW());

-- ----------------------------------------------------------------------------
-- MODULE: GESTION DES SURSALAIRES
-- ----------------------------------------------------------------------------
INSERT INTO permissions (code_permission, nom_permission, description, module, date_creation) VALUES
('SURSALAIRE_VIEW', 'Consulter les sursalaires', 'Permet de visualiser les sursalaires et primes', 'Sursalaires', NOW()),
('SURSALAIRE_CREATE', 'Créer un sursalaire', 'Permet d''ajouter de nouveaux sursalaires ou primes', 'Sursalaires', NOW()),
('SURSALAIRE_EDIT', 'Modifier un sursalaire', 'Permet de modifier les sursalaires existants', 'Sursalaires', NOW()),
('SURSALAIRE_DELETE', 'Supprimer un sursalaire', 'Permet de supprimer des sursalaires', 'Sursalaires', NOW());

-- ----------------------------------------------------------------------------
-- MODULE: GESTION DES ABONNEMENTS
-- ----------------------------------------------------------------------------
INSERT INTO permissions (code_permission, nom_permission, description, module, date_creation) VALUES
('ABONNEMENT_VIEW', 'Consulter les abonnements', 'Permet de visualiser les abonnements (téléphone, internet, etc.)', 'Abonnements', NOW()),
('ABONNEMENT_CREATE', 'Créer un abonnement', 'Permet d''ajouter de nouveaux abonnements', 'Abonnements', NOW()),
('ABONNEMENT_EDIT', 'Modifier un abonnement', 'Permet de modifier les abonnements existants', 'Abonnements', NOW()),
('ABONNEMENT_DELETE', 'Supprimer un abonnement', 'Permet de supprimer des abonnements', 'Abonnements', NOW());

-- ----------------------------------------------------------------------------
-- MODULE: GESTION DES SALAIRES HORAIRES
-- ----------------------------------------------------------------------------
INSERT INTO permissions (code_permission, nom_permission, description, module, date_creation) VALUES
('SALAIRE_HORAIRE_VIEW', 'Consulter les salaires horaires', 'Permet de visualiser les fiches de paie horaires', 'Salaires', NOW()),
('SALAIRE_HORAIRE_CREATE', 'Créer un salaire horaire', 'Permet de saisir de nouvelles paies horaires', 'Salaires', NOW()),
('SALAIRE_HORAIRE_EDIT', 'Modifier un salaire horaire', 'Permet de modifier les paies horaires existantes', 'Salaires', NOW()),
('SALAIRE_HORAIRE_DELETE', 'Supprimer un salaire horaire', 'Permet de supprimer des paies horaires', 'Salaires', NOW()),
('SALAIRE_HORAIRE_VALIDATE', 'Valider un salaire horaire', 'Permet de valider et clôturer les paies horaires', 'Salaires', NOW());

-- ----------------------------------------------------------------------------
-- MODULE: GESTION DES SALAIRES JOURNALIERS
-- ----------------------------------------------------------------------------
INSERT INTO permissions (code_permission, nom_permission, description, module, date_creation) VALUES
('SALAIRE_JOURNALIER_VIEW', 'Consulter les salaires journaliers', 'Permet de visualiser les fiches de paie journalières', 'Salaires', NOW()),
('SALAIRE_JOURNALIER_CREATE', 'Créer un salaire journalier', 'Permet de saisir de nouvelles paies journalières', 'Salaires', NOW()),
('SALAIRE_JOURNALIER_EDIT', 'Modifier un salaire journalier', 'Permet de modifier les paies journalières existantes', 'Salaires', NOW()),
('SALAIRE_JOURNALIER_DELETE', 'Supprimer un salaire journalier', 'Permet de supprimer des paies journalières', 'Salaires', NOW()),
('SALAIRE_JOURNALIER_VALIDATE', 'Valider un salaire journalier', 'Permet de valider et clôturer les paies journalières', 'Salaires', NOW());

-- ----------------------------------------------------------------------------
-- MODULE: BULLETINS DE PAIE
-- ----------------------------------------------------------------------------
INSERT INTO permissions (code_permission, nom_permission, description, module, date_creation) VALUES
('BULLETIN_VIEW', 'Consulter les bulletins', 'Permet de visualiser les bulletins de paie', 'Bulletins', NOW()),
('BULLETIN_PRINT', 'Imprimer les bulletins', 'Permet d''imprimer les bulletins de paie individuels', 'Bulletins', NOW()),
('BULLETIN_PRINT_BATCH', 'Impression en lot', 'Permet d''imprimer plusieurs bulletins simultanément', 'Bulletins', NOW()),
('BULLETIN_EXPORT', 'Exporter les bulletins', 'Permet d''exporter les bulletins vers PDF ou Excel', 'Bulletins', NOW());

-- ----------------------------------------------------------------------------
-- MODULE: SYSTÈME - UTILISATEURS
-- ----------------------------------------------------------------------------
INSERT INTO permissions (code_permission, nom_permission, description, module, date_creation) VALUES
('SYSTEM_USERS', 'Gérer les utilisateurs', 'Permet de créer, modifier et supprimer des utilisateurs', 'Système', NOW()),
('SYSTEM_USER_RESET_PASSWORD', 'Réinitialiser les mots de passe', 'Permet de réinitialiser les mots de passe des utilisateurs', 'Système', NOW()),
('SYSTEM_USER_UNLOCK', 'Déverrouiller les comptes', 'Permet de déverrouiller les comptes utilisateurs bloqués', 'Système', NOW());

-- ----------------------------------------------------------------------------
-- MODULE: SYSTÈME - RÔLES ET PERMISSIONS
-- ----------------------------------------------------------------------------
INSERT INTO permissions (code_permission, nom_permission, description, module, date_creation) VALUES
('SYSTEM_ROLES', 'Gérer les rôles', 'Permet de créer, modifier et supprimer des rôles', 'Système', NOW()),
('SYSTEM_PERMISSIONS', 'Gérer les permissions', 'Permet d''attribuer des permissions aux rôles', 'Système', NOW());

-- ----------------------------------------------------------------------------
-- MODULE: SYSTÈME - LOGS ET AUDIT
-- ----------------------------------------------------------------------------
INSERT INTO permissions (code_permission, nom_permission, description, module, date_creation) VALUES
('SYSTEM_LOGS', 'Consulter les logs', 'Permet de visualiser l''historique des actions système', 'Système', NOW()),
('SYSTEM_LOGS_EXPORT', 'Exporter les logs', 'Permet d''exporter les logs d''activité', 'Système', NOW());

-- ----------------------------------------------------------------------------
-- MODULE: SYSTÈME - CONFIGURATION
-- ----------------------------------------------------------------------------
INSERT INTO permissions (code_permission, nom_permission, description, module, date_creation) VALUES
('SYSTEM_CONFIG', 'Configuration système', 'Permet d''accéder aux paramètres de configuration du système', 'Système', NOW()),
('SYSTEM_BACKUP', 'Sauvegardes', 'Permet de gérer les sauvegardes de la base de données', 'Système', NOW());

-- ============================================================================
-- ÉTAPE 3: CRÉATION DES RÔLES
-- ============================================================================

-- ----------------------------------------------------------------------------
-- RÔLE: SUPER ADMINISTRATEUR
-- ----------------------------------------------------------------------------
INSERT INTO roles (nom_role, description, niveau_acces, date_creation) VALUES
('Super Administrateur', 'Accès complet au système avec tous les droits de gestion et configuration', 100, NOW());

SET @role_super_admin = LAST_INSERT_ID();

-- Attribuer TOUTES les permissions au Super Administrateur
INSERT INTO role_permissions (role_id, permission_id)
SELECT @role_super_admin, id FROM permissions;

-- ----------------------------------------------------------------------------
-- RÔLE: ADMINISTRATEUR RH
-- ----------------------------------------------------------------------------
INSERT INTO roles (nom_role, description, niveau_acces, date_creation) VALUES
('Administrateur RH', 'Gestion complète des ressources humaines sans accès aux paramètres système critiques', 80, NOW());

SET @role_admin_rh = LAST_INSERT_ID();

-- Attribuer les permissions RH (tout sauf système critique)
INSERT INTO role_permissions (role_id, permission_id)
SELECT @role_admin_rh, id FROM permissions
WHERE code_permission NOT IN (
    'SYSTEM_ROLES',
    'SYSTEM_PERMISSIONS',
    'SYSTEM_CONFIG',
    'SYSTEM_BACKUP'
);

-- ----------------------------------------------------------------------------
-- RÔLE: GESTIONNAIRE DE PAIE
-- ----------------------------------------------------------------------------
INSERT INTO roles (nom_role, description, niveau_acces, date_creation) VALUES
('Gestionnaire de Paie', 'Gestion complète de la paie, bulletins et calculs salariaux', 60, NOW());

SET @role_paie = LAST_INSERT_ID();

-- Attribuer les permissions de paie
INSERT INTO role_permissions (role_id, permission_id)
SELECT @role_paie, id FROM permissions
WHERE code_permission IN (
    -- Employés (lecture seule pour les infos)
    'EMPLOYE_VIEW',
    -- Charges, indemnités, sursalaires, abonnements
    'CHARGE_VIEW', 'CHARGE_CREATE', 'CHARGE_EDIT',
    'INDEMNITE_VIEW', 'INDEMNITE_CREATE', 'INDEMNITE_EDIT', 'INDEMNITE_DELETE',
    'SURSALAIRE_VIEW', 'SURSALAIRE_CREATE', 'SURSALAIRE_EDIT', 'SURSALAIRE_DELETE',
    'ABONNEMENT_VIEW', 'ABONNEMENT_CREATE', 'ABONNEMENT_EDIT', 'ABONNEMENT_DELETE',
    -- Salaires
    'SALAIRE_HORAIRE_VIEW', 'SALAIRE_HORAIRE_CREATE', 'SALAIRE_HORAIRE_EDIT',
    'SALAIRE_HORAIRE_DELETE', 'SALAIRE_HORAIRE_VALIDATE',
    'SALAIRE_JOURNALIER_VIEW', 'SALAIRE_JOURNALIER_CREATE', 'SALAIRE_JOURNALIER_EDIT',
    'SALAIRE_JOURNALIER_DELETE', 'SALAIRE_JOURNALIER_VALIDATE',
    -- Bulletins
    'BULLETIN_VIEW', 'BULLETIN_PRINT', 'BULLETIN_PRINT_BATCH', 'BULLETIN_EXPORT'
);

-- ----------------------------------------------------------------------------
-- RÔLE: RESPONSABLE PERSONNEL
-- ----------------------------------------------------------------------------
INSERT INTO roles (nom_role, description, niveau_acces, date_creation) VALUES
('Responsable Personnel', 'Gestion du personnel et de la structure organisationnelle', 50, NOW());

SET @role_personnel = LAST_INSERT_ID();

-- Attribuer les permissions de gestion du personnel
INSERT INTO role_permissions (role_id, permission_id)
SELECT @role_personnel, id FROM permissions
WHERE code_permission IN (
    -- Structure organisationnelle
    'ENTREPRISE_VIEW', 'ENTREPRISE_CREATE', 'ENTREPRISE_EDIT',
    'DIRECTION_VIEW', 'DIRECTION_CREATE', 'DIRECTION_EDIT', 'DIRECTION_DELETE',
    'SERVICE_VIEW', 'SERVICE_CREATE', 'SERVICE_EDIT', 'SERVICE_DELETE',
    'CATEGORIE_VIEW', 'CATEGORIE_CREATE', 'CATEGORIE_EDIT', 'CATEGORIE_DELETE',
    -- Employés
    'EMPLOYE_VIEW', 'EMPLOYE_CREATE', 'EMPLOYE_EDIT', 'EMPLOYE_DELETE',
    'EMPLOYE_IMPORT', 'EMPLOYE_EXPORT',
    -- Bulletins (consultation)
    'BULLETIN_VIEW', 'BULLETIN_PRINT'
);

-- ----------------------------------------------------------------------------
-- RÔLE: ASSISTANT RH
-- ----------------------------------------------------------------------------
INSERT INTO roles (nom_role, description, niveau_acces, date_creation) VALUES
('Assistant RH', 'Consultation et saisie de base sans droits de suppression', 30, NOW());

SET @role_assistant = LAST_INSERT_ID();

-- Attribuer les permissions de consultation et saisie
INSERT INTO role_permissions (role_id, permission_id)
SELECT @role_assistant, id FROM permissions
WHERE code_permission IN (
    -- Consultation de la structure
    'ENTREPRISE_VIEW', 'DIRECTION_VIEW', 'SERVICE_VIEW', 'CATEGORIE_VIEW',
    -- Gestion employés (pas de suppression)
    'EMPLOYE_VIEW', 'EMPLOYE_CREATE', 'EMPLOYE_EDIT',
    -- Éléments de paie (consultation et saisie)
    'CHARGE_VIEW',
    'INDEMNITE_VIEW', 'INDEMNITE_CREATE', 'INDEMNITE_EDIT',
    'SURSALAIRE_VIEW', 'SURSALAIRE_CREATE', 'SURSALAIRE_EDIT',
    'ABONNEMENT_VIEW', 'ABONNEMENT_CREATE', 'ABONNEMENT_EDIT',
    -- Salaires (saisie uniquement)
    'SALAIRE_HORAIRE_VIEW', 'SALAIRE_HORAIRE_CREATE', 'SALAIRE_HORAIRE_EDIT',
    'SALAIRE_JOURNALIER_VIEW', 'SALAIRE_JOURNALIER_CREATE', 'SALAIRE_JOURNALIER_EDIT',
    -- Bulletins (consultation)
    'BULLETIN_VIEW'
);

-- ----------------------------------------------------------------------------
-- RÔLE: CONSULTANT (LECTURE SEULE)
-- ----------------------------------------------------------------------------
INSERT INTO roles (nom_role, description, niveau_acces, date_creation) VALUES
('Consultant', 'Accès en lecture seule pour consultation et reporting', 10, NOW());

SET @role_consultant = LAST_INSERT_ID();

-- Attribuer uniquement les permissions de consultation
INSERT INTO role_permissions (role_id, permission_id)
SELECT @role_consultant, id FROM permissions
WHERE code_permission LIKE '%_VIEW'
   OR code_permission IN ('BULLETIN_PRINT', 'BULLETIN_EXPORT', 'EMPLOYE_EXPORT');

-- ============================================================================
-- ÉTAPE 4: MISE À JOUR DU COMPTE ADMINISTRATEUR PAR DÉFAUT
-- ============================================================================

-- S'assurer que le compte admin existe et a le rôle Super Administrateur
UPDATE utilisateurs
SET actif = 1,
    compte_verrouille = 0,
    tentatives_connexion = 0,
    date_modification = NOW()
WHERE nom_utilisateur = 'admin';

-- Attribuer le rôle Super Administrateur au compte admin
INSERT INTO utilisateur_roles (utilisateur_id, role_id)
SELECT u.id, @role_super_admin
FROM utilisateurs u
WHERE u.nom_utilisateur = 'admin'
AND NOT EXISTS (
    SELECT 1 FROM utilisateur_roles ur
    WHERE ur.utilisateur_id = u.id AND ur.role_id = @role_super_admin
);

-- ============================================================================
-- ÉTAPE 5: VÉRIFICATION ET STATISTIQUES
-- ============================================================================

SELECT 'RÉSUMÉ DE LA RÉINITIALISATION' as '===';

SELECT
    (SELECT COUNT(*) FROM permissions) as 'Total Permissions',
    (SELECT COUNT(*) FROM roles) as 'Total Rôles',
    (SELECT COUNT(*) FROM role_permissions) as 'Total Associations Rôle-Permission',
    (SELECT COUNT(*) FROM utilisateur_roles) as 'Total Utilisateurs avec Rôles';

SELECT '=== PERMISSIONS PAR MODULE ===' as '';
SELECT module, COUNT(*) as nombre_permissions
FROM permissions
GROUP BY module
ORDER BY module;

SELECT '=== RÔLES CRÉÉS ===' as '';
SELECT
    r.nom_role,
    r.niveau_acces,
    COUNT(rp.permission_id) as nombre_permissions
FROM roles r
LEFT JOIN role_permissions rp ON r.id = rp.role_id
GROUP BY r.id, r.nom_role, r.niveau_acces
ORDER BY r.niveau_acces DESC;

SELECT 'Réinitialisation terminée avec succès !' as 'STATUT';
