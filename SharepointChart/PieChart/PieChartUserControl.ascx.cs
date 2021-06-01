using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections;

namespace SharepointChart.PieChart
{
    [ToolboxItemAttribute(false)]
    public partial class PieChartUserControl : WebPart
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            // InitializeControl();
        }

        public PieChartUserControl()
        {
        }


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region Custom ASP.NET web part property
        public override object WebBrowsableObject
        {
            get
            {
                return this;
            }
        }
        //Task list name string
        private String _listname = null;
        private String _ColumnName = null;
        private String _ViewName = null;

        [Personalizable(), WebBrowsable()]
        public String listname
        {
            get { return _listname; }
            set { _listname = value; }
        }

        [Personalizable(), WebBrowsable()]
        public String ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }

        [Personalizable(), WebBrowsable()]
        public String ViewName
        {
            get { return _ViewName; }
            set { _ViewName = value; }
        }

        //Create an editor part to set the custom task list
        public override EditorPartCollection CreateEditorParts()
        {
            ArrayList editorArray = new ArrayList();
            CustomProperty edPart = new CustomProperty();
            edPart.ID = this.ID + "_editorPart1";
            editorArray.Add(edPart);
            EditorPartCollection editorParts = new EditorPartCollection(editorArray);
            return editorParts;
        }
        //Create a custom EditorPart to edit the WebPart control.
        private class CustomProperty : EditorPart
        {
            TextBox _tblistname;
            TextBox _ColumnName;
            TextBox _ViewName;

            public CustomProperty()
            {
                Title = "WebPart Settings";
            }

            public override bool ApplyChanges()
            {
                PieChartUserControl part = (PieChartUserControl)WebPartToEdit;
                //Update the custom WebPart control with the task list
                part.listname = tblistname.Text;
                part.ColumnName = ColName.Text;
                part.ViewName = VName.Text;
                return true;
            }

            public override void SyncChanges()
            {
                PieChartUserControl part = (PieChartUserControl)WebPartToEdit;
                String currentList = part.listname;
            }

            protected override void CreateChildControls()
            {
                Controls.Clear();

                //Add a new textbox control to set the task list
                _tblistname = new TextBox();

                _ColumnName = new TextBox();

                _ViewName = new TextBox();

                Controls.Add(_tblistname);

                Controls.Add(_ViewName);

                Controls.Add(_ColumnName);

            }

            protected override void RenderContents(HtmlTextWriter writer)
            {
                writer.Write("<strong>List or Library Name :</strong>");
                writer.WriteBreak();
                _tblistname.RenderControl(writer);
                writer.WriteBreak();
                writer.WriteBreak();
                writer.Write("<strong>View Name :</strong>");
                writer.WriteBreak();
                _ViewName.RenderControl(writer);
                writer.WriteBreak();
                writer.WriteBreak();
                writer.Write("<strong>Column Name :</strong>");
                writer.WriteBreak();
                _ColumnName.RenderControl(writer);

                writer.WriteBreak();
            }

            //Return the task list name from the textbox
            private TextBox tblistname
            {
                get
                {
                    EnsureChildControls();
                    return _tblistname;
                }
            }

            private TextBox ColName
            {
                get
                {
                    EnsureChildControls();
                    return _ColumnName;
                }
            }

            private TextBox VName
            {
                get
                {
                    EnsureChildControls();
                    return _ViewName;
                }
            }
        }
        #endregion
    }
}