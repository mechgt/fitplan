namespace FitPlan.Controls
{
    using System.Windows.Forms;
    using ZoneFiveSoftware.Common.Visuals;

    public class AutoExpandTreelist : TreeList
    {
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            // Do nothing so the user can't collapse
        }

        public new void SetExpanded(object element, bool bExpanded, bool bExpandChildren)
        {
            // Ignore boolean parameters, force expanded
            base.SetExpanded(element, true, true);
        }

        public new object RowData
        {
            get { return base.RowData; }
            set
            {
                base.RowData = value;
                SetExpanded(RowData, true, true);
            }
        }
    }
}
