using MySql.Data.MySqlClient;
using RH_GRH;
using System;
using System.Data;
using System.Windows.Forms;



public static class EmployeClass
{


    /// Enregistre un nouvel employé dans la base de données.
    /// *****************************************************************************************

    public static void EnregistrerEmploye(
    string matricule,
    string nomPrenom,
    string civilite,
    string sexe,
    DateTime dateNaissance,   
    string adresse,
    string telephone,
    string identification,
    string poste,
    string contrat,
    string cadre,
    DateTime dateEntree,      
    DateTime? dateSortie,
    string typeContrat,
    int heureContrat, 
    int jourContrat, 
    string modePayement, 
    string banque, 
    string numeroBancaire,
    int idDirection,
    int idService,
    int idCategorie,
    int idEntreprise,
    string dureeContrat,
    string numerocnss,
    decimal salairemoyen

    )
    {
        try
        {
            if (string.IsNullOrWhiteSpace(matricule))
                throw new ArgumentException("Le matricule est requis.");
            if (string.IsNullOrWhiteSpace(nomPrenom))
                throw new ArgumentException("Le nom complet est requis.");

            var connect = new Dbconnect();
            using (var con = connect.getconnection)
            {
                con.Open();

                // Vérifier doublon (matricule + entreprise)
                const string checkSql = @"
                SELECT 1
                FROM personnel
                WHERE matricule = @matricule AND id_entreprise = @id_entreprise
                LIMIT 1;";
                using (var check = new MySqlCommand(checkSql, con))
                {
                    check.Parameters.AddWithValue("@matricule", matricule);
                    check.Parameters.AddWithValue("@id_entreprise", idEntreprise);
                    var exists = check.ExecuteScalar();
                    if (exists != null)
                    {
                        MessageBox.Show(
                            $"L'employé '{matricule}' existe déjà pour l'entreprise sélectionnée.",
                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Insert minimal
                const string insertSql = @"
                INSERT INTO personnel
                    (matricule, nomPrenom, civilite, sexe, date_naissance, adresse, telephone, identification, poste, contrat, cadre, date_entree, date_sortie, typeContrat ,heureContrat, jourContrat, modePayement, banque, numeroBancaire, id_direction, id_categorie, id_service, id_entreprise, dureeContrat,numerocnss,salairemoyen)
                VALUES
                    (@matricule, @nomPrenom, @civilite, @sexe,  @date_naissance, @adresse, @telephone, @identification, @poste, @contrat, @cadre, @date_entree, @date_sortie, @typeContrat, @heureContrat, @jourContrat, @modePayement, @banque, @numeroBancaire, @id_direction, @id_categorie, @id_service, @id_entreprise, @dureeContrat,@numerocnss,@salairemoyen);";

                using (var cmd = new MySqlCommand(insertSql, con))
                {
                    cmd.Parameters.AddWithValue("@matricule", matricule);
                    cmd.Parameters.AddWithValue("@nomPrenom", nomPrenom);
                    cmd.Parameters.AddWithValue("@civilite", civilite);
                    cmd.Parameters.AddWithValue("@sexe", sexe);
                    cmd.Parameters.Add("@date_naissance", MySqlDbType.Date).Value = dateNaissance;
                    cmd.Parameters.AddWithValue("@adresse", adresse);
                    cmd.Parameters.AddWithValue("@telephone", telephone);
                    cmd.Parameters.AddWithValue("@identification", identification);
                    cmd.Parameters.AddWithValue("@poste", poste);
                    cmd.Parameters.AddWithValue("@contrat", contrat);
                    cmd.Parameters.AddWithValue("@cadre", cadre);
                    cmd.Parameters.Add("@date_entree", MySqlDbType.Date).Value = dateEntree;
                    cmd.Parameters.Add("@date_sortie", MySqlDbType.Date).Value = (object)dateSortie ?? DBNull.Value;
                    cmd.Parameters.AddWithValue("@typeContrat",typeContrat);
                    cmd.Parameters.AddWithValue("@heureContrat", heureContrat);
                    cmd.Parameters.AddWithValue("@jourContrat", jourContrat);
                    cmd.Parameters.AddWithValue("@modePayement", modePayement);
                    cmd.Parameters.AddWithValue("@banque", banque);
                    cmd.Parameters.AddWithValue("@numeroBancaire", numeroBancaire);
                    cmd.Parameters.AddWithValue("@id_direction", idDirection);
                    cmd.Parameters.AddWithValue("@id_categorie", idCategorie);
                    cmd.Parameters.AddWithValue("@id_service", idService);
                    cmd.Parameters.AddWithValue("@id_entreprise", idEntreprise); 
                    cmd.Parameters.AddWithValue("@dureeContrat", dureeContrat);
                    cmd.Parameters.AddWithValue("@numerocnss", numerocnss);
                    cmd.Parameters.AddWithValue("@salairemoyen", salairemoyen);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Employé enregistré avec succes .",
                    "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (MySqlException sqlEx) when (sqlEx.Number == 1062) // duplicate key
        {
            MessageBox.Show("Doublon: matricule déjà utilisé pour cette entreprise.",
                "Conflit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erreur : " + ex.Message, "Erreur",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }





    /// *****************************************************************************************

                    public static DataTable GetEmployeList(int? idEntreprise = null)
                    {
                        var table = new DataTable();

                        var connect = new Dbconnect();
                        using (var con = connect.getconnection)
                        {
                            con.Open();

                            const string sql = @"
                SELECT
                    p.id_personnel                    AS `Id`,
                    e.nomEntreprise                   AS `Entreprise`,
                    p.matricule                       AS `Matricule`,
                    p.nomPrenom                       AS `Nom Prenom`,
                    p.poste                           AS `Poste`,
                    p.dureeContrat                    AS `Contrat`,
                    p.telephone                       AS `Telephone`
                FROM personnel p
                LEFT JOIN direction  d ON d.id_direction  = p.id_direction
                LEFT JOIN service    s ON s.id_service    = p.id_service
                LEFT JOIN categorie  c ON c.id_categorie  = p.id_categorie
                LEFT JOIN entreprise e ON e.id_entreprise = p.id_entreprise
                WHERE (@idEnt IS NULL OR p.id_entreprise = @idEnt)
                ORDER BY p.nomPrenom;";

                            using (var cmd = new MySqlCommand(sql, con))
                            {
                                cmd.Parameters.AddWithValue("@idEnt", (object)idEntreprise ?? DBNull.Value);

                                using (var da = new MySqlDataAdapter(cmd))
                                {
                                    da.Fill(table);
                                }
                            }
                        }

                        return table;
                    }










    //****************************************************************************************************

        public static DataTable RecherchePersonnel(string recherche, int? idEntreprise = null)
        {
            var table = new DataTable();
            var connect = new Dbconnect();

            using (var con = connect.getconnection)
            {
                con.Open();

                const string sql = @"
    SELECT
                    p.id_personnel                    AS `Id`,
                    e.nomEntreprise                   AS `Entreprise`,
                    p.matricule                       AS `Matricule`,
                    p.nomPrenom                       AS `Nom Prenom`,
                    p.poste                           AS `Poste`,
                    p.dureeContrat                    AS `Contrat`,
                    p.telephone                       AS `Telephone`
    FROM personnel p
    LEFT JOIN direction  d ON d.id_direction  = p.id_direction
    LEFT JOIN service    s ON s.id_service    = p.id_service
    LEFT JOIN categorie  c ON c.id_categorie  = p.id_categorie
    LEFT JOIN entreprise e ON e.id_entreprise = p.id_entreprise
    WHERE
        (@idEnt IS NULL OR p.id_entreprise = @idEnt)
    AND (
        @q = '' OR
        p.matricule     LIKE @like OR
        p.nomPrenom     LIKE @like OR
        p.civilite      LIKE @like OR
        p.sexe          LIKE @like OR
        p.telephone     LIKE @like OR
        p.adresse       LIKE @like OR
        p.contrat       LIKE @like OR
        p.poste         LIKE @like OR
        p.typeContrat   LIKE @like OR
        p.dureeContrat   LIKE @like OR
        d.nomDirection  LIKE @like OR
        s.nomService    LIKE @like OR
        c.nomCategorie  LIKE @like OR
        e.nomEntreprise LIKE @like
    )
    ORDER BY p.nomPrenom;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    // id entreprise optionnel
                    cmd.Parameters.Add("@idEnt", MySqlDbType.Int32).Value =
                        (object)idEntreprise ?? DBNull.Value;

                    var q = (recherche ?? "").Trim();
                    var like = "%" + q + "%";

                    // paramètres de recherche
                    cmd.Parameters.Add("@q", MySqlDbType.VarChar).Value = q;
                    cmd.Parameters.Add("@like", MySqlDbType.VarChar).Value = like;

                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(table);
                    }
                }
            }

            return table;
        }


    //************************//********************//*****************************/**************************

    /// <summary>
    /// Alias pour RecherchePersonnel - permet la recherche d'employés
    /// </summary>
    public static DataTable RechercheEmploye(string recherche, int? idEntreprise = null)
    {
        return RecherchePersonnel(recherche, idEntreprise);
    }



    /// <summary>
    /// Met à jour un employé existant.
    /// </summary>
    public static void ModifierEmploye(
        int idPersonnel,
        string matricule,
        string nomPrenom,
        string civilite,
        string sexe,
        DateTime dateNaissance,
        string adresse,
        string telephone,
        string identification,
        string poste,
        string contrat,
        string cadre,                 // si TINYINT(1) en BD, remplacer par byte et adapter le paramètre
        DateTime dateEntree,
        DateTime? dateSortie,
        string typeContrat,
        int heureContrat,
        int jourContrat,
        string modePayement,
        string banque,
        string numeroBancaire,
        int idDirection,
        int idService,
        int idCategorie,
        int idEntreprise,
        string dureeContrat,          // "CDD"/"CDI" (ou selon ton modèle)
        string numerocnss,
        decimal salairemoyen
    )
    {
        try
        {
            // --- validations de base
            if (idPersonnel <= 0)
                throw new ArgumentException("Identifiant de l'employé invalide.");
            if (string.IsNullOrWhiteSpace(matricule))
                throw new ArgumentException("Le matricule est requis.");
            if (string.IsNullOrWhiteSpace(nomPrenom))
                throw new ArgumentException("Le nom complet est requis.");

            var connect = new Dbconnect();
            using (var con = connect.getconnection)
            {
                con.Open();

                // 1) Vérifier unicité matricule pour l’entreprise (hors id courant)
                const string checkSql = @"
SELECT 1
FROM personnel
WHERE matricule = @matricule
  AND id_entreprise = @id_entreprise
  AND id_personnel <> @id_personnel
LIMIT 1;";
                using (var check = new MySqlCommand(checkSql, con))
                {
                    check.Parameters.Add("@matricule", MySqlDbType.VarChar).Value = matricule;
                    check.Parameters.Add("@id_entreprise", MySqlDbType.Int32).Value = idEntreprise;
                    check.Parameters.Add("@id_personnel", MySqlDbType.Int32).Value = idPersonnel;

                    var exists = check.ExecuteScalar();
                    if (exists != null)
                    {
                        MessageBox.Show(
                            $"Le matricule '{matricule}' est déjà utilisé pour cette entreprise.",
                            "Conflit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // 2) UPDATE typé
                const string updateSql = @"
UPDATE personnel
SET
    matricule       = @matricule,
    nomPrenom       = @nomPrenom,
    civilite        = @civilite,
    sexe            = @sexe,
    date_naissance  = @date_naissance,
    adresse         = @adresse,
    telephone       = @telephone,
    identification  = @identification,
    poste           = @poste,
    contrat         = @contrat,
    cadre           = @cadre,
    date_entree     = @date_entree,
    date_sortie     = @date_sortie,
    typeContrat     = @typeContrat,
    heureContrat    = @heureContrat,
    jourContrat     = @jourContrat,
    modePayement    = @modePayement,
    banque          = @banque,
    numeroBancaire  = @numeroBancaire,
    id_direction    = @id_direction,
    id_categorie    = @id_categorie,
    id_service      = @id_service,
    id_entreprise   = @id_entreprise,
    dureeContrat    = @dureeContrat,
    numerocnss      = @numerocnss,
    salairemoyen    = @salairemoyen
WHERE id_personnel  = @id_personnel;";

                using (var cmd = new MySqlCommand(updateSql, con))
                {
                    // Clé
                    cmd.Parameters.Add("@id_personnel", MySqlDbType.Int32).Value = idPersonnel;

                    // Champs
                    cmd.Parameters.Add("@matricule", MySqlDbType.VarChar).Value = matricule;
                    cmd.Parameters.Add("@nomPrenom", MySqlDbType.VarChar).Value = nomPrenom;
                    cmd.Parameters.Add("@civilite", MySqlDbType.VarChar).Value = civilite ?? string.Empty;
                    cmd.Parameters.Add("@sexe", MySqlDbType.VarChar).Value = sexe ?? string.Empty;
                    cmd.Parameters.Add("@date_naissance", MySqlDbType.Date).Value = dateNaissance.Date;
                    cmd.Parameters.Add("@adresse", MySqlDbType.VarChar).Value = adresse ?? string.Empty;
                    cmd.Parameters.Add("@telephone", MySqlDbType.VarChar).Value = telephone ?? string.Empty;
                    cmd.Parameters.Add("@identification", MySqlDbType.VarChar).Value = identification ?? string.Empty;
                    cmd.Parameters.Add("@poste", MySqlDbType.VarChar).Value = poste ?? string.Empty;
                    cmd.Parameters.Add("@contrat", MySqlDbType.VarChar).Value = contrat ?? string.Empty;

                    // Si 'cadre' est TINYINT(1) en BD, mappe ici (0/1) et type Byte
                    cmd.Parameters.Add("@cadre", MySqlDbType.VarChar).Value = cadre ?? string.Empty;

                    cmd.Parameters.Add("@date_entree", MySqlDbType.Date).Value = dateEntree.Date;
                    cmd.Parameters.Add("@date_sortie", MySqlDbType.Date).Value = (object)dateSortie ?? DBNull.Value;

                    cmd.Parameters.Add("@typeContrat", MySqlDbType.VarChar).Value = typeContrat ?? string.Empty;
                    cmd.Parameters.Add("@heureContrat", MySqlDbType.Int32).Value = heureContrat;
                    cmd.Parameters.Add("@jourContrat", MySqlDbType.Int32).Value = jourContrat;

                    cmd.Parameters.Add("@modePayement", MySqlDbType.VarChar).Value = modePayement ?? string.Empty;
                    cmd.Parameters.Add("@banque", MySqlDbType.VarChar).Value = banque ?? string.Empty;
                    cmd.Parameters.Add("@numeroBancaire", MySqlDbType.VarChar).Value = numeroBancaire ?? string.Empty;

                    cmd.Parameters.Add("@id_direction", MySqlDbType.Int32).Value = idDirection;
                    cmd.Parameters.Add("@id_categorie", MySqlDbType.Int32).Value = idCategorie;
                    cmd.Parameters.Add("@id_service", MySqlDbType.Int32).Value = idService;
                    cmd.Parameters.Add("@id_entreprise", MySqlDbType.Int32).Value = idEntreprise;

                    cmd.Parameters.Add("@dureeContrat", MySqlDbType.VarChar).Value = dureeContrat ?? string.Empty;
                    cmd.Parameters.Add("@numerocnss", MySqlDbType.VarChar).Value = numerocnss ?? string.Empty;
                    cmd.Parameters.Add("@salairemoyen", MySqlDbType.Decimal).Value = salairemoyen;

                    int rows = cmd.ExecuteNonQuery();
                    if (rows != 1)
                    {
                        MessageBox.Show("Aucune ligne modifiée. Vérifiez l'identifiant.",
                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                MessageBox.Show("Employé modifié avec succès.",
                    "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        catch (MySqlException sqlEx) when (sqlEx.Number == 1062) // duplicate key
        {
            MessageBox.Show("Doublon: matricule déjà utilisé pour cette entreprise.",
                "Conflit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erreur : " + ex.Message, "Erreur",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }





    //**************************///************************//*////////////////*************************///


    public static void SupprimerEmploye(int idPersonnel)
    {
        try
        {
            if (idPersonnel <= 0)
                throw new ArgumentException("Identifiant employé invalide.");

            // Confirmation avant suppression
            var confirm = MessageBox.Show(
                "Voulez-vous vraiment supprimer cet employé ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            var connect = new Dbconnect();
            using (var con = connect.getconnection)
            {
                con.Open();

                const string sql = @"DELETE FROM personnel WHERE id_personnel = @id;";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = idPersonnel;

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Employé supprimé avec succès.",
                            "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Aucun employé trouvé avec cet identifiant.",
                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        catch (MySqlException sqlEx)
        {
            MessageBox.Show("Erreur SQL : " + sqlEx.Message,
                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erreur : " + ex.Message,
                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }




    //***************************************************************************************************






    public static void ChargerEmployesParEntreprise(ComboBox combo, int idEntreprise, int? idSel = null, bool placeholder = true)
    {
        var connect = new Dbconnect();
        var con = connect.getconnection;
        con.Open();

        const string sql = @"SELECT id_personnel, nomPrenom FROM personnel WHERE id_entreprise=@e ORDER BY nomPrenom;";
         var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@e", MySqlDbType.Int32).Value = idEntreprise;

        var da = new MySqlDataAdapter(cmd);
        var dt = new DataTable();
        da.Fill(dt);

        if (placeholder)
        {
            var row = dt.NewRow();
            row["id_personnel"] = 0;
            row["nomPrenom"] = "--- Sélectionner employé ---";
            dt.Rows.InsertAt(row, 0);
        }

        combo.ValueMember = "id_personnel";
        combo.DisplayMember = "nomPrenom";
        combo.DataSource = dt;
        combo.SelectedValue = idSel ?? (placeholder ? 0 : -1);
    }


    public static void ChargerEmployesParEntrepriseHoraire(ComboBox combo, int idEntreprise, int? idSel = null, bool placeholder = true)
    {
        var connect = new Dbconnect();
        var con = connect.getconnection;
        con.Open();

        const string sql = @"
    SELECT id_personnel, nomPrenom 
    FROM personnel 
    WHERE id_entreprise = @e
    AND typeContrat = 'Horaire'  -- Filtrer par typeContrat = 'Horaire'
    ORDER BY nomPrenom;";

        var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@e", MySqlDbType.Int32).Value = idEntreprise;

        var da = new MySqlDataAdapter(cmd);
        var dt = new DataTable();
        da.Fill(dt);

        if (placeholder)
        {
            var row = dt.NewRow();
            row["id_personnel"] = 0;
            row["nomPrenom"] = "--- Sélectionner employé ---";
            dt.Rows.InsertAt(row, 0);
        }

        combo.ValueMember = "id_personnel";
        combo.DisplayMember = "nomPrenom";
        combo.DataSource = dt;
        combo.SelectedValue = idSel ?? (placeholder ? 0 : -1);
    }




    public static void ChargerEmployesParEntrepriseJournalier(ComboBox combo, int idEntreprise, int? idSel = null, bool placeholder = true)
    {
        var connect = new Dbconnect();
        var con = connect.getconnection;
        con.Open();

        const string sql = @"
    SELECT id_personnel, nomPrenom 
    FROM personnel 
    WHERE id_entreprise = @e
    AND typeContrat = 'Journalier'  -- Filtrer par typeContrat = 'Journalier'
    ORDER BY nomPrenom;";

        var cmd = new MySqlCommand(sql, con);
        cmd.Parameters.Add("@e", MySqlDbType.Int32).Value = idEntreprise;

        var da = new MySqlDataAdapter(cmd);
        var dt = new DataTable();
        da.Fill(dt);

        if (placeholder)
        {
            var row = dt.NewRow();
            row["id_personnel"] = 0;
            row["nomPrenom"] = "--- Sélectionner employé ---";
            dt.Rows.InsertAt(row, 0);
        }

        combo.ValueMember = "id_personnel";
        combo.DisplayMember = "nomPrenom";
        combo.DataSource = dt;
        combo.SelectedValue = idSel ?? (placeholder ? 0 : -1);
    }




}

