# ✅ Guide de Test - Restriction Administrateur RH

**Version** : 1.1.5 | **Date** : 13 février 2026

---

## 🎯 Objectif des Tests

Vérifier que le rôle "Administrateur RH" **N'A PLUS ACCÈS** aux sous-menus système :
- ❌ Gestion des Utilisateurs
- ❌ Visualisation des Logs
- ❌ Gestion des Rôles et Permissions

Tout en conservant l'accès aux autres fonctions admin :
- ✅ Entreprise, Direction, Service, Catégorie, Abonnement

---

## 📋 Prérequis

### Comptes de Test Nécessaires

Vous devez avoir **au minimum 2 comptes** :

| Nom d'utilisateur | Rôle | Mot de passe | Utilisation |
|-------------------|------|--------------|-------------|
| `superadmin` ou `admin` | **Super Administrateur** | Votre mdp admin | Test accès complet |
| `admin_rh` | **Administrateur RH** | À créer si nécessaire | Test restriction |

### Si vous n'avez pas de compte "Administrateur RH"

**Option A : Via l'Application (Si vous avez accès Super Admin)**

1. Lancer l'application
2. Se connecter en tant que Super Administrateur
3. Menu **Administration** → **Gestion des Utilisateurs** → **Ajouter**
4. Créer un utilisateur :
   - Nom d'utilisateur : `admin_rh`
   - Nom complet : `Administrateur RH Test`
   - Rôle : Cocher **Administrateur RH**
5. Noter le mot de passe par défaut : `RHPlus2026!`
6. Enregistrer

