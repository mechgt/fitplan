using System;
using System.Collections.Generic;
using System.Text;

namespace FitPlan.Schedule
{
    public interface IWorkoutBase
    {
        TimeSpan TotalTime { get; set; }
        double TotalDistanceMeters { get; set; }
        float Score { get; set; }
    }
}
