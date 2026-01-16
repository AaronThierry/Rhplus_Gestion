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
    internal class DirectionClass
    {

        public static void EnregistrerDirection(string nomDirection, int idEntreprise)
        {
            try
            {
                Dbconnect connect = new Dbconnect();

                using (MySqlConnection con = connect.getconnection)
                {
                    con.Open();

                    // Vérifier si la direction existe déjà
                    string checkQuery = "SELECT COUNT(*) FROM direction WHERE nomDirection = @nomDirection AND id_entreprise = @idEntreprise";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@nomDirection", nomDirection);
                        checkCmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count == 0)
                        {
                            // Insérer la nouvelle direction
                            string insertQuery = "INSERT INTO direction (nomDirection, id_entreprise) VALUES (@nomDirection, @idEntreprise)";
                            using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, con))
                            {
                                insertCmd.Parameters.AddWithValue("@nomDirection", nomDirection);
                                insertCmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);

                                insertCmd.ExecuteNonQuery();
                            }

                            CustomMessageBox.Show("Direction enregistrée avec succès !", "Succès", CustomMessageBox.MessageType.Success);
                        }
                        else
                        {
                            CustomMessageBox.Show($"La direction '{nomDirection}' existe déjà pour l'entreprise sélectionnée.", "Information", CustomMessageBox.MessageType.Warning);
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
        public DataTable GetDirectionList()
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
                    d.id_direction  AS `ID`,
                    e.nomEntreprise AS `Entreprise`,
                    d.nomDirection  AS `Direction`,
                    d.id_entreprise AS `ID_Entreprise`
                FROM direction d
                INNER JOIN entreprise e ON e.id_entreprise = d.id_entreprise
                ORDER BY e.nomEntreprise, d.nomDirection;";

                con.Open();
                da.Fill(table);
            }

            return table;
        }

        /////////////////////////////////////////////////////////////////////////////////

        private readonly Dbconnect connect = new Dbconnect();

        // Recherche par mot-clé (nom de la direction et/ou nom de l'entreprise)
        public DataTable rechercheDirection(string recherchedata)
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

        public DataTable RechercheDirectionConcat(string recherche)
        {
            var table = new DataTable();
            string filtre = (recherche ?? string.Empty).Trim();

            string sql = @"
        SELECT
            d.id_direction  AS `ID`,
            e.id_entreprise AS `ID_Entreprise`,
            e.nomEntreprise AS `Entreprise`,
            d.nomDirection  AS `Direction`
        FROM direction d
        INNER JOIN entreprise e ON e.id_entreprise = d.id_entreprise
        WHERE CONCAT_WS(' ',
                d.nomDirection,
                e.nomEntreprise,
                e.activite,
                e.sigle,
                e.telephone
            ) LIKE @q
        ORDER BY e.nomEntreprise, d.nomDirection;";

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


        // Surcharge avec entreprise (utilisée par ModifierDirectionForm)
        public static void ModifierDirection(int idDirection, string nomDirection, int idEntreprise)
        {
            if (idDirection <= 0)
            {
                CustomMessageBox.Show("Identifiant de direction invalide.", "Information",
                                CustomMessageBox.MessageType.Info);
                return;
            }

            if (string.IsNullOrWhiteSpace(nomDirection))
            {
                CustomMessageBox.Show("Veuillez saisir le nom de la direction.", "Information",
                                CustomMessageBox.MessageType.Info);
                return;
            }

            try
            {
                var connect = new Dbconnect();
                using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
                {
                    con.Open();

                    string nom = nomDirection.Trim();

                    // Vérifier l'unicité du nom dans la nouvelle entreprise
                    const string checkSql = @"
                    SELECT COUNT(*)
                    FROM direction
                    WHERE id_entreprise = @idEntreprise
                      AND id_direction <> @id
                      AND nomDirection = @nom;";

                    using (var checkCmd = new MySqlCommand(checkSql, con))
                    {
                        checkCmd.Parameters.Add("@id", MySqlDbType.Int32).Value = idDirection;
                        checkCmd.Parameters.Add("@idEntreprise", MySqlDbType.Int32).Value = idEntreprise;
                        checkCmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;

                        int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (exists > 0)
                        {
                            CustomMessageBox.Show($"Une autre direction porte déjà le nom « {nom} » dans cette entreprise.",
                                            "Doublon", CustomMessageBox.MessageType.Warning);
                            return;
                        }
                    }

                    // Mettre à jour le nom ET l'entreprise
                    const string updateSql = @"
                    UPDATE direction
                    SET nomDirection = @nom, id_entreprise = @idEntreprise
                    WHERE id_direction = @id;";

                    using (var upd = new MySqlCommand(updateSql, con))
                    {
                        upd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;
                        upd.Parameters.Add("@idEntreprise", MySqlDbType.Int32).Value = idEntreprise;
                        upd.Parameters.Add("@id", MySqlDbType.Int32).Value = idDirection;

                        int rows = upd.ExecuteNonQuery();
                        if (rows > 0)
                            CustomMessageBox.Show("Direction modifiée avec succès !", "Succès",
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

        // Surcharge sans entreprise (compatibilité avec code existant)
        public static void ModifierDirection(int idDirection, string nomDirection)
        {
            if (idDirection <= 0)
            {
                CustomMessageBox.Show("Identifiant de direction invalide.", "Information",
                                CustomMessageBox.MessageType.Info);
                return;
            }

            if (string.IsNullOrWhiteSpace(nomDirection))
            {
                CustomMessageBox.Show("Veuillez saisir le nom de la direction.", "Information",
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

                    string nom = nomDirection.Trim();

                    // 1) Vérifier l'unicité du nom DANS LA MÊME ENTREPRISE (sans changer l'entreprise)
                    const string checkSql = @"
                    SELECT COUNT(*)
                    FROM direction d
                    JOIN direction d2 ON d2.id_entreprise = d.id_entreprise
                    WHERE d.id_direction = @id
                      AND d2.id_direction <> @id
                      AND d2.nomDirection = @nom;";

                    using (var checkCmd = new MySqlCommand(checkSql, con))
                    {
                        checkCmd.Parameters.Add("@id", MySqlDbType.Int32).Value = idDirection;
                        checkCmd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;

                        int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (exists > 0)
                        {
                            CustomMessageBox.Show($"Une autre direction porte déjà le nom « {nom} » dans la même entreprise.",
                                            "Doublon", CustomMessageBox.MessageType.Warning);
                            return;
                        }
                    }

                    // 2) Mettre à jour UNIQUEMENT le nom
                    const string updateSql = @"
                    UPDATE direction
                    SET nomDirection = @nom
                    WHERE id_direction = @id;";

                    using (var upd = new MySqlCommand(updateSql, con))
                    {
                        upd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;
                        upd.Parameters.Add("@id", MySqlDbType.Int32).Value = idDirection;

                        int rows = upd.ExecuteNonQuery();
                        if (rows > 0)
                            CustomMessageBox.Show("Direction modifiée avec succès !", "Succès",
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

        public static bool SupprimerDirection(int idDirection)
        {
            if (idDirection <= 0)
            {
                CustomMessageBox.Show("Identifiant de direction invalide.", "Information",
                                CustomMessageBox.MessageType.Info);
                return false;
            }

            try
            {
                var connect = new Dbconnect();

                // ⚠️ Important : créer une nouvelle connexion depuis la connection string
                using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
                using (var cmd = new MySqlCommand("DELETE FROM direction WHERE id_direction = @id;", con))
                {
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = idDirection;

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        CustomMessageBox.Show("Direction supprimée avec succès.", "Succès",
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
                    "Impossible de supprimer cette direction car elle est utilisée ailleurs (contrainte de clé étrangère). " +
                    "Vérifiez les enregistrements liés (départements, services, etc.).",
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


        // <summary>Retourne les directions d'une entreprise.</summary>
        public static DataTable ListerParEntrepriseDirection(int idEntreprise)
        {
            var table = new DataTable();
            var db = new Dbconnect();

            // Si getconnection renvoie une *chaîne*, utilisez: new MySqlConnection(db.getconnection)
            using (var con = new MySqlConnection(db.getconnection.ConnectionString))
            using (var cmd = new MySqlCommand(@"
            SELECT id_direction, nomDirection
            FROM direction
            WHERE id_entreprise = @idEntreprise
            ORDER BY nomDirection;", con))
            using (var da = new MySqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);
                con.Open();
                da.Fill(table);
            }
            return table;
        }

        /// <summary>Bind un ComboBox avec les directions d'une entreprise.</summary>
        public static void BindComboDirection(ComboBox combo, int idEntreprise, int? idASelectionner = null, bool placeholder = true)
        {
            var dt = ListerParEntrepriseDirection(idEntreprise);

            if (placeholder)
            {
                var row = dt.NewRow();
                row["id_direction"] = DBNull.Value;
                row["nomDirection"] = "Sélectionner direction";
                dt.Rows.InsertAt(row, 0);
            }

            combo.DataSource = dt;
            combo.DisplayMember = "nomDirection";
            combo.ValueMember = "id_direction";
            combo.SelectedIndex = placeholder ? 0 : -1;

            if (idASelectionner.HasValue)
                combo.SelectedValue = idASelectionner.Value;
        }

        /// <summary>Recharge les directions à partir d'un ComboBox entreprise déjà bindé.</summary>
        public static void BindComboDepuisEntreprise(ComboBox comboDirection, ComboBox comboEntreprise, int? idASelectionner = null, bool placeholder = true)
        {
            int? idEnt = EntrepriseClass.GetIdEntrepriseSelectionnee(comboEntreprise);
            if (!idEnt.HasValue)
            {
                Categorie.ClearCombo(comboDirection); // on réutilise le clear générique
                return;
            }
            BindComboDirection(comboDirection, idEnt.Value, idASelectionner, placeholder);
        }

        /// <summary>Déduit l'id_direction sélectionné dans un ComboBox.</summary>
        public static int? GetIdDirectionSelectionnee(ComboBox combo)
        {
            var val = combo?.SelectedValue;
            if (val == null || val == DBNull.Value) return null;

            if (val is int i) return i;
            if (val is long l) return (int)l;
            if (val is short s) return (int)s;

            return int.TryParse(val.ToString(), out var id) ? (int?)id : null;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////



        public static void ChargerDirections(
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
            SELECT id_direction, nomDirection
            FROM direction
            WHERE id_entreprise = @idEnt
            ORDER BY nomDirection;";

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
                            row["id_direction"] = 0;
                            row["nomDirection"] = "--- Sélectionner direction ---";
                            dt.Rows.InsertAt(row, 0);
                        }

                        combo.ValueMember = "id_direction";
                        combo.DisplayMember = "nomDirection";
                        combo.DataSource = dt;

                        if (idSelection.HasValue && idSelection.Value > 0)
                        {
                            bool exists = dt.Select("id_direction = " + idSelection.Value).Length > 0;
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
        /// Récupère les directions d'une entreprise sous forme de DataTable
        /// Utilisé pour le filtrage intelligent des ComboBox
        /// </summary>
        public static DataTable GetDirectionsByEntreprise(int idEntreprise)
        {
            var connect = new Dbconnect();
            using (var con = connect.getconnection)
            {
                con.Open();
                const string sql = @"
                    SELECT id_direction, nomDirection
                    FROM direction
                    WHERE id_entreprise = @idEnt
                    ORDER BY nomDirection;";

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
