namespace FitPlan.UI
{
    using System;
    using FitPlan.Data;
    using FitPlan.Data.GarminFitness;
    using FitPlan.Schedule;
    using FitPlan.Settings;

    internal static class MigrationAsst
    {
        internal static TrainingPlan MigrateTrainingPlan(TrainingPlan plan)
        {
            if (new Version(plan.FileVersion) < new Version("0.3"))
            {
                if (Manager.IsInstalled)
                {
                    // Add templateIds to fit plan to link planned workouts with garmin fitness activities
                    WorkoutCollection workouts = plan.GetWorkouts();

                    foreach (Workout workout in workouts)
                        if (workout.TemplateId == Guid.Empty)
                            foreach (WorkoutDefinition def in LogbookSettings.Main.TemplateDefinitions)
                                if (workout.Name == def.Name)
                                    workout.TemplateId = def.Id;
                }
            }

            plan.FileVersion = TrainingPlan.currentFileVer.ToString();
            return plan;
        }
    }
}
