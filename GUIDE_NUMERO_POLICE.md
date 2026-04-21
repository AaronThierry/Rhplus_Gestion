# Guide d'implémentation du Numéro de Police

## Vue d'ensemble

Ce guide explique comment implémenter le **numéro de police** - un identifiant unique et automatique attribué à chaque employé lors de sa création dans le système RH.

### Caractéristiques

- **Format**: `POL-XXXXXX` (ex: POL-000001, POL-000002, etc.)
- **Génération**: Automatique et séquentielle
- **Unicité**: Garantie par le système
- **Persistance**: Stocké dans la base de données

---

## 📋 Étapes d'installation

### Étape 1: Exécution des scripts SQL

#### 1.1. Ajouter la colonne Police

Exécutez le script `add_police_column.sql` dans votre base de données MySQL:

```bash
mysql -u votre_utilisateur -p votre_base_de_donnees < add_police_column.sql
```

**Ou directement dans MySQL Workbench / phpMyAdmin:**

```sql
-- Ajouter la colonne police
ALTER TABLE personnel
ADD COLUMN police VARCHAR(20) NULL UNIQUE;

-- Créer la table de séquence
CREATE TABLE IF NOT EXISTS police_sequence (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    last_value INT NOT NULL DEFAULT 0,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

-- Initialiser la séquence
INSERT INTO police_sequence (last_value)
SELECT 0
WHERE NOT EXISTS (SELECT 1 FROM police_sequence LIMIT 1);
```

**Vérification:**
```sql
-- Vérifier que la colonne a été ajoutée
DESCRIBE personnel;

-- Vérifier la table de séquence
SELECT * FROM police_sequence;
```

#### 1.2. Générer les numéros pour les employés existants

Exécutez le script `generate_police_numbers.sql`:

```bash
mysql -u votre_utilisateur -p votre_base_de_donnees < generate_police_numbers.sql
```

**Ou directement:**

```sql
-- Variable temporaire pour stocker le compteur
SET @counter = 0;

-- Générer les numéros de police pour les employés existants
UPDATE personnel
SET police = CONCAT('POL-', LPAD(@counter := @counter + 1, 6, '0'))
WHERE police IS NULL
ORDER BY date_entree ASC, id_personnel ASC;

-- Mettre à jour la séquence
UPDATE police_sequence
SET last_value = @counter
WHERE id = 1;
```

**Vérification:**
```sql
-- Vérifier que tous les employés ont un numéro de police
SELECT
    COUNT(*) as total_employes,
    COUNT(police) as employes_avec_police,
    COUNT(*) - COUNT(police) as employes_sans_police
FROM personnel;

-- Afficher quelques exemples
SELECT police, matricule, nomPrenom, date_entree
FROM personnel
ORDER BY police ASC
LIMIT 10;
```

---

## 🔧 Fichiers modifiés

### 1. Nouveaux fichiers créés

#### `RH_GRH/PoliceGenerator.cs`
Classe utilitaire pour générer les numéros de police.

**Méthodes principales:**
- `GenererNumeroPolice()` - Génère un nouveau numéro unique
- `NumeroPoliceExiste(string)` - Vérifie si un numéro existe
- `GetDernierNumeroPolice()` - Récupère le dernier numéro généré
- `InitialiserSequence()` - Initialise la table de séquence

#### Scripts SQL
- `add_police_column.sql` - Ajoute la colonne et la table de séquence
- `generate_police_numbers.sql` - Génère les numéros pour les employés existants

### 2. Fichiers modifiés

#### `RH_GRH/EmployeData.cs`
- Ajout de la propriété `Police` (ligne 13)
- Ajout du paramètre `police` au constructeur (ligne 67)
- Initialisation de la propriété dans le constructeur (ligne 70)

#### `RH_GRH/EmployeClass.cs`

**Méthode `EnregistrerEmploye()` (lignes 78-90):**
- Génération automatique du numéro de police avant l'insertion
- Ajout de la colonne `police` dans la requête INSERT
- Ajout du paramètre `@police` dans les paramètres SQL
- Affichage du numéro de police dans le message de succès

**Méthode `GetEmployeList()` (lignes 152-168):**
- Ajout de la colonne `police` dans la requête SELECT
- Tri par numéro de police et nom

#### `RH_GRH/EmployeDetail.cs`

**Méthode `GetEmployeDetails()` (lignes 19-84):**
- Ajout de la colonne `police` dans la requête SELECT
- Passage du numéro de police au constructeur

---

## 🎯 Fonctionnement

### Pour les nouveaux employés

Lors de l'ajout d'un employé via `AjouterEmployeForm`:

1. L'utilisateur remplit le formulaire
2. Au moment de la validation, `EmployeClass.EnregistrerEmploye()` est appelé
3. La méthode génère automatiquement un numéro de police via `PoliceGenerator.GenererNumeroPolice()`
4. Le numéro est inséré dans la base de données avec les autres informations
5. Un message affiche le numéro de police attribué

**Exemple de message:**
```
Employé enregistré avec succès.
Numéro de police: POL-000042
```

### Pour les employés existants

Les employés déjà présents dans la base de données reçoivent un numéro de police lors de l'exécution du script `generate_police_numbers.sql`.

