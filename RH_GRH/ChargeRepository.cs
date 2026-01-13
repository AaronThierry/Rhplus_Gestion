using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RH_GRH
{


    public enum ChargeType { Epouse, Enfant }

    public class Charge
    {
        public int IdCharge { get; set; }
        public int IdPersonnel { get; set; }
        public ChargeType Type { get; set; }
        public string NomPrenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Identification { get; set; } // peut être null
        public string Scolarisation { get; set; }     // true = Oui, false = Non
    }

    public static class ChargeRepository
    {
        // --- AJOUT ---
        public static void Ajouter(Charge charge)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();
                using (var tx = con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        // 1) Pré-contrôles BD (robuste, même si déjà contrôlé côté UI)
                        int totalCharges = CountTotalCharges(charge.IdPersonnel, con, tx);
                        if (totalCharges >= 4)
                            throw new InvalidOperationException("Un personnel ne peut pas avoir plus de 4 charges.");

                        if (charge.Type == ChargeType.Epouse &&
                            CountCharges(charge.IdPersonnel, ChargeType.Epouse, con, tx) >= 1)
                            throw new InvalidOperationException("Un personnel ne peut avoir qu'une seule épouse.");

                        // 2) Insertion
                        const string insertSql = @"
                        INSERT INTO charge (id_personnel, type, nom_prenom, date_naissance, identification, scolarisation)
                        VALUES (@id_personnel, @type, @nom_prenom, @date_naissance, @identification, @scolarisation);";

                        using (var cmd = new MySqlCommand(insertSql, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@id_personnel", charge.IdPersonnel);
                            cmd.Parameters.AddWithValue("@type", charge.Type == ChargeType.Epouse ? "Épouse" : "Enfant");
                            cmd.Parameters.AddWithValue("@nom_prenom", charge.NomPrenom);
                            cmd.Parameters.AddWithValue("@date_naissance", charge.DateNaissance);
                            cmd.Parameters.AddWithValue("@identification", charge.Identification ?? "");
                            cmd.Parameters.AddWithValue("@scolarisation", charge.Scolarisation);
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

        // --- COMPTE PAR TYPE ---
        public static int CountCharges(int idPersonnel, ChargeType type)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();
                return CountCharges(idPersonnel, type, con, null);
            }
        }

        private static int CountCharges(int idPersonnel, ChargeType type, MySqlConnection con, MySqlTransaction tx)
        {
            const string sql = @"
SELECT COUNT(*)
FROM charge
WHERE id_personnel = @id_personnel AND type = @type;";

            using (var cmd = new MySqlCommand(sql, con, tx))
            {
                cmd.Parameters.AddWithValue("@id_personnel", idPersonnel);
                cmd.Parameters.AddWithValue("@type", type == ChargeType.Epouse ? "Épouse" : "Enfant");
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // --- COMPTE TOTAL ---
        public static int CountTotalCharges(int idPersonnel)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();
                return CountTotalCharges(idPersonnel, con, null);
            }
        }

        private static int CountTotalCharges(int idPersonnel, MySqlConnection con, MySqlTransaction tx)
        {
            const string sql = @"
SELECT COUNT(*)
FROM charge
WHERE id_personnel = @id_personnel;";

            using (var cmd = new MySqlCommand(sql, con, tx))
            {
                cmd.Parameters.AddWithValue("@id_personnel", idPersonnel);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // --- LISTE PAR PERSONNEL ---
        public static List<Charge> ListerParPersonnel(int idPersonnel)
        {
            var list = new List<Charge>();
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();

                const string sql = @"
SELECT id_charge, id_personnel, type, nom_prenom, date_naissance, identification, scolarisation
FROM charge
WHERE id_personnel = @id_personnel
ORDER BY type, nom_prenom;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id_personnel", idPersonnel);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Charge
                            {
                                IdCharge = Convert.ToInt32(reader["id_charge"]),
                                IdPersonnel = Convert.ToInt32(reader["id_personnel"]),
                                Type = reader["type"].ToString() == "Épouse" ? ChargeType.Epouse : ChargeType.Enfant,
                                NomPrenom = reader["nom_prenom"].ToString(),
                                DateNaissance = reader["date_naissance"] == DBNull.Value
                                    ? DateTime.MinValue
                                    : Convert.ToDateTime(reader["date_naissance"]),
                                Identification = reader["identification"]?.ToString(),
                                Scolarisation = reader["scolarisation"]?.ToString() 
                            });
                        }
                    }
                }
            }
            return list;
        }

        // --- RÉCUPÉRER UNE CHARGE PAR ID ---
        public static Charge GetById(int idCharge)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();

                const string sql = @"
