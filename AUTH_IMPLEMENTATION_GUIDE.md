# GUIDE D'IMPLÉMENTATION - SYSTÈME D'AUTHENTIFICATION & LOGS
## Version 1.2.0 - 11 Février 2026

---

## 📋 RÉSUMÉ DU SYSTÈME

Ce système fournit une authentification complète avec gestion des rôles, permissions et journalisation (audit trail) pour l'application Gestion Moderne RH.

### Fonctionnalités implémentées :

✅ **Authentification sécurisée**
- Hachage des mots de passe avec BCrypt
- Gestion des sessions utilisateur
- Protection contre les attaques par force brute (verrouillage après 5 tentatives)
- Connexion avec nom d'utilisateur et mot de passe

✅ **Système de rôles et permissions**
- 5 rôles prédéfinis : Administrateur, Gestionnaire RH, Assistant RH, Comptable, Consultant
- 19 permissions granulaires couvrant tous les modules
- Attribution flexible des permissions aux rôles

✅ **Journalisation complète (Audit Trail)**
- Enregistrement de toutes les actions utilisateur
- Traçabilité complète (qui, quoi, quand, où)
- Logs de connexion/déconnexion
- Logs d'accès refusés
- Historique des modifications avec ancien/nouvel état

✅ **Interfaces utilisateur**
- Formulaire de connexion moderne et sécurisé
- Gestion des utilisateurs (à finaliser)
- Visualisation des logs (à créer)

---

## 🔧 ÉTAPES D'INSTALLATION

### 1. Installation des packages NuGet requis

Ouvrez la Console du Gestionnaire de Packages (Tools > NuGet Package Manager > Package Manager Console) et exécutez :

```powershell
# Installation de BCrypt.Net-Next pour le hachage des mots de passe
Install-Package BCrypt.Net-Next -Version 4.0.3

# Installation de Newtonsoft.Json pour la sérialisation JSON (si pas déjà installé)
Install-Package Newtonsoft.Json -Version 13.0.3
```

### 2. Création des tables en base de données

Exécutez le script SQL situé dans `Database/create_auth_tables.sql` sur votre base de données MySQL.

Ce script va créer :
- 7 tables (utilisateurs, roles, permissions, etc.)
- 5 rôles prédéfinis
- 19 permissions
- 1 utilisateur administrateur par défaut

**Identifiants par défaut :**
- Nom d'utilisateur : `admin`
- Mot de passe : `Admin@123`

⚠️ **IMPORTANT** : Changez ce mot de passe dès la première connexion !

### 3. Intégration dans Program.cs

Modifiez le fichier `Program.cs` pour afficher le formulaire de connexion avant Formmain :

```csharp
static void Main()
{
    // ... code existant ...

    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);

    // Afficher le formulaire de connexion
    using (LoginForm loginForm = new LoginForm())
    {
        if (loginForm.ShowDialog() == DialogResult.OK)
        {
            // Connexion réussie - lancer l'application principale
            Application.Run(new Formmain());
        }
        else
        {
            // Connexion annulée - fermer l'application
            Application.Exit();
        }
    }
}
```

### 4. Modification de Formmain.cs

Ajoutez la gestion de la déconnexion et l'affichage de l'utilisateur connecté :

```csharp
// Dans le constructeur de Formmain
public Formmain()
{
    InitializeComponent();

    // Afficher l'utilisateur connecté
    var user = SessionManager.Instance.CurrentUser;
    if (user != null)
    {
        // Ajouter un label pour afficher le nom de l'utilisateur
        labelUtilisateurConnecte.Text = $"Connecté : {user.NomComplet}";
    }

    // Appliquer les permissions sur les boutons de menu
    ApplyMenuPermissions();
}

// Méthode pour appliquer les permissions
private void ApplyMenuPermissions()
{
    // Exemple : désactiver les boutons selon les permissions
    button_personnel.Enabled = PermissionManager.HasPermission(PermissionManager.PERSONNEL_VIEW);
    button_salaire.Enabled = PermissionManager.HasPermission(PermissionManager.SALAIRE_VIEW);
    button_administration.Enabled = PermissionManager.HasPermission(PermissionManager.ADMIN_ENTREPRISE);

    // Les utilisateurs sans permissions voient les boutons grisés
}

// Bouton de déconnexion
private void buttonDeconnexion_Click(object sender, EventArgs e)
{
    var result = CustomMessageBox.Show("Voulez-vous vraiment vous déconnecter?",
        "Déconnexion", CustomMessageBox.MessageType.Question, MessageBoxButtons.YesNo);

    if (result == DialogResult.Yes)
    {
        // Déconnexion
        new AuthenticationService().Logout();

        // Fermer l'application ou revenir au login
        this.Close();
        Application.Restart();
    }
}
```

