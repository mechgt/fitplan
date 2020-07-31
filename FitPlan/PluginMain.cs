namespace FitPlan
{
    using System.ComponentModel;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using FitPlan.Settings;
    using ZoneFiveSoftware.Common.Data.Measurement;
    using ZoneFiveSoftware.Common.Visuals.Fitness;

    internal class PluginMain : IPlugin
    {
        #region IPlugin Members

        public IApplication Application
        {
            set
            {
                if (m_App != null)
                {
                    m_App.PropertyChanged -= new PropertyChangedEventHandler(AppPropertyChanged);
                }

                m_App = value;

                if (m_App != null)
                {
                    m_App.PropertyChanged += new PropertyChangedEventHandler(AppPropertyChanged);
                }
            }
        }

        public System.Guid Id
        {
            get { return GUIDs.PluginMain; }
        }

        public string Name
        {
            get
            {
                return Resources.Strings.Label_FitPlan;
            }
        }

        public string Version
        {
            get { return GetType().Assembly.GetName().Version.ToString(4); }
        }

        public void ReadOptions(XmlDocument xmlDoc, XmlNamespaceManager nsmgr, XmlElement pluginNode)
        {
            try
            {
                // Deserialization
                GlobalSettings settings;
                XmlSerializer xs = new XmlSerializer(typeof(GlobalSettings));
                MemoryStream memoryStream = new MemoryStream(Utilities.StringToUTF8ByteArray(pluginNode.InnerText));
                object deserialize = xs.Deserialize(memoryStream);

                settings = (GlobalSettings)deserialize;
                memoryStream.Close();
                memoryStream.Dispose();
            }
            catch
            { 
            }
        }

        public void WriteOptions(XmlDocument xmlDoc, XmlElement pluginNode)
        {
            // Serialization
            string xmlizedString;
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(typeof(GlobalSettings));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            xs.Serialize(xmlTextWriter, GlobalSettings.Main);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            xmlizedString = Utilities.UTF8ByteArrayToString(memoryStream.ToArray());

            pluginNode.InnerText = xmlizedString;

            xmlTextWriter.Close();
            memoryStream.Close();
            memoryStream.Dispose();
        }

        #endregion

        public static event PropertyChangedEventHandler LogbookChanged;

        /// <summary>
        /// Plugin folder - plugins\data\{Name}\
        /// </summary>
        internal static string DataFolder
        {
            get
            {
                // NOTE: File storage location
                string path = Path.Combine(m_App.Configuration.CommonPluginsDataFolder, "FitPlan");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        internal static string LogbookFolder
        {
            get
            {
                // NOTE: File storage location
                if (m_App.Logbook == null) return string.Empty;

                string path = Path.GetDirectoryName(m_App.Logbook.FileLocation);
                return path;
            }
        }

        public static IApplication GetApplication()
        {
            return m_App;
        }

        private void AppPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Logbook")
            {
                if (LogbookChanged != null)
                {
                    LogbookChanged.Invoke(m_App, new PropertyChangedEventArgs("Logbook"));
                }
            }
        }

        private static IApplication m_App = null;

        /// <summary>
        /// Gets the file extension filter for open and save dialog boxes
        /// </summary>
        [XmlIgnore]
        internal static string PlanFileFilter
        {
            get
            {
                return "Fit Plan files (" + PlanFileExtension + ")|*" + PlanFileExtension + "|XML files (.xml)|*.xml|All files (*.*)|*.*";
            }
        }

        /// <summary>
        /// Gets the default file extension
        /// </summary>
        [XmlIgnore]
        internal static string PlanFileExtension
        {
            get
            {
                return ".fpf";
            }
        }

        #region Commonly Accessed Application/Logbook Properties

        /// <summary>
        /// Returns user defined distance units
        /// </summary>
        internal static Length.Units DistanceUnits
        {
            get
            {
                return m_App.SystemPreferences.DistanceUnits;
            }
        }

        #endregion
    }
}
