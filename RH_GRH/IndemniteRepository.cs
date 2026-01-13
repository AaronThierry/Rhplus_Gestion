using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RH_GRH
{
    // ------------------ ENUM ------------------
    public enum IndemniteType
    {
        LogementNumeraire,
        Fonction,
        TransportNumeraire,
        LogementNature,
        TransportNature,
        DomesticiteNationaux,
        DomesticiteEtrangers,
        AutresAvantages
    }

    // ------------------ MODEL ------------------
    public class Indemnite
    {
        public int IdIndemnite { get; set; }
        public int IdPersonnel { get; set; }
        public IndemniteType Type { get; set; }
        public decimal Valeur { get; set; }           // montant
    }

    // ------------------ REPOSITORY ------------------
    public static class IndemniteRepository
    {
        // --- AJOUT ---
        public static void Ajouter(Indemnite ind)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();
                using (var tx = con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        const string insertSql = @"
INSERT INTO indemnite (id_personnel, type, valeur)
VALUES (@id_personnel, @type, @valeur);";

                        using (var cmd = new MySqlCommand(insertSql, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@id_personnel", ind.IdPersonnel);
                            cmd.Parameters.AddWithValue("@type", ToDbType(ind.Type));
                            cmd.Parameters.AddWithValue("@valeur", ind.Valeur);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        // --- LISTE PAR PERSONNEL ---
        public static List<Indemnite> ListerParPersonnel(int idPersonnel)
        {
            var list = new List<Indemnite>();
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();

                const string sql = @"
SELECT id_indemnite, id_personnel, type, valeur
FROM indemnite
WHERE id_personnel = @id_personnel
ORDER BY type;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id_personnel", idPersonnel);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Indemnite
                            {
                                IdIndemnite = Convert.ToInt32(reader["id_indemnite"]),
                                IdPersonnel = Convert.ToInt32(reader["id_personnel"]),
                                Type = FromDbType(reader["type"]?.ToString()),
                                Valeur = reader["valeur"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["valeur"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        // --- RÉCUPÉRER UNE INDEMNITE PAR ID ---
        public static Indemnite GetById(int idIndemnite)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();

                const string sql = @"
SELECT id_indemnite, id_personnel, type, valeur
FROM indemnite
WHERE id_indemnite = @id_indemnite
LIMIT 1;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id_indemnite", idIndemnite);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Indemnite
                            {
                                IdIndemnite = Convert.ToInt32(reader["id_indemnite"]),
                                IdPersonnel = Convert.ToInt32(reader["id_personnel"]),
                                Type = FromDbType(reader["type"]?.ToString()),
                                Valeur = reader["valeur"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["valeur"])
                            };
                        }
                    }
                }
            }
            return null; // Indemnité non trouvée
        }

        // --- SUPPRESSION ---
        public static void Supprimer(int idIndemnite)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();
                const string sql = "DELETE FROM indemnite WHERE id_indemnite = @id_indemnite;";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id_indemnite", idIndemnite);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // --- MODIFICATION (on ne change pas l'employé) ---
        public static void Modifier(Indemnite ind)
        {
            if (ind == null) throw new ArgumentNullException(nameof(ind));
            if (ind.IdIndemnite <= 0) throw new ArgumentException("IdIndemnite invalide.");

            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();
                using (var tx = con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        const string sqlUpdate = @"
UPDATE indemnite
SET type = @type,
    valeur = @valeur
WHERE id_indemnite = @id;";

                        using (var cmd = new MySqlCommand(sqlUpdate, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", ind.IdIndemnite);
                            cmd.Parameters.AddWithValue("@type", ToDbType(ind.Type));
                            cmd.Parameters.AddWithValue("@valeur", ind.Valeur);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        // --- MAPPING TYPE <-> DB (string) ---
        public static string ToDbType(IndemniteType t)
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

        private static IndemniteType FromDbType(string s)
        {
            switch ((s ?? "").Trim())
            {
                case "Logement Numeraire": return IndemniteType.LogementNumeraire;
                case "Fonction": return IndemniteType.Fonction;
                case "Transport Numeraire": return IndemniteType.TransportNumeraire;
                case "Logement Nature": return IndemniteType.LogementNature;
                case "Transport Nature": return IndemniteType.TransportNature;
                case "Domesticite Nationaux": return IndemniteType.DomesticiteNationaux;
                case "Domesticite Etrangers": return IndemniteType.DomesticiteEtrangers;
                case "Autres Avantages": return IndemniteType.AutresAvantages;
                default: return IndemniteType.AutresAvantages;
            }
        }
    }
}

