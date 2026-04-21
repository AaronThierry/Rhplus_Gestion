-- Script de création de la table de séquence pour les matricules
-- Cette table permet de générer automatiquement des matricules uniques au format: XX###A
-- où XX = initiales de l'entreprise, ### = numéro séquentiel (001-999), A = lettre (A-Z)

-- Vérifier si la table existe déjà
DROP TABLE IF EXISTS seq_matricule;

-- Créer la table de séquence
CREATE TABLE seq_matricule (
    id BIGINT AUTO_INCREMENT PRIMARY KEY,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Insérer une première ligne pour initialiser la séquence (optionnel)
-- INSERT INTO seq_matricule() VALUES ();

-- Vérifier la création
SELECT 'Table seq_matricule créée avec succès' AS Status;
SELECT COUNT(*) AS NombreLignes FROM seq_matricule;
