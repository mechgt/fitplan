namespace FitPlan.UI
{
    using System.Drawing;
    using System.Windows.Forms;
    using FitPlan.Schedule;
    using ZoneFiveSoftware.Common.Visuals;

    internal class PhaseTreeRenderer : TreeList.DefaultRowDataRenderer
    {
        private TreeList tree;

        public PhaseTreeRenderer(TreeList tree)
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
            FitPlanNode node = element as FitPlanNode;
            if (node == null) return RowDecoration.None;

            // Underline phases with child workouts
            if (node.IsPhase && tree.HasChildElements(element))
            {
                return RowDecoration.BottomLineSingle;
            }
            else if (node.IsPhaseSummary)
            {
                // Separate first 'summary' line from previous workouts
                Phase.PhaseSummary summary = node.Element as Phase.PhaseSummary;
                if (summary.Style == Phase.PhaseSummary.SummaryType.Avg)
                    return RowDecoration.TopLineSingle;
            }

            return base.GetRowDecoration(element);
        }

        protected override Brush GetCellTextBrush(TreeList.DrawItemState rowState, object element, TreeList.Column column)
        {
            FitPlanNode node = element as FitPlanNode;

            // Make child workouts a lighter color
            if (node != null && node.Element.GetType() == typeof(Workout))
            {
                Workout workout = node.Element as Workout;

                if (workout.IsChild && workout.IsLinked)
                    return new SolidBrush(ControlPaint.Light(PluginMain.GetApplication().VisualTheme.ControlText, 1f));
            }

            return base.GetCellTextBrush(rowState, element, column);
        }

        protected override FontStyle GetCellFontStyle(object element, TreeList.Column column)
        {
            FitPlanNode node = element as FitPlanNode;
            if (node == null) return FontStyle.Regular;

            // Phases should be bold
            if (node.IsPhase)
                return FontStyle.Bold;

            else if (node.IsPlan && column.Id == "Name")
                return FontStyle.Bold | FontStyle.Underline;

            else if (node.IsPhaseSummary)
                return FontStyle.Italic;

            return base.GetCellFontStyle(element, column);
        }

        protected override void DrawCell(Graphics graphics, TreeList.DrawItemState rowState, object element, TreeList.Column column, Rectangle cellRect)
        {
            FitPlanNode node = element as FitPlanNode;

            if (node == null)
            {
                base.DrawCell(graphics, rowState, element, column, cellRect);
                return;
            }
            else if (node.IsPhase)
            {
                DrawPhase(graphics, rowState, node, column, cellRect);
                return;
            }
            else if (node.IsPhaseSummary)
            {
                DrawPhaseSummary(graphics, rowState, node, column, cellRect);
            }
            else if (node.IsPlan)
            {
                DrawPlan(graphics, rowState, node, column, cellRect);
            }
            else if (node.IsWorkout)
            {
                DrawWorkout(graphics, rowState, node, column, cellRect);
                return;
            }

            base.DrawCell(graphics, rowState, element, column, cellRect);
        }

        #endregion

        #region Private Draw Methods

        private void DrawPlan(Graphics graphics, TreeList.DrawItemState rowState, FitPlanNode node, TreeList.Column column, Rectangle cellRect)
        {
            TrainingPlan plan = node.Element as TrainingPlan;
            
            string text = plan.GetFormattedText(column.Id);

            // Draw String if custom text defined
            if (text != null)
            {
                Color textColor = PluginMain.GetApplication().VisualTheme.ControlText;

                graphics.DrawString(text,
                    this.Font(GetCellFontStyle(node, column)),
                    GetCellTextBrush(rowState, node, column),
                    cellRect);

                return;
            }
        }

        private void DrawPhase(Graphics graphics, TreeList.DrawItemState rowState, FitPlanNode node, TreeList.Column column, Rectangle cellRect)
        {
            Phase phase = node.Element as Phase;

            if (column.Id == "Complete")
            {
                // Draw colored square in column 1
                Brush selectedBrush = new SolidBrush(phase.DisplayColor);
                graphics.FillRectangle(selectedBrush, cellRect);
                selectedBrush.Dispose();

                // Draw plus/minus
                base.DrawCell(graphics, rowState, node, column, cellRect);
                return;
            }
            else
            {
                string text = phase.GetFormattedText(column.Id);

                // Draw String if custom text defined
                if (text != null)
                {
                    graphics.DrawString(text,
                        this.Font(GetCellFontStyle(node, column)),
                        GetCellTextBrush(rowState, node, column),
                        cellRect);

                    return;

                }
            }
        }

        private void DrawPhaseSummary(Graphics graphics, TreeList.DrawItemState rowState, FitPlanNode node, TreeList.Column column, Rectangle cellRect)
        {
            Phase.PhaseSummary summary = node.Element as Phase.PhaseSummary;
            string text;
            if (column.Id == "StartDate")
                // Display the titles in the StartDate column
                text = summary.GetFormattedText("Name");

            else if (column.Id == "Name")
                text = null;

            else
                text = summary.GetFormattedText(column.Id);

            // Draw String if custom text defined
            if (text != null)
            {
                Color textColor = PluginMain.GetApplication().VisualTheme.ControlText;

                graphics.DrawString(text,
                    this.Font(GetCellFontStyle(node, column)),
                    GetCellTextBrush(rowState, node, column),
                    cellRect);

                return;
            }
        }

        private void DrawWorkout(Graphics graphics, TreeList.DrawItemState rowState, FitPlanNode node, TreeList.Column column, Rectangle cellRect)
        {
            Workout workout = node.Element as Workout;

            if (column.Id == "Complete")
            {
                Image image = tree.LabelProvider.GetImage(node, column);
                if (image != null)
                    graphics.DrawImage(image, cellRect.Location);
            }
            else
            {
                string text = workout.GetFormattedText(column.Id);

                // Draw String if custom text defined
                if (text != null)
                {
                    Color textColor = PluginMain.GetApplication().VisualTheme.ControlText;

                    graphics.DrawString(text,
                        this.Font(GetCellFontStyle(node, column)),
                        GetCellTextBrush(rowState, node, column),
                        cellRect);

                    return;
                }
            }
        }

        #endregion
    }
}
