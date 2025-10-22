using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace RH_GRH
{
    public class IndemniteClass
    {
        // ----------------- Enregistrement -----------------
        public static void EnregistrerIndemnite(int idPersonnel, IndemniteType type, decimal valeur)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)   // <-- ici on définit 'con'
            {
                con.Open();

                // Récupère le libellé du type
                var typeLib = ToDbType(type);

                // Vérification unicité : existe déjà pour cet employé ?
                if (ExisteDejaPourType(con, null, idPersonnel, typeLib))
                    throw new InvalidOperationException("Cet employé possède déjà une indemnité de ce type.");

                // Insertion si tout est OK
                const string sqlInsert = @"
            INSERT INTO indemnite (id_personnel, type, valeur)
            VALUES (@idPersonnel, @type, @valeur);";

                using (var cmd = new MySqlCommand(sqlInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idPersonnel", idPersonnel);
                    cmd.Parameters.AddWithValue("@type", typeLib);
                    cmd.Parameters.AddWithValue("@valeur", valeur);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        


        private static bool ExisteDejaPourType(MySqlConnection con, MySqlTransaction tx, int idPers, string typeLibelle)
        {
            const string sql = @"SELECT COUNT(*) FROM indemnite WHERE id_personnel = @p AND type = @t;";

             var cmd = new MySqlCommand(sql, con, tx);
            cmd.Parameters.AddWithValue("@p", idPers);
            cmd.Parameters.AddWithValue("@t", typeLibelle);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }



        // ----------------- Modification (par id) -----------------
        public static void ModifierIndemnite(int idIndemnite, IndemniteType type, decimal valeur)
        {
            if (idIndemnite <= 0) throw new ArgumentException("Id d'indemnité invalide.", nameof(idIndemnite));

            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();

                const string sqlUpdate = @"
UPDATE indemnite
SET type = @type,
    valeur = @valeur
WHERE id_indemnite = @id;";

                using (var cmd = new MySqlCommand(sqlUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@id", idIndemnite);
                    cmd.Parameters.AddWithValue("@type", ToDbType(type));
                    cmd.Parameters.AddWithValue("@valeur", valeur);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ----------------- Suppression (par id) -----------------
        public static void SupprimerIndemnite(int idIndemnite)
        {
            if (idIndemnite <= 0) throw new ArgumentException("Id d'indemnité invalide.", nameof(idIndemnite));

            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();

                const string sql = "DELETE FROM indemnite WHERE id_indemnite = @id;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", idIndemnite);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ----------------- Liste (DataTable) -----------------
        public static DataTable GetIndemniteList(int? idPersonnel = null)
        {
            var table = new DataTable();

            var connect = new Dbconnect();
            using (var con = connect.getconnection)
            {
                con.Open();

                const string sql = @"
SELECT
    i.id_indemnite           AS `Id`,
    p.nomPrenom              AS `Employe`,
    i.type                   AS `Type`,
    i.valeur                 AS `Valeur`
FROM indemnite i
LEFT JOIN personnel p ON p.id_personnel = i.id_personnel
WHERE (@idPersonnel IS NULL OR i.id_personnel = @idPersonnel)
ORDER BY i.type;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@idPersonnel", (object)idPersonnel ?? DBNull.Value);

                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(table);
                    }
                }
            }

            return table;
        }

        // ----------------- Helper type -> libellé DB -----------------
        private static string ToDbType(IndemniteType t)
        {
            switch (t)
            {
                case IndemniteType.LogementNumeraire: return "Logement Numeraire";
                case IndemniteType.Fonction: return "Fonction";
                case IndemniteType.TransportNumeraire: return "Transport Numeraire";
                case IndemniteType.LogementNature: return "Logement Nature";
                case IndemniteType.TransportNature: return "Transport Nature";
                case IndemniteType.DomesticiteNationaux: return "Domesticite Nationaux";
                case IndemniteType.DomesticiteEtrangers: return "Domesticite Etrangers";
                case IndemniteType.AutresAvantages: return "Autres Avantages";
                default: return "Autres Avantages";
            }
        }



        //-------------------- Recherche --------------------//

        public static DataTable RechercheIndemnite(string recherche, int? idEntreprise = null)
        {
            var table = new DataTable();
            var connect = new Dbconnect();

            using (var con = connect.getconnection)
            {
                con.Open();

                const string sql = @"
SELECT
    i.id_indemnite                  AS `Id`,
    p.nomPrenom                     AS `Employe`,
    i.type                          AS `Type`,
    i.valeur                        AS `Valeur`
FROM indemnite i
LEFT JOIN personnel  p ON p.id_personnel  = i.id_personnel
LEFT JOIN entreprise e ON e.id_entreprise = p.id_entreprise
WHERE
    (@idEnt IS NULL OR p.id_entreprise = @idEnt)
AND (
    @q = '' OR
    i.type          LIKE @like OR
    CAST(i.valeur AS CHAR) LIKE @like OR
    p.nomPrenom     LIKE @like OR
    p.matricule     LIKE @like OR
    e.nomEntreprise LIKE @like
)
ORDER BY p.nomPrenom, i.type;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@idEnt", MySqlDbType.Int32).Value = (object)idEntreprise ?? DBNull.Value;

                    var q = (recherche ?? "").Trim();
                    var like = "%" + q + "%";

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



        public static List<IndemniteList> GetIndemnitesByEmploye(int idEmploye)
        {
            List<IndemniteList> listeIndemnites = new List<IndemniteList>();

            string sql = "SELECT  id_personnel, type, valeur, taux_indemnite FROM indemnite WHERE id_personnel = @idEmploye";
            var connect = new Dbconnect();
            try
            {
                using (var con = connect.getconnection)
                {
                    con.Open();
                    using (var command = new MySqlCommand(sql, con))
                    {
                        command.Parameters.AddWithValue("@idEmploye", idEmploye);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string nomIndemnite = reader.GetString("type");
                                string montantIndemnite = reader.GetDouble("valeur").ToString();
                                string tauxIndem = reader.GetDouble("taux_indemnite").ToString();  // Ici tu peux personnaliser le taux d'indemnité si nécessaire

                                listeIndemnites.Add(new IndemniteList(idEmploye, nomIndemnite, montantIndemnite, tauxIndem));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Gère l'exception de manière appropriée
                Console.WriteLine($"Erreur lors de la récupération des indemnités : {ex.Message}");
            }

            return listeIndemnites;
        }




    }
}
