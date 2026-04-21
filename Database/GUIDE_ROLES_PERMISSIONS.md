# 🔐 Guide du Système de Rôles et Permissions - RH+ Gestion

## 📋 Vue d'ensemble

Ce système de permissions a été conçu pour correspondre exactement aux fonctionnalités réelles du logiciel RH+ Gestion. Il offre une gestion fine des accès avec **6 rôles prédéfinis** et **75 permissions** organisées par modules.

---

## 🎭 Rôles Disponibles

### 1️⃣ Super Administrateur (Niveau 100)
**Profil**: Direction informatique, Administrateur système

**Accès**: Complet et illimité
- ✅ Tous les modules du logiciel
- ✅ Gestion des utilisateurs, rôles et permissions
- ✅ Configuration système et sauvegardes
- ✅ Consultation des logs d'audit
- ✅ Tous les droits de création, modification et suppression

**Nombre de permissions**: 75/75 (100%)

---

### 2️⃣ Administrateur RH (Niveau 80)
**Profil**: Directeur RH, Responsable RH principal

**Accès**: Gestion complète des RH sans accès système critique
- ✅ Gestion complète du personnel
- ✅ Gestion complète de la paie
- ✅ Gestion de la structure organisationnelle
- ✅ Import/Export de données
- ✅ Consultation des utilisateurs et logs
- ❌ Modification des rôles et permissions
- ❌ Configuration système avancée

**Nombre de permissions**: 71/75 (95%)

---

### 3️⃣ Gestionnaire de Paie (Niveau 60)
**Profil**: Responsable paie, Comptable paie

**Accès**: Spécialisé dans la gestion de la paie
- ✅ Consultation des fiches employés
- ✅ Gestion complète des charges, indemnités, sursalaires
- ✅ Saisie et validation des salaires (horaires/journaliers)
- ✅ Impression et export des bulletins de paie
- ❌ Modification des données employés
- ❌ Gestion de la structure organisationnelle
- ❌ Accès système

**Nombre de permissions**: 27/75 (36%)

**Modules accessibles**:
- Employés (lecture seule)
- Charges
- Indemnités
- Sursalaires
- Abonnements
- Salaires (horaires et journaliers)
- Bulletins de paie

---

### 4️⃣ Responsable Personnel (Niveau 50)
**Profil**: Chef du personnel, Responsable recrutement

**Accès**: Gestion du personnel et structure organisationnelle
- ✅ Gestion complète des employés
- ✅ Import/Export d'employés
- ✅ Gestion de la structure (entreprises, directions, services, catégories)
- ✅ Consultation et impression des bulletins
- ❌ Saisie de la paie
- ❌ Gestion des charges et indemnités
- ❌ Accès système

**Nombre de permissions**: 21/75 (28%)

**Modules accessibles**:
- Entreprises
- Directions
- Services
- Catégories
- Employés (complet)
- Bulletins (consultation)

---

### 5️⃣ Assistant RH (Niveau 30)
**Profil**: Assistant administratif, Secrétaire RH

**Accès**: Consultation et saisie de base sans suppressions
- ✅ Consultation de toute la structure
- ✅ Ajout et modification d'employés (pas de suppression)
- ✅ Saisie des éléments de paie (indemnités, sursalaires, abonnements)
- ✅ Saisie des salaires (sans validation)
- ✅ Consultation des bulletins
- ❌ Suppression de données
- ❌ Validation des paies
- ❌ Export de données sensibles

**Nombre de permissions**: 21/75 (28%)

**Modules accessibles**:
- Structure (lecture seule)
- Employés (ajout/modification)
- Éléments de paie (saisie)
- Salaires (saisie sans validation)
- Bulletins (consultation)

---

### 6️⃣ Consultant (Niveau 10)
**Profil**: Auditeur, Consultant externe, Stagiaire

**Accès**: Lecture seule avec exports
- ✅ Consultation de tous les modules
- ✅ Impression et export des bulletins
- ✅ Export de la liste des employés
- ❌ Aucune modification
- ❌ Aucune suppression
- ❌ Aucune saisie

**Nombre de permissions**: 17/75 (23%)

**Modules accessibles**:
- Tous les modules (lecture seule)
- Export et impression autorisés

---

## 📊 Permissions par Module

### 🏢 Entreprises (4 permissions)
| Code | Description | Admin RH | Gestionnaire Paie | Responsable Personnel | Assistant RH | Consultant |
|------|-------------|----------|-------------------|----------------------|--------------|------------|
| `ENTREPRISE_VIEW` | Consulter | ✅ | ❌ | ✅ | ✅ | ✅ |
| `ENTREPRISE_CREATE` | Créer | ✅ | ❌ | ✅ | ❌ | ❌ |
| `ENTREPRISE_EDIT` | Modifier | ✅ | ❌ | ✅ | ❌ | ❌ |
| `ENTREPRISE_DELETE` | Supprimer | ✅ | ❌ | ❌ | ❌ | ❌ |