SELECT id_charge, id_personnel, type, nom_prenom, date_naissance, identification, scolarisation
FROM charge
WHERE id_charge = @id_charge
LIMIT 1;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id_charge", idCharge);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Charge
                            {
                                IdCharge = Convert.ToInt32(reader["id_charge"]),
                                IdPersonnel = Convert.ToInt32(reader["id_personnel"]),
                                Type = reader["type"].ToString() == "Épouse" ? ChargeType.Epouse : ChargeType.Enfant,
                                NomPrenom = reader["nom_prenom"].ToString(),
                                DateNaissance = reader["date_naissance"] == DBNull.Value
                                    ? DateTime.MinValue
                                    : Convert.ToDateTime(reader["date_naissance"]),
                                Identification = reader["identification"]?.ToString(),
                                Scolarisation = reader["scolarisation"]?.ToString()
                            };
                        }
                    }
                }
            }
            return null; // Charge non trouvée
        }

        // --- SUPPRESSION ---
        public static void Supprimer(int idCharge)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();
                const string sql = "DELETE FROM charge WHERE id_charge = @id_charge;";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id_charge", idCharge);
                    cmd.ExecuteNonQuery();
                }
            }


        }        // --- MODIFICATION ---


        public static void ModifierCharge(Charge charge)
        {
            if (charge == null) throw new ArgumentNullException(nameof(charge));
            if (charge.IdCharge <= 0) throw new ArgumentException("IdCharge invalide.");

            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();
                using (var tx = con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        // Récupérer id_personnel de la charge (on ne modifie pas l'employé)
                        int idPersonnel;
                        const string sqlGetPers = @"SELECT id_personnel FROM charge WHERE id_charge = @idc LIMIT 1;";
                        using (var cmdGet = new MySqlCommand(sqlGetPers, con, tx))
                        {
                            cmdGet.Parameters.AddWithValue("@idc", charge.IdCharge);
                            object val = cmdGet.ExecuteScalar();
                            if (val == null || val == DBNull.Value)
                                throw new InvalidOperationException("Charge introuvable.");
                            idPersonnel = Convert.ToInt32(val);
                        }

                        // Normalisation du type
                        string typeTexte = (charge.Type == ChargeType.Epouse) ? "Épouse" : "Enfant";

                        // Si Épouse => scolarisation toujours NULL
                        string scolarisation = (charge.Type == ChargeType.Epouse)
                                                ? null
                                                : charge.Scolarisation; // attend "Oui", "Non" ou null

                        // ---- Contrôles métiers ----
                        // Compter charges hors celle-ci
                        const string sqlCountTotal = @"
                SELECT COUNT(*) FROM charge
                WHERE id_personnel = @idp AND id_charge <> @idc;";
                        int totalAutres;
                        using (var cmd = new MySqlCommand(sqlCountTotal, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@idp", idPersonnel);
                            cmd.Parameters.AddWithValue("@idc", charge.IdCharge);
                            totalAutres = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        if (totalAutres >= 4)
                            throw new InvalidOperationException("Un personnel ne peut pas avoir plus de 4 charges.");

                        // Vérifier épouse unique
                        if (charge.Type == ChargeType.Epouse)
                        {
                            const string sqlCountEpouse = @"
                    SELECT COUNT(*) FROM charge
                    WHERE id_personnel = @idp AND type = 'Épouse' AND id_charge <> @idc;";
                            int autresEpouses;
                            using (var cmd = new MySqlCommand(sqlCountEpouse, con, tx))
                            {
                                cmd.Parameters.AddWithValue("@idp", idPersonnel);
                                cmd.Parameters.AddWithValue("@idc", charge.IdCharge);
                                autresEpouses = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            if (autresEpouses >= 1)
                                throw new InvalidOperationException("Un personnel ne peut avoir qu'une seule épouse.");
                        }

                        // ---- UPDATE ----
                        const string sqlUpdate = @"
                UPDATE charge
                SET type           = @type,
                    nom_prenom     = @nom_prenom,
                    date_naissance = @date_naissance,
                    identification = @identification,
                    scolarisation  = @scolarisation
                WHERE id_charge    = @idc;";

                        using (var cmd = new MySqlCommand(sqlUpdate, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@idc", charge.IdCharge);
                            cmd.Parameters.AddWithValue("@type", typeTexte);
                            cmd.Parameters.AddWithValue("@nom_prenom", charge.NomPrenom ?? "");
                            cmd.Parameters.AddWithValue("@date_naissance",
                                charge.DateNaissance == DateTime.MinValue ? (object)DBNull.Value : charge.DateNaissance);
                            cmd.Parameters.AddWithValue("@identification", (object)(charge.Identification ?? ""));

                            if (scolarisation == null)
                                cmd.Parameters.AddWithValue("@scolarisation", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@scolarisation", scolarisation);

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




    }
}

