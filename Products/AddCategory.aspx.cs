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

namespace ShopCopyForXML.Products
{
    public partial class AddCategory : System.Web.UI.Page
    {
        ControlProject ControlProject = new ControlProject();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ADOHelper helper = new ADOHelper("categories_get");
                //DataTable dt = helper.Execute();
                //ddlCategories.DataTextField = "cate_name";
                //ddlCategories.DataValueField = "cate_id";
                //ddlCategories.DataSource = dt;
                //ddlCategories.DataBind();
                ControlProject.VisibleMenu(true,this);
                ControlProject.CheckSession(this);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    decimal.Parse(tbxPrice.Text);
            //}
            //catch 
            //{
            //    FailureText.Text = "Podana cena zawiera znaki niedozwolone";
            //    return;
            //}
            ADOHelper helper = new ADOHelper("categories_get");
            DataTable dt = helper.Execute();
            
            if (tbxNazwa.Text.Length < 2) 
            {
                FailureText.Text = "Nazwa kategorii jest zbyt krótka";
                return;
            }
            if (tbxNazwa.Text.Length > 49)
            {
                FailureText.Text = "Nazwa kategorii jest zbyt długa";
                return;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["cate_name"].ToString() == tbxNazwa.Text) { FailureText.Text = "Podana kategoria już istnieje!"; return; }
            }
            try
            {
                FailureText.Text = "";
                SaveCategory(tbxNazwa.Text);
                Session["SaveSuccess"] = "1";
                Response.Redirect("~/Storehouse/ProductsReport.aspx");
            }
            catch
            {
                FailureText.Text = "Błąd w procedurze";
            }
            //    ADOHelper helper = new ADOHelper("users_get");
            //    DataTable dt = helper.Execute();
            //    //String EncryptedPassword = Encrypt("lol", KeyString);
            //    //String DecryptedPassword = Decrypt(EncryptedPassword, KeyString);

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        if (dt.Rows[i]["user_login"].ToString() == Session["UserLogin"].ToString())
            //        {
            //            if (Decode.Decrypt(dt.Rows[i]["user_password"].ToString(), KeyString) == tbxPasswordOld.Text)
            //            {
            //                try
            //                {
            //                    SavePassword(tbxPasswordNew.Text, int.Parse(Session["IdUser"].ToString()));
            //                }
            //                catch 
            //                {}
            //            }
            //            else FailureText.Text = "Stare hasło jest niepoprawne!";
            //        }
            //    }

            
        }

        private void SaveCategory(string tbxNazwa)
        {
            ADOHelper helper = new ADOHelper("category_add");
            helper.AddParameter("@cate_name", tbxNazwa);
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