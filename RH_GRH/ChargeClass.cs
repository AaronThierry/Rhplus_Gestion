using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH_GRH
{
    public class ChargeClass
    {
        // Méthode pour enregistrer une charge
        public static void EnregistrerCharge(int idEmploye, ChargeType type, string nomPrenom, DateTime dateNaissance, string identification, string scolarisation)
        {
            var db = new Dbconnect();
            using (var con = db.getconnection)
            {
                con.Open();

                // Vérification du nombre total de charges (Epouse + Enfant)
                int totalCharges = ChargeRepository.CountTotalCharges(idEmploye);
                if (totalCharges >= 4)
                {
                    throw new InvalidOperationException("Un employé ne peut pas avoir plus de 4 charges (épouse + enfants).");
                }

                // Vérification de l'unicité de l'épouse (si Epouse est sélectionné)
                if (type == ChargeType.Epouse && ChargeRepository.CountCharges(idEmploye, ChargeType.Epouse) >= 1)
                {
                    throw new InvalidOperationException("Cet employé a déjà une épouse enregistrée.");
                }

                // Insertion de la charge dans la base de données
                string sqlInsert = @"
                INSERT INTO charge (id_personnel, type, nom_prenom, date_naissance, identification, scolarisation)
                VALUES (@idEmploye, @type, @nomPrenom, @dateNaissance, @identification, @scolarisation);";

                using (var cmd = new MySqlCommand(sqlInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idEmploye", idEmploye);
                    cmd.Parameters.AddWithValue("@type", type == ChargeType.Epouse ? "Epouse" : "Enfant");
                    cmd.Parameters.AddWithValue("@nomPrenom", nomPrenom);
                    cmd.Parameters.AddWithValue("@dateNaissance", dateNaissance);
                    cmd.Parameters.AddWithValue("@identification", identification ?? "");
                    cmd.Parameters.AddWithValue("@scolarisation", scolarisation );

                    cmd.ExecuteNonQuery();
                }
            }
        }





        //******************************//****************************//***********************************//

        public static DataTable GetChargeList(int? idPersonnel = null)
        {
            var table = new DataTable();

            var connect = new Dbconnect();
            using (var con = connect.getconnection)
            {
                con.Open();

                const string sql = @"
            SELECT
                c.id_charge                          AS `Id`,
                p.nomPrenom                          AS `Employe`,
                c.type                                AS `Type`,
                c.nom_prenom                         AS `Nom et Prenom `,
                c.date_naissance                     AS `Date Naissance`,
                c.identification                      AS `Identification`,
                c.scolarisation                       AS `Scolarisation`
            FROM charge c
            LEFT JOIN personnel p ON p.id_personnel = c.id_personnel
            WHERE (@idPersonnel IS NULL OR c.id_personnel = @idPersonnel)
            ORDER BY c.type, c.nom_prenom;";

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



        /*******************************//****************************//***********************************/

            public static DataTable RechercheCharge(string recherche, int? idEntreprise = null)
        {
            var table = new DataTable();
            var connect = new Dbconnect();

            using (var con = connect.getconnection)
            {
                con.Open();

                const string sql = @"
SELECT
    c.id_charge                      AS `Id`,
    p.nomPrenom                      AS `Employe`,
    c.type                           AS `Type`,
    c.nom_prenom                     AS `Nom et Prenom`,
    c.date_naissance                 AS `Date Naissance`,
    c.identification                 AS `Identification`,
    c.scolarisation                  AS `Scolarisation`
FROM charge c
LEFT JOIN personnel  p ON p.id_personnel  = c.id_personnel
LEFT JOIN entreprise e ON e.id_entreprise = p.id_entreprise
WHERE
    (@idEnt IS NULL OR p.id_entreprise = @idEnt)
AND (
    @q = '' OR
    c.type           LIKE @like OR
    c.nom_prenom     LIKE @like OR
    c.identification LIKE @like OR
    c.scolarisation  LIKE @like OR
    p.nomPrenom      LIKE @like OR
    p.matricule      LIKE @like OR
    e.nomEntreprise  LIKE @like
)
ORDER BY p.nomPrenom, c.type, c.nom_prenom;";

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



        // <summary>
        /// Retourne le nombre total de charges pour un employé.
        /// Hypothèses : table = 'charges', colonnes = 'id_employe' et optionnel 'is_active'.
        /// </summary>
        public static int CountTotalCharges(int idEmploye)
        {
            if (idEmploye <= 0) return 0;

            // Si vous n'avez pas de colonne 'is_active', supprimez la condition correspondante.
            const string sql = @"
            SELECT COALESCE(COUNT(*), 0)
            FROM charge
            WHERE id_personnel = @emp;";

            var connect = new Dbconnect();
            using (var con = connect.getconnection)
            {
                con.Open();
                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@emp", idEmploye);

                    object o = cmd.ExecuteScalar();
                    int count = (o == null || o == DBNull.Value) ? 0 : Convert.ToInt32(o);

                    // Debug compact
                    Debug.WriteLine($"[CHARGES] EmpId={idEmploye} | Total={count}");
                    return count;
                }
            }
        }


    }

}
