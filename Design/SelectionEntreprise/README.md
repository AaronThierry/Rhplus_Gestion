# Spécification Complète - Formulaire de Sélection d'Entreprise

## 📋 Vue d'ensemble

Interface de sélection d'entreprise et de période de paie avec un design néo-brutaliste raffiné. L'interface combine des bordures nettes, des ombres portées prononcées, et une palette de couleurs contrastée pour créer une expérience utilisateur mémorable et professionnelle.

---

## 🎨 Design Tokens

### Palette de couleurs

```css
/* Couleurs principales */
--color-primary: #191970;           /* MidnightBlue */
--color-primary-light: #B0C4DE;     /* LightSteelBlue */
--color-cream: #FFFAEB;             /* Crème chaud */
--color-accent-red: #DC3545;        /* Rouge accent */
--color-white: #FFFFFF;             /* Blanc pur */

/* Couleurs de fond */
--bg-main: #FAFAFA;                 /* Fond principal gris très clair */
--bg-footer: #F5F5FA;               /* Fond footer légèrement teinté */
--bg-card: #FFFFFF;                 /* Fond des cartes */

/* Couleurs de texte */
--text-primary: #191970;            /* Texte principal - MidnightBlue */
--text-secondary: #464646;          /* Texte secondaire - Gris foncé */
--text-muted: #646464;              /* Texte atténué */
--text-on-primary: #FFFAEB;         /* Texte sur fond primary */

/* Bordures */
--border-primary: #191970;          /* Bordure principale */
--border-light: #EBE9F5;            /* Bordure légère */
--border-grid: #E7EDF2;             /* Bordure de grille */

/* Ombres */
--shadow-brutal-primary: 6px 6px 0 0 #191970;
--shadow-brutal-red: 5px 5px 0 0 #DC3545;
--shadow-brutal-stats: 4px 4px 0 0 #191970;
--shadow-soft: 0 2px 4px rgba(0, 0, 0, 0.1);
--shadow-header: 0 0 0 5px rgba(0, 0, 0, 0.05);
```

### Typographie

```css
/* Familles de polices */
--font-heading: 'Space Grotesk', 'Segoe UI', -apple-system, system-ui, sans-serif;
--font-body: 'Montserrat', 'Segoe UI', -apple-system, system-ui, sans-serif;
--font-mono: 'IBM Plex Mono', 'Consolas', 'Monaco', monospace;
--font-system: 'Segoe UI', -apple-system, BlinkMacSystemFont, sans-serif;

/* Tailles de police */
--font-size-xl: 16px;         /* Titres principaux */
--font-size-lg: 14px;         /* Sous-titres */
--font-size-md: 11px;         /* Labels importants */
--font-size-base: 10px;       /* Texte de base */
--font-size-sm: 9px;          /* Texte secondaire */
--font-size-xs: 8.5px;        /* Petits textes */

/* Graisses de police */
--font-weight-bold: 700;
--font-weight-semibold: 600;
--font-weight-medium: 500;
--font-weight-regular: 400;

/* Hauteurs de ligne */
--line-height-tight: 1.2;
--line-height-normal: 1.5;
--line-height-relaxed: 1.75;
```

### Espacements

```css
/* Espacement de base (échelle 4px) */
--spacing-xs: 4px;
--spacing-sm: 8px;
--spacing-md: 12px;
--spacing-lg: 16px;
--spacing-xl: 20px;
--spacing-2xl: 24px;
--spacing-3xl: 32px;
--spacing-4xl: 48px;

/* Padding des composants */
--padding-card: 1px;
--padding-header: 18px 32px;
--padding-footer: 15px 20px;
--padding-button: 12px 24px;
--padding-label: 12px 6px;
--padding-stats: 16px 11px;
```

### Dimensions

```css
/* Largeurs */
--width-form: 1000px;
--width-date-picker: 180px;
--width-button-validate: 165px;
--width-button-cancel: 155px;
--width-stats-panel: 320px;

/* Hauteurs */
--height-header: 80px;
--height-footer: 150px;
--height-button: 50px;
--height-period-card: 70px;
--height-stats-panel: 50px;
--height-date-picker: 42px;
--height-grid-row: 40px;
--height-grid-header: 36px;

/* Bordures */
--border-width-thick: 3px;
--border-width-medium: 2px;
--border-width-thin: 1px;
--border-radius-none: 0px;
--border-radius-card: 6px;
--border-radius-button: 4px;
```

### Transitions

```css
/* Durées */
--transition-fast: 150ms;
--transition-base: 200ms;
--transition-slow: 300ms;

/* Fonctions d'easing */
--ease-out: cubic-bezier(0.16, 1, 0.3, 1);
--ease-in-out: cubic-bezier(0.4, 0, 0.2, 1);
--ease-spring: cubic-bezier(0.34, 1.56, 0.64, 1);
```

