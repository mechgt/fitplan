namespace FitPlan.Schedule
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml.Serialization;
    using FitPlan.UI;
    using Pabo.Calendar;
    using ZoneFiveSoftware.Common.Data.Measurement;
    using ZoneFiveSoftware.Common.Visuals.Chart;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using FitPlan.Data;
    using System.Collections.Generic;

    /// <summary>
    /// A Phase contains a set of workouts that repeat with some regularity throughout the phase.
    /// It can also contain single non-repeating workouts, a race or specialty workout for instance.
    /// </summary>
    [XmlRootAttribute(ElementName = "Phase", IsNullable = false)]
    public class Phase : IComparable, IComparable<Phase>
    {
        #region Fields

        public event PropertyChangedEventHandler PropertyChanged;
        public event DateRangeEventHandler StartDateChanging;

        private WorkoutCollection workouts;
        private PhaseSummary averageSummary;
        private PhaseSummary dailySummary;
        private PhaseSummary totalSummary;
        private DateItem phaseItem = new DateItem();
        private GradientFillObj grad;
        private TextObj text;
        private DateTime start, end;
        private FitPlanNode node;
        private FitPlanNode averageNode;
        private FitPlanNode dailyNode;
        private FitPlanNode totalNode;
        private Color displayColor;
        private string name;
        private Guid id;
        private bool modified;

        #endregion

        #region Constructor

        /// <summary>
        /// Used only for XML serialization
        /// </summary>
        public Phase()
        {
            workouts = new WorkoutCollection();
            workouts.CollectionChanged += workouts_CollectionChanged;

            totalSummary = new PhaseSummary(this, PhaseSummary.SummaryType.Totals);
            averageSummary = new PhaseSummary(this, PhaseSummary.SummaryType.Avg);
            dailySummary = new PhaseSummary(this, PhaseSummary.SummaryType.Daily);

            totalNode = new FitPlanNode(null, totalSummary);
            averageNode = new FitPlanNode(null, averageSummary);
            dailyNode = new FitPlanNode(null, dailySummary);

            node = new FitPlanNode(null, this);
        }

        /// <summary>
        /// Create a new instance of a Phase
        /// </summary>
        /// <param name="name"></param>
        /// <param name="start"></param>
        /// <param name="numDays"></param>
        /// <param name="color"></param>
        public Phase(string name, DateTime start, int numDays, Color color)
            : this()
        {
            StartDate = start;
            EndDate = StartDate.AddDays(numDays - 1);
            Name = name;
            DisplayColor = color;
            id = Guid.NewGuid();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets workout start date.  This is the date used if this is a single stand-alone workout.
        /// </summary>
        public DateTime StartDate
        {
            get { return start; }
            set
            {
                if (start != value)
                {
                    RaiseStartDatePropertyChanging(start, value);

                    TimeSpan offset = value - StartDate;
                    bool intoPast = offset < TimeSpan.Zero;
                    if (intoPast)
                    {
                        // Set StartDate prior to moving workouts to keep workouts in Phase while moving into past.
                        // If moving into future, move workouts first, then set phase StartDate.
                        start = value;
                    }

                    int i = 0;
                    while (i < this.Workouts.Count)
                    {
                        Workout workout = this.Workouts[i];

                        // Linked child workouts self manage.  Only update parent workouts.
                        if ((!workout.IsChild || !workout.IsLinked) && !workout.DateLocked)
                        {
                            if (intoPast)
                            {
                                // Moving into past
                                // Note the order of setting the dates below (start then end)
                                workout.StartDate = workout.StartDate.Add(offset);
                                if (workout.StartDate <= workout.EndDate.Add(offset))
                                    workout.EndDate = workout.EndDate.Add(offset);
                            }
                            else
                            {
                                // Moving into future.  May truncate workouts at end of phase.
                                if (this.EndDate < workout.StartDate.Add(offset))
                                {
                                    // Truncate workout.
                                    RemoveWorkout(workout);
                                    continue;
                                }
                                else if (this.EndDate <= workout.EndDate.Add(offset))
                                {
                                    // Shorten repeat period
                                    workout.EndDate = this.EndDate;
                                    workout.StartDate = workout.StartDate.Add(offset);
                                }
                                else
                                {
                                    // Simply slide the workouts forward.  No modification required.
                                    // Note the order of setting the dates below (end then start)
                                    workout.EndDate = workout.EndDate.Add(offset);
                                    workout.StartDate = workout.StartDate.Add(offset);
                                }
                            }
                        }
                        i++;
                    }

                    start = value; // Only changes when moving into future, previously set if moving into past.
                    phaseItem.Date = start;

                    // Update gradient and title placement
                    GradientObj.Location = new Location(xStart, 0, xEnd - xStart + 1, 1, CoordType.XScaleYChartFraction, AlignH.Left, AlignV.Top);
                    TitleObj.Location = new Location(xStart + (xEnd - xStart + 1) / 2, .01, CoordType.XScaleYChartFraction, AlignH.Center, AlignV.Top);

                    phaseItem.Range = this.EndDate;
                    RaisePropertyChanged("StartDate");
                }
            }
        }

        /// <summary>
        /// Gets or sets the last instance of this workout.
        /// </summary>
        public DateTime EndDate
        {
            get { return end; }
            set
            {
                if (end != value)
                {
                    TimeSpan offset = value - EndDate;

                    // Cannot set end before start date
                    if (value < start)
                        value = start;

                    int i = 0;
                    while (i < this.Workouts.Count)
                    {
                        Workout workout = this.workouts[i];

                        if (value < workout.StartDate)
                        {
                            // Remove workouts that are truncated
                            RemoveWorkout(workout);
                        }
                        else if (workout.IsRepeating && this.EndDate <= workout.EndDate)
                        {
                            // Extend workouts that end at the same time the phase ends
                            workout.EndDate = workout.EndDate.Add(offset);
                            i++;
                        }
                        else if (value < workout.EndDate)
                        {
                            // Do not allow workouts to extend past the end of the phase                            
                            workout.EndDate = value;
                            i++;
                        }
                        else
                        {
                            i++;
                        }
                    }

                    end = value;

                    // Update gradient and title placement
                    GradientObj.Location = new Location(xStart, 0, xEnd - xStart + 1, 1, CoordType.XScaleYChartFraction, AlignH.Left, AlignV.Top);
                    TitleObj.Location = new Location(xStart + (xEnd - xStart + 1) / 2, .01, CoordType.XScaleYChartFraction, AlignH.Center, AlignV.Top);

                    phaseItem.Range = end;
                    RaisePropertyChanged("EndDate");
                }
            }
        }

        /// <summary>
        /// Gets or sets a unique id representing this phase
        /// </summary>
        public string ReferenceId
        {
            get { return id.ToString("D"); }
            set
            {
                if (id.ToString() != value)
                {
                    id = new Guid(value);
                    RaisePropertyChanged("ReferenceId");
                }
            }
        }

        /// <summary>
        /// Gets a list of all workout definitions.  This collection includes 
        /// both child and parent Workouts.  For a list of only the parent
        /// Workouts, see GetParentWorkouts().
        /// </summary>
        public WorkoutCollection Workouts
        {
            get { return workouts; }
            set
            {
                if (workouts != value)
                {
                    workouts = value;
                    RaisePropertyChanged("Workouts");
                }
            }
        }

        /// <summary>
        /// Workout Name
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    text.Text = name;
                    RaisePropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// Color to be displayed on the calendar
        /// </summary>
        [XmlIgnore]
        public Color DisplayColor
        {
            get { return displayColor; }
            set
            {
                if (displayColor != value)
                {
                    displayColor = value;
                    phaseItem.BackColor1 = displayColor;

                    // Setup gradient for each phase
                    Color[] colors = { displayColor, Color.Transparent };
                    GradientObj.Fill = new Fill(colors, 90);

                    RaisePropertyChanged("DisplayColor");
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlElement("DisplayColor")]
        public string XMLDisplayColor
        {
            get
            {
                return ColorTranslator.ToHtml(DisplayColor);
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    displayColor = Color.Tomato;
                    return;
                }

                DisplayColor = ColorTranslator.FromHtml(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public FitPlanNode Node
        {
            get { return node; }
        }

        /// <summary>
        /// Summary Node: Phase Average
        /// </summary>
        [XmlIgnore]
        public FitPlanNode AverageNode
        {
            get { return averageNode; }
        }

        /// <summary>
        /// Summary Node: Daily summary node.
        /// </summary>
        [XmlIgnore]
        public FitPlanNode DailyNode
        {
            get { return dailyNode; }
        }

        /// <summary>
        /// Summary Node: Phase Total
        /// </summary>
        [XmlIgnore]
        public FitPlanNode TotalNode
        {
            get { return totalNode; }
        }

        /// <summary>
        /// True if this phase has already been completed (end date is in the past).
        /// </summary>
        [XmlIgnore]
        public bool Complete
        {
            get
            {
                return DateTime.Now.ToLocalTime().Date > EndDate;
            }
        }

        /// <summary>
        /// Total days in this Phase
        /// </summary>
        [XmlIgnore]
        public int TotalDays
        {
            get { return (EndDate - StartDate).Days + 1; }
        }

        /// <summary>
        /// Gets the calendar item representing the phase.  This is a single item 
        /// that will repeat daily and contain proper coloration.
        /// </summary>
        [XmlIgnore]
        public DateItem CalendarItem
        {
            get
            {
                phaseItem.Date = StartDate;
                phaseItem.Range = EndDate;
                phaseItem.Pattern = mcDayInfoRecurrence.Daily;

                phaseItem.BackColor1 = DisplayColor;
                phaseItem.GradientMode = mcGradientMode.ForwardDiagonal;
                phaseItem.Tag = "Phase" + ReferenceId;

                return phaseItem;
            }
        }

        /// <summary>
        /// Gradient object highlighting the phase date range on the chart.
        /// </summary>
        [XmlIgnore]
        public GradientFillObj GradientObj
        {
            get
            {
                if (grad == null)
                {
                    grad = new GradientFillObj(xStart, 0, xEnd - xStart + 1, 1, CoordType.XScaleYChartFraction);
                    grad.Tag = "G" + ReferenceId;
                }

                return grad;
            }
        }

        /// <summary>
        /// Phase name above the gradient on the chart.
        /// </summary>
        [XmlIgnore]
        public TextObj TitleObj
        {
            get
            {
                if (text == null)
                {
                    // Create new Text Object
                    text = new TextObj(Name, xStart + (xEnd - xStart + 1) / 2, .01, CoordType.XScaleYChartFraction, AlignH.Center, AlignV.Top);
                    text.Tag = "T" + ReferenceId;
                    text.FontSpec.Border.IsVisible = false;
                    text.FontSpec.Fill.IsVisible = false;
                    text.IsXClippedToChartRect = true;
                    text.IsYClippedToChartRect = true;
                }

                return text;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if this object has been modified from original save state.
        /// If set to false, all objects contained by this one will be set to unmodified as well.
        /// </summary>
        [XmlIgnore]
        internal bool Modified
        {
            get
            {
                if (modified)
                {
                    return modified;
                }
                else
                {
                    foreach (Workout item in Workouts)
                    {
                        if (item.Modified)
                        {
                            return item.Modified;
                        }
                    }
                }

                return false;
            }
            set
            {
                modified = value;
                if (modified == false)
                {
                    foreach (Workout item in Workouts)
                    {
                        item.Modified = value;
                    }
                }
                else
                {
                    PluginMain.GetApplication().Logbook.Modified = true;
                }
            }
        }

        [XmlIgnore]
        internal double xStart
        {
            get { return XDate.DateTimeToXLDate(start); }
        }

        [XmlIgnore]
        internal double xEnd
        {
            get { return XDate.DateTimeToXLDate(end); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the custom formatted text with proper decimal places, etc.
        /// </summary>
        /// <param name="column">Column defines the property value to get</param>
        /// <returns>Returns custom formatted string ready for display to user.</returns>
        public string GetFormattedText(string Id)
        {
            return GetFormattedText(this, Id);
        }

        /// <summary>
        /// Gets the custom formatted text with proper decimal places, etc.
        /// </summary>
        /// <param name="activity">Activity containing value</param>
        /// <param name="column">Column defines the property value to get</param>
        /// <returns>Returns custom formatted string ready for display to user.</returns>
        public static string GetFormattedText(Phase phase, string Id)
        {
            Type phaseType = typeof(Phase);  // Used to collect property value from activity
            string text = null;                         // text to display in cell (if defined)

            // Create custom display text
            if (Id == "StartDate" ||
                Id == "EndDate")
            {
                // 9/11/2010
                DateTime value = (DateTime)phaseType.GetProperty(Id).GetValue(phase, null);

                if (value == DateTime.MinValue || value == null)
                {
                    text = string.Empty;
                }
                else
                {
                    text = value.ToString("d", CultureInfo.CurrentCulture);
                }
            }
            else
            {
                // Default
                PropertyInfo info = phaseType.GetProperty(Id);

                if (info != null)
                {
                    object value = info.GetValue(phase, null);
                    text = value.ToString();
                }
            }

            return text;
        }

        #region IComparable<Phase> Members

        public int CompareTo(object value)
        {
            Phase phase = value as Phase;

            if (phase != null)
                return this.CompareTo(phase);

            return 0;
        }

        public int CompareTo(Phase other)
        {
            return this.StartDate.CompareTo(other.StartDate);
        }

        #endregion

        /// <summary>
        /// Returns all parent workouts in this Phase in a new WorkoutCollection.
        /// Child workouts are not included in this set.
        /// </summary>
        /// <returns></returns>
        public WorkoutCollection GetParentWorkouts()
        {
            WorkoutCollection parents = new WorkoutCollection();

            foreach (Workout workout in Workouts)
            {
                if (!workout.IsChild && workout.IsRepeating)
                {
                    parents.Add(workout);
                }
            }

            return parents;
        }

        /// <summary>
        /// Add a workout to this Phase
        /// </summary>
        /// <param name="workout"></param>
        public void AddWorkout(Workout workout)
        {
            // Add parent workout
            workouts.Add(workout);

            // Define instanced/child workouts
            if (workout.IsRepeating && workout.PeriodDays != 0)
            {
                // Create list of repeating workouts
                DateTime date = workout.StartDate.AddDays(workout.PeriodDays);

                while (date <= EndDate)
                {
                    // Add child workouts
                    Workout child = new Workout(date, workout);
                    workouts.Add(child);
                    date = date.AddDays(workout.PeriodDays);
                }
            }

            workouts.Sort();

            RaisePropertyChanged("Workouts");
        }

        /// <summary>
        /// Remove workout (including all child workouts if this is a parent workout) from this Phase.
        /// Workouts are removed and the "Workouts" PropertyChanged event is raised.
        /// </summary>
        /// <param name="workout">Workout to be removed</param>
        public void RemoveWorkout(Workout workout)
        {
            // Remove children first
            int i = 0;
            while (i < workouts.Count)
            {
                if (workouts[i].ParentId == workout.ReferenceId)
                    workouts.Remove(workouts[i]);
                else
                    i++;
            }

            // Remove from phase instance list
            if (workouts.Contains(workout))
                workouts.Remove(workout);

            RaisePropertyChanged("Workouts");
        }

        /// <summary>
        /// Gets a value indicating if modifying the end of the Phase to this new date
        /// will cause any workouts to be DELETED.  If includeChildren is true, this 
        /// evaluates against ALL workouts; if includeChildren is false, this only
        /// checks for parents that will be permanently removed.  Child workouts that
        /// are removed may be recovered by adjusting the parent's EndDate causing
        /// additional repeating children to be created.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool IsSafeEndDate(DateTime date, bool includeChildren)
        {
            foreach (Workout workout in Workouts)
            {
                if (date < workout.StartDate && (!includeChildren || workout.IsChild) && !workout.DateLocked)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Get all workouts after a particular date.  This is typically used
        /// in preparation of modifying the end date of a Phase in order to
        /// nicely remove the workouts.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public WorkoutCollection GetWorkouts(DateTime date)
        {
            WorkoutCollection workouts = new WorkoutCollection();

            // Look through all workouts and find workouts that occur on this specific date
            // Typically this would only be 1 workout, but can be multiple.
            foreach (Workout workout in this.Workouts)
            {
                if (workout.StartDate.Date == date.Date)
                {
                    workouts.Add(workout);
                }
            }

            return workouts;
        }

        /// <summary>
        /// Get all workouts between some particular dates.  This is typically used
        /// in preparation of modifying Phase Start/End dates in order to
        /// nicely remove the workouts.
        /// </summary>
        /// <returns></returns>
        public WorkoutCollection GetWorkouts(DateTime start, DateTime end)
        {
            return GetWorkouts(start, end, true, true);
        }

        /// <summary>
        /// Get all workouts between some particular dates.  This is typically used
        /// in preparation of modifying Phase Start/End dates in order to
        /// nicely remove the workouts.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="includeDateLocked">True to include all workouts.  
        /// False will exclude workouts with a static date, i.e. DateLocked items,
        /// from the returned WorkoutCollection.</param>
        /// <returns></returns>
        public WorkoutCollection GetWorkouts(DateTime start, DateTime end, bool includeDateLocked, bool includeMovable)
        {
            WorkoutCollection workouts = new WorkoutCollection();

            foreach (Workout workout in this.workouts)
            {
                if (start <= workout.StartDate && workout.StartDate <= end &&
                    ((includeDateLocked && workout.DateLocked) || (includeMovable && !workout.DateLocked)))
                {
                    workouts.Add(workout);
                }
            }

            return workouts;
        }

        /// <summary>
        /// Shift the phase Start and End dates by a number of days.
        /// If days is less than 0 this will shift the phase into the future,
        /// if days is greater than 0 this will shift the phase into the past.
        /// </summary>
        /// <param name="days">Number of days to shift phase.</param>
        public void Shift(int days)
        {
            if (days > 0)
            {
                // Shift into future...
                this.EndDate = this.EndDate.AddDays(days);
                this.StartDate = this.StartDate.AddDays(days);
            }
            else if (days < 0)
            {
                // Shift into past...
                this.StartDate = this.StartDate.AddDays(days);
                this.EndDate = this.EndDate.AddDays(days);
            }
        }

        /// <summary>
        /// Update all 'Actual' type data associated with all workouts in this plan.  For example, this will update the ActualDistance parameter for workouts.
        /// </summary>
        internal void LinkActivities()
        {
            // Clear all previous associations
            const double minScore = 40;
            Dictionary<string, double> linked = new Dictionary<string, double>();

            ClearActivityLinks();

            // Loop through all workouts in this phase.  Analyze all workouts on a given day at the same time to account for multiple workout days.
            foreach (Workout workout in Workouts)
            {
                foreach (IActivity activity in PluginMain.GetApplication().Logbook.Activities)
                {
                    // Possible to match activity & workout at most 1 day apart (delayed activity)
                    if (Math.Abs((activity.StartTime.Date - workout.StartDate).TotalDays) <= 1)
                    {
                        ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);

                        // Match activity to closest workout.  There's a minimum confidence score.
                        double score = workout.GetMatchScore(activity);
                        if (minScore <= score && workout.Actual.MatchScore < score)
                        {
                            // Activities are added/linked when:
                            // this confidence score is better previous matches
                            if (linked.ContainsKey(activity.ReferenceId))
                            {
                                if (linked[activity.ReferenceId] < score)
                                {
                                    linked[activity.ReferenceId] = score;
                                    workout.Actual.LinkActivity(activity);
                                }
                            }
                            else
                            {
                                // ... or its never been matched before
                                linked.Add(activity.ReferenceId, score);
                                workout.Actual.LinkActivity(activity);
                            }
                        }
                    }
                }
            }

            // Clear activities that were assigned to multiple activities
            foreach (Workout workout in Workouts)
            {
                if (linked.ContainsKey(workout.Actual.ActivityRefId) && linked[workout.Actual.ActivityRefId] != workout.Actual.MatchScore)
                    workout.Actual.Clear();
            }

        }

        /// <summary>
        /// Clear all 'actual' values to zero
        /// </summary>
        internal void ClearActivityLinks()
        {
            foreach (Workout workout in Workouts)
            {
                workout.Actual.Clear();
            }
        }

        internal void UpdateSubscriptions()
        {
            foreach (Workout child in Workouts)
            {
                Workout parent = Workouts.GetParent(child);
                if (parent != null)
                    child.SubscribeToParent(parent);
            }
        }

        /// <summary>
        /// Manage summary statistics.  Add/Remove them as necessary, or simply refresh data.
        /// </summary>
        internal void RefreshSummary()
        {
            averageSummary.Refresh();
            totalSummary.Refresh();
            dailySummary.Refresh();

            return;
        }

        private void workout_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Workout workout = sender as Workout;
            if (workout != null && e.PropertyName == "StartDate")
            {
                if (Node.Children.Contains(workout.Node))
                {
                    Node.Children.Remove(workout.Node);

                    for (int i = 0; i < Node.Children.Count; i++)
                    {
                        Workout item = Node.Children[i].Element as Workout;
                        if (item != null && (workout.StartDate < item.StartDate || i == Node.Children.Count))
                        {
                            // Insert at the proper spot
                            Node.Children.Insert(i, workout.Node);
                            break;
                        }
                        else if (item != null && i == Node.Children.Count - 1)
                        {
                            // Add to end
                            Node.Children.Add(workout.Node);
                            break;
                        }
                        else if ((Node.Children[i] as FitPlanNode).IsPhaseSummary)
                        {
                            // Insert in front of summary lines
                            Node.Children.Insert(i, workout.Node);
                            break;
                        }
                    }
                }
            }

            RefreshSummary();

            RaisePropertyChanged("Workouts." + e.PropertyName);
        }

        private void workouts_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            // Anytime the workout collection changes, update the nodelist.
            // This will properly manage the object collection (TreeNode collection)
            // rather than re-creating it each time.
            // Once the list is added to the treelist, it should self manage at that point.
            Workout workout = e.Element as Workout;

            if (workout != null && e.Action == CollectionChangeAction.Add)
            {
                // Add a new node to the collection
                workout.Node.Parent = Node;

                if (Node.Children.Count == 0)
                {
                    // First child
                    Node.Children.Add(workout.Node);
                }
                else
                {
                    for (int i = 0; i < Node.Children.Count; i++)
                    {
                        Workout item = Node.Children[i].Element as Workout;
                        if (item != null && (workout.StartDate < item.StartDate))
                        {
                            // Insert at the proper spot
                            Node.Children.Insert(i, workout.Node);
                            break;
                        }
                        else if (item != null && i == Node.Children.Count - 1)
                        {
                            // Add to end
                            Node.Children.Add(workout.Node);
                            break;
                        }
                        else if ((Node.Children[i] as FitPlanNode).IsPhaseSummary)
                        {
                            // Insert in front of summary lines
                            Node.Children.Insert(i, workout.Node);
                            break;
                        }
                    }
                }

                workout.PropertyChanged += workout_PropertyChanged;
            }
            else if (workout != null && e.Action == CollectionChangeAction.Remove)
            {
                // Remove node from list
                if (Node.Children.Contains(workout.Node))
                    Node.Children.Remove(workout.Node);

                workout.PropertyChanged -= workout_PropertyChanged;
            }

            RefreshSummary();

            if (Loaded.IsLoaded) LinkActivities();

            RaisePropertyChanged("Workouts");
        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));

            if (!Loaded.initializing)
                Modified = true;
        }

        private void RaiseStartDatePropertyChanging(DateTime oldStart, DateTime newStart)
        {
            if (StartDateChanging != null)
                StartDateChanging.Invoke(this, new System.Windows.Forms.DateRangeEventArgs(oldStart, newStart));
        }

        #endregion

        #region Class: Phase Summary

        public class PhaseSummary : IWorkoutBase
        {
            private SummaryType summary;
            private Phase phase;
            private Workout.Actuals actual;

            public enum SummaryType
            {
                Totals,
                Avg,
                Daily
            }

            public PhaseSummary(Phase phase, SummaryType type)
            {
                this.phase = phase;
                summary = type;
                actual = new Workout.Actuals(this);

                Refresh();
            }

            public string Name { get; set; }

            public TimeSpan TotalTime
            {
                get;
                set;
            }

            public float Score { get; set; }

            public double TotalDistanceMeters { get; set; }

            public float CTL { get; set; }

            public Workout.Actuals Actual
            {
                get { return actual; }
            }

            public SummaryType Style
            {
                get { return summary; }
            }

            public Phase Parent
            {
                get { return phase; }
            }

            /// <summary>
            /// Gets the custom formatted text with proper decimal places, etc.
            /// </summary>
            /// <param name="Id">Column defines the property value to get</param>
            /// <returns>Returns custom formatted string ready for display to user.</returns>
            public string GetFormattedText(string Id)
            {
                Type phaseType = typeof(Phase.PhaseSummary);  // Used to collect property value from activity
                string text = null;                         // text to display in cell (if defined)
                object source = this;

                if (Id.StartsWith("Actual."))
                {
                    source = this.Actual;
                    Id = Id.Substring(7);
                    phaseType = typeof(Workout.Actuals);
                }

                // Create custom display text
                if (Id == "TotalTime" || Id == "DeltaTime")
                {
                    // hh:mm:ss
                    TimeSpan value = (TimeSpan)phaseType.GetProperty(Id).GetValue(source, null);
                    text = Utilities.ToTimeString(value);
                }
                else if (Id == "TotalDistanceMeters" || Id == "DeltaDistanceMeters")
                {
                    // 1.5
                    double value = (double)phaseType.GetProperty(Id).GetValue(source, null);
                    value = Length.Convert(value, Length.Units.Meter, PluginMain.DistanceUnits);
                    text = value.ToString("0.#", CultureInfo.CurrentCulture);
                }
                else if (Id == "Score")
                {
                    // nnn
                    float value = (float)phaseType.GetProperty(Id).GetValue(source, null);
                    text = value.ToString("#");
                }
                else if (Id == "CTL")
                {
                    // nnn
                    float value = (float)phaseType.GetProperty(Id).GetValue(source, null);
                    text = value.ToString("+#;-#;0");
                }
                else
                {
                    // Default
                    PropertyInfo info = phaseType.GetProperty(Id);

                    if (info != null)
                    {
                        object value = info.GetValue(this, null);
                        if (value != null)
                        {
                            text = value.ToString();
                        }
                    }
                }

                if (text == "NaN")
                    text = string.Empty;

                return text;
            }

            /// <summary>
            /// Recalculate all values in this summary node.
            /// </summary>
            public void Refresh()
            {
                float ctlStart = 0, ctlEnd = 0;

                TotalTime = TimeSpan.Zero;
                TotalDistanceMeters = 0;
                Score = 0;
                Actual.TotalDistanceMeters = 0;
                Actual.TotalTime = TimeSpan.Zero;
                Actual.Score = 0;

                // Sum up all workouts in Phase
                foreach (Workout workout in phase.Workouts)
                {
                    TotalTime += workout.TotalTime;
                    TotalDistanceMeters += workout.TotalDistanceMeters;
                    Score += workout.Score;
                    Actual.TotalDistanceMeters += workout.Actual.TotalDistanceMeters;
                    Actual.TotalTime += workout.Actual.TotalTime;
                    Actual.Score += workout.Actual.Score;
                }

                if (FitPlan.Data.ChartData.CTL.GetInterpolatedValue(phase.StartDate) != null && FitPlan.Data.ChartData.CTL.GetInterpolatedValue(phase.EndDate) != null)
                {
                    ctlStart = FitPlan.Data.ChartData.CTL.GetInterpolatedValue(phase.StartDate).Value;
                    ctlEnd = FitPlan.Data.ChartData.CTL.GetInterpolatedValue(phase.EndDate).Value;
                }

                switch (summary)
                {
                    case SummaryType.Avg:
                        Name = Resources.Strings.Label_WeeklyAvg;
                        TotalDistanceMeters = TotalDistanceMeters / phase.TotalDays * 7;
                        Actual.TotalDistanceMeters = Actual.TotalDistanceMeters / phase.TotalDays * 7;
                        Actual.Score = Actual.Score / phase.TotalDays * 7;
                        Score = Score / phase.TotalDays * 7;
                        CTL = (ctlEnd - ctlStart) / phase.TotalDays * 7;

                        if (phase.TotalDays != 0)
                        {
                            TotalTime = TimeSpan.FromMinutes(TotalTime.TotalMinutes / phase.TotalDays * 7);
                            Actual.TotalTime = TimeSpan.FromMinutes(Actual.TotalTime.TotalMinutes / phase.TotalDays * 7);
                        }
                        else
                        {
                            TotalTime = TimeSpan.Zero;
                            Actual.TotalTime = TimeSpan.Zero;
                        }
                        break;

                    case SummaryType.Daily:
                        Name = Resources.Strings.Label_DailyAvg;
                        TotalDistanceMeters = TotalDistanceMeters / phase.TotalDays;
                        Actual.TotalDistanceMeters = Actual.TotalDistanceMeters / phase.TotalDays;
                        Score = Score / phase.TotalDays;
                        Actual.Score = Actual.Score / phase.TotalDays;
                        CTL = float.NaN;

                        if (phase.TotalDays != 0)
                        {
                            TotalTime = TimeSpan.FromMinutes(TotalTime.TotalMinutes / phase.TotalDays);
                            Actual.TotalTime = TimeSpan.FromMinutes(Actual.TotalTime.TotalMinutes / phase.TotalDays);
                        }
                        else
                        {
                            TotalTime = TimeSpan.Zero;
                            Actual.TotalTime = TimeSpan.Zero;
                        }
                        break;

                    case SummaryType.Totals:
                        Name = Resources.Strings.Label_PhaseTotal;
                        CTL = ctlEnd - ctlStart;
                        break;
                }
            }
        }

        #endregion
    }
}