using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Data.Fitness;

namespace FitPlan.Data
{
    class DanielsActivity
    {
        private IActivity activity;

        #region Constructor

        internal DanielsActivity(IActivity activity)
        {
            this.activity = activity;
        }

        #endregion

        /// <summary>
        /// Daniels Points (similar to TRIMP)
        /// </summary>
        public double DanielsPoints
        {
            get { return 0; }
        }

        public double EstimatedVDOT
        {
            get
            {
                double? valueNullable = activity.GetCustomDataValue(Common.EstVDOTfield) as double?;
                if (valueNullable != null)
                {
                    // Returned cached value
                    return (double)valueNullable;
                }

                // Recalculate
                double value;

                // 210 * 1000 / 60 = 3500 -> 210 (from formula), 1000 km>m, 60 min>sec
                value = 210f / 1000f * 60f * Info.AverageSpeedMetersPerSecond / PercentHRmax;

                // TODO: How/when to check if this is running related?
                if (IsRunningActivity && !double.IsInfinity(value) && !double.IsNaN(value))
                {
                    // Valid value
                    CustomDataFields.SetCustomDataValue(Common.EstVDOTfield, value, activity);
                    return value;
                }
                else
                {
                    // default to 0 if bad value or not applicable
                    return 0;
                }
            }
        }

        public double PercentHRmax
        {
            get
            {
                double percent = (Info.AverageHeartRate - Athlete.GetHRrest(StartTimeLocal)) / (Athlete.GetHRmax(StartTimeLocal) - Athlete.GetHRrest(StartTimeLocal));

                // Enforce sensible limits on this range.  Sometimes AvgHR may return 0.
                percent = Math.Max(Math.Min(percent, 1.1), 0);

                return percent;
            }
        }

        public DateTime StartTimeLocal
        {
            get { return activity.StartTime.Add(activity.TimeZoneUtcOffset); }
        }

        public ActivityInfo Info
        {
            get { return ActivityInfoCache.Instance.GetInfo(activity); }
        }

        public bool IsRunningActivity
        {
            get
            {
                IActivityCategory category = activity.Category;

                while (category != null)
                {
                    if (category.Name == "Running")
                    {
                        return true;
                    }

                    category = category.Parent;
                }

                return false;
            }
        }

        /// <summary>
        /// Refresh logbook data
        /// </summary>
        public void Refresh()
        {
            if (IsRunningActivity)
            {
                double cache = this.EstimatedVDOT;
            }
        }
    }
}
