namespace FitPlan
{
    using System.Collections.Generic;
    using FitPlan.Actions;
    using FitPlan.UI;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Fitness;

    internal class Extend : IExtendDailyActivityViewActions, IExtendActivityReportsViewActions, IExtendRouteViewActions, IExtendSettingsPages, IExtendViews
    {
        #region IExtendXXXViewActions Members

        public IList<IAction> GetActions(IDailyActivityView view, ExtendViewActions.Location location)
        {
            switch (location)
            {
                case ExtendViewActions.Location.EditMenu:
                    return new List<IAction> { new ActivityToTemplate(view) };
            }

            return null;
        }

        public IList<IAction> GetActions(IActivityReportsView view, ExtendViewActions.Location location)
        {
            switch (location)
            {
                case ExtendViewActions.Location.EditMenu:
                    return new List<IAction> { new ActivityToTemplate(view) };
            }

            return null;
        }

        public IList<IAction> GetActions(IRouteView view, ExtendViewActions.Location location)
        {
            switch (location)
            {
                case ExtendViewActions.Location.EditMenu:
                    return new List<IAction> { new ActivityToTemplate(view) };
            }

            return null;
        }       
        
        #endregion

        #region IExtendSettingsPages Members

        public IList<ISettingsPage> SettingsPages
        {
            get { return null; }
        }

        #endregion

        #region IExtendViews Members

        public IList<IView> Views
        {
            get
            {
                List<IView> list = new List<IView> { new ScheduleView() };
                return list;
            }
        }

        #endregion

    }
}
