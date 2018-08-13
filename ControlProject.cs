using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ShopCopyForXML
{
    public partial class ControlProject : System.Web.UI.Page
    {
        public void VisibleMenu(bool Visible, Page p)
        {
            int GroupId = int.Parse(p.Session["GroupID"].ToString());
            ((HtmlContainerControl)p.Master.FindControl("loginLink")).Visible = Visible;
            ((HtmlContainerControl)p.Master.FindControl("tddd")).Style.Remove("visibility");
            ((Label)p.Master.FindControl("LoginName1")).Text = Session["UserFirstName"] + " " + Session["UserLastName"];
            ((HtmlContainerControl)p.Master.FindControl("aAbout")).Visible = Visible;
            ((HtmlContainerControl)p.Master.FindControl("aManage")).Visible = Visible;
            if (GroupId!=6)
            {
               if (GroupId != 2 && GroupId != 4 && GroupId != 5) ((HtmlContainerControl)p.Master.FindControl("aProducts")).Visible = Visible;
               if (GroupId != 3 && GroupId != 5) ((HtmlContainerControl)p.Master.FindControl("aStorehouse")).Visible = Visible;
               if (GroupId != 3 && GroupId != 4) ((HtmlContainerControl)p.Master.FindControl("aClients")).Visible = Visible;
               if (GroupId != 3 && GroupId != 4) ((HtmlContainerControl)p.Master.FindControl("aSale")).Visible = Visible;
            }
            else ((HtmlContainerControl)p.Master.FindControl("aOrders")).Visible = Visible;
            if (GroupId != 2 && GroupId != 3 && GroupId != 4 && GroupId != 5) 
                ((HtmlContainerControl)p.Master.FindControl("aOrders")).Visible = Visible;
            //((HtmlContainerControl)p.Master.FindControl("aRaports")).Visible = Visible;
            ((HtmlContainerControl)p.Master.FindControl("aStore")).Visible = Visible;
            if (GroupId != 3 && GroupId != 4 ) ((HtmlContainerControl)p.Master.FindControl("aPayments")).Visible = Visible;  
        }

        public static void CheckSession(Page p)
        {
            try
            {
                if (p.Session["IdUser"].ToString() == "") HttpContext.Current.Response.Redirect("~/");
                if (p.Session["GroupID"].ToString() == "") HttpContext.Current.Response.Redirect("~/");
            }
            catch 
            {
                p.Response.Redirect("~/");
            }
        }

        public static void SavePassword(string pass, int userID, int userModID)
        {
            ADOHelper helper = new ADOHelper("user_password_save");
            helper.AddParameter("@user_password", Decode.Encrypt(pass, CommonConst.MilaKey));
            helper.AddParameter("@user_id", userID);
            helper.AddParameter("@user_mod_id", userModID);
            helper.ExecuteNonQuery();
        }
    }
}