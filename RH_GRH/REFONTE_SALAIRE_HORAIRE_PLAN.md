# 🚀 Plan de Refonte - Gestion Salaire Horaire

## 🎯 Objectifs de la Refonte

### Priorités
1. **CRITIQUE** : Afficher les résultats de calcul (actuellement invisibles)
2. **HAUTE** : Simplifier le workflow avec navigation claire
3. **HAUTE** : Réorganiser visuellement les contrôles par catégories logiques
4. **MOYENNE** : Améliorer la validation en temps réel
5. **MOYENNE** : Optimiser l'expérience utilisateur globale

---

## 📐 Nouvelle Architecture : Interface par Onglets

### Structure Proposée (TabControl avec 4 onglets)

```
┌─────────────────────────────────────────────────────────────┐
│ GESTION DES SALAIRES HORAIRES                                │
│ ──────────────────────────────────────────────────────────── │
│ [1.EMPLOYÉ] [2.PÉRIODE & SAISIE] [3.RÉSULTATS] [4.BULLETIN] │
└─────────────────────────────────────────────────────────────┘
```

---

## 📋 Détail des Onglets

### ONGLET 1 : SÉLECTION EMPLOYÉ

**Objectif** : Rechercher et sélectionner l'employé

**Contenu** :
```
┌─────────────────────────────────────────────────────────────┐
│ 🔍 RECHERCHE ET SÉLECTION                                    │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│ Rechercher :                                                 │
│ ┌──────────────────────────────────────────────────────┐   │
│ │ [Nom, matricule, poste, service...]        🔍        │   │
│ └──────────────────────────────────────────────────────┘   │
│                                                              │
│ Employé :                                                    │
│ ┌──────────────────────────────────────────────────────┐   │
│ │ [Jean DUPONT - MAT001 - Technicien ▼]               │   │
│ └──────────────────────────────────────────────────────┘   │
│                                                              │
│ ┌──────────────────────────────────────────────────────┐   │
│ │ ℹ️ INFORMATIONS EMPLOYÉ                              │   │
│ ├──────────────────────────────────────────────────────┤   │
│ │ Matricule :       MAT001                             │   │
│ │ Nom complet :     Jean DUPONT                        │   │
│ │ Poste :           Technicien                         │   │
│ │ Contrat :         CDD - Horaire                      │   │
│ │ Catégorie :       A1                                 │   │
│ │ H. Contrat :      172 heures/mois                    │   │
│ │ Salaire catég. :  200,000 FCFA                       │   │
│ │ Ancienneté :      2 ans 5 mois                       │   │
│ └──────────────────────────────────────────────────────┘   │
│                                                              │
│                                   [CONTINUER À L'ÉTAPE 2 →] │
└─────────────────────────────────────────────────────────────┘
```

**Améliorations** :
- GroupBox clair "Informations Employé"
- Police plus grande pour lisibilité
- Disposition verticale (plus lisible que grille)
- Bouton "Continuer" désactivé jusqu'à sélection valide

---

### ONGLET 2 : PÉRIODE & SAISIE

**Objectif** : Définir période et saisir heures/absences

**Contenu** :
```
┌─────────────────────────────────────────────────────────────┐
│ 📅 PÉRIODE DE PAIE                                           │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│ Période du :    [📅 01/09/2025]  au :  [📅 30/09/2025]    │
│                                                              │
│ ✅ Durée : 30 jours  (240 heures contractuelles)           │
│                                                              │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│ ⏱️ SAISIE DES HEURES ET ABSENCES                            │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│ ┌──────────────────────────────────────────────────────┐   │
│ │ 🚫 ABSENCES                                          │   │
│ ├──────────────────────────────────────────────────────┤   │
│ │  Heures d'absence : [____0____] heures               │   │
│ │                                                       │   │
│ │  ℹ️ Heures de travail effectif : 240 heures         │   │
│ └──────────────────────────────────────────────────────┘   │
│                                                              │
│ ┌──────────────────────────────────────────────────────┐   │
│ │ ☀️ HEURES SUPPLÉMENTAIRES - JOURS NORMAUX           │   │
│ ├──────────────────────────────────────────────────────┤   │
│ │  Jour (06h-22h)   [____0____] h  💰 Taux +15%/+35%  │   │
│ │  Nuit (22h-06h)   [____0____] h  💰 Taux +50%       │   │
│ └──────────────────────────────────────────────────────┘   │
│                                                              │
│ ┌──────────────────────────────────────────────────────┐   │
│ │ 🎉 HEURES SUPPLÉMENTAIRES - JOURS FÉRIÉS/DIMANCHES  │   │
│ ├──────────────────────────────────────────────────────┤   │
│ │  Jour (06h-22h)   [____0____] h  💰 Taux +60%       │   │
│ │  Nuit (22h-06h)   [____0____] h  💰 Taux +120%      │   │
│ └──────────────────────────────────────────────────────┘   │
│                                                              │
│ ┌──────────────────────────────────────────────────────┐   │
│ │ 💸 RETENUES ET DÉDUCTIONS                            │   │
│ ├──────────────────────────────────────────────────────┤   │
│ │  Remboursement dette : [____0____] FCFA              │   │
│ └──────────────────────────────────────────────────────┘   │
│                                                              │
│                   [← RETOUR]        [CALCULER 🧮]          │
└─────────────────────────────────────────────────────────────┘
```

