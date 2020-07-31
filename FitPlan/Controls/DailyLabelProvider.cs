using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;

namespace FitPlan.Controls
{
    class DailyLabelProvider : TreeList.DefaultLabelProvider
    {
        internal DailyLabelProvider()
            : base()
        { }

        public override string GetText(object element, TreeList.Column column)
        {
            return base.GetText(element, column);
        }
    }
}
