namespace FitPlan.Data
{
    using System;
    using System.Reflection;

    internal static class TrainingLoadPlugin
    {
        private static bool isInstalled;
        private static bool installChecked;
        private static Type Common;

        internal static bool IsInstalled
        {
            get
            {
                if (installChecked)
                {
                    // Return true if plugin has already been found
                    return isInstalled;
                }

                installChecked = true;

                foreach (Assembly loadedAssembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    AssemblyName name = loadedAssembly.GetName();

                    if (name.Name.Equals("TrainingLoad"))
                    {
                        if (name.Version >= new Version(1, 8, 9, 0))
                        {
                            try
                            {
                                Common = loadedAssembly.GetType("TrainingLoad.Data.Shared");
                                MethodInfo method = Common.GetMethod("GetATL");
                                method = Common.GetMethod("GetCTL");
                                isInstalled = true;
                            }
                            catch
                            {
                                isInstalled = false;
                            }
                            return isInstalled;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                return false;
            }
        }

        internal static float TCa
        {
            get
            {
                if (IsInstalled)
                {
                    return (float)Common.GetProperty("TCa").GetValue(null, null);
                }

                return float.NaN;
            }
        }

        internal static float TCc
        {
            get
            {
                if (IsInstalled)
                {
                    return (float)Common.GetProperty("TCc").GetValue(null, null);
                }

                return float.NaN;
            }
        }
        
        internal static float GetATL(DateTime date)
        {
            if (IsInstalled)
            {
                // 0=Power, 1=HR, 2=Daniels
                MethodInfo method = Common.GetMethod("GetATL");
                object[] args = { date, (int)Settings.LogbookSettings.Main.ScoreType };
                return (float)method.Invoke(null, args);
            }
            else
            {
                return float.NaN;
            }
        }

        internal static float GetCTL(DateTime date)
        {
            if (IsInstalled)
            {
                MethodInfo method = Common.GetMethod("GetCTL");
                object[] args = { date, (int)Settings.LogbookSettings.Main.ScoreType };
                return (float)method.Invoke(null, args);
            }
            else
            {
                return float.NaN;
            }
        }
    }
}
