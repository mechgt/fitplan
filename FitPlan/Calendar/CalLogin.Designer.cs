namespace FitPlan.Calendar
{
    partial class CalLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalLogin));
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnLogin = new ZoneFiveSoftware.Common.Visuals.Button();
            this.chkPasswordSave = new System.Windows.Forms.CheckBox();
            this.lblPleaseLogin = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.UserName = new System.Windows.Forms.TextBox();
            this.cboCalendar = new System.Windows.Forms.ComboBox();
            this.lblFitPlanCal = new System.Windows.Forms.Label();
            this.btnClose = new ZoneFiveSoftware.Common.Visuals.Button();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.Location = new System.Drawing.Point(12, 36);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(100, 23);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Username";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(15, 61);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(97, 19);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Password";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLogin.BackgroundImage")));
            this.btnLogin.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnLogin.CenterImage = null;
            this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnLogin.HyperlinkStyle = false;
            this.btnLogin.ImageMargin = 2;
            this.btnLogin.LeftImage = null;
            this.btnLogin.Location = new System.Drawing.Point(118, 107);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.PushStyle = true;
            this.btnLogin.RightImage = null;
            this.btnLogin.Size = new System.Drawing.Size(148, 30);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnLogin.TextLeftMargin = 2;
            this.btnLogin.TextRightMargin = 2;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // chkPasswordSave
            // 
            this.chkPasswordSave.AutoSize = true;
            this.chkPasswordSave.Location = new System.Drawing.Point(118, 84);
            this.chkPasswordSave.Name = "chkPasswordSave";
            this.chkPasswordSave.Size = new System.Drawing.Size(156, 17);
            this.chkPasswordSave.TabIndex = 2;
            this.chkPasswordSave.Text = "Save Password in Logbook";
            this.chkPasswordSave.UseVisualStyleBackColor = true;
            this.chkPasswordSave.CheckedChanged += new System.EventHandler(this.chkPasswordSave_CheckedChanged);
            // 
            // lblPleaseLogin
            // 
            this.lblPleaseLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPleaseLogin.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblPleaseLogin.Location = new System.Drawing.Point(115, 8);
            this.lblPleaseLogin.Name = "lblPleaseLogin";
            this.lblPleaseLogin.Size = new System.Drawing.Size(170, 23);
            this.lblPleaseLogin.TabIndex = 5;
            this.lblPleaseLogin.Text = "Please Log In";
            this.lblPleaseLogin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Password
            // 
            this.Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Password.Location = new System.Drawing.Point(118, 58);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(156, 20);
            this.Password.TabIndex = 1;
            // 
            // UserName
            // 
            this.UserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserName.Location = new System.Drawing.Point(118, 34);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(156, 20);
            this.UserName.TabIndex = 0;
            // 
            // cboCalendar
            // 
            this.cboCalendar.FormattingEnabled = true;
            this.cboCalendar.Location = new System.Drawing.Point(118, 143);
            this.cboCalendar.Name = "cboCalendar";
            this.cboCalendar.Size = new System.Drawing.Size(156, 21);
            this.cboCalendar.TabIndex = 4;
            this.cboCalendar.SelectedIndexChanged += new System.EventHandler(this.cboCalendar_SelectedIndexChanged);
            // 
            // lblFitPlanCal
            // 
            this.lblFitPlanCal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFitPlanCal.Location = new System.Drawing.Point(0, 141);
            this.lblFitPlanCal.Name = "lblFitPlanCal";
            this.lblFitPlanCal.Size = new System.Drawing.Size(112, 23);
            this.lblFitPlanCal.TabIndex = 5;
            this.lblFitPlanCal.Text = "Fit Plan Calendar";
            this.lblFitPlanCal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnClose.CenterImage = null;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnClose.HyperlinkStyle = false;
            this.btnClose.ImageMargin = 2;
            this.btnClose.LeftImage = null;
            this.btnClose.Location = new System.Drawing.Point(118, 176);
            this.btnClose.Name = "btnClose";
            this.btnClose.PushStyle = true;
            this.btnClose.RightImage = null;
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnClose.TextLeftMargin = 2;
            this.btnClose.TextRightMargin = 2;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // CalLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 211);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cboCalendar);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.lblFitPlanCal);
            this.Controls.Add(this.lblPleaseLogin);
            this.Controls.Add(this.chkPasswordSave);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CalLogin";
            this.ShowInTaskbar = false;
            this.Text = "Google Calendar Login";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private ZoneFiveSoftware.Common.Visuals.Button btnLogin;
        private System.Windows.Forms.CheckBox chkPasswordSave;
        private System.Windows.Forms.Label lblPleaseLogin;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.ComboBox cboCalendar;
        private System.Windows.Forms.Label lblFitPlanCal;
        private ZoneFiveSoftware.Common.Visuals.Button btnClose;
    }
}