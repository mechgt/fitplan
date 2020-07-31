using System;
using System.Collections.Generic;
using System.Text;

namespace FitPlan.UI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using FitPlan.Controls;
    using FitPlan.Data;
    using FitPlan.Resources;
    using FitPlan.Schedule;
    using FitPlan.Settings;
    using Pabo.Calendar;
    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.Measurement;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Chart;
    using ZoneFiveSoftware.Common.Visuals.Util;

    class PlanOverviewColumns
    {
        private static IList<IListColumnDefinition> allColumns;

        internal static IList<IListColumnDefinition> AllColumns
        {
            get
            {
                if (allColumns == null)
                {
                    CreateAllColumns();
                }

                return allColumns;
            }
        }

        internal static IList<IListColumnDefinition> SelectedColumns
        {
            get
            {
                IList<IListColumnDefinition> selected = new List<IListColumnDefinition>();
                string[] info;

                // Get user settings
                foreach (string item in GlobalSettings.Main.OverviewColumns.Split(';'))
                {
                    info = item.Split('|');

                    // Build list of selected columns
                    foreach (ColumnDef column in AllColumns)
                    {
                        if (info[0].Equals(column.Id))
                        {
                            column.Width = int.Parse(info[1]);
                            selected.Add(column);
                            break;
                        }
                    }
                }

                return selected;
            }
        }

        private static void CreateAllColumns()
        {
            allColumns = new List<IListColumnDefinition>();

            // Load Phases
            allColumns.Add(new ColumnDef("Complete", string.Empty, null, 36, StringAlignment.Near));
            allColumns.Add(new ColumnDef("StartDate", CommonResources.Text.LabelDate, null, 80, StringAlignment.Near));
            allColumns.Add(new ColumnDef("Name", CommonResources.Text.LabelName, null, 100, StringAlignment.Near));

            // Load Workouts
            allColumns.Add(new ColumnDef("TotalTime", CommonResources.Text.LabelTime, null, 50, StringAlignment.Far));
            allColumns.Add(new ColumnDef("TotalDistanceMeters", CommonResources.Text.LabelDistance + " (" + Length.LabelAbbr(PluginMain.DistanceUnits) + ")", null, 60, StringAlignment.Far));
            allColumns.Add(new ColumnDef("Score", ScheduleControl.ScoreText, null, 50, StringAlignment.Far));
            allColumns.Add(new ColumnDef("Actual.TotalDistanceMeters", string.Format(Resources.Strings.Label_Actual, CommonResources.Text.LabelDistance) + " (" + Length.LabelAbbr(PluginMain.DistanceUnits) + ")", null, 60, StringAlignment.Far));
            allColumns.Add(new ColumnDef("Actual.TotalTime", string.Format(Resources.Strings.Label_Actual, CommonResources.Text.LabelTime), null, 50, StringAlignment.Far));
            allColumns.Add(new ColumnDef("Actual.Score", string.Format(Resources.Strings.Label_Actual, ScheduleControl.ScoreText), null, 50, StringAlignment.Far));
            allColumns.Add(new ColumnDef("Actual.DeltaDistanceMeters", string.Format(Resources.Strings.Label_Delta, CommonResources.Text.LabelDistance) + " (" + Length.LabelAbbr(PluginMain.DistanceUnits) + ")", null, 60, StringAlignment.Far));
            allColumns.Add(new ColumnDef("Actual.DeltaTime", string.Format(Resources.Strings.Label_Delta, CommonResources.Text.LabelTime), null, 50, StringAlignment.Far));
            allColumns.Add(new ColumnDef("ATL", Resources.Strings.Label_ATL, null, 35, StringAlignment.Far));
            allColumns.Add(new ColumnDef("CTL", Resources.Strings.Label_CTL, null, 35, StringAlignment.Far));
            allColumns.Add(new ColumnDef("TSB", Resources.Strings.Label_TSB, null, 35, StringAlignment.Far));
        }
    }
}
