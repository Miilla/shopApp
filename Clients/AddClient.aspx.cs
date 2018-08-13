using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace ShopCopyForXML.Clients
{
    public partial class AddClient : System.Web.UI.Page
    {
        ControlProject ControlProject = new ControlProject();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ControlProject.VisibleMenu(true,this);
                ControlProject.CheckSession(this);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //string data =datepicker.
            if (tbxFname.Text.Length < 1) FailureText.Text = "Podaj imię";
            else if (tbxLname.Text.Length < 1) FailureText.Text = "Podaj nazwisko";
            //else if (tbxPrice.Text.Length < 1 || decimal.Parse(tbxPrice.Text) < 1 || decimal.Parse(tbxPrice.Text)>1000) FailureText.Text = "Podana cena jest nieprawidłowa";
            else if (tbxLogin.Text.Length < 1) FailureText.Text = "Podaj login";
            else if (tbxNip.Text.Length < 1) FailureText.Text = "Podaj nip";
            else if (tbxStreet.Text.Length < 1) FailureText.Text = "Podaj ulice";
            else if (tbxHome.Text.Length < 1) FailureText.Text = "Podaj nr domu";
            else if (tbxCity.Text.Length < 1) FailureText.Text = "Podaj miasto";
            else if (tbxZip.Text.Length < 1) FailureText.Text = "Podaj kod pocztowy";
            else
            {
                FailureText.Text = "";
                string UserId = SaveUser(tbxFname.Text, tbxLname.Text,  tbxLogin.Text, int.Parse(Session["IdUser"].ToString()));


                SaveClient(UserId, hdnDate.Value, ddlPlec.SelectedValue, tbxNip.Text, tbxStreet.Text, tbxHome.Text, tbxCity.Text, tbxZip.Text, int.Parse(Session["IdUser"].ToString()));
                Session["SaveSuccess"] = "1";
                Response.Redirect("~/Clients/ClientsReport.aspx");
            }            
        }

        private void SaveClient(string UserId, string dateTime, string ddlPlec, string tbxNip, string tbxStreet, string tbxHome, string tbxCity, string tbxZip, int IdUser)
        {
            ADOHelper helper = new ADOHelper("client_add");
            helper.AddParameter("@clie_user_id", UserId);
            helper.AddParameter("@clie_bdate", dateTime);
            helper.AddParameter("@clie_sex", ddlPlec);
            helper.AddParameter("@clie_saldo", 0);
            helper.AddParameter("@clie_nip", tbxNip);
            helper.AddParameter("@clie_street", tbxStreet);
            helper.AddParameter("@clie_home", tbxHome);
            helper.AddParameter("@clie_city", tbxCity);
            helper.AddParameter("@clie_zip", tbxZip);
            helper.AddParameter("@clie_user_mod_id", IdUser);
            helper.ExecuteNonQuery();  
        }

        private string SaveUser(string tbxFname, string tbxLname, string tbxLogin, int IdUser)
        {
            ADOHelper helper = new ADOHelper("user_insert");
            helper.AddParameter("@user_grou_id", 6);
            helper.AddParameter("@user_fname", tbxFname);
            helper.AddParameter("@user_lname", tbxLname);
            helper.AddParameter("@user_login", tbxLogin);
            helper.AddParameter("@user_user_mod_id", IdUser);
            var a = helper.ExecuteScalar();
            return a.ToString();
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