**Améliorations** :
- GroupBox pour chaque catégorie avec icônes emoji
- Taux affichés à côté des champs (éducatif)
- Indicateur de durée en temps réel
- Labels plus explicites
- Bouton "Calculer" proéminent

---

### ONGLET 3 : RÉSULTATS (NOUVEAU !)

**Objectif** : Afficher clairement tous les résultats de calcul

**Contenu** :
```
┌─────────────────────────────────────────────────────────────┐
│ 📊 RÉSULTATS DU CALCUL                                       │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                 SALAIRE NET À PAYER                          │
│                                                              │
│                   250,000 FCFA                              │
│                                                              │
│                    (Deux cent cinquante mille francs CFA)   │
└─────────────────────────────────────────────────────────────┘

┌────────────────────────────┬────────────────────────────────┐
│ ✅ GAINS                    │ ❌ RETENUES                     │
├────────────────────────────┼────────────────────────────────┤
│                            │                                │
│ Salaire de base            │ CNSS Employé (3.6%)            │
│ 200,000 FCFA               │ -7,200 FCFA                    │
│                            │                                │
│ Heures supp. normales      │ IUTS (Impôt)                   │
│ 15,000 FCFA                │ -500 FCFA                      │
│                            │                                │
│ Heures supp. fériées       │ Avantages en nature            │
│ 8,000 FCFA                 │ -5,000 FCFA                    │
│                            │                                │
│ Prime d'ancienneté         │ Effort de paix (1%)            │
│ 10,000 FCFA                │ -300 FCFA                      │
│                            │                                │
│ Indemnités                 │ Dette                          │
│ 32,000 FCFA                │ -0 FCFA                        │
│ • Logement : 20,000        │                                │
│ • Transport : 10,000       │ TOTAL RETENUES                 │
│ • Fonction : 2,000         │ -13,000 FCFA                   │
│                            │                                │
│ TOTAL GAINS                │                                │
│ 265,000 FCFA               │                                │
│                            │                                │
├────────────────────────────┴────────────────────────────────┤
│ 📝 SALAIRE BRUT : 265,000 FCFA                              │
│ 💵 SALAIRE NET : 252,000 FCFA                               │
│ 💰 NET À PAYER : 250,000 FCFA (après effort de paix)        │
└─────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│ 🏢 CHARGES PATRONALES                                        │
├─────────────────────────────────────────────────────────────┤
│ CNSS Employeur (8.5%)     :  22,525 FCFA                    │
│ Risque professionnel (1.5%):  3,975 FCFA                    │
│ Prestation familiale (6%) :  15,900 FCFA                    │
│ TPA (1.5%)                :  3,975 FCFA                     │
│                                                              │
│ TOTAL CHARGES PATRONALES  :  46,375 FCFA                    │
└─────────────────────────────────────────────────────────────┘

           [← MODIFIER]  [IMPRIMER BULLETIN 🖨️]  [NOUVEAU]
```

**Améliorations** :
- **Net à payer** très visible en haut (grande police)
- Montant en lettres (aide juridique)
- Séparation claire gains/retenues (colonnes)
- Détail des indemnités
- Charges patronales visibles
- Couleurs : Vert pour gains, Rouge pour retenues
- Boutons d'action clairs

---

### ONGLET 4 : BULLETIN (PREVIEW)

**Objectif** : Prévisualiser avant impression, sauvegarder

