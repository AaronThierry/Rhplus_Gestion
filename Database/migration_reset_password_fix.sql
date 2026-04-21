-- ================================================================================
-- MIGRATION: Correction et amélioration du système de réinitialisation de mot de passe
-- Date: 2026-03-16
-- Version: 1.1.5
-- ================================================================================

USE gmp_rh_gestion;

-- ================================================================================
-- 1. VÉRIFICATION ET AJOUT DES COLONNES NÉCESSAIRES
-- ================================================================================

-- Vérifier et ajouter la colonne premier_connexion si elle n'existe pas
SET @col_premier_connexion = (
    SELECT COUNT(*)
    FROM information_schema.COLUMNS
    WHERE TABLE_SCHEMA = DATABASE()
      AND TABLE_NAME = 'utilisateurs'
      AND COLUMN_NAME = 'premier_connexion'
);

SET @sql_premier_connexion = IF(@col_premier_connexion = 0,
    'ALTER TABLE utilisateurs ADD COLUMN premier_connexion BOOLEAN DEFAULT TRUE COMMENT ''TRUE si utilisateur doit changer son mot de passe''',
    'SELECT ''Colonne premier_connexion existe déjà'' AS message'
);

PREPARE stmt FROM @sql_premier_connexion;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- Vérifier et ajouter la colonne mot_de_passe_par_defaut si elle n'existe pas
SET @col_mdp_defaut = (
    SELECT COUNT(*)
    FROM information_schema.COLUMNS
    WHERE TABLE_SCHEMA = DATABASE()
      AND TABLE_NAME = 'utilisateurs'
      AND COLUMN_NAME = 'mot_de_passe_par_defaut'
);

SET @sql_mdp_defaut = IF(@col_mdp_defaut = 0,
    'ALTER TABLE utilisateurs ADD COLUMN mot_de_passe_par_defaut VARCHAR(20) DEFAULT NULL COMMENT ''Mot de passe par défaut (référence admin)''',
    'SELECT ''Colonne mot_de_passe_par_defaut existe déjà'' AS message'
);

PREPARE stmt FROM @sql_mdp_defaut;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- ================================================================================
-- 2. CORRECTION DES DONNÉES EXISTANTES
-- ================================================================================

-- Marquer les utilisateurs existants comme n'étant plus en première connexion
-- (sauf s'ils ont déjà le flag premier_connexion à TRUE)
UPDATE utilisateurs
SET premier_connexion = COALESCE(premier_connexion, FALSE)
WHERE premier_connexion IS NULL;

-- Réinitialiser les utilisateurs qui utilisent encore l'ancien mot de passe par défaut "Password@123"
-- Note: Cette requête est commentée pour éviter les réinitialisations non désirées
-- Décommentez si vous voulez forcer la migration vers le nouveau mot de passe par défaut

/*
UPDATE utilisateurs
SET premier_connexion = TRUE,
    mot_de_passe_par_defaut = 'RHPlus2026!'
WHERE mot_de_passe_par_defaut = 'Password@123';
*/

-- ================================================================================
-- 3. VÉRIFICATION DE LA STRUCTURE
-- ================================================================================

SELECT
    '=== STRUCTURE DE LA TABLE UTILISATEURS ===' AS '';

SELECT
    COLUMN_NAME as 'Colonne',
    COLUMN_TYPE as 'Type',
    IS_NULLABLE as 'Nullable',
    COLUMN_DEFAULT as 'Défaut',
    COLUMN_COMMENT as 'Commentaire'
FROM information_schema.COLUMNS
WHERE TABLE_SCHEMA = DATABASE()
  AND TABLE_NAME = 'utilisateurs'
  AND COLUMN_NAME IN (
      'id', 'nom_utilisateur', 'mot_de_passe_hash', 'nom_complet',
      'actif', 'tentatives_echec', 'compte_verrouille',
      'premier_connexion', 'mot_de_passe_par_defaut',
      'derniere_connexion', 'date_creation', 'date_modification'
  )
ORDER BY ORDINAL_POSITION;

-- ================================================================================
-- 4. STATISTIQUES DES UTILISATEURS
-- ================================================================================

SELECT
    '=== STATISTIQUES DES UTILISATEURS ===' AS '';

SELECT
    COUNT(*) as 'Total utilisateurs',
    SUM(CASE WHEN actif = 1 THEN 1 ELSE 0 END) as 'Actifs',
    SUM(CASE WHEN actif = 0 THEN 1 ELSE 0 END) as 'Inactifs',
    SUM(CASE WHEN compte_verrouille = 1 THEN 1 ELSE 0 END) as 'Verrouillés',
    SUM(CASE WHEN premier_connexion = 1 THEN 1 ELSE 0 END) as 'Première connexion'
FROM utilisateurs;

-- ================================================================================
-- 5. LISTE DES UTILISATEURS EN PREMIÈRE CONNEXION
-- ================================================================================

SELECT
    '=== UTILISATEURS EN PREMIÈRE CONNEXION ===' AS '';

SELECT
    id,
    nom_utilisateur,
    nom_complet,
    email,
    mot_de_passe_par_defaut as 'Mot de passe par défaut',
    date_creation as 'Date création',
    actif as 'Actif'
FROM utilisateurs
WHERE premier_connexion = TRUE
ORDER BY date_creation DESC;

-- ================================================================================
-- 6. LOG DE LA MIGRATION
-- ================================================================================

INSERT INTO logs_activite (nom_utilisateur, action, module, details, resultat)
VALUES (
    'SYSTEM',
    'MIGRATION_RESET_PASSWORD',
    'Système',
    'Migration du système de réinitialisation de mot de passe - Ajout colonnes premier_connexion et mot_de_passe_par_defaut',
    'SUCCESS'
);

SELECT '=== MIGRATION TERMINÉE AVEC SUCCÈS ===' AS '';

-- ================================================================================
-- NOTES D'UTILISATION
-- ================================================================================

/*
UTILISATION DU SYSTÈME DE RÉINITIALISATION:

1. Réinitialiser le mot de passe d'un utilisateur (via l'interface admin):
   - Aller dans Système > Gestion des utilisateurs
   - Sélectionner un utilisateur
   - Cliquer sur "Réinitialiser mot de passe"
   - Le mot de passe sera défini sur: RHPlus2026!
   - L'utilisateur sera forcé de le changer à la prochaine connexion

2. Réinitialiser manuellement via SQL (usage avancé):

   UPDATE utilisateurs
   SET mot_de_passe_hash = '$2a$11$7rLSvRVyTfIapPwVu.bEy.KqV5pDfKCrRfKpG/QNVQbW7VqkN6qZG',
       premier_connexion = TRUE,
       mot_de_passe_par_defaut = 'RHPlus2026!',
       tentatives_echec = 0,
       compte_verrouille = 0,
       date_modification = NOW()
   WHERE nom_utilisateur = 'nom_utilisateur_ici';

3. Mot de passe oublié (côté utilisateur):
   - Cliquer sur "Mot de passe oublié ?" sur l'écran de connexion
   - Contacter l'administrateur avec les informations affichées
   - L'admin réinitialise le mot de passe via l'interface

4. Sécurité:
   - Mot de passe par défaut: RHPlus2026!
   - Règles: 8+ caractères, majuscule, minuscule, chiffre, caractère spécial
   - Changement obligatoire à la première connexion
   - Verrouillage après 5 tentatives échouées
*/
