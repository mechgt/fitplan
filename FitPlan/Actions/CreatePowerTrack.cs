using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Mechgt.Licensing;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Util;

namespace FitPlan.Actions
{
    class CreatePowerTrack : IAction
    {
        #region Fields

        private const string newLine = "\r\n";

        #endregion

        #region Constructors

        public CreatePowerTrack(IDailyActivityView view)
        {
        }

        public CreatePowerTrack(IActivityReportsView view)
        {
        }

        #endregion

        #region IAction Members

        public bool Enabled
        {
            get
            {
                // TODO: Determine if this is a running activity
                return true;
            }
        }

        public bool HasMenuArrow
        {
            get { return false; }
        }

        public Image Image
        {
            get
            {
                return Resources.Images.trainer_icon;
            }
        }

        public IList<string> MenuPath
        {
            get { return new List<string> { CommonResources.Text.LabelPower }; }
        }

        public void Refresh()
        {

        }

        public void Run(Rectangle rectButton)
        {
            IEnumerable<IActivity> activities = Utilities.GetActivities();

            if (!Licensing.IsActivated)
            {
                MessageDialog.Show(Resources.Strings.Text_EvalMessage, Resources.Strings.Label_FitPlan, MessageBoxButtons.OK);
            }

            foreach (IActivity activity in activities)
            {
                DialogResult result = DialogResult.Yes;

                if (activity.PowerWattsTrack != null)
                {
                    // Warn if replacing an existing track
                    result = MessageDialog.Show(Resources.Strings.Text_OverwritePowerTrack + newLine
                        + activity.StartTime.Add(activity.TimeZoneUtcOffset) + newLine
                        + CommonResources.Text.LabelAvgPower + ": " + activity.PowerWattsTrack.Avg.ToString("0") + " " + CommonResources.Text.LabelWatts,
                        Resources.Strings.Label_FitPlan + ": " + activity.StartTime.Add(activity.TimeZoneUtcOffset), System.Windows.Forms.MessageBoxButtons.YesNo);
                }

                if (result == DialogResult.Yes)
                {
                    INumericTimeDataSeries powerTrack = GetRunningPowerTrack(activity);

                    if (powerTrack != null)
                    {
                        activity.PowerWattsTrack = powerTrack;
                        activity.Notes = string.Format(Resources.Strings.Text_NotesMessage, Resources.Strings.Label_FitPlan, new PluginMain().Version) + newLine + newLine + activity.Notes;
                    }
                }
            }
        }

        public string Title
        {
            get
            {
                string title = Resources.Strings.Label_CreatePowerTrack + ": " + Resources.Strings.Label_Running;
                return title;
            }
        }

        public bool Visible
        {
            get
            {
                List<IActivity> activities = Utilities.GetActivities() as List<IActivity>;

                if (activities.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        internal static INumericTimeDataSeries GetRunningPowerTrack(IActivity activity)
        {
            double Ci, gp, grade, power;
            ITimeValueEntry<float> speed;
            double a0 = 1.68;   // minimum possible energy cost (J/m/kg)
            double gp0 = .184;  // descending g' of min energy cost (%)
            double a1 = 54.9;   // coefficient (J/m/kg)
            double a2 = -102;   // coefficient (J/m/kg)
            double a3 = 200;    // coefficient (J/m/kg)

            INumericTimeDataSeries gradeTrack = Utilities.GetGradeTrack(activity);
            INumericTimeDataSeries speedTrack = Utilities.GetSpeedTrack(activity);

            if (gradeTrack == null || gradeTrack.Count == 0 || speedTrack == null || speedTrack.Count == 0)
            {
                // Bad or invalid data
                return null;
            }

            INumericTimeDataSeries powerTrack = new NumericTimeDataSeries();
            ITimeValueEntry<float> lastPoint = null;

            // Define default weight if not specified
            float weight = PluginMain.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(activity.StartTime.Add(activity.TimeZoneUtcOffset)).WeightKilograms;

            if (float.IsNaN(weight))
            {
                if (PluginMain.GetApplication().Logbook.Athlete.Sex == AthleteSex.Female)
                {
                    weight = 55;
                }
                else
                {
                    weight = 80;
                }
            }

            // Create power track
            foreach (ITimeValueEntry<float> item in gradeTrack)
            {
                if (lastPoint != null)
                {
                    grade = item.Value;
                    speed = speedTrack.GetInterpolatedValue(gradeTrack.EntryDateTime(item));

                    if (speed != null)
                    {
                        // gp = g' (gprime)
                        gp = grade / Math.Sqrt(1 + grade * grade);
                        Ci = a0 + a1 * Math.Pow((gp + gp0), 2) + a2 * Math.Pow((gp + gp0), 4) + a3 * Math.Pow((gp + gp0), 6);
                        power = Ci * weight * speed.Value * GetRunningEfficiency(speed.Value);

                        // NOTE: Clip power at 0 and 1000
                        power = Math.Min(Math.Max(0, power), 1000);

                        powerTrack.Add(gradeTrack.EntryDateTime(item), (float)power);
                    }
                }

                lastPoint = item;
            }

#if Debug
            // TODO: Store power track in activity? NO... NOT automatically, ESP if an existing power track is available
            // activity.PowerWattsTrack = powerTrack;
            //string path = "C:\\STexports\\";

            //Utilities.ExportTrack(gradeTrack, path + activity.StartTime.Date.ToString("yyyy-MM-dd") + "-grade.csv");
            //Utilities.ExportTrack(speedTrack, path + activity.StartTime.Date.ToString("yyyy-MM-dd") + "-speed.csv");
            //Utilities.ExportTrack(powerTrack, path + activity.StartTime.Date.ToString("yyyy-MM-dd") + "-power.csv");
#endif
            return powerTrack;
        }

        /// <summary>
        /// Based off of a linear coorelation with velocity, get the cooresponding running energy efficiency
        /// </summary>
        /// <param name="speedMetersPerSecond">speed in m/s</param>
        /// <returns>Returns efficiency</returns>
        internal static double GetRunningEfficiency(double speedMetersPerSecond)
        {
            // Efficiency at velocity.  Increases linearly with velocity: .3 slowest running speed -> .6 @ 8.33 m/s
            double m = 0.04894f;
            double eff = (m * (speedMetersPerSecond - 2.2)) + .3;

            return eff;
        }

        #endregion
    }
}
