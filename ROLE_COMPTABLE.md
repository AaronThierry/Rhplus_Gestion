# 💼 Rôle Comptable - Documentation Complète

**Date** : 13 février 2026
**Version** : 1.1.4
**Statut** : ✅ Prêt à déployer

---

## 🎯 Objectif

Créer un rôle "Comptable" avec accès **UNIQUEMENT** à la section Salaire pour :
- Gérer les salaires horaires et journaliers
- Gérer les sursalaires et primes
- Générer et imprimer les bulletins de paie
- Exporter les données de paie
- **SANS** accès aux autres sections (Personnel, Administration, etc.)

---

## 📋 Description du Rôle

**Nom** : Comptable

**Description** : Accès complet à la gestion des salaires et bulletins de paie. Aucun accès aux autres sections.

**Cas d'usage** :
- Comptable paie
- Assistant comptable
- Responsable paie
- Tout utilisateur chargé uniquement du calcul et de la gestion de la paie

---

## ✅ PERMISSIONS ACCORDÉES

### 1. SURSALAIRES (4 permissions)
| Permission | Description |
|------------|-------------|
| `SURSALAIRE_VIEW` | Consulter les sursalaires et primes |
| `SURSALAIRE_CREATE` | Créer de nouveaux sursalaires |
| `SURSALAIRE_EDIT` | Modifier les sursalaires existants |
| `SURSALAIRE_DELETE` | Supprimer des sursalaires |

### 2. SALAIRES HORAIRES (5 permissions)
| Permission | Description |
|------------|-------------|
| `SALAIRE_HORAIRE_VIEW` | Consulter les salaires horaires |
| `SALAIRE_HORAIRE_CREATE` | Créer/Saisir des salaires horaires |
| `SALAIRE_HORAIRE_EDIT` | Modifier les salaires horaires |
| `SALAIRE_HORAIRE_DELETE` | Supprimer des salaires horaires |
| `SALAIRE_HORAIRE_VALIDATE` | Valider et clôturer les salaires horaires |

### 3. SALAIRES JOURNALIERS (5 permissions)
| Permission | Description |
|------------|-------------|
| `SALAIRE_JOURNALIER_VIEW` | Consulter les salaires journaliers |
| `SALAIRE_JOURNALIER_CREATE` | Créer/Saisir des salaires journaliers |
| `SALAIRE_JOURNALIER_EDIT` | Modifier les salaires journaliers |
| `SALAIRE_JOURNALIER_DELETE` | Supprimer des salaires journaliers |
| `SALAIRE_JOURNALIER_VALIDATE` | Valider et clôturer les salaires journaliers |

### 4. BULLETINS DE PAIE (4 permissions)
| Permission | Description |
|------------|-------------|
| `BULLETIN_VIEW` | Consulter les bulletins de paie |
| `BULLETIN_PRINT` | Imprimer les bulletins individuels |
| `BULLETIN_PRINT_BATCH` | Imprimer plusieurs bulletins simultanément |
| `BULLETIN_EXPORT` | Exporter les bulletins (PDF/Excel) |

### 5. PERMISSIONS GÉNÉRALES SALAIRE (4 permissions)
| Permission | Description |
|------------|-------------|
| `SALAIRE_VIEW` | Consulter les données de salaire |
| `SALAIRE_PROCESS` | Traiter/Calculer les salaires |
| `SALAIRE_EDIT` | Modifier les données de salaire |
| `SALAIRE_EXPORT` | Exporter les données de paie |

### 6. ACCÈS LECTURE EMPLOYÉS (1 permission)
| Permission | Description |
|------------|-------------|
| `EMPLOYE_VIEW` | Consulter les employés (nécessaire pour calcul paie) |

**TOTAL** : **23 permissions**

---

## ❌ PERMISSIONS NON ACCORDÉES

Le Comptable **N'A PAS** accès à :

### Section Personnel
- ❌ Création/Modification d'employés
- ❌ Gestion des charges familiales
- ❌ Gestion des indemnités
- ❌ Suppression d'employés

### Section Administration
- ❌ Gestion des entreprises
- ❌ Gestion des directions
- ❌ Gestion des services
- ❌ Gestion des catégories
- ❌ Gestion des abonnements

### Fonctions Système
- ❌ Gestion des utilisateurs
- ❌ Visualisation des logs
- ❌ Gestion des rôles et permissions

---

## 📊 Matrice des Accès

