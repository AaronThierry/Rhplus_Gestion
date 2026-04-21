-- =====================================================
-- MIGRATION COMPLÈTE: Numéro de Police
-- =====================================================
-- Ce script combine toutes les étapes nécessaires pour
-- implémenter le numéro de police dans votre base de données
-- =====================================================
-- Date: 2026-04-01
-- Version: 1.0
-- Auteur: Système RH GMP
-- =====================================================

-- =====================================================
-- ÉTAPE 1: SAUVEGARDE ET VÉRIFICATION
-- =====================================================

-- Afficher la version de MySQL
SELECT VERSION() AS mysql_version;

-- Compter les employés actuels
SELECT COUNT(*) AS total_employes_avant_migration
FROM personnel;

-- =====================================================
-- ÉTAPE 2: CRÉATION DE LA STRUCTURE
-- =====================================================

-- 2.1. Créer la table de séquence pour les numéros de police
CREATE TABLE IF NOT EXISTS police_sequence (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    last_value INT NOT NULL DEFAULT 0,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 2.2. Initialiser la séquence
INSERT INTO police_sequence (last_value)
SELECT 0
WHERE NOT EXISTS (SELECT 1 FROM police_sequence LIMIT 1);

-- 2.3. Ajouter la colonne police à la table personnel
-- Vérifier d'abord si la colonne existe déjà
SET @column_exists = (
    SELECT COUNT(*)
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_SCHEMA = DATABASE()
    AND TABLE_NAME = 'personnel'
    AND COLUMN_NAME = 'police'
);

-- Ajouter la colonne seulement si elle n'existe pas
SET @sql_add_column = IF(
    @column_exists = 0,
    'ALTER TABLE personnel ADD COLUMN police VARCHAR(20) NULL UNIQUE',
    'SELECT "La colonne police existe déjà" AS message'
);

PREPARE stmt FROM @sql_add_column;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- =====================================================
-- ÉTAPE 3: GÉNÉRATION DES NUMÉROS POUR EMPLOYÉS EXISTANTS
-- =====================================================

-- Compter les employés sans numéro de police
SELECT COUNT(*) AS employes_sans_police
FROM personnel
WHERE police IS NULL;

-- Générer les numéros de police
-- Les employés les plus anciens (date_entree) obtiennent les plus petits numéros
SET @counter = (SELECT COALESCE(last_value, 0) FROM police_sequence WHERE id = 1);

UPDATE personnel
SET police = CONCAT('POL-', LPAD(@counter := @counter + 1, 6, '0'))
WHERE police IS NULL
ORDER BY date_entree ASC, id_personnel ASC;

-- Mettre à jour le compteur de séquence
UPDATE police_sequence
SET last_value = @counter
WHERE id = 1;

-- =====================================================
-- ÉTAPE 4: VÉRIFICATIONS POST-MIGRATION
-- =====================================================

-- 4.1. Vérifier que tous les employés ont un numéro
SELECT
    'VÉRIFICATION 1: Employés avec numéro de police' AS test,
    COUNT(*) AS total_employes,
    COUNT(police) AS employes_avec_police,
    COUNT(*) - COUNT(police) AS employes_sans_police,
    CASE
        WHEN COUNT(*) = COUNT(police) THEN '✓ PASS'
        ELSE '✗ ÉCHEC'
    END AS statut
FROM personnel;

-- 4.2. Vérifier l'unicité des numéros
SELECT
    'VÉRIFICATION 2: Unicité des numéros' AS test,
    COUNT(*) AS total_numéros,
    COUNT(DISTINCT police) AS numéros_uniques,
    CASE
        WHEN COUNT(*) = COUNT(DISTINCT police) THEN '✓ PASS'
        ELSE '✗ ÉCHEC'
    END AS statut
FROM personnel
WHERE police IS NOT NULL;

-- 4.3. Vérifier le format des numéros
SELECT
    'VÉRIFICATION 3: Format des numéros' AS test,
    COUNT(*) AS total_numéros,
    SUM(CASE WHEN police REGEXP '^POL-[0-9]{6}$' THEN 1 ELSE 0 END) AS format_correct,
    SUM(CASE WHEN police NOT REGEXP '^POL-[0-9]{6}$' THEN 1 ELSE 0 END) AS format_incorrect,
    CASE
        WHEN SUM(CASE WHEN police NOT REGEXP '^POL-[0-9]{6}$' THEN 1 ELSE 0 END) = 0 THEN '✓ PASS'
        ELSE '✗ ÉCHEC'
    END AS statut
FROM personnel
WHERE police IS NOT NULL;

-- 4.4. Vérifier la cohérence de la séquence
SELECT
    'VÉRIFICATION 4: Cohérence de la séquence' AS test,
    ps.last_value AS compteur_sequence,
    (SELECT COUNT(*) FROM personnel WHERE police IS NOT NULL) AS nombre_employes,
    (SELECT CAST(SUBSTRING(MAX(police), 5) AS UNSIGNED) FROM personnel WHERE police IS NOT NULL) AS dernier_numero_attribue,
    CASE
        WHEN ps.last_value = (SELECT COUNT(*) FROM personnel WHERE police IS NOT NULL)
        AND ps.last_value = (SELECT CAST(SUBSTRING(MAX(police), 5) AS UNSIGNED) FROM personnel WHERE police IS NOT NULL)
        THEN '✓ PASS'
        ELSE '✗ ÉCHEC'
    END AS statut
FROM police_sequence ps
WHERE id = 1;

-- =====================================================
-- ÉTAPE 5: AFFICHAGE DES RÉSULTATS
-- =====================================================

-- Afficher les 10 premiers numéros attribués
SELECT
    '--- 10 PREMIERS NUMÉROS ATTRIBUÉS ---' AS section,
    police,
    matricule,
    nomPrenom,
    date_entree
FROM personnel
WHERE police IS NOT NULL
ORDER BY police ASC
LIMIT 10;

-- Afficher les 10 derniers numéros attribués
SELECT
    '--- 10 DERNIERS NUMÉROS ATTRIBUÉS ---' AS section,
    police,
    matricule,
    nomPrenom,
    date_entree
FROM personnel
WHERE police IS NOT NULL
ORDER BY police DESC
LIMIT 10;

-- =====================================================
-- RÉSUMÉ DE LA MIGRATION
-- =====================================================

SELECT
    '╔═══════════════════════════════════════════════════╗' AS ligne
UNION ALL
SELECT '║       RÉSUMÉ DE LA MIGRATION - NUMÉRO DE POLICE       ║'
UNION ALL
SELECT '╠═══════════════════════════════════════════════════╣'
UNION ALL
SELECT CONCAT('║ Total employés: ', LPAD(COUNT(*), 31, ' '), ' ║')
FROM personnel
UNION ALL
SELECT CONCAT('║ Employés avec numéro: ', LPAD(COUNT(police), 25, ' '), ' ║')
FROM personnel
UNION ALL
SELECT CONCAT('║ Premier numéro: ', LPAD(MIN(police), 31, ' '), ' ║')
FROM personnel WHERE police IS NOT NULL
UNION ALL
SELECT CONCAT('║ Dernier numéro: ', LPAD(MAX(police), 31, ' '), ' ║')
FROM personnel WHERE police IS NOT NULL
UNION ALL
SELECT CONCAT('║ Prochain numéro: POL-', LPAD(last_value + 1, 6, '0'), LPAD('', 19, ' '), ' ║')
FROM police_sequence WHERE id = 1
UNION ALL
SELECT '╠═══════════════════════════════════════════════════╣'
UNION ALL
SELECT CASE
    WHEN (SELECT COUNT(*) FROM personnel) = (SELECT COUNT(police) FROM personnel WHERE police IS NOT NULL)
    THEN '║                ✓ MIGRATION RÉUSSIE                   ║'
    ELSE '║                ✗ MIGRATION ÉCHOUÉE                   ║'
END
UNION ALL
SELECT '╚═══════════════════════════════════════════════════╝';

-- =====================================================
-- NOTES IMPORTANTES
-- =====================================================
--
-- 1. Cette migration est IDEMPOTENTE: vous pouvez l'exécuter
--    plusieurs fois sans risque de corruption des données
--
-- 2. Les numéros de police déjà attribués ne seront pas modifiés
--
-- 3. Seuls les employés sans numéro de police recevront un nouveau numéro
--
-- 4. Le compteur de séquence est mis à jour automatiquement
--
-- 5. Pour annuler la migration, exécutez:
--    ALTER TABLE personnel DROP COLUMN police;
--    DROP TABLE police_sequence;
--
-- =====================================================
