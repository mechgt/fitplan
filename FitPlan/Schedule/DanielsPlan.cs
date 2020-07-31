namespace FitPlan.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class DanielsPlan : TrainingPlan
    {
        private static Dictionary<Intensity, float> dictionary;
        private static SortedList<int, int> phaseDef;

        public enum Intensity
        {
            Recovery,
            Easy1,
            Easy2,
            Easy3,
            ModAero,
            HiAero,
            Marathon,
            HalfMarathon,
            _15k,
            Threshold,
            _12k,
            _10k,
            _8k,
            _5k,
            Interval,
            _3k,
            Repetition,
            Mile,
        }

        public DanielsPlan() { }
        public DanielsPlan(DateTime start, int totalDays, float avgMetersPerWeek)
        {
            // Default will create a Daniels Plan
            Phase phase1 = new Phase("1-Base", start, DanielsPlan.GetPhaseDays(1, totalDays), Color.Orange);

            List<Workout> workouts = new List<Workout>();
            for (int i = 0; i < 7; i++)
            {
                workouts.Add(new Workout(start.AddDays(i), start.AddDays(i + 21), 7, avgMetersPerWeek / 7, 2, .05f, "Easy"));
            }
            
            Phase phase2 = new Phase("2-Build", phase1.EndDate, DanielsPlan.GetPhaseDays(2, totalDays), Color.Chartreuse);
            Phase phase3 = new Phase("3-Training", phase2.EndDate, DanielsPlan.GetPhaseDays(3, totalDays), Color.Olive);
            Phase phase4 = new Phase("4-Final", phase3.EndDate, DanielsPlan.GetPhaseDays(4, totalDays), Color.RosyBrown);

            Phases = new PhaseCollection { phase1, phase2, phase3, phase4 };
        }

        /// <summary>
        /// Returns a priority definition list in the format of (week, phase).
        /// This is helpful for training plans that do not have the full time allowed for 
        /// the entire plan.  It'll help prioritize which phases should be used given
        /// limited training plan time.
        /// EXAMPLE:  Given 10 weeks for a plan, the first 10 entries will be allowed, giving
        /// 3 weeks phase 1, 1 week phase 2, 3 weeks phase 3, 3 weeks phase 4
        /// </summary>
        private static SortedList<int, int> DanielsPhaseDef
        {
            get
            {
                if (phaseDef == null)
                {
                    phaseDef = new SortedList<int, int>();
                    phaseDef.Add(1, 1);
                    phaseDef.Add(2, 1);
                    phaseDef.Add(3, 1);
                    phaseDef.Add(4, 4);
                    phaseDef.Add(5, 4);
                    phaseDef.Add(6, 4);
                    phaseDef.Add(7, 3);
                    phaseDef.Add(8, 3);
                    phaseDef.Add(9, 3);
                    phaseDef.Add(10, 2);
                    phaseDef.Add(11, 2);
                    phaseDef.Add(12, 2);
                    phaseDef.Add(13, 1);
                    phaseDef.Add(14, 3);
                    phaseDef.Add(15, 3);
                    phaseDef.Add(16, 3);
                    phaseDef.Add(17, 4);
                    phaseDef.Add(18, 2);
                    phaseDef.Add(19, 2);
                    phaseDef.Add(20, 2);
                    phaseDef.Add(21, 1);
                    phaseDef.Add(22, 4);
                    phaseDef.Add(23, 1);
                    phaseDef.Add(24, 4);
                }

                return phaseDef;
            }
        }

        private static Dictionary<Intensity, float> DistanceTable
        {
            get
            {
                if (dictionary == null)
                {
                    dictionary = new Dictionary<Intensity, float>();
                    dictionary.Add(Intensity._10k, 10000);
                    dictionary.Add(Intensity._12k, 12000);
                    dictionary.Add(Intensity._15k, 15000);
                    dictionary.Add(Intensity._3k, 3000);
                    dictionary.Add(Intensity._5k, 5000);
                    dictionary.Add(Intensity._8k, 8000);
                    dictionary.Add(Intensity.HalfMarathon, 21097.5f);
                    dictionary.Add(Intensity.Marathon, 42195f);
                    dictionary.Add(Intensity.Mile, 1069.3f);
                }

                return dictionary;
            }
        }

        /// <summary>
        /// Gets the number of planned weeks for a phase (1-4) according to Jack Daniels.
        /// </summary>
        /// <param name="phase">A number 1-4</param>
        /// <param name="totalWeeks">Total weeks of training available from start of plan until race day</param>
        /// <returns>Number of weeks that should be scheduled for the particular plan</returns>
        internal static int GetPhaseDays(int phase, int totalDays)
        {
            int days = 0;
            int totalWeeks = totalDays / 7;

            for (int i = 1; i <= totalWeeks; i++)
            {
                if (DanielsPhaseDef.ContainsKey(i) && DanielsPhaseDef[i] == phase)
                {
                    days += 7;
                }
                else if (!DanielsPhaseDef.ContainsKey(i) && phase == 1)
                {
                    days += 7;
                }
            }

            return days;
        }

        internal static float GetSpeedMetersPerSec(Intensity intensity, float VDot)
        {
            float speed;

            switch (intensity)
            {
                default:
                    speed = 2;
                    break;
            }


            return speed;
        }

        private static float GetDistance(Intensity intensity)
        {
            return 10000;
        }
    }
}