| Menu / Section | Comptable | Admin RH | Gestionnaire Paie | Super Admin |
|----------------|-----------|----------|-------------------|-------------|
| **Personnel** | ❌ Non (sauf EMPLOYE_VIEW) | ✅ Oui | ❌ Non | ✅ Oui |
| **Salaire** | ✅ **OUI COMPLET** | ✅ Oui | ✅ Oui | ✅ Oui |
| **Administration** | ❌ Non | ✅ Partiel | ❌ Non | ✅ Oui |
| **Gestion Utilisateurs** | ❌ Non | ❌ Non | ❌ Non | ✅ Oui |
| **Logs** | ❌ Non | ❌ Non | ❌ Non | ✅ Oui |
| **Rôles & Permissions** | ❌ Non | ❌ Non | ❌ Non | ✅ Oui |

---

## 🔧 Installation

### Étape 1 : Exécuter le Script SQL

**Via MySQL Workbench** (Recommandé) :
1. Ouvrir MySQL Workbench
2. Se connecter à votre base de données
3. File → Open SQL Script
4. Sélectionner : `Database\add_role_comptable.sql`
5. Cliquer Execute (Ctrl+Shift+Enter)

**Via Ligne de Commande** :
```bash
mysql -u utilisateur -p nom_base < Database/add_role_comptable.sql
```

### Étape 2 : Vérifier la Création

```sql
-- Vérifier que le rôle existe
SELECT id, nom_role, description, actif
FROM roles
WHERE nom_role = 'Comptable';

-- Vérifier les permissions
SELECT COUNT(*) as nb_permissions
FROM role_permissions rp
JOIN roles r ON rp.role_id = r.id
WHERE r.nom_role = 'Comptable';

-- Résultat attendu : ~23 permissions
```

### Étape 3 : Attribuer le Rôle à un Utilisateur

**Méthode 1 : Via l'Application** (Recommandé)
1. Se connecter en tant que Super Administrateur
2. Menu Administration → Gestion des Utilisateurs
3. Créer ou Modifier un utilisateur
4. Cocher le rôle "Comptable"
5. Enregistrer

**Méthode 2 : Via SQL**
```sql
-- 1. Récupérer l'ID de l'utilisateur
SELECT id FROM utilisateurs WHERE nom_utilisateur = 'comptable_user';
-- Supposons ID = 20

-- 2. Récupérer l'ID du rôle Comptable
SELECT id FROM roles WHERE nom_role = 'Comptable';
-- Supposons ID = 5

-- 3. Attribuer le rôle
INSERT INTO utilisateur_roles (utilisateur_id, role_id)
VALUES (20, 5);

-- 4. Vérifier l'attribution
SELECT u.nom_utilisateur, r.nom_role
FROM utilisateurs u
JOIN utilisateur_roles ur ON u.id = ur.utilisateur_id
JOIN roles r ON ur.role_id = r.id
WHERE u.id = 20;
```

---

## 🧪 Tests

### Test 1 : Créer un Utilisateur Comptable

1. **Se connecter** Super Admin
2. **Administration** → **Gestion Utilisateurs** → **Ajouter**
3. **Remplir** :
   - Nom complet : `Jean Comptable`
   - Nom d'utilisateur : `jcomptable`
   - Email : (optionnel)
4. **Cocher** le rôle **"Comptable"**
5. **Enregistrer**
6. **Noter** le mot de passe par défaut : `RHPlus2026!`

### Test 2 : Première Connexion Comptable

1. **Se déconnecter**
2. **Se connecter** :
   - User : `jcomptable`
   - Mot de passe : `RHPlus2026!`
3. **Changer** le mot de passe (obligatoire)
4. **Se reconnecter** avec le nouveau mot de passe

### Test 3 : Vérifier les Accès

**Menu Tableau de Bord** :
- ✅ Accessible

**Menu Personnel** :
- ❌ **MASQUÉ** (ou lecture seule employés si implémenté)

**Menu Salaire** :
- ✅ **VISIBLE et ACCESSIBLE**
- ✅ Gestion Salaire Horaire
- ✅ Gestion Salaire Journalier
- ✅ Génération Bulletins
- ✅ Impression Bulletins
- ✅ Export Bulletins

**Menu Administration** :
- ❌ **MASQUÉ**

**Résultat Attendu** :
```
Visible :
✅ Tableau de Bord
✅ Salaire (complet)
✅ Sortir

Masqué :
❌ Personnel
❌ Administration
```

### Test 4 : Fonctionnalités Salaire

1. **Cliquer** Salaire → Gestion Salaire Horaire
2. **Vérifier** : Accès complet (création, modification, validation)
3. **Cliquer** Salaire → Génération Bulletins
4. **Vérifier** : Accès complet (génération, impression, export)
5. **Tester** Impression en lot
6. **Tester** Export PDF/Excel

