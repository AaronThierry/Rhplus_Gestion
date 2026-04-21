-- ================================================================================
-- ATTRIBUTION DU RÔLE ADMINISTRATEUR (ACCÈS COMPLET)
-- Utilisateurs: bamikou.traore et bibiane.traore
-- Date: 2026-03-16
-- ================================================================================
-- ATTENTION: Ce script donne un accès COMPLET au système
-- Si vous voulez un accès RH limité, utilisez plutôt:
-- add_role_gestionnaire_rh_bamikou_bibiane.sql
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
-- 2. VÉRIFICATION DU RÔLE ADMINISTRATEUR
-- ================================================================================

SELECT '=== VÉRIFICATION DU RÔLE ADMINISTRATEUR ===' AS '';

SELECT
    id,
    nom_role,
    description,
    niveau_acces
FROM roles
WHERE nom_role = 'Administrateur';

-- ================================================================================
-- 3. SUPPRESSION DES ANCIENS RÔLES
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
-- 4. ATTRIBUTION DU RÔLE ADMINISTRATEUR
-- ================================================================================

SELECT '=== ATTRIBUTION DU RÔLE ADMINISTRATEUR ===' AS '';

-- Attribution pour bamikou.traore
INSERT INTO utilisateur_roles (utilisateur_id, role_id)
SELECT u.id, r.id
FROM utilisateurs u
CROSS JOIN roles r
WHERE u.nom_utilisateur = 'bamikou.traore'
  AND r.nom_role = 'Administrateur'
ON DUPLICATE KEY UPDATE utilisateur_id = utilisateur_id;

-- Attribution pour bibiane.traore
INSERT INTO utilisateur_roles (utilisateur_id, role_id)
SELECT u.id, r.id
FROM utilisateurs u
CROSS JOIN roles r
WHERE u.nom_utilisateur = 'bibiane.traore'
  AND r.nom_role = 'Administrateur'
ON DUPLICATE KEY UPDATE utilisateur_id = utilisateur_id;

SELECT 'Rôle Administrateur attribué avec succès' AS resultat;

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
-- 6. PERMISSIONS DU RÔLE ADMINISTRATEUR
-- ================================================================================

SELECT '=== PERMISSIONS DU RÔLE ADMINISTRATEUR (TOUTES) ===' AS '';

SELECT
    COUNT(*) AS 'Nombre total de permissions'
FROM permissions p
INNER JOIN role_permissions rp ON p.id = rp.permission_id
INNER JOIN roles r ON rp.role_id = r.id
WHERE r.nom_role = 'Administrateur';

SELECT
    p.module,
    COUNT(*) AS 'Nombre de permissions'
FROM permissions p
INNER JOIN role_permissions rp ON p.id = rp.permission_id
INNER JOIN roles r ON rp.role_id = r.id
WHERE r.nom_role = 'Administrateur'
GROUP BY p.module
ORDER BY p.module;

-- ================================================================================
-- 7. ENREGISTREMENT DANS LES LOGS
-- ================================================================================

INSERT INTO logs_activite (nom_utilisateur, action, module, details, resultat)
VALUES
    ('admin', 'CHANGE_ROLE_ADMIN', 'Système', 'Attribution du rôle Administrateur (accès complet) à bamikou.traore', 'SUCCESS'),
    ('admin', 'CHANGE_ROLE_ADMIN', 'Système', 'Attribution du rôle Administrateur (accès complet) à bibiane.traore', 'SUCCESS');

-- ================================================================================
-- RÉSUMÉ
-- ================================================================================

SELECT '=== RÉSUMÉ ===' AS '';
SELECT '✓ Rôles précédents supprimés' AS '';
SELECT '✓ Rôle Administrateur attribué à bamikou.traore' AS '';
SELECT '✓ Rôle Administrateur attribué à bibiane.traore' AS '';
SELECT '✓ Logs enregistrés' AS '';
SELECT '' AS '';
SELECT '⚠️  ACCÈS COMPLET ADMINISTRATEUR ACTIVÉ !' AS '';
SELECT '' AS '';
SELECT 'Ces utilisateurs ont maintenant:' AS '';
SELECT '  - Accès à toutes les fonctionnalités' AS '';
SELECT '  - Gestion des utilisateurs et rôles' AS '';
SELECT '  - Gestion complète du système' AS '';
SELECT '  - Accès aux logs et configurations' AS '';

-- ================================================================================
-- NOTES SUR LE RÔLE ADMINISTRATEUR
-- ================================================================================

/*
Le rôle "Administrateur" inclut TOUTES les permissions du système:

MODULE PERSONNEL:
- PERSONNEL_VIEW : Consulter les employés
- PERSONNEL_ADD : Ajouter des employés
- PERSONNEL_EDIT : Modifier des employés
- PERSONNEL_DELETE : Supprimer des employés
- PERSONNEL_IMPORT : Importer des employés en masse

MODULE SALAIRE:
- SALAIRE_VIEW : Consulter les salaires
- SALAIRE_PROCESS : Traiter les salaires
- SALAIRE_EDIT : Modifier les salaires
- SALAIRE_EXPORT : Exporter les bulletins de paie

MODULE ADMINISTRATION:
- ADMIN_ENTREPRISE : Gérer les entreprises
- ADMIN_CATEGORIES : Gérer les catégories
- ADMIN_SERVICES : Gérer les services
- ADMIN_DIRECTIONS : Gérer les directions
- ADMIN_CHARGES : Gérer les charges
- ADMIN_INDEMNITES : Gérer les indemnités

MODULE SYSTÈME (ACCÈS COMPLET):
- SYSTEM_USERS : Gérer les utilisateurs
- SYSTEM_ROLES : Gérer les rôles
- SYSTEM_LOGS : Consulter les logs
- SYSTEM_CONFIG : Configuration système

RECOMMANDATION:
Si vous voulez donner un accès RH sans accès système,
utilisez plutôt le rôle "Gestionnaire RH" avec le script:
add_role_gestionnaire_rh_bamikou_bibiane.sql
*/
