namespace FitPlan.Actions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using FitPlan.Data;
    using FitPlan.Settings;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Fitness;
    using ZoneFiveSoftware.Common.Visuals.Util;

    internal class ActivityToTemplate : IAction
    {
        #region Fields

        private IView view;

        #endregion

        #region Constructor

        public ActivityToTemplate(IView view)
        {
            this.view = view;
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this is the routes view or not.
        /// </summary>
        private bool IsRouteView
        {
            get { return view is IRouteView; }
        }

        #endregion

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
            get { return FitPlan.Resources.Images.Calendar; }
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
            if (IsRouteView)
            {
                IEnumerable<IRoute> routes = GetRoutes();

                foreach (IRoute route in routes)
                {
                    WorkoutDefinition def = new WorkoutDefinition(route);
                    LogbookSettings.Main.TemplateDefinitions.Add(def);
                }
            }
            else
            {
                IEnumerable<IActivity> activities = Utilities.GetActivities();

                foreach (IActivity activity in activities)
                {
                    WorkoutDefinition def = new WorkoutDefinition(activity);
                    LogbookSettings.Main.TemplateDefinitions.Add(def);
                }
            }
        }

        public string Title
        {
            get { return string.Format(Resources.Strings.Action_CreateTemplate,Resources.Strings.Label_Workout); }
        }

        public bool Visible
        {
            get
            {
                if (IsRouteView)
                {
                    List<IRoute> routes = GetRoutes() as List<IRoute>;

                    if (routes.Count > 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    List<IActivity> activities = Utilities.GetActivities() as List<IActivity>;

                    if (activities.Count > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all routes selected
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<IRoute> GetRoutes()
        {
            IList<IRoute> routes = new List<IRoute>();

            // Prevent null ref error during startup
            if (PluginMain.GetApplication().Logbook == null ||
                PluginMain.GetApplication().ActiveView == null)
            {
                return routes;
            }

            if (view != null && view is IRouteView)
            {
                IRouteView routeView = view as IRouteView;
                routes = CollectionUtils.GetItemsOfType<IRoute>(routeView.SelectionProvider.SelectedItems);
            }

            return routes;
        }

        #endregion
    }
}
