using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RH_GRH.Auth.Models;

namespace RH_GRH
{
    public partial class AjouterModifierRoleForm : Form
    {
        private Role _role;
        private List<Permission> _allPermissions;
        private List<int> _selectedPermissionIds;
        private bool _isEditMode;

        public Role Role => _role;
        public List<int> SelectedPermissionIds => _selectedPermissionIds;

        public AjouterModifierRoleForm(List<Permission> allPermissions, Role roleToEdit = null)
        {
            InitializeComponent();
            _allPermissions = allPermissions;
            _isEditMode = roleToEdit != null;
            _selectedPermissionIds = new List<int>();

            if (_isEditMode)
            {
                _role = roleToEdit;
                LoadRoleData();
                this.Text = "Modifier un Rôle";
                labelTitle.Text = "Modifier un Rôle";
            }
            else
            {
                _role = new Role();
                this.Text = "Ajouter un Rôle";
                labelTitle.Text = "Ajouter un Rôle";
            }

            ConfigureForm();
            LoadPermissions();
        }

        private void ConfigureForm()
        {
            this.Size = new Size(900, 650);
            this.MinimumSize = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 245, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void LoadRoleData()
        {
            textBoxNomRole.Text = _role.NomRole;
            textBoxDescription.Text = _role.Description;
            numericUpDownNiveauAcces.Value = _role.NiveauAcces;

            if (_role.Permissions != null)
            {
                _selectedPermissionIds = _role.Permissions.Select(p => p.Id).ToList();
            }
        }

        private void LoadPermissions()
        {
            checkedListBoxPermissions.Items.Clear();

            // Grouper les permissions par module
            var groupedPermissions = _allPermissions.GroupBy(p => p.Module).OrderBy(g => g.Key);

            foreach (var group in groupedPermissions)
            {
                // Ajouter le nom du module comme séparateur (non-cochable)
                int headerIndex = checkedListBoxPermissions.Items.Add($"━━━ {group.Key} ━━━");
                checkedListBoxPermissions.SetItemCheckState(headerIndex, CheckState.Indeterminate);

                // Ajouter les permissions du module
                foreach (var permission in group.OrderBy(p => p.Action))
                {
                    int index = checkedListBoxPermissions.Items.Add(permission);

                    // Cocher si la permission fait partie du rôle
                    if (_selectedPermissionIds.Contains(permission.Id))
                    {
                        checkedListBoxPermissions.SetItemChecked(index, true);
                    }
                }
            }
        }

        private void buttonSelectionnerTout_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxPermissions.Items.Count; i++)
            {
                if (checkedListBoxPermissions.Items[i] is Permission)
                {
                    checkedListBoxPermissions.SetItemChecked(i, true);
                }
            }
        }

        private void buttonDeselectionnerTout_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxPermissions.Items.Count; i++)
            {
                if (checkedListBoxPermissions.Items[i] is Permission)
                {
                    checkedListBoxPermissions.SetItemChecked(i, false);
                }
            }
        }

        private void buttonEnregistrer_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            _role.NomRole = textBoxNomRole.Text.Trim();
            _role.Description = textBoxDescription.Text.Trim();
            _role.NiveauAcces = (int)numericUpDownNiveauAcces.Value;

            // Récupérer les IDs des permissions sélectionnées
            _selectedPermissionIds.Clear();
            for (int i = 0; i < checkedListBoxPermissions.Items.Count; i++)
            {
                if (checkedListBoxPermissions.GetItemChecked(i) &&
                    checkedListBoxPermissions.Items[i] is Permission permission)
                {
                    _selectedPermissionIds.Add(permission.Id);
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textBoxNomRole.Text))
            {
                MessageBox.Show("Le nom du rôle est obligatoire.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNomRole.Focus();
                return false;
            }

            if (textBoxNomRole.Text.Length < 3)
            {
                MessageBox.Show("Le nom du rôle doit contenir au moins 3 caractères.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNomRole.Focus();
                return false;
            }

            if (numericUpDownNiveauAcces.Value < 1 || numericUpDownNiveauAcces.Value > 100)
            {
                MessageBox.Show("Le niveau d'accès doit être entre 1 et 100.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numericUpDownNiveauAcces.Focus();
                return false;
            }

            return true;
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void checkedListBoxPermissions_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Empêcher de cocher/décocher les en-têtes
            if (!(checkedListBoxPermissions.Items[e.Index] is Permission))
            {
                e.NewValue = CheckState.Indeterminate;
            }
        }

        private void textBoxRecherche_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = textBoxRecherche.Text.ToLower();

            checkedListBoxPermissions.Items.Clear();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                LoadPermissions();
                return;
            }

            // Filtrer les permissions
            var filteredPermissions = _allPermissions.Where(p =>
                p.NomPermission.ToLower().Contains(searchTerm) ||
                p.Module.ToLower().Contains(searchTerm) ||
                p.Action.ToLower().Contains(searchTerm) ||
                (p.Description != null && p.Description.ToLower().Contains(searchTerm))
            ).ToList();

            var groupedPermissions = filteredPermissions.GroupBy(p => p.Module).OrderBy(g => g.Key);

            foreach (var group in groupedPermissions)
            {
                int headerIndex = checkedListBoxPermissions.Items.Add($"━━━ {group.Key} ━━━");
                checkedListBoxPermissions.SetItemCheckState(headerIndex, CheckState.Indeterminate);

                foreach (var permission in group.OrderBy(p => p.Action))
                {
                    int index = checkedListBoxPermissions.Items.Add(permission);

                    if (_selectedPermissionIds.Contains(permission.Id))
                    {
                        checkedListBoxPermissions.SetItemChecked(index, true);
                    }
                }
            }
        }
    }
}
