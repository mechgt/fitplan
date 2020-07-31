using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using FitPlan.Schedule;
using Google.GData.Calendar;
using Google.GData.Client;
using Google.GData.Extensions;
using ZoneFiveSoftware.Common.Visuals;

namespace FitPlan.Calendar
{
    class GoogleProvider : UserCalendar
    {
        internal event PropertyChangedEventHandler PropertyChanged;
        internal event EventHandler Syncing;
        internal event EventHandler Synced;

        private AtomEntryCollection calendars;
        private bool isLoggedIn;
        private string id;
        private int update, add, delete, skip;
        private AtomFeed batchQueue;
        private EventFeed readFeed;

        private CalendarService service = new CalendarService("FitPlan");

        private const string calKeyFitId = "FitId";
        private const string calKeyPlanId = "PlanId";
        private const string calKeyPluginId = "PluginId";

        private enum EntryType
        {
            Main,
            Icon,
            Any
        }

        internal GoogleProvider(TrainingPlan plan)
            : base(plan)
        {
        }

        #region Properties

        /// <summary>
        /// Gets or sets the cached Google Calendar list for the currently logged in Google user.
        /// </summary>
        internal AtomEntryCollection Calendars
        {
            get { return calendars; }
            set
            {
                if (calendars != value)
                {
                    calendars = value;
                    RaisePropertyChanged("Calendars");
                }
            }
        }

        /// <summary>
        /// Gets a value indicating if the user is currently logged in
        /// </summary>
        internal override bool IsLoggedIn
        {
            get { return isLoggedIn; }
            //set { isLoggedIn = value; }
        }

        internal override bool LoginAvailable
        {
            get
            {
                return IsLoggedIn ||
                    (Settings.LogbookSettings.Main.SaveGoogPas &&
                     !string.IsNullOrEmpty(Settings.LogbookSettings.Main.XMLGoogPasEnc) &&
                     !string.IsNullOrEmpty(Settings.LogbookSettings.Main.CalendarId));
            }
        }

        /// <summary>
        /// User-Friendly name of currently selected Calendar
        /// </summary>
        internal override string CalendarName
        {
            get
            {
                if (this.Calendars != null)
                {
                    foreach (AtomEntry calendar in this.Calendars)
                    {
                        if (plan.CalendarProvider.GetCalendarId(calendar.Id) == Settings.LogbookSettings.Main.CalendarId)
                        {
                            return calendar.Title.Text;
                        }
                    }
                }

                return string.Empty;
            }
        }

        #endregion

        #region Methods

        internal bool Login()
        {
            return Login(Settings.LogbookSettings.Main.GoogleUserName, Settings.LogbookSettings.Main.GooglePas);
        }

        /// <summary>
        /// Login to Google Calendar service.  If login fails, an exception will be thrown.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns>Returns true if Login was successful, or false if login failed</returns>
        internal override bool Login(string userName, string passWord)
        {
            if (userName != null && userName.Length > 0)
            {
                service.setUserCredentials(userName, passWord);
            }

            // Login to Google
            CalendarQuery query = new CalendarQuery();
            query.Uri = new Uri("https://www.google.com/calendar/feeds/default/owncalendars/full");

            CalendarFeed resultFeed = (CalendarFeed)service.Query(query);
            this.isLoggedIn = true;

            // Store calendars in Global Settings
            this.Calendars = resultFeed.Entries;

            // Store current login info
            Settings.LogbookSettings.Main.GoogleUserName = userName;
            Settings.LogbookSettings.Main.GooglePas = passWord;

            RaisePropertyChanged("IsLoggedIn");
            return true;
        }

        /// <summary>
        /// Update content for an existing entry
        /// </summary>
        /// <param name="workout"></param>
        /// <param name="existing"></param>
        /// <returns></returns>
        private EventEntry GetEventEntry(Workout workout, EventEntry existing, EntryType type)
        {
            EventEntry workoutEntry = GetEventEntry(workout, type);
            existing.Content = workoutEntry.Content;
            existing.Times.Clear();
            existing.Times.Add(workoutEntry.Times[0]);
            existing.Title = workoutEntry.Title;

            if (workoutEntry.WebContentLink != null)
            {
                existing.WebContentLink = workoutEntry.WebContentLink;
            }

            return existing;
        }

