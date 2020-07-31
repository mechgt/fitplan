using System;
using System.Collections.Generic;
using System.ComponentModel;
using FitPlan.Schedule;
using FitPlan.UI;

namespace FitPlan.Data
{
    /// <summary>
    /// Used to manage the plans and schedule objects loaded into memory
    /// </summary>
    internal static class Loaded
    {
        internal static bool initializing;

        static Loaded()
        {
            PluginMain.LogbookChanged += new PropertyChangedEventHandler(PluginMain_LogbookChanged);
        }

        private static List<TrainingPlan> loadedPlans = new List<TrainingPlan>();
        private static bool isLoaded;

        internal static IEnumerable<TrainingPlan> Plans
        {
            get { return loadedPlans; }
        }

        internal static bool IsLoaded
        {
            get { return isLoaded; }
        }

        internal static bool Contains(TrainingPlan plan)
        {
            return loadedPlans.Contains(plan);
        }

        internal static TrainingPlan AddPlan(TrainingPlan plan)
        {
            if (plan == null)
                throw new NullReferenceException("Cannot Load a null plan");

            // Prevent loading duplicates
            if (Loaded.Contains(plan))
                return plan;

            // Migrate plan if required
            if (plan.MigrateRequired)
                plan = MigrationAsst.MigrateTrainingPlan(plan);

            loadedPlans.Add(plan);
            isLoaded = true;

            return plan;
        }

        internal static void RemovePlan(TrainingPlan plan)
        {
            loadedPlans.Remove(plan);

            if (loadedPlans.Count == 0)
                isLoaded = false;
        }

        internal static void PluginMain_LogbookChanged(object sender, PropertyChangedEventArgs e)
        {
            loadedPlans = new List<TrainingPlan>();
            isLoaded = false;
        }
    }
}