**Résultat Attendu** : ✅ Toutes les fonctions Salaire fonctionnent

---

## 💡 Cas d'Usage

### Workflow Typique d'un Comptable

**1. Début de Mois - Saisie des Heures**
```
Salaire → Gestion Salaire Horaire
  → Saisir heures travaillées pour chaque employé
  → Saisir heures supplémentaires
  → Ajouter sursalaires/primes si nécessaire
  → Valider
```

**2. Calcul des Salaires**
```
Salaire → Génération Bulletins
  → Sélectionner période
  → Lancer le calcul
  → Vérifier les cotisations
  → Vérifier les montants
```

**3. Génération des Bulletins**
```
Salaire → Génération Bulletins
  → Générer bulletins pour tous les employés
  → Vérifier aperçu
  → Confirmer génération
```

**4. Impression et Export**
```
Salaire → Impression Bulletins
  → Sélectionner employés
  → Imprimer en lot (PDF)
  → Ou exporter Excel pour comptabilité
```

**5. Archivage**
```
Salaire → Export
  → Exporter données paie du mois
  → Sauvegarder pour archives
```

---

## 🔐 Sécurité

### Principe du Moindre Privilège
Le rôle Comptable respecte le principe de sécurité :
- ✅ Accès uniquement aux données nécessaires (Salaire)
- ❌ Pas d'accès aux données sensibles (Personnel complet)
- ❌ Pas d'accès aux fonctions système
- ✅ Séparation claire des responsabilités

### Séparation des Responsabilités
| Responsabilité | Rôle Approprié |
|----------------|----------------|
| Gestion Personnel (embauche, contrats) | Admin RH |
| Calcul et Paie | **Comptable** |
| Configuration Entreprise | Admin RH |
| Gestion Utilisateurs | Super Admin |
| Audit et Logs | Super Admin |

### Audit Trail
Toutes les actions du Comptable sont tracées :
- Création/Modification salaires → Logué
- Génération bulletins → Logué
- Validation paie → Logué
- Export données → Logué

---

## 🆚 Comparaison avec Autres Rôles

### Comptable vs Gestionnaire de Paie

| Aspect | Comptable | Gestionnaire de Paie |
|--------|-----------|---------------------|
| Salaires | ✅ Gestion complète | ✅ Gestion complète |
| Bulletins | ✅ Génération/Impression | ✅ Génération/Impression |
| Personnel | ❌ Non (lecture seule) | ❌ Non |
| Sursalaires | ✅ Gestion complète | ✅ Gestion complète |
| Administration | ❌ Non | ❌ Non |
| Export | ✅ Oui | ✅ Oui |

**Conclusion** : Comptable ≈ Gestionnaire de Paie (fonctionnellement identiques)

### Comptable vs Admin RH

| Aspect | Comptable | Admin RH |
|--------|-----------|----------|
| Salaires | ✅ Gestion complète | ✅ Gestion complète |
| Personnel | ❌ Non (lecture seule) | ✅ **Gestion complète** |
| Administration | ❌ Non | ✅ **Partiel** (Config métier) |
| Utilisateurs | ❌ Non | ❌ Non |
| Logs | ❌ Non | ❌ Non |

**Conclusion** : Admin RH a plus de permissions (Personnel + Config)

---

## 📚 Documentation de Référence

### Fichiers Créés
- `Database\add_role_comptable.sql` - Script de création du rôle
- `ROLE_COMPTABLE.md` - Ce document

### Scripts SQL Utiles

**Lister les permissions du Comptable** :
```sql
SELECT p.code_permission, p.description, p.categorie
FROM role_permissions rp
JOIN roles r ON rp.role_id = r.id
JOIN permissions p ON rp.permission_id = p.id
WHERE r.nom_role = 'Comptable'
ORDER BY p.categorie, p.code_permission;
```

**Lister les utilisateurs Comptables** :
```sql
SELECT u.id, u.nom_utilisateur, u.nom_complet, u.email, u.actif
FROM utilisateurs u
JOIN utilisateur_roles ur ON u.id = ur.utilisateur_id
JOIN roles r ON ur.role_id = r.id
WHERE r.nom_role = 'Comptable'
ORDER BY u.nom_complet;
```

**Supprimer le rôle Comptable d'un utilisateur** :
```sql
DELETE ur FROM utilisateur_roles ur
JOIN roles r ON ur.role_id = r.id
WHERE ur.utilisateur_id = <ID_UTILISATEUR>
  AND r.nom_role = 'Comptable';
```

