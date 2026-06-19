using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace RH_GRH
{
    public partial class SelectionEntrepriseModernForm : Form
    {
        private DataTable entreprisesData;
        private List<EntrepriseCard> entrepriseCards = new List<EntrepriseCard>();
        private EntrepriseCard selectedCard;
        private int animationIndex = 0;

        public int EntrepriseSelectionneeId { get; private set; }
        public string EntrepriseSelectionneeNom { get; private set; }
        public DateTime PeriodeDebut { get; private set; }
        public DateTime PeriodeFin { get; private set; }

        public SelectionEntrepriseModernForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint, true);
            this.UpdateStyles();

            // Arrondir les coins du formulaire
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // Configurer le label counter avec coins arrondis
            ConfigureCounterLabel();
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        private void ConfigureCounterLabel()
        {
            // Créer une région arrondie pour le label counter
            var path = new GraphicsPath();
            path.AddArc(0, 0, 20, 20, 180, 90);
            path.AddArc(labelCounter.Width - 20, 0, 20, 20, 270, 90);
            path.AddArc(labelCounter.Width - 20, labelCounter.Height - 20, 20, 20, 0, 90);
            path.AddArc(0, labelCounter.Height - 20, 20, 20, 90, 90);
            path.CloseFigure();

            labelCounter.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(Color.FromArgb(59, 130, 246)))
                {
                    e.Graphics.FillPath(brush, path);
                }

                var sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                using (var textBrush = new SolidBrush(Color.White))
                {
                    e.Graphics.DrawString(labelCounter.Text, labelCounter.Font, textBrush,
                        labelCounter.ClientRectangle, sf);
                }
            };
        }

        private void SelectionEntrepriseModernForm_Load(object sender, EventArgs e)
        {
            LoadEntreprises();
            InitializeAnimation();
        }

        private void LoadEntreprises()
        {
            try
            {
                Dbconnect connect = new Dbconnect();
                entreprisesData = new DataTable();

                using (MySqlConnection connection = new MySqlConnection(connect.getconnection.ConnectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT id_entreprise, sigle, nomEntreprise
                        FROM entreprise
                        ORDER BY nomEntreprise";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(entreprisesData);
                    }
                }

                DisplayEntreprises(entreprisesData);
                UpdateCounter(entreprisesData.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des entreprises : {ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayEntreprises(DataTable data)
        {
            flowPanelEntreprises.SuspendLayout();
            flowPanelEntreprises.Controls.Clear();
            entrepriseCards.Clear();
            animationIndex = 0;

            foreach (DataRow row in data.Rows)
            {
                var card = new EntrepriseCard
                {
                    EntrepriseId = Convert.ToInt32(row["id_entreprise"]),
                    Sigle = row["sigle"].ToString(),
                    NomEntreprise = row["nomEntreprise"].ToString(),
                    Width = 320,
                    Height = 100,
                    Margin = new Padding(8, 8, 8, 8),
                    Opacity = 0 // Commencer invisible pour l'animation
                };

                card.CardClicked += EntrepriseCard_Clicked;
                card.CardDoubleClicked += EntrepriseCard_DoubleClicked;
                entrepriseCards.Add(card);
                flowPanelEntreprises.Controls.Add(card);
            }

            flowPanelEntreprises.ResumeLayout();

            // Démarrer l'animation d'apparition
            if (entrepriseCards.Count > 0)
            {
                animationTimer.Start();
            }
        }

        private void InitializeAnimation()
        {
            // L'animation sera déclenchée après le chargement des données
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            if (animationIndex < entrepriseCards.Count)
            {
                var card = entrepriseCards[animationIndex];
                AnimateCardAppearance(card, animationIndex);
                animationIndex++;
            }
            else
            {
                animationTimer.Stop();
                animationIndex = 0;
            }
        }

        private async void AnimateCardAppearance(EntrepriseCard card, int index)
        {
            // Animation de fondu et de glissement
            int steps = 10;
            int delay = 20;

            for (int i = 0; i <= steps; i++)
            {
                card.Opacity = (float)i / steps;
                card.AnimationOffset = 20 - (20 * i / steps);
                card.Invalidate();
                await System.Threading.Tasks.Task.Delay(delay);
            }
        }

        private void EntrepriseCard_Clicked(object sender, EventArgs e)
        {
            var clickedCard = sender as EntrepriseCard;
            if (clickedCard == null) return;

            // Désélectionner l'ancienne carte
            if (selectedCard != null && selectedCard != clickedCard)
            {
                selectedCard.IsSelected = false;
            }

            // Sélectionner la nouvelle carte
            selectedCard = clickedCard;
            selectedCard.IsSelected = true;

            // Activer le bouton de validation
            buttonValidate.Enabled = true;

            // Afficher la sélection
            labelSelection.Text = $"Sélection : {selectedCard.NomEntreprise} ({selectedCard.Sigle})";
            labelSelection.Visible = true;
        }

        private void EntrepriseCard_DoubleClicked(object sender, EventArgs e)
        {
            var clickedCard = sender as EntrepriseCard;
            if (clickedCard == null) return;

            // Sélectionner la carte
            if (selectedCard != null && selectedCard != clickedCard)
            {
                selectedCard.IsSelected = false;
            }
            selectedCard = clickedCard;
            selectedCard.IsSelected = true;

            // Valider directement
            EntrepriseSelectionneeId = selectedCard.EntrepriseId;
            EntrepriseSelectionneeNom = selectedCard.NomEntreprise;
            PeriodeDebut = datePickerDebut.Value;
            PeriodeFin = datePickerFin.Value;

            if (PeriodeDebut > PeriodeFin)
            {
                MessageBox.Show("La date de début ne peut pas être postérieure à la date de fin.",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (entreprisesData == null) return;

            string searchText = txtSearch.Text.Trim().ToLower();
            DataView dv = entreprisesData.DefaultView;

            if (string.IsNullOrEmpty(searchText))
            {
                dv.RowFilter = "";
            }
            else
            {
                dv.RowFilter = $"nomEntreprise LIKE '%{searchText}%' OR sigle LIKE '%{searchText}%'";
            }

            DisplayEntreprises(dv.ToTable());
            UpdateCounter(dv.Count);
        }

        private void UpdateCounter(int count)
        {
            labelCounter.Text = count == 1 ? "1 entreprise" : $"{count} entreprises";
            labelCounter.Invalidate();
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            if (selectedCard == null)
            {
                MessageBox.Show("Veuillez sélectionner une entreprise.", "Attention",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EntrepriseSelectionneeId = selectedCard.EntrepriseId;
            EntrepriseSelectionneeNom = selectedCard.NomEntreprise;
            PeriodeDebut = datePickerDebut.Value;
            PeriodeFin = datePickerFin.Value;

            if (PeriodeDebut > PeriodeFin)
            {
                MessageBox.Show("La date de début ne peut pas être postérieure à la date de fin.",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void datePickerDebut_ValueChanged(object sender, EventArgs e)
        {
            // Mettre à jour la MinDate
            datePickerFin.MinDate = datePickerDebut.Value.Date;

            // Calculer le dernier jour du mois sélectionné
            DateTime dateDebut = datePickerDebut.Value;
            int dernierJour = DateTime.DaysInMonth(dateDebut.Year, dateDebut.Month);
            DateTime dateFin = new DateTime(dateDebut.Year, dateDebut.Month, dernierJour);

            // Mettre automatiquement la date de fin au dernier jour du mois
            datePickerFin.Value = dateFin;
        }
    }

    // Classe personnalisée pour la carte d'entreprise
    public class EntrepriseCard : Panel
    {
        public int EntrepriseId { get; set; }
        public string Sigle { get; set; }
        public string NomEntreprise { get; set; }
        public bool IsSelected { get; set; }
        public float Opacity { get; set; } = 1f;
        public int AnimationOffset { get; set; } = 0;

        private bool isHovered = false;
        private int hoverAnimationProgress = 0;
        private Timer hoverTimer;

        public event EventHandler CardClicked;
        public event EventHandler CardDoubleClicked;

        public EntrepriseCard()
        {
            this.DoubleBuffered = true;
            this.Cursor = Cursors.Hand;

            hoverTimer = new Timer { Interval = 15 };
            hoverTimer.Tick += (s, e) =>
            {
                if (isHovered && hoverAnimationProgress < 100)
                {
                    hoverAnimationProgress += 10;
                    this.Invalidate();
                }
                else if (!isHovered && hoverAnimationProgress > 0)
                {
                    hoverAnimationProgress -= 10;
                    this.Invalidate();
                }
                else
                {
                    hoverTimer.Stop();
                }
            };
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovered = true;
            hoverTimer.Start();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovered = false;
            hoverTimer.Start();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            CardClicked?.Invoke(this, e);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            CardDoubleClicked?.Invoke(this, e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Appliquer l'opacité pour l'animation d'apparition
            if (Opacity < 1f)
            {
                var cm = new ColorMatrix
                {
                    Matrix33 = Opacity
                };
                var ia = new System.Drawing.Imaging.ImageAttributes();
                ia.SetColorMatrix(cm);
            }

            // Calculer le décalage Y pour l'animation
            int yOffset = AnimationOffset;

            // Calculer l'élévation du hover
            float elevation = hoverAnimationProgress / 100f * 8;

            // Dessiner l'ombre
            using (var shadowPath = CreateRoundedRectangle(
                new Rectangle(2, 4 + (int)elevation + yOffset, Width - 4, Height - 8), 16))
            {
                using (var shadowBrush = new SolidBrush(Color.FromArgb(
                    (int)(20 * Opacity), 15, 23, 42)))
                {
                    e.Graphics.FillPath(shadowBrush, shadowPath);
                }
            }

            // Couleur de fond et bordure selon l'état
            Color bgColor, borderColor;
            if (IsSelected)
            {
                bgColor = Color.FromArgb(239, 246, 255);
                borderColor = Color.FromArgb(59, 130, 246);
            }
            else
            {
                bgColor = Color.White;
                borderColor = Color.FromArgb(226, 232, 240);
            }

            // Dessiner le fond de la carte
            using (var cardPath = CreateRoundedRectangle(
                new Rectangle(0, yOffset, Width - 4, Height - 8), 16))
            {
                using (var bgBrush = new SolidBrush(Color.FromArgb((int)(255 * Opacity), bgColor)))
                {
                    e.Graphics.FillPath(bgBrush, cardPath);
                }

                // Dessiner la bordure
                int borderWidth = IsSelected ? 2 : 1;
                using (var borderPen = new Pen(Color.FromArgb((int)(255 * Opacity), borderColor), borderWidth))
                {
                    e.Graphics.DrawPath(borderPen, cardPath);
                }
            }

            // Dessiner le sigle (badge en haut à gauche)
            if (!string.IsNullOrEmpty(Sigle))
            {
                using (var siglePath = CreateRoundedRectangle(
                    new Rectangle(12, 12 + yOffset, 65, 24), 6))
                {
                    using (var sigleBrush = new SolidBrush(
                        Color.FromArgb((int)(255 * Opacity), 59, 130, 246)))
                    {
                        e.Graphics.FillPath(sigleBrush, siglePath);
                    }

                    var sigleFont = new Font("Montserrat", 7.5F, FontStyle.Bold);
                    var sigleBrushText = new SolidBrush(Color.FromArgb((int)(255 * Opacity), Color.White));
                    var sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    e.Graphics.DrawString(Sigle, sigleFont, sigleBrushText,
                        new Rectangle(12, 12 + yOffset, 65, 24), sf);
                }
            }

            // Dessiner le nom de l'entreprise
            var nameFont = new Font("Montserrat", 9.5F, FontStyle.Bold);
            var nameBrush = new SolidBrush(Color.FromArgb((int)(255 * Opacity), 15, 23, 42));
            e.Graphics.DrawString(NomEntreprise, nameFont, nameBrush,
                new RectangleF(12, 45 + yOffset, Width - 24, 45));

            // Dessiner l'icône de sélection si sélectionné
            if (IsSelected)
            {
                var checkFont = new Font("Segoe UI", 14F);
                var checkBrush = new SolidBrush(Color.FromArgb((int)(255 * Opacity), 59, 130, 246));
                e.Graphics.DrawString("✓", checkFont, checkBrush, Width - 35, Height - 32 + yOffset);
            }

            // Effet de brillance au hover
            if (hoverAnimationProgress > 0)
            {
                using (var shinePath = CreateRoundedRectangle(
                    new Rectangle(0, yOffset, Width - 4, Height - 8), 16))
                {
                    using (var shineBrush = new SolidBrush(
                        Color.FromArgb(hoverAnimationProgress * 5 / 100, 255, 255, 255)))
                    {
                        e.Graphics.FillPath(shineBrush, shinePath);
                    }
                }
            }
        }

        private GraphicsPath CreateRoundedRectangle(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
