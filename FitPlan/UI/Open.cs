namespace FitPlan.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using ZoneFiveSoftware.Common.Visuals;

    internal class Open : IAction
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
            get { return CommonResources.Images.Folder16; }
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
            ScheduleView.CurrentControl.OpenPlanDialog();
        }

        public string Title
        {
            get { return CommonResources.Text.ActionOpen; }
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