**Option B : Via SQL (Si pas d'accès Super Admin)**

```sql
-- 1. Créer l'utilisateur
INSERT INTO utilisateurs
(nom_utilisateur, mot_de_passe_hash, nom_complet, email, actif,
 date_creation, date_modification, tentatives_echec, compte_verrouille,
 premier_connexion, mot_de_passe_par_defaut)
VALUES
('admin_rh', SHA2('RHPlus2026!', 256), 'Administrateur RH Test', 'admin_rh@test.com', 1,
 NOW(), NOW(), 0, 0, TRUE, 'RHPlus2026!');

-- 2. Récupérer l'ID de l'utilisateur créé
SELECT id FROM utilisateurs WHERE nom_utilisateur = 'admin_rh';
-- Supposons ID = 10

-- 3. Récupérer l'ID du rôle "Administrateur RH"
SELECT id FROM roles WHERE nom_role = 'Administrateur RH';
-- Supposons ID = 3

-- 4. Attribuer le rôle
INSERT INTO utilisateur_roles (utilisateur_id, role_id)
VALUES (10, 3);
```

---

## 🧪 Tests à Effectuer

### ✅ TEST 1 : Accès Super Administrateur (Témoin)

**Objectif** : Vérifier que le Super Admin voit TOUS les menus.

**Étapes** :

1. **Lancer** l'application `RH_GRH.exe`
2. **Se connecter** avec le compte Super Administrateur :
   - Nom d'utilisateur : `superadmin` (ou votre compte admin)
   - Mot de passe : Votre mot de passe
3. **Cliquer** sur le menu **Administration** dans la barre latérale
4. **Observer** les sous-menus affichés

**Résultat Attendu** :

```
Administration
  ├─ Entreprise              ✅ Visible
  ├─ Direction               ✅ Visible
  ├─ Service                 ✅ Visible
  ├─ Catégorie               ✅ Visible
  ├─ Utilisateurs            ✅ Visible ← IMPORTANT
  ├─ Abonnement              ✅ Visible
  ├─ Logs                    ✅ Visible ← IMPORTANT
  └─ Rôles & Permissions     ✅ Visible ← IMPORTANT
```

**Validation** :
- [ ] ✅ Tous les 8 sous-menus sont visibles
- [ ] ✅ Les boutons "Utilisateurs", "Logs" et "Rôles & Permissions" sont bien là

**Si NON** → ❌ Problème de configuration, vérifier le rôle "Super Administrateur" dans la base de données

---

### ✅ TEST 2 : Restriction Administrateur RH (Test Principal)

**Objectif** : Vérifier que l'Administrateur RH NE VOIT PAS les menus système.

**Étapes** :

1. **Se déconnecter** du compte Super Admin
2. **Se connecter** avec le compte Administrateur RH :
   - Nom d'utilisateur : `admin_rh`
   - Mot de passe : `RHPlus2026!` (si première connexion)
3. **Si première connexion** : Changer le mot de passe, puis se reconnecter
4. **Cliquer** sur le menu **Administration** dans la barre latérale
5. **Observer** les sous-menus affichés

**Résultat Attendu** :

```
Administration
  ├─ Entreprise              ✅ Visible
  ├─ Direction               ✅ Visible
  ├─ Service                 ✅ Visible
  ├─ Catégorie               ✅ Visible
  ├─ Utilisateurs            ❌ MASQUÉ ← CRITIQUE
  ├─ Abonnement              ✅ Visible
  ├─ Logs                    ❌ MASQUÉ ← CRITIQUE
  └─ Rôles & Permissions     ❌ MASQUÉ ← CRITIQUE
```

**Validation** :
- [ ] ✅ Les boutons "Entreprise", "Direction", "Service", "Catégorie" et "Abonnement" sont visibles
- [ ] ✅ Le bouton "Utilisateurs" est **MASQUÉ**
- [ ] ✅ Le bouton "Logs" est **MASQUÉ**
- [ ] ✅ Le bouton "Rôles & Permissions" est **MASQUÉ**

**Si OUI** → ✅ **TEST RÉUSSI !** La restriction fonctionne correctement.

**Si NON** → ❌ Problème :
- Vérifier que le fichier `RH_GRH.exe` a bien été recompilé
- Vérifier que vous utilisez la bonne version de l'application
- Exécuter le script `FORCER_RELOAD_VS.bat` et recompiler

---

### ✅ TEST 3 : Accès aux Fonctions Métier (Administrateur RH)

**Objectif** : Vérifier que l'Administrateur RH conserve l'accès aux fonctions métier.

**Étapes** :

1. **Rester connecté** en tant qu'Administrateur RH
2. **Tester l'accès** aux menus suivants :

**Menu Personnel** :
- Cliquer sur **Personnel**
- Vérifier que les sous-menus s'affichent :
  - ✅ Employés
  - ✅ Charges familiales
  - ✅ Indemnités

**Menu Salaire** :
- Cliquer sur **Salaire**
- Vérifier que les sous-menus s'affichent :
  - ✅ Génération bulletin
  - ✅ Calcul salaires
  - ✅ Cotisations
  - ✅ etc.

**Menu Administration** (fonctions conservées) :
- Cliquer sur **Administration** → **Entreprise**
- Vérifier que le formulaire s'ouvre
- Essayer de modifier une entreprise
- Cliquer sur **Administration** → **Direction**
- Vérifier que la liste s'affiche

**Résultat Attendu** :
- [ ] ✅ Menu **Personnel** : Accès complet
- [ ] ✅ Menu **Salaire** : Accès complet
- [ ] ✅ Menu **Administration** → Entreprise : Fonctionnel
- [ ] ✅ Menu **Administration** → Direction : Fonctionnel
- [ ] ✅ Menu **Administration** → Service : Fonctionnel
- [ ] ✅ Menu **Administration** → Catégorie : Fonctionnel
- [ ] ✅ Menu **Administration** → Abonnement : Fonctionnel

**Si NON** → ❌ Problème : L'Administrateur RH ne peut plus faire son travail, revoir la configuration

---

### ✅ TEST 4 : Tentative d'Accès Direct (Sécurité)

**Objectif** : Vérifier qu'un Administrateur RH ne peut pas contourner la restriction.

**Note** : Ce test est optionnel et nécessite des connaissances techniques.

**Étapes** :

1. **Connecté en tant qu'Administrateur RH**
2. **Tenter d'ouvrir directement** le formulaire de gestion des utilisateurs en tapant dans la barre d'adresse (si applicable) ou en essayant de forcer l'ouverture

**Résultat Attendu** :
- [ ] ✅ Le bouton est masqué et inaccessible
- [ ] ✅ Aucune possibilité de contournement

**Si l'Administrateur RH peut quand même accéder** → ❌ Vulnérabilité de sécurité, il faut ajouter des vérifications dans les formulaires eux-mêmes

---

## 📊 Tableau Récapitulatif des Tests

| Test | Utilisateur | Menu Testé | Résultat Attendu | Statut |
|------|-------------|------------|------------------|--------|
| 1 | Super Admin | Administration | Tous les sous-menus visibles | ⬜ À tester |
| 2a | Admin RH | Administration → Utilisateurs | **MASQUÉ** | ⬜ À tester |
| 2b | Admin RH | Administration → Logs | **MASQUÉ** | ⬜ À tester |
| 2c | Admin RH | Administration → Rôles | **MASQUÉ** | ⬜ À tester |
| 2d | Admin RH | Administration → Entreprise | Visible | ⬜ À tester |
| 2e | Admin RH | Administration → Direction | Visible | ⬜ À tester |
| 3a | Admin RH | Personnel | Accès complet | ⬜ À tester |
| 3b | Admin RH | Salaire | Accès complet | ⬜ À tester |

**Légende** :
- ⬜ À tester
- ✅ Réussi
- ❌ Échoué

---

## 🐛 Dépannage

### Problème 1 : Tous les menus sont visibles pour l'Administrateur RH

**Causes possibles** :
1. Application non recompilée avec les modifications
2. Cache Visual Studio non rafraîchi
3. Mauvaise version de l'application lancée

**Solutions** :
1. Fermer Visual Studio
2. Exécuter `FORCER_RELOAD_VS.bat`
3. Rouvrir Visual Studio
4. Rebuild complet (Ctrl+Shift+B)
5. Vérifier que `bin\Debug\RH_GRH.exe` a été mis à jour (date de modification récente)
6. Relancer l'application

---

### Problème 2 : Aucun menu n'est visible (écran vide)

**Causes possibles** :
1. Erreur de compilation non détectée
2. Problème de session utilisateur
3. Rôle non correctement attribué

**Solutions** :
1. Vérifier les logs d'erreur
2. Se déconnecter et se reconnecter
3. Vérifier en base de données :
```sql
SELECT u.nom_utilisateur, r.nom_role
FROM utilisateurs u
JOIN utilisateur_roles ur ON u.id = ur.utilisateur_id
JOIN roles r ON ur.role_id = r.id
WHERE u.nom_utilisateur = 'admin_rh';
```
4. Vérifier que le rôle "Administrateur RH" existe et est bien attribué

---

### Problème 3 : L'application ne se lance pas

**Causes possibles** :
1. Erreur de compilation
2. Dépendances manquantes
3. Fichier .exe corrompu

**Solutions** :
1. Ouvrir Visual Studio
2. Regarder la fenêtre "Erreurs" (Ctrl+W, E)
3. Rebuild complet
4. Si erreur "assembly introuvable", restaurer les packages NuGet :
   - Clic droit sur la solution → Restaurer les packages NuGet

---

## 📸 Captures d'Écran de Référence

### Attendu pour Super Administrateur

```
┌─ Administration ────────────────┐
│  📊 Entreprise                  │
│  🏢 Direction                   │
│  🏬 Service                     │
│  📋 Catégorie                   │
│  👥 Utilisateurs     ← Visible  │
│  📅 Abonnement                  │
│  📜 Logs             ← Visible  │
│  🔐 Rôles & Permissions ← Visible│
└─────────────────────────────────┘
```

### Attendu pour Administrateur RH

```
┌─ Administration ────────────────┐
│  📊 Entreprise                  │
│  🏢 Direction                   │
│  🏬 Service                     │
│  📋 Catégorie                   │
│  ❌ (Utilisateurs masqué)       │
│  📅 Abonnement                  │
│  ❌ (Logs masqué)               │
│  ❌ (Rôles masqué)              │
└─────────────────────────────────┘
```

---

## ✅ Validation Finale

**Cochez cette liste après avoir terminé tous les tests** :

- [ ] Test 1 réussi : Super Admin voit tout
- [ ] Test 2 réussi : Admin RH ne voit pas Utilisateurs, Logs, Rôles
- [ ] Test 3 réussi : Admin RH a accès aux fonctions métier
- [ ] Test 4 réussi : Pas de contournement possible (optionnel)
- [ ] Aucun message d'erreur lors de la connexion
- [ ] L'application est stable
- [ ] Les utilisateurs finaux ont été informés

**Si toutes les cases sont cochées** → ✅ **Déploiement validé !**

---

## 📞 Support

Si vous rencontrez des problèmes pendant les tests :

1. **Consulter la documentation** : `RESTRICTION_ADMIN_RH.md`
2. **Vérifier les logs** : Menu Logs (avec compte Super Admin)
3. **Vérifier la base de données** :
```sql
-- Lister tous les rôles et leurs permissions
SELECT r.nom_role, p.code_permission
FROM roles r
LEFT JOIN role_permissions rp ON r.id = rp.role_id
LEFT JOIN permissions p ON rp.permission_id = p.id
ORDER BY r.nom_role, p.code_permission;
```

---

**Bon test ! 🚀**

**Date** : 13 février 2026
**Version** : 1.1.5
**Auteur** : Claude Code (Anthropic)
