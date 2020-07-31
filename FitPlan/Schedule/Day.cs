namespace FitPlan.Schedule
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    public class Day : IComparable, IComparable<Day>
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int targetCTL;
        private int days;

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Day()
        {
            days = 0;
            targetCTL = -1;
        }

        public Day(DateTime start, DateTime current)
            : this()
        {
            SetDays(start, current);
        }

        public Day(DateTime start, DateTime current, int targetCTL)
            : this(start, current)
        {
            TargetCTL = targetCTL;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the number of days from the start of the Training Plan.
        /// </summary>
        [XmlAttribute]
        public int Days
        {
            get { return days; }
            set
            {
                days = value;
                RaisePropertyChanged("Days");
            }
        }

        /// <summary>
        /// CTL target value for this day (-1 if not defined)
        /// </summary>
        [XmlAttribute]
        public int TargetCTL
        {
            get { return targetCTL; }
            set
            {
                targetCTL = value;
                RaisePropertyChanged("TargetCTL");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the Days property based on a given start date, and the desired date.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        internal int SetDays(DateTime start, DateTime current)
        {
            Days = (int)(current - start).TotalDays;
            return Days;
        }

        internal DateTime GetDate(DateTime start)
        {
            return start.AddDays(Days);
        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is Day)
                return CompareTo(obj as Day);

            return 0;
        }

        public int CompareTo(Day other)
        {
            return this.Days.CompareTo(other.Days);
        }

        #endregion
    }
}
