using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Visuals.Chart;
using FitPlan.Settings;

namespace FitPlan.Data
{
    public partial class ChartPointPairList : PointPairList
    {
        #region Enumeration

        public enum CombineAction
        {
            RemoveHigh,
            RemoveLow,
            Combine
        }

        #endregion

        #region Constructor

        public ChartPointPairList()
            : base()
        {
        }

        public ChartPointPairList(IPointList list)
            : base(list)
        {
        }

        public ChartPointPairList(PointPairList rhs)
            : base(rhs)
        {
        }

        public ChartPointPairList(double[] x, double[] y)
            : base(x, y)
        {
        }

        public ChartPointPairList(double[] x, double[] y, double[] baseVal)
            : base(x, y, baseVal)
        {
        }

        #endregion

        /// <summary>
        /// Combine neighboring duplicate entries by combining Y values together.  Duplicate entries are recognized by having the same X value.
        /// </summary>
        public void HandleDuplicates(bool sort, CombineAction action)
        {
            if (this.Count <= 1)
                return;

            if (sort && !this.Sorted)
                this.Sort(SortType.XValues);

            for (int i = 1; i < this.Count; )
            {
                if (this[i - 1].X == this[i].X)
                {
                    switch (action)
                    {
                        case CombineAction.RemoveHigh:
                            if (this[i - 1].Y < this[i].Y)
                                this.RemoveAt(i);
                            else
                                this.RemoveAt(i - 1);
                            break;

                        case CombineAction.RemoveLow:
                            if (this[i - 1].Y < this[i].Y)
                                this.RemoveAt(i - 1);
                            else
                                this.RemoveAt(i);
                            break;

                        case CombineAction.Combine:
                            this[i - 1].Y += this[i].Y;
                            this.RemoveAt(i);
                            break;
                    }
                }
                else
                {
                    i++;
                }
            }
        }

        /// <summary>
        /// Returns the first occurrance of the requested value, or -1 if value is not found.
        /// </summary>
        /// <param name="xValue"></param>
        /// <returns></returns>
        public int IndexOf(double xValue)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].X == xValue)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Returns a new series which is grouped by the selected option.
        /// NOTE: This method likely assumes the X value is an XDate!
        /// </summary>
        /// <param name="option"></param>
        public ChartPointPairList GroupBy(GlobalSettings.GroupOption option)
        {
            DayOfWeek startOfWeek = PluginMain.GetApplication().SystemPreferences.StartOfWeek;
            ChartPointPairList groupedList;
            XDate date;

            // Category sort: X: Minor Category, Y: Value, Z: Major Category
            if (option == GlobalSettings.GroupOption.Category)
            {
                groupedList = new ChartPointPairList(this);

                // Duplicate X's are combined, Y's are added, Z should be maintained (combined values s/b equal)
                groupedList.HandleDuplicates(true, CombineAction.Combine);
                return groupedList;
            }

            groupedList = new ChartPointPairList();

            // Date/Time sorts
            foreach (PointPair point in this)
            {
                date = new XDate(point.X);

                switch (option)
                {
                    default:
                    case GlobalSettings.GroupOption.Day:
                        date = new XDate(date.DateTime.Year, date.DateTime.Month, date.DateTime.Day);
                        break;

                    case GlobalSettings.GroupOption.Week:
                        int offset = (date.DateTime.DayOfWeek - startOfWeek);
                        if (offset < 0) offset += 7;
                        date.AddDays(-offset);
                        break;

                    case GlobalSettings.GroupOption.Month:
                        date = new XDate(date.DateTime.Year, date.DateTime.Month, 1);
                        break;

                }

                groupedList.Add(date, point.Y, point.Z);
            }

            groupedList.HandleDuplicates(true, CombineAction.Combine);

            return groupedList;
        }
    }
}
