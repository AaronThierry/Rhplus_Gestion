using System;
using OfficeOpenXml;

namespace RH_GRH
{
    /// <summary>
    /// Classe de test pour vérifier le fonctionnement d'EPPlus
    /// </summary>
    public static class EPPlusTest
    {
        public static string TestEPPlus(string testFilePath)
        {
            try
            {
                // Définir le contexte de licence
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // Créer un fichier Excel simple
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Test");
                    worksheet.Cells[1, 1].Value = "Test EPPlus";
                    worksheet.Cells[2, 1].Value = "Si vous voyez ce texte, EPPlus fonctionne!";
                    worksheet.Cells[3, 1].Value = DateTime.Now.ToString();

                    // Sauvegarder
                    using (var stream = new System.IO.FileStream(testFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                    {
                        package.SaveAs(stream);
                    }
                }

                // Vérifier la taille du fichier
                var fileInfo = new System.IO.FileInfo(testFilePath);
                if (fileInfo.Exists)
                {
                    return $"Succès! Fichier créé ({fileInfo.Length} octets)";
                }
                else
                {
                    return "Échec: Le fichier n'a pas été créé";
                }
            }
            catch (Exception ex)
            {
                return $"Erreur: {ex.Message}\n\nStack: {ex.StackTrace}";
            }
        }
    }
}
