namespace FitPlan
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.GPS;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Fitness;
    using ZoneFiveSoftware.Common.Visuals.Util;
    using System.Drawing.Drawing2D;

    internal static class Utilities
    {
        private static Random rand = new Random(1);
        private static IList<IActivityCategory> categoryIndex;

        /// <summary>
        /// Gets a flat list of ALL logbook categories
        /// </summary>
        internal static IList<IActivityCategory> CategoryIndex
        {
            get
            {
                if (categoryIndex == null || categoryIndex.Count == 0)
                    categoryIndex = GetCategoryIndex();

                return categoryIndex;
            }
        }

        /// <summary>
        /// Get the major/parent category (running, cycling, swimming, etc.).  This is the 2nd level below My Activity Category.
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        internal static IActivityCategory GetUpperCategory(IActivity activity)
        {
            return GetUpperCategory(activity.Category);
        }

        /// <summary>
        /// Get the major/parent category (running, cycling, swimming, etc.).  This is the 2nd level below My Activity Category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        internal static IActivityCategory GetUpperCategory(IActivityCategory category)
        {
            while (category != null && category.Parent != null)
            {
                if (category.Parent.Parent == null)
                    return category;
                else
                    category = category.Parent;
            }

            // Should only land here for top level activities (My Activities, My Friend's Activities, etc.)
            return category;
        }

        /// <summary>
        /// Get the major/parent category (running, cycling, swimming, etc.).  This is the 2nd level below My Activity Category.
        /// </summary>
        /// <param name="refId">Category RefId</param>
        /// <returns></returns>
        internal static IActivityCategory GetUpperCategory(string refId)
        {
            IActivityCategory cat = GetCategory(refId);
            return GetUpperCategory(cat);
        }

        /// <summary>
        /// Gets the category object given a reference id for that category
        /// </summary>
        /// <param name="refId">RefId of the category being sought</param>
        /// <returns>Category</returns>
        public static IActivityCategory GetCategory(string refId)
        {
            foreach (IActivityCategory category in CategoryIndex)
            {
                if (category.ReferenceId == refId)
                    return category;
            }

            return null;
        }

        /// <summary>
        /// Get all activities, or only those selected
        /// </summary>
        /// <param name="all"></param>
        /// <returns></returns>
        internal static IEnumerable<IActivity> GetActivities()
        {
            IList<IActivity> activities = new List<IActivity>();

            // Prevent null ref error during startup
            if (PluginMain.GetApplication().Logbook == null ||
                PluginMain.GetApplication().ActiveView == null)
            {
                return activities;
            }

            IView view = PluginMain.GetApplication().ActiveView;

            if (view != null && view.Id == GUIDs.DailyActivityView)
            {
                IDailyActivityView activityView = view as IDailyActivityView;
                activities = CollectionUtils.GetItemsOfType<IActivity>(activityView.SelectionProvider.SelectedItems);
            }
            else if (view != null && view.Id == GUIDs.ActivityReportsView)
            {
                IActivityReportsView reportsView = view as IActivityReportsView;
                activities = CollectionUtils.GetItemsOfType<IActivity>(reportsView.SelectionProvider.SelectedItems);
            }

            return activities;
        }

        /// <summary>
        /// Create Grade track from GPSRoute
        /// </summary>
        /// <returns></returns>
        internal static INumericTimeDataSeries GetGradeTrack(IActivity activity)
        {
            INumericTimeDataSeries gradeTrack = new NumericTimeDataSeries();

            if (activity.GPSRoute != null && activity.GPSRoute.Count > 0)
            {
                IGPSPoint lastpoint = activity.GPSRoute[0].Value;

                foreach (ITimeValueEntry<IGPSPoint> item in activity.GPSRoute)
                {
                    float length = item.Value.DistanceMetersToPoint(lastpoint);
                    float height = item.Value.ElevationMeters - lastpoint.ElevationMeters;
                    float grade = height / length;

                    gradeTrack.Add(activity.GPSRoute.EntryDateTime(item), grade);

                    lastpoint = item.Value;
                }
            }
            else
            {
                // Bad or invalid data
                return null;
            }

            return gradeTrack;
        }

        /// <summary>
        /// Create Speed track from GPSRoute
        /// </summary>
        /// <returns></returns>
        internal static INumericTimeDataSeries GetSpeedTrack(IActivity activity)
        {
            INumericTimeDataSeries speedTrack = new NumericTimeDataSeries();

            if (activity.GPSRoute != null && activity.GPSRoute.Count > 0)
            {
                ITimeValueEntry<IGPSPoint> lastpoint = activity.GPSRoute[0];

                foreach (ITimeValueEntry<IGPSPoint> item in activity.GPSRoute)
                {
                    float length = item.Value.DistanceMetersToPoint(lastpoint.Value);
                    float deltaSeconds = (float)((activity.GPSRoute.EntryDateTime(item) - activity.GPSRoute.EntryDateTime(lastpoint)).TotalSeconds);
                    float speed = length / deltaSeconds;

                    speedTrack.Add(activity.GPSRoute.EntryDateTime(item), speed);

                    lastpoint = item;
                }
            }
            else
            {
                // Bad or invalid data
                return null;
            }

            return speedTrack;
        }

        /// <summary>
        /// Perform a smoothing operation using a moving average on the data series
        /// </summary>
        /// <param name="track">The data series to smooth</param>
        /// <param name="period">The range to smooth.  This is the total number of seconds to smooth across (slightly different than the ST method.)</param>
        /// <param name="min">An out parameter set to the minimum value of the smoothed data series</param>
        /// <param name="max">An out parameter set to the maximum value of the smoothed data series</param>
        /// <returns></returns>
        internal static INumericTimeDataSeries Smooth(INumericTimeDataSeries track, uint period, out float min, out float max)
        {
            min = float.NaN;
            max = float.NaN;
            INumericTimeDataSeries smooth = new NumericTimeDataSeries();

            if (track != null && track.Count > 0 && period > 1)
            {
                int start = 0;
                int index = 0;
                float value = 0;
                float delta;

                float per = period;

                // Iterate through track
                // For each point, create average starting with 'start' index and go forward averaging 'period' seconds.
                // Stop when last 'full' period can be created ([start].ElapsedSeconds + 'period' seconds >= TotalElapsedSeconds)
                while (track[start].ElapsedSeconds + period < track.TotalElapsedSeconds)
                {
                    while (track[index].ElapsedSeconds < track[start].ElapsedSeconds + period)
                    {
                        delta = track[index + 1].ElapsedSeconds - track[index].ElapsedSeconds;
                        value += track[index].Value * delta;
                        index++;
                    }

                    // Finish value calculation
                    per = track[index].ElapsedSeconds - track[start].ElapsedSeconds;
                    value = value / per;

                    // Add value to track
                    smooth.Add(track.EntryDateTime(track[index]), value);

                    // Remove beginning point for next cycle
                    delta = track[start + 1].ElapsedSeconds - track[start].ElapsedSeconds;
                    value = per * value - delta * track[start].Value;

                    // Next point
                    start++;
                }

                max = smooth.Max;
                min = smooth.Min;
            }
            else if (track != null && track.Count > 0 && period == 1)
            {
                min = track.Min;
                max = track.Max;
                return track;
            }

            return smooth;
        }

        /// <summary>
        /// SportTracks smoothing algorithm as implemented in ST core
        /// </summary>
        /// <param name="data"></param>
        /// <param name="seconds"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        internal static INumericTimeDataSeries STSmooth(INumericTimeDataSeries data, int seconds, out float min, out float max)
        {
            min = float.NaN;
            max = float.NaN;
            if (data.Count == 0)
            {
                // Special case, no data
                return new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries();
            }
            else if (data.Count == 1 || seconds < 1)
            {
                // Special case
                INumericTimeDataSeries copyData = new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries();
                min = data[0].Value;
                max = data[0].Value;
                foreach (ITimeValueEntry<float> entry in data)
                {
                    copyData.Add(data.StartTime.AddSeconds(entry.ElapsedSeconds), entry.Value);
                    min = Math.Min(min, entry.Value);
                    max = Math.Max(max, entry.Value);
                }
                return copyData;
            }
            min = float.MaxValue;
            max = float.MinValue;
            int smoothWidth = Math.Max(0, seconds * 2); // Total width/period.  'seconds' is the half-width... seconds on each side to smooth
            int denom = smoothWidth * 2; // Final value to divide by.  It's divide by 2 because we're double-adding everything
            INumericTimeDataSeries smoothedData = new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries();

            // Loop through entire dataset
            for (int nEntry = 0; nEntry < data.Count; nEntry++)
            {
                ITimeValueEntry<float> entry = data[nEntry];
                // TODO: (LOW) STSmooth: Don't reset value & index markers, instead continue data here...
                double value = 0;
                double delta;
                // Data prior to entry
                long secondsRemaining = seconds;
                ITimeValueEntry<float> p1, p2;
                int increment = -1;
                int pos = nEntry - 1;
                p2 = data[nEntry];


                while (secondsRemaining > 0 && pos >= 0)
                {
                    p1 = data[pos];
                    if (SumValues(p2, p1, ref value, ref secondsRemaining))
                    {
                        pos += increment;
                        p2 = p1;
                    }
                    else
                    {
                        break;
                    }
                }
                if (secondsRemaining > 0)
                {
                    // Occurs at beginning of track when period extends before beginning of track.
                    delta = data[0].Value * secondsRemaining * 2;
                    value += delta;
                }
                // Data after entry
                secondsRemaining = seconds;
                increment = 1;
                pos = nEntry;
                p1 = data[nEntry];
                while (secondsRemaining > 0 && pos < data.Count - 1)
                {
                    p2 = data[pos + 1];
                    if (SumValues(p1, p2, ref value, ref secondsRemaining))
                    {
                        // Move to next point
                        pos += increment;
                        p1 = p2;
                    }
                    else
                    {
                        break;
                    }
                }
                if (secondsRemaining > 0)
                {
                    // Occurs at end of track when period extends past end of track
                    value += data[data.Count - 1].Value * secondsRemaining * 2;
                }
                float entryValue = (float)(value / denom);
                smoothedData.Add(data.StartTime.AddSeconds(entry.ElapsedSeconds), entryValue);
                min = Math.Min(min, entryValue);
                max = Math.Max(max, entryValue);

                // STSmooth: TODO: Remove 'first' p1 & p2 SumValues from 'value'
                if (data[nEntry].ElapsedSeconds - seconds < 0)
                {
                    // Remove 1 second worth of first data point (multiply by 2 because everything is double here)
                    value -= data[0].Value * 2;
                }
                else
                {
                    // Remove data in middle of track (typical scenario)
                    //value -= 
                }
            }
            return smoothedData;
        }

        /// <summary>
        /// Used by STSmooth
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="value"></param>
        /// <param name="secondsRemaining"></param>
        /// <returns></returns>
        private static bool SumValues(ITimeValueEntry<float> p1, ITimeValueEntry<float> p2, ref double value, ref long secondsRemaining)
        {
            double spanSeconds = Math.Abs((double)p2.ElapsedSeconds - (double)p1.ElapsedSeconds);
            if (spanSeconds <= secondsRemaining)
            {
                value += (p1.Value + p2.Value) * spanSeconds;
                secondsRemaining -= (long)spanSeconds;
                return true;
            }
            else
            {
                double percent = (double)secondsRemaining / (double)spanSeconds;
                value += (p1.Value * ((float)2 - percent) + p2.Value * percent) * secondsRemaining;
                secondsRemaining = 0;
                return false;
            }
        }

        /// <summary>
        /// Formats a timespan into hh:mm:ss format.
        /// </summary>
        /// <param name="span">Timespan</param>
        /// <returns>hh:mm:ss formatted string (omits hours if less than 1 hour).  Returns empty string if timespan = 0.</returns>
        internal static string ToTimeString(TimeSpan span)
        {
            return ToTimeString(span, string.Empty);
        }

        /// <summary>
        /// Formats a timespan into hh:mm:ss format.
        /// </summary>
        /// <param name="span">Timespan</param>
        /// <returns>hh:mm:ss formatted string (omits hours if less than 1 hour).  Returns empty string if timespan = 0.</returns>
        internal static string ToTimeString(TimeSpan span, string format)
        {
            if (string.IsNullOrEmpty(format))
                format = "hh:mm:ss;(hh:mm:ss);#";

            if (span == TimeSpan.Zero)
            {
                // Return empty if zero.
                return string.Empty;
            }

            if (format.Contains(";"))
            {
                // positive, negative, zero
                if (TimeSpan.Zero < span)
                {
                    format = format.Split(';')[0];
                }
                else if (span < TimeSpan.Zero)
                {
                    span = -span;
                    format = format.Split(';')[1];
                }
                else if (span == TimeSpan.Zero)
                {
                    format = format.Split(';')[2];
                }
            }
            else if (span < TimeSpan.Zero)
            {
                span = -span;
                format = string.Format("({0})", format);
            }

            string displayTime, hours = "0", minutes, seconds;

            if (format.Contains("hh"))
            {
                // Hours & minutes
                hours = ((span.Days * 24) + span.Hours).ToString("#0");
                minutes = span.Minutes.ToString("00");
            }
            else
            {
                // Double digit minutes
                minutes = span.Minutes.ToString("#0");
            }

            seconds = span.Seconds.ToString("00");

            format = format.Replace("hh", "{0}");
            format = format.Replace("mm", "{1}");
            format = format.Replace("ss", "{2}");

            displayTime = string.Format(format, hours, minutes, seconds);

            return displayTime;
        }

        /// <summary>
        /// This will parse a timespan formatted in mm:ss.
        /// This automatically interprets unspecific times (e.g 2:00 <-- hours?, minutes?) to be appropriate workout times.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        internal static bool TryParseMinutesSeconds(string s, out TimeSpan result)
        {
            float value;
            if (!s.Contains(":") && float.TryParse(s, out value))
            {
                if (value < 10)
                {
                    result = TimeSpan.FromHours(value);
                    return true;
                }
                else
                {
                    result = TimeSpan.FromMinutes(value);
                    return true;
                }
            }
            else if (s.Split(':').Length == 2)
            {
                // TODO: (LOW) Intelligent time pickin' code below, works well for 'time', but screws up 'pace'.
                //if (float.TryParse(s.Split(':')[0], out value) && value < 10)
                //{
                //    s = s + ":00";
                //}
                //else
                {
                    s = "00:" + s;
                }
            }

            if (TimeSpan.TryParse(s, out result))
            {
                return true;
            }
            else
            {
                result = TimeSpan.Zero;
                return false;
            }
        }

        /// <summary>
        /// Removes paused (but not stopped?) times in track.
        /// </summary>
        /// <param name="sourceTrack">Source data track to remove paused times</param>
        /// <param name="activity"></param>
        /// <returns>Returns an INumericTimeDataSeries with the paused times removed.</returns>
        public static INumericTimeDataSeries RemovePausedTimesInTrack(INumericTimeDataSeries sourceTrack, IActivity activity)
        {
            ActivityInfo activityInfo = ActivityInfoCache.Instance.GetInfo(activity);

            if (activityInfo != null && sourceTrack != null)
            {
                INumericTimeDataSeries result = new NumericTimeDataSeries();

                if (activityInfo.NonMovingTimes.Count == 0)
                {
                    // Remove invalid data nonetheless
                    DateTime currentTime = sourceTrack.StartTime;
                    IEnumerator<ITimeValueEntry<float>> sourceEnumerator = sourceTrack.GetEnumerator();
                    bool sourceEnumeratorIsValid;

                    sourceEnumeratorIsValid = sourceEnumerator.MoveNext();

                    while (sourceEnumeratorIsValid)
                    {
                        if (!float.IsNaN(sourceEnumerator.Current.Value))
                        {
                            result.Add(currentTime, sourceEnumerator.Current.Value);
                        }

                        sourceEnumeratorIsValid = sourceEnumerator.MoveNext();
                        currentTime = sourceTrack.StartTime + new TimeSpan(0, 0, (int)sourceEnumerator.Current.ElapsedSeconds);
                    }
                }
                else
                {
                    DateTime currentTime = sourceTrack.StartTime;
                    IEnumerator<ITimeValueEntry<float>> sourceEnumerator = sourceTrack.GetEnumerator();
                    IEnumerator<IValueRange<DateTime>> pauseEnumerator = activityInfo.NonMovingTimes.GetEnumerator();
                    double totalPausedTimeToDate = 0;
                    bool sourceEnumeratorIsValid;
                    bool pauseEnumeratorIsValid;

                    pauseEnumeratorIsValid = pauseEnumerator.MoveNext();
                    sourceEnumeratorIsValid = sourceEnumerator.MoveNext();

                    while (sourceEnumeratorIsValid)
                    {
                        bool addCurrentSourceEntry = true;
                        bool advanceCurrentSourceEntry = true;

                        // Loop to handle all pauses up to this current track point
                        if (pauseEnumeratorIsValid)
                        {
                            if (currentTime > pauseEnumerator.Current.Lower &&
                                currentTime <= pauseEnumerator.Current.Upper)
                            {
                                addCurrentSourceEntry = false;
                            }
                            else if (currentTime > pauseEnumerator.Current.Upper)
                            {
                                // Advance pause enumerator
                                totalPausedTimeToDate += (pauseEnumerator.Current.Upper - pauseEnumerator.Current.Lower).TotalSeconds;
                                pauseEnumeratorIsValid = pauseEnumerator.MoveNext();

                                // Make sure we retry with the next pause
                                addCurrentSourceEntry = false;
                                advanceCurrentSourceEntry = false;
                            }
                        }

                        if (addCurrentSourceEntry && !float.IsNaN(sourceEnumerator.Current.Value))
                        {
                            DateTime entryTime = currentTime - new TimeSpan(0, 0, (int)totalPausedTimeToDate);

                            result.Add(entryTime, sourceEnumerator.Current.Value);
                        }

                        if (advanceCurrentSourceEntry)
                        {
                            sourceEnumeratorIsValid = sourceEnumerator.MoveNext();
                            currentTime = sourceTrack.StartTime + new TimeSpan(0, 0, (int)sourceEnumerator.Current.ElapsedSeconds);
                        }
                    }
                }

                return result;
            }

            return null;
        }

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        internal static string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return constructedString;
        }

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        internal static byte[] StringToUTF8ByteArray(string pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        /// <summary>
        /// Rainbow will return a distinct list of colors based on ROYGBIV
        /// </summary>
        /// <param name="totalItems">Number of colors to generate</param>
        /// <param name="alpha">Alpha to be applied to all colors</param>
        /// <returns>Returns a distinct list of colors</returns>
        internal static List<Color> Rainbow(int totalItems, int alpha)
        {
            List<Color> colors = new List<Color>();
            double red;
            double green;
            double blue;
            double scaleFactor;

            // Harshness of the color. Max is 255
            int harshness = 150;

            // Manually add the colors if there are less than 6 items
            if (totalItems == 1)
            {
                colors.Add(Color.FromArgb(alpha, harshness, 0, 0));
            }
            else if (totalItems == 2)
            {
                colors.Add(Color.FromArgb(alpha, harshness, 0, 0));
                colors.Add(Color.FromArgb(alpha, 0, harshness, 0));
            }
            else if (totalItems == 3)
            {
                colors.Add(Color.FromArgb(alpha, harshness, 0, 0));
                colors.Add(Color.FromArgb(alpha, 0, harshness, 0));
                colors.Add(Color.FromArgb(alpha, 0, 0, harshness));
            }
            else if (totalItems == 4)
            {
                colors.Add(Color.FromArgb(alpha, harshness, 0, 0));
                colors.Add(Color.FromArgb(alpha, harshness, harshness, 0));
                colors.Add(Color.FromArgb(alpha, 0, harshness, 0));
                colors.Add(Color.FromArgb(alpha, 0, 0, harshness));
            }
            else if (totalItems == 5)
            {
                colors.Add(Color.FromArgb(alpha, harshness, 0, 0));
                colors.Add(Color.FromArgb(alpha, harshness, harshness, 0));
                colors.Add(Color.FromArgb(alpha, 0, harshness, 0));
                colors.Add(Color.FromArgb(alpha, 0, harshness, harshness));
                colors.Add(Color.FromArgb(alpha, 0, 0, harshness));
            }

            // Make sure we have a multiple of 6 to rainbow
            while (totalItems % 6 != 0)
            {
                totalItems += 1;
            }

            // Find the factor to which we will scale the colors
            scaleFactor = ((double)harshness / totalItems) * 6f;

            // Red is our starting point
            red = harshness;
            green = 0;
            blue = 0;

            // Add red to the list
            colors.Add(Color.FromArgb(alpha, (int)red, (int)green, (int)blue));

            // Work your way through the spectrum to build the colors
            while (green < harshness)
            {
                green += scaleFactor;

                // Catch any potential rounding issues
                if (green > harshness)
                {
                    green = harshness;
                }
                colors.Add(Color.FromArgb(alpha, (int)red, (int)green, (int)blue));
            }

            while (red > 0)
            {
                red -= scaleFactor;

                // Catch any potential rounding issues
                if (red < 0)
                {
                    red = 0;
                }

                colors.Add(Color.FromArgb(alpha, (int)red, (int)green, (int)blue));
            }

            while (blue < harshness)
            {
                blue += scaleFactor;

                // Catch any potential rounding issues
                if (blue > harshness)
                {
                    blue = harshness;
                }

                colors.Add(Color.FromArgb(alpha, (int)red, (int)green, (int)blue));
            }

            while (green > 0)
            {
                green -= scaleFactor;

                // Catch any potential rounding issues
                if (green < 0)
                {
                    green = 0;
                }

                colors.Add(Color.FromArgb(alpha, (int)red, (int)green, (int)blue));
            }

            while (red < harshness)
            {
                red += scaleFactor;

                // Catch any potential rounding issues
                if (red > harshness)
                {
                    red = harshness;
                }

                colors.Add(Color.FromArgb(alpha, (int)red, (int)green, (int)blue));
            }

            while (blue > 0)
            {
                blue -= scaleFactor;

                // Catch any potential rounding issues
                if (blue < 0)
                {
                    blue = 0;
                }

                colors.Add(Color.FromArgb(alpha, (int)red, (int)green, (int)blue));
            }

            // The last color and the first color should be the same.  Remove the last color
            colors.RemoveAt(colors.Count - 1);

            // Return the colors list
            return colors;
        }

        /// <summary>
        /// Get a random, but controlled color.
        /// </summary>
        /// <param name="alpha">Alpha value of the returned color.</param>
        /// <param name="harshness"></param>
        /// <param name="seed">Re-seed the random number and return random color.</param>
        /// <returns></returns>
        internal static Color RandomColor(int alpha, int harshness, int seed)
        {
            rand = new Random(seed);
            return RandomColor(alpha, harshness);
        }

        /// <summary>
        /// Get a random, but controlled color.
        /// </summary>
        /// <param name="alpha">Alpha value of the returned color.</param>
        /// <param name="harshness"></param>
        /// <returns></returns>
        internal static Color RandomColor(int alpha, int harshness)
        {
            return Color.FromArgb(alpha, rand.Next(harshness), rand.Next(harshness), rand.Next(harshness));
        }

        internal static Bitmap MakeGrayscale(Bitmap original)
        {
            // create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            // get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            // create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
              {
                 new float[] { .3f, .3f, .3f, 0, 0 },
                 new float[] { .59f, .59f, .59f, 0, 0 },
                 new float[] { .11f, .11f, .11f, 0, 0 },
                 new float[] { 0, 0, 0, 1, 0 },
                 new float[] { 0, 0, 0, 0, 1 }
              });

            // create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            // set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            // draw the original image on the new image
            // using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            // dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

        internal static Bitmap MakeTransparent(Bitmap image)
        {
            Bitmap bitmap = new Bitmap(image);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.Gray);
            Rectangle rect = new Rectangle(20, 20, 200, 100);
            float[][] ptsArray = {
                new float[] { 1, 0, 0, 0, 0 },
                new float[] { 0, 1, 0, 0, 0 },
                new float[] { 0, 0, 1, 0, 0 },
                new float[] { 0, 0, 0, 0.5f, 0 },
                new float[] { 0, 0, 0, 0, 1 } };
            ColorMatrix clrMatrix = new ColorMatrix(ptsArray);
            ImageAttributes imgAttributes = new ImageAttributes();
            imgAttributes.SetColorMatrix(clrMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            g.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imgAttributes);

            // Dispose
            g.Dispose();

            return bitmap;
        }

        /// <summary>
        /// Makes an image semi-transparent.
        /// </summary>
        /// <param name="image">Input image</param>
        /// <param name="opacity">0 to 1</param>
        /// <returns>Semi transparent image</returns>
        internal static Image SetOpacity(Image image, float opacity)
        {
            if (image == null)
                return null;

            ColorMatrix colorMatrix = new ColorMatrix();
            colorMatrix.Matrix33 = opacity;
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(
                colorMatrix,
                ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);
            Image output = new Bitmap(image.Width, image.Height);
            using (Graphics gfx = Graphics.FromImage(output))
            {
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.DrawImage(
                    image,
                    new Rectangle(0, 0, image.Width, image.Height),
                    0,
                    0,
                    image.Width,
                    image.Height,
                    GraphicsUnit.Pixel,
                    imageAttributes);
            }
            return output;
        }

        /// <summary>
        /// Creates and returns a flat list of ALL categories
        /// </summary>
        /// <returns></returns>
        private static IList<IActivityCategory> GetCategoryIndex()
        {
            IList<IActivityCategory> categoryIndex = new List<IActivityCategory>();

            foreach (IActivityCategory category in PluginMain.GetApplication().Logbook.ActivityCategories)
            {
                categoryIndex.Add(category);
                AddSubcategories(ref categoryIndex, category);
            }

            return categoryIndex;
        }

        /// <summary>
        /// Generate a random string
        /// </summary>
        /// <param name="size">Number of characters</param>
        /// <returns>Returns a random string of (size) length.  Random string contains capital letters and numbers only.</returns>
        public static string RandomString(int size)
        {
            const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[rand.Next(_chars.Length)];
            }

            return new string(buffer);
        }

        private const string Clist = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly char[] Clistarr = Clist.ToCharArray();

        public static long Base36Decode(string inputString)
        {
            long result = 0;
            var pow = 0;
            for (var i = inputString.Length - 1; i >= 0; i--)
            {
                var c = inputString[i];
                var pos = Clist.IndexOf(c);
                if (pos > -1)
                    result += pos * (long)Math.Pow(Clist.Length, pow);
                else
                    return -1;
                pow++;
            }
            return result;
        }

        public static string Base36Encode(ulong inputNumber)
        {
            var sb = new StringBuilder();
            do
            {
                sb.Append(Clistarr[inputNumber % (ulong)Clist.Length]);
                inputNumber /= (ulong)Clist.Length;
            } while (inputNumber != 0);

            return Reverse(sb.ToString());
        }

        public static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// Used by GetCategoryIndex() to recurse through subcategories
        /// </summary>
        /// <param name="categoryIndex"></param>
        /// <param name="parent"></param>
        private static void AddSubcategories(ref IList<IActivityCategory> categoryIndex, IActivityCategory parent)
        {
            foreach (IActivityCategory category in parent.SubCategories)
            {
                categoryIndex.Add(category);
                AddSubcategories(ref categoryIndex, category);
            }
        }

        /// <summary>
        /// Converts speed to pace (in length/minute)
        /// </summary>
        /// <param name="speed">Speed in some distance units (length units are maintained)</param>
        /// <returns>Returns a number of minutes per length unit (miles for instance)</returns>
        public static double SpeedToPace(double speed)
        {
            ushort MinutesPerHour = 60;
            return MinutesPerHour / speed;
        }

        /// <summary>
        /// Converts a pace unit (number of minutes) to a speed
        /// </summary>
        /// <param name="pace"></param>
        /// <returns></returns>
        public static double PaceToSpeed(double paceMinutes)
        {
            ushort MinutesPerHour = 60;
            return MinutesPerHour / paceMinutes;
        }

        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
