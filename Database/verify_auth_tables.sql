-- Script pour vérifier si les tables d'authentification existent
-- Exécutez ce script pour voir quelles tables sont présentes

-- Vérifier les tables d'authentification
SELECT
    'utilisateurs' as table_name,
    COUNT(*) as table_exists
FROM information_schema.tables
WHERE table_schema = 'rhplusCshrp'
  AND table_name = 'utilisateurs'

UNION ALL

SELECT
    'roles' as table_name,
    COUNT(*) as table_exists
FROM information_schema.tables
WHERE table_schema = 'rhplusCshrp'
  AND table_name = 'roles'

UNION ALL

SELECT
    'permissions' as table_name,
    COUNT(*) as table_exists
FROM information_schema.tables
WHERE table_schema = 'rhplusCshrp'
  AND table_name = 'permissions'

UNION ALL

SELECT
    'sessions' as table_name,
    COUNT(*) as table_exists
FROM information_schema.tables
WHERE table_schema = 'rhplusCshrp'
  AND table_name = 'sessions'

UNION ALL

SELECT
    'logs_activite' as table_name,
    COUNT(*) as table_exists
FROM information_schema.tables
WHERE table_schema = 'rhplusCshrp'
  AND table_name = 'logs_activite';

-- Si toutes les tables existent, vérifier l'utilisateur admin
SELECT * FROM utilisateurs WHERE nom_utilisateur = 'admin';