### 5. Sécurisation des formulaires existants

Pour chaque formulaire métier, ajoutez les vérifications de permissions :

```csharp
// Exemple dans GestionEmployeForm
public GestionEmployeForm()
{
    InitializeComponent();

    // Vérifier la permission de consultation
    if (!PermissionManager.CheckPermission(PermissionManager.PERSONNEL_VIEW))
    {
        this.Close();
        return;
    }

    // Appliquer les permissions sur les boutons
    buttonAjouter.Enabled = PermissionManager.HasPermission(PermissionManager.PERSONNEL_ADD);
    buttonModifier.Enabled = PermissionManager.HasPermission(PermissionManager.PERSONNEL_EDIT);
    buttonSupprimer.Enabled = PermissionManager.HasPermission(PermissionManager.PERSONNEL_DELETE);

    // Charger les données
    ChargerDonnees();
}

// Dans les méthodes d'action, ajouter des logs
private void buttonAjouter_Click(object sender, EventArgs e)
{
    if (!PermissionManager.CheckPermission(PermissionManager.PERSONNEL_ADD))
        return;

    // Ouvrir le formulaire d'ajout
    AjouterEmployeForm form = new AjouterEmployeForm();
    if (form.ShowDialog() == DialogResult.OK)
    {
        // Log de l'action
        AuditLogger.LogAdd("Personnel", "Employé", $"Ajout employé: {form.NomEmploye}");

        // Rafraîchir
        ChargerDonnees();
    }
}
```

### 6. Ajout de logs dans les opérations critiques

Exemples d'utilisation du système de logs :

```csharp
// Ajout d'un employé
AuditLogger.LogAdd("Personnel", "Employé", $"Ajout: {nomEmploye} - Matricule: {matricule}");

// Modification
AuditLogger.LogEdit("Personnel", "Employé", $"Modification: {nomEmploye} - ID: {idEmploye}");

// Suppression
AuditLogger.LogDelete("Personnel", "Employé", $"Suppression: {nomEmploye} - ID: {idEmploye}");

// Export de bulletin
AuditLogger.LogExport("Salaire", "PDF", $"Export bulletin mois {mois}/{annee} - {nbBulletins} bulletins");

// Import d'employés
AuditLogger.LogImport("Personnel", "Excel", $"Import de {nbEmployes} employés depuis {nomFichier}");

// Action personnalisée avec état
var ancienEtat = new { Salaire = ancienSalaire, Statut = ancienStatut };
var nouvelEtat = new { Salaire = nouveauSalaire, Statut = nouveauStatut };
AuditLogger.LogModification("SALARY_UPDATE", "Personnel",
    $"Mise à jour salaire employé {matricule}", ancienEtat, nouvelEtat);
```

---

## 📂 STRUCTURE DES FICHIERS CRÉÉS

```
RH_GRH/
├── Auth/
│   ├── Models/
│   │   ├── User.cs                 # Modèle utilisateur
│   │   ├── Role.cs                 # Modèle rôle
│   │   ├── Permission.cs           # Modèle permission
│   │   ├── Session.cs              # Modèle session
│   │   └── AuditLog.cs             # Modèle log d'audit
│   ├── PasswordHasher.cs           # Service de hachage BCrypt
│   ├── SessionManager.cs           # Gestionnaire de sessions (Singleton)
│   ├── AuthenticationService.cs    # Service d'authentification
│   ├── AuditLogger.cs              # Service de journalisation
│   └── PermissionManager.cs        # Gestionnaire de permissions
├── LoginForm.cs                    # Formulaire de connexion
├── LoginForm.Designer.cs           # Designer du formulaire
└── (À créer)
    ├── GestionUtilisateursForm.cs  # Gestion des utilisateurs
    └── VisualisationLogsForm.cs    # Consultation des logs

Database/
└── create_auth_tables.sql          # Script de création des tables
```

---

## 🔐 RÔLES ET PERMISSIONS PAR DÉFAUT

### Administrateur (Niveau 100)
- **Toutes les permissions** : Accès complet au système

### Gestionnaire RH (Niveau 80)
- Personnel : VIEW, ADD, EDIT, IMPORT
- Salaire : VIEW, PROCESS, EDIT, EXPORT
- Administration : CATEGORIES, SERVICES, DIRECTIONS, CHARGES, INDEMNITES

### Assistant RH (Niveau 50)
- Personnel : VIEW, ADD, EDIT
- Salaire : VIEW

### Comptable (Niveau 60)
- Personnel : VIEW
- Salaire : VIEW, EXPORT
- Administration : CHARGES

