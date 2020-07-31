namespace FitPlan.UI
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using ZoneFiveSoftware.Common.Visuals;

    public class TreeNodeCollection : CollectionBase
    {
        public event CollectionChangeEventHandler CollectionChanged;

        public TreeNodeCollection() { }

        public TreeNodeCollection(TreeNodeCollection nodes)
        {
            TreeNodeCollection clone = nodes.MemberwiseClone() as TreeNodeCollection;

            foreach (TreeList.TreeListNode node in clone)
            {
                this.List.Add(node);
            }
        }

        /// <summary>
        /// Creates an instance of a collection containing a single tree node item.
        /// </summary>
        /// <param name="node"></param>
        internal TreeNodeCollection(TreeList.TreeListNode node)
            : base()
        {
            this.List.Add(node);
        }

        public TreeList.TreeListNode this[int index]
        {
            get { return (TreeList.TreeListNode)this.List[index]; }
            set { this.List[index] = value; }
        }

        public new int Count
        {
            get { return this.List.Count; }
        }

        public void Add(TreeList.TreeListNode node)
        {
            this.List.Add(node);

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, node));
            }
        }

        public void AddRange(TreeNodeCollection list)
        {
            this.InnerList.AddRange(list);

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, list));
            }
        }

        public void Remove(TreeList.TreeListNode node)
        {
            this.List.Remove(node);

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Remove, node));
            }
        }

        public void Insert(int index, TreeList.TreeListNode node)
        {
            this.InnerList.Insert(index, node);

            if (CollectionChanged != null)
            {
                CollectionChanged.Invoke(this, new CollectionChangeEventArgs(CollectionChangeAction.Add, node));
            }
        }

        public bool Contains(object value)
        {
            return this.List.Contains(value);
        }

        public int IndexOf(TreeList.TreeListNode node)
        {
            return List.IndexOf(node);
        }

        protected override void OnValidate(object value)
        {
            base.OnValidate(value);
            if (!(value is TreeList.TreeListNode))
            {
                throw new ArgumentException("Collection only supports TreeList.TreeListNode objects");
            }
        }
    }
}
