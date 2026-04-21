# 👨‍💼 Guide du Rôle "Agent Administratif"

## 📋 Description

Le rôle **Agent Administratif** est conçu pour les employés qui gèrent quotidiennement le personnel et la paie de l'entreprise. Ce rôle offre un accès complet aux sections Personnel et Salaire sans accès aux fonctions système critiques.

---

## 🎯 Profil Type

- **Agent administratif RH**
- **Assistant de paie**
- **Gestionnaire RH opérationnel**
- **Responsable du personnel (niveau opérationnel)**

---

## ✅ Accès et Permissions

### 📊 Vue d'ensemble

| Catégorie | Niveau d'accès | Permissions |
|-----------|----------------|-------------|
| **Personnel** | Complet | 32 permissions |
| **Salaire** | Complet | 14 permissions |
| **Bulletins** | Complet | 4 permissions |
| **Structure** | Lecture seule | 4 permissions |
| **TOTAL** | - | **54 permissions** |

**Niveau hiérarchique** : 55 (entre Gestionnaire de Paie et Administrateur RH)

---

## 📂 MODULE PERSONNEL (Accès Complet)

### 👥 Gestion des Employés
- ✅ **Consulter** la liste des employés
- ✅ **Créer** de nouveaux employés
- ✅ **Modifier** les informations des employés
- ✅ **Supprimer** des employés
- ✅ **Importer** des employés en masse (Excel)
- ✅ **Exporter** la liste des employés (Excel)

**Cas d'usage** :
- Enregistrer les nouveaux recrutés
- Mettre à jour les informations personnelles
- Gérer les départs et les changements de poste
- Import massif lors d'acquisition d'entreprise

### 💰 Gestion des Charges Familiales
- ✅ **Consulter** les charges
- ✅ **Créer** de nouvelles charges
- ✅ **Modifier** les charges existantes
- ✅ **Supprimer** des charges

**Cas d'usage** :
- Enregistrer les enfants à charge
- Mettre à jour les situations familiales
- Gérer les changements de statut

### 🎁 Gestion des Indemnités
- ✅ **Consulter** les indemnités
- ✅ **Créer** de nouvelles indemnités (transport, logement, etc.)
- ✅ **Modifier** les indemnités existantes
- ✅ **Supprimer** des indemnités

**Cas d'usage** :
- Attribuer indemnités de transport
- Gérer les allocations logement
- Mettre à jour les primes fixes

### 💵 Gestion des Sursalaires
- ✅ **Consulter** les sursalaires
- ✅ **Créer** de nouveaux sursalaires/primes
- ✅ **Modifier** les sursalaires existants
- ✅ **Supprimer** des sursalaires

**Cas d'usage** :
- Attribuer des primes de performance
- Gérer les heures supplémentaires
- Ajouter des bonus exceptionnels

### 📱 Gestion des Abonnements
- ✅ **Consulter** les abonnements
- ✅ **Créer** de nouveaux abonnements
- ✅ **Modifier** les abonnements existants
- ✅ **Supprimer** des abonnements

**Cas d'usage** :
- Gérer les forfaits téléphone
- Enregistrer les frais internet
- Suivre les abonnements divers

---

## 💼 MODULE SALAIRE (Accès Complet)

### ⏰ Salaires Horaires
- ✅ **Consulter** les fiches de paie horaires
- ✅ **Créer** de nouvelles saisies horaires
- ✅ **Modifier** les saisies horaires
- ✅ **Supprimer** des saisies horaires
- ✅ **Valider** les paies horaires (clôture)

**Cas d'usage** :
- Saisir les heures travaillées
- Calculer les paies des employés horaires
- Corriger les erreurs de saisie
- Valider et clôturer les périodes de paie

### 📅 Salaires Journaliers
- ✅ **Consulter** les fiches de paie journalières
- ✅ **Créer** de nouvelles saisies journalières
- ✅ **Modifier** les saisies journalières
- ✅ **Supprimer** des saisies journalières
- ✅ **Valider** les paies journalières (clôture)

**Cas d'usage** :
- Saisir les jours travaillés
- Calculer les paies des employés journaliers
- Gérer les absences et présences
- Valider et clôturer les périodes de paie

---

## 📄 MODULE BULLETINS (Accès Complet)

### Gestion des Bulletins de Paie
- ✅ **Consulter** tous les bulletins de paie
- ✅ **Imprimer** les bulletins individuels
- ✅ **Imprimer en lot** plusieurs bulletins simultanément
- ✅ **Exporter** les bulletins (PDF, Excel)

**Cas d'usage** :
- Distribuer les bulletins aux employés
- Créer des archives PDF
- Exporter pour comptabilité
- Impression massive en fin de mois

---

## 🏢 MODULE STRUCTURE (Lecture Seule)

### Consultation de la Structure Organisationnelle
- ✅ **Consulter** les entreprises
- ✅ **Consulter** les directions
- ✅ **Consulter** les services
- ✅ **Consulter** les catégories professionnelles

**Utilité** :
- Facilite la saisie des employés (choix des services, directions, etc.)
- Permet de comprendre l'organigramme
- Aide à la classification correcte du personnel

**Restriction** : Aucune modification de la structure organisationnelle

---

## ❌ Accès Interdits

### Modules Système (Réservés aux Administrateurs)
- ❌ Gestion des utilisateurs
- ❌ Gestion des rôles et permissions
- ❌ Consultation des logs système
- ❌ Configuration système
- ❌ Sauvegardes

### Modifications de la Structure
- ❌ Créer/Modifier/Supprimer des entreprises
- ❌ Créer/Modifier/Supprimer des directions
- ❌ Créer/Modifier/Supprimer des services
- ❌ Créer/Modifier/Supprimer des catégories

---

## 🚀 Installation

