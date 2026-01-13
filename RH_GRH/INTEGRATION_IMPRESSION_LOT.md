# 🖨️ GUIDE D'INTÉGRATION - IMPRESSION EN LOT DES BULLETINS

## ✅ Fichiers créés

1. **BatchBulletinService.cs** - Service de génération en lot
2. **SelectionEmployesImpressionForm.cs** + Designer - Formulaire de sélection
3. **ProgressionImpressionForm.cs** + Designer - Formulaire de progression

## 📋 ÉTAPES D'INTÉGRATION

### Étape 1 : Ajouter un bouton dans le menu principal (Formmain)

```csharp
// Dans Formmain.Designer.cs, ajouter un nouveau bouton Guna2
private Guna.UI2.WinForms.Guna2Button buttonImpressionLot;

// Configuration du bouton
this.buttonImpressionLot = new Guna.UI2.WinForms.Guna2Button();
this.buttonImpressionLot.BorderRadius = 10;
this.buttonImpressionLot.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(0)))));
this.buttonImpressionLot.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold);
this.buttonImpressionLot.ForeColor = System.Drawing.Color.White;
this.buttonImpressionLot.Location = new System.Drawing.Point(X, Y); // À ajuster
this.buttonImpressionLot.Name = "buttonImpressionLot";
this.buttonImpressionLot.Size = new System.Drawing.Size(200, 50);
this.buttonImpressionLot.TabIndex = XX;
this.buttonImpressionLot.Text = "🖨️ Impression Lot";
this.buttonImpressionLot.Click += new System.EventHandler(this.buttonImpressionLot_Click);
```

### Étape 2 : Ajouter l'événement Click dans Formmain.cs

```csharp
private void buttonImpressionLot_Click(object sender, EventArgs e)
{
    try
    {
        // Récupérer l'entreprise active (à adapter selon votre système)
        int idEntreprise = GetIdEntrepriseActive(); // Méthode à implémenter

        // Ouvrir le formulaire de sélection
        using (var formSelection = new SelectionEmployesImpressionForm(idEntreprise))
        {
            if (formSelection.ShowDialog() == DialogResult.OK)
            {
                // L'utilisateur a validé la sélection
                var employes = formSelection.EmployesSelectionnes;
                var periodeDebut = formSelection.PeriodeDebut;
                var periodeFin = formSelection.PeriodeFin;
                var dossier = formSelection.DossierDestination;

                // Ouvrir le formulaire de progression
                using (var formProgress = new ProgressionImpressionForm())
                {
                    formProgress.Show();

                    // Lancer la génération de manière asynchrone
                    var task = formProgress.GenererBulletinsAsync(
                        employes,
                        periodeDebut,
                        periodeFin,
                        dossier);

                    // Attendre la fin (le formulaire gère l'UI)
                    Application.DoEvents();

                    // Afficher le résultat
                    formProgress.ShowDialog();
                }
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Erreur : {ex.Message}", "Erreur",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

// Méthode helper pour récupérer l'ID entreprise active
private int GetIdEntrepriseActive()
{
    // Option 1 : Si vous avez un ComboBox entreprise global
    // return Convert.ToInt32(comboBoxEntreprise.SelectedValue);

    // Option 2 : Si vous stockez l'entreprise dans une variable
    // return this.idEntrepriseEnCours;

    // Option 3 : Ouvrir un dialog pour sélectionner l'entreprise
    // return SelectionnerEntreprise();

    // Temporaire : Demander l'ID
    var result = Microsoft.VisualBasic.Interaction.InputBox(
        "Entrez l'ID de l'entreprise :",
        "Sélection entreprise",
        "1");

    return string.IsNullOrEmpty(result) ? 0 : Convert.ToInt32(result);
}
```

### Étape 3 : ALTERNATIVE - Ajouter directement dans GestionSalaireHoraireForm

Si vous voulez intégrer directement dans le formulaire de gestion de salaire :

