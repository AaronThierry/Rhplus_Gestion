-- ============================================================================
-- CRÉATION DU RÔLE "AGENT ADMINISTRATIF"
-- Accès complet aux sections Personnel et Salaire
-- ============================================================================

-- Vérifier si le rôle existe déjà
SELECT '=== VÉRIFICATION ===' as '';
SELECT COUNT(*) as role_existe
FROM roles
WHERE nom_role = 'Agent Administratif';

-- Supprimer le rôle s'il existe déjà (pour réinitialisation)
DELETE FROM role_permissions
WHERE role_id = (SELECT id FROM roles WHERE nom_role = 'Agent Administratif');

DELETE FROM utilisateur_roles
WHERE role_id = (SELECT id FROM roles WHERE nom_role = 'Agent Administratif');

DELETE FROM roles
WHERE nom_role = 'Agent Administratif';

-- ============================================================================
-- CRÉATION DU RÔLE
-- ============================================================================

INSERT INTO roles (nom_role, description, niveau_acces, date_creation) VALUES
('Agent Administratif',
 'Accès complet à la gestion du personnel et des salaires sans droits système',
 55,
 NOW());

SET @role_agent_admin = LAST_INSERT_ID();

SELECT '=== RÔLE CRÉÉ ===' as '';
SELECT id, nom_role, niveau_acces FROM roles WHERE id = @role_agent_admin;

-- ============================================================================
-- ATTRIBUTION DES PERMISSIONS - MODULE PERSONNEL
-- ============================================================================

INSERT INTO role_permissions (role_id, permission_id)
SELECT @role_agent_admin, id FROM permissions
WHERE nom_permission IN (
    -- EMPLOYÉS (complet)
    'EMPLOYE_VIEW',
    'EMPLOYE_CREATE',
    'EMPLOYE_EDIT',
    'EMPLOYE_DELETE',
    'EMPLOYE_IMPORT',
    'EMPLOYE_EXPORT',

    -- CHARGES (complet)
    'CHARGE_VIEW',
    'CHARGE_CREATE',
    'CHARGE_EDIT',
    'CHARGE_DELETE',

    -- INDEMNITÉS (complet)
    'INDEMNITE_VIEW',
    'INDEMNITE_CREATE',
    'INDEMNITE_EDIT',
    'INDEMNITE_DELETE',

    -- SURSALAIRES (complet)
    'SURSALAIRE_VIEW',
    'SURSALAIRE_CREATE',
    'SURSALAIRE_EDIT',
    'SURSALAIRE_DELETE',

    -- ABONNEMENTS (complet)
    'ABONNEMENT_VIEW',
    'ABONNEMENT_CREATE',
    'ABONNEMENT_EDIT',
    'ABONNEMENT_DELETE'
);

-- ============================================================================
-- ATTRIBUTION DES PERMISSIONS - MODULE SALAIRE
-- ============================================================================

INSERT INTO role_permissions (role_id, permission_id)
SELECT @role_agent_admin, id FROM permissions
WHERE nom_permission IN (
    -- SALAIRES HORAIRES (complet avec validation)
    'SALAIRE_HORAIRE_VIEW',
    'SALAIRE_HORAIRE_CREATE',
    'SALAIRE_HORAIRE_EDIT',
    'SALAIRE_HORAIRE_DELETE',
    'SALAIRE_HORAIRE_VALIDATE',

    -- SALAIRES JOURNALIERS (complet avec validation)
    'SALAIRE_JOURNALIER_VIEW',
    'SALAIRE_JOURNALIER_CREATE',
    'SALAIRE_JOURNALIER_EDIT',
    'SALAIRE_JOURNALIER_DELETE',
    'SALAIRE_JOURNALIER_VALIDATE',

    -- BULLETINS (consultation et impression)
    'BULLETIN_VIEW',
    'BULLETIN_PRINT',
    'BULLETIN_PRINT_BATCH',
    'BULLETIN_EXPORT'
);