### Consultant (Niveau 20)
- Personnel : VIEW
- Salaire : VIEW

---

## 📊 UTILISATION DES LOGS

Les logs sont automatiquement enregistrés avec les informations suivantes :
- **ID et nom de l'utilisateur**
- **Action effectuée** (LOGIN, ADD_EMPLOYE, EDIT_SALAIRE, etc.)
- **Module concerné** (Personnel, Salaire, Administration, Système)
- **Détails** de l'action
- **Ancien et nouvel état** (pour les modifications)
- **Horodatage** précis
- **Adresse IP** de l'utilisateur
- **Résultat** (SUCCESS, FAILURE, WARNING)

### Requêtes SQL utiles pour consulter les logs :

```sql
-- Logs des dernières 24 heures
SELECT * FROM logs_activite
WHERE date_action >= DATE_SUB(NOW(), INTERVAL 24 HOUR)
ORDER BY date_action DESC;

-- Logs d'un utilisateur spécifique
SELECT * FROM logs_activite
WHERE nom_utilisateur = 'admin'
ORDER BY date_action DESC;

-- Logs d'échec de connexion
SELECT * FROM logs_activite
WHERE action = 'LOGIN_FAILED'
AND date_action >= DATE_SUB(NOW(), INTERVAL 7 DAY)
ORDER BY date_action DESC;

-- Statistiques par action
SELECT action, COUNT(*) as total, resultat
FROM logs_activite
GROUP BY action, resultat
ORDER BY total DESC;

-- Activité par utilisateur
SELECT nom_utilisateur, COUNT(*) as nb_actions,
       MAX(date_action) as derniere_action
FROM logs_activite
GROUP BY nom_utilisateur
ORDER BY nb_actions DESC;
```

---

## 🎯 PROCHAINES ÉTAPES

1. ✅ Installation des packages NuGet (BCrypt, Newtonsoft.Json)
2. ✅ Exécution du script SQL de création des tables
3. 🔄 Modification de Program.cs pour intégrer le login
4. 🔄 Modification de Formmain.cs pour afficher l'utilisateur connecté
5. 🔄 Ajout du bouton de déconnexion dans Formmain
6. 📝 Création du formulaire de gestion des utilisateurs
7. 📝 Création du formulaire de visualisation des logs
8. 🔄 Sécurisation progressive des formulaires existants
9. ✅ Tests complets du système
10. 📚 Formation des utilisateurs

---

## ⚠️ NOTES IMPORTANTES

1. **Sécurité** : Les mots de passe sont hachés avec BCrypt (facteur 11). Ne jamais stocker les mots de passe en clair.

2. **Verrouillage de compte** : Après 5 tentatives échouées, le compte est automatiquement verrouillé. Seul un administrateur peut le déverrouiller.

3. **Sessions** : Une seule session active par utilisateur. La nouvelle connexion invalide l'ancienne.

4. **Logs** : Les logs ne sont jamais supprimés automatiquement. Prévoir une politique d'archivage.

5. **Permissions** : Les permissions sont vérifiées au niveau du code. Ne pas se fier uniquement à l'UI.

6. **Performance** : Le chargement des rôles et permissions se fait à la connexion et est mis en cache.

---

## 🆘 DÉPANNAGE

### Le formulaire de connexion ne s'affiche pas
- Vérifier que les packages NuGet sont installés
- Vérifier que les références Guna.UI2 sont présentes
- Vérifier que le fichier LoginForm.resx existe

### Erreur "Table doesn't exist"
- Exécuter le script SQL create_auth_tables.sql
- Vérifier la connexion à la base de données dans Dbconnect.cs

### Connexion impossible avec admin/Admin@123
- Vérifier que le script SQL a été exécuté complètement
- Vérifier qu'il n'y a pas eu d'erreur lors de l'insertion de l'utilisateur admin
- Le hash BCrypt est : $2a$11$7rLSvRVyTfIapPwVu.bEy.KqV5pDfKCrRfKpG/QNVQbW7VqkN6qZG

### Les permissions ne fonctionnent pas
- Vérifier que l'utilisateur est bien authentifié
- Vérifier que les rôles et permissions sont bien chargés
- Utiliser le débogueur pour vérifier SessionManager.Instance.CurrentUser.Roles

---

## 📞 SUPPORT

Pour toute question ou problème, consultez :
- Ce guide d'implémentation
- Le code source commenté
- Les logs système (Debug.WriteLine)
- La base de données (table logs_activite)

---

**Version** : 1.2.0
**Date** : 11 Février 2026
**Auteur** : Système d'authentification Gestion Moderne RH