### 🏛️ Directions (4 permissions)
| Code | Description | Admin RH | Gestionnaire Paie | Responsable Personnel | Assistant RH | Consultant |
|------|-------------|----------|-------------------|----------------------|--------------|------------|
| `DIRECTION_VIEW` | Consulter | ✅ | ❌ | ✅ | ✅ | ✅ |
| `DIRECTION_CREATE` | Créer | ✅ | ❌ | ✅ | ❌ | ❌ |
| `DIRECTION_EDIT` | Modifier | ✅ | ❌ | ✅ | ❌ | ❌ |
| `DIRECTION_DELETE` | Supprimer | ✅ | ❌ | ✅ | ❌ | ❌ |

### 🏪 Services (4 permissions)
Identique aux Directions

### 📋 Catégories (4 permissions)
Identique aux Directions

### 👥 Employés (6 permissions)
| Code | Description | Admin RH | Gestionnaire Paie | Responsable Personnel | Assistant RH | Consultant |
|------|-------------|----------|-------------------|----------------------|--------------|------------|
| `EMPLOYE_VIEW` | Consulter | ✅ | ✅ | ✅ | ✅ | ✅ |
| `EMPLOYE_CREATE` | Créer | ✅ | ❌ | ✅ | ✅ | ❌ |
| `EMPLOYE_EDIT` | Modifier | ✅ | ❌ | ✅ | ✅ | ❌ |
| `EMPLOYE_DELETE` | Supprimer | ✅ | ❌ | ✅ | ❌ | ❌ |
| `EMPLOYE_IMPORT` | Importer | ✅ | ❌ | ✅ | ❌ | ❌ |
| `EMPLOYE_EXPORT` | Exporter | ✅ | ❌ | ✅ | ❌ | ✅ |

### 💰 Charges (4 permissions)
| Code | Description | Admin RH | Gestionnaire Paie | Responsable Personnel | Assistant RH | Consultant |
|------|-------------|----------|-------------------|----------------------|--------------|------------|
| `CHARGE_VIEW` | Consulter | ✅ | ✅ | ❌ | ✅ | ✅ |
| `CHARGE_CREATE` | Créer | ✅ | ✅ | ❌ | ❌ | ❌ |
| `CHARGE_EDIT` | Modifier | ✅ | ✅ | ❌ | ❌ | ❌ |
| `CHARGE_DELETE` | Supprimer | ✅ | ❌ | ❌ | ❌ | ❌ |

### 🎁 Indemnités (4 permissions)
| Code | Description | Admin RH | Gestionnaire Paie | Responsable Personnel | Assistant RH | Consultant |
|------|-------------|----------|-------------------|----------------------|--------------|------------|
| `INDEMNITE_VIEW` | Consulter | ✅ | ✅ | ❌ | ✅ | ✅ |
| `INDEMNITE_CREATE` | Créer | ✅ | ✅ | ❌ | ✅ | ❌ |
| `INDEMNITE_EDIT` | Modifier | ✅ | ✅ | ❌ | ✅ | ❌ |
| `INDEMNITE_DELETE` | Supprimer | ✅ | ✅ | ❌ | ❌ | ❌ |

### 💵 Sursalaires (4 permissions)
Identique aux Indemnités

### 📱 Abonnements (4 permissions)
Identique aux Indemnités

### ⏰ Salaires Horaires (5 permissions)
| Code | Description | Admin RH | Gestionnaire Paie | Responsable Personnel | Assistant RH | Consultant |
|------|-------------|----------|-------------------|----------------------|--------------|------------|
| `SALAIRE_HORAIRE_VIEW` | Consulter | ✅ | ✅ | ❌ | ✅ | ✅ |
| `SALAIRE_HORAIRE_CREATE` | Créer | ✅ | ✅ | ❌ | ✅ | ❌ |
| `SALAIRE_HORAIRE_EDIT` | Modifier | ✅ | ✅ | ❌ | ✅ | ❌ |
| `SALAIRE_HORAIRE_DELETE` | Supprimer | ✅ | ✅ | ❌ | ❌ | ❌ |
| `SALAIRE_HORAIRE_VALIDATE` | Valider | ✅ | ✅ | ❌ | ❌ | ❌ |

### 📅 Salaires Journaliers (5 permissions)
Identique aux Salaires Horaires

### 📄 Bulletins de Paie (4 permissions)
| Code | Description | Admin RH | Gestionnaire Paie | Responsable Personnel | Assistant RH | Consultant |
|------|-------------|----------|-------------------|----------------------|--------------|------------|
| `BULLETIN_VIEW` | Consulter | ✅ | ✅ | ✅ | ✅ | ✅ |
| `BULLETIN_PRINT` | Imprimer | ✅ | ✅ | ✅ | ❌ | ✅ |
| `BULLETIN_PRINT_BATCH` | Impression lot | ✅ | ✅ | ❌ | ❌ | ❌ |
| `BULLETIN_EXPORT` | Exporter | ✅ | ✅ | ❌ | ❌ | ✅ |