-- ============================================================================
-- PERMISSIONS ADDITIONNELLES - CONSULTATION STRUCTURE
-- ============================================================================
-- Pour permettre de voir les entreprises, directions, services, catégories
-- (en lecture seule pour faciliter la saisie)

INSERT INTO role_permissions (role_id, permission_id)
SELECT @role_agent_admin, id FROM permissions
WHERE nom_permission IN (
    'ENTREPRISE_VIEW',
    'DIRECTION_VIEW',
    'SERVICE_VIEW',
    'CATEGORIE_VIEW'
);

-- ============================================================================
-- RAPPORT DES PERMISSIONS ATTRIBUÉES
-- ============================================================================

SELECT '=== PERMISSIONS PAR MODULE ===' as '';

SELECT
    p.module,
    COUNT(*) as nombre_permissions,
    GROUP_CONCAT(p.action ORDER BY p.action SEPARATOR ', ') as actions
FROM role_permissions rp
JOIN permissions p ON rp.permission_id = p.id
WHERE rp.role_id = @role_agent_admin
GROUP BY p.module
ORDER BY p.module;

-- ============================================================================
-- DÉTAIL DES PERMISSIONS
-- ============================================================================

SELECT '=== LISTE COMPLÈTE DES PERMISSIONS ===' as '';

SELECT
    p.module,
    p.nom_permission,
    p.description,
    p.action
FROM role_permissions rp
JOIN permissions p ON rp.permission_id = p.id
WHERE rp.role_id = @role_agent_admin
ORDER BY p.module, p.action;

-- ============================================================================
-- RÉSUMÉ FINAL
-- ============================================================================

SELECT '=== RÉSUMÉ FINAL ===' as '';

SELECT
    r.nom_role as 'Rôle',
    r.niveau_acces as 'Niveau',
    COUNT(rp.permission_id) as 'Permissions',
    r.description as 'Description'
FROM roles r
LEFT JOIN role_permissions rp ON r.id = rp.role_id
WHERE r.id = @role_agent_admin
GROUP BY r.id;

-- ============================================================================
-- COMPARAISON AVEC AUTRES RÔLES
-- ============================================================================

SELECT '=== COMPARAISON AVEC AUTRES RÔLES ===' as '';

SELECT
    r.nom_role,
    r.niveau_acces,
    COUNT(rp.permission_id) as total_permissions,
    SUM(CASE WHEN p.module IN ('Employés', 'Charges', 'Indemnités', 'Sursalaires', 'Abonnements') THEN 1 ELSE 0 END) as permissions_personnel,
    SUM(CASE WHEN p.module = 'Salaires' THEN 1 ELSE 0 END) as permissions_salaire,
    SUM(CASE WHEN p.module = 'Bulletins' THEN 1 ELSE 0 END) as permissions_bulletins
FROM roles r
LEFT JOIN role_permissions rp ON r.id = rp.role_id
LEFT JOIN permissions p ON rp.permission_id = p.id
GROUP BY r.id, r.nom_role, r.niveau_acces
ORDER BY r.niveau_acces DESC;

SELECT 'Rôle Agent Administratif créé avec succès !' as STATUT;

-- ============================================================================
-- EXEMPLE D'ATTRIBUTION À UN UTILISATEUR
-- ============================================================================

SELECT '=== POUR ATTRIBUER CE RÔLE À UN UTILISATEUR ===' as '';
SELECT '
-- Remplacer "nom_utilisateur" par le login de l\'utilisateur
INSERT INTO utilisateur_roles (utilisateur_id, role_id)
SELECT u.id, r.id
FROM utilisateurs u, roles r
WHERE u.nom_utilisateur = "nom_utilisateur"
  AND r.nom_role = "Agent Administratif"
  AND NOT EXISTS (
    SELECT 1 FROM utilisateur_roles ur
    WHERE ur.utilisateur_id = u.id AND ur.role_id = r.id
  );
' as exemple_sql;