**Ordre d'attribution:**
- Les employés les plus anciens (date d'entrée) reçoivent les plus petits numéros
- En cas d'égalité de date, l'ID personnel est utilisé comme critère secondaire

---

## 📊 Affichage dans l'interface

### DataGridView (GestionEmployeForm)

La colonne "Police" apparaît maintenant dans la liste des employés:

| Police | Entreprise | Matricule | Nom Prénom | Poste | Contrat | Téléphone |
|--------|------------|-----------|------------|-------|---------|-----------|
| POL-000001 | ACME Corp | EMP001 | Jean Dupont | Développeur | CDI | ... |
| POL-000002 | ACME Corp | EMP002 | Marie Martin | Designer | CDD | ... |

### Détails de l'employé

Le numéro de police est chargé avec les autres informations via `EmployeService.GetEmployeDetails()`.

---

## 🔒 Sécurité et unicité

### Mécanisme de génération

1. **Transaction SQL**: L'incrémentation et la lecture du compteur se font dans une transaction pour éviter les doublons en cas d'accès concurrent
2. **Contrainte UNIQUE**: La colonne `police` a une contrainte d'unicité au niveau de la base de données
3. **Format standardisé**: Le format `POL-XXXXXX` avec padding garantit un tri correct

### Gestion des erreurs

Si une erreur survient lors de la génération:
- La transaction est annulée (rollback)
- Une exception est levée avec un message explicite
- L'utilisateur est informé du problème

---

## 🧪 Tests

### Test 1: Création d'un nouvel employé

```
1. Ouvrir le formulaire "Ajouter un employé"
2. Remplir les informations obligatoires
3. Valider le formulaire
4. Vérifier que le message affiche un numéro de police
5. Consulter la liste des employés
6. Vérifier que le numéro de police apparaît dans la colonne
```

### Test 2: Vérification de l'unicité

```sql
-- Vérifier qu'il n'y a pas de doublons
SELECT police, COUNT(*)
FROM personnel
WHERE police IS NOT NULL
GROUP BY police
HAVING COUNT(*) > 1;

-- Résultat attendu: 0 lignes
```

### Test 3: Vérification de la séquence

```sql
-- Créer 5 employés de test
-- Vérifier que les numéros se suivent: POL-XXXXXX, POL-XXXXXX+1, etc.

SELECT police
FROM personnel
WHERE police LIKE 'POL-%'
ORDER BY police DESC
LIMIT 5;
```

---

## 🔄 Migration et mise à jour

### Pour mettre à jour une installation existante

1. **Sauvegarder la base de données**
   ```bash
   mysqldump -u utilisateur -p base_de_donnees > backup_avant_police.sql
   ```

2. **Exécuter les scripts SQL** (voir Étape 1)

3. **Compiler le nouveau code** dans Visual Studio

4. **Tester sur un environnement de développement** avant de déployer en production

5. **Déployer la nouvelle version** de l'application

### Rollback (en cas de problème)

```sql
-- Supprimer la colonne police
ALTER TABLE personnel DROP COLUMN police;

-- Supprimer la table de séquence
DROP TABLE IF EXISTS police_sequence;

-- Restaurer la sauvegarde si nécessaire
mysql -u utilisateur -p base_de_donnees < backup_avant_police.sql
```

---

## 📝 Notes importantes

1. **Ne jamais modifier manuellement les numéros de police** dans la base de données
2. **Ne jamais supprimer ou modifier la table `police_sequence`** manuellement
3. **Le numéro de police est permanent** et ne change jamais pour un employé donné
4. **Le numéro de police ne peut pas être réutilisé** même si un employé est supprimé
5. **En cas de restauration de base de données**, s'assurer que la table `police_sequence` est également restaurée

---

## 🆘 Dépannage

### Problème: "Erreur lors de la génération du numéro de police"

**Cause**: La table `police_sequence` n'existe pas ou est corrompue

**Solution**:
```sql
-- Recréer la table de séquence
DROP TABLE IF EXISTS police_sequence;

CREATE TABLE police_sequence (
    id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    last_value INT NOT NULL DEFAULT 0,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

-- Initialiser avec le dernier numéro utilisé
INSERT INTO police_sequence (last_value)
SELECT COALESCE(
    CAST(SUBSTRING(MAX(police), 5) AS UNSIGNED),
    0
)
FROM personnel
WHERE police IS NOT NULL;
```

### Problème: Des employés n'ont pas de numéro de police

**Solution**:
```sql
-- Compter les employés sans numéro
SELECT COUNT(*) FROM personnel WHERE police IS NULL;

-- Réexécuter le script de génération
-- (voir generate_police_numbers.sql)
```

### Problème: La colonne Police n'apparaît pas dans le DataGrid

**Cause**: Le cache du DataGridView n'est pas rafraîchi

**Solution**:
1. Redémarrer l'application
2. Ou forcer le rechargement en appelant `ShowTableEmployeGestion()`

---

## ✅ Checklist de déploiement

- [ ] Sauvegarde de la base de données effectuée
- [ ] Script `add_police_column.sql` exécuté avec succès
- [ ] Script `generate_police_numbers.sql` exécuté avec succès
- [ ] Tous les employés ont un numéro de police (vérification SQL)
- [ ] Compilation du code réussie sans erreurs
- [ ] Test de création d'un nouvel employé réussi
- [ ] Numéro de police affiché dans la liste des employés
- [ ] Documentation mise à jour
- [ ] Équipe formée sur la nouvelle fonctionnalité

---

## 📞 Support

Pour toute question ou problème concernant l'implémentation du numéro de police, consultez:
- Ce guide
- Le code source dans `RH_GRH/PoliceGenerator.cs`
- Les scripts SQL dans le répertoire racine
