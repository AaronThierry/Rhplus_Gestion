using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RH_GRH.Auth;
using RH_GRH.Auth.Models;

namespace RH_GRH
{
    public partial class GestionRolesPermissionsForm : Form
    {
        private RoleService roleService;
        private List<Role> roles;
        private List<Permission> allPermissions;

        public GestionRolesPermissionsForm()
        {
            InitializeComponent();
            roleService = new RoleService();
            ConfigureForm();
            LoadData();
        }

        private void ConfigureForm()
        {
            this.Text = "Gestion des Rôles et Permissions";
            this.Size = new Size(1200, 700);
            this.MinimumSize = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 245, 250);
        }

        private void LoadData()
        {
            try
            {
                // Initialiser les permissions par défaut si nécessaire
                roleService.InitializeDefaultPermissions();

                // Charger les rôles et permissions
                roles = roleService.GetAllRoles();
                allPermissions = roleService.GetAllPermissions();

                LoadRolesIntoDataGridView();
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des données: {ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRolesIntoDataGridView()
        {
            dataGridViewRoles.Rows.Clear();

            foreach (var role in roles)
            {
                int index = dataGridViewRoles.Rows.Add();
                DataGridViewRow row = dataGridViewRoles.Rows[index];

                row.Cells["ColumnId"].Value = role.Id;
                row.Cells["ColumnNomRole"].Value = role.NomRole;
                row.Cells["ColumnDescription"].Value = role.Description;
                row.Cells["ColumnNiveauAcces"].Value = role.NiveauAcces;
                row.Cells["ColumnNbPermissions"].Value = role.Permissions?.Count ?? 0;
                row.Cells["ColumnDateCreation"].Value = role.DateCreation.ToString("dd/MM/yyyy");

                row.Tag = role;
            }
        }

        private void UpdateStatistics()
        {
            // Les statistiques sont maintenant affichées dans le sous-titre du header
            int totalRoles = roles?.Count ?? 0;
            int totalPermissions = allPermissions?.Count ?? 0;
            headerEntreprise.SousTitre = $"{totalRoles} rôle(s) • {totalPermissions} permission(s) configurées";
        }

        private void buttonAjouter_Click(object sender, EventArgs e)
        {
            using (AjouterModifierRoleForm form = new AjouterModifierRoleForm(allPermissions))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        roleService.CreateRole(form.Role, form.SelectedPermissionIds);
                        MessageBox.Show("Rôle créé avec succès!", "Succès",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de la création du rôle: {ex.Message}",
                            "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonModifier_Click(object sender, EventArgs e)
        {
            if (dataGridViewRoles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un rôle à modifier.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Role selectedRole = dataGridViewRoles.SelectedRows[0].Tag as Role;

            if (selectedRole == null) return;

            using (AjouterModifierRoleForm form = new AjouterModifierRoleForm(allPermissions, selectedRole))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        roleService.UpdateRole(form.Role, form.SelectedPermissionIds);
                        MessageBox.Show("Rôle modifié avec succès!", "Succès",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erreur lors de la modification du rôle: {ex.Message}",
                            "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonSupprimer_Click(object sender, EventArgs e)
        {
            if (dataGridViewRoles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un rôle à supprimer.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Role selectedRole = dataGridViewRoles.SelectedRows[0].Tag as Role;

            if (selectedRole == null) return;

            // Confirmation
            DialogResult result = MessageBox.Show(
                $"Êtes-vous sûr de vouloir supprimer le rôle '{selectedRole.NomRole}' ?\n\nCette action est irréversible.",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    roleService.DeleteRole(selectedRole.Id);
                    MessageBox.Show("Rôle supprimé avec succès!", "Succès",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la suppression du rôle: {ex.Message}",
                        "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonActualiser_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridViewRoles_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewRoles.SelectedRows.Count > 0)
            {
                Role selectedRole = dataGridViewRoles.SelectedRows[0].Tag as Role;
                if (selectedRole != null)
                {
                    DisplayRolePermissions(selectedRole);
                }
            }
            else
            {
                listBoxPermissions.Items.Clear();
            }
        }

        private void DisplayRolePermissions(Role role)
        {
            listBoxPermissions.Items.Clear();

            if (role.Permissions == null || role.Permissions.Count == 0)
            {
                listBoxPermissions.Items.Add("Aucune permission assignée");
                return;
            }

            // Grouper par module
            var grouped = role.Permissions.GroupBy(p => p.Module).OrderBy(g => g.Key);

            foreach (var group in grouped)
            {
                listBoxPermissions.Items.Add($"━━ {group.Key} ━━");

                foreach (var permission in group)
                {
                    listBoxPermissions.Items.Add($"  ✓ {permission.Action} - {permission.Description}");
                }

                listBoxPermissions.Items.Add("");  // Ligne vide entre les modules
            }
        }

        private void dataGridViewRoles_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                buttonModifier_Click(sender, e);
            }
        }

        private void textBoxRecherche_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = textBoxRecherche.Text.ToLower();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                LoadRolesIntoDataGridView();
                return;
            }

            var filteredRoles = roles.Where(r =>
                r.NomRole.ToLower().Contains(searchTerm) ||
                (r.Description != null && r.Description.ToLower().Contains(searchTerm))
            ).ToList();

            dataGridViewRoles.Rows.Clear();

            foreach (var role in filteredRoles)
            {
                int index = dataGridViewRoles.Rows.Add();
                DataGridViewRow row = dataGridViewRoles.Rows[index];

                row.Cells["ColumnId"].Value = role.Id;
                row.Cells["ColumnNomRole"].Value = role.NomRole;
                row.Cells["ColumnDescription"].Value = role.Description;
                row.Cells["ColumnNiveauAcces"].Value = role.NiveauAcces;
                row.Cells["ColumnNbPermissions"].Value = role.Permissions?.Count ?? 0;
                row.Cells["ColumnDateCreation"].Value = role.DateCreation.ToString("dd/MM/yyyy");

                row.Tag = role;
            }
        }
    }
}
