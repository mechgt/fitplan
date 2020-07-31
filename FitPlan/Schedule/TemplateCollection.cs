namespace FitPlan.Schedule
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using FitPlan.Data;

    public class TemplateCollection : CollectionBase
    {
        public event CollectionChangeEventHandler CollectionChanged;

        public TemplateCollection() { }

        public WorkoutDefinition this[int index]
        {
            get { return (WorkoutDefinition)this.List[index]; }
            set { this.List[index] = value; }
        }

        public new int Count
        {
            get { return this.List.Count; }
        }

        public void Add(WorkoutDefinition template)
        {
            if (!this.List.Contains(template))
            {
                this.List.Add(template);

                if (CollectionChanged != null)
                {
                    CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, template));
                }
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

        public void Remove(WorkoutDefinition template)
        {
            this.List.Remove(template);

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Remove, template));
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

        public int IndexOf(WorkoutDefinition template)
        {
            return List.IndexOf(template);
        }

        protected override void OnValidate(object value)
        {
            base.OnValidate(value);
            if (!(value is WorkoutDefinition))
            {
                throw new ArgumentException("Collection only supports WorkoutDefinition objects");
            }
        }
    }
}
