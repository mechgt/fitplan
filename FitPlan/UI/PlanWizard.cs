namespace FitPlan.UI
{
    using System;
    using System.Globalization;
    using System.Windows.Forms;
    using FitPlan.Schedule;

    public partial class PlanWizard : Form
    {
        private DateTime start;
        private DateTime end;
        private string name = "Training Plan";

        public PlanWizard()
        {
            InitializeComponent();
        }

        public void UICultureChanged(CultureInfo culture)
        {
            // TODO: Plan Wizard: Localize
        }

        private void CollectInfo()
        {
            if (!DateTime.TryParse(lblStartDate.Text, out start))
            {
                // start error
            }
            if (!DateTime.TryParse(lblEndDate.Text, out end))
            {
                // end error
            }
            if (txtName.Text != string.Empty)
            {
                name = txtName.Text;
            }
        }

        #region Button Handlers

        private void wizardControl_FinishButtonClick(object sender, EventArgs e)
        {
            TrainingPlan newPlan = new Schedule.DanielsPlan(start, (int)(end - start).TotalDays, 60000);
            newPlan.Name = name;

            this.Tag = newPlan;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void wizardControl_CancelButtonClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
    }
}
