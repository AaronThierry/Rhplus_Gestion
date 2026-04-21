-- ============================================================================
-- AJOUT DU SYSTÈME DE PREMIÈRE CONNEXION
-- Ajoute une colonne pour forcer le changement de mot de passe
-- ============================================================================

-- Vérifier si la colonne existe déjà
SELECT '=== VÉRIFICATION DE LA COLONNE premier_connexion ===' as '';

SELECT
    COUNT(*) as colonne_existe
FROM information_schema.COLUMNS
WHERE TABLE_SCHEMA = DATABASE()
  AND TABLE_NAME = 'utilisateurs'
  AND COLUMN_NAME = 'premier_connexion';

-- Ajouter la colonne si elle n'existe pas
-- Note: Utilisez cette version compatible avec toutes les versions de MySQL
SET @sql_premier_connexion = (
    SELECT IF(COUNT(*) = 0,
        'ALTER TABLE utilisateurs ADD COLUMN premier_connexion BOOLEAN DEFAULT TRUE COMMENT ''TRUE si l''''utilisateur doit changer son mot de passe''',
        'SELECT ''Colonne premier_connexion existe déjà'' AS message')
    FROM information_schema.COLUMNS
    WHERE TABLE_SCHEMA = DATABASE()
      AND TABLE_NAME = 'utilisateurs'
      AND COLUMN_NAME = 'premier_connexion'
);

PREPARE stmt_premier_connexion FROM @sql_premier_connexion;
EXECUTE stmt_premier_connexion;
DEALLOCATE PREPARE stmt_premier_connexion;

-- Ajouter la colonne pour stocker le mot de passe par défaut (optionnel, pour traçabilité)
SET @sql_mdp_defaut = (
    SELECT IF(COUNT(*) = 0,
        'ALTER TABLE utilisateurs ADD COLUMN mot_de_passe_par_defaut VARCHAR(20) DEFAULT NULL COMMENT ''Stocke le mot de passe par défaut généré (pour référence admin uniquement)''',
        'SELECT ''Colonne mot_de_passe_par_defaut existe déjà'' AS message')
    FROM information_schema.COLUMNS
    WHERE TABLE_SCHEMA = DATABASE()
      AND TABLE_NAME = 'utilisateurs'
      AND COLUMN_NAME = 'mot_de_passe_par_defaut'
);

PREPARE stmt_mdp_defaut FROM @sql_mdp_defaut;
EXECUTE stmt_mdp_defaut;
DEALLOCATE PREPARE stmt_mdp_defaut;

-- Marquer les utilisateurs existants comme ayant déjà changé leur mot de passe
UPDATE utilisateurs
SET premier_connexion = FALSE
WHERE premier_connexion IS NULL;

SELECT '=== COLONNE AJOUTÉE AVEC SUCCÈS ===' as '';

-- Afficher la structure de la table
SELECT
    COLUMN_NAME as 'Colonne',
    COLUMN_TYPE as 'Type',
    IS_NULLABLE as 'Nullable',
    COLUMN_DEFAULT as 'Défaut',
    COLUMN_COMMENT as 'Commentaire'
FROM information_schema.COLUMNS
WHERE TABLE_SCHEMA = DATABASE()
  AND TABLE_NAME = 'utilisateurs'
  AND COLUMN_NAME IN ('premier_connexion', 'mot_de_passe_par_defaut')
ORDER BY ORDINAL_POSITION;

-- ============================================================================
-- MISE À JOUR DES UTILISATEURS EXISTANTS POUR TEST
-- ============================================================================

SELECT '=== EXEMPLE: RÉINITIALISER UN UTILISATEUR POUR TEST ===' as '';
SELECT '
-- Pour forcer un utilisateur à changer son mot de passe :
UPDATE utilisateurs
SET premier_connexion = TRUE,
    mot_de_passe = SHA2(''RHPlus2026!'', 256),  -- Mot de passe par défaut
    mot_de_passe_par_defaut = ''RHPlus2026!''
WHERE nom_utilisateur = ''nom_utilisateur_test'';
' as exemple_sql;

SELECT 'Système de première connexion configuré avec succès !' as STATUT;
