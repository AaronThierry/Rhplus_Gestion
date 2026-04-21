-- ================================================================================
-- SYSTÈME D'AUTHENTIFICATION ET LOGS - GESTION MODERNE RH
-- Version: 1.2.0
-- Date: 2026-02-11
-- ================================================================================

-- Table des utilisateurs
CREATE TABLE IF NOT EXISTS utilisateurs (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nom_utilisateur VARCHAR(50) UNIQUE NOT NULL,
    mot_de_passe_hash VARCHAR(255) NOT NULL,
    nom_complet VARCHAR(100) NOT NULL,
    email VARCHAR(100),
    telephone VARCHAR(20),
    actif TINYINT(1) DEFAULT 1,
    date_creation DATETIME DEFAULT CURRENT_TIMESTAMP,
    date_modification DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    derniere_connexion DATETIME,
    
    tentatives_echec INT DEFAULT 0,
    compte_verrouille TINYINT(1) DEFAULT 0,
    INDEX idx_nom_utilisateur (nom_utilisateur),
    INDEX idx_actif (actif)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table des rôles
CREATE TABLE IF NOT EXISTS roles (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nom_role VARCHAR(50) UNIQUE NOT NULL,
    description TEXT,
    niveau_acces INT DEFAULT 1,
    date_creation DATETIME DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_nom_role (nom_role)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table de liaison utilisateur-rôles (many-to-many)
CREATE TABLE IF NOT EXISTS utilisateur_roles (
    id INT AUTO_INCREMENT PRIMARY KEY,
    utilisateur_id INT NOT NULL,
    role_id INT NOT NULL,
    date_attribution DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (utilisateur_id) REFERENCES utilisateurs(id) ON DELETE CASCADE,
    FOREIGN KEY (role_id) REFERENCES roles(id) ON DELETE CASCADE,
    UNIQUE KEY unique_user_role (utilisateur_id, role_id),
    INDEX idx_utilisateur (utilisateur_id),
    INDEX idx_role (role_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table des permissions
CREATE TABLE IF NOT EXISTS permissions (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nom_permission VARCHAR(100) UNIQUE NOT NULL,
    description TEXT,
    module VARCHAR(50) NOT NULL,
    action VARCHAR(50) NOT NULL,
    date_creation DATETIME DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_module (module),
    INDEX idx_action (action)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table de liaison rôle-permissions (many-to-many)
CREATE TABLE IF NOT EXISTS role_permissions (
    id INT AUTO_INCREMENT PRIMARY KEY,
    role_id INT NOT NULL,
    permission_id INT NOT NULL,
    date_attribution DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (role_id) REFERENCES roles(id) ON DELETE CASCADE,
    FOREIGN KEY (permission_id) REFERENCES permissions(id) ON DELETE CASCADE,
    UNIQUE KEY unique_role_permission (role_id, permission_id),
    INDEX idx_role (role_id),
    INDEX idx_permission (permission_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table des sessions
CREATE TABLE IF NOT EXISTS sessions (
    id INT AUTO_INCREMENT PRIMARY KEY,
    utilisateur_id INT NOT NULL,
    token VARCHAR(255) UNIQUE NOT NULL,
    date_debut DATETIME DEFAULT CURRENT_TIMESTAMP,
    date_fin DATETIME,
    ip_address VARCHAR(45),
    user_agent VARCHAR(255),
    actif TINYINT(1) DEFAULT 1,
    FOREIGN KEY (utilisateur_id) REFERENCES utilisateurs(id) ON DELETE CASCADE,
    INDEX idx_token (token),
    INDEX idx_utilisateur (utilisateur_id),
    INDEX idx_actif (actif)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table des logs d'activité
CREATE TABLE IF NOT EXISTS logs_activite (
    id INT AUTO_INCREMENT PRIMARY KEY,
    utilisateur_id INT,
    nom_utilisateur VARCHAR(50),
    action VARCHAR(100) NOT NULL,
    module VARCHAR(50) NOT NULL,
    details TEXT,
    ancien_etat TEXT,
    nouvel_etat TEXT,
    date_action DATETIME DEFAULT CURRENT_TIMESTAMP,
    ip_address VARCHAR(45),
    resultat ENUM('SUCCESS', 'FAILURE', 'WARNING') DEFAULT 'SUCCESS',
    FOREIGN KEY (utilisateur_id) REFERENCES utilisateurs(id) ON DELETE SET NULL,
    INDEX idx_utilisateur (utilisateur_id),
    INDEX idx_action (action),
    INDEX idx_module (module),
    INDEX idx_date (date_action),
    INDEX idx_resultat (resultat)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ================================================================================
-- DONNÉES INITIALES
-- ================================================================================

-- Insertion des rôles par défaut
INSERT INTO roles (nom_role, description, niveau_acces) VALUES
('Administrateur', 'Accès complet à toutes les fonctionnalités du système', 100),
('Gestionnaire RH', 'Gestion complète des employés et de la paie', 80),
('Assistant RH', 'Consultation et modifications limitées', 50),
('Comptable', 'Accès aux données financières et bulletins de paie', 60),
('Consultant', 'Consultation seule, aucune modification', 20)
ON DUPLICATE KEY UPDATE description=VALUES(description);

-- Insertion des permissions par défaut
INSERT INTO permissions (nom_permission, description, module, action) VALUES
-- Module Personnel
('PERSONNEL_VIEW', 'Consulter les employés', 'Personnel', 'VIEW'),
('PERSONNEL_ADD', 'Ajouter des employés', 'Personnel', 'ADD'),
('PERSONNEL_EDIT', 'Modifier des employés', 'Personnel', 'EDIT'),
('PERSONNEL_DELETE', 'Supprimer des employés', 'Personnel', 'DELETE'),
('PERSONNEL_IMPORT', 'Importer des employés en masse', 'Personnel', 'IMPORT'),

-- Module Salaire
('SALAIRE_VIEW', 'Consulter les salaires', 'Salaire', 'VIEW'),
('SALAIRE_PROCESS', 'Traiter les salaires', 'Salaire', 'PROCESS'),
('SALAIRE_EDIT', 'Modifier les salaires', 'Salaire', 'EDIT'),
('SALAIRE_EXPORT', 'Exporter les bulletins de paie', 'Salaire', 'EXPORT'),

-- Module Administration
('ADMIN_ENTREPRISE', 'Gérer les entreprises', 'Administration', 'ENTREPRISE'),
('ADMIN_CATEGORIES', 'Gérer les catégories', 'Administration', 'CATEGORIES'),
('ADMIN_SERVICES', 'Gérer les services', 'Administration', 'SERVICES'),
('ADMIN_DIRECTIONS', 'Gérer les directions', 'Administration', 'DIRECTIONS'),
('ADMIN_CHARGES', 'Gérer les charges', 'Administration', 'CHARGES'),
('ADMIN_INDEMNITES', 'Gérer les indemnités', 'Administration', 'INDEMNITES'),

-- Module Système
('SYSTEM_USERS', 'Gérer les utilisateurs', 'Système', 'USERS'),
('SYSTEM_ROLES', 'Gérer les rôles', 'Système', 'ROLES'),
('SYSTEM_LOGS', 'Consulter les logs', 'Système', 'LOGS'),
('SYSTEM_CONFIG', 'Configuration système', 'Système', 'CONFIG')
ON DUPLICATE KEY UPDATE description=VALUES(description);

-- Attribution des permissions au rôle Administrateur
INSERT INTO role_permissions (role_id, permission_id)
SELECT r.id, p.id
FROM roles r
CROSS JOIN permissions p
WHERE r.nom_role = 'Administrateur'
ON DUPLICATE KEY UPDATE role_id=role_id;

-- Attribution des permissions au rôle Gestionnaire RH
INSERT INTO role_permissions (role_id, permission_id)
SELECT r.id, p.id
FROM roles r
CROSS JOIN permissions p
WHERE r.nom_role = 'Gestionnaire RH'
AND p.nom_permission IN (
    'PERSONNEL_VIEW', 'PERSONNEL_ADD', 'PERSONNEL_EDIT', 'PERSONNEL_IMPORT',
    'SALAIRE_VIEW', 'SALAIRE_PROCESS', 'SALAIRE_EDIT', 'SALAIRE_EXPORT',
    'ADMIN_CATEGORIES', 'ADMIN_SERVICES', 'ADMIN_DIRECTIONS', 'ADMIN_CHARGES', 'ADMIN_INDEMNITES'
)
ON DUPLICATE KEY UPDATE role_id=role_id;

-- Attribution des permissions au rôle Assistant RH
INSERT INTO role_permissions (role_id, permission_id)
SELECT r.id, p.id
FROM roles r
CROSS JOIN permissions p
WHERE r.nom_role = 'Assistant RH'
AND p.nom_permission IN (
    'PERSONNEL_VIEW', 'PERSONNEL_ADD', 'PERSONNEL_EDIT',
    'SALAIRE_VIEW'
)
ON DUPLICATE KEY UPDATE role_id=role_id;

-- Attribution des permissions au rôle Comptable
INSERT INTO role_permissions (role_id, permission_id)
SELECT r.id, p.id
FROM roles r
CROSS JOIN permissions p
WHERE r.nom_role = 'Comptable'
AND p.nom_permission IN (
    'PERSONNEL_VIEW',
    'SALAIRE_VIEW', 'SALAIRE_EXPORT',
    'ADMIN_CHARGES'
)
ON DUPLICATE KEY UPDATE role_id=role_id;

-- Attribution des permissions au rôle Consultant
INSERT INTO role_permissions (role_id, permission_id)
SELECT r.id, p.id
FROM roles r
CROSS JOIN permissions p
WHERE r.nom_role = 'Consultant'
AND p.nom_permission IN (
    'PERSONNEL_VIEW',
    'SALAIRE_VIEW'
)
ON DUPLICATE KEY UPDATE role_id=role_id;

-- Création de l'utilisateur administrateur par défaut
-- Mot de passe: Admin@123 (hash BCrypt)
-- IMPORTANT: À CHANGER LORS DE LA PREMIÈRE CONNEXION
INSERT INTO utilisateurs (nom_utilisateur, mot_de_passe_hash, nom_complet, email, actif)
VALUES (
    'admin',
    '$2a$11$7rLSvRVyTfIapPwVu.bEy.KqV5pDfKCrRfKpG/QNVQbW7VqkN6qZG',
    'Administrateur Système',
    'admin@gmp-rh.local',
    1
)
ON DUPLICATE KEY UPDATE nom_complet=VALUES(nom_complet);

-- Attribution du rôle Administrateur à l'utilisateur admin
INSERT INTO utilisateur_roles (utilisateur_id, role_id)
SELECT u.id, r.id
FROM utilisateurs u
CROSS JOIN roles r
WHERE u.nom_utilisateur = 'admin'
AND r.nom_role = 'Administrateur'
ON DUPLICATE KEY UPDATE utilisateur_id=utilisateur_id;

-- ================================================================================
-- LOG DE CRÉATION
-- ================================================================================
INSERT INTO logs_activite (nom_utilisateur, action, module, details, resultat)
VALUES ('SYSTEM', 'CREATION_TABLES_AUTH', 'Système', 'Création des tables d\'authentification et logs', 'SUCCESS');

-- ================================================================================
-- FIN DU SCRIPT
-- ================================================================================
