namespace FitPlan.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using FitPlan.Resources;
    using FitPlan.Settings;
    using FitPlan.UI;
    using Pabo.Calendar;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Chart;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using FitPlan.Data;

    // TODO: Export to iCal
    // TODO: Detect and display completed activities in Week View
    // TODO: Display progress in week view
    /*
     * File Version Change Log
     * 0.2 Initial release
     * 0.3 Added Garmin workout properties.  Requires migration from prior releases.
     * 0.4 Added Days property.
     * 0.5 Added ReferenceId (used specifically for Google Calendar Integration).
     */
    [XmlRoot(ElementName = "TrainingPlan", IsNullable = false)]
    public class TrainingPlan
    {
        #region Fields

        public static event PropertyChangingEventHandler Opening;
        public static event PropertyChangedEventHandler Opened;
        public static event EventHandler EvalLimit;
        public event PropertyChangedEventHandler PropertyChanged;
        public event CollectionChangeEventHandler WorkoutsChanged;
        public event CollectionChangeEventHandler PhasesChanged;

        internal static Version currentFileVer = new Version("0.5");

        private PhaseCollection phases;
        private string name;
        private string fileVersion;
        private int autoScheduleDays = -1;
        private bool modified;
        private bool isCalAutoSync;
        private bool isCalIconSync;
        private bool isCalTextSync;
        private string filename;
        private FitPlanNode node;
        private TreeNodeCollection nodeList;
        private DayCollection days;
        private FitPlan.Calendar.UserCalendar calProvider;
        private Guid id;

        #endregion

        #region Constructors

        public TrainingPlan()
        {
            nodeList = new TreeNodeCollection();
            node = new FitPlanNode(this);
            nodeList.Add(node);
            phases = new PhaseCollection();
            Modified = false;
            phases.CollectionChanged += new CollectionChangeEventHandler(phases_CollectionChanged);
            Days.CollectionChanged += new CollectionChangeEventHandler(Days_CollectionChanged);
            fileVersion = currentFileVer.ToString();
            id = Guid.NewGuid();
        }

        public TrainingPlan(DateTime start, int totalWeeks)
            : this()
        {
            // Default plan
            Phase phase = new Phase("1-Base", start, DanielsPlan.GetPhaseDays(1, totalWeeks * 7), System.Drawing.Color.Orange);
            AddPhase(phase);

            phase = new Phase("2-Build", this.EndDate.AddDays(1), DanielsPlan.GetPhaseDays(2, totalWeeks * 7), System.Drawing.Color.LightSkyBlue);
            if (phase.TotalDays > 1) AddPhase(phase);

            phase = new Phase("3-Training", this.EndDate.AddDays(1), DanielsPlan.GetPhaseDays(3, totalWeeks * 7), System.Drawing.Color.Orchid);
            if (phase.TotalDays > 1) AddPhase(phase);

            phase = new Phase("4-Final", this.EndDate.AddDays(1), DanielsPlan.GetPhaseDays(4, totalWeeks * 7), System.Drawing.Color.GreenYellow);
            if (phase.TotalDays > 1) AddPhase(phase);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value that determines if this 
        /// calendar is supposed to automatically be internet sync'd upon fitplan save 
        /// (Google Calendar) or not.
        /// </summary>
        public bool IsCalendarAutoSync
        {
            get
            {
                return isCalAutoSync;
            }
            set
            {
                if (isCalAutoSync != value)
                {
                    isCalAutoSync = value;
                    RaisePropertyChanged("IsCalendarAutoSync");
                }
            }
        }

        /// <summary>
        /// Display Option to sync an entry for the icon entry.
        /// (Specific to Google Calendar)
        /// </summary>
        public bool IsCalendarIconSync
        {
            get
            {
                return isCalIconSync;
            }
            set
            {
                if (isCalIconSync != value)
                {
                    isCalIconSync = value;
                    RaisePropertyChanged("IsCalendarIconSync");
                }
            }
        }

        /// <summary>
        /// Display option to sync an entry for the main text entry.
        /// (Specific to Google Calendar)
        /// </summary>
        public bool IsCalendarTextSync
        {
            get
            {
                return isCalTextSync;
            }
            set
            {
                if (isCalTextSync != value)
                {
                    isCalTextSync = value;
                    RaisePropertyChanged("IsCalendarTextSync");
                }
            }
        }

        public PhaseCollection Phases
        {
            get { return phases; }
            set
            {
                if (phases != value)
                {
                    phases = value;
                    RaisePropertyChanged("Phases");
                }
            }
        }

        public DateTime StartDate
        {
            get
            {
                DateTime date = DateTime.MaxValue;

                foreach (Phase phase in Phases)
                {
                    if (date > phase.StartDate)
                    {
                        date = phase.StartDate;
                    }
                }

                return date;
            }
            set
            {
                if (StartDate != value)
                {
                    DateTime start = StartDate;
                    TimeSpan offset = value - StartDate;

                    if (phases.Count > 0)
                    {
                        if (offset > TimeSpan.Zero)
                        {
                            // Moving into future
                            phases[phases.Count - 1].EndDate = phases[phases.Count - 1].EndDate.Add(offset);
                            for (int i = phases.Count - 1; i >= 0; i--)
                            {
                                phases[i].StartDate = phases[i].StartDate.Add(offset);
                                RefreshPhases();
                            }
                        }
                        else
                        {
                            // Moving into past
                            for (int i = 0; i < phases.Count; i++)
                            {
                                phases[i].StartDate = phases[i].StartDate.Add(offset);
                            }
                            phases[phases.Count - 1].EndDate = phases[phases.Count - 1].EndDate.Add(offset);
                        }
                    }

                    RefreshPhases();
                    RaisePropertyChanged("StartDate");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of days into the future in which to
        /// automatically (and persistantly) schedule upcoming Garmin Fitness 
        /// workouts.  A setting of -1 will disable automatic scheduling.  
        /// Anything greater will cause workouts to be automatically 
        /// scheduled upon.  For instance, a setting of 7 will automatically
        /// schedule upcoming workouts in the next 7 days.
        /// </summary>
        public int AutoScheduleDays
        {
            get { return autoScheduleDays; }
            set
            {
                if (autoScheduleDays == value) return;

                autoScheduleDays = value;
                ScheduleCurrentWorkouts();
                RaisePropertyChanged("AutoScheduleDays");
            }
        }

        /// <summary>
        /// Gets the last date of any phase in the plan
        /// </summary>
        [XmlIgnore]
        public DateTime EndDate
        {
            get
            {
                DateTime date = DateTime.MinValue;

                foreach (Phase phase in Phases)
                {
                    if (date < phase.EndDate)
                    {
                        date = phase.EndDate;
                    }
                }

                return date;
            }
            set
            {
                if (EndDate != value)
                {
                    DateTime date = value;
                    Phase lastPhase = null;

                    foreach (Phase phase in Phases)
                    {
                        if (lastPhase == null || lastPhase.StartDate < phase.StartDate)
                        {
                            lastPhase = phase;
                        }
                    }

                    if (lastPhase != null)
                    {
                        if (date < lastPhase.StartDate)
                        {
                            date = lastPhase.StartDate.AddDays(1);
                        }

                        lastPhase.EndDate = date;
                    }
                }

                RaisePropertyChanged("EndDate");
            }
        }

        /// <summary>
        /// Training Plan Name
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// Gets the entire structure to be displayed in the tree.
        /// </summary>
        [XmlIgnore]
        public TreeNodeCollection TreeListRowData
        {
            get
            {
                return nodeList;
            }
        }

        public DayCollection Days
        {
            // Added in v0.4
            get
            {
                if (days == null)
                    days = new DayCollection();

                return days;
            }
            set
            {
                days = value;
            }
        }

        [XmlAttribute]
        public string PluginVersion
        {
            get { return new PluginMain().Version; }
            set { }
        }

        [XmlAttribute]
        public string FileVersion
        {
            get
            {
                if (string.IsNullOrEmpty(fileVersion)) return "0.0";
                return fileVersion;
            }
            set { fileVersion = value; }
        }

        /// <summary>
        /// Unique identifier for this plan.
        /// </summary>
        [XmlAttribute]
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

        /// <summary>
        /// Gets a value indicating if this plan is an older format needing to be updated.
        /// </summary>
        internal bool MigrateRequired
        {
            get
            {
                if (string.IsNullOrEmpty(fileVersion)) return true;
                return new Version(fileVersion) < currentFileVer;
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
                    foreach (Phase item in Phases)
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
                    foreach (Phase phase in Phases)
                    {
                        phase.Modified = value;
                    }
                }
                else
                {
                    PluginMain.GetApplication().Logbook.Modified = true;
                }
            }
        }

        /// <summary>
        /// Gets or Sets the full filename including path.
        /// </summary>
        [XmlIgnore]
        internal string FilePath
        {
            get
            {
                return filename;
            }
            set
            {
                if (filename != value)
                {
                    filename = value;
                    RaisePropertyChanged("FileName");
                }
            }
        }
        /// <summary>
        /// Gets the filename and extension.  Filename only, does not include path.
        /// </summary>
        [XmlIgnore]
        internal string FileName
        {
            get { return Path.GetFileName(FilePath); }
        }

        /// <summary>
        /// Gets the default filename including path: 'PluginDataFolder\Athlete's Training Plan.xml'
        /// </summary>
        [XmlIgnore]
        internal string DefaultFileName
        {
            get
            {
                return Path.Combine(GlobalSettings.Main.FolderPath, Name + PluginMain.PlanFileExtension);
            }
        }

        /// <summary>
        /// Gets the plan TreeNode object for this Training Plan
        /// </summary>
        internal FitPlanNode PlanNode
        {
            get
            {
                return node;
            }
        }

        [XmlIgnore]
        internal FitPlan.Calendar.UserCalendar CalendarProvider
        {
            get
            {
                if (calProvider == null)
                    calProvider = new FitPlan.Calendar.GoogleProvider(this);

                return calProvider;
            }
        }

        #endregion

        #region Methods

        public DateItemCollection GetCalendarItems(MonthCalendar calendar)
        {
            DateItemCollection dates = new DateItemCollection(calendar);

            foreach (Phase phase in phases)
            {
                dates.Add(phase.CalendarItem);

                foreach (Workout workout in phase.Workouts)
                {
                    dates.Add(workout.CalendarItem);
                }
            }

            return dates;
        }

        /// <summary>
        /// Add a phase to this training plan.  This has the potential to 
        /// delete Workouts if not done properly.  New phase will be added at
        /// the specified StartDate, then all other phases will be re-calculated.
        /// If this shortens a phase, it could cause Workouts in this plan to be
        /// deleted.  Use InsertPhase to add a phase non-destructively.
        /// </summary>
        /// <param name="phase">Phase to be added to the plan.</param>
        public void AddPhase(Phase phase)
        {
            phases.Add(phase);
            RefreshPhases();
            RaisePropertyChanged("Phases");
        }

        /// <summary>
        /// This will non-destructively insert a phase into the training plan.
        /// The new phase will be inserted after the phase already containing this
        /// StartDate, and before the next contiguous phase.  All phases occurring
        /// after phase.StartDate will be shifted into the future by phase.TotalDays.
        /// All phases will maintain the same duration, thus eliminating the potential
        /// for any workouts to be truncated/deleted.
        /// </summary>
        /// <param name="phase"></param>
        /// <returns>the newly added Phase.  The phase dates may be modified during the 
        /// insert routine to avoid overwriting workouts and to insert exactly in between
        /// 2 phases in this training plan.</returns>
        public Phase InsertPhase(Phase phase)
        {
            int i = this.phases.Count - 1;

            // Ensure we're inserting the phase withing the bouds of the plan.  
            // If not, no changes to the plan or phase are required.
            if (this.phases.Count > 0 && phase.StartDate < this.EndDate && this.StartDate < phase.EndDate)
            {
                // Shift future phases out into the future starting from the end.
                while (0 <= i && this.phases[i].EndDate > phase.StartDate)
                {
                    this.phases[i].Shift(phase.TotalDays);
                    i--;
                }

                if (0 <= i)
                    phase.Shift((int)(this.phases[i].EndDate - phase.StartDate).TotalDays + 1);
                else
                    phase.Shift((int)(this.StartDate - phase.StartDate).TotalDays - phase.TotalDays);
            }

            AddPhase(phase);

            return phase;
        }

        /// <summary>
        /// Delete a phase from this training plan.
        /// Nothing is changed if the Plan doesn't include this phase.
        /// </summary>
        /// <param name="phase">Phase to be removed</param>
        /// <param name="shortenPlan">True to remove phase, and shift later phases up to cover the gap.
        /// False to remove this phase and leave a gap that will be covered by the previous phase.</param>
        public void DeletePhase(Phase phase, bool shortenPlan)
        {
            phase.PropertyChanged -= phase_PropertyChanged;
            phase.StartDateChanging -= phase_StartDateChanging;

            // Shift future phases back in to cover the void
            if (shortenPlan)
                foreach (Phase item in phases)
                    if (phase.StartDate < item.StartDate)
                        item.Shift(-phase.TotalDays);

            phases.Remove(phase);
            RefreshPhases();
            RaisePropertyChanged("Phases");
        }

        /// <summary>
        /// Searches through all workouts in the plan, finds the containing phase
        /// and removes the workout.
        /// </summary>
        /// <param name="workout"></param>
        public void RemoveWorkout(Workout workout)
        {
            foreach (Phase phase in phases)
            {
                if (phase.Workouts.Contains(workout))
                {
                    phase.RemoveWorkout(workout);
                    break;
                }
            }
        }

        /// <summary>
        /// Remove <paramref name="workout"/> from <paramref name="origin"/> and
        /// add it to <paramref name="destination"/>
        /// </summary>
        /// <param name="workout">Workout to be moved</param>
        /// <param name="origin">Origin phase currently containing the workout</param>
        /// <param name="destination">Destination phase to which the workout will be added.</param>
        public void MoveWorkout(Workout workout, Phase origin, Phase destination)
        {
            // Move from one phase to the right phase
            origin.RemoveWorkout(workout);
            destination.AddWorkout(workout);
        }

        /// <summary>
        /// Gets the phase that contains this date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public Phase GetPhase(DateTime date)
        {
            if (Phases == null)
            {
                return null;
            }

            // Search for a current phase
            foreach (Phase phase in Phases)
            {
                if (phase.StartDate <= date.Date && date.Date <= phase.EndDate)
                {
                    return phase;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the phase chronologically following this phase
        /// </summary>
        /// <param name="phase">Search for the phase after this one</param>
        /// <returns>Returns the phase that occurs after 'phase', or the current phase 
        /// (passed in as argument) if this is the last phase or null if phase not found
        /// in this plan.</returns>
        public Phase GetNextPhase(Phase phase)
        {
            int current = Phases.IndexOf(phase);
            int next = current + 1;

            if (current < 0)
            {
                // Phase not found
                return null;
            }
            else if (next < Phases.Count)
            {
                // Return next phase
                return Phases[next];
            }
            else if (Phases.Count == next)
            {
                // This was the last phase
                return phase;
            }

            // should never hit here
            return null;
        }

        /// <summary>
        /// Gets the chronologically preceeding phase to this one in the training plan.
        /// </summary>
        /// <param name="phase">Search for the phase before this one</param>
        /// <returns>Returns the phase that occurs before 'phase', or the current phase 
        /// (passed in as argument) if this is the first phase or null if phase not found
        /// in this plan.</returns>
        public Phase GetPreviousPhase(Phase phase)
        {
            int current = Phases.IndexOf(phase);
            int prev = current - 1;

            if (current < 0)
            {
                // Phase not found
                return null;
            }
            else if (0 <= prev)
            {
                // Return previous phase
                return Phases[prev];
            }
            else if (prev < 0)
            {
                // This was the first phase
                return phase;
            }

            // should never hit here
            return null;
        }

        /// <summary>
        /// Find workout with matching Id.  
        /// </summary>
        /// <remarks>This method will search all workouts in this 
        /// training plan and return the workout with the matching 
        /// ReferenceId. If no matching workout is found, return null.
        /// </remarks>
        /// <param name="id">ReferenceId to search</param>
        /// <returns>the requested workout, or null if no match is found.
        /// </returns>
        public Workout GetWorkout(string id)
        {
            foreach (Workout workout in GetWorkouts())
            {
                if (workout.ReferenceId == id)
                {
                    return workout;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets all child workouts of a particular parent workout
        /// </summary>
        /// <param name="parent">Parent workout</param>
        /// <returns>List of workouts that are children of the requested parent.  An empty collection
        /// will be returned if none are found</returns>
        public WorkoutCollection GetWorkouts(Workout parent)
        {
            WorkoutCollection workouts = new WorkoutCollection();

            // Look through all workouts and find workouts that occur on this specific date
            // Typically this would only be 1 workout, but can be multiple.
            foreach (Workout workout in GetWorkouts())
            {
                if (workout.ParentId == parent.ReferenceId)
                {
                    workouts.Add(workout);
                }
            }

            // Return workout list or empty list if none exist.
            return workouts;
        }

        /// <summary>
        /// Gets all workouts in the plan on a specific date.  Time is ignored.
        /// </summary>
        /// <param name="date">Date to search</param>
        /// <returns>List of workouts on a specific date.  Typically this may only be 1 workout.</returns>
        public WorkoutCollection GetWorkouts(DateTime date)
        {
            // Find the phase where this date occurs
            Phase phase = GetPhase(date);

            if (phase != null && phase.Workouts != null)
            {
                return phase.GetWorkouts(date);
            }

            // Return empty list if none exist.
            return new WorkoutCollection();
        }

        /// <summary>
        /// Gets all workouts in the plan between specific start/end dates.
        /// </summary>
        /// <param name="start">Inclusive start date (workouts on this date will be included)</param>
        /// <param name="end">Inclusive end date (workouts on this date will be included</param>
        /// <returns>Returns a collection of workouts occuring on or between these dates.</returns>
        public WorkoutCollection GetWorkouts(DateTime start, DateTime end)
        {
            WorkoutCollection workouts = new WorkoutCollection();

            // Find the phase where this date occurs
            foreach (Phase phase in phases)
            {
                if (phase != null && phase.Workouts != null && phase.StartDate <= end && start <= phase.EndDate)
                {
                    workouts.AddRange(phase.GetWorkouts(start, end));
                }
            }

            return workouts;
        }

        /// <summary>
        /// Gets all workouts in the training plan.  
        /// This returns a separate item for each instance of a repeating workout.
        /// </summary>
        /// <returns>Returns all workouts contained in this training plan, 
        /// or a new empty collection if none are found</returns>
        public WorkoutCollection GetWorkouts()
        {
            WorkoutCollection workouts = new WorkoutCollection();

            foreach (Phase phase in Phases)
            {
                workouts.AddRange(phase.Workouts);
            }

            return workouts;
        }

        /// <summary>
        /// Create and return a list of all graph objects associated with this plan.
        /// </summary>
        /// <returns></returns>
        public GraphObjList GetPlanGraphObjs()
        {
            GraphObjList theList = new GraphObjList();

            foreach (Phase phase in this.Phases)
            {
                theList.Add(phase.TitleObj);
                theList.Add(phase.GradientObj);
            }

            return theList;
        }

        /// <summary>
        /// Returns true if this plan contains a workout on the specified date (time is ignored).
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool ContainsWorkout(DateTime date)
        {
            return GetWorkouts(date).Count > 0;
        }

        /// <summary>
        /// Gets the custom formatted text with proper decimal places, etc.
        /// </summary>
        /// <param name="Id">Column defines the property value to get</param>
        /// <returns>Returns custom formatted string ready for display to user.</returns>
        public string GetFormattedText(string Id)
        {
            Type planType = typeof(TrainingPlan);  // Used to collect property value from activity
            string text = null;                         // text to display in cell (if defined)

            if (Id == "StartDate" ||
                Id == "EndDate")
            {
                // 9/11/2010
                DateTime value = (DateTime)planType.GetProperty(Id).GetValue(this, null);

                if (value == DateTime.MinValue || value == null)
                    text = string.Empty;

                else
                    text = value.ToString("d", CultureInfo.CurrentCulture);
            }
            else
            {
                // Default
                PropertyInfo info = planType.GetProperty(Id);

                if (info != null)
                {
                    object value = info.GetValue(this, null);

                    if (value != null)
                        text = value.ToString();
                }
            }

            return text;
        }

        /// <summary>
        /// Read in a Fit Plan xml file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static TrainingPlan OpenPlan(string path)
        {
            Loaded.initializing = true;
            if (Opening != null)
                Opening(path, new PropertyChangingEventArgs("Plan"));

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                Loaded.initializing = false;
                if (Opened != null)
                    Opened(path, new PropertyChangedEventArgs("Plan"));

                return null;
            }

            // Read Training Plan file
            FileStream fs = null;
            try
            {
                // Deserialization
                XmlSerializer xs = new XmlSerializer(typeof(TrainingPlan));

                fs = new FileStream(path, FileMode.Open);
                TrainingPlan plan = new TrainingPlan();

                // Read Training Plan file

                plan = (TrainingPlan)xs.Deserialize(fs);

                // Initialize phases & workouts
                plan.phases.Sort();
                foreach (Phase phase in plan.Phases)
                {
                    phase.UpdateSubscriptions();
                    phase.Workouts.Sort();
                }

                plan.FilePath = path;
                FitPlan.Settings.LogbookSettings.Main.CurrentTrainingPlan = path;
                Settings.GlobalSettings.Main.FolderPath = System.IO.Path.GetDirectoryName(path);

                return plan;
            }
            // TODO: (MED) Make file opening error specific, not generic.  Errors are being caught here leading people to believe files are corrupt when that's not the case.
            catch (Exception e)
            {
                string msg = CommonResources.Text.ErrorText_FileCouldNotBeOpened + Environment.NewLine + e.Message + Environment.NewLine + e.StackTrace;
                int i = 0;
                while (e.InnerException != null && i < 10)
                {
                    e = e.InnerException;
                    msg += Environment.NewLine + e.Message + Environment.NewLine + e.StackTrace;
                    i++;
                }

                MessageDialog.Show(msg, Resources.Strings.Label_FitPlan, System.Windows.Forms.MessageBoxButtons.OK);
            }
            finally
            {
                Loaded.initializing = false;
                if (Opened != null)
                    Opened(path, new PropertyChangedEventArgs("Plan"));

                fs.Close();
            }

            return null;
        }

        /// <summary>
        /// Save Training Plan to file.  Optionally, a prompt can be presented to the user if desired. 
        /// </summary>
        /// <param name="promptSave">If true, always prompt user to save, no matter if you're updating
        /// a file, overwriting an existing file, or saving a new file.  A prompt will always be presented.
        /// If false, a prompt will only be presented if you're about to overwrite an existing (but different)
        /// Fit Plan, or if the filename has not yet been established; a new plan for example.</param>
        /// <returns>Returns true if the user saves, or declines to save, and false if the users selects 'Cancel'.</returns>
        public bool SavePlan(bool promptSave)
        {
            // Serialization
            XmlSerializer xs = new XmlSerializer(typeof(TrainingPlan));
            string path;

            if (string.IsNullOrEmpty(FilePath))
            {
                // New file, never saved
                // Save As dialog...
                if (promptSave)
                {
                    string savePrompt = string.Format("{0} {1}? ", CommonResources.Text.ActionSave, this.Name);
                    switch (MessageDialog.Show(savePrompt, Strings.Label_FitPlan, System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Question))
                    {
                        case System.Windows.Forms.DialogResult.Cancel:
                            // No save.  Cancelled.
                            return false;

                        case System.Windows.Forms.DialogResult.No:
                            // No save.  User declined saving.
                            return true;
                    }
                }

                System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
                dlg.DefaultExt = PluginMain.PlanFileExtension;
                dlg.Filter = PluginMain.PlanFileFilter;
                dlg.FilterIndex = 1;
                dlg.AddExtension = true;
                dlg.InitialDirectory = GlobalSettings.Main.FolderPath;
                dlg.FileName = Path.GetFileName(DefaultFileName);

                if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    // Cancel or no selected in Save As dialog
                    return false;
                }
                else
                {
                    // Continue with new filename
                    path = dlg.FileName;
                }
            }
            else if (promptSave && Modified)
            {
                // Existing File, prompt to save (overwrite)
                string savePrompt = string.Format("{0} {1} ({2})? ", CommonResources.Text.ActionSave, this.Name, this.FileName);
                switch (MessageDialog.Show(savePrompt, Strings.Label_FitPlan, System.Windows.Forms.MessageBoxButtons.YesNoCancel, System.Windows.Forms.MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Cancel:
                        // No save.  Cancelled.
                        return false;

                    case System.Windows.Forms.DialogResult.No:
                        // No save.  User declined saving.
                        return true;
                }


                // Existing file.  Overwrite (update) file if exists
                path = FilePath;
            }
            else
            {
                // No prompt, just save.
                path = FilePath;
            }
            
            // Perform the save
            Settings.GlobalSettings.Main.FolderPath = System.IO.Path.GetDirectoryName(path);
            FilePath = path;

            // TODO: (HOLD) Omit byte order mark in XML document?
            //XmlTextWriter xmlTextWriter = new XmlTextWriter(path, new UTF8Encoding(false)); // Omits byte order mark: http://stackoverflow.com/questions/67959/net-xml-serialization-gotchas
            XmlTextWriter xmlTextWriter = new XmlTextWriter(path, Encoding.UTF8); // Includes byte order mark

            xs.Serialize(xmlTextWriter, this as TrainingPlan);
            xmlTextWriter.Close();

            LogbookSettings.Main.CurrentTrainingPlan = path;
            this.Modified = false;

            // Automatically sync calendar if logged in upon saving plan
            if (CalendarProvider != null && this.IsCalendarAutoSync && CalendarProvider.LoginAvailable)
            {
                CalendarProvider.SyncPlan();
            }

            return true;
        }

        /// <summary>
        /// Sort and update end dates of to ensure phases are ordered properly and are contiguous
        /// </summary>
        internal void RefreshPhases()
        {
            phases.Sort();

            if (phases.Count < 2)
            {
                return;
            }

            // Adjust end dates to ensure phases are contiguous
            for (int i = phases.Count - 2; i >= 0; i--)
            {
                if (phases[i].StartDate < phases[i + 1].StartDate.AddDays(-1))
                {
                    phases[i].EndDate = phases[i + 1].StartDate.AddDays(-1);
                }
                else
                {
                    phases[i].EndDate = phases[i].StartDate;
                }
            }
        }

        /// <summary>
        /// Show or Hide the Phase Summary nodes as determined by GlobalSettings.ShowPhaseSummary
        /// </summary>
        internal void UpdateSummaryNodes()
        {
            if (GlobalSettings.Main.ShowPhaseSummary)
            {
                // Cycle through all phases and add summary nodes after the respective phase
                foreach (Phase phase in this.Phases)
                {
                    if (!PlanNode.Children.Contains(phase.AverageNode))
                    {
                        int i = PlanNode.Children.IndexOf(phase.Node);

                        // Insert at the proper spot
                        PlanNode.Children.Insert(i + 1, phase.TotalNode);
                        PlanNode.Children.Insert(i + 1, phase.DailyNode);
                        PlanNode.Children.Insert(i + 1, phase.AverageNode);
                    }
                }

            }
            else
            {
                foreach (Phase phase in this.Phases)
                {
                    // Remove summary nodes from list
                    if (PlanNode.Children.Contains(phase.AverageNode))
                        PlanNode.Children.Remove(phase.AverageNode);

                    if (PlanNode.Children.Contains(phase.DailyNode))
                        PlanNode.Children.Remove(phase.DailyNode);

                    if (PlanNode.Children.Contains(phase.TotalNode))
                        PlanNode.Children.Remove(phase.TotalNode);
                }
            }
        }

        /// <summary>
        /// Schedule all current workouts.  Current is defined as the
        /// StartDate occurring between today and Plan.AutoScheduleDays
        /// in the future.
        /// Garmin Fitness keeps track of the schedule and will prevent
        /// duplicates from being scheduled.
        /// </summary>
        internal void ScheduleCurrentWorkouts()
        {
            WorkoutCollection workouts = this.GetWorkouts();

            if (Data.GarminFitness.Manager.IsInstalled)
            {
                foreach (Workout workout in workouts)
                    if (DateTime.Today <= workout.StartDate && workout.StartDate <= DateTime.Today.AddDays(this.autoScheduleDays))
                        Data.GarminFitness.Manager.ScheduleWorkout(workout);

                // Clear these because GF will populate the calendar as it's scheduled (prevent 'odd' behaviour)
                PluginMain.GetApplication().Calendar.SetHighlightedDates(null);
            }
        }

        /// <summary>
        /// Update all 'Actual' type data associated with all workouts in this plan.  For example, this will update the ActualDistance parameter for workouts.
        /// </summary>
        internal void LinkActivities()
        {
            foreach (Phase phase in Phases)
            {
                phase.LinkActivities();
            }
        }

        private void phases_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            // Anytime the phase collection changes, update the nodelist.
            // This will properly manage the object collection (TreeNode collection)
            // rather than re-creating it each time.
            // Once the list is added to the treelist, it should self manage at that point.
            Phase phase = e.Element as Phase;

            if (phase != null && e.Action == CollectionChangeAction.Add)
            {
                // Add a new phase node to the collection
                phase.Node.Parent = PlanNode;
                phase.AverageNode.Parent = PlanNode;
                phase.DailyNode.Parent = PlanNode;
                phase.TotalNode.Parent = PlanNode;

                phase.PropertyChanged += phase_PropertyChanged;
                phase.StartDateChanging += new System.Windows.Forms.DateRangeEventHandler(phase_StartDateChanging);
                phase.Workouts.CollectionChanged += new CollectionChangeEventHandler(Workouts_CollectionChanged);
                // Add a new node to the collection
                if (PlanNode.Children.Count == 0)
                {
                    // First child
                    PlanNode.Children.Add(phase.Node);
                    if (GlobalSettings.Main.ShowPhaseSummary)
                    {
                        PlanNode.Children.Add(phase.AverageNode);
                        PlanNode.Children.Add(phase.DailyNode);
                        PlanNode.Children.Add(phase.TotalNode);
                    }
                }
                else
                {
                    // Insert node item in the right spot
                    for (int i = 0; i < PlanNode.Children.Count; i++)
                    {
                        Phase item = PlanNode.Children[i].Element as Phase;
                        if (item != null && phase.StartDate < item.StartDate)
                        {
                            // Insert at the proper spot, or at the end
                            if (GlobalSettings.Main.ShowPhaseSummary)
                            {
                                PlanNode.Children.Insert(i, phase.TotalNode);
                                PlanNode.Children.Insert(i, phase.DailyNode);
                                PlanNode.Children.Insert(i, phase.AverageNode);
                            }
                            PlanNode.Children.Insert(i, phase.Node);
                            break;
                        }
                        else if (i == PlanNode.Children.Count - 1)
                        {
                            // Add to end
                            PlanNode.Children.Add(phase.Node);
                            if (GlobalSettings.Main.ShowPhaseSummary)
                            {
                                PlanNode.Children.Add(phase.AverageNode);
                                PlanNode.Children.Add(phase.DailyNode);
                                PlanNode.Children.Add(phase.TotalNode);
                            }
                            break;
                        }
                    }
                }
            }
            else if (phase != null && e.Action == CollectionChangeAction.Remove)
            {
                // Remove phase node from list
                if (PlanNode.Children.Contains(phase.Node))
                    PlanNode.Children.Remove(phase.Node);

                if (PlanNode.Children.Contains(phase.AverageNode))
                    PlanNode.Children.Remove(phase.AverageNode);

                if (PlanNode.Children.Contains(phase.DailyNode))
                    PlanNode.Children.Remove(phase.DailyNode);

                if (PlanNode.Children.Contains(phase.TotalNode))
                    PlanNode.Children.Remove(phase.TotalNode);

            }

            // Fire PhasesChanged event
            if (PhasesChanged != null)
                PhasesChanged.Invoke(this, e);
        }

        private void phase_StartDateChanging(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            Phase phase = sender as Phase;
            Phase prevPhase = this.GetPreviousPhase(phase);

            if (e.End < e.Start)
            {
                // Moving into Past... truncate earlier phase first.
                // Handle DateLocked Workouts that may be forced to a new phase
                WorkoutCollection workouts = prevPhase.GetWorkouts(e.End, e.Start);
                foreach (Workout workout in workouts)
                {
                    if (workout.DateLocked)
                        this.MoveWorkout(workout, prevPhase, phase);
                }

                if (phase != null && phase != prevPhase)
                    prevPhase.EndDate = e.End.AddDays(-1);
            }
            else
            {
                // Moving StartDate into future.  
                // Handle DateLocked Workouts that may be forced to a new phase
                WorkoutCollection workouts = phase.GetWorkouts(e.Start, e.End.AddDays(-1), true, false);
                foreach (Workout workout in workouts)
                {
                    this.MoveWorkout(workout, phase, prevPhase);
                }
            }
        }

        private void Workouts_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            // This needs to propogate all the way up to the schedule control
            // so that it can manage the display objects (calendar items, etc.)
            if (WorkoutsChanged != null)
            {
                // Bubble up...
                WorkoutsChanged.Invoke(sender, e);
            }
        }

        private void Days_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            RaisePropertyChanged("Days");
        }

        private void phase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("Phase." + e.PropertyName);
        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));

            if (!Loaded.initializing)
                Modified = true;
        }

        #endregion
    }
}
