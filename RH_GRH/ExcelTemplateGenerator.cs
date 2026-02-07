using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.DataValidation;
using System.IO;
using System.Drawing;

namespace RH_GRH
{
    public class ExcelTemplateGenerator
    {
        private string connectionString;

        public ExcelTemplateGenerator(string connectionString)
        {
            this.connectionString = connectionString;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public string GenerateTemplate(int idEntreprise, string nomEntreprise, string outputPath)
        {
            try
            {
                var fileInfo = new FileInfo(outputPath);
                if (fileInfo.Exists) fileInfo.Delete();

                using (var package = new ExcelPackage(fileInfo))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Employes");

                    AddHeaders(worksheet);
                    AddExampleRow(worksheet, nomEntreprise);
                    AddDataValidation(worksheet, idEntreprise);
                    FormatWorksheet(worksheet);
                    AddInstructionsSheet(package);

                    package.Save();
                }

                return outputPath;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur generation template: " + ex.Message, ex);
            }
        }

        private void AddHeaders(ExcelWorksheet ws)
        {
            string[] headers = {
                "Matricule", "Identification", "Civilite", "Sexe", "Nom", "Prenom",
                "Date Naissance", "Adresse", "Telephone", "Poste", "Contrat", "Cadre",
                "Type Contrat", "Date Entree", "Date Sortie", "Heure Contrat", "Jour Contrat",
                "Mode Paiement", "Banque", "N Bancaire", "Categorie", "Service", "Direction",
                "N CNSS", "Duree Contrat", "Salaire Moyen"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                var cell = ws.Cells[1, i + 1];
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Font.Size = 11;
                cell.Style.Font.Color.SetColor(Color.White);
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(25, 25, 112));
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            }
        }

        private void AddExampleRow(ExcelWorksheet ws, string entreprise)
        {
            ws.Cells[2, 1].Value = "EMP001";
            ws.Cells[2, 2].Value = "CNI12345678";
            ws.Cells[2, 3].Value = "M.";
            ws.Cells[2, 4].Value = "Masculin";
            ws.Cells[2, 5].Value = "KOUADIO";
            ws.Cells[2, 6].Value = "Jean";
            ws.Cells[2, 7].Value = new DateTime(1985, 5, 15);
            ws.Cells[2, 8].Value = "123 Avenue de la Republique";
            ws.Cells[2, 9].Value = "0123456789";
            ws.Cells[2, 10].Value = "Developpeur Senior";
            ws.Cells[2, 11].Value = "CDI";
            ws.Cells[2, 12].Value = "Cadre";
            ws.Cells[2, 13].Value = "Temps plein";
            ws.Cells[2, 14].Value = new DateTime(2020, 1, 15);
            ws.Cells[2, 16].Value = 40;
            ws.Cells[2, 17].Value = 22;
            ws.Cells[2, 18].Value = "Virement bancaire";
            ws.Cells[2, 19].Value = "Banque Atlantique";
            ws.Cells[2, 20].Value = "BF1234567890123456";
            ws.Cells[2, 24].Value = "987654321";
            ws.Cells[2, 25].Value = "Indeterminee";
            ws.Cells[2, 26].Value = 350000;

            ws.Cells[2, 7].Style.Numberformat.Format = "dd/mm/yyyy";
            ws.Cells[2, 14].Style.Numberformat.Format = "dd/mm/yyyy";
            ws.Cells[2, 15].Style.Numberformat.Format = "dd/mm/yyyy";
            ws.Cells[2, 26].Style.Numberformat.Format = "#,##0";
        }

