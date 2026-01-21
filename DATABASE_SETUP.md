# Configuration de la Base de Données - RH Plus Gestion

## Vue d'ensemble

Ce document décrit comment configurer la base de données MySQL pour RH Plus Gestion v1.0.

## Prérequis

- MySQL Server 5.7 ou supérieur (8.0 recommandé)
- Droits administrateur MySQL
- Client MySQL (MySQL Workbench, phpMyAdmin, ou ligne de commande)

## Installation de MySQL Server

### Windows

1. **Télécharger MySQL**
   - URL : https://dev.mysql.com/downloads/mysql/
   - Choisir "MySQL Installer for Windows"

2. **Installer MySQL**
   - Exécuter le programme d'installation
   - Choisir "Developer Default" ou "Server only"
   - Configurer le mot de passe root
   - Démarrer le service MySQL

3. **Vérifier l'installation**
   ```batch
   mysql --version
   ```

### Configuration recommandée

Éditer `my.ini` ou `my.cnf` :
```ini
[mysqld]
# Configuration recommandée pour RH Plus Gestion
max_connections=100
character-set-server=utf8mb4
collation-server=utf8mb4_unicode_ci
default-storage-engine=INNODB
max_allowed_packet=64M
innodb_buffer_pool_size=256M
```

## Création de la base de données

### Méthode 1 : Via MySQL Workbench (Recommandé)

1. **Ouvrir MySQL Workbench**
2. **Se connecter au serveur MySQL**
   - Host: localhost
   - Port: 3306
   - Username: root
   - Password: [votre mot de passe]

3. **Créer la base de données**
   ```sql
   CREATE DATABASE rhplus_gestion
   CHARACTER SET utf8mb4
   COLLATE utf8mb4_unicode_ci;
   ```

4. **Créer l'utilisateur dédié**
   ```sql
   CREATE USER 'rhplus_user'@'localhost' IDENTIFIED BY 'VotreMotDePasseSecurise123!';
   GRANT ALL PRIVILEGES ON rhplus_gestion.* TO 'rhplus_user'@'localhost';
   FLUSH PRIVILEGES;
   ```

5. **Exécuter les scripts SQL**
   - Ouvrir et exécuter `Database/schema.sql`
   - (Optionnel) Exécuter `Database/initial_data.sql`
   - Exécuter `verify_column.sql` pour vérification

### Méthode 2 : Via ligne de commande

```bash
# Se connecter à MySQL
mysql -u root -p

# Créer la base
CREATE DATABASE rhplus_gestion CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

# Créer l'utilisateur
CREATE USER 'rhplus_user'@'localhost' IDENTIFIED BY 'VotreMotDePasseSecurise123!';
GRANT ALL PRIVILEGES ON rhplus_gestion.* TO 'rhplus_user'@'localhost';
FLUSH PRIVILEGES;

# Quitter MySQL
EXIT;

# Importer les scripts
mysql -u rhplus_user -p rhplus_gestion < Database/schema.sql
mysql -u rhplus_user -p rhplus_gestion < Database/initial_data.sql
mysql -u rhplus_user -p rhplus_gestion < verify_column.sql
```

## Structure de la base de données

### Tables principales

```
rhplus_gestion
├── entreprises          # Informations des entreprises
├── directions           # Directions des entreprises
├── services            # Services des directions
├── categories          # Catégories d'employés
├── employes            # Données des employés
├── salaire_horaire     # Salaires horaires
├── salaire_journalier  # Salaires journaliers
├── indemnites          # Indemnités des employés
├── charges             # Charges patronales
└── bulletins           # Bulletins de paie générés
```

### Script de création minimal

Si vous n'avez pas les scripts SQL complets, voici une structure minimale :

