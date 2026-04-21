using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using RH_GRH.Auth;

namespace RH_GRH
{
    /// <summary>
    /// Formulaire de diagnostic pour les problèmes de mot de passe
    /// À utiliser uniquement pour le débogage
    /// </summary>
    public partial class PasswordDebugForm : Form
    {
        public PasswordDebugForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonTest = new System.Windows.Forms.Button();
            this.buttonGenerateHash = new System.Windows.Forms.Button();
            this.textBoxResults = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // labelUsername
            //
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(20, 20);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(120, 13);
            this.labelUsername.TabIndex = 0;
            this.labelUsername.Text = "Nom d\'utilisateur:";
            //
            // textBoxUsername
            //
            this.textBoxUsername.Location = new System.Drawing.Point(20, 40);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(200, 20);
            this.textBoxUsername.TabIndex = 1;
            this.textBoxUsername.Text = "admin";
            //
            // labelPassword
            //
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(20, 70);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(85, 13);
            this.labelPassword.TabIndex = 2;
            this.labelPassword.Text = "Mot de passe:";
            //
            // textBoxPassword
            //
            this.textBoxPassword.Location = new System.Drawing.Point(20, 90);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(200, 20);
            this.textBoxPassword.TabIndex = 3;
            this.textBoxPassword.Text = "Admin@123";
            //
            // buttonTest
            //
            this.buttonTest.Location = new System.Drawing.Point(20, 120);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(200, 30);
            this.buttonTest.TabIndex = 4;
            this.buttonTest.Text = "Tester le mot de passe";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            //
            // buttonGenerateHash
            //
            this.buttonGenerateHash.Location = new System.Drawing.Point(20, 160);
            this.buttonGenerateHash.Name = "buttonGenerateHash";
            this.buttonGenerateHash.Size = new System.Drawing.Size(200, 30);
            this.buttonGenerateHash.TabIndex = 5;
            this.buttonGenerateHash.Text = "Générer un nouveau hash";
            this.buttonGenerateHash.UseVisualStyleBackColor = true;
            this.buttonGenerateHash.Click += new System.EventHandler(this.buttonGenerateHash_Click);
            //
            // textBoxResults
            //
            this.textBoxResults.Font = new System.Drawing.Font("Consolas", 9F);
            this.textBoxResults.Location = new System.Drawing.Point(20, 200);
            this.textBoxResults.Multiline = true;
            this.textBoxResults.Name = "textBoxResults";
            this.textBoxResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResults.Size = new System.Drawing.Size(740, 340);
            this.textBoxResults.TabIndex = 6;
            //
            // PasswordDebugForm
            //
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.textBoxResults);
            this.Controls.Add(this.buttonGenerateHash);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.labelUsername);
            this.Name = "PasswordDebugForm";
            this.Text = "Diagnostic Mot de Passe - DEBUG UNIQUEMENT";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Button buttonGenerateHash;
        private System.Windows.Forms.TextBox textBoxResults;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelPassword;

        private void buttonTest_Click(object sender, EventArgs e)
        {
            textBoxResults.Clear();
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text;

            var results = new System.Text.StringBuilder();
            results.AppendLine("=== DIAGNOSTIC MOT DE PASSE ===");
            results.AppendLine($"Date/Heure: {DateTime.Now}");
            results.AppendLine($"Username: {username}");
            results.AppendLine($"Password: {password}");
            results.AppendLine($"Password Length: {password.Length} caractères");
            results.AppendLine();

            Dbconnect db = new Dbconnect();
            try
            {
                db.openConnect();
                string query = @"SELECT id, nom_utilisateur, mot_de_passe_hash,
                                LENGTH(mot_de_passe_hash) as hash_length,
                                actif, compte_verrouille, tentatives_echec
                                FROM utilisateurs
                                WHERE nom_utilisateur = @username";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@username", username);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string hashFromDb = reader.GetString("mot_de_passe_hash");
                    int hashLength = reader.GetInt32("hash_length");
                    bool actif = reader.GetBoolean("actif");
                    bool verrouille = reader.GetBoolean("compte_verrouille");
                    int tentatives = reader.GetInt32("tentatives_echec");

                    results.AppendLine("--- UTILISATEUR TROUVÉ ---");
                    results.AppendLine($"ID: {reader.GetInt32("id")}");
                    results.AppendLine($"Actif: {actif}");
                    results.AppendLine($"Verrouillé: {verrouille}");
                    results.AppendLine($"Tentatives échouées: {tentatives}");
                    results.AppendLine();

                    results.AppendLine("--- HASH STOCKÉ ---");
                    results.AppendLine($"Longueur: {hashLength} caractères");
                    results.AppendLine($"Hash complet: {hashFromDb}");
                    results.AppendLine($"30 premiers caractères: {hashFromDb.Substring(0, Math.Min(30, hashFromDb.Length))}");
                    results.AppendLine();

                    reader.Close();

                    // Tester la vérification
                    results.AppendLine("--- TEST DE VÉRIFICATION ---");
                    try
                    {
                        bool isValid = PasswordHasher.VerifyPassword(password, hashFromDb);
                        results.AppendLine($"Résultat VerifyPassword: {isValid}");

                        if (isValid)
                        {
                            results.AppendLine("✓ LE MOT DE PASSE EST CORRECT !");
                        }
                        else
                        {
                            results.AppendLine("✗ LE MOT DE PASSE NE CORRESPOND PAS");

                            // Test avec BCrypt direct
                            bool bcryptDirect = BCrypt.Net.BCrypt.Verify(password, hashFromDb);
                            results.AppendLine($"Test BCrypt direct: {bcryptDirect}");
                        }
                    }
                    catch (Exception ex)
                    {
                        results.AppendLine($"ERREUR lors de la vérification: {ex.Message}");
                        results.AppendLine($"Type: {ex.GetType().Name}");
                        results.AppendLine($"Stack: {ex.StackTrace}");
                    }
                }
                else
                {
                    results.AppendLine("✗ UTILISATEUR NON TROUVÉ DANS LA BASE DE DONNÉES");
                }
            }
            catch (Exception ex)
            {
                results.AppendLine($"ERREUR DATABASE: {ex.Message}");
                results.AppendLine($"Stack: {ex.StackTrace}");
            }
            finally
            {
                db.closeConnect();
            }

            textBoxResults.Text = results.ToString();
        }

        private void buttonGenerateHash_Click(object sender, EventArgs e)
        {
            string password = textBoxPassword.Text;

            var results = new System.Text.StringBuilder();
            results.AppendLine("=== GÉNÉRATION DE HASH ===");
            results.AppendLine($"Mot de passe: {password}");
            results.AppendLine();

            try
            {
                string newHash = PasswordHasher.HashPassword(password);
                results.AppendLine($"Nouveau hash généré:");
                results.AppendLine($"Longueur: {newHash.Length} caractères");
                results.AppendLine($"Hash: {newHash}");
                results.AppendLine();

                // Vérifier immédiatement
                bool verify = PasswordHasher.VerifyPassword(password, newHash);
                results.AppendLine($"Vérification immédiate: {verify}");
                results.AppendLine();

                results.AppendLine("--- COMMANDE SQL POUR METTRE À JOUR ---");
                results.AppendLine($"UPDATE utilisateurs");
                results.AppendLine($"SET mot_de_passe_hash = '{newHash}',");
                results.AppendLine($"    tentatives_echec = 0,");
                results.AppendLine($"    compte_verrouille = 0");
                results.AppendLine($"WHERE nom_utilisateur = '{textBoxUsername.Text.Trim()}';");
            }
            catch (Exception ex)
            {
                results.AppendLine($"ERREUR: {ex.Message}");
            }

            textBoxResults.Text = results.ToString();
        }
    }
}
