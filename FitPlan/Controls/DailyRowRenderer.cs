using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using System.Drawing;
using System.Windows.Forms;

namespace FitPlan.Controls
{
    class DailyRowRenderer : TreeList.DefaultRowDataRenderer
    {
        internal DailyRowRenderer(TreeList tree)
            : base(tree)
        {
            Tree.NumHeaderRows = TreeList.HeaderRows.None;
            this.RowSeparatorLines = false;
            this.RowAlternatingColors = false;
            //this.RowHotlightColor = tree.BackColor;
            this.RowHotlightColor = Color.Empty;
            this.RowSelectedColor = Color.Empty;
        }

        /// <summary>
        /// Render the TreeList. Normally this would not be overridden, instead override
        /// DrawBackground, DrawRowBackground, DrawCellData.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipRect"></param>
        /// <param name="rectDraw"></param>
        public override void Draw(Graphics graphics, Rectangle clipRect, Rectangle rectDraw)
        {
            base.Draw(graphics, clipRect, rectDraw);
        }

        /// <summary>
        /// Draws the background for the entire TreeList
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipRect"></param>
        /// <param name="rectDraw"></param>
        protected override void DrawBackground(Graphics graphics, Rectangle clipRect, Rectangle rectDraw)
        {
            base.DrawBackground(graphics, clipRect, rectDraw);
        }

        /// <summary>
        /// Draw the cell data for a particular row and column.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="rowState"></param>
        /// <param name="element"></param>
        /// <param name="column"></param>
        /// <param name="cellRect"></param>
        protected override void DrawCell(Graphics graphics, TreeList.DrawItemState rowState, object element, TreeList.Column column, Rectangle cellRect)
        {
            base.DrawCell(graphics, rowState, element, column, cellRect);
        }

        /// <summary>
        /// Draw the cell data for a particular row and column.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipRect"></param>
        /// <param name="rectDraw"></param>
        protected override void DrawCellData(Graphics graphics, Rectangle clipRect, Rectangle rectDraw)
        {
            base.DrawCellData(graphics, clipRect, rectDraw);
        }

        /// <summary>
        /// Draw the cell data for a particular column.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipRect"></param>
        /// <param name="columnDrawRect"></param>
        /// <param name="column"></param>
        protected override void DrawColumnData(Graphics graphics, Rectangle clipRect, Rectangle columnDrawRect, TreeList.Column column)
        {
            base.DrawColumnData(graphics, clipRect, columnDrawRect, column);
        }

        /// <summary>
        /// Draw the background for the rows based on the row state.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipRect"></param>
        /// <param name="rectDraw"></param>
        protected override void DrawRowBackground(Graphics graphics, Rectangle clipRect, Rectangle rectDraw)
        {
            base.DrawRowBackground(graphics, clipRect, rectDraw);
        }

        /// <summary>
        /// Draw the cell data for the first column in the tree, including any indicators
        /// for checkboxes or expand/collapse controls.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="rowState"></param>
        /// <param name="element"></param>
        /// <param name="column"></param>
        /// <param name="cellRect"></param>
        /// <param name="cellText"></param>
        protected override void DrawTreeColumnCell(Graphics graphics, TreeList.DrawItemState rowState, object element, TreeList.Column column, Rectangle cellRect, string cellText)
        {
            DailyNode node = element as DailyNode;
            if (node.IsParent)
                ControlPaint.DrawButton(graphics, cellRect.X - 3, cellRect.Y - 1, cellRect.Width + 8, cellRect.Height + 3, ButtonState.Normal);

            base.DrawTreeColumnCell(graphics, rowState, element, column, cellRect, cellText);
        }

        public override int RowHeight(object rowElement)
        {
            return base.RowHeight(rowElement);
        }

        protected override RowDecoration GetRowDecoration(object element)
        {
            DailyNode node = element as DailyNode;
            if (node.IsParent)
                return RowDecoration.BottomLineSingle;

            return base.GetRowDecoration(element);
        }

        protected override FontStyle GetCellFontStyle(object element, TreeList.Column column)
        {
            //DailyNode node = element as DailyNode;
            //if (node.IsParent)
            //    return FontStyle.Italic;

            return base.GetCellFontStyle(element, column);
        }

        protected override StringFormat GetCellStringFormat(object rowElement, TreeList.Column column)
        {
            return base.GetCellStringFormat(rowElement, column);
        }

        protected override Brush GetCellTextBrush(TreeList.DrawItemState rowState, object element, TreeList.Column column)
        {
            return base.GetCellTextBrush(rowState, element, column);
        }
    }
}
