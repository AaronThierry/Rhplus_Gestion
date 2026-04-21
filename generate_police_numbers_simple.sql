-- =====================================================
-- Script SIMPLIFIÉ de génération des numéros de police
-- =====================================================
-- Version sans fonction - compatible avec tous les clients SQL
-- Format: XXXAXXX (7 caractères - 3 chiffres + 1 lettre + 3 chiffres)
-- =====================================================

-- NOTE: Ce script génère des numéros de police pour les employés existants
-- Les numéros sont aléatoires mais générés côté SQL

-- Afficher le nombre d'employés sans numéro de police
SELECT
    COUNT(*) AS employes_sans_police,
    'Employés à traiter' AS description
FROM personnel
WHERE police IS NULL;

-- =====================================================
-- IMPORTANT: Exécutez ce bloc plusieurs fois si nécessaire
-- Chaque exécution génère des numéros pour un lot d'employés
-- =====================================================

-- Générer les numéros de police (par lot de 100 pour éviter les timeouts)
UPDATE personnel
SET police = CONCAT(
    LPAD(FLOOR(RAND() * 1000), 3, '0'),
    CHAR(65 + FLOOR(RAND() * 26)),
    LPAD(FLOOR(RAND() * 1000), 3, '0')
)
WHERE police IS NULL
  AND id_personnel IN (
      SELECT id_personnel
      FROM (
          SELECT id_personnel
          FROM personnel
          WHERE police IS NULL
          ORDER BY date_entree ASC, id_personnel ASC
          LIMIT 100
      ) AS temp
  );

-- Afficher le progrès
SELECT
    COUNT(CASE WHEN police IS NOT NULL THEN 1 END) AS employes_avec_police,
    COUNT(CASE WHEN police IS NULL THEN 1 END) AS employes_sans_police,
    COUNT(*) AS total_employes,
    CONCAT(
        ROUND((COUNT(CASE WHEN police IS NOT NULL THEN 1 END) * 100.0 / COUNT(*)), 2),
        '%'
    ) AS pourcentage_traite
FROM personnel;

-- Afficher quelques exemples
SELECT
    police,
    matricule,
    nomPrenom,
    date_entree
FROM personnel
WHERE police IS NOT NULL
ORDER BY date_entree ASC
LIMIT 10;

-- =====================================================
-- VÉRIFICATIONS
-- =====================================================

-- 1. Vérifier qu'il n'y a plus d'employés sans numéro
SELECT
    'Vérification: Employés sans police' AS test,
    COUNT(*) AS nombre,
    CASE WHEN COUNT(*) = 0 THEN '✓ OK' ELSE '⚠ Réexécuter le script' END AS statut
FROM personnel
WHERE police IS NULL;

-- 2. Vérifier l'unicité (TRÈS IMPORTANT)
SELECT
    'Vérification: Unicité des numéros' AS test,
    COUNT(*) AS total,
    COUNT(DISTINCT police) AS uniques,
    COUNT(*) - COUNT(DISTINCT police) AS doublons,
    CASE
        WHEN COUNT(*) = COUNT(DISTINCT police) THEN '✓ OK'
        ELSE '✗ DOUBLONS DÉTECTÉS'
    END AS statut
FROM personnel
WHERE police IS NOT NULL;

-- 3. Si des doublons existent, les afficher
SELECT
    police,
    COUNT(*) AS nombre_doublons,
    GROUP_CONCAT(id_personnel) AS ids_personnel
FROM personnel
WHERE police IS NOT NULL
GROUP BY police
HAVING COUNT(*) > 1;

-- 4. Vérifier le format (7 caractères)
SELECT
    'Vérification: Format correct' AS test,
    COUNT(*) AS total,
    SUM(CASE WHEN LENGTH(police) = 7 THEN 1 ELSE 0 END) AS format_ok,
    SUM(CASE WHEN LENGTH(police) != 7 THEN 1 ELSE 0 END) AS format_invalide,
    CASE
        WHEN COUNT(*) = SUM(CASE WHEN LENGTH(police) = 7 THEN 1 ELSE 0 END) THEN '✓ OK'
        ELSE '✗ FORMAT INCORRECT'
    END AS statut
FROM personnel
WHERE police IS NOT NULL;

-- =====================================================
-- EN CAS DE DOUBLONS (peu probable mais possible)
-- =====================================================
-- Si des doublons sont détectés, exécutez cette requête
-- pour régénérer les numéros en doublon:

-- UPDATE personnel p1
-- SET police = CONCAT(
--     LPAD(FLOOR(RAND() * 1000), 3, '0'),
--     CHAR(65 + FLOOR(RAND() * 26)),
--     LPAD(FLOOR(RAND() * 1000), 3, '0')
-- )
-- WHERE police IN (
--     SELECT police FROM (
--         SELECT police
--         FROM personnel
--         WHERE police IS NOT NULL
--         GROUP BY police
--         HAVING COUNT(*) > 1
--     ) AS doublons
-- );

-- =====================================================
-- RÉSUMÉ FINAL
-- =====================================================
SELECT
    '═══════════════════════════════════════════' AS ligne
UNION ALL
SELECT '   GÉNÉRATION DES NUMÉROS DE POLICE'
UNION ALL
SELECT '═══════════════════════════════════════════'
UNION ALL
SELECT CONCAT('Total employés: ', COUNT(*))
FROM personnel
UNION ALL
SELECT CONCAT('Avec numéro: ', COUNT(police))
FROM personnel WHERE police IS NOT NULL
UNION ALL
SELECT CONCAT('Sans numéro: ', COUNT(*))
FROM personnel WHERE police IS NULL
UNION ALL
SELECT CONCAT('Exemple: ', MIN(police))
FROM personnel WHERE police IS NOT NULL
UNION ALL
SELECT '═══════════════════════════════════════════'
UNION ALL
SELECT CASE
    WHEN (SELECT COUNT(*) FROM personnel WHERE police IS NULL) = 0
    THEN '✓ GÉNÉRATION TERMINÉE'
    ELSE '⚠ Réexécuter le script pour finir'
END;

-- =====================================================
-- INSTRUCTIONS:
-- =====================================================
-- 1. Si "employes_sans_police" > 0, réexécutez ce script
-- 2. Vérifiez qu'il n'y a pas de doublons
-- 3. Si doublons, exécutez la requête de correction ci-dessus
-- =====================================================
