# Handoff : Boîte de dialogue « Sélection de l'entreprise »

## Vue d'ensemble
Boîte de dialogue modale d'une application de paie (desktop). Elle permet à
l'utilisateur de **sélectionner une entreprise** dans une liste recherchable et
de **définir une période de paie** (date de début / date de fin) avant de valider.
Il s'agit du redesign moderne d'un écran existant.

## À propos des fichiers de design
Le fichier `Selection Entreprise.dc.html` de ce dossier est une **référence de
design réalisée en HTML** — un prototype illustrant l'apparence et le comportement
attendus, **pas du code de production à copier tel quel**.

La tâche consiste à **recréer ce design dans l'application C# cible** en utilisant
ses patterns établis. Le HTML sert de spécification visuelle et comportementale,
pas de livrable. Voir la section **« Implémentation C# »** ci-dessous pour le
mapping concret WPF (recommandé) / WinForms.

## Fidélité
**Haute fidélité (hifi).** Couleurs, typographie, espacements et interactions sont
finaux. Reproduire l'UI au pixel près en utilisant les composants/librairies du
codebase cible.

---

## Écran : Sélection de l'entreprise

**Objectif** : choisir une entreprise + définir la période de paie, puis valider.

### Structure générale
Carte modale centrée, largeur **960 px**, fond `#ffffff`, coins arrondis **16 px**,
ombre portée `0 24px 64px -12px rgba(20,28,70,0.35)`. Empilement vertical (flex
column) de 6 blocs :

1. Barre de titre
2. En-tête (header bleu)
3. Barre d'outils (recherche + compteur)
4. Tableau des entreprises
5. Pied : période de paie + boutons d'action
6. Résumé de la sélection

### 1. Barre de titre
- Hauteur **44 px**, fond `#f4f6fb`, bordure basse `1px #e6e9f3`.
- Gauche : pastille ronde **8 px** `#2e9e5b` + libellé « Sélection d'entreprise »
  (13 px, 600, `#475073`).
- Droite : bouton fermeture (croix) 28×28 px, coins 7 px, couleur `#8a93b2`.
  Hover : fond `#fde7e7`, couleur `#d4453f`.

### 2. En-tête (header)
- Padding `26px 32px 24px`. Fond dégradé `linear-gradient(120deg, #20296e 0%, #2c3892 100%)`.
- Cercles décoratifs translucides (blanc 5 % et vert `rgba(46,158,91,0.16)`) en overflow hidden.
- Icône « immeuble » dans un carré 48×48 px, coins 12 px, fond `rgba(255,255,255,0.12)`, trait blanc.
- Titre `h1` : « Sélection de l'entreprise » — **25 px / 800**, blanc, letter-spacing `-0.3px`.
- Sous-titre : « Choisissez une entreprise et définissez la période de paie » — 14 px / 500, `#b9c0e8`.

### 3. Barre d'outils
- Padding `18px 32px 14px`, flex, gap 14 px.
- **Champ de recherche** (flex:1) : hauteur 44 px, bordure `1.5px #e2e6f1`, coins 11 px,
  fond `#fbfcfe`, icône loupe `#9aa3c2` à gauche (padding-left 40 px), texte 14.5 px `#2a3157`.
  Placeholder : « Rechercher par sigle, nom ou ID… ».
  Focus : bordure `#4a58c4`, fond `#fff`, halo `0 0 0 3.5px rgba(74,88,196,0.12)`.
- **Compteur** : pilule fond `#eef0f9`, coins 11 px, hauteur 44 px. Nombre **19 px / 800**
  `#2c3892` + libellé « entreprise(s) disponible(s) » 12.5 px / 600 `#6b73a0`. Le nombre
  reflète le résultat **filtré**.

