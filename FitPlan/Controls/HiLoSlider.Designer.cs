namespace FitPlan.Controls
{
    partial class HiLoSlider
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // HiLoSlider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.MinimumSize = new System.Drawing.Size(0, 30);
            this.Name = "HiLoSlider";
            this.Size = new System.Drawing.Size(393, 30);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.HiLoSlider_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HiLoSlider_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HiLoSlider_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HiLoSlider_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
