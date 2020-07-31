using System.ComponentModel;
using FitPlan.Schedule;

namespace FitPlan.Calendar
{
    /// <summary>
    /// This class is the generic primary interface between Fit Plans 
    /// and various internet calendar providers.
    /// </summary>
    internal abstract class UserCalendar
    {
        internal enum SyncMode
        {
            Update,
            Delete
        }

        internal event PropertyChangedEventHandler PropertyChanged;

        internal TrainingPlan plan;

        protected UserCalendar(TrainingPlan plan)
            : this()
        {
            this.plan = plan;
        }

        protected UserCalendar() { }

        internal virtual bool IsLoggedIn { get; set; }

        internal abstract bool LoginAvailable { get; }

        internal abstract string CalendarName { get; }
        internal abstract string GetCalendarId(object input);

        internal virtual string GetIconUrl(Workout workout)
        {
            return GetIconUrl(workout.ImageName);
        }

        internal virtual string GetIconUrl(string imageName)
        {
            if (string.IsNullOrEmpty(imageName) || imageName == "-")
            {
                // Default Image
                return "http://mechgt.com/st/images/fitplan/FitPlan16.png";
            }
            else
            {
                return string.Format("http://mechgt.com/st/images/fitplan/{0}32.png", imageName);
            }
        }

        internal abstract bool Login(string userName, string passWord);
        internal abstract bool SyncPlan();
        internal abstract bool SyncPlan(bool prompt);
        internal abstract bool DeletePlan();
    }
}
