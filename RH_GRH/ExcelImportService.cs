using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RH_GRH
{
    public class ExcelImportService
    {
        public class EmployeImportRow
        {
            public int LigneExcel { get; set; }
            public string NomPrenom { get; set; }
            public string Civilite { get; set; }
            public string Sexe { get; set; }
            public DateTime? DateNaissance { get; set; }
            public string Adresse { get; set; }
            public string Telephone { get; set; }
            public string Identification { get; set; }
            public string NomEntreprise { get; set; }
            public string NomDirection { get; set; }
            public string NomService { get; set; }
            public string NomCategorie { get; set; }
            public string Poste { get; set; }
            public string NumeroCNSS { get; set; }
            public string Contrat { get; set; }
            public string TypeContrat { get; set; }
            public string ModePayement { get; set; }
            public string Cadre { get; set; }
            public DateTime? DateEntree { get; set; }
            public DateTime? DateSortie { get; set; }
            public decimal? HeureContrat { get; set; }
            public int? JourContrat { get; set; }
            public string NumeroBancaire { get; set; }
            public string Banque { get; set; }
            public decimal? SalaireMoyen { get; set; }
            public string DureeContrat { get; set; }

            // Résultat de l'importation
            public bool Succes { get; set; }
            public string Erreur { get; set; }
            public string Matricule { get; set; }
        }

        public static List<EmployeImportRow> LireFichierExcel(string cheminFichier)
        {
            var employes = new List<EmployeImportRow>();

            string extension = Path.GetExtension(cheminFichier).ToLower();
            string connectionString = "";

            if (extension == ".xlsx")
            {
                connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={cheminFichier};Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'";
            }
            else if (extension == ".xls")
            {
                connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={cheminFichier};Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";
            }
            else
            {
                throw new Exception("Format de fichier non supporté. Utilisez .xls ou .xlsx");
            }

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    // Récupérer le nom de la première feuille
                    DataTable dtSheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string sheetName = dtSheets.Rows[0]["TABLE_NAME"].ToString();

                    // Lire les données
                    string query = $"SELECT * FROM [{sheetName}]";
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        int ligne = 2; // Ligne 1 est le header, on commence à 2
                        foreach (DataRow row in dt.Rows)
                        {
                            // Ignorer les lignes vides
                            if (row.ItemArray.All(field => field == DBNull.Value || string.IsNullOrWhiteSpace(field?.ToString())))
                                continue;

                            var employe = new EmployeImportRow
                            {
                                LigneExcel = ligne,
                                NomPrenom = LireString(row, "NomPrenom"),
                                Civilite = LireString(row, "Civilite"),
                                Sexe = LireString(row, "Sexe"),
                                DateNaissance = LireDate(row, "DateNaissance"),
                                Adresse = LireString(row, "Adresse"),
                                Telephone = LireString(row, "Telephone"),
                                Identification = LireString(row, "Identification"),
                                NomEntreprise = LireString(row, "Entreprise"),
                                NomDirection = LireString(row, "Direction"),
                                NomService = LireString(row, "Service"),
                                NomCategorie = LireString(row, "Categorie"),
                                Poste = LireString(row, "Poste"),
                                NumeroCNSS = LireString(row, "NumeroCNSS"),
                                Contrat = LireString(row, "Contrat"),
                                TypeContrat = LireString(row, "TypeContrat"),
                                ModePayement = LireString(row, "ModePayement"),
                                Cadre = LireString(row, "Cadre"),
                                DateEntree = LireDate(row, "DateEntree"),
                                DateSortie = LireDate(row, "DateSortie"),
                                HeureContrat = LireDecimal(row, "HeureContrat"),
                                JourContrat = LireInt(row, "JourContrat"),
                                NumeroBancaire = LireString(row, "NumeroBancaire"),
                                Banque = LireString(row, "Banque"),
                                SalaireMoyen = LireDecimal(row, "SalaireMoyen"),
                                DureeContrat = LireString(row, "DureeContrat")
                            };

                            employes.Add(employe);
                            ligne++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la lecture du fichier Excel : {ex.Message}", ex);
            }

            return employes;
        }

        private static string LireString(DataRow row, string columnName)
        {
            try
            {
                if (!row.Table.Columns.Contains(columnName))
                    return null;

                var value = row[columnName];
                if (value == DBNull.Value)
                    return null;

                return value.ToString().Trim();
            }
            catch
            {
                return null;
            }
        }

        private static DateTime? LireDate(DataRow row, string columnName)
        {
            try
            {
                if (!row.Table.Columns.Contains(columnName))
                    return null;

                var value = row[columnName];
                if (value == DBNull.Value)
                    return null;

                if (value is DateTime)
                    return (DateTime)value;

                if (DateTime.TryParse(value.ToString(), out DateTime result))
                    return result;

                return null;
            }
            catch
            {
                return null;
            }
        }

        private static decimal? LireDecimal(DataRow row, string columnName)
        {
            try
            {
                if (!row.Table.Columns.Contains(columnName))
                    return null;

                var value = row[columnName];
                if (value == DBNull.Value)
                    return null;

                if (decimal.TryParse(value.ToString(), out decimal result))
                    return result;

                return null;
            }
            catch
            {
                return null;
            }
        }

        private static int? LireInt(DataRow row, string columnName)
        {
            try
            {
                if (!row.Table.Columns.Contains(columnName))
                    return null;

                var value = row[columnName];
                if (value == DBNull.Value)
                    return null;

                if (int.TryParse(value.ToString(), out int result))
                    return result;

                return null;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<List<EmployeImportRow>> ImporterEmployesAsync(List<EmployeImportRow> employes, IProgress<string> progress = null)
        {
            var connect = new Dbconnect();
            var resultats = new List<EmployeImportRow>();

            foreach (var employe in employes)
            {
                progress?.Report($"Traitement ligne {employe.LigneExcel}: {employe.NomPrenom}...");

                try
                {
                    // Validation des champs obligatoires
                    if (string.IsNullOrWhiteSpace(employe.NomPrenom))
                    {
                        employe.Succes = false;
                        employe.Erreur = "Le nom et prénom sont obligatoires";
                        resultats.Add(employe);
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(employe.NomEntreprise))
                    {
                        employe.Succes = false;
                        employe.Erreur = "L'entreprise est obligatoire";
                        resultats.Add(employe);
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(employe.NomCategorie))
                    {
                        employe.Succes = false;
                        employe.Erreur = "La catégorie est obligatoire";
                        resultats.Add(employe);
                        continue;
                    }

                    using (var cn = connect.getconnection)
                    {
                        await cn.OpenAsync();

                        // Récupérer les IDs
                        int? idEntreprise = await RecupererIdEntreprise(cn, employe.NomEntreprise);
                        if (!idEntreprise.HasValue)
                        {
                            employe.Succes = false;
                            employe.Erreur = $"Entreprise '{employe.NomEntreprise}' introuvable";
                            resultats.Add(employe);
                            continue;
                        }

                        int? idDirection = null;
                        if (!string.IsNullOrWhiteSpace(employe.NomDirection))
                        {
                            idDirection = await RecupererIdDirection(cn, idEntreprise.Value, employe.NomDirection);
                            if (!idDirection.HasValue)
                            {
                                employe.Succes = false;
                                employe.Erreur = $"Direction '{employe.NomDirection}' introuvable pour l'entreprise '{employe.NomEntreprise}'";
                                resultats.Add(employe);
                                continue;
                            }
                        }

                        int? idService = null;
                        if (!string.IsNullOrWhiteSpace(employe.NomService))
                        {
                            idService = await RecupererIdService(cn, idEntreprise.Value, employe.NomService);
                            if (!idService.HasValue)
                            {
                                employe.Succes = false;
                                employe.Erreur = $"Service '{employe.NomService}' introuvable pour l'entreprise '{employe.NomEntreprise}'";
                                resultats.Add(employe);
                                continue;
                            }
                        }

                        int? idCategorie = await RecupererIdCategorie(cn, idEntreprise.Value, employe.NomCategorie);
                        if (!idCategorie.HasValue)
                        {
                            employe.Succes = false;
                            employe.Erreur = $"Catégorie '{employe.NomCategorie}' introuvable pour l'entreprise '{employe.NomEntreprise}'";
                            resultats.Add(employe);
                            continue;
                        }

                        // Générer le matricule
                        string matricule = MatriculeGenerator.GenererMatricule(idEntreprise.Value);
                        employe.Matricule = matricule;

                        // Insérer l'employé
                        string query = @"INSERT INTO personnel (
                            nomPrenom, matricule, civilite, sexe, date_naissance, adresse, telephone, poste, numerocnss, identification,
                            id_entreprise, id_direction, id_service, id_categorie,
                            contrat, typeContrat, modePayement, cadre, date_entree, date_sortie,
                            heureContrat, jourContrat, numeroBancaire, banque, salairemoyen, dureeContrat
                        ) VALUES (
                            @nomPrenom, @matricule, @civilite, @sexe, @dateNaissance, @adresse, @telephone, @poste, @numerocnss, @identification,
                            @idEntreprise, @idDirection, @idService, @idCategorie,
                            @contrat, @typeContrat, @modePayement, @cadre, @dateEntree, @dateSortie,
                            @heureContrat, @jourContrat, @numeroBancaire, @banque, @salaireMoyen, @dureeContrat
                        )";

                        using (var cmd = new MySqlCommand(query, cn))
                        {
                            cmd.Parameters.AddWithValue("@nomPrenom", employe.NomPrenom);
                            cmd.Parameters.AddWithValue("@matricule", matricule);
                            cmd.Parameters.AddWithValue("@civilite", (object)employe.Civilite ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@sexe", (object)employe.Sexe ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@dateNaissance", (object)employe.DateNaissance ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@adresse", (object)employe.Adresse ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@telephone", (object)employe.Telephone ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@poste", (object)employe.Poste ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@numerocnss", (object)employe.NumeroCNSS ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@identification", (object)employe.Identification ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@idEntreprise", idEntreprise.Value);
                            cmd.Parameters.AddWithValue("@idDirection", (object)idDirection ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@idService", (object)idService ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@idCategorie", idCategorie.Value);
                            cmd.Parameters.AddWithValue("@contrat", (object)(employe.Contrat ?? "CDI") ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@typeContrat", (object)employe.TypeContrat ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@modePayement", (object)employe.ModePayement ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@cadre", (object)employe.Cadre ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@dateEntree", (object)(employe.DateEntree ?? DateTime.Now) ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@dateSortie", (object)employe.DateSortie ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@heureContrat", (object)employe.HeureContrat ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@jourContrat", (object)employe.JourContrat ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@numeroBancaire", (object)employe.NumeroBancaire ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@banque", (object)employe.Banque ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@salaireMoyen", (object)employe.SalaireMoyen ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@dureeContrat", (object)employe.DureeContrat ?? DBNull.Value);

                            await cmd.ExecuteNonQueryAsync();
                        }

                        employe.Succes = true;
                        employe.Erreur = null;
                    }
                }
                catch (Exception ex)
                {
                    employe.Succes = false;
                    employe.Erreur = $"Erreur: {ex.Message}";
                }

                resultats.Add(employe);
            }

            return resultats;
        }

        private static async Task<int?> RecupererIdEntreprise(MySqlConnection cn, string nomEntreprise)
        {
            string query = "SELECT id_entreprise FROM entreprise WHERE nomEntreprise = @nom LIMIT 1";
            using (var cmd = new MySqlCommand(query, cn))
            {
                cmd.Parameters.AddWithValue("@nom", nomEntreprise);
                var result = await cmd.ExecuteScalarAsync();
                return result != null ? Convert.ToInt32(result) : (int?)null;
            }
        }

        private static async Task<int?> RecupererIdDirection(MySqlConnection cn, int idEntreprise, string nomDirection)
        {
            string query = "SELECT id_direction FROM direction WHERE nomDirection = @nom AND id_entreprise = @idEnt LIMIT 1";
            using (var cmd = new MySqlCommand(query, cn))
            {
                cmd.Parameters.AddWithValue("@nom", nomDirection);
                cmd.Parameters.AddWithValue("@idEnt", idEntreprise);
                var result = await cmd.ExecuteScalarAsync();
                return result != null ? Convert.ToInt32(result) : (int?)null;
            }
        }

        private static async Task<int?> RecupererIdService(MySqlConnection cn, int idEntreprise, string nomService)
        {
            string query = "SELECT id_service FROM service WHERE nomService = @nom AND id_entreprise = @idEnt LIMIT 1";
            using (var cmd = new MySqlCommand(query, cn))
            {
                cmd.Parameters.AddWithValue("@nom", nomService);
                cmd.Parameters.AddWithValue("@idEnt", idEntreprise);
                var result = await cmd.ExecuteScalarAsync();
                return result != null ? Convert.ToInt32(result) : (int?)null;
            }
        }

        private static async Task<int?> RecupererIdCategorie(MySqlConnection cn, int idEntreprise, string nomCategorie)
        {
            string query = "SELECT id_categorie FROM categorie WHERE nomCategorie = @nom AND id_entreprise = @idEnt LIMIT 1";
            using (var cmd = new MySqlCommand(query, cn))
            {
                cmd.Parameters.AddWithValue("@nom", nomCategorie);
                cmd.Parameters.AddWithValue("@idEnt", idEntreprise);
                var result = await cmd.ExecuteScalarAsync();
                return result != null ? Convert.ToInt32(result) : (int?)null;
            }
        }

        public static void CreerModeleExcel(string cheminFichier)
        {
            // Cette méthode crée un fichier Excel modèle avec les en-têtes de colonnes
            try
            {
                string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={cheminFichier};Extended Properties='Excel 12.0 Xml;HDR=YES;'";

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();

                    string createTable = @"CREATE TABLE [Employes] (
                        NomPrenom VARCHAR,
                        Civilite VARCHAR,
                        Sexe VARCHAR,
                        DateNaissance DATETIME,
                        Adresse VARCHAR,
                        Telephone VARCHAR,
                        Identification VARCHAR,
                        Entreprise VARCHAR,
                        Direction VARCHAR,
                        Service VARCHAR,
                        Categorie VARCHAR,
                        Poste VARCHAR,
                        NumeroCNSS VARCHAR,
                        Contrat VARCHAR,
                        TypeContrat VARCHAR,
                        ModePayement VARCHAR,
                        Cadre VARCHAR,
                        DateEntree DATETIME,
                        DateSortie DATETIME,
                        HeureContrat NUMERIC,
                        JourContrat INT,
                        NumeroBancaire VARCHAR,
                        Banque VARCHAR,
                        SalaireMoyen CURRENCY,
                        DureeContrat VARCHAR
                    )";

                    using (OleDbCommand cmd = new OleDbCommand(createTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la création du modèle Excel : {ex.Message}", ex);
            }
        }
    }
}
