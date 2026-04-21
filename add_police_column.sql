-- =====================================================
-- Script d'ajout de la colonne Police (Numéro système)
-- =====================================================
-- Ce script ajoute une colonne "police" unique
-- pour chaque employé dans la table personnel
-- Format: XXXAXXX (7 caractères - 3 chiffres + 1 lettre + 3 chiffres)
-- Exemple: 123A456, 789B012, 456C789
-- =====================================================

-- Ajouter la colonne police (VARCHAR(7) pour format XXXAXXX)
ALTER TABLE personnel
ADD COLUMN police VARCHAR(7) NULL UNIQUE
COMMENT 'Numéro de police unique - Format: XXXAXXX (ex: 123A456)';

-- Ajouter un index pour optimiser les recherches
CREATE INDEX idx_police ON personnel(police);

-- =====================================================
-- NOTE IMPORTANTE
-- =====================================================
-- Après l'exécution de ce script, exécutez le script
-- "generate_police_numbers.sql" pour générer les numéros
-- de police aléatoires pour les employés existants
-- =====================================================
