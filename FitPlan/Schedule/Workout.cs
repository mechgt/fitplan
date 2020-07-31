namespace FitPlan.Schedule
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Reflection;
    using System.Xml.Serialization;
    using FitPlan.Data;
    using FitPlan.UI;
    using Pabo.Calendar;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.Measurement;
    using System.Collections;
    using System.Collections.Generic;
    using FitPlan.Settings;
    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness.CustomData;

    [XmlRootAttribute(ElementName = "Workout", IsNullable = false)]
    public class Workout : IComparable, IComparable<Workout>, IWorkout
    {
        #region Fields

        public event PropertyChangedEventHandler PropertyChanged;
        public event System.Windows.Forms.DateRangeEventHandler StartDateChanged;

        private DateItem workoutItem = new DateItem();
        private DateTime start, end;
        private double totalDistanceMeters;
        private float ramp;
        private float score;
        private Guid id;
        private FitPlanNode node;
        private int period;
        private string name;
        private string parentId;
        private Guid templateId;
        private string notes;
        private string imageName;
        private TimeSpan totalTime;
        private bool modified;
        private bool linked;
        private bool dateLocked;
        private IActivityCategory category;
        private string categoryName;
        private string categoryId;
        private Actuals actual;

        #endregion

        #region Enumerations

        #endregion

        #region Constructors

        /// <summary>
        /// Create a recurring workout
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="periodDays"></param>
        /// <param name="distanceMeters"></param>
        /// <param name="metersPerSecond"></param>
        /// <param name="ramp"></param>
        /// <param name="name"></param>
        public Workout(DateTime startDate, DateTime endDate, int periodDays, float distanceMeters, float metersPerSecond, float ramp, string name)
            : this(startDate, distanceMeters, metersPerSecond, name)
        {
            end = endDate;
            PeriodDays = periodDays;

            if (metersPerSecond != 0)
            {
                TotalTime = TimeSpan.FromSeconds(distanceMeters / metersPerSecond);
            }
            else
            {
                TotalTime = TimeSpan.Zero;
            }

            Ramp = ramp;
        }

        /// <summary>
        /// Create a single workout
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="distanceMeters"></param>
        /// <param name="metersPerSecond"></param>
        /// <param name="name"></param>
        public Workout(DateTime startDate, float distanceMeters, float metersPerSecond, string name)
            : this()
        {
            start = startDate;
            end = startDate;
            TotalDistanceMeters = distanceMeters;

            if (metersPerSecond != 0)
            {
                TotalTime = TimeSpan.FromSeconds(distanceMeters / metersPerSecond);
            }
            else
            {
                TotalTime = TimeSpan.Zero;
            }

            Name = name;
        }

        public Workout(FitPlan.Data.WorkoutDefinition workoutDef)
            : this()
        {
            Name = workoutDef.Name;
            Notes = workoutDef.Notes;
            Score = workoutDef.Score;
            ImageName = workoutDef.ImageName;
            TotalTime = workoutDef.TotalTime;
            TotalDistanceMeters = workoutDef.TotalDistanceMeters;
            Category = workoutDef.Category;
            templateId = workoutDef.Id;
        }

        /// <summary>
        /// Create a child or instanced workout.  This would typically be an instance of a repeating workout used for display.
        /// </summary>
        /// <param name="date">Date when the workout is scheduled</param>
        /// <param name="parent">Parent repeating workout</param>
        public Workout(DateTime date, Workout parent)
            : this()
        {
            float multiplier = (float)Math.Pow(1 + parent.Ramp, (date - parent.StartDate).TotalDays / parent.PeriodDays);
            if (float.IsNaN(multiplier)) multiplier = 1;

            linked = true;
            start = date;
            end = date;
            Name = parent.Name;
            TotalDistanceMeters = parent.TotalDistanceMeters * multiplier;
            TotalTime = TimeSpan.FromMinutes(parent.TotalTime.TotalMinutes * multiplier);
            parentId = parent.ReferenceId;
            parent.PropertyChanged += new PropertyChangedEventHandler(Parent_PropertyChanged);
            parent.StartDateChanged += new System.Windows.Forms.DateRangeEventHandler(parent_StartDateChanged);
            this.PropertyChanged += parent.Child_PropertyChanged;
            period = parent.PeriodDays;
            score = parent.score * multiplier;
            imageName = parent.imageName;
            notes = parent.notes;
            category = parent.category;
            templateId = parent.templateId;
        }


        /// <summary>
        /// Blank Constructor.
        /// </summary>
        public Workout()
        {
            node = new FitPlanNode(null, this);
            dateLocked = false;
            id = Guid.NewGuid();
            actual = new Actuals(this);
        }

        #endregion

        #region Properties

        [XmlIgnore]
        public IActivityCategory Category
        {
            get
            {
                if (category == null)
                {
                    return Settings.LogbookSettings.MyActivities;
                }
                return category;
            }
            set
            {
                if (category == value) return;

                category = value;
                RefreshCalendarItem();
                RaisePropertyChanged("Category");
            }
        }

        [XmlAttribute("categoryName")]
        public string XMLCategoryName
        {
            get { return Category.Name; }
            set
            {
                if (categoryName == value)
                    return;

                categoryName = value;
                IActivityCategory match = GetCategory(this.XMLCategoryId, value);
                if (match != null)
                {
                    Category = match;
                }
            }
        }

        [XmlAttribute("categoryId")]
        public string XMLCategoryId
        {
            get { return Category.ReferenceId; }
            set
            {
                if (categoryId == value) return;

                categoryId = value;

                IActivityCategory match = GetCategory(value, this.XMLCategoryName);
                if (match != null)
                {
                    Category = match;
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the workout
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if (name == value) return;

                name = value;
                RefreshCalendarItem();

                RaisePropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets notes attached to the workout
        /// </summary>
        public string Notes
        {
            get
            {
                if (notes != null && notes.Contains("\n"))
                {
                    return notes.Replace("\r", string.Empty).Replace("\n", "\r\n");
                }

                return notes;
            }
            set
            {
                if (notes == value) return;

                notes = value;
                RaisePropertyChanged("Notes");
            }
        }

        /// <summary>
        /// Image name associated with this workout.  Image is displayed on the training calendar
        /// </summary>
        public string ImageName
        {
            get
            {
                if (string.IsNullOrEmpty(imageName))
                {
                    imageName = "-";
                }
                return imageName;
            }
            set
            {
                if (imageName == value) return;

                imageName = value;
                workoutItem.Image = GlobalImages.GetImage(imageName, this.DateLocked);
                RaisePropertyChanged("ImageName");
            }
        }

        /// <summary>
        /// Date of first occurrance of this workout.  If this is a single workout, this
        /// is the proper date to use to get/set the date it occurs.
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                return start.Date;
            }
            set
            {
                value = value.Date;

                if (start == value) return;

                // Update end if not repeating
                if (!IsRepeating)
                {
                    end = value;
                }

                DateTime oldStart = start;
                start = value;

                RefreshCalendarItem();

                if (Data.GarminFitness.Manager.IsInstalled)
                {
                    // Remove scheduled date from Garmin Fitness.
                    // Done here because this is the only place with direct access to old start date
                    // Cannot be scheduled here because we need the AutoSchedDays plan setting to
                    // see if the new date qualifies.
                    Data.GarminFitness.Manager.ScheduleWorkout(this.LinkedTemplate, oldStart, true);
                }

                RaiseStartDatePropertyChanged(oldStart, value);
                RaisePropertyChanged("StartDate");
            }
        }

        /// <summary>
        /// Date in which the workout series ends.
        /// In the event of a non-recurring workout, this should be equal 
        /// to StartDate
        /// </summary>
        public DateTime EndDate
        {
            get { return end.Date; }
            set
            {
                if (end == value) return;

                // Finally update end date.
                end = value;
                RaisePropertyChanged("EndDate");
            }
        }

        /// <summary>
        /// Gets or sets the workout repeat period in days
        /// </summary>
        public int PeriodDays
        {
            get { return period; }
            set
            {
                if (period == value) return;

                period = value;
                RaisePropertyChanged("PeriodDays");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if this workout is linked to it's parent workout.
        /// Value is ignored if this is not a child workout.
        /// </summary>
        public bool IsLinked
        {
            get { return linked; }
            set
            {
                if (linked != value)
                {
                    linked = value;
                    RefreshCalendarItem();
                    RaisePropertyChanged("Linked");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if this workout's date is fixed.  If true, then the
        /// workout date can only be changed manually and will not shift/move when managing phases,
        /// etc.  If false, it will slide and move dates with it's parent phase.
        /// Default value = false.
        /// </summary>
        public bool DateLocked
        {
            get { return dateLocked; }
            set
            {
                if (dateLocked == value) return;
                dateLocked = value;
                workoutItem.Image = GlobalImages.GetImage(imageName, this.DateLocked);
                RaisePropertyChanged("DateLocked");
            }
        }

        /// <summary>
        /// Gets or sets the total time duration of the workout
        /// </summary>
        [XmlIgnore]
        public TimeSpan TotalTime
        {
            get { return totalTime; }
            set
            {
                if (totalTime == value) return;

                totalTime = value;
                RefreshCalendarItem();
                RaisePropertyChanged("TotalTime");
            }
        }

        /// <summary>
        /// Used to read write during serialization.
        /// Only used during serialization
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [XmlElement("TotalTime")]
        public int XMLTotalTime
        {
            get { return (int)TotalTime.TotalSeconds; }
            set { totalTime = TimeSpan.FromSeconds(value); }
        }

        /// <summary>
        /// Gets or sets the total distance (in meters) defined for the workout
        /// </summary>
        public double TotalDistanceMeters
        {
            get
            {
                if (double.IsNaN(totalDistanceMeters))
                {
                    return 0;
                }

                return totalDistanceMeters;
            }
            set
            {
                if (totalDistanceMeters == value) return;

                totalDistanceMeters = value;
                RefreshCalendarItem();
                RaisePropertyChanged("TotalDistanceMeters");
            }
        }

        /// <summary>
        /// Gets or sets the periodic (weekly for instance) ramp rate to vary how the workout will change over time
        /// </summary>
        public float Ramp
        {
            get { return ramp; }
            set
            {
                if (ramp == value) return;

                ramp = value;
                RaisePropertyChanged("Ramp");
            }
        }

        /// <summary>
        /// ReferenceId of parent workout if it exists.  Returns null for child workouts.
        /// </summary>
        public string ParentId
        {
            get { return parentId; }
            set
            {
                if (parentId == value) return;

                parentId = value;
                RaisePropertyChanged("ParentId");
            }
        }

        /// <summary>
        /// Unique identifier for each workout.
        /// </summary>
        public string ReferenceId
        {
            get { return id.ToString("D"); }
            set
            {
                if (id.ToString("D") == value) return;

                id = new Guid(value);
                RaisePropertyChanged("ReferenceId");
            }
        }

        ///// <summary>
        ///// Shortened Base 64 version of Workout ReferenceId
        ///// </summary>
        //public string FitId
        //{
        //    get { return Convert.ToBase64String(id.ToByteArray()).Replace("/", "-").Replace("+", "_").Replace("=", ""); }
        //}

        /// <summary>
        /// Id of associated template if it exists.  
        /// Returns empty guid for non-linked workouts.
        /// </summary>
        public Guid TemplateId
        {
            get { return templateId; }
            set
            {
                if (templateId == value) return;

                if (Data.GarminFitness.Manager.IsInstalled)
                {
                    // Remove scheduled date from Garmin Fitness.
                    // Done here because this is the only place with direct access to old template
                    // Cannot be scheduled here because we need the AutoSchedDays plan setting to
                    // see if the start date qualifies.
                    Data.GarminFitness.Manager.ScheduleWorkout(this.LinkedTemplate, StartDate, true);
                }

                templateId = value;
                RaisePropertyChanged("TemplateId");
            }
        }

        /// <summary>
        /// Gets the pace in generic units.  Need to convert to user units prior to display.
        /// Shortcut for TotalTime / TotalDistance
        /// </summary>
        [XmlIgnore]
        public TimeSpan PaceMinPerMeter
        {
            get
            {
                if (TotalDistanceMeters.Equals(0))
                {
                    return TimeSpan.Zero;
                }
                else
                {
                    return TimeSpan.FromMinutes(TotalTime.TotalMinutes / TotalDistanceMeters);
                }
            }
        }

        /// <summary>
        /// Gets the speed in generic units.  Need to convert to user units prior to display.
        /// Shortcut for TotalDistance / TotalTime
        /// </summary>
        [XmlIgnore]
        public float MetersPerSecond
        {
            get
            {
                if (TotalTime != TimeSpan.Zero)
                {
                    return (float)(TotalDistanceMeters / TotalTime.TotalSeconds);
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating if this workout is a repeating workout.
        /// Returns true if it repeats, false if it is a single stand-alone instance.
        /// </summary>
        [XmlIgnore]
        public bool IsRepeating
        {
            get { return StartDate != EndDate && EndDate != DateTime.MinValue; }
        }

        /// <summary>
        /// Gets a value indicating if this is a child workout.
        /// A child workout is an instance of a parent repeating workout.
        /// </summary>
        [XmlIgnore]
        public bool IsChild
        {
            get
            {
                return parentId != null;
            }
        }

        /// <summary>
        /// The item to be displayed on the large month calendar representing this workout.
        /// Note that parent workouts only get a single DateItem occuring on the StartDate 
        /// and that items from all instances would be needed to populate a full repeating 
        /// workout schedule.
        /// </summary>
        [XmlIgnore]
        public DateItem CalendarItem
        {
            get
            {
                RefreshCalendarItem();
                return workoutItem;
            }
        }

        [XmlIgnore]
        public FitPlanNode Node
        {
            get { return node; }
        }

        /// <summary>
        /// Gets the linked Fit Plan template if one exists.
        /// </summary>
        public WorkoutDefinition LinkedTemplate
        {
            get { return FitPlan.Settings.LogbookSettings.GetWorkoutDef(this.templateId); }
        }

        /// <summary>
        /// Gets or sets a score, such as Trimp or TSS
        /// </summary>
        public float Score
        {
            get { return score; }
            set
            {
                if (score != value)
                {
                    score = value;
                    ChartData.Calculated = false;
                    RaisePropertyChanged("Score");
                }
            }
        }

        [XmlIgnore]
        public float ATL
        {
            get
            {
                ITimeValueEntry<float> item = ChartData.ATL.GetInterpolatedValue(this.StartDate);
                if (item != null)
                    return item.Value;
                else
                    return 0;
            }
        }

        [XmlIgnore]
        public float CTL
        {
            get
            {
                ITimeValueEntry<float> item = ChartData.CTL.GetInterpolatedValue(this.StartDate);
                if (item != null)
                    return item.Value;
                else
                    return 0;
            }
        }

        [XmlIgnore]
        public float TSB
        {
            get
            {
                ITimeValueEntry<float> item = ChartData.TSB.GetInterpolatedValue(this.StartDate);
                if (item != null)
                    return item.Value;
                else
                    return 0;
            }
        }

        internal bool Modified
        {
            get { return modified; }
            set
            {
                modified = value;
                if (modified)
                {
                    PluginMain.GetApplication().Logbook.Modified = true;
                }
            }
        }

        [XmlIgnore]
        public Actuals Actual
        {
            get { return actual; }
        }

        /// <summary>
        /// Get the upper level category for this workout such as Running, Cycling, Swimming, etc.  This is based on the workout's assigned category.
        /// </summary>
        [XmlIgnore]
        public IActivityCategory UpperCategory
        {
            get
            {
                return Utilities.GetUpperCategory(this.Category);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the custom formatted text with proper decimal places, etc.
        /// Values are ready for user display (converted to proper units etc.)
        /// </summary>
        /// <param name="column">Column defines the property value to get</param>
        /// <returns>Returns custom formatted string ready for display to user.</returns>
        public string GetFormattedText(string Id)
        {
            return GetFormattedText(this, Id);
        }

        /// <summary>
        /// Gets the custom formatted text with proper decimal places, etc.  
        /// Values are ready for user display (converted to proper units etc.)
        /// </summary>
        /// <param name="activity">Activity containing value</param>
        /// <param name="column">Column defines the property value to get</param>
        /// <returns>Returns custom formatted string ready for display to user.</returns>
        public static string GetFormattedText(Workout workout, string Id)
        {
            Type workoutType = typeof(Workout);  // Used to collect property value from activity
            string text = null;                         // text to display in cell (if defined)
            object source = workout;
            bool actual = false;

            if (Id.StartsWith("Actual."))
            {
                actual = true;
                source = workout.Actual;
                Id = Id.Substring(7);
                workoutType = typeof(Workout.Actuals);
            }

            // Create custom display text
            if (Id == "StartDate" ||
                Id == "EndDate")
            {
                // 9/11/2010
                DateTime value = (DateTime)workoutType.GetProperty(Id).GetValue(source, null);

                if (value == DateTime.MinValue || value == null)
                    text = string.Empty;
                else
                    text = value.ToString("d", CultureInfo.CurrentCulture);
            }
            else if (Id == "TotalTime" || Id == "DeltaTime")
            {
                // hh:mm:ss
                TimeSpan value = (TimeSpan)workoutType.GetProperty(Id).GetValue(source, null);
                text = Utilities.ToTimeString(value);
            }
            else if ((Id == "TotalDistanceMeters" || Id == "DeltaDistanceMeters") && actual)
            {
                // 1.5
                double value = (double)workoutType.GetProperty(Id).GetValue(source, null);
                value = Length.Convert(value, Length.Units.Meter, PluginMain.DistanceUnits);
                text = value.ToString("#.##", CultureInfo.CurrentCulture);
            }
            else if (Id == "TotalDistanceMeters")
            {
                // 1.5
                double value = (double)workoutType.GetProperty(Id).GetValue(source, null);
                value = Length.Convert(value, Length.Units.Meter, PluginMain.DistanceUnits);
                text = value.ToString("0.##", CultureInfo.CurrentCulture);
            }
            else if (Id == "PaceMinPerMeter")
            {
                // mm:ss
                text = Speed.ToPaceString(workout.TotalDistanceMeters, workout.TotalTime, new Length(1, PluginMain.DistanceUnits), string.Empty);
            }
            else if (Id == "MetersPerSecond")
            {
                text = Speed.ToSpeedString(workout.TotalDistanceMeters, workout.TotalTime, new Length(1, PluginMain.DistanceUnits), "#.##");
            }
            else if (Id == "SpeedPace")
            {
                switch (workout.Category.SpeedUnits)
                {
                    case Speed.Units.Pace:
                        text = GetFormattedText(workout, "PaceMinPerMeter");
                        break;
                    case Speed.Units.Speed:
                        text = GetFormattedText(workout, "MetersPerSecond");
                        break;
                }

                text = string.Format("{0} {1}", text, Speed.Label(workout.Category.SpeedUnits, new Length(1, Length.LargeUnits(PluginMain.DistanceUnits)), true));
            }
            else if (Id == "Ramp")
            {
                // Percent 5 %
                float value = (float)workoutType.GetProperty(Id).GetValue(source, null);
                text = value.ToString("P0", CultureInfo.CurrentCulture);
            }
            else if (Id == "Score")
            {
                // nnn (or blank if 0)
                float value = (float)workoutType.GetProperty(Id).GetValue(source, null);
                text = value.ToString("#");
            }
            else if (Id == "CTL" || Id == "ATL" || Id == "TSB")
            {
                // nnn
                float value = (float)workoutType.GetProperty(Id).GetValue(source, null);
                text = value.ToString("0");
            }
            else if (Id == "TemplateId")
            {
                WorkoutDefinition template = workout.LinkedTemplate;
                if (template == null)
                    return string.Empty;
                else
                    return template.Name;
            }
            else
            {
                // Default
                PropertyInfo info = workoutType.GetProperty(Id);

                if (info != null)
                {
                    object value = info.GetValue(source, null);
                    if (value != null)
                    {
                        text = value.ToString();
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// Clones the specified workout to a new workout with a unique GUID
        /// </summary>
        /// <returns>A unique copy of this Workout</returns>
        public Workout Clone()
        {
            Workout clone = this.MemberwiseClone() as Workout;
            clone.id = Guid.NewGuid();
            clone.parentId = null;
            clone.node = new FitPlanNode(this.Node.Parent as FitPlanNode, clone);
            clone.workoutItem = new DateItem();
            clone.RefreshCalendarItem();

            foreach (Delegate handler in clone.PropertyChanged.GetInvocationList())
                clone.PropertyChanged -= (PropertyChangedEventHandler)handler;

            foreach (Delegate handler in clone.StartDateChanged.GetInvocationList())
                clone.StartDateChanged -= (System.Windows.Forms.DateRangeEventHandler)handler;

            this.RefreshCalendarItem();
            return clone as Workout;
        }

        #region IComparable<Workout> Members

        public int CompareTo(object value)
        {
            Workout workout = value as Workout;

            if (workout != null)
            {
                return this.CompareTo(workout);
            }

            return 0;
        }

        public int CompareTo(Workout other)
        {
            return CompareTo(this);
        }

        public int CompareTo(Workout other, WorkoutCollection.CompareType comparer)
        {
            int result = 0;

            switch (comparer)
            {
                default:
                case WorkoutCollection.CompareType.StartDate:
                    result = this.StartDate.CompareTo(other.StartDate);
                    break;

                case WorkoutCollection.CompareType.Distance:
                    result = this.TotalDistanceMeters.CompareTo(other.TotalDistanceMeters);
                    break;

                case WorkoutCollection.CompareType.Time:
                    result = this.TotalTime.CompareTo(other.TotalTime);
                    break;

                case WorkoutCollection.CompareType.Score:
                    result = this.Score.CompareTo(other.Score);
                    break;

                case WorkoutCollection.CompareType.ActualDistance:
                    result = this.Actual.TotalDistanceMeters.CompareTo(other.Actual.TotalDistanceMeters);
                    break;

                case WorkoutCollection.CompareType.ActualTime:
                    result = this.Actual.TotalTime.CompareTo(other.Actual.TotalTime);
                    break;

                case WorkoutCollection.CompareType.Category:
                    result = this.Category.ToString().CompareTo(other.Category.ToString());
                    break;
            }

            if (result == 0)
                result = this.StartDate.CompareTo(other.StartDate);

            return result;
        }

        #endregion

        public override string ToString()
        {
            object[] args = { this.GetFormattedText("StartDate"), this.GetFormattedText("TotalDistanceMeters"), this.GetFormattedText("Category"), Length.ToString(Length.Convert(this.TotalDistanceMeters, Length.Units.Meter, Length.Units.Mile), Length.Units.Mile, "0.0u") };
            return string.Format("({2}) {0} {3}", args);
        }

        public override bool Equals(object obj)
        {
            Workout item = obj as Workout;
            if (item != null)
                return this.GetHashCode() == item.GetHashCode();
            else
                return false;
        }

        public override int GetHashCode()
        {
            return ReferenceId.GetHashCode();
        }

        /// <summary>
        /// Subscribe this workout to a given parent workout and 
        /// set the IsLinked property to true.
        /// </summary>
        /// <remarks>This workout will now watch the parent workout
        /// for changes and updates.  As long as it's linked, it will
        /// mimic the parent's settings and respond to parent changes.
        /// </remarks>
        /// <param name="parent"></param>
        internal void SubscribeToParent(Workout parent)
        {
            if (this.ReferenceId == parent.ReferenceId)
            {
                throw new ArgumentException("Cannot subscribe workout to itself");
            }

            parent.PropertyChanged += Parent_PropertyChanged;
            parent.StartDateChanged += parent_StartDateChanged;
            this.IsLinked = true;
            this.PropertyChanged += parent.Child_PropertyChanged;
        }

        internal double GetMatchScore(IActivity activity)
        {
            double points, distScore, timeScore, catScore, schedScore;

            if (this.Category == activity.Category)
                catScore = 1;
            else if (this.UpperCategory != Utilities.GetUpperCategory(activity))
                catScore = .7;
            else
                catScore = 0;

            if (activity.StartTime.Date == this.StartDate)
                schedScore = 1;
            else
            {
                schedScore = 1 / Math.Pow(Math.Abs((activity.StartTime.Date - this.StartDate).TotalDays) + 1, 2);
            }

            distScore = 1 - (Math.Abs(activity.TotalDistanceMetersEntered - this.TotalDistanceMeters) / this.TotalDistanceMeters);
            timeScore = 1 - (Math.Abs(activity.TotalTimeEntered.TotalMinutes - this.TotalTime.TotalMinutes) / this.TotalTime.TotalMinutes);

            if (distScore == double.NegativeInfinity) distScore = 0;
            if (timeScore == double.NegativeInfinity) timeScore = 0;

            // Score weights, below values must total 100%
            double wDist = 17;
            double wTime = 9;
            double wCat = 14;
            const double wSched = 60;

            points = distScore * wDist + timeScore * wTime + catScore * wCat + schedScore * wSched;
            return points;
        }

        /// <summary>
        /// Get closest category match based on XMLCategoryReferenceId and XMLCategoryName
        /// </summary>
        /// <param name="workout">Workout containing properties</param>
        /// <returns></returns>
        internal static IActivityCategory GetCategory(string categoryId, string categoryName)
        {
            IActivityCategory bestMatch = null;

            foreach (IActivityCategory logbookCategory in PluginMain.GetApplication().Logbook.ActivityCategories)
            {
                if (string.IsNullOrEmpty(categoryName) && string.IsNullOrEmpty(categoryId))
                {
                    // No info.  Leave as is.
                    return null;
                }

                // Exact Id match confirms proper cat found
                if (logbookCategory.ReferenceId == categoryId)
                {
                    return logbookCategory;
                }
                else if (logbookCategory.Name == categoryName)
                {
                    if (string.IsNullOrEmpty(categoryId))
                    {
                        // No id.  First name match wins.
                        return logbookCategory;
                    }
                    else if (bestMatch == null && !string.IsNullOrEmpty(categoryName))
                    {
                        // First Best match... but keep looking for id match.
                        bestMatch = logbookCategory;
                    }
                }

                IActivityCategory cat = SearchChildren(logbookCategory, categoryId, categoryName);
                if (cat != null)
                {
                    // match found.
                    return cat;
                }
            }

            if (bestMatch == null)
            {
                return Settings.LogbookSettings.MyActivities;
            }
            else
            {
                return bestMatch;
            }
        }

        /// <summary>
        /// Convert a given FitId into a Referende Id (base64 conversion to a GUID string)
        /// </summary>
        /// <param name="fitId"></param>
        /// <returns></returns>
        internal static string GetRefId(string fitId)
        {
            Guid guid = default(Guid);
            fitId = fitId.Replace("-", "/").Replace("_", "+") + "==";

            try
            {
                guid = new Guid(Convert.FromBase64String(fitId));
            }
            catch (Exception ex)
            {
                //throw new Exception("Bad Base64 conversion to GUID", ex);
                return default(Guid).ToString("D");
            }

            return guid.ToString("D");
        }

        internal Workout GetParent(TrainingPlan plan)
        {
            return plan.GetWorkout(this.parentId);
        }

        /// <summary>
        /// Recursively search child nodes for a matching object
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="matchItem"></param>
        /// <returns></returns>
        private static IActivityCategory SearchChildren(IActivityCategory parent, string categoryId, string categoryName)
        {
            IActivityCategory bestMatch = null;

            foreach (IActivityCategory child in parent.SubCategories)
            {
                IActivityCategory match = SearchChildren(child, categoryId, categoryName);

                // Exact Id match confirms proper cat found
                if (match != null && match.ReferenceId == categoryId)
                {
                    return match;
                }
                else if (child.ReferenceId == categoryId)
                {
                    return child;
                }
                else if (child.Name == categoryName)
                {
                    if (string.IsNullOrEmpty(categoryId))
                    {
                        // No id.  First name match wins.
                        return child;
                    }
                    else if (bestMatch != null)
                    {
                        // First Best match... but keep looking for id match.
                        bestMatch = child;
                    }
                }
                else if (bestMatch == null && match != null)
                {
                    // Close name match.  Store, but keep looking.
                    bestMatch = match;
                }
            }

            return bestMatch;
        }

        /// <summary>
        /// Update the calendar item information for this workout
        /// </summary>
        private void RefreshCalendarItem()
        {
            workoutItem.Date = StartDate.Date;

            string workoutText = Name;

            if (this.TotalTime != TimeSpan.Zero)
            {
                workoutText += Environment.NewLine + "  " + GetFormattedText("TotalTime");
            }
            if (this.TotalDistanceMeters != 0)
            {
                workoutText += Environment.NewLine + "  " + GetFormattedText("TotalDistanceMeters") + " " + Length.LabelAbbr(PluginMain.DistanceUnits);
            }
            if (!this.PaceMinPerMeter.Equals(TimeSpan.Zero))
            {
                string speedProperty;
                switch (this.Category.SpeedUnits)
                {
                    case Speed.Units.Speed:
                        speedProperty = "MetersPerSecond";
                        break;
                    default:
                    case Speed.Units.Pace:
                        speedProperty = "PaceMinPerMeter";
                        break;
                }
                workoutText += Environment.NewLine + "  " + GetFormattedText(speedProperty) + " " + Speed.Label(this.Category.SpeedUnits, new Length(1, this.Category.DistanceUnits));
            }

            workoutItem.Text = workoutText;
            workoutItem.Image = GlobalImages.GetImage(this.ImageName, this.DateLocked);
            workoutItem.BoldedDate = true;
            workoutItem.GradientMode = mcGradientMode.ForwardDiagonal;

            if (IsChild && IsLinked)
            {
                workoutItem.TextColor = System.Windows.Forms.ControlPaint.Light(PluginMain.GetApplication().VisualTheme.ControlText, 1f);
            }
            else
            {
                workoutItem.TextColor = PluginMain.GetApplication().VisualTheme.ControlText;
            }

            workoutItem.Tag = "Workout" + ReferenceId;
        }

        #region Events

        /// <summary>
        /// Raise Property changed event
        /// </summary>
        /// <param name="property"></param>
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));

            if (!Loaded.initializing)
                Modified = true;
        }

        private void RaiseStartDatePropertyChanged(DateTime oldStart, DateTime newStart)
        {
            if (StartDateChanged != null)
                StartDateChanged.Invoke(this, new System.Windows.Forms.DateRangeEventArgs(oldStart, newStart));
        }

        private void Parent_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Ignore changes if this is not linked
            if (!this.IsLinked) return;
            Workout parent = sender as Workout;

            if (parent != null && parentId == parent.ReferenceId)
            {
                if (e.PropertyName == "PeriodDays")
                {
                    // Update workout period
                    if (this.period != 0)
                    {
                        int index = (StartDate - parent.StartDate).Days / this.period;
                        DateTime start = parent.StartDate.AddDays(parent.PeriodDays * index);
                        this.period = parent.PeriodDays;

                        // Set to start date
                        this.StartDate = start;
                    }
                }
                else if (e.PropertyName == "Name")
                {
                    Name = parent.Name;
                }
                else if (e.PropertyName == "Score")
                {
                    Score = parent.Score;
                }
                else if (e.PropertyName == "EndDate")
                {
                    EndDate = parent.EndDate;
                }
                else if (e.PropertyName == "Category")
                {
                    Category = parent.Category;
                }
                else if (e.PropertyName == "ImageName")
                {
                    ImageName = parent.ImageName;
                }
                else if (e.PropertyName == "TemplateId")
                {
                    TemplateId = parent.TemplateId;
                }
                else if (e.PropertyName == "Notes")
                {
                    Notes = parent.Notes;
                }

                if (e.PropertyName == "Ramp"
                    || e.PropertyName == "StartDate"
                    || e.PropertyName == "PeriodDays"
                    || e.PropertyName == "TotalDistanceMeters"
                    || e.PropertyName == "TotalTime")
                {
                    float multiplier = (float)Math.Pow(1 + parent.Ramp, (StartDate - parent.StartDate).TotalDays / parent.PeriodDays);
                    if (float.IsNaN(multiplier) || float.IsInfinity(multiplier)) multiplier = 1;
                    TotalDistanceMeters = parent.TotalDistanceMeters * multiplier;
                    TotalTime = TimeSpan.FromMinutes(parent.TotalTime.TotalMinutes * multiplier);
                    Score = parent.Score * multiplier;
                }
            }
        }

        private void parent_StartDateChanged(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            if (!this.DateLocked)
            {
                TimeSpan offset = e.End - e.Start;
                this.StartDate = this.StartDate.Add(offset);
            }
        }

        private void Child_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        #endregion

        #endregion

        public class Actuals
        {
            private double totalDistanceMeters = 0;
            private float score = 0;
            private TimeSpan totalTime = TimeSpan.Zero;
            private double matchScore = 0;
            private string activityRefId = string.Empty;
            IWorkoutBase workout;

            internal Actuals(IWorkoutBase workout)
            {
                this.workout = workout;
            }

            #region Properties
            public double TotalDistanceMeters
            {
                get
                {
                    if (double.IsNaN(totalDistanceMeters))
                    {
                        return 0;
                    }

                    return totalDistanceMeters;
                }
                set
                {
                    totalDistanceMeters = value;
                }
            }

            public TimeSpan TotalTime
            {
                get { return totalTime; }
                set { totalTime = value; }
            }

            public float Score
            {
                get { return score; }
                set { score = value; }
            }

            public TimeSpan DeltaTime
            {
                get
                {
                    if (TotalTime != TimeSpan.Zero)
                        return TotalTime - workout.TotalTime;
                    else
                        return TimeSpan.Zero;
                }
            }

            public double DeltaDistanceMeters
            {
                get
                {
                    if (TotalDistanceMeters != 0)
                        return TotalDistanceMeters - workout.TotalDistanceMeters;
                    else
                        return 0;
                }
            }

            public bool IsMatched
            {
                get
                {
                    return TotalDistanceMeters != 0 || TotalTime != TimeSpan.Zero || Score != 0;
                }
            }

            /// <summary>
            /// Actual vs Workout confidence score
            /// </summary>
            [XmlIgnore]
            public double MatchScore
            {
                get { return matchScore; }
                set { matchScore = value; }
            }

            [XmlIgnore]
            public string ActivityRefId
            {
                get { return activityRefId; }
            }

            #endregion

            #region Methods
            /// <summary>
            /// Link an activity to this workout.  These values will be stored in the Actuals class.
            /// </summary>
            /// <param name="activity">Activity to be linked.</param>
            public void LinkActivity(IActivity activity)
            {
                double? tss = null, trimp = null;
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);

                totalTime = info.Time;
                totalDistanceMeters = info.DistanceMeters;
                activityRefId = activity.ReferenceId;

                if (workout is Workout)
                    matchScore = (workout as Workout).GetMatchScore(activity);

                ICustomDataFieldDefinition field = CustomDataFields.GetCustomProperty(CustomDataFields.CustomFields.TSS, false);
                if (field != null)
                    tss = activity.GetCustomDataValue(field) as double?;


                field = CustomDataFields.GetCustomProperty(CustomDataFields.CustomFields.Trimp, false);
                if (field != null)
                    trimp = activity.GetCustomDataValue(field) as double?;

                if (LogbookSettings.Main.ScoreType == Common.Score.TSS && tss != null && tss != 0)
                    score = (float)tss;

                if ((LogbookSettings.Main.ScoreType == Common.Score.Trimp || score == 0) && trimp != null)
                    score = (float)trimp;
            }

            public void Clear()
            {
                this.TotalDistanceMeters = 0;
                this.TotalTime = TimeSpan.Zero;
                this.Score = 0;
                this.MatchScore = 0;
                this.activityRefId = string.Empty;
            }

            #endregion
        }
    }
}
