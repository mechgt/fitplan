namespace FitPlan.Schedule
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class DayCollection : CollectionBase
    {
        public event CollectionChangeEventHandler CollectionChanged;

        public DayCollection()
        {
        }

        public Day this[int index]
        {
            get { return (Day)this.List[index]; }
            set { this.List[index] = value; 

            }
        }

        public new int Count
        {
            get { return this.List.Count; }
        }

        /// <summary>
        /// Adds a day to the list.  Days are sorted afterward to maintain proper order.
        /// </summary>
        /// <param name="Day">New day to add</param>
        public void Add(Day day)
        {
            this.List.Add(day);
            day.PropertyChanged += new PropertyChangedEventHandler(day_PropertyChanged);

            Sort();

            if (CollectionChanged != null)
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, day));
        }

        /// <summary>
        /// Removes a day from the list.
        /// </summary>
        /// <param name="Day">Day to remove</param>
        public void Remove(Day day)
        {
            if (this.List.Contains(day))
            {
                this.List.Remove(day);
            }
        }

        protected override void OnRemove(int index, object value)
        {
            Day day = value as Day;
            day.PropertyChanged -= day_PropertyChanged;
            base.OnRemove(index, value);

            if (CollectionChanged != null)
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Remove, day));
        }

        public void Sort()
        {
            this.InnerList.Sort();
        }

        public bool Contains(object value)
        {
            return this.List.Contains(value);
        }

        /// <summary>
        /// Determines the index of a specific Day in the list.
        /// </summary>
        /// <param name="Day">The Day to locate in the list</param>
        /// <returns></returns>
        public int IndexOf(Day day)
        {
            return List.IndexOf(day);
        }

        protected override void OnValidate(object value)
        {
            base.OnValidate(value);

            if (!(value is Day))
                throw new ArgumentException("Collection only supports Day objects");
        }

        private void day_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (CollectionChanged != null)
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Refresh, sender));
        }
    }
}
