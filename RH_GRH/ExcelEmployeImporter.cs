using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using OfficeOpenXml;

namespace RH_GRH
{
    /// <summary>
    /// Classe pour importer des employés depuis un fichier Excel avec validation complète
    /// </summary>
    public class ExcelEmployeImporter
    {
        private string connectionString;
        private int idEntreprise;
        private string nomEntreprise;

        // Dictionnaires pour mapper les noms vers les IDs
        private Dictionary<string, int> categorieMap = new Dictionary<string, int>();
        private Dictionary<string, int> serviceMap = new Dictionary<string, int>();
        private Dictionary<string, int> directionMap = new Dictionary<string, int>();

        public ExcelEmployeImporter(string connectionString, int idEntreprise, string nomEntreprise)
        {
            this.connectionString = connectionString;
            this.idEntreprise = idEntreprise;
            this.nomEntreprise = nomEntreprise;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            LoadReferenceMaps();
        }

        /// <summary>
        /// Charge les mappings de référence depuis la base de données
        /// </summary>
        private void LoadReferenceMaps()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Charger les catégories
                string query = "SELECT id_categorie, nomCategorie FROM categorie WHERE id_entreprise = @idEntreprise";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categorieMap[reader.GetString("nomCategorie")] = reader.GetInt32("id_categorie");
                        }
                    }
                }

                // Charger les services
                query = "SELECT id_service, nomService FROM service WHERE id_entreprise = @idEntreprise";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            serviceMap[reader.GetString("nomService")] = reader.GetInt32("id_service");
                        }
                    }
                }

                // Charger les directions
                query = "SELECT id_direction, nomDirection FROM direction WHERE id_entreprise = @idEntreprise";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            directionMap[reader.GetString("nomDirection")] = reader.GetInt32("id_direction");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Importe les employés depuis un fichier Excel avec validation complète
        /// </summary>
        public ImportResult ImportEmployees(string excelFilePath)
        {
            var result = new ImportResult();

            try
            {
                var fileInfo = new System.IO.FileInfo(excelFilePath);
                using (var package = new ExcelPackage(fileInfo))
                {
                    var worksheet = package.Workbook.Worksheets["Employés"];
                    if (worksheet == null)
                    {
                        result.Success = false;
                        result.ErrorMessage = "La feuille 'Employés' est introuvable dans le fichier Excel.";
                        return result;
                    }

                    // Lire les données à partir de la ligne 2 (après l'en-tête)
                    int currentRow = 2;
                    var rowsToImport = new List<EmployeeImportRow>();
                    var existingMatricules = GetExistingMatricules();

                    // Phase 1: Lecture et validation
                    while (currentRow <= worksheet.Dimension.End.Row && !string.IsNullOrWhiteSpace(worksheet.Cells[currentRow, 1].Text))
                    {
                        var importRow = new EmployeeImportRow
                        {
                            RowNumber = currentRow,
                            Errors = new List<string>()
                        };

                        try
                        {
                            // Lire toutes les colonnes - Aligné 100% avec table personnel
                            importRow.Matricule = GetCellValue(worksheet, currentRow, 1);
                            importRow.Identification = GetCellValue(worksheet, currentRow, 2);
                            importRow.Civilite = GetCellValue(worksheet, currentRow, 3);
                            importRow.Sexe = GetCellValue(worksheet, currentRow, 4);
                            importRow.Nom = GetCellValue(worksheet, currentRow, 5);
                            importRow.Prenom = GetCellValue(worksheet, currentRow, 6);
                            importRow.DateNaissance = GetDateValue(worksheet, currentRow, 7);
                            importRow.Adresse = GetCellValue(worksheet, currentRow, 8);
                            importRow.Telephone = GetCellValue(worksheet, currentRow, 9);
                            importRow.Poste = GetCellValue(worksheet, currentRow, 10);
                            importRow.Contrat = GetCellValue(worksheet, currentRow, 11);
                            importRow.Cadre = GetCellValue(worksheet, currentRow, 12);
                            importRow.TypeContrat = GetCellValue(worksheet, currentRow, 13);
                            importRow.DateEntree = GetDateValue(worksheet, currentRow, 14);
                            importRow.DateSortie = GetDateValue(worksheet, currentRow, 15);
                            importRow.HeureContrat = GetIntValue(worksheet, currentRow, 16);
                            importRow.JourContrat = GetIntValue(worksheet, currentRow, 17);
                            importRow.ModePayement = GetCellValue(worksheet, currentRow, 18);
                            importRow.Banque = GetCellValue(worksheet, currentRow, 19);
                            importRow.NumeroBancaire = GetCellValue(worksheet, currentRow, 20);
                            importRow.NomCategorie = GetCellValue(worksheet, currentRow, 21);
                            importRow.NomService = GetCellValue(worksheet, currentRow, 22);
                            importRow.NomDirection = GetCellValue(worksheet, currentRow, 23);
                            importRow.NumeroCNSS = GetCellValue(worksheet, currentRow, 24);
                            importRow.DureeContrat = GetCellValue(worksheet, currentRow, 25);
                            importRow.SalaireMoyen = GetNullableDecimalValue(worksheet, currentRow, 26);

                            // DEBUG: Afficher ce qui a été lu
                            System.Diagnostics.Debug.WriteLine($"=== LIGNE {currentRow} ===");
                            System.Diagnostics.Debug.WriteLine($"Col 1 (Matricule): '{importRow.Matricule}'");
                            System.Diagnostics.Debug.WriteLine($"Col 2 (Identification): '{importRow.Identification}'");
                            System.Diagnostics.Debug.WriteLine($"Col 3 (Civilité): '{importRow.Civilite}'");
                            System.Diagnostics.Debug.WriteLine($"Col 5 (Nom): '{importRow.Nom}'");
                            System.Diagnostics.Debug.WriteLine($"Col 6 (Prénom): '{importRow.Prenom}'");
                            System.Diagnostics.Debug.WriteLine($"Col 21 (Catégorie): '{importRow.NomCategorie}'");
                            System.Diagnostics.Debug.WriteLine($"Col 22 (Service): '{importRow.NomService}'");
                            System.Diagnostics.Debug.WriteLine($"Col 23 (Direction): '{importRow.NomDirection}'");

                            // Validation
                            ValidateRow(importRow, existingMatricules);

                            rowsToImport.Add(importRow);
                        }
                        catch (Exception ex)
                        {
                            importRow.Errors.Add($"Erreur lors de la lecture: {ex.Message}");
                            rowsToImport.Add(importRow);
                        }

                        currentRow++;
                    }

                    // Vérifier s'il y a des données
                    if (rowsToImport.Count == 0)
                    {
                        result.Success = false;
                        result.ErrorMessage = "Aucune donnée trouvée dans le fichier Excel.";
                        return result;
                    }

                    // Compter les erreurs
                    result.TotalRows = rowsToImport.Count;
                    result.InvalidRows = rowsToImport.Where(r => r.HasErrors).ToList();

                    if (result.InvalidRows.Count > 0)
                    {
                        result.Success = false;
                        result.ErrorMessage = $"{result.InvalidRows.Count} ligne(s) contiennent des erreurs.";
                        return result;
                    }

                    // Phase 2: Insertion dans la base de données
                    result.ValidRows = rowsToImport;
                    int importedCount = InsertEmployees(rowsToImport, result);

                    result.Success = true;
                    result.ImportedCount = importedCount;
                    result.ErrorMessage = $"{importedCount} employé(s) importé(s) avec succès.";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = $"Erreur lors de l'import: {ex.Message}";
            }

            return result;
        }

        /// <summary>
        /// Valide une ligne de données
        /// </summary>
        private void ValidateRow(EmployeeImportRow row, HashSet<string> existingMatricules)
        {
            // Champs obligatoires
            if (string.IsNullOrWhiteSpace(row.Matricule))
                row.Errors.Add("Matricule requis");
            else if (existingMatricules.Contains(row.Matricule))
                row.Errors.Add($"Matricule '{row.Matricule}' déjà existant");

            if (string.IsNullOrWhiteSpace(row.Identification))
                row.Errors.Add("Identification requise");

            if (string.IsNullOrWhiteSpace(row.Civilite))
                row.Errors.Add("Civilité requise");
            else if (!new[] { "M.", "Mme", "Mlle" }.Contains(row.Civilite))
                row.Errors.Add("Civilité invalide (M., Mme, Mlle)");

            if (string.IsNullOrWhiteSpace(row.Sexe))
                row.Errors.Add("Sexe requis");
            else if (!new[] { "Masculin", "Féminin" }.Contains(row.Sexe))
                row.Errors.Add("Sexe invalide (Masculin, Féminin)");

            if (string.IsNullOrWhiteSpace(row.Nom))
                row.Errors.Add("Nom requis");

            if (string.IsNullOrWhiteSpace(row.Prenom))
                row.Errors.Add("Prénom requis");

            if (row.DateNaissance == null)
                row.Errors.Add("Date de naissance requise");
            else if (row.DateNaissance.Value > DateTime.Now.AddYears(-16))
                row.Errors.Add("L'employé doit avoir au moins 16 ans");

            if (string.IsNullOrWhiteSpace(row.Contrat))
                row.Errors.Add("Contrat requis");
            else if (!new[] { "CDI", "CDD", "Stage", "Temporaire" }.Contains(row.Contrat))
                row.Errors.Add("Contrat invalide (CDI, CDD, Stage, Temporaire)");

            if (string.IsNullOrWhiteSpace(row.Cadre))
                row.Errors.Add("Cadre requis");
            else if (!new[] { "Cadre", "Non-cadre" }.Contains(row.Cadre))
                row.Errors.Add("Cadre invalide (Cadre, Non-cadre)");

            if (row.DateEntree == null)
                row.Errors.Add("Date d'entrée requise");
            else if (row.DateEntree.Value > DateTime.Now)
                row.Errors.Add("Date d'entrée ne peut pas être dans le futur");

            if (row.SalaireMoyen.HasValue && row.SalaireMoyen.Value <= 0)
                row.Errors.Add("Salaire moyen doit être supérieur à 0");

            // Validation de la catégorie
            if (string.IsNullOrWhiteSpace(row.NomCategorie))
                row.Errors.Add("Catégorie requise");
            else if (!categorieMap.ContainsKey(row.NomCategorie))
                row.Errors.Add($"Catégorie '{row.NomCategorie}' introuvable");

            // Validation du service
            if (string.IsNullOrWhiteSpace(row.NomService))
                row.Errors.Add("Service requis");
            else if (!serviceMap.ContainsKey(row.NomService))
                row.Errors.Add($"Service '{row.NomService}' introuvable");

            // Validation de la direction
            if (string.IsNullOrWhiteSpace(row.NomDirection))
                row.Errors.Add("Direction requise");
            else if (!directionMap.ContainsKey(row.NomDirection))
                row.Errors.Add($"Direction '{row.NomDirection}' introuvable");

            // Validation mode de paiement
            if (!string.IsNullOrWhiteSpace(row.ModePayement))
            {
                if (!new[] { "Espèces", "Virement bancaire", "Chèque", "Mobile Money" }.Contains(row.ModePayement))
                    row.Errors.Add("Mode de paiement invalide");

                if (row.ModePayement == "Virement bancaire" && string.IsNullOrWhiteSpace(row.Banque))
                    row.Errors.Add("Banque requise pour virement bancaire");
            }
        }

        /// <summary>
        /// Récupère les matricules existants
        /// </summary>
        private HashSet<string> GetExistingMatricules()
        {
            var matricules = new HashSet<string>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT matricule FROM personnel WHERE id_entreprise = @idEntreprise";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            matricules.Add(reader.GetString("matricule"));
                        }
                    }
                }
            }
            return matricules;
        }

        /// <summary>
        /// Insère les employés dans la base de données
        /// </summary>
        private int InsertEmployees(List<EmployeeImportRow> rows, ImportResult result)
        {
            int count = 0;
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (MySqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string query = @"INSERT INTO personnel
                            (matricule, nomPrenom, civilite, sexe, date_naissance, adresse, telephone,
                             identification, poste, contrat, cadre, date_entree, date_sortie, typeContrat,
                             heureContrat, jourContrat, modePayement, banque, numeroBancaire,
                             id_direction, id_service, id_categorie, id_entreprise,
                             dureeContrat, numerocnss, salairemoyen)
                            VALUES
                            (@matricule, @nomPrenom, @civilite, @sexe, @dateNaissance, @adresse, @telephone,
                             @identification, @poste, @contrat, @cadre, @dateEntree, @dateSortie, @typeContrat,
                             @heureContrat, @jourContrat, @modePayement, @banque, @numeroBancaire,
                             @idDirection, @idService, @idCategorie, @idEntreprise,
                             @dureeContrat, @numeroCNSS, @salaireMoyen)";

                        foreach (var row in rows)
                        {
                            try
                            {
                                using (MySqlCommand cmd = new MySqlCommand(query, conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@matricule", row.Matricule);
                                    cmd.Parameters.AddWithValue("@nomPrenom", row.NomPrenom);
                                    cmd.Parameters.AddWithValue("@civilite", row.Civilite);
                                    cmd.Parameters.AddWithValue("@sexe", row.Sexe);
                                    cmd.Parameters.AddWithValue("@dateNaissance", row.DateNaissance);
                                    cmd.Parameters.AddWithValue("@adresse", (object)row.Adresse ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@telephone", (object)row.Telephone ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@identification", row.Identification);
                                    cmd.Parameters.AddWithValue("@poste", (object)row.Poste ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@contrat", row.Contrat);
                                    cmd.Parameters.AddWithValue("@cadre", row.Cadre);
                                    cmd.Parameters.AddWithValue("@dateEntree", row.DateEntree);
                                    cmd.Parameters.AddWithValue("@dateSortie", (object)row.DateSortie ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@typeContrat", (object)row.TypeContrat ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@heureContrat", (object)row.HeureContrat ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@jourContrat", (object)row.JourContrat ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@modePayement", (object)row.ModePayement ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@banque", (object)row.Banque ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@numeroBancaire", (object)row.NumeroBancaire ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@idDirection", directionMap[row.NomDirection]);
                                    cmd.Parameters.AddWithValue("@idService", serviceMap[row.NomService]);
                                    cmd.Parameters.AddWithValue("@idCategorie", categorieMap[row.NomCategorie]);
                                    cmd.Parameters.AddWithValue("@idEntreprise", idEntreprise);
                                    cmd.Parameters.AddWithValue("@dureeContrat", (object)row.DureeContrat ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@numeroCNSS", (object)row.NumeroCNSS ?? DBNull.Value);
                                    cmd.Parameters.AddWithValue("@salaireMoyen", (object)row.SalaireMoyen ?? DBNull.Value);

                                    cmd.ExecuteNonQuery();
                                    count++;
                                }
                            }
                            catch (Exception ex)
                            {
                                row.Errors.Add($"Erreur d'insertion: {ex.Message}");
                                result.InvalidRows.Add(row);
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Erreur lors de l'insertion en base: {ex.Message}");
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Récupère la valeur d'une cellule
        /// </summary>
        private string GetCellValue(ExcelWorksheet ws, int row, int col)
        {
            var cell = ws.Cells[row, col];
            if (cell == null || cell.Value == null)
                return "";
            return cell.Text.Trim();
        }

        /// <summary>
        /// Récupère une valeur de date
        /// </summary>
        private DateTime? GetDateValue(ExcelWorksheet ws, int row, int col)
        {
            var cell = ws.Cells[row, col];
            if (cell == null || cell.Value == null)
                return null;

            try
            {
                if (cell.Value is DateTime)
                    return (DateTime)cell.Value;

                string value = cell.Text.Trim();
                if (DateTime.TryParse(value, out DateTime result))
                    return result;

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Récupère une valeur décimale
        /// </summary>
        private decimal GetDecimalValue(ExcelWorksheet ws, int row, int col)
        {
            var cell = ws.Cells[row, col];
            if (cell == null || cell.Value == null)
                return 0;

            try
            {
                return Convert.ToDecimal(cell.Value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Récupère une valeur décimale nullable
        /// </summary>
        private decimal? GetNullableDecimalValue(ExcelWorksheet ws, int row, int col)
        {
            var cell = ws.Cells[row, col];
            if (cell == null || cell.Value == null)
                return null;

            try
            {
                return Convert.ToDecimal(cell.Value);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Récupère une valeur entière nullable
        /// </summary>
        private int? GetIntValue(ExcelWorksheet ws, int row, int col)
        {
            var cell = ws.Cells[row, col];
            if (cell == null || cell.Value == null)
                return null;

            try
            {
                return Convert.ToInt32(cell.Value);
            }
            catch
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Résultat de l'import
    /// </summary>
    public class ImportResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public int TotalRows { get; set; }
        public int ImportedCount { get; set; }
        public List<EmployeeImportRow> ValidRows { get; set; } = new List<EmployeeImportRow>();
        public List<EmployeeImportRow> InvalidRows { get; set; } = new List<EmployeeImportRow>();
    }

    /// <summary>
    /// Ligne d'employé à importer
    /// </summary>
    public class EmployeeImportRow
    {
        public int RowNumber { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public bool HasErrors => Errors.Count > 0;

        // Données de l'employé - Aligné 100% avec table personnel
        public string Matricule { get; set; }
        public string Police { get; set; }
        public string Identification { get; set; }
        public string Civilite { get; set; }
        public string Sexe { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime? DateNaissance { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public string Poste { get; set; }
        public string Contrat { get; set; }
        public string Cadre { get; set; }
        public DateTime? DateEntree { get; set; }
        public DateTime? DateSortie { get; set; }
        public string TypeContrat { get; set; }
        public int? HeureContrat { get; set; }
        public int? JourContrat { get; set; }
        public string ModePayement { get; set; }
        public string Banque { get; set; }
        public string NumeroBancaire { get; set; }
        public string NomCategorie { get; set; }
        public string NomService { get; set; }
        public string NomDirection { get; set; }
        public string NumeroCNSS { get; set; }
        public string DureeContrat { get; set; }
        public decimal? SalaireMoyen { get; set; }

        // Propriété calculée pour nomPrenom
        public string NomPrenom => $"{Nom} {Prenom}".Trim();

        public string ErrorSummary => string.Join("; ", Errors);
    }
}
