namespace FitPlan.Schedule
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public class PhaseCollection : CollectionBase
    {
        public event CollectionChangeEventHandler CollectionChanged;

        public PhaseCollection() { }

        public Phase this[int index]
        {
            get { return (Phase)this.List[index]; }
            set { this.List[index] = value; }
        }

        public new int Count
        {
            get { return this.List.Count; }
        }

        /// <summary>
        /// Adds a phase to the list.  Phases are sorted afterward to maintain proper order.
        /// </summary>
        /// <param name="phase">New phase to add</param>
        public void Add(Phase phase)
        {
            this.List.Add(phase);
            Sort();

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, phase));
            }
        }

        public void Remove(Phase phase)
        {
            this.List.Remove(phase);

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Remove, phase));
            }
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
        /// Determines the index of a specific phase in the list.
        /// </summary>
        /// <param name="phase">The phase to locate in the list</param>
        /// <returns></returns>
        public int IndexOf(Phase phase)
        {
            return List.IndexOf(phase);
        }

        protected override void OnValidate(object value)
        {
            base.OnValidate(value);
            if (!(value is Phase))
            {
                throw new ArgumentException("Collection only supports Phase objects");
            }
        }
    }
}
