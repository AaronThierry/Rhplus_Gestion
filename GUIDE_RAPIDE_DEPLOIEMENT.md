# ⚡ Guide Rapide de Déploiement - Mot de Passe par Défaut

**Version** : 1.1.4 | **Date** : 13 février 2026

---

## 🎯 EN BREF

Tous les nouveaux utilisateurs reçoivent automatiquement le mot de passe `RHPlus2026!` et doivent le changer à la première connexion.

---

## 🚀 3 ÉTAPES POUR DÉPLOYER

### ✅ ÉTAPE 1 : Appliquer le Script SQL (CRITIQUE)

**Via MySQL Workbench** :
1. Ouvrir MySQL Workbench
2. File → Open SQL Script
3. Sélectionner : `Database\add_premier_connexion_column.sql`
4. Cliquer ⚡ Execute (Ctrl+Shift+Enter)

**Via Ligne de Commande** :
```bash
mysql -u root -p votre_base < "C:\Users\aaron\Pojet GMP RH\Rhplus_Gestion\Database\add_premier_connexion_column.sql"
```

**Vérification** :
```sql
SHOW COLUMNS FROM utilisateurs LIKE 'premier_connexion';
```
✅ Si la colonne existe → OK, passez à l'étape 2

---

### ✅ ÉTAPE 2 : Recharger Visual Studio (Si Erreurs)

**Si vous voyez des erreurs rouges dans Visual Studio** :

**Méthode Rapide** :
1. Fermer Visual Studio
2. Double-cliquer : `FORCER_RELOAD_VS.bat`
3. Attendre "SUCCÈS !"
4. Rouvrir `RH_GRH.sln`

**OU Méthode Manuelle** :
1. Dans VS, clic droit sur projet RH_GRH
2. "Décharger le projet"
3. Clic droit → "Recharger le projet"
4. Générer → Régénérer la solution (Ctrl+Shift+B)

---

### ✅ ÉTAPE 3 : Tester

1. **Lancer l'application** (F5)
2. **Se connecter** (Super Admin)
3. **Administration → Gestion Utilisateurs → Ajouter**
4. **Vérifier** : Pas de champ mot de passe, message informatif
5. **Créer** un utilisateur de test
6. **Vérifier** : Message affiche "RHPlus2026!"
7. **Se déconnecter**
8. **Se connecter** avec le nouveau user (mdp: RHPlus2026!)
9. **Vérifier** : Formulaire changement s'affiche
10. **Changer** le mot de passe
11. **Se reconnecter** avec le nouveau mot de passe

✅ **Si tout fonctionne → Déploiement réussi !**

---

## 📋 CHECKLIST RAPIDE

- [ ] Script SQL exécuté sans erreur
- [ ] Colonnes `premier_connexion` et `mot_de_passe_par_defaut` créées
- [ ] Visual Studio rechargé (pas d'erreurs)
- [ ] Application compile (F5 fonctionne)
- [ ] Test création utilisateur OK
- [ ] Test première connexion OK
- [ ] Test changement mot de passe OK
- [ ] Test reconnexion OK

---

## 🔧 PROBLÈMES FRÉQUENTS

### ❌ Erreur SQL 1064 "syntax error near 'IF NOT EXISTS'"

**Solution** : Le script a été corrigé. Utilisez la dernière version dans `Database\add_premier_connexion_column.sql`

---

### ❌ Erreurs IntelliSense CS0246, CS0234 dans Visual Studio

**Solution** : Cache VS obsolète. Exécutez `FORCER_RELOAD_VS.bat`

**OU** : Clic droit projet → Décharger → Recharger

---

### ❌ MissingManifestResourceException au démarrage

**Solution** : Déjà corrigé dans le fichier `.csproj`. Rebuild la solution (Ctrl+Shift+B)

---

### ❌ L'utilisateur ne voit pas le formulaire de changement

**Vérification** :
```sql
SELECT premier_connexion FROM utilisateurs WHERE nom_utilisateur = 'user';
```

Si `premier_connexion = 0` → Normal, c'est un utilisateur existant

Si `premier_connexion = 1` → Vérifier le code dans `LoginFormModern.cs` lignes 311-346

---

## 📚 DOCUMENTATION COMPLÈTE

| Document | Description |
|----------|-------------|
| **RESUME_COMPLET_v1.1.4.md** | Documentation complète (ce document) |
| **CORRECTION_SQL_MIGRATION.md** | Guide résolution erreur SQL + tests |
| **MODIFICATION_FORMULAIRE_UTILISATEUR.md** | Détails modifications UI |
| **INSTRUCTIONS_VISUAL_STUDIO.md** | Guide rechargement Visual Studio |

---

## 🎯 WORKFLOW UTILISATEUR

```
1. Admin crée utilisateur
   └─> Pas de saisie mot de passe
   └─> Message affiche "RHPlus2026!"

2. User se connecte (mdp: RHPlus2026!)
   └─> Formulaire changement s'affiche automatiquement
   └─> User saisit nouveau mot de passe fort
   └─> Validation stricte (8 chars, maj, min, chiffre, spécial)
   └─> Déconnexion automatique

3. User se reconnecte avec nouveau mdp
   └─> Accès normal à l'application
```

---

## 🔐 MOT DE PASSE PAR DÉFAUT

**Valeur** : `RHPlus2026!`

**Définition** : `RH_GRH\Auth\PasswordGenerator.cs` ligne 12

**Règles Nouveau Mot de Passe** :
- ✅ Minimum 8 caractères
- ✅ Au moins 1 majuscule
- ✅ Au moins 1 minuscule
- ✅ Au moins 1 chiffre
- ✅ Au moins 1 caractère spécial (!@#$%^&*...)
- ❌ Ne peut PAS être "RHPlus2026!"

---

## 💾 COMMANDES SQL UTILES

**Voir utilisateurs en attente de changement** :
```sql
SELECT nom_utilisateur, nom_complet, date_creation
FROM utilisateurs
WHERE premier_connexion = TRUE;
```

**Réinitialiser un utilisateur** :
```sql
UPDATE utilisateurs
SET premier_connexion = TRUE,
    mot_de_passe_hash = SHA2('RHPlus2026!', 256),
    mot_de_passe_par_defaut = 'RHPlus2026!'
WHERE nom_utilisateur = 'nom_user';
```

**Désactiver le changement obligatoire** :
```sql
UPDATE utilisateurs
SET premier_connexion = FALSE,
    mot_de_passe_par_defaut = NULL
WHERE nom_utilisateur = 'nom_user';
```

---

## 📞 BESOIN D'AIDE ?

**Erreur SQL** → Voir `CORRECTION_SQL_MIGRATION.md`

**Erreur Visual Studio** → Voir `INSTRUCTIONS_VISUAL_STUDIO.md`

**Comprendre les modifications** → Voir `RESUME_COMPLET_v1.1.4.md`

---

## ✅ STATUT ACTUEL

| Composant | Statut |
|-----------|--------|
| Code C# | ✅ Compilé avec succès |
| Interface utilisateur | ✅ Champs supprimés, message ajouté |
| Script SQL | ✅ Corrigé (compatible MySQL toutes versions) |
| Documentation | ✅ Complète |
| Tests manuels | ⏳ À effectuer |

---

**Prêt à déployer ! Suivez les 3 étapes ci-dessus. 🚀**

---

**Dernière mise à jour** : 13 février 2026
**Version** : 1.1.4
**Créé par** : Claude Code (Anthropic)