**Désactiver le rôle Comptable** :
```sql
UPDATE roles
SET actif = 0
WHERE nom_role = 'Comptable';
```

---

## ⚠️ Notes Importantes

### 1. Accès Lecture Employés
Le Comptable a la permission `EMPLOYE_VIEW` pour :
- Voir la liste des employés lors du calcul de paie
- Afficher les noms sur les bulletins
- Sélectionner les employés pour génération bulletins

**MAIS** :
- ❌ Ne peut PAS créer d'employés
- ❌ Ne peut PAS modifier les informations personnelles
- ❌ Ne peut PAS supprimer d'employés

### 2. Dépendance avec Personnel
Pour que le Comptable puisse calculer les salaires, il **FAUT** :
- ✅ Que les employés aient été créés (par Admin RH)
- ✅ Que les informations de paie soient renseignées (salaire de base, etc.)
- ✅ Que les charges familiales soient à jour

**Workflow Recommandé** :
1. Admin RH crée/met à jour les employés
2. Comptable calcule les salaires
3. Comptable génère les bulletins

### 3. Permissions Optionnelles
Si vous souhaitez donner plus d'accès au Comptable :

**Ajouter accès Charges Familiales** (pour calcul allocations) :
```sql
SET @comptable_id = (SELECT id FROM roles WHERE nom_role = 'Comptable');
INSERT INTO role_permissions (role_id, permission_id)
SELECT @comptable_id, id FROM permissions
WHERE code_permission IN ('CHARGE_VIEW', 'CHARGE_EDIT');
```

**Ajouter accès Indemnités** (pour calcul indemnités) :
```sql
INSERT INTO role_permissions (role_id, permission_id)
SELECT @comptable_id, id FROM permissions
WHERE code_permission IN ('INDEMNITE_VIEW', 'INDEMNITE_EDIT');
```

---

## 🔄 Mise à Jour du Rôle

Si vous devez modifier les permissions du rôle Comptable :

```sql
-- 1. Supprimer les anciennes permissions
DELETE FROM role_permissions
WHERE role_id = (SELECT id FROM roles WHERE nom_role = 'Comptable');

-- 2. Réexécuter le script add_role_comptable.sql
-- Ou ajouter manuellement les nouvelles permissions
```

---

## ✅ Checklist de Déploiement

**Avant Déploiement**
- [ ] Backup base de données effectué
- [ ] Script `add_role_comptable.sql` vérifié

**Déploiement**
- [ ] Script SQL exécuté sans erreur
- [ ] Rôle "Comptable" créé (vérification SELECT)
- [ ] ~23 permissions attribuées (vérification COUNT)

**Tests**
- [ ] Utilisateur comptable créé
- [ ] Connexion réussie
- [ ] Menu Salaire visible et accessible
- [ ] Menus Personnel et Administration masqués
- [ ] Toutes fonctions Salaire testées
- [ ] Génération bulletins OK
- [ ] Impression/Export OK

**Communication**
- [ ] Utilisateurs comptables informés
- [ ] Formation sur le workflow effectuée
- [ ] Documentation distribuée

---

## 📞 Support

### Problème : Le rôle ne s'affiche pas dans la liste
**Solution** :
```sql
SELECT * FROM roles WHERE nom_role = 'Comptable';
-- Si vide, réexécuter add_role_comptable.sql
```

### Problème : Comptable voit menu Administration
**Solution** :
Vérifier le code dans `Formmain.cs` ligne 74-84 pour s'assurer que seuls les admins voient le menu Administration.

### Problème : Comptable ne voit pas menu Salaire
**Solution** :
```sql
-- Vérifier les permissions
SELECT COUNT(*) FROM role_permissions rp
JOIN roles r ON rp.role_id = r.id
WHERE r.nom_role = 'Comptable';

-- Si 0, réexécuter add_role_comptable.sql
```

---

## 🎉 Résumé

**Rôle créé** : Comptable

**Permissions** : 23 (Salaire complet + EMPLOYE_VIEW)

**Accès** :
- ✅ Salaire (complet)
- ✅ Bulletins (complet)
- ✅ Export (complet)
- ❌ Personnel (lecture seule employés)
- ❌ Administration (aucun)
- ❌ Système (aucun)

**Fichier SQL** : `Database\add_role_comptable.sql`

**Statut** : ✅ **PRÊT POUR PRODUCTION**

---

**Créé le** : 13 février 2026
**Auteur** : Claude Code (Anthropic)
**Projet** : Gestion Moderne RH v1.1.4
