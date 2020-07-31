using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using FitPlan.Schedule;
using System.Drawing;
using FitPlan.UI;
using ZoneFiveSoftware.Common.Data.Measurement;

namespace FitPlan.Controls
{
    class DailyNode : TreeList.TreeListNode
    {
        private Workout workout;
        private string text;
        private Image alphaImage;

        internal DailyNode(Workout workout)
            : base(null, workout)
        {
            this.workout = workout;

            if (workout.TotalDistanceMeters > 0)
                this.Children.Add(new DailyNode(this, Length.ToString(Length.Convert(workout.TotalDistanceMeters, Length.Units.Meter, PluginMain.DistanceUnits), PluginMain.DistanceUnits, "0.##u")));
            if (workout.TotalTime > TimeSpan.Zero)
                this.Children.Add(new DailyNode(this, workout.GetFormattedText("TotalTime")));
            if (workout.MetersPerSecond > 0)
                this.Children.Add(new DailyNode(this, workout.GetFormattedText("SpeedPace")));
            if (workout.Score > 0)
                this.Children.Add(new DailyNode(this, string.Format("{0} {1}", ScheduleControl.ScoreText, workout.GetFormattedText("Score"))));

            while (Children.Count < 5)
            {
                Children.Add(new DailyNode(this,string.Empty));
            }
        }

        internal DailyNode(TreeList.TreeListNode parent, string text)
            : base(parent, text)
        {
            this.text = text;
        }

        public Image WorkoutImage
        {
            get
            {
                if (alphaImage != null)
                    return alphaImage;

                if (this.IsParent && !string.IsNullOrEmpty(this.Workout.ImageName))
                {
                    alphaImage = GlobalImages.GetImage(Workout.ImageName, false, GlobalImages.ImageSize.Large);
                    alphaImage = Utilities.SetOpacity(alphaImage, .1f);
                }

                return alphaImage;
            }
        }

        internal bool IsParent
        {
            get { return workout != null; }
        }

        public Workout Workout
        {
            get { return workout; }
        }

        public override string ToString()
        {
            if (IsParent)
                return workout.Name;
            else
                return text;
        }
    }
}
