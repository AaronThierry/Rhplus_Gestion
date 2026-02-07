using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace RH_GRH
{
    // ------------------ REPOSITORY ------------------
    public static class AbonnementRepository
    {
        // --- AJOUT ---
        public static void Ajouter(Abonnement abonnement)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();
                using (var tx = con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        // Vérifier si l'employé a déjà un abonnement
                        const string checkSql = "SELECT COUNT(*) FROM abonnement WHERE id_personnel = @id_personnel;";
                        using (var cmdCheck = new MySqlCommand(checkSql, con, tx))
                        {
                            cmdCheck.Parameters.AddWithValue("@id_personnel", abonnement.IdPersonnel);
                            var count = Convert.ToInt32(cmdCheck.ExecuteScalar());
                            if (count > 0)
                            {
                                throw new InvalidOperationException("Cet employé a déjà un abonnement. Veuillez le modifier au lieu d'en créer un nouveau.");
                            }
                        }

                        const string insertSql = @"
INSERT INTO abonnement (id_personnel, nom, description, date_debut, date_fin, montant)
VALUES (@id_personnel, @nom, @description, @date_debut, @date_fin, @montant);";

                        using (var cmd = new MySqlCommand(insertSql, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@id_personnel", abonnement.IdPersonnel);
                            cmd.Parameters.AddWithValue("@nom", abonnement.Nom);
                            cmd.Parameters.AddWithValue("@description", abonnement.Description ?? string.Empty);
                            cmd.Parameters.AddWithValue("@date_debut", abonnement.DateDebut);
                            cmd.Parameters.AddWithValue("@date_fin", abonnement.DateFin);
                            cmd.Parameters.AddWithValue("@montant", abonnement.Montant);
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
        public static List<Abonnement> ListerParPersonnel(int idPersonnel)
        {
            var list = new List<Abonnement>();
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();

                const string sql = @"
SELECT id_abonnement, id_personnel, nom, description, date_debut, date_fin, montant
FROM abonnement
WHERE id_personnel = @id_personnel
ORDER BY nom;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id_personnel", idPersonnel);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Abonnement
                            {
                                IdAbonnement = Convert.ToInt32(reader["id_abonnement"]),
                                IdPersonnel = Convert.ToInt32(reader["id_personnel"]),
                                Nom = reader["nom"]?.ToString() ?? string.Empty,
                                Description = reader["description"]?.ToString() ?? string.Empty,
                                DateDebut = reader["date_debut"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["date_debut"]),
                                DateFin = reader["date_fin"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["date_fin"]),
                                Montant = reader["montant"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["montant"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        // --- LISTE COMPLÈTE POUR AFFICHAGE ---
        public static System.Data.DataTable GetAbonnementList()
        {
            var dt = new System.Data.DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Entreprise", typeof(string));
            dt.Columns.Add("Matricule", typeof(string));
            dt.Columns.Add("Employe", typeof(string));
            dt.Columns.Add("Nom", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("DateDebut", typeof(string));
            dt.Columns.Add("DateFin", typeof(string));
            dt.Columns.Add("Montant", typeof(string));

            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();

                const string sql = @"
SELECT
    a.id_abonnement,
    e.nomEntreprise AS entreprise,
    p.matricule,
    p.nomPrenom AS employe,
    a.nom,
    a.description,
    a.date_debut,
    a.date_fin,
    a.montant
FROM abonnement a
INNER JOIN personnel p ON a.id_personnel = p.id_personnel
INNER JOIN entreprise e ON p.id_entreprise = e.id_entreprise
ORDER BY e.nomEntreprise, p.nomPrenom, a.nom;";

                using (var cmd = new MySqlCommand(sql, con))
                using (
                    var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dt.Rows.Add(
                            reader["id_abonnement"],
                            reader["entreprise"],
                            reader["matricule"],
                            reader["employe"],
                            reader["nom"],
                            reader["description"],
                            reader["date_debut"] == DBNull.Value ? "" : Convert.ToDateTime(reader["date_debut"]).ToString("dd/MM/yyyy"),
                            reader["date_fin"] == DBNull.Value ? "" : Convert.ToDateTime(reader["date_fin"]).ToString("dd/MM/yyyy"),
                            string.Format("{0:N0} F CFA", reader["montant"])
                        );
                    }
                }
            }
            return dt;
        }

        // --- RÉCUPÉRER UN ABONNEMENT PAR ID ---
        public static Abonnement GetById(int idAbonnement)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();

                const string sql = @"
SELECT id_abonnement, id_personnel, nom, description, date_debut, date_fin, montant
FROM abonnement
WHERE id_abonnement = @id_abonnement
LIMIT 1;";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id_abonnement", idAbonnement);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Abonnement
                            {
                                IdAbonnement = Convert.ToInt32(reader["id_abonnement"]),
                                IdPersonnel = Convert.ToInt32(reader["id_personnel"]),
                                Nom = reader["nom"]?.ToString() ?? string.Empty,
                                Description = reader["description"]?.ToString() ?? string.Empty,
                                DateDebut = reader["date_debut"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["date_debut"]),
                                DateFin = reader["date_fin"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["date_fin"]),
                                Montant = reader["montant"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["montant"])
                            };
                        }
                    }
                }
            }
            return null;
        }

        // --- SUPPRESSION ---
        public static void Supprimer(int idAbonnement)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();
                const string sql = "DELETE FROM abonnement WHERE id_abonnement = @id_abonnement;";
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id_abonnement", idAbonnement);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // --- MODIFICATION ---
        public static void Modifier(Abonnement abonnement)
        {
            if (abonnement == null) throw new ArgumentNullException(nameof(abonnement));
            if (abonnement.IdAbonnement <= 0) throw new ArgumentException("IdAbonnement invalide.");

            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();
                using (var tx = con.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {
                    try
                    {
                        const string sqlUpdate = @"
UPDATE abonnement
SET nom = @nom,
    description = @description,
    date_debut = @date_debut,
    date_fin = @date_fin,
    montant = @montant
WHERE id_abonnement = @id;";

                        using (var cmd = new MySqlCommand(sqlUpdate, con, tx))
                        {
                            cmd.Parameters.AddWithValue("@id", abonnement.IdAbonnement);
                            cmd.Parameters.AddWithValue("@nom", abonnement.Nom);
                            cmd.Parameters.AddWithValue("@description", abonnement.Description ?? string.Empty);
                            cmd.Parameters.AddWithValue("@date_debut", abonnement.DateDebut);
                            cmd.Parameters.AddWithValue("@date_fin", abonnement.DateFin);
                            cmd.Parameters.AddWithValue("@montant", abonnement.Montant);
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

        // --- TOTAL DES ABONNEMENTS PAR PERSONNEL ---
        public static decimal CalculerTotalParPersonnel(int idPersonnel)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();

                const string sql = @"
SELECT COALESCE(SUM(montant), 0) AS total
FROM abonnement
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
