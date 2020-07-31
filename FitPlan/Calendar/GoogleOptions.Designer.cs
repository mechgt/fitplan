namespace FitPlan.Calendar
{
    partial class GoogleOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoogleOptions));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chkIconEntry = new System.Windows.Forms.CheckBox();
            this.chkMainEntry = new System.Windows.Forms.CheckBox();
            this.btnClose = new ZoneFiveSoftware.Common.Visuals.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FitPlan.Properties.Resources.GoogleOptionsMain;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(202, 162);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // chkIconEntry
            // 
            this.chkIconEntry.AutoSize = true;
            this.chkIconEntry.Location = new System.Drawing.Point(221, 27);
            this.chkIconEntry.Name = "chkIconEntry";
            this.chkIconEntry.Size = new System.Drawing.Size(84, 17);
            this.chkIconEntry.TabIndex = 1;
            this.chkIconEntry.Text = "Display Icon";
            this.chkIconEntry.UseVisualStyleBackColor = true;
            this.chkIconEntry.CheckedChanged += new System.EventHandler(this.chkIconEntry_CheckedChanged);
            // 
            // chkMainEntry
            // 
            this.chkMainEntry.AutoSize = true;
            this.chkMainEntry.Location = new System.Drawing.Point(220, 103);
            this.chkMainEntry.Name = "chkMainEntry";
            this.chkMainEntry.Size = new System.Drawing.Size(111, 17);
            this.chkMainEntry.TabIndex = 1;
            this.chkMainEntry.Text = "Display Text Entry";
            this.chkMainEntry.UseVisualStyleBackColor = true;
            this.chkMainEntry.CheckedChanged += new System.EventHandler(this.chkMainEntry_CheckedChanged);
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
            this.btnClose.Location = new System.Drawing.Point(244, 151);
            this.btnClose.Name = "btnClose";
            this.btnClose.PushStyle = true;
            this.btnClose.RightImage = null;
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnClose.TextLeftMargin = 2;
            this.btnClose.TextRightMargin = 2;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // GoogleOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 185);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.chkMainEntry);
            this.Controls.Add(this.chkIconEntry);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GoogleOptions";
            this.Text = "Google Calendar Display Options";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox chkIconEntry;
        private System.Windows.Forms.CheckBox chkMainEntry;
        private ZoneFiveSoftware.Common.Visuals.Button btnClose;
    }
}