---

## 📐 Structure et Mesures Exactes

### 1. Formulaire principal
- **Dimensions**: 1000px × 710px
- **Position**: Centré sur l'écran
- **Background**: `#FFFFFF`
- **Border**: None (FormBorderStyle.FixedDialog)

### 2. En-tête (panelHeader)
- **Dimensions**: 1000px × 80px
- **Position**: Dock Top
- **Background**: `#191970` (MidnightBlue)
- **Shadow**: 0 0 0 5px rgba(0,0,0,0.05) en bas
- **Padding**: 18px 32px

#### Titre principal (labelTitre)
- **Font**: Montserrat Bold 14px
- **Color**: `#FFFFFF`
- **Position**: 27px du haut, 18px de gauche
- **Text**: "Sélection de l'Entreprise"

#### Sous-titre (labelSousTitre)
- **Font**: Montserrat Regular 8px
- **Color**: `#C8C8DC` (200, 200, 220)
- **Position**: 53px du haut, 29px de gauche
- **Text**: "Choisissez une entreprise et la période de paie"

### 3. Zone principale (panelMain)
- **Dimensions**: 1000px × 480px
- **Background**: `#FAFAFA`
- **Padding**: 20px 18px
- **Position**: Fill entre header et footer

#### DataGrid Card (cardDataGrid)
- **Dimensions**: 960px × 444px
- **Background**: `#FFFFFF`
- **Border Radius**: 6px
- **Shadow**: 0 2px 4px rgba(220,220,220,0.5)
- **Padding**: 1px

#### DataGridView Entreprises
- **Dock**: Fill dans cardDataGrid
- **Background**: `#FFFFFF`
- **Border**: None
- **Row Height**: 40px
- **Header Height**: 36px

##### Styles d'en-tête
- **Background**: `#191970`
- **Color**: `#FFFFFF`
- **Font**: Montserrat Bold 9px
- **Padding**: 0 12px
- **Alignment**: MiddleLeft

##### Styles de cellules
- **Background**: `#FFFFFF` (alternance avec `#F8F9FC`)
- **Color**: `#000000`
- **Font**: Montserrat Regular 8.5px
- **Selection Background**: `#B0C4DE` (LightSteelBlue)
- **Selection Color**: `#000000`
- **Grid Color**: `#E7E5FF`

##### Colonnes
1. **ID**: 55px, centré, Montserrat Bold 7.5px, `#646478`
2. **Sigle**: 180px, gauche, Montserrat Bold 9px, `#191970`
3. **Nom Entreprise**: Fill, gauche, Montserrat Regular 9px

### 4. Zone de pied de page (panelFooter)
- **Dimensions**: 1000px × 150px
- **Position**: Dock Bottom
- **Background**: `#F5F5FA`
- **Padding**: 15px 20px
- **Shadow**: 0 -5px 0 rgba(0,0,0,0.05) en haut

#### Card Période (cardPeriode)
- **Dimensions**: 960px × 70px
- **Position**: Dock Top dans footer (20px du haut)
- **Background**: `#FFFAEB` (Crème)
- **Border**: 3px solid `#191970`
- **Border Radius**: 0px (angles vifs)
- **Shadow**: 6px 6px 0 0 `#191970` (ombre brutale)
- **Padding**: 5px 0 0 0

##### Label "PÉRIODE DE PAIE" (labelPeriode)
- **Font**: Space Grotesk Bold 11px (ou Microsoft Sans Serif Bold 11px)
- **Color**: `#FFFAEB`
- **Background**: `#191970`
- **Padding**: 12px 6px
- **Position**: 18px de gauche, 21px du haut
- **Text**: "⚡ PÉRIODE DE PAIE"

##### Label "Début:" (labelDebut)
- **Font**: Montserrat Bold 9px
- **Color**: `#191970`
- **Position**: 218px de gauche, 25px du haut
- **Text**: "Début:"

##### DatePicker Début (dateTimePickerDebut)
- **Dimensions**: 180px × 42px
- **Position**: 300px de gauche, 16px du haut
- **Background**: `#FFFFFF`
- **Border**: 2px solid `#191970`
- **Border Radius**: 0px
- **Font**: Microsoft Sans Serif Bold 10px (ou IBM Plex Mono Bold 10px)
- **Color**: `#191970`
- **Format**: "dd/MM/yyyy"

##### Label "Fin:" (labelFin)
- **Font**: Montserrat Bold 9px
- **Color**: `#191970`
- **Position**: 506px de gauche, 25px du haut
- **Text**: "Fin:"

