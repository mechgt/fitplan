namespace FitPlan.Controls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public partial class HiLoSlider : UserControl
    {
        #region Fields

        public event PropertyChangedEventHandler SliderMoved;
        private ColorBlend colors;
        private float low, high, rangeMin, rangeMax;
        private int triSize = 6;
        private Slider slider;

        #endregion

        #region Constructors

        public HiLoSlider()
        {
            InitializeComponent();
        }

        #endregion

        #region Enumerations

        private enum Slider
        {
            Low, High
        }

        #endregion

        #region Properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(typeof(ColorBlend), "")]
        public ColorBlend Colors
        {
            get { return colors; }
            set
            {
                colors = value;
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(0)]
        public float RangeMin
        {
            get { return rangeMin; }
            set
            {
                rangeMin = value;
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(10)]
        public float RangeMax
        {
            get { return rangeMax; }
            set
            {
                rangeMax = value;
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(7f)]
        public float High
        {
            get { return Math.Max(Math.Min(high, RangeMax), low); }
            set
            {
                high = value;
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Misc")]
        [DefaultValue(2f)]
        public float Low
        {
            get { return Math.Max(Math.Min(low, high), RangeMin); }
            set
            {
                low = value;
                Invalidate();
            }
        }

        internal Point LowPixel
        {
            get { return new Point((int)(Low / (RangeMax - RangeMin) * this.Width), BarRect.Bottom + triSize / 2 - 1); }
        }

        internal Point HighPixel
        {
            get { return new Point((int)(High / (RangeMax - RangeMin) * this.Width), BarRect.Bottom + triSize / 2 - 1); }
        }

        internal Rectangle BarRect
        {
            get { return new Rectangle(1, 12, this.Width - 2, ((this.Height - 12) * 3 / 4) - 2); }
        }

        #endregion

        #region Paint

        private void HiLoSlider_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        private void Draw(Graphics g)
        {
            colors = new ColorBlend();
            colors.Colors = new Color[] { Color.Green, Color.Green, Color.Yellow, Color.Red, Color.Red };
            float lowRat = Low / (RangeMax - RangeMin);
            float highRat = High / (RangeMax - RangeMin);
            colors.Positions = new float[] { 0, lowRat, (lowRat + highRat) / 2, highRat, 1 };

            // Filled gradient
            LinearGradientBrush gradBrush = new LinearGradientBrush(Point.Add(BarRect.Location, new Size(1, 1)), Point.Add(BarRect.Location, BarRect.Size), Color.Blue, Color.MediumOrchid);
            gradBrush.InterpolationColors = colors;
            g.FillRectangle(gradBrush, BarRect);

            // Border
            Pen blackPen = new Pen(Color.Black);
            g.DrawRectangle(blackPen, BarRect);

            // Low
            int loc = (int)(this.Width * lowRat);
            PointF[] triangle = new PointF[] { 
                new PointF(loc, BarRect.Bottom - 3), 
                new PointF(loc + triSize, BarRect.Bottom + triSize - 2),
                new PointF(loc - triSize, BarRect.Bottom + triSize - 2),
                new PointF(loc, BarRect.Bottom - 3) };
            g.FillPolygon(Brushes.DarkGreen, triangle);
            g.DrawPolygon(blackPen, triangle);

            // High
            loc = (int)(this.Width * highRat);
            triangle = new PointF[] { 
                new PointF(loc, BarRect.Bottom - 3), 
                new PointF(loc + triSize, BarRect.Bottom + triSize - 2),
                new PointF(loc - triSize, BarRect.Bottom + triSize - 2),
                new PointF(loc, BarRect.Bottom - 3) };
            g.FillPolygon(Brushes.DarkRed, triangle);
            g.DrawPolygon(blackPen, triangle);

            // Label Text
            Font font = new Font("Microsoft Sans Serif", 8.25f);
            StringFormat format = new StringFormat();

            g.DrawString(RangeMin.ToString(), font, Brushes.Black, 0, 0);
            format.Alignment = StringAlignment.Far;
            g.DrawString(RangeMax.ToString("#"), font, Brushes.Black, BarRect.Right, 0, format);
            format.Alignment = StringAlignment.Center;
            g.DrawString(Low.ToString("#.#"), font, Brushes.Black, this.Width * lowRat, 0, format);
            g.DrawString(High.ToString("#.#"), font, Brushes.Black, this.Width * highRat, 0, format);

            gradBrush.Dispose();
            blackPen.Dispose();
        }

        #endregion

        #region Methods

        public override void Refresh()
        {
            this.Invalidate();
        }

        private bool HitTest(Point mousePt, Point testPx)
        {
            return Math.Abs(mousePt.Y - testPx.Y) < triSize / 2 + 4 && Math.Abs(mousePt.X - testPx.X) < triSize / 2 + 4;
        }

        /// <summary>
        /// Return scaled user X value along slider, given some pixel value.
        /// </summary>
        /// <param name="pixel"></param>
        /// <returns></returns>
        private float ReverseTransform(Point pixel)
        {
            return (pixel.X - 1f) / (this.Width - 2f) * (RangeMax - RangeMin);
        }

        #endregion

        #region Mouse Events

        private bool isSliding;

        private void HiLoSlider_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (HitTest(e.Location, LowPixel))
                {
                    isSliding = true;
                    slider = Slider.Low;
                }
                else if (HitTest(e.Location, HighPixel))
                {
                    isSliding = true;
                    slider = Slider.High;
                }
            }
        }

        private void HiLoSlider_MouseUp(object sender, MouseEventArgs e)
        {
            if (isSliding)
            {
                isSliding = false;
                HandleSliderMoved();
            }
        }

        private void HiLoSlider_MouseMove(object sender, MouseEventArgs e)
        {
            Point lowPx = LowPixel;
            Point highPx = HighPixel;

            if (isSliding)
            {
                // Already sliding
                HandleSliding(e.Location);
            }
            else if (HitTest(e.Location, lowPx))
            {
                // Mouse over Low drag item
                Cursor.Current = Cursors.SizeWE;
            }
            else if (HitTest(e.Location, highPx))
            {
                // Mouse over High drag item
                Cursor.Current = Cursors.SizeWE;
            }
            else if (Cursor.Current != Cursors.Default)
            {
                // Reset to default
                Cursor.Current = Cursors.Default;
            }
        }

        private void HandleSliding(Point mousePt)
        {
            switch (slider)
            {
                case Slider.Low:
                    Low = ReverseTransform(mousePt);
                    break;
                case Slider.High:
                    High = ReverseTransform(mousePt);
                    break;
            }

            this.Invalidate();
        }

        private void HandleSliderMoved()
        {
            if (SliderMoved != null)
            {
                string propName;
                if (slider == Slider.Low)
                    propName = "Low";
                else
                    propName = "High";

                SliderMoved.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }

        #endregion
    }
}
