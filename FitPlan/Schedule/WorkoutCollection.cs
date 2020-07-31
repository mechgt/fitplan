namespace FitPlan.Schedule
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Collections.Generic;

    public class WorkoutCollection : CollectionBase
    {
        public event CollectionChangeEventHandler CollectionChanged;

        public enum CompareType
        {
            StartDate,
            Distance,
            Time,
            Score,
            ActualDistance,
            ActualTime,
            Category
        }

        private WorkoutComparer comparer;

        public WorkoutCollection()
        {
            comparer = new WorkoutComparer(CompareType.StartDate, false);
        }

        public Workout this[int index]
        {
            get { return (Workout)this.List[index]; }
            set { this.List[index] = value; }
        }

        public DateTime MinDate
        {
            get
            {
                DateTime date = DateTime.MaxValue;
                foreach (Workout item in this)
                {
                    if (item.StartDate.Date < date)
                        date = item.StartDate.Date;
                }

                return date;
            }
        }

        public WorkoutComparer Comparer
        {
            get { return comparer; }
            set { comparer = value; }
        }

        /// <summary>
        /// Search this collection for a parent workout definition
        /// </summary>
        /// <param name="child"></param>
        /// <returns></returns>
        public Workout GetParent(Workout child)
        {
            if (string.IsNullOrEmpty(child.ParentId))
            {
                return null;
            }

            foreach (Workout workout in InnerList)
            {
                if (workout.ReferenceId == child.ParentId)
                {
                    return workout;
                }
            }

            return null;
        }

        new public int Count
        {
            get { return this.List.Count; }
        }

        public void Add(Workout workout)
        {
            this.List.Add(workout);

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, workout));
            }
        }

        public void AddRange(WorkoutCollection list)
        {
            this.InnerList.AddRange(list);

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, list));
            }
        }

        public void Remove(Workout workout)
        {
            this.List.Remove(workout);

            if (CollectionChanged != null)
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Remove, workout));
        }

        public void Sort()
        {
            this.Sort(this.comparer);
        }

        public void Sort(WorkoutComparer comparer)
        {
            this.InnerList.Sort(comparer);
        }

        public void Sort(CompareType sortType, bool ascending)
        {
            // Update compare type
            this.comparer.sortType = sortType;
            this.comparer.ascending = ascending;
            
            // Sort list
            this.Sort();
        }

        public bool Contains(object value)
        {
            return this.List.Contains(value);
        }

        public int IndexOf(Workout workout)
        {
            return List.IndexOf(workout);
        }

        protected override void OnValidate(object value)
        {
            base.OnValidate(value);
            if (!(value is Workout))
            {
                throw new ArgumentException("Collection only supports Workout objects");
            }
        }
    }

    public class WorkoutComparer : IComparer, IComparer<Workout>
    {
        internal WorkoutCollection.CompareType sortType = WorkoutCollection.CompareType.StartDate;
        internal bool ascending = false;

        public WorkoutComparer(WorkoutCollection.CompareType sortType, bool ascending)
        {
            this.sortType = sortType;
            this.ascending = ascending;
        }

        #region IComparer<Workout> Members

        public int Compare(Workout x, Workout y)
        {
            return x.CompareTo(y, sortType);
        }

        #endregion

        #region IComparer Members

        public int Compare(object x, object y)
        {
            Workout x1 = x as Workout;
            Workout y1 = y as Workout;
            int result = 0;

            if (x1 != null && y1 != null)
                result = Compare(x1, y1);

            if (ascending)
                result = result * -1;

            return result;
        }

        #endregion
    }


}
