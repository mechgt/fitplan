using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Visuals;
using FitPlan.Schedule;
using ZoneFiveSoftware.Common.Data.Fitness;
using FitPlan.Data;
using FitPlan.Settings;
using System.Globalization;

namespace FitPlan.Controls
{
    public partial class CreatePlan : Form
    {
        # region Fields

        internal event PropertyChangedEventHandler PropertyChanged;
        private List<PlanComponent> lastMod = new List<PlanComponent>() { PlanComponent.start, PlanComponent.end, PlanComponent.weeks };
        private DateTime planStart, planEnd;
        private int planWeeks;
        private bool updating;

        #endregion

        # region Constructor

        public CreatePlan()
        {
            InitializeComponent();

            txtPlanName.Text = string.Format(Resources.Strings.Label_AthletesTrainingPlan, PluginMain.GetApplication().Logbook.Athlete.Name);
            txtPlanWeeks.ForeColor = txtPlanName.ForeColor;
            PropertyChanged += new PropertyChangedEventHandler(CreatePlan_PropertyChanged);
        }

        private void CreatePlan_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (updating)
            {
                return;
            }

            updating = true;

            if (e.PropertyName == "StartDate")
            {
                CalculatePlan(PlanComponent.start);
            }
            else if (e.PropertyName == "EndDate")
            {
                CalculatePlan(PlanComponent.end);
            }
            else if (e.PropertyName == "Weeks")
            {
                CalculatePlan(PlanComponent.weeks);
            }

            updating = false;
        }

        public CreatePlan(DateTime startDate)
            : this()
        {
            StartDate = startDate;
            EndDate = startDate.AddDays((double)txtPlanWeeks.Value * 7);
        }

        #endregion

        #region Enums

        private enum PlanComponent
        {
            start,
            end,
            weeks
        }

        #endregion

        #region Properties

        internal TrainingPlan Plan;

        internal DateTime StartDate
        {
            get
            {
                DateTime date;
                if (DateTime.TryParse(txtPlanStart.Text, out date))
                    return date.Date;
                else
                    return this.planStart;
            }
            set
            {
                value = value.Date;

                if (this.planStart != value)
                {
                    this.planStart = value;
                    txtPlanStart.Text = value.ToString("d", CultureInfo.CurrentCulture);
                    RaisePropertyChangedEvent("StartDate");
                }
            }
        }

        internal DateTime EndDate
        {
            get
            {
                DateTime date;
                if (DateTime.TryParse(txtPlanEnd.Text, out date))
                    return date.Date;
                else
                    return this.planEnd;
            }
            set
            {
                value = value.Date;
                if (this.planEnd != value)
                {
                    this.planEnd = value;
                    txtPlanEnd.Text = value.ToString("d", CultureInfo.CurrentCulture);
                    RaisePropertyChangedEvent("EndDate");
                }
            }
        }

        internal int Weeks
        {
            get
            {
                return (int)txtPlanWeeks.Value;
            }
            set
            {
                if (this.planWeeks != value)
                {
                    this.planWeeks = value;
                    txtPlanWeeks.Value = value;
                    RaisePropertyChangedEvent("Weeks");
                }
            }
        }

        internal DateTime LogStart
        {
            get
            {
                DateTime date;
                if (DateTime.TryParse(txtLogStart.Text, out date))
                    return date;
                else
                    return DateTime.MinValue;
            }
            set
            {
                txtLogStart.Text = value.ToString("d", CultureInfo.CurrentCulture);
            }
        }

        internal DateTime LogEnd
        {
            get
            {
                DateTime date;
                if (DateTime.TryParse(txtLogEnd.Text, out date))
                    return date;
                else
                    return DateTime.MinValue;
            }
            set
            {
                txtLogEnd.Text = value.ToString("d", CultureInfo.CurrentCulture);
            }
        }

        #endregion

        #region Methods

        private void radTemplate_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                string type = radio.Tag as string;

