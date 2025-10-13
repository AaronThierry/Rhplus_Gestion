using MySql.Data.MySqlClient;
using System;

namespace RH_GRH
{

    public class EmployeService
    {
        // Méthode pour récupérer les informations d'un employé par son ID
        public static Employe GetEmployeDetails(int idEmploye)
        {
            Employe employe = null;

            var connect = new Dbconnect();
            using (var con = connect.getconnection)
            {
                con.Open();

                string sql = @"SELECT
                p.id_personnel,
                p.nomPrenom,
                p.civilite,
                p.matricule,
                p.telephone,
                p.adresse,
                p.contrat,
                p.modePayement,
                p.identification,
                p.cadre,
                p.date_entree,
                p.date_sortie,
                p.poste,
                p.id_entreprise,
                p.id_service,
                p.id_categorie,
                p.heureContrat,
                p.jourContrat,
                p.typeContrat,
                p.salairemoyen,
                c.montant           AS MontantCategorie,
                c.nomCategorie AS categorie,          -- << nom de la catégorie
                s.nomService   AS service,            -- (optionnel) libellé service
                e.nomEntreprise AS entreprise,        -- (optionnel) libellé entreprise
                e.tpa                               -- (optionnel) taux TPA entreprise
            FROM personnel p
            LEFT JOIN categorie  c ON c.id_categorie  = p.id_categorie
            LEFT JOIN service    s ON s.id_service    = p.id_service
            LEFT JOIN entreprise e ON e.id_entreprise = p.id_entreprise
            WHERE p.id_personnel = @id
            LIMIT 1;
            ";

                using (var cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", idEmploye);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employe = new Employe(
                                id: reader.GetInt32("id_personnel"),
                                nom: reader.GetString("nomPrenom"),
                                dateEntree: reader.GetDateTime("date_entree"),
                                dateSortie: reader.IsDBNull(reader.GetOrdinal("date_sortie")) ? null : (DateTime?)reader.GetDateTime("date_sortie"),
                                poste: reader.GetString("poste"),
                                matricule: reader.GetString("matricule"),
                                telephone: reader.GetString("telephone"),
                                adresse: reader.GetString("adresse"),
                                contrat: reader.GetString("contrat"),
                                modePaiement: reader.GetString("modePayement"),
                                identification: reader.GetString("identification"),
                                cadre: reader.GetString("cadre"),
                                entreprise: reader.GetInt32("id_entreprise"),
                                service: reader.GetString("service"),
                                categorie:reader.GetString("categorie"),
                                montant:reader.GetDouble("MontantCategorie"),
                                typeContrat:reader.GetString("typecontrat"),
                                heureContrat:reader.GetInt32("heureContrat"),
                                jourContrat:reader.GetInt32("jourContrat"),
                                salairemoyen:reader.GetDecimal("salairemoyen"),
                                tpa:reader.GetDecimal("tpa"),
                                civilite: reader.GetString("civilite")
                            );
                        }
                    }
                }
            }

            return employe;
        }
    }

}
