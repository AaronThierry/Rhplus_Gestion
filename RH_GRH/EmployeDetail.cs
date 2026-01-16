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
                e.sigle,
                e.nomEntreprise,
                e.telephone       AS TelephoneEntreprise,
                e.email           AS EmailEntreprise,
                e.adresse_physique        AS AdressePhysiqueEntreprise,
                e.adresse_postale        AS AdressePostaleEntreprise,
                e.responsable     AS ResponsableEntreprise,
                p.nomPrenom,
                p.civilite,
                p.sexe,
                p.matricule,
                p.telephone,
                p.adresse,
                p.contrat,
                p.modePayement,
                p.identification,
                p.cadre,
                p.date_naissance,
                p.date_entree,
                p.date_sortie,
                p.poste,
                p.id_entreprise,
                p.id_service,
                p.numerocnss,
                p.id_categorie,
                p.heureContrat,
                p.jourContrat,
                p.typeContrat,
                p.dureeContrat,
                p.salairemoyen,
                c.montant           AS MontantCategorie,
                c.nomCategorie AS categorie,          -- << nom de la catégorie
                s.nomService   AS service,            -- (optionnel) libellé service
                d.nomDirection   AS direction,            -- (optionnel) libellé Direction
                e.nomEntreprise AS entreprise,        -- (optionnel) libellé entreprise
                e.tpa                               -- (optionnel) taux TPA entreprise
            FROM personnel p
            LEFT JOIN categorie  c ON c.id_categorie  = p.id_categorie
            LEFT JOIN service    s ON s.id_service    = p.id_service
            LEFT JOIN direction  d ON d.id_direction    = p.id_direction
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
                                nom: reader.IsDBNull(reader.GetOrdinal("nomPrenom")) ? string.Empty : reader.GetString("nomPrenom"),
                                dateEntree: reader.IsDBNull(reader.GetOrdinal("date_entree")) ? DateTime.MinValue : reader.GetDateTime("date_entree"),
                                dateSortie: reader.IsDBNull(reader.GetOrdinal("date_sortie")) ? null : (DateTime?)reader.GetDateTime("date_sortie"),
                                poste: reader.IsDBNull(reader.GetOrdinal("poste")) ? string.Empty : reader.GetString("poste"),
                                matricule: reader.IsDBNull(reader.GetOrdinal("matricule")) ? string.Empty : reader.GetString("matricule"),
                                telephone: reader.IsDBNull(reader.GetOrdinal("telephone")) ? string.Empty : reader.GetString("telephone"),
                                adresse: reader.IsDBNull(reader.GetOrdinal("adresse")) ? string.Empty : reader.GetString("adresse"),
                                contrat: reader.IsDBNull(reader.GetOrdinal("contrat")) ? string.Empty : reader.GetString("contrat"),
                                modePaiement: reader.IsDBNull(reader.GetOrdinal("modePayement")) ? string.Empty : reader.GetString("modePayement"),
                                identification: reader.IsDBNull(reader.GetOrdinal("identification")) ? string.Empty : reader.GetString("identification"),
                                cadre: reader.IsDBNull(reader.GetOrdinal("cadre")) ? string.Empty : reader.GetString("cadre"),
                                entreprise: reader.GetInt32("id_entreprise"),
                                service: reader.IsDBNull(reader.GetOrdinal("service")) ? string.Empty : reader.GetString("service"),
                                categorie: reader.IsDBNull(reader.GetOrdinal("categorie")) ? string.Empty : reader.GetString("categorie"),
                                montant: reader.IsDBNull(reader.GetOrdinal("MontantCategorie")) ? 0.0 : reader.GetDouble("MontantCategorie"),
                                typeContrat: reader.IsDBNull(reader.GetOrdinal("typeContrat")) ? string.Empty : reader.GetString("typeContrat"),
                                heureContrat: reader.IsDBNull(reader.GetOrdinal("heureContrat")) ? 0 : reader.GetInt32("heureContrat"),
                                jourContrat: reader.IsDBNull(reader.GetOrdinal("jourContrat")) ? 0 : reader.GetInt32("jourContrat"),
                                salairemoyen: reader.IsDBNull(reader.GetOrdinal("salairemoyen")) ? 0m : reader.GetDecimal("salairemoyen"),
                                tpa: reader.IsDBNull(reader.GetOrdinal("tpa")) ? 0m : reader.GetDecimal("tpa"),
                                civilite: reader.IsDBNull(reader.GetOrdinal("civilite")) ? string.Empty : reader.GetString("civilite"),
                                dateNaissance: reader.IsDBNull(reader.GetOrdinal("date_naissance")) ? DateTime.MinValue : reader.GetDateTime("date_naissance"),
                                direction: reader.IsDBNull(reader.GetOrdinal("direction")) ? string.Empty : reader.GetString("direction"),
                                numeroCnssEmploye: reader.IsDBNull(reader.GetOrdinal("numerocnss")) ? string.Empty : reader.GetString("numerocnss"),
                                sexe: reader.IsDBNull(reader.GetOrdinal("sexe")) ? string.Empty : reader.GetString("sexe"),
                                dureeContrat: reader.IsDBNull(reader.GetOrdinal("dureeContrat")) ? string.Empty : reader.GetString("dureeContrat"),
                                sigle: reader.IsDBNull(reader.GetOrdinal("sigle")) ? string.Empty : reader.GetString("sigle"),
                                nomEntreprise: reader.IsDBNull(reader.GetOrdinal("nomEntreprise")) ? string.Empty : reader.GetString("nomEntreprise"),
                                telephoneEntreprise: reader.IsDBNull(reader.GetOrdinal("TelephoneEntreprise")) ? string.Empty : reader.GetString("TelephoneEntreprise"),
                                emailEntreprise: reader.IsDBNull(reader.GetOrdinal("EmailEntreprise")) ? string.Empty : reader.GetString("EmailEntreprise"),
                                adressePhysiqueEntreprise: reader.IsDBNull(reader.GetOrdinal("AdressePhysiqueEntreprise")) ? string.Empty : reader.GetString("AdressePhysiqueEntreprise"),
                                adressePostaleEntreprise: reader.IsDBNull(reader.GetOrdinal("AdressePostaleEntreprise")) ? string.Empty : reader.GetString("AdressePostaleEntreprise"),
                                responsableEntreprise: reader.IsDBNull(reader.GetOrdinal("ResponsableEntreprise")) ? string.Empty : reader.GetString("ResponsableEntreprise")




                            );
                        }
                    }
                }
            }

            return employe;
        }
    }

}
