-- Script de création de la table sursalaire
-- Gestion des sursalaires pour les employés

CREATE TABLE IF NOT EXISTS `sursalaire` (
  `id_sursalaire` INT NOT NULL AUTO_INCREMENT,
  `id_personnel` INT NOT NULL,
  `nom` VARCHAR(100) NOT NULL,
  `description` VARCHAR(255) DEFAULT NULL,
  `montant` DECIMAL(15,2) NOT NULL DEFAULT 0.00,
  `date_creation` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `date_modification` TIMESTAMP NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`id_sursalaire`),
  INDEX `idx_personnel` (`id_personnel`),
  INDEX `idx_nom` (`nom`),
  CONSTRAINT `fk_sursalaire_personnel`
    FOREIGN KEY (`id_personnel`)
    REFERENCES `personnel` (`id_personnel`)
    ON DELETE CASCADE
    ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_general_ci;

-- Exemples d'insertion (commentés par défaut)
-- INSERT INTO sursalaire (id_personnel, nom, description, montant)
-- VALUES
--   (1, 'Prime de rendement', 'Prime mensuelle pour performance exceptionnelle', 50000),
--   (1, 'Prime d''ancienneté', 'Prime pour 5 ans de service', 25000),
--   (2, 'Prime de risque', 'Prime pour conditions de travail difficiles', 30000);
