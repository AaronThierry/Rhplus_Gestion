using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RH_GRH
{
    public class ExcelImportServiceV2
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
            string extension = Path.GetExtension(cheminFichier).ToLower();

            if (extension == ".csv")
            {
                return LireFichierCSV(cheminFichier);
            }
            else if (extension == ".xlsx" || extension == ".xls")
            {
                // Essayer avec OleDb si disponible, sinon suggérer CSV
                try
                {
                    return LireFichierExcelOleDb(cheminFichier);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Microsoft.ACE.OLEDB") || ex.Message.Contains("provider"))
                    {
                        throw new Exception(
                            "Le driver Microsoft Access Database Engine n'est pas installé.\n\n" +
                            "Solutions :\n" +
                            "1. Utilisez un fichier CSV au lieu d'Excel (recommandé)\n" +
                            "2. Installez Microsoft Access Database Engine 2016 depuis :\n" +
                            "   https://www.microsoft.com/fr-fr/download/details.aspx?id=54920\n\n" +
                            "Pour utiliser CSV :\n" +
                            "- Dans Excel, ouvrez votre fichier\n" +
                            "- Fichier > Enregistrer sous\n" +
                            "- Type : CSV (séparateur : point-virgule) (*.csv)",
                            ex);
                    }
                    throw;
                }
            }
            else
            {
                throw new Exception("Format de fichier non supporté. Utilisez .xls, .xlsx ou .csv");
            }
        }

        private static List<EmployeImportRow> LireFichierCSV(string cheminFichier)
        {
            var employes = new List<EmployeImportRow>();

            try
            {
                // Détecter l'encodage
                Encoding encoding = DetectEncoding(cheminFichier);

                // Lire toutes les lignes
                string[] lines = File.ReadAllLines(cheminFichier, encoding);

                if (lines.Length < 2)
                {
                    throw new Exception("Le fichier CSV est vide ou ne contient que l'en-tête");
                }

                // Lire l'en-tête
                string[] headers = lines[0].Split(';', ',');

                // Nettoyer les en-têtes (enlever les BOM, espaces, etc.)
                for (int i = 0; i < headers.Length; i++)
                {
                    headers[i] = headers[i].Trim().Trim('"').Trim();
                    // Enlever le BOM si présent
                    if (headers[i].Length > 0 && headers[i][0] == '\uFEFF')
                        headers[i] = headers[i].Substring(1);
                }

                // Déterminer le séparateur (; ou ,)
                char separator = lines[0].Contains(';') ? ';' : ',';

                // Lire les données
                for (int ligneIndex = 1; ligneIndex < lines.Length; ligneIndex++)
                {
                    string line = lines[ligneIndex].Trim();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] values = ParseCSVLine(line, separator);

                    // Créer un dictionnaire colonne -> valeur
                    var rowData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    for (int i = 0; i < Math.Min(headers.Length, values.Length); i++)
                    {
                        rowData[headers[i]] = values[i]?.Trim()?.Trim('"')?.Trim();
                    }

                    var employe = new EmployeImportRow
                    {
                        LigneExcel = ligneIndex + 1,
                        NomPrenom = GetValue(rowData, "NomPrenom"),
                        Civilite = GetValue(rowData, "Civilite"),
                        Sexe = GetValue(rowData, "Sexe"),
                        DateNaissance = ParseDate(GetValue(rowData, "DateNaissance")),
                        Adresse = GetValue(rowData, "Adresse"),
                        Telephone = GetValue(rowData, "Telephone"),
                        Identification = GetValue(rowData, "Identification"),
                        NomEntreprise = GetValue(rowData, "Entreprise"),
                        NomDirection = GetValue(rowData, "Direction"),
                        NomService = GetValue(rowData, "Service"),
                        NomCategorie = GetValue(rowData, "Categorie"),
                        Poste = GetValue(rowData, "Poste"),
                        NumeroCNSS = GetValue(rowData, "NumeroCNSS"),
                        Contrat = GetValue(rowData, "Contrat"),
                        TypeContrat = GetValue(rowData, "TypeContrat"),
                        ModePayement = GetValue(rowData, "ModePayement"),
                        Cadre = GetValue(rowData, "Cadre"),
                        DateEntree = ParseDate(GetValue(rowData, "DateEntree")),
                        DateSortie = ParseDate(GetValue(rowData, "DateSortie")),
                        HeureContrat = ParseDecimal(GetValue(rowData, "HeureContrat")),
                        JourContrat = ParseInt(GetValue(rowData, "JourContrat")),
                        NumeroBancaire = GetValue(rowData, "NumeroBancaire"),
                        Banque = GetValue(rowData, "Banque"),
                        SalaireMoyen = ParseDecimal(GetValue(rowData, "SalaireMoyen")),
                        DureeContrat = GetValue(rowData, "DureeContrat")
                    };

                    employes.Add(employe);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la lecture du fichier CSV : {ex.Message}", ex);
            }

            return employes;
        }

        private static string[] ParseCSVLine(string line, char separator)
        {
            var values = new List<string>();
            var currentValue = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == separator && !inQuotes)
                {
                    values.Add(currentValue.ToString());
                    currentValue.Clear();
                }
                else
                {
                    currentValue.Append(c);
                }
            }

            values.Add(currentValue.ToString());
            return values.ToArray();
        }

        private static Encoding DetectEncoding(string filename)
        {
            // Lire les premiers octets pour détecter le BOM
            byte[] bom = new byte[4];
            using (FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyser le BOM
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)
                return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe)
                return Encoding.Unicode; // UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff)
                return Encoding.BigEndianUnicode; // UTF-16BE

            // Par défaut, utiliser l'encodage Windows-1252 (courant pour Excel français)
            return Encoding.GetEncoding("Windows-1252");
        }

        private static string GetValue(Dictionary<string, string> data, string key)
        {
            if (data.TryGetValue(key, out string value) && !string.IsNullOrWhiteSpace(value))
                return value;
            return null;
        }

        private static DateTime? ParseDate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            // Essayer plusieurs formats
            string[] formats = {
                "dd/MM/yyyy", "d/M/yyyy", "dd-MM-yyyy", "d-M-yyyy",
                "yyyy-MM-dd", "yyyy/MM/dd",
                "dd/MM/yyyy HH:mm:ss", "dd-MM-yyyy HH:mm:ss"
            };

            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(value, format, System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out DateTime result))
                {
                    return result;
                }
            }

            // Essayer un parsing standard
            if (DateTime.TryParse(value, out DateTime result2))
                return result2;

            return null;
        }

        private static decimal? ParseDecimal(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            // Remplacer la virgule par un point pour le parsing
            value = value.Replace(',', '.');

            if (decimal.TryParse(value, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out decimal result))
                return result;

            return null;
        }

        private static int? ParseInt(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            if (int.TryParse(value, out int result))
                return result;

            return null;
        }

        private static List<EmployeImportRow> LireFichierExcelOleDb(string cheminFichier)
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

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                DataTable dtSheets = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = dtSheets.Rows[0]["TABLE_NAME"].ToString();

                string query = $"SELECT * FROM [{sheetName}]";
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    int ligne = 2;
                    foreach (DataRow row in dt.Rows)
                    {
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
                            HeureContrat = LireDecimalFromRow(row, "HeureContrat"),
                            JourContrat = LireIntFromRow(row, "JourContrat"),
                            NumeroBancaire = LireString(row, "NumeroBancaire"),
                            Banque = LireString(row, "Banque"),
                            SalaireMoyen = LireDecimalFromRow(row, "SalaireMoyen"),
                            DureeContrat = LireString(row, "DureeContrat")
                        };

                        employes.Add(employe);
                        ligne++;
                    }
                }
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

        private static decimal? LireDecimalFromRow(DataRow row, string columnName)
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

        private static int? LireIntFromRow(DataRow row, string columnName)
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
                                employe.Erreur = $"Direction '{employe.NomDirection}' introuvable";
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
                                employe.Erreur = $"Service '{employe.NomService}' introuvable";
                                resultats.Add(employe);
                                continue;
                            }
                        }

                        int? idCategorie = await RecupererIdCategorie(cn, idEntreprise.Value, employe.NomCategorie);
                        if (!idCategorie.HasValue)
                        {
                            employe.Succes = false;
                            employe.Erreur = $"Catégorie '{employe.NomCategorie}' introuvable";
                            resultats.Add(employe);
                            continue;
                        }

                        string matricule = MatriculeGenerator.GenererMatricule(idEntreprise.Value);
                        employe.Matricule = matricule;

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
                            cmd.Parameters.AddWithValue("@contrat", (object)(employe.Contrat ?? "CDI"));
                            cmd.Parameters.AddWithValue("@typeContrat", (object)employe.TypeContrat ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@modePayement", (object)employe.ModePayement ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@cadre", (object)employe.Cadre ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@dateEntree", (object)(employe.DateEntree ?? DateTime.Now));
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

        public static void CreerModeleCSV(string cheminFichier)
        {
            try
            {
                var sb = new StringBuilder();

                // En-têtes
                sb.AppendLine("NomPrenom;Civilite;Sexe;DateNaissance;Adresse;Telephone;Identification;Entreprise;Direction;Service;Categorie;Poste;NumeroCNSS;Contrat;TypeContrat;ModePayement;Cadre;DateEntree;DateSortie;HeureContrat;JourContrat;NumeroBancaire;Banque;SalaireMoyen;DureeContrat");

                // Ligne d'exemple
                sb.AppendLine("Jean Dupont;M.;Masculin;01/01/1980;123 Rue Exemple;0123456789;CNI123456;Nom Entreprise;Nom Direction;Nom Service;Nom Categorie;Responsable;123456789;;Mensuel;Virement;Cadre;01/01/2024;;;40;;ABC Bank;;");

                File.WriteAllText(cheminFichier, sb.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la création du modèle CSV : {ex.Message}", ex);
            }
        }
    }
}
