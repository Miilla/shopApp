using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ShopCopyForXML.Accounts
{
    public partial class Manage : System.Web.UI.Page
    {
        ControlProject ControlProject = new ControlProject();

        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
                if (Request["succes"] == "1" || Session["succes"] == "1") { Status.Text = "Dane zostały zapisane!"; Status.Visible = true; }
                else Status.Visible = false;
                if (int.Parse(Session["GroupID"].ToString()) > 5)
                {
                    aUsers.Visible = false;
                    aGroups.Visible = false;
                }
            }
            catch { Status.Visible = false; }

            
            Session["succes"] = "0";
            ControlProject.CheckSession(this);
            ControlProject.VisibleMenu(true,this);
        }
    }
}