using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopCopyForXML
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //[System.Web.Services.WebMethod]
        //public static string SavePass(string tbxPasswordOld, string tbxPasswordNew)
        //{
        //    String KeyString = Decode.GenerateAPassKey("MilasKey");
        //    ADOHelper helper = new ADOHelper("users_get");
        //    DataTable dt = helper.Execute();
        //    //String EncryptedPassword = Encrypt("lol", KeyString);
        //    //String DecryptedPassword = Decrypt(EncryptedPassword, KeyString);

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (dt.Rows[i]["user_login"].ToString() == HttpContext.Current.Session["UserLogin"].ToString())
        //        {
        //            if (Decode.Decrypt(dt.Rows[i]["user_password"].ToString(), KeyString) == tbxPasswordOld)
        //            {
        //                try
        //                {
        //                    SavePassword(tbxPasswordNew, int.Parse(HttpContext.Current.Session["IdUser"].ToString()));
        //                }
        //                catch
        //                { }
        //            }
        //            else return "Stare hasło jest niepoprawne!";
        //            //else FailureText.Text = "Stare hasło jest niepoprawne!";
        //        }
        //    }
        //    return "done";
        //}

        //protected static void SavePassword(string pass, int userID)
        //{
        //    ADOHelper helper = new ADOHelper("user_password_save");
        //    helper.AddParameter("@user_password", Decode.Encrypt(pass, CommonConst.MilaKey));
        //    helper.AddParameter("@user_id", userID);
        //    helper.ExecuteNonQuery();
        //}
    }
}