# Gestion des Rôles et Permissions - Documentation

## Vue d'ensemble

Un système complet de gestion des rôles et permissions a été implémenté pour contrôler l'accès aux différentes fonctionnalités de l'application RH+ Gestion.

## 📋 Fichiers créés

### 1. Services et Modèles
- `Auth/RoleService.cs` - Service de gestion CRUD des rôles et permissions
- `Auth/Models/Role.cs` - Modèle de données pour les rôles (déjà existant)
- `Auth/Models/Permission.cs` - Modèle de données pour les permissions (déjà existant)

### 2. Interfaces Utilisateur
- `GestionRolesPermissionsForm.cs` - Formulaire principal de gestion
- `GestionRolesPermissionsForm.Designer.cs` - Designer du formulaire principal
- `AjouterModifierRoleForm.cs` - Formulaire d'ajout/modification de rôle
- `AjouterModifierRoleForm.Designer.cs` - Designer du formulaire d'édition

### 3. Base de données
- `SQL/InitRolesPermissions.sql` - Script SQL d'initialisation

### 4. Configuration
- `Formmain.cs` - Mise à jour avec la méthode `ConfigureUserPermissions()`

## 🗄️ Structure de la base de données

### Tables créées

#### `permissions`
```sql
- id (INT, PRIMARY KEY, AUTO_INCREMENT)
- nom_permission (VARCHAR(100), UNIQUE)
- description (TEXT)
- module (VARCHAR(50))
- action (VARCHAR(50))
- date_creation (DATETIME)
```

#### `roles`
```sql
- id (INT, PRIMARY KEY, AUTO_INCREMENT)
- nom_role (VARCHAR(100), UNIQUE)
- description (TEXT)
- niveau_acces (INT)
- date_creation (DATETIME)
```

#### `role_permissions`
```sql
- id (INT, PRIMARY KEY, AUTO_INCREMENT)
- role_id (INT, FOREIGN KEY)
- permission_id (INT, FOREIGN KEY)
- date_attribution (DATETIME)
```

#### `utilisateur_roles`
```sql
- id (INT, PRIMARY KEY, AUTO_INCREMENT)
- utilisateur_id (INT, FOREIGN KEY)
- role_id (INT, FOREIGN KEY)
- date_attribution (DATETIME)
```

## 🔧 Installation

### 1. Exécuter le script SQL

Exécutez le script `SQL/InitRolesPermissions.sql` sur votre base de données MySQL pour :
- Créer les tables nécessaires
- Initialiser les permissions par défaut
- Créer les rôles prédéfinis
- Assigner les permissions aux rôles

```bash
mysql -u votre_utilisateur -p votre_base_de_donnees < SQL/InitRolesPermissions.sql
```

### 2. Ajouter le bouton dans le menu (Optionnel)

Pour ajouter un bouton dans le menu Administration :

1. Ouvrir `Formmain.Designer.cs` dans Visual Studio
2. Ajouter un nouveau `Button` nommé `button_roles`
3. Placer le bouton dans `panel_administration_submenu`
4. Configurer les propriétés du bouton
5. Décommenter le handler dans `Formmain.cs` (ligne 563-570)

OU utiliser directement le formulaire depuis le code :
```csharp
GestionRolesPermissionsForm form = new GestionRolesPermissionsForm();
form.ShowDialog();
```

## 📝 Permissions par défaut

Le système initialise automatiquement ces permissions :

### Module Administration
- `Administration.Entreprise` - Gérer les entreprises
- `Administration.Direction` - Gérer les directions
- `Administration.Service` - Gérer les services
- `Administration.Categorie` - Gérer les catégories

### Module Gestion
- `Gestion.Personnel` - Accéder à la section Personnel
- `Gestion.Salaire` - Accéder à la section Salaire

### Module Personnel
- `Personnel.Employes` - Gérer les employés
- `Personnel.Charges` - Gérer les charges familiales
- `Personnel.Indemnites` - Gérer les indemnités

### Module Salaire
- `Salaire.Sursalaires` - Gérer les sursalaires
- `Salaire.Horaires` - Gérer les horaires
- `Salaire.Journalier` - Gérer le journalier

## 👥 Rôles prédéfinis

### 1. Administrateur (Niveau 100)
- Accès complet à toutes les fonctionnalités
- Toutes les permissions assignées automatiquement
- Peut gérer les utilisateurs et les rôles

### 2. Gestionnaire RH (Niveau 80)
- Accès à la gestion du personnel
- Accès aux configurations administratives (sauf entreprises)
- Ne peut pas gérer les utilisateurs

### 3. Gestionnaire Paie (Niveau 70)
- Accès à la section Salaire
- Peut consulter les employés
- Gestion des horaires et sursalaires

### 4. Responsable Personnel (Niveau 60)
- Gestion du personnel uniquement
- Employés, charges et indemnités
- Pas d'accès aux salaires

### 5. Consultant RH (Niveau 30)
- Accès en lecture seule
- Pour consultation uniquement

## 🎯 Fonctionnalités

### Formulaire Principal (`GestionRolesPermissionsForm`)

**Actions disponibles :**
- ➕ **Ajouter** - Créer un nouveau rôle
- ✏️ **Modifier** - Modifier un rôle existant
- 🗑️ **Supprimer** - Supprimer un rôle (si non utilisé)
- 🔄 **Actualiser** - Recharger la liste des rôles

