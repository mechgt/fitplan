namespace FitPlan.Settings
{
    using System.ComponentModel;
    using System.Xml.Serialization;

    /// <summary>
    /// User settings
    /// </summary>
    [XmlRootAttribute(ElementName = "FitPlan", IsNullable = false)]
    public class GlobalSettings
    {
        #region Fields

        internal static event PropertyChangedEventHandler PropertyChanged;

        private static GlobalSettings main;
        private static bool showPhaseSummary = true;
        private static bool showCTLTarget = false;
        private static bool showChartPhases = true;
        private static bool showEmptyFolders = true;
        private static bool showGarminWorkouts = true;
        private static bool showFitPlanWorkouts = true;
        private static bool enableCTLedit = false;
        private static bool maxChart;
        private static string folderPath;
        private static string overviewColumns;
        private static ChartOption chartOption;
        private static ProgressOption progressChart;
        private static TreeOption treeType;
        private static GroupOption groupBy;

        #endregion

        #region Enumerations

        public enum ChartOption
        {
            TrainingLoad,
            Progress
        }

        public enum TreeOption
        {
            TrainingPlan,
            Library
        }

        public enum ProgressOption
        {
            CumulativeTime,
            CumulativeDistance,
            Time,
            Distance
        }

        public enum GroupOption
        {
            Day,
            Week,
            Month,
            Category
        }

        #endregion

        #region Properties

        public TreeOption TreeType
        {
            get { return treeType; }
            set
            {
                if (treeType != value)
                {
                    treeType = value;
                    RaisePropertyChanged("TreeType");
                }
            }
        }

        public ChartOption ChartType
        {
            get { return chartOption; }
            set { chartOption = value; }
        }

        public ProgressOption ProgressChart
        {
            get { return progressChart; }
            set { progressChart = value; }
        }
        
        public bool IsChartMaximized
        {
            get { return maxChart; }
            set { maxChart = value; }
        }

        /// <summary>
        /// Show CTL target line on the Training Load chart.
        /// </summary>
        public bool ShowCTLTarget
        {
            get { return showCTLTarget; }
            set
            {
                if (showCTLTarget != value)
                {
                    showCTLTarget = value;
                    RaisePropertyChanged("ShowCTLTarget");
                }
            }
        }

        /// <summary>
        /// Show phase colors and names on chart background.
        /// </summary>
        public bool ShowChartPhases
        {
            get { return showChartPhases; }
            set
            {
                if (showChartPhases != value)
                {
                    showChartPhases = value;
                    RaisePropertyChanged("ShowChartPhases");
                }
            }
        }

        /// <summary>
        /// Gets or sets the non-cumulative chart group style.
        /// </summary>
        public GroupOption GroupBy
        {
            get { return groupBy; }
            set
            {
                if (groupBy != value)
                {
                    groupBy = value;
                    RaisePropertyChanged("GroupBy");
                }
            }
        }

        /// <summary>
        /// Gets a value indicating if the chart's x-axis should be date based.
        /// For example, it would return false if the x-axis should be ordinal.
        /// </summary>
        [XmlIgnore]
        public bool IsDateBasedChart
        {
            get
            {
                return ChartType == ChartOption.TrainingLoad || IsProgressCumulative || GroupBy != GroupOption.Category;
            }
        }

        /// <summary>
        /// Enable CTL editing
        /// </summary>
        [XmlIgnore]
        public bool EnableCTLEdit
        {
            get { return enableCTLedit; }
            set
            {
                if (enableCTLedit != value)
                {
                    enableCTLedit = value;
                    RaisePropertyChanged("EnableCTLEdit");
                }
            }
        }

        public string FolderPath
        {
            get
            {
                if (string.IsNullOrEmpty(folderPath))
                {
                    return PluginMain.LogbookFolder;
                }
                return folderPath;
            }
            set { folderPath = value; }
        }

        public string OverviewColumns
        {
            get
            {
                if (string.IsNullOrEmpty(overviewColumns))
                {
                    overviewColumns = "Complete|36;StartDate|80;Name|100;TotalTime|50;TotalDistanceMeters|60;Score|50;Actual.TotalDistanceMeters|60;Actual.TotalTime|50";
                }

                return overviewColumns.TrimEnd(';');
            }

            set { overviewColumns = value; }
        }

        public bool ShowPhaseSummary
        {
            get { return showPhaseSummary; }
            set
            {
                if (showPhaseSummary != value)
                {
                    showPhaseSummary = value;
                    RaisePropertyChanged("ShowPhaseSummary");
                }
            }
        }

        public bool ShowEmptyFolders
        {
            get { return showEmptyFolders; }
            set { showEmptyFolders = value; }
        }

        public bool ShowGarminWorkouts
        {
            get { return showGarminWorkouts; }
            set { showGarminWorkouts = value; }
        }

        public bool ShowFitPlanWorkouts
        {
            get { return showFitPlanWorkouts; }
            set { showFitPlanWorkouts = value; }
        }

        [XmlIgnore]
        public bool IsCalendarMaximized
        {
            get { return !maxChart; }
            set { maxChart = !value; }
        }

        [XmlIgnore]
        public bool IsProgressCumulative
        {
            get
            {
                switch (ProgressChart)
                {
                    case ProgressOption.CumulativeTime:
                        return true;

                    case ProgressOption.CumulativeDistance:
                        return true;

                    case ProgressOption.Time:
                        return false;

                    case ProgressOption.Distance:
                        return false;

                    default:
                        return false;
                }
            }
        }

        #endregion

        [XmlIgnore]
        internal static GlobalSettings Main
        {
            get
            {
                if (main == null)
                {
                    main = new GlobalSettings();
                }

                return main;
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
