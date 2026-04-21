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

            // Boucle de connexion/déconnexion
            bool continuerApplication = true;

            while (continuerApplication)
            {
                // Afficher le formulaire de connexion moderne
                using (LoginFormModern loginForm = new LoginFormModern())
                {
                    DialogResult loginResult = loginForm.ShowDialog();

                    if (loginResult == DialogResult.OK)
                    {
                        // Connexion réussie - lancer l'application principale
                        using (Formmain mainForm = new Formmain())
                        {
                            Application.Run(mainForm);
                        }

                        // Après fermeture du formulaire principal
                        // Vérifier si l'utilisateur s'est déconnecté ou a fermé l'app
                        if (!Auth.SessionManager.Instance.IsAuthenticated)
                        {
                            // Déconnexion normale - retour au login
                            continuerApplication = true;
                        }
                        else
                        {
                            // Fermeture de l'application sans déconnexion
                            continuerApplication = false;
                        }
                    }
                    else
                    {
                        // Connexion annulée - fermer l'application
                        continuerApplication = false;
                    }
                }
            }

            // Fermeture propre de l'application
            Application.Exit();
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
