using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RH_GRH
{
    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Gestionnaire d'exceptions global
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // Déclaration obligatoire de la licence
            QuestPDF.Settings.License = LicenseType.Community;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Formmain());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string errorMsg = $"ERREUR NON GÉRÉE:\n\n{e.Exception.Message}\n\nDétails complets:\n{e.Exception.ToString()}";
            MessageBox.Show(errorMsg, "Erreur Critique", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            string errorMsg = $"ERREUR FATALE:\n\n{ex?.Message ?? "Erreur inconnue"}\n\nDétails complets:\n{ex?.ToString() ?? "Aucun détail disponible"}";
            MessageBox.Show(errorMsg, "Erreur Fatale", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
