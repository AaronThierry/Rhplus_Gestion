using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RH_GRH
{
    /// <summary>
    /// ComboBox avec TextBox de recherche intégré
    /// L'utilisateur tape dans le TextBox, le ComboBox se filtre automatiquement
    /// </summary>
    public class SearchableComboBox : UserControl
    {
        private Guna2TextBox textBoxSearch;
        private Guna2ComboBox comboBox;
        private DataTable sourceData;
        private string displayMember;
        private string valueMember;
        private bool isUpdating = false;

        public SearchableComboBox()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Configuration du UserControl
            this.Size = new Size(300, 71);
            this.Padding = new Padding(0);
            this.Margin = new Padding(0);

            // TextBox de recherche - Style cohérent avec le reste du formulaire
            textBoxSearch = new Guna2TextBox
            {
                PlaceholderText = "🔍 Rechercher...",
                Font = new Font("Montserrat SemiBold", 9F, FontStyle.Bold),
                Size = new Size(300, 30),
                Location = new Point(0, 0),
                BorderRadius = 0,
                BorderThickness = 2,
                BorderColor = Color.FromArgb(64, 64, 64),
                ForeColor = Color.Black,
                FillColor = Color.White
            };

            // ComboBox - Style cohérent avec le reste du formulaire
            comboBox = new Guna2ComboBox
            {
                Font = new Font("Montserrat SemiBold", 10F, FontStyle.Bold),
                Size = new Size(300, 36),
                Location = new Point(0, 35),
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                BorderRadius = 0,
                BorderThickness = 2,
                BorderColor = Color.FromArgb(64, 64, 64),
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList,
                ItemHeight = 30,
                MaxDropDownItems = 5, // Afficher 5 lignes maximum
                IntegralHeight = true, // Active le scroll automatique au-delà de 5 items
                DropDownHeight = 150 // 5 items × 30px
            };

            // Événement de recherche
            textBoxSearch.TextChanged += TextBoxSearch_TextChanged;

            // Ajouter les contrôles
            this.Controls.Add(textBoxSearch);
            this.Controls.Add(comboBox);
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (isUpdating || sourceData == null) return;

            string searchText = textBoxSearch.Text.Trim();

            try
            {
                isUpdating = true;

                // Sauvegarder la sélection actuelle
                object currentValue = comboBox.SelectedValue;

                DataTable filteredData;

                if (string.IsNullOrEmpty(searchText))
                {
                    // Afficher toutes les données
                    filteredData = sourceData.Copy();
                }
                else
                {
                    // Filtrer les données
                    filteredData = sourceData.Clone();
                    foreach (DataRow row in sourceData.Rows)
                    {
                        string displayValue = row[displayMember]?.ToString() ?? "";
                        if (displayValue.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            filteredData.ImportRow(row);
                        }
                    }
                }

                // Mettre à jour le ComboBox
                comboBox.DataSource = null;
                comboBox.DataSource = filteredData;
                comboBox.DisplayMember = displayMember;
                comboBox.ValueMember = valueMember;

                // Forcer la hauteur du dropdown à 5 items maximum
                comboBox.DropDownHeight = 150; // 5 items × 30px

                // Restaurer la sélection si elle existe encore
                if (currentValue != null && filteredData.Rows.Count > 0)
                {
                    try
                    {
                        var exists = filteredData.AsEnumerable()
                            .Any(row => row[valueMember].ToString() == currentValue.ToString());
                        if (exists)
                        {
                            comboBox.SelectedValue = currentValue;
                        }
                    }
                    catch { }
                }

                // Ouvrir la liste si des résultats existent
                if (filteredData.Rows.Count > 0 && !string.IsNullOrEmpty(searchText))
                {
                    comboBox.DroppedDown = true;
                }
            }
            finally
            {
                isUpdating = false;
            }
        }

        /// <summary>
        /// Charge les données dans le ComboBox
        /// </summary>
        public void SetDataSource(DataTable data, string displayMemberName, string valueMemberName)
        {
            this.sourceData = data?.Copy();
            this.displayMember = displayMemberName;
            this.valueMember = valueMemberName;

            if (data != null)
            {
                comboBox.DataSource = data;
                comboBox.DisplayMember = displayMemberName;
                comboBox.ValueMember = valueMemberName;

                // Forcer la hauteur du dropdown à 5 items maximum
                comboBox.DropDownHeight = 150; // 5 items × 30px
            }
            else
            {
                comboBox.DataSource = null;
            }
        }

        /// <summary>
        /// Efface le TextBox de recherche
        /// </summary>
        public void ClearSearch()
        {
            textBoxSearch.Clear();
        }

        /// <summary>
        /// Obtient ou définit la valeur sélectionnée
        /// </summary>
        public object SelectedValue
        {
            get => comboBox.SelectedValue;
            set => comboBox.SelectedValue = value;
        }

        /// <summary>
        /// Obtient l'index sélectionné
        /// </summary>
        public int SelectedIndex
        {
            get => comboBox.SelectedIndex;
            set => comboBox.SelectedIndex = value;
        }

        /// <summary>
        /// Événement déclenché quand la sélection change
        /// </summary>
        public event EventHandler SelectedIndexChanged
        {
            add => comboBox.SelectedIndexChanged += value;
            remove => comboBox.SelectedIndexChanged -= value;
        }

        /// <summary>
        /// Obtient le ComboBox interne (pour accès avancé)
        /// </summary>
        public Guna2ComboBox InnerComboBox => comboBox;

        /// <summary>
        /// Obtient le TextBox de recherche interne (pour accès avancé)
        /// </summary>
        public Guna2TextBox InnerTextBox => textBoxSearch;
    }
}
