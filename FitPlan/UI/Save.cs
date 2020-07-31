namespace FitPlan.UI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using ZoneFiveSoftware.Common.Visuals;

    internal class Save : IAction
    {
        #region Fields

        private bool saving = false;
        private bool saveAs = false;

        #endregion

        #region Constructors

        public Save(bool saveAs)
        {
            this.saveAs = saveAs;
            ScheduleView.CurrentControl.PropertyChanged += new PropertyChangedEventHandler(CurrentControl_PropertyChanged);
        }

        private void CurrentControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TrainingPlan" && this.PropertyChanged != null)
            {                
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Enabled"));
            }
        }

        #endregion

        #region IAction Members

        public bool Enabled
        {
            get { return ScheduleView.CurrentControl.SelectedPlan != null; }
        }

        public bool HasMenuArrow
        {
            get { return false; }
        }

        public Image Image
        {
            get { return CommonResources.Images.Save16; }
        }

        public IList<string> MenuPath
        {
            get { return null; }
        }

        public void Refresh()
        {

        }

        public void Run(Rectangle rectButton)
        {
            saving = true;
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Title"));

            if (saveAs)
            {
                // Save As dialog...
                System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
                dlg.DefaultExt = PluginMain.PlanFileExtension;
                dlg.Filter = PluginMain.PlanFileFilter;
                dlg.FilterIndex = 1;
                dlg.AddExtension = true;
                dlg.InitialDirectory = Settings.GlobalSettings.Main.FolderPath;

                if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    // Cancel or no selected in Save As dialog
                    return;
                }
                else
                {
                    // Continue with new filename
                    ScheduleView.CurrentControl.SelectedPlan.FilePath = dlg.FileName;
                }

            }

            ScheduleView.CurrentControl.SelectedPlan.SavePlan(false);
            saving = false;
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Title"));
        }

        public string Title
        {
            get
            {
                // Save As
                if (saveAs)
                {
                    return Resources.Strings.Action_SaveAs + "...";
                }

                // Save
                if (saving)
                {
                    return CommonResources.Text.ActionSave + "...";
                }
                else
                {
                    return CommonResources.Text.ActionSave;
                }
            }
        }

        public bool Visible
        {
            get { return true; }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
