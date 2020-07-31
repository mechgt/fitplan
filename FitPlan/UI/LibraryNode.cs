namespace FitPlan.UI
{
    using System;
    using FitPlan.Data;
    using FitPlan.Schedule;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals;

    public class LibraryNode : TreeList.TreeListNode, IComparable, IComparable<LibraryNode>
    {
        public LibraryNode(LibraryNode parent, IActivityCategory element)
            : base(parent, element)
        { }

        public LibraryNode(LibraryNode parent, WorkoutDefinition element)
            : base(parent, element)
        { }

        public bool IsCategory
        {
            get
            {
                IActivityCategory category = Element as IActivityCategory;
                return category != null;
            }
        }

        public bool IsWorkoutDef
        {
            get { return Workout != null; }
        }

        public bool IsGarminWorkout
        {
            get
            {
                if (IsWorkoutDef)
                {
                    return Workout.IsGarminWorkout;
                }

                return false;
            }
        }

        /// <summary>
        /// Index number of first "category" within this nodes children.  
        /// Used for example for inserting new nodes just prior to the categories to maintain order.
        /// </summary>
        public int FirstCategoryChild
        {
            get
            {
                for (int i = 0; i < Children.Count; i++)
                {
                    LibraryNode child = Children[i] as LibraryNode;

                    if (child.IsCategory)
                    {
                        return i;
                    }
                }

                return Children.Count;
            }
        }

        public IActivityCategory Category
        {
            get
            {
                IActivityCategory category = Element as IActivityCategory;
                if (category != null)
                {
                    return category;
                }
                else if (IsWorkoutDef)
                {
                    return Workout.Category;
                }

                return null;
            }
        }

        public WorkoutDefinition Workout
        {
            get
            {
                WorkoutDefinition value = Element as WorkoutDefinition;
                return value;
            }
        }

        #region IComparable<LibraryNode> Members

        public int CompareTo(object value)
        {
            LibraryNode node = value as LibraryNode;

            if (node != null)
            {
                return this.CompareTo(node);
            }

            return 0;
        }

        public int CompareTo(LibraryNode other)
        {
            // Is 'this' before or after 'other'?
            // int test = "a".CompareTo("b"); // returns -1 --> before
            // int test = "b".CompareTo("a"); // returns 1 --> after
            // int test = "a".CompareTo("a"); // returns 0 --> same/equal

            /* Comparison order:
             * Category 
             *      Garmin Workout
             *      Garmin Workout (sorted by name)
             *      Garmin Workout
             *      Fit Plan Workout
             *      Fit Plan Workout (sorted by name)
             *      Fit Plan Workout
             *      Category (categories are at bottom but are unsorted)
             *          ...
             */

            if (this.IsCategory && other.IsWorkoutDef)
            {
                return 1;
            }
            else if (this.IsCategory && other.IsCategory)
            {
                // Leave categories sorted as-is.
                return 0;
            }
            else if (this.IsWorkoutDef && other.IsWorkoutDef)
                if (!this.IsGarminWorkout && other.IsGarminWorkout)
                    return 1;
                else if (this.IsGarminWorkout && !other.IsGarminWorkout)
                    return -1;
                else
                    return this.Workout.Name.CompareTo(other.Workout.Name);

            else if (this.IsWorkoutDef && other.IsCategory)
                return -1;
            else
                throw new Exception();
        }

        #endregion
    }
}