        private void AddDataValidation(ExcelWorksheet ws, int idEntreprise)
        {
            var categories = GetCategories(idEntreprise);
            var services = GetServices(idEntreprise);
            var directions = GetDirections(idEntreprise);

            if (categories.Count > 0)
            {
                var catSheet = ws.Workbook.Worksheets.Add("Data_Categories");
                for (int i = 0; i < categories.Count; i++)
                    catSheet.Cells[i + 1, 1].Value = categories[i];
                catSheet.Hidden = eWorkSheetHidden.VeryHidden;

                var catValidation = ws.DataValidations.AddListValidation("U3:U1000");
                catValidation.Formula.ExcelFormula = "Data_Categories!$A$1:$A$" + categories.Count;
            }

            if (services.Count > 0)
            {
                var svcSheet = ws.Workbook.Worksheets.Add("Data_Services");
                for (int i = 0; i < services.Count; i++)
                    svcSheet.Cells[i + 1, 1].Value = services[i];
                svcSheet.Hidden = eWorkSheetHidden.VeryHidden;

                var svcValidation = ws.DataValidations.AddListValidation("V3:V1000");
                svcValidation.Formula.ExcelFormula = "Data_Services!$A$1:$A$" + services.Count;
            }

            if (directions.Count > 0)
            {
                var dirSheet = ws.Workbook.Worksheets.Add("Data_Directions");
                for (int i = 0; i < directions.Count; i++)
                    dirSheet.Cells[i + 1, 1].Value = directions[i];
                dirSheet.Hidden = eWorkSheetHidden.VeryHidden;

                var dirValidation = ws.DataValidations.AddListValidation("W3:W1000");
                dirValidation.Formula.ExcelFormula = "Data_Directions!$A$1:$A$" + directions.Count;
            }

            var civValidation = ws.DataValidations.AddListValidation("C3:C1000");
            civValidation.Formula.Values.Add("M.");
            civValidation.Formula.Values.Add("Mme");
            civValidation.Formula.Values.Add("Mlle");

            var sexeValidation = ws.DataValidations.AddListValidation("D3:D1000");
            sexeValidation.Formula.Values.Add("Masculin");
            sexeValidation.Formula.Values.Add("Feminin");

            var contratValidation = ws.DataValidations.AddListValidation("K3:K1000");
            contratValidation.Formula.Values.Add("CDI");
            contratValidation.Formula.Values.Add("CDD");
            contratValidation.Formula.Values.Add("Stage");
            contratValidation.Formula.Values.Add("Temporaire");

            var cadreValidation = ws.DataValidations.AddListValidation("L3:L1000");
            cadreValidation.Formula.Values.Add("Cadre");
            cadreValidation.Formula.Values.Add("Non-cadre");

            var modeValidation = ws.DataValidations.AddListValidation("R3:R1000");
            modeValidation.Formula.Values.Add("Especes");
            modeValidation.Formula.Values.Add("Virement bancaire");
            modeValidation.Formula.Values.Add("Cheque");
            modeValidation.Formula.Values.Add("Mobile Money");
        }

        private void FormatWorksheet(ExcelWorksheet ws)
        {
            ws.Column(1).Width = 15;
            ws.Column(2).Width = 18;
            ws.Column(3).Width = 10;
            ws.Column(4).Width = 12;
            ws.Column(5).Width = 20;
            ws.Column(6).Width = 20;
            ws.Column(7).Width = 15;
            ws.Column(8).Width = 30;
            ws.Column(9).Width = 15;
            ws.Column(10).Width = 25;
            ws.Column(11).Width = 12;
            ws.Column(12).Width = 12;
            ws.Column(13).Width = 15;
            ws.Column(14).Width = 15;
            ws.Column(15).Width = 15;
            ws.Column(16).Width = 15;
            ws.Column(17).Width = 15;
            ws.Column(18).Width = 18;
            ws.Column(19).Width = 20;
            ws.Column(20).Width = 20;
            ws.Column(21).Width = 15;
            ws.Column(22).Width = 20;
            ws.Column(23).Width = 25;
            ws.Column(24).Width = 15;
            ws.Column(25).Width = 15;
            ws.Column(26).Width = 18;

            ws.View.FreezePanes(2, 1);
        }

        private void AddInstructionsSheet(ExcelPackage package)
        {
            var ws = package.Workbook.Worksheets.Add("Instructions");
            ws.Cells[1, 1].Value = "INSTRUCTIONS IMPORTATION - GESTION MODERNE RH";
            ws.Cells[1, 1].Style.Font.Bold = true;
            ws.Cells[1, 1].Style.Font.Size = 14;

            int row = 3;
            ws.Cells[row++, 1].Value = "CHAMPS OBLIGATOIRES:";
            ws.Cells[row++, 1].Value = "Matricule, Identification, Civilite, Sexe, Nom, Prenom";
            ws.Cells[row++, 1].Value = "Date Naissance, Contrat, Cadre, Date Entree";
            ws.Cells[row++, 1].Value = "Categorie, Service, Direction";
            row++;
            ws.Cells[row++, 1].Value = "NOTES:";
            ws.Cells[row++, 1].Value = "1. Supprimez la ligne exemple avant import";
            ws.Cells[row++, 1].Value = "2. Dates format: jj/mm/aaaa";
            ws.Cells[row++, 1].Value = "3. Age minimum: 16 ans";
            ws.Cells[row++, 1].Value = "4. Matricule doit etre unique";

            ws.Column(1).Width = 80;
        }

        private List<string> GetCategories(int idEntreprise)
        {
            var list = new List<string>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT nomCategorie FROM categorie WHERE id_entreprise = @id ORDER BY nomCategorie", conn);
                cmd.Parameters.AddWithValue("@id", idEntreprise);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(reader.GetString(0));
                }
            }
            return list;
        }

        private List<string> GetServices(int idEntreprise)
        {
            var list = new List<string>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT nomService FROM service WHERE id_entreprise = @id ORDER BY nomService", conn);
                cmd.Parameters.AddWithValue("@id", idEntreprise);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(reader.GetString(0));
                }
            }
            return list;
        }

        private List<string> GetDirections(int idEntreprise)
        {
            var list = new List<string>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT nomDirection FROM direction WHERE id_entreprise = @id ORDER BY nomDirection", conn);
                cmd.Parameters.AddWithValue("@id", idEntreprise);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) list.Add(reader.GetString(0));
                }
            }
            return list;
        }
    }
}
