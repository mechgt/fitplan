namespace FitPlan.UI
{
    partial class ScheduleControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduleControl));
            System.Drawing.Drawing2D.ColorBlend colorBlend4 = new System.Drawing.Drawing2D.ColorBlend();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.itemTree = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.contextTreelist = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxTreeAddPhase = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTreeInsertPhase = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTreeAddWorkout = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTreeDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTreeClosePlan = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTreeColumnsPicker = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTreeResetColumns = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlLibrary = new System.Windows.Forms.Panel();
            this.grpTemplate = new System.Windows.Forms.GroupBox();
            this.btnLibImage = new ZoneFiveSoftware.Common.Visuals.Button();
            this.txtLibName = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtLibNotes = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.radLibScore = new System.Windows.Forms.Label();
            this.radLibPace = new System.Windows.Forms.RadioButton();
            this.txtLibScore = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtLibCategory = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.radLibDist = new System.Windows.Forms.RadioButton();
            this.radLibTime = new System.Windows.Forms.RadioButton();
            this.txtLibTime = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblLibMi = new System.Windows.Forms.Label();
            this.lblLibMph = new System.Windows.Forms.Label();
            this.txtLibSpeed = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtLibPace = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtLibDist = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.bnrTree = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.btnDelTemplate = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnNewTemplate = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnGoogCal = new ZoneFiveSoftware.Common.Visuals.Button();
            this.trainingCal = new Pabo.Calendar.MonthCalendar();
            this.contextCalendar = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxCalLockWorkout = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCalAddTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCalScheduleWorkout = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCalSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxCalAddPhase = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCalInsertPhase = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCalAddWorkout = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCalDeleteWorkout = new System.Windows.Forms.ToolStripMenuItem();
            this.bnrCalendar = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.btnMaxLower = new ZoneFiveSoftware.Common.Visuals.Button();
            this.pnlChart = new System.Windows.Forms.Panel();
            this.zedChart = new ZoneFiveSoftware.Common.Visuals.Chart.ZedGraphControl();
            this.bnrChart = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.btnGroupBy = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnZoomFit = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnMaxUpper = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnRampEdit = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnTrainingLoad = new ZoneFiveSoftware.Common.Visuals.Button();
            this.menuTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuTreePlanOverview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTreeWorkoutLibrary = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTreeSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTreeShowSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTreeShowEmptyFolders = new System.Windows.Forms.ToolStripMenuItem();
            this.menuChart = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuChartTrainingLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartDistance = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartDistanceCum = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartTime = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartTimeCum = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuChartShowCTLtarget = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChartShowPhases = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGroupBy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuGroupDay = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGroupWeek = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGroupMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGroupCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.chartToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextInetCalBtn = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoSyncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.syncCalendarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFromGoogleCalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new FitPlan.Controls.TabControlBG();
            this.tabWeek = new System.Windows.Forms.TabPage();
            this.weekPanel = new System.Windows.Forms.TableLayoutPanel();
            this.dailyY2 = new FitPlan.Controls.DailyPanel();
            this.dailyY3 = new FitPlan.Controls.DailyPanel();
            this.dailyT2 = new FitPlan.Controls.DailyPanel();
            this.btnBack = new ZoneFiveSoftware.Common.Visuals.Button();
            this.dailyT3 = new FitPlan.Controls.DailyPanel();
            this.dailyY1 = new FitPlan.Controls.DailyPanel();
            this.dailyToday = new FitPlan.Controls.DailyPanel();
            this.dailyT1 = new FitPlan.Controls.DailyPanel();
            this.btnNext = new ZoneFiveSoftware.Common.Visuals.Button();
            this.WeekSummary = new FitPlan.Controls.DailyPanel();
            this.tabDetails = new System.Windows.Forms.TabPage();
            this.grpGarminFitness = new System.Windows.Forms.GroupBox();
            this.txtPlanGarminAutoSched = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.chkGarminAutoSched = new System.Windows.Forms.CheckBox();
            this.lblPlanDays = new System.Windows.Forms.Label();
            this.btnGarminFitness = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnLockWorkout = new ZoneFiveSoftware.Common.Visuals.Button();
            this.txtWorkoutCategory = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.grpPhase = new System.Windows.Forms.GroupBox();
            this.txtPhaseEnd = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtPhaseStart = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblPhaseEnd = new System.Windows.Forms.Label();
            this.lblPhaseDuration = new System.Windows.Forms.Label();
            this.lblPhaseStart = new System.Windows.Forms.Label();
            this.grpPlan = new System.Windows.Forms.GroupBox();
            this.lblPlanEnd = new System.Windows.Forms.Label();
            this.lblPlanStart = new System.Windows.Forms.Label();
            this.lblActivityCount = new System.Windows.Forms.Label();
            this.txtWorkoutNotes = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.grpRepeat = new System.Windows.Forms.GroupBox();
            this.btnParent = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnLink = new ZoneFiveSoftware.Common.Visuals.Button();
            this.chkRepeat = new System.Windows.Forms.CheckBox();
            this.txtWorkoutRamp = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblWorkoutEnd = new System.Windows.Forms.Label();
            this.txtWorkoutEnd = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblWorkoutRepeat = new System.Windows.Forms.Label();
            this.txtWorkoutPeriod = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblWorkoutDays = new System.Windows.Forms.Label();
            this.lblWorkoutRamp = new System.Windows.Forms.Label();
            this.grpWorkout = new System.Windows.Forms.GroupBox();
            this.btnImage = new ZoneFiveSoftware.Common.Visuals.Button();
            this.radPace = new System.Windows.Forms.RadioButton();
            this.radDistance = new System.Windows.Forms.RadioButton();
            this.radTime = new System.Windows.Forms.RadioButton();
            this.txtWorkoutTime = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblWorkoutMi = new System.Windows.Forms.Label();
            this.lblWorkoutMph = new System.Windows.Forms.Label();
            this.txtWorkoutSpeed = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtWorkoutPace = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtWorkoutDistance = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtWorkoutScore = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.lblWorkoutScore = new System.Windows.Forms.Label();
            this.lblWorkoutTitle = new System.Windows.Forms.Label();
            this.radTSS = new System.Windows.Forms.RadioButton();
            this.radTrimp = new System.Windows.Forms.RadioButton();
            this.lblPlanHelp = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.lblPhase = new System.Windows.Forms.Label();
            this.lblPlanTitle = new System.Windows.Forms.Label();
            this.txtGarminWorkout = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.btnGarminWorkout = new ZoneFiveSoftware.Common.Visuals.Button();
            this.txtWorkoutName = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtPhaseName = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.txtPlanName = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.tabPlanning = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.hiLoSlider1 = new FitPlan.Controls.HiLoSlider();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextTreelist.SuspendLayout();
            this.pnlLibrary.SuspendLayout();
            this.grpTemplate.SuspendLayout();
            this.bnrTree.SuspendLayout();
            this.contextCalendar.SuspendLayout();
            this.bnrCalendar.SuspendLayout();
            this.pnlChart.SuspendLayout();
            this.bnrChart.SuspendLayout();
            this.menuTree.SuspendLayout();
            this.menuChart.SuspendLayout();
            this.menuGroupBy.SuspendLayout();
            this.contextInetCalBtn.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabWeek.SuspendLayout();
            this.weekPanel.SuspendLayout();
            this.tabDetails.SuspendLayout();
            this.grpGarminFitness.SuspendLayout();
            this.grpPhase.SuspendLayout();
            this.grpPlan.SuspendLayout();
            this.grpRepeat.SuspendLayout();
            this.grpWorkout.SuspendLayout();
            this.tabPlanning.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 229);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.itemTree);
            this.splitContainer1.Panel1.Controls.Add(this.pnlLibrary);
            this.splitContainer1.Panel1.Controls.Add(this.bnrTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnGoogCal);
            this.splitContainer1.Panel2.Controls.Add(this.trainingCal);
            this.splitContainer1.Panel2.Controls.Add(this.bnrCalendar);
            this.splitContainer1.Size = new System.Drawing.Size(1013, 374);
            this.splitContainer1.SplitterDistance = 337;
            this.splitContainer1.TabIndex = 2;
            // 
            // itemTree
            // 
            this.itemTree.BackColor = System.Drawing.Color.Transparent;
            this.itemTree.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.itemTree.CheckBoxes = false;
            this.itemTree.ContextMenuStrip = this.contextTreelist;
            this.itemTree.DefaultIndent = 15;
            this.itemTree.DefaultRowHeight = -1;
            this.itemTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemTree.HeaderRowHeight = 21;
            this.itemTree.Location = new System.Drawing.Point(0, 23);
            this.itemTree.MultiSelect = false;
            this.itemTree.Name = "itemTree";
            this.itemTree.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.itemTree.NumLockedColumns = 0;
            this.itemTree.RowAlternatingColors = true;
            this.itemTree.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(10)))), ((int)(((byte)(36)))), ((int)(((byte)(106)))));
            this.itemTree.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.itemTree.RowHotlightMouse = true;
            this.itemTree.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.itemTree.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.itemTree.RowSeparatorLines = true;
            this.itemTree.ShowLines = false;
            this.itemTree.ShowPlusMinus = true;
            this.itemTree.Size = new System.Drawing.Size(337, 182);
            this.itemTree.TabIndex = 3;
            this.itemTree.Load += new System.EventHandler(this.phaseTree_Load);
            this.itemTree.DoubleClick += new System.EventHandler(this.itemTree_DoubleClick);
            this.itemTree.SelectedItemsChanged += new System.EventHandler(this.itemTreePlanItem_SelectedItemsChanged);
            this.itemTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.itemTree_MouseDown);
            this.itemTree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.itemTree_KeyPress);
            this.itemTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.itemTree_KeyDown);
            // 
            // contextTreelist
            // 
            this.contextTreelist.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxTreeAddPhase,
            this.ctxTreeInsertPhase,
            this.ctxTreeAddWorkout,
            this.ctxTreeDelete,
            this.ctxTreeClosePlan,
            this.ctxTreeColumnsPicker,
            this.ctxTreeResetColumns});
            this.contextTreelist.Name = "contextCalendar";
            this.contextTreelist.Size = new System.Drawing.Size(154, 158);
            this.contextTreelist.Opening += new System.ComponentModel.CancelEventHandler(this.contextTreelist_Opening);
            // 
            // ctxTreeAddPhase
            // 
            this.ctxTreeAddPhase.Image = ((System.Drawing.Image)(resources.GetObject("ctxTreeAddPhase.Image")));
            this.ctxTreeAddPhase.Name = "ctxTreeAddPhase";
            this.ctxTreeAddPhase.Size = new System.Drawing.Size(153, 22);
            this.ctxTreeAddPhase.Text = "Add Phase";
            this.ctxTreeAddPhase.Click += new System.EventHandler(this.addPhaseTreelistMenu_Click);
            // 
            // ctxTreeInsertPhase
            // 
            this.ctxTreeInsertPhase.Name = "ctxTreeInsertPhase";
            this.ctxTreeInsertPhase.Size = new System.Drawing.Size(153, 22);
            this.ctxTreeInsertPhase.Text = "Insert Phase";
            this.ctxTreeInsertPhase.Click += new System.EventHandler(this.insertPhase_Click);
            // 
            // ctxTreeAddWorkout
            // 
            this.ctxTreeAddWorkout.Image = ((System.Drawing.Image)(resources.GetObject("ctxTreeAddWorkout.Image")));
            this.ctxTreeAddWorkout.Name = "ctxTreeAddWorkout";
            this.ctxTreeAddWorkout.Size = new System.Drawing.Size(153, 22);
            this.ctxTreeAddWorkout.Text = "Add Workout";
            this.ctxTreeAddWorkout.Click += new System.EventHandler(this.addWorkoutTreelistMenu_Click);
            // 
            // ctxTreeDelete
            // 
            this.ctxTreeDelete.Image = ((System.Drawing.Image)(resources.GetObject("ctxTreeDelete.Image")));
            this.ctxTreeDelete.Name = "ctxTreeDelete";
            this.ctxTreeDelete.Size = new System.Drawing.Size(153, 22);
            this.ctxTreeDelete.Text = "Delete";
            this.ctxTreeDelete.Click += new System.EventHandler(this.deleteTreelistMenuItem_Click);
            // 
            // ctxTreeClosePlan
            // 
            this.ctxTreeClosePlan.Name = "ctxTreeClosePlan";
            this.ctxTreeClosePlan.Size = new System.Drawing.Size(153, 22);
            this.ctxTreeClosePlan.Text = "Close Plan";
            // 
            // ctxTreeColumnsPicker
            // 
            this.ctxTreeColumnsPicker.Image = ((System.Drawing.Image)(resources.GetObject("ctxTreeColumnsPicker.Image")));
            this.ctxTreeColumnsPicker.Name = "ctxTreeColumnsPicker";
            this.ctxTreeColumnsPicker.Size = new System.Drawing.Size(153, 22);
            this.ctxTreeColumnsPicker.Text = "List Settings...";
            this.ctxTreeColumnsPicker.Click += new System.EventHandler(this.ctxTreeColumnsPicker_Click);
            // 
            // ctxTreeResetColumns
            // 
            this.ctxTreeResetColumns.Name = "ctxTreeResetColumns";
            this.ctxTreeResetColumns.Size = new System.Drawing.Size(153, 22);
            this.ctxTreeResetColumns.Text = "Reset Columns";
            this.ctxTreeResetColumns.Click += new System.EventHandler(this.ctxTreeResetColumns_Click);
            // 
            // pnlLibrary
            // 
            this.pnlLibrary.Controls.Add(this.grpTemplate);
            this.pnlLibrary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLibrary.Location = new System.Drawing.Point(0, 205);
            this.pnlLibrary.MinimumSize = new System.Drawing.Size(300, 0);
            this.pnlLibrary.Name = "pnlLibrary";
            this.pnlLibrary.Size = new System.Drawing.Size(337, 169);
            this.pnlLibrary.TabIndex = 6;
            // 
            // grpTemplate
            // 
            this.grpTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTemplate.Controls.Add(this.btnLibImage);
            this.grpTemplate.Controls.Add(this.txtLibName);
            this.grpTemplate.Controls.Add(this.txtLibNotes);
            this.grpTemplate.Controls.Add(this.radLibScore);
            this.grpTemplate.Controls.Add(this.radLibPace);
            this.grpTemplate.Controls.Add(this.txtLibScore);
            this.grpTemplate.Controls.Add(this.txtLibCategory);
            this.grpTemplate.Controls.Add(this.radLibDist);
            this.grpTemplate.Controls.Add(this.radLibTime);
            this.grpTemplate.Controls.Add(this.txtLibTime);
            this.grpTemplate.Controls.Add(this.lblLibMi);
            this.grpTemplate.Controls.Add(this.lblLibMph);
            this.grpTemplate.Controls.Add(this.txtLibSpeed);
            this.grpTemplate.Controls.Add(this.txtLibPace);
            this.grpTemplate.Controls.Add(this.txtLibDist);
            this.grpTemplate.Location = new System.Drawing.Point(3, 3);
            this.grpTemplate.Name = "grpTemplate";
            this.grpTemplate.Size = new System.Drawing.Size(334, 163);
            this.grpTemplate.TabIndex = 32;
            this.grpTemplate.TabStop = false;
            this.grpTemplate.Text = "Template";
            // 
            // btnLibImage
            // 
            this.btnLibImage.BackColor = System.Drawing.Color.Transparent;
            this.btnLibImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLibImage.BackgroundImage")));
            this.btnLibImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnLibImage.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnLibImage.CenterImage = null;
            this.btnLibImage.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnLibImage.HyperlinkStyle = false;
            this.btnLibImage.ImageMargin = 2;
            this.btnLibImage.LeftImage = null;
            this.btnLibImage.Location = new System.Drawing.Point(134, 18);
            this.btnLibImage.Margin = new System.Windows.Forms.Padding(0);
            this.btnLibImage.Name = "btnLibImage";
            this.btnLibImage.PushStyle = true;
            this.btnLibImage.RightImage = null;
            this.btnLibImage.Size = new System.Drawing.Size(24, 24);
            this.btnLibImage.TabIndex = 33;
            this.btnLibImage.Tag = "";
            this.btnLibImage.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnLibImage.TextLeftMargin = 2;
            this.btnLibImage.TextRightMargin = 2;
            this.btnLibImage.Click += new System.EventHandler(this.btnImage_Click);
            // 
            // txtLibName
            // 
            this.txtLibName.AcceptsReturn = false;
            this.txtLibName.AcceptsTab = false;
            this.txtLibName.BackColor = System.Drawing.Color.White;
            this.txtLibName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtLibName.ButtonImage = null;
            this.txtLibName.Enabled = false;
            this.txtLibName.Location = new System.Drawing.Point(6, 18);
            this.txtLibName.MaxLength = 32767;
            this.txtLibName.Multiline = false;
            this.txtLibName.Name = "txtLibName";
            this.txtLibName.ReadOnly = false;
            this.txtLibName.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtLibName.ReadOnlyTextColor = System.Drawing.SystemColors.ControlText;
            this.txtLibName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtLibName.Size = new System.Drawing.Size(123, 19);
            this.txtLibName.TabIndex = 0;
            this.txtLibName.Tag = "Name";
            this.txtLibName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLibName.Leave += new System.EventHandler(this.txtLibInfo_Leave);
            // 
            // txtLibNotes
            // 
            this.txtLibNotes.AcceptsReturn = false;
            this.txtLibNotes.AcceptsTab = false;
            this.txtLibNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLibNotes.BackColor = System.Drawing.Color.White;
            this.txtLibNotes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtLibNotes.ButtonImage = null;
            this.txtLibNotes.Location = new System.Drawing.Point(164, 43);
            this.txtLibNotes.MaxLength = 32767;
            this.txtLibNotes.Multiline = true;
            this.txtLibNotes.Name = "txtLibNotes";
            this.txtLibNotes.ReadOnly = false;
            this.txtLibNotes.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtLibNotes.ReadOnlyTextColor = System.Drawing.SystemColors.ControlText;
            this.txtLibNotes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtLibNotes.Size = new System.Drawing.Size(164, 114);
            this.txtLibNotes.TabIndex = 7;
            this.txtLibNotes.Tag = "Notes";
            this.txtLibNotes.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLibNotes.Leave += new System.EventHandler(this.txtLibInfo_Leave);
            // 
            // radLibScore
            // 
            this.radLibScore.Location = new System.Drawing.Point(3, 137);
            this.radLibScore.Name = "radLibScore";
            this.radLibScore.Size = new System.Drawing.Size(75, 15);
            this.radLibScore.TabIndex = 25;
            this.radLibScore.Text = "Score";
            this.radLibScore.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // radLibPace
            // 
            this.radLibPace.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radLibPace.Location = new System.Drawing.Point(3, 91);
            this.radLibPace.Name = "radLibPace";
            this.radLibPace.Size = new System.Drawing.Size(75, 39);
            this.radLibPace.TabIndex = 30;
            this.radLibPace.Text = "Speed\r\nPace";
            this.radLibPace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radLibPace.UseVisualStyleBackColor = true;
            this.radLibPace.CheckedChanged += new System.EventHandler(this.radLibTimeDistancePace_CheckedChanged);
            // 
            // txtLibScore
            // 
            this.txtLibScore.AcceptsReturn = false;
            this.txtLibScore.AcceptsTab = false;
            this.txtLibScore.BackColor = System.Drawing.Color.White;
            this.txtLibScore.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtLibScore.ButtonImage = null;
            this.txtLibScore.Enabled = false;
            this.txtLibScore.Location = new System.Drawing.Point(83, 138);
            this.txtLibScore.MaxLength = 32767;
            this.txtLibScore.Multiline = false;
            this.txtLibScore.Name = "txtLibScore";
            this.txtLibScore.ReadOnly = false;
            this.txtLibScore.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtLibScore.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtLibScore.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtLibScore.Size = new System.Drawing.Size(46, 19);
            this.txtLibScore.TabIndex = 5;
            this.txtLibScore.Tag = "Score";
            this.txtLibScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLibScore.Leave += new System.EventHandler(this.txtLibInfo_Leave);
            // 
            // txtLibCategory
            // 
            this.txtLibCategory.AcceptsReturn = false;
            this.txtLibCategory.AcceptsTab = false;
            this.txtLibCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLibCategory.BackColor = System.Drawing.Color.White;
            this.txtLibCategory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtLibCategory.ButtonImage = ((System.Drawing.Image)(resources.GetObject("txtLibCategory.ButtonImage")));
            this.txtLibCategory.Location = new System.Drawing.Point(164, 18);
            this.txtLibCategory.MaxLength = 32767;
            this.txtLibCategory.Multiline = false;
            this.txtLibCategory.Name = "txtLibCategory";
            this.txtLibCategory.ReadOnly = false;
            this.txtLibCategory.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtLibCategory.ReadOnlyTextColor = System.Drawing.SystemColors.ControlText;
            this.txtLibCategory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtLibCategory.Size = new System.Drawing.Size(164, 19);
            this.txtLibCategory.TabIndex = 6;
            this.txtLibCategory.Tag = "Template";
            this.txtLibCategory.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLibCategory.ButtonClick += new System.EventHandler(this.txtCategory_Click);
            this.txtLibCategory.Leave += new System.EventHandler(this.txtWorkoutInfo_Leave);
            // 
            // radLibDist
            // 
            this.radLibDist.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radLibDist.Location = new System.Drawing.Point(3, 68);
            this.radLibDist.Name = "radLibDist";
            this.radLibDist.Size = new System.Drawing.Size(75, 17);
            this.radLibDist.TabIndex = 29;
            this.radLibDist.Text = "Distance";
            this.radLibDist.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radLibDist.UseVisualStyleBackColor = true;
            this.radLibDist.CheckedChanged += new System.EventHandler(this.radLibTimeDistancePace_CheckedChanged);
            // 
            // radLibTime
            // 
            this.radLibTime.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radLibTime.Location = new System.Drawing.Point(3, 45);
            this.radLibTime.Name = "radLibTime";
            this.radLibTime.Size = new System.Drawing.Size(75, 17);
            this.radLibTime.TabIndex = 28;
            this.radLibTime.Text = "Total Time";
            this.radLibTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radLibTime.UseVisualStyleBackColor = true;
            this.radLibTime.CheckedChanged += new System.EventHandler(this.radLibTimeDistancePace_CheckedChanged);
            // 
            // txtLibTime
            // 
            this.txtLibTime.AcceptsReturn = false;
            this.txtLibTime.AcceptsTab = false;
            this.txtLibTime.BackColor = System.Drawing.Color.White;
            this.txtLibTime.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtLibTime.ButtonImage = null;
            this.txtLibTime.Enabled = false;
            this.txtLibTime.Location = new System.Drawing.Point(83, 43);
            this.txtLibTime.MaxLength = 32767;
            this.txtLibTime.Multiline = false;
            this.txtLibTime.Name = "txtLibTime";
            this.txtLibTime.ReadOnly = false;
            this.txtLibTime.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtLibTime.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtLibTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtLibTime.Size = new System.Drawing.Size(46, 19);
            this.txtLibTime.TabIndex = 1;
            this.txtLibTime.Tag = "Time";
            this.txtLibTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLibTime.Leave += new System.EventHandler(this.txtLibInfo_Leave);
            // 
            // lblLibMi
            // 
            this.lblLibMi.AutoSize = true;
            this.lblLibMi.Location = new System.Drawing.Point(131, 72);
            this.lblLibMi.Name = "lblLibMi";
            this.lblLibMi.Size = new System.Drawing.Size(20, 13);
            this.lblLibMi.TabIndex = 32;
            this.lblLibMi.Text = "mi.";
            // 
            // lblLibMph
            // 
            this.lblLibMph.AutoSize = true;
            this.lblLibMph.Location = new System.Drawing.Point(131, 97);
            this.lblLibMph.Name = "lblLibMph";
            this.lblLibMph.Size = new System.Drawing.Size(27, 13);
            this.lblLibMph.TabIndex = 31;
            this.lblLibMph.Text = "mph";
            // 
            // txtLibSpeed
            // 
            this.txtLibSpeed.AcceptsReturn = false;
            this.txtLibSpeed.AcceptsTab = false;
            this.txtLibSpeed.BackColor = System.Drawing.Color.White;
            this.txtLibSpeed.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtLibSpeed.ButtonImage = null;
            this.txtLibSpeed.Enabled = false;
            this.txtLibSpeed.Location = new System.Drawing.Point(83, 93);
            this.txtLibSpeed.MaxLength = 32767;
            this.txtLibSpeed.Multiline = false;
            this.txtLibSpeed.Name = "txtLibSpeed";
            this.txtLibSpeed.ReadOnly = true;
            this.txtLibSpeed.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtLibSpeed.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtLibSpeed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtLibSpeed.Size = new System.Drawing.Size(46, 19);
            this.txtLibSpeed.TabIndex = 3;
            this.txtLibSpeed.Tag = "Speed";
            this.txtLibSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLibSpeed.Leave += new System.EventHandler(this.txtLibInfo_Leave);
            // 
            // txtLibPace
            // 
            this.txtLibPace.AcceptsReturn = false;
            this.txtLibPace.AcceptsTab = false;
            this.txtLibPace.BackColor = System.Drawing.Color.White;
            this.txtLibPace.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtLibPace.ButtonImage = null;
            this.txtLibPace.Enabled = false;
            this.txtLibPace.Location = new System.Drawing.Point(83, 113);
            this.txtLibPace.MaxLength = 32767;
            this.txtLibPace.Multiline = false;
            this.txtLibPace.Name = "txtLibPace";
            this.txtLibPace.ReadOnly = true;
            this.txtLibPace.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtLibPace.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtLibPace.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtLibPace.Size = new System.Drawing.Size(46, 19);
            this.txtLibPace.TabIndex = 4;
            this.txtLibPace.Tag = "Pace";
            this.txtLibPace.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLibPace.Leave += new System.EventHandler(this.txtLibInfo_Leave);
            // 
            // txtLibDist
            // 
            this.txtLibDist.AcceptsReturn = false;
            this.txtLibDist.AcceptsTab = false;
            this.txtLibDist.BackColor = System.Drawing.Color.White;
            this.txtLibDist.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtLibDist.ButtonImage = null;
            this.txtLibDist.Enabled = false;
            this.txtLibDist.Location = new System.Drawing.Point(83, 68);
            this.txtLibDist.MaxLength = 32767;
            this.txtLibDist.Multiline = false;
            this.txtLibDist.Name = "txtLibDist";
            this.txtLibDist.ReadOnly = false;
            this.txtLibDist.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtLibDist.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtLibDist.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtLibDist.Size = new System.Drawing.Size(46, 19);
            this.txtLibDist.TabIndex = 2;
            this.txtLibDist.Tag = "Distance";
            this.txtLibDist.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtLibDist.Leave += new System.EventHandler(this.txtLibInfo_Leave);
            // 
            // bnrTree
            // 
            this.bnrTree.BackColor = System.Drawing.Color.Transparent;
            this.bnrTree.Controls.Add(this.btnDelTemplate);
            this.bnrTree.Controls.Add(this.btnNewTemplate);
            this.bnrTree.Dock = System.Windows.Forms.DockStyle.Top;
            this.bnrTree.HasMenuButton = true;
            this.bnrTree.Location = new System.Drawing.Point(0, 0);
            this.bnrTree.Name = "bnrTree";
            this.bnrTree.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bnrTree.Size = new System.Drawing.Size(337, 23);
            this.bnrTree.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.bnrTree.TabIndex = 5;
            this.bnrTree.UseStyleFont = true;
            this.bnrTree.MenuClicked += new System.EventHandler(this.bnrTree_MenuClicked);
            // 
            // btnDelTemplate
            // 
            this.btnDelTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelTemplate.BackColor = System.Drawing.Color.Transparent;
            this.btnDelTemplate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelTemplate.BackgroundImage")));
            this.btnDelTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDelTemplate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnDelTemplate.CenterImage = null;
            this.btnDelTemplate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDelTemplate.HyperlinkStyle = false;
            this.btnDelTemplate.ImageMargin = 2;
            this.btnDelTemplate.LeftImage = null;
            this.btnDelTemplate.Location = new System.Drawing.Point(2742, 0);
            this.btnDelTemplate.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelTemplate.Name = "btnDelTemplate";
            this.btnDelTemplate.PushStyle = true;
            this.btnDelTemplate.RightImage = null;
            this.btnDelTemplate.Size = new System.Drawing.Size(24, 24);
            this.btnDelTemplate.TabIndex = 19;
            this.btnDelTemplate.Tag = "263";
            this.btnDelTemplate.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnDelTemplate.TextLeftMargin = 2;
            this.btnDelTemplate.TextRightMargin = 2;
            this.btnDelTemplate.Click += new System.EventHandler(this.btnDelTemplate_Click);
            // 
            // btnNewTemplate
            // 
            this.btnNewTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewTemplate.BackColor = System.Drawing.Color.Transparent;
            this.btnNewTemplate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNewTemplate.BackgroundImage")));
            this.btnNewTemplate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNewTemplate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnNewTemplate.CenterImage = null;
            this.btnNewTemplate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnNewTemplate.HyperlinkStyle = false;
            this.btnNewTemplate.ImageMargin = 2;
            this.btnNewTemplate.LeftImage = null;
            this.btnNewTemplate.Location = new System.Drawing.Point(2766, 0);
            this.btnNewTemplate.Margin = new System.Windows.Forms.Padding(0);
            this.btnNewTemplate.Name = "btnNewTemplate";
            this.btnNewTemplate.PushStyle = true;
            this.btnNewTemplate.RightImage = null;
            this.btnNewTemplate.Size = new System.Drawing.Size(24, 24);
            this.btnNewTemplate.TabIndex = 19;
            this.btnNewTemplate.Tag = "287";
            this.btnNewTemplate.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnNewTemplate.TextLeftMargin = 2;
            this.btnNewTemplate.TextRightMargin = 2;
            this.btnNewTemplate.Click += new System.EventHandler(this.btnNewTemplate_Click);
            // 
            // btnGoogCal
            // 
            this.btnGoogCal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoogCal.BackColor = System.Drawing.Color.Transparent;
            this.btnGoogCal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGoogCal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnGoogCal.CenterImage = global::FitPlan.Properties.Resources.White_signin_Small_base_20dp;
            this.btnGoogCal.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnGoogCal.HyperlinkStyle = false;
            this.btnGoogCal.ImageMargin = 0;
            this.btnGoogCal.LeftImage = null;
            this.btnGoogCal.Location = new System.Drawing.Point(616, 26);
            this.btnGoogCal.Name = "btnGoogCal";
            this.btnGoogCal.PushStyle = true;
            this.btnGoogCal.RightImage = null;
            this.btnGoogCal.Size = new System.Drawing.Size(24, 24);
            this.btnGoogCal.TabIndex = 34;
            this.btnGoogCal.Tag = "616, 26";
            this.btnGoogCal.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnGoogCal.TextLeftMargin = 0;
            this.btnGoogCal.TextRightMargin = 0;
            this.btnGoogCal.Click += new System.EventHandler(this.btnGoogCal_Click);
            // 
            // trainingCal
            // 
            this.trainingCal.ActiveMonth.Month = 11;
            this.trainingCal.ActiveMonth.Year = 2010;
            this.trainingCal.AllowDrop = true;
            this.trainingCal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(198)))), ((int)(((byte)(214)))));
            this.trainingCal.ContextMenuStrip = this.contextCalendar;
            this.trainingCal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trainingCal.FirstDayOfWeek = 0;
            this.trainingCal.Footer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.trainingCal.Header.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(177)))), ((int)(((byte)(179)))), ((int)(((byte)(200)))));
            this.trainingCal.Header.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.trainingCal.Header.TextColor = System.Drawing.Color.White;
            this.trainingCal.ImageList = null;
            this.trainingCal.Location = new System.Drawing.Point(0, 23);
            this.trainingCal.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.trainingCal.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.trainingCal.Month.BackgroundImage = null;
            this.trainingCal.Month.Colors.Focus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(213)))), ((int)(((byte)(224)))));
            this.trainingCal.Month.Colors.Focus.Border = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(198)))), ((int)(((byte)(214)))));
            this.trainingCal.Month.Colors.Selected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(198)))), ((int)(((byte)(214)))));
            this.trainingCal.Month.Colors.Selected.Border = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(97)))), ((int)(((byte)(135)))));
            this.trainingCal.Month.DateAlign = Pabo.Calendar.mcItemAlign.TopLeft;
            this.trainingCal.Month.DateFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.trainingCal.Month.ShowMonthInDay = true;
            this.trainingCal.Month.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.trainingCal.Name = "trainingCal";
            this.trainingCal.SelectionMode = Pabo.Calendar.mcSelectionMode.One;
            this.trainingCal.ShowFooter = false;
            this.trainingCal.Size = new System.Drawing.Size(672, 351);
            this.trainingCal.TabIndex = 3;
            this.trainingCal.Theme = true;
            this.trainingCal.Weekdays.BackColor2 = System.Drawing.Color.Silver;
            this.trainingCal.Weekdays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.trainingCal.Weekdays.GradientMode = Pabo.Calendar.mcGradientMode.Horizontal;
            this.trainingCal.Weekdays.TextColor = System.Drawing.SystemColors.ActiveCaption;
            this.trainingCal.Weeknumbers.GradientMode = Pabo.Calendar.mcGradientMode.Vertical;
            this.trainingCal.Weeknumbers.TextColor = System.Drawing.SystemColors.ActiveCaption;
            this.trainingCal.DayDoubleClick += new Pabo.Calendar.DayClickEventHandler(this.trainingCal_DayDoubleClick);
            this.trainingCal.DragEnter += new System.Windows.Forms.DragEventHandler(this.trainingCal_DragEnter);
            this.trainingCal.DayDragDrop += new Pabo.Calendar.DayDragDropEventHandler(this.trainingCal_DayDragDrop);
            this.trainingCal.DaySelected += new Pabo.Calendar.DaySelectedEventHandler(this.trainingCal_DaySelected);
            this.trainingCal.DayClick += new Pabo.Calendar.DayClickEventHandler(this.trainingCal_DayClick);
            // 
            // contextCalendar
            // 
            this.contextCalendar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxCalLockWorkout,
            this.ctxCalAddTemplate,
            this.ctxCalScheduleWorkout,
            this.ctxCalSeparator1,
            this.ctxCalAddPhase,
            this.ctxCalInsertPhase,
            this.ctxCalAddWorkout,
            this.ctxCalDeleteWorkout});
            this.contextCalendar.Name = "contextCalendar";
            this.contextCalendar.Size = new System.Drawing.Size(172, 164);
            this.contextCalendar.Opening += new System.ComponentModel.CancelEventHandler(this.contextCalendar_Opening);
            // 
            // ctxCalLockWorkout
            // 
            this.ctxCalLockWorkout.Image = ((System.Drawing.Image)(resources.GetObject("ctxCalLockWorkout.Image")));
            this.ctxCalLockWorkout.Name = "ctxCalLockWorkout";
            this.ctxCalLockWorkout.Size = new System.Drawing.Size(171, 22);
            this.ctxCalLockWorkout.Text = "Lock Workout";
            this.ctxCalLockWorkout.Click += new System.EventHandler(this.ctxCalLockWorkout_Click);
            // 
            // ctxCalAddTemplate
            // 
            this.ctxCalAddTemplate.Name = "ctxCalAddTemplate";
            this.ctxCalAddTemplate.Size = new System.Drawing.Size(171, 22);
            this.ctxCalAddTemplate.Text = "Add to Library";
            this.ctxCalAddTemplate.Click += new System.EventHandler(this.ctxCalAddTemplate_Click);
            // 
            // ctxCalScheduleWorkout
            // 
            this.ctxCalScheduleWorkout.Image = ((System.Drawing.Image)(resources.GetObject("ctxCalScheduleWorkout.Image")));
            this.ctxCalScheduleWorkout.Name = "ctxCalScheduleWorkout";
            this.ctxCalScheduleWorkout.Size = new System.Drawing.Size(171, 22);
            this.ctxCalScheduleWorkout.Text = "Schedule Workout";
            this.ctxCalScheduleWorkout.Click += new System.EventHandler(this.scheduleWorkoutToolStripMenuItem_Click);
            // 
            // ctxCalSeparator1
            // 
            this.ctxCalSeparator1.Name = "ctxCalSeparator1";
            this.ctxCalSeparator1.Size = new System.Drawing.Size(168, 6);
            // 
            // ctxCalAddPhase
            // 
            this.ctxCalAddPhase.Image = ((System.Drawing.Image)(resources.GetObject("ctxCalAddPhase.Image")));
            this.ctxCalAddPhase.Name = "ctxCalAddPhase";
            this.ctxCalAddPhase.Size = new System.Drawing.Size(171, 22);
            this.ctxCalAddPhase.Text = "Add Phase";
            this.ctxCalAddPhase.Click += new System.EventHandler(this.ctxCalAddPhase_Click);
            // 
            // ctxCalInsertPhase
            // 
            this.ctxCalInsertPhase.Image = ((System.Drawing.Image)(resources.GetObject("ctxCalInsertPhase.Image")));
            this.ctxCalInsertPhase.Name = "ctxCalInsertPhase";
            this.ctxCalInsertPhase.Size = new System.Drawing.Size(171, 22);
            this.ctxCalInsertPhase.Text = "Insert Phase";
            this.ctxCalInsertPhase.Click += new System.EventHandler(this.insertPhase_Click);
            // 
            // ctxCalAddWorkout
            // 
            this.ctxCalAddWorkout.Image = ((System.Drawing.Image)(resources.GetObject("ctxCalAddWorkout.Image")));
            this.ctxCalAddWorkout.Name = "ctxCalAddWorkout";
            this.ctxCalAddWorkout.Size = new System.Drawing.Size(171, 22);
            this.ctxCalAddWorkout.Text = "Add Workout";
            this.ctxCalAddWorkout.Click += new System.EventHandler(this.ctxCalAddWorkout_Click);
            // 
            // ctxCalDeleteWorkout
            // 
            this.ctxCalDeleteWorkout.Image = ((System.Drawing.Image)(resources.GetObject("ctxCalDeleteWorkout.Image")));
            this.ctxCalDeleteWorkout.Name = "ctxCalDeleteWorkout";
            this.ctxCalDeleteWorkout.Size = new System.Drawing.Size(171, 22);
            this.ctxCalDeleteWorkout.Text = "Delete Workout";
            this.ctxCalDeleteWorkout.Click += new System.EventHandler(this.ctxCalDeleteWorkout_Click);
            // 
            // bnrCalendar
            // 
            this.bnrCalendar.BackColor = System.Drawing.Color.Transparent;
            this.bnrCalendar.Controls.Add(this.btnMaxLower);
            this.bnrCalendar.Dock = System.Windows.Forms.DockStyle.Top;
            this.bnrCalendar.HasMenuButton = false;
            this.bnrCalendar.Location = new System.Drawing.Point(0, 0);
            this.bnrCalendar.Name = "bnrCalendar";
            this.bnrCalendar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bnrCalendar.Size = new System.Drawing.Size(672, 23);
            this.bnrCalendar.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.bnrCalendar.TabIndex = 6;
            this.bnrCalendar.UseStyleFont = true;
            // 
            // btnMaxLower
            // 
            this.btnMaxLower.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaxLower.BackColor = System.Drawing.Color.Transparent;
            this.btnMaxLower.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMaxLower.BackgroundImage")));
            this.btnMaxLower.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMaxLower.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnMaxLower.CenterImage = null;
            this.btnMaxLower.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnMaxLower.HyperlinkStyle = false;
            this.btnMaxLower.ImageMargin = 2;
            this.btnMaxLower.LeftImage = null;
            this.btnMaxLower.Location = new System.Drawing.Point(15497, 0);
            this.btnMaxLower.Margin = new System.Windows.Forms.Padding(0);
            this.btnMaxLower.Name = "btnMaxLower";
            this.btnMaxLower.PushStyle = true;
            this.btnMaxLower.RightImage = null;
            this.btnMaxLower.Size = new System.Drawing.Size(24, 24);
            this.btnMaxLower.TabIndex = 1;
            this.btnMaxLower.Tag = "623";
            this.btnMaxLower.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnMaxLower.TextLeftMargin = 2;
            this.btnMaxLower.TextRightMargin = 2;
            this.btnMaxLower.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // pnlChart
            // 
            this.pnlChart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlChart.Controls.Add(this.zedChart);
            this.pnlChart.Controls.Add(this.bnrChart);
            this.pnlChart.Location = new System.Drawing.Point(805, 0);
            this.pnlChart.Name = "pnlChart";
            this.pnlChart.Size = new System.Drawing.Size(208, 229);
            this.pnlChart.TabIndex = 4;
            // 
            // zedChart
            // 
            this.zedChart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.zedChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedChart.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zedChart.EditModifierKeys = System.Windows.Forms.Keys.None;
            this.zedChart.FitToSelection = true;
            this.zedChart.IsShowPointValues = true;
            this.zedChart.Location = new System.Drawing.Point(0, 23);
            this.zedChart.Name = "zedChart";
            this.zedChart.PointDateFormat = "d";
            this.zedChart.PointValueFormat = "#.#";
            this.zedChart.ScrollGrace = 0;
            this.zedChart.ScrollMaxX = 0;
            this.zedChart.ScrollMaxY = 0;
            this.zedChart.ScrollMaxY2 = 0;
            this.zedChart.ScrollMinX = 0;
            this.zedChart.ScrollMinY = 0;
            this.zedChart.ScrollMinY2 = 0;
            this.zedChart.Size = new System.Drawing.Size(208, 206);
            this.zedChart.TabIndex = 5;
            this.zedChart.PointEditEvent += new ZoneFiveSoftware.Common.Visuals.Chart.ZedGraphControl.PointEditHandler(this.zedPlanning_PointEditEvent);
            this.zedChart.PointValueEvent += new ZoneFiveSoftware.Common.Visuals.Chart.ZedGraphControl.PointValueHandler(this.zedChart_PointValueEvent);
            this.zedChart.MouseDownEvent += new ZoneFiveSoftware.Common.Visuals.Chart.ZedGraphControl.ZedMouseEventHandler(this.zedPlanning_MouseDownEvent);
            this.zedChart.DoubleClickEvent += new ZoneFiveSoftware.Common.Visuals.Chart.ZedGraphControl.ZedMouseEventHandler(this.zedPlanning_DoubleClickEvent);
            this.zedChart.CursorValueEvent += new ZoneFiveSoftware.Common.Visuals.Chart.ZedGraphControl.CursorValueHandler(this.zedChart_CursorValueEvent);
            // 
            // bnrChart
            // 
            this.bnrChart.BackColor = System.Drawing.Color.Transparent;
            this.bnrChart.Controls.Add(this.btnGroupBy);
            this.bnrChart.Controls.Add(this.btnZoomFit);
            this.bnrChart.Controls.Add(this.btnMaxUpper);
            this.bnrChart.Controls.Add(this.btnRampEdit);
            this.bnrChart.Controls.Add(this.btnTrainingLoad);
            this.bnrChart.Dock = System.Windows.Forms.DockStyle.Top;
            this.bnrChart.HasMenuButton = true;
            this.bnrChart.Location = new System.Drawing.Point(0, 0);
            this.bnrChart.Name = "bnrChart";
            this.bnrChart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bnrChart.Size = new System.Drawing.Size(208, 23);
            this.bnrChart.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.bnrChart.TabIndex = 4;
            this.bnrChart.UseStyleFont = true;
            this.bnrChart.MenuClicked += new System.EventHandler(this.bnrChart_MenuClicked);
            // 
            // btnGroupBy
            // 
            this.btnGroupBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGroupBy.AutoSize = true;
            this.btnGroupBy.BackColor = System.Drawing.Color.Transparent;
            this.btnGroupBy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGroupBy.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnGroupBy.CenterImage = null;
            this.btnGroupBy.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnGroupBy.HyperlinkStyle = false;
            this.btnGroupBy.ImageMargin = 2;
            this.btnGroupBy.LeftImage = ((System.Drawing.Image)(resources.GetObject("btnGroupBy.LeftImage")));
            this.btnGroupBy.Location = new System.Drawing.Point(-1777, 0);
            this.btnGroupBy.Margin = new System.Windows.Forms.Padding(0);
            this.btnGroupBy.Name = "btnGroupBy";
            this.btnGroupBy.PushStyle = true;
            this.btnGroupBy.RightImage = null;
            this.btnGroupBy.Size = new System.Drawing.Size(67, 22);
            this.btnGroupBy.TabIndex = 1;
            this.btnGroupBy.Tag = "64";
            this.btnGroupBy.Text = "Group";
            this.btnGroupBy.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnGroupBy.TextLeftMargin = 2;
            this.btnGroupBy.TextRightMargin = 2;
            this.btnGroupBy.Click += new System.EventHandler(this.btnGroupBy_Click);
            this.btnGroupBy.Resize += new System.EventHandler(this.btnGroupBy_Resize);
            // 
            // btnZoomFit
            // 
            this.btnZoomFit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZoomFit.BackColor = System.Drawing.Color.Transparent;
            this.btnZoomFit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnZoomFit.BackgroundImage")));
            this.btnZoomFit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnZoomFit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnZoomFit.CenterImage = null;
            this.btnZoomFit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnZoomFit.HyperlinkStyle = false;
            this.btnZoomFit.ImageMargin = 2;
            this.btnZoomFit.LeftImage = null;
            this.btnZoomFit.Location = new System.Drawing.Point(-1662, 0);
            this.btnZoomFit.Margin = new System.Windows.Forms.Padding(0);
            this.btnZoomFit.Name = "btnZoomFit";
            this.btnZoomFit.PushStyle = true;
            this.btnZoomFit.RightImage = null;
            this.btnZoomFit.Size = new System.Drawing.Size(24, 24);
            this.btnZoomFit.TabIndex = 1;
            this.btnZoomFit.Tag = "136";
            this.btnZoomFit.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnZoomFit.TextLeftMargin = 2;
            this.btnZoomFit.TextRightMargin = 2;
            this.btnZoomFit.Click += new System.EventHandler(this.btnZoomFit_Click);
            // 
            // btnMaxUpper
            // 
            this.btnMaxUpper.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaxUpper.BackColor = System.Drawing.Color.Transparent;
            this.btnMaxUpper.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMaxUpper.BackgroundImage")));
            this.btnMaxUpper.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnMaxUpper.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnMaxUpper.CenterImage = null;
            this.btnMaxUpper.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnMaxUpper.HyperlinkStyle = false;
            this.btnMaxUpper.ImageMargin = 2;
            this.btnMaxUpper.LeftImage = null;
            this.btnMaxUpper.Location = new System.Drawing.Point(-1638, 0);
            this.btnMaxUpper.Margin = new System.Windows.Forms.Padding(0);
            this.btnMaxUpper.Name = "btnMaxUpper";
            this.btnMaxUpper.PushStyle = true;
            this.btnMaxUpper.RightImage = null;
            this.btnMaxUpper.Size = new System.Drawing.Size(24, 24);
            this.btnMaxUpper.TabIndex = 1;
            this.btnMaxUpper.Tag = "160";
            this.btnMaxUpper.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnMaxUpper.TextLeftMargin = 2;
            this.btnMaxUpper.TextRightMargin = 2;
            this.btnMaxUpper.Click += new System.EventHandler(this.btnMaximize_Click);
            // 
            // btnRampEdit
            // 
            this.btnRampEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRampEdit.BackColor = System.Drawing.Color.Transparent;
            this.btnRampEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRampEdit.BackgroundImage")));
            this.btnRampEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRampEdit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnRampEdit.CenterImage = null;
            this.btnRampEdit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRampEdit.HyperlinkStyle = false;
            this.btnRampEdit.ImageMargin = 2;
            this.btnRampEdit.LeftImage = null;
            this.btnRampEdit.Location = new System.Drawing.Point(-1710, 0);
            this.btnRampEdit.Margin = new System.Windows.Forms.Padding(0);
            this.btnRampEdit.Name = "btnRampEdit";
            this.btnRampEdit.PushStyle = true;
            this.btnRampEdit.RightImage = null;
            this.btnRampEdit.Size = new System.Drawing.Size(24, 24);
            this.btnRampEdit.TabIndex = 1;
            this.btnRampEdit.Tag = "88";
            this.btnRampEdit.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnRampEdit.TextLeftMargin = 2;
            this.btnRampEdit.TextRightMargin = 2;
            this.btnRampEdit.Click += new System.EventHandler(this.btnRampEdit_Click);
            // 
            // btnTrainingLoad
            // 
            this.btnTrainingLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTrainingLoad.BackColor = System.Drawing.Color.Transparent;
            this.btnTrainingLoad.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTrainingLoad.BackgroundImage")));
            this.btnTrainingLoad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTrainingLoad.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnTrainingLoad.CenterImage = null;
            this.btnTrainingLoad.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnTrainingLoad.HyperlinkStyle = false;
            this.btnTrainingLoad.ImageMargin = 2;
            this.btnTrainingLoad.LeftImage = null;
            this.btnTrainingLoad.Location = new System.Drawing.Point(-1686, 0);
            this.btnTrainingLoad.Margin = new System.Windows.Forms.Padding(0);
            this.btnTrainingLoad.Name = "btnTrainingLoad";
            this.btnTrainingLoad.PushStyle = true;
            this.btnTrainingLoad.RightImage = null;
            this.btnTrainingLoad.Size = new System.Drawing.Size(24, 24);
            this.btnTrainingLoad.TabIndex = 1;
            this.btnTrainingLoad.Tag = "136";
            this.btnTrainingLoad.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnTrainingLoad.TextLeftMargin = 2;
            this.btnTrainingLoad.TextRightMargin = 2;
            this.btnTrainingLoad.Click += new System.EventHandler(this.btnTrainingLoad_Click);
            // 
            // menuTree
            // 
            this.menuTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTreePlanOverview,
            this.mnuTreeWorkoutLibrary,
            this.mnuTreeSeparator1,
            this.mnuTreeShowSummary,
            this.mnuTreeShowEmptyFolders});
            this.menuTree.Name = "menuTree";
            this.menuTree.Size = new System.Drawing.Size(182, 98);
            // 
            // mnuTreePlanOverview
            // 
            this.mnuTreePlanOverview.Checked = true;
            this.mnuTreePlanOverview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuTreePlanOverview.Name = "mnuTreePlanOverview";
            this.mnuTreePlanOverview.Size = new System.Drawing.Size(181, 22);
            this.mnuTreePlanOverview.Text = "Plan Overview";
            this.mnuTreePlanOverview.Click += new System.EventHandler(this.mnuTreeItem_Click);
            // 
            // mnuTreeWorkoutLibrary
            // 
            this.mnuTreeWorkoutLibrary.Name = "mnuTreeWorkoutLibrary";
            this.mnuTreeWorkoutLibrary.Size = new System.Drawing.Size(181, 22);
            this.mnuTreeWorkoutLibrary.Text = "Workout Library";
            this.mnuTreeWorkoutLibrary.Click += new System.EventHandler(this.mnuTreeItem_Click);
            // 
            // mnuTreeSeparator1
            // 
            this.mnuTreeSeparator1.Name = "mnuTreeSeparator1";
            this.mnuTreeSeparator1.Size = new System.Drawing.Size(178, 6);
            // 
            // mnuTreeShowSummary
            // 
            this.mnuTreeShowSummary.Checked = true;
            this.mnuTreeShowSummary.CheckOnClick = true;
            this.mnuTreeShowSummary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuTreeShowSummary.Name = "mnuTreeShowSummary";
            this.mnuTreeShowSummary.Size = new System.Drawing.Size(181, 22);
            this.mnuTreeShowSummary.Text = "Show Summary";
            this.mnuTreeShowSummary.Click += new System.EventHandler(this.mnuTreeShowSummary_Click);
            // 
            // mnuTreeShowEmptyFolders
            // 
            this.mnuTreeShowEmptyFolders.Checked = true;
            this.mnuTreeShowEmptyFolders.CheckOnClick = true;
            this.mnuTreeShowEmptyFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuTreeShowEmptyFolders.Name = "mnuTreeShowEmptyFolders";
            this.mnuTreeShowEmptyFolders.Size = new System.Drawing.Size(181, 22);
            this.mnuTreeShowEmptyFolders.Text = "Show Empty Folders";
            this.mnuTreeShowEmptyFolders.Click += new System.EventHandler(this.mnuTreeShowEmptyFolders_Click);
            // 
            // menuChart
            // 
            this.menuChart.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChartTrainingLoad,
            this.mnuChartDistance,
            this.mnuChartDistanceCum,
            this.mnuChartTime,
            this.mnuChartTimeCum,
            this.mnuChartSeparator1,
            this.mnuChartShowCTLtarget,
            this.mnuChartShowPhases});
            this.menuChart.Name = "menuTree";
            this.menuChart.Size = new System.Drawing.Size(192, 164);
            // 
            // mnuChartTrainingLoad
            // 
            this.mnuChartTrainingLoad.Checked = true;
            this.mnuChartTrainingLoad.CheckOnClick = true;
            this.mnuChartTrainingLoad.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuChartTrainingLoad.Name = "mnuChartTrainingLoad";
            this.mnuChartTrainingLoad.Size = new System.Drawing.Size(191, 22);
            this.mnuChartTrainingLoad.Tag = "TrainingLoad";
            this.mnuChartTrainingLoad.Text = "Training Load";
            this.mnuChartTrainingLoad.Click += new System.EventHandler(this.mnuChartItem_Click);
            // 
            // mnuChartDistance
            // 
            this.mnuChartDistance.CheckOnClick = true;
            this.mnuChartDistance.Name = "mnuChartDistance";
            this.mnuChartDistance.Size = new System.Drawing.Size(191, 22);
            this.mnuChartDistance.Tag = "Distance";
            this.mnuChartDistance.Text = "Distance";
            this.mnuChartDistance.Click += new System.EventHandler(this.mnuChartItem_Click);
            // 
            // mnuChartDistanceCum
            // 
            this.mnuChartDistanceCum.CheckOnClick = true;
            this.mnuChartDistanceCum.Name = "mnuChartDistanceCum";
            this.mnuChartDistanceCum.Size = new System.Drawing.Size(191, 22);
            this.mnuChartDistanceCum.Tag = "CumulativeDistance";
            this.mnuChartDistanceCum.Text = "Distance (Cumulative)";
            this.mnuChartDistanceCum.Click += new System.EventHandler(this.mnuChartItem_Click);
            // 
            // mnuChartTime
            // 
            this.mnuChartTime.CheckOnClick = true;
            this.mnuChartTime.Name = "mnuChartTime";
            this.mnuChartTime.Size = new System.Drawing.Size(191, 22);
            this.mnuChartTime.Tag = "Time";
            this.mnuChartTime.Text = "Time";
            this.mnuChartTime.Click += new System.EventHandler(this.mnuChartItem_Click);
            // 
            // mnuChartTimeCum
            // 
            this.mnuChartTimeCum.CheckOnClick = true;
            this.mnuChartTimeCum.Name = "mnuChartTimeCum";
            this.mnuChartTimeCum.Size = new System.Drawing.Size(191, 22);
            this.mnuChartTimeCum.Tag = "CumulativeTime";
            this.mnuChartTimeCum.Text = "Time (Cumulative)";
            this.mnuChartTimeCum.Click += new System.EventHandler(this.mnuChartItem_Click);
            // 
            // mnuChartSeparator1
            // 
            this.mnuChartSeparator1.Name = "mnuChartSeparator1";
            this.mnuChartSeparator1.Size = new System.Drawing.Size(188, 6);
            // 
            // mnuChartShowCTLtarget
            // 
            this.mnuChartShowCTLtarget.CheckOnClick = true;
            this.mnuChartShowCTLtarget.Name = "mnuChartShowCTLtarget";
            this.mnuChartShowCTLtarget.Size = new System.Drawing.Size(191, 22);
            this.mnuChartShowCTLtarget.Tag = "ignore";
            this.mnuChartShowCTLtarget.Text = "Show CTL Target";
            this.mnuChartShowCTLtarget.CheckedChanged += new System.EventHandler(this.mnuChartShowCTLTarget_CheckChanged);
            // 
            // mnuChartShowPhases
            // 
            this.mnuChartShowPhases.CheckOnClick = true;
            this.mnuChartShowPhases.Name = "mnuChartShowPhases";
            this.mnuChartShowPhases.Size = new System.Drawing.Size(191, 22);
            this.mnuChartShowPhases.Tag = "ignore";
            this.mnuChartShowPhases.Text = "Show Phases";
            this.mnuChartShowPhases.CheckedChanged += new System.EventHandler(this.mnuChartShowPhases_CheckChanged);
            // 
            // menuGroupBy
            // 
            this.menuGroupBy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGroupDay,
            this.menuGroupWeek,
            this.menuGroupMonth,
            this.menuGroupCategory});
            this.menuGroupBy.Name = "menuGroupBy";
            this.menuGroupBy.Size = new System.Drawing.Size(123, 92);
            // 
            // menuGroupDay
            // 
            this.menuGroupDay.Name = "menuGroupDay";
            this.menuGroupDay.Size = new System.Drawing.Size(122, 22);
            this.menuGroupDay.Tag = "Day";
            this.menuGroupDay.Text = "Day";
            this.menuGroupDay.Click += new System.EventHandler(this.menuGroupItem_Click);
            // 
            // menuGroupWeek
            // 
            this.menuGroupWeek.Name = "menuGroupWeek";
            this.menuGroupWeek.Size = new System.Drawing.Size(122, 22);
            this.menuGroupWeek.Tag = "Week";
            this.menuGroupWeek.Text = "Week";
            this.menuGroupWeek.Click += new System.EventHandler(this.menuGroupItem_Click);
            // 
            // menuGroupMonth
            // 
            this.menuGroupMonth.Name = "menuGroupMonth";
            this.menuGroupMonth.Size = new System.Drawing.Size(122, 22);
            this.menuGroupMonth.Tag = "Month";
            this.menuGroupMonth.Text = "Month";
            this.menuGroupMonth.Click += new System.EventHandler(this.menuGroupItem_Click);
            // 
            // menuGroupCategory
            // 
            this.menuGroupCategory.Name = "menuGroupCategory";
            this.menuGroupCategory.Size = new System.Drawing.Size(122, 22);
            this.menuGroupCategory.Tag = "Category";
            this.menuGroupCategory.Text = "Category";
            this.menuGroupCategory.Click += new System.EventHandler(this.menuGroupItem_Click);
            // 
            // chartToolTip
            // 
            this.chartToolTip.AutoPopDelay = 5000;
            this.chartToolTip.InitialDelay = 100;
            this.chartToolTip.ReshowDelay = 100;
            // 
            // contextInetCalBtn
            // 
            this.contextInetCalBtn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.autoSyncToolStripMenuItem,
            this.syncCalendarToolStripMenuItem,
            this.removeFromGoogleCalToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.contextInetCalBtn.Name = "contextInetCalBtn";
            this.contextInetCalBtn.Size = new System.Drawing.Size(208, 136);
            this.contextInetCalBtn.Opening += new System.ComponentModel.CancelEventHandler(this.contextInetCalBtn_Opening);
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Image = global::FitPlan.Properties.Resources.White_signin_Small_base_20dp;
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.loginToolStripMenuItem.Text = "Login...";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // autoSyncToolStripMenuItem
            // 
            this.autoSyncToolStripMenuItem.CheckOnClick = true;
            this.autoSyncToolStripMenuItem.Name = "autoSyncToolStripMenuItem";
            this.autoSyncToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.autoSyncToolStripMenuItem.Text = "Auto Sync";
            this.autoSyncToolStripMenuItem.Click += new System.EventHandler(this.autoSyncToolStripMenuItem_Click);
            // 
            // syncCalendarToolStripMenuItem
            // 
            this.syncCalendarToolStripMenuItem.Image = global::FitPlan.Properties.Resources.sync16;
            this.syncCalendarToolStripMenuItem.Name = "syncCalendarToolStripMenuItem";
            this.syncCalendarToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.syncCalendarToolStripMenuItem.Text = "Sync Calendar";
            this.syncCalendarToolStripMenuItem.Click += new System.EventHandler(this.syncCalendarToolStripMenuItem_Click);
            // 
            // removeFromGoogleCalToolStripMenuItem
            // 
            this.removeFromGoogleCalToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("removeFromGoogleCalToolStripMenuItem.Image")));
            this.removeFromGoogleCalToolStripMenuItem.Name = "removeFromGoogleCalToolStripMenuItem";
            this.removeFromGoogleCalToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.removeFromGoogleCalToolStripMenuItem.Text = "Remove from Google Cal";
            this.removeFromGoogleCalToolStripMenuItem.Click += new System.EventHandler(this.removeFromGoogleCalToolStripMenuItem_Click);
            // 
            // tabControl
            // 
            this.tabControl.BackColor = System.Drawing.SystemColors.Control;
            this.tabControl.Controls.Add(this.tabWeek);
            this.tabControl.Controls.Add(this.tabDetails);
            this.tabControl.Controls.Add(this.tabPlanning);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(803, 229);
            this.tabControl.TabIndex = 0;
            // 
            // tabWeek
            // 
            this.tabWeek.Controls.Add(this.weekPanel);
            this.tabWeek.Location = new System.Drawing.Point(4, 25);
            this.tabWeek.Name = "tabWeek";
            this.tabWeek.Size = new System.Drawing.Size(795, 200);
            this.tabWeek.TabIndex = 0;
            this.tabWeek.Text = "Week View";
            this.tabWeek.UseVisualStyleBackColor = true;
            // 
            // weekPanel
            // 
            this.weekPanel.ColumnCount = 10;
            this.weekPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.weekPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.weekPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.weekPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.weekPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.weekPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.weekPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.weekPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.49953F));
            this.weekPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.50328F));
            this.weekPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.weekPanel.Controls.Add(this.dailyY2, 0, 0);
            this.weekPanel.Controls.Add(this.dailyY3, 0, 0);
            this.weekPanel.Controls.Add(this.dailyT2, 6, 0);
            this.weekPanel.Controls.Add(this.btnBack, 0, 0);
            this.weekPanel.Controls.Add(this.dailyT3, 7, 0);
            this.weekPanel.Controls.Add(this.dailyY1, 3, 0);
            this.weekPanel.Controls.Add(this.dailyToday, 4, 0);
            this.weekPanel.Controls.Add(this.dailyT1, 5, 0);
            this.weekPanel.Controls.Add(this.btnNext, 9, 0);
            this.weekPanel.Controls.Add(this.WeekSummary, 8, 0);
            this.weekPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weekPanel.Location = new System.Drawing.Point(0, 0);
            this.weekPanel.Margin = new System.Windows.Forms.Padding(0);
            this.weekPanel.Name = "weekPanel";
            this.weekPanel.RowCount = 1;
            this.weekPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.weekPanel.Size = new System.Drawing.Size(795, 200);
            this.weekPanel.TabIndex = 1;
            // 
            // dailyY2
            // 
            this.dailyY2.ATL = 0F;
            this.dailyY2.BottomGradientColor = System.Drawing.Color.Empty;
            this.dailyY2.BottomGradientPercent = 0;
            this.dailyY2.CTL = 0F;
            this.dailyY2.Date = new System.DateTime(((long)(0)));
            this.dailyY2.Distance = null;
            this.dailyY2.DistanceActual = null;
            this.dailyY2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dailyY2.HeadingBackColor = System.Drawing.Color.Crimson;
            this.dailyY2.HeadingText = "12/1/2010";
            this.dailyY2.LeftGradientColor = System.Drawing.Color.Empty;
            this.dailyY2.LeftGradientPercent = 0;
            this.dailyY2.Location = new System.Drawing.Point(107, 3);
            this.dailyY2.Name = "dailyY2";
            this.dailyY2.Pace = null;
            this.dailyY2.PaceActual = null;
            this.dailyY2.RightGradientColor = System.Drawing.Color.Empty;
            this.dailyY2.RightGradientPercent = 0;
            this.dailyY2.Score = null;
            this.dailyY2.Size = new System.Drawing.Size(72, 194);
            this.dailyY2.TabIndex = 18;
            this.dailyY2.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dailyY2.Time = null;
            this.dailyY2.TimeActual = null;
            this.dailyY2.TopGradientColor = System.Drawing.Color.Empty;
            this.dailyY2.TopGradientPercent = 0;
            this.dailyY2.WorkoutName = null;
            // 
            // dailyY3
            // 
            this.dailyY3.ATL = 0F;
            this.dailyY3.BottomGradientColor = System.Drawing.Color.Empty;
            this.dailyY3.BottomGradientPercent = 0;
            this.dailyY3.CTL = 0F;
            this.dailyY3.Date = new System.DateTime(((long)(0)));
            this.dailyY3.Distance = null;
            this.dailyY3.DistanceActual = null;
            this.dailyY3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dailyY3.HeadingBackColor = System.Drawing.Color.Crimson;
            this.dailyY3.HeadingText = "12/1/2010";
            this.dailyY3.LeftGradientColor = System.Drawing.Color.Empty;
            this.dailyY3.LeftGradientPercent = 0;
            this.dailyY3.Location = new System.Drawing.Point(29, 3);
            this.dailyY3.Name = "dailyY3";
            this.dailyY3.Pace = null;
            this.dailyY3.PaceActual = null;
            this.dailyY3.RightGradientColor = System.Drawing.Color.Empty;
            this.dailyY3.RightGradientPercent = 0;
            this.dailyY3.Score = null;
            this.dailyY3.Size = new System.Drawing.Size(72, 194);
            this.dailyY3.TabIndex = 14;
            this.dailyY3.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dailyY3.Time = null;
            this.dailyY3.TimeActual = null;
            this.dailyY3.TopGradientColor = System.Drawing.Color.Empty;
            this.dailyY3.TopGradientPercent = 0;
            this.dailyY3.WorkoutName = null;
            // 
            // dailyT2
            // 
            this.dailyT2.ATL = 0F;
            this.dailyT2.BottomGradientColor = System.Drawing.Color.Empty;
            this.dailyT2.BottomGradientPercent = 0;
            this.dailyT2.CTL = 0F;
            this.dailyT2.Date = new System.DateTime(((long)(0)));
            this.dailyT2.Distance = null;
            this.dailyT2.DistanceActual = null;
            this.dailyT2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dailyT2.HeadingBackColor = System.Drawing.Color.Crimson;
            this.dailyT2.HeadingText = "12/1/2010";
            this.dailyT2.LeftGradientColor = System.Drawing.Color.Empty;
            this.dailyT2.LeftGradientPercent = 0;
            this.dailyT2.Location = new System.Drawing.Point(419, 3);
            this.dailyT2.Name = "dailyT2";
            this.dailyT2.Pace = null;
            this.dailyT2.PaceActual = null;
            this.dailyT2.RightGradientColor = System.Drawing.Color.Empty;
            this.dailyT2.RightGradientPercent = 0;
            this.dailyT2.Score = null;
            this.dailyT2.Size = new System.Drawing.Size(72, 194);
            this.dailyT2.TabIndex = 9;
            this.dailyT2.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dailyT2.Time = null;
            this.dailyT2.TimeActual = null;
            this.dailyT2.TopGradientColor = System.Drawing.Color.Empty;
            this.dailyT2.TopGradientPercent = 0;
            this.dailyT2.WorkoutName = null;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBack.BackgroundImage")));
            this.btnBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBack.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnBack.CenterImage = null;
            this.btnBack.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBack.HyperlinkStyle = false;
            this.btnBack.ImageMargin = 2;
            this.btnBack.LeftImage = null;
            this.btnBack.Location = new System.Drawing.Point(3, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.PushStyle = true;
            this.btnBack.RightImage = null;
            this.btnBack.Size = new System.Drawing.Size(20, 194);
            this.btnBack.TabIndex = 1;
            this.btnBack.Tag = "-1";
            this.btnBack.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnBack.TextLeftMargin = 2;
            this.btnBack.TextRightMargin = 2;
            this.btnBack.Click += new System.EventHandler(this.btnBackNext_Click);
            // 
            // dailyT3
            // 
            this.dailyT3.ATL = 0F;
            this.dailyT3.BottomGradientColor = System.Drawing.Color.Empty;
            this.dailyT3.BottomGradientPercent = 0;
            this.dailyT3.CTL = 0F;
            this.dailyT3.Date = new System.DateTime(((long)(0)));
            this.dailyT3.Distance = null;
            this.dailyT3.DistanceActual = null;
            this.dailyT3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dailyT3.HeadingBackColor = System.Drawing.Color.Crimson;
            this.dailyT3.HeadingText = "12/1/2010";
            this.dailyT3.LeftGradientColor = System.Drawing.Color.Empty;
            this.dailyT3.LeftGradientPercent = 0;
            this.dailyT3.Location = new System.Drawing.Point(497, 3);
            this.dailyT3.Name = "dailyT3";
            this.dailyT3.Pace = null;
            this.dailyT3.PaceActual = null;
            this.dailyT3.RightGradientColor = System.Drawing.Color.Empty;
            this.dailyT3.RightGradientPercent = 0;
            this.dailyT3.Score = null;
            this.dailyT3.Size = new System.Drawing.Size(72, 194);
            this.dailyT3.TabIndex = 8;
            this.dailyT3.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dailyT3.Time = "30:00";
            this.dailyT3.TimeActual = null;
            this.dailyT3.TopGradientColor = System.Drawing.Color.Empty;
            this.dailyT3.TopGradientPercent = 0;
            this.dailyT3.WorkoutName = null;
            // 
            // dailyY1
            // 
            this.dailyY1.ATL = 0F;
            this.dailyY1.BottomGradientColor = System.Drawing.Color.Empty;
            this.dailyY1.BottomGradientPercent = 0;
            this.dailyY1.CTL = 0F;
            this.dailyY1.Date = new System.DateTime(((long)(0)));
            this.dailyY1.Distance = null;
            this.dailyY1.DistanceActual = null;
            this.dailyY1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dailyY1.HeadingBackColor = System.Drawing.Color.Crimson;
            this.dailyY1.HeadingText = "12/1/2010";
            this.dailyY1.LeftGradientColor = System.Drawing.Color.Empty;
            this.dailyY1.LeftGradientPercent = 0;
            this.dailyY1.Location = new System.Drawing.Point(185, 3);
            this.dailyY1.Name = "dailyY1";
            this.dailyY1.Pace = null;
            this.dailyY1.PaceActual = null;
            this.dailyY1.RightGradientColor = System.Drawing.Color.Empty;
            this.dailyY1.RightGradientPercent = 0;
            this.dailyY1.Score = null;
            this.dailyY1.Size = new System.Drawing.Size(72, 194);
            this.dailyY1.TabIndex = 15;
            this.dailyY1.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dailyY1.Time = null;
            this.dailyY1.TimeActual = null;
            this.dailyY1.TopGradientColor = System.Drawing.Color.Empty;
            this.dailyY1.TopGradientPercent = 0;
            this.dailyY1.WorkoutName = null;
            // 
            // dailyToday
            // 
            this.dailyToday.ATL = 0F;
            this.dailyToday.BottomGradientColor = System.Drawing.Color.Empty;
            this.dailyToday.BottomGradientPercent = 0;
            this.dailyToday.CTL = 0F;
            this.dailyToday.Date = new System.DateTime(((long)(0)));
            this.dailyToday.Distance = null;
            this.dailyToday.DistanceActual = null;
            this.dailyToday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dailyToday.HeadingBackColor = System.Drawing.Color.Crimson;
            this.dailyToday.HeadingText = "12/1/2010";
            this.dailyToday.LeftGradientColor = System.Drawing.Color.Empty;
            this.dailyToday.LeftGradientPercent = 0;
            this.dailyToday.Location = new System.Drawing.Point(263, 3);
            this.dailyToday.Name = "dailyToday";
            this.dailyToday.Pace = null;
            this.dailyToday.PaceActual = null;
            this.dailyToday.RightGradientColor = System.Drawing.Color.Empty;
            this.dailyToday.RightGradientPercent = 0;
            this.dailyToday.Score = null;
            this.dailyToday.Size = new System.Drawing.Size(72, 194);
            this.dailyToday.TabIndex = 16;
            this.dailyToday.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dailyToday.Time = null;
            this.dailyToday.TimeActual = null;
            this.dailyToday.TopGradientColor = System.Drawing.Color.Empty;
            this.dailyToday.TopGradientPercent = 0;
            this.dailyToday.WorkoutName = null;
            // 
            // dailyT1
            // 
            this.dailyT1.ATL = 0F;
            this.dailyT1.BottomGradientColor = System.Drawing.Color.Empty;
            this.dailyT1.BottomGradientPercent = 0;
            this.dailyT1.CTL = 0F;
            this.dailyT1.Date = new System.DateTime(((long)(0)));
            this.dailyT1.Distance = null;
            this.dailyT1.DistanceActual = null;
            this.dailyT1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dailyT1.HeadingBackColor = System.Drawing.Color.Crimson;
            this.dailyT1.HeadingText = "12/1/2010";
            this.dailyT1.LeftGradientColor = System.Drawing.Color.Empty;
            this.dailyT1.LeftGradientPercent = 0;
            this.dailyT1.Location = new System.Drawing.Point(341, 3);
            this.dailyT1.Name = "dailyT1";
            this.dailyT1.Pace = null;
            this.dailyT1.PaceActual = null;
            this.dailyT1.RightGradientColor = System.Drawing.Color.Empty;
            this.dailyT1.RightGradientPercent = 0;
            this.dailyT1.Score = null;
            this.dailyT1.Size = new System.Drawing.Size(72, 194);
            this.dailyT1.TabIndex = 17;
            this.dailyT1.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dailyT1.Time = null;
            this.dailyT1.TimeActual = null;
            this.dailyT1.TopGradientColor = System.Drawing.Color.Empty;
            this.dailyT1.TopGradientPercent = 0;
            this.dailyT1.WorkoutName = null;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNext.BackgroundImage")));
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNext.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnNext.CenterImage = null;
            this.btnNext.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNext.HyperlinkStyle = false;
            this.btnNext.ImageMargin = 2;
            this.btnNext.LeftImage = null;
            this.btnNext.Location = new System.Drawing.Point(654, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.PushStyle = true;
            this.btnNext.RightImage = null;
            this.btnNext.Size = new System.Drawing.Size(138, 194);
            this.btnNext.TabIndex = 0;
            this.btnNext.Tag = "1";
            this.btnNext.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnNext.TextLeftMargin = 2;
            this.btnNext.TextRightMargin = 2;
            this.btnNext.Click += new System.EventHandler(this.btnBackNext_Click);
            // 
            // WeekSummary
            // 
            this.WeekSummary.ATL = 0F;
            this.WeekSummary.BottomGradientColor = System.Drawing.Color.Empty;
            this.WeekSummary.BottomGradientPercent = 0;
            this.WeekSummary.CTL = 0F;
            this.WeekSummary.Date = new System.DateTime(((long)(0)));
            this.WeekSummary.Distance = null;
            this.WeekSummary.DistanceActual = null;
            this.WeekSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WeekSummary.HeadingBackColor = System.Drawing.Color.SlateGray;
            this.WeekSummary.HeadingText = "Summary:";
            this.WeekSummary.IsSummary = true;
            this.WeekSummary.LeftGradientColor = System.Drawing.Color.Empty;
            this.WeekSummary.LeftGradientPercent = 0;
            this.WeekSummary.Location = new System.Drawing.Point(575, 3);
            this.WeekSummary.Name = "WeekSummary";
            this.WeekSummary.Pace = null;
            this.WeekSummary.PaceActual = null;
            this.WeekSummary.RightGradientColor = System.Drawing.Color.Empty;
            this.WeekSummary.RightGradientPercent = 0;
            this.WeekSummary.Score = null;
            this.WeekSummary.ShowLegend = true;
            this.WeekSummary.Size = new System.Drawing.Size(73, 194);
            this.WeekSummary.TabIndex = 8;
            this.WeekSummary.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.WeekSummary.Time = null;
            this.WeekSummary.TimeActual = null;
            this.WeekSummary.TopGradientColor = System.Drawing.Color.Empty;
            this.WeekSummary.TopGradientPercent = 0;
            this.WeekSummary.WorkoutName = null;
            // 
            // tabDetails
            // 
            this.tabDetails.Controls.Add(this.grpGarminFitness);
            this.tabDetails.Controls.Add(this.btnLockWorkout);
            this.tabDetails.Controls.Add(this.txtWorkoutCategory);
            this.tabDetails.Controls.Add(this.lblCategory);
            this.tabDetails.Controls.Add(this.grpPhase);
            this.tabDetails.Controls.Add(this.grpPlan);
            this.tabDetails.Controls.Add(this.txtWorkoutNotes);
            this.tabDetails.Controls.Add(this.grpRepeat);
            this.tabDetails.Controls.Add(this.grpWorkout);
            this.tabDetails.Controls.Add(this.lblWorkoutTitle);
            this.tabDetails.Controls.Add(this.radTSS);
            this.tabDetails.Controls.Add(this.radTrimp);
            this.tabDetails.Controls.Add(this.lblPlanHelp);
            this.tabDetails.Controls.Add(this.lblNotes);
            this.tabDetails.Controls.Add(this.lblPhase);
            this.tabDetails.Controls.Add(this.lblPlanTitle);
            this.tabDetails.Controls.Add(this.txtGarminWorkout);
            this.tabDetails.Controls.Add(this.btnGarminWorkout);
            this.tabDetails.Controls.Add(this.txtWorkoutName);
            this.tabDetails.Controls.Add(this.txtPhaseName);
            this.tabDetails.Controls.Add(this.txtPlanName);
            this.tabDetails.Location = new System.Drawing.Point(4, 25);
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabDetails.Size = new System.Drawing.Size(795, 200);
            this.tabDetails.TabIndex = 3;
            this.tabDetails.Text = "Details";
            this.tabDetails.UseVisualStyleBackColor = true;
            // 
            // grpGarminFitness
            // 
            this.grpGarminFitness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grpGarminFitness.Controls.Add(this.txtPlanGarminAutoSched);
            this.grpGarminFitness.Controls.Add(this.chkGarminAutoSched);
            this.grpGarminFitness.Controls.Add(this.lblPlanDays);
            this.grpGarminFitness.Controls.Add(this.btnGarminFitness);
            this.grpGarminFitness.Location = new System.Drawing.Point(0, 139);
            this.grpGarminFitness.Name = "grpGarminFitness";
            this.grpGarminFitness.Size = new System.Drawing.Size(137, 60);
            this.grpGarminFitness.TabIndex = 34;
            this.grpGarminFitness.TabStop = false;
            this.grpGarminFitness.Text = "Garmin Fitness";
            // 
            // txtPlanGarminAutoSched
            // 
            this.txtPlanGarminAutoSched.AcceptsReturn = false;
            this.txtPlanGarminAutoSched.AcceptsTab = false;
            this.txtPlanGarminAutoSched.BackColor = System.Drawing.Color.White;
            this.txtPlanGarminAutoSched.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtPlanGarminAutoSched.ButtonImage = null;
            this.txtPlanGarminAutoSched.Location = new System.Drawing.Point(6, 35);
            this.txtPlanGarminAutoSched.MaxLength = 32767;
            this.txtPlanGarminAutoSched.Multiline = false;
            this.txtPlanGarminAutoSched.Name = "txtPlanGarminAutoSched";
            this.txtPlanGarminAutoSched.ReadOnly = false;
            this.txtPlanGarminAutoSched.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtPlanGarminAutoSched.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtPlanGarminAutoSched.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPlanGarminAutoSched.Size = new System.Drawing.Size(45, 19);
            this.txtPlanGarminAutoSched.TabIndex = 4;
            this.txtPlanGarminAutoSched.Tag = "Plan.AutoScheduleDays";
            this.txtPlanGarminAutoSched.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPlanGarminAutoSched.Leave += new System.EventHandler(this.txtPlanInfo_Leave);
            // 
            // chkGarminAutoSched
            // 
            this.chkGarminAutoSched.AutoSize = true;
            this.chkGarminAutoSched.Location = new System.Drawing.Point(6, 19);
            this.chkGarminAutoSched.Name = "chkGarminAutoSched";
            this.chkGarminAutoSched.Size = new System.Drawing.Size(96, 17);
            this.chkGarminAutoSched.TabIndex = 0;
            this.chkGarminAutoSched.Text = "Auto Schedule";
            this.chkGarminAutoSched.UseVisualStyleBackColor = true;
            this.chkGarminAutoSched.CheckedChanged += new System.EventHandler(this.chkGarminAutoSched_CheckedChanged);
            // 
            // lblPlanDays
            // 
            this.lblPlanDays.AutoSize = true;
            this.lblPlanDays.Location = new System.Drawing.Point(57, 38);
            this.lblPlanDays.Name = "lblPlanDays";
            this.lblPlanDays.Size = new System.Drawing.Size(29, 13);
            this.lblPlanDays.TabIndex = 25;
            this.lblPlanDays.Text = "days";
            // 
            // btnGarminFitness
            // 
            this.btnGarminFitness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGarminFitness.BackColor = System.Drawing.Color.Transparent;
            this.btnGarminFitness.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGarminFitness.BackgroundImage")));
            this.btnGarminFitness.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGarminFitness.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnGarminFitness.CenterImage = null;
            this.btnGarminFitness.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnGarminFitness.HyperlinkStyle = false;
            this.btnGarminFitness.ImageMargin = 2;
            this.btnGarminFitness.LeftImage = null;
            this.btnGarminFitness.Location = new System.Drawing.Point(110, 19);
            this.btnGarminFitness.Margin = new System.Windows.Forms.Padding(0);
            this.btnGarminFitness.Name = "btnGarminFitness";
            this.btnGarminFitness.PushStyle = true;
            this.btnGarminFitness.RightImage = null;
            this.btnGarminFitness.Size = new System.Drawing.Size(24, 24);
            this.btnGarminFitness.TabIndex = 34;
            this.btnGarminFitness.Tag = "";
            this.btnGarminFitness.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnGarminFitness.TextLeftMargin = 2;
            this.btnGarminFitness.TextRightMargin = 2;
            this.btnGarminFitness.Click += new System.EventHandler(this.btnGarminFitness_Click);
            // 
            // btnLockWorkout
            // 
            this.btnLockWorkout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLockWorkout.BackColor = System.Drawing.Color.Transparent;
            this.btnLockWorkout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLockWorkout.BackgroundImage")));
            this.btnLockWorkout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnLockWorkout.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnLockWorkout.CenterImage = null;
            this.btnLockWorkout.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnLockWorkout.HyperlinkStyle = false;
            this.btnLockWorkout.ImageMargin = 2;
            this.btnLockWorkout.LeftImage = null;
            this.btnLockWorkout.Location = new System.Drawing.Point(306, 17);
            this.btnLockWorkout.Margin = new System.Windows.Forms.Padding(0);
            this.btnLockWorkout.Name = "btnLockWorkout";
            this.btnLockWorkout.PushStyle = true;
            this.btnLockWorkout.RightImage = null;
            this.btnLockWorkout.Size = new System.Drawing.Size(20, 20);
            this.btnLockWorkout.TabIndex = 34;
            this.btnLockWorkout.Tag = "";
            this.btnLockWorkout.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnLockWorkout.TextLeftMargin = 2;
            this.btnLockWorkout.TextRightMargin = 2;
            this.btnLockWorkout.Click += new System.EventHandler(this.btnLockWorkout_Click);
            // 
            // txtWorkoutCategory
            // 
            this.txtWorkoutCategory.AcceptsReturn = false;
            this.txtWorkoutCategory.AcceptsTab = false;
            this.txtWorkoutCategory.BackColor = System.Drawing.Color.White;
            this.txtWorkoutCategory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtWorkoutCategory.ButtonImage = ((System.Drawing.Image)(resources.GetObject("txtWorkoutCategory.ButtonImage")));
            this.txtWorkoutCategory.Enabled = false;
            this.txtWorkoutCategory.Location = new System.Drawing.Point(494, 37);
            this.txtWorkoutCategory.MaxLength = 32767;
            this.txtWorkoutCategory.Multiline = false;
            this.txtWorkoutCategory.Name = "txtWorkoutCategory";
            this.txtWorkoutCategory.ReadOnly = false;
            this.txtWorkoutCategory.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtWorkoutCategory.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtWorkoutCategory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWorkoutCategory.Size = new System.Drawing.Size(142, 19);
            this.txtWorkoutCategory.TabIndex = 4;
            this.txtWorkoutCategory.Tag = "Workout";
            this.txtWorkoutCategory.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtWorkoutCategory.ButtonClick += new System.EventHandler(this.txtCategory_Click);
            this.txtWorkoutCategory.Leave += new System.EventHandler(this.txtWorkoutInfo_Leave);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(491, 21);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(52, 13);
            this.lblCategory.TabIndex = 8;
            this.lblCategory.Text = "Category:";
            // 
            // grpPhase
            // 
            this.grpPhase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grpPhase.Controls.Add(this.txtPhaseEnd);
            this.grpPhase.Controls.Add(this.txtPhaseStart);
            this.grpPhase.Controls.Add(this.lblPhaseEnd);
            this.grpPhase.Controls.Add(this.lblPhaseDuration);
            this.grpPhase.Controls.Add(this.lblPhaseStart);
            this.grpPhase.Location = new System.Drawing.Point(143, 62);
            this.grpPhase.Name = "grpPhase";
            this.grpPhase.Size = new System.Drawing.Size(99, 137);
            this.grpPhase.TabIndex = 35;
            this.grpPhase.TabStop = false;
            this.grpPhase.Text = "Phase";
            // 
            // txtPhaseEnd
            // 
            this.txtPhaseEnd.AcceptsReturn = false;
            this.txtPhaseEnd.AcceptsTab = false;
            this.txtPhaseEnd.BackColor = System.Drawing.Color.White;
            this.txtPhaseEnd.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtPhaseEnd.ButtonImage = ((System.Drawing.Image)(resources.GetObject("txtPhaseEnd.ButtonImage")));
            this.txtPhaseEnd.Enabled = false;
            this.txtPhaseEnd.Location = new System.Drawing.Point(6, 70);
            this.txtPhaseEnd.MaxLength = 32767;
            this.txtPhaseEnd.Multiline = false;
            this.txtPhaseEnd.Name = "txtPhaseEnd";
            this.txtPhaseEnd.ReadOnly = false;
            this.txtPhaseEnd.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtPhaseEnd.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtPhaseEnd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPhaseEnd.Size = new System.Drawing.Size(87, 19);
            this.txtPhaseEnd.TabIndex = 1;
            this.txtPhaseEnd.Tag = "End";
            this.txtPhaseEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPhaseEnd.ButtonClick += new System.EventHandler(this.btnCalendar_Click);
            this.txtPhaseEnd.Leave += new System.EventHandler(this.txtPhaseInfo_Leave);
            // 
            // txtPhaseStart
            // 
            this.txtPhaseStart.AcceptsReturn = false;
            this.txtPhaseStart.AcceptsTab = false;
            this.txtPhaseStart.BackColor = System.Drawing.Color.White;
            this.txtPhaseStart.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtPhaseStart.ButtonImage = ((System.Drawing.Image)(resources.GetObject("txtPhaseStart.ButtonImage")));
            this.txtPhaseStart.Enabled = false;
            this.txtPhaseStart.Location = new System.Drawing.Point(6, 32);
            this.txtPhaseStart.MaxLength = 32767;
            this.txtPhaseStart.Multiline = false;
            this.txtPhaseStart.Name = "txtPhaseStart";
            this.txtPhaseStart.ReadOnly = false;
            this.txtPhaseStart.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtPhaseStart.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtPhaseStart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPhaseStart.Size = new System.Drawing.Size(87, 19);
            this.txtPhaseStart.TabIndex = 0;
            this.txtPhaseStart.Tag = "Start";
            this.txtPhaseStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPhaseStart.ButtonClick += new System.EventHandler(this.btnCalendar_Click);
            this.txtPhaseStart.Leave += new System.EventHandler(this.txtPhaseInfo_Leave);
            // 
            // lblPhaseEnd
            // 
            this.lblPhaseEnd.Location = new System.Drawing.Point(6, 54);
            this.lblPhaseEnd.Name = "lblPhaseEnd";
            this.lblPhaseEnd.Size = new System.Drawing.Size(58, 13);
            this.lblPhaseEnd.TabIndex = 24;
            this.lblPhaseEnd.Text = "End";
            // 
            // lblPhaseDuration
            // 
            this.lblPhaseDuration.AutoSize = true;
            this.lblPhaseDuration.BackColor = System.Drawing.Color.Transparent;
            this.lblPhaseDuration.Location = new System.Drawing.Point(6, 92);
            this.lblPhaseDuration.Name = "lblPhaseDuration";
            this.lblPhaseDuration.Size = new System.Drawing.Size(53, 26);
            this.lblPhaseDuration.TabIndex = 25;
            this.lblPhaseDuration.Text = "Duration: \r\n6 weeks";
            // 
            // lblPhaseStart
            // 
            this.lblPhaseStart.Location = new System.Drawing.Point(6, 16);
            this.lblPhaseStart.Name = "lblPhaseStart";
            this.lblPhaseStart.Size = new System.Drawing.Size(58, 13);
            this.lblPhaseStart.TabIndex = 24;
            this.lblPhaseStart.Text = "Start";
            // 
            // grpPlan
            // 
            this.grpPlan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grpPlan.Controls.Add(this.lblPlanEnd);
            this.grpPlan.Controls.Add(this.lblPlanStart);
            this.grpPlan.Controls.Add(this.lblActivityCount);
            this.grpPlan.Location = new System.Drawing.Point(0, 62);
            this.grpPlan.Name = "grpPlan";
            this.grpPlan.Size = new System.Drawing.Size(137, 74);
            this.grpPlan.TabIndex = 34;
            this.grpPlan.TabStop = false;
            this.grpPlan.Text = "Plan";
            // 
            // lblPlanEnd
            // 
            this.lblPlanEnd.Location = new System.Drawing.Point(3, 42);
            this.lblPlanEnd.Name = "lblPlanEnd";
            this.lblPlanEnd.Size = new System.Drawing.Size(108, 13);
            this.lblPlanEnd.TabIndex = 25;
            this.lblPlanEnd.Text = "End: Dec. 31, 2011";
            // 
            // lblPlanStart
            // 
            this.lblPlanStart.Location = new System.Drawing.Point(3, 29);
            this.lblPlanStart.Name = "lblPlanStart";
            this.lblPlanStart.Size = new System.Drawing.Size(108, 13);
            this.lblPlanStart.TabIndex = 25;
            this.lblPlanStart.Text = "Start: Jan 1, 2011";
            // 
            // lblActivityCount
            // 
            this.lblActivityCount.Location = new System.Drawing.Point(3, 16);
            this.lblActivityCount.Name = "lblActivityCount";
            this.lblActivityCount.Size = new System.Drawing.Size(130, 13);
            this.lblActivityCount.TabIndex = 25;
            this.lblActivityCount.Text = "Activities: 15 (52 weeks)";
            // 
            // txtWorkoutNotes
            // 
            this.txtWorkoutNotes.AcceptsReturn = false;
            this.txtWorkoutNotes.AcceptsTab = false;
            this.txtWorkoutNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWorkoutNotes.BackColor = System.Drawing.Color.White;
            this.txtWorkoutNotes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtWorkoutNotes.ButtonImage = null;
            this.txtWorkoutNotes.Enabled = false;
            this.txtWorkoutNotes.Location = new System.Drawing.Point(620, 74);
            this.txtWorkoutNotes.MaxLength = 32767;
            this.txtWorkoutNotes.Multiline = true;
            this.txtWorkoutNotes.Name = "txtWorkoutNotes";
            this.txtWorkoutNotes.ReadOnly = false;
            this.txtWorkoutNotes.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtWorkoutNotes.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtWorkoutNotes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWorkoutNotes.Size = new System.Drawing.Size(175, 125);
            this.txtWorkoutNotes.TabIndex = 6;
            this.txtWorkoutNotes.Tag = "Notes";
            this.txtWorkoutNotes.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // grpRepeat
            // 
            this.grpRepeat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grpRepeat.Controls.Add(this.btnParent);
            this.grpRepeat.Controls.Add(this.btnLink);
            this.grpRepeat.Controls.Add(this.chkRepeat);
            this.grpRepeat.Controls.Add(this.txtWorkoutRamp);
            this.grpRepeat.Controls.Add(this.lblWorkoutEnd);
            this.grpRepeat.Controls.Add(this.txtWorkoutEnd);
            this.grpRepeat.Controls.Add(this.lblWorkoutRepeat);
            this.grpRepeat.Controls.Add(this.txtWorkoutPeriod);
            this.grpRepeat.Controls.Add(this.lblWorkoutDays);
            this.grpRepeat.Controls.Add(this.lblWorkoutRamp);
            this.grpRepeat.Location = new System.Drawing.Point(457, 62);
            this.grpRepeat.Name = "grpRepeat";
            this.grpRepeat.Size = new System.Drawing.Size(157, 137);
            this.grpRepeat.TabIndex = 32;
            this.grpRepeat.TabStop = false;
            this.grpRepeat.Text = "Repeat";
            // 
            // btnParent
            // 
            this.btnParent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnParent.BackColor = System.Drawing.Color.Transparent;
            this.btnParent.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnParent.BackgroundImage")));
            this.btnParent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnParent.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnParent.CenterImage = null;
            this.btnParent.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnParent.HyperlinkStyle = false;
            this.btnParent.ImageMargin = 2;
            this.btnParent.LeftImage = null;
            this.btnParent.Location = new System.Drawing.Point(103, 12);
            this.btnParent.Margin = new System.Windows.Forms.Padding(0);
            this.btnParent.Name = "btnParent";
            this.btnParent.PushStyle = true;
            this.btnParent.RightImage = null;
            this.btnParent.Size = new System.Drawing.Size(24, 24);
            this.btnParent.TabIndex = 29;
            this.btnParent.Tag = "";
            this.btnParent.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnParent.TextLeftMargin = 2;
            this.btnParent.TextRightMargin = 2;
            this.btnParent.Visible = false;
            this.btnParent.Click += new System.EventHandler(this.btnParent_Click);
            // 
            // btnLink
            // 
            this.btnLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLink.BackColor = System.Drawing.Color.Transparent;
            this.btnLink.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLink.BackgroundImage")));
            this.btnLink.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnLink.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnLink.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.btnLink.CenterImage = null;
            this.btnLink.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnLink.HyperlinkStyle = false;
            this.btnLink.ImageMargin = 2;
            this.btnLink.LeftImage = null;
            this.btnLink.Location = new System.Drawing.Point(127, 12);
            this.btnLink.Margin = new System.Windows.Forms.Padding(0);
            this.btnLink.Name = "btnLink";
            this.btnLink.PushStyle = true;
            this.btnLink.RightImage = null;
            this.btnLink.Size = new System.Drawing.Size(24, 24);
            this.btnLink.TabIndex = 4;
            this.btnLink.Tag = "";
            this.btnLink.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnLink.TextLeftMargin = 2;
            this.btnLink.TextRightMargin = 2;
            this.btnLink.Visible = false;
            this.btnLink.Click += new System.EventHandler(this.btnLink_Click);
            // 
            // chkRepeat
            // 
            this.chkRepeat.AutoSize = true;
            this.chkRepeat.Location = new System.Drawing.Point(6, 19);
            this.chkRepeat.Name = "chkRepeat";
            this.chkRepeat.Size = new System.Drawing.Size(61, 17);
            this.chkRepeat.TabIndex = 0;
            this.chkRepeat.Text = "Repeat";
            this.chkRepeat.UseVisualStyleBackColor = true;
            this.chkRepeat.CheckedChanged += new System.EventHandler(this.chkRepeat_CheckedChanged);
            // 
            // txtWorkoutRamp
            // 
            this.txtWorkoutRamp.AcceptsReturn = false;
            this.txtWorkoutRamp.AcceptsTab = false;
            this.txtWorkoutRamp.BackColor = System.Drawing.Color.White;
            this.txtWorkoutRamp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtWorkoutRamp.ButtonImage = null;
            this.txtWorkoutRamp.Enabled = false;
            this.txtWorkoutRamp.Location = new System.Drawing.Point(70, 92);
            this.txtWorkoutRamp.MaxLength = 32767;
            this.txtWorkoutRamp.Multiline = false;
            this.txtWorkoutRamp.Name = "txtWorkoutRamp";
            this.txtWorkoutRamp.ReadOnly = false;
            this.txtWorkoutRamp.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtWorkoutRamp.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtWorkoutRamp.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWorkoutRamp.Size = new System.Drawing.Size(29, 19);
            this.txtWorkoutRamp.TabIndex = 3;
            this.txtWorkoutRamp.Tag = "Ramp";
            this.txtWorkoutRamp.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtWorkoutRamp.Leave += new System.EventHandler(this.txtWorkoutInfo_Leave);
            // 
            // lblWorkoutEnd
            // 
            this.lblWorkoutEnd.Location = new System.Drawing.Point(6, 48);
            this.lblWorkoutEnd.Name = "lblWorkoutEnd";
            this.lblWorkoutEnd.Size = new System.Drawing.Size(58, 13);
            this.lblWorkoutEnd.TabIndex = 24;
            this.lblWorkoutEnd.Text = "End Date";
            this.lblWorkoutEnd.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtWorkoutEnd
            // 
            this.txtWorkoutEnd.AcceptsReturn = false;
            this.txtWorkoutEnd.AcceptsTab = false;
            this.txtWorkoutEnd.BackColor = System.Drawing.Color.White;
            this.txtWorkoutEnd.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtWorkoutEnd.ButtonImage = ((System.Drawing.Image)(resources.GetObject("txtWorkoutEnd.ButtonImage")));
            this.txtWorkoutEnd.Enabled = false;
            this.txtWorkoutEnd.Location = new System.Drawing.Point(70, 42);
            this.txtWorkoutEnd.MaxLength = 32767;
            this.txtWorkoutEnd.Multiline = false;
            this.txtWorkoutEnd.Name = "txtWorkoutEnd";
            this.txtWorkoutEnd.ReadOnly = false;
            this.txtWorkoutEnd.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtWorkoutEnd.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtWorkoutEnd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWorkoutEnd.Size = new System.Drawing.Size(81, 19);
            this.txtWorkoutEnd.TabIndex = 1;
            this.txtWorkoutEnd.Tag = "End";
            this.txtWorkoutEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtWorkoutEnd.ButtonClick += new System.EventHandler(this.btnCalendar_Click);
            this.txtWorkoutEnd.Leave += new System.EventHandler(this.txtWorkoutInfo_Leave);
            // 
            // lblWorkoutRepeat
            // 
            this.lblWorkoutRepeat.Location = new System.Drawing.Point(3, 73);
            this.lblWorkoutRepeat.Name = "lblWorkoutRepeat";
            this.lblWorkoutRepeat.Size = new System.Drawing.Size(61, 13);
            this.lblWorkoutRepeat.TabIndex = 23;
            this.lblWorkoutRepeat.Text = "Period";
            this.lblWorkoutRepeat.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtWorkoutPeriod
            // 
            this.txtWorkoutPeriod.AcceptsReturn = false;
            this.txtWorkoutPeriod.AcceptsTab = false;
            this.txtWorkoutPeriod.BackColor = System.Drawing.Color.White;
            this.txtWorkoutPeriod.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtWorkoutPeriod.ButtonImage = null;
            this.txtWorkoutPeriod.Enabled = false;
            this.txtWorkoutPeriod.Location = new System.Drawing.Point(70, 67);
            this.txtWorkoutPeriod.MaxLength = 32767;
            this.txtWorkoutPeriod.Multiline = false;
            this.txtWorkoutPeriod.Name = "txtWorkoutPeriod";
            this.txtWorkoutPeriod.ReadOnly = false;
            this.txtWorkoutPeriod.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtWorkoutPeriod.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtWorkoutPeriod.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWorkoutPeriod.Size = new System.Drawing.Size(50, 19);
            this.txtWorkoutPeriod.TabIndex = 2;
            this.txtWorkoutPeriod.Tag = "Period";
            this.txtWorkoutPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtWorkoutPeriod.Leave += new System.EventHandler(this.txtWorkoutInfo_Leave);
            // 
            // lblWorkoutDays
            // 
            this.lblWorkoutDays.AutoSize = true;
            this.lblWorkoutDays.Location = new System.Drawing.Point(120, 73);
            this.lblWorkoutDays.Name = "lblWorkoutDays";
            this.lblWorkoutDays.Size = new System.Drawing.Size(29, 13);
            this.lblWorkoutDays.TabIndex = 28;
            this.lblWorkoutDays.Text = "days";
            // 
            // lblWorkoutRamp
            // 
            this.lblWorkoutRamp.Location = new System.Drawing.Point(6, 98);
            this.lblWorkoutRamp.Name = "lblWorkoutRamp";
            this.lblWorkoutRamp.Size = new System.Drawing.Size(58, 13);
            this.lblWorkoutRamp.TabIndex = 26;
            this.lblWorkoutRamp.Text = "Ramp (%)";
            this.lblWorkoutRamp.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // grpWorkout
            // 
            this.grpWorkout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grpWorkout.Controls.Add(this.btnImage);
            this.grpWorkout.Controls.Add(this.radPace);
            this.grpWorkout.Controls.Add(this.radDistance);
            this.grpWorkout.Controls.Add(this.radTime);
            this.grpWorkout.Controls.Add(this.txtWorkoutTime);
            this.grpWorkout.Controls.Add(this.lblWorkoutMi);
            this.grpWorkout.Controls.Add(this.lblWorkoutMph);
            this.grpWorkout.Controls.Add(this.txtWorkoutSpeed);
            this.grpWorkout.Controls.Add(this.txtWorkoutPace);
            this.grpWorkout.Controls.Add(this.txtWorkoutDistance);
            this.grpWorkout.Controls.Add(this.txtWorkoutScore);
            this.grpWorkout.Controls.Add(this.lblWorkoutScore);
            this.grpWorkout.Location = new System.Drawing.Point(248, 62);
            this.grpWorkout.Name = "grpWorkout";
            this.grpWorkout.Size = new System.Drawing.Size(203, 137);
            this.grpWorkout.TabIndex = 31;
            this.grpWorkout.TabStop = false;
            this.grpWorkout.Text = "Workout";
            // 
            // btnImage
            // 
            this.btnImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImage.BackColor = System.Drawing.Color.Transparent;
            this.btnImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImage.BackgroundImage")));
            this.btnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnImage.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnImage.CenterImage = null;
            this.btnImage.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnImage.HyperlinkStyle = false;
            this.btnImage.ImageMargin = 2;
            this.btnImage.LeftImage = null;
            this.btnImage.Location = new System.Drawing.Point(168, 12);
            this.btnImage.Margin = new System.Windows.Forms.Padding(0);
            this.btnImage.Name = "btnImage";
            this.btnImage.PushStyle = true;
            this.btnImage.RightImage = null;
            this.btnImage.Size = new System.Drawing.Size(24, 24);
            this.btnImage.TabIndex = 33;
            this.btnImage.Tag = "";
            this.btnImage.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnImage.TextLeftMargin = 2;
            this.btnImage.TextRightMargin = 2;
            this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
            // 
            // radPace
            // 
            this.radPace.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radPace.Location = new System.Drawing.Point(4, 67);
            this.radPace.Name = "radPace";
            this.radPace.Size = new System.Drawing.Size(87, 39);
            this.radPace.TabIndex = 30;
            this.radPace.Text = "Speed\r\nPace";
            this.radPace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radPace.UseVisualStyleBackColor = true;
            this.radPace.CheckedChanged += new System.EventHandler(this.radTimeDistancePace_CheckedChanged);
            // 
            // radDistance
            // 
            this.radDistance.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radDistance.Location = new System.Drawing.Point(4, 44);
            this.radDistance.Name = "radDistance";
            this.radDistance.Size = new System.Drawing.Size(87, 17);
            this.radDistance.TabIndex = 29;
            this.radDistance.Text = "Distance";
            this.radDistance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radDistance.UseVisualStyleBackColor = true;
            this.radDistance.CheckedChanged += new System.EventHandler(this.radTimeDistancePace_CheckedChanged);
            // 
            // radTime
            // 
            this.radTime.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radTime.Location = new System.Drawing.Point(4, 21);
            this.radTime.Name = "radTime";
            this.radTime.Size = new System.Drawing.Size(87, 17);
            this.radTime.TabIndex = 28;
            this.radTime.Text = "Total Time";
            this.radTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.radTime.UseVisualStyleBackColor = true;
            this.radTime.CheckedChanged += new System.EventHandler(this.radTimeDistancePace_CheckedChanged);
            // 
            // txtWorkoutTime
            // 
            this.txtWorkoutTime.AcceptsReturn = false;
            this.txtWorkoutTime.AcceptsTab = false;
            this.txtWorkoutTime.BackColor = System.Drawing.Color.White;
            this.txtWorkoutTime.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtWorkoutTime.ButtonImage = null;
            this.txtWorkoutTime.Enabled = false;
            this.txtWorkoutTime.Location = new System.Drawing.Point(97, 17);
            this.txtWorkoutTime.MaxLength = 32767;
            this.txtWorkoutTime.Multiline = false;
            this.txtWorkoutTime.Name = "txtWorkoutTime";
            this.txtWorkoutTime.ReadOnly = false;
            this.txtWorkoutTime.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtWorkoutTime.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtWorkoutTime.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWorkoutTime.Size = new System.Drawing.Size(62, 19);
            this.txtWorkoutTime.TabIndex = 0;
            this.txtWorkoutTime.Tag = "Time";
            this.txtWorkoutTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtWorkoutTime.Leave += new System.EventHandler(this.txtWorkoutInfo_Leave);
            // 
            // lblWorkoutMi
            // 
            this.lblWorkoutMi.AutoSize = true;
            this.lblWorkoutMi.Location = new System.Drawing.Point(165, 46);
            this.lblWorkoutMi.Name = "lblWorkoutMi";
            this.lblWorkoutMi.Size = new System.Drawing.Size(20, 13);
            this.lblWorkoutMi.TabIndex = 32;
            this.lblWorkoutMi.Text = "mi.";
            // 
            // lblWorkoutMph
            // 
            this.lblWorkoutMph.AutoSize = true;
            this.lblWorkoutMph.Location = new System.Drawing.Point(165, 71);
            this.lblWorkoutMph.Name = "lblWorkoutMph";
            this.lblWorkoutMph.Size = new System.Drawing.Size(27, 13);
            this.lblWorkoutMph.TabIndex = 31;
            this.lblWorkoutMph.Text = "mph";
            // 
            // txtWorkoutSpeed
            // 
            this.txtWorkoutSpeed.AcceptsReturn = false;
            this.txtWorkoutSpeed.AcceptsTab = false;
            this.txtWorkoutSpeed.BackColor = System.Drawing.Color.White;
            this.txtWorkoutSpeed.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtWorkoutSpeed.ButtonImage = null;
            this.txtWorkoutSpeed.Enabled = false;
            this.txtWorkoutSpeed.Location = new System.Drawing.Point(97, 67);
            this.txtWorkoutSpeed.MaxLength = 32767;
            this.txtWorkoutSpeed.Multiline = false;
            this.txtWorkoutSpeed.Name = "txtWorkoutSpeed";
            this.txtWorkoutSpeed.ReadOnly = true;
            this.txtWorkoutSpeed.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtWorkoutSpeed.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtWorkoutSpeed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWorkoutSpeed.Size = new System.Drawing.Size(62, 19);
            this.txtWorkoutSpeed.TabIndex = 2;
            this.txtWorkoutSpeed.Tag = "Speed";
            this.txtWorkoutSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtWorkoutSpeed.Leave += new System.EventHandler(this.txtWorkoutInfo_Leave);
            // 
            // txtWorkoutPace
            // 
            this.txtWorkoutPace.AcceptsReturn = false;
            this.txtWorkoutPace.AcceptsTab = false;
            this.txtWorkoutPace.BackColor = System.Drawing.Color.White;
            this.txtWorkoutPace.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtWorkoutPace.ButtonImage = null;
            this.txtWorkoutPace.Enabled = false;
            this.txtWorkoutPace.Location = new System.Drawing.Point(97, 87);
            this.txtWorkoutPace.MaxLength = 32767;
            this.txtWorkoutPace.Multiline = false;
            this.txtWorkoutPace.Name = "txtWorkoutPace";
            this.txtWorkoutPace.ReadOnly = true;
            this.txtWorkoutPace.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtWorkoutPace.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtWorkoutPace.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWorkoutPace.Size = new System.Drawing.Size(62, 19);
            this.txtWorkoutPace.TabIndex = 3;
            this.txtWorkoutPace.Tag = "Pace";
            this.txtWorkoutPace.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtWorkoutPace.Leave += new System.EventHandler(this.txtWorkoutInfo_Leave);
            // 
            // txtWorkoutDistance
            // 
            this.txtWorkoutDistance.AcceptsReturn = false;
            this.txtWorkoutDistance.AcceptsTab = false;
            this.txtWorkoutDistance.BackColor = System.Drawing.Color.White;
            this.txtWorkoutDistance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtWorkoutDistance.ButtonImage = null;
            this.txtWorkoutDistance.Enabled = false;
            this.txtWorkoutDistance.Location = new System.Drawing.Point(97, 42);
            this.txtWorkoutDistance.MaxLength = 32767;
            this.txtWorkoutDistance.Multiline = false;
            this.txtWorkoutDistance.Name = "txtWorkoutDistance";
            this.txtWorkoutDistance.ReadOnly = false;
            this.txtWorkoutDistance.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtWorkoutDistance.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtWorkoutDistance.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWorkoutDistance.Size = new System.Drawing.Size(62, 19);
            this.txtWorkoutDistance.TabIndex = 1;
            this.txtWorkoutDistance.Tag = "Distance";
            this.txtWorkoutDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtWorkoutDistance.Leave += new System.EventHandler(this.txtWorkoutInfo_Leave);
            // 
            // txtWorkoutScore
            // 
            this.txtWorkoutScore.AcceptsReturn = false;
            this.txtWorkoutScore.AcceptsTab = false;
            this.txtWorkoutScore.BackColor = System.Drawing.Color.White;
            this.txtWorkoutScore.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtWorkoutScore.ButtonImage = null;
            this.txtWorkoutScore.Enabled = false;
            this.txtWorkoutScore.Location = new System.Drawing.Point(97, 112);
            this.txtWorkoutScore.MaxLength = 32767;
            this.txtWorkoutScore.Multiline = false;
            this.txtWorkoutScore.Name = "txtWorkoutScore";
            this.txtWorkoutScore.ReadOnly = false;
            this.txtWorkoutScore.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtWorkoutScore.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtWorkoutScore.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWorkoutScore.Size = new System.Drawing.Size(62, 19);
            this.txtWorkoutScore.TabIndex = 4;
            this.txtWorkoutScore.Tag = "Score";
            this.txtWorkoutScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtWorkoutScore.Leave += new System.EventHandler(this.txtWorkoutInfo_Leave);
            // 
            // lblWorkoutScore
            // 
            this.lblWorkoutScore.Location = new System.Drawing.Point(12, 115);
            this.lblWorkoutScore.Name = "lblWorkoutScore";
            this.lblWorkoutScore.Size = new System.Drawing.Size(79, 13);
            this.lblWorkoutScore.TabIndex = 25;
            this.lblWorkoutScore.Text = "Score";
            this.lblWorkoutScore.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblWorkoutTitle
            // 
            this.lblWorkoutTitle.AutoSize = true;
            this.lblWorkoutTitle.Location = new System.Drawing.Point(329, 21);
            this.lblWorkoutTitle.Name = "lblWorkoutTitle";
            this.lblWorkoutTitle.Size = new System.Drawing.Size(110, 13);
            this.lblWorkoutTitle.TabIndex = 17;
            this.lblWorkoutTitle.Text = "Workout: Jan 1, 2011";
            // 
            // radTSS
            // 
            this.radTSS.AutoSize = true;
            this.radTSS.Location = new System.Drawing.Point(743, 17);
            this.radTSS.Name = "radTSS";
            this.radTSS.Size = new System.Drawing.Size(46, 17);
            this.radTSS.TabIndex = 14;
            this.radTSS.Text = "TSS";
            this.radTSS.UseVisualStyleBackColor = true;
            this.radTSS.CheckedChanged += new System.EventHandler(this.radScore_CheckedChanged);
            // 
            // radTrimp
            // 
            this.radTrimp.AutoSize = true;
            this.radTrimp.Checked = true;
            this.radTrimp.Location = new System.Drawing.Point(686, 17);
            this.radTrimp.Name = "radTrimp";
            this.radTrimp.Size = new System.Drawing.Size(51, 17);
            this.radTrimp.TabIndex = 14;
            this.radTrimp.TabStop = true;
            this.radTrimp.Text = "Trimp";
            this.radTrimp.UseVisualStyleBackColor = true;
            this.radTrimp.CheckedChanged += new System.EventHandler(this.radScore_CheckedChanged);
            // 
            // lblPlanHelp
            // 
            this.lblPlanHelp.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPlanHelp.Location = new System.Drawing.Point(3, 3);
            this.lblPlanHelp.Name = "lblPlanHelp";
            this.lblPlanHelp.Size = new System.Drawing.Size(789, 19);
            this.lblPlanHelp.TabIndex = 0;
            this.lblPlanHelp.Text = "A Fit Plan is a collection of Phases, and Phases contain Workouts.  Fit Plans can" +
                " be saved to a file for later or shared with others.";
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(620, 62);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(38, 13);
            this.lblNotes.TabIndex = 8;
            this.lblNotes.Text = "Notes:";
            // 
            // lblPhase
            // 
            this.lblPhase.AutoSize = true;
            this.lblPhase.Location = new System.Drawing.Point(195, 21);
            this.lblPhase.Name = "lblPhase";
            this.lblPhase.Size = new System.Drawing.Size(37, 13);
            this.lblPhase.TabIndex = 8;
            this.lblPhase.Text = "Phase";
            // 
            // lblPlanTitle
            // 
            this.lblPlanTitle.AutoSize = true;
            this.lblPlanTitle.Location = new System.Drawing.Point(0, 21);
            this.lblPlanTitle.Name = "lblPlanTitle";
            this.lblPlanTitle.Size = new System.Drawing.Size(167, 13);
            this.lblPlanTitle.TabIndex = 1;
            this.lblPlanTitle.Text = "Plan: Jan. 1, 2011 - Dec 31, 2011";
            // 
            // txtGarminWorkout
            // 
            this.txtGarminWorkout.AcceptsReturn = false;
            this.txtGarminWorkout.AcceptsTab = false;
            this.txtGarminWorkout.AllowDrop = true;
            this.txtGarminWorkout.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtGarminWorkout.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtGarminWorkout.ButtonImage = null;
            this.txtGarminWorkout.Enabled = false;
            this.txtGarminWorkout.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.txtGarminWorkout.Location = new System.Drawing.Point(669, 37);
            this.txtGarminWorkout.MaxLength = 32767;
            this.txtGarminWorkout.Multiline = false;
            this.txtGarminWorkout.Name = "txtGarminWorkout";
            this.txtGarminWorkout.ReadOnly = false;
            this.txtGarminWorkout.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtGarminWorkout.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtGarminWorkout.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtGarminWorkout.Size = new System.Drawing.Size(120, 19);
            this.txtGarminWorkout.TabIndex = 4;
            this.txtGarminWorkout.Tag = "Workout";
            this.txtGarminWorkout.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtGarminWorkout.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtGarminWorkout_DragDrop);
            this.txtGarminWorkout.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtGarminWorkout_DragEnter);
            // 
            // btnGarminWorkout
            // 
            this.btnGarminWorkout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGarminWorkout.BackColor = System.Drawing.Color.Transparent;
            this.btnGarminWorkout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGarminWorkout.BackgroundImage")));
            this.btnGarminWorkout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnGarminWorkout.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnGarminWorkout.CenterImage = null;
            this.btnGarminWorkout.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnGarminWorkout.Enabled = false;
            this.btnGarminWorkout.HyperlinkStyle = false;
            this.btnGarminWorkout.ImageMargin = 2;
            this.btnGarminWorkout.LeftImage = null;
            this.btnGarminWorkout.Location = new System.Drawing.Point(643, 34);
            this.btnGarminWorkout.Margin = new System.Windows.Forms.Padding(0);
            this.btnGarminWorkout.Name = "btnGarminWorkout";
            this.btnGarminWorkout.PushStyle = true;
            this.btnGarminWorkout.RightImage = null;
            this.btnGarminWorkout.Size = new System.Drawing.Size(24, 24);
            this.btnGarminWorkout.TabIndex = 34;
            this.btnGarminWorkout.Tag = "";
            this.btnGarminWorkout.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnGarminWorkout.TextLeftMargin = 2;
            this.btnGarminWorkout.TextRightMargin = 2;
            // 
            // txtWorkoutName
            // 
            this.txtWorkoutName.AcceptsReturn = false;
            this.txtWorkoutName.AcceptsTab = false;
            this.txtWorkoutName.BackColor = System.Drawing.Color.White;
            this.txtWorkoutName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtWorkoutName.ButtonImage = ((System.Drawing.Image)(resources.GetObject("txtWorkoutName.ButtonImage")));
            this.txtWorkoutName.Enabled = false;
            this.txtWorkoutName.Location = new System.Drawing.Point(306, 37);
            this.txtWorkoutName.MaxLength = 32767;
            this.txtWorkoutName.Multiline = false;
            this.txtWorkoutName.Name = "txtWorkoutName";
            this.txtWorkoutName.ReadOnly = false;
            this.txtWorkoutName.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtWorkoutName.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtWorkoutName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtWorkoutName.Size = new System.Drawing.Size(182, 19);
            this.txtWorkoutName.TabIndex = 4;
            this.txtWorkoutName.Tag = "Workout.Name";
            this.txtWorkoutName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtWorkoutName.ButtonClick += new System.EventHandler(this.txtCalendar_Click);
            this.txtWorkoutName.Leave += new System.EventHandler(this.txtWorkoutInfo_Leave);
            // 
            // txtPhaseName
            // 
            this.txtPhaseName.AcceptsReturn = false;
            this.txtPhaseName.AcceptsTab = false;
            this.txtPhaseName.BackColor = System.Drawing.Color.White;
            this.txtPhaseName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtPhaseName.ButtonImage = ((System.Drawing.Image)(resources.GetObject("txtPhaseName.ButtonImage")));
            this.txtPhaseName.Enabled = false;
            this.txtPhaseName.Location = new System.Drawing.Point(198, 37);
            this.txtPhaseName.MaxLength = 32767;
            this.txtPhaseName.Multiline = false;
            this.txtPhaseName.Name = "txtPhaseName";
            this.txtPhaseName.ReadOnly = false;
            this.txtPhaseName.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtPhaseName.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtPhaseName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPhaseName.Size = new System.Drawing.Size(102, 19);
            this.txtPhaseName.TabIndex = 3;
            this.txtPhaseName.Tag = "Name";
            this.txtPhaseName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPhaseName.ButtonClick += new System.EventHandler(this.colorPhase_Click);
            this.txtPhaseName.Leave += new System.EventHandler(this.txtPhaseInfo_Leave);
            // 
            // txtPlanName
            // 
            this.txtPlanName.AcceptsReturn = false;
            this.txtPlanName.AcceptsTab = false;
            this.txtPlanName.BackColor = System.Drawing.Color.White;
            this.txtPlanName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.txtPlanName.ButtonImage = ((System.Drawing.Image)(resources.GetObject("txtPlanName.ButtonImage")));
            this.txtPlanName.Location = new System.Drawing.Point(0, 37);
            this.txtPlanName.MaxLength = 32767;
            this.txtPlanName.Multiline = false;
            this.txtPlanName.Name = "txtPlanName";
            this.txtPlanName.ReadOnly = false;
            this.txtPlanName.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.txtPlanName.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.txtPlanName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPlanName.Size = new System.Drawing.Size(192, 19);
            this.txtPlanName.TabIndex = 2;
            this.txtPlanName.Tag = "Plan.Name";
            this.txtPlanName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPlanName.ButtonClick += new System.EventHandler(this.txtCalendar_Click);
            this.txtPlanName.Leave += new System.EventHandler(this.txtPlanInfo_Leave);
            // 
            // tabPlanning
            // 
            this.tabPlanning.Controls.Add(this.label2);
            this.tabPlanning.Controls.Add(this.label3);
            this.tabPlanning.Controls.Add(this.label1);
            this.tabPlanning.Controls.Add(this.hiLoSlider1);
            this.tabPlanning.Location = new System.Drawing.Point(4, 25);
            this.tabPlanning.Name = "tabPlanning";
            this.tabPlanning.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlanning.Size = new System.Drawing.Size(795, 200);
            this.tabPlanning.TabIndex = 4;
            this.tabPlanning.Text = "Planning";
            this.tabPlanning.UseVisualStyleBackColor = true;
            this.tabPlanning.Enter += new System.EventHandler(this.tabPlanning_Enter);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(337, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(455, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Target CTL profile: Double-click to add a point, drag to edit, right click to rem" +
                "ove point.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(224, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "What to put here?  Planning info, statistics,...?";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "CTL Ramp High/Low limits (in CTL points/week):";
            // 
            // hiLoSlider1
            // 
            colorBlend4.Colors = new System.Drawing.Color[] {
        System.Drawing.Color.Green,
        System.Drawing.Color.Green,
        System.Drawing.Color.Yellow,
        System.Drawing.Color.Red,
        System.Drawing.Color.Red};
            colorBlend4.Positions = new float[] {
        0F,
        0F,
        0.2F,
        0.4F,
        1F};
            this.hiLoSlider1.Colors = colorBlend4;
            this.hiLoSlider1.High = 4F;
            this.hiLoSlider1.Location = new System.Drawing.Point(-1, 170);
            this.hiLoSlider1.Low = 0F;
            this.hiLoSlider1.MinimumSize = new System.Drawing.Size(0, 30);
            this.hiLoSlider1.Name = "hiLoSlider1";
            this.hiLoSlider1.RangeMax = 10F;
            this.hiLoSlider1.RangeMin = 0F;
            this.hiLoSlider1.Size = new System.Drawing.Size(334, 30);
            this.hiLoSlider1.TabIndex = 1;
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // ScheduleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlChart);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ScheduleControl";
            this.Size = new System.Drawing.Size(1013, 603);
            this.Load += new System.EventHandler(this.ScheduleControl_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextTreelist.ResumeLayout(false);
            this.pnlLibrary.ResumeLayout(false);
            this.grpTemplate.ResumeLayout(false);
            this.grpTemplate.PerformLayout();
            this.bnrTree.ResumeLayout(false);
            this.contextCalendar.ResumeLayout(false);
            this.bnrCalendar.ResumeLayout(false);
            this.bnrCalendar.PerformLayout();
            this.pnlChart.ResumeLayout(false);
            this.bnrChart.ResumeLayout(false);
            this.bnrChart.PerformLayout();
            this.menuTree.ResumeLayout(false);
            this.menuChart.ResumeLayout(false);
            this.menuGroupBy.ResumeLayout(false);
            this.contextInetCalBtn.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabWeek.ResumeLayout(false);
            this.weekPanel.ResumeLayout(false);
            this.tabDetails.ResumeLayout(false);
            this.tabDetails.PerformLayout();
            this.grpGarminFitness.ResumeLayout(false);
            this.grpGarminFitness.PerformLayout();
            this.grpPhase.ResumeLayout(false);
            this.grpPhase.PerformLayout();
            this.grpPlan.ResumeLayout(false);
            this.grpRepeat.ResumeLayout(false);
            this.grpRepeat.PerformLayout();
            this.grpWorkout.ResumeLayout(false);
            this.grpWorkout.PerformLayout();
            this.tabPlanning.ResumeLayout(false);
            this.tabPlanning.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Pabo.Calendar.MonthCalendar trainingCal;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip contextCalendar;
        private System.Windows.Forms.ToolStripMenuItem ctxCalAddWorkout;
        private System.Windows.Forms.ToolStripMenuItem ctxCalDeleteWorkout;
        private ZoneFiveSoftware.Common.Visuals.TreeList itemTree;
        private System.Windows.Forms.ToolStripMenuItem ctxCalAddPhase;
        private System.Windows.Forms.ContextMenuStrip contextTreelist;
        private System.Windows.Forms.ToolStripMenuItem ctxTreeInsertPhase;
        private System.Windows.Forms.ToolStripMenuItem ctxTreeAddWorkout;
        private System.Windows.Forms.ToolStripMenuItem ctxTreeDelete;
        private System.Windows.Forms.Panel pnlChart;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner bnrChart;
        private ZoneFiveSoftware.Common.Visuals.Chart.ZedGraphControl zedChart;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner bnrTree;
        private System.Windows.Forms.ContextMenuStrip menuTree;
        private System.Windows.Forms.ToolStripMenuItem mnuTreePlanOverview;
        private System.Windows.Forms.ToolStripMenuItem mnuTreeWorkoutLibrary;
        private System.Windows.Forms.ContextMenuStrip menuChart;
        private System.Windows.Forms.ToolStripMenuItem mnuChartTrainingLoad;
        private System.Windows.Forms.ToolStripMenuItem mnuTreeShowSummary;
        private System.Windows.Forms.ToolStripSeparator mnuTreeSeparator1;
        private ZoneFiveSoftware.Common.Visuals.Button btnTrainingLoad;
        private ZoneFiveSoftware.Common.Visuals.Button btnMaxUpper;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner bnrCalendar;
        private ZoneFiveSoftware.Common.Visuals.Button btnMaxLower;
        private System.Windows.Forms.ToolStripSeparator mnuChartSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuChartDistance;
        private System.Windows.Forms.ToolStripMenuItem mnuChartDistanceCum;
        private System.Windows.Forms.ToolStripMenuItem mnuChartTime;
        private System.Windows.Forms.ToolStripMenuItem mnuChartTimeCum;
        private System.Windows.Forms.ToolStripMenuItem mnuTreeShowEmptyFolders;
        private System.Windows.Forms.Panel pnlLibrary;
        private System.Windows.Forms.GroupBox grpTemplate;
        private ZoneFiveSoftware.Common.Visuals.Button btnLibImage;
        private System.Windows.Forms.RadioButton radLibPace;
        private System.Windows.Forms.RadioButton radLibDist;
        private System.Windows.Forms.RadioButton radLibTime;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtLibTime;
        private System.Windows.Forms.Label lblLibMi;
        private System.Windows.Forms.Label lblLibMph;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtLibSpeed;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtLibPace;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtLibDist;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtLibName;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtLibScore;
        private System.Windows.Forms.Label radLibScore;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtLibNotes;
        private ZoneFiveSoftware.Common.Visuals.Button btnNewTemplate;
        private System.Windows.Forms.ToolStripMenuItem ctxCalAddTemplate;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtLibCategory;
        private ZoneFiveSoftware.Common.Visuals.Button btnDelTemplate;
        private System.Windows.Forms.ToolStripMenuItem ctxTreeAddPhase;
        private System.Windows.Forms.ToolStripMenuItem ctxCalScheduleWorkout;
        private System.Windows.Forms.TabPage tabDetails;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtWorkoutCategory;
        private System.Windows.Forms.Label lblCategory;
        private ZoneFiveSoftware.Common.Visuals.Button btnGarminWorkout;
        private System.Windows.Forms.GroupBox grpPhase;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtPhaseEnd;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtPhaseStart;
        private System.Windows.Forms.Label lblPhaseEnd;
        private System.Windows.Forms.Label lblPhaseDuration;
        private System.Windows.Forms.Label lblPhaseStart;
        private System.Windows.Forms.GroupBox grpPlan;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtPlanGarminAutoSched;
        private System.Windows.Forms.CheckBox chkGarminAutoSched;
        private System.Windows.Forms.Label lblPlanEnd;
        private ZoneFiveSoftware.Common.Visuals.Button btnGarminFitness;
        private System.Windows.Forms.Label lblPlanDays;
        private System.Windows.Forms.Label lblPlanStart;
        private System.Windows.Forms.Label lblActivityCount;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtWorkoutNotes;
        private System.Windows.Forms.GroupBox grpRepeat;
        private ZoneFiveSoftware.Common.Visuals.Button btnParent;
        private ZoneFiveSoftware.Common.Visuals.Button btnLink;
        private System.Windows.Forms.CheckBox chkRepeat;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtWorkoutRamp;
        private System.Windows.Forms.Label lblWorkoutEnd;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtWorkoutEnd;
        private System.Windows.Forms.Label lblWorkoutRepeat;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtWorkoutPeriod;
        private System.Windows.Forms.Label lblWorkoutDays;
        private System.Windows.Forms.Label lblWorkoutRamp;
        private System.Windows.Forms.GroupBox grpWorkout;
        private ZoneFiveSoftware.Common.Visuals.Button btnImage;
        private System.Windows.Forms.RadioButton radPace;
        private System.Windows.Forms.RadioButton radDistance;
        private System.Windows.Forms.RadioButton radTime;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtWorkoutTime;
        private System.Windows.Forms.Label lblWorkoutMi;
        private System.Windows.Forms.Label lblWorkoutMph;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtWorkoutSpeed;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtWorkoutPace;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtWorkoutDistance;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtWorkoutScore;
        private System.Windows.Forms.Label lblWorkoutScore;
        private System.Windows.Forms.Label lblWorkoutTitle;
        private System.Windows.Forms.RadioButton radTSS;
        private System.Windows.Forms.RadioButton radTrimp;
        private System.Windows.Forms.Label lblPlanHelp;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Label lblPhase;
        private System.Windows.Forms.Label lblPlanTitle;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtGarminWorkout;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtWorkoutName;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtPhaseName;
        private ZoneFiveSoftware.Common.Visuals.TextBox txtPlanName;
        private System.Windows.Forms.TabPage tabWeek;
        private System.Windows.Forms.TableLayoutPanel weekPanel;
        private FitPlan.Controls.DailyPanel dailyY2;
        private FitPlan.Controls.DailyPanel dailyY3;
        private FitPlan.Controls.DailyPanel dailyT2;
        private ZoneFiveSoftware.Common.Visuals.Button btnBack;
        private FitPlan.Controls.DailyPanel dailyT3;
        private FitPlan.Controls.DailyPanel dailyY1;
        private FitPlan.Controls.DailyPanel dailyToday;
        private FitPlan.Controls.DailyPanel dailyT1;
        private ZoneFiveSoftware.Common.Visuals.Button btnNext;
        private FitPlan.Controls.DailyPanel WeekSummary;
        private FitPlan.Controls.TabControlBG tabControl;
        private System.Windows.Forms.ToolStripMenuItem mnuChartShowCTLtarget;
        private System.Windows.Forms.TabPage tabPlanning;
        private FitPlan.Controls.HiLoSlider hiLoSlider1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ZoneFiveSoftware.Common.Visuals.Button btnRampEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuChartShowPhases;
        private System.Windows.Forms.ToolStripMenuItem ctxTreeClosePlan;
        private ZoneFiveSoftware.Common.Visuals.Button btnZoomFit;
        private ZoneFiveSoftware.Common.Visuals.Button btnLockWorkout;
        private System.Windows.Forms.ToolStripMenuItem ctxCalLockWorkout;
        private System.Windows.Forms.ToolStripSeparator ctxCalSeparator1;
        private System.Windows.Forms.ContextMenuStrip menuGroupBy;
        private System.Windows.Forms.ToolStripMenuItem menuGroupDay;
        private System.Windows.Forms.ToolStripMenuItem menuGroupWeek;
        private ZoneFiveSoftware.Common.Visuals.Button btnGroupBy;
        private System.Windows.Forms.ToolStripMenuItem menuGroupMonth;
        private System.Windows.Forms.ToolStripMenuItem menuGroupCategory;
        private System.Windows.Forms.ToolTip chartToolTip;
        private System.Windows.Forms.ToolStripMenuItem ctxTreeColumnsPicker;
        private System.Windows.Forms.ToolStripMenuItem ctxTreeResetColumns;
        private System.Windows.Forms.ToolStripMenuItem ctxCalInsertPhase;
        private System.Windows.Forms.ContextMenuStrip contextInetCalBtn;
        private System.Windows.Forms.ToolStripMenuItem syncCalendarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFromGoogleCalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private ZoneFiveSoftware.Common.Visuals.Button btnGoogCal;
        private System.Windows.Forms.GroupBox grpGarminFitness;
        private System.Windows.Forms.ToolStripMenuItem autoSyncToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    }
}
