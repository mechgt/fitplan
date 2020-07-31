using System;
using System.ComponentModel;
using System.Windows.Forms;
using FitPlan.Schedule;
using Google.GData.Client;
using ZoneFiveSoftware.Common.Visuals;
using FitPlan.Settings;

namespace FitPlan.Calendar
{
    public partial class CalLogin : Form
    {
        TrainingPlan plan;

        public CalLogin(TrainingPlan plan)
        {
            InitializeComponent();

            this.plan = plan;
            this.UserName.Text = Settings.LogbookSettings.Main.GoogleUserName;
            ThemeChanged(PluginMain.GetApplication().VisualTheme);
            this.plan.CalendarProvider.PropertyChanged += new PropertyChangedEventHandler(CalendarProvider_PropertyChanged);
            this.chkPasswordSave.Checked = LogbookSettings.Main.SaveGoogPas;
            this.Password.Text = LogbookSettings.Main.GooglePas;

            RefreshPlanSettings();
        }

        /// <summary>
        /// Refresh details displayed on plan settings tab
        /// </summary>
        private void RefreshPlanSettings()
        {
            GoogleProvider cal = (GoogleProvider)plan.CalendarProvider;

            cboCalendar.Items.Clear();
            cboCalendar.Enabled = cal.IsLoggedIn;
            AtomEntry selected = null;

            if (cal.IsLoggedIn)
            {
                foreach (AtomEntry calendar in cal.Calendars)
                {
                    cboCalendar.Items.Add(calendar.Title.Text);
                    if (cal.GetCalendarId(calendar.Id) == Settings.LogbookSettings.Main.CalendarId)
                    {
                        selected = calendar;
                    }
                }

                if (selected != null)
                {
                    this.cboCalendar.SelectedItem = selected.Title.Text;
                }

                lblPleaseLogin.Text = "Logged in as " + Settings.LogbookSettings.Main.GoogleUserName;
            }
            else
            {
                lblPleaseLogin.Text = "Please Log In";
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Login();
            }
            catch (Exception err)
            {
                MessageDialog.Show(string.Format("Error while logging in: {0}", err.Message), "Google Calendar Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //if (err.InnerException is System.Net.WebException && err.InnerException.Message.Contains("(401)"))
                //{
                //    // Unauthorized
                //}
                //else if (err.Message.Contains("Invalid credentials"))
                //{
                //    // Bad user/password
                //}
                //else if (err.Message.Contains("Execution of request failed"))
                //{
                //    // Happens if a proxy is blocking the request
                //}
            }

            RefreshPlanSettings();
        }

        private void CalendarProvider_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsLoggedIn")
                RefreshPlanSettings();
        }

        private void cboCalendar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty((string)cboCalendar.SelectedItem))
            {
                GoogleProvider cal = (GoogleProvider)plan.CalendarProvider;

                foreach (AtomEntry calendar in cal.Calendars)
                {
                    if (calendar.Title.Text == cboCalendar.SelectedItem)
                    {
                        if (cal != null)
                            Settings.LogbookSettings.Main.CalendarId = cal.GetCalendarId(calendar.Id);

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Login to the calendar provider using the specified username and password
        /// </summary>
        private void Login()
        {
            plan.CalendarProvider.Login(this.UserName.Text, this.Password.Text);
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            // TODO: Apply theme colors to Google Cal Login
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkPasswordSave_CheckedChanged(object sender, EventArgs e)
        {
            LogbookSettings.Main.SaveGoogPas = chkPasswordSave.Checked;
        }
    }
}
