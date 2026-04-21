-- ================================================================================
-- ATTRIBUTION DU RÔLE GESTIONNAIRE RH
-- Utilisateurs: bamikou.traore et bibiane.traore
-- Date: 2026-03-16
-- ================================================================================

USE gmp_rh_gestion;

-- ================================================================================
-- 1. VÉRIFICATION DES UTILISATEURS EXISTANTS
-- ================================================================================

SELECT '=== VÉRIFICATION DES UTILISATEURS ===' AS '';

SELECT
    u.id,
    u.nom_utilisateur,
    u.nom_complet,
    u.actif AS 'Actif',
    GROUP_CONCAT(r.nom_role SEPARATOR ', ') AS 'Rôles actuels'
FROM utilisateurs u
LEFT JOIN utilisateur_roles ur ON u.id = ur.utilisateur_id
LEFT JOIN roles r ON ur.role_id = r.id
WHERE u.nom_utilisateur IN ('bamikou.traore', 'bibiane.traore')
GROUP BY u.id;

-- ================================================================================
-- 2. VÉRIFICATION DU RÔLE GESTIONNAIRE RH
-- ================================================================================

SELECT '=== VÉRIFICATION DU RÔLE GESTIONNAIRE RH ===' AS '';

SELECT
    id,
    nom_role,
    description,
    niveau_acces
FROM roles
WHERE nom_role = 'Gestionnaire RH';

-- ================================================================================
-- 3. SUPPRESSION DES ANCIENS RÔLES (si nécessaire)
-- ================================================================================

SELECT '=== SUPPRESSION DES ANCIENS RÔLES ===' AS '';

-- Supprimer tous les rôles actuels de bamikou.traore
DELETE ur FROM utilisateur_roles ur
INNER JOIN utilisateurs u ON ur.utilisateur_id = u.id
WHERE u.nom_utilisateur = 'bamikou.traore';

-- Supprimer tous les rôles actuels de bibiane.traore
DELETE ur FROM utilisateur_roles ur
INNER JOIN utilisateurs u ON ur.utilisateur_id = u.id
WHERE u.nom_utilisateur = 'bibiane.traore';

SELECT 'Anciens rôles supprimés' AS resultat;

-- ================================================================================
-- 4. ATTRIBUTION DU RÔLE GESTIONNAIRE RH
-- ================================================================================

SELECT '=== ATTRIBUTION DU RÔLE GESTIONNAIRE RH ===' AS '';

-- Attribution pour bamikou.traore
INSERT INTO utilisateur_roles (utilisateur_id, role_id)
SELECT u.id, r.id
FROM utilisateurs u
CROSS JOIN roles r
WHERE u.nom_utilisateur = 'bamikou.traore'
  AND r.nom_role = 'Gestionnaire RH'
ON DUPLICATE KEY UPDATE utilisateur_id = utilisateur_id;

-- Attribution pour bibiane.traore
INSERT INTO utilisateur_roles (utilisateur_id, role_id)
SELECT u.id, r.id
FROM utilisateurs u
CROSS JOIN roles r
WHERE u.nom_utilisateur = 'bibiane.traore'
  AND r.nom_role = 'Gestionnaire RH'
ON DUPLICATE KEY UPDATE utilisateur_id = utilisateur_id;

SELECT 'Rôle Gestionnaire RH attribué avec succès' AS resultat;

-- ================================================================================
-- 5. VÉRIFICATION FINALE
-- ================================================================================

SELECT '=== VÉRIFICATION FINALE ===' AS '';

SELECT
    u.id,
    u.nom_utilisateur,
    u.nom_complet,
    u.actif AS 'Actif',
    GROUP_CONCAT(r.nom_role SEPARATOR ', ') AS 'Rôles'
FROM utilisateurs u
LEFT JOIN utilisateur_roles ur ON u.id = ur.utilisateur_id
LEFT JOIN roles r ON ur.role_id = r.id
WHERE u.nom_utilisateur IN ('bamikou.traore', 'bibiane.traore')
GROUP BY u.id;

-- ================================================================================
-- 6. PERMISSIONS DU RÔLE GESTIONNAIRE RH
-- ================================================================================

SELECT '=== PERMISSIONS DU RÔLE GESTIONNAIRE RH ===' AS '';

SELECT
    p.nom_permission,
    p.module,
    p.action,
    p.description
FROM permissions p
INNER JOIN role_permissions rp ON p.id = rp.permission_id
INNER JOIN roles r ON rp.role_id = r.id
WHERE r.nom_role = 'Gestionnaire RH'
ORDER BY p.module, p.action;

-- ================================================================================
-- 7. ENREGISTREMENT DANS LES LOGS
-- ================================================================================

INSERT INTO logs_activite (nom_utilisateur, action, module, details, resultat)
VALUES
    ('admin', 'CHANGE_ROLE', 'Système', 'Attribution du rôle Gestionnaire RH à bamikou.traore', 'SUCCESS'),
    ('admin', 'CHANGE_ROLE', 'Système', 'Attribution du rôle Gestionnaire RH à bibiane.traore', 'SUCCESS');

-- ================================================================================
-- RÉSUMÉ
-- ================================================================================

SELECT '=== RÉSUMÉ ===' AS '';
SELECT '✓ Rôles précédents supprimés' AS '';
SELECT '✓ Rôle Gestionnaire RH attribué à bamikou.traore' AS '';
SELECT '✓ Rôle Gestionnaire RH attribué à bibiane.traore' AS '';
SELECT '✓ Logs enregistrés' AS '';
SELECT '' AS '';
SELECT 'OPÉRATION TERMINÉE AVEC SUCCÈS !' AS '';

-- ================================================================================
-- NOTES SUR LE RÔLE GESTIONNAIRE RH
-- ================================================================================

/*
Le rôle "Gestionnaire RH" inclut les permissions suivantes :

MODULE PERSONNEL:
- PERSONNEL_VIEW : Consulter les employés
- PERSONNEL_ADD : Ajouter des employés
- PERSONNEL_EDIT : Modifier des employés
- PERSONNEL_IMPORT : Importer des employés en masse

MODULE SALAIRE:
- SALAIRE_VIEW : Consulter les salaires
- SALAIRE_PROCESS : Traiter les salaires
- SALAIRE_EDIT : Modifier les salaires
- SALAIRE_EXPORT : Exporter les bulletins de paie

MODULE ADMINISTRATION:
- ADMIN_CATEGORIES : Gérer les catégories
- ADMIN_SERVICES : Gérer les services
- ADMIN_DIRECTIONS : Gérer les directions
- ADMIN_CHARGES : Gérer les charges
- ADMIN_INDEMNITES : Gérer les indemnités

LIMITATIONS:
- Pas d'accès aux permissions système (SYSTEM_*)
- Pas d'accès à la gestion des entreprises (ADMIN_ENTREPRISE)
- Pas de suppression d'employés (PERSONNEL_DELETE)

Pour donner un accès complet administrateur, utilisez plutôt le rôle "Administrateur".
*/
