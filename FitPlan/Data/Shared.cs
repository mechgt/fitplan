using System;
using System.Collections.Generic;
using System.Text;
using FitPlan.Schedule;
using System.Runtime.Serialization;
using System.IO;
using ZoneFiveSoftware.Common.Data.Fitness;
using FitPlan.Settings;

namespace FitPlan.Data
{
    public static class Shared
    {
        public static WorkoutCollection GetWorkouts()
        {
            WorkoutCollection workouts = new WorkoutCollection();

            if (!Loaded.IsLoaded)
            {
                LogbookSettings.LoadSettings();
                TrainingPlan plan = TrainingPlan.OpenPlan(LogbookSettings.Main.CurrentTrainingPlan);

                if (plan != null)
                    Loaded.AddPlan(plan);
            }

            foreach (TrainingPlan plan in Loaded.Plans)
            {
                workouts.AddRange(plan.GetWorkouts());
            }

            return workouts;
        }
    }
}