        /// <summary>
        /// Creates a new entry
        /// </summary>
        /// <param name="workout"></param>
        /// <returns></returns>
        private EventEntry GetEventEntry(Workout workout, EntryType type)
        {
            // Compiling generic info
            string description;
            object[] args = { workout.GetFormattedText("Notes"), workout.GetFormattedText("TotalTime"), workout.GetFormattedText("TotalDistanceMeters"), workout.GetFormattedText("Score") };

            if (workout.Notes != null && workout.Notes != string.Empty)
            {
                description = string.Format("{0}\n\nTime: {1}\nDistance: {2}\nScore: {3}", args);
            }
            else
            {
                description = string.Format("Time: {1}\nDistance: {2}\nScore: {3}", args);
            }

            ExtendedProperty pluginId = new ExtendedProperty(GUIDs.PluginMain.ToString("D"), calKeyPluginId);
            ExtendedProperty planId = new ExtendedProperty(plan.ReferenceId, calKeyPlanId);

            EventEntry entry = new EventEntry(workout.Name, description, workout.Category.Name);
            entry.Times.Add(new When(workout.StartDate, workout.StartDate, true));

            entry.ExtensionElements.Add(pluginId);
            entry.ExtensionElements.Add(planId);

            AtomPerson author = new AtomPerson(AtomPersonType.Author, "FitPlan");
            author.Email = Settings.LogbookSettings.Main.GoogleUserName;
            entry.Authors.Add(author);

            switch (type)
            {
                case EntryType.Main:
                    {
                        ExtendedProperty refId = new ExtendedProperty(workout.ReferenceId, calKeyFitId);
                        entry.ExtensionElements.Add(refId);

                        return entry;
                    }
                case EntryType.Icon:
                    {
                        if (workout.ImageName != "-")
                        {
                            ExtendedProperty refId = new ExtendedProperty(string.Format("{0}-i", workout.ReferenceId), calKeyFitId); // icon id is same as main entry with a '-i' appended to it
                            entry.ExtensionElements.Add(refId);

                            WebContentLink wc = new WebContentLink();
                            wc.Title = workout.Name;
                            wc.Type = "image/png";
                            wc.Icon = GetIconUrl(workout);
                            //wc.Url = "http://mechgt.com/st/images/logo.png";
                            //wc.width, height, type of page?... other stuff? (if adding a nice popup window)
                            entry.WebContentLink = wc;

                            return entry;
                        }
                        else
                        {
                            return null;
                        }
                    }
            }

            throw new ArgumentException("Must specify either Main or Icon Entry type.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        internal override bool SyncPlan()
        {
            return SyncPlan(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plan"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        internal override bool SyncPlan(bool prompt)
        {
            RaiseEvent(Syncing);

            // 1. Read Google Cal
            List<AtomEntry> events = ReadCalendar(plan);

            if (!this.IsLoggedIn || events == null || readFeed == null)
                return false;

            WorkoutCollection workouts = plan.GetWorkouts();
            update = 0; add = 0; delete = 0;
            batchQueue = new AtomFeed(readFeed);

            // 2. Compare cal to plan (look for items to update or add)
            // Loop through workouts to ensure they're all up to date
            foreach (Workout workout in workouts)
            {
                if (plan.IsCalendarTextSync)
                    SyncUpdateEntry(workout, events, EntryType.Main);

                if (plan.IsCalendarIconSync)
                    SyncUpdateEntry(workout, events, EntryType.Icon);
            }

            // Purge deleted / extraneous workouts
            foreach (EventEntry entry in events)
            {
                // All events are necessarly related to this plan (ReadCalendar(plan) above); all entries not currently associated with a workout can be deleted
                if (!batchQueue.Entries.Contains(entry)) // Skip any entries that are already scheduled for add/update
                {
                    string id = this.GetFitId(entry);
                    EntryType typ = EntryType.Main;
                    if (2 < id.Length && id.Substring(id.Length - 2, 2) == "-i")
                    {
                        // Icon entry (denoted by the '-i' suffix)
                        id = id.Substring(0, id.Length - 2);
                        typ = EntryType.Icon;
                    }

                    // Remove purged workouts and entries that are not configured for display
                    if (plan.GetWorkout(id) == null ||
                        (typ == EntryType.Main && !plan.IsCalendarTextSync) ||
                        (typ == EntryType.Icon && !plan.IsCalendarIconSync))
                    {
                        // Delete:  Queue event for update
                        entry.BatchData = new GDataBatchEntryData(id, GDataBatchOperationType.delete);
                        delete++;
                        batchQueue.Entries.Add(entry);
                    }
                }
            }

            // 3. Sync:  Add, Update
            // TODO: Delete extra calendar items during sync?
            if (update + add + delete <= 0) return true; // Nothing to do

            if (prompt)
            {
                DialogResult result = ZoneFiveSoftware.Common.Visuals.MessageDialog.Show(string.Format("Fit Plan is about to update {0} events, add {1} events, and delete {2} events.  Continue?", update, add, delete), "Google Calendar Sync", System.Windows.Forms.MessageBoxButtons.YesNoCancel);
                if (result != System.Windows.Forms.DialogResult.Yes)
                    return false;
            }

            return ProcessQueue();
        }

        private void SyncUpdateEntry(Workout workout, List<AtomEntry> events, EntryType type)
        {
            // MAIN/ICON entry: Add/Update
            int index = IndexOf(workout, events, type);
            string id = workout.ReferenceId;

            if (type == EntryType.Icon) id += "-i";

            if (0 <= index)
            {
                EventEntry entry = (EventEntry)events[index];
                if (!IsUpdated(entry, workout, type))
                {
                    // Updates:  Queue event for update
                    EventEntry workoutEntry = GetEventEntry(workout, entry, type);


                    entry.BatchData = new GDataBatchEntryData(id, GDataBatchOperationType.update);
                    update++;
                    batchQueue.Entries.Add(entry);
                }
            }
            else
            {
                // Add:  Add new event entry
                EventEntry entry = GetEventEntry(workout, type);
                if (entry != null)
                {
                    entry.BatchData = new GDataBatchEntryData(id, GDataBatchOperationType.insert);
                    add++;
                    batchQueue.Entries.Add(entry);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        internal override bool DeletePlan()
        {
            // TODO: Rework delete plan simply query for the events with pluginId or planId and delete all of those events.
            delete = 0; skip = 0;
            DialogResult result = DialogResult.Retry;

            // 1. Read Google Cal
            List<AtomEntry> events = ReadCalendar();

            if (!this.IsLoggedIn || events == null || readFeed == null)
                return false;

            batchQueue = new AtomFeed(readFeed);

            // 2. Compare cal to plan
            // Loop through workouts to ensure they're all up to date
            foreach (EventEntry entry in events)
            {
                if (IsFitPlanObject(entry, plan))
                {
                    // Delete:  Entry is for this plan; Queue event for removal
                    entry.BatchData = new GDataBatchEntryData(GetFitId(entry), GDataBatchOperationType.delete);
                    //entry.BatchData = new GDataBatchEntryData(delete.ToString(), GDataBatchOperationType.delete);
                    if (!batchQueue.Entries.Contains(entry))
                    {
                        batchQueue.Entries.Add(entry);
                        delete++;
                    }
                    else
                    {
                        skip++;
                    }
                }
                else if (this.IsFitPlanObject(entry))
                {
                    if (result == System.Windows.Forms.DialogResult.Retry)
                    {
                        // Prompt once if other workouts are found.
                        result = MessageDialog.Show("Workouts that aren't in the current plan were found on this calendar.  Do you want to remove ALL Fit Plan events from the calendar?", "Google Calendar Sync", System.Windows.Forms.MessageBoxButtons.YesNoCancel);
                    }

                    if (result == DialogResult.Yes)
                    {
                        // Delete ALL FitPlan events found
                        entry.BatchData = new GDataBatchEntryData(this.GetFitId(entry), GDataBatchOperationType.delete);
                        batchQueue.Entries.Add(entry);
                        delete++;
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        // Cancel pressed.  Do nothing.
                        return false;
                    }
                }
            }

            // 3. Process Queue
            if (delete <= 0) return true; // Nothing to do

            result = ZoneFiveSoftware.Common.Visuals.MessageDialog.Show(string.Format("Fit Plan is about to delete {0} events (skipped {1}).  Continue?", delete, skip), "Google Calendar Sync", System.Windows.Forms.MessageBoxButtons.YesNoCancel);
            if (result != DialogResult.Yes)
                return false;

            return ProcessQueue();
        }

        private bool ProcessQueue()
        {
            EventFeed batchResultFeed = (EventFeed)service.Batch(batchQueue, new Uri(batchQueue.Batch));

            // 4. Return true/false to show success/failure
            bool success = true;
            foreach (EventEntry entry in batchResultFeed.Entries)
            {
                if (entry.BatchData.Status.Code != 200 && entry.BatchData.Status.Code != 201)
                {
                    success = false;
                    //Console.WriteLine("The batch operation with ID " + entry.BatchData.Id + " failed.");
                }
            }

            return success;
        }

        /// <summary>
        /// Convert calendar identification object to unique 
        /// string used to identify the calendar
        /// </summary>
        /// <param name="input">Google Calendar.Id object</param>
        /// <returns></returns>
        internal override string GetCalendarId(object input)
        {
            AtomId googId = input as AtomId;
            if (googId != null)
            {
                int index = googId.AbsoluteUri.LastIndexOf("/") + 1;

                if (0 < index)
                {
                    string id = googId.AbsoluteUri.Substring(index);
                    return id;
                }
            }

            // Bad/empty input
            return string.Empty;
        }

        private int IndexOf(EventEntry entry, WorkoutCollection workouts)
        {
            EntryType type;

            if (entry.WebContentLink != null && !string.IsNullOrEmpty(entry.WebContentLink.Icon))
            {
                type = EntryType.Icon;
            }
            else
            {
                type = EntryType.Main;
            }

            for (int i = 0; i < workouts.Count; i++)
            {
                if (IsFitPlanObject(entry, workouts[i], type))
                    return i;
            }

            return -1;
        }

        private int IndexOf(Workout workout, List<AtomEntry> entries, EntryType type)
        {
            for (int i = 0; i < entries.Count; i++)
            {
                if (IsFitPlanObject((EventEntry)entries[i], workout, type))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Determines if this calendar entry is paired with the given workout.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="workout"></param>
        /// <returns>True if these are paired together, or false otherwise.  This does 
        /// not evaluate if the entry is up to date (differences may exist).</returns>
        /// <summary>
        /// Determines if an entry and workout are synchronized, or if changes exist
        /// that require an update.
        /// </summary>
        /// <param name="entry">A Google Calendar event entry</param>
        /// <param name="workout">A workout to be compared to the entry</param>
        /// <returns>True if items match, or false if updates are required.</returns>
        private bool IsUpdated(EventEntry entry, Workout workout, EntryType type)
        {
            EventEntry current = GetEventEntry(workout, type);

            // Universal qualifiers (both main and icon type entries)
            if (entry == null || workout == null)
                return false;

            else if (entry.Title.Text != workout.Name)
                return false;

            else if (entry.Times[0].StartTime.Date != workout.StartDate)
                return false;

            if (type == EntryType.Main)
            {
                // Compare description (use the following to ignore whitespace)
                string normalized1 = System.Text.RegularExpressions.Regex.Replace(entry.Content.Content, @"\s", "");
                string normalized2 = System.Text.RegularExpressions.Regex.Replace(current.Content.Content, @"\s", "");

                if (!normalized1.Equals(normalized2))
                    return false;
            }
            else if (type == EntryType.Icon)
            {
                // Compare description (use the following to ignore whitespace)
                string normalized1 = System.Text.RegularExpressions.Regex.Replace(entry.Content.Content, @"\s", "");
                string normalized2 = System.Text.RegularExpressions.Regex.Replace(current.Content.Content, @"\s", "");

                if (!normalized1.Equals(normalized2))
                    return false;

                // Compare images
                if (entry.WebContentLink == null && workout.ImageName != "-")
                    return false; // default icon has been updated

                else if (entry.WebContentLink != null && entry.WebContentLink.Icon != GetIconUrl(workout))
                    return false; // defined icon is different
            }

            return true;
        }

        /// <summary>
        /// Returns a value indicating if this calendar entry is related to the Fit Plan plugin (any plan)
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        private bool IsFitPlanObject(EventEntry entry)
        {
            return IsEntryKeyPair(entry, calKeyPluginId, GUIDs.PluginMain.ToString("D"));
        }

        /// <summary>
        /// Returs a value indicating if this calendar entry matches this workout
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="workout"></param>
        /// <returns></returns>
        private bool IsFitPlanObject(EventEntry entry, Workout workout, EntryType type)
        {
            bool result = false;

            if (type == EntryType.Main || type == EntryType.Any)
            {
                result = result || IsEntryKeyPair(entry, calKeyFitId, workout.ReferenceId);
            }

            if (type == EntryType.Icon || type == EntryType.Any)
            {
                result = result || IsEntryKeyPair(entry, calKeyFitId, string.Format("{0}-i", workout.ReferenceId));
            }

            return result;
        }

        /// <summary>
        /// Returns a value indicating if this calendar entry is related to this plan
        /// </summary>
        /// <param name="plan"></param>
        /// <param name="entry"></param>
        /// <returns></returns>
        private bool IsFitPlanObject(EventEntry entry, TrainingPlan plan)
        {
            return IsEntryKeyPair(entry, calKeyPlanId, plan.ReferenceId);
        }

        /// <summary>
        /// Utility routine for validating extension settings.
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsEntryKeyPair(EventEntry entry, string name, string value)
        {
            foreach (IExtensionElementFactory item in entry.ExtensionElements)
            {
                ExtendedProperty ext = item as ExtendedProperty;
                if (ext != null)
                {
                    if (ext.Name == name)
                    {
                        if (ext.Value == value)
                            return true; // Match found
                        else
                            return false; // Attribute found, wrong value
                    }
                }
            }

            return false; // Attribute does not exist
        }

        private EntryType GetEntryType(EventEntry entry)
        {
            // TODO: (LOW) Figure out how to identify evententry type.  REQUIRED. Maybe check Id to see what kind of entry this is?
            // Due to not knowing exactly how to determine this easily, this method is no longer used
            // This code is completely untested
            if (entry.WebContentLink != null && !string.IsNullOrEmpty(entry.WebContentLink.Icon))
            {
                return EntryType.Icon;
            }
            else
            {
                return EntryType.Main;
            }
        }

        private string GetFitId(EventEntry entry)
        {
            // TODO: Change method name
            foreach (IExtensionElementFactory item in entry.ExtensionElements)
            {
                ExtendedProperty ext = item as ExtendedProperty;
                if (ext != null)
                {
                    if (ext.Name == calKeyFitId)
                        return ext.Value;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get all events for a parrticular Fit Plan
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private List<AtomEntry> ReadCalendar(TrainingPlan plan)
        {
            EventQuery query = new EventQuery();
            query.ExtraParameters = string.Format("extq=[{0}:{1}]", calKeyPlanId, plan.ReferenceId);
            query.Uri = GetCalendarUri();

            return ReadCalendar(query);
        }

        /// <summary>
        /// Get all events to the Fit Plan plugin
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private List<AtomEntry> ReadCalendar()
        {
            EventQuery query = new EventQuery();
            query.ExtraParameters = string.Format("extq=[{0}:{1}]", calKeyPluginId, GUIDs.PluginMain);
            query.Uri = GetCalendarUri();

            return ReadCalendar(query);
        }

        /// <summary>
        /// Get all events within a particular date range
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private List<AtomEntry> ReadCalendar(DateTime start, DateTime end)
        {
            EventQuery query = new EventQuery();
            query.Uri = GetCalendarUri();
            query.StartTime = start.AddDays(-1);
            query.EndTime = end.AddDays(1);

            return ReadCalendar(query);
        }

        /// <summary>
        /// Get all events within a particular date range
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private List<AtomEntry> ReadCalendar(EventQuery query)
        {
            try
            {
                if (!IsLoggedIn && !Login())
                    return null;
            }
            catch (Exception err)
            {
                //MessageDialog.Show(string.Format("Error while logging in: {0}", err.Message), "Google Calendar Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            try
            {
                // Initiate web request
                // TODO: (LOW) Check to see if setting max events to retrieve makes the queries any faster/better.  If kept, move it to more appropriate place (where query was generated)
                //query.NumberToRetrieve = 100; 
                readFeed = service.Query(query);
            }
            catch
            {
                // Bad things can happen during the internet read request
                return null;
            }

            List<AtomEntry> entries = new List<AtomEntry>();

            foreach (AtomEntry item in readFeed.Entries)
            {
                if (!entries.Contains(item))
                    entries.Add(item);
                else
                { }
            }

            while (readFeed.NextChunk != null)
            {
                query.Uri = new Uri(readFeed.NextChunk);

                try
                {
                    readFeed = service.Query(query);
                }
                catch
                {
                    // Error happened during internet comms.
                    return null;
                }

                foreach (AtomEntry item in readFeed.Entries)
                    entries.Add(item);
            }

            return entries;
        }

        private Uri GetCalendarUri()
        {
            id = Settings.LogbookSettings.Main.CalendarId;

            return new Uri(string.Format("http://www.google.com/calendar/feeds/{0}/private/full", id));
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(null, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void RaiseEvent(EventHandler handler)
        {
            if (handler != null)
            {
                handler.Invoke(this, new EventArgs());
            }
        }

        #endregion
    }
}
