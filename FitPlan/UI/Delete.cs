namespace FitPlan.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using FitPlan.Schedule;
    using ZoneFiveSoftware.Common.Visuals;

    internal class Delete : IAction
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
            get { return CommonResources.Images.Delete16; }
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
            Workout workout = ScheduleView.CurrentControl.SelectedWorkout;
            if (workout != null)
            {
                ScheduleView.CurrentControl.RemoveWorkout(workout);
            }
            else if (ScheduleView.CurrentControl.SelectedPhase != null)
            {
                ScheduleView.CurrentControl.RemovePhase(ScheduleView.CurrentControl.SelectedPhase);
            }
        }

        public string Title
        {
            get { return CommonResources.Text.ActionRemove; }
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
