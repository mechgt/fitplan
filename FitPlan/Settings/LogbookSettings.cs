namespace FitPlan.Settings
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using FitPlan.Schedule;
    using FitPlan.UI;
    using GarminFitnessPublic;
    using FitPlan.Data;
    using System.ComponentModel;
    using ZoneFiveSoftware.Common.Visuals;
    using System;

    /// <summary>
    /// User settings
    /// </summary>
    [XmlRootAttribute(ElementName = "FitPlan", IsNullable = false)]
    public class LogbookSettings
    {
        #region Fields

        internal static event PropertyChangedEventHandler PropertyChanged;

        internal static bool loaded;

        private static LogbookSettings main;
        private static string currentTrainingPlanPath;
        private static Common.Score scoreType;
        private static TemplateCollection templateDefinitions;
        private static TreeNodeCollection categoryNodes;
        private static float ctlRampHighLim;
        private static float ctlRampLowLim;
        private static bool saveGoogPas;
        private static string calendarId;
        private static string googleUserName;
        private static string googlePasEnc;

        #endregion

        #region Constructors

        public LogbookSettings()
        {
            if (templateDefinitions == null)
            {
                templateDefinitions = new TemplateCollection();
                templateDefinitions.CollectionChanged += new System.ComponentModel.CollectionChangeEventHandler(TemplateDefinitions_CollectionChanged);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the path to the most recently used Training Plan.
        /// </summary>
        public string CurrentTrainingPlan
        {
            get
            {
                if (!File.Exists(currentTrainingPlanPath))
                {
                    currentTrainingPlanPath = string.Empty;
                }

                return currentTrainingPlanPath;
            }
            set
            {
                if (currentTrainingPlanPath != value)
                {
                    currentTrainingPlanPath = value;
                    RaisePropertyChanged("CurrentTrainingPlan");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Common.Score ScoreType
        {
            get { return scoreType; }
            set
            {
                if (scoreType != value)
                {
                    scoreType = value;
                    RaisePropertyChanged("ScoreType");
                }
            }
        }

        /// <summary>
        /// Gets or sets the collection of template definitions.  This defines
        /// both stand-alone workout defs and Garmin Fitness workouts
        /// </summary>
        public TemplateCollection TemplateDefinitions
        {
            get
            {
                return templateDefinitions;
            }
            set
            {
                templateDefinitions = value;
            }
        }

        /// <summary>
        /// Google Calendar UserName
        /// </summary>
        public string GoogleUserName
        {
            get { return googleUserName; }
            set
            {
                if (googleUserName != value)
                {
                    googleUserName = value;
                    RaisePropertyChanged("GoogleUserName");
                }
            }
        }

        /// <summary>
        /// Google Password
        /// </summary>
        [XmlIgnore]
        internal string GooglePas
        {
            get {
                // NOTE: Password saving is disabled for security.
                String key = "notsecure-constant!";
                return Crypto.DecryptStringAES(googlePasEnc, key); 
            }
            set
            {
                // NOTE: Password saving is disabled for security.
                String key = "notsecure-constant!";
                XMLGoogPasEnc = Crypto.EncryptStringAES(value, key);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not to save the Google Password
        /// </summary>
        public bool SaveGoogPas
        {
            get {
                // NOTE: Password saving is disabled for security.
                saveGoogPas = false;
                return saveGoogPas; }
            set
            {
                if (saveGoogPas != value)
                {
                    saveGoogPas = value;
                    RaisePropertyChanged("SaveGoogPas");
                }
            }
        }

        /// <summary>
        /// Used to save/restore from logbook.
        /// </summary>
        public string XMLGoogPasEnc
        {
            get
            {
                if (SaveGoogPas)
                    return googlePasEnc;

                else
                    return string.Empty;
            }
            set
            {
                if (googlePasEnc != value)
                {
                    googlePasEnc = value;
                    RaisePropertyChanged("GooglePas");
                }
            }
        }

        /// <summary>
        /// Calendar Id for internet calendar synchronization
        /// </summary>
        public string CalendarId
        {
            get { return calendarId; }
            set
            {
                if (calendarId != value)
                {
                    calendarId = value;
                    RaisePropertyChanged("CalendarId");
                }
            }
        }

        /// <summary>
        /// Gets or sets CTL High Ramp Rate Limit in points per week.
        /// </summary>
        [XmlIgnore]
        public float CTLRampHighLim
        {
            // TODO: [XmlIgnore] Include CTL Ramp property in saved settings
            get { return Math.Max(ctlRampHighLim, ctlRampLowLim); }
            set { ctlRampHighLim = value; }
        }

        /// <summary>
        /// Gets or sets CTL Low Ramp Rate Limit in points per week.
        /// </summary>
        [XmlIgnore]
        public float CTLRampLowLim
        {
            // TODO: [XmlIgnore] Include CTL Ramp property in saved settings
            get { return Math.Min(ctlRampLowLim, ctlRampHighLim); }
            set { ctlRampLowLim = value; }
        }

        [XmlIgnore]
        public static IActivityCategory MyActivities
        {
            get
            {
                return PluginMain.GetApplication().Logbook.ActivityCategories[0];
            }
        }

        [XmlIgnore]
        internal static LogbookSettings Main
        {
            get
            {
                if (main == null)
                {
                    main = new LogbookSettings();
                }

                return main;
            }
        }

        #endregion

        #region Methods

        internal static void LoadSettings()
        {
            ILogbook logbook = PluginMain.GetApplication().Logbook;

            if (logbook != null && !loaded)
            {
                try
                {
                    // Byte Data
                    byte[] byteArray = logbook.GetExtensionData(GUIDs.PluginMain);

                    if (byteArray.Length > 0)
                    {
                        // Deserialization
                        LogbookSettings settings;
                        XmlSerializer xs = new XmlSerializer(typeof(LogbookSettings));

                        MemoryStream memoryStream = new MemoryStream(byteArray);

                        object deserialize = xs.Deserialize(memoryStream);

                        settings = (LogbookSettings)deserialize;

                        memoryStream.Close();
                        memoryStream.Dispose();
                    }
                    else
                    {
                        // Default settings - required because changing logbooks will not otherwise initialize settings
                        LogbookSettings.Main.CurrentTrainingPlan = string.Empty;
                        LogbookSettings.Main.ScoreType = Common.Score.Trimp;
                    }
                }
                catch
                {
                }
                finally
                {
                    loaded = true;
                }
            }
        }

        internal static void SaveSettings()
        {
            if (!loaded) return;

            string xmlizedString;
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(typeof(LogbookSettings));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            xs.Serialize(xmlTextWriter, LogbookSettings.Main);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            xmlizedString = Utilities.UTF8ByteArrayToString(memoryStream.ToArray());

            // Bytes
            PluginMain.GetApplication().Logbook.SetExtensionData(GUIDs.PluginMain, memoryStream.ToArray());
            PluginMain.GetApplication().Logbook.Modified = true;

            xmlTextWriter.Close();
            memoryStream.Close();
            memoryStream.Dispose();
        }

        /// <summary>
        /// Gets the extended user defined information matching the garmin workout.
        /// Returns null if no extended info has been defined.
        /// </summary>
        /// <param name="garmin"></param>
        /// <returns></returns>
        internal static WorkoutDefinition GetGarminUserData(IPublicWorkout garmin)
        {
            foreach (WorkoutDefinition item in templateDefinitions)
            {
                if (item.GarminId == garmin.Id.ToString("D"))
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Get a specific template from the loaded template collection.  The complete 
        /// template collection is searched, including Garmin Fitness nodes and stand-
        /// alone Fit Plan templates.
        /// </summary>
        /// <param name="id">Template identifier of interest</param>
        /// <returns>the template definition or null if none found</returns>
        internal static WorkoutDefinition GetWorkoutDef(Guid id)
        {
            if (id == null || id == Guid.Empty)
                return null;

            foreach (WorkoutDefinition template in templateDefinitions)
            {
                if (template.Id == id)
                    return template;
            }

            return null;
        }

        internal static TreeNodeCollection GetCategoryNodes()
        {
            categoryNodes = new TreeNodeCollection();

            // Loops through parent categories (My Activities, My Friends Activities)
            // Add Category folders
            foreach (IActivityCategory item in PluginMain.GetApplication().Logbook.ActivityCategories)
            {
                LibraryNode parent = new LibraryNode(null, item);
                categoryNodes.Add(parent);
                AddSubcategories(ref parent, item);
            }

            return categoryNodes;
        }

        private static void AddSubcategories(ref LibraryNode node, IActivityCategory parent)
        {
            foreach (IActivityCategory category in parent.SubCategories)
            {
                LibraryNode child = new LibraryNode(node, category);
                node.Children.Add(child);
                AddSubcategories(ref child, category);
            }
        }

        private static void TemplateDefinitions_CollectionChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
        {
            SaveSettings();

            WorkoutDefinition template = e.Element as WorkoutDefinition;

            if (e.Action == CollectionChangeAction.Add)
            {
                template.PropertyChanged += template_PropertyChanged;
            }
            else if (e.Action == CollectionChangeAction.Remove)
            {
                template.PropertyChanged -= template_PropertyChanged;
            }

        }

        private static void template_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveSettings();
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            SaveSettings();
        }

        #endregion
    }
}
