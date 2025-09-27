﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RH_GRH
{
    internal class Categorie
    {


        public static void EnregistrerCategorie(string nomCategorie, decimal montant, int idEntreprise)
        {
            try
            {
                Dbconnect connect = new Dbconnect();

                using (MySqlConnection con = connect.getconnection)
                {
                    con.Open();

                    // Vérifier si la direction existe déjà
                    string checkQuery = "SELECT COUNT(*) FROM categorie WHERE nomCategorie = @nomCategorie AND id_entreprise = @idEntreprise";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@nomCategorie", nomCategorie);
                        checkCmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count == 0)
                        {
                            // Insérer la nouvelle direction
                            string insertQuery = "INSERT INTO categorie (nomCategorie,montant, id_entreprise) VALUES (@nomCategorie, @montant, @idEntreprise)";
                            using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, con))
                            {
                                insertCmd.Parameters.AddWithValue("@nomCategorie", nomCategorie);
                                insertCmd.Parameters.AddWithValue("@montant", montant);
                                insertCmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);

                                insertCmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Categorie enregistrée avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"La categorie '{nomCategorie}' existe déjà pour l'entreprise sélectionnée.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //////////////////////////////////////////////////////////////////////////////////

        private readonly Dbconnect _connect = new Dbconnect();

        // 1) Toutes les directions
        public DataTable GetCategorieList()
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
                    d.id_categorie  AS `ID`,
                    e.nomEntreprise AS `Entreprise`,
                    d.nomCategorie  AS `Categorie`,
                    d.montant       AS `Montant`
                FROM categorie d
                INNER JOIN entreprise e ON e.id_entreprise = d.id_entreprise
                ORDER BY e.nomEntreprise, d.nomCategorie;";

                con.Open();
                da.Fill(table);
            }

            return table;
        }

        /////////////////////////////////////////////////////////////////////////////////

        private readonly Dbconnect connect = new Dbconnect();

        // Recherche par mot-clé (nom de la direction et/ou nom de l'entreprise)
        public DataTable rechercheCategorie(string recherchedata)
        {
            var table = new DataTable();
            string filtre = (recherchedata ?? string.Empty).Trim();

            string query = @"
            SELECT
                d.id_categorie  AS `ID`,
                d.nomCategorie  AS `Categorie`,
                e.id_entreprise AS `ID Entreprise`,
                e.nomEntreprise AS `Entreprise`
            FROM categorie d
            INNER JOIN entreprise e ON e.id_entreprise = d.id_entreprise
            WHERE (@filtre = '' OR d.nomCategorie LIKE @like OR e.nomEntreprise LIKE @like)
            ORDER BY e.nomEntreprise, d.nomCategorie;";

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


        public DataTable RechercheCategorieConcat(string recherche)
        {
            var table = new DataTable();
            string filtre = (recherche ?? string.Empty).Trim();

            string sql = @"
        SELECT
            c.id_categorie  AS `ID`,
            e.nomEntreprise AS `Entreprise`,
            c.nomCategorie  AS `Categorie`,
            c.montant       AS `Montant`
        FROM categorie c
        INNER JOIN entreprise e ON e.id_entreprise = c.id_entreprise
        WHERE CONCAT_WS(' ',
                c.nomCategorie,
                e.nomEntreprise,
                e.activite,
                e.sigle,
                e.adresse_physique,
                e.telephone
            ) LIKE @q
        ORDER BY e.nomEntreprise, c.nomCategorie;";

            // ⚠️ Si getconnection retourne une chaîne, utilisez :
            // using (var con = new MySqlConnection(connect.getconnection))
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


        public static void ModifierCategorie(int idCategorie, string nomCategorie, decimal montant)
        {
            if (idCategorie <= 0)
            {
                MessageBox.Show("Identifiant de la catégorie invalide.", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(nomCategorie))
            {
                MessageBox.Show("Veuillez saisir le nom de la catégorie.", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (montant < 0m)
            {
                MessageBox.Show("Le montant doit être positif.", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var connect = new Dbconnect();

                // ⚠️ Si getconnection retourne une chaîne, utilisez: new MySqlConnection(connect.getconnection)
                using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
                {
                    con.Open();

                    string nom = nomCategorie.Trim();

                    // 1) Unicité du nom dans LA MÊME ENTREPRISE (on ne change pas l'entreprise)
                    const string checkSql = @"
                    SELECT COUNT(*)
                    FROM categorie c
                    WHERE c.id_entreprise = (
                        SELECT id_entreprise FROM categorie WHERE id_categorie = @id
                    )
                      AND c.nomCategorie = @nom
                      AND c.id_categorie <> @id;";

                    using (var check = new MySqlCommand(checkSql, con))
                    {
                        check.Parameters.Add("@id", MySqlDbType.Int32).Value = idCategorie;
                        check.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;

                        int exists = Convert.ToInt32(check.ExecuteScalar());
                        if (exists > 0)
                        {
                            MessageBox.Show($"Une autre catégorie porte déjà le nom « {nom} » dans la même entreprise.",
                                            "Doublon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // 2) Mise à jour : nom + montant
                    const string updateSql = @"
                    UPDATE categorie
                    SET nomCategorie = @nom,
                        montant      = @montant
                    WHERE id_categorie = @id;";

                    using (var upd = new MySqlCommand(updateSql, con))
                    {
                        upd.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nom;
                        upd.Parameters.Add("@montant", MySqlDbType.Decimal).Value = montant;
                        upd.Parameters.Add("@id", MySqlDbType.Int32).Value = idCategorie;

                        int rows = upd.ExecuteNonQuery();
                        if (rows > 0)
                            MessageBox.Show("Catégorie modifiée avec succès !", "Succès",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Aucune modification effectuée (enregistrement introuvable).", "Information",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification : " + ex.Message, "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /////////////////////////////////////////////////////////////////////////////////

        public static bool SupprimerCategorie(int idCategorie)
        {
            if (idCategorie <= 0)
            {
                MessageBox.Show("Identifiant de la catégorie invalide.", "Information",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            try
            {
                var connect = new Dbconnect();

                // Si getconnection retourne une chaîne, utilisez: new MySqlConnection(connect.getconnection)
                using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
                using (var cmd = new MySqlCommand("DELETE FROM categorie WHERE id_categorie = @id;", con))
                {
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = idCategorie;

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Catégorie supprimée avec succès.", "Succès",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Aucune suppression effectuée (enregistrement introuvable).", "Information",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
            }
            catch (MySqlException ex) when (ex.Number == 1451 || ex.Number == 1452)
            {
                // 1451/1452 : contrainte de clé étrangère
                MessageBox.Show(
                    "Impossible de supprimer cette catégorie car elle est référencée ailleurs (contrainte de clé étrangère). " +
                    "Vérifiez les enregistrements liés (produits/services, etc.).",
                    "Opération interdite", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression : " + ex.Message, "Erreur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////


        /// <summary>Retourne la liste des catégories d'une entreprise.</summary>
        public static DataTable ListerParEntrepriseCategorie(int idEntreprise)
        {
            var table = new DataTable();
            var db = new Dbconnect();

            // Si getconnection renvoie une *chaîne*, utilisez: new MySqlConnection(db.getconnection)
            using (var con = new MySqlConnection(db.getconnection.ConnectionString))
            using (var cmd = new MySqlCommand(@"
            SELECT id_categorie, nomCategorie
            FROM categorie
            WHERE id_entreprise = @idEntreprise
            ORDER BY nomCategorie;", con))
            using (var da = new MySqlDataAdapter(cmd))
            {
                cmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);
                con.Open();
                da.Fill(table);
            }
            return table;
        }

        /// <summary>Bind un ComboBox avec les catégories d'une entreprise.</summary>
        public static void BindComboCategorie(ComboBox combo, int idEntreprise, int? idASelectionner = null, bool placeholder = true)
        {
            var dt = ListerParEntrepriseCategorie(idEntreprise);

            if (placeholder)
            {
                var row = dt.NewRow();
                row["id_categorie"] = DBNull.Value;
                row["nomCategorie"] = "---Sélectionner catégorie";
                dt.Rows.InsertAt(row, 0);
            }

            combo.DataSource = dt;
            combo.DisplayMember = "nomCategorie";
            combo.ValueMember = "id_categorie";
            combo.SelectedIndex = placeholder ? 0 : -1;

            if (idASelectionner.HasValue)
                combo.SelectedValue = idASelectionner.Value;
        }

        /// <summary>Déduit l'id_categorie sélectionné dans un ComboBox.</summary>
        public static int? GetIdCategorieSelectionnee(ComboBox combo)
        {
            var val = combo?.SelectedValue;
            if (val == null || val == DBNull.Value) return null;

            if (val is int i) return i;
            if (val is long l) return (int)l;
            if (val is short s) return (int)s;

            return int.TryParse(val.ToString(), out var id) ? (int?)id : null;
        }

        /// <summary>Vide complètement le ComboBox (datasource + items).</summary>
        public static void ClearCombo(ComboBox combo)
        {
            combo.DataSource = null;
            combo.Items.Clear();
            combo.SelectedIndex = -1;
            combo.Text = string.Empty;
        }


        ////////////****************************///////////******************************/////////

        public static void ChargerCategories(
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
            SELECT id_categorie, nomCategorie
            FROM categorie
            WHERE id_entreprise = @idEnt
            ORDER BY nomCategorie;";

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
                            row["id_categorie"] = 0;
                            row["nomCategorie"] = "--- Sélectionner catégorie ---";
                            dt.Rows.InsertAt(row, 0);
                        }

                        combo.ValueMember = "id_categorie";
                        combo.DisplayMember = "nomCategorie";
                        combo.DataSource = dt;

                        if (idSelection.HasValue && idSelection.Value > 0)
                        {
                            bool exists = dt.Select("id_categorie = " + idSelection.Value).Length > 0;
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



    }

}

