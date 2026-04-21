# Correction : Attribution Police et Génération Matricule

## Problèmes identifiés

1. **Police non attribuée** lors de l'ajout d'un employé
2. **Matricule généré en format TEMP** au lieu du format avec initiales de l'entreprise

## Solutions apportées

### 1. Attribution automatique du numéro de police

**Fichier modifié**: `RH_GRH/AjouterEmployeForm.cs`

- Ajout de la génération automatique du numéro de police avant l'insertion
- Ajout du champ `police` dans la requête INSERT
- Le numéro de police est généré via `PoliceGenerator.GenererNumeroPolice()`

### 2. Correction de la génération du matricule

**Problème**: La table `seq_matricule` n'existe pas dans la base de données, ce qui provoque une exception et le retour d'un matricule temporaire au format `TEMP####`.

**Solution**: Exécuter le script SQL de création de la table.

## Instructions de déploiement

### Étape 1: Créer la table seq_matricule

Exécutez le script SQL suivant dans votre base de données:

```sql
-- Script disponible dans: create_seq_matricule_table.sql

DROP TABLE IF EXISTS seq_matricule;

CREATE TABLE seq_matricule (
    id BIGINT AUTO_INCREMENT PRIMARY KEY,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
```

### Étape 2: Vérifier la configuration

Après avoir créé la table, testez l'ajout d'un employé pour vérifier que:
- Le numéro de police est généré automatiquement (format: A1B2C3D)
- Le matricule est généré avec les initiales de l'entreprise (format: XX###A)

### Étape 3: Mise à jour des employés existants sans police

Si vous avez des employés existants sans numéro de police, exécutez ce script:

```sql
-- Mettre à jour les employés sans numéro de police
UPDATE personnel
SET police = NULL
WHERE police IS NULL OR police = '';

-- Puis utilisez le script generate_police_OPTIMAL.sql pour générer les numéros
-- (ce script doit déjà exister dans votre projet)
```

## Format des identifiants

### Numéro de Police
- Format: **7 caractères alphanumériques aléatoires**
- Exemple: `A1B2C3D`, `X9Y8Z7W`
- Généré par: `PoliceGenerator.GenererNumeroPolice()`

### Matricule
- Format: **XX###A**
  - XX = Initiales de l'entreprise (2 caractères)
  - ### = Numéro séquentiel (001-999)
  - A = Lettre de cycle (A-Z)
- Exemple: `SI001A`, `AG042B`, `RH999Z`
- Généré par: `MatriculeGenerator.GenererMatricule(idEntreprise)`

## Fichiers modifiés

1. `RH_GRH/AjouterEmployeForm.cs` - Ajout génération police et correction INSERT
2. `create_seq_matricule_table.sql` - Script de création de la table de séquence

## Test de validation

1. Ouvrir le formulaire d'ajout d'employé
2. Remplir les informations obligatoires
3. Enregistrer l'employé
4. Vérifier dans la table `personnel`:
   - Champ `police` rempli (ex: `A1B2C3D`)
   - Champ `matricule` au bon format (ex: `SI001A` et non `TEMP1463`)

## Logs de débogage

Les logs sont écrits dans: `%TEMP%\rh_debug_log.txt`

Vérifiez ce fichier pour diagnostiquer les problèmes de génération de matricule.
