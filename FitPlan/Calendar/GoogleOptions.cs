using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FitPlan.Schedule;

namespace FitPlan.Calendar
{
    public partial class GoogleOptions : Form
    {
        TrainingPlan plan;

        public GoogleOptions(TrainingPlan plan)
        {
            InitializeComponent();

            this.plan = plan;
            chkIconEntry.Checked = plan.IsCalendarIconSync;
            chkMainEntry.Checked = plan.IsCalendarTextSync;
        }

        private void chkIconEntry_CheckedChanged(object sender, EventArgs e)
        {
            plan.IsCalendarIconSync = chkIconEntry.Checked;
        }

        private void chkMainEntry_CheckedChanged(object sender, EventArgs e)
        {
            plan.IsCalendarTextSync = chkMainEntry.Checked;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