```sql
-- Base de données
CREATE DATABASE IF NOT EXISTS rhplus_gestion
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE rhplus_gestion;

-- Table entreprises
CREATE TABLE IF NOT EXISTS entreprises (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nom VARCHAR(255) NOT NULL,
    adresse TEXT,
    telephone VARCHAR(50),
    email VARCHAR(100),
    nif VARCHAR(50),
    rc VARCHAR(50),
    date_creation TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    INDEX idx_nom (nom)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table directions
CREATE TABLE IF NOT EXISTS directions (
    id INT AUTO_INCREMENT PRIMARY KEY,
    entreprise_id INT NOT NULL,
    nom VARCHAR(255) NOT NULL,
    description TEXT,
    FOREIGN KEY (entreprise_id) REFERENCES entreprises(id) ON DELETE CASCADE,
    INDEX idx_entreprise (entreprise_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table services
CREATE TABLE IF NOT EXISTS services (
    id INT AUTO_INCREMENT PRIMARY KEY,
    direction_id INT NOT NULL,
    nom VARCHAR(255) NOT NULL,
    description TEXT,
    FOREIGN KEY (direction_id) REFERENCES directions(id) ON DELETE CASCADE,
    INDEX idx_direction (direction_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table categories
CREATE TABLE IF NOT EXISTS categories (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nom VARCHAR(100) NOT NULL,
    description TEXT,
    salaire_base DECIMAL(10,2) DEFAULT 0,
    INDEX idx_nom (nom)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table employés
CREATE TABLE IF NOT EXISTS employes (
    id INT AUTO_INCREMENT PRIMARY KEY,
    matricule VARCHAR(50) UNIQUE NOT NULL,
    nom VARCHAR(100) NOT NULL,
    prenom VARCHAR(100) NOT NULL,
    sexe ENUM('M', 'F') NOT NULL,
    date_naissance DATE,
    lieu_naissance VARCHAR(100),
    adresse TEXT,
    telephone VARCHAR(50),
    email VARCHAR(100),
    entreprise_id INT NOT NULL,
    direction_id INT,
    service_id INT,
    categorie_id INT,
    date_embauche DATE NOT NULL,
    situation_matrimoniale VARCHAR(50),
    nombre_enfants INT DEFAULT 0,
    numero_cnss VARCHAR(50),
    compte_bancaire VARCHAR(50),
    statut ENUM('Actif', 'Suspendu', 'Démissionné', 'Licencié') DEFAULT 'Actif',
    date_creation TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (entreprise_id) REFERENCES entreprises(id) ON DELETE CASCADE,
    FOREIGN KEY (direction_id) REFERENCES directions(id) ON DELETE SET NULL,
    FOREIGN KEY (service_id) REFERENCES services(id) ON DELETE SET NULL,
    FOREIGN KEY (categorie_id) REFERENCES categories(id) ON DELETE SET NULL,
    INDEX idx_matricule (matricule),
    INDEX idx_nom (nom, prenom),
    INDEX idx_entreprise (entreprise_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table salaire_horaire
CREATE TABLE IF NOT EXISTS salaire_horaire (
    id INT AUTO_INCREMENT PRIMARY KEY,
    employe_id INT NOT NULL,
    periode VARCHAR(20) NOT NULL,
    taux_horaire DECIMAL(10,2) NOT NULL,
    heures_travaillees DECIMAL(10,2) NOT NULL,
    heures_supplementaires DECIMAL(10,2) DEFAULT 0,
    salaire_brut DECIMAL(10,2) NOT NULL,
    cnss DECIMAL(10,2) DEFAULT 0,
    tpa DECIMAL(10,2) DEFAULT 0,
    iuts DECIMAL(10,2) DEFAULT 0,
    salaire_net DECIMAL(10,2) NOT NULL,
    date_creation TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (employe_id) REFERENCES employes(id) ON DELETE CASCADE,
    INDEX idx_employe (employe_id),
    INDEX idx_periode (periode)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table salaire_journalier
CREATE TABLE IF NOT EXISTS salaire_journalier (
    id INT AUTO_INCREMENT PRIMARY KEY,
    employe_id INT NOT NULL,
    periode VARCHAR(20) NOT NULL,
    taux_journalier DECIMAL(10,2) NOT NULL,
    jours_travailles DECIMAL(10,2) NOT NULL,
    salaire_brut DECIMAL(10,2) NOT NULL,
    cnss DECIMAL(10,2) DEFAULT 0,
    tpa DECIMAL(10,2) DEFAULT 0,
    iuts DECIMAL(10,2) DEFAULT 0,
    salaire_net DECIMAL(10,2) NOT NULL,
    date_creation TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (employe_id) REFERENCES employes(id) ON DELETE CASCADE,
    INDEX idx_employe (employe_id),
    INDEX idx_periode (periode)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table indemnites
CREATE TABLE IF NOT EXISTS indemnites (
    id INT AUTO_INCREMENT PRIMARY KEY,
    employe_id INT NOT NULL,
    type_indemnite VARCHAR(100) NOT NULL,
    montant DECIMAL(10,2) NOT NULL,
    periode VARCHAR(20),
    imposable BOOLEAN DEFAULT TRUE,
    date_creation TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (employe_id) REFERENCES employes(id) ON DELETE CASCADE,
    INDEX idx_employe (employe_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table charges
CREATE TABLE IF NOT EXISTS charges (
    id INT AUTO_INCREMENT PRIMARY KEY,
    employe_id INT NOT NULL,
    type_charge VARCHAR(100) NOT NULL,
    montant DECIMAL(10,2) NOT NULL,
    periode VARCHAR(20),
    date_creation TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (employe_id) REFERENCES employes(id) ON DELETE CASCADE,
    INDEX idx_employe (employe_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table bulletins
CREATE TABLE IF NOT EXISTS bulletins (
    id INT AUTO_INCREMENT PRIMARY KEY,
    employe_id INT NOT NULL,
    periode VARCHAR(20) NOT NULL,
    salaire_brut DECIMAL(10,2) NOT NULL,
    total_indemnites DECIMAL(10,2) DEFAULT 0,
    total_charges DECIMAL(10,2) DEFAULT 0,
    cnss DECIMAL(10,2) DEFAULT 0,
    tpa DECIMAL(10,2) DEFAULT 0,
    iuts DECIMAL(10,2) DEFAULT 0,
    salaire_net DECIMAL(10,2) NOT NULL,
    fichier_pdf VARCHAR(255),
    date_generation TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (employe_id) REFERENCES employes(id) ON DELETE CASCADE,
    INDEX idx_employe (employe_id),
    INDEX idx_periode (periode)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
```