### 4. Tableau
- Conteneur : marge latérale 32 px, bordure `1px #e6e9f3`, coins 13 px, overflow hidden.
- **En-tête de colonnes** : grille `92px 240px 1fr`, fond `#20296e`, padding `0 20px`.
  Libellés 11.5 px / 700, `#a9b1e0`, MAJUSCULES, letter-spacing `0.7px`, padding vertical 13 px.
  Colonnes : « ID », « Sigle », « Nom de l'entreprise ».
- **Corps** : hauteur fixe **268 px**, `overflow-y: auto`, fond `#fff`.
- **Ligne** : même grille `92px 240px 1fr`, hauteur **54 px**, padding `0 20px`,
  bordure basse `1px #f0f2f8`, `cursor: pointer`, bordure gauche 3 px transparente.
  - ID : badge inline (min-width 30 px, hauteur 24 px, coins 7 px). Inactif : fond `#eef0f9`, texte `#5a6285`. Actif : fond `#2e9e5b`, texte blanc.
  - Sigle : 14 px / 700. Inactif `#2a3157`, actif `#1c6e41`.
  - Nom : 13.5 px / 500. Inactif `#828ab0`, actif `#3a6b51`. **Troncature** `ellipsis` (nowrap, padding-right 12 px).
  - **Ligne active** : bordure gauche 3 px `#2e9e5b`, fond `#eef6f1`.
- **État vide** (aucun résultat) : icône loupe + texte « Aucune entreprise trouvée » centré, `#9aa3c2`.

### 5. Pied — Période de paie + actions
- Conteneur : marge `18px 32px 0`, padding `18px 20px`, fond `#f7f8fc`, bordure `1px #eaedf6`, coins 13 px, flex wrap, gap 18 px.
- **Libellé période** : carré icône calendrier 32×32 px, coins 9 px, fond `#e7ebfa`, trait `#3d49a8` + texte « Période de paie » 13.5 px / 700 `#3a4271`.
- **Début / Fin** : `label` 13 px / 600 `#6b73a0` + `<input type="date">` hauteur 40 px,
  bordure `1.5px #e2e6f1`, coins 9 px, texte 13.5 px `#2a3157`. Focus : bordure `#4a58c4`, halo `rgba(74,88,196,0.12)`.
- Spacer flex pousse les boutons à droite.
- **Bouton Annuler** : hauteur 42 px, padding `0 22px`, coins 10 px, bordure `1.5px #d8dcea`,
  fond `#fff`, texte 14 px / 600 `#5a6285`. Hover : fond `#f0f1f7`, bordure `#c4cae0`.
- **Bouton Valider** : hauteur 42 px, padding `0 24px`, coins 10 px, icône check + texte 14 px / 700.
  - Actif : fond `#2e9e5b`, texte blanc, ombre `0 4px 12px -2px rgba(46,158,91,0.45)`, `cursor: pointer`.
  - Désactivé (aucune sélection) : fond `#b7d7c4`, `cursor: not-allowed`.

### 6. Résumé de la sélection
- Padding `14px 32px 24px`. Visible seulement si une entreprise est sélectionnée.
- Texte 13 px : « Sélection : » (`#6b73a0`, 600) + libellé en 700 `#2c3892`,
  format `{SIGLE} — période du {jj/mm/aaaa} au {jj/mm/aaaa}`.

---

## Interactions & comportement
- **Recherche** : filtrage en direct (insensible à la casse) sur `sigle`, `name`, `id`. Met à jour le tableau ET le compteur.
- **Sélection de ligne** : clic sur une ligne → devient active (état visuel ci-dessus). Une seule ligne active à la fois.
- **Dates** : `type="date"`, indépendantes. Aucune validation croisée imposée (à ajouter côté métier si besoin : fin ≥ début).
- **Valider** : désactivé tant qu'aucune entreprise sélectionnée n'existe dans la liste. Au clic → renvoyer `{ entreprise, dateDebut, dateFin }` à l'appelant et fermer la modale.
- **Annuler / Fermer (croix)** : ferme la modale sans rien renvoyer.
- **État vide** : si le filtre ne renvoie aucun résultat, afficher le bloc vide à la place des lignes.
- Transitions : hover/focus en `0.12–0.15s`.

