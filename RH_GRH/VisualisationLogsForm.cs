using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using RH_GRH.Auth;

namespace RH_GRH
{
    public partial class VisualisationLogsForm : Form
    {
        private Dbconnect db = new Dbconnect();

        public VisualisationLogsForm()
        {
            InitializeComponent();

            this.Load += VisualisationLogsForm_Load;
        }

        private void VisualisationLogsForm_Load(object sender, EventArgs e)
        {
            // Vérifier les permissions
            if (!PermissionManager.CheckPermission(PermissionManager.SYSTEM_LOGS))
            {
                this.Close();
                return;
            }

            InitialiserFiltres();
            ChargerLogs();
            ConfigurerDataGridView();
        }

        private void InitialiserFiltres()
        {
            // Remplir le combo des modules
            comboBoxModule.Items.Add("Tous");
            comboBoxModule.Items.Add("Personnel");
            comboBoxModule.Items.Add("Salaire");
            comboBoxModule.Items.Add("Administration");
            comboBoxModule.Items.Add("Système");
            comboBoxModule.Items.Add("Authentification");
            comboBoxModule.SelectedIndex = 0;

            // Remplir le combo des résultats
            comboBoxResultat.Items.Add("Tous");
            comboBoxResultat.Items.Add("SUCCESS");
            comboBoxResultat.Items.Add("FAILURE");
            comboBoxResultat.Items.Add("WARNING");
            comboBoxResultat.SelectedIndex = 0;

            // Dates par défaut : derniers 7 jours
            datePickerDebut.Value = DateTime.Now.AddDays(-7);
            datePickerFin.Value = DateTime.Now;
        }

        private void ConfigurerDataGridView()
        {
            dataGridViewLogs.AutoGenerateColumns = false;
            dataGridViewLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewLogs.MultiSelect = false;
            dataGridViewLogs.ReadOnly = true;
            dataGridViewLogs.AllowUserToAddRows = false;
            dataGridViewLogs.RowHeadersVisible = false;
        }

        private void ChargerLogs()
        {
            try
            {
                db.openConnect();

                string moduleFiltre = comboBoxModule.SelectedItem.ToString();
                string resultatFiltre = comboBoxResultat.SelectedItem.ToString();
                DateTime dateDebut = datePickerDebut.Value.Date;
                DateTime dateFin = datePickerFin.Value.Date.AddDays(1).AddSeconds(-1);

                string query = @"SELECT
                                id,
                                nom_utilisateur,
                                action,
                                module,
                                details,
                                date_action,
                                ip_address,
                                resultat
                            FROM logs_activite
                            WHERE date_action BETWEEN @dateDebut AND @dateFin";

                if (moduleFiltre != "Tous")
                {
                    query += " AND module = @module";
                }

                if (resultatFiltre != "Tous")
                {
                    query += " AND resultat = @resultat";
                }

                if (!string.IsNullOrWhiteSpace(textBoxRecherche.Text))
                {
                    query += @" AND (nom_utilisateur LIKE @recherche
                                OR action LIKE @recherche
                                OR details LIKE @recherche)";
                }

                query += " ORDER BY date_action DESC LIMIT 1000";

                MySqlCommand cmd = new MySqlCommand(query, db.getconnection);
                cmd.Parameters.AddWithValue("@dateDebut", dateDebut);
                cmd.Parameters.AddWithValue("@dateFin", dateFin);

                if (moduleFiltre != "Tous")
                    cmd.Parameters.AddWithValue("@module", moduleFiltre);

                if (resultatFiltre != "Tous")
                    cmd.Parameters.AddWithValue("@resultat", resultatFiltre);

                if (!string.IsNullOrWhiteSpace(textBoxRecherche.Text))
                    cmd.Parameters.AddWithValue("@recherche", $"%{textBoxRecherche.Text.Trim()}%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridViewLogs.DataSource = dt;

                // Mettre à jour le label
                labelNombreLogs.Text = $"Total : {dt.Rows.Count} log(s) (limité à 1000)";

                AuditLogger.LogView("Système", "Logs", $"Consultation logs - Période: {dateDebut:dd/MM/yyyy} - {dateFin:dd/MM/yyyy}");
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show($"Erreur lors du chargement des logs :\n{ex.Message}",
                    "Erreur", CustomMessageBox.MessageType.Error);
            }
            finally
            {
                db.closeConnect();
            }
        }

        private void buttonFiltrer_Click(object sender, EventArgs e)
        {
            ChargerLogs();
        }

        private void buttonActualiser_Click(object sender, EventArgs e)
        {
            ChargerLogs();
        }

        private void buttonExporter_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Show("Fonctionnalité d'export en cours de développement\nVous pourrez exporter les logs en Excel ou PDF prochainement",
                "Info", CustomMessageBox.MessageType.Info);
        }

        private void textBoxRecherche_TextChanged(object sender, EventArgs e)
        {
            ChargerLogs();
        }
    }
}
