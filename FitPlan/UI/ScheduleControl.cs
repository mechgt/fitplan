namespace FitPlan.UI
{
    using System;
    using System.Collections.Generic;
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
    using FitPlan.Calendar;

    public partial class ScheduleControl : UserControl
    {
        #region Fields

        private int blinkCount;
        private TrainingPlan currentPlan;
        private DateItemCollection activityItems;
        private ActivitiesCollection activities;
        private Workout selectedWorkout;
        private Phase selectedPhase;
        private DateTime mouseDate;
        private ToolTip tip;
        private bool workoutSubscribed = true;
        private Point tooltipMousePt;

        // TODO: Implement movable dates via drag/drop
        private DateTime dragDate;
        private bool isDraggingDate;

        #endregion

        #region Enumerations

        #endregion

        #region Constructors

        public ScheduleControl()
        {
            GlobalSettings.PropertyChanged += new PropertyChangedEventHandler(GlobalSettings_PropertyChanged);
            TrainingPlan.Opening += new PropertyChangingEventHandler(TrainingPlan_Opening);
            TrainingPlan.Opened += new PropertyChangedEventHandler(TrainingPlan_Opened);

            InitializeComponent();

            LogbookSettings.LoadSettings();

            // Initialize control items
            radDistance.Tag = txtWorkoutDistance;
            radTime.Tag = txtWorkoutTime;
            radPace.Tag = txtWorkoutPace;
            radLibDist.Tag = txtLibDist;
            radLibTime.Tag = txtLibTime;
            radLibPace.Tag = txtLibPace;
            radPace.Checked = true;
            radLibPace.Checked = true;
            dailyY3.Tag = -3;
            dailyY2.Tag = -2;
            dailyY1.Tag = -1;
            dailyToday.Tag = 0;
            dailyT1.Tag = 1;
            dailyT2.Tag = 2;
            dailyT3.Tag = 3;
            mnuTreeWorkoutLibrary.Tag = GlobalSettings.TreeOption.Library;
            mnuTreePlanOverview.Tag = GlobalSettings.TreeOption.TrainingPlan;
            btnImage.Tag = "Workout";
            btnLibImage.Tag = "Template";

            trainingCal.MouseWheel += new MouseEventHandler(trainingCal_MouseWheel);
            trainingCal.ImageList = GlobalImages.CalendarImageList;
            trainingCal.Month.ImageAlign = mcItemAlign.TopRight;
            trainingCal.Month.Calendar.FirstDayOfWeek = (int)PluginMain.GetApplication().SystemPreferences.StartOfWeek + 1;
            trainingCal.Month.SelectedMonth = DateTime.Now.Date;
            trainingCal.Setup();
            trainingCal.Dates.Add(GetLogbookCalendarItems());
            PluginMain.GetApplication().Logbook.BeforeSave += new EventHandler(Logbook_BeforeSave);
            PluginMain.LogbookChanged += new PropertyChangedEventHandler(PluginMain_LogbookChanged);
            PluginMain.GetApplication().Logbook.Activities.CollectionChanged += new NotifyCollectionChangedEventHandler<IActivity>(Activities_CollectionChanged);
            hiLoSlider1.SliderMoved += new PropertyChangedEventHandler(hiLoSlider1_SliderMoved);
            activityItems = new DateItemCollection(trainingCal);
            tip = new ToolTip();

            // Initialize to user settings
            radTrimp.Checked = LogbookSettings.Main.ScoreType == Common.Score.Trimp;
            radTSS.Checked = LogbookSettings.Main.ScoreType == Common.Score.TSS;
            mnuTreeShowSummary.Checked = GlobalSettings.Main.ShowPhaseSummary;
            mnuTreeShowEmptyFolders.Checked = GlobalSettings.Main.ShowEmptyFolders;
            mnuChartShowPhases.Checked = GlobalSettings.Main.ShowChartPhases;
            mnuChartShowCTLtarget.Checked = GlobalSettings.Main.ShowCTLTarget;
            GlobalSettings_PropertyChanged(null, new PropertyChangedEventArgs("ShowCTLTarget")); // Enable/Disable edit button.
            menuGroupDay.Checked = GlobalSettings.Main.GroupBy == GlobalSettings.GroupOption.Day;
            menuGroupWeek.Checked = GlobalSettings.Main.GroupBy == GlobalSettings.GroupOption.Week;
            menuGroupMonth.Checked = GlobalSettings.Main.GroupBy == GlobalSettings.GroupOption.Month;
            menuGroupCategory.Checked = GlobalSettings.Main.GroupBy == GlobalSettings.GroupOption.Category;
            btnGroupBy.Enabled = GlobalSettings.Main.ChartType == GlobalSettings.ChartOption.Progress && !GlobalSettings.Main.IsProgressCumulative;
            mnuChartTrainingLoad.Checked = GlobalSettings.Main.ChartType == GlobalSettings.ChartOption.TrainingLoad;
            mnuChartDistance.Checked = GlobalSettings.Main.ChartType == GlobalSettings.ChartOption.Progress && GlobalSettings.Main.ProgressChart == GlobalSettings.ProgressOption.Distance;
            mnuChartDistanceCum.Checked = GlobalSettings.Main.ChartType == GlobalSettings.ChartOption.Progress && GlobalSettings.Main.ProgressChart == GlobalSettings.ProgressOption.CumulativeDistance;
            mnuChartTime.Checked = GlobalSettings.Main.ChartType == GlobalSettings.ChartOption.Progress && GlobalSettings.Main.ProgressChart == GlobalSettings.ProgressOption.Time;
            mnuChartTimeCum.Checked = GlobalSettings.Main.ChartType == GlobalSettings.ChartOption.Progress && GlobalSettings.Main.ProgressChart == GlobalSettings.ProgressOption.CumulativeTime;

            if (Data.TrainingLoadPlugin.IsInstalled)
                bnrChart.Text = "CTL/TSB";
            else
                btnTrainingLoad.Enabled = false;

            // Setup Garmin Fitness display items
            grpGarminFitness.Enabled = Data.GarminFitness.Manager.IsInstalled;
            chkGarminAutoSched.Enabled = Data.GarminFitness.Manager.IsInstalled;
            txtPlanGarminAutoSched.Enabled = Data.GarminFitness.Manager.IsInstalled;
            lblPlanDays.Enabled = Data.GarminFitness.Manager.IsInstalled;
            btnGarminFitness.Enabled = Data.GarminFitness.Manager.IsInstalled;

            // Reset stubborn controls that continue to resize automatically
            weekPanel.ColumnStyles[0].Width = 26;
            weekPanel.ColumnStyles[weekPanel.ColumnStyles.Count - 1].Width = 23;

            btnMaxUpper.Location = new Point(bnrChart.Width - 48, 0);
            btnZoomFit.Location = new Point(btnMaxUpper.Location.X - 24, 0);
            btnTrainingLoad.Location = new Point(btnZoomFit.Location.X - 24, 0);
            btnRampEdit.Location = new Point(btnTrainingLoad.Location.X - 24, 0);
            btnGroupBy.Location = new Point(btnRampEdit.Location.X - btnGroupBy.Width, 0);

            btnMaxLower.Location = new Point(bnrCalendar.Width - 48, 0);
            btnNewTemplate.Location = new Point(bnrTree.Width - 48, 0);
            btnDelTemplate.Location = new Point(btnNewTemplate.Location.X - 24, 0);

            // Initialize display
            RefreshAutoSchedule();
            RefreshWeekView(); // TODO: Is causing FitPlan to construct this control twice (caused double settings Load).  Possibly related to training load requesting data.
            zedChart.GraphPane.GraphObjList.Add(ChartData.HighlightFocus);
            zedChart.GraphPane.GraphObjList.Add(ChartData.HighlightToday);

            if (GlobalSettings.Main.IsChartMaximized)
            {
                SwapChartCalendar();
            }

            // TODO: Address static CTL Hi/Lo settings below
            tabControl.TabPages.Remove(tabPlanning);
            LogbookSettings.Main.CTLRampHighLim = 7;
            LogbookSettings.Main.CTLRampLowLim = 4;
            hiLoSlider1.Low = LogbookSettings.Main.CTLRampLowLim;
            hiLoSlider1.High = LogbookSettings.Main.CTLRampHighLim;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties

        internal static string ScoreText
        {
            get
            {
                if (LogbookSettings.Main.ScoreType == Common.Score.Trimp)
                {
                    return Strings.Label_Trimp;
                }
                else
                {
                    return Strings.Label_TSS;
                }
            }
        }

        internal ActivitiesCollection Activities
        {
            get { return activities; }
            set { activities = value; }
        }

        /// <summary>
        /// Gets the date currently selected on the calendar
        /// </summary>
        internal DateTime SelectedCalendarDate
        {
            get
            {
                if (trainingCal.SelectedDates.Count > 0)
                {
                    return trainingCal.SelectedDates[0].Date;
                }

                return DateTime.Now.Date;
            }
        }

        /// <summary>
        /// Gets the workout of the current training plan 
        /// </summary>
        internal Workout SelectedCalendarWorkout
        {
            get
            {
                Workout workout = CollectionUtils.GetFirstItemOfType<Workout>(SelectedPlan.GetWorkouts(SelectedCalendarDate));
                return workout;
            }
        }

        /// <summary>
        /// Gets the Phase (explicit or parent) currently selected on the treelist.
        /// If a workout is currently selected, this will return the parent phase.
        /// </summary>
        internal Phase SelectedPhase
        {
            get { return selectedPhase; }
        }

        /// <summary>
        /// Gets the currently selected workout thats loaded in the details view.
        /// </summary>
        internal Workout SelectedWorkout
        {
            get { return selectedWorkout; }
        }

        /// <summary>
        /// Gets the Template currently selected in the item tree.  Null if none selected.
        /// </summary>
        internal WorkoutDefinition SelectedTemplate
        {
            get
            {
                LibraryNode node = CollectionUtils.GetFirstItemOfType<LibraryNode>(itemTree.SelectedItems);
                if (node != null)
                {
                    return node.Workout;
                }
                else
                {
                    return null;
                }
            }
        }

        internal TrainingPlan SelectedPlan
        {
            get
            {
                return currentPlan;
            }
        }

        #endregion

        #region Theme & Culture

        /// <summary>
        /// Update visual theme
        /// </summary>
        /// <param name="visualTheme">Theme to load into the display</param>
        public void ThemeChanged(ITheme visualTheme)
        {
            itemTree.ThemeChanged(visualTheme);

            tabControl.ThemeChanged(visualTheme);
            tabWeek.BackColor = visualTheme.Window;
            tabDetails.BackColor = visualTheme.Window;
            tabPlanning.BackColor = visualTheme.Window;

            trainingCal.Header.BackColor2 = visualTheme.MainHeader;
            trainingCal.Header.BackColor1 = Color.WhiteSmoke;
            trainingCal.Header.GradientMode = mcGradientMode.Vertical;
            trainingCal.Header.TextColor = visualTheme.MainHeaderText;
            trainingCal.Weekdays.TextColor = visualTheme.ControlText;
            trainingCal.Weekdays.BackColor1 = visualTheme.MainHeader;
            trainingCal.Weekdays.BackColor2 = Color.WhiteSmoke;
            trainingCal.Weekdays.GradientMode = mcGradientMode.Vertical;
            trainingCal.Month.Colors.Trailing.Date = ControlPaint.Light(visualTheme.ControlText, .5f);
            trainingCal.Month.Colors.Selected.BackColor = Color.FromArgb(50, visualTheme.Selected);
            trainingCal.Month.Colors.Selected.Border = Color.FromArgb(255, visualTheme.Selected);
            trainingCal.Month.Colors.Focus.BackColor = Color.FromArgb(150, visualTheme.Selected);
            trainingCal.Month.Colors.Focus.Border = Color.FromArgb(255, visualTheme.Selected);

            // Set first day of week.  0-Default, 1-Sunday, 2-Monday, etc. (thus the +1)
            trainingCal.Month.Calendar.FirstDayOfWeek = (int)PluginMain.GetApplication().SystemPreferences.StartOfWeek + 1;
            trainingCal.Month.Calendar.Refresh();

            txtPhaseEnd.ReadOnlyColor = visualTheme.Window;
            txtPhaseName.ReadOnlyColor = visualTheme.Window;
            txtPhaseStart.ReadOnlyColor = visualTheme.Window;
            txtPlanName.ReadOnlyColor = visualTheme.Window;
            txtPlanGarminAutoSched.ReadOnlyColor = visualTheme.Window;
            txtWorkoutDistance.ReadOnlyColor = visualTheme.Window;
            txtWorkoutEnd.ReadOnlyColor = visualTheme.Window;
            txtWorkoutName.ReadOnlyColor = visualTheme.Window;
            txtWorkoutNotes.ReadOnlyColor = visualTheme.Window;
            txtWorkoutPace.ReadOnlyColor = visualTheme.Window;
            txtWorkoutPeriod.ReadOnlyColor = visualTheme.Window;
            txtWorkoutRamp.ReadOnlyColor = visualTheme.Window;
            txtWorkoutScore.ReadOnlyColor = visualTheme.Window;
            txtWorkoutSpeed.ReadOnlyColor = visualTheme.Window;
            txtWorkoutTime.ReadOnlyColor = visualTheme.Window;
            txtWorkoutCategory.ReadOnlyColor = visualTheme.Window;
            txtLibDist.ReadOnlyColor = visualTheme.Window;
            txtLibName.ReadOnlyColor = visualTheme.Window;
            txtLibNotes.ReadOnlyColor = visualTheme.Window;
            txtLibPace.ReadOnlyColor = visualTheme.Window;
            txtLibScore.ReadOnlyColor = visualTheme.Window;
            txtLibSpeed.ReadOnlyColor = visualTheme.Window;
            txtLibTime.ReadOnlyColor = visualTheme.Window;
            txtLibCategory.ReadOnlyColor = visualTheme.Window;
            txtLibCategory.ReadOnlyTextColor = visualTheme.ControlText;
            txtLibNotes.ReadOnlyTextColor = visualTheme.ControlText;
            txtLibName.ReadOnlyTextColor = visualTheme.ControlText;
            txtGarminWorkout.ReadOnlyColor = visualTheme.Window;
            txtGarminWorkout.ReadOnlyTextColor = visualTheme.ControlText;

            dailyY3.ThemeChanged(visualTheme);
            dailyY2.ThemeChanged(visualTheme);
            dailyY1.ThemeChanged(visualTheme);
            dailyToday.ThemeChanged(visualTheme);
            dailyT1.ThemeChanged(visualTheme);
            dailyT2.ThemeChanged(visualTheme);
            dailyT3.ThemeChanged(visualTheme);
            WeekSummary.ThemeChanged(visualTheme);

            bnrChart.ThemeChanged(visualTheme);
            bnrTree.ThemeChanged(visualTheme);

            ThemedContextMenuStripRenderer menuRenderer = new ZoneFiveSoftware.Common.Visuals.ThemedContextMenuStripRenderer(visualTheme);
            menuChart.Renderer = menuRenderer;
            menuTree.Renderer = menuRenderer;
            contextCalendar.Renderer = menuRenderer;
            contextTreelist.Renderer = menuRenderer;

            zedChart.GraphPane.BarSettings.Type = BarType.Overlay;
            zedChart.GraphPane.BarSettings.ClusterScaleWidth = 1.9;
            zedChart.ThemeChanged(visualTheme);
        }

        public void UICultureChanged(CultureInfo culture)
        {
            tabWeek.Text = Strings.Label_WeekView;
            tabDetails.Text = Strings.Label_Details;
            tabPlanning.Text = Strings.Label_Planning;

            lblPhase.Text = Strings.Label_Phase;
            lblPlanEnd.Text = Strings.Label_End;
            lblPlanHelp.Text = Strings.Text_PlanHelp;
            lblPlanStart.Text = CommonResources.Text.LabelStart;
            lblWorkoutDays.Text = Time.LabelPlural(Time.TimeRange.Day);
            lblWorkoutEnd.Text = Strings.Label_End;
            lblWorkoutMi.Text = Length.LabelAbbr(PluginMain.DistanceUnits);
            lblWorkoutMph.Text = Speed.Label(Speed.Units.Speed, new Length(1, PluginMain.DistanceUnits));
            lblLibMi.Text = Length.LabelAbbr(PluginMain.DistanceUnits);
            lblLibMph.Text = Speed.Label(Speed.Units.Speed, new Length(1, PluginMain.DistanceUnits));
            lblWorkoutRamp.Text = Strings.Label_Ramp;
            lblWorkoutRepeat.Text = Strings.Label_Repeat;
            lblWorkoutScore.Text = ScoreText;
            lblNotes.Text = CommonResources.Text.LabelNotes + ":";
            lblPhaseDuration.Text = string.Format("{0}: \r\n{1} {2}", Strings.Label_Duration, 0, Time.LabelPlural(Time.TimeRange.Week));
            lblCategory.Text = CommonResources.Text.LabelCategory;
            lblPhaseStart.Text = CommonResources.Text.LabelStart;
            lblPhaseEnd.Text = Strings.Label_End;
            lblWorkoutTitle.Text = Strings.Label_Workout + ":";
            btnGroupBy.Text = CommonResources.Text.LabelGroupBy;

            chkRepeat.Text = Strings.Label_Repeat;
            radDistance.Text = CommonResources.Text.LabelDistance;
            radPace.Text = string.Format("{0}\r\n{1}", CommonResources.Text.LabelSpeed, CommonResources.Text.LabelPace);
            radTime.Text = CommonResources.Text.LabelTime;
            radTrimp.Text = Strings.Label_Trimp;
            radTSS.Text = Strings.Label_TSS;

            // TreeList context menu
            ctxTreeAddPhase.Text = string.Format(Strings.Action_Add, Strings.Label_Phase);
            ctxTreeInsertPhase.Text = string.Format(Strings.Action_Insert, Strings.Label_Phase);
            ctxTreeAddWorkout.Text = string.Format(Strings.Action_Add, Strings.Label_Workout);
            ctxTreeDelete.Text = CommonResources.Text.ActionRemove;
            ctxTreeClosePlan.Text = string.Format(Strings.Action_Close, Strings.Label_Plan);

            // Calendar Context menu
            ctxCalLockWorkout.Text = Strings.Action_LockDate;
            ctxCalAddPhase.Text = string.Format(Strings.Action_Add, Strings.Label_Phase);
            ctxCalAddWorkout.Text = string.Format(Strings.Action_Add, Strings.Label_Workout);
            ctxCalDeleteWorkout.Text = string.Format(Strings.Action_Delete, Strings.Label_Workout);
            ctxCalAddTemplate.Text = string.Format(Strings.Action_CreateTemplate, Resources.Strings.Label_Workout);
            ctxCalScheduleWorkout.Text = Strings.Action_ScheduleWorkout;

            // Chart Menu
            mnuChartTrainingLoad.Text = Strings.Label_TrainingLoad;
            mnuChartShowCTLtarget.Text = string.Format(Strings.Action_Show, Strings.Label_CTLtarget);
            mnuChartShowPhases.Text = string.Format(Strings.Action_Show, Strings.Label_Phases);
            mnuChartDistance.Text = CommonResources.Text.LabelDistance;
            mnuChartDistanceCum.Text = string.Format("{0} ({1})", CommonResources.Text.LabelDistance, Strings.Label_Cumulative);
            mnuChartTime.Text = CommonResources.Text.LabelTime;
            mnuChartTimeCum.Text = string.Format("{0} ({1})", CommonResources.Text.LabelTime, Strings.Label_Cumulative);

            // TreeList Menu
            mnuTreePlanOverview.Text = Strings.Label_PlanOverview;
            mnuTreeWorkoutLibrary.Text = Strings.Label_WorkoutLibrary;
            mnuTreeShowSummary.Text = string.Format(Strings.Action_Show, Strings.Label_Summary);
            mnuTreeShowEmptyFolders.Text = string.Format(Strings.Action_Show, Strings.Label_EmptyFolders);

            // Detail tab group labels
            grpPhase.Text = Strings.Label_Phase;
            grpPlan.Text = Strings.Label_Plan;
            grpWorkout.Text = Strings.Label_Workout;
            grpRepeat.Text = Strings.Label_Repeat;

            // Template group
            grpTemplate.Text = Strings.Label_Template;
            radLibDist.Text = CommonResources.Text.LabelDistance;
            radLibPace.Text = string.Format("{0}\r\n{1}", CommonResources.Text.LabelSpeed, CommonResources.Text.LabelPace);
            radLibTime.Text = CommonResources.Text.LabelTime;
            radLibScore.Text = ScoreText;

            WeekSummary.HeadingText = string.Format("{0}:", Strings.Label_Summary);
        }

        #endregion

        #region Methods

        #region Load

        /// <summary>
        /// Loads the selected plan into the display.  Assembles all event handlers, and puts everything together to run.
        /// </summary>
        /// <param name="plan">TrainingPlan to be loaded</param>
        /// <param name="saveCurrent">True to attempt a save the current plan, false to just simply close it, even if it's been Modified.</param>
        internal void LoadPlan(TrainingPlan plan, bool saveCurrent)
        {
            EnablePlanEvents(false);

            // Only refresh display if loading a new plan.
            if (SelectedPlan != plan && plan != null)
            {
                if (SelectedPlan != null && saveCurrent && SelectedPlan.Modified)
                    SelectedPlan.SavePlan(true);

                // Close old plan first
                ClosePlan(SelectedPlan);

                // Duplicates are prevented from being loaded (see AddPlan)
                currentPlan = Loaded.AddPlan(plan);

                trainingCal.Dates.Add(SelectedPlan.GetCalendarItems(trainingCal));
                trainingCal.Refresh();

                txtPlanName.Enabled = true;
                txtPlanName.Text = SelectedPlan.Name;
                autoSyncToolStripMenuItem.Checked = SelectedPlan.IsCalendarAutoSync;

                if (0 <= SelectedPlan.AutoScheduleDays && chkGarminAutoSched.Checked == false)
                    chkGarminAutoSched.Checked = true;
                else if (SelectedPlan.AutoScheduleDays < 0)
                    chkGarminAutoSched.Checked = false;

                RefreshPlanDetails();

                ChartData.SetTrainingPlan(SelectedPlan);
                SelectedPlan.PropertyChanged += new PropertyChangedEventHandler(currentPlan_PropertyChanged);
                SelectedPlan.WorkoutsChanged += new CollectionChangeEventHandler(currentPlan_WorkoutsChanged);
                SelectedPlan.PhasesChanged += new CollectionChangeEventHandler(currentPlan_PhasesChanged);
                SelectedPlan.Days.CollectionChanged += new CollectionChangeEventHandler(Days_CollectionChanged);

                // Combine multiple workout dates in calendar where appropriate
                foreach (Workout workout in SelectedPlan.GetWorkouts())
                {
                    // monitor start date changes to manage multi-workout days better
                    workout.StartDateChanged += new DateRangeEventHandler(workout_StartDateChanged);

                    // Combine multiple workout days on the calendar
                    DateTime date = workout.StartDate.Date;
                    DateItemCollection dates = GetDateItemType(date, "Workout");

                    if (1 < dates.Count)
                    {
                        // Handle managing the display on the calendar, particularly multi-workout day instances
                        WorkoutCollection workouts = SelectedPlan.GetWorkouts(date);
                        if (1 < workouts.Count)
                        {
                            // Multiple workouts on this date
                            // Remove existing WORKOUT items
                            foreach (DateItem item in dates)
                            {
                                string tag = item.Tag as string;
                                if (!tag.StartsWith("WorkoutMultiple"))
                                    item.Visible = false;
                            }

                            // Add new workout item (multiple)
                            DateItem multiples = GetMutipleWorkoutItem(workouts);
                            trainingCal.Dates.Add(multiples);

                            foreach (Workout item in workouts)
                            {
                                item.PropertyChanged += multiple_PropertyChanged;
                            }
                        }
                    }
                }

                foreach (Phase phase in SelectedPlan.Phases)
                {
                    phase.GradientObj.IsVisible = GlobalSettings.Main.ShowChartPhases;
                    zedChart.GraphPane.GraphObjList.Add(phase.GradientObj);

                    phase.TitleObj.IsVisible = GlobalSettings.Main.ShowChartPhases;
                    zedChart.GraphPane.GraphObjList.Add(phase.TitleObj);
                }

                // Autoschedule Garmin Fitness workouts
                SelectedPlan.ScheduleCurrentWorkouts();

                // Associate actual values from Activities
                SelectedPlan.LinkActivities();

                // Property Only raised when changing plans
                RaisePropertyChanged("TrainingPlan");

                RefreshTree();
                RefreshAutoSchedule();
                SortCalendarItems();
                ChartData.RefreshTargetRampColors();

                Phase currentPhase = SelectedPlan.GetPhase(DateTime.Now);
                if (currentPhase != null)
                {
                    itemTree.SelectedItemsChanged -= itemTreePlanItem_SelectedItemsChanged;
                    itemTree.SetExpanded(currentPhase.Node, true);
                    itemTree.SelectedItems = new TreeNodeCollection(currentPhase.Node);
                    itemTree.SelectedItemsChanged += itemTreePlanItem_SelectedItemsChanged;
                    LoadPhase(currentPhase);

                    trainingCal.SelectDate(DateTime.Today);
                }

                RefreshChart();
                ZoomPlan(true);
            }
            else if (SelectedPlan == null)
            {
                txtPlanName.Text = string.Empty;
                txtPlanName.Enabled = false;
            }

            EnablePlanEvents(true);
        }

        /// <summary>
        /// Close plan by removing all related display elements (calendar items, etc.)
        /// and setting plan to null.
        /// </summary>
        /// <param name="plan">TrainingPlan to be closed</param>
        internal void ClosePlan(TrainingPlan plan)
        {
            if (plan == null)
            {
                if (zedChart.GraphPane.CurveList.Contains(ChartData.TargetCTL))
                    // Remove target CTL from chart.
                    zedChart.GraphPane.CurveList.Remove(ChartData.TargetCTL);

                return;
            }

            Loaded.RemovePlan(plan);

            // Remove plan related date items from the calendar (but not independant ST activities)
            foreach (DateItem item in plan.GetCalendarItems(trainingCal))
            {
                if (trainingCal.Dates.Contains(item))
                    trainingCal.Dates.Remove(item);
            }

            int i = 0;
            while (i < trainingCal.Dates.Count)
            {
                string tag = trainingCal.Dates[i].Tag as string;
                if (tag.StartsWith("Workout"))
                    trainingCal.RemoveDateInfo(trainingCal.Dates[i]);
                else
                    i++;
            }

            // Remove graph objects (title and phase gradients)
            foreach (GraphObj item in plan.GetPlanGraphObjs())
                zedChart.GraphPane.GraphObjList.Remove(item);

            // Remove target CTL from chart.
            zedChart.GraphPane.CurveList.Remove(ChartData.TargetCTL);

            plan = null;

            Refresh();
        }

        internal void OpenPlanDialog()
        {
            if (SelectedPlan != null && SelectedPlan.Modified && !SelectedPlan.SavePlan(true))
                return; // User cancelled saving current plan.  Do nothing.

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Settings.GlobalSettings.Main.FolderPath;
            dlg.Filter = PluginMain.PlanFileFilter;
            dlg.Multiselect = false;

            if (dlg.ShowDialog() == DialogResult.OK)
                LoadPlan(TrainingPlan.OpenPlan(dlg.FileName), false);
        }

        /// <summary>
        /// Loads the selected workout into the display
        /// </summary>
        /// <param name="workout">Workout to be loaded</param>
        internal void LoadWorkout(Workout workout)
        {
            EnableWorkoutEvents(false);
            selectedWorkout = workout;

            int week = 0;

            if (workout == null)
            {
                if (SelectedCalendarDate != null && SelectedPlan != null && SelectedPlan.StartDate <= SelectedCalendarDate && SelectedCalendarDate <= SelectedPlan.EndDate)
                    week = (SelectedCalendarDate - SelectedPlan.StartDate).Days / 7 + 1;

                txtWorkoutDistance.Text = string.Empty;
                txtWorkoutEnd.Text = string.Empty;
                txtWorkoutName.Text = string.Empty;
                txtWorkoutPace.Text = string.Empty;
                txtWorkoutRamp.Text = string.Empty;
                txtWorkoutScore.Text = string.Empty;
                txtWorkoutSpeed.Text = string.Empty;
                txtWorkoutTime.Text = string.Empty;
                txtWorkoutPeriod.Text = string.Empty;
                txtWorkoutNotes.Text = string.Empty;
                txtWorkoutCategory.Text = string.Empty;
                txtGarminWorkout.Text = Strings.Label_DragTemplate;
                lblWorkoutTitle.Text = string.Format("{0}: {1} {2}", Strings.Label_Workout, CommonResources.Text.LabelWeek, week);

                txtWorkoutDistance.Enabled = false;
                txtWorkoutEnd.Enabled = false;
                txtWorkoutName.Enabled = false;
                txtWorkoutPace.Enabled = false;
                txtWorkoutRamp.Enabled = false;
                txtWorkoutScore.Enabled = false;
                txtWorkoutSpeed.Enabled = false;
                txtWorkoutTime.Enabled = false;
                txtWorkoutPeriod.Enabled = false;
                txtWorkoutNotes.Enabled = false;
                txtWorkoutCategory.Enabled = false;

                chkRepeat.CheckState = CheckState.Unchecked;
                chkRepeat.Enabled = false;
                btnLink.Visible = false;
                btnParent.Visible = false;
                btnImage.Enabled = false;
                btnImage.BackgroundImageLayout = ImageLayout.Center;
                btnImage.BackgroundImage = CommonResources.Images.Camera16;
                btnGarminWorkout.BackgroundImage = Images.GarminWorkoutGrey16;
                btnLockWorkout.BorderStyle = BorderStyle.None;
                btnLockWorkout.Enabled = false;

                EnableWorkoutEvents(true);
                return;
            }
            else if (workout.IsRepeating)
            {
                txtWorkoutEnd.Enabled = true;
                txtWorkoutRamp.Enabled = true;
                txtWorkoutPeriod.Enabled = true;
                chkRepeat.Enabled = true;
                txtWorkoutEnd.Text = workout.EndDate.ToString("d", CultureInfo.CurrentCulture);
                txtWorkoutRamp.Text = workout.GetFormattedText("Ramp");
                txtWorkoutPeriod.Text = workout.PeriodDays.ToString(CultureInfo.CurrentCulture);
                chkRepeat.CheckState = CheckState.Checked;
                btnLink.Enabled = true;
                btnLink.Visible = true;

                Workout parent = workout.GetParent(SelectedPlan);
                if (string.IsNullOrEmpty(workout.Notes) && parent != null)
                    txtWorkoutNotes.Text = parent.Notes;
                else
                    txtWorkoutNotes.Text = workout.Notes;
            }
            else
            {
                txtWorkoutEnd.Enabled = false;
                txtWorkoutRamp.Enabled = false;
                txtWorkoutPeriod.Enabled = false;
                chkRepeat.Enabled = true;
                txtWorkoutEnd.Text = string.Empty;
                txtWorkoutRamp.Text = string.Empty;
                txtWorkoutPeriod.Text = string.Empty;
                chkRepeat.CheckState = CheckState.Unchecked;
                btnLink.Enabled = true;
                btnLink.Visible = true;
                txtWorkoutNotes.Text = workout.Notes;
            }

            if (workout.IsChild)
            {
                chkRepeat.CheckState = CheckState.Indeterminate;
                chkRepeat.Enabled = false;

                Workout parent = workout.GetParent(SelectedPlan);
                if (parent != null)
                    tip.SetToolTip(btnParent, CommonResources.Text.ActionOpen + " " + parent.StartDate.ToShortDateString());
                else
                    tip.SetToolTip(btnParent, string.Empty);
            }

            btnParent.Visible = workout.IsChild;

            if (workout.IsLinked)
                btnLink.BorderStyle = BorderStyle.Fixed3D;
            else
                btnLink.BorderStyle = BorderStyle.None;

            double distance = Length.Convert(workout.TotalDistanceMeters, Length.Units.Meter, PluginMain.DistanceUnits);

            if (workout.LinkedTemplate == null)
                btnGarminWorkout.BackgroundImage = Images.GarminWorkoutGrey16;
            else if (workout.LinkedTemplate.IsGarminWorkout)
                btnGarminWorkout.BackgroundImage = Images.GarminWorkout16;
            else
                btnGarminWorkout.BackgroundImage = Images.Calendar;

            txtWorkoutDistance.Enabled = true;
            txtWorkoutName.Enabled = true;
            txtWorkoutScore.Enabled = true;
            txtWorkoutSpeed.Enabled = true;
            txtWorkoutPace.Enabled = true;
            txtWorkoutTime.Enabled = true;
            txtWorkoutNotes.Enabled = true;
            txtWorkoutCategory.Enabled = true;
            btnImage.Enabled = true;
            btnImage.Visible = true;
            btnLockWorkout.Enabled = true;
            btnLockWorkout.BorderStyle = workout.DateLocked ? BorderStyle.Fixed3D : BorderStyle.None;

            txtWorkoutDistance.Text = workout.GetFormattedText("TotalDistanceMeters");
            txtWorkoutName.Text = workout.Name;
            txtWorkoutScore.Text = workout.GetFormattedText("Score");
            txtWorkoutSpeed.Text = workout.GetFormattedText("MetersPerSecond");
            week = (workout.StartDate - SelectedPlan.StartDate).Days / 7 + 1;
            lblWorkoutTitle.Text = string.Format("{0}: {1} ({2} {3})", Strings.Label_Workout, workout.StartDate.ToString("d", CultureInfo.CurrentCulture), CommonResources.Text.LabelWeek, week);
            txtWorkoutPace.Text = workout.GetFormattedText("PaceMinPerMeter");
            txtWorkoutTime.Text = workout.GetFormattedText("TotalTime");
            txtWorkoutCategory.Text = workout.GetFormattedText("Category");

            if (workout.LinkedTemplate == null)
                txtGarminWorkout.Text = Strings.Label_DragTemplate;
            else
                txtGarminWorkout.Text = workout.GetFormattedText("TemplateId");

            Image image = GlobalImages.GetImage(workout.ImageName);
            if (image != null)
            {
                btnImage.BackgroundImageLayout = ImageLayout.Zoom;
                btnImage.BackgroundImage = image;
            }
            else
            {
                btnImage.BackgroundImageLayout = ImageLayout.Center;
                btnImage.BackgroundImage = CommonResources.Images.Camera16;
            }

            lblWorkoutMi.Text = Length.LabelAbbr(PluginMain.DistanceUnits);
            lblWorkoutMph.Text = Speed.Label(Speed.Units.Speed, new Length(1, PluginMain.DistanceUnits));

            EnableWorkoutEvents(true);
        }

        internal void LoadTemplate(WorkoutDefinition template)
        {
            EnableTemplateEvents(false);

            txtLibTime.Enabled = template != null;
            txtLibDist.Enabled = template != null;
            txtLibPace.Enabled = template != null;
            txtLibScore.Enabled = template != null;
            txtLibSpeed.Enabled = template != null;

            txtLibName.Enabled = template != null && !template.IsGarminWorkout;
            txtLibNotes.Enabled = template != null && !template.IsGarminWorkout;
            txtLibCategory.Enabled = template != null && !template.IsGarminWorkout;

            if (template == null)
            {
                txtLibName.Text = string.Empty;
                txtLibDist.Text = string.Empty;
                txtLibNotes.Text = string.Empty;
                txtLibPace.Text = string.Empty;
                txtLibScore.Text = string.Empty;
                txtLibSpeed.Text = string.Empty;
                txtLibTime.Text = string.Empty;
                txtLibCategory.Text = string.Empty;

                EnableTemplateEvents(true);
                return;
            }
            else
            {
                txtLibName.Text = template.Name;
                txtLibDist.Text = template.GetFormattedText("TotalDistanceMeters");
                txtLibNotes.Text = template.Notes;
                txtLibPace.Text = template.GetFormattedText("PaceMinPerMeter");
                txtLibScore.Text = template.GetFormattedText("Score");
                txtLibSpeed.Text = template.GetFormattedText("MetersPerSecond");
                txtLibTime.Text = template.GetFormattedText("TotalTime");
                txtLibCategory.Text = template.GetFormattedText("Category");
            }

            Image image = GlobalImages.GetImage(template.ImageName);
            if (image != null)
            {
                btnLibImage.BackgroundImageLayout = ImageLayout.Zoom;
                btnLibImage.BackgroundImage = image;
            }
            else
            {
                btnLibImage.BackgroundImageLayout = ImageLayout.Center;
                btnLibImage.BackgroundImage = CommonResources.Images.Camera16;
            }

            EnableTemplateEvents(true);
        }

        /// <summary>
        /// Loads the selected phase into the display
        /// </summary>
        /// <param name="phase">Phase to be loaded</param>
        internal void LoadPhase(Phase phase)
        {
            EnablePhaseEvents(false);

            if (phase == null)
            {
                txtPhaseEnd.Text = string.Empty;
                txtPhaseName.Text = string.Empty;
                txtPhaseStart.Text = string.Empty;

                txtPhaseEnd.Enabled = false;
                txtPhaseName.Enabled = false;
                txtPhaseStart.Enabled = false;
            }
            else
            {
                txtPhaseEnd.Enabled = true;
                txtPhaseEnd.Text = phase.EndDate.ToString("d", CultureInfo.CurrentCulture);
                txtPhaseName.Enabled = true;
                txtPhaseName.Text = phase.Name;
                txtPhaseStart.Enabled = true;
                txtPhaseStart.Text = phase.StartDate.ToString("d", CultureInfo.CurrentCulture);

                int time = (phase.EndDate - phase.StartDate).Days + 1;
                string units;

                if (time >= 14)
                {
                    time = time / 7;
                    units = Time.LabelPlural(Time.TimeRange.Week);
                }
                else if (time > 2)
                {
                    units = Time.LabelPlural(Time.TimeRange.Day);
                }
                else
                {
                    units = Time.Label(Time.TimeRange.Day);
                }

                lblPhaseDuration.Text = string.Format("{0}:\r\n  {1} {2}", Strings.Label_Duration, time, units);
            }

            EnablePhaseEvents(true);
        }

        /// <summary>
        /// Initialize the structure of the tree.  Build the columns, set the title, define the formatting providers, etc.
        /// </summary>
        /// <param name="option">How to load the tree.  Similar to a display mode.</param>
        private void LoadTree(GlobalSettings.TreeOption option)
        {
            switch (option)
            {
                default:
                case GlobalSettings.TreeOption.TrainingPlan:
                    itemTree.Columns.Clear();

                    // Load Phases
                    foreach (ColumnDef column in PlanOverviewColumns.SelectedColumns)
                    {
                        // TODO: I should be able to add the column def object here instead of creating a new column object?  This would handle dynamic text also.
                        itemTree.Columns.Add(new TreeList.Column(column.Id, column.Text(null), column.Width, column.Align));
                    }

                    itemTree.ShowPlusMinus = true;

                    itemTree.LabelProvider = new PhaseTreeLabelProvider();
                    itemTree.RowDataRenderer = new PhaseTreeRenderer(itemTree);

                    bnrTree.Text = Strings.Label_PlanOverview;
                    pnlLibrary.Hide();
                    break;

                case GlobalSettings.TreeOption.Library:
                    itemTree.Columns.Clear();

                    // Load Phases
                    foreach (ColumnDef column in LibraryColumns.SelectedColumns)
                    {
                        // TODO: I should be able to add the column def object here instead of creating a new column object.  This would handle dynamic text also.
                        itemTree.Columns.Add(new TreeList.Column(column.Id, column.Text(null), column.Width, column.Align));
                    }

                    itemTree.ShowPlusMinus = true;

                    itemTree.LabelProvider = new LibraryLabelProvider();
                    itemTree.RowDataRenderer = new LibraryTreeRenderer(itemTree);

                    bnrTree.Text = Strings.Label_WorkoutLibrary;
                    pnlLibrary.Show();
                    break;
            }

            // Refresh the data displayed on the tree
            RefreshTree();
        }

        #endregion

        #region Add Remove

        internal TrainingPlan AddPlan(bool promptSave)
        {
            // Prompt to save current plan if selected
            if (SelectedPlan != null && SelectedPlan.Modified && promptSave)
            {
                if (!SelectedPlan.SavePlan(promptSave))
                {
                    // User cancelled save.
                    return SelectedPlan;
                }
            }

            // Select start date
            if (SelectedCalendarDate == null)
                trainingCal.SelectDate(DateTime.Now.ToLocalTime().Date);

            // Create new default plan (different lengths based on trial/full version)
            TrainingPlan plan;

            CreatePlan dlg = new CreatePlan(SelectedCalendarDate);
            DialogResult result = dlg.ShowDialog();

            if (result == DialogResult.OK)
                plan = dlg.Plan;
            else
                return SelectedPlan;

            // Load new plan into display and return it.
            LoadPlan(plan, false);
            return plan;
        }

        /// <summary>
        /// Add a phase to this training plan.  Phase will be added at 
        /// selected date and continue until the end of the current phase.
        /// Workouts within this time frame (selected date to end date of 
        /// existing phase) will be moved into the new phase.
        /// </summary>
        /// <param name="date">Start date for the new phase</param>
        /// <returns>the newly created phase</returns>
        internal Phase AddPhase(DateTime date)
        {
            // Find end date
            DateTime end;
            WorkoutCollection moveWorkouts = new WorkoutCollection();
            Phase currentPhase = SelectedPlan.GetPhase(date);

            if (currentPhase != null)
            {
                end = currentPhase.EndDate;
                moveWorkouts = currentPhase.GetWorkouts(date, DateTime.MaxValue);
            }
            else
            {
                end = date.AddDays(7);
            }

            // Create new phase and add to plan
            Phase phase = new Phase(string.Format(Resources.Strings.Action_New, Resources.Strings.Label_Phase), date, (end - date).Days + 1, Utilities.RandomColor(255, 170));
            SelectedPlan.AddPhase(phase);
            trainingCal.Dates.Add(phase.CalendarItem);

            foreach (Workout item in moveWorkouts)
            {
                if (!item.IsChild)
                {
                    // Move parent workouts to new phase
                    currentPhase.RemoveWorkout(item);
                    phase.AddWorkout(item);
                    UpdateWorkoutSeries(item);
                }
                else
                {
                    // If we're orphaning a child, then update the parent's end date to be proper
                    Workout parent = SelectedPlan.GetWorkout(item.ParentId);
                    if (parent != null && SelectedPlan.GetPhase(parent.StartDate) != phase)
                    {
                        parent.EndDate = phase.StartDate.AddDays(-1);
                        UpdateWorkoutSeries(parent);
                    }
                }
            }

            SortCalendarItems();
            trainingCal.Month.Calendar.Refresh();
            RefreshTree();
            RefreshChart();

            foreach (FitPlanNode item in SelectedPlan.TreeListRowData)
            {
                if (item.Element == phase)
                {
                    itemTree.SelectedItems.Clear();
                    itemTree.SelectedItems = new List<FitPlanNode> { item };
                    break;
                }
            }

            itemTree.Refresh();

            return phase;
        }

        /// <summary>
        /// Add a phase to this training plan.  Phase will be inserted at 
        /// selected date shifting later phases by the duration of the 
        /// inserted phase.
        /// </summary>
        /// <param name="date">Start date for the new phase</param>
        /// <param name="days">Duration of the new phase</param>
        /// <returns>the newly created phase</returns>
        internal Phase InsertPhase(DateTime date, int days)
        {
            Phase phase = new Phase(string.Format(Resources.Strings.Action_New, Resources.Strings.Label_Phase), date, days, Utilities.RandomColor(255, 170));

            phase = SelectedPlan.InsertPhase(phase);
            trainingCal.Dates.Add(phase.CalendarItem);
            trainingCal.Month.Calendar.Refresh();
            RefreshTree();
            RefreshChart();

            foreach (FitPlanNode item in SelectedPlan.TreeListRowData)
            {
                if (item.Element == phase)
                {
                    itemTree.SelectedItems.Clear();
                    itemTree.SelectedItems = new List<FitPlanNode> { item };
                    break;
                }
            }

            trainingCal.SelectDate(phase.StartDate);
            trainingCal.Setup();
            itemTree.Refresh();

            return phase;
        }

        internal void RemovePhase(Phase phase)
        {
            // Remove Phase
            if (phase == null || !SelectedPlan.Phases.Contains(phase))
            {
                // Oops...?!?
                return;
            }

            Phase prevPhase = SelectedPlan.GetPreviousPhase(phase);
            Phase nextPhase = SelectedPlan.GetNextPhase(phase);

            if (phase.Workouts.Count > 0)
            {
                if (prevPhase != null && prevPhase != phase)
                {
                    // Offer option to move workouts to new phase
                    switch (MessageDialog.Show(string.Format(Strings.Text_DeletePhaseWarning, phase.Workouts.Count, prevPhase.Name), Strings.Label_FitPlan, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                    {
                        case DialogResult.Yes:
                            // Move workouts
                            WorkoutCollection workouts = phase.Workouts;
                            prevPhase.EndDate = phase.EndDate;
                            while (0 < workouts.Count)
                            {
                                Workout workout = workouts[0];
                                phase.Workouts.Remove(workout);
                                prevPhase.Workouts.Add(workout);
                            }

                            trainingCal.Dates.Remove(phase.CalendarItem);
                            SelectedPlan.DeletePhase(phase, false);
                            break;

                        case DialogResult.No:
                            // Delete workouts
                            trainingCal.Dates.Remove(phase.CalendarItem);
                            SelectedPlan.DeletePhase(phase, true);
                            break;

                        default:
                        case DialogResult.Cancel:
                            // Cancel operation
                            return;
                    }
                }
                else if (nextPhase != null && nextPhase != phase)
                {
                    // Specifically for moving the first phase as it's handled slightly different from all other phases.
                    // Offer option to move workouts to new phase
                    switch (MessageDialog.Show(string.Format(Strings.Text_DeletePhaseWarning, phase.Workouts.Count, nextPhase.Name), Strings.Label_FitPlan, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                    {
                        case DialogResult.Yes:
                            // Move workouts
                            WorkoutCollection workouts = nextPhase.Workouts;
                            phase.EndDate = nextPhase.EndDate;
                            phase.Name = nextPhase.Name;
                            phase.DisplayColor = nextPhase.DisplayColor;

                            while (0 < workouts.Count)
                            {
                                Workout workout = workouts[0];
                                nextPhase.Workouts.Remove(workout);
                                phase.Workouts.Add(workout);
                            }

                            trainingCal.Dates.Remove(nextPhase.CalendarItem);
                            SelectedPlan.DeletePhase(nextPhase, false);
                            break;

                        case DialogResult.No:
                            // Delete workouts & shift plan forward to next phase date
                            trainingCal.Dates.Remove(phase.CalendarItem);
                            SelectedPlan.StartDate = nextPhase.StartDate;
                            SelectedPlan.DeletePhase(phase, true);
                            break;

                        default:
                        case DialogResult.Cancel:
                            // Cancel operation
                            return;
                    }
                }
                else
                {
                    // This is the only phase in the plan and cannot be deleted.
                }
            }
            else if (1 < SelectedPlan.Phases.Count)
            {
                // Empty phase
                trainingCal.Dates.Remove(phase.CalendarItem);

                if (prevPhase == null || prevPhase == phase)
                    SelectedPlan.StartDate = nextPhase.StartDate;

                SelectedPlan.DeletePhase(phase, true);
            }
            else
            {
                // This is the only phase in the plan and cannot be deleted.  Phase contains no workouts.
            }

            selectedPhase = SelectedPlan.GetPhase(SelectedCalendarDate);
            phase = null;

            LoadPhase(selectedPhase);
            SortCalendarItems();
            trainingCal.Refresh();
            RefreshTree();
            RefreshChart();
        }

        /// <summary>
        /// Add a new default workout on a particular date
        /// </summary>
        /// <param name="date">Date of the workout</param>
        /// <returns>Returns the newly created workout.</returns>
        internal Workout AddWorkout(DateTime date)
        {
            if (SelectedPlan == null || SelectedPlan.GetPhase(date) == null)
                return null;

            Phase phase = SelectedPlan.GetPhase(date);
            Workout workout;

            // Single workout
            workout = new Workout(date, 0, 0, date.ToString("ddd", CultureInfo.CurrentCulture) + " " + Strings.Label_Workout);

            phase.AddWorkout(workout);

            RefreshTree();
            RefreshChart();

            LoadWorkout(workout);
            ChartData.Calculated = false;
            RefreshWeekView();

            trainingCal.Month.Calendar.Refresh();

            if (tabControl.SelectedTab == tabDetails)
            {
                txtWorkoutName.Focus();
            }

            return workout;
        }

        /// <summary>
        /// Add a pre-defined workout to the training plan and calendars
        /// </summary>
        /// <param name="workout">Workout to be added to the plan.</param>
        /// <returns>Returns the Workout</returns>
        internal Workout AddWorkout(Workout workout)
        {
            if (SelectedPlan == null || SelectedPlan.GetPhase(workout.StartDate) == null)
                return null;

            Phase phase = SelectedPlan.GetPhase(workout.StartDate);
            phase.AddWorkout(workout);

            RefreshTree(false, true);
            RefreshChart();
            LoadWorkout(workout);
            ChartData.Calculated = false;
            RefreshWeekView();

            return workout;
        }

        internal void RemoveWorkout(Workout workout)
        {
            if (workout != null)
            {
                // Remove workout
                SelectedPlan.RemoveWorkout(workout);

                LoadWorkout(null);
                trainingCal.Month.Calendar.Refresh();
                RefreshTree();
                RefreshChart();
            }
        }

        #endregion

        #region Refresh

        /// <summary>
        /// Refresh the chart that typically resides in upper right of window.
        /// </summary>
        internal void RefreshChart()
        {
            switch (GlobalSettings.Main.ChartType)
            {
                default:
                case GlobalSettings.ChartOption.TrainingLoad:
                    bnrChart.Text = string.Format("{0}/{1} - {2}", Strings.Label_CTL, Strings.Label_TSB, ScoreText);
                    RefreshTrainingLoadChart(zedChart);
                    break;

                case GlobalSettings.ChartOption.Progress:
                    if (GlobalSettings.Main.ProgressChart == GlobalSettings.ProgressOption.CumulativeDistance || GlobalSettings.Main.ProgressChart == GlobalSettings.ProgressOption.Distance)
                        bnrChart.Text = string.Format("{0} - {1}", Strings.Label_Progress, CommonResources.Text.LabelDistance);

                    else
                        bnrChart.Text = string.Format("{0} - {1}", Strings.Label_Progress, CommonResources.Text.LabelTime);

                    RefreshProgressChart(zedChart);
                    break;
            }
        }

        /// <summary>
        /// Refresh the tree ONLY if it's required
        /// </summary>
        /// <param name="libraryRefresh"></param>
        /// <param name="trainingPlanRefresh"></param>
        internal void RefreshTree(bool libraryRefresh, bool trainingPlanRefresh)
        {
            if (libraryRefresh && GlobalSettings.Main.TreeType == GlobalSettings.TreeOption.Library)
                RefreshTree();

            else if (trainingPlanRefresh && GlobalSettings.Main.TreeType == GlobalSettings.TreeOption.TrainingPlan)
                RefreshTree();
        }

        /// <summary>
        /// Refresh the data displayed on the tree.
        /// </summary>
        internal void RefreshTree()
        {
            switch (GlobalSettings.Main.TreeType)
            {
                default:
                case GlobalSettings.TreeOption.TrainingPlan:
                    if (SelectedPlan != null)
                        itemTree.RowData = SelectedPlan.TreeListRowData;

                    else
                        itemTree.RowData = string.Empty;

                    btnDelTemplate.Hide();
                    btnNewTemplate.Hide();
                    break;

                case GlobalSettings.TreeOption.Library:
                    // Refresh tree while maintaining selction
                    LibraryNode originalSelection = CollectionUtils.GetFirstItemOfType<LibraryNode>(itemTree.SelectedItems);
                    ChartData.RefreshLibraryNodes();
                    itemTree.RowData = ChartData.LibraryNodes;

                    if (originalSelection != null)
                    {
                        // Restore selection after updating the row data.
                        LibraryNode restoredSelection = ChartData.GetLibraryNode(originalSelection.Element, ChartData.LibraryNodes);
                        if (restoredSelection != null)
                            itemTree.Selected = new List<LibraryNode> { restoredSelection };
                    }

                    btnDelTemplate.Show();
                    btnNewTemplate.Show();
                    break;
            }

            itemTree.Refresh();
        }

        /// <summary>
        /// Refresh plan Details tab.  Includes info such as num activities, plan start, etc.
        /// </summary>
        private void RefreshPlanDetails()
        {
            lblPlanStart.Text = string.Format("{0}: {1}", CommonResources.Text.LabelStart, SelectedPlan.StartDate.ToString("d", CultureInfo.CurrentCulture));

            int weeks = (SelectedPlan.EndDate - SelectedPlan.StartDate).Days / 7 + 1;
            string weeksUnit;

            if (weeks > 1)
                weeksUnit = Time.LabelPlural(Time.TimeRange.Week);
            else
                weeksUnit = Time.Label(Time.TimeRange.Week);

            lblPlanEnd.Text = string.Format("{0}: {1}", Strings.Label_End, SelectedPlan.EndDate.ToString("d", CultureInfo.CurrentCulture));
            lblPlanTitle.Text = string.Format("{0}: {1} - {2}", Strings.Label_Plan,
                SelectedPlan.StartDate.ToString("d", CultureInfo.CurrentCulture),
                SelectedPlan.EndDate.ToString("d", CultureInfo.CurrentCulture));
            lblActivityCount.Text = string.Format("{0}: {1} ({2} {3})", Strings.Label_Activities, SelectedPlan.GetWorkouts().Count, weeks, weeksUnit);
        }

        private void RefreshWeekView()
        {
            List<DailyPanel> dayPanels = new List<DailyPanel> { dailyY3, dailyY2, dailyY1, dailyToday, dailyT1, dailyT2, dailyT3 };
            double distance = 0, score = 0, actScore = 0, actDist = 0;
            TimeSpan time = TimeSpan.Zero, actTime = TimeSpan.Zero;
            int count = 0, actCount = 0;
            int week = 0;

            // Populate daily detail panels
            foreach (DailyPanel panel in dayPanels)
            {
                RefreshDailyPanel(panel);

                if (SelectedPlan != null)
                {
                    WorkoutCollection workouts = SelectedPlan.GetWorkouts(panel.Date);
                    foreach (Workout workout in workouts)
                    {
                        distance += Length.Convert(workout.TotalDistanceMeters, Length.Units.Meter, PluginMain.DistanceUnits);
                        actDist += Length.Convert(workout.Actual.TotalDistanceMeters, Length.Units.Meter, PluginMain.DistanceUnits);
                        score += workout.Score;
                        actScore += workout.Actual.Score;
                        time += workout.TotalTime;
                        actTime += workout.Actual.TotalTime;
                        count++;
                        if (workout.Actual.IsMatched) actCount++;
                    }
                }
            }

            // Collect data
            ITimeValueEntry<float> day1 = ChartData.ATL.GetInterpolatedValue(dailyY3.Date.AddDays(-1));
            ITimeValueEntry<float> day7 = ChartData.ATL.GetInterpolatedValue(dailyT3.Date);

            if (day1 != null && day7 != null)
            {
                // ATL
                WeekSummary.ATL = day7.Value - day1.Value;

                // CTL
                day1 = ChartData.CTL.GetInterpolatedValue(dailyY3.Date.AddDays(-1));
                day7 = ChartData.CTL.GetInterpolatedValue(dailyT3.Date);
                WeekSummary.CTL = day7.Value - day1.Value;
            }

            if (SelectedPlan != null)
                week = (dailyToday.Date - SelectedPlan.StartDate).Days / 7 + 1;

            if (DateTime.Today < dailyY3.Date)
            {
                WeekSummary.WorkoutName = string.Format("{0} {1}\r\n{2}: {3}", CommonResources.Text.LabelWeek, week, Strings.Label_Activities, count);
                WeekSummary.Distance = distance.ToString("#.#", CultureInfo.CurrentCulture);
                WeekSummary.Time = Utilities.ToTimeString(time);
                WeekSummary.Pace = string.Empty;
                WeekSummary.Score = score.ToString("#", CultureInfo.CurrentCulture);
                WeekSummary.Refresh();
            }
            //else
            //{
            // TODO: (LOW) This is another good way to display weekly summary data.  Come up with a way for the user to choose?
            //    object[] args = { CommonResources.Text.LabelWeek, week, Strings.Label_Activities, actCount, count };
            //    WeekSummary.WorkoutName = string.Format("{0} {1}\r\n{2}:\r\n(Actual/Plan)\r\n{3} / {4}", args);
            //    WeekSummary.Distance = string.Format("{0} / {1}", actDist.ToString("0.#", CultureInfo.CurrentCulture), distance.ToString("0.#", CultureInfo.CurrentCulture));
            //    WeekSummary.Time = Utilities.ToTimeString(time);
            //    WeekSummary.Pace = string.Empty;
            //    WeekSummary.Score = string.Format("{0} / {1}", (actScore * -1.0).ToString("0", CultureInfo.CurrentCulture), score.ToString("0", CultureInfo.CurrentCulture));
            //    WeekSummary.Refresh();
            //}
            else
            {
                object[] args = { CommonResources.Text.LabelWeek, week, Strings.Label_Activities, actCount, count };
                WeekSummary.WorkoutName = string.Format("{0} {1}\r\n{2}:\r\n{3} / {4}\r\nPlan (delta)", args);
                WeekSummary.Distance = string.Format("{0} ({1})", distance.ToString("0.#", CultureInfo.CurrentCulture), (actDist - distance).ToString("+0.#;-0.#;0", CultureInfo.CurrentCulture));
                WeekSummary.Time = string.Format("{0} ({1})", Utilities.ToTimeString(time, "hh:mm"), Utilities.ToTimeString(actTime - time, "+hh:mm;-hh:mm;0"));
                WeekSummary.Pace = string.Empty;
                WeekSummary.Score = string.Format("{0} ({1})", actScore.ToString("0", CultureInfo.CurrentCulture), (actScore - score).ToString("+0;-0;0", CultureInfo.CurrentCulture));
                WeekSummary.Refresh();
            }
        }

        private void RefreshDailyPanel(DailyPanel daily)
        {
            int offset = (int)daily.Tag;
            DateTime date = SelectedCalendarDate.AddDays(offset);

            Phase phase = null;

            daily.Date = date;
            daily.HeadingText = string.Format("{0} {1}", date.ToString("ddd", CultureInfo.CurrentCulture), date.ToString("d", CultureInfo.CurrentCulture));

            if (Data.TrainingLoadPlugin.IsInstalled)
            {
                if (date.Date <= DateTime.Today)
                {
                    // Historic CTL/ATL
                    daily.ATL = Data.TrainingLoadPlugin.GetATL(date);
                    daily.CTL = Data.TrainingLoadPlugin.GetCTL(date);
                }
                else if (ChartData.ATL != null)
                {
                    // Projected data
                    ITimeValueEntry<float> entry = ChartData.ATL.GetInterpolatedValue(date);
                    if (entry != null)
                    {
                        daily.ATL = entry.Value;
                        daily.CTL = ChartData.CTL.GetInterpolatedValue(date).Value;
                    }
                }
            }

            if (SelectedPlan != null)
                phase = SelectedPlan.GetPhase(date);

            WorkoutCollection workouts = new WorkoutCollection();
            daily.ClearPanel();

            if (phase != null)
            {
                workouts = SelectedPlan.GetWorkouts(date);
                if (workouts.Count > 0)
                {
                    foreach (Workout workout in workouts)
                        daily.AddWorkout(workout);

                    daily.Refresh();
                }

                daily.BottomGradientColor = SelectedPlan.GetPhase(date).DisplayColor;
            }
            else
            {
                daily.BottomGradientColor = daily.TopGradientColor;
                daily.ClearPanel();
            }
        }

        internal void RefreshMultipleWorkoutDate(DateTime date)
        {
            DateItem[] dates = trainingCal.Dates.DateInfo(date);

            foreach (DateItem item in dates)
            {
                string tag = item.Tag as string;
                if (tag.StartsWith("WorkoutMultiple"))
                {
                    WorkoutCollection workouts = SelectedPlan.GetWorkouts(date);
                    DateItem info = GetMutipleWorkoutItem(workouts);

                    // Simply update the text
                    item.Text = info.Text;
                    item.ImageListIndex = info.ImageListIndex;
                    return;
                }
            }
        }

        /// <summary>
        /// Re-sort the calendar item 'layers'.  This will re-arrange the
        /// calendar items to be as such:
        /// Phases (bottom) > Workouts > ST Activities (Top)
        /// </summary>
        internal void SortCalendarItems()
        {
            int activities = 0;
            int i = 0;
            while (i < trainingCal.Dates.Count - activities)
            {
                DateItem item = trainingCal.Dates[i];
                string tag = item.Tag as string;

                if (tag.StartsWith("Phase"))
                {
                    trainingCal.Dates.MoveToBottom(item);
                    i++;
                }
                else if (tag.StartsWith("Workout"))
                {
                    // Do nothing... these sit in the middle
                    i++;
                }
                else if (tag.StartsWith("Activity"))
                {
                    // Top most items
                    trainingCal.Dates.MoveToTop(item);
                    activities++;
                }
                else
                {
                    // Oops...?
                    i++;
                }
            }
        }

        #endregion

        private void SwapChartCalendar()
        {
            SuspendLayout();

            // Swap Settings
            GlobalSettings.Main.IsCalendarMaximized = !GlobalSettings.Main.IsCalendarMaximized;

            if (GlobalSettings.Main.IsCalendarMaximized && this.pnlChart.Controls.Contains(this.trainingCal))
            {
                this.splitContainer1.Panel2.Controls.Remove(this.zedChart);
                this.splitContainer1.Panel2.Controls.Remove(this.bnrChart);
                this.pnlChart.Controls.Remove(this.trainingCal);
                this.pnlChart.Controls.Remove(this.bnrCalendar);

                this.splitContainer1.Panel2.Controls.Add(this.trainingCal);
                this.splitContainer1.Panel2.Controls.Add(this.bnrCalendar);
                this.pnlChart.Controls.Add(this.zedChart);
                this.pnlChart.Controls.Add(this.bnrChart);
            }
            else
            {
                this.splitContainer1.Panel2.Controls.Remove(this.trainingCal);
                this.splitContainer1.Panel2.Controls.Remove(this.bnrCalendar);
                this.pnlChart.Controls.Remove(this.zedChart);
                this.pnlChart.Controls.Remove(this.bnrChart);

                this.splitContainer1.Panel2.Controls.Add(this.zedChart);
                this.splitContainer1.Panel2.Controls.Add(this.bnrChart);
                this.pnlChart.Controls.Add(this.trainingCal);
                this.pnlChart.Controls.Add(this.bnrCalendar);

            }

            zedChart.AxisChange();
            ResumeLayout();
        }

        /// <summary>
        /// Add or remove workouts to end of series as necessary.  Child workouts are added/removed 
        /// based on parent properties (end date, period, etc.)
        /// </summary>
        /// <param name="parent"></param>
        private void UpdateWorkoutSeries(Workout parent)
        {
            // Remove workouts as necessary
            DateTime lastDate = parent.StartDate;
            foreach (Workout child in SelectedPlan.GetWorkouts(parent))
            {
                if (parent.EndDate < child.StartDate || parent.PeriodDays <= 0)
                {
                    // Remove workouts past the end of the parent defined range
                    SelectedPlan.RemoveWorkout(child);
                }
                else if (lastDate < child.StartDate)
                {
                    // Find last (good) occuring workout date
                    lastDate = child.StartDate;
                }
            }

            // Add extra workouts at the end of the series as required
            if (parent.PeriodDays > 0)
            {
                DateTime start = parent.StartDate;

                // Add extra workouts at the end of the series as required
                while (lastDate.AddDays(parent.PeriodDays) <= parent.EndDate)
                {
                    lastDate = lastDate.AddDays(parent.PeriodDays);
                    Workout child = new Workout(lastDate, parent);

                    Phase phase = SelectedPlan.GetPhase(lastDate);

                    if (phase != null)
                    {
                        phase.AddWorkout(child);
                    }
                }
            }
        }

        private DateItemCollection GetLogbookCalendarItems()
        {

            DateItemCollection activityItem = new DateItemCollection(trainingCal);

            foreach (IActivity activity in PluginMain.GetApplication().Logbook.Activities)
            {
                int index = activityItem.IndexOf(activity.StartTime.Add(activity.TimeZoneUtcOffset));
                if (activityItem.IndexOf(activity.StartTime.Add(activity.TimeZoneUtcOffset)) < 0)
                {
                    // Not added yet
                    activityItem.Add(GetActivityItem(activity));
                }
            }

            return activityItem;
        }

        /// <summary>
        /// Get the new or existing date item for a specific activity
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        private DateItem GetActivityItem(IActivity activity)
        {
            foreach (DateItem date in trainingCal.Dates)
            {
                string tag = date.Tag as string;
                if (tag == "Activity" + activity.ReferenceId)
                    return date;
            }

            // NOTE: DateItem.Date needs the date to be a DATE (no time part).
            DateItem item = new DateItem();
            item.Date = activity.StartTime.Add(activity.TimeZoneUtcOffset).Date;

            string text = activity.StartTime.Add(activity.TimeZoneUtcOffset).ToShortTimeString();
            if (!string.IsNullOrEmpty(activity.Name))
                text += Environment.NewLine + activity.Name;

            text += Environment.NewLine + Utilities.ToTimeString(ActivityInfoCache.Instance.GetInfo(activity).Time);
            item.Text = text;
            item.BoldedDate = true;
            item.BackColor2 = Color.Chartreuse;
            item.GradientMode = mcGradientMode.ForwardDiagonal;
            item.TextColor = PluginMain.GetApplication().VisualTheme.ControlText;
            item.Tag = "Activity" + activity.ReferenceId;

            return item;
        }

        /// <summary>
        /// Get the first instance of a specific date item type from the calendar.  
        /// Types are indicated by the beginning on the tag.
        /// "Workout", "WorkoutMultiple", "Phase", etc.
        /// Returns empty collection if none found.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private DateItemCollection GetDateItemType(DateTime date, string type)
        {
            DateItemCollection items = new DateItemCollection(trainingCal);
            foreach (DateItem item in trainingCal.Dates.DateInfo(date))
            {
                string tag = item.Tag as string;
                if (!string.IsNullOrEmpty(tag) && tag.StartsWith(type))
                    items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// Get a single date item representing multiple workouts
        /// </summary>
        /// <param name="workouts"></param>
        /// <returns></returns>
        private DateItem GetMutipleWorkoutItem(WorkoutCollection workouts)
        {
            DateItem item;

            if (workouts.Count > 0)
            {
                DateItemCollection items = GetDateItemType(workouts.MinDate, "WorkoutMultiple");
                if (items.Count > 0)
                {
                    // Get existing multi-workout item
                    item = items[0];
                }
                else
                {
                    // Couldn't find an existing item
                    item = new DateItem();
                }
            }
            else
            {
                // Bad input.  Typically there should be multiple input workouts.
                item = new DateItem();
            }

            string text = string.Empty;
            double distance = 0;
            TimeSpan time = TimeSpan.Zero;
            bool iconsMatch = true;
            string imageKey = string.Empty;
            string tag = "WorkoutMultiple";

            foreach (Workout workout in workouts)
            {
                if (string.IsNullOrEmpty(imageKey) || imageKey == "-")
                    imageKey = workout.ImageName;

                else if (imageKey != workout.ImageName && workout.ImageName != "-")
                    iconsMatch = false;

                distance += workout.TotalDistanceMeters;
                time += workout.TotalTime;
                item.Date = workout.StartDate;
                tag += "|" + workout.ReferenceId;
            }

            // Convert to proper units
            distance = (float)Length.Convert(distance, Length.Units.Meter, PluginMain.DistanceUnits);

            text = string.Format("({0}: {1})", CommonResources.Text.LabelMultiple, workouts.Count);
            if (time > TimeSpan.Zero)
                text += Environment.NewLine + Utilities.ToTimeString(time);

            if (distance > 0)
                text += Environment.NewLine + distance.ToString("0.##", CultureInfo.CurrentCulture) + " " + Length.LabelAbbr(PluginMain.DistanceUnits);

            item.Text = text;
            if (iconsMatch && !string.IsNullOrEmpty(imageKey) && imageKey != "-")
                item.ImageListIndex = GlobalImages.CalendarImageList.Images.IndexOfKey(imageKey);

            else
                item.ImageListIndex = GlobalImages.CalendarImageList.Images.IndexOfKey("Brick");

            item.BoldedDate = true;
            item.GradientMode = mcGradientMode.ForwardDiagonal;
            item.TextColor = PluginMain.GetApplication().VisualTheme.ControlText;
            item.Tag = tag;
            return item;
        }

        /// <summary>
        /// Raise Property Changed event
        /// </summary>
        /// <param name="property"></param>
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Add 'track' to 'graph' and apply labels based on 'chartType'
        /// </summary>
        /// <param name="track">Data track</param>
        /// <param name="graph">Which graph to stick the data on</param>
        /// <param name="chartType">This determines the labeling, coloring, etc. (all appearance related)</param>
        internal static void RefreshTrainingLoadChart(ZedGraphControl graph)
        {
            GraphPane myPane = graph.GraphPane;

            myPane.XAxis.Title.Text = CommonResources.Text.LabelDate;
            myPane.XAxis.Type = AxisType.Date;

            if (myPane.YAxis.Title.Text != Strings.Label_CTL)
            {
                // If switching view from another chart type...
                myPane.CurveList.Clear();

                myPane.YAxis.Title.Text = Strings.Label_CTL;
                myPane.Y2Axis.Title.Text = Strings.Label_TSB;
                myPane.XAxis.MinorTic.IsOutside = true;
                myPane.XAxis.MajorGrid.IsVisible = false;
                myPane.XAxis.Title.IsVisible = false;
                myPane.YAxis.Title.FontSpec.FontColor = ChartData.CtlColor;
                myPane.YAxis.Scale.FontSpec.FontColor = ChartData.CtlColor;
            }

            // Add CTL Curves, and TSB curve
            CurveList ctlCurves = ChartData.GetCTLlines(myPane);
            if (myPane.CurveList.IndexOf("CTLf") == -1 && myPane.CurveList.IndexOf("CTLp") == -1 && ctlCurves.Count > 0)
                myPane.CurveList.AddRange(ctlCurves);
            else if (myPane.CurveList.IndexOf("CTLf") > 0 && myPane.CurveList.IndexOf("CTLp") > 0)
            {
                // Do nothing.  Curves already added.
            }
            else if (ctlCurves.Count > 0)
            {
                int index;
                if (myPane.CurveList.IndexOf("CTLp") == -1)
                {
                    index = ctlCurves.IndexOf("CTLp");
                    if (index != -1)
                        myPane.CurveList.Add(ctlCurves[index]);
                }

                if (myPane.CurveList.IndexOf("CTLf") == -1)
                {
                    index = ctlCurves.IndexOf("CTLf");
                    if (index != -1)
                        myPane.CurveList.Add(ctlCurves[index]);
                }
            }

            // CTL Target curve.  NOTE, the visibility property is managed to show/hide this.
            if (GlobalSettings.Main.ShowCTLTarget)
            {
                ChartData.RefreshTargetRampColors();

                if (!myPane.CurveList.Contains(ChartData.TargetCTL))
                    myPane.CurveList.Add(ChartData.TargetCTL);
            }

            LineItem tsbCurve = ChartData.GetTSBline(myPane);
            if (!myPane.CurveList.Contains(tsbCurve))
                myPane.CurveList.Insert(0, tsbCurve);

            // Insert TSB as the topmost chart
            Axis axis = tsbCurve.GetYAxis(myPane);
            axis.IsVisible = true;
            axis.Title.IsVisible = false;
            axis.Scale.IsVisible = true;
            axis.MajorTic.IsAllTics = false;
            axis.MinorTic.IsAllTics = false;
            axis.Color = Color.DarkGray;

            if (ctlCurves.Count > 0)
                graph.AxisChange();

            graph.Refresh();
        }

        /// <summary>
        /// Add 'track' to 'graph' and apply labels based on 'chartType'
        /// </summary>
        /// <param name="track">Data track</param>
        /// <param name="graph">Which graph to stick the data on</param>
        /// <param name="chartType">This determines the labeling, coloring, etc. (all appearance related)</param>
        internal static void RefreshProgressChart(ZedGraphControl graph)
        {
            bool zoomRequired = false;

            // Deafault Settings
            GraphPane myPane = graph.GraphPane;
            myPane.CurveList.Clear();
            myPane.BarSettings.MinClusterGap = .1f;
            myPane.BarSettings.Type = BarType.Overlay;
            graph.IsShowCursorValues = false;

            Color actualColor = Color.OrangeRed;
            Color planColor = Color.Green;

            myPane.YAxis.Title.Text = ChartData.GetProgressLabel(GlobalSettings.Main.ProgressChart);
            myPane.XAxis.MinorTic.IsOutside = true;
            myPane.XAxis.MajorGrid.IsVisible = false;
            myPane.XAxis.Title.IsVisible = false;

            // Plan data
            ChartPointPairList planTrack = ChartData.GetPlanProgress(GlobalSettings.Main.ProgressChart, GlobalSettings.Main.GroupBy);

            // Actual data
            ChartPointPairList actualTrack = ChartData.GetActualProgress(GlobalSettings.Main.ProgressChart, GlobalSettings.Main.GroupBy);

            // Add lines/bars to chart
            if (GlobalSettings.Main.IsProgressCumulative)
            {
                // Cumulative charts
                if (myPane.XAxis.Type != AxisType.Date)
                    zoomRequired = true;

                myPane.XAxis.Title.Text = CommonResources.Text.LabelDate;
                myPane.XAxis.Type = AxisType.Date;

                // Line chart
                LineItem line = myPane.AddCurve(GlobalSettings.Main.ProgressChart.ToString(), planTrack, planColor, SymbolType.None);
                line.Line.Width = 1f;
                line.Line.Fill.Type = FillType.None;
                line.Line.Fill.Color = Color.FromArgb(128, line.Color);
                line.Line.IsAntiAlias = true;

                line = myPane.AddCurve(GlobalSettings.Main.ProgressChart.ToString(), actualTrack, actualColor, SymbolType.None);
                line.Line.Width = 1f;
                line.Line.Fill.Type = FillType.None;
                line.Line.Fill.Color = Color.FromArgb(128, line.Color);
                line.Line.IsAntiAlias = true;
            }
            else if (GlobalSettings.Main.GroupBy == GlobalSettings.GroupOption.Category)
            {
                // Category grouped chart
                // Chart settings
                if (myPane.XAxis.Type != AxisType.Text)
                    zoomRequired = true;

                myPane.XAxis.Title.Text = CommonResources.Text.LabelCategory;
                myPane.XAxis.Type = AxisType.Text;
                myPane.BarSettings.ClusterScaleWidthAuto = true;
                myPane.BarSettings.MinBarGap = .2f;
                myPane.BarSettings.MinClusterGap = 1;
                myPane.BarSettings.Type = BarType.Stack;

                // Collect all categories (lines/bars will be arranged under these later)
                List<IActivityCategory> filterCats = new List<IActivityCategory>();
                for (int i = 0; i < planTrack.Count; i++)
                {
                    IActivityCategory majorCat = Utilities.CategoryIndex[(int)planTrack[i].Z];
                    if (!filterCats.Contains(majorCat))
                        filterCats.Add(majorCat);

                    // Convert from minor categories to major categories
                    planTrack[i].X = filterCats.IndexOf(majorCat) + 1;
                }
                planTrack.HandleDuplicates(true, ChartPointPairList.CombineAction.Combine);

                // Planned - Horizontal lines (bar toppers)
                LineItem line = myPane.AddCurve(GlobalSettings.Main.ProgressChart.ToString(), planTrack, planColor);
                line.Line.Color = Color.FromArgb(90, planColor);
                line.Line.Fill.Type = FillType.None;
                line.Line.Fill.Color = Color.Empty;
                line.Line.SelectedFill.Type = FillType.None;
                line.Line.SelectedFill.Color = Color.Empty;
                line.Line.StepType = StepType.ForwardStep;
                line.Color = Color.FromArgb(160, planColor);
                line.IsRangeSelectable = false;
                line.Symbol.Type = SymbolType.HBar;
                line.Symbol.IsAntiAlias = true;

                // Actual - Stacked Bars (stacks represent sub categories)
                BarItem bar;
                foreach (PointPair point in actualTrack)
                {
                    IActivityCategory majorCat = Utilities.CategoryIndex[(int)point.Z];
                    if (!filterCats.Contains(majorCat))
                        filterCats.Add(majorCat);

                    // X: Minor Category, Y: Value, Z: Major Category
                    PointPairList singleCategory = new PointPairList();
                    singleCategory.Add(filterCats.IndexOf(majorCat) + 1, point.Y, point.X, point.Tag as string);
                    bar = myPane.AddBar(point.Tag as string, singleCategory, Utilities.RandomColor(180, 170, Utilities.CategoryIndex[(int)point.X].GetHashCode()));
                    bar.Bar.Fill.Type = FillType.Solid;
                    bar.IsRangeSelectable = false;
                    bar.IsSelectable = false;
                    bar.Bar.Border.IsVisible = false;
                }

                foreach (CurveItem curve in myPane.CurveList)
                {
                    if (curve.IsBar)
                    {
                        for (int i = 0; i < filterCats.Count; i++)
                        {
                            if (filterCats[i] != Utilities.CategoryIndex[(int)curve[0].Z])
                            {
                                curve.AddPoint(i, 0);
                            }
                        }
                    }
                }

                // Set Major category labels
                List<string> labels = new List<string>();
                foreach (IActivityCategory category in filterCats)
                    labels.Add(category.Name);

                myPane.XAxis.Scale.TextLabels = labels.ToArray();
            }
            else
            {
                // Distance/time non-cumulative charts
                if (myPane.XAxis.Type != AxisType.Date)
                    zoomRequired = true;

                // Actual Values
                BarItem bar = myPane.AddBar(GlobalSettings.Main.ProgressChart.ToString(), actualTrack, actualColor);
                bar.Bar.Fill.Type = FillType.Solid;
                bar.Bar.Fill.Color = Color.FromArgb(180, bar.Color);
                bar.Bar.Border.IsVisible = false;


                // Planned Values
                LineItem line;
                switch (GlobalSettings.Main.GroupBy)
                {
                    case GlobalSettings.GroupOption.Day:
                        bar = myPane.AddBar(GlobalSettings.Main.ProgressChart.ToString(), planTrack, planColor);
                        bar.Bar.Fill.Type = FillType.Brush;
                        bar.Bar.Fill.SecondaryValueGradientColor = Color.Transparent;
                        bar.Bar.Fill.Angle = 270;
                        bar.Bar.Border.IsVisible = false;

                        myPane.BarSettings.ClusterScaleWidthAuto = true;
                        myPane.XAxis.Type = AxisType.Date;
                        myPane.BarSettings.MinBarGap = .2f;
                        myPane.BarSettings.MinClusterGap = 1;
                        break;

                    case GlobalSettings.GroupOption.Week:
                        line = myPane.AddCurve(GlobalSettings.Main.ProgressChart.ToString(), planTrack, planColor);
                        line.Line.Color = Color.FromArgb(90, planColor);
                        line.Line.Fill.Type = FillType.None;
                        line.Line.Fill.Color = Color.Empty;
                        line.Line.SelectedFill.Type = FillType.None;
                        line.Line.SelectedFill.Color = Color.Empty;
                        line.Color = Color.FromArgb(160, planColor);
                        line.IsRangeSelectable = false;
                        line.Symbol.Type = SymbolType.HBar;
                        line.Symbol.IsAntiAlias = true;

                        myPane.BarSettings.ClusterScaleWidthAuto = true;
                        myPane.BarSettings.MinBarGap = 14f;
                        myPane.BarSettings.MinClusterGap = 1;
                        line.Line.StepType = StepType.ForwardStep;
                        break;

                    case GlobalSettings.GroupOption.Month:
                        line = myPane.AddCurve(GlobalSettings.Main.ProgressChart.ToString(), planTrack, planColor);
                        line.Line.Color = Color.FromArgb(90, planColor);
                        line.Line.Fill.Type = FillType.None;
                        line.Line.Fill.Color = Color.Empty;
                        line.Line.SelectedFill.Type = FillType.None;
                        line.Line.SelectedFill.Color = Color.Empty;
                        line.Color = Color.FromArgb(160, planColor);
                        line.IsRangeSelectable = false;
                        line.Symbol.Type = SymbolType.HBar;
                        line.Symbol.IsAntiAlias = true;

                        myPane.BarSettings.ClusterScaleWidthAuto = true;
                        myPane.BarSettings.MinBarGap = 50f;
                        //myPane.BarSettings.MinClusterGap = 1;
                        line.Line.StepType = StepType.ForwardStep;
                        break;
                }

                myPane.XAxis.Title.Text = CommonResources.Text.LabelDate;
                myPane.XAxis.Type = AxisType.Date;
            }

            myPane.YAxis.Title.FontSpec.FontColor = planColor;
            myPane.YAxis.Scale.FontSpec.FontColor = planColor;

            if (zoomRequired)
            {
                // TODO: (MED) Fix zooming here (this will resuilt in funny zooms when changing group types.
                // Should use ZoomPlan(...) in some cases but it's not a static method.  Fix this.
                if (GlobalSettings.Main.IsDateBasedChart)
                {
                    // ZoomPlan(false/true);
                    graph.ZoomAuto(myPane);
                }
                else
                {
                    graph.ZoomAuto(myPane);
                }
            }

            if (actualTrack.Count > 0 || planTrack.Count > 0)
                graph.AxisChange();

            graph.Refresh();
        }

        /// <summary>
        /// Set the highlighted date on the chart
        /// </summary>
        /// <param name="date">Date to highlight</param>
        internal void SetHighlightDate(GradientFillObj grad, DateTime date)
        {
            grad.Location = new Location(XDate.DateTimeToXLDate(date) - 1, 0, 2, 1, CoordType.XScaleYChartFraction, AlignH.Center, AlignV.Top);
            grad.IsVisible = true;
            zedChart.Refresh();
        }

        /// <summary>
        /// Auto zooms the chart to fit the currently selected plan data.
        /// This ignores the Training Load data which may extend far one way or the other.
        /// </summary>
        /// <param name="zoomAll">True zooms y-axis (all y axes) as well as x-axis, </param>
        internal void ZoomPlan(bool zoomAll)
        {
            if (zoomAll)
                zedChart.ZoomAuto(zedChart.GraphPane);

            if (zedChart.GraphPane.XAxis.Type == AxisType.Date && SelectedPlan != null)
            {
                zedChart.GraphPane.XAxis.Scale.Min = XDate.DateTimeToXLDate(SelectedPlan.StartDate);
                zedChart.GraphPane.XAxis.Scale.Max = XDate.DateTimeToXLDate(SelectedPlan.EndDate);
                zedChart.AxisChange();
            }
        }

        #endregion

        #region Event Handlers

        #region Detail Tab

        #endregion

        private void Activities_CollectionChanged(object sender, NotifyCollectionChangedEventArgs<IActivity> e)
        {
            // Add newly imported activities to the calendar
            foreach (IActivity activity in e.NewItems)
            {
                // Hide workouts to show actual activities
                foreach (DateItem info in GetDateItemType(activity.StartTime.Add(activity.TimeZoneUtcOffset).Date, "Workout"))
                    info.Visible = false;

                trainingCal.AddDateInfo(GetActivityItem(activity));
            }

            // Remove deleted activities from the calendar
            foreach (IActivity activity in e.OldItems)
            {
                DateItemCollection items = GetDateItemType(activity.StartTime.Add(activity.TimeZoneUtcOffset).Date, "Activity");
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Tag as string == "Activity" + activity.ReferenceId)
                    {
                        trainingCal.RemoveDateInfo(items[i]);
                        break;
                    }
                }
            }

            ChartData.Calculated = false;

            if (currentPlan != null)
                currentPlan.LinkActivities();

            RefreshChart();
        }

        private void TrainingPlan_Opened(object sender, PropertyChangedEventArgs e)
        {
            // Moved to TrainingPlan
            //initializing = false;
        }

        private void TrainingPlan_Opening(object sender, PropertyChangingEventArgs e)
        {
            // Moved to TrainingPlan
            // initializing = true;
        }

        private void bnrChart_MenuClicked(object sender, EventArgs e)
        {
            menuChart.Show(bnrChart, new Point(bnrChart.Right - 2, bnrChart.Bottom), ToolStripDropDownDirection.BelowLeft);
        }

        private void bnrTree_MenuClicked(object sender, EventArgs e)
        {
            menuTree.Show(bnrTree, new Point(bnrTree.Right - 2, bnrTree.Bottom), ToolStripDropDownDirection.BelowLeft);
        }

        private void btnBackNext_Click(object sender, EventArgs e)
        {
            int daysOffset = int.Parse((sender as ZoneFiveSoftware.Common.Visuals.Button).Tag as string);
            trainingCal.SelectDate(SelectedCalendarDate.AddDays(daysOffset * 7));
            mouseDate = SelectedCalendarDate.AddDays(daysOffset);

            RefreshWeekView();
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            TreeListPopup popup = new TreeListPopup();
            popup.Tree.Columns.Add(new TreeList.Column("Image", string.Empty, 120, StringAlignment.Near));
            popup.Tree.LabelProvider = new ImagePopupLabelProvider();
            popup.Tree.RowDataRenderer = new ImagePopupRenderer(popup.Tree);
            popup.Tree.ThemeChanged(PluginMain.GetApplication().VisualTheme);
            popup.Tree.RowData = GlobalImages.CalendarImageList.Images.Keys;

            ZoneFiveSoftware.Common.Visuals.Button button = sender as ZoneFiveSoftware.Common.Visuals.Button;
            popup.Tag = button.Tag;

            popup.ItemSelected += new TreeListPopup.ItemSelectedEventHandler(popupWorkoutImage_ItemSelected);
            popup.Popup(button.RectangleToScreen(button.ClientRectangle));
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            SwapChartCalendar();
        }

        private void btnZoomFit_Click(object sender, EventArgs e)
        {
            ZoomPlan(true);
        }

        private void btnParent_Click(object sender, EventArgs e)
        {
            if (SelectedPlan != null && SelectedWorkout != null)
            {
                Workout parent = SelectedPlan.GetWorkout(SelectedWorkout.ParentId);
                if (parent != null)
                    if (GlobalSettings.Main.TreeType == GlobalSettings.TreeOption.TrainingPlan)
                    {
                        itemTree.SelectedItems = new TreeNodeCollection(parent.Node);
                    }
                    else
                    {
                        trainingCal.SelectDate(parent.StartDate);
                        LoadWorkout(parent);
                    }
            }
        }

        private void btnLockWorkout_Click(object sender, EventArgs e)
        {
            SelectedWorkout.DateLocked = !SelectedWorkout.DateLocked;
        }

        private void btnRampEdit_Click(object sender, EventArgs e)
        {
            GlobalSettings.Main.EnableCTLEdit = !GlobalSettings.Main.EnableCTLEdit;
            btnRampEdit.Selected = GlobalSettings.Main.EnableCTLEdit;
        }

        /// <summary>
        /// Delete the lowest level selected item.  
        /// If a workout is selected, delete the workout.
        /// If a Phase is selected, delete the phase.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Workout workout = CollectionUtils.GetFirstItemOfType<Workout>(SelectedPlan.GetWorkouts(SelectedCalendarDate));
            if (workout != null)
                RemoveWorkout(workout);

            else if (SelectedPhase != null)
                RemovePhase(SelectedPhase);

            RefreshTree();
            RefreshChart();
            trainingCal.Month.Calendar.Refresh();
        }

        private void btnPlanSave_Click(object sender, EventArgs e)
        {
            SelectedPlan.SavePlan(false);
        }

        private void btnTrainingLoad_Click(object sender, EventArgs e)
        {
            if (TrainingLoadPlugin.IsInstalled)
                PluginMain.GetApplication().ShowView(GUIDs.TrainingLoad, string.Empty);

            // FYI, example of how to call a settings page (Trainer Power):
            // PluginMain.GetApplication().ShowView(GUIDs.SettingsView, "PageId=" + "36dbdc5f-1f9e-46c3-af1e-64a45d7dedda");
        }

        private void btnWorkoutDelete_Click(object sender, EventArgs e)
        {
            Workout workout = CollectionUtils.GetFirstItemOfType<Workout>(SelectedPlan.GetWorkouts(SelectedCalendarDate));
            RemoveWorkout(workout);
        }

        private void colorPhase_Click(object sender, EventArgs e)
        {
            Phase phase = SelectedPhase;
            if (phase == null) return;

            ColorSelectorPopup popup = new ColorSelectorPopup();
            popup.Selected = phase.DisplayColor;
            popup.Tag = txtPhaseName;
            popup.ItemSelected += popupColor_ItemSelected;
            popup.Popup(txtPhaseName.RectangleToScreen(txtPhaseName.ClientRectangle));
        }

        private void currentPlan_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "StartDate")
            {
                // Overall plan start date
                ChartData.Calculated = false;
                RefreshChart();
                RefreshPlanDetails();
            }
            else if (e.PropertyName == "Phase.EndDate")
            {
                RefreshPlanDetails();
            }
            else if (e.PropertyName == "Phase.Workouts")
            {
                trainingCal.Invalidate();
                ChartData.Calculated = false;
            }
            else if (e.PropertyName == "Phase.Workouts.StartDate")
            {
                SelectedPlan.ScheduleCurrentWorkouts();
            }
            else if (e.PropertyName == "Phase.Workouts.TemplateId")
            {
                SelectedPlan.ScheduleCurrentWorkouts();
            }
            else if (e.PropertyName == "Phase.Workouts.DateLocked")
            {
                if (SelectedWorkout != null)
                {
                    btnLockWorkout.BorderStyle = SelectedWorkout.DateLocked ? BorderStyle.Fixed3D : BorderStyle.None;
                    trainingCal.Invalidate();
                }
            }
        }

        private void currentPlan_WorkoutsChanged(object sender, CollectionChangeEventArgs e)
        {
            // Called anytime a workout is added or removed from the plan
            Workout workout = e.Element as Workout;
            DateItemCollection items;
            if (workout == null) return;
            bool containsActivity = false;

            if (e.Action == CollectionChangeAction.Add)
            {
                // Handle managing the display on the calendar, particularly multi-workout day instances
                WorkoutCollection workouts = SelectedPlan.GetWorkouts(workout.StartDate);
                trainingCal.AddDateInfo(workout.CalendarItem);

                // Determine if an activity exists on this date (should hide workout items if so)
                // (Activity is real, vs. workout which is planned)
                items = GetDateItemType(workout.StartDate, "Activity");
                if (items.Count > 0)
                {
                    containsActivity = true;
                    workout.CalendarItem.Visible = false;
                }

                if (1 < workouts.Count)
                {
                    // Multiple workouts on this date
                    items = GetDateItemType(workout.StartDate, "Workout");

                    // Hide existing WORKOUT items
                    foreach (DateItem item in items)
                        item.Visible = false;

                    // Subscribe changes to manage info updates
                    if (workouts.Count == 2)
                    {
                        // Switch from single day to multi day
                        foreach (Workout multiple in workouts)
                        {
                            multiple.PropertyChanged += new PropertyChangedEventHandler(multiple_PropertyChanged);
                        }
                    }
                    else
                    {
                        // Add a new handler only to the new workout
                        workout.PropertyChanged += new PropertyChangedEventHandler(multiple_PropertyChanged);
                    }

                    // Add new workout item (multiple)
                    DateItem multiItem = GetMutipleWorkoutItem(workouts);
                    multiItem.Visible = true;

                    if (!trainingCal.Dates.Contains(multiItem))
                        trainingCal.Dates.Add(multiItem);

                    if (containsActivity)
                        multiItem.Visible = false;
                }

                workout.StartDateChanged += workout_StartDateChanged;
            }
            else if (e.Action == CollectionChangeAction.Remove)
            {
                if (trainingCal.Dates.Contains(workout.CalendarItem))
                {
                    // Remove Single Date
                    trainingCal.RemoveDateInfo(workout.CalendarItem);
                }

                // Remaining workouts after the one in question has been removed
                WorkoutCollection workouts = SelectedPlan.GetWorkouts(workout.StartDate);

                if (workouts.Count == 1)
                {
                    // multiple to single
                    foreach (DateItem item in GetDateItemType(workout.StartDate, "WorkoutMultiple"))
                    {
                        trainingCal.RemoveDateInfo(item);
                    }

                    // ...only 1 item exists here, the single workout.
                    workouts[0].CalendarItem.Visible = true;

                }
                else if (workouts.Count > 1)
                {
                    // Still a multiple workout date (Example: 3 workouts to 2 workouts)
                    RefreshMultipleWorkoutDate(workout.StartDate);
                }

                workout.StartDateChanged -= workout_StartDateChanged;

            }

            lblActivityCount.Text = string.Format("{0}: {1}", Strings.Label_Activities, SelectedPlan.GetWorkouts().Count);
        }

        private void currentPlan_PhasesChanged(object sender, CollectionChangeEventArgs e)
        {
            Phase phase = e.Element as Phase;

            if (e.Action == CollectionChangeAction.Add)
            {
                zedChart.GraphPane.GraphObjList.Add(phase.GradientObj);
                zedChart.GraphPane.GraphObjList.Add(phase.TitleObj);

            }
            else if (e.Action == CollectionChangeAction.Remove)
            {
                zedChart.GraphPane.GraphObjList.Remove(phase.GradientObj);
                zedChart.GraphPane.GraphObjList.Remove(phase.TitleObj);
            }

            RefreshPlanDetails();
        }

        private void workout_StartDateChanged(object sender, DateRangeEventArgs e)
        {
            Workout workout = sender as Workout;

            // Clear start date info related to multi-workout days
            DateItemCollection info = GetDateItemType(e.Start, "WorkoutMultiple"); // Original date items.
            WorkoutCollection workouts = SelectedPlan.GetWorkouts(e.Start); // Workouts from original date
            foreach (DateItem item in info)
            {
                // Original dates: Splitting multiple workouts (or simply updating)
                if (workouts.Count > 1)
                {
                    RefreshMultipleWorkoutDate(e.Start); // Update multi-workout day
                }
                else if (workouts.Count == 1)
                {
                    // Date has moved, split workouts that were previously on the same day
                    trainingCal.Dates.Remove(item); // Remove old multi-workout day

                    workouts[0].CalendarItem.Visible = true; // Workout whose date did not move
                    workout.CalendarItem.Visible = true; // Recently edited workout who moved to a new date
                }
            }

            // Manage new date with regard to multi-workout date
            info = GetDateItemType(e.End, "Workout"); // Destination date items.
            workouts = SelectedPlan.GetWorkouts(e.End); // Workouts on destination date
            DateItem multiItem = null;
            bool addMultiDay = false;

            foreach (DateItem item in info)
            {
                // Destination Dates: Combining into multiple workouts (or simply updating)
                string tag = item.Tag as string;
                if (tag.StartsWith("WorkoutMultiple"))
                    multiItem = item;

                else if (tag.StartsWith("Workout") && tag != "Workout" + workout.ReferenceId)
                {
                    // Single to multi-workout day
                    item.Visible = false;
                    workout.CalendarItem.Visible = false;

                    addMultiDay = true;
                }
            }

            if (addMultiDay)
            {
                if (multiItem != null)
                    RefreshMultipleWorkoutDate(e.End);
                else
                    trainingCal.Dates.Add(GetMutipleWorkoutItem(workouts));
            }
        }

        private void GlobalSettings_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            GraphPane myPane = zedChart.GraphPane;

            if (e.PropertyName == "ShowCTLTarget")
            {
                btnRampEdit.Enabled = GlobalSettings.Main.ShowCTLTarget;

                if (GlobalSettings.Main.ShowCTLTarget && GlobalSettings.Main.ChartType == GlobalSettings.ChartOption.TrainingLoad)
                {
                    if (!myPane.CurveList.Contains(ChartData.TargetCTL))
                        myPane.CurveList.Add(ChartData.TargetCTL);

                    btnRampEdit.BackgroundImage = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.Edit16;
                }
                else
                {
                    btnRampEdit.BackgroundImage = Images.EditGrey16;
                    GlobalSettings.Main.EnableCTLEdit = false;
                }

                // Show/Hide the line from the chart
                ChartData.TargetCTL.IsVisible = GlobalSettings.Main.ShowCTLTarget;

            }
            else if (e.PropertyName == "ShowChartPhases" || e.PropertyName == "GroupBy")
            {
                foreach (GraphObj item in myPane.GraphObjList)
                {
                    string tag = item.Tag as string;
                    // Show/Hide phase Gradient and Text
                    if (tag != null && (tag.StartsWith("G") || tag.StartsWith("T")))
                        item.IsVisible = GlobalSettings.Main.ShowChartPhases && GlobalSettings.Main.IsDateBasedChart;
                }
            }
            else if (e.PropertyName == "EnableCTLEdit")
            {
                zedChart.IsEnableHEdit = GlobalSettings.Main.EnableCTLEdit;
                zedChart.IsEnableVEdit = GlobalSettings.Main.EnableCTLEdit;
                ChartData.TargetCTL.Symbol.IsVisible = GlobalSettings.Main.EnableCTLEdit;
            }
            else if (e.PropertyName == "ShowPhaseSummary")
            {
                foreach (TrainingPlan plan in Loaded.Plans)
                    plan.UpdateSummaryNodes();
            }
        }

        private void ctxTreeColumnsPicker_Click(object sender, EventArgs e)
        {
            ZoneFiveSoftware.Common.Visuals.Forms.ListSettingsDialog dlg = new ZoneFiveSoftware.Common.Visuals.Forms.ListSettingsDialog();
            //dlg.AllowZeroSelected = false;
            //dlg.ThemeChanged(PluginMain.GetApplication().VisualTheme);
            IList<IListColumnDefinition> available = new List<IListColumnDefinition>(PlanOverviewColumns.AllColumns);
            IList<string> selected = new List<string>();

            available.RemoveAt(0); // Don't expose the first column (Complete) for modification.
            dlg.AvailableColumns = available;

            foreach (TreeList.Column column in itemTree.Columns)
            {
                if (column.Id != "Complete")
                    selected.Add(column.Id);
            }

            dlg.SelectedColumns = selected;
            dlg.AllowFixedColumnSelect = false;
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string selectedString = "Complete|36;";

            foreach (string id in dlg.SelectedColumns)
            {
                int width = int.MinValue;
                foreach (TreeList.Column column in itemTree.Columns)
                {
                    if (column.Id == id)
                    {
                        width = column.Width;
                        break;
                    }
                }

                if (width == int.MinValue)
                {
                    foreach (ColumnDef column in PlanOverviewColumns.AllColumns)
                    {
                        if (column.Id == id)
                            width = column.Width;
                    }
                }

                selectedString += id + "|" + width.ToString("0") + ";";
            }

            // Store new column settings & Refresh
            GlobalSettings.Main.OverviewColumns = selectedString;
            LoadTree(GlobalSettings.Main.TreeType);
        }

        private void ctxTreeResetColumns_Click(object sender, EventArgs e)
        {
            switch (GlobalSettings.Main.TreeType)
            {
                case GlobalSettings.TreeOption.TrainingPlan:
                    GlobalSettings.Main.OverviewColumns = string.Empty;
                    break;

                case GlobalSettings.TreeOption.Library:
                    break;

            }

            LoadTree(GlobalSettings.Main.TreeType);
        }

        private void mnuTreeItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            mnuTreeWorkoutLibrary.Checked = item == mnuTreeWorkoutLibrary;
            mnuTreePlanOverview.Checked = item == mnuTreePlanOverview;

            // Handles selection of the treelist menu (blue down arrow)
            GlobalSettings.TreeOption setting = (GlobalSettings.TreeOption)item.Tag;

            if (GlobalSettings.Main.TreeType == setting) return;

            GlobalSettings.Main.TreeType = setting;

            mnuTreeShowEmptyFolders.Visible = GlobalSettings.Main.TreeType == GlobalSettings.TreeOption.Library;
            mnuTreeShowSummary.Visible = GlobalSettings.Main.TreeType == GlobalSettings.TreeOption.TrainingPlan;

            LoadTree(GlobalSettings.Main.TreeType);
        }

        private void mnuChartItem_Click(object sender, EventArgs e)
        {
            // Handles selection of the chart menu (blue down arrow)
            ToolStripMenuItem selectedItem = sender as ToolStripMenuItem;

            foreach (ToolStripItem item in menuChart.Items)
            {
                if (item.GetType() == typeof(ToolStripMenuItem) && item.Tag as string != "ignore")
                {
                    ToolStripMenuItem menuItem = item as ToolStripMenuItem;
                    menuItem.Checked = menuItem == selectedItem;
                }
            }

            string tag = selectedItem.Tag as string;

            if (tag == "TrainingLoad")
            {
                GlobalSettings.Main.ChartType = (GlobalSettings.ChartOption)Enum.Parse(typeof(GlobalSettings.ChartOption), tag);
                mnuChartShowCTLtarget.Enabled = true;
            }
            else if (tag.Contains("Distance") || tag.Contains("Time"))
            {
                GlobalSettings.Main.ChartType = GlobalSettings.ChartOption.Progress;
                GlobalSettings.Main.ProgressChart = (GlobalSettings.ProgressOption)Enum.Parse(typeof(GlobalSettings.ProgressOption), tag);
                mnuChartShowCTLtarget.Enabled = false;
            }

            btnGroupBy.Enabled = tag.Equals("Distance") || tag.Equals("Time");

            RefreshChart();
        }

        /// <summary>
        /// Begin dragging a workout out of the library
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemTree_MouseDown(object sender, MouseEventArgs e)
        {
            TreeList.RowHitState state;
            TreeList.TreeListNode node = itemTree.RowHitTest(e.Location, out state) as TreeList.TreeListNode;

            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                itemTree_DoubleClick(sender, new EventArgs());
                return;
            }
            else if (e.Button == MouseButtons.Right && state == TreeList.RowHitState.Nothing)
            {
                itemTree.SelectedItems = new TreeNodeCollection();
                return;
            }
            else if (e.Button == MouseButtons.Right)
            {
                return;
            }

            if (node == null) return;

            if (node.GetType() == typeof(FitPlanNode))
            {
                if ((node as FitPlanNode).IsWorkout)
                {
                    Workout workout = node.Element as Workout;
                    itemTree.DoDragDrop(workout, DragDropEffects.Copy);
                }
            }
            else if (node.GetType() == typeof(LibraryNode))
            {
                if ((node as LibraryNode).IsWorkoutDef)
                {
                    WorkoutDefinition workout = node.Element as WorkoutDefinition;
                    itemTree.DoDragDrop(workout, DragDropEffects.Copy);
                }
            }
        }

        private void trainingCal_MouseDown(object sender, MouseEventArgs e)
        {
            Workout workout = SelectedWorkout; // = node.Element as Workout;
            trainingCal.DoDragDrop(workout, DragDropEffects.Move);
        }

        private void mnuTreeShowSummary_Click(object sender, EventArgs e)
        {
            GlobalSettings.Main.ShowPhaseSummary = mnuTreeShowSummary.Checked;

            foreach (Phase phase in SelectedPlan.Phases)
            {
                phase.RefreshSummary();
            }

            RefreshTree();
        }

        private void mnuTreeShowEmptyFolders_Click(object sender, EventArgs e)
        {
            GlobalSettings.Main.ShowEmptyFolders = mnuTreeShowEmptyFolders.Checked;
            RefreshTree();
        }

        private void mnuChartShowCTLTarget_CheckChanged(object sender, EventArgs e)
        {
            GlobalSettings.Main.ShowCTLTarget = mnuChartShowCTLtarget.Checked;
        }

        private void mnuChartShowPhases_CheckChanged(object sender, EventArgs e)
        {
            GlobalSettings.Main.ShowChartPhases = mnuChartShowPhases.Checked;
            RefreshChart();
        }

        private void itemTreePlanItem_SelectedItemsChanged(object sender, EventArgs e)
        {
            if (GlobalSettings.Main.TreeType == GlobalSettings.TreeOption.TrainingPlan)
            {
                itemTreePlanItemChanged();
            }
            else
            {
                itemTreeLibraryItemChanged();
            }
        }

        /// <summary>
        /// Double click item tree to go to Garmin Fitness workout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemTree_DoubleClick(object sender, EventArgs e)
        {
            switch (GlobalSettings.Main.TreeType)
            {
                case GlobalSettings.TreeOption.TrainingPlan:
                    this.OnDoubleClick(e);
                    break;

                case GlobalSettings.TreeOption.Library:
                    if (FitPlan.Data.GarminFitness.Manager.IsInstalled && SelectedTemplate != null && SelectedTemplate.IsGarminWorkout)
                        FitPlan.Data.GarminFitness.Manager.GarminManager.OpenWorkoutInView(SelectedTemplate);

                    break;
            }
        }

        /// <summary>
        /// Refresh training cal date info for dates containing multiple workouts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void multiple_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TotalTime" || e.PropertyName == "TotalDistanceMeters" || e.PropertyName == "ImageName")
            {
                Workout workout = sender as Workout;
                RefreshMultipleWorkoutDate(workout.StartDate);
            }
        }

        private void popupWorkoutImage_ItemSelected(object sender, TreeListPopup.ItemSelectedEventArgs e)
        {
            TreeListPopup popup = sender as TreeListPopup;
            string key = e.Item as string;

            if (popup.Tag as string == "Workout" && SelectedWorkout != null)
            {
                SelectedWorkout.ImageName = key;
                LoadWorkout(SelectedWorkout);
                trainingCal.Refresh();
            }
            else if (popup.Tag as string == "Template" && SelectedTemplate != null)
            {
                if (!LogbookSettings.Main.TemplateDefinitions.Contains(SelectedTemplate))
                {
                    LogbookSettings.Main.TemplateDefinitions.Add(SelectedTemplate);
                    SelectedTemplate.PropertyChanged += new PropertyChangedEventHandler(template_PropertyChanged);
                }

                SelectedTemplate.ImageName = key;
                LoadTemplate(SelectedTemplate);
            }
        }

        private void popupColor_ItemSelected(object sender, ColorSelectorPopup.ItemSelectedEventArgs e)
        {
            ColorSelectorPopup popup = sender as ColorSelectorPopup;

            SelectedPhase.DisplayColor = e.Item;
            itemTree.Refresh();
            trainingCal.Invalidate();
            if (GlobalSettings.Main.ShowChartPhases)
                zedChart.Invalidate();
        }

        #region PlanTab

        private void txtPhaseInfo_Leave(object sender, EventArgs e)
        {
            Phase phase = SelectedPhase;
            if (phase != null)
            {
                EnablePhaseEvents(false);

                ZoneFiveSoftware.Common.Visuals.TextBox textbox = sender as ZoneFiveSoftware.Common.Visuals.TextBox;
                string property = textbox.Tag as string;

                switch (property)
                {
                    case "Name":
                        phase.Name = textbox.Text;
                        break;

                    case "Start":
                        DateTime date;
                        if (DateTime.TryParse(textbox.Text, out date))
                        {
                            Phase neighbor;
                            DateTime endDate;
                            if (phase.StartDate < date)
                            {
                                // Moving into future
                                neighbor = phase;
                                endDate = phase.EndDate.AddDays((phase.StartDate - date).TotalDays);
                            }
                            else if (date < phase.StartDate)
                            {
                                // Moving into past
                                neighbor = SelectedPlan.GetPreviousPhase(phase);
                                if (phase != neighbor)
                                {
                                    endDate = date.AddDays(-1);
                                }
                                else
                                {
                                    // Phase is first phase in plan.  No worries.
                                    neighbor = phase;
                                    endDate = phase.EndDate;
                                }
                            }
                            else
                            {
                                // date is equal.  No update needed.
                                textbox.Text = phase.GetFormattedText("StartDate");
                                break;
                            }

                            if (!IsOkToChangePhaseEnd(neighbor, endDate))
                            {
                                // Update rejected
                                textbox.Text = phase.GetFormattedText("StartDate");
                                break;
                            }

                            phase.StartDate = date;
                            SelectedPlan.RefreshPhases();

                            foreach (Workout workout in phase.GetParentWorkouts())
                            {
                                UpdateWorkoutSeries(workout);
                            }
                        }
                        break;

                    case "End":
                        DateTime end = DateTime.Parse(textbox.Text).Date;
                        if (end == phase.EndDate)
                        {
                            // No update.
                            break;
                        }
                        else if (end < phase.StartDate)
                        {
                            // Cannot end before it starts
                            end = phase.StartDate.AddDays(1);
                        }

                        Phase nextPhase = SelectedPlan.GetNextPhase(phase);
                        if (nextPhase == null)
                        {
                            // Error: phase not found in list
                        }
                        else if (nextPhase == phase)
                        {
                            // This is the last phase
                            phase.EndDate = end;
                        }
                        else
                        {
                            // Typical: Set start date of next phase
                            if (nextPhase.EndDate < end)
                            {
                                // Skipping over phases not allowed
                                txtPhaseEnd.Text = phase.EndDate.ToString("d", CultureInfo.CurrentCulture);
                                break;
                            }
                            else if (phase.EndDate < end && IsOkToChangePhaseEnd(nextPhase, nextPhase.EndDate.AddDays((nextPhase.StartDate - end.AddDays(1)).Days)) ||
                                (end < phase.EndDate && IsOkToChangePhaseEnd(phase, end)))
                            {
                                // Moving into past or future, check nextPhase
                                nextPhase.StartDate = end.AddDays(1);
                            }
                            else
                            {
                                // End date rejected
                                txtPhaseEnd.Text = phase.EndDate.ToString("d", CultureInfo.CurrentCulture);
                                break;
                            }
                        }

                        SelectedPlan.RefreshPhases();

                        foreach (Workout workout in phase.GetParentWorkouts())
                        {
                            UpdateWorkoutSeries(workout);
                        }

                        break;
                }

                EnablePhaseEvents(true);

                LoadPhase(phase);
                RefreshTree();
                RefreshChart();
                trainingCal.Setup();
                trainingCal.Month.Calendar.Refresh();
            }
        }

        /// <summary>
        /// Check phase to ensure no Workouts will be truncated.  If so, prompt user to ensure they're not removed accidentally
        /// </summary>
        /// <param name="phase"></param>
        /// <param name="end"></param>
        /// <returns>Returns true if there are not workouts after this date, OR if the user agrees to have these 
        /// workouts removed, OR false if user rejects the deletion of workouts.</returns>
        private bool IsOkToChangePhaseEnd(Phase phase, DateTime end)
        {
            if (!phase.IsSafeEndDate(end, false))
            {
                WorkoutCollection removeWorkouts = phase.GetWorkouts(end.AddDays(1), DateTime.MaxValue, false, true);

                foreach (Workout workout in removeWorkouts)
                    workout.CalendarItem.BackgroundImage = Images.DeleteLg;

                trainingCal.Refresh();

                DialogResult result = MessageDialog.Show(string.Format(Resources.Strings.Text_DeleteWorkouts, removeWorkouts.Count), Strings.Label_FitPlan, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                foreach (Workout workout in removeWorkouts)
                    workout.CalendarItem.BackgroundImage = null;

                if (result == DialogResult.No)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Register or unregister the 'Phase' text change events
        /// </summary>
        /// <param name="enable">True to enable them, false to disable (unregister)</param>
        private void EnablePhaseEvents(bool enable)
        {
            if (enable)
            {
                txtPhaseEnd.Leave += txtPhaseInfo_Leave;
                txtPhaseName.Leave += txtPhaseInfo_Leave;
                txtPhaseStart.Leave += txtPhaseInfo_Leave;
            }
            else
            {
                txtPhaseEnd.Leave -= txtPhaseInfo_Leave;
                txtPhaseName.Leave -= txtPhaseInfo_Leave;
                txtPhaseStart.Leave -= txtPhaseInfo_Leave;
            }
        }

        private void txtPlanInfo_Leave(object sender, EventArgs e)
        {
            if (SelectedPlan != null)
            {
                ZoneFiveSoftware.Common.Visuals.TextBox textbox = sender as ZoneFiveSoftware.Common.Visuals.TextBox;
                string property = textbox.Tag as string;

                switch (property)
                {
                    case "Plan.Name":
                        SelectedPlan.Name = textbox.Text;
                        break;

                    case "Start":
                        SelectedPlan.StartDate = DateTime.Parse(textbox.Text);
                        ChartData.Calculated = false;
                        break;

                    case "End":
                        SelectedPlan.EndDate = DateTime.Parse(textbox.Text);
                        textbox.Text = SelectedPlan.EndDate.ToString("d", CultureInfo.CurrentCulture);
                        LoadPhase(SelectedPhase);
                        break;

                    case "Plan.AutoScheduleDays":
                        int days;
                        if (int.TryParse(txtPlanGarminAutoSched.Text, out days))
                        {
                            SelectedPlan.AutoScheduleDays = days;
                        }
                        else
                        {
                            txtPlanGarminAutoSched.Text = SelectedPlan.AutoScheduleDays.ToString();
                        }
                        break;

                }

                itemTree.Refresh();
            }
        }

        /// <summary>
        /// Register or unregister the 'Plan' text change events
        /// </summary>
        /// <param name="enable">True to enable them, false to disable (unregister)</param>
        private void EnablePlanEvents(bool enable)
        {
            if (enable)
            {
                txtPlanName.Leave += txtPlanInfo_Leave;
                lblPlanStart.Leave += txtPlanInfo_Leave;
                txtPlanGarminAutoSched.Leave += txtPlanInfo_Leave;
            }
            else
            {
                txtPlanName.Leave -= txtPlanInfo_Leave;
                lblPlanStart.Leave -= txtPlanInfo_Leave;
                txtPlanGarminAutoSched.Leave -= txtPlanInfo_Leave;
            }
        }

        private void txtWorkoutInfo_Leave(object sender, EventArgs e)
        {
            Workout workout = SelectedWorkout;
            if (workout != null)
            {
                EnableWorkoutEvents(false);
                ZoneFiveSoftware.Common.Visuals.TextBox textbox = sender as ZoneFiveSoftware.Common.Visuals.TextBox;
                string property = textbox.Tag as string;
                DateTime date;
                int integer;
                TimeSpan pace;
                bool reload = false;

                switch (property)
                {
                    case "Workout.Name":
                        workout.Name = textbox.Text;
                        break;

                    case "End":
                        if (DateTime.TryParse(textbox.Text, CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
                        {
                            if (date < workout.StartDate)
                            {
                                // Cannot end before it starts
                                date = workout.StartDate;
                            }
                            else if (SelectedPlan.GetPhase(workout.StartDate).EndDate < date)
                            {
                                // Workout cannot cross Phase boundaries
                                date = SelectedPlan.GetPhase(workout.StartDate).EndDate;
                            }

                            reload = workout.IsRepeating ^ workout.StartDate == date;
                            workout.EndDate = date.Date;

                            UpdateWorkoutSeries(workout);
                            if (reload) LoadWorkout(workout);
                        }
                        else if (!string.IsNullOrEmpty(textbox.Text))
                        {
                            textbox.Text = workout.EndDate.ToString("d", CultureInfo.CurrentCulture);
                        }
                        break;

                    case "Period":
                        if (int.TryParse(textbox.Text, out integer))
                        {
                            // Setting period days in a parent workout will automatically update all child instances
                            workout.PeriodDays = integer;

                            UpdateWorkoutSeries(workout);
                        }
                        else
                        {
                            textbox.Text = workout.GetFormattedText("PeriodDays");
                        }
                        break;

                    case "Ramp":
                        float ramp;
                        if (float.TryParse(textbox.Text.TrimEnd('%'), out ramp))
                        {
                            workout.Ramp = ramp / 100f;
                        }

                        textbox.Text = workout.GetFormattedText("Ramp");
                        break;

                    case "Notes":
                        workout.Notes = textbox.Text;
                        break;

                    case "Time":
                        TimeSpan time;
                        if (Utilities.TryParseMinutesSeconds(textbox.Text, out time))
                        {
                            workout.TotalTime = time;
                        }

                        textbox.Text = workout.GetFormattedText("TotalTime");
                        break;

                    case "Distance":
                        float distance;
                        if (float.TryParse(textbox.Text, out distance))
                        {
                            workout.TotalDistanceMeters = (float)Length.Convert(distance, PluginMain.DistanceUnits, Length.Units.Meter);
                        }
                        else
                        {
                            textbox.Text = workout.GetFormattedText("TotalDistanceMeters");
                        }
                        break;

                    case "Pace":
                        if (!Utilities.TryParseMinutesSeconds(textbox.Text, out pace))
                        {
                            txtWorkoutPace.Text = workout.GetFormattedText("PaceMinPerMeter");
                        }
                        break;

                    case "Speed":
                        if (string.IsNullOrEmpty(textbox.Text))
                        {
                            textbox.Text = "0";
                        }

                        float speed;
                        if (float.TryParse(textbox.Text, out speed))
                        {
                            if (speed == 0)
                            {
                                // Special case if speed is zero or empty
                                txtWorkoutPace.Text = "0:00";
                            }
                            else
                            {
                                // Update pace string from speed
                                txtWorkoutPace.Text = Utilities.ToTimeString(TimeSpan.FromMinutes(Utilities.SpeedToPace(speed)));
                            }
                        }
                        else
                        {
                            textbox.Text = workout.GetFormattedText("MetersPerSecond");
                        }
                        break;

                    case "Score":
                        if (int.TryParse(textbox.Text, out integer))
                        {
                            workout.Score = float.Parse(textbox.Text);
                        }
                        else
                        {
                            textbox.Text = workout.GetFormattedText("Score");
                        }

                        break;
                }

                if (property == "Time" || property == "Distance" || property == "Pace" || property == "Speed")
                {
                    if (Utilities.TryParseMinutesSeconds(txtWorkoutPace.Text, out pace) || (txtWorkoutPace.Text == "-" && radPace.Checked))
                    {
                        // Calculate the 'calculated' item in the 3 part group
                        if (radTime.Checked)
                        {
                            // Recalculate time
                            workout.TotalTime = TimeSpan.FromMinutes(pace.TotalMinutes * Length.Convert(workout.TotalDistanceMeters, Length.Units.Meter, PluginMain.DistanceUnits));
                            txtWorkoutTime.Text = workout.GetFormattedText("TotalTime");
                        }
                        else if (radDistance.Checked)
                        {
                            // Recalculate distance
                            workout.TotalDistanceMeters = (float)Length.Convert(workout.TotalTime.TotalMinutes / pace.TotalMinutes, PluginMain.DistanceUnits, Length.Units.Meter);
                            txtWorkoutDistance.Text = workout.GetFormattedText("TotalDistanceMeters");
                        }
                        else if (radPace.Checked)
                        {
                            txtWorkoutPace.Text = workout.GetFormattedText("PaceMinPerMeter");
                            txtWorkoutSpeed.Text = workout.GetFormattedText("MetersPerSecond");
                        }

                        // Ensure speed and pace update as partners
                        if (property == "Pace")
                        {
                            txtWorkoutSpeed.Text = workout.GetFormattedText("MetersPerSecond");
                        }
                        else if (property == "Speed")
                        {
                            txtWorkoutPace.Text = workout.GetFormattedText("PaceMinPerMeter");
                        }
                    }
                }

                RefreshTree();
                trainingCal.Month.Calendar.Refresh();
                RefreshChart();
                EnableWorkoutEvents(true);
            }
        }

        /// <summary>
        /// Register or unregister the 'Workout' text change events
        /// </summary>
        /// <param name="enable">True to enable them, false to disable (unregister)</param>
        private void EnableWorkoutEvents(bool enable)
        {
            if (enable && !workoutSubscribed)
            {
                txtWorkoutDistance.Leave += txtWorkoutInfo_Leave;
                txtWorkoutEnd.Leave += txtWorkoutInfo_Leave;
                txtWorkoutName.Leave += txtWorkoutInfo_Leave;
                txtWorkoutPace.Leave += txtWorkoutInfo_Leave;
                txtWorkoutRamp.Leave += txtWorkoutInfo_Leave;
                txtWorkoutScore.Leave += txtWorkoutInfo_Leave;
                txtWorkoutTime.Leave += txtWorkoutInfo_Leave;
                txtWorkoutPeriod.Leave += txtWorkoutInfo_Leave;
                txtWorkoutNotes.Leave += txtWorkoutInfo_Leave;
                txtWorkoutSpeed.Leave += txtWorkoutInfo_Leave;
                chkRepeat.CheckedChanged += chkRepeat_CheckedChanged;
                btnLink.Click += btnLink_Click;
                workoutSubscribed = true;
            }
            else if (!enable)
            {
                txtWorkoutDistance.Leave -= txtWorkoutInfo_Leave;
                txtWorkoutEnd.Leave -= txtWorkoutInfo_Leave;
                txtWorkoutName.Leave -= txtWorkoutInfo_Leave;
                txtWorkoutPace.Leave -= txtWorkoutInfo_Leave;
                txtWorkoutRamp.Leave -= txtWorkoutInfo_Leave;
                txtWorkoutScore.Leave -= txtWorkoutInfo_Leave;
                txtWorkoutTime.Leave -= txtWorkoutInfo_Leave;
                txtWorkoutPeriod.Leave -= txtWorkoutInfo_Leave;
                txtWorkoutNotes.Leave -= txtWorkoutInfo_Leave;
                txtWorkoutSpeed.Leave -= txtWorkoutInfo_Leave;
                chkRepeat.CheckedChanged -= chkRepeat_CheckedChanged;
                btnLink.Click -= btnLink_Click;
                workoutSubscribed = false;
            }
        }

        private void txtLibInfo_Leave(object sender, EventArgs e)
        {
            WorkoutDefinition template = SelectedTemplate;

            if (template != null)
            {
                EnableTemplateEvents(false);

                if (!LogbookSettings.Main.TemplateDefinitions.Contains(SelectedTemplate))
                {
                    LogbookSettings.Main.TemplateDefinitions.Add(SelectedTemplate);
                    template.PropertyChanged += new PropertyChangedEventHandler(template_PropertyChanged);
                }

                ZoneFiveSoftware.Common.Visuals.TextBox textbox = sender as ZoneFiveSoftware.Common.Visuals.TextBox;
                string property = textbox.Tag as string;
                TimeSpan pace;
                int integer;

                switch (property)
                {
                    case "Name":
                        if (!template.IsGarminWorkout)
                        {
                            template.Name = textbox.Text;
                        }
                        else
                        {
                            // Name is read-only for Garmin Workouts
                            txtLibName.Text = template.Name;
                        }
                        break;

                    case "Time":
                        TimeSpan time;
                        if (Utilities.TryParseMinutesSeconds(textbox.Text, out time))
                        {
                            template.TotalTime = time;
                        }
                        else if (string.IsNullOrEmpty(textbox.Text))
                        {
                            template.TotalTime = TimeSpan.Zero;
                        }

                        textbox.Text = template.GetFormattedText("TotalTime");

                        break;

                    case "Distance":
                        float distance;
                        if (float.TryParse(textbox.Text, out distance))
                        {
                            template.TotalDistanceMeters = (float)Length.Convert(distance, PluginMain.DistanceUnits, Length.Units.Meter);
                        }
                        else if (string.IsNullOrEmpty(textbox.Text))
                        {
                            template.TotalDistanceMeters = 0;
                        }
                        else
                        {
                            textbox.Text = template.GetFormattedText("TotalDistanceMeters");
                        }
                        break;

                    case "Pace":
                        if (!Utilities.TryParseMinutesSeconds(textbox.Text, out pace))
                        {
                            txtLibPace.Text = template.GetFormattedText("PaceMinPerMeter");
                        }
                        break;

                    case "Speed":
                        if (string.IsNullOrEmpty(textbox.Text))
                        {
                            textbox.Text = "0";
                        }

                        float speed;
                        if (float.TryParse(textbox.Text, out speed))
                        {
                            if (speed == 0)
                            {
                                // Special case if speed is zero or empty
                                txtLibPace.Text = "0:00";
                            }
                            else
                            {
                                // Update pace string from speed
                                txtLibPace.Text = Utilities.ToTimeString(TimeSpan.FromMinutes(Utilities.SpeedToPace(speed)));
                            }
                        }
                        else
                        {
                            textbox.Text = template.GetFormattedText("MetersPerSecond");
                        }
                        break;

                    case "Score":
                        if (int.TryParse(textbox.Text, out integer))
                        {
                            template.Score = float.Parse(textbox.Text);
                            ChartData.Calculated = false;
                        }
                        else if (string.IsNullOrEmpty(textbox.Text))
                        {
                            template.Score = 0;
                        }
                        else
                        {
                            textbox.Text = template.GetFormattedText("Score");
                        }
                        break;
                    case "Notes":
                        template.Notes = textbox.Text;
                        break;
                }

                if (property == "Time" || property == "Distance" || property == "Pace" || property == "Speed")
                {
                    if (Utilities.TryParseMinutesSeconds(txtLibPace.Text, out pace) || txtLibPace.Text == "-")
                    {
                        // Calculate the 'calculated' item in the 3 part group
                        if (radLibTime.Checked)
                        {
                            // Recalculate time
                            template.TotalTime = TimeSpan.FromMinutes(pace.TotalMinutes * Length.Convert(template.TotalDistanceMeters, Length.Units.Meter, PluginMain.DistanceUnits));
                            txtLibTime.Text = template.GetFormattedText("TotalTime");
                        }
                        else if (radLibDist.Checked)
                        {
                            // Recalculate distance
                            if (template.TotalTime == TimeSpan.Zero || pace == TimeSpan.Zero)
                            {
                                template.TotalDistanceMeters = 0;
                            }
                            else
                            {
                                template.TotalDistanceMeters = (float)Length.Convert(template.TotalTime.TotalMinutes / pace.TotalMinutes, PluginMain.DistanceUnits, Length.Units.Meter);
                            }
                            txtLibDist.Text = template.GetFormattedText("TotalDistanceMeters");
                        }
                        else if (radLibPace.Checked)
                        {
                            txtLibPace.Text = template.GetFormattedText("PaceMinPerMeter");
                            txtLibSpeed.Text = template.GetFormattedText("MetersPerSecond");
                        }

                        // Ensure speed and pace update as partners
                        if (property == "Pace")
                        {
                            txtLibSpeed.Text = template.GetFormattedText("MetersPerSecond");
                        }
                        else if (property == "Speed")
                        {
                            txtLibPace.Text = template.GetFormattedText("PaceMinPerMeter");
                        }
                    }
                }

                EnableTemplateEvents(true);
            }
        }

        /// <summary>
        /// Register or unregister the 'WorkoutDefinition' text change events
        /// </summary>
        /// <param name="enable">True to enable them, false to disable (unregister)</param>
        private void EnableTemplateEvents(bool enable)
        {
            if (enable)
            {
                txtLibDist.Leave += txtLibInfo_Leave;
                txtLibName.Leave += txtLibInfo_Leave;
                txtLibNotes.Leave += txtLibInfo_Leave;
                txtLibPace.Leave += txtLibInfo_Leave;
                txtLibScore.Leave += txtLibInfo_Leave;
                txtLibSpeed.Leave += txtLibInfo_Leave;
                txtLibTime.Leave += txtLibInfo_Leave;
            }
            else
            {
                txtLibDist.Leave -= txtLibInfo_Leave;
                txtLibName.Leave -= txtLibInfo_Leave;
                txtLibNotes.Leave -= txtLibInfo_Leave;
                txtLibPace.Leave -= txtLibInfo_Leave;
                txtLibScore.Leave -= txtLibInfo_Leave;
                txtLibSpeed.Leave -= txtLibInfo_Leave;
                txtLibTime.Leave -= txtLibInfo_Leave;
            }
        }

        private void chkRepeat_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectedWorkout == null) return;

            CheckBox checkbox = sender as CheckBox;

            if (checkbox.Checked)
            {
                SelectedWorkout.EndDate = SelectedPhase.EndDate;
                txtWorkoutEnd.Enabled = true;
                txtWorkoutRamp.Enabled = true;
                txtWorkoutPeriod.Enabled = true;

                if (SelectedWorkout.PeriodDays == 0)
                {
                    SelectedWorkout.PeriodDays = 7;
                }
            }
            else
            {
                SelectedWorkout.EndDate = SelectedWorkout.StartDate;
                txtWorkoutEnd.Enabled = false;
                txtWorkoutRamp.Enabled = false;
                txtWorkoutPeriod.Enabled = false;
            }

            UpdateWorkoutSeries(SelectedWorkout);
            RefreshTree();
            RefreshChart();
            LoadWorkout(SelectedWorkout);
        }

        private void radScore_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;

            if (radio.Checked)
            {
                if (radTrimp.Checked)
                {
                    LogbookSettings.Main.ScoreType = Common.Score.Trimp;
                }
                else
                {
                    LogbookSettings.Main.ScoreType = Common.Score.TSS;
                }

                lblWorkoutScore.Text = ScoreText;

                foreach (TreeList.Column column in itemTree.Columns)
                {
                    if (column.Id == "Score")
                    {
                        column.Text = ScoreText;
                    }
                    else if (column.Id == "Actual.Score")
                    {
                        column.Text = CommonResources.Text.LabelActivity + " " + ScheduleControl.ScoreText;
                    }
                }

                ChartData.Calculated = false;

                if (SelectedPlan != null)
                    SelectedPlan.LinkActivities();

                RefreshChart();
            }
        }

        #endregion

        #region Month Calendar

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            CalendarPopup calendarPopup = new CalendarPopup();

            ZoneFiveSoftware.Common.Visuals.TextBox textbox = sender as ZoneFiveSoftware.Common.Visuals.TextBox;
            calendarPopup.Tag = textbox;

            DateTime date;
            if (!DateTime.TryParse(textbox.Text, out date))
            {
                date = DateTime.Now.Date;
            }

            calendarPopup.Calendar.SetSelected(date, date);
            calendarPopup.ItemSelected += new CalendarPopup.ItemSelectedEventHandler(popupCalendar_ItemSelected);
            calendarPopup.ThemeChanged(PluginMain.GetApplication().VisualTheme);
            calendarPopup.Calendar.StartOfWeek = PluginMain.GetApplication().SystemPreferences.StartOfWeek;
            calendarPopup.Popup(textbox.RectangleToScreen(textbox.ClientRectangle));
        }

        private void popupCalendar_ItemSelected(object sender, CalendarPopup.ItemSelectedEventArgs e)
        {
            // Get objects of interest
            CalendarPopup calendarPopup = sender as CalendarPopup;
            ZoneFiveSoftware.Common.Visuals.TextBox textbox = calendarPopup.Tag as ZoneFiveSoftware.Common.Visuals.TextBox;

            // Store selected value in textbox and save to workout
            textbox.Text = e.Item.ToString("d", CultureInfo.CurrentCulture);
            textbox.SelectNextControl(textbox, true, true, true, true);

            itemTree.Refresh();
            trainingCal.Refresh();

            // Dispose of popup
            calendarPopup.Hide();
            calendarPopup.Dispose();
        }

        /// <summary>
        /// Handles the workout and plan title calendar buttons since they're a little different
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCalendar_Click(object sender, EventArgs e)
        {
            CalendarPopup calendarPopup = new CalendarPopup();

            DateTime date;
            ZoneFiveSoftware.Common.Visuals.TextBox textbox = sender as ZoneFiveSoftware.Common.Visuals.TextBox;
            calendarPopup.Tag = textbox.Tag;

            if (textbox.Tag as string == "Plan.Name" && SelectedPlan != null)
            {
                date = SelectedPlan.StartDate;
            }
            else if (textbox.Tag as string == "Workout.Name" && SelectedWorkout != null)
            {
                date = SelectedWorkout.StartDate;
            }
            else
            {
                return;
            }

            calendarPopup.Calendar.SetSelected(date, date);
            calendarPopup.ItemSelected += new CalendarPopup.ItemSelectedEventHandler(calendarTextPopup_ItemSelected);
            calendarPopup.ThemeChanged(PluginMain.GetApplication().VisualTheme);
            calendarPopup.Calendar.StartOfWeek = PluginMain.GetApplication().SystemPreferences.StartOfWeek;
            calendarPopup.Popup(textbox.RectangleToScreen(textbox.ClientRectangle));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCategory_Click(object sender, EventArgs e)
        {
            TreeListPopup popup = new TreeListPopup();

            ZoneFiveSoftware.Common.Visuals.TextBox textbox = sender as ZoneFiveSoftware.Common.Visuals.TextBox;
            popup.Tag = textbox.Tag;
            TreeNodeCollection categories = LogbookSettings.GetCategoryNodes();

            popup.Tree.Columns.Add(new TreeList.Column());
            popup.Tree.RowData = categories;

            foreach (LibraryNode category in categories)
            {
                popup.Tree.SetExpanded(category, true, true);
            }

            popup.ItemSelected += new TreeListPopup.ItemSelectedEventHandler(popupCategory_ItemSelected);
            popup.ThemeChanged(PluginMain.GetApplication().VisualTheme);
            popup.Popup(textbox.RectangleToScreen(textbox.ClientRectangle));
        }

        private void popupCategory_ItemSelected(object sender, TreeListPopup.ItemSelectedEventArgs e)
        {
            TreeListPopup popup = sender as TreeListPopup;
            string tag = popup.Tag as string;
            LibraryNode node = e.Item as LibraryNode;

            if (tag == "Workout")
            {
                SelectedWorkout.Category = node.Category;
                txtWorkoutCategory.Text = SelectedWorkout.GetFormattedText("Category");
            }
            else if (tag == "Template")
            {
                txtLibCategory.Text = SelectedTemplate.GetFormattedText("Category");
                WorkoutDefinition template = SelectedTemplate;
                template.Category = node.Category;
                itemTree.SelectedItems = new TreeNodeCollection(ChartData.GetLibraryNode(template, itemTree.RowData as TreeNodeCollection));
            }
        }

        /// <summary>
        /// Handles the workout and plan title calendar buttons since they're a little different.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calendarTextPopup_ItemSelected(object sender, CalendarPopup.ItemSelectedEventArgs e)
        {
            // Get objects of interest
            CalendarPopup calendarPopup = sender as CalendarPopup;
            string property = calendarPopup.Tag as string;

            // Store selected value in textbox and save to workout
            if (property == "Workout.Name")
            {
                // Update workout start time
                if (SelectedPlan.GetPhase(e.Item) != SelectedPlan.GetPhase(SelectedWorkout.StartDate))
                {
                    Phase destPhase = SelectedPlan.GetPhase(e.Item);
                    if (destPhase == null)
                    {
                        // Illegal move.  Cannot move outside bounds of the plan.  Reject change.
                        return;
                    }

                    // Move from one phase to the right phase
                    SelectedPlan.GetPhase(SelectedWorkout.StartDate).RemoveWorkout(SelectedWorkout);
                    destPhase.AddWorkout(SelectedWorkout);
                    trainingCal.Dates.MoveToTop(SelectedWorkout.CalendarItem);
                }

                SelectedWorkout.StartDate = e.Item;
                UpdateWorkoutSeries(SelectedWorkout);
                trainingCal.SelectDate(SelectedWorkout.StartDate);
                LoadWorkout(SelectedWorkout);
            }
            else if (property == "Plan.Name")
            {
                SelectedPlan.StartDate = e.Item;
                lblPlanTitle.Text = string.Format("{0}: {1} - {2}", Strings.Label_Plan,
                    SelectedPlan.StartDate.ToString("d", CultureInfo.CurrentCulture),
                    SelectedPlan.EndDate.ToString("d", CultureInfo.CurrentCulture));
                trainingCal.Invalidate();
            }
            else
            {
                throw new ArgumentException();
            }

            RefreshTree();
            RefreshChart();

            // Dispose of popup
            calendarPopup.Hide();
            calendarPopup.Dispose();
        }

        /// <summary>
        /// Enable wheel scrolling through the calendar using the mouse
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trainingCal_MouseWheel(object sender, MouseEventArgs e)
        {
            int days;
            if (e.Delta > 0)
                days = -7;
            else
                days = 7;

            trainingCal.Month.SelectedMonth = trainingCal.Month.SelectedMonth.AddDays(days).Date;

            trainingCal.Setup();

            if (trainingCal.SelectedDates.Count > 0)
            {
                DateTime selectedDate = trainingCal.SelectedDates[0].AddDays(-days);

                if (trainingCal.Month.FirstDay <= selectedDate && selectedDate < trainingCal.Month.LastDay)
                {
                    trainingCal.DaySelected -= trainingCal_DaySelected;
                    trainingCal.SelectDate(selectedDate);
                    trainingCal.DaySelected += trainingCal_DaySelected;
                }
            }

            trainingCal.Month.Calendar.Refresh();
        }

        private void radTimeDistancePace_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            ZoneFiveSoftware.Common.Visuals.TextBox textbox = radio.Tag as ZoneFiveSoftware.Common.Visuals.TextBox;
            textbox.ReadOnly = radio.Checked;

            if (radio.Checked)
            {
                // Readonly
                textbox.BackColor = PluginMain.GetApplication().VisualTheme.Window;
                textbox.ForeColor = PluginMain.GetApplication().VisualTheme.ControlText;
            }
            else
            {
                // Read/write
                textbox.BackColor = Color.White;
                textbox.ForeColor = PluginMain.GetApplication().VisualTheme.ControlText;
            }

            if (textbox.Tag as string == "Pace")
            {
                txtWorkoutSpeed.ReadOnly = textbox.ReadOnly;
                txtWorkoutSpeed.BackColor = textbox.BackColor;
                txtWorkoutSpeed.ForeColor = textbox.ForeColor;
            }
        }

        private void radLibTimeDistancePace_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            ZoneFiveSoftware.Common.Visuals.TextBox textbox = radio.Tag as ZoneFiveSoftware.Common.Visuals.TextBox;
            textbox.ReadOnly = radio.Checked;

            if (radio.Checked)
            {
                // Readonly
                textbox.BackColor = PluginMain.GetApplication().VisualTheme.Window;
                textbox.ForeColor = PluginMain.GetApplication().VisualTheme.ControlText;
            }
            else
            {
                // Read/write
                textbox.BackColor = Color.White;
                textbox.ForeColor = PluginMain.GetApplication().VisualTheme.ControlText;
            }

            if (textbox.Tag as string == "Pace")
            {
                txtLibSpeed.ReadOnly = textbox.ReadOnly;
                txtLibSpeed.BackColor = textbox.BackColor;
                txtLibSpeed.ForeColor = textbox.ForeColor;
            }
        }

        private void trainingCal_DaySelected(object sender, DaySelectedEventArgs e)
        {
            if (tabControl.SelectedTab == tabWeek)
                RefreshWeekView();

            DateTime date = SelectedCalendarDate;
            if (SelectedPlan == null) return;

            selectedPhase = SelectedPlan.GetPhase(date);
            selectedWorkout = CollectionUtils.GetFirstItemOfType<Workout>(SelectedPlan.GetWorkouts(date));

            if (selectedWorkout != null && GlobalSettings.Main.TreeType == GlobalSettings.TreeOption.TrainingPlan)
                itemTree.SelectedItems = new TreeNodeCollection(selectedWorkout.Node);

            LoadPhase(SelectedPhase);
            LoadWorkout(SelectedWorkout);

            SetHighlightDate(ChartData.HighlightFocus, date);
        }

        /// <summary>
        /// Drag event.  Incoming workout from the library
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trainingCal_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void trainingCal_DayDragDrop(object sender, DayDragDropEventArgs e)
        {
            DateTime date = DateTime.Parse(e.Date);

            if (e.Data.GetData(typeof(Workout)) != null)
            {
                Workout workout = e.Data.GetData(typeof(Workout)) as Workout;
                Workout clone = workout.Clone();
                clone.StartDate = date;

                if (workout.IsRepeating)
                {
                    Phase phase = SelectedPlan.GetPhase(clone.StartDate);
                    clone.EndDate = phase.EndDate;
                }

                AddWorkout(clone);
                LoadWorkout(clone);
            }
            else if (e.Data.GetData(typeof(WorkoutDefinition)) != null)
            {
                WorkoutDefinition definition = e.Data.GetData(typeof(WorkoutDefinition)) as WorkoutDefinition;
                Workout instance = new Workout(definition);
                instance.StartDate = date;

                if (!LogbookSettings.Main.TemplateDefinitions.Contains(definition))
                {
                    LogbookSettings.Main.TemplateDefinitions.Add(definition);
                }

                AddWorkout(instance);
                LoadWorkout(instance);
            }
        }

        private void trainingCal_DayClick(object sender, DayClickEventArgs e)
        {
            DateTime date;
            if (e.Button == MouseButtons.Right && DateTime.TryParse(e.Date, out date))
            {
                // Set mouse date prior to opening a context menu.  Date used in context menu initialization.
                mouseDate = date;
            }
        }

        private void ScheduleControl_Load(object sender, EventArgs e)
        {
            TrainingPlan plan = TrainingPlan.OpenPlan(LogbookSettings.Main.CurrentTrainingPlan);
            LoadPlan(plan, true);
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            Workout workout = SelectedWorkout;
            ZoneFiveSoftware.Common.Visuals.Button button = sender as ZoneFiveSoftware.Common.Visuals.Button;

            workout.IsLinked = !workout.IsLinked;

            if (workout.IsLinked)
                button.BorderStyle = BorderStyle.Fixed3D;

            else
                button.BorderStyle = BorderStyle.None;
        }

        #endregion

        #region Logbook

        private void PluginMain_LogbookChanged(object sender, PropertyChangedEventArgs e)
        {
            PluginMain.GetApplication().Logbook.Activities.CollectionChanged += new NotifyCollectionChangedEventHandler<IActivity>(Activities_CollectionChanged);
            ChartData.Calculated = false;

            LogbookSettings.loaded = false;
            LogbookSettings.LoadSettings();

            Utilities.CategoryIndex.Clear();

            // Load proper FitPlan
            string path = LogbookSettings.Main.CurrentTrainingPlan;
            if (string.IsNullOrEmpty(path) || !System.IO.File.Exists(path))
                AddPlan(false);
            else
                LoadPlan(TrainingPlan.OpenPlan(LogbookSettings.Main.CurrentTrainingPlan), false);

            // Set UI for new settings
            radTrimp.Checked = LogbookSettings.Main.ScoreType == Common.Score.Trimp;
            radTSS.Checked = LogbookSettings.Main.ScoreType == Common.Score.TSS;
        }

        private void Logbook_BeforeSave(object sender, EventArgs e)
        {
            if (SelectedPlan != null)
                SelectedPlan.SavePlan(false);
        }

        #endregion

        #region Workout/Phase/Plan Tree

        private void phaseTree_Load(object sender, EventArgs e)
        {
            LoadTree(GlobalSettings.Main.TreeType);
        }

        /// <summary>
        /// Set text on treelist menu items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextTreelist_Opening(object sender, CancelEventArgs e)
        {
            bool headerClicked = false;
            FitPlanNode node;

            if (itemTree.SelectedItems.Count == 0 || itemTree.SelectedItems[0] as FitPlanNode == null)
            {
                Point client = itemTree.PointToClient(MousePosition);
                node = null;

                if (client.Y <= itemTree.HeaderRowHeight && GlobalSettings.Main.TreeType == GlobalSettings.TreeOption.TrainingPlan)
                {
                    headerClicked = true;
                }
                else
                {
                    e.Cancel = true;
                    return;
                }
            }
            else
            {
                node = itemTree.SelectedItems[0] as FitPlanNode;
            }

            ctxTreeAddPhase.Visible = !headerClicked;
            ctxTreeDelete.Visible = !headerClicked;
            ctxTreeInsertPhase.Visible = !headerClicked;
            ctxTreeAddWorkout.Visible = !headerClicked;
            ctxTreeClosePlan.Visible = !headerClicked;

            ctxTreeColumnsPicker.Visible = headerClicked;
            ctxTreeResetColumns.Visible = headerClicked;

            if (headerClicked)
            {
            }
            else if (node.IsPhase)
            {
                ctxTreeDelete.Text = string.Format(Strings.Action_Delete, Strings.Label_Phase);
                ctxTreeDelete.Enabled = true;
            }
            else if (node.IsWorkout)
            {
                ctxTreeDelete.Text = string.Format(Strings.Action_Delete, Strings.Label_Workout);
                ctxTreeDelete.Enabled = true;
            }
            else
            {
                ctxTreeDelete.Enabled = false;
            }
        }

        /// <summary>
        /// "Delete xxx" treelist menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteTreelistMenuItem_Click(object sender, EventArgs e)
        {
            if (itemTree.SelectedItems.Count == 0)
                return;

            FitPlanNode node = itemTree.SelectedItems[0] as FitPlanNode;

            if (node.IsPhase)
                RemovePhase(node.Element as Phase);

            else if (node.IsWorkout)
                RemoveWorkout(node.Element as Workout);

            trainingCal.Month.Calendar.Refresh();
        }

        /// <summary>
        /// "Add Workout" treelist menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addWorkoutTreelistMenu_Click(object sender, EventArgs e)
        {
            DateTime date = SelectedPhase.StartDate;
            AddWorkout(date);
        }

        /// <summary>
        /// "Add Phase" treelist menu item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addPhaseTreelistMenu_Click(object sender, EventArgs e)
        {
            AddPhase(SelectedCalendarDate);
        }

        /// <summary>
        /// "Insert Phase" menu item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Used for several different UI menu items.</remarks>
        private void insertPhase_Click(object sender, EventArgs e)
        {
            InsertPhase(SelectedCalendarDate, Common.DefaultPhaseDays);
        }

        private void btnNewTemplate_Click(object sender, EventArgs e)
        {
            WorkoutDefinition def = new WorkoutDefinition();

            LibraryNode node = CollectionUtils.GetFirstItemOfType<LibraryNode>(itemTree.SelectedItems);
            if (node != null)
                def.Category = node.Category;

            def.PropertyChanged += new PropertyChangedEventHandler(template_PropertyChanged);
            LogbookSettings.Main.TemplateDefinitions.Add(def);

            RefreshTree();

            LibraryNode defNode = ChartData.GetLibraryNode(def, itemTree.RowData as TreeNodeCollection);
            if (defNode != null)
                itemTree.SelectedItems = new TreeNodeCollection(defNode);
        }

        private void btnDelTemplate_Click(object sender, EventArgs e)
        {
            LibraryNode node = CollectionUtils.GetFirstItemOfType<LibraryNode>(itemTree.SelectedItems);

            if (node != null && node.IsWorkoutDef && LogbookSettings.Main.TemplateDefinitions.Contains(node.Element))
            {
                WorkoutDefinition def = node.Element as WorkoutDefinition;
                LogbookSettings.Main.TemplateDefinitions.Remove(def);
                LibraryNode next = itemTree.FindNextSelectedAfterDelete(node) as LibraryNode;

                RefreshTree();

                next = ChartData.GetLibraryNode(next.Element, itemTree.RowData as TreeNodeCollection);

                if (next != null)
                    itemTree.SelectedItems = new TreeNodeCollection(next);
            }
        }

        private void template_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Category" || e.PropertyName == "ImageName" || e.PropertyName == "Time" || e.PropertyName == "Distance" || e.PropertyName == "Score" || e.PropertyName == "Name")
                RefreshTree();
        }

        #endregion

        #region Toolbar, Menu, and other Actions

        private void itemTreePlanItemChanged()
        {
            FitPlanNode node = CollectionUtils.GetFirstItemOfType<FitPlanNode>(itemTree.SelectedItems);

            if (node != null && node.IsWorkout)
            {
                selectedWorkout = node.Element as Workout;
                mouseDate = selectedWorkout.StartDate;
                selectedPhase = SelectedPlan.GetPhase(selectedWorkout.StartDate);
                trainingCal.SelectDate(selectedWorkout.StartDate);
            }
            else if (node != null && node.IsPhase)
            {
                selectedPhase = node.Element as Phase;
                selectedWorkout = null;
                if (SelectedPlan.GetPhase(SelectedCalendarDate) != selectedPhase)
                {
                    if ((SelectedCalendarDate - selectedPhase.StartDate).Duration() < (SelectedCalendarDate - selectedPhase.EndDate).Duration())
                        trainingCal.SelectDate(selectedPhase.StartDate);
                    else
                        trainingCal.SelectDate(selectedPhase.EndDate);
                }
            }
            else if (node != null && node.IsPhaseSummary)
            {
                selectedWorkout = null;
                selectedPhase = (node.Element as Phase.PhaseSummary).Parent;
            }

            LoadWorkout(SelectedWorkout);
            LoadPhase(SelectedPhase);
            LoadPlan(SelectedPlan, false); // Will be used in future when multiple plans are managed
        }

        private void itemTreeLibraryItemChanged()
        {
            LibraryNode node = CollectionUtils.GetFirstItemOfType<LibraryNode>(itemTree.SelectedItems);

            if (node != null)
            {
                WorkoutDefinition template = node.Workout;
                if (template != null || node.IsCategory)
                    LoadTemplate(template);
            }
        }

        #endregion

        #region Calendar Context Menu

        private void contextCalendar_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool dateInPlan = SelectedPlan != null && SelectedPlan.StartDate <= mouseDate && mouseDate <= SelectedPlan.EndDate;
            bool containsWorkout = SelectedPlan != null && SelectedPlan.ContainsWorkout(mouseDate);

            ctxCalAddWorkout.Enabled = dateInPlan;
            ctxCalAddPhase.Enabled = dateInPlan;
            ctxCalAddTemplate.Enabled = dateInPlan && containsWorkout;
            ctxCalLockWorkout.Enabled = dateInPlan && containsWorkout;
            if (containsWorkout)
            {
                Workout workout = CollectionUtils.GetFirstItemOfType<Workout>(SelectedPlan.GetWorkouts(mouseDate));

                if (workout != null && workout.DateLocked)
                    ctxCalLockWorkout.Text = Strings.Action_UnlockDate;
                else
                    ctxCalLockWorkout.Text = Strings.Action_LockDate;
            }

            if (dateInPlan && SelectedPlan.ContainsWorkout(mouseDate))
            {
                // Delete Workout
                ctxCalDeleteWorkout.Text = string.Format(Strings.Action_Delete, Strings.Label_Workout);
                ctxCalDeleteWorkout.Enabled = true;
            }
            else if (dateInPlan && 1 < SelectedPlan.Phases.Count)
            {
                // Delete Phase
                ctxCalDeleteWorkout.Text = string.Format(Strings.Action_Delete, Strings.Label_Phase);
                ctxCalDeleteWorkout.Enabled = true;
            }
            else
            {
                // Disabled (Delete Phase)
                ctxCalDeleteWorkout.Text = string.Format(Strings.Action_Delete, Strings.Label_Phase);
                ctxCalDeleteWorkout.Enabled = false;
            }

            ctxCalScheduleWorkout.Enabled = dateInPlan && SelectedWorkout != null && SelectedWorkout.LinkedTemplate != null && SelectedWorkout.LinkedTemplate.IsGarminWorkout;
        }

        private void ctxCalAddWorkout_Click(object sender, EventArgs e)
        {
            DateTime date = mouseDate;
            AddWorkout(date);
        }

        private void ctxCalAddPhase_Click(object sender, EventArgs e)
        {
            AddPhase(mouseDate);
        }

        private void ctxCalDeleteWorkout_Click(object sender, EventArgs e)
        {
            Workout workout = CollectionUtils.GetFirstItemOfType<Workout>(SelectedPlan.GetWorkouts(mouseDate));

            if (workout != null)
                RemoveWorkout(workout);
            else
                RemovePhase(SelectedPlan.GetPhase(mouseDate));
        }

        private void ctxCalAddTemplate_Click(object sender, EventArgs e)
        {
            Workout workout = CollectionUtils.GetFirstItemOfType<Workout>(SelectedPlan.GetWorkouts(mouseDate));
            if (workout != null)
            {
                WorkoutDefinition template = new WorkoutDefinition(workout);
                template.PropertyChanged += new PropertyChangedEventHandler(template_PropertyChanged);
                LogbookSettings.Main.TemplateDefinitions.Add(template);
                RefreshTree();
            }
        }

        private void ctxCalLockWorkout_Click(object sender, EventArgs e)
        {
            Workout workout = CollectionUtils.GetFirstItemOfType<Workout>(SelectedPlan.GetWorkouts(mouseDate));
            workout.DateLocked = !workout.DateLocked;
        }

        #endregion

        #region Garmin

        private void scheduleWorkoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Data.GarminFitness.Manager.IsInstalled)
            {
                WorkoutCollection workouts = SelectedPlan.GetWorkouts(mouseDate);
                foreach (Workout workout in workouts)
                {
                    bool ok = Data.GarminFitness.Manager.ScheduleWorkout(workout);
                }
            }
        }

        private void txtGarminWorkout_DragDrop(object sender, DragEventArgs e)
        {
            WorkoutDefinition def = e.Data.GetData(typeof(WorkoutDefinition)) as WorkoutDefinition;
            if (def != null)
            {
                if (!LogbookSettings.Main.TemplateDefinitions.Contains(def))
                    LogbookSettings.Main.TemplateDefinitions.Add(def);

                SelectedWorkout.TemplateId = def.Id;
                LoadWorkout(SelectedWorkout);
            }
        }

        private void txtGarminWorkout_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void chkGarminAutoSched_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGarminAutoSched.Checked)
            {
                if (SelectedPlan.AutoScheduleDays < 0)
                    SelectedPlan.AutoScheduleDays = 7;
            }
            else
            {
                SelectedPlan.AutoScheduleDays = -1;
            }

            RefreshAutoSchedule();
        }

        internal void RefreshAutoSchedule()
        {
            if (!Data.GarminFitness.Manager.IsInstalled) return;

            if (!chkGarminAutoSched.Checked || SelectedPlan == null || SelectedPlan.AutoScheduleDays < 0)
                txtPlanGarminAutoSched.Text = string.Empty;
            else
                txtPlanGarminAutoSched.Text = SelectedPlan.AutoScheduleDays.ToString();

            txtPlanGarminAutoSched.Enabled = chkGarminAutoSched.Checked;
        }

        private void btnGarminFitness_Click(object sender, EventArgs e)
        {
            if (FitPlan.Data.GarminFitness.Manager.IsInstalled)
                PluginMain.GetApplication().ShowView(new Guid("abf90a14-e325-47d1-b5e1-801e12df8bb0"), string.Empty);
        }

        #endregion

        private void trainingCal_MouseMove(object sender, MouseEventArgs e)
        {
            // TODO: Implement movable dates via drag/drop
            if (e.Button == MouseButtons.Left)
            {
                isDraggingDate = true;
            }
            else if (isDraggingDate)
            {
                dragDate = DateTime.MinValue;
                isDraggingDate = false;
            }

        }

        private void trainingCal_DayMouseMove(object sender, DayMouseMoveEventArgs e)
        {
            // TODO: Implement movable dates via drag/drop
            if (isDraggingDate)
            {
                dragDate = DateTime.Parse(e.Date);
                //trainingCal.DoDragDrop(dragDate, DragDropEffects.Move);
            }
        }

        private void trainingCal_DayDoubleClick(object sender, DayClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DateTime date = DateTime.Parse(e.Date);
                DateItemCollection dates = GetDateItemType(date, "Activity");
                if (dates != null && 0 < dates.Count)
                {
                    string tag = dates[0].Tag as string;
                    string bookmark = "id=" + tag.Substring(8, 36);
                    PluginMain.GetApplication().ShowView(GUIDs.DailyActivityView, bookmark);
                }
            }
        }

        private void btnGroupBy_Click(object sender, EventArgs e)
        {
            menuGroupBy.Show(btnGroupBy, btnGroupBy.ClientRectangle.Left, btnGroupBy.ClientRectangle.Bottom);
        }

        private void menuGroupItem_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem item in menuGroupBy.Items)
            {
                item.Checked = (item == sender);

                if (item.Checked)
                    Settings.GlobalSettings.Main.GroupBy = (GlobalSettings.GroupOption)Enum.Parse(typeof(GlobalSettings.GroupOption), item.Tag as string);
            }

            RefreshChart();
        }

        private void btnGroupBy_Resize(object sender, EventArgs e)
        {
            btnGroupBy.Location = new Point(btnRampEdit.Location.X - btnGroupBy.Width, 0);
        }

        private string zedChart_PointValueEvent(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt)
        {
            if (!GlobalSettings.Main.IsDateBasedChart && GlobalSettings.Main.GroupBy == GlobalSettings.GroupOption.Category && curve.IsBar)
            {
                // "Running: 128 miles" - Category Chart Tooltip
                if (MousePosition != tooltipMousePt)
                {
                    // Refresh if mouse moves
                    chartToolTip.Active = true;
                    string unit;
                    if (GlobalSettings.Main.ProgressChart == GlobalSettings.ProgressOption.Time)
                        unit = Time.LabelAbbr(Time.TimeRange.Hour);
                    else
                        unit = Length.LabelAbbr(PluginMain.DistanceUnits);

                    string label = string.Format("{0}:\r\n{1} {2}", curve[iPt].Tag as string, curve[iPt].Y.ToString("0.#", CultureInfo.CurrentCulture), unit);
                    chartToolTip.SetToolTip(sender, label);
                    tooltipMousePt = MousePosition;
                }
            }
            else if (GlobalSettings.Main.ChartType == GlobalSettings.ChartOption.Progress && curve.IsBar)
            {
                // "Actual: 64.2 miles" - Bar tooltip
                if (MousePosition != tooltipMousePt)
                {
                    // Refresh if mouse moves
                    chartToolTip.Active = true;
                    string unit;
                    if (GlobalSettings.Main.ProgressChart == GlobalSettings.ProgressOption.Time)
                        unit = Time.LabelAbbr(Time.TimeRange.Hour);
                    else
                        unit = Length.LabelAbbr(PluginMain.DistanceUnits);

                    string actual = (Strings.Label_Actual.Substring(0, Strings.Label_Actual.IndexOf("{")) + Strings.Label_Actual.Substring(Strings.Label_Actual.IndexOf("}") + 1)).Trim();
                    string label = string.Format("{0}:\r\n{1} {2}", actual, curve[iPt].Y.ToString("0.#", CultureInfo.CurrentCulture), unit);
                    chartToolTip.SetToolTip(sender, label);
                    tooltipMousePt = MousePosition;
                }

            }
            else
            {
                // Ignore, display nothing
                chartToolTip.Active = false;
                return default(string);
            }
            return default(string);
        }

        private string zedChart_CursorValueEvent(ZedGraphControl sender, GraphPane pane, Point mousePt)
        {
            return default(string);
        }

        #region CTL Target Handling/Management

        private void hiLoSlider1_SliderMoved(object sender, PropertyChangedEventArgs e)
        {
            HiLoSlider slider = sender as HiLoSlider;

            if (slider == null)
                return;
            else if (e.PropertyName == "Low")
                LogbookSettings.Main.CTLRampLowLim = slider.Low;
            else if (e.PropertyName == "High")
                LogbookSettings.Main.CTLRampHighLim = slider.High;

            // Refresh Planning chart to match new color scheme
            RefreshChart();
        }

        private string zedPlanning_PointEditEvent(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt)
        {
            if (curve.Label.Text != "CTLt")
            {
                // Only allow editing of CTL target series
                RefreshChart();
                return string.Empty;
            }

            // Move single point in X & Y direction
            curve[iPt].Y = Math.Round(curve[iPt].Y);
            curve[iPt].X = Math.Round(curve[iPt].X);
            SelectedPlan.Days[iPt].TargetCTL = (int)curve[iPt].Y;
            SelectedPlan.Days[iPt].SetDays(SelectedPlan.StartDate, XDate.XLDateToDateTime(curve[iPt].X).Date);
            SelectedPlan.Days.Sort();

            PointPairList points = ChartData.TargetCTL.Points as PointPairList;

            if (!points.Sorted)
                points.Sort(SortType.XValues);

            RefreshChart();

            if (GlobalSettings.Main.ShowCTLTarget)
                RefreshChart();

            return string.Empty;
        }

        private bool zedPlanning_DoubleClickEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            // Only handle this when adding points to CTL target.  Otherwise, double-click should auto-zoom.  Click must be in chart boundaries
            if (e.Button == MouseButtons.Left && sender.IsEnableVEdit && sender.GraphPane.Chart.Rect.Contains(e.X, e.Y))
            {
                double x, y;
                GraphPane pane = sender.GraphPane;
                LineItem line = ChartData.TargetCTL;
                pane.ReverseTransform(e.Location, out x, out y);

                // Add new day to current plan
                SelectedPlan.Days.Add(new FitPlan.Schedule.Day(SelectedPlan.StartDate, XDate.XLDateToDateTime(x), (int)y));

                PointPairList points = ChartData.TargetCTL.Points as PointPairList;
                if (!points.Sorted)
                    points.Sort(SortType.XValues);

                RefreshChart();

                return true; // handled
            }
            else if (e.Button == MouseButtons.Left && sender.GraphPane.Chart.Rect.Contains(e.X, e.Y))
            {
                // Double-click chart area, auto-zoom to current plan
                ZoomPlan(true);
                return true; // handled
            }
            else if (e.Button == MouseButtons.Left && sender.GraphPane.XAxis.GetRectangle(sender.GraphPane).Contains(e.X, e.Y))
            {
                // Double-click X-axis, auto-zoom to current plan
                ZoomPlan(false);
                return true; // handled
            }

            return false; // bubble event up
        }

        private bool zedPlanning_MouseDownEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            //int ctlTindex = sender.GraphPane.CurveList.IndexOf("CTLt");
            int iNearest;
            CurveItem curve;
            bool tst = sender.GraphPane.FindNearestPoint(e.Location, ChartData.TargetCTL, out curve, out iNearest);

            if (e.Button == MouseButtons.Right && sender.IsEnableVEdit &&
                sender.GraphPane.FindNearestPoint(e.Location, ChartData.TargetCTL, out curve, out iNearest))
            {
                SelectedPlan.Days.RemoveAt(iNearest);

                RefreshChart();
                return true;
            }

            return false;
        }

        private void tabPlanning_Enter(object sender, EventArgs e)
        {
            RefreshChart();
        }

        /// <summary>
        /// As Days are added/removed from the current plan, update chart data to reflect these changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Days_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            DayCollection days = sender as DayCollection;
            FitPlan.Schedule.Day day = e.Element as FitPlan.Schedule.Day;

            switch (e.Action)
            {
                case CollectionChangeAction.Add:
                    ChartData.TargetCTL.AddPoint(new XDate(day.GetDate(SelectedPlan.StartDate)), day.TargetCTL);
                    ChartData.RefreshTargetRampColors();
                    break;

                case CollectionChangeAction.Remove:
                    int lo, hi;
                    DateTime date = day.GetDate(SelectedPlan.StartDate);
                    double xdate = XDate.DateTimeToXLDate(date);

                    if (ChartData.TargetCTL.GetPointIndex(xdate, out lo, out hi, zedChart.GraphPane))
                    {
                        ChartData.TargetCTL.RemovePoint(lo);
                        ChartData.RefreshTargetRampColors();
                    }
                    break;
            }
        }

        #endregion

        private void digitValidator(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8 && e.KeyChar != '.')
                e.Handled = true; // input is not passed on to the control(TextBox)
        }

        #endregion

        private void itemTree_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void itemTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                switch (GlobalSettings.Main.TreeType)
                {
                    case GlobalSettings.TreeOption.TrainingPlan:
                        deleteTreelistMenuItem_Click(itemTree, null);
                        e.Handled = true;
                        break;

                    case GlobalSettings.TreeOption.Library:
                        btnDelTemplate_Click(itemTree, null);
                        e.Handled = true;
                        break;

                    default:
                        break;
                }
            }
        }

        private void syncCalendarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            currentPlan.CalendarProvider.SyncPlan(true);
            this.Cursor = Cursors.Default;
        }

        private void removeFromGoogleCalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPlan.CalendarProvider.DeletePlan();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FitPlan.Calendar.CalLogin calLoginDlg = new FitPlan.Calendar.CalLogin(currentPlan);
            calLoginDlg.ShowDialog();
        }

        private void btnGoogCal_Click(object sender, EventArgs e)
        {
            contextInetCalBtn.Show(btnGoogCal, new Point(btnGoogCal.Width, btnGoogCal.Height), ToolStripDropDownDirection.BelowLeft);
        }

        private void contextInetCalBtn_Opening(object sender, CancelEventArgs e)
        {
            // Customize the menu titles (show "logged in as" and "sync calendar" info) 
            string format;
            if (currentPlan.CalendarProvider.IsLoggedIn && !string.IsNullOrEmpty(LogbookSettings.Main.GoogleUserName))
                format = "{0} ({1})";
            else
                format = "{0}";

            loginToolStripMenuItem.Text = string.Format(format, Strings.Action_Login, LogbookSettings.Main.GoogleUserName, currentPlan.CalendarProvider.CalendarName);
            // TODO: Localize "Synchronize Calendars"
            syncCalendarToolStripMenuItem.Text = string.Format(format, "Sync Calendar", currentPlan.CalendarProvider.CalendarName);

            // NOTE: Premium feature: Google Calendar Integration
            // Enable/Disable menu items as appropriate
            syncCalendarToolStripMenuItem.Enabled = currentPlan.CalendarProvider.LoginAvailable;
            autoSyncToolStripMenuItem.Enabled = syncCalendarToolStripMenuItem.Enabled;
            optionsToolStripMenuItem.Enabled = syncCalendarToolStripMenuItem.Enabled;
            removeFromGoogleCalToolStripMenuItem.Enabled = currentPlan.CalendarProvider.LoginAvailable; // Can clear / repair a calendar even if it's not activated 
        }

        private void autoSyncToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPlan.IsCalendarAutoSync = autoSyncToolStripMenuItem.Checked;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoogleOptions form = new GoogleOptions(currentPlan);
            form.ShowDialog();
        }
    }
}