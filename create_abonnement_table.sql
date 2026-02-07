-- Script de création de la table abonnement
-- Gestion des abonnements pour les employés

CREATE TABLE IF NOT EXISTS `abonnement` (
  `id_abonnement` INT NOT NULL AUTO_INCREMENT,
  `id_personnel` INT NOT NULL,
  `nom` VARCHAR(100) NOT NULL,
  `description` VARCHAR(255) DEFAULT NULL,
  `date_debut` DATE NOT NULL,
  `date_fin` DATE NOT NULL,
  `montant` DECIMAL(15,2) NOT NULL DEFAULT 0.00,
  `date_creation` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `date_modification` TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id_abonnement`),
  INDEX `idx_personnel` (`id_personnel`),
  INDEX `idx_nom` (`nom`),
  INDEX `idx_date_fin` (`date_fin`),
  UNIQUE INDEX `idx_unique_personnel` (`id_personnel`),
  CONSTRAINT `fk_abonnement_personnel`
    FOREIGN KEY (`id_personnel`)
    REFERENCES `personnel` (`id_personnel`)
    ON DELETE CASCADE
    ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- Exemples d'insertion (commentés par défaut)
-- INSERT INTO abonnement (id_personnel, nom, description, date_debut, date_fin, montant)
-- VALUES
--   (1, 'Abonnement Internet', 'Internet Fibre 100Mbps', '2025-01-01', '2025-12-31', 15000),
--   (2, 'Abonnement Téléphone', 'Forfait mobile illimité', '2025-01-01', '2025-06-30', 10000);
