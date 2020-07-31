namespace FitPlan.Data.GarminFitness
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using FitPlan.Schedule;
    using FitPlan.UI;
    using GarminFitnessPublic;

    internal static class Manager
    {
        private static IPublicWorkoutManager manager;
        private static bool isInstalled;
        private static bool installChecked;
        private static Type Common;
        private static Type gfWorkout;
        private static MethodInfo gfRemoveWorkout;

        internal static bool IsInstalled
        {
            get
            {
                if (!installChecked)
                {
                    // Return true if plugin has already been found
                    DetectMethodsAndClasses();
                    installChecked = true;
                }

                return isInstalled;
            }
        }

        internal static IPublicWorkoutManager GarminManager
        {
            get
            {
                if (IsInstalled)
                {
                    return manager;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Get a specific Garmin Fitness workout from the available list.
        /// If garmin workout does not exist, a null value will be returned.
        /// </summary>
        /// <param name="id">The reference id in string format of the 
        /// Garmin workout.</param>
        /// <returns>the Garmin workout or null if none found.</returns>
        internal static IPublicWorkout GetWorkout(string id)
        {
            return GetWorkout(new Guid(id));
        }
        /// <summary>
        /// Get a specific Garmin Fitness workout from the available list.
        /// If garmin workout does not exist, a null value will be returned.
        /// </summary>
        /// <param name="id">The reference id of the Garmin workout.</param>
        /// <returns>the Garmin workout or null if none found.</returns>
        internal static IPublicWorkout GetWorkout(Guid id)
        {
            if (GarminManager != null)
            {
                foreach (IPublicWorkout workout in GarminManager.Workouts)
                {
                    if (workout.Id == id)
                    {
                        return workout;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Schedule a workout in Garmin Fitness.  This workout must be linked 
        /// to a template, and that template must be a Garmin Fitness workout for
        /// the scheduling to succeed.  The accociated Garmin Fitness workout
        /// will be scheduled for the StartDate of the Workout.
        /// </summary>
        /// <param name="workout">workout to schedule</param>
        /// <returns>true if scheduling was successful, otherwise false.</returns>
        internal static bool ScheduleWorkout(Workout workout)
        {
            if (workout == null || workout.LinkedTemplate == null)
                return false;

            return ScheduleWorkout(workout.LinkedTemplate, workout.StartDate);
        }

        /// <summary>
        /// Schedule a workout in Garmin Fitness.  The template must be a 
        /// Garmin Fitness workout for the scheduling to succeed.  The 
        /// accociated Garmin Fitness workout will be scheduled for the 
        /// requested date.
        /// </summary>
        /// <param name="template">Garmin Fitness workout</param>
        /// <param name="date">Date to schedule workout.</param>
        /// <returns>true if scheduling was successful, otherwise false.</returns>
        internal static bool ScheduleWorkout(WorkoutDefinition template, DateTime date)
        { return ScheduleWorkout(template, date, false); }

        /// <summary>
        /// Schedule a workout in Garmin Fitness.  The template must be a 
        /// Garmin Fitness workout for the scheduling to succeed.  The 
        /// accociated Garmin Fitness workout will be scheduled for the 
        /// requested date.
        /// </summary>
        /// <param name="template">Garmin Fitness workout</param>
        /// <param name="date">Date to schedule workout.</param>
        /// <<param name="clear">true to remove the workout from the schedule, false
        /// to enter the workout into the schedule.</param>
        /// <returns>true if scheduling was successful, otherwise false.</returns>
        internal static bool ScheduleWorkout(WorkoutDefinition template, DateTime date, bool clear)
        {
            if (template == null) return false;

            IPublicWorkout garminWorkout = Manager.GetWorkout(template.GarminId);
            if (garminWorkout != null)
            {
                if (!clear)
                {
                    // Schedule workout
                    GarminManager.ScheduleWorkout(garminWorkout, date);
                    return true;
                }
                else if (clear && gfRemoveWorkout != null)
                {
                    // Remove workout
                    try
                    {
                        object[] param = { date };
                        gfRemoveWorkout.Invoke(garminWorkout, param);
                        return true;
                    }
                    catch
                    {
                        // Oops, GF changed the remove method
                        return false;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check for the Garmin Fitness plugin, and make sure that it's the right version to support
        /// all necessary options/features.  This will scan the loaded assemblies looking for GarminFitness.
        /// Supports the IsInstalled property.
        /// </summary>
        private static void DetectMethodsAndClasses()
        {
            isInstalled = false;

            foreach (Assembly loadedAssembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                AssemblyName name = loadedAssembly.GetName();

                if (loadedAssembly.FullName.StartsWith("GarminFitnessPlugin"))
                {
                    try
                    {
                        Common = loadedAssembly.GetType("GarminFitnessPlugin.Controller.PublicWorkoutManager");
                        PropertyInfo property = Common.GetProperty("Instance");
                        manager = Common.GetProperty("Instance").GetValue(null, null) as IPublicWorkoutManager;
                        isInstalled = true;
                    }
                    catch 
                    { 
                    }

                    try
                    {
                        gfWorkout = loadedAssembly.GetType("GarminFitnessPlugin.Data.IWorkout");
                        gfRemoveWorkout = gfWorkout.GetMethod("RemoveScheduledDate");
                    }
                    catch 
                    { 
                    }
                }
            }
        }
    }
}
