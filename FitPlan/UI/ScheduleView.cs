namespace FitPlan.UI
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ZoneFiveSoftware.Common.Visuals;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Globalization;
    using System.ComponentModel;

    internal class ScheduleView : IView
    {
        #region Fields

        private static ScheduleControl control;
        public static ScheduleView Instance;

        #endregion

        #region Constructor

        public ScheduleView()
        {
            Instance = this;
        }

        #endregion

        #region IView Members

        public IList<IAction> Actions
        {
            get
            {
                return new List<IAction> { 
                    new AddPlan(), 
                    new Open(), 
                    new Save(false), 
                    new Save(true), 
                    new AddPhase(),
                    new InsertPhase(), 
                    new AddWorkout(), 
                    new Delete() };
            }
        }

        public Guid Id
        {
            get { return GUIDs.FitPlanView; }
        }

        public string SubTitle
        {
            get
            {
                if (CurrentControl.SelectedPlan != null)
                {
                    return System.IO.Path.GetFileName(CurrentControl.SelectedPlan.FilePath);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public void SubTitleClicked(Rectangle subTitleRect)
        {

        }

        public bool SubTitleHyperlink
        {
            get { return false; }
        }

        public string TasksHeading
        {
            get { return Resources.Strings.Label_Tasks; }
        }

        #endregion

        #region IDialogPage Members

        public Control CreatePageControl()
        {
            if (control == null)
            {
                control = CurrentControl;
                control.PropertyChanged += new PropertyChangedEventHandler(control_PropertyChanged);
            }

            return control;
        }

        public bool HidePage()
        {
            return true;
        }

        public string PageName
        {
            get
            {
                return Resources.Strings.Label_FitPlan;
            }
        }

        public void ShowPage(string bookmark)
        {
            FitPlan.Data.ChartData.RefreshLibraryNodes();
            if (Settings.GlobalSettings.Main.TreeType == FitPlan.Settings.GlobalSettings.TreeOption.Library)
            {
                control.RefreshTree();
            }
        }

        public IPageStatus Status
        {
            get { return null; }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            CreatePageControl();
            control.ThemeChanged(visualTheme);
        }

        public string Title
        {
            get { return Resources.Strings.Label_FitPlan; }
        }

        public void UICultureChanged(CultureInfo culture)
        {
            CreatePageControl();
            control.UICultureChanged(culture);
        }

        internal static ScheduleControl CurrentControl
        {
            get
            {
                if (control == null)
                {
                    control = new ScheduleControl();
                }

                return control;
            }
        }

        private void control_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TrainingPlan")
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SubTitle"));
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
