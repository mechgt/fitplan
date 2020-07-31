namespace FitPlan.UI
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ZoneFiveSoftware.Common.Visuals;
    using FitPlan.Schedule;
    using System.Drawing;
    using System.Windows.Forms;

    internal class LibraryTreeRenderer : TreeList.DefaultRowDataRenderer
    {
        private TreeList tree;

        public LibraryTreeRenderer(TreeList tree)
            : base(tree)
        {
            this.tree = tree;
        }

        #region Overrides

        protected override StringFormat GetCellStringFormat(object rowElement, TreeList.Column column)
        {
            return base.GetCellStringFormat(rowElement, column);
        }

        protected override TreeList.DefaultRowDataRenderer.RowDecoration GetRowDecoration(object element)
        {
            return base.GetRowDecoration(element);
        }

        protected override Brush GetCellTextBrush(TreeList.DrawItemState rowState, object element, TreeList.Column column)
        {
            return base.GetCellTextBrush(rowState, element, column);
        }

        protected override FontStyle GetCellFontStyle(object element, TreeList.Column column)
        {
            if (element.GetType() == typeof(LibraryNode))
            {
                return FontStyle.Regular;
            }

            return base.GetCellFontStyle(element, column);
        }

        protected override void DrawCell(Graphics graphics, TreeList.DrawItemState rowState, object element, TreeList.Column column, Rectangle cellRect)
        {
            base.DrawCell(graphics, rowState, element, column, cellRect);
        }

        #endregion

    }
}