**Contenu** :
```
┌─────────────────────────────────────────────────────────────┐
│ 🖨️ PRÉVISUALISATION ET IMPRESSION                          │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│ Employé :  Jean DUPONT (MAT001)                             │
│ Période :  Du 01/09/2025 au 30/09/2025                     │
│ Net à payer : 250,000 FCFA                                  │
│                                                              │
│ ┌──────────────────────────────────────────────────────┐   │
│ │                                                       │   │
│ │    [APERÇU DU BULLETIN EN PDF]                       │   │
│ │                                                       │   │
│ │    (Miniature ou PDF viewer intégré)                 │   │
│ │                                                       │   │
│ └──────────────────────────────────────────────────────┘   │
│                                                              │
│ Options d'impression :                                       │
│ ☑ Enregistrer une copie dans la base de données            │
│ ☑ Ouvrir le bulletin automatiquement                        │
│ ☐ Envoyer par email à l'employé                            │
│                                                              │
│ Emplacement : [C:\Documents\Bulletins\Sep2025\    [...]]   │
│                                                              │
│         [← RETOUR AUX RÉSULTATS]    [GÉNÉRER PDF 🖨️]      │
└─────────────────────────────────────────────────────────────┘
```

**Améliorations** :
- Récapitulatif avant impression
- Options configurables
- Choix du dossier de sauvegarde
- Possibilité d'annuler

---

## 🎨 Design System

### Couleurs

```csharp
// Palette principale
Color Primary = Color.FromArgb(52, 152, 219);    // Bleu principal
Color Secondary = Color.FromArgb(46, 204, 113);   // Vert succès
Color Danger = Color.FromArgb(231, 76, 60);      // Rouge erreur
Color Warning = Color.FromArgb(241, 196, 15);    // Jaune attention
Color Info = Color.FromArgb(52, 73, 94);         // Gris foncé

// Gains/Retenues
Color GainBackground = Color.FromArgb(230, 255, 230);    // Vert clair
Color DeductionBackground = Color.FromArgb(255, 230, 230); // Rouge clair

// Neutral
Color LightGray = Color.FromArgb(236, 240, 241);
Color MediumGray = Color.FromArgb(189, 195, 199);
Color DarkGray = Color.FromArgb(52, 73, 94);
```

### Typographie

```csharp
// Titres
Font TitleFont = new Font("Montserrat", 14F, FontStyle.Bold);
Font SubtitleFont = new Font("Montserrat", 12F, FontStyle.SemiBold);

// Labels
Font LabelFont = new Font("Montserrat", 10F, FontStyle.Regular);
Font LabelBold = new Font("Montserrat", 10F, FontStyle.Bold);

// Montants
Font AmountLarge = new Font("Montserrat", 24F, FontStyle.Bold);
Font AmountMedium = new Font("Montserrat", 14F, FontStyle.SemiBold);
Font AmountSmall = new Font("Montserrat", 11F, FontStyle.Regular);

// Inputs
Font InputFont = new Font("Montserrat", 10F, FontStyle.Regular);
```

### Espacements

```csharp
int PaddingSmall = 5;
int PaddingMedium = 10;
int PaddingLarge = 20;

int MarginSmall = 5;
int MarginMedium = 10;
int MarginLarge = 20;

int BorderRadius = 8; // Pour Guna controls
```

---

## 🔧 Améliorations Techniques

### 1. Validation en Temps Réel

```csharp
private void textBoxAbsences_TextChanged(object sender, EventArgs e)
{
    // Valide que c'est un nombre
    if (!decimal.TryParse(textBoxAbsences.Text, out decimal absences))
    {
        textBoxAbsences.BorderColor = Color.Red;
        errorProvider.SetError(textBoxAbsences, "Valeur numérique requise");
        return;
    }

    // Valide que absences <= heures contractuelles
    decimal heuresContrat = GetHeuresContrat();
    if (absences > heuresContrat)
    {
        textBoxAbsences.BorderColor = Color.Orange;
        errorProvider.SetError(textBoxAbsences,
            $"Les absences ne peuvent pas dépasser {heuresContrat}h");
        return;
    }

    // Valide que absences >= 0
    if (absences < 0)
    {
        textBoxAbsences.BorderColor = Color.Red;
        errorProvider.SetError(textBoxAbsences, "Valeur négative interdite");
        return;
    }

    // Tout est OK
    textBoxAbsences.BorderColor = Color.Green;
    errorProvider.Clear();

    // Met à jour l'indicateur d'heures effectives
    UpdateHeuresEffectives();
}

private void UpdateHeuresEffectives()
{
    decimal heuresContrat = GetHeuresContrat();
    decimal absences = ParseDecimalSafe(textBoxAbsences.Text);
    decimal effectives = heuresContrat - absences;

    labelHeuresEffectives.Text = $"✅ Heures de travail effectif : {effectives} heures";
    labelHeuresEffectives.ForeColor = effectives > 0 ? Color.Green : Color.Red;
}
```

