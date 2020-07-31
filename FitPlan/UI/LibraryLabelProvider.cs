namespace FitPlan.UI
{
    using System.Drawing;
    using FitPlan.Resources;
    using ZoneFiveSoftware.Common.Visuals;

    internal class LibraryLabelProvider : TreeList.DefaultLabelProvider
    {
        public LibraryLabelProvider()
            : base()
        { }

        public override Image GetImage(object element, TreeList.Column column)
        {
            LibraryNode node = element as LibraryNode;

            if (node != null && column.Id == "Name")
            {
                if (node.IsCategory)
                    return CommonResources.Images.Folder16;

                else
                {
                    Image workoutImage = GlobalImages.GetImage(node.Workout.ImageName);

                    if (workoutImage != null)
                    {
                        // Resize 32px image to 16px
                        Bitmap newImage = new Bitmap(16, 16);
                        using (Graphics gr = Graphics.FromImage(newImage))
                        {
                            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                            gr.DrawImage(workoutImage, new Rectangle(0, 0, 16, 16));
                        }

                        return newImage;
                    }
                    else if (node.IsGarminWorkout)
                        return Images.GarminWorkout16;

                    else
                        return Images.Calendar; // Default image

                }
            }

            return base.GetImage(element, column);
        }

        public override string GetText(object element, TreeList.Column column)
        {
            LibraryNode node = element as LibraryNode;

            if (node != null)
                if (node.IsCategory && column.Id == "Name")
                    return node.Category.Name;

                else if (node.IsWorkoutDef)
                    return node.Workout.GetFormattedText(column.Id);

            return base.GetText(element, column);
        }
    }
}
