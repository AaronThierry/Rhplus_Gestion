using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RH_GRH
{
    internal class ServiceClass
    {

        public static void EnregistrerService(string nomService, int idEntreprise)
        {
            try
            {
                Dbconnect connect = new Dbconnect();

                using (MySqlConnection con = connect.getconnection)
                {
                    con.Open();

                    // Vérifier si la direction existe déjà
                    string checkQuery = "SELECT COUNT(*) FROM service WHERE nomService = @nomService AND id_entreprise = @idEntreprise";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@nomService", nomService);
                        checkCmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count == 0)
                        {
                            // Insérer la nouvelle direction
                            string insertQuery = "INSERT INTO service (nomService, id_entreprise) VALUES (@nomService, @idEntreprise)";
                            using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, con))
                            {
                                insertCmd.Parameters.AddWithValue("@nomService", nomService);
                                insertCmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);

                                insertCmd.ExecuteNonQuery();
                            }

                            CustomMessageBox.Show("Service enregistrée avec succès !", "Succès", CustomMessageBox.MessageType.Success);
                        }
                        else
                        {
                            CustomMessageBox.Show($"Le service '{nomService}' existe déjà pour l'entreprise sélectionnée.", "Information", CustomMessageBox.MessageType.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Erreur : " + ex.Message, "Erreur", CustomMessageBox.MessageType.Error);
            }
        }


        //////////////////////////////////////////////////////////////////////////////////

        private readonly Dbconnect _connect = new Dbconnect();

        // 1) Toutes les directions
        public DataTable GetServiceList()
        {
            var table = new DataTable();

            // ⚠️ Assure-toi que Dbconnect expose bien la chaîne de connexion.
            //   - Si getconnection => string  : new MySqlConnection(_db.getconnection)
            //   - Si getconnection => MySqlConnection : new MySqlConnection(_db.getconnection.ConnectionString)
            using (var con = new MySqlConnection(_connect.getconnection.ConnectionString)) // <— chaîne de connexion
            using (var cmd = con.CreateCommand())
            using (var da = new MySqlDataAdapter(cmd))
            {
                cmd.CommandText = @"
                SELECT
                    d.id_service  AS `ID`,
                    e.id_entreprise AS `ID_Entreprise`,
                    e.nomEntreprise AS `Entreprise`,
                    d.nomService  AS `Service`
                FROM service d
                INNER JOIN entreprise e ON e.id_entreprise = d.id_entreprise
                ORDER BY e.nomEntreprise, d.nomService;";

                con.Open();
                da.Fill(table);
            }

            return table;
        }

        /////////////////////////////////////////////////////////////////////////////////

        private readonly Dbconnect connect = new Dbconnect();

        // Recherche par mot-clé (nom de la direction et/ou nom de l'entreprise)
        public DataTable rechercheService(string recherchedata)
        {
            var table = new DataTable();
            string filtre = (recherchedata ?? string.Empty).Trim();

            string query = @"
            SELECT
                d.id_direction  AS `ID`,
                d.nomDirection  AS `Direction`,
                e.id_entreprise AS `ID Entreprise`,
                e.nomEntreprise AS `Entreprise`
            FROM direction d
            INNER JOIN entreprise e ON e.id_entreprise = d.id_entreprise
            WHERE (@filtre = '' OR d.nomDirection LIKE @like OR e.nomEntreprise LIKE @like)
            ORDER BY e.nomEntreprise, d.nomDirection;";

            using (var con = connect.getconnection)               // si getconnection => MySqlConnection
            using (var cmd = new MySqlCommand(query, con))
            using (var da = new MySqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@filtre", filtre);
                cmd.Parameters.AddWithValue("@like", $"%{filtre}%");

                con.Open();
                da.Fill(table);
            }

            return table;
        }



        public DataTable RechercheServiceConcat(string recherche)
        {
            var table = new DataTable();
            string filtre = (recherche ?? string.Empty).Trim();

            string sql = @"
        SELECT
            d.id_service  AS `ID`,
            e.id_entreprise AS `ID_Entreprise`,
            e.nomEntreprise AS `Entreprise`,
            d.nomService  AS `Service`
        FROM service d
        INNER JOIN entreprise e ON e.id_entreprise = d.id_entreprise
        WHERE CONCAT_WS(' ',
                d.nomService,
                e.nomEntreprise,
                e.activite,
                e.sigle,
                e.telephone
            ) LIKE @q
        ORDER BY e.nomEntreprise, d.nomService;";

            // ⚠️ Le point clé : créer UNE NOUVELLE connexion
            using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
            using (var cmd = new MySqlCommand(sql, con))
            using (var da = new MySqlDataAdapter(cmd))
            {
                cmd.Parameters.Add("@q", MySqlDbType.VarChar).Value = "%" + filtre + "%";
                con.Open();
                da.Fill(table);
            }

            return table;
        }


        ////////////////////////////////////////////////////////////////////////////////


        // Surcharge pour modifier avec changement d'entreprise
        public static void ModifierService(int idService, string nomService, int idEntreprise)
        {
            if (idService <= 0)
            {
                CustomMessageBox.Show("Identifiant du service invalide.", "Information",
                                CustomMessageBox.MessageType.Info);
                return;
            }

            if (string.IsNullOrWhiteSpace(nomService))
            {
                CustomMessageBox.Show("Veuillez saisir le nom du service.", "Information",
                                CustomMessageBox.MessageType.Info);
                return;
            }

            try
            {
                var connect = new Dbconnect();
                using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
                {
                    con.Open();

                    string nom = nomService.Trim();

                    // Vérifier l'unicité dans la nouvelle entreprise
                    const string checkSql = @"
                    SELECT COUNT(*)
                    FROM service
                    WHERE id_entreprise = @idEntreprise
                      AND nomService = @nom
                      AND id_service <> @id;";

                    using (var checkCmd = new MySqlCommand(checkSql, con))
                    {
                        checkCmd.Parameters.Add("@id", MySqlDbType.Int32).Value = idService;
                        checkCmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;
                        checkCmd.Parameters.Add("@idEntreprise", MySqlDbType.Int32).Value = idEntreprise;

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            CustomMessageBox.Show($"Le service « {nom} » existe déjà dans cette entreprise.", "Doublon",
                                            CustomMessageBox.MessageType.Warning);
                            return;
                        }
                    }

                    // Mise à jour
                    const string updateSql = @"
                    UPDATE service
                    SET nomService = @nom, id_entreprise = @idEntreprise
                    WHERE id_service = @id;";

                    using (var updateCmd = new MySqlCommand(updateSql, con))
                    {
                        updateCmd.Parameters.Add("@id", MySqlDbType.Int32).Value = idService;
                        updateCmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;
                        updateCmd.Parameters.Add("@idEntreprise", MySqlDbType.Int32).Value = idEntreprise;

                        int rowsAffected = updateCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            CustomMessageBox.Show("Service modifié avec succès.", "Succès",
                                            CustomMessageBox.MessageType.Success);
                        }
                        else
                        {
                            CustomMessageBox.Show("Aucune modification effectuée.", "Info",
                                            CustomMessageBox.MessageType.Info);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Erreur lors de la modification : " + ex.Message, "Erreur",
                                CustomMessageBox.MessageType.Error);
            }
        }

        public static void ModifierService(int idService, string nomService)
        {
            if (idService <= 0)
            {
                CustomMessageBox.Show("Identifiant du service invalide.", "Information",
                                CustomMessageBox.MessageType.Info);
                return;
            }

            if (string.IsNullOrWhiteSpace(nomService))
            {
                CustomMessageBox.Show("Veuillez saisir le nom du service.", "Information",
                                CustomMessageBox.MessageType.Info);
                return;
            }

            try
            {
                var connect = new Dbconnect();
                // ⚠️ Crée une nouvelle connexion (évite les erreurs de connexion réutilisée)
                using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
                {
                    con.Open();

                    string nom = nomService.Trim();

                    // 1) Vérifier l'unicité du nom DANS LA MÊME ENTREPRISE (sans changer l'entreprise)
                    const string checkSql = @"
                    SELECT COUNT(*)
                    FROM service d
                    JOIN service d2 ON d2.id_entreprise = d.id_entreprise
                    WHERE d.id_service = @id
                      AND d2.id_service <> @id
                      AND d2.nomService = @nom;";

                    using (var checkCmd = new MySqlCommand(checkSql, con))
                    {
                        checkCmd.Parameters.Add("@id", MySqlDbType.Int32).Value = idService;
                        checkCmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;

                        int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (exists > 0)
                        {
                            CustomMessageBox.Show($"Un autre service porte déjà le nom « {nom} » dans la même entreprise.",
                                            "Doublon", CustomMessageBox.MessageType.Warning);
                            return;
                        }
                    }

                    // 2) Mettre à jour UNIQUEMENT le nom
                    const string updateSql = @"
                    UPDATE service
                    SET nomService = @nom
                    WHERE id_service = @id;";

                    using (var upd = new MySqlCommand(updateSql, con))
                    {
                        upd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;
                        upd.Parameters.Add("@id", MySqlDbType.Int32).Value = idService;

                        int rows = upd.ExecuteNonQuery();
                        if (rows > 0)
                            CustomMessageBox.Show("Service modifié avec succès !", "Succès",
                                            CustomMessageBox.MessageType.Success);
                        else
                            CustomMessageBox.Show("Aucune modification effectuée (enregistrement introuvable).", "Information",
                                            CustomMessageBox.MessageType.Info);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Erreur lors de la modification : " + ex.Message, "Erreur",
                                CustomMessageBox.MessageType.Error);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////

        public static bool SupprimerService(int idService)
        {
            if (idService <= 0)
            {
                CustomMessageBox.Show("Identifiant du service invalide.", "Information",
                                CustomMessageBox.MessageType.Info);
                return false;
            }

            try
            {
                var connect = new Dbconnect();

                // ⚠️ Important : créer une nouvelle connexion depuis la connection string
                using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
                using (var cmd = new MySqlCommand("DELETE FROM service WHERE id_service = @id;", con))
                {
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = idService;

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        CustomMessageBox.Show("Service supprimée avec succès.", "Succès",
                                        CustomMessageBox.MessageType.Success);
                        return true;
                    }
                    else
                    {
                        CustomMessageBox.Show("Aucune suppression effectuée (enregistrement introuvable).", "Information",
                                        CustomMessageBox.MessageType.Info);
                        return false;
                    }
                }
            }
            catch (MySqlException ex) when (ex.Number == 1451 || ex.Number == 1452)
            {
                // 1451: Cannot delete or update a parent row: a foreign key constraint fails
                CustomMessageBox.Show(
                    "Impossible de supprimer ce service car elle est utilisée ailleurs (contrainte de clé étrangère). " +
                    "Vérifiez les enregistrements liés (départements, directions, etc.).",
                    "Opération interdite", CustomMessageBox.MessageType.Warning);
                return false;
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Erreur lors de la suppression : " + ex.Message, "Erreur",
                                CustomMessageBox.MessageType.Error);
                return false;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////
    


    //***//////////////////////////////***////////////////////***///////////////////////


            // <summary>Retourne les directions d'une entreprise.</summary>
        public static DataTable ListerParEntrepriseService(int idEntreprise)
        {
            var table = new DataTable();
            var db = new Dbconnect();

            // Si getconnection renvoie une *chaîne*, utilisez: new MySqlConnection(db.getconnection)
            using (var con = new MySqlConnection(db.getconnection.ConnectionString))
            using (var cmd = new MySqlCommand(@"
            SELECT id_service, nomService
            FROM service
            WHERE id_entreprise = @idEntreprise
            ORDER BY nomService;", con))
            using (var da = new MySqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);
                con.Open();
                da.Fill(table);
            }
            return table;
        }

        /// <summary>Bind un ComboBox avec les directions d'une entreprise.</summary>
        public static void BindComboService(ComboBox combo, int idEntreprise, int? idASelectionner = null, bool placeholder = true)
        {
            var dt = ListerParEntrepriseService(idEntreprise);

            if (placeholder)
            {
                var row = dt.NewRow();
                row["id_service"] = DBNull.Value;
                row["nomService"] = "Sélectionner service";
                dt.Rows.InsertAt(row, 0);
            }

            combo.DataSource = dt;
            combo.DisplayMember = "nomService";
            combo.ValueMember = "id_service";
            combo.SelectedIndex = placeholder ? 0 : -1;

            if (idASelectionner.HasValue)
                combo.SelectedValue = idASelectionner.Value;
        }

        /// <summary>Recharge les directions à partir d'un ComboBox entreprise déjà bindé.</summary>
        public static void BindComboDepuisEntreprise(ComboBox comboService, ComboBox comboEntreprise, int? idASelectionner = null, bool placeholder = true)
        {
            int? idEnt = EntrepriseClass.GetIdEntrepriseSelectionnee(comboEntreprise);
            if (!idEnt.HasValue)
            {
                Categorie.ClearCombo(comboService); // on réutilise le clear générique
                return;
            }
            BindComboService(comboService, idEnt.Value, idASelectionner, placeholder);
        }

        /// <summary>Déduit l'id_direction sélectionné dans un ComboBox.</summary>
        public static int? GetIdServiceSelectionnee(ComboBox combo)
        {
            var val = combo?.SelectedValue;
            if (val == null || val == DBNull.Value) return null;

            if (val is int i) return i;
            if (val is long l) return (int)l;
            if (val is short s) return (int)s;

            return int.TryParse(val.ToString(), out var id) ? (int?)id : null;
        }



        //***********************************//*************************//****************//


        public static void ChargerServices(
       ComboBox combo,
       int idEntreprise,
       int? idSelection = null,
       bool ajouterPlaceholder = false)
        {
            var connect = new Dbconnect();
            using (var con = connect.getconnection)
            {
                con.Open();
                const string sql = @"
            SELECT id_service, nomService
            FROM service
            WHERE id_entreprise = @idEnt
            ORDER BY nomService;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@idEnt", MySqlDbType.Int32).Value = idEntreprise;

                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);

                        if (ajouterPlaceholder)
                        {
                            var row = dt.NewRow();
                            row["id_service"] = 0;
                            row["nomService"] = "--- Sélectionner service ---";
                            dt.Rows.InsertAt(row, 0);
                        }

                        // Définir Value/Display AVANT DataSource (bonne pratique)
                        combo.ValueMember = "id_service";
                        combo.DisplayMember = "nomService";
                        combo.DataSource = dt;

                        // Sélectionner seulement si présent
                        if (idSelection.HasValue && idSelection.Value > 0)
                        {
                            // cherche une ligne dont id_service == idSelection
                            bool exists = dt.Select("id_service = " + idSelection.Value).Length > 0;
                            combo.SelectedValue = exists ? (object)idSelection.Value : (ajouterPlaceholder ? 0 : -1);
                        }
                        else
                        {
                            combo.SelectedValue = ajouterPlaceholder ? 0 : -1;
                        }
                    }
                }
            }
        }

        // Surcharge pour Guna2ComboBox
        public static void ChargerServices(
            Guna.UI2.WinForms.Guna2ComboBox combo,
            int idEntreprise,
            int? idSelection = null,
            bool ajouterPlaceholder = false)
        {
            var connect = new Dbconnect();
            using (var con = connect.getconnection)
            {
                con.Open();
                string sql = @"
            SELECT id_service, nomService
            FROM service
            WHERE id_entreprise = @idEnt
            ORDER BY nomService;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@idEnt", MySqlDbType.Int32).Value = idEntreprise;

                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);

                        if (ajouterPlaceholder)
                        {
                            var row = dt.NewRow();
                            row["id_service"] = 0;
                            row["nomService"] = "--- Sélectionner service ---";
                            dt.Rows.InsertAt(row, 0);
                        }

                        combo.ValueMember = "id_service";
                        combo.DisplayMember = "nomService";
                        combo.DataSource = dt;

                        if (idSelection.HasValue && idSelection.Value > 0)
                        {
                            bool exists = dt.Select("id_service = " + idSelection.Value).Length > 0;
                            combo.SelectedValue = exists ? (object)idSelection.Value : (ajouterPlaceholder ? 0 : -1);
                        }
                        else
                        {
                            combo.SelectedValue = ajouterPlaceholder ? 0 : -1;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Récupère les services d'une entreprise sous forme de DataTable
        /// Utilisé pour le filtrage intelligent des ComboBox
        /// </summary>
        public static DataTable GetServicesByEntreprise(int idEntreprise)
        {
            var connect = new Dbconnect();
            using (var con = connect.getconnection)
            {
                con.Open();
                const string sql = @"
                    SELECT id_service, nomService
                    FROM service
                    WHERE id_entreprise = @idEnt
                    ORDER BY nomService;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@idEnt", MySqlDbType.Int32).Value = idEntreprise;

                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }



    }

}
