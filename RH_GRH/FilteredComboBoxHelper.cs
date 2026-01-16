using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace RH_GRH
{
    /// <summary>
    /// Classe utilitaire simple pour activer l'autocomplete sur les ComboBox
    /// Solution native, simple et efficace
    /// </summary>
    public static class FilteredComboBoxHelper
    {
        /// <summary>
        /// Active l'autocomplete natif sur un ComboBox
        /// L'utilisateur tape et voit les suggestions automatiquement
        /// </summary>
        public static void EnableAutoComplete(
            Guna2ComboBox combo,
            DataTable sourceData,
            string displayMember,
            string valueMember)
        {
            if (combo == null || sourceData == null) return;

            try
            {
                // Charger les données
                combo.DataSource = sourceData;
                combo.DisplayMember = displayMember;
                combo.ValueMember = valueMember;

                // Configurer l'autocomplete - SIMPLE ET EFFICACE
                combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                combo.AutoCompleteSource = AutoCompleteSource.ListItems;

                // Style pour une meilleure UX
                combo.ForeColor = Color.Black;
                combo.Font = new Font("Montserrat", 9F, FontStyle.Regular);
            }
            catch (Exception ex)
            {
                // En cas d'erreur, charger sans autocomplete
                combo.DataSource = sourceData;
                combo.DisplayMember = displayMember;
                combo.ValueMember = valueMember;
            }
        }

        /// <summary>
        /// Active l'autocomplete sur plusieurs ComboBox d'un coup
        /// </summary>
        public static void EnableAutoCompleteMultiple(
            params (Guna2ComboBox combo, DataTable sourceData, string displayMember, string valueMember)[] combos)
        {
            foreach (var item in combos)
            {
                EnableAutoComplete(item.combo, item.sourceData, item.displayMember, item.valueMember);
            }
        }

    }
}
