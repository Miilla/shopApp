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

namespace ShopCopyForXML.Sale
{
    public partial class AddOrder : System.Web.UI.Page
    {
        int licznik = 0;
        ControlProject ControlProject = new ControlProject();
        protected void Page_Load(object sender, EventArgs e)
        {
            ADOHelper helper = new ADOHelper("categories_get");
            DataTable dt = helper.Execute();
            if (!IsPostBack)
            {
                
                ddlCategories.DataTextField = "cate_name";
                ddlCategories.DataValueField = "cate_id";
                ddlCategories.DataSource = dt;
                ddlCategories.DataBind();

                helper = new ADOHelper("client_for_order_get");
                dt = helper.Execute();
                ddlClient.DataTextField = "name";
                ddlClient.DataValueField = "id";
                ddlClient.DataSource = dt;
                ddlClient.DataBind();


                ControlProject.VisibleMenu(true,this);
                ControlProject.CheckSession(this);

                if (ViewState["Products"] == null)
                {
                    DataTable dtProducts = new DataTable();
                    dtProducts.Columns.AddRange(new DataColumn[3] { new DataColumn("ProductId", typeof(int)),
                                    new DataColumn("ProductName", typeof(string)),new DataColumn("ProductPrice", typeof(string)) });
                    //dtProducts.Columns["FruitId"].AutoIncrement = true;
                    //dtProducts.Columns["FruitId"].AutoIncrementSeed = 1;
                    //dtProducts.Columns["FruitId"].AutoIncrementStep = 1;
                    ViewState["Products"] = dtProducts;
                }
            }
            helper = new ADOHelper("products_get");
            helper.AddParameter("@banner", 1);
            helper.AddParameter("@prod_quantity", 0);
            dt = helper.Execute();
            gvMessageInfo.DataSource = dt;
            gvMessageInfo.DataBind();
            lbProducts.DataBind();
        }
        protected void gvMessageInfo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow row = gvMessageInfo.Rows[e.NewSelectedIndex];

            DataTable dtProducts = (DataTable)ViewState["Products"];
            dtProducts.Rows.Add();
            dtProducts.Rows[dtProducts.Rows.Count - 1]["ProductName"] = licznik + 1 + ". ID: " + row.Cells[1].Text + ". Nazwa: " + row.Cells[2].Text + ". Ilość: 1" + ". Cena: " + row.Cells[5].Text;
            dtProducts.Rows[dtProducts.Rows.Count - 1]["ProductId"] = int.Parse(row.Cells[1].Text);
            dtProducts.Rows[dtProducts.Rows.Count - 1]["ProductPrice"] = row.Cells[5].Text;
            //txtFruit.Text = string.Empty;
            ViewState["Products"] = dtProducts;

            lbProducts.DataSource = dtProducts;
            lbProducts.DataTextField = "ProductName";
            lbProducts.DataValueField = "ProductId";
            lbProducts.DataBind();
            //tbxProducts.Text = tbxProducts.Text +licznik+1+ ". ID: " + row.Cells[1].Text + ". Nazwa: " + row.Cells[2].Text + ". Ilość: 1" + ". Cena: " + row.Cells[5].Text + "\n"; 

            //lbProducts.Items.Add(//tbxProducts.Text +
            //    licznik + 1 + ". ID: " + row.Cells[1].Text + ". Nazwa: " + row.Cells[2].Text + ". Ilość: 1" + ". Cena: " + row.Cells[5].Text + "\n");
            licznik = licznik + 1;
            decimal suma=0;
            if (tbxSum.Text != "") 
            {
                
                suma = decimal.Parse(tbxSum.Text) + decimal.Parse(row.Cells[5].Text);
                tbxSum.Text =suma.ToString();
            }                
            else tbxSum.Text = row.Cells[5].Text;
            ChangeColumns(sender);
        }
        protected void gvMessageInfo_DataBound(object sender, EventArgs e)
        {
            //if(gvMessageInfo.Columns.Count>1)
            //{
            //    gvMessageInfo.Columns[0].Visible = false;
            //}
            ChangeColumns(sender);

            foreach (GridViewRow row in gvMessageInfo.Rows)
            {
                row.Cells[1].Visible = false;
                //row.Cells[5].Visible = false;
                row.Cells[6].Visible = false;
                row.Cells[7].Visible = false;
                row.Cells[8].Visible = false;
                row.Cells[4].Text = ddlCategories.Items.FindByValue(row.Cells[4].Text).Text;


                //if (row.Cells[5].Text == "1")
                //    row.Cells[5].Text = "Tak";
            }
        }
        protected void ChangeColumns(object sender)
        {
            GridView gridView = (GridView)sender;

            if (gridView.HeaderRow != null && gridView.HeaderRow.Cells.Count > 0)
            {
                gridView.HeaderRow.Cells[1].Visible = false;
                //gridView.HeaderRow.Cells[5].Visible = false;
                gridView.HeaderRow.Cells[6].Visible = false;
                gridView.HeaderRow.Cells[7].Visible = false;
                gridView.HeaderRow.Cells[8].Visible = false;
                gridView.HeaderRow.Cells[2].Text = "Nazwa";
                gridView.HeaderRow.Cells[3].Text = "Ilość";
                gridView.HeaderRow.Cells[4].Text = "Kategoria";
                gridView.HeaderRow.Cells[5].Text = "Cena";
                gridView.HeaderRow.Cells[9].Text = "Modyfikujący";
                gridView.HeaderRow.Cells[10].Text = "Zmodyfikowany";
            }
        }