### 2. Navigation Entre Onglets

```csharp
private void ConfigurerNavigationOnglets()
{
    // Désactive les onglets suivants tant que prérequis non remplis
    tabControlMain.TabPages[1].Enabled = false; // Période & Saisie
    tabControlMain.TabPages[2].Enabled = false; // Résultats
    tabControlMain.TabPages[3].Enabled = false; // Bulletin
}

private void ComboBoxEmploye_SelectedIndexChanged(object sender, EventArgs e)
{
    if (GetSelectedIntOrNull(ComboBoxEmploye) != null)
    {
        // Active l'onglet suivant
        tabControlMain.TabPages[1].Enabled = true;

        // Affiche bouton "Continuer"
        buttonContinuerEtape2.Visible = true;
    }
}

private void buttonContinuerEtape2_Click(object sender, EventArgs e)
{
    // Passe à l'onglet 2
    tabControlMain.SelectedIndex = 1;
}

private void buttonCalculer_Click(object sender, EventArgs e)
{
    // Effectue le calcul
    bool success = EffectuerCalcul();

    if (success)
    {
        // Affiche les résultats dans l'onglet 3
        AfficherResultats();

        // Active et passe à l'onglet résultats
        tabControlMain.TabPages[2].Enabled = true;
        tabControlMain.SelectedIndex = 2;
    }
}
```

### 3. Affichage des Résultats

```csharp
private void AfficherResultats()
{
    if (_lastSnapshot == null) return;

    var snap = _lastSnapshot;

    // Net à payer (grand et visible)
    labelNetAPayer.Text = $"{snap.SalaireNetaPayerFinal:N0} FCFA";
    labelNetAPayer.Font = new Font("Montserrat", 28F, FontStyle.Bold);
    labelNetAPayer.ForeColor = Color.FromArgb(46, 204, 113); // Vert

    // Montant en lettres
    labelNetEnLettres.Text = $"({ConvertirEnLettres(snap.SalaireNetaPayerFinal)})";

    // GAINS (colonne gauche)
    listViewGains.Items.Clear();
    AjouterLigneGain("Salaire de base", snap.SalaireDeBase);
    AjouterLigneGain("Heures supp. normales", snap.PrimeHeureSupp);
    AjouterLigneGain("Heures supp. fériées", 0); // Si applicable
    AjouterLigneGain("Prime d'ancienneté", snap.PrimeAnciennete);

    // Indemnités (avec détails)
    if (snap.SommeIndemnitesNumeraire > 0 || snap.SommeIndemnitesNature > 0)
    {
        decimal totalIndem = snap.SommeIndemnitesNumeraire + snap.SommeIndemnitesNature;
        var itemIndem = AjouterLigneGain("Indemnités", totalIndem);
        itemIndem.Font = new Font(itemIndem.Font, FontStyle.Bold);

        // Sous-items
        if (snap.LogementNumeraire > 0)
            AjouterLigneGain("  • Logement", snap.LogementNumeraire);
        if (snap.TransportNumeraire > 0)
            AjouterLigneGain("  • Transport", snap.TransportNumeraire);
        // ... autres indemnités
    }

    AjouterLigneGain("TOTAL GAINS", snap.SalaireBrut, isBold: true);

    // RETENUES (colonne droite)
    listViewRetenues.Items.Clear();
    AjouterLigneRetenue("CNSS Employé (3.6%)", -snap.CnssEmploye);
    AjouterLigneRetenue("IUTS (Impôt)", -snap.Iuts);
    if (snap.SommeIndemnitesNature > 0)
        AjouterLigneRetenue("Avantages en nature", -snap.SommeIndemnitesNature);
    if (snap.EffortPaix > 0)
        AjouterLigneRetenue("Effort de paix (1%)", -snap.EffortPaix);
    if (snap.ValeurDette > 0)
        AjouterLigneRetenue("Dette", -snap.ValeurDette);

    decimal totalRetenues = snap.CnssEmploye + snap.Iuts + snap.SommeIndemnitesNature
                          + snap.EffortPaix + snap.ValeurDette;
    AjouterLigneRetenue("TOTAL RETENUES", -totalRetenues, isBold: true);

    // Récapitulatif
    labelBrut.Text = $"📝 SALAIRE BRUT : {snap.SalaireBrut:N0} FCFA";
    labelNet.Text = $"💵 SALAIRE NET : {snap.SalaireNet:N0} FCFA";
    labelNetAPayer2.Text = $"💰 NET À PAYER : {snap.SalaireNetaPayerFinal:N0} FCFA";

    // Charges patronales
    AfficherChargesPatronales(snap);
}

private ListViewItem AjouterLigneGain(string libelle, decimal montant, bool isBold = false)
{
    var item = new ListViewItem(libelle);
    item.SubItems.Add($"{montant:N0} FCFA");
    item.ForeColor = Color.FromArgb(39, 174, 96); // Vert foncé
    if (isBold)
        item.Font = new Font(listViewGains.Font, FontStyle.Bold);

    listViewGains.Items.Add(item);
    return item;
}

private ListViewItem AjouterLigneRetenue(string libelle, decimal montant, bool isBold = false)
{
    var item = new ListViewItem(libelle);
    item.SubItems.Add($"{montant:N0} FCFA");
    item.ForeColor = Color.FromArgb(192, 57, 43); // Rouge foncé
    if (isBold)
        item.Font = new Font(listViewRetenues.Font, FontStyle.Bold);

    listViewRetenues.Items.Add(item);
    return item;
}
```

