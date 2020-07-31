namespace FitPlan.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using FitPlan.Resources;
    using ZoneFiveSoftware.Common.Visuals;

    internal class AddPhase : IAction
    {
        #region IAction Members

        public bool Enabled
        {
            get { return true; }
        }

        public bool HasMenuArrow
        {
            get { return false; }
        }

        public Image Image
        {
            get
            {
                return ZoneFiveSoftware.Common.Visuals.CommonResources.Images.DocumentAdd16;
            }
        }

        public IList<string> MenuPath
        {
            get { return null; }
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void Run(Rectangle rectButton)
        {
            ScheduleView.CurrentControl.AddPhase(ScheduleView.CurrentControl.SelectedCalendarDate);
        }

        public string Title
        {
            get { return string.Format(Strings.Action_Add, Strings.Label_Phase); }
        }

        public bool Visible
        {
            get { return true; }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
