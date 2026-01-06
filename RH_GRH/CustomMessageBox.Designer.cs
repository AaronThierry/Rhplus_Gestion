namespace RH_GRH
{
    partial class CustomMessageBox
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelIcon = new System.Windows.Forms.Panel();
            this.labelIcon = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelMessage = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOK = new Guna.UI2.WinForms.Guna2Button();
            this.buttonCancel = new Guna.UI2.WinForms.Guna2Button();
            this.buttonYes = new Guna.UI2.WinForms.Guna2Button();
            this.buttonNo = new Guna.UI2.WinForms.Guna2Button();
            this.panelIcon.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // panelIcon
            //
            this.panelIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.panelIcon.Controls.Add(this.labelIcon);
            this.panelIcon.Location = new System.Drawing.Point(175, 30);
            this.panelIcon.Name = "panelIcon";
            this.panelIcon.Size = new System.Drawing.Size(100, 100);
            this.panelIcon.TabIndex = 0;
            this.panelIcon.Paint += new System.Windows.Forms.PaintEventHandler(this.panelIcon_Paint);
            this.panelIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.panelIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            this.panelIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
            //
            // labelIcon
            //
            this.labelIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelIcon.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold);
            this.labelIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.labelIcon.Location = new System.Drawing.Point(0, 0);
            this.labelIcon.Name = "labelIcon";
            this.labelIcon.Size = new System.Drawing.Size(100, 100);
            this.labelIcon.TabIndex = 0;
            this.labelIcon.Text = "ℹ";
            this.labelIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.labelIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            this.labelIcon.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
            //
            // labelTitle
            //
            this.labelTitle.Font = new System.Drawing.Font("Montserrat", 14F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(58)))), ((int)(((byte)(64)))));
            this.labelTitle.Location = new System.Drawing.Point(30, 145);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(390, 30);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Information";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.labelTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            this.labelTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
            //
            // labelMessage
            //
            this.labelMessage.Font = new System.Drawing.Font("Montserrat", 9.5F);
            this.labelMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.labelMessage.Location = new System.Drawing.Point(40, 185);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(370, 60);
            this.labelMessage.TabIndex = 2;
            this.labelMessage.Text = "Message text goes here";
            this.labelMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelMessage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.labelMessage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            this.labelMessage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
            //
            // panelButtons
            //
            this.panelButtons.BackColor = System.Drawing.Color.White;
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonCancel);
            this.panelButtons.Controls.Add(this.buttonYes);
            this.panelButtons.Controls.Add(this.buttonNo);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 255);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(450, 65);
            this.panelButtons.TabIndex = 3;
            //
            // buttonOK
            //
            this.buttonOK.Animated = true;
            this.buttonOK.BorderRadius = 8;
            this.buttonOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonOK.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonOK.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonOK.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonOK.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonOK.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.buttonOK.Font = new System.Drawing.Font("Montserrat", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonOK.ForeColor = System.Drawing.Color.White;
            this.buttonOK.Location = new System.Drawing.Point(165, 12);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.ShadowDecoration.BorderRadius = 8;
            this.buttonOK.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.buttonOK.ShadowDecoration.Depth = 8;
            this.buttonOK.ShadowDecoration.Enabled = true;
            this.buttonOK.Size = new System.Drawing.Size(120, 42);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.Visible = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            //
            // buttonCancel
            //
            this.buttonCancel.Animated = true;
            this.buttonCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(212)))), ((int)(((byte)(218)))));
            this.buttonCancel.BorderRadius = 8;
            this.buttonCancel.BorderThickness = 1;
            this.buttonCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonCancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonCancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonCancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonCancel.FillColor = System.Drawing.Color.White;
            this.buttonCancel.Font = new System.Drawing.Font("Montserrat", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.buttonCancel.Location = new System.Drawing.Point(165, 12);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(120, 42);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Annuler";
            this.buttonCancel.Visible = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            //
            // buttonYes
            //
            this.buttonYes.Animated = true;
            this.buttonYes.BorderRadius = 8;
            this.buttonYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonYes.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonYes.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonYes.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonYes.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonYes.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.buttonYes.Font = new System.Drawing.Font("Montserrat", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonYes.ForeColor = System.Drawing.Color.White;
            this.buttonYes.Location = new System.Drawing.Point(165, 12);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.ShadowDecoration.BorderRadius = 8;
            this.buttonYes.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.buttonYes.ShadowDecoration.Depth = 8;
            this.buttonYes.ShadowDecoration.Enabled = true;
            this.buttonYes.Size = new System.Drawing.Size(120, 42);
            this.buttonYes.TabIndex = 2;
            this.buttonYes.Text = "Oui";
            this.buttonYes.Visible = false;
            this.buttonYes.Click += new System.EventHandler(this.buttonYes_Click);
            //
            // buttonNo
            //
            this.buttonNo.Animated = true;
            this.buttonNo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.buttonNo.BorderRadius = 8;
            this.buttonNo.BorderThickness = 1;
            this.buttonNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNo.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonNo.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonNo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonNo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonNo.FillColor = System.Drawing.Color.White;
            this.buttonNo.Font = new System.Drawing.Font("Montserrat", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.buttonNo.Location = new System.Drawing.Point(165, 12);
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.Size = new System.Drawing.Size(120, 42);
            this.buttonNo.TabIndex = 3;
            this.buttonNo.Text = "Non";
            this.buttonNo.Visible = false;
            this.buttonNo.Click += new System.EventHandler(this.buttonNo_Click);
            //
            // CustomMessageBox
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(450, 320);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.panelIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomMessageBox";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
            this.panelIcon.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelIcon;
        private System.Windows.Forms.Label labelIcon;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Panel panelButtons;
        private Guna.UI2.WinForms.Guna2Button buttonOK;
        private Guna.UI2.WinForms.Guna2Button buttonCancel;
        private Guna.UI2.WinForms.Guna2Button buttonYes;
        private Guna.UI2.WinForms.Guna2Button buttonNo;
    }
}
