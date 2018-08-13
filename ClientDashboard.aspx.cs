using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ShopCopyForXML
{
    public partial class ClientDashboard : Page
    {

        ControlProject ControlProject = new ControlProject();
        string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
               // if (Session["SaveSuccess"].ToString() == "1")
                   // Success.Text = "Element został dodany";
            }
            catch
            {

            }
            Session["SaveSuccess"] = "0";
            ControlProject.CheckSession(this);
            ControlProject.VisibleMenu(true, this);
            //Status.Text = "";

            if (!IsPostBack)
            {
                GetData();
            }
            GetLastOrders();
        }

        private void GetLastOrders()
        {
            ADOHelper helper = new ADOHelper("orders_top5_get");
            helper.AddParameter("@status", -1);
            DataTable dt = helper.Execute();
            gvMessageInfo.DataSource = dt;
            gvMessageInfo.DataBind();
        }

        private void GetData()
        {
            ADOHelper helper = new ADOHelper("statuses_get");
            DataTable dt = helper.Execute();
            Session["statuses"] = dt;
            ddlStatuses.DataTextField = "stat_name";
            ddlStatuses.DataValueField = "stat_id";
            ddlStatuses.DataSource = dt;
            ddlStatuses.DataBind();
        }

        protected void gvMessageInfo_DataBound(object sender, EventArgs e)
        {
            ChangeColumns(sender);
            foreach (GridViewRow row in gvMessageInfo.Rows)
            {
                row.Cells[0].Visible = false;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[7].Visible = false;
                row.Cells[8].Visible = false;
                row.Cells[9].Text = ddlStatuses.Items.FindByValue(row.Cells[9].Text).Text;
            }
        }

        protected void ChangeColumns(object sender)
        {
            GridView gridView = (GridView)sender;

            if (gridView.HeaderRow != null && gridView.HeaderRow.Cells.Count > 0)
            {
                gridView.HeaderRow.Cells[0].Visible = false;
                gridView.HeaderRow.Cells[1].Visible = false;
                gridView.HeaderRow.Cells[2].Visible = false;
                gridView.HeaderRow.Cells[3].Visible = false;
                gridView.HeaderRow.Cells[7].Visible = false;
                gridView.HeaderRow.Cells[8].Visible = false;
                gridView.HeaderRow.Cells[3].Text = "Imię";
                gridView.HeaderRow.Cells[4].Text = "Nazwisko";
                gridView.HeaderRow.Cells[5].Text = "Miasto";
                gridView.HeaderRow.Cells[6].Text = "Ulica";
                gridView.HeaderRow.Cells[7].Text = "Nr domu";
                gridView.HeaderRow.Cells[8].Text = "Kod pocztowy";
                gridView.HeaderRow.Cells[9].Text = "Status";
                gridView.HeaderRow.Cells[10].Text = "Data Zamówienia";
                gridView.HeaderRow.Cells[11].Text = "Modyfikujący";
                gridView.HeaderRow.Cells[12].Text = "Zmodyfikowany";
            }
        }
    }
}