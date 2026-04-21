# Update v1.1.2 - Gestion Moderne RH

**Date de release** : 9 février 2026

## Correction Critique

### Erreur GDI+ dans le formulaire de modification d'entreprise

Cette mise à jour corrige une erreur critique qui empêchait la modification des informations d'entreprise lorsqu'un logo était présent.

**Symptôme** :
```
System.Runtime.InteropServices.ExternalException
Une erreur générique s'est produite dans GDI+.
```

**Cause** :
- Verrouillage du stream lors du chargement d'images depuis la base de données
- Utilisation de `RawFormat` pour sauvegarder les images, ce qui causait des problèmes avec les images chargées depuis un MemoryStream

**Solution** :
- Création de copies Bitmap indépendantes lors du chargement des logos
- Utilisation de `ImageFormat.Png` pour la sauvegarde au lieu de `RawFormat`
- Gestion propre de la mémoire avec Dispose automatique

## Fichiers modifiés

- **ModifierEntrepriseForm.cs**
  - Ajout de `System.Drawing.Imaging`
  - Méthode `ChargerDonnees()` : création de copie Bitmap
  - Méthode `buttonValider_Click()` : sauvegarde en PNG
  - Méthode `buttonParcourir_Click()` : gestion du verrouillage de fichier

## Installation

### Option 1 : Mise à jour complète (recommandé)

1. Fermez complètement l'application RH+ si elle est en cours d'exécution
2. Sauvegardez votre base de données (optionnel mais recommandé)
3. Remplacez tous les fichiers de votre installation actuelle par ceux de ce dossier
4. Relancez l'application

### Option 2 : Mise à jour de l'exécutable uniquement

1. Fermez complètement l'application RH+ si elle est en cours d'exécution
2. Remplacez uniquement le fichier `RH_GRH.exe` dans votre dossier d'installation
3. Relancez l'application

## Compatibilité

- Compatible avec toutes les versions 1.1.x
- Aucune modification de base de données requise
- Migration transparente depuis v1.1.1
- Les logos existants restent compatibles

## Vérification de la version

Après l'installation, vérifiez que la version affichée dans la sidebar est bien **Version 1.1.2**

## Notes techniques

### Taille de l'exécutable
- RH_GRH.exe : 29 MB

### Configuration minimale requise
- Windows 7 SP1 ou supérieur
- .NET Framework 4.7.2 ou supérieur
- 2 GB RAM minimum
- 100 MB d'espace disque

## Support

Pour toute question ou problème :
- Vérifiez le CHANGELOG.md pour plus de détails
- Consultez la documentation complète
- Contactez le support technique

---

**Version** : 1.1.2
**Type** : Correctif critique
**Priorité** : Haute - recommandé pour tous les utilisateurs de la v1.1.1