                switch (type)
                {
                    case "Default":
                        txtLogStart.Enabled = false;
                        txtLogEnd.Enabled = false;
                        lblLogStart.Enabled = false;
                        lblLogEnd.Enabled = false;
                        txtPlanWeeks.Enabled = true;
                        txtPlanEnd.Enabled = true;
                        break;

                    case "Logbook":
                        txtLogStart.Enabled = true;
                        txtLogEnd.Enabled = true;
                        lblLogStart.Enabled = true;
                        lblLogEnd.Enabled = true;
                        txtPlanWeeks.Enabled = false;
                        txtPlanEnd.Enabled = false;
                        break;
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (StartDate == DateTime.MinValue)
                return;

            TrainingPlan plan = new TrainingPlan(StartDate, (int)txtPlanWeeks.Value);

            if (radLogbook.Checked)
            {
                Workout workout;

                if (LogStart == DateTime.MinValue || LogEnd == DateTime.MinValue)
                    return;

                // Align old activities to the same day of week
                TimeSpan offset = new TimeSpan((int)(Math.Round((StartDate - LogStart).TotalDays / 7) * 7), 0, 0, 0);

                int count = 0;
                foreach (IActivity activity in PluginMain.GetApplication().Logbook.Activities)
                {
                    DateTime activityDate = activity.StartTime.Add(activity.TimeZoneUtcOffset);
                    if (ChartData.IsInCategory(activity, LogbookSettings.MyActivities) &&
                        LogStart <= activityDate &&
                        activityDate <= LogEnd)
                    {
                        workout = new Workout(new WorkoutDefinition(activity));
                        workout.StartDate = activityDate.Add(offset);
                        workout.TemplateId = Guid.Empty;

                        if (workout.Name == string.Format(Resources.Strings.Action_New, Resources.Strings.Label_Template))
                            workout.Name = workout.StartDate.ToString("ddd", CultureInfo.CurrentCulture) + " " + Resources.Strings.Label_Workout;

                        Phase phase = plan.GetPhase(workout.StartDate);

                        if (phase != null)
                        {
                            phase.AddWorkout(workout);
                            count++;
                        }
                        else
                        { }
                    }
                }
            }
            else if (radDefault.Checked)
            {
                plan = new TrainingPlan(StartDate, (int)txtPlanWeeks.Value);
            }

            plan.Name = txtPlanName.Text;
            this.Plan = plan;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

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

            // Dispose of popup
            calendarPopup.Hide();
            calendarPopup.Dispose();
        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtPlanStart_Leave(object sender, EventArgs e)
        {
            // Set the date to the user entered value
            StartDate = StartDate;
        }

        private void txtPlanEnd_Leave(object sender, EventArgs e)
        {
            // Set the date to the user entered value
            EndDate = EndDate;
        }

        private void txtPlanWeeks_ValueChanged(object sender, EventArgs e)
        {
            // Set the date to the user entered value
            Weeks = Weeks;
        }

        private PlanComponent GetNextValidUpdate(PlanComponent proposed)
        {
            if (proposed == PlanComponent.weeks && StartDate != DateTime.MinValue && EndDate != DateTime.MinValue)
                return PlanComponent.weeks;

            else if (proposed == PlanComponent.end && StartDate != DateTime.MinValue)
                return PlanComponent.end;

            else if (proposed == PlanComponent.start && EndDate != DateTime.MinValue)
                return PlanComponent.start;

            else
                return PlanComponent.weeks;
        }

        private void CalculatePlan(PlanComponent update)
        {
            lastMod.Remove(update);
            lastMod.Insert(0, update);

            switch (GetNextValidUpdate(lastMod[2]))
            {
                case PlanComponent.start:
                    StartDate = EndDate.AddDays((int)txtPlanWeeks.Value * -7);
                    break;

                case PlanComponent.end:
                    EndDate = StartDate.AddDays((int)txtPlanWeeks.Value * 7);
                    break;

                default:
                case PlanComponent.weeks:
                    int weeks = (int)((EndDate - StartDate).TotalDays / 7);
                    if (txtPlanWeeks.Minimum <= weeks && weeks <= txtPlanWeeks.Maximum)
                        Weeks = weeks;
                    break;
            }
        }

        private void txtLogStart_Leave(object sender, EventArgs e)
        {
            LogUpdated(PlanComponent.start);
        }

        private void txtLogEnd_Leave(object sender, EventArgs e)
        {
            LogUpdated(PlanComponent.end);
        }

        private void LogUpdated(PlanComponent type)
        {
            if (LogStart == DateTime.MinValue || LogEnd == DateTime.MinValue)
                return;

            txtPlanEnd.Text = StartDate.Add(LogEnd - LogStart).ToString("d", CultureInfo.CurrentCulture);
            lastMod.Remove(PlanComponent.weeks);
            lastMod.Insert(2, PlanComponent.weeks);
            CalculatePlan(PlanComponent.start);
        }

        private void RaisePropertyChangedEvent(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(null, new PropertyChangedEventArgs(property));
        }
    }
}
