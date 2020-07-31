using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Visuals;

namespace FitPlan.Controls
{
    public partial class TabControlBG : System.Windows.Forms.TabControl
    {
        private ITheme theme;
        private Color backColor;
        private Color borderColor;

        public TabControlBG()
        {
            InitializeComponent();
            backColor = Color.Gray;
            borderColor = Color.Olive;
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("Tab background color")]
        [DefaultValue(typeof(Color), "Gray")]
        public override Color BackColor
        {
            get
            {
                return backColor;
            }
            set
            {
                backColor = value;
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("Tab control border color")]
        [DefaultValue(typeof(Color), "Olive")]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.Clear(this.BackColor);
            if (TabCount <= 0) return;

            //Draw a border around TabPage
            Rectangle r = SelectedTab.Bounds;
            r.Inflate(3, 3);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            TabPage tp = TabPages[SelectedIndex];
            SolidBrush PaintBrush = new SolidBrush(tp.BackColor);
            System.Drawing.Drawing2D.LinearGradientBrush gradBrush = new System.Drawing.Drawing2D.LinearGradientBrush(GetTabRect(0), BorderColor, tp.BackColor, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            Point[] points = ControlBorder.GetBorderPoints(r, ControlBorder.Style.Round);
            e.Graphics.FillPolygon(PaintBrush, points);
            ControlBorder.DrawBorder(e.Graphics, r, ControlBorder.Style.Round, BorderColor, Color.Transparent);

            Pen pen = new Pen(tp.BackColor, 1);

            //Draw the Tabs
            for (int index = 0; index <= TabCount - 1; index++)
            {
                tp = TabPages[index];
                r = GetTabRect(index);

                // Offset first tab
                if (index == 0)
                {
                    r.Location = new Point(r.Left + 1, r.Top);
                    r.Width -= 1;
                }

                // Set spacing between tabs
                r.Inflate(-1, 0);

                // Tab background
                points = ControlBorder.GetBorderPoints(r, ControlBorder.Style.Round);
                points = new Point[] { new Point(r.Left, r.Bottom - 1), points[7], points[8], points[1], points[2], new Point(r.Right - 1, r.Bottom - 1) };
                if (SelectedIndex != index)
                {
                    PaintBrush.Color = ControlPaint.Dark(tp.BackColor, .01f);
                    e.Graphics.FillPolygon(gradBrush, points);
                }
                else
                {
                    e.Graphics.TranslateTransform(1, 0);
                    pen.Color = ControlPaint.Dark(BorderColor);
                    e.Graphics.DrawLine(pen, points[3], points[4]);
                    e.Graphics.DrawLine(pen, points[4].X, points[4].Y, r.Right - 1, r.Bottom - 2);
                    e.Graphics.TranslateTransform(-1, 0);

                    PaintBrush.Color = tp.BackColor;
                    e.Graphics.FillPolygon(PaintBrush, points);
                }

                pen.Color = BorderColor;
                e.Graphics.DrawLines(pen, points);

                if (index == SelectedIndex)
                {
                    // Close off bottom of tab border
                    pen.Color = tp.BackColor;
                    e.Graphics.DrawLine(pen, r.Left + 1, r.Bottom - 1, r.Right - 1, r.Bottom - 1);
                }

                PaintBrush.Color = tp.ForeColor;

                //Set up rotation for left and right aligned tabs
                if (Alignment == TabAlignment.Left || Alignment == TabAlignment.Right)
                {
                    float RotateAngle = 90;
                    if (Alignment == TabAlignment.Left) RotateAngle = 270;
                    PointF cp = new PointF(r.Left + (r.Width >> 1), r.Top + (r.Height >> 1));
                    e.Graphics.TranslateTransform(cp.X, cp.Y);
                    e.Graphics.RotateTransform(RotateAngle);
                    r = new Rectangle(-(r.Height >> 1), -(r.Width >> 1), r.Height, r.Width);
                }

                //Draw the Tab Text
                if (tp.Enabled)
                    e.Graphics.DrawString(tp.Text, Font, PaintBrush, r, sf);
                else
                    ControlPaint.DrawStringDisabled(e.Graphics, tp.Text, Font, tp.BackColor, r, sf);

                e.Graphics.ResetTransform();
            }

            gradBrush.Dispose();
            pen.Dispose();
            PaintBrush.Dispose();
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            this.theme = visualTheme;
            BackColor = theme.Control;
            BorderColor = theme.Border;
            Invalidate();
        }

        public override void Refresh()
        {
            base.Refresh();
        }
    }
}