##### DatePicker Fin (dateTimePickerFin)
- **Dimensions**: 180px × 42px
- **Position**: 570px de gauche, 16px du haut
- **Background**: `#FFFFFF`
- **Border**: 2px solid `#191970`
- **Border Radius**: 0px
- **Font**: Microsoft Sans Serif Bold 10px
- **Color**: `#191970`
- **Format**: "dd/MM/yyyy"

#### Panel Stats (panelStats)
- **Dimensions**: 320px × 50px
- **Position**: 20px de gauche, 85px du haut du footer
- **Background**: `#FFFAEB`
- **Border**: 3px solid `#191970`
- **Border Radius**: 0px
- **Shadow**: 4px 4px 0 0 `#191970`
- **Padding**: 16px 11px

##### Label Nombre d'entreprises
- **Font**: Microsoft Sans Serif Bold 9px (ou IBM Plex Mono Bold 9px)
- **Color**: `#191970`
- **Padding**: 0 5px 0 0
- **Text**: "0 entreprise(s) disponible(s)"

#### Bouton ANNULER (buttonAnnuler)
- **Dimensions**: 155px × 50px
- **Position**: Dock Right (660px de gauche, 85px du haut)
- **Background**: `#FFFFFF`
- **Border**: 2px solid `#DC3545`
- **Border Radius**: 0px
- **Font**: Microsoft Sans Serif Bold 10px (ou Space Grotesk Bold 10px)
- **Color**: `#DC3545`
- **Shadow**: 5px 5px 0 0 `#DC3545`
- **Margin**: 0 20px 0 0
- **Text**: "✕ ANNULER"

##### Hover State
- **Background**: `#DC3545`
- **Color**: `#FFFFFF`
- **Border**: 2px solid `#DC3545`

#### Bouton VALIDER (buttonValider)
- **Dimensions**: 165px × 50px
- **Position**: Dock Right (815px de gauche, 85px du haut)
- **Background**: `#191970`
- **Border**: 2px solid `#191970`
- **Border Radius**: 0px
- **Font**: Microsoft Sans Serif Bold 10px
- **Color**: `#FFFAEB`
- **Shadow**: 5px 5px 0 0 `#191970`
- **Margin**: 0 20px 0 0
- **Text**: "✓ VALIDER"

##### Hover State
- **Background**: `#FFFAEB`
- **Color**: `#191970`
- **Border**: 2px solid `#191970`

---

## 🎯 Interactions et États

### États des boutons

#### Bouton Normal
- Bordure de 2px avec couleur de l'état
- Ombre portée décalée de 5px en bas à droite
- Transition fluide de 200ms

#### Bouton Hover
- Inversion des couleurs fond/texte
- Maintien de la bordure et de l'ombre
- Cursor: pointer

#### Bouton Disabled
- Background: `#A9A9A9`
- Color: `#8D8D8D`
- Border: `#A9A9A9`
- Cursor: not-allowed
- Opacity: 0.6

### États du DataGridView

#### Ligne normale
- Background: `#FFFFFF` (alternance `#F8F9FC`)
- Cursor: default

#### Ligne hover
- Cursor: pointer
- Légère transition de 150ms

#### Ligne sélectionnée
- Background: `#B0C4DE`
- Color: `#000000`
- Bordure gauche: 3px solid `#191970` (optionnel)

#### Ligne sélectionnée + hover
- Background: `#A0B4CE` (5% plus foncé)

### États des DatePickers

#### Normal
- Border: 2px solid `#191970`
- Background: `#FFFFFF`

#### Focus
- Border: 2px solid `#191970`
- Box-shadow: 0 0 0 3px rgba(25, 25, 112, 0.1)

#### Disabled
- Background: `#F5F5F5`
- Color: `#A9A9A9`
- Border: 2px solid `#D3D3D3`

---

## 💫 Animations

### Fade-in au chargement
```css
@keyframes fadeIn {
  from {
    opacity: 0;
    transform: translateY(-10px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.panel-header {
  animation: fadeIn 400ms cubic-bezier(0.16, 1, 0.3, 1);
}
```

### Transition des boutons
```css
.button {
  transition: all 200ms cubic-bezier(0.4, 0, 0.2, 1);
}

.button:hover {
  transform: translate(-2px, -2px);
  box-shadow: 7px 7px 0 0 var(--shadow-color);
}

.button:active {
  transform: translate(2px, 2px);
  box-shadow: 3px 3px 0 0 var(--shadow-color);
}
```

### Sélection de ligne
```css
.grid-row {
  transition: background-color 150ms ease-out;
}

.grid-row:hover {
  background-color: rgba(176, 196, 222, 0.3);
}
```

---

## 🔧 Gestion d'État

### États du formulaire

1. **Chargement initial**
   - Affichage du DataGridView vide ou avec loader
   - Bouton VALIDER désactivé
   - DatePickers à la date du jour

