namespace FitPlan.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using ZoneFiveSoftware.Common.Data.Measurement;
    using ZoneFiveSoftware.Common.Visuals;
    using System.Drawing.Imaging;
    using System.Drawing.Drawing2D;
    using FitPlan.Schedule;
    using System.Collections.Generic;

    public class DailyPanel : System.Windows.Forms.UserControl
    {
        private ZoneFiveSoftware.Common.Visuals.Panel pnlBG;
        private Label lblCtlAtl;
        private ButtonTree tree;
        private Label lblBody;
        private bool isSummary;

        /// <summary>
        /// Create an instance of the Panel class.
        /// </summary>
        public DailyPanel()
        {
            InitializeComponent();

            tree.Columns.Add(new TreeList.Column());
            tree.RowDataRenderer = new DailyRowRenderer(tree);
            tree.LabelProvider = new DailyLabelProvider();
            tree.RowData = new List<DailyNode>();
        }

        /// <summary>
        /// Get or set the background color.
        /// </summary>
        public override Color BackColor { get; set; }

        /// <summary>
        /// Get or set the bottom edge gradient color.  Refresh for display to reflect change.
        /// </summary>
        [Category("Background Effect")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(typeof(Color), "Transparent")]
        public Color BottomGradientColor
        {
            get { return pnlBG.BottomGradientColor; }
            set
            {
                pnlBG.BottomGradientColor = value;
            }
        }

        /// <summary>
        /// Get or set the bottom edge gradient percent.  Refresh for display to reflect change.
        /// </summary>
        [DefaultValue(0.5)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Background Effect")]
        public double BottomGradientPercent { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(typeof(bool), "False")]
        public bool ShowLegend { get; set; }

        /// <summary>
        /// True if CTL,ATL,TSB should always be signed (preceeded by plus/minus)
        /// </summary>
        /// <remarks>Applies to Summary pane where a weekly delta is displayed instead
        /// of an absolute value.</remarks>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(typeof(bool), "False")]
        public bool IsSummary
        {
            get { return isSummary; }
            set
            {
                isSummary = value;
                if (isSummary)
                {
                    tree.Hide();
                    lblBody.Show();
                }
                else
                {
                    lblBody.Hide();
                    tree.Show();
                }
            }
        }

        /// <summary>
        /// Get or set the workout name text.  Refresh for display to reflect change.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(typeof(string), "Workout")]
        public string WorkoutName { get; set; }

        /// <summary>
        /// Get or set the date in the panel header.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(typeof(string), "12/31/2010")]
        public string HeadingText
        {
            get { return this.pnlBG.HeadingText; }
            set { this.pnlBG.HeadingText = value; }
        }

        /// <summary>
        /// Get or set the background color for the heading area
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(typeof(Color), "DarkKhaki")]
        public Color HeadingBackColor
        {
            get { return this.pnlBG.HeadingBackColor; }
            set { this.pnlBG.HeadingBackColor = value; }
        }

        /// <summary>
        /// Get or set the heading text color.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(typeof(Color), "Black")]
        public Color TextColor
        {
            get { return pnlBG.HeadingTextColor; }
            set { pnlBG.HeadingTextColor = value; }
        }

        public float ATL { get; set; }
        public float CTL { get; set; }

        /// <summary>
        /// Get or set the workout details text.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(typeof(string), "0 mi.")]
        public string Distance { get; set; }

        /// <summary>
        /// Get or set the workout details text.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(typeof(string), "0")]
        public string Score { get; set; }

        /// <summary>
        /// Get or set the workout details text.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(typeof(string), "0 mi.")]
        public string DistanceActual { get; set; }

        /// <summary>
        /// Get or set the workout details text.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(typeof(string), "0:00 min/mi.")]
        public string Pace { get; set; }

        /// <summary>
        /// Get or set the workout details text.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(typeof(string), "0:00 min/mi.")]
        public string PaceActual { get; set; }

        /// <summary>
        /// Get or set the workout details text.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(typeof(string), "00:00")]
        public string Time { get; set; }

        /// <summary>
        /// Get or set the workout details text.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(typeof(string), "00:00")]
        public string TimeActual { get; set; }

        /// <summary>
        /// Get or set the left edge gradient color.
        /// </summary>
        [Category("Background Effect")]
        [DefaultValue(typeof(Color), "Transparent")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color LeftGradientColor { get; set; }

        /// <summary>
        /// Get or set the left edge gradient percent.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Background Effect")]
        [DefaultValue(0.5)]
        public double LeftGradientPercent { get; set; }

        /// <summary>
        /// Get or set the right edge gradient color.
        /// </summary>
        [DefaultValue(typeof(Color), "Transparent")]
        [Category("Background Effect")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color RightGradientColor { get; set; }

        /// <summary>
        /// Get or set the right edge gradient percent.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Background Effect")]
        [DefaultValue(0.5)]
        public double RightGradientPercent { get; set; }

        /// <summary>
        /// Get or set the top edge gradient color.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Background Effect")]
        [DefaultValue(typeof(Color), "Transparent")]
        public Color TopGradientColor { get; set; }

        /// <summary>
        /// Get or set the top edge gradient percent.
        /// </summary>
        [Category("Background Effect")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(0.5)]
        public double TopGradientPercent { get; set; }

        public DateTime Date { get; set; }

        /// <summary>
        /// Set the theme which is used to render the control.
        /// </summary>
        /// <param name="visualTheme"></param>
        public virtual void ThemeChanged(ITheme visualTheme)
        {
            this.BackColor = visualTheme.Control;
            this.TextColor = visualTheme.MainHeaderText;
            Color headerBG = this.HeadingBackColor;
            this.pnlBG.ThemeChanged(visualTheme);
            this.pnlBG.HeadingBackColor = headerBG;

            tree.ThemeChanged(visualTheme);
            tree.BackColor = Color.Transparent;
            tree.RowSelectedColor = Color.Transparent;
            tree.RowSelectedColorText = tree.ForeColor;
        }

        public override void Refresh()
        {
            base.Refresh();
            object[] args = new object[] { WorkoutName, 
                Distance, Length.LabelAbbr(PluginMain.DistanceUnits), 
                Time, 
                Pace,  Speed.Label(Speed.Units.Pace, new Length(1, PluginMain.DistanceUnits)),
                Score };

            string bodyText = string.Empty;

            if (IsSummary)
            {
                bodyText = "{0}\r\n"; // Name

                if (!string.IsNullOrEmpty(Distance))
                    bodyText += "{1} {2}\r\n"; // Distance

                bodyText += "{3}\r\n"; // Time

                if (!string.IsNullOrEmpty(Pace))
                    bodyText += "{4} {5}\r\n"; // Pace

                if (!string.IsNullOrEmpty(Score))
                    bodyText += "{6}"; // SCORE

                lblBody.Text = string.Format(bodyText, args);
            }

            if (Data.TrainingLoadPlugin.IsInstalled)
            {
                string text = "{0} \\ {1} \\ {2}";
                string format;
                if (IsSummary)
                    format = "+#;-#;0";
                else
                    format = "0";

                args = new object[] { Math.Round(CTL).ToString(format), Math.Round(ATL).ToString(format), Math.Round(CTL - ATL).ToString(format) };
                if (ShowLegend)
                    text = string.Format("{0}\\{1}\\{2}\r\n", Resources.Strings.Label_CTL, Resources.Strings.Label_ATL, Resources.Strings.Label_TSB) + text;

                lblCtlAtl.Text = string.Format(text, args);
                lblCtlAtl.Visible = true;
            }
            else
            {
                lblCtlAtl.Visible = false;
            }
        }

        public void AddWorkout(Workout workout)
        {
            tree.AddNode(new DailyNode(workout));
        }

        public void ClearPanel()
        {
            WorkoutName = string.Empty;
            Distance = string.Empty;
            Time = string.Empty;
            Pace = string.Empty;
            Score = string.Empty;
            tree.ClearNodes();

            Invalidate();
        }

        /// <summary>
        /// Handle the paint event to render the component.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Draw(e.Graphics, e.ClipRectangle);
        }

        internal void Draw(Graphics g, Rectangle rect)
        {
            if (!this.IsSummary)
            {
                // String writing params
                Font italic = new Font(this.Font, FontStyle.Underline);
                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Near;
                Rectangle r = new Rectangle(new Point(this.Padding.Left, 20), new Size(this.Width, italic.Height));

                // Workout Name
                g.DrawString(WorkoutName, italic, Brushes.Black, r, format);
            }
        }

        private void InitializeComponent()
        {
            this.pnlBG = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.lblBody = new System.Windows.Forms.Label();
            this.lblCtlAtl = new System.Windows.Forms.Label();
            this.tree = new FitPlan.Controls.ButtonTree();
            this.pnlBG.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBG
            // 
            this.pnlBG.BackColor = System.Drawing.Color.Transparent;
            this.pnlBG.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.RoundShadow;
            this.pnlBG.BorderColor = System.Drawing.Color.Gray;
            this.pnlBG.BottomGradientColor = System.Drawing.Color.OrangeRed;
            this.pnlBG.BottomGradientPercent = 0.6;
            this.pnlBG.Controls.Add(this.tree);
            this.pnlBG.Controls.Add(this.lblCtlAtl);
            this.pnlBG.Controls.Add(this.lblBody);
            this.pnlBG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBG.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlBG.HeadingBackColor = System.Drawing.Color.DarkKhaki;
            this.pnlBG.HeadingFont = null;
            this.pnlBG.HeadingLeftMargin = 5;
            this.pnlBG.HeadingText = "12/1/2010";
            this.pnlBG.HeadingTextColor = System.Drawing.Color.Black;
            this.pnlBG.HeadingTopMargin = 3;
            this.pnlBG.Location = new System.Drawing.Point(0, 0);
            this.pnlBG.Name = "pnlBG";
            this.pnlBG.Padding = new System.Windows.Forms.Padding(1, 19, 3, 5);
            this.pnlBG.Size = new System.Drawing.Size(136, 273);
            this.pnlBG.TabIndex = 9;
            this.pnlBG.Tag = "3";
            // 
            // lblBody
            // 
            this.lblBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBody.Location = new System.Drawing.Point(4, 19);
            this.lblBody.Name = "lblBody";
            this.lblBody.Size = new System.Drawing.Size(126, 217);
            this.lblBody.TabIndex = 3;
            this.lblBody.Text = "lblBody";
            this.lblBody.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCtlAtl
            // 
            this.lblCtlAtl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCtlAtl.Location = new System.Drawing.Point(1, 236);
            this.lblCtlAtl.Name = "lblCtlAtl";
            this.lblCtlAtl.Size = new System.Drawing.Size(132, 32);
            this.lblCtlAtl.TabIndex = 1;
            this.lblCtlAtl.Text = "CTL/ATL/TSB";
            this.lblCtlAtl.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // tree
            // 
            this.tree.BackColor = System.Drawing.Color.Transparent;
            this.tree.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.None;
            this.tree.CheckBoxes = false;
            this.tree.DefaultIndent = 15;
            this.tree.DefaultRowHeight = -1;
            this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tree.HeaderRowHeight = 0;
            this.tree.Location = new System.Drawing.Point(1, 19);
            this.tree.MultiSelect = false;
            this.tree.Name = "tree";
            this.tree.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.tree.NumLockedColumns = 0;
            this.tree.RowAlternatingColors = false;
            this.tree.RowHotlightColor = System.Drawing.Color.Transparent;
            this.tree.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.tree.RowHotlightMouse = false;
            this.tree.RowSelectedColor = System.Drawing.Color.Transparent;
            this.tree.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.tree.RowSeparatorLines = false;
            this.tree.ShowLines = false;
            this.tree.ShowPlusMinus = false;
            this.tree.Size = new System.Drawing.Size(132, 217);
            this.tree.TabIndex = 2;
            // 
            // DailyPanel
            // 
            this.Controls.Add(this.pnlBG);
            this.Name = "DailyPanel";
            this.Size = new System.Drawing.Size(136, 273);
            this.pnlBG.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
