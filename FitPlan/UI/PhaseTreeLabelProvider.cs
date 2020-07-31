namespace FitPlan.UI
{
    using System.Drawing;
    using FitPlan.Resources;
    using FitPlan.Schedule;
    using ZoneFiveSoftware.Common.Visuals;

    internal class PhaseTreeLabelProvider : TreeList.DefaultLabelProvider
    {
        public PhaseTreeLabelProvider()
            : base()
        { }

        public override Image GetImage(object element, TreeList.Column column)
        {
            FitPlanNode node = element as FitPlanNode;
            if (node.IsWorkout && column.Id == "Complete")
            {
                Workout workout = node.Element as Workout;
                Image workoutImage = GlobalImages.GetImage(workout.ImageName);

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
            }
            else if (node.IsPlan && column.Id == "Complete")
            {
                return Images.FitPlan16;
            }

            return base.GetImage(element, column);
        }

        public override string GetText(object element, TreeList.Column column)
        {
            return base.GetText(element, column);
        }
    }
}
