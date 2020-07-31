namespace FitPlan.Data
{
    using System;
    using System.Drawing;
    using FitPlan.Schedule;
    using FitPlan.Settings;
    using FitPlan.UI;
    using GarminFitnessPublic;
    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.Measurement;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Chart;
    using System.Collections.Generic;

    internal static class ChartData
    {
        #region Fields

        internal static readonly Color CtlColor = Color.Blue;
        internal static readonly Color CtltColor = Color.LightBlue;
        internal static readonly Color TsbColor = Color.DarkOliveGreen;

        private static INumericTimeDataSeries atlSeries;
        private static INumericTimeDataSeries ctlSeries;
        private static INumericTimeDataSeries tsbSeries;
        private static LineItem ctlTarget;
        private static DateTime startDate = DateTime.MaxValue;
        private static bool calculated;
        private static TrainingPlan plan;
        private static TreeNodeCollection libraryNodes;
        private static GradientFillObj highlightFocus;
        private static LineObj highlightToday;

        #endregion

        #region Properties

        internal static bool Calculated
        {
            get { return calculated; }
            set
            {
                if (!value)
                    calculated = false;
            }
        }

        internal static INumericTimeDataSeries ATL
        {
            get
            {
                if (!calculated)
                    RefreshChartData();

                return atlSeries;
            }
        }

        internal static INumericTimeDataSeries CTL
        {
            get
            {
                if (!calculated)
                    RefreshChartData();

                return ctlSeries;
            }
        }

        internal static INumericTimeDataSeries TSB
        {
            get
            {
                if (!calculated)
                    RefreshChartData();

                return tsbSeries;
            }
        }

        internal static LineItem TargetCTL
        {
            get
            {
                if (ctlTarget == null)
                {
                    ctlTarget = CreateCTLtarget();

                    if (plan != null)
                    {
                        foreach (Day day in plan.Days)
                            ctlTarget.AddPoint(new XDate(day.GetDate(plan.StartDate)), day.TargetCTL);
                    }
                }

                return ctlTarget;
            }
        }

        internal static TreeNodeCollection LibraryNodes
        {
            get
            {
                if (libraryNodes == null)
                    libraryNodes = GetLibraryNodes();

                return libraryNodes;
            }
        }

        internal static DateTime LogbookStartDate
        {
            get
            {
                if (startDate == DateTime.MaxValue && PluginMain.GetApplication().Logbook != null)
                {
                    startDate = DateTime.Today;

                    foreach (IActivity activity in PluginMain.GetApplication().Logbook.Activities)
                    {
                        if (activity.StartTime < startDate && 1900 < activity.StartTime.Year)
                        {
                            // Find earliest activity with just a little error checking
                            startDate = activity.StartTime.Add(activity.TimeZoneUtcOffset).Date;
                        }
                    }
                }

                return startDate;
            }
        }

        internal static GradientFillObj HighlightFocus
        {
            get
            {
                if (highlightFocus == null)
                {
                    double xToday = XDate.DateTimeToXLDate(DateTime.Today);
                    highlightFocus = new GradientFillObj(xToday - 1, 0, 2, 1, CoordType.XScaleYChartFraction);
                    Color[] colors = { Color.Silver, Color.Transparent, Color.Silver };
                    highlightFocus.Fill = new Fill(colors, 0);
                    highlightFocus.ZOrder = ZOrder.E_BehindCurves;
                    highlightFocus.IsVisible = false;
                }

                return highlightFocus;
            }
        }

        internal static LineObj HighlightToday
        {
            get
            {
                if (highlightToday == null)
                {
                    double xToday = XDate.DateTimeToXLDate(DateTime.Today);
                    highlightToday = new LineObj(Color.Red, xToday, 0, xToday, 1);
                    highlightToday.Location.CoordinateFrame = CoordType.XScaleYChartFraction;
                    highlightToday.IsXClippedToChartRect = true;
                    highlightToday.IsYClippedToChartRect = true;
                    highlightToday.ZOrder = ZOrder.E_BehindCurves;
                }

                return highlightToday;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get progress chart data (distance/time chart data points).
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="groupBy">Used for non-cumulative display modes.</param>
        /// <returns></returns>
        internal static ChartPointPairList GetPlanProgress(GlobalSettings.ProgressOption mode, GlobalSettings.GroupOption groupBy)
        {
            double value = 0;
            ChartPointPairList planTrack = new ChartPointPairList();

            if (plan == null) return planTrack;

            if (groupBy != GlobalSettings.GroupOption.Category)
                planTrack.Add(XDate.DateTimeToXLDate(plan.StartDate), value);

            WorkoutCollection workouts = plan.GetWorkouts();
            workouts.Sort();

            foreach (Workout workout in workouts)
            {
                switch (mode)
                {
                    case GlobalSettings.ProgressOption.CumulativeTime:
                        value += workout.TotalTime.TotalHours;
                        break;

                    case GlobalSettings.ProgressOption.CumulativeDistance:
                        value += Length.Convert(workout.TotalDistanceMeters, Length.Units.Meter, PluginMain.DistanceUnits);
                        break;

                    case GlobalSettings.ProgressOption.Time:
                        value = workout.TotalTime.TotalHours;
                        break;

                    case GlobalSettings.ProgressOption.Distance:
                        value = Length.Convert(workout.TotalDistanceMeters, Length.Units.Meter, PluginMain.DistanceUnits);
                        break;
                }

                if (groupBy == GlobalSettings.GroupOption.Category && !GlobalSettings.Main.IsProgressCumulative)
                {
                    // Category grouping (ordinal chart)
                    // X: activity category, Y: value, Z: major category, Tag: Name
                    IActivityCategory upperCat = GetUpperCategory(workout.Category);
                    planTrack.Add(Utilities.CategoryIndex.IndexOf(workout.Category), value, Utilities.CategoryIndex.IndexOf(upperCat), workout.Category.Name);
                }
                else
                {
                    // Time based grouping (date based chart: day, week, month, etc.)
                    planTrack.Add(XDate.DateTimeToXLDate(workout.StartDate), value);
                }
            }

            if (mode == GlobalSettings.ProgressOption.CumulativeDistance || mode == GlobalSettings.ProgressOption.CumulativeTime)
            {
                planTrack.Sort(SortType.XValues);
                planTrack.Add(XDate.DateTimeToXLDate(plan.EndDate), planTrack[planTrack.Count - 1].Y);
                planTrack.HandleDuplicates(true, ChartPointPairList.CombineAction.RemoveLow);
            }
            else if (groupBy == GlobalSettings.GroupOption.Category)
            {
                // X: Minor Category, Y: Value, Z: Major Category; combine and group activities into categories
                planTrack = planTrack.GroupBy(groupBy);
            }
            else
            {
                planTrack.Add(XDate.DateTimeToXLDate(plan.EndDate), 0);
                planTrack = planTrack.GroupBy(groupBy);
            }

            return planTrack;
        }

        /// <summary>
        /// Get progress chart data (distance/time chart data points).
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="groupBy">Used for non-cumulative display modes.</param>
        /// <returns></returns>
        internal static ChartPointPairList GetActualProgress(GlobalSettings.ProgressOption mode, GlobalSettings.GroupOption groupBy)
        {
            double value = 0;
            ChartPointPairList actualTrack = new ChartPointPairList();

            if (plan == null) return actualTrack;

            if (DateTime.Now < plan.StartDate) return actualTrack;

            if (groupBy != GlobalSettings.GroupOption.Category)
                actualTrack.Add(XDate.DateTimeToXLDate(plan.StartDate), value);

            foreach (IActivity activity in PluginMain.GetApplication().Logbook.Activities)
            {
                // Only display 'My Activities' and filter out future activities (possible placeholder activities)
                if (IsInCategory(activity, LogbookSettings.MyActivities) && activity.StartTime.Add(activity.TimeZoneUtcOffset) <= DateTime.Now)
                {
                    if (plan.StartDate <= activity.StartTime.Add(activity.TimeZoneUtcOffset) && activity.StartTime.Add(activity.TimeZoneUtcOffset) <= plan.EndDate)
                    {
                        // Time based grouping
                        switch (mode)
                        {
                            case GlobalSettings.ProgressOption.CumulativeTime:
                                // Sum later because activities are not presented in order
                                value = activity.TotalTimeEntered.TotalHours;
                                break;

                            case GlobalSettings.ProgressOption.CumulativeDistance:
                                // Sum later because activities are not presented in order
                                value = Length.Convert(activity.TotalDistanceMetersEntered, Length.Units.Meter, PluginMain.DistanceUnits);
                                break;

                            case GlobalSettings.ProgressOption.Time:
                                value = activity.TotalTimeEntered.TotalHours;
                                break;

                            case GlobalSettings.ProgressOption.Distance:
                                value = Length.Convert(activity.TotalDistanceMetersEntered, Length.Units.Meter, PluginMain.DistanceUnits);
                                break;
                        }

                        if (groupBy == GlobalSettings.GroupOption.Category && !GlobalSettings.Main.IsProgressCumulative)
                        {
                            // Category grouping (ordinal chart)
                            // X: activity category, Y: value, Z: major category, Tag: Name
                            IActivityCategory upperCat = GetUpperCategory(activity);
                            actualTrack.Add(Utilities.CategoryIndex.IndexOf(activity.Category), value, Utilities.CategoryIndex.IndexOf(upperCat), activity.Category.Name);
                        }
                        else
                        {
                            // Time based grouping (date based chart: day, week, month, etc.)
                            actualTrack.Add(XDate.DateTimeToXLDate(activity.StartTime.Add(activity.TimeZoneUtcOffset).Date), value);
                        }
                    }
                }
            }

            // Post-processing of collected data
            if (mode == GlobalSettings.ProgressOption.CumulativeDistance || mode == GlobalSettings.ProgressOption.CumulativeTime)
            {
                // Make values cumulative
                value = 0;
                actualTrack.Sort(SortType.XValues);
                for (int i = 0; i < actualTrack.Count; i++)
                {
                    value += actualTrack[i].Y;
                    actualTrack[i].Y = value;
                }

                // Add end date and handle multi-workout dates with 2 chart values on the same day
                actualTrack.Sort(SortType.XValues);
                actualTrack.Add(XDate.DateTimeToXLDate(DateTime.Now), value);
                actualTrack.HandleDuplicates(true, ChartPointPairList.CombineAction.RemoveLow);
            }
            else if (groupBy == GlobalSettings.GroupOption.Category)
            {
                // X: Minor Category, Y: Value, Z: Major Category; combine and group activities into categories
                actualTrack = actualTrack.GroupBy(groupBy);
            }
            else
            {
                // Add end date and handle multi-workout dates with 2 chart values on the same day
                actualTrack.Add(XDate.DateTimeToXLDate(DateTime.Now), 0);
                actualTrack = actualTrack.GroupBy(groupBy);
            }

            return actualTrack;
        }

        /// <summary>
        /// Get CTL LineItems.  If line exists in pane already, it will be returned with updated
        /// values.  Otherwise a new LineItem will be created and populated.
        /// </summary>
        /// <param name="pane">Chart to be used</param>
        internal static CurveList GetCTLlines(GraphPane pane)
        {
            // CTL chart
            PointPairList ctlTrackPast = new PointPairList();
            PointPairList ctlTrackFuture = new PointPairList();
            int index;
            LineItem curve;
            CurveList ctlCurves = new CurveList();

            foreach (ITimeValueEntry<float> item in ChartData.CTL)
            {
                XDate date = new XDate(ChartData.CTL.EntryDateTime(item).Date);

                if (date <= DateTime.Today)
                    ctlTrackPast.Add(date, item.Value);

                else
                    ctlTrackFuture.Add(date, item.Value);
            }

            if (ctlTrackPast.Count > 0 && ctlTrackFuture.Count > 0)
            {
                // Add joining point to make charts meet IF the charts need to meet
                ctlTrackPast.Add(new XDate(DateTime.Today), ChartData.CTL.GetInterpolatedValue(DateTime.Today).Value);
                ctlTrackFuture.Insert(0, new XDate(DateTime.Today), ChartData.CTL.GetInterpolatedValue(DateTime.Today).Value);
            }

            if (ctlTrackPast.Count > 0)
            {
                // Display past chart if it's populated
                index = pane.CurveList.IndexOf("CTLp");

                // Locate or create new LineItem
                if (index >= 0)
                {
                    curve = pane.CurveList[index] as LineItem;
                    curve.Points = ctlTrackPast;
                }
                else
                {
                    curve = new LineItem("CTLp", ctlTrackPast, CtlColor, SymbolType.None);
                    curve.Line.Width = 1f;
                    curve.Line.Fill.Type = FillType.Solid;
                    curve.Line.Fill.Color = Color.FromArgb(128, CtlColor);
                    curve.Line.IsAntiAlias = true;
                }

                ctlCurves.Add(curve);
            }

            if (ctlTrackFuture.Count > 0)
            {
                // Display future chart if it's populated
                index = pane.CurveList.IndexOf("CTLf");

                // Locate or create new LineItem
                if (index >= 0)
                {
                    curve = pane.CurveList[index] as LineItem;
                    curve.Points = ctlTrackFuture;
                }
                else
                {
                    curve = new LineItem("CTLf", ctlTrackFuture, CtlColor, SymbolType.None);
                    curve.Line.Width = 1f;
                    curve.Line.Fill.Type = FillType.Solid;
                    curve.Line.Fill.Color = Color.FromArgb(80, CtlColor);
                    curve.Line.IsAntiAlias = true;
                }

                ctlCurves.Add(curve);
            }

            return ctlCurves;
        }

        /// <summary>
        /// Get TSB LineItem.  If line exists in pane already, it will be returned with updated
        /// values.  Otherwise a new LineItem will be created and populated.
        /// </summary>
        /// <param name="pane">Chart to be used</param>
        /// <returns></returns>
        internal static LineItem GetTSBline(GraphPane pane)
        {
            LineItem curve;
            PointPairList track = new PointPairList();
            INumericTimeDataSeries series = ChartData.TSB;

            foreach (ITimeValueEntry<float> item in series)
            {
                XDate date = new XDate(series.EntryDateTime(item));
                track.Add(date, item.Value);
            }

            int index = pane.CurveList.IndexOf("TSB");

            // Locate or create new LineItem
            if (index >= 0)
            {
                curve = pane.CurveList[index] as LineItem;
                curve.Points = track;
            }
            else
            {
                curve = new LineItem("TSB", track, TsbColor, SymbolType.None);
                curve.Line.Width = 1f;
                curve.Line.Fill.Type = FillType.None;
                curve.Line.IsAntiAlias = true;
                curve.IsY2Axis = true;
            }

            return curve;
        }

        /// <summary>
        /// Get chart axis label for progress charts.
        /// "Distance (mi.)" for example.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        internal static string GetProgressLabel(GlobalSettings.ProgressOption mode)
        {
            switch (mode)
            {
                case GlobalSettings.ProgressOption.CumulativeTime:
                    return string.Format("{0} ({1})", CommonResources.Text.LabelTime, Time.LabelAbbr(Time.TimeRange.Hour));

                case GlobalSettings.ProgressOption.CumulativeDistance:
                    return string.Format("{0} ({1})", CommonResources.Text.LabelDistance, Length.LabelAbbr(PluginMain.DistanceUnits));

                case GlobalSettings.ProgressOption.Time:
                    return string.Format("{0} ({1})", CommonResources.Text.LabelTime, Time.LabelAbbr(Time.TimeRange.Hour));

                case GlobalSettings.ProgressOption.Distance:
                    return string.Format("{0} ({1})", CommonResources.Text.LabelDistance, Length.LabelAbbr(PluginMain.DistanceUnits));

                default:
                    throw new NotImplementedException();
            }
        }

        internal static LibraryNode GetLibraryNode(object item, TreeNodeCollection nodes)
        {
            if (item == null) return null;

            // Loops through top parent items
            foreach (LibraryNode node in nodes)
            {
                if (node.Element == item)
                {
                    return node;
                }

                // Recursively search all child nodes
                LibraryNode value = SearchChildren(node, item);
                if (value != null)
                {
                    return value;
                }
            }

            return null;
        }

        /// <summary>
        /// Recursively search child nodes for a matching object
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="matchItem"></param>
        /// <returns></returns>
        internal static LibraryNode SearchChildren(LibraryNode parent, object matchItem)
        {
            foreach (LibraryNode child in parent.Children)
            {
                if (child.Element == matchItem)
                {
                    return child;
                }

                LibraryNode value = SearchChildren(child, matchItem);

                if (value != null)
                {
                    return value;
                }
            }

            return null;
        }

        internal static void SetTrainingPlan(TrainingPlan plan)
        {
            ChartData.plan = plan;
            ctlTarget = null;
            calculated = false;
            RefreshChartData();
        }

        /// <summary>
        /// 
        /// </summary>
        internal static void RefreshChartData()
        {
            if (plan != null && !calculated)
            {
                ClearCharts();
                RefreshHistoricalTL(ChartData.LogbookStartDate);
                RefreshProjectedTL();
                RefreshTargetCTL();

                calculated = true;
            }
            else if (plan == null)
            {
                ClearCharts();
            }
        }

        internal static void RefreshTargetRampColors()
        {
            for (int i = 2; i <= TargetCTL.NPts; i++)
            {
                float ptsPerDay = (float)((TargetCTL[i - 1].Y - TargetCTL[i - 2].Y) / (XDate.XLDateToDateTime(TargetCTL[i - 1].X) - XDate.XLDateToDateTime(TargetCTL[i - 2].X)).TotalDays);
                TargetCTL[i - 2].ColorValue = GetRampColorValue(ptsPerDay);
            }
        }

        internal static void RefreshTargetCTL()
        {
            if (plan != null)
            {
                TargetCTL.Clear();
                foreach (Day day in plan.Days)
                    ctlTarget.AddPoint(new XDate(day.GetDate(plan.StartDate)), day.TargetCTL);
            }
        }

        /// <summary>
        /// Clear ATL, CTL, & TSB chart values
        /// </summary>
        internal static void ClearCharts()
        {
            atlSeries = new NumericTimeDataSeries();
            ctlSeries = new NumericTimeDataSeries();
            tsbSeries = new NumericTimeDataSeries();
        }

        /// <summary>
        /// Populate ATL & CTL dataseries for historical values
        /// (only available if TL installed)
        /// </summary>
        /// <param name="start"></param>
        internal static void RefreshHistoricalTL(DateTime start)
        {
            if (TrainingLoadPlugin.IsInstalled)
            {
                float atl = 0, ctl = 0;
                DateTime date = start.Date;

                while (date <= DateTime.Now)
                {
                    float tsb = ctl - atl;
                    atl = TrainingLoadPlugin.GetATL(date);
                    ctl = TrainingLoadPlugin.GetCTL(date);

                    atlSeries.Add(date, atl);
                    ctlSeries.Add(date, ctl);
                    tsbSeries.Add(date, tsb);

                    date = date.AddDays(1);
                }
            }
            else
            {
                // Handle if TL is not installed
            }
        }

        /// <summary>
        /// Calculate TL values starting today and starting from specific values
        /// </summary>
        /// <param name="start"></param>
        /// <param name="ctl"></param>
        /// <param name="atl"></param>
        internal static void RefreshProjectedTL()
        {
            if (!Data.TrainingLoadPlugin.IsInstalled || plan == null)
            {
                // Only supported if TL is installed
                return;
            }

            float atl, ctl;
            DateTime date = DateTime.UtcNow.Date;

            foreach (IActivity activity in PluginMain.GetApplication().Logbook.Activities)
            {
                if (activity.StartTime.Add(activity.TimeZoneUtcOffset).Date == DateTime.Today)
                {
                    date = DateTime.Today.AddDays(1);
                    break;
                }
            }

            float tsb, score, atl_y, ctl_y;
            ctl = TrainingLoadPlugin.GetCTL(date.AddDays(-1));
            atl = TrainingLoadPlugin.GetATL(date.AddDays(-1));

            // Iterate through all dates in the plan
            while (date < plan.EndDate)
            {
                // Setup
                atl_y = atl;
                ctl_y = ctl;
                score = 0;

                // TSB (Displayed as TSB at the beginning of the day)
                tsb = ctl - atl;

                // Compiled score for the day
                foreach (Workout workout in plan.GetWorkouts(date))
                {
                    if (DateTime.Today == workout.StartDate.Date && workout.Actual.Score != 0)
                    {
                        // Today:  Count actual value
                        continue;
                    }

                    score += workout.Score;
                }

                // ATL
                atl = atl_y + ((score - atl_y) / Data.TrainingLoadPlugin.TCa);

                // CTL
                ctl = ctl_y + ((score - ctl_y) / Data.TrainingLoadPlugin.TCc);

                // Store values
                atlSeries.Add(date, atl);
                ctlSeries.Add(date, ctl);
                tsbSeries.Add(date, tsb);

                // Next day
                date = date.AddDays(1);
            }
        }

        internal static void RefreshLibraryNodes()
        {
            libraryNodes = GetLibraryNodes();
        }

        internal static double GetRampColorValue(float ptsPerDay)
        {
            // High and Low ramp settings values are stored in pts / week.
            double hi = LogbookSettings.Main.CTLRampHighLim / 7f;
            double lo = LogbookSettings.Main.CTLRampLowLim / 7f;
            double percent = (ptsPerDay - lo) / (hi - lo);
            return percent;
        }

        /// <summary>
        /// Checks for activity to be included in default MyActivities category or subcategory.
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        internal static bool IsInCategory(IActivity activity)
        {
            return IsInCategory(activity, LogbookSettings.MyActivities);
        }

        /// <summary>
        /// Determine if the activitiy is in a particular category or subcategory.
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        internal static bool IsInCategory(IActivity activity, IActivityCategory category)
        {
            IActivityCategory activityCat = activity.Category;

            while (activityCat != category && activityCat.Parent != null)
            {
                activityCat = activityCat.Parent;
            }

            if (activityCat == LogbookSettings.MyActivities)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static IActivityCategory GetUpperCategory(IActivity activity)
        {
            return GetUpperCategory(activity.Category);
        }

        internal static IActivityCategory GetUpperCategory(IActivityCategory category)
        {
            while (category.Parent != null)
            {
                if (category.Parent.Parent == null)
                    return category;
                else
                    category = category.Parent;
            }

            // Should only land here for top level activities (My Activities, My Friend's Activities, etc.)
            return category;
        }

        /// <summary>
        /// Get target CTL LineItem.  If line exists in pane already, it will be returned with updated
        /// values.  Otherwise a new LineItem will be created and populated.
        /// </summary>
        /// <param name="pane">Chart to be used</param>
        /// <returns></returns>
        private static LineItem CreateCTLtarget()
        {
            ctlTarget = new LineItem("CTLt", new PointPairList(), ChartData.CtltColor, SymbolType.Diamond);
            ctlTarget.Line.Width = 2f;
            ctlTarget.Line.Fill.Type = FillType.None;
            ctlTarget.Line.IsAntiAlias = true;
            ctlTarget.Symbol.Fill.Color = Color.Black;
            ctlTarget.Symbol.IsVisible = GlobalSettings.Main.EnableCTLEdit;

            // 'GradientFill' fills the line in variuos colors
            Color green = Color.FromArgb(106, 255, 0);
            Color yellow = Color.Yellow;
            Color red = Color.FromArgb(255, 33, 0);
            ctlTarget.Line.GradientFill = new Fill(green, yellow, red); // Green = low numbers, Red = high numbers
            ctlTarget.Line.GradientFill.Type = FillType.GradientByColorValue;
            ctlTarget.Line.GradientFill.RangeMin = 0;
            ctlTarget.Line.GradientFill.RangeMax = 1;
            ctlTarget.Tag = "CTLtarg";

            return ctlTarget;
        }

        private static void RemoveEmptyChildren(LibraryNode parent)
        {
            int i = 0;
            while (i < parent.Children.Count)
            {
                LibraryNode child = parent.Children[i] as LibraryNode;
                if (child.Children.Count > 0)
                {
                    RemoveEmptyChildren(child);
                }

                if (child.IsCategory && child.Children.Count == 0)
                {
                    parent.Children.Remove(child);
                }
                else
                {
                    i++;
                }
            }
        }

        private static TreeNodeCollection GetLibraryNodes()
        {
            // Begin by collecting the categories:
            TreeNodeCollection nodes = new TreeNodeCollection(LogbookSettings.GetCategoryNodes());

            // Display Garmin Fitness Workouts
            if (GarminFitness.Manager.GarminManager != null)
            {
                IPublicWorkoutManager manager = GarminFitness.Manager.GarminManager;

                foreach (IPublicWorkout workout in manager.Workouts)
                {
                    LibraryNode categoryNode = GetLibraryNode(workout.Category, nodes);
                    if (categoryNode != null)
                    {
                        WorkoutDefinition def = LogbookSettings.GetGarminUserData(workout);
                        if (def == null)
                        {
                            def = new WorkoutDefinition(workout);
                            def.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(template_PropertyChanged);
                        }

                        LibraryNode child = new LibraryNode(categoryNode, def);
                        categoryNode.Children.Insert(categoryNode.FirstCategoryChild, child);
                    }
                }
            }

            // Add user definitions
            foreach (WorkoutDefinition template in LogbookSettings.Main.TemplateDefinitions)
            {
                if (!template.IsGarminWorkout)
                {
                    LibraryNode node = GetLibraryNode(template.Category, nodes);
                    if (node != null)
                    {
                        LibraryNode child = new LibraryNode(node, template);

                        int i;
                        for (i = 0; i < node.FirstCategoryChild; i++)
                        {
                            int compare = child.CompareTo(node.Children[i]);

                            if (compare < 0)
                                break;
                        }
                        node.Children.Insert(i, child);
                    }
                }
            }

            // Hide child workouts if necessary
            if (!GlobalSettings.Main.ShowEmptyFolders)
            {
                foreach (LibraryNode node in nodes)
                {
                    RemoveEmptyChildren(node);
                }
            }

            return nodes;
        }

        private static void template_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Category")
            {
                RefreshLibraryNodes();
            }
        }

        #endregion
    }
}
