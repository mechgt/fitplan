using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using System.ComponentModel;
using ZoneFiveSoftware.Common.Data.Fitness;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data;
using Mechgt.Licensing;
using FitPlan.Data;
using System.Drawing;

namespace FitPlan.Actions
{
    class EstimateVDOT : IAction
    {
        #region IAction Members

        public bool Enabled
        {
            get
            {
                // TODO: Determine if this is a running activity
                return true;
            }
        }

        public bool HasMenuArrow
        {
            get { return false; }
        }

        public Image Image
        {
            get
            {
                return CommonResources.Images.Analyze16;
            }
        }

        public IList<string> MenuPath
        {
            get { return null; }
        }

        public void Refresh()
        {
        }

        public void Run(Rectangle rectButton)
        {
            List<IActivity> activities = Utilities.GetActivities() as List<IActivity>;

            foreach (IActivity activity in activities)
            {
                DanielsActivity daniels = new DanielsActivity(activity);
                
            }
        }

        public string Title
        {
            get
            {
                string title = Resources.Strings.Label_EstimateVDOT;
                return title;
            }
        }

        public bool Visible
        {
            get
            {
                List<IActivity> activities = Utilities.GetActivities() as List<IActivity>;

                if (activities.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
