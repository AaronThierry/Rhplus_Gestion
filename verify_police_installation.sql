-- =====================================================
-- Script de vérification de l'installation du numéro de police
-- =====================================================
-- Exécutez ce script pour vérifier que tout fonctionne correctement
-- =====================================================

-- 1. Vérifier que la colonne police existe
SELECT
    COLUMN_NAME,
    COLUMN_TYPE,
    IS_NULLABLE,
    COLUMN_KEY,
    COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'personnel'
AND COLUMN_NAME = 'police';

-- Résultat attendu: 1 ligne avec COLUMN_NAME = 'police', COLUMN_TYPE = 'varchar(20)', COLUMN_KEY = 'UNI'

-- 2. Vérifier que la table police_sequence existe
SELECT
    TABLE_NAME,
    TABLE_ROWS,
    CREATE_TIME,
    UPDATE_TIME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME = 'police_sequence';

-- Résultat attendu: 1 ligne

-- 3. Vérifier le contenu de la table police_sequence
SELECT
    id,
    last_value,
    created_at,
    updated_at
FROM police_sequence;

-- Résultat attendu: 1 ligne avec last_value > 0 si des employés existent

-- 4. Statistiques sur les numéros de police
SELECT
    COUNT(*) AS total_employes,
    COUNT(police) AS employes_avec_police,
    COUNT(*) - COUNT(police) AS employes_sans_police,
    MIN(police) AS premier_numero,
    MAX(police) AS dernier_numero
FROM personnel;

-- Résultat attendu: employes_sans_police = 0 (tous les employés doivent avoir un numéro)

-- 5. Vérifier l'unicité des numéros de police
SELECT
    police,
    COUNT(*) AS nombre_doublons
FROM personnel
WHERE police IS NOT NULL
GROUP BY police
HAVING COUNT(*) > 1;

-- Résultat attendu: 0 lignes (aucun doublon)

-- 6. Vérifier le format des numéros de police (XXXAXXX - 7 caractères)
SELECT
    COUNT(*) AS total_avec_police,
    SUM(CASE WHEN police REGEXP '^[0-9]{3}[A-Z]{1}[0-9]{3}$' THEN 1 ELSE 0 END) AS format_correct,
    SUM(CASE WHEN police NOT REGEXP '^[0-9]{3}[A-Z]{1}[0-9]{3}$' THEN 1 ELSE 0 END) AS format_incorrect,
    SUM(CASE WHEN LENGTH(police) != 7 THEN 1 ELSE 0 END) AS longueur_incorrecte
FROM personnel
WHERE police IS NOT NULL;

-- Résultat attendu: format_incorrect = 0 et longueur_incorrecte = 0
-- (tous les numéros doivent avoir le format XXXAXXX, ex: 123A456)

-- 7. Afficher les 10 premiers employés par numéro de police
SELECT
    police,
    matricule,
    nomPrenom,
    poste,
    date_entree,
    id_personnel
FROM personnel
WHERE police IS NOT NULL
ORDER BY police ASC
LIMIT 10;

-- 8. Afficher les 10 derniers employés créés
SELECT
    police,
    matricule,
    nomPrenom,
    poste,
    date_entree,
    id_personnel
FROM personnel
WHERE police IS NOT NULL
ORDER BY police DESC
LIMIT 10;

-- 9. Afficher quelques exemples de numéros générés
SELECT
    'Exemples de numéros aléatoires' AS information,
    GROUP_CONCAT(police ORDER BY police SEPARATOR ', ') AS exemples
FROM (
    SELECT police FROM personnel WHERE police IS NOT NULL LIMIT 10
) AS sample;

-- =====================================================
-- RÉSUMÉ
-- =====================================================
SELECT
    '✓ Installation vérifiée' AS statut,
    CONCAT(COUNT(*), ' employés ont un numéro de police') AS message,
    MIN(police) AS exemple_numero_1,
    MAX(police) AS exemple_numero_2,
    CONCAT(
        FLOOR(RAND() * 1000),
        CHAR(65 + FLOOR(RAND() * 26)),
        FLOOR(RAND() * 1000)
    ) AS exemple_prochain_format
FROM personnel
WHERE police IS NOT NULL;

-- =====================================================
-- Si toutes les vérifications passent, l'installation est correcte
-- =====================================================