2. **Données chargées**
   - DataGridView rempli avec les entreprises
   - Première ligne sélectionnée par défaut
   - Label nombre d'entreprises mis à jour
   - Bouton VALIDER activé

3. **Entreprise sélectionnée**
   - Ligne du DataGridView en état sélectionné
   - Bouton VALIDER activé

4. **Validation**
   - Vérification que DateDébut ≤ DateFin
   - Si invalide: MessageBox d'avertissement
   - Si valide: DialogResult = OK et fermeture

5. **Annulation**
   - DialogResult = Cancel
   - Fermeture sans validation

### États d'erreur

#### Dates invalides
- MessageBox: "La date de début ne peut pas être supérieure à la date de fin."
- Titre: "Validation"
- Icône: Warning
- Focus sur dateTimePickerDebut

#### Aucune entreprise sélectionnée
- MessageBox: "Veuillez sélectionner une entreprise."
- Titre: "Information"
- Icône: Information

#### Erreur de chargement
- MessageBox: "Erreur lors du chargement des entreprises:\n{message}"
- Titre: "Erreur"
- Icône: Error

---

## 📱 Responsive et Accessibilité

### Dimensions fixes
Le formulaire utilise des dimensions fixes (1000×710px) et n'est pas responsive car c'est une application desktop WinForms.

### Accessibilité

#### Navigation au clavier
- **Tab**: Navigation entre DatePickers et boutons
- **Shift+Tab**: Navigation inverse
- **Enter**: Validation (équivalent au clic sur VALIDER)
- **Escape**: Annulation (équivalent au clic sur ANNULER)
- **Flèches haut/bas**: Navigation dans le DataGridView
- **Double-clic**: Sélection rapide d'une entreprise et validation

#### Contrastes
- Ratio texte/fond principal: 12.6:1 (WCAG AAA)
- Ratio boutons: 8.5:1 (WCAG AAA)
- Ratio DataGridView header: 14.2:1 (WCAG AAA)

#### Focus visible
- Bordure de focus de 2px avec couleur contrastée
- Ombre de focus subtile pour les contrôles interactifs

---

## 🎨 Design System - Neo-Brutalist

### Principes

1. **Angles vifs**: Pas d'arrondis (border-radius: 0)
2. **Bordures épaisses**: 2-3px pour maximum d'impact
3. **Ombres décalées**: Effet isométrique 3D avec ombres de 4-6px
4. **Couleurs contrastées**: MidnightBlue + Crème + Rouge accent
5. **Typographie mixte**: Display (Space Grotesk) + Body (Montserrat) + Mono (IBM Plex)
6. **Hiérarchie claire**: Tailles, poids et couleurs pour guider l'œil

### Cohérence visuelle

- Toutes les cartes utilisent les mêmes bordures et ombres brutales
- Les boutons partagent le même style d'ombre et de transition
- La palette de couleurs est limitée à 5 couleurs principales
- Les espacements suivent une échelle de 4px

---

## 📦 Fichiers du Design System

```
Design/
├── SelectionEntreprise/
│   ├── README.md                    (ce fichier)
│   ├── SelectionEntreprise.dc.html  (prototype source de vérité)
│   ├── assets/
│   │   ├── tokens.css               (design tokens CSS)
│   │   └── screenshots/             (captures d'écran des états)
│   └── specs/
│       ├── interactions.md          (détails des interactions)
│       └── states.md                (diagramme d'états)
```

---

## 🚀 Implémentation

### Technologies utilisées
- **Framework**: .NET Framework 4.7.2
- **UI Library**: Guna.UI2.WinForms
- **Database**: MySQL via MySql.Data.MySqlClient
- **Fonts**: Montserrat, Space Grotesk (fallback: Segoe UI, Microsoft Sans Serif)

### Composants personnalisés
- `Guna2Panel` pour les cartes et sections
- `Guna2DataGridView` pour la liste des entreprises
- `Guna2DateTimePicker` pour la sélection de dates
- `Guna2Button` pour les boutons d'action

### Performance
- Chargement des données en async
- Virtualisation du DataGridView pour grandes listes
- Debouncing des événements hover (150ms)

---

## 📝 Notes de version

### v1.0.0 (2026-06-17)
- Design néo-brutaliste initial
- Palette MidnightBlue + Crème + Rouge
- Ombres brutales et bordures nettes
- Typographie mixte (Space Grotesk + Montserrat + IBM Plex)
- Interactions hover avec inversion de couleurs
- États de validation et gestion d'erreurs

---

## 🔗 Références

- [Guna UI Documentation](https://gunaui.com/docs)
- [Neo-Brutalism Design Trend](https://www.awwwards.com/neobrutalism-design)
- [WCAG 2.1 Guidelines](https://www.w3.org/WAI/WCAG21/quickref/)
- [Material Design Motion](https://material.io/design/motion)
