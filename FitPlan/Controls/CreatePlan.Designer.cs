namespace FitPlan.Controls
{
    partial class CreatePlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreatePlan));
            this.radDefault = new System.Windows.Forms.RadioButton();
            this.radLogbook = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPlanEnd = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtPlanStart = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblPlanName = new System.Windows.Forms.Label();
            this.lblPlanStart = new System.Windows.Forms.Label();
            this.lblPlanEnd = new System.Windows.Forms.Label();
            this.lblPlanWeeks = new System.Windows.Forms.Label();
            this.txtPlanName = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblLogStart = new System.Windows.Forms.Label();
            this.lblLogEnd = new System.Windows.Forms.Label();
            this.txtLogStart = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtLogEnd = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.btnCancel = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnOk = new ZoneFiveSoftware.Common.Visuals.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPlanWeeks = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlanWeeks)).BeginInit();
            this.SuspendLayout();
            // 
            // radDefault
            // 
            this.radDefault.AutoSize = true;
            this.radDefault.Checked = true;
            this.radDefault.Location = new System.Drawing.Point(12, 30);
            this.radDefault.Name = "radDefault";
            this.radDefault.Size = new System.Drawing.Size(106, 17);
            this.radDefault.TabIndex = 0;
            this.radDefault.TabStop = true;
            this.radDefault.Tag = "Default";
            this.radDefault.Text = "Default Template";
            this.radDefault.UseVisualStyleBackColor = true;
            this.radDefault.CheckedChanged += new System.EventHandler(this.radTemplate_CheckedChanged);
            // 
            // radLogbook
            // 
            this.radLogbook.AutoSize = true;
            this.radLogbook.Location = new System.Drawing.Point(12, 53);
            this.radLogbook.Name = "radLogbook";
            this.radLogbook.Size = new System.Drawing.Size(112, 17);
            this.radLogbook.TabIndex = 0;
            this.radLogbook.Tag = "Logbook";
            this.radLogbook.Text = "Logbook Activities";
            this.radLogbook.UseVisualStyleBackColor = true;
            this.radLogbook.CheckedChanged += new System.EventHandler(this.radTemplate_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Create New Plan From...";
            // 
            // txtPlanEnd
            // 
            this.txtPlanEnd.AcceptsReturn = false;
            this.txtPlanEnd.AcceptsTab = false;
            this.txtPlanEnd.BackColor = System.Drawing.Color.White;
            this.txtPlanEnd.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtPlanEnd.ButtonImage = ((System.Drawing.Image)(resources.GetObject("txtPlanEnd.ButtonImage")));
            this.txtPlanEnd.Location = new System.Drawing.Point(85, 68);
            this.txtPlanEnd.MaxLength = 32767;
            this.txtPlanEnd.Multiline = false;
            this.txtPlanEnd.Name = "txtPlanEnd";
            this.txtPlanEnd.ReadOnly = false;
            this.txtPlanEnd.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtPlanEnd.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtPlanEnd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPlanEnd.Size = new System.Drawing.Size(107, 19);
            this.txtPlanEnd.TabIndex = 4;
            this.txtPlanEnd.Tag = "End";
            this.txtPlanEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPlanEnd.ButtonClick += new System.EventHandler(this.btnCalendar_Click);
            this.txtPlanEnd.Leave += new System.EventHandler(this.txtPlanEnd_Leave);
            // 
            // txtPlanStart
            // 
            this.txtPlanStart.AcceptsReturn = false;
            this.txtPlanStart.AcceptsTab = false;
            this.txtPlanStart.BackColor = System.Drawing.Color.White;
            this.txtPlanStart.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtPlanStart.ButtonImage = ((System.Drawing.Image)(resources.GetObject("txtPlanStart.ButtonImage")));
            this.txtPlanStart.Location = new System.Drawing.Point(85, 43);
            this.txtPlanStart.MaxLength = 32767;
            this.txtPlanStart.Multiline = false;
            this.txtPlanStart.Name = "txtPlanStart";
            this.txtPlanStart.ReadOnly = false;
            this.txtPlanStart.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtPlanStart.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtPlanStart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPlanStart.Size = new System.Drawing.Size(107, 19);
            this.txtPlanStart.TabIndex = 3;
            this.txtPlanStart.Tag = "Start";
            this.txtPlanStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPlanStart.ButtonClick += new System.EventHandler(this.btnCalendar_Click);
            this.txtPlanStart.Leave += new System.EventHandler(this.txtPlanStart_Leave);
            // 
            // lblPlanName
            // 
            this.lblPlanName.AutoSize = true;
            this.lblPlanName.Location = new System.Drawing.Point(44, 24);
            this.lblPlanName.Name = "lblPlanName";
            this.lblPlanName.Size = new System.Drawing.Size(35, 13);
            this.lblPlanName.TabIndex = 1;
            this.lblPlanName.Text = "Name";
            this.lblPlanName.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPlanStart
            // 
            this.lblPlanStart.AutoSize = true;
            this.lblPlanStart.Location = new System.Drawing.Point(50, 49);
            this.lblPlanStart.Name = "lblPlanStart";
            this.lblPlanStart.Size = new System.Drawing.Size(29, 13);
            this.lblPlanStart.TabIndex = 1;
            this.lblPlanStart.Text = "Start";
            this.lblPlanStart.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPlanEnd
            // 
            this.lblPlanEnd.AutoSize = true;
            this.lblPlanEnd.Location = new System.Drawing.Point(53, 74);
            this.lblPlanEnd.Name = "lblPlanEnd";
            this.lblPlanEnd.Size = new System.Drawing.Size(26, 13);
            this.lblPlanEnd.TabIndex = 1;
            this.lblPlanEnd.Text = "End";
            this.lblPlanEnd.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPlanWeeks
            // 
            this.lblPlanWeeks.AutoSize = true;
            this.lblPlanWeeks.Location = new System.Drawing.Point(38, 99);
            this.lblPlanWeeks.Name = "lblPlanWeeks";
            this.lblPlanWeeks.Size = new System.Drawing.Size(41, 13);
            this.lblPlanWeeks.TabIndex = 1;
            this.lblPlanWeeks.Text = "Weeks";
            this.lblPlanWeeks.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPlanName
            // 
            this.txtPlanName.AcceptsReturn = false;
            this.txtPlanName.AcceptsTab = false;
            this.txtPlanName.BackColor = System.Drawing.Color.White;
            this.txtPlanName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtPlanName.ButtonImage = null;
            this.txtPlanName.Location = new System.Drawing.Point(85, 18);
            this.txtPlanName.MaxLength = 32767;
            this.txtPlanName.Multiline = false;
            this.txtPlanName.Name = "txtPlanName";
            this.txtPlanName.ReadOnly = false;
            this.txtPlanName.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtPlanName.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtPlanName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPlanName.Size = new System.Drawing.Size(107, 19);
            this.txtPlanName.TabIndex = 3;
            this.txtPlanName.Tag = "Name";
            this.txtPlanName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblLogStart
            // 
            this.lblLogStart.AutoSize = true;
            this.lblLogStart.Location = new System.Drawing.Point(31, 86);
            this.lblLogStart.Name = "lblLogStart";
            this.lblLogStart.Size = new System.Drawing.Size(29, 13);
            this.lblLogStart.TabIndex = 1;
            this.lblLogStart.Text = "Start";
            this.lblLogStart.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblLogEnd
            // 
            this.lblLogEnd.AutoSize = true;
            this.lblLogEnd.Location = new System.Drawing.Point(34, 111);
            this.lblLogEnd.Name = "lblLogEnd";
            this.lblLogEnd.Size = new System.Drawing.Size(26, 13);
            this.lblLogEnd.TabIndex = 1;
            this.lblLogEnd.Text = "End";
            this.lblLogEnd.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtLogStart
            // 
            this.txtLogStart.AcceptsReturn = false;
            this.txtLogStart.AcceptsTab = false;
            this.txtLogStart.BackColor = System.Drawing.Color.White;
            this.txtLogStart.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtLogStart.ButtonImage = ((System.Drawing.Image)(resources.GetObject("txtLogStart.ButtonImage")));
            this.txtLogStart.Enabled = false;
            this.txtLogStart.Location = new System.Drawing.Point(66, 80);
            this.txtLogStart.MaxLength = 32767;
            this.txtLogStart.Multiline = false;
            this.txtLogStart.Name = "txtLogStart";
            this.txtLogStart.ReadOnly = false;
            this.txtLogStart.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtLogStart.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtLogStart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtLogStart.Size = new System.Drawing.Size(107, 19);
            this.txtLogStart.TabIndex = 3;
            this.txtLogStart.Tag = "Start";
            this.txtLogStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLogStart.ButtonClick += new System.EventHandler(this.btnCalendar_Click);
            this.txtLogStart.Leave += new System.EventHandler(this.txtLogStart_Leave);
            // 
            // txtLogEnd
            // 
            this.txtLogEnd.AcceptsReturn = false;
            this.txtLogEnd.AcceptsTab = false;
            this.txtLogEnd.BackColor = System.Drawing.Color.White;
            this.txtLogEnd.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtLogEnd.ButtonImage = ((System.Drawing.Image)(resources.GetObject("txtLogEnd.ButtonImage")));
            this.txtLogEnd.Enabled = false;
            this.txtLogEnd.Location = new System.Drawing.Point(66, 105);
            this.txtLogEnd.MaxLength = 32767;
            this.txtLogEnd.Multiline = false;
            this.txtLogEnd.Name = "txtLogEnd";
            this.txtLogEnd.ReadOnly = false;
            this.txtLogEnd.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtLogEnd.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtLogEnd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtLogEnd.Size = new System.Drawing.Size(107, 19);
            this.txtLogEnd.TabIndex = 4;
            this.txtLogEnd.Tag = "End";
            this.txtLogEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLogEnd.ButtonClick += new System.EventHandler(this.btnCalendar_Click);
            this.txtLogEnd.Leave += new System.EventHandler(this.txtLogEnd_Leave);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnCancel.CenterImage = null;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.HyperlinkStyle = false;
            this.btnCancel.ImageMargin = 2;
            this.btnCancel.LeftImage = null;
            this.btnCancel.Location = new System.Drawing.Point(204, 140);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PushStyle = true;
            this.btnCancel.RightImage = null;
            this.btnCancel.Size = new System.Drawing.Size(107, 24);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Tag = "";
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnCancel.TextLeftMargin = 2;
            this.btnCancel.TextRightMargin = 2;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.BackColor = System.Drawing.Color.Transparent;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnOk.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnOk.CenterImage = null;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.HyperlinkStyle = false;
            this.btnOk.ImageMargin = 2;
            this.btnOk.LeftImage = null;
            this.btnOk.Location = new System.Drawing.Point(88, 140);
            this.btnOk.Margin = new System.Windows.Forms.Padding(0);
            this.btnOk.Name = "btnOk";
            this.btnOk.PushStyle = true;
            this.btnOk.RightImage = null;
            this.btnOk.Size = new System.Drawing.Size(107, 24);
            this.btnOk.TabIndex = 34;
            this.btnOk.Tag = "";
            this.btnOk.Text = "OK";
            this.btnOk.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnOk.TextLeftMargin = 2;
            this.btnOk.TextRightMargin = 2;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPlanWeeks);
            this.groupBox1.Controls.Add(this.txtPlanEnd);
            this.groupBox1.Controls.Add(this.txtPlanName);
            this.groupBox1.Controls.Add(this.txtPlanStart);
            this.groupBox1.Controls.Add(this.lblPlanWeeks);
            this.groupBox1.Controls.Add(this.lblPlanEnd);
            this.groupBox1.Controls.Add(this.lblPlanStart);
            this.groupBox1.Controls.Add(this.lblPlanName);
            this.groupBox1.Location = new System.Drawing.Point(179, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 125);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New Plan";
            // 
            // txtPlanWeeks
            // 
            this.txtPlanWeeks.Location = new System.Drawing.Point(85, 92);
            this.txtPlanWeeks.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtPlanWeeks.Name = "txtPlanWeeks";
            this.txtPlanWeeks.Size = new System.Drawing.Size(107, 20);
            this.txtPlanWeeks.TabIndex = 5;
            this.txtPlanWeeks.Tag = "Weeks";
            this.txtPlanWeeks.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPlanWeeks.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.txtPlanWeeks.ValueChanged += new System.EventHandler(this.txtPlanWeeks_ValueChanged);
            // 
            // CreatePlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 173);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtLogEnd);
            this.Controls.Add(this.txtLogStart);
            this.Controls.Add(this.lblLogEnd);
            this.Controls.Add(this.lblLogStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radLogbook);
            this.Controls.Add(this.radDefault);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreatePlan";
            this.Text = "CreatePlan";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlanWeeks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radDefault;
        private System.Windows.Forms.RadioButton radLogbook;
        private System.Windows.Forms.Label label1;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtPlanEnd;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtPlanStart;
        private System.Windows.Forms.Label lblPlanName;
        private System.Windows.Forms.Label lblPlanStart;
        private System.Windows.Forms.Label lblPlanEnd;
        private System.Windows.Forms.Label lblPlanWeeks;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtPlanName;
        private System.Windows.Forms.Label lblLogStart;
        private System.Windows.Forms.Label lblLogEnd;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtLogStart;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtLogEnd;
        private ZoneFiveSoftware.Common.Visuals.Button btnCancel;
        private ZoneFiveSoftware.Common.Visuals.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown txtPlanWeeks;
    }
}