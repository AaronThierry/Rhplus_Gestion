-- =====================================================
-- Script de génération des numéros de police pour les employés existants
-- =====================================================
-- Ce script génère automatiquement des numéros de police aléatoires
-- pour tous les employés qui n'en ont pas encore
-- Format: XXXAXXX (7 caractères - 3 chiffres + 1 lettre + 3 chiffres)
-- Exemples: 123A456, 789B012, 456C789
-- =====================================================

DELIMITER $$

-- Fonction pour générer un numéro de police aléatoire
DROP FUNCTION IF EXISTS generer_numero_police$$
CREATE FUNCTION generer_numero_police()
RETURNS VARCHAR(7)
DETERMINISTIC
BEGIN
    DECLARE numero VARCHAR(7);
    DECLARE partie1 INT;
    DECLARE lettre CHAR(1);
    DECLARE partie2 INT;
    DECLARE tentatives INT DEFAULT 0;
    DECLARE existe INT;

    -- Essayer de générer un numéro unique (max 100 tentatives)
    WHILE tentatives < 100 DO
        -- Générer 3 premiers chiffres (000-999)
        SET partie1 = FLOOR(RAND() * 1000);

        -- Générer 1 lettre aléatoire (A-Z)
        SET lettre = CHAR(65 + FLOOR(RAND() * 26));

        -- Générer 3 derniers chiffres (000-999)
        SET partie2 = FLOOR(RAND() * 1000);

        -- Formater le numéro: 123A456
        SET numero = CONCAT(
            LPAD(partie1, 3, '0'),
            lettre,
            LPAD(partie2, 3, '0')
        );

        -- Vérifier si le numéro existe déjà
        SELECT COUNT(*) INTO existe FROM personnel WHERE police = numero;

        IF existe = 0 THEN
            RETURN numero;
        END IF;

        SET tentatives = tentatives + 1;
    END WHILE;

    -- Si échec après 100 tentatives, retourner NULL
    RETURN NULL;
END$$

DELIMITER ;

-- Générer les numéros de police pour les employés sans numéro
-- On utilise une procédure pour gérer les employés un par un
UPDATE personnel
SET police = generer_numero_police()
WHERE police IS NULL
ORDER BY date_entree ASC, id_personnel ASC;

-- Afficher le résultat
SELECT
    CONCAT('✓ ', COUNT(*), ' numéros de police générés avec succès') AS resultat,
    MIN(police) AS premier_numero,
    MAX(police) AS dernier_numero
FROM personnel
WHERE police IS NOT NULL;

-- Afficher quelques exemples
SELECT
    id_personnel,
    police,
    matricule,
    nomPrenom,
    date_entree,
    poste
FROM personnel
WHERE police IS NOT NULL
ORDER BY date_entree ASC
LIMIT 20;

-- =====================================================
-- VÉRIFICATION
-- =====================================================

-- 1. Vérifier qu'il n'y a plus d'employés sans numéro de police
SELECT
    'Employés sans numéro de police' AS verification,
    COUNT(*) AS nombre,
    CASE
        WHEN COUNT(*) = 0 THEN '✓ OK'
        ELSE '✗ ERREUR'
    END AS statut
FROM personnel
WHERE police IS NULL;

-- 2. Vérifier l'unicité des numéros
SELECT
    'Unicité des numéros' AS verification,
    COUNT(*) AS total_numeros,
    COUNT(DISTINCT police) AS numeros_uniques,
    CASE
        WHEN COUNT(*) = COUNT(DISTINCT police) THEN '✓ OK'
        ELSE '✗ ERREUR - Doublons détectés'
    END AS statut
FROM personnel
WHERE police IS NOT NULL;

-- 3. Vérifier le format (7 caractères)
SELECT
    'Format des numéros (7 caractères)' AS verification,
    COUNT(*) AS total_numeros,
    SUM(CASE WHEN LENGTH(police) = 7 THEN 1 ELSE 0 END) AS format_correct,
    CASE
        WHEN COUNT(*) = SUM(CASE WHEN LENGTH(police) = 7 THEN 1 ELSE 0 END) THEN '✓ OK'
        ELSE '✗ ERREUR - Format incorrect détecté'
    END AS statut
FROM personnel
WHERE police IS NOT NULL;

-- Nettoyer la fonction temporaire
DROP FUNCTION IF EXISTS generer_numero_police;

-- =====================================================
-- Si toutes les vérifications sont OK, la génération est réussie
-- =====================================================
