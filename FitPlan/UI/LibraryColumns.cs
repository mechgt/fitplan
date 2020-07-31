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

    class LibraryColumns
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
                // TODO: Handle dynamic Library Columns
                return AllColumns;
            }
        }

        private static void CreateAllColumns()
        {
            allColumns = new List<IListColumnDefinition>();

            allColumns.Add(new ColumnDef("Name", string.Empty, null, 200, StringAlignment.Near));
            allColumns.Add(new ColumnDef("TotalTime", CommonResources.Text.LabelTime, null, 50, StringAlignment.Far));
            allColumns.Add(new ColumnDef("TotalDistanceMeters", CommonResources.Text.LabelDistance + " (" + Length.LabelAbbr(PluginMain.DistanceUnits) + ")", null, 60, StringAlignment.Far));
            allColumns.Add(new ColumnDef("Score", ScheduleControl.ScoreText, null, 50, StringAlignment.Far));
        }
    }
}
