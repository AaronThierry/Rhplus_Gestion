-- Ajout de la colonne Conformite dans la table personnel
-- Cette colonne indique si un employé est conforme ou non conforme
-- Valeur par défaut: 1 (Conforme)

-- Vérifier si la colonne existe déjà
SELECT COUNT(*) INTO @colExists
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = DATABASE()
AND TABLE_NAME = 'personnel'
AND COLUMN_NAME = 'Conformite';

-- Ajouter la colonne si elle n'existe pas
SET @sql = IF(@colExists = 0,
    'ALTER TABLE personnel ADD COLUMN Conformite BIT NOT NULL DEFAULT 1',
    'SELECT "La colonne Conformite existe déjà dans la table personnel" AS Message'
);

PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- Vérifier l'ajout
SELECT
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE,
    COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = DATABASE()
AND TABLE_NAME = 'personnel'
AND COLUMN_NAME = 'Conformite';