### ⚙️ Système - Utilisateurs (3 permissions)
| Code | Description | Admin RH | Autres |
|------|-------------|----------|--------|
| `SYSTEM_USERS` | Gérer utilisateurs | ✅ | ❌ |
| `SYSTEM_USER_RESET_PASSWORD` | Réinitialiser MDP | ✅ | ❌ |
| `SYSTEM_USER_UNLOCK` | Déverrouiller comptes | ✅ | ❌ |

### 🔐 Système - Rôles (2 permissions)
**Super Administrateur uniquement**
- `SYSTEM_ROLES` - Gérer les rôles
- `SYSTEM_PERMISSIONS` - Gérer les permissions

### 📊 Système - Logs (2 permissions)
| Code | Description | Admin RH | Autres |
|------|-------------|----------|--------|
| `SYSTEM_LOGS` | Consulter logs | ✅ | ❌ |
| `SYSTEM_LOGS_EXPORT` | Exporter logs | ✅ | ❌ |

### 🔧 Système - Configuration (2 permissions)
**Super Administrateur uniquement**
- `SYSTEM_CONFIG` - Configuration système
- `SYSTEM_BACKUP` - Gestion des sauvegardes

---

## 🚀 Installation

### Prérequis
- MySQL 5.7+ ou MariaDB 10.2+
- Accès administrateur à la base de données
- Base de données `rh_grh` existante

### Étapes d'installation

1. **Sauvegarde de sécurité** (recommandé)
```sql
mysqldump -u root -p rh_grh > backup_avant_reset_$(date +%Y%m%d_%H%M%S).sql
```

2. **Exécution du script**
```bash
mysql -u root -p rh_grh < reset_roles_permissions.sql
```

Ou via phpMyAdmin :
- Ouvrir phpMyAdmin
- Sélectionner la base `rh_grh`
- Aller dans l'onglet SQL
- Copier-coller le contenu du fichier `reset_roles_permissions.sql`
- Exécuter

3. **Vérification**
Le script affiche automatiquement un résumé :
- Nombre de permissions créées
- Nombre de rôles créés
- Répartition par module
- Attribution des permissions par rôle

---

## 👤 Attribution des Rôles

### Via l'interface RH+ Gestion
1. Se connecter avec le compte `admin`
2. Menu **Administration** → **Gestion des Utilisateurs**
3. Sélectionner un utilisateur
4. Cliquer sur **Modifier**
5. Cocher les rôles à attribuer
6. Valider

### Via SQL (direct)
```sql
-- Attribuer le rôle "Gestionnaire de Paie" à l'utilisateur "marie"
INSERT INTO utilisateur_roles (utilisateur_id, role_id)
SELECT u.id, r.id
FROM utilisateurs u, roles r
WHERE u.nom_utilisateur = 'marie'
  AND r.nom_role = 'Gestionnaire de Paie';
```

---

## ⚠️ Points d'attention

### Sécurité
- ✅ Le compte `admin` conserve tous ses droits
- ✅ Les mots de passe existants ne sont pas modifiés
- ✅ Les sessions actives restent valides
- ⚠️ Tous les utilisateurs perdent leurs anciens rôles (sauf admin)
- ⚠️ Il faudra réattribuer les rôles aux utilisateurs

### Recommandations
1. **Exécuter ce script en dehors des heures de travail**
2. **Prévenir tous les utilisateurs** de la réinitialisation
3. **Préparer la liste des attributions** rôles/utilisateurs à l'avance
4. **Tester avec un compte non-admin** après l'installation
5. **Conserver une sauvegarde** avant l'exécution

---

## 🔄 Évolution et Personnalisation

### Créer une nouvelle permission
```sql
INSERT INTO permissions (code_permission, nom_permission, description, module, date_creation)
VALUES ('MA_PERMISSION', 'Mon Action', 'Description de l\'action', 'Mon Module', NOW());
```

### Créer un nouveau rôle personnalisé
```sql
-- Étape 1: Créer le rôle
INSERT INTO roles (nom_role, description, niveau_acces, date_creation)
VALUES ('Mon Rôle Custom', 'Description du rôle', 45, NOW());

-- Étape 2: Attribuer les permissions
INSERT INTO role_permissions (role_id, permission_id)
SELECT LAST_INSERT_ID(), id FROM permissions
WHERE code_permission IN ('EMPLOYE_VIEW', 'BULLETIN_VIEW', ...);
```

---

## 📞 Support

Pour toute question ou problème :
- 📧 Email : support@rhplus.com
- 📱 Téléphone : +XXX XXX XXX
- 📖 Documentation complète : [docs.rhplus.com](https://docs.rhplus.com)

---

**Version**: 2.0
**Date**: 12 février 2026
**Auteur**: Équipe RH+ Gestion
**Licence**: Propriétaire - © 2026 RH+ Gestion
