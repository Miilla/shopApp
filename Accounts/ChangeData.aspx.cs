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

namespace ShopCopyForXML.Accounts
{
    public partial class ChangeData : System.Web.UI.Page
    {
        int UserId;
        ControlProject ControlProject = new ControlProject();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (int.Parse(Session["GroupID"].ToString()) > 5)
                {
                    aUsers.Visible = false;
                    aGroups.Visible = false;
                }
                UserId = int.Parse(Session["IdUser"].ToString());
            }
            catch (Exception)
            {
                
                throw;
            }
            ADOHelper helper = new ADOHelper("client_get");
            helper.AddParameter("@user_id", UserId);
            DataTable dt = helper.Execute();
            if (!IsPostBack)
            {
                ControlProject.VisibleMenu(true, this);
                ControlProject.CheckSession(this);
                SetData(dt);
            }

        }

        private void SetData(DataTable dt)
        {
            tbxLogin.Text = dt.Rows[0]["user_login"].ToString();
            tbxFname.Text = dt.Rows[0]["user_fname"].ToString();
            tbxLname.Text = dt.Rows[0]["user_lname"].ToString();
            hdnDate.Value = dt.Rows[0]["clie_bdate"].ToString();
            ddlPlec.SelectedValue = dt.Rows[0]["clie_sex"].ToString();
            tbxNip.Text = dt.Rows[0]["clie_nip"].ToString();
            tbxStreet.Text = dt.Rows[0]["clie_street"].ToString();
            tbxHome.Text = dt.Rows[0]["clie_home"].ToString();
            tbxCity.Text = dt.Rows[0]["clie_city"].ToString();
            tbxZip.Text = dt.Rows[0]["clie_zip"].ToString();
            Session["ClieId"]= dt.Rows[0]["clie_id"].ToString();
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
                ///string UserId = SaveUser(tbxFname.Text, tbxLname.Text, tbxLogin.Text, int.Parse(Session["IdUser"].ToString()));


                UpdateClient(hdnDate.Value, tbxFname.Text, tbxLname.Text, tbxNip.Text, tbxStreet.Text, tbxHome.Text, tbxCity.Text, tbxZip.Text, ddlPlec.SelectedValue, int.Parse(Session["ClieId"].ToString()), int.Parse(Session["IdUser"].ToString()));
                Session["SaveSuccess"] = "1";
                Response.Redirect("~/Accounts/Manage.aspx");
            }
        }

        private void UpdateClient(string tbxBDate, string tbxImię, string tbxNazwisko, string tbxNip, string tbxStreet, string tbxHome, string tbxCity, string tbxZip, string ddlPlec, int ClieId, int UserId)
        {
            ADOHelper helper = new ADOHelper("client_update");
            helper.AddParameter("@clie_id", ClieId);
            helper.AddParameter("@clie_bdate", tbxBDate);
            helper.AddParameter("@user_fname", tbxImię);
            helper.AddParameter("@user_lname", tbxNazwisko);
            helper.AddParameter("@clie_nip", tbxNip);
            helper.AddParameter("@clie_street", tbxStreet);
            helper.AddParameter("@clie_home", tbxHome);
            helper.AddParameter("@clie_city", tbxCity);
            helper.AddParameter("@clie_zip", tbxZip);
            helper.AddParameter("@clie_sex", ddlPlec);
            helper.AddParameter("@clie_user_id", UserId);
            helper.ExecuteNonQuery();
        }

        //private void SaveClient(string UserId, string dateTime, string ddlPlec, string tbxNip, string tbxStreet, string tbxHome, string tbxCity, string tbxZip, int IdUser)
        //{
        //    ADOHelper helper = new ADOHelper("client_update");
        //    helper.AddParameter("@clie_user_id", UserId);
        //    helper.AddParameter("@clie_bdate", dateTime);
        //    helper.AddParameter("@clie_sex", ddlPlec);
        //    helper.AddParameter("@clie_saldo", 0);
        //    helper.AddParameter("@clie_nip", tbxNip);
        //    helper.AddParameter("@clie_street", tbxStreet);
        //    helper.AddParameter("@clie_home", tbxHome);
        //    helper.AddParameter("@clie_city", tbxCity);
        //    helper.AddParameter("@clie_zip", tbxZip);
        //    helper.AddParameter("@clie_user_mod_id", IdUser);
        //    helper.ExecuteNonQuery();
        //}

        //private string SaveUser(string tbxFname, string tbxLname, string tbxLogin, int IdUser)
        //{
        //    ADOHelper helper = new ADOHelper("user_update");
        //    helper.AddParameter("@user_grou_id", 6);
        //    helper.AddParameter("@user_fname", tbxFname);
        //    helper.AddParameter("@user_lname", tbxLname);
        //    helper.AddParameter("@user_login", tbxLogin);
        //    helper.AddParameter("@user_user_mod_id", IdUser);
        //    var a = helper.ExecuteScalar();
        //    return a.ToString();
        //}
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

        //[WebMethod(true)]
        //public static Result SavePass(string tbxPasswordOld, string tbxPasswordNew)
        //{
        //    Result retVal = new Result { errorMessage = "" };
        //    ADOHelper helper = new ADOHelper("users_get");
        //    DataTable dt = helper.Execute();

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (dt.Rows[i]["user_login"].ToString() == HttpContext.Current.Session["UserLogin"].ToString())
        //        {
        //            if (Decode.Decrypt(dt.Rows[i]["user_password"].ToString(), CommonConst.MilaKey) == tbxPasswordOld && Decode.Decrypt(dt.Rows[i]["user_password"].ToString(), CommonConst.MilaKey) != tbxPasswordNew)
        //            {
        //                try
        //                {
        //                    ControlProject.SavePassword(tbxPasswordNew, int.Parse(HttpContext.Current.Session["IdUser"].ToString()), int.Parse(HttpContext.Current.Session["IdUser"].ToString()));
        //                    retVal.success = true;
        //                }
        //                catch
        //                {
        //                    retVal.errorMessage = "Błąd wystąpił w procedurze";
        //                }
        //            }
        //            else retVal.errorMessage = "Stare hasło jest niepoprawne lub nowe hasło nie różni się od starego!";
        //        }
        //    }
        //    return retVal;
        //}



    }
}