-- =====================================================
-- Script de correction des doublons de numéros de police
-- =====================================================
-- À exécuter UNIQUEMENT si des doublons ont été détectés
-- =====================================================

-- 1. Afficher les doublons existants
SELECT
    'DOUBLONS DÉTECTÉS' AS alerte,
    police,
    COUNT(*) AS nombre_occurrences,
    GROUP_CONCAT(id_personnel) AS ids_concernes
FROM personnel
WHERE police IS NOT NULL
GROUP BY police
HAVING COUNT(*) > 1;

-- 2. Mettre à NULL les numéros en doublon (sauf le premier)
-- Cela permet de les régénérer ensuite
UPDATE personnel p1
SET police = NULL
WHERE police IN (
    SELECT police FROM (
        SELECT police
        FROM personnel
        WHERE police IS NOT NULL
        GROUP BY police
        HAVING COUNT(*) > 1
    ) AS doublons
)
AND id_personnel NOT IN (
    -- Garder le premier employé avec ce numéro
    SELECT MIN(id_personnel) FROM (
        SELECT id_personnel, police
        FROM personnel
        WHERE police IS NOT NULL
    ) AS temp
    WHERE police = p1.police
);

-- 3. Régénérer les numéros pour les employés sans numéro (anciens doublons)
UPDATE personnel
SET police = CONCAT(
    LPAD(FLOOR(RAND() * 1000), 3, '0'),
    CHAR(65 + FLOOR(RAND() * 26)),
    LPAD(FLOOR(RAND() * 1000), 3, '0')
)
WHERE police IS NULL;

-- 4. Vérifier qu'il n'y a plus de doublons
SELECT
    'VÉRIFICATION APRÈS CORRECTION' AS statut,
    COUNT(*) AS total_numeros,
    COUNT(DISTINCT police) AS numeros_uniques,
    COUNT(*) - COUNT(DISTINCT police) AS doublons_restants,
    CASE
        WHEN COUNT(*) = COUNT(DISTINCT police) THEN '✓ AUCUN DOUBLON'
        ELSE '✗ DOUBLONS ENCORE PRÉSENTS - Réexécuter'
    END AS resultat
FROM personnel
WHERE police IS NOT NULL;

-- Si encore des doublons, afficher lesquels
SELECT
    police,
    COUNT(*) AS occurrences
FROM personnel
WHERE police IS NOT NULL
GROUP BY police
HAVING COUNT(*) > 1;
