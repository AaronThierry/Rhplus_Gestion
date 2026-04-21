-- ============================================================================
-- VÉRIFICATION ET CORRECTION DES PERMISSIONS DU SUPER ADMIN
-- ============================================================================

-- 1. Vérifier l'utilisateur admin
SELECT '=== UTILISATEUR ADMIN ===' as '';
SELECT id, nom_utilisateur, nom_complet, actif, compte_verrouille
FROM utilisateurs
WHERE nom_utilisateur = 'admin';

-- 2. Vérifier les rôles de l'admin
SELECT '=== RÔLES DE L\'ADMIN ===' as '';
SELECT u.nom_utilisateur, r.nom_role, r.niveau_acces
FROM utilisateurs u
JOIN utilisateur_roles ur ON u.id = ur.utilisateur_id
JOIN roles r ON ur.role_id = r.id
WHERE u.nom_utilisateur = 'admin';

-- 3. Compter les permissions du Super Administrateur
SELECT '=== PERMISSIONS DU SUPER ADMIN ===' as '';
SELECT
    r.nom_role,
    COUNT(rp.permission_id) as nombre_permissions,
    (SELECT COUNT(*) FROM permissions) as total_permissions
FROM roles r
LEFT JOIN role_permissions rp ON r.id = rp.role_id
WHERE r.nom_role = 'Super Administrateur'
GROUP BY r.id, r.nom_role;

-- 4. Lister quelques permissions du Super Admin
SELECT '=== EXEMPLES DE PERMISSIONS ===' as '';
SELECT p.nom_permission, p.module, p.action
FROM roles r
JOIN role_permissions rp ON r.id = rp.role_id
JOIN permissions p ON rp.permission_id = p.id
WHERE r.nom_role = 'Super Administrateur'
ORDER BY p.module, p.action
LIMIT 20;

-- 5. Vérifier les permissions système spécifiques
SELECT '=== PERMISSIONS SYSTÈME CRITIQUES ===' as '';
SELECT p.nom_permission, p.module
FROM permissions p
WHERE p.nom_permission IN ('SYSTEM_USERS', 'SYSTEM_ROLES', 'SYSTEM_LOGS', 'SYSTEM_CONFIG')
ORDER BY p.nom_permission;

-- 6. Vérifier si le Super Admin a ces permissions
SELECT '=== VÉRIFICATION ACCÈS SUPER ADMIN ===' as '';
SELECT
    p.nom_permission,
    CASE
        WHEN rp.id IS NOT NULL THEN 'OUI ✓'
        ELSE 'NON ✗'
    END as a_permission
FROM permissions p
LEFT JOIN role_permissions rp ON p.id = rp.permission_id
    AND rp.role_id = (SELECT id FROM roles WHERE nom_role = 'Super Administrateur')
WHERE p.nom_permission IN ('SYSTEM_USERS', 'SYSTEM_ROLES', 'SYSTEM_LOGS', 'SYSTEM_CONFIG',
                           'EMPLOYE_VIEW', 'EMPLOYE_CREATE', 'EMPLOYE_EDIT', 'EMPLOYE_DELETE')
ORDER BY p.nom_permission;

-- 7. Si des permissions manquent, les ajouter
INSERT IGNORE INTO role_permissions (role_id, permission_id)
SELECT
    (SELECT id FROM roles WHERE nom_role = 'Super Administrateur'),
    p.id
FROM permissions p
WHERE NOT EXISTS (
    SELECT 1
    FROM role_permissions rp
    WHERE rp.role_id = (SELECT id FROM roles WHERE nom_role = 'Super Administrateur')
    AND rp.permission_id = p.id
);

SELECT '=== CORRECTION EFFECTUÉE ===' as '';
SELECT
    'Permissions manquantes ajoutées' as statut,
    ROW_COUNT() as permissions_ajoutees;

-- 8. Vérification finale
SELECT '=== VÉRIFICATION FINALE ===' as '';
SELECT
    r.nom_role,
    COUNT(rp.permission_id) as permissions_attribuees,
    (SELECT COUNT(*) FROM permissions) as permissions_disponibles,
    CASE
        WHEN COUNT(rp.permission_id) = (SELECT COUNT(*) FROM permissions) THEN 'COMPLET ✓'
        ELSE 'INCOMPLET ✗'
    END as statut
FROM roles r
LEFT JOIN role_permissions rp ON r.id = rp.role_id
WHERE r.nom_role = 'Super Administrateur'
GROUP BY r.id, r.nom_role;
