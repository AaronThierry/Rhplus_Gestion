# Guide d'importation d'employés par lot via Excel

## Vue d'ensemble

La fonctionnalité d'importation Excel permet d'ajouter plusieurs employés en une seule opération en chargeant un fichier Excel (.xls ou .xlsx).

## Comment utiliser l'importation Excel

### Étape 1: Accéder à la fonctionnalité
1. Ouvrez le formulaire **Gestion des Employés**
2. Cliquez sur le bouton **📥 Importer depuis Excel** (bouton bleu cyan)

### Étape 2: Préparer votre fichier Excel
1. Cliquez sur **📄 Télécharger modèle** pour obtenir un fichier Excel avec les en-têtes de colonnes pré-configurées
2. OU créez votre propre fichier Excel avec les colonnes suivantes:

#### Colonnes obligatoires:
- **NomPrenom**: Nom complet de l'employé (obligatoire)
- **Entreprise**: Nom de l'entreprise (obligatoire, doit exister dans la base)
- **Categorie**: Nom de la catégorie (obligatoire, doit exister dans la base pour l'entreprise)

#### Colonnes optionnelles:
- **Civilite**: M., Mme, Mlle
- **Sexe**: Masculin, Féminin
- **DateNaissance**: Format date Excel ou DD/MM/YYYY
- **Adresse**: Adresse de l'employé
- **Telephone**: Numéro de téléphone
- **Identification**: Numéro de carte d'identité ou autre
- **Direction**: Nom de la direction (doit exister dans la base pour l'entreprise)
- **Service**: Nom du service (doit exister dans la base pour l'entreprise)
- **Poste**: Intitulé du poste
- **NumeroCNSS**: Numéro CNSS
- **Contrat**: CDI, CDD
- **TypeContrat**: Horaire, Journalier, Mensuel
- **ModePayement**: Espèces, Virement, Chèque
- **Cadre**: Cadre, Non-Cadre
- **DateEntree**: Date d'entrée (par défaut: date du jour)
- **DateSortie**: Date de sortie (optionnel)
- **HeureContrat**: Nombre d'heures du contrat
- **JourContrat**: Nombre de jours du contrat
- **NumeroBancaire**: Numéro de compte bancaire
- **Banque**: Nom de la banque
- **SalaireMoyen**: Salaire moyen
- **DureeContrat**: Durée du contrat

### Étape 3: Remplir les données
1. Ouvrez le fichier Excel téléchargé
2. Remplissez une ligne par employé
3. **IMPORTANT**:
   - Les noms d'entreprise, direction, service et catégorie doivent correspondre EXACTEMENT aux noms dans votre base de données
   - Les dates doivent être au format date Excel
   - Ne supprimez pas la ligne d'en-têtes (ligne 1)

### Étape 4: Importer les données
1. Dans le formulaire d'importation, cliquez sur **📁 Parcourir...**
2. Sélectionnez votre fichier Excel
3. Une prévisualisation des données s'affiche
4. Vérifiez les données
5. Cliquez sur **📥 Importer** pour lancer l'importation

### Étape 5: Vérifier les résultats
- Le système affiche une barre de progression pendant l'importation
- Après l'importation:
  - Les lignes en **vert** indiquent les employés importés avec succès
  - Les lignes en **rouge** indiquent les erreurs
  - Le matricule généré est affiché pour chaque employé importé
- Les statistiques s'affichent en haut:
  - **Total employés**: Nombre total de lignes lues
  - **Importés avec succès**: Nombre d'employés créés
  - **Erreurs**: Nombre d'employés non importés

## Exemples de fichiers Excel

### Exemple minimal (colonnes obligatoires uniquement):
```
NomPrenom          | Entreprise      | Categorie
Jean Dupont        | ABC Corp        | Cadre
Marie Martin       | ABC Corp        | Employé
Pierre Durand      | XYZ SA          | Technicien
```

### Exemple complet:
```
NomPrenom     | Civilite | Sexe      | Entreprise | Direction | Service    | Categorie | Poste           | Telephone    | DateEntree | TypeContrat
Jean Dupont   | M.       | Masculin  | ABC Corp   | RH        | Recrutement| Cadre     | Responsable RH  | 0123456789   | 01/01/2024 | Mensuel
Marie Martin  | Mme      | Féminin   | ABC Corp   | IT        | Développement| Employé | Développeur     | 0987654321   | 15/02/2024 | Mensuel
```

## Messages d'erreur courants

| Erreur | Cause | Solution |
|--------|-------|----------|
| "Le nom et prénom sont obligatoires" | Cellule NomPrenom vide | Remplir la cellule |
| "L'entreprise est obligatoire" | Cellule Entreprise vide | Remplir la cellule |
| "La catégorie est obligatoire" | Cellule Categorie vide | Remplir la cellule |
| "Entreprise 'XXX' introuvable" | Nom d'entreprise inexact | Vérifier le nom exact dans la base |
| "Direction 'XXX' introuvable pour l'entreprise 'YYY'" | Direction inexistante ou mal orthographiée | Vérifier le nom de la direction |
| "Service 'XXX' introuvable pour l'entreprise 'YYY'" | Service inexistant ou mal orthographié | Vérifier le nom du service |
| "Catégorie 'XXX' introuvable pour l'entreprise 'YYY'" | Catégorie inexistante ou mal orthographiée | Vérifier le nom de la catégorie |

## Conseils et bonnes pratiques

1. **Testez d'abord avec un petit fichier**: Importez 2-3 employés pour vérifier que tout fonctionne
2. **Vérifiez les noms**: Copiez-collez les noms d'entreprise, direction, service et catégorie directement depuis l'application pour éviter les erreurs de frappe
3. **Utilisez le modèle**: Le modèle Excel fourni garantit que les colonnes sont correctement nommées
4. **Sauvegardez votre base**: Avant une importation importante, faites une sauvegarde de votre base de données
5. **Dates**: Utilisez le format de date natif d'Excel (pas de texte)
6. **Ne pas modifier après importation**: Le bouton "Importer" est désactivé après l'importation pour éviter les doublons - fermez et rouvrez le formulaire pour un nouvel import

## Configuration technique requise

Pour que l'importation Excel fonctionne, vous devez avoir installé:
- **Microsoft Access Database Engine** (32-bit ou 64-bit selon votre installation de Windows)
  - Pour fichiers .xlsx: ACE.OLEDB.12.0 provider
  - Pour fichiers .xls: Jet.OLEDB.4.0 provider

### Installation du provider ACE OLEDB:
Si vous obtenez une erreur "Provider cannot be found", téléchargez et installez:
- **Microsoft Access Database Engine 2016 Redistributable**
- Lien: https://www.microsoft.com/fr-fr/download/details.aspx?id=54920
- Installez la version correspondant à votre Office (32-bit ou 64-bit)

## Support

Pour toute question ou problème:
1. Vérifiez que les colonnes obligatoires sont remplies
2. Vérifiez que les noms d'entreprise/direction/service/catégorie existent dans la base
3. Consultez le tableau des données importées pour identifier les erreurs spécifiques
4. Contactez le support technique avec le fichier de log si nécessaire
