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
    public partial class AddProduct : System.Web.UI.Page
    {
        ControlProject ControlProject = new ControlProject();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ADOHelper helper = new ADOHelper("categories_get");
                DataTable dt = helper.Execute();
                ddlCategories.DataTextField = "cate_name";
                ddlCategories.DataValueField = "cate_id";
                ddlCategories.DataSource = dt;
                ddlCategories.DataBind();
                ControlProject.VisibleMenu(true,this);
                ControlProject.CheckSession(this);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                decimal.Parse(tbxPrice.Text);
            }
            catch 
            {
                FailureText.Text = "Podana cena zawiera znaki niedozwolone";
                return;
            }
            if (tbxNazwa.Text.Length < 2) FailureText.Text = "Nazwa produktu jest zbyt krótka";
            else if (tbxIlosc.Text.Length < 1) FailureText.Text = "Ilość jest pusta";
            else if (tbxPrice.Text.Length < 1 || decimal.Parse(tbxPrice.Text) < 1 || decimal.Parse(tbxPrice.Text)>1000) 
                FailureText.Text = "Podana cena jest nieprawidłowa";
            else if (tbxOpis.Text.Length < 1) FailureText.Text = "Opis produktu jest pusty";
            else if (tbxPath.Text.Length < 1) FailureText.Text = "Podaj lokalizacje produktu";
            else
            {
                FailureText.Text = "";
                SaveProduct(tbxNazwa.Text, tbxIlosc.Text, decimal.Parse(tbxPrice.Text), cbBanner.Checked, tbxOpis.Text, tbxPath.Text, 
                            int.Parse(ddlCategories.SelectedValue), int.Parse(Session["IdUser"].ToString()));
                Session["SaveSuccess"] = "1";
                Response.Redirect("~/Storehouse/ProductsReport.aspx");
            }            
        }

        private void SaveProduct(string tbxNazwa, string tbxIlosc, decimal tbxPrice, bool cbBanner, string tbxOpis, string tbxPath, int ddlCategories, int IdUser)
        {
            ADOHelper helper = new ADOHelper("product_add");
            helper.AddParameter("@prod_name", tbxNazwa);
            helper.AddParameter("@prod_quantity", tbxIlosc);
            helper.AddParameter("@prod_price", tbxPrice);
            helper.AddParameter("@prod_banner", cbBanner);
            helper.AddParameter("@prod_desc", tbxOpis);
            helper.AddParameter("@prod_path", tbxPath);
            helper.AddParameter("@prod_cate_id", ddlCategories);
            helper.AddParameter("@prod_user_id", IdUser);
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