## Gestion de l'état
| État | Type | Rôle |
|---|---|---|
| `query` | string | Texte de recherche |
| `selectedId` | number \| null | ID de l'entreprise sélectionnée |
| `startDate` | date (ISO `yyyy-mm-dd`) | Début de période |
| `endDate` | date (ISO `yyyy-mm-dd`) | Fin de période |
| `data` | Entreprise[] | Liste source (à charger depuis l'API/DB) |

`Entreprise = { id: number, sigle: string, name: string }`.
La liste filtrée est dérivée de `data` + `query`. `Valider` est désactivé si aucune entreprise valide n'est sélectionnée.

## Design tokens
**Couleurs**
- Bleu marine foncé : `#20296e` / `#2c3892` (header, en-tête tableau)
- Bleu accent / focus : `#4a58c4` · texte fort `#2c3892` · `#3d49a8`
- Vert validation : `#2e9e5b` (actif) · `#b7d7c4` (désactivé) · `#1c6e41` (texte sigle actif)
- Texte : `#2a3157` (principal) · `#5a6285` / `#6b73a0` / `#828ab0` (secondaires) · `#475073`
- Fonds : `#ffffff` · `#fbfcfe` · `#f4f6fb` · `#f7f8fc` · `#eef0f9` · `#e7ebfa` · `#e9ecf4` (overlay)
- Bordures : `#e6e9f3` · `#e2e6f1` · `#eaedf6` · `#f0f2f8` · `#d8dcea`
- Rouge fermeture (hover) : fond `#fde7e7`, icône `#d4453f`

**Rayons** : 7 / 9 / 10 / 11 / 12 / 13 / 16 px
**Ombres** : carte `0 24px 64px -12px rgba(20,28,70,0.35)` · bouton vert `0 4px 12px -2px rgba(46,158,91,0.45)` · halo focus `0 0 0 3px rgba(74,88,196,0.12)`
**Typo** : famille **Figtree** (Google Fonts) — remplaçable par la police système du codebase (ex. Segoe UI pour WPF). Graisses 400/500/600/700/800. Tailles clés : 25 / 19 / 14.5 / 14 / 13.5 / 13 / 12.5 / 11.5 px.

## Assets
Aucune image. Toutes les icônes sont des **SVG inline** (immeuble, loupe, calendrier, check, croix) — à remplacer par la bibliothèque d'icônes du codebase cible (ex. Lucide, Fluent, Material) en conservant trait ~1.7–2.2 px et tailles indiquées.

## Fichiers
- `Selection Entreprise.dc.html` — prototype HTML haute-fidélité, source de vérité visuelle et comportementale. Le markup et la logique (filtrage, sélection, formatage de date) y sont lisibles directement.

---

## Implémentation C# — WinForms

> Cible : **C# WinForms**. La modale = une `Form` (`FormBorderStyle = None`).
> WinForms ne gère pas nativement coins arrondis / dégradés / ombres : on les
> obtient par dessin GDI+ (`Paint`) et `Region`. Les sections ci-dessous donnent
> le mapping et les points délicats.

### Mapping des composants

| Élément du design | Équivalent WinForms |
|---|---|
| Carte modale | `Form` `FormBorderStyle=None`, `StartPosition=CenterParent`, ouverte via `ShowDialog()`. Coins arrondis : `this.Region = GraphicsPath` arrondi (rayon 16). Ombre : voir note. |
| Barre de titre custom | `Panel` haut `#F4F6FB` + `Label`. Déplacement fenêtre : sur `MouseDown` appeler l'astuce `ReleaseCapture()` + `SendMessage(Handle, 0xA1, 0x2, 0)` (P/Invoke). |
| Bouton fermeture | `Label`/`PictureBox` 28×28 ; `MouseEnter/Leave` changent `BackColor`/`ForeColor`. |
| Header dégradé | `Panel` avec `Paint` custom : `LinearGradientBrush` (`#20296E` → `#2C3892`, angle ~30°). Cercles déco : `e.Graphics.FillEllipse` semi-transparents. |
| Titre / sous-titre | `Label` (`AutoSize`, polices ci-dessous). |
| Barre de recherche | `Panel` arrondi contenant une `PictureBox` (loupe) + `TextBox` `BorderStyle=None`. Événement `TextChanged` → re-filtrer. |
| Compteur | `Panel` arrondi `#EEF0F9` + 2 `Label`. |
| **Tableau** | `DataGridView` stylé (recommandé) — voir détail ci-dessous. |
| Badge ID | Dessiné en `CellPainting`, ou colonne avec `DataGridViewTextBoxCell` + fond arrondi peint. |
| Sélecteurs de date | `DateTimePicker` (`Format=Custom`, `CustomFormat="dd/MM/yyyy"`). |
| Boutons Annuler / Valider | `Button` `FlatStyle=Flat`, `FlatAppearance.BorderSize` réglé, coins arrondis via `Region`, `MouseEnter/Leave` pour le hover. |
| Résumé sélection | `Label` mis à jour à chaque changement. |

### Modèle & état
```csharp
public class Entreprise {
    public int Id { get; set; }
    public string Sigle { get; set; }
    public string Nom { get; set; }
}
```
Champs du formulaire :
```csharp
private List<Entreprise> _data;            // source (BDD/API)
private BindingList<Entreprise> _affichees; // liste filtrée bindée au grid
private Entreprise _selection;             // ligne courante

// Résultats exposés à l'appelant après Valider :
public Entreprise EntrepriseChoisie { get; private set; }
public DateTime DateDebut { get; private set; }
public DateTime DateFin   { get; private set; }
```

### Filtrage (recherche en direct)
```csharp
private void txtRecherche_TextChanged(object sender, EventArgs e) {
    var q = txtRecherche.Text.Trim().ToLowerInvariant();
    var filtre = string.IsNullOrEmpty(q)
        ? _data
        : _data.Where(x =>
              x.Sigle.ToLowerInvariant().Contains(q) ||
              x.Nom.ToLowerInvariant().Contains(q)   ||
              x.Id.ToString().Contains(q)).ToList();

    _affichees = new BindingList<Entreprise>(filtre);
    dgvEntreprises.DataSource = _affichees;

    lblCompteur.Text = filtre.Count.ToString();
    panelVide.Visible = filtre.Count == 0;   // état "Aucune entreprise trouvée"
}
```

### Sélection + activation du bouton Valider
```csharp
private void dgvEntreprises_SelectionChanged(object sender, EventArgs e) {
    _selection = dgvEntreprises.CurrentRow?.DataBoundItem as Entreprise;
    btnValider.Enabled = _selection != null;
    btnValider.BackColor = _selection != null
        ? ColorTranslator.FromHtml("#2E9E5B")   // actif
        : ColorTranslator.FromHtml("#B7D7C4");   // désactivé
    MajResume();
}

private void MajResume() {
    lblResume.Visible = _selection != null;
    if (_selection != null)
        lblResume.Text = $"Sélection : {_selection.Sigle} — période du " +
                         $"{dtpDebut.Value:dd/MM/yyyy} au {dtpFin.Value:dd/MM/yyyy}";
}
```

### Valider / Annuler
```csharp
private void btnValider_Click(object sender, EventArgs e) {
    if (_selection == null) return;
    EntrepriseChoisie = _selection;
    DateDebut = dtpDebut.Value.Date;
    DateFin   = dtpFin.Value.Date;
    DialogResult = DialogResult.OK;    // ferme la modale
}
private void btnAnnuler_Click(object sender, EventArgs e) {
    DialogResult = DialogResult.Cancel;
}
```
Appel depuis le parent :
```csharp
using (var dlg = new FrmSelectionEntreprise(listeEntreprises)) {
    if (dlg.ShowDialog(this) == DialogResult.OK) {
        var ent = dlg.EntrepriseChoisie;
        var d1 = dlg.DateDebut; var d2 = dlg.DateFin;
        // ... charger la paie
    }
}
```

### Style du DataGridView (pour coller au design)
```csharp
dgv.AutoGenerateColumns = false;
dgv.RowHeadersVisible = false;
dgv.AllowUserToAddRows = false;
dgv.AllowUserToResizeRows = false;
dgv.ReadOnly = true;
dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
dgv.MultiSelect = false;
dgv.EnableHeadersVisualStyles = false;          // indispensable pour recolorer l'en-tête
dgv.BorderStyle = BorderStyle.None;
dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
dgv.GridColor = ColorTranslator.FromHtml("#F0F2F8");
dgv.BackgroundColor = Color.White;
dgv.RowTemplate.Height = 54;

// En-tête bleu marine
dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#20296E");
dgv.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#A9B1E0");
dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
dgv.ColumnHeadersHeight = 40;

// Cellules
dgv.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#EEF6F1");
dgv.DefaultCellStyle.SelectionForeColor = ColorTranslator.FromHtml("#1C6E41");
dgv.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#2A3157");

// Colonnes : ID (92), Sigle (240), Nom (Fill)
```
- **Badge ID + barre verte de ligne active** : gérer dans l'événement `CellPainting`
  (peindre un rectangle arrondi derrière l'ID, et une barre verte 3 px à gauche de
  la ligne sélectionnée).
- **État vide** : un `Panel` (`panelVide`) avec icône loupe + label « Aucune
  entreprise trouvée », affiché par-dessus le grid quand le filtre est vide.

### Couleurs (helper)
Utiliser `ColorTranslator.FromHtml("#......")` avec les hex de la section
**Design tokens**. Définir des constantes statiques pour éviter les répétitions.

### Coins arrondis, dégradés, ombres (GDI+)
- **Coins arrondis** : créer un `GraphicsPath` avec arcs aux 4 coins, l'affecter à
  `control.Region`. Réutilisable pour la `Form`, les panels recherche/compteur et les boutons.
- **Dégradé header** : dans `Paint`, `new LinearGradientBrush(rect, c1, c2, 30f)`.
- **Ombre de la carte** : WinForms n'a pas de box-shadow. Options : (a) `Form`
  parente semi-transparente derrière, ou (b) dessiner plusieurs rectangles arrondis
  de plus en plus transparents, ou (c) accepter une bordure fine `#E6E9F3` sans ombre
  (acceptable si la modale est sur un overlay sombre). Ne pas bloquer le dev là-dessus.
- **Halo de focus** : `Enter/Leave` sur les `TextBox`/`DateTimePicker` → changer la
  couleur de bordure peinte du panel conteneur (`#4A58C4`).

### Police
**Figtree** n'est pas installée sous Windows → utiliser **Segoe UI** (cohérent OS).
Correspondance graisses : 800→Bold, 700→Bold, 600→Semibold/Bold, 500→Regular.
Tailles px du design → points WinForms approximatifs : 25px≈14pt (titre),
19px≈11pt (compteur), 14px≈9.5pt, 13.5px≈9pt, 11.5px≈8pt (en-têtes). Ajuster à l'œil.

### Icônes
Les SVG inline (immeuble, loupe, calendrier, check, croix) → exporter en **PNG**
(plusieurs tailles pour DPI) dans les `Resources`, ou utiliser une police d'icônes
(Segoe Fluent Icons / Segoe MDL2 Assets) affichée via `Label`. Conserver tailles indiquées.

### DPI
Activer le DPI-awareness (`app.manifest`) et utiliser `AutoScaleMode = Dpi` pour
que la mise en page reste nette sur écrans haute résolution.
