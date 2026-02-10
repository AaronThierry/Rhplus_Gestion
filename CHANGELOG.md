# Changelog - Gestion Moderne RH

Toutes les modifications notables de ce projet seront documentées dans ce fichier.

Le format est basé sur [Keep a Changelog](https://keepachangelog.com/fr/1.0.0/),
et ce projet adhère au [Semantic Versioning](https://semver.org/lang/fr/).

---

## [1.1.3] - 2026-02-10

### Corrigé
- **CRITIQUE** : Application du plafond CNSS de 800 000 FCFA pour cotisation employé
  - Le calcul ne respectait pas le plafond légal de la CNSS
  - Pour les salaires > 800 000 FCFA, la cotisation dépassait le maximum autorisé de 44 000 FCFA
  - Solution : Plafonner l'assiette à 800 000 FCFA avant application du taux 5.5%
  - Impact : `CNSS_TPA_CALCULATOR.cs` - Fonction `CalculerCNSSEmploye()`

- **Gestion automatique date de fin contrat CDD**
  - Impossible d'insérer la date de fin pour les contrats CDD
  - Checkbox de date de sortie non activé automatiquement pour les CDD
  - Solution : Activation automatique du DatePicker lors de la sélection "CDD"
  - Validation obligatoire : Impossible de sauvegarder un CDD sans date de fin
  - Impact : `AjouterEmployeForm.cs`, `ModifierEmployeForm.cs`

- **Activation modification du matricule employé**
  - Le champ matricule était en lecture seule dans le formulaire de modification
  - Impossible de corriger un matricule erroné
  - Solution : Rendre le champ modifiable avec validation complète
  - Impact : `ModifierEmployeForm.cs`

### Ajouté
- **Constantes CNSS légales** dans `CNSS_TPA_CALCULATOR.cs`
  - `PLAFOND_CNSS = 800 000 FCFA`
  - `TAUX_CNSS_EMPLOYE = 5.5%`
  - `MONTANT_MAX_CNSS_EMPLOYE = 44 000 FCFA`

- **Validation CDD obligatoire**
  - Événement `ComboBoxContrat_SelectedIndexChanged` dans les formulaires
  - Activation automatique date de sortie si "CDD" sélectionné
  - Message d'erreur si tentative de sauvegarde CDD sans date de fin

- **Validation modification matricule**
  - Variable `matriculeOriginal` pour tracking
  - Validation format avec `MatriculeGenerator.ValiderFormatMatricule()`
  - Normalisation automatique avec `MatriculeGenerator.NormaliserMatricule()`
  - Vérification doublons avec exclusion de l'employé actuel

### Modifié
- **Calcul CNSS employé** - Application du plafond légal
  - Salaire ≤ 800 000 : CNSS = salaire × 5.5%
  - Salaire > 800 000 : CNSS = 44 000 FCFA (plafonné)
  - Exemple : Salaire 1 000 000 → CNSS = 44 000 (au lieu de 55 000)

- **Affichage bulletin de paie**
  - Masquage du nom complet de l'entreprise
  - Seuls le sigle, téléphone et email sont affichés
  - Impact : `BulletinDocument.cs` (ligne 90)

- **Formulaire modification employé**
  - Matricule modifiable au lieu de ReadOnly
  - Ajout champ matricule dans la requête UPDATE SQL
  - Validation et normalisation complètes

### Technique
- **Fichiers modifiés** : 5 fichiers
  - `CNSS_TPA_CALCULATOR.cs` : Plafond CNSS légal
  - `AjouterEmployeForm.cs` : Gestion CDD automatique
  - `ModifierEmployeForm.cs` : Gestion CDD + Modification matricule
  - `BulletinDocument.cs` : Masquage nom entreprise
  - `AssemblyInfo.cs` : Version 1.1.3.0

- **Conformité législative** : Respect strict du plafond CNSS Burkina Faso
- **Amélioration UX** : Activation automatique des champs pertinents
- **Sécurité données** : Validation complète des matricules

### Compatibilité
- Compatible avec les versions précédentes
- Aucune modification de schéma de base de données
- Migration transparente depuis v1.1.2
- Recalcul automatique des cotisations avec nouveau plafond

---

## [1.1.2] - 2026-02-09

### Corrigé
- **CRITIQUE** : Correction de l'erreur GDI+ lors de la modification d'entreprise
  - Erreur "Une erreur générique s'est produite dans GDI+" dans `ModifierEntrepriseForm.buttonValider_Click`
  - Le problème survenait lors de la sauvegarde du logo en raison d'un verrouillage de stream/fichier
  - Solution : Création d'une copie Bitmap indépendante de l'image au lieu d'utiliser directement l'image source
  - Impact : `ModifierEntrepriseForm.cs` (lignes 103-106, 186, 281-287)

### Modifié
- **Gestion des images dans ModifierEntrepriseForm**
  - Ajout de l'import `System.Drawing.Imaging` pour ImageFormat
  - Méthode `ChargerDonnees()` : création d'une copie Bitmap lors du chargement du logo depuis la base de données
  - Méthode `buttonValider_Click()` : utilisation de `ImageFormat.Png` au lieu de `RawFormat` pour la sauvegarde
  - Méthode `buttonParcourir_Click()` : création d'une copie Bitmap lors de la sélection d'un nouveau fichier image
  - Dispose automatique des images précédentes pour éviter les fuites mémoire

### Technique
- **Fichiers modifiés** : 1 fichier (ModifierEntrepriseForm.cs)
- **Lignes modifiées** : ~20 lignes
- **Architecture** : Gestion robuste des ressources images avec copies indépendantes
- **Prévention** : Évite le verrouillage de fichiers et streams lors de la manipulation d'images

### Compatibilité
- Compatible avec les versions précédentes
- Aucune modification de base de données
- Migration transparente depuis v1.1.1
- Les logos existants restent compatibles

---

## [1.0.7] - 2026-01-26

### Ajouté
- **Label de version dans la sidebar**
  - Ajout d'un label affichant la version de l'application (Version 1.0.7)
  - Positionnement en haut de la sidebar, au-dessus du logo
  - Lecture dynamique de la version depuis AssemblyInfo
  - Police Montserrat 7.5pt, couleur gris clair
  - Alignement centré pour une meilleure lisibilité

- **Espacements uniformes dans la sidebar**
  - Espacement de 5px entre le logo et le bouton "Tableau de bord"
  - Espacement de 10px entre tous les boutons principaux (Personnel, Salaire, Administration, Sortir)
  - Amélioration de l'organisation visuelle et de l'ergonomie

### Corrigé
- **Affichage de la valeur de remboursement de dette**
  - Correction du bug où la valeur de dette ne s'affichait pas sur le bulletin de paie journalier individuel
  - Ajout de `ValeurDette` dans le modèle BulletinModel lors de la génération du PDF
  - Impact : GestionSalaireJournalierForm.cs (ligne 1542)

- **Chevauchement des boutons dans la sidebar**
  - Correction du problème de chevauchement entre "Tableau de bord" et "Personnel"
  - Déplacement du button2 (Tableau de bord) du panel1 vers panel_slide
  - Ajout de la propriété Dock.Top pour un alignement correct

- **Message de succès personnalisé**
  - Remplacement du MessageBox standard par CustomMessageBox pour le bulletin journalier
  - Utilisation de CustomMessageBox.MessageType.Success pour un affichage cohérent
  - Amélioration de l'expérience utilisateur avec le style personnalisé de l'application

### Modifié
- **Structure de la sidebar (Formmain.Designer.cs)**
  - Réorganisation de la hiérarchie des contrôles pour éviter les chevauchements
  - panel1 contient maintenant uniquement : labelVersion + pictureBox1 (logo)
  - button2 (Tableau de bord) directement dans panel_slide avec Dock.Top

- **Gestion des espacements (Formmain.cs)**
  - Refactorisation du code d'espacement des boutons
  - Utilisation de Padding uniformes : new Padding(0, top, 0, 0)
  - Code plus maintenable et cohérent

### Technique
- **Fichiers modifiés** : 5 fichiers
  - Formmain.cs : Ajout de SetVersionLabel() et espacements uniformes
  - Formmain.Designer.cs : Ajout labelVersion, réorganisation de panel1
  - GestionSalaireJournalierForm.cs : Correction ValeurDette + CustomMessageBox
  - App.config : Binding redirect System.Resources.Extensions (de v1.0.6)
  - AssemblyInfo.cs : Version 1.0.7.0

### Architecture
- **Sidebar modernisée**
  ```
  panel_slide
  ├── panel1 (Version + Logo)
  │   ├── labelVersion (Version 1.0.7)
  │   └── pictureBox1 (Logo RH+)
  ├── button2 (Tableau de bord) - 5px margin
  ├── button_personnel (Personnel) - 10px margin
  ├── button_salaire (Salaire) - 10px margin
  ├── button_administration (Administration) - 10px margin
  └── button_exit (Sortir) - 10px margin
  ```

### Compatibilité
- Compatible avec les versions précédentes
- Aucune modification de base de données
- Migration transparente depuis v1.0.6

---

## [1.0.6] - 2026-01-26

### Ajouté
- **Gestion avancée des matricules**
  - Mode de génération automatique des matricules avec format personnalisé (XX###A)
  - Option de saisie manuelle des matricules avec validation en temps réel
  - Validation du format : 2-20 caractères alphanumériques, lettres majuscules, chiffres, tirets et underscores
  - Vérification automatique des doublons lors de l'ajout et modification d'employés
  - Normalisation automatique (conversion en majuscules, suppression des espaces)
  - Interface utilisateur moderne avec indicateur visuel du mode sélectionné
  - Aide contextuelle affichant les formats acceptés

- **Système de modes de paiement enrichi**
  - Ajout du mode "Mobile Money" aux options de paiement
  - Champs conditionnels selon le mode de paiement sélectionné :
    - **Espèces** : pas de champs bancaires
    - **Virement bancaire** : banque + numéro de compte
    - **Chèque** : banque + numéro de chèque
    - **Mobile Money** : opérateur + numéro de téléphone
  - Labels dynamiques adaptés au contexte (ex: "Numéro de compte" vs "Numéro de chèque")
  - Affichage intelligent sur les bulletins de paie avec formatage conditionnel

- **Améliorations des bulletins de paie**
  - Formatage intelligent du mode de paiement avec détails bancaires
  - Affichage conditionnel des informations selon le type de paiement
  - Meilleure présentation des coordonnées bancaires

- **Nouvelles propriétés dans le modèle de données**
  - Ajout des propriétés `Banque` et `NumeroBancaire` dans `EmployeData`
  - Ajout des propriétés `ModePayement`, `Banque` et `NumeroBancaire` dans `BulletinModel`
  - Ajout de la propriété `NumeroBancaire` dans `PayrollSnapshot`

### Modifié
- **Interface d'ajout d'employé (AjouterEmployeForm.cs)**
  - Refonte complète de la section matricule avec design moderne
  - Ajout de 371 lignes de code pour la gestion des matricules
  - Intégration de contrôles Guna.UI2 avec style cohérent

- **Interface de modification d'employé (ModifierEmployeForm.cs)**
  - Extension du ComboBox des modes de paiement (4 options au lieu de 3)
  - Ajout de la méthode `UpdateModePayementFields()` pour gérer l'affichage conditionnel
  - Gestion dynamique de la visibilité des champs bancaires
  - Sauvegarde automatique des valeurs lors du changement de mode

- **Générateur de matricules (MatriculeGenerator.cs)**
  - Ajout de 63 lignes de nouvelles fonctionnalités
  - Méthode `MatriculeExiste()` : vérification des doublons avec exclusion optionnelle
  - Méthode `ValiderFormatMatricule()` : validation avec messages d'erreur détaillés
  - Méthode `NormaliserMatricule()` : normalisation et nettoyage des matricules

- **Service de génération de bulletins batch (BatchBulletinService.cs)**
  - Ajout du support des nouvelles propriétés bancaires
  - Propagation des informations de paiement vers les bulletins

- **Génération de documents bulletins (BulletinDocument.cs)**
  - Ajout de la méthode `FormaterModePayement()` (104 lignes)
  - Gestion intelligente de 5 types de paiements différents
  - Formatage contextuel avec informations bancaires complètes

### Technique
- **Fichiers modifiés** : 13 fichiers (691 insertions, 15 suppressions)
- **Nouveaux contrôles UI** : TextBox et CheckBox avec style Guna.UI2
- **Validation robuste** : Regex pour format matricule, vérification base de données
- **Architecture** : Séparation des préoccupations avec méthodes dédiées

### Compatibilité
- Compatible avec les versions précédentes
- Les matricules existants restent valides
- Migration transparente des modes de paiement existants
- Aucune modification de schéma de base de données requise

---

## [1.0.5] - 2026-01-24

### Corrigé
- **CRITIQUE** : Correction du calcul IUTS pour les employés cadres dans le traitement par lot
  - Le statut "Cadre" n'était pas correctement reconnu lors de la génération des bulletins en masse
  - Ajout de la normalisation du statut cadre dans `BatchBulletinService.cs`
  - Impact : calculs fiscaux corrects pour tous les cadres

### Modifié
- Reconstruction complète de l'installateur v1.0.5 avec binaires vérifiés

---

## [1.0.4] - 2026-01-23

### Modifié
- Application de l'arrondi au plafond à 1 FCFA pour tous les calculs de paie
  - Standardisation dans le traitement par lot (`BatchBulletinService.cs`)
  - Uniformisation de la méthode d'arrondi à travers toute l'application
  - Garantit la cohérence des montants sur tous les bulletins

---

## [1.0.3] - 2026-01-22

### Versions antérieures
- Versions initiales du système
- Fonctionnalités de base de gestion RH et paie
- Calculs de cotisations sociales CNSS, IUTS
- Génération de bulletins de paie PDF
- Gestion des employés (ajout, modification, consultation)
- Support des types de contrats : horaire, journalier, mensuel
- Traitement par lot des bulletins

---

## Légende

- **Ajouté** : Nouvelles fonctionnalités
- **Modifié** : Changements dans les fonctionnalités existantes
- **Corrigé** : Corrections de bugs
- **Supprimé** : Fonctionnalités retirées
- **Sécurité** : Correctifs de sécurité
- **Déprécié** : Fonctionnalités obsolètes mais toujours présentes

---

**Format des versions** : MAJEUR.MINEUR.CORRECTIF
- **MAJEUR** : Changements incompatibles avec les versions précédentes
- **MINEUR** : Ajout de fonctionnalités rétrocompatibles
- **CORRECTIF** : Corrections de bugs rétrocompatibles
