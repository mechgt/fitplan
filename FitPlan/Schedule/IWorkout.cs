namespace FitPlan.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using System.Xml.Serialization;

    public interface IWorkout : IWorkoutBase
    {
        string ImageName { get; set; }
        TimeSpan PaceMinPerMeter { get; }
        float MetersPerSecond { get; }
        IActivityCategory Category { get; set; }
        string XMLCategoryName { get; set; }
        string XMLCategoryId { get; set; }
    }
}
