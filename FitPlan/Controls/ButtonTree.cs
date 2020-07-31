using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Visuals;
using FitPlan.UI;

namespace FitPlan.Controls
{
    public partial class ButtonTree : TreeList
    {
        List<int> selectStack;

        public ButtonTree()
        {
            InitializeComponent();
            this.HeaderRowHeight = 0;
            this.BeforeSelectedChange += new BeforeSelectedChangeEventHandler(ButtonTree_BeforeSelectedChange);
            this.SelectedItemsChanged += new EventHandler(ButtonTree_SelectedItemsChanged);
            selectStack = new List<int>();
        }

        public int AvailableRows
        {
            get
            {
                return (int)this.Height / this.DefaultRowHeight;
            }
        }

        internal void AddNode(DailyNode node)
        {
            List<DailyNode> nodes = RowDataDailyNodes;
            nodes.Add(node);
            RowData = nodes;

            if (RowDataDailyNodes.Count == 1)
            {
                this.SetExpanded(node, true);
                selectStack.Add(0);
            }
        }

        internal void ClearNodes()
        {
            RowData = new List<DailyNode>();
        }

        void ButtonTree_SelectedItemsChanged(object sender, EventArgs e)
        {
            return;
        }

        void ButtonTree_BeforeSelectedChange(object sender, TreeList.BeforeSelectedChangeEventArgs e)
        {
            DailyNode node = e.Selected[0] as DailyNode;

            if (!node.IsParent)
                return;

            // Collapse old selection
            while (selectStack.Count > 0)
            {
                this.SetExpanded(this.RowDataDailyNodes[selectStack[0]], false);
                selectStack.RemoveAt(0);
            }

            // Expand new selection
            this.SetExpanded(node, true);
            int index = this.RowDataDailyNodes.IndexOf(node);
            selectStack.Add(index);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            // Ignore double-clicks
            return;
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Do not expand/collapse from user input
            if (e.KeyData.Equals(Keys.Left) || e.KeyData.Equals(Keys.Right))
                e.Handled = true;
            else
                base.OnKeyDown(e);
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Do not expand/collapse from user input
            if (e.KeyChar.Equals(Keys.Left) || e.KeyChar.Equals(Keys.Right))
                e.Handled = true;
            else
                base.OnKeyPress(e);
        }

        public new object RowData
        {
            get { return base.RowData; }
            set { base.RowData = value; }
        }

        internal List<DailyNode> RowDataDailyNodes
        {
            get { return RowData as List<DailyNode>; }
        }

        private DailyNode SelectedNode
        {
            get
            {
                if (RowDataDailyNodes == null || RowDataDailyNodes.Count == 0)
                    return null;

                if (selectStack.Count >= 1 && RowDataDailyNodes.Count > selectStack[0])
                    return RowDataDailyNodes[selectStack[0]];

                return null;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Draw(e.Graphics, e.ClipRectangle);
        }

        internal void Draw(Graphics g, Rectangle rect)
        {
            DailyNode node = SelectedNode;

            if (node != null && node.WorkoutImage != null)
                g.DrawImage(node.WorkoutImage, 2, (selectStack[0] + 1) * this.RowDataRenderer.RowHeight(node) + 3);
        }
    }
}
