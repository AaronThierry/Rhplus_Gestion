-- ============================================================================
-- AJOUT DU RÔLE COMPTABLE
-- Accès uniquement à la section Salaire (consultation et gestion)
-- ============================================================================

-- Vérifier si le rôle existe déjà
SELECT '=== VÉRIFICATION DU RÔLE COMPTABLE ===' as '';

SELECT COUNT(*) as role_existe
FROM roles
WHERE nom_role = 'Comptable';

-- Insérer le rôle Comptable s'il n'existe pas
INSERT INTO roles (nom_role, description, niveau_acces, date_creation)
SELECT 'Comptable',
       'Accès complet à la gestion des salaires et bulletins de paie. Aucun accès aux autres sections.',
       2,
       NOW()
WHERE NOT EXISTS (
    SELECT 1 FROM roles WHERE nom_role = 'Comptable'
);

-- Récupérer l'ID du rôle Comptable
SET @comptable_role_id = (SELECT id FROM roles WHERE nom_role = 'Comptable');

SELECT CONCAT('ID du rôle Comptable : ', @comptable_role_id) as info;

-- ============================================================================
-- SUPPRESSION DES ANCIENNES PERMISSIONS (si le rôle existait déjà)
-- ============================================================================

DELETE FROM role_permissions WHERE role_id = @comptable_role_id;

-- ============================================================================
-- ATTRIBUTION DES PERMISSIONS - SECTION SALAIRE UNIQUEMENT
-- ============================================================================
-- NOTE IMPORTANTE:
-- Ce script attribue TOUTES les permissions liées aux salaires qui existent
-- dans votre base de données. Si certaines permissions n'existent pas encore,
-- elles seront simplement ignorées (pas d'erreur).
--
-- Pour créer toutes les permissions détaillées, exécutez d'abord:
-- - reset_roles_permissions.sql OU reset_roles_permissions_v2.sql
-- ============================================================================

SELECT '=== ATTRIBUTION DES PERMISSIONS SALAIRE ===' as '';

-- SALAIRE - Gestion complète des bulletins et calculs
INSERT INTO role_permissions (role_id, permission_id)
SELECT @comptable_role_id, p.id
FROM permissions p
WHERE p.nom_permission IN (
    -- PERMISSIONS DE BASE (toujours présentes)
    'SALAIRE_VIEW',
    'SALAIRE_PROCESS',
    'SALAIRE_EDIT',
    'SALAIRE_EXPORT',
    'PERSONNEL_VIEW',

    -- PERMISSIONS DÉTAILLÉES (si reset_roles_permissions a été exécuté)
    -- Sursalaires
    'SURSALAIRE_VIEW',
    'SURSALAIRE_CREATE',
    'SURSALAIRE_EDIT',
    'SURSALAIRE_DELETE',

    -- Salaires horaires
    'SALAIRE_HORAIRE_VIEW',
    'SALAIRE_HORAIRE_CREATE',
    'SALAIRE_HORAIRE_EDIT',
    'SALAIRE_HORAIRE_DELETE',
    'SALAIRE_HORAIRE_VALIDATE',

    -- Salaires journaliers
    'SALAIRE_JOURNALIER_VIEW',
    'SALAIRE_JOURNALIER_CREATE',
    'SALAIRE_JOURNALIER_EDIT',
    'SALAIRE_JOURNALIER_DELETE',
    'SALAIRE_JOURNALIER_VALIDATE',

    -- Bulletins
    'BULLETIN_VIEW',
    'BULLETIN_PRINT',
    'BULLETIN_PRINT_BATCH',
    'BULLETIN_EXPORT',

    -- Employés (lecture seule)
    'EMPLOYE_VIEW'
);

-- Vérifier le nombre de permissions ajoutées
SELECT COUNT(*) as nb_permissions_ajoutees
FROM role_permissions
WHERE role_id = @comptable_role_id;

-- ============================================================================
-- AFFICHAGE DES PERMISSIONS DU RÔLE COMPTABLE
-- ============================================================================

SELECT '=== PERMISSIONS DU RÔLE COMPTABLE ===' as '';

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

-- ============================================================================
-- STATISTIQUES
-- ============================================================================

SELECT '=== RÉSUMÉ ===' as '';

SELECT
    'Comptable' as 'Rôle créé',
    COUNT(DISTINCT rp.permission_id) as 'Nombre de permissions',
    GROUP_CONCAT(DISTINCT p.module ORDER BY p.module SEPARATOR ', ') as 'Modules'
FROM role_permissions rp
JOIN permissions p ON rp.permission_id = p.id
WHERE rp.role_id = @comptable_role_id;

-- ============================================================================
-- EXEMPLE D'ATTRIBUTION DU RÔLE À UN UTILISATEUR
-- ============================================================================

SELECT '=== EXEMPLE ATTRIBUTION DU RÔLE ===' as '';

SELECT '
-- Pour attribuer le rôle Comptable à un utilisateur existant :

-- 1. Récupérer l''ID de l''utilisateur
SELECT id FROM utilisateurs WHERE nom_utilisateur = ''nom_utilisateur_comptable'';
-- Supposons ID = 15

-- 2. Attribuer le rôle
INSERT INTO utilisateur_roles (utilisateur_id, role_id)
VALUES (15, ' as exemple_sql, @comptable_role_id as role_id, ');

-- 3. Vérifier l''attribution
SELECT u.nom_utilisateur, r.nom_role
FROM utilisateurs u
JOIN utilisateur_roles ur ON u.id = ur.utilisateur_id
JOIN roles r ON ur.role_id = r.id
WHERE u.id = 15;
' as suite_exemple;

-- ============================================================================
-- VÉRIFICATION FINALE
-- ============================================================================

SELECT '=== VÉRIFICATION FINALE ===' as '';

-- Vérifier que le rôle a bien été créé
SELECT
    id,
    nom_role,
    description,
    niveau_acces,
    date_creation
FROM roles
WHERE nom_role = 'Comptable';

SELECT 'Rôle Comptable créé avec succès !' as STATUT;

-- ============================================================================
-- PERMISSIONS NON ATTRIBUÉES (pour référence)
-- ============================================================================

SELECT '=== PERMISSIONS NON ATTRIBUÉES AU COMPTABLE ===' as '';

SELECT '
Le rôle Comptable N''A PAS accès à :
- Section Personnel (EMPLOYE_*, CHARGE_*, INDEMNITE_*)
- Section Administration (ENTREPRISE_*, DIRECTION_*, SERVICE_*, etc.)
- Fonctions système (UTILISATEUR_*, ROLE_*, LOG_*)
- Autres modules non liés à la paie

Le Comptable peut UNIQUEMENT :
- Consulter les employés (lecture seule pour calcul paie)
- Gérer les salaires et bulletins
- Calculer les cotisations
- Enregistrer les paiements
- Exporter les données de paie
' as restrictions;

-- ============================================================================