**Fonctionnalités :**
- Liste des rôles avec tri par niveau d'accès
- Recherche en temps réel
- Affichage des permissions du rôle sélectionné
- Statistiques (nombre total de rôles et permissions)
- Double-clic pour modifier rapidement

### Formulaire d'Édition (`AjouterModifierRoleForm`)

**Informations du rôle :**
- Nom du rôle (obligatoire, minimum 3 caractères)
- Description
- Niveau d'accès (1-100)

**Gestion des permissions :**
- Liste des permissions groupées par module
- Recherche de permissions
- Sélection/désélection rapide (tout/rien)
- Affichage visuel par module

**Validation :**
- Vérification du nom unique
- Validation du niveau d'accès
- Prévention de la suppression de rôles en usage

## 🔒 Sécurité et Contrôle d'Accès

### Configuration automatique de l'interface

La méthode `ConfigureUserPermissions()` dans `Formmain.cs` :
- Vérifie l'authentification de l'utilisateur
- Masque les boutons sans permissions
- Applique le principe du moindre privilège
- Fonctionne automatiquement au chargement

### Règles de visibilité

**Sections principales :**
- **Administration** : Réservée aux administrateurs
- **Personnel** : Requiert `Gestion.Personnel` ou admin
- **Salaire** : Requiert `Gestion.Salaire` ou admin

**Sous-menus :**
Chaque bouton vérifie individuellement les permissions ou le statut admin.

## 📊 Utilisation

### Créer un nouveau rôle

1. Ouvrir le formulaire de gestion des rôles
2. Cliquer sur **Ajouter**
3. Remplir les informations :
   - Nom du rôle
   - Description
   - Niveau d'accès (1-100)
4. Sélectionner les permissions souhaitées
5. Cliquer sur **Enregistrer**

### Modifier un rôle

1. Sélectionner le rôle dans la liste
2. Cliquer sur **Modifier** (ou double-clic)
3. Modifier les informations
4. Ajuster les permissions
5. Enregistrer

### Assigner un rôle à un utilisateur

Utiliser le formulaire de gestion des utilisateurs existant pour assigner les rôles.

### Supprimer un rôle

1. Sélectionner le rôle
2. Cliquer sur **Supprimer**
3. Confirmer l'action

**Note :** Un rôle ne peut pas être supprimé s'il est assigné à des utilisateurs.

## 🎨 Interface

**Design moderne avec :**
- Palette de couleurs RH+ (Orange, Bleu, Gris, Blanc)
- Police Montserrat pour les titres
- Police Segoe UI pour le contenu
- DataGridView avec tri et sélection
- Groupement visuel des permissions par module
- Boutons colorés selon l'action (Vert=Créer, Bleu=Modifier, Rouge=Supprimer)

## ⚠️ Notes importantes

1. **Sauvegarde recommandée** : Avant d'exécuter le script SQL, sauvegarder votre base de données
2. **Rôle Administrateur** : Ne peut pas être supprimé ou modifié via l'interface (protection)
3. **Niveau d'accès** : Plus le niveau est élevé, plus le rôle a de privilèges
4. **Permissions granulaires** : Permet un contrôle fin de l'accès aux fonctionnalités
5. **Audit automatique** : Toutes les actions sont enregistrées via `AuditLogger`

## 🔄 Mise à jour et Maintenance

### Ajouter de nouvelles permissions

1. Exécuter un `INSERT` dans la table `permissions`
2. Ou utiliser `RoleService.InitializeDefaultPermissions()` pour ajouter des permissions par code

```sql
INSERT INTO permissions (nom_permission, description, module, action)
VALUES ('NouveauModule.NouvelleAction', 'Description', 'NouveauModule', 'NouvelleAction');
```

### Logs et Audit

Toutes les actions CRUD sont enregistrées automatiquement :
- Création de rôle
- Modification de rôle
- Suppression de rôle

Consultable via `AuditLogger` et le formulaire de visualisation des logs.

## 🎓 Exemple d'utilisation programmatique

```csharp
// Créer une instance du service
RoleService roleService = new RoleService();

// Récupérer tous les rôles
List<Role> roles = roleService.GetAllRoles();

// Récupérer un rôle spécifique
Role adminRole = roleService.GetRoleById(1);

// Créer un nouveau rôle
Role newRole = new Role
{
    NomRole = "Stagiaire",
    Description = "Accès limité pour les stagiaires",
    NiveauAcces = 10
};

List<int> permissionIds = new List<int> { 7, 8 }; // Personnel.Employes, Personnel.Charges
roleService.CreateRole(newRole, permissionIds);

// Mettre à jour un rôle
newRole.Description = "Description mise à jour";
roleService.UpdateRole(newRole, permissionIds);

// Supprimer un rôle
roleService.DeleteRole(newRole.Id);
```

## 📞 Support

Pour toute question ou problème :
1. Vérifier les logs d'audit
2. Consulter cette documentation
3. Contacter l'équipe de développement

---

**Version** : 1.0
**Dernière mise à jour** : 11/02/2026
**Auteur** : Système RH+ Gestion
**License** : Propriétaire
