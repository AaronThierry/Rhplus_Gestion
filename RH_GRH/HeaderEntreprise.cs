using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class HeaderEntreprise : UserControl
    {
        private Timer animationTimer;
        private Timer clockTimer;
        private float animationProgress = 0f;
        private string _titre = "Titre du Module";
        private string _sousTitre = "";
        private bool _afficherHorloge = true;
        private bool _afficherUtilisateur = true;

        // Propriétés personnalisables
        public string Titre
        {
            get => _titre;
            set
            {
                _titre = value;
                Invalidate();
            }
        }

        public string SousTitre
        {
            get => _sousTitre;
            set
            {
                _sousTitre = value;
                Invalidate();
            }
        }

        public bool AfficherHorloge
        {
            get => _afficherHorloge;
            set
            {
                _afficherHorloge = value;
                clockTimer.Enabled = value;
                Invalidate();
            }
        }

        public bool AfficherUtilisateur
        {
            get => _afficherUtilisateur;
            set
            {
                _afficherUtilisateur = value;
                Invalidate();
            }
        }

        public HeaderEntreprise()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Configuration du contrôle
            this.DoubleBuffered = true;
            this.Height = 120;
            this.Dock = DockStyle.Top;
            this.BackColor = Color.White;

            // Timer pour l'animation d'entrée
            animationTimer = new Timer { Interval = 16 }; // ~60 FPS
            animationTimer.Tick += AnimationTimer_Tick;

            // Timer pour l'horloge
            clockTimer = new Timer { Interval = 1000 };
            clockTimer.Tick += (s, e) => Invalidate();
            clockTimer.Start();

            // Démarrer l'animation
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            animationProgress += 0.08f;
            if (animationProgress >= 1f)
            {
                animationProgress = 1f;
                animationTimer.Stop();
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Couleurs modernes et sophistiquées
            Color primaryDark = Color.FromArgb(15, 37, 80);      // MidnightBlue profond
            Color primaryMedium = Color.FromArgb(25, 52, 110);   // Bleu intermédiaire
            Color accentGold = Color.FromArgb(255, 193, 7);      // Or élégant
            Color textPrimary = Color.FromArgb(30, 41, 59);      // Gris très foncé
            Color textSecondary = Color.FromArgb(100, 116, 139); // Gris moyen

            // Dégradé de fond subtil
            using (LinearGradientBrush bgBrush = new LinearGradientBrush(
                new Rectangle(0, 0, Width, Height),
                Color.White,
                Color.FromArgb(248, 250, 252),
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(bgBrush, new Rectangle(0, 0, Width, Height));
            }

            // Barre d'accent supérieure avec dégradé
            using (LinearGradientBrush accentBrush = new LinearGradientBrush(
                new Rectangle(0, 0, Width, 5),
                primaryDark,
                primaryMedium,
                LinearGradientMode.Horizontal))
            {
                g.FillRectangle(accentBrush, new Rectangle(0, 0, Width, 5));
            }

            // Animation d'entrée fluide
            float offsetX = (1f - EaseOutCubic(animationProgress)) * 50;
            float alpha = EaseOutCubic(animationProgress);

            // Zone logo et identité (gauche)
            int leftMargin = 30;
            int topMargin = 20;

            // Logo circulaire stylisé (simulé)
            DrawStylizedLogo(g, leftMargin, topMargin + 5, alpha);

            // Nom de l'entreprise et badge
            using (Font fontEntreprise = new Font("Montserrat", 11f, FontStyle.Bold))
            using (SolidBrush brushEntreprise = new SolidBrush(Color.FromArgb((int)(alpha * 255), primaryDark)))
            {
                g.DrawString("RH+ GESTION", fontEntreprise, brushEntreprise,
                    leftMargin + 55 - offsetX, topMargin + 8);
            }

            // Badge "PRO"
            DrawBadge(g, leftMargin + 178, topMargin + 10, "PRO", accentGold, alpha);

            // Ligne de séparation verticale élégante
            using (Pen separatorPen = new Pen(Color.FromArgb((int)(alpha * 40), textSecondary), 1.5f))
            {
                g.DrawLine(separatorPen, leftMargin + 45, topMargin + 35, leftMargin + 250, topMargin + 35);
            }

            // Titre du module - Grande typographie distinctive
            using (Font fontTitre = new Font("Montserrat", 22f, FontStyle.Bold))
            using (SolidBrush brushTitre = new SolidBrush(Color.FromArgb((int)(alpha * 255), textPrimary)))
            {
                g.DrawString(_titre, fontTitre, brushTitre, leftMargin + 45 - offsetX, topMargin + 48);
            }

            // Sous-titre si présent
            if (!string.IsNullOrEmpty(_sousTitre))
            {
                using (Font fontSousTitre = new Font("Segoe UI", 9.5f, FontStyle.Regular))
                using (SolidBrush brushSousTitre = new SolidBrush(Color.FromArgb((int)(alpha * 255), textSecondary)))
                {
                    g.DrawString(_sousTitre, fontSousTitre, brushSousTitre, leftMargin + 48, topMargin + 82);
                }
            }

            // Zone informations contextuelles (droite)
            int rightMargin = Width - 40;

            // Horloge en temps réel
            if (_afficherHorloge)
            {
                DrawClockWidget(g, rightMargin - 200, topMargin + 15, alpha, textSecondary, accentGold);
            }

            // Informations utilisateur
            if (_afficherUtilisateur)
            {
                DrawUserWidget(g, rightMargin - 200, topMargin + 55, alpha, textPrimary, textSecondary);
            }

            // Bordure inférieure subtile avec ombre
            using (Pen borderPen = new Pen(Color.FromArgb(30, textSecondary), 1))
            {
                g.DrawLine(borderPen, 0, Height - 1, Width, Height - 1);
            }
        }

        private void DrawStylizedLogo(Graphics g, int x, int y, float alpha)
        {
            int size = 40;

            // Ombre du cercle
            using (GraphicsPath shadowPath = new GraphicsPath())
            {
                shadowPath.AddEllipse(x + 2, y + 2, size, size);
                using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
                {
                    shadowBrush.CenterColor = Color.FromArgb((int)(alpha * 40), 0, 0, 0);
                    shadowBrush.SurroundColors = new[] { Color.FromArgb(0, 0, 0, 0) };
                    g.FillPath(shadowBrush, shadowPath);
                }
            }

            // Dégradé du cercle principal
            using (GraphicsPath circlePath = new GraphicsPath())
            {
                circlePath.AddEllipse(x, y, size, size);
                using (LinearGradientBrush logoBrush = new LinearGradientBrush(
                    new Rectangle(x, y, size, size),
                    Color.FromArgb((int)(alpha * 255), 15, 37, 80),
                    Color.FromArgb((int)(alpha * 255), 35, 72, 130),
                    45f))
                {
                    g.FillPath(logoBrush, circlePath);
                }
            }

            // Initiales "RH" stylisées
            using (Font logoFont = new Font("Montserrat", 14f, FontStyle.Bold))
            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb((int)(alpha * 255), Color.White)))
            {
                StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString("RH", logoFont, textBrush,
                    new RectangleF(x, y, size, size), sf);
            }

            // Bordure dorée subtile
            using (Pen borderPen = new Pen(Color.FromArgb((int)(alpha * 80), 255, 193, 7), 2f))
            {
                g.DrawEllipse(borderPen, x, y, size, size);
            }
        }

        private void DrawBadge(Graphics g, int x, int y, string text, Color color, float alpha)
        {
            using (Font badgeFont = new Font("Montserrat", 7f, FontStyle.Bold))
            {
                SizeF textSize = g.MeasureString(text, badgeFont);
                int padding = 6;
                int width = (int)textSize.Width + padding * 2;
                int height = (int)textSize.Height + padding;

                // Fond du badge avec coins arrondis
                using (GraphicsPath badgePath = GetRoundedRectangle(x, y, width, height, 3))
                using (SolidBrush badgeBrush = new SolidBrush(Color.FromArgb((int)(alpha * 255), color)))
                {
                    g.FillPath(badgeBrush, badgePath);
                }

                // Texte du badge
                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb((int)(alpha * 255), Color.White)))
                {
                    g.DrawString(text, badgeFont, textBrush, x + padding, y + padding / 2 + 1);
                }
            }
        }

        private void DrawClockWidget(Graphics g, int x, int y, float alpha, Color textColor, Color accentColor)
        {
            DateTime now = DateTime.Now;

            // Icône horloge stylisée
            int iconSize = 18;
            using (Pen clockPen = new Pen(Color.FromArgb((int)(alpha * 200), accentColor), 2f))
            {
                g.DrawEllipse(clockPen, x, y, iconSize, iconSize);
                // Aiguilles
                int centerX = x + iconSize / 2;
                int centerY = y + iconSize / 2;
                g.DrawLine(clockPen, centerX, centerY, centerX, centerY - 5); // Heure
                g.DrawLine(clockPen, centerX, centerY, centerX + 4, centerY); // Minute
            }

            // Heure et date
            using (Font timeFont = new Font("Segoe UI Semibold", 9.5f, FontStyle.Bold))
            using (Font dateFont = new Font("Segoe UI", 8f, FontStyle.Regular))
            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb((int)(alpha * 255), textColor)))
            {
                string timeStr = now.ToString("HH:mm:ss");
                string dateStr = now.ToString("dd/MM/yyyy");
                g.DrawString(timeStr, timeFont, textBrush, x + 25, y - 2);
                g.DrawString(dateStr, dateFont, textBrush, x + 105, y + 1);
            }
        }

        private void DrawUserWidget(Graphics g, int x, int y, float alpha, Color primaryColor, Color secondaryColor)
        {
            // Icône utilisateur
            int iconSize = 22;
            using (GraphicsPath userPath = new GraphicsPath())
            {
                // Cercle de tête
                userPath.AddEllipse(x + 5, y, 8, 8);
                // Corps
                userPath.AddArc(x, y + 7, iconSize, 12, 200, 140);

                using (SolidBrush userBrush = new SolidBrush(Color.FromArgb((int)(alpha * 220), primaryColor)))
                {
                    g.FillPath(userBrush, userPath);
                }
            }

            // Nom de l'utilisateur connecté
            string username = "Utilisateur";
            try
            {
                if (Auth.SessionManager.Instance.IsAuthenticated)
                {
                    username = Auth.SessionManager.Instance.CurrentUser?.NomUtilisateur ?? "Utilisateur";
                }
            }
            catch { }

            using (Font userFont = new Font("Segoe UI Semibold", 9f, FontStyle.Bold))
            using (Font roleFont = new Font("Segoe UI", 7.5f, FontStyle.Regular))
            using (SolidBrush primaryBrush = new SolidBrush(Color.FromArgb((int)(alpha * 255), primaryColor)))
            using (SolidBrush secondaryBrush = new SolidBrush(Color.FromArgb((int)(alpha * 255), secondaryColor)))
            {
                g.DrawString(username, userFont, primaryBrush, x + 28, y);
                g.DrawString("Connecté", roleFont, secondaryBrush, x + 28, y + 14);
            }
        }

        private GraphicsPath GetRoundedRectangle(int x, int y, int width, int height, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(x, y, radius * 2, radius * 2, 180, 90);
            path.AddArc(x + width - radius * 2, y, radius * 2, radius * 2, 270, 90);
            path.AddArc(x + width - radius * 2, y + height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(x, y + height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            return path;
        }

        private float EaseOutCubic(float t)
        {
            return 1f - (float)Math.Pow(1 - t, 3);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                animationTimer?.Dispose();
                clockTimer?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
