using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using System.Drawing;

namespace FitPlan.UI
{
    class ColumnDef : IListColumnDefinition
    {
        private string id;
        private string text;
        private string groupName;
        private int width;
        private StringAlignment align;
        private TextSource source;

        public enum TextSource
        {
            Plugin,
            SportTracks,
            Static
        }

        public ColumnDef(string id, string text, string groupName, int width, StringAlignment align, TextSource source)
            : this(id, text, groupName, width, align)
        {
            // TODO: Manage units text on column title display
            this.source = source;
        }

        public ColumnDef(string id, string text, string groupName, int width, StringAlignment align)
        {
            this.id = id;
            this.text = text;
            this.groupName = groupName;
            this.width = width;
            this.align = align;
            this.source = TextSource.Static;
        }

        #region IListColumnDefinition Members

        public StringAlignment Align
        {
            get { return this.align; }
        }

        public string GroupName
        {
            get { return this.groupName; }
        }

        public string Id
        {
            get { return this.id; }
        }

        public string Text(string columnId)
        {
            switch (source)
            {
                case TextSource.Plugin:
                    return Resources.Strings.ResourceManager.GetString(this.text);

                case TextSource.SportTracks:
                    // TODO: Dynamically retreive ST resource text (code below does NOT work)
                    //CommonResources.Text.ActionAbort
                    string value = Resources.SportTracks.ResourceManager.GetString(this.text);
                    return value;

                default:
                case TextSource.Static:
                    return this.text;
            }
        }

        public int Width
        {
            get { return this.width; }
            set { width = value; }
        }

        #endregion

        public override string ToString()
        {
            return Text(null);
        }
    }
}
