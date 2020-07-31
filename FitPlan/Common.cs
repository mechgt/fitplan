namespace FitPlan
{
    using FitPlan.Data;
    using ZoneFiveSoftware.Common.Data.Fitness.CustomData;

    public class Common
    {
        public static ICustomDataFieldDefinition EstVDOTfield
        {
            get
            {
                return CustomDataFields.GetCustomProperty(CustomDataFields.CustomFields.EstimateVDOT, true);
            }
        }

        /// <summary>
        /// Number of days in a default phase to be added AFTER a plan has been created (not the initial setup of a new plan).
        /// </summary>
        public static int DefaultPhaseDays = 7;

        /// <summary>
        /// Score type.
        /// </summary>
        /// <remarks>DO NOT CHANGE ENUMERATION-INT VALUES.  These values are specifically used in Training Load 
        /// for setting the proper score type for getting CTL/ATL/etc. values.</remarks>
        public enum Score
        {
            // 0=Power, 1=HR, 2=Daniels
            TSS = 0,
            Trimp = 1
        }
    }
}
