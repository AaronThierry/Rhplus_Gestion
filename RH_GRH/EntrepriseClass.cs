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
    internal class EntrepriseClass
    {
        Dbconnect connect = new Dbconnect();
        //Enregistrer
        public bool insertEntreprise(string nomEntreprise, string formeJuridique, string sigle, string activite, string adressePhysique, string adressePostale, string telephone, string commune, string quartier, string rue, string lot, string centreImpots, string numeroIfu, string numeroCnss, string codeActivite, string regimeFiscal, string registreCommerce, string numeroBancaire, decimal? tpa, byte[] logoEntreprise)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `entreprise`(`nomEntreprise`, `forme_juridique`, `sigle`, `activite`, `adresse_physique`, `adresse_postale`, `telephone`, `commune`, `quartier`, `rue`, `lot`, `centre_impots`, `numero_ifu`, `numero_cnss`, `code_activite`, `regime_fiscal`, `registre_commerce`, `numero_bancaire`, `tpa`, `logo_entreprise`) VALUES (@nomEntreprise, @forme_juridique, @sigle, @activite, @adresse_physique, @adresse_postale, @telephone, @commune, @quartier, @rue, @lot, @centre_impots, @numero_ifu, @numero_cnss, @code_activite, @regime_fiscal, @registre_commerce, @numero_bancaire, @tpa, @logo_entreprise)", connect.getconnection);
            command.Parameters.Add("@nomEntreprise", MySqlDbType.VarChar).Value = nomEntreprise;
            command.Parameters.Add("@forme_juridique", MySqlDbType.VarChar).Value = formeJuridique;
            command.Parameters.Add("@sigle", MySqlDbType.VarChar).Value = sigle;
            command.Parameters.Add("@activite", MySqlDbType.VarChar).Value = activite;
            command.Parameters.Add("@adresse_physique", MySqlDbType.VarChar).Value = adressePhysique;
            command.Parameters.Add("@adresse_postale", MySqlDbType.VarChar).Value = adressePostale;
            command.Parameters.Add("@telephone", MySqlDbType.VarChar).Value = telephone;
            command.Parameters.Add("@commune", MySqlDbType.VarChar).Value = commune;
            command.Parameters.Add("@quartier", MySqlDbType.VarChar).Value = quartier;
            command.Parameters.Add("@rue", MySqlDbType.VarChar).Value = rue;
            command.Parameters.Add("@lot", MySqlDbType.VarChar).Value = lot;
            command.Parameters.Add("@centre_impots", MySqlDbType.VarChar).Value = centreImpots;
            command.Parameters.Add("@numero_ifu", MySqlDbType.VarChar).Value = numeroIfu;
            command.Parameters.Add("@numero_cnss", MySqlDbType.VarChar).Value = numeroCnss;
            command.Parameters.Add("@code_activite", MySqlDbType.VarChar).Value = codeActivite;
            command.Parameters.Add("@regime_fiscal", MySqlDbType.VarChar).Value = regimeFiscal;
            command.Parameters.Add("@registre_commerce", MySqlDbType.VarChar).Value = registreCommerce;
            command.Parameters.Add("@numero_bancaire", MySqlDbType.VarChar).Value = numeroBancaire;
            command.Parameters.Add("@tpa", MySqlDbType.Decimal).Value = tpa;
            command.Parameters.Add("@logo_entreprise", MySqlDbType.Blob).Value = logoEntreprise;


            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }


        //Modifier
        public bool updateEntreprise(int id, string nomEntreprise, string formeJuridique, string sigle, string activite, string adressePhysique, string adressePostale, string telephone, string commune, string quartier, string rue, string lot, string centreImpots, string numeroIfu, string numeroCnss, string codeActivite, string regimeFiscal, string registreCommerce, string numeroBancaire, decimal? tpa, byte[] logoEntreprise)
        {
            MySqlCommand command = new MySqlCommand(@"
        UPDATE entreprise SET 
            nomEntreprise = @nomEntreprise,
            forme_juridique = @forme_juridique,
            sigle = @sigle,
            activite = @activite,
            adresse_physique = @adresse_physique,
            adresse_postale = @adresse_postale,
            telephone = @telephone,
            commune = @commune,
            quartier = @quartier,
            rue = @rue,
            lot = @lot,
            centre_impots = @centre_impots,
            numero_ifu = @numero_ifu,
            numero_cnss = @numero_cnss,
            code_activite = @code_activite,
            regime_fiscal = @regime_fiscal,
            registre_commerce = @registre_commerce,
            numero_bancaire = @numero_bancaire,
            tpa = @tpa,
            logo_entreprise = @logo_entreprise
        WHERE id_entreprise = @id", connect.getconnection);

            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@nomEntreprise", MySqlDbType.VarChar).Value = nomEntreprise;
            command.Parameters.Add("@forme_juridique", MySqlDbType.VarChar).Value = formeJuridique;
            command.Parameters.Add("@sigle", MySqlDbType.VarChar).Value = sigle;
            command.Parameters.Add("@activite", MySqlDbType.VarChar).Value = activite;
            command.Parameters.Add("@adresse_physique", MySqlDbType.VarChar).Value = adressePhysique;
            command.Parameters.Add("@adresse_postale", MySqlDbType.VarChar).Value = adressePostale;
            command.Parameters.Add("@telephone", MySqlDbType.VarChar).Value = telephone;
            command.Parameters.Add("@commune", MySqlDbType.VarChar).Value = commune;
            command.Parameters.Add("@quartier", MySqlDbType.VarChar).Value = quartier;
            command.Parameters.Add("@rue", MySqlDbType.VarChar).Value = rue;
            command.Parameters.Add("@lot", MySqlDbType.VarChar).Value = lot;
            command.Parameters.Add("@centre_impots", MySqlDbType.VarChar).Value = centreImpots;
            command.Parameters.Add("@numero_ifu", MySqlDbType.VarChar).Value = numeroIfu;
            command.Parameters.Add("@numero_cnss", MySqlDbType.VarChar).Value = numeroCnss;
            command.Parameters.Add("@code_activite", MySqlDbType.VarChar).Value = codeActivite;
            command.Parameters.Add("@regime_fiscal", MySqlDbType.VarChar).Value = regimeFiscal;
            command.Parameters.Add("@registre_commerce", MySqlDbType.VarChar).Value = registreCommerce;
            command.Parameters.Add("@numero_bancaire", MySqlDbType.VarChar).Value = numeroBancaire;
            command.Parameters.Add("@tpa", MySqlDbType.Decimal).Value = tpa.HasValue ? tpa.Value : (object)DBNull.Value;
            command.Parameters.Add("@logo_entreprise", MySqlDbType.Blob).Value = logoEntreprise ?? (object)DBNull.Value;

            connect.openConnect();
            bool success = command.ExecuteNonQuery() == 1;
            connect.closeConnect();

            return success;
        }





        //Recuperer et afficher
        public DataTable getEntrepriseList()
        {
            // MySqlCommand command = new MySqlCommand("SELECT id_entreprise AS N°,nomEntreprise AS Nom Entreprise ,activite,adresse_physique AS Adresse physique ,telephone,logo_entreprise  FROM `entreprise`", connect.getconnection);
            MySqlCommand command = new MySqlCommand("SELECT id_entreprise AS `N°`, nomEntreprise AS `Nom Entreprise`, activite AS Activite , adresse_physique AS `Adresse physique`, telephone AS Telephone, logo_entreprise AS `Logo` FROM entreprise", connect.getconnection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }





        // Créer une fonction pour exécuter une requête COUNT (total, hommes, femmes, etc.)
        public string exeCount(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connect.getconnection);
            connect.openConnect(); // ouvre la connexion

            string count = command.ExecuteScalar()?.ToString(); // exécute la requête
            connect.closeConnect(); // ferme la connexion

            return count;
        }

        // Récupère le nombre total d'entreprise
        public string totalEntreprie()
        {
            return exeCount("SELECT COUNT(*) FROM entreprise");
        }


        //Fonction rechercher
        public DataTable rechercheEntreprise(string recherchedata)
        {
            string query = @"SELECT id_entreprise AS `N°`,
                            nomEntreprise AS `Nom Entreprise`,
                            activite AS Activite,
                            adresse_physique AS `Adresse physique`,
                            telephone AS Telephone,
                            logo_entreprise AS `Logo`
                     FROM entreprise
                     WHERE CONCAT(nomEntreprise, activite,sigle,adresse_physique,telephone) LIKE @recherche";

            MySqlCommand command = new MySqlCommand(query, connect.getconnection);
            command.Parameters.AddWithValue("@recherche", "%" + recherchedata + "%");

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }


        public static bool entrepriseExiste(string nomEntreprise, string sigle)
        {
            Dbconnect connect = new Dbconnect();

            MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM entreprise WHERE nomEntreprise = @nom OR sigle = @sigle", connect.getconnection);
            command.Parameters.Add("@nom", MySqlDbType.VarChar).Value = nomEntreprise;
            command.Parameters.Add("@sigle", MySqlDbType.VarChar).Value = sigle;

            connect.openConnect();
            int count = Convert.ToInt32(command.ExecuteScalar());
            connect.closeConnect();

            return count > 0;
        }



        public static void ChargerEntreprises(
          ComboBox combo,
          int? idASelectionner = null,
          bool ajouterPlaceholder = true)
        {
            Dbconnect connect = new Dbconnect();
            var dt = new DataTable();

            using (var con = new MySqlConnection(connect.getconnection.ConnectionString))
            using (var cmd = new MySqlCommand(
                "SELECT id_entreprise, nomEntreprise FROM entreprise ORDER BY nomEntreprise", con))
            using (var da = new MySqlDataAdapter(cmd))
            {
                con.Open();
                da.Fill(dt);
            }

            if (ajouterPlaceholder)
            {
                var row = dt.NewRow();
                row["id_entreprise"] = DBNull.Value;
                row["nomEntreprise"] = "--- Sélectionner une entreprise ---";
                dt.Rows.InsertAt(row, 0);
            }

            combo.DataSource = dt;
            combo.DisplayMember = "nomEntreprise";
            combo.ValueMember = "id_entreprise";

            if (idASelectionner.HasValue)
            {
                combo.SelectedValue = idASelectionner.Value;
            }
            else
            {
                combo.SelectedIndex = ajouterPlaceholder ? 0 : -1;
            }
        }

        // ✅ Méthode pour récupérer l'ID de l'entreprise sélectionnée
        public static int? GetIdEntrepriseSelectionnee(ComboBox combo)
        {
            if (combo.SelectedValue == null || combo.SelectedValue == DBNull.Value)
                return null;

            return Convert.ToInt32(combo.SelectedValue);
        }




        public static byte[] GetLogoEntreprise(int idEntreprise)
        {
            const string sql = "SELECT logo_entreprise FROM entreprise WHERE id_entreprise = @id";

            var connect = new Dbconnect();
            using (var con = connect.getconnection)
            {
                con.Open();
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", idEntreprise);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() && !reader.IsDBNull(0))
                        {
                            return (byte[])reader["logo_entreprise"];
                        }
                    }
                }
            }

            return null; // Aucun logo trouvé
        }



    }


}
