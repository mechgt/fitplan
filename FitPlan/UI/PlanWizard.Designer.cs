namespace FitPlan.UI
{
    partial class PlanWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanWizard));
            this.wizardControl = new WizardBase.WizardControl();
            this.startStep1 = new WizardBase.StartStep();
            this.stepPrimaryInfo = new WizardBase.IntermediateStep();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rad5k = new System.Windows.Forms.RadioButton();
            this.radOther = new System.Windows.Forms.RadioButton();
            this.txtOther = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.radMarathon = new System.Windows.Forms.RadioButton();
            this.rad10k = new System.Windows.Forms.RadioButton();
            this.radHalfMarathon = new System.Windows.Forms.RadioButton();
            this.lblMarathon = new System.Windows.Forms.Label();
            this.lblHalfMarathon = new System.Windows.Forms.Label();
            this.lbl10k = new System.Windows.Forms.Label();
            this.lbl5k = new System.Windows.Forms.Label();
            this.lblEndText = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblStartText = new System.Windows.Forms.Label();
            this.calEnd = new ZoneFiveSoftware.Common.Visuals.Calendar();
            this.calStart = new ZoneFiveSoftware.Common.Visuals.Calendar();
            this.stepDetailInfo = new WizardBase.IntermediateStep();
            this.txtAvgHours = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtName = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtAvgDistance = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblMaxHours = new System.Windows.Forms.Label();
            this.lblMaxDistance = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblAvgHoursText = new System.Windows.Forms.Label();
            this.lblAvgDistanceText = new System.Windows.Forms.Label();
            this.stepReview = new WizardBase.IntermediateStep();
            this.finishStep1 = new WizardBase.FinishStep();
            this.stepPrimaryInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.stepDetailInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl
            // 
            this.wizardControl.BackButtonEnabled = true;
            this.wizardControl.BackButtonVisible = true;
            this.wizardControl.CancelButtonEnabled = true;
            this.wizardControl.CancelButtonVisible = true;
            this.wizardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl.EulaButtonEnabled = true;
            this.wizardControl.EulaButtonText = "eula";
            this.wizardControl.EulaButtonVisible = false;
            this.wizardControl.HelpButtonEnabled = true;
            this.wizardControl.HelpButtonVisible = false;
            this.wizardControl.Location = new System.Drawing.Point(0, 0);
            this.wizardControl.Margin = new System.Windows.Forms.Padding(0);
            this.wizardControl.Name = "wizardControl";
            this.wizardControl.NextButtonEnabled = true;
            this.wizardControl.NextButtonVisible = true;
            this.wizardControl.Size = new System.Drawing.Size(653, 421);
            this.wizardControl.WizardSteps.AddRange(new WizardBase.WizardStep[] {
            this.startStep1,
            this.stepPrimaryInfo,
            this.stepDetailInfo,
            this.stepReview,
            this.finishStep1});
            this.wizardControl.FinishButtonClick += new System.EventHandler(this.wizardControl_FinishButtonClick);
            this.wizardControl.CancelButtonClick += new System.EventHandler(this.wizardControl_CancelButtonClick);
            // 
            // startStep1
            // 
            this.startStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("startStep1.BindingImage")));
            this.startStep1.Icon = ((System.Drawing.Image)(resources.GetObject("startStep1.Icon")));
            this.startStep1.Name = "startStep1";
            this.startStep1.Subtitle = "Use this wizard to create a new training plan.  Mechgt, please update this text t" +
                "o be really descriptive of what we\'re doing.";
            this.startStep1.Title = "Create a new Training Plan";
            // 
            // stepPrimaryInfo
            // 
            this.stepPrimaryInfo.BindingImage = ((System.Drawing.Image)(resources.GetObject("stepPrimaryInfo.BindingImage")));
            this.stepPrimaryInfo.Controls.Add(this.groupBox1);
            this.stepPrimaryInfo.Controls.Add(this.lblEndText);
            this.stepPrimaryInfo.Controls.Add(this.lblEndDate);
            this.stepPrimaryInfo.Controls.Add(this.lblStartDate);
            this.stepPrimaryInfo.Controls.Add(this.lblStartText);
            this.stepPrimaryInfo.Controls.Add(this.calEnd);
            this.stepPrimaryInfo.Controls.Add(this.calStart);
            this.stepPrimaryInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.stepPrimaryInfo.Name = "stepPrimaryInfo";
            this.stepPrimaryInfo.Title = "New Wizard Step";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rad5k);
            this.groupBox1.Controls.Add(this.radOther);
            this.groupBox1.Controls.Add(this.txtOther);
            this.groupBox1.Controls.Add(this.radMarathon);
            this.groupBox1.Controls.Add(this.rad10k);
            this.groupBox1.Controls.Add(this.radHalfMarathon);
            this.groupBox1.Controls.Add(this.lblMarathon);
            this.groupBox1.Controls.Add(this.lblHalfMarathon);
            this.groupBox1.Controls.Add(this.lbl10k);
            this.groupBox1.Controls.Add(this.lbl5k);
            this.groupBox1.Location = new System.Drawing.Point(394, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 156);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Race Distance";
            // 
            // rad5k
            // 
            this.rad5k.AutoSize = true;
            this.rad5k.Location = new System.Drawing.Point(95, 36);
            this.rad5k.Name = "rad5k";
            this.rad5k.Size = new System.Drawing.Size(37, 17);
            this.rad5k.TabIndex = 3;
            this.rad5k.TabStop = true;
            this.rad5k.Text = "5k";
            this.rad5k.UseVisualStyleBackColor = true;
            // 
            // radOther
            // 
            this.radOther.AutoSize = true;
            this.radOther.Location = new System.Drawing.Point(95, 128);
            this.radOther.Name = "radOther";
            this.radOther.Size = new System.Drawing.Size(83, 17);
            this.radOther.TabIndex = 3;
            this.radOther.TabStop = true;
            this.radOther.Text = "Other (miles)";
            this.radOther.UseVisualStyleBackColor = true;
            // 
            // txtOther
            // 
            this.txtOther.AcceptsReturn = false;
            this.txtOther.AcceptsTab = false;
            this.txtOther.BackColor = System.Drawing.Color.White;
            this.txtOther.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtOther.ButtonImage = null;
            this.txtOther.Location = new System.Drawing.Point(22, 128);
            this.txtOther.MaxLength = 32767;
            this.txtOther.Multiline = false;
            this.txtOther.Name = "txtOther";
            this.txtOther.ReadOnly = false;
            this.txtOther.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtOther.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtOther.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtOther.Size = new System.Drawing.Size(67, 19);
            this.txtOther.TabIndex = 2;
            this.txtOther.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // radMarathon
            // 
            this.radMarathon.AutoSize = true;
            this.radMarathon.Location = new System.Drawing.Point(95, 105);
            this.radMarathon.Name = "radMarathon";
            this.radMarathon.Size = new System.Drawing.Size(70, 17);
            this.radMarathon.TabIndex = 3;
            this.radMarathon.TabStop = true;
            this.radMarathon.Text = "Marathon";
            this.radMarathon.UseVisualStyleBackColor = true;
            // 
            // rad10k
            // 
            this.rad10k.AutoSize = true;
            this.rad10k.Location = new System.Drawing.Point(95, 59);
            this.rad10k.Name = "rad10k";
            this.rad10k.Size = new System.Drawing.Size(43, 17);
            this.rad10k.TabIndex = 3;
            this.rad10k.TabStop = true;
            this.rad10k.Text = "10k";
            this.rad10k.UseVisualStyleBackColor = true;
            // 
            // radHalfMarathon
            // 
            this.radHalfMarathon.AutoSize = true;
            this.radHalfMarathon.Location = new System.Drawing.Point(95, 82);
            this.radHalfMarathon.Name = "radHalfMarathon";
            this.radHalfMarathon.Size = new System.Drawing.Size(92, 17);
            this.radHalfMarathon.TabIndex = 3;
            this.radHalfMarathon.TabStop = true;
            this.radHalfMarathon.Text = "Half-Marathon";
            this.radHalfMarathon.UseVisualStyleBackColor = true;
            // 
            // lblMarathon
            // 
            this.lblMarathon.Location = new System.Drawing.Point(30, 107);
            this.lblMarathon.Name = "lblMarathon";
            this.lblMarathon.Size = new System.Drawing.Size(59, 13);
            this.lblMarathon.TabIndex = 1;
            this.lblMarathon.Text = "26.2 miles";
            this.lblMarathon.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblHalfMarathon
            // 
            this.lblHalfMarathon.Location = new System.Drawing.Point(30, 84);
            this.lblHalfMarathon.Name = "lblHalfMarathon";
            this.lblHalfMarathon.Size = new System.Drawing.Size(59, 13);
            this.lblHalfMarathon.TabIndex = 1;
            this.lblHalfMarathon.Text = "13.1 miles";
            this.lblHalfMarathon.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbl10k
            // 
            this.lbl10k.Location = new System.Drawing.Point(30, 61);
            this.lbl10k.Name = "lbl10k";
            this.lbl10k.Size = new System.Drawing.Size(59, 13);
            this.lbl10k.TabIndex = 1;
            this.lbl10k.Text = "6.2 miles";
            this.lbl10k.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbl5k
            // 
            this.lbl5k.Location = new System.Drawing.Point(30, 38);
            this.lbl5k.Name = "lbl5k";
            this.lbl5k.Size = new System.Drawing.Size(59, 13);
            this.lbl5k.TabIndex = 1;
            this.lbl5k.Text = "3.1 miles";
            this.lbl5k.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblEndText
            // 
            this.lblEndText.Location = new System.Drawing.Point(181, 132);
            this.lblEndText.Name = "lblEndText";
            this.lblEndText.Size = new System.Drawing.Size(150, 13);
            this.lblEndText.TabIndex = 1;
            this.lblEndText.Text = "End/Race Date";
            this.lblEndText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblEndDate
            // 
            this.lblEndDate.Location = new System.Drawing.Point(181, 281);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(150, 13);
            this.lblEndDate.TabIndex = 1;
            this.lblEndDate.Text = "3/25/2011";
            this.lblEndDate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblStartDate
            // 
            this.lblStartDate.Location = new System.Drawing.Point(25, 281);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(150, 13);
            this.lblStartDate.TabIndex = 1;
            this.lblStartDate.Text = "11/25/2010";
            this.lblStartDate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblStartText
            // 
            this.lblStartText.Location = new System.Drawing.Point(22, 132);
            this.lblStartText.Name = "lblStartText";
            this.lblStartText.Size = new System.Drawing.Size(150, 13);
            this.lblStartText.TabIndex = 1;
            this.lblStartText.Text = "Start Date";
            this.lblStartText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // calEnd
            // 
            this.calEnd.BackColor = System.Drawing.SystemColors.Window;
            this.calEnd.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.RoundShadow;
            this.calEnd.Location = new System.Drawing.Point(181, 148);
            this.calEnd.MarkedColor = System.Drawing.SystemColors.Highlight;
            this.calEnd.MarkedColorText = System.Drawing.SystemColors.HighlightText;
            this.calEnd.MonthTitleColor = System.Drawing.SystemColors.InactiveCaption;
            this.calEnd.MonthTitleColorText = System.Drawing.SystemColors.InactiveCaptionText;
            this.calEnd.Name = "calEnd";
            this.calEnd.OneMonthOnly = true;
            this.calEnd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.calEnd.SelectedColor = System.Drawing.SystemColors.Highlight;
            this.calEnd.SelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.calEnd.SelectMode = ZoneFiveSoftware.Common.Visuals.Calendar.SelectModeType.Calendar;
            this.calEnd.Size = new System.Drawing.Size(150, 130);
            this.calEnd.StartOfWeek = System.DayOfWeek.Monday;
            this.calEnd.TabIndex = 0;
            // 
            // calStart
            // 
            this.calStart.BackColor = System.Drawing.SystemColors.Window;
            this.calStart.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.RoundShadow;
            this.calStart.Location = new System.Drawing.Point(25, 148);
            this.calStart.MarkedColor = System.Drawing.SystemColors.Highlight;
            this.calStart.MarkedColorText = System.Drawing.SystemColors.HighlightText;
            this.calStart.MonthTitleColor = System.Drawing.SystemColors.InactiveCaption;
            this.calStart.MonthTitleColorText = System.Drawing.SystemColors.InactiveCaptionText;
            this.calStart.Name = "calStart";
            this.calStart.OneMonthOnly = true;
            this.calStart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.calStart.SelectedColor = System.Drawing.SystemColors.Highlight;
            this.calStart.SelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.calStart.SelectMode = ZoneFiveSoftware.Common.Visuals.Calendar.SelectModeType.Calendar;
            this.calStart.Size = new System.Drawing.Size(150, 130);
            this.calStart.StartOfWeek = System.DayOfWeek.Monday;
            this.calStart.TabIndex = 0;
            // 
            // stepDetailInfo
            // 
            this.stepDetailInfo.BindingImage = ((System.Drawing.Image)(resources.GetObject("stepDetailInfo.BindingImage")));
            this.stepDetailInfo.Controls.Add(this.txtAvgHours);
            this.stepDetailInfo.Controls.Add(this.txtName);
            this.stepDetailInfo.Controls.Add(this.txtAvgDistance);
            this.stepDetailInfo.Controls.Add(this.lblMaxHours);
            this.stepDetailInfo.Controls.Add(this.lblMaxDistance);
            this.stepDetailInfo.Controls.Add(this.lblName);
            this.stepDetailInfo.Controls.Add(this.lblAvgHoursText);
            this.stepDetailInfo.Controls.Add(this.lblAvgDistanceText);
            this.stepDetailInfo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.stepDetailInfo.Name = "stepDetailInfo";
            // 
            // txtAvgHours
            // 
            this.txtAvgHours.AcceptsReturn = false;
            this.txtAvgHours.AcceptsTab = false;
            this.txtAvgHours.BackColor = System.Drawing.Color.White;
            this.txtAvgHours.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtAvgHours.ButtonImage = null;
            this.txtAvgHours.Location = new System.Drawing.Point(273, 194);
            this.txtAvgHours.MaxLength = 32767;
            this.txtAvgHours.Multiline = false;
            this.txtAvgHours.Name = "txtAvgHours";
            this.txtAvgHours.ReadOnly = false;
            this.txtAvgHours.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtAvgHours.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtAvgHours.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtAvgHours.Size = new System.Drawing.Size(100, 19);
            this.txtAvgHours.TabIndex = 7;
            this.txtAvgHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtName
            // 
            this.txtName.AcceptsReturn = false;
            this.txtName.AcceptsTab = false;
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtName.ButtonImage = null;
            this.txtName.Location = new System.Drawing.Point(179, 104);
            this.txtName.MaxLength = 32767;
            this.txtName.Multiline = false;
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = false;
            this.txtName.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtName.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtName.Size = new System.Drawing.Size(100, 19);
            this.txtName.TabIndex = 8;
            this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // txtAvgDistance
            // 
            this.txtAvgDistance.AcceptsReturn = false;
            this.txtAvgDistance.AcceptsTab = false;
            this.txtAvgDistance.BackColor = System.Drawing.Color.White;
            this.txtAvgDistance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtAvgDistance.ButtonImage = null;
            this.txtAvgDistance.Location = new System.Drawing.Point(273, 168);
            this.txtAvgDistance.MaxLength = 32767;
            this.txtAvgDistance.Multiline = false;
            this.txtAvgDistance.Name = "txtAvgDistance";
            this.txtAvgDistance.ReadOnly = false;
            this.txtAvgDistance.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtAvgDistance.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtAvgDistance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtAvgDistance.Size = new System.Drawing.Size(100, 19);
            this.txtAvgDistance.TabIndex = 8;
            this.txtAvgDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // lblMaxHours
            // 
            this.lblMaxHours.AutoSize = true;
            this.lblMaxHours.Location = new System.Drawing.Point(379, 200);
            this.lblMaxHours.Name = "lblMaxHours";
            this.lblMaxHours.Size = new System.Drawing.Size(126, 13);
            this.lblMaxHours.TabIndex = 6;
            this.lblMaxHours.Text = "Max per week ({0} hours)";
            // 
            // lblMaxDistance
            // 
            this.lblMaxDistance.AutoSize = true;
            this.lblMaxDistance.Location = new System.Drawing.Point(379, 174);
            this.lblMaxDistance.Name = "lblMaxDistance";
            this.lblMaxDistance.Size = new System.Drawing.Size(123, 13);
            this.lblMaxDistance.TabIndex = 3;
            this.lblMaxDistance.Text = "Max per week ({0} miles)";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(53, 110);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(62, 13);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Plan Name:";
            // 
            // lblAvgHoursText
            // 
            this.lblAvgHoursText.AutoSize = true;
            this.lblAvgHoursText.Location = new System.Drawing.Point(147, 200);
            this.lblAvgHoursText.Name = "lblAvgHoursText";
            this.lblAvgHoursText.Size = new System.Drawing.Size(123, 13);
            this.lblAvgHoursText.TabIndex = 4;
            this.lblAvgHoursText.Text = "Average hours per week";
            // 
            // lblAvgDistanceText
            // 
            this.lblAvgDistanceText.AutoSize = true;
            this.lblAvgDistanceText.Location = new System.Drawing.Point(147, 174);
            this.lblAvgDistanceText.Name = "lblAvgDistanceText";
            this.lblAvgDistanceText.Size = new System.Drawing.Size(120, 13);
            this.lblAvgDistanceText.TabIndex = 5;
            this.lblAvgDistanceText.Text = "Average miles per week";
            // 
            // stepReview
            // 
            this.stepReview.BindingImage = ((System.Drawing.Image)(resources.GetObject("stepReview.BindingImage")));
            this.stepReview.ForeColor = System.Drawing.SystemColors.ControlText;
            this.stepReview.Name = "stepReview";
            this.stepReview.Subtitle = "Please review your plan.  You\'ll be able to modify or tweak it later if you like." +
                "";
            this.stepReview.Title = "Review your selections";
            // 
            // finishStep1
            // 
            this.finishStep1.BindingImage = ((System.Drawing.Image)(resources.GetObject("finishStep1.BindingImage")));
            this.finishStep1.Name = "finishStep1";
            // 
            // PlanWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 421);
            this.Controls.Add(this.wizardControl);
            this.Name = "PlanWizard";
            this.Text = "PlanWizard";
            this.stepPrimaryInfo.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.stepDetailInfo.ResumeLayout(false);
            this.stepDetailInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private WizardBase.WizardControl wizardControl;
        private WizardBase.StartStep startStep1;
        private WizardBase.IntermediateStep stepPrimaryInfo;
        private WizardBase.FinishStep finishStep1;
        private System.Windows.Forms.Label lblEndText;
        private System.Windows.Forms.Label lblStartText;
        private ZoneFiveSoftware.Common.Visuals.Calendar calEnd;
        private ZoneFiveSoftware.Common.Visuals.Calendar calStart;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.RadioButton radMarathon;
        private System.Windows.Forms.RadioButton radHalfMarathon;
        private System.Windows.Forms.RadioButton rad10k;
        private System.Windows.Forms.RadioButton rad5k;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl5k;
        private System.Windows.Forms.RadioButton radOther;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtOther;
        private System.Windows.Forms.Label lblMarathon;
        private System.Windows.Forms.Label lblHalfMarathon;
        private System.Windows.Forms.Label lbl10k;
        private WizardBase.IntermediateStep stepReview;
        private WizardBase.IntermediateStep stepDetailInfo;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtAvgHours;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtName;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtAvgDistance;
        private System.Windows.Forms.Label lblMaxHours;
        private System.Windows.Forms.Label lblMaxDistance;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblAvgHoursText;
        private System.Windows.Forms.Label lblAvgDistanceText;
    }
}