```csharp
// Dans GestionSalaireHoraireForm.Designer.cs
// Modifier le bouton "buttonPrint" existant OU ajouter un nouveau bouton

private Guna.UI2.WinForms.Guna2Button buttonImprimerLot;

// Configuration
this.buttonImprimerLot = new Guna.UI2.WinForms.Guna2Button();
this.buttonImprimerLot.BorderRadius = 8;
this.buttonImprimerLot.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(0)))));
this.buttonImprimerLot.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold);
this.buttonImprimerLot.ForeColor = System.Drawing.Color.White;
this.buttonImprimerLot.Location = new System.Drawing.Point(X, Y); // À côté de buttonPrint
this.buttonImprimerLot.Name = "buttonImprimerLot";
this.buttonImprimerLot.Size = new System.Drawing.Size(150, 40);
this.buttonImprimerLot.TabIndex = XX;
this.buttonImprimerLot.Text = "🖨️ Imprimer LOT";
this.buttonImprimerLot.Click += new System.EventHandler(this.buttonImprimerLot_Click);

// Ajouter au panel
this.panel3.Controls.Add(this.buttonImprimerLot);
```

```csharp
// Dans GestionSalaireHoraireForm.cs

private void buttonImprimerLot_Click(object sender, EventArgs e)
{
    try
    {
        // Récupérer l'ID entreprise depuis le contexte actuel
        int idEntreprise = 1; // À adapter

        using (var formSelection = new SelectionEmployesImpressionForm(idEntreprise))
        {
            if (formSelection.ShowDialog() == DialogResult.OK)
            {
                using (var formProgress = new ProgressionImpressionForm())
                {
                    formProgress.Show();

                    var task = formProgress.GenererBulletinsAsync(
                        formSelection.EmployesSelectionnes,
                        formSelection.PeriodeDebut,
                        formSelection.PeriodeFin,
                        formSelection.DossierDestination);

                    formProgress.ShowDialog();
                }
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Erreur : {ex.Message}", "Erreur",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
```

## 🔧 COMPILATION

Assurez-vous d'ajouter les fichiers au projet :

```xml
<!-- Dans RH_GRH.csproj -->
<ItemGroup>
    <Compile Include="BatchBulletinService.cs" />
    <Compile Include="SelectionEmployesImpressionForm.cs">
        <SubType>Form</SubType>
    </Compile>
    <Compile Include="SelectionEmployesImpressionForm.Designer.cs">
        <DependentUpon>SelectionEmployesImpressionForm.cs</DependentUpon>
    </Compile>
    <Compile Include="ProgressionImpressionForm.cs">
        <SubType>Form</SubType>
    </Compile>
    <Compile Include="ProgressionImpressionForm.Designer.cs">
        <DependentUpon>ProgressionImpressionForm.cs</DependentUpon>
    </Compile>
</ItemGroup>

<ItemGroup>
    <EmbeddedResource Include="SelectionEmployesImpressionForm.resx">
        <DependentUpon>SelectionEmployesImpressionForm.cs</DependentUpon>
    </EmbeddedResource>
    <EmbeddedResource Include="ProgressionImpressionForm.resx">
        <DependentUpon>ProgressionImpressionForm.cs</DependentUpon>
    </EmbeddedResource>
</ItemGroup>
```

## ⚠️ IMPORTANT - LIMITATIONS ACTUELLES

**La version actuelle génère des bulletins avec des données MINIMALES.**

Pour une utilisation complète, vous devez :

### 1. Créer une table de sauvegarde des paies calculées

```sql
CREATE TABLE paie_calculee (
    id_paie INT AUTO_INCREMENT PRIMARY KEY,
    id_personnel INT NOT NULL,
    periode_debut DATE NOT NULL,
    periode_fin DATE NOT NULL,
    snapshot_json TEXT NOT NULL,  -- PayrollSnapshot en JSON
    date_creation DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_personnel) REFERENCES personnel(id_personnel),
    UNIQUE KEY unique_paie (id_personnel, periode_debut, periode_fin)
);
```

