﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace ShopCopyForXML.Accounts
{
    public partial class AddGroup : System.Web.UI.Page
    {
        ControlProject ControlProject = new ControlProject();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ControlProject.VisibleMenu(true,this);
                ControlProject.CheckSession(this);
            }
            try
            {
                if (int.Parse(Session["GroupID"].ToString())>5)
                {
                    aUsers.Visible=false;
                    aGroups.Visible = false;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ADOHelper helper = new ADOHelper("groups_get");
            DataTable dt = helper.Execute();
            
            if (tbxNazwa.Text.Length < 2) 
            {
                FailureText.Text = "Nazwa grupy jest zbyt krótka";
                return;
            }
            if (tbxNazwa.Text.Length > 49)
            {
                FailureText.Text = "Nazwa grupy jest zbyt długa";
                return;
            }
            try
            {
                int.Parse(tbxPerm.Text);
            }
            catch 
            {
                FailureText.Text = "Poziom dostępu nie jest liczbą!";
                return;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["grou_name"].ToString() == tbxNazwa.Text) { FailureText.Text = "Podana grupa już istnieje!"; return; }

                if (dt.Rows[i]["grou_permissions"].ToString() == tbxPerm.Text) { FailureText.Text = "Istnieje grupa o takich uprawniniach!"; return; }
            }
            try
            {
                
                FailureText.Text = "";
                SaveGroup(tbxNazwa.Text,int.Parse(tbxPerm.Text), int.Parse(Session["IdUser"].ToString()));
                Session["succes"] = "1";
                Response.Redirect("~/Accounts/Manage.aspx");
            }
            catch
            {
                FailureText.Text = "Błąd w procedurze";
            }            
        }

        private void SaveGroup(string tbxNazwa, int tbxPerm, int UserId)
        {
            ADOHelper helper = new ADOHelper("group_add");
            helper.AddParameter("@grou_name", tbxNazwa);
            helper.AddParameter("@grou_permissions", tbxPerm);
            helper.AddParameter("@grou_user_id", UserId);
            helper.ExecuteNonQuery();            
        }
        public static JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        public struct Result
        {
            public string errorMessage;
            public object value;
            public bool success;
            public bool finished;

            public override string ToString()
            {
                return javaScriptSerializer.Serialize(this);
            }
        }

        [WebMethod(true)]
        public static Result SavePass(string tbxPasswordOld, string tbxPasswordNew)
        {
            Result retVal = new Result { errorMessage = "" };
            ADOHelper helper = new ADOHelper("users_get");
            DataTable dt = helper.Execute();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["user_login"].ToString() == HttpContext.Current.Session["UserLogin"].ToString())
                {
                    if (Decode.Decrypt(dt.Rows[i]["user_password"].ToString(), CommonConst.MilaKey) == tbxPasswordOld && Decode.Decrypt(dt.Rows[i]["user_password"].ToString(), CommonConst.MilaKey) != tbxPasswordNew)
                    {
                        try
                        {
                            ControlProject.SavePassword(tbxPasswordNew, int.Parse(HttpContext.Current.Session["IdUser"].ToString()), int.Parse(HttpContext.Current.Session["IdUser"].ToString()));
                            retVal.success = true;
                        }
                        catch
                        {
                            retVal.errorMessage = "Błąd wystąpił w procedurze";
                        }
                    }
                    else retVal.errorMessage= "Stare hasło jest niepoprawne lub nowe hasło nie różni się od starego!";
                }
            }
            return retVal;
        }


        
    }
}