### Exécution du Script SQL

```bash
# Via ligne de commande
mysql -u root -p rh_grh < Database/add_role_agent_administratif.sql

# Ou via phpMyAdmin
# Copier-coller le contenu dans l'onglet SQL et exécuter
```

### Résultat Attendu

Le script affiche :
```
=== RÔLE CRÉÉ ===
Agent Administratif | Niveau 55 | 54 permissions

=== PERMISSIONS PAR MODULE ===
Employés      : 6 permissions
Charges       : 4 permissions
Indemnités    : 4 permissions
Sursalaires   : 4 permissions
Abonnements   : 4 permissions
Salaires      : 10 permissions
Bulletins     : 4 permissions
Entreprises   : 1 permission (VIEW)
Directions    : 1 permission (VIEW)
Services      : 1 permission (VIEW)
Catégories    : 1 permission (VIEW)
```

---

## 👤 Attribution du Rôle à un Utilisateur

### Via SQL

```sql
-- Exemple : Attribuer le rôle à l'utilisateur "marie.durand"
INSERT INTO utilisateur_roles (utilisateur_id, role_id)
SELECT u.id, r.id
FROM utilisateurs u, roles r
WHERE u.nom_utilisateur = 'marie.durand'
  AND r.nom_role = 'Agent Administratif'
  AND NOT EXISTS (
    SELECT 1 FROM utilisateur_roles ur
    WHERE ur.utilisateur_id = u.id AND ur.role_id = r.id
  );
```

### Via l'Interface RH+ Gestion

1. Se connecter avec un compte **Super Administrateur**
2. Menu **Administration** → **Gestion des Utilisateurs**
3. Sélectionner l'utilisateur
4. Cliquer sur **Modifier**
5. Cocher le rôle **"Agent Administratif"**
6. Cliquer sur **Enregistrer**

⚠️ **Important** : L'utilisateur doit se déconnecter et se reconnecter pour que les nouveaux droits soient appliqués.

---

## 📊 Comparaison avec d'autres Rôles

| Rôle | Niveau | Personnel | Salaire | Bulletins | Structure | Système |
|------|--------|-----------|---------|-----------|-----------|---------|
| **Super Administrateur** | 100 | ✅ Complet | ✅ Complet | ✅ Complet | ✅ Complet | ✅ Complet |
| **Administrateur RH** | 80 | ✅ Complet | ✅ Complet | ✅ Complet | ✅ Complet | ❌ |
| **Gestionnaire de Paie** | 60 | 👁️ Vue | ✅ Complet | ✅ Complet | ❌ | ❌ |
| **Agent Administratif** | 55 | ✅ Complet | ✅ Complet | ✅ Complet | 👁️ Vue | ❌ |
| **Responsable Personnel** | 50 | ✅ Complet | ❌ | 👁️ Vue | ✅ Complet | ❌ |
| **Assistant RH** | 30 | ⚠️ Saisie | ⚠️ Saisie | 👁️ Vue | 👁️ Vue | ❌ |
| **Consultant** | 10 | 👁️ Vue | 👁️ Vue | 👁️ Vue | 👁️ Vue | ❌ |

**Légende** :
- ✅ Complet = Création, Modification, Suppression, Export
- ⚠️ Saisie = Création et Modification uniquement (pas de suppression)
- 👁️ Vue = Consultation uniquement
- ❌ = Aucun accès

---

## 💡 Cas d'Usage Pratiques

### Scénario 1 : Traitement de la Paie Mensuelle
1. ✅ Consulter la liste des employés
2. ✅ Vérifier/Mettre à jour les indemnités et sursalaires
3. ✅ Saisir les heures/jours travaillés
4. ✅ Valider les salaires
5. ✅ Imprimer les bulletins en lot
6. ✅ Exporter pour comptabilité

### Scénario 2 : Intégration d'un Nouvel Employé
1. ✅ Créer la fiche employé
2. ✅ Enregistrer les charges familiales
3. ✅ Attribuer les indemnités (transport, logement)
4. ✅ Configurer les abonnements (téléphone)
5. ✅ Saisir la première paie

### Scénario 3 : Gestion des Primes Trimestrielles
1. ✅ Consulter la liste des employés éligibles
2. ✅ Créer les sursalaires (primes)
3. ✅ Attribuer individuellement ou en masse
4. ✅ Intégrer dans le calcul de la paie
5. ✅ Générer les bulletins avec primes

---

## 🔐 Sécurité et Bonnes Pratiques

### ✅ Ce que peut faire un Agent Administratif

- Gérer l'ensemble des opérations courantes de paie
- Administrer les dossiers du personnel
- Produire et distribuer les bulletins de paie
- Effectuer des corrections en cas d'erreur de saisie

### ⚠️ Ce qui nécessite un Administrateur

- Créer/Modifier/Supprimer des utilisateurs
- Attribuer des rôles et permissions
- Modifier la structure organisationnelle (entreprises, services)
- Accéder aux logs d'audit
- Configurer les paramètres système

### 🔒 Recommandations

1. **Attribuer ce rôle uniquement au personnel de confiance** ayant une formation RH/Paie
2. **Former les utilisateurs** sur les procédures de validation des paies (opérations irréversibles)
3. **Superviser régulièrement** les actions via les logs système
4. **Vérifier périodiquement** l'exactitude des saisies
5. **Maintenir une documentation** des procédures internes

---

## 📞 Support

Pour toute question concernant ce rôle :
- 📧 Email : support@rhplus.com
- 📱 Téléphone : +XXX XXX XXX
- 📖 Documentation : [docs.rhplus.com](https://docs.rhplus.com)

---

**Version** : 1.0
**Date de création** : 12 février 2026
**Dernière mise à jour** : 12 février 2026
**Auteur** : Équipe RH+ Gestion