## Configuration de l'application

### Fichier App.config

Après l'installation de l'application, configurer la connexion dans `App.config` :

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <connectionStrings>
    <add name="MySqlConnection"
         connectionString="Server=localhost;Port=3306;Database=rhplus_gestion;Uid=rhplus_user;Pwd=VotreMotDePasse;CharSet=utf8mb4;SslMode=none;AllowUserVariables=True;"
         providerName="MySql.Data.MySqlClient" />
  </connectionStrings>
</configuration>
```

### Première connexion

Au premier lancement de l'application :
1. L'application détectera automatiquement la configuration
2. Si la connexion échoue, une boîte de dialogue apparaîtra
3. Entrer les informations de connexion :
   - Serveur : `localhost`
   - Port : `3306`
   - Base : `rhplus_gestion`
   - Utilisateur : `rhplus_user`
   - Mot de passe : [votre mot de passe]

## Sécurité

### Bonnes pratiques

1. **Mot de passe fort**
   - Minimum 12 caractères
   - Mélange de majuscules, minuscules, chiffres, symboles
   - Exemple : `RhP!us2025@Secure#`

2. **Utilisateur dédié**
   ```sql
   -- Ne pas utiliser root pour l'application
   CREATE USER 'rhplus_user'@'localhost' IDENTIFIED BY 'MotDePasseSecurise';
   GRANT SELECT, INSERT, UPDATE, DELETE ON rhplus_gestion.* TO 'rhplus_user'@'localhost';
   ```

3. **Accès réseau**
   ```sql
   -- Pour accès distant (à utiliser avec précaution)
   CREATE USER 'rhplus_user'@'%' IDENTIFIED BY 'MotDePasseSecurise';
   GRANT ALL PRIVILEGES ON rhplus_gestion.* TO 'rhplus_user'@'%';
   FLUSH PRIVILEGES;
   ```

4. **Pare-feu**
   - Autoriser uniquement le port 3306 pour les IPs approuvées
   - Utiliser SSL/TLS pour les connexions distantes

### Sauvegarde

