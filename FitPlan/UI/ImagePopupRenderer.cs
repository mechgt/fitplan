namespace FitPlan.UI
{
    using ZoneFiveSoftware.Common.Visuals;

    internal class ImagePopupRenderer : TreeList.DefaultRowDataRenderer
    {
        private TreeList tree;

        public ImagePopupRenderer(TreeList tree)
            : base(tree)
        {
            this.tree = tree;
        }

        public override int RowHeight(object rowElement)
        {
            return 32;
        }
    }
}
