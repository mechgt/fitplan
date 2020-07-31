namespace FitPlan.UI
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ZoneFiveSoftware.Common.Visuals;
    using System.Drawing;

    internal class ImagePopupLabelProvider : TreeList.DefaultLabelProvider
    {
        public ImagePopupLabelProvider()
            : base()
        {
        }

        public override Image GetImage(object element, TreeList.Column column)
        {
            string item = element as string;

            if (item != null && column.Id == "Image")
            {
                return GlobalImages.GetImage(item);
            }

            return base.GetImage(element, column);
        }
    }
}
