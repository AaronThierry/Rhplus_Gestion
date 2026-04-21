-- =====================================================
-- SCRIPT OPTIMAL - Génération des numéros de police
-- =====================================================
-- Ce script génère TOUS les numéros en une seule exécution
-- Compatible HeidiSQL, phpMyAdmin, MySQL Workbench
-- =====================================================

-- ÉTAPE 1: Compter les employés à traiter
SELECT
    COUNT(*) AS total_employes,
    COUNT(police) AS deja_avec_numero,
    COUNT(*) - COUNT(police) AS a_generer
FROM personnel;

-- ÉTAPE 2: Générer TOUS les numéros (sans limite)
UPDATE personnel
SET police = CONCAT(
    LPAD(FLOOR(RAND() * 1000), 3, '0'),
    CHAR(65 + FLOOR(RAND() * 26)),
    LPAD(FLOOR(RAND() * 1000), 3, '0')
)
WHERE police IS NULL;

-- ÉTAPE 3: Vérifier le résultat
SELECT
    '✓ GÉNÉRATION TERMINÉE' AS statut,
    COUNT(*) AS total_employes,
    COUNT(police) AS avec_numero,
    COUNT(*) - COUNT(police) AS sans_numero,
    CONCAT(ROUND(COUNT(police) * 100.0 / COUNT(*), 1), '%') AS pourcentage
FROM personnel;

-- ÉTAPE 4: Vérifier l'unicité (IMPORTANT)
SELECT
    CASE
        WHEN COUNT(*) = COUNT(DISTINCT police) THEN '✓ AUCUN DOUBLON'
        ELSE CONCAT('⚠ ', COUNT(*) - COUNT(DISTINCT police), ' DOUBLONS DÉTECTÉS')
    END AS verification_unicite,
    COUNT(*) AS total_numeros,
    COUNT(DISTINCT police) AS numeros_uniques
FROM personnel
WHERE police IS NOT NULL;

-- ÉTAPE 5: Afficher les doublons (s'il y en a)
SELECT
    police,
    COUNT(*) AS nombre_fois,
    GROUP_CONCAT(id_personnel) AS ids_concernes
FROM personnel
WHERE police IS NOT NULL
GROUP BY police
HAVING COUNT(*) > 1;

-- ÉTAPE 6: Exemples de numéros générés
SELECT
    police,
    matricule,
    nomPrenom,
    date_entree
FROM personnel
WHERE police IS NOT NULL
ORDER BY date_entree ASC
LIMIT 20;

-- =====================================================
-- SI DES DOUBLONS SONT DÉTECTÉS (très rare)
-- Décommentez et exécutez cette section:
-- =====================================================
/*
-- Corriger les doublons
UPDATE personnel p1
SET police = CONCAT(
    LPAD(FLOOR(RAND() * 1000), 3, '0'),
    CHAR(65 + FLOOR(RAND() * 26)),
    LPAD(FLOOR(RAND() * 1000), 3, '0')
)
WHERE police IN (
    SELECT police FROM (
        SELECT police FROM personnel
        GROUP BY police HAVING COUNT(*) > 1
    ) AS doublons
)
AND id_personnel NOT IN (
    SELECT MIN(id_personnel) FROM (
        SELECT id_personnel, police FROM personnel
    ) AS temp
    GROUP BY police
);

-- Revérifier
SELECT
    CASE
        WHEN COUNT(*) = COUNT(DISTINCT police) THEN '✓ DOUBLONS CORRIGÉS'
        ELSE '✗ Encore des doublons'
    END AS statut
FROM personnel WHERE police IS NOT NULL;
*/