#### Sauvegarde manuelle

```bash
# Sauvegarde complète
mysqldump -u rhplus_user -p rhplus_gestion > backup_rhplus_$(date +%Y%m%d_%H%M%S).sql

# Sauvegarde structure uniquement
mysqldump -u rhplus_user -p --no-data rhplus_gestion > schema_backup.sql

# Sauvegarde données uniquement
mysqldump -u rhplus_user -p --no-create-info rhplus_gestion > data_backup.sql
```

#### Sauvegarde automatique (Windows)

Créer un script `backup_db.bat` :

```batch
@echo off
set MYSQL_USER=rhplus_user
set MYSQL_PASS=VotreMotDePasse
set DATABASE=rhplus_gestion
set BACKUP_DIR=C:\Backups\RHPlus
set DATE=%date:~-4%%date:~3,2%%date:~0,2%_%time:~0,2%%time:~3,2%%time:~6,2%
set DATE=%DATE: =0%

if not exist "%BACKUP_DIR%" mkdir "%BACKUP_DIR%"

mysqldump -u %MYSQL_USER% -p%MYSQL_PASS% %DATABASE% > "%BACKUP_DIR%\backup_%DATE%.sql"

echo Sauvegarde terminee : %BACKUP_DIR%\backup_%DATE%.sql
```

Ajouter au Planificateur de tâches Windows pour exécution automatique.

#### Restauration

```bash
# Restaurer depuis une sauvegarde
mysql -u rhplus_user -p rhplus_gestion < backup_file.sql
```

## Maintenance

### Optimisation

```sql
-- Analyser les tables
ANALYZE TABLE employes, salaire_horaire, salaire_journalier;

-- Optimiser les tables
OPTIMIZE TABLE employes, salaire_horaire, salaire_journalier;

-- Vérifier l'intégrité
CHECK TABLE employes, salaire_horaire, salaire_journalier;
```

### Surveillance

```sql
-- Vérifier l'espace utilisé
SELECT
    table_name AS 'Table',
    ROUND(((data_length + index_length) / 1024 / 1024), 2) AS 'Taille (MB)'
FROM information_schema.TABLES
WHERE table_schema = 'rhplus_gestion'
ORDER BY (data_length + index_length) DESC;

-- Vérifier les connexions actives
SHOW PROCESSLIST;

-- Vérifier les logs d'erreurs
SHOW VARIABLES LIKE 'log_error';
```

## Dépannage

### Erreur : Access denied

**Problème** : Droits insuffisants
**Solution** :
```sql
GRANT ALL PRIVILEGES ON rhplus_gestion.* TO 'rhplus_user'@'localhost';
FLUSH PRIVILEGES;
```

### Erreur : Can't connect to MySQL server

**Problème** : Service MySQL arrêté
**Solution** :
```batch
# Windows
net start MySQL80

# Vérifier le service
sc query MySQL80
```

### Erreur : Table doesn't exist

**Problème** : Base non initialisée
**Solution** :
```bash
mysql -u rhplus_user -p rhplus_gestion < Database/schema.sql
```

### Performance lente

**Problème** : Index manquants ou cache insuffisant
**Solution** :
```sql
-- Vérifier les requêtes lentes
SHOW VARIABLES LIKE 'slow_query_log';

-- Augmenter le buffer pool
SET GLOBAL innodb_buffer_pool_size = 268435456; -- 256MB
```

## Migration de données

### Depuis Excel

L'application inclut une fonction d'import Excel :
1. Préparer votre fichier Excel avec les colonnes requises
2. Menu Employés → Importer depuis Excel
3. Suivre l'assistant d'importation

### Depuis une autre base

```sql
-- Export depuis l'ancienne base
mysqldump -u olduser -p olddb > export.sql

-- Import dans la nouvelle base
mysql -u rhplus_user -p rhplus_gestion < export.sql
```

## Support

Pour toute question sur la configuration de la base de données :
- Documentation MySQL : https://dev.mysql.com/doc/
- Support : support@gmp-rh.com
- GitHub Issues : https://github.com/AaronThierry/Rhplus_Gestion/issues

---

**Dernière mise à jour** : 21 janvier 2025
