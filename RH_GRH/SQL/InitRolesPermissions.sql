-- Script d'initialisation des tables pour la gestion des rôles et permissions
-- Base de données RH+ Gestion

-- ========================================
-- TABLE: permissions
-- ========================================
CREATE TABLE IF NOT EXISTS `permissions` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `nom_permission` VARCHAR(100) NOT NULL UNIQUE,
  `description` TEXT,
  `module` VARCHAR(50) NOT NULL,
  `action` VARCHAR(50) NOT NULL,
  `date_creation` DATETIME DEFAULT CURRENT_TIMESTAMP,
  INDEX `idx_module` (`module`),
  INDEX `idx_action` (`action`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABLE: roles
-- ========================================
CREATE TABLE IF NOT EXISTS `roles` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `nom_role` VARCHAR(100) NOT NULL UNIQUE,
  `description` TEXT,
  `niveau_acces` INT DEFAULT 1,
  `date_creation` DATETIME DEFAULT CURRENT_TIMESTAMP,
  INDEX `idx_niveau_acces` (`niveau_acces`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABLE: role_permissions
-- ========================================
CREATE TABLE IF NOT EXISTS `role_permissions` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `role_id` INT NOT NULL,
  `permission_id` INT NOT NULL,
  `date_attribution` DATETIME DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (`role_id`) REFERENCES `roles`(`id`) ON DELETE CASCADE,
  FOREIGN KEY (`permission_id`) REFERENCES `permissions`(`id`) ON DELETE CASCADE,
  UNIQUE KEY `unique_role_permission` (`role_id`, `permission_id`),
  INDEX `idx_role_id` (`role_id`),
  INDEX `idx_permission_id` (`permission_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- TABLE: utilisateur_roles
-- ========================================
CREATE TABLE IF NOT EXISTS `utilisateur_roles` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `utilisateur_id` INT NOT NULL,
  `role_id` INT NOT NULL,
  `date_attribution` DATETIME DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (`utilisateur_id`) REFERENCES `utilisateurs`(`id`) ON DELETE CASCADE,
  FOREIGN KEY (`role_id`) REFERENCES `roles`(`id`) ON DELETE CASCADE,
  UNIQUE KEY `unique_user_role` (`utilisateur_id`, `role_id`),
  INDEX `idx_utilisateur_id` (`utilisateur_id`),
  INDEX `idx_role_id_user` (`role_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ========================================
-- PERMISSIONS PAR DÉFAUT
-- ========================================
INSERT IGNORE INTO `permissions` (`nom_permission`, `description`, `module`, `action`) VALUES
-- Administration
('Administration.Entreprise', 'Gérer les entreprises', 'Administration', 'Entreprise'),
('Administration.Direction', 'Gérer les directions', 'Administration', 'Direction'),
('Administration.Service', 'Gérer les services', 'Administration', 'Service'),
('Administration.Categorie', 'Gérer les catégories', 'Administration', 'Categorie'),

-- Gestion
('Gestion.Personnel', 'Accéder à la section Personnel', 'Gestion', 'Personnel'),
('Gestion.Salaire', 'Accéder à la section Salaire', 'Gestion', 'Salaire'),

-- Personnel
('Personnel.Employes', 'Gérer les employés', 'Personnel', 'Employes'),
('Personnel.Charges', 'Gérer les charges familiales', 'Personnel', 'Charges'),
('Personnel.Indemnites', 'Gérer les indemnités', 'Personnel', 'Indemnites'),

-- Salaire
('Salaire.Sursalaires', 'Gérer les sursalaires', 'Salaire', 'Sursalaires'),
('Salaire.Horaires', 'Gérer les horaires', 'Salaire', 'Horaires'),
('Salaire.Journalier', 'Gérer le journalier', 'Salaire', 'Journalier');

-- ========================================
-- RÔLE ADMINISTRATEUR PAR DÉFAUT
-- ========================================
INSERT IGNORE INTO `roles` (`nom_role`, `description`, `niveau_acces`) VALUES
('Administrateur', 'Administrateur système avec tous les droits', 100);

-- ========================================
-- ATTRIBUER TOUTES LES PERMISSIONS À L'ADMINISTRATEUR
-- ========================================
INSERT IGNORE INTO `role_permissions` (`role_id`, `permission_id`)
SELECT
    (SELECT id FROM roles WHERE nom_role = 'Administrateur'),
    id
FROM permissions;

-- ========================================
-- RÔLES PRÉDÉFINIS
-- ========================================
INSERT IGNORE INTO `roles` (`nom_role`, `description`, `niveau_acces`) VALUES
('Gestionnaire RH', 'Gestionnaire des ressources humaines', 80),
('Gestionnaire Paie', 'Gestionnaire de la paie et des salaires', 70),
('Responsable Personnel', 'Responsable de la gestion du personnel', 60),
('Consultant RH', 'Consultation uniquement - accès en lecture', 30);

-- ========================================
-- PERMISSIONS POUR GESTIONNAIRE RH
-- ========================================
INSERT IGNORE INTO `role_permissions` (`role_id`, `permission_id`)
SELECT
    (SELECT id FROM roles WHERE nom_role = 'Gestionnaire RH'),
    id
FROM permissions
WHERE module IN ('Gestion', 'Personnel', 'Administration')
  AND nom_permission NOT LIKE '%Entreprise%';

-- ========================================
-- PERMISSIONS POUR GESTIONNAIRE PAIE
-- ========================================
INSERT IGNORE INTO `role_permissions` (`role_id`, `permission_id`)
SELECT
    (SELECT id FROM roles WHERE nom_role = 'Gestionnaire Paie'),
    id
FROM permissions
WHERE module IN ('Gestion', 'Salaire', 'Personnel')
  AND nom_permission IN ('Gestion.Salaire', 'Salaire.Sursalaires', 'Salaire.Horaires', 'Salaire.Journalier', 'Personnel.Employes');

-- ========================================
-- PERMISSIONS POUR RESPONSABLE PERSONNEL
-- ========================================
INSERT IGNORE INTO `role_permissions` (`role_id`, `permission_id`)
SELECT
    (SELECT id FROM roles WHERE nom_role = 'Responsable Personnel'),
    id
FROM permissions
WHERE module IN ('Gestion', 'Personnel')
  AND nom_permission IN ('Gestion.Personnel', 'Personnel.Employes', 'Personnel.Charges', 'Personnel.Indemnites');

-- Script terminé avec succès
SELECT 'Tables et données initiales créées avec succès!' AS Statut;
