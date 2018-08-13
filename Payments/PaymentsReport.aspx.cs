using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace ShopCopyForXML.Payments
{
    public partial class PaymentsReport : System.Web.UI.Page
    {
        ControlProject ControlProject = new ControlProject();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["SaveSuccess"]= "0";
            ControlProject.CheckSession(this);
            ControlProject.VisibleMenu(true, this);
            GetData();
        }

        private  void GetData()
        {
            ADOHelper helper = new ADOHelper("payments_get");
            helper.AddParameter("@user_id", Session["IdUser"].ToString());
            DataTable dt = helper.Execute();
            gvMessageInfo.DataSource = dt;
            gvMessageInfo.DataBind();
        }

        protected void gvMessageInfo_DataBound(object sender, EventArgs e)
        {
            ChangeColumns(sender);

            foreach (GridViewRow row in gvMessageInfo.Rows)
            {
                row.Cells[0].Visible = false;
                row.Cells[4].Visible = false;
                if (row.Cells[3].Text == "1")
                    row.Cells[3].Text = "Obciążenie";
                else row.Cells[3].Text = "Wpłata";
            }
        }

        protected void ChangeColumns(object sender)
        {
            GridView gridView = (GridView)sender;

            if (gridView.HeaderRow != null && gridView.HeaderRow.Cells.Count > 0)
            {
                gridView.HeaderRow.Cells[0].Visible = false;
                gridView.HeaderRow.Cells[4].Visible = false;
                gridView.HeaderRow.Cells[1].Text = "Imie";
                gridView.HeaderRow.Cells[2].Text = "Nazwisko";
                gridView.HeaderRow.Cells[3].Text = "Kierunek";
                gridView.HeaderRow.Cells[5].Text = "Data operacji";
                gridView.HeaderRow.Cells[6].Text = "Wartość";
                gridView.HeaderRow.Cells[7].Text = "Modyfikujący";
                gridView.HeaderRow.Cells[8].Text = "Zmodyfikowany";
            }
        }

        protected void gvMessageInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMessageInfo.PageIndex = e.NewPageIndex;
            gvMessageInfo.DataBind();
        }

    }

}