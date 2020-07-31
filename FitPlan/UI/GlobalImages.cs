namespace FitPlan.UI
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using ZoneFiveSoftware.Common.Visuals;

    internal static class GlobalImages
    {
        internal enum ImageSize
        {
            Small,
            Med,
            Large
        }
        private static ImageList calImageList;

        internal static ImageList CalendarImageList
        {
            get
            {
                if (calImageList == null)
                {
                    InitializeImages();
                }

                return calImageList;
            }
        }

        /// <summary>
        /// Gets the Image associated with the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal static Image GetImage(string key)
        {
            return GetImage(key, false);
        }

        /// <summary>
        /// Gets the Image associated with the given key.  Can optionally incorporate a lock
        /// overlay in the lower right corner.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="showLock"></param>
        /// <returns></returns>
        internal static Image GetImage(string key, bool showLock)
        {
            Image img = null;
            if (CalendarImageList.Images.ContainsKey(key) && key != "-")
                img = CalendarImageList.Images[key];
            else if (showLock)
                img = new Bitmap(32, 32);

            if (showLock)
            {
                OverlayLock(img);
            }

            return img;
        }

        internal static Image GetImage(string key, bool showLock, ImageSize size)
        {
            switch (size)
            {
                case ImageSize.Small:
                    key += "16";
                    break;
                case ImageSize.Med:
                    key += "32";
                    break;
                case ImageSize.Large:
                    key += "Lg";
                    break;
            }

            object obj = Resources.Images.ResourceManager.GetObject(key);
            Image img = (Bitmap)(obj);
            if (showLock && img != null)
                img = OverlayLock(img);
            return img;
        }

        private static Image OverlayLock(Image img)
        {
            using (Graphics g = Graphics.FromImage(img))
            {
                Image lockImage = FitPlan.Resources.Images.lock16;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(lockImage, new Rectangle(img.Width - 12, img.Height - 12, 12, 12));
            }

            return img;
        }

        /// <summary>
        /// Initialize the available calendar images.
        /// </summary>
        private static void InitializeImages()
        {
            calImageList = new ImageList();
            calImageList.ImageSize = new System.Drawing.Size(32, 32);
            calImageList.ColorDepth = ColorDepth.Depth32Bit;

            calImageList.Images.Add("-", CommonResources.Images.Clear16);
            calImageList.Images.Add("Running", Resources.Images.Running32);
            calImageList.Images.Add("RunBike", Resources.Images.RunBike32);
            calImageList.Images.Add("CycleRoad", Resources.Images.CycleRoad32);
            calImageList.Images.Add("CycleMTB", Resources.Images.CycleMTB32);
            calImageList.Images.Add("TrackCycle", Resources.Images.TrackCycle32);
            calImageList.Images.Add("SwimBike", Resources.Images.SwimBike32);
            calImageList.Images.Add("SwimRun", Resources.Images.SwimRun32);
            calImageList.Images.Add("Swim", Resources.Images.Swim32);
            calImageList.Images.Add("Triathlon", Resources.Images.Triathlon32);
            calImageList.Images.Add("Weights", Resources.Images.Weights32);
            calImageList.Images.Add("Skiing", Resources.Images.Skiing32);
            calImageList.Images.Add("Nordic", Resources.Images.Nordic32);
            calImageList.Images.Add("Snowboard", Resources.Images.Snowboard32);
            calImageList.Images.Add("Slow", Resources.Images.Slow32);
            calImageList.Images.Add("Fast", Resources.Images.Fast32);
            calImageList.Images.Add("Hills", Resources.Images.Hills32);
            calImageList.Images.Add("Stopwatch", Resources.Images.Stopwatch32);
            calImageList.Images.Add("Repeat", Resources.Images.Repeat32);
            calImageList.Images.Add("Brick", Resources.Images.Brick);
        }
    }

}
