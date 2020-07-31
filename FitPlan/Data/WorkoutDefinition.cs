namespace FitPlan.Data
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Reflection;
    using System.Xml.Serialization;
    using FitPlan.Schedule;
    using FitPlan.Settings;
    using FitPlan.UI;
    using GarminFitnessPublic;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.Measurement;

    public class WorkoutDefinition : IPublicWorkout, IWorkout
    {
        #region Fields

        public event PropertyChangedEventHandler PropertyChanged;

        private IActivityCategory category;
        private Guid id;
        private Guid garminId;
        private string name;
        private string notes;
        private string imageName;
        private TimeSpan totalTime;
        private double totalDistanceMeters;
        private float score;
        private LibraryNode node;
        private string categoryName;
        private string categoryId;

        #endregion

        #region Constructor

        public WorkoutDefinition()
        {
            if (string.IsNullOrEmpty(name))
            {
                name = string.Format(Resources.Strings.Action_New, Resources.Strings.Label_Template);
            }

            node = new LibraryNode(null, this);
            id = Guid.NewGuid();
        }

        internal WorkoutDefinition(IPublicWorkout garminWorkout)
            : this()
        {
            category = garminWorkout.Category;
            garminId = garminWorkout.Id;
            name = garminWorkout.Name;
            notes = garminWorkout.Notes;
        }

        internal WorkoutDefinition(FitPlan.Schedule.Workout fitPlanWorkout)
            : this()
        {
            name = fitPlanWorkout.Name;
            category = fitPlanWorkout.Category;
            notes = fitPlanWorkout.Notes;
            score = fitPlanWorkout.Score;
            imageName = fitPlanWorkout.ImageName;
            totalTime = fitPlanWorkout.TotalTime;
            totalDistanceMeters = fitPlanWorkout.TotalDistanceMeters;
        }

        internal WorkoutDefinition(IActivity activity)
            : this()
        {
            category = activity.Category;
            totalDistanceMeters = activity.TotalDistanceMetersEntered;
            totalTime = activity.TotalTimeEntered;
            if (activity.Name != string.Empty)
                name = activity.Name;
            else
                name = string.Format(Resources.Strings.Action_New, Resources.Strings.Label_Template);

            notes = activity.Notes;

            double? tss = activity.GetCustomDataValue(CustomDataFields.GetCustomProperty(CustomDataFields.CustomFields.TSS, false)) as double?;
            double? trimp = activity.GetCustomDataValue(CustomDataFields.GetCustomProperty(CustomDataFields.CustomFields.Trimp, false)) as double?;

            if (LogbookSettings.Main.ScoreType == Common.Score.TSS && tss != null && tss != 0)
                score = (float)tss;

            if ((LogbookSettings.Main.ScoreType == Common.Score.Trimp || score == 0) && trimp != null)
                score = (float)trimp;
        }

        internal WorkoutDefinition(IRoute route)
            : this()
        {
            totalDistanceMeters = route.TotalDistanceMeters;
            totalTime = route.TotalTime;

            if (route.Name != string.Empty)
                name = route.Name;
            else
                name = string.Format(Resources.Strings.Action_New, Resources.Strings.Label_Template);

            notes = route.Description;
        }

        #endregion

        #region IPublicWorkout Members

        [XmlIgnore]
        public IActivityCategory Category
        {
            get
            {
                if (IsGarminWorkout && GarminWorkout != null)
                {
                    return GarminWorkout.Category;
                }
                else if (category == null)
                {
                    return LogbookSettings.MyActivities;
                }
                else
                {
                    return category;
                }
            }
            set
            {
                if (!IsGarminWorkout && category != value)
                {
                    category = value;
                    RaisePropertyChanged("Category");
                }
            }
        }

        [XmlAttribute("categoryName")]
        public string XMLCategoryName
        {
            get
            {
                if (Category != null)
                    return Category.Name;
                else
                    return string.Empty;
            }
            set
            {
                if (categoryName == value) return;

                categoryName = value;
                category = Workout.GetCategory(categoryId, categoryName);
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
                category = Workout.GetCategory(categoryId, categoryName);
            }
        }

        public Guid Id
        {
            get { return id; }
            set
            {
                if (id == value) return;

                id = value;
                RaisePropertyChanged("Id");
            }
        }

        public string Name
        {
            get
            {
                if (IsGarminWorkout && GarminWorkout != null)
                {
                    return GarminWorkout.Name;
                }
                else
                {
                    return name;
                }
            }
            set
            {
                if (name == value) return;

                name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string Notes
        {
            get
            {
                if (IsGarminWorkout && GarminWorkout != null)
                {
                    return GarminWorkout.Notes;
                }
                else
                {
                    return notes;
                }
            }
            set
            {
                if (!IsGarminWorkout && notes != value)
                {
                    notes = value;
                    RaisePropertyChanged("Notes");
                }
            }
        }

        public GarminSports Sport
        {
            get { return GarminWorkout.Sport; }
        }

        #endregion

        #region IWorkout Members

        public float Score
        {
            get
            {
                if (!float.IsNaN(score) && !float.IsInfinity(score) && score != 0)
                {
                    return score;
                }

                if (!string.IsNullOrEmpty(Notes))
                {
                    if (Notes.Contains("TSS=") && Notes.Length >= Notes.IndexOf("TSS=", StringComparison.InvariantCulture) + 5)
                    {
                        // Pull in max 4 chars following TSS=
                        string strTss = Notes.Substring(Notes.IndexOf("TSS=", StringComparison.InvariantCulture) + 4, Math.Min(4, Notes.Length - (Notes.IndexOf("TSS=", StringComparison.InvariantCulture) + 4)));

                        int i = Math.Min(4, strTss.Length);
                        while (i > 0)
                        {
                            // Read TSS from Notes field
                            float tssNotes;
                            if (float.TryParse(strTss, out tssNotes))
                            {
                                score = tssNotes;
                                break;
                            }

                            i--;
                            strTss = strTss.Substring(0, i);
                        }
                    }
                    else if (Notes.Contains("TRIMP=") && Notes.Length >= Notes.IndexOf("TRIMP=", StringComparison.InvariantCulture) + 6)
                    {
                        // Pull in max 4 chars following TRIMP=
                        string strTrimp = Notes.Substring(Notes.IndexOf("TRIMP=", StringComparison.InvariantCulture) + 6, Math.Min(4, Notes.Length - (Notes.IndexOf("TRIMP=", StringComparison.InvariantCulture) + 6)));
                        int i = Math.Min(4, strTrimp.Length);
                        while (i > 0)
                        {
                            // Read TSS from Notes field
                            float tssNotes;
                            if (float.TryParse(strTrimp, out tssNotes))
                            {
                                score = tssNotes;
                                break;
                            }

                            i--;
                            strTrimp = strTrimp.Substring(0, i);
                        }
                    }
                }
                else
                {
                    // Score is not defined and Notes is empty
                    return 0;
                }

                return score;
            }
            set
            {
                if (score == value) return;

                score = value;
                RaisePropertyChanged("Score");
            }
        }

        public string ImageName
        {
            get { return imageName; }
            set
            {
                if (imageName == value) return;

                imageName = value;
                RaisePropertyChanged("ImageName");
            }
        }

        [XmlIgnore]
        public TimeSpan TotalTime
        {
            get { return totalTime; }
            set
            {
                if (totalTime == value) return;

                totalTime = value;
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

        public double TotalDistanceMeters
        {
            get { return totalDistanceMeters; }
            set
            {
                if (totalDistanceMeters == value) return;

                totalDistanceMeters = value;
                RaisePropertyChanged("TotalDistanceMeters");
            }
        }

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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets an Id unique within the set of available Garmin workouts. 
        /// </summary>
        public string GarminId
        {
            get { return garminId.ToString("D"); }
            set
            {
                if (garminId.ToString("D") == value) return;

                garminId = new Guid(value);
                RaisePropertyChanged("GarminId");
            }
        }

        [XmlIgnore]
        public bool IsGarminWorkout
        {
            get
            {
                if (garminId == Guid.Empty)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        [XmlIgnore]
        internal LibraryNode Node
        {
            get { return node; }
        }

        [XmlIgnore]
        private IPublicWorkout GarminWorkout
        {
            get { return GarminFitness.Manager.GetWorkout(this.garminId); }
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
        public static string GetFormattedText(WorkoutDefinition workout, string Id)
        {
            Type workoutType = typeof(WorkoutDefinition);  // Used to collect property value from activity
            string text = null;                         // text to display in cell (if defined)

            if (Id == "TotalTime")
            {
                // hh:mm:ss
                TimeSpan value = (TimeSpan)workoutType.GetProperty(Id).GetValue(workout, null);
                text = Utilities.ToTimeString(value);
            }
            else if (Id == "TotalDistanceMeters")
            {
                // 1.5
                double value = (double)workoutType.GetProperty(Id).GetValue(workout, null);
                value = Length.Convert(value, Length.Units.Meter, PluginMain.DistanceUnits);
                text = value.ToString("#.##", CultureInfo.CurrentCulture);
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
            else if (Id == "Score")
            {
                // nnn
                float value = (float)workoutType.GetProperty(Id).GetValue(workout, null);
                text = value.ToString("#");
            }
            else
            {
                // Default
                PropertyInfo info = workoutType.GetProperty(Id);

                if (info != null)
                {
                    object value = info.GetValue(workout, null);
                    if (value != null)
                    {
                        text = value.ToString();
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// Raise Property changed event
        /// </summary>
        /// <param name="property"></param>
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }

        #region IComparable<Workout> Members

        public int CompareTo(object value)
        {
            WorkoutDefinition template = value as WorkoutDefinition;

            if (template != null)
            {
                return this.CompareTo(template);
            }

            return 0;
        }

        public int CompareTo(WorkoutDefinition other)
        {
            return this.Name.CompareTo(other.Name);
        }

        #endregion

        public override bool Equals(object obj)
        {
            WorkoutDefinition item = obj as WorkoutDefinition;
            if (item != null)
            {
                return this.GetHashCode() == item.GetHashCode();
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

        #region IWorkout Members

        #endregion
    }
}
