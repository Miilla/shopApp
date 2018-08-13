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
    public partial class WorkerDashboard : Page
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

            if (!IsPostBack)
            {
                GetData();
            }
            GetLastProducts();
        }

        private void GetLastProducts()
        {
            ADOHelper helper = new ADOHelper("products_get");
            helper.AddParameter("@banner", 0);
            helper.AddParameter("@prod_quantity", 0);
            DataTable dt = helper.Execute();
            gvMessageInfo.DataSource = dt;
            gvMessageInfo.DataBind();
        }

        private void GetData()
        {
            ADOHelper helper = new ADOHelper("categories_get");
            DataTable dt = helper.Execute();
            Session["statuses"] = dt;
            ddlStatuses.DataTextField = "cate_name";
            ddlStatuses.DataValueField = "cate_id";
            ddlStatuses.DataSource = dt;
            ddlStatuses.DataBind();
        }

        protected void gvMessageInfo_DataBound(object sender, EventArgs e)
        {
            ChangeColumns(sender);

            foreach (GridViewRow row in gvMessageInfo.Rows)
            {
                row.Cells[0].Visible = false;
                //row.Cells[5].Visible = false;
                row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                row.Cells[7].Visible = false;
                row.Cells[3].Text = ddlStatuses.Items.FindByValue(row.Cells[3].Text).Text;


            }
        }

        protected void ChangeColumns(object sender)
        {
            GridView gridView = (GridView)sender;

            if (gridView.HeaderRow != null && gridView.HeaderRow.Cells.Count > 0)
            {
                gridView.HeaderRow.Cells[0].Visible = false;
                //gridView.HeaderRow.Cells[5].Visible = false;
                gridView.HeaderRow.Cells[5].Visible = false;
                gridView.HeaderRow.Cells[6].Visible = false;
                gridView.HeaderRow.Cells[7].Visible = false;
                gridView.HeaderRow.Cells[1].Text = "Nazwa";
                gridView.HeaderRow.Cells[2].Text = "Ilość";
                gridView.HeaderRow.Cells[3].Text = "Kategoria";
                gridView.HeaderRow.Cells[4].Text = "Cena";
                gridView.HeaderRow.Cells[8].Text = "Modyfikujący";
                gridView.HeaderRow.Cells[9].Text = "Zmodyfikowany";
            }
        }
    }
}