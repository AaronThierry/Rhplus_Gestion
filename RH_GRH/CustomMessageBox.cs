using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RH_GRH
{
    public partial class CustomMessageBox : Form
    {
        public enum MessageType
        {
            Success,
            Error,
            Info,
            Warning,
            Question
        }

        public enum MessageButtons
        {
            OK,
            OKCancel,
            YesNo,
            YesNoCancel
        }

        private DialogResult dialogResult = DialogResult.None;
        private MessageType messageType;

        private CustomMessageBox()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.BackColor = Color.White;
            this.Size = new Size(450, 320);

            // Coins arrondis
            ApplyRoundedCorners(20);

            // Ombre portée
            this.Paint += CustomMessageBox_Paint;
        }

        private void ApplyRoundedCorners(int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path.AddArc(new Rectangle(this.Width - radius, 0, radius, radius), 270, 90);
            path.AddArc(new Rectangle(this.Width - radius, this.Height - radius, radius, radius), 0, 90);
            path.AddArc(new Rectangle(0, this.Height - radius, radius, radius), 90, 90);
            path.CloseFigure();
            this.Region = new Region(path);
        }

        private void CustomMessageBox_Paint(object sender, PaintEventArgs e)
        {
            // Bordure subtile
            using (Pen pen = new Pen(Color.FromArgb(230, 230, 230), 2))
            {
                GraphicsPath borderPath = new GraphicsPath();
                int radius = 20;
                borderPath.AddArc(new Rectangle(1, 1, radius, radius), 180, 90);
                borderPath.AddArc(new Rectangle(this.Width - radius - 2, 1, radius, radius), 270, 90);
                borderPath.AddArc(new Rectangle(this.Width - radius - 2, this.Height - radius - 2, radius, radius), 0, 90);
                borderPath.AddArc(new Rectangle(1, this.Height - radius - 2, radius, radius), 90, 90);
                borderPath.CloseFigure();
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, borderPath);
            }
        }

        private void panelIcon_Paint(object sender, PaintEventArgs e)
        {
            // Rendre le panel d'icône circulaire
            Panel panel = (Panel)sender;
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(0, 0, panel.Width - 1, panel.Height - 1);
                panel.Region = new Region(path);
            }
        }

        public static DialogResult Show(string message, string title = "", MessageType type = MessageType.Info, MessageButtons buttons = MessageButtons.OK)
        {
            using (CustomMessageBox msgBox = new CustomMessageBox())
            {
                msgBox.messageType = type;
                msgBox.SetupMessageBox(message, title, type, buttons);
                msgBox.ShowDialog();
                return msgBox.dialogResult;
            }
        }

        private void SetupMessageBox(string message, string title, MessageType type, MessageButtons buttons)
        {
            labelTitle.Text = string.IsNullOrWhiteSpace(title) ? GetDefaultTitle(type) : title;
            labelMessage.Text = message;

            // Configurer les couleurs et icône selon le type
            Color iconColor;
            string iconText;

            switch (type)
            {
                case MessageType.Success:
                    iconColor = Color.FromArgb(40, 167, 69);
                    iconText = "✓";
                    break;
                case MessageType.Error:
                    iconColor = Color.FromArgb(220, 53, 69);
                    iconText = "✗";
                    break;
                case MessageType.Warning:
                    iconColor = Color.FromArgb(255, 193, 7);
                    iconText = "⚠";
                    break;
                case MessageType.Question:
                    iconColor = Color.FromArgb(111, 66, 193);
                    iconText = "?";
                    break;
                default: // Info
                    iconColor = Color.FromArgb(23, 162, 184);
                    iconText = "ℹ";
                    break;
            }

            // Icône circulaire colorée
            panelIcon.BackColor = Color.FromArgb(30, iconColor);
            labelIcon.Text = iconText;
            labelIcon.ForeColor = iconColor;

            // Titre et message
            labelTitle.ForeColor = Color.FromArgb(52, 58, 64);
            labelMessage.ForeColor = Color.FromArgb(108, 117, 125);

            // Configurer les boutons
            SetupButtons(buttons, iconColor);

            // Animation d'entrée (fade in)
            this.Opacity = 0;
            Timer fadeInTimer = new Timer();
            fadeInTimer.Interval = 10;
            fadeInTimer.Tick += (s, e) =>
            {
                if (this.Opacity < 1)
                {
                    this.Opacity += 0.08;
                }
                else
                {
                    fadeInTimer.Stop();
                    fadeInTimer.Dispose();
                }
            };
            fadeInTimer.Start();
        }

        private string GetDefaultTitle(MessageType type)
        {
            switch (type)
            {
                case MessageType.Success:
                    return "Succès";
                case MessageType.Error:
                    return "Erreur";
                case MessageType.Warning:
                    return "Attention";
                case MessageType.Question:
                    return "Question";
                default:
                    return "Information";
            }
        }

        private void SetupButtons(MessageButtons buttons, Color accentColor)
        {
            // Cacher tous les boutons d'abord
            buttonOK.Visible = false;
            buttonCancel.Visible = false;
            buttonYes.Visible = false;
            buttonNo.Visible = false;

            // Appliquer la couleur d'accent
            buttonOK.FillColor = accentColor;
            buttonYes.FillColor = accentColor;

            switch (buttons)
            {
                case MessageButtons.OK:
                    buttonOK.Visible = true;
                    buttonOK.Location = new Point((panelButtons.Width - buttonOK.Width) / 2, 15);
                    break;

                case MessageButtons.OKCancel:
                    buttonOK.Visible = true;
                    buttonCancel.Visible = true;
                    int totalWidth = buttonOK.Width + buttonCancel.Width + 15;
                    buttonCancel.Location = new Point((panelButtons.Width - totalWidth) / 2, 15);
                    buttonOK.Location = new Point(buttonCancel.Right + 15, 15);
                    break;

                case MessageButtons.YesNo:
                    buttonYes.Visible = true;
                    buttonNo.Visible = true;
                    totalWidth = buttonYes.Width + buttonNo.Width + 15;
                    buttonNo.Location = new Point((panelButtons.Width - totalWidth) / 2, 15);
                    buttonYes.Location = new Point(buttonNo.Right + 15, 15);
                    break;

                case MessageButtons.YesNoCancel:
                    buttonYes.Visible = true;
                    buttonNo.Visible = true;
                    buttonCancel.Visible = true;
                    totalWidth = buttonYes.Width + buttonNo.Width + buttonCancel.Width + 30;
                    buttonCancel.Location = new Point((panelButtons.Width - totalWidth) / 2, 15);
                    buttonNo.Location = new Point(buttonCancel.Right + 15, 15);
                    buttonYes.Location = new Point(buttonNo.Right + 15, 15);
                    break;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.OK;
            FadeOutAndClose();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.Cancel;
            FadeOutAndClose();
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.Yes;
            FadeOutAndClose();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.No;
            FadeOutAndClose();
        }

        private void FadeOutAndClose()
        {
            Timer fadeOutTimer = new Timer();
            fadeOutTimer.Interval = 10;
            fadeOutTimer.Tick += (s, e) =>
            {
                if (this.Opacity > 0)
                {
                    this.Opacity -= 0.1;
                }
                else
                {
                    fadeOutTimer.Stop();
                    fadeOutTimer.Dispose();
                    this.Close();
                }
            };
            fadeOutTimer.Start();
        }

        // Permettre de déplacer le formulaire en cliquant n'importe où
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