### 2. Sauvegarder le snapshot après chaque calcul

Dans `GestionSalaireHoraireForm.cs`, après le calcul (buttonEffacer_Click) :

```csharp
// Après avoir créé le snapshot (ligne ~1433)
_lastSnapshot = snapshot;

// NOUVEAU : Sauvegarder dans la BDD
SauvegarderSnapshot(snapshot);

// Méthode à ajouter
private void SauvegarderSnapshot(PayrollSnapshot snapshot)
{
    try
    {
        var connect = new Dbconnect();
        using (var con = connect.getconnection)
        {
            con.Open();

            // Sérialiser le snapshot en JSON
            string snapshotJson = System.Text.Json.JsonSerializer.Serialize(snapshot);

            string sql = @"
                INSERT INTO paie_calculee (id_personnel, periode_debut, periode_fin, snapshot_json)
                VALUES (@idPersonnel, @periodeDebut, @periodeFin, @snapshotJson)
                ON DUPLICATE KEY UPDATE
                    snapshot_json = @snapshotJson,
                    date_creation = CURRENT_TIMESTAMP";

            using (var cmd = new MySqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@idPersonnel", snapshot.IdEmploye);
                cmd.Parameters.AddWithValue("@periodeDebut", guna2DateTimePickerDebut.Value.Date);
                cmd.Parameters.AddWithValue("@periodeFin", guna2DateTimePickerFin.Value.Date);
                cmd.Parameters.AddWithValue("@snapshotJson", snapshotJson);

                cmd.ExecuteNonQuery();
            }
        }
    }
    catch (Exception ex)
    {
        // Log l'erreur mais ne bloque pas l'utilisateur
        System.Diagnostics.Debug.WriteLine($"Erreur sauvegarde snapshot : {ex.Message}");
    }
}
```

### 3. Modifier RecupererOuCalculerSnapshot dans BatchBulletinService

```csharp
private static PayrollSnapshot RecupererOuCalculerSnapshot(
    int idEmploye,
    DateTime periodeDebut,
    DateTime periodeFin)
{
    var connect = new Dbconnect();
    using (var con = connect.getconnection)
    {
        con.Open();

        string sql = @"
            SELECT snapshot_json
            FROM paie_calculee
            WHERE id_personnel = @idPersonnel
            AND periode_debut = @periodeDebut
            AND periode_fin = @periodeFin
            LIMIT 1";

        using (var cmd = new MySqlCommand(sql, con))
        {
            cmd.Parameters.AddWithValue("@idPersonnel", idEmploye);
            cmd.Parameters.AddWithValue("@periodeDebut", periodeDebut);
            cmd.Parameters.AddWithValue("@periodeFin", periodeFin);

            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                string json = result.ToString();
                return System.Text.Json.JsonSerializer.Deserialize<PayrollSnapshot>(json);
            }
        }
    }

    return null; // Aucune paie calculée trouvée
}
```

## 🎯 WORKFLOW COMPLET

1. **Calcul individuel** : L'utilisateur calcule les paies employé par employé
2. **Sauvegarde auto** : Chaque calcul est sauvegardé dans `paie_calculee`
3. **Impression lot** : L'utilisateur sélectionne l'entreprise et la période
4. **Génération** : Le système récupère tous les snapshots sauvegardés
5. **Export** : Génération des PDF dans un dossier avec la date

## 📊 AVANTAGES

✅ Interface moderne et professionnelle
✅ Progression en temps réel
✅ Possibilité d'annuler
✅ Gestion des erreurs par employé
✅ Export automatique dans un dossier organisé
✅ Statistiques de génération
✅ Support multi-threading (futur)

## 🚀 ÉVOLUTIONS FUTURES

- [ ] Génération ZIP automatique
- [ ] Envoi par email en masse
- [ ] Export Excel récapitulatif
- [ ] Comparaison entre périodes
- [ ] Historique des impressions
- [ ] Filtres avancés (service, direction, catégorie)