        protected void gvMessageInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Success.Text = "";
            gvMessageInfo.PageIndex = e.NewPageIndex;
            //pProduct.Visible = false;
            //gvMessageInfo.SelectRow(0);
            gvMessageInfo.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string orderID = SaveOrder( int.Parse(ddlClient.SelectedValue), int.Parse(Session["IdUser"].ToString()));

            DataTable dtProducts = (DataTable)ViewState["Products"];
            foreach (ListItem item in lbProducts.Items)
            {
                    DataRow[] rows = dtProducts.Select("ProductId = " + item.Value);
                    int IdProduktu  = int.Parse(rows[0]["ProductId"].ToString());
                    SaveProductInOrder(orderID, IdProduktu, int.Parse(Session["IdUser"].ToString()));
            }
            FailureText.Text = "";
            Session["SaveSuccess"] = "1";
            Response.Redirect("~/Sale/OrderReportSale.aspx");
            //for (int i = 0; i < lbProducts.Items.Count; i++)
            //{
            //    lbProducts.Items[i][];
            //}
            //try
            //{
            //    decimal.Parse(tbxPrice.Text);
            //}
            //catch 
            //{
            //    FailureText.Text = "Podana cena zawiera znaki niedozwolone";
            //    return;
            //}
            //if (tbxNazwa.Text.Length < 2) FailureText.Text = "Nazwa produktu jest zbyt krótka";
            //else if (tbxIlosc.Text.Length < 1) FailureText.Text = "Ilość jest pusta";
            //else if (tbxPrice.Text.Length < 1 || decimal.Parse(tbxPrice.Text) < 1 || decimal.Parse(tbxPrice.Text)>1000) FailureText.Text = "Podana cena jest nieprawidłowa";
            //else if (tbxOpis.Text.Length < 1) FailureText.Text = "Opis produktu jest pusty";
            //else if (tbxPath.Text.Length < 1) FailureText.Text = "Podaj lokalizacje produktu";
            //else
            //{
            //    FailureText.Text = "";
            //    SaveProduct(tbxNazwa.Text, tbxIlosc.Text, decimal.Parse(tbxPrice.Text), cbBanner.Checked, tbxOpis.Text, tbxPath.Text, int.Parse(ddlCategories.SelectedValue), int.Parse(Session["IdUser"].ToString()));
            //    Session["SaveSuccess"] = "1";
            //    Response.Redirect("~/Storehouse/ProductsReport.aspx");
            //}

            
        }

        private void SaveProductInOrder(string orderID, int IdProduktu, int IdUser)
        {
            ADOHelper helper = new ADOHelper("order_product_insert");
            helper.AddParameter("@orde_id", orderID);
            helper.AddParameter("@prod_id", IdProduktu);
            helper.AddParameter("@user_id", IdUser);
            helper.ExecuteNonQuery();

        }

        private string SaveOrder(int ddlClient, int IdUser)
        {
            ADOHelper helper = new ADOHelper("order_insert");
            helper.AddParameter("@clie_id", ddlClient);
            helper.AddParameter("@user_id", IdUser);
            var a = helper.ExecuteScalar();
            return a.ToString();
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

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            //lbProducts.Items.Remove(lbProducts.SelectedIndex);
            //if (this.lbProducts.SelectedIndex >= 0)
            //    this.lbProducts.Items.RemoveAt(this.lbProducts.SelectedIndex);
            //for (int i = 0; i < lbProducts.SelectedItems.Count; i++)
            //{
            //    listBox1.Items.Remove(listBox1.SelectedItems[i].ToString());
            //    i--;
            //}
            decimal price = 0;
            DataTable dtProducts = (DataTable)ViewState["Products"];
            foreach (ListItem item in lbProducts.Items)
            {
                if (item.Selected)
                {
                    DataRow[] rows = dtProducts.Select("ProductId = " + item.Value);
                    price = decimal.Parse(rows[0]["ProductPrice"].ToString());
                    dtProducts.Rows.Remove(rows[0]);
                }
            }
            dtProducts.AcceptChanges();
            ViewState["Products"] = dtProducts;

            decimal suma = 0;
            if (tbxSum.Text != "")
            {

                suma = decimal.Parse(tbxSum.Text) - price;
                tbxSum.Text = suma.ToString();
            }
            else tbxSum.Text = "0";

            lbProducts.DataSource = dtProducts;
            lbProducts.DataTextField = "ProductName";
            lbProducts.DataValueField = "ProductId";
            lbProducts.DataBind();
        }


        
    }
}