### 4. Conversion Montant en Lettres

```csharp
private string ConvertirEnLettres(decimal montant)
{
    // Utilise une bibliothèque existante ou implémente
    // Pour simplification, version basique :

    if (montant == 0) return "Zéro franc CFA";

    // Exemple : 250000 → "Deux cent cinquante mille francs CFA"
    // Implémentation complète nécessaire

    return $"{ConvertirNombreEnLettres((long)montant)} francs CFA";
}
```

---

## 📊 Checklist d'Implémentation

### Phase 1 : Structure de Base (2-3 heures)
- [ ] Créer TabControl principal avec 4 onglets
- [ ] Migrer contrôles existants vers bons onglets
- [ ] Configurer navigation entre onglets
- [ ] Désactiver onglets non accessibles

### Phase 2 : Onglet 1 - Employé (1 heure)
- [ ] Réorganiser recherche/sélection
- [ ] Créer GroupBox "Informations Employé"
- [ ] Améliorer mise en page verticale
- [ ] Ajouter bouton "Continuer"

### Phase 3 : Onglet 2 - Saisie (2 heures)
- [ ] Créer GroupBox pour chaque catégorie
- [ ] Ajouter icônes/emojis
- [ ] Afficher taux à côté des champs
- [ ] Ajouter indicateur durée/heures
- [ ] Renommer button "Calculer"

### Phase 4 : Onglet 3 - Résultats (3-4 heures) **PRIORITÉ**
- [ ] Créer panel Net à payer (grand)
- [ ] Ajouter conversion en lettres
- [ ] Créer ListView Gains/Retenues
- [ ] Afficher détails indemnités
- [ ] Afficher charges patronales
- [ ] Styler avec couleurs appropriées

### Phase 5 : Validation (2 heures)
- [ ] Ajouter ErrorProvider
- [ ] Validation temps réel absences
- [ ] Validation heures supplémentaires
- [ ] Validation dette (numérique, >= 0)
- [ ] Indicateurs visuels (couleurs bordures)

### Phase 6 : Optimisation Code (2 heures)
- [ ] Renommer buttonEffacer_Click → buttonCalculer_Click
- [ ] Extraire méthodes d'affichage
- [ ] Refactorer validation
- [ ] Ajouter commentaires XML

### Phase 7 : Tests (1 heure)
- [ ] Tester workflow complet
- [ ] Tester cas limites (absences > contrat)
- [ ] Tester navigation onglets
- [ ] Tester validation

---

## 🎁 Fonctionnalités Bonus (Optionnelles)

### Si temps disponible :

1. **Onglet 4 - Prévisualisation PDF**
   - Intégrer viewer PDF (AxAcroPDF ou autre)
   - Options d'export

2. **Historique des Calculs**
   - Conserver derniers calculs en session
   - Bouton "Recharger calcul précédent"

3. **Raccourcis Clavier**
   - F5 : Calculer
   - Ctrl+P : Imprimer
   - Ctrl+N : Nouveau

4. **Export Excel**
   - Exporter résultats vers Excel
   - Template formaté

5. **Aide Contextuelle**
   - Tooltips sur champs complexes
   - Bouton "?" avec explication calculs

---

## 📈 Résultat Attendu

**Avant** :
- Interface confuse, résultats invisibles
- Workflow non clair
- Validation inexistante
- Expérience frustrante

**Après** :
- Interface moderne par onglets
- Résultats clairement affichés
- Workflow guidé étape par étape
- Validation en temps réel
- Expérience fluide et professionnelle

**Temps estimé total** : 11-14 heures de développement

---

**Prêt à démarrer l'implémentation !** 🚀
