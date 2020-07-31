namespace FitPlan.UI
{
    using FitPlan.Schedule;
    using ZoneFiveSoftware.Common.Visuals;

    public class FitPlanNode : TreeList.TreeListNode
    {
        public FitPlanNode(FitPlanNode parent, Workout element)
            : base(parent, element)
        { }

        public FitPlanNode(FitPlanNode parent, Phase element)
            : base(parent, element)
        { }

        public FitPlanNode(FitPlanNode parent, Phase.PhaseSummary element)
            : base(parent, element)
        { }

        public FitPlanNode(TrainingPlan element)
            : base(null, element)
        { }

        public bool IsPlan
        {
            get { return Element.GetType() == typeof(TrainingPlan); }
        }

        public bool IsPhase
        {
            get
            {
                return Element.GetType() == typeof(Phase);
            }
        }

        public bool IsPhaseSummary
        {
            get
            {
                return Element.GetType() == typeof(Phase.PhaseSummary);
            }
        }

        public bool IsWorkout
        {
            get { return Element.GetType() == typeof(Workout); }
        }

        /// <summary>
        /// Returns true for Phases that contain child summary lines, otherwise false.
        /// </summary>
        public bool HasPhaseSummary
        {
            get
            {
                if (IsPhase)
                {
                    foreach (FitPlanNode child in Children)
                    {
                        if (child.IsPhaseSummary)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }
    }
}
