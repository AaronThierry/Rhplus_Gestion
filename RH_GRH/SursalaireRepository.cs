using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace RH_GRH
{
    // ------------------ REPOSITORY ------------------
    public static class SursalaireRepository
    {
        // --- AJOUT ---
        public static void Ajouter(Sursalaire sursalaire)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();
                using (var tx = con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        // Vérifier si l'employé a déjà un sursalaire
                        const string checkSql = "SELECT COUNT(*) FROM sursalaire WHERE id_personnel = @id_personnel;";
                        using (var cmdCheck = new MySqlCommand(checkSql, con, tx))
                        {
                            cmdCheck.Parameters.AddWithValue("@id_personnel", sursalaire.IdPersonnel);
                            var count = Convert.ToInt32(cmdCheck.ExecuteScalar());
                            if (count > 0)
                            {
                                throw new InvalidOperationException("Cet employé a déjà un sursalaire. Veuillez le modifier au lieu d'en créer un nouveau.");
                            }
                        }

                        const string insertSql = @"
INSERT INTO sursalaire (id_personnel, nom, description, montant)
VALUES (@id_personnel, @nom, @description, @montant);";

                        using (var cmd = new MySqlCommand(insertSql, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@id_personnel", sursalaire.IdPersonnel);
                            cmd.Parameters.AddWithValue("@nom", sursalaire.Nom);
                            cmd.Parameters.AddWithValue("@description", sursalaire.Description ?? string.Empty);
                            cmd.Parameters.AddWithValue("@montant", sursalaire.Montant);
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
        public static List<Sursalaire> ListerParPersonnel(int idPersonnel)
        {
            var list = new List<Sursalaire>();
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();

                const string sql = @"
SELECT id_sursalaire, id_personnel, nom, description, montant
FROM sursalaire
WHERE id_personnel = @id_personnel
ORDER BY nom;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id_personnel", idPersonnel);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Sursalaire
                            {
                                IdSursalaire = Convert.ToInt32(reader["id_sursalaire"]),
                                IdPersonnel = Convert.ToInt32(reader["id_personnel"]),
                                Nom = reader["nom"]?.ToString() ?? string.Empty,
                                Description = reader["description"]?.ToString() ?? string.Empty,
                                Montant = reader["montant"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["montant"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        // --- LISTE COMPLÈTE POUR AFFICHAGE ---
        public static System.Data.DataTable GetSursalaireList()
        {
            var dt = new System.Data.DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Entreprise", typeof(string));
            dt.Columns.Add("Matricule", typeof(string));
            dt.Columns.Add("Employe", typeof(string));
            dt.Columns.Add("Nom", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Montant", typeof(string));

            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();

                const string sql = @"
SELECT
    s.id_sursalaire,
    e.nomEntreprise AS entreprise,
    p.matricule,
    p.nomPrenom AS employe,
    s.nom,
    s.description,
    s.montant
FROM sursalaire s
INNER JOIN personnel p ON s.id_personnel = p.id_personnel
INNER JOIN entreprise e ON p.id_entreprise = e.id_entreprise
ORDER BY e.nomEntreprise, p.nomPrenom, s.nom;";

                using (var cmd = new MySqlCommand(sql, con))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dt.Rows.Add(
                            reader["id_sursalaire"],
                            reader["entreprise"],
                            reader["matricule"],
                            reader["employe"],
                            reader["nom"],
                            reader["description"],
                            string.Format("{0:N0} F CFA", reader["montant"])
                        );
                    }
                }
            }
            return dt;
        }

        // --- RÉCUPÉRER UN SURSALAIRE PAR ID ---
        public static Sursalaire GetById(int idSursalaire)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();

                const string sql = @"
SELECT id_sursalaire, id_personnel, nom, description, montant
FROM sursalaire
WHERE id_sursalaire = @id_sursalaire
LIMIT 1;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id_sursalaire", idSursalaire);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Sursalaire
                            {
                                IdSursalaire = Convert.ToInt32(reader["id_sursalaire"]),
                                IdPersonnel = Convert.ToInt32(reader["id_personnel"]),
                                Nom = reader["nom"]?.ToString() ?? string.Empty,
                                Description = reader["description"]?.ToString() ?? string.Empty,
                                Montant = reader["montant"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["montant"])
                            };
                        }
                    }
                }
            }
            return null;
        }

        // --- SUPPRESSION ---
        public static void Supprimer(int idSursalaire)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();
                const string sql = "DELETE FROM sursalaire WHERE id_sursalaire = @id_sursalaire;";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id_sursalaire", idSursalaire);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // --- MODIFICATION ---
        public static void Modifier(Sursalaire sursalaire)
        {
            if (sursalaire == null) throw new ArgumentNullException(nameof(sursalaire));
            if (sursalaire.IdSursalaire <= 0) throw new ArgumentException("IdSursalaire invalide.");

            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();
                using (var tx = con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        const string sqlUpdate = @"
UPDATE sursalaire
SET nom = @nom,
    description = @description,
    montant = @montant
WHERE id_sursalaire = @id;";

                        using (var cmd = new MySqlCommand(sqlUpdate, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", sursalaire.IdSursalaire);
                            cmd.Parameters.AddWithValue("@nom", sursalaire.Nom);
                            cmd.Parameters.AddWithValue("@description", sursalaire.Description ?? string.Empty);
                            cmd.Parameters.AddWithValue("@montant", sursalaire.Montant);
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

        // --- TOTAL DES SURSALAIRES PAR PERSONNEL ---
        public static decimal CalculerTotalParPersonnel(int idPersonnel)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();

                const string sql = @"
SELECT COALESCE(SUM(montant), 0) AS total
FROM sursalaire
WHERE id_personnel = @id_personnel;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id_personnel", idPersonnel);
                    var result = cmd.ExecuteScalar();
                    return result == DBNull.Value ? 0m : Convert.ToDecimal(result);
                }
            }
        }
    }
}
