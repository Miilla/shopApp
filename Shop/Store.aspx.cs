using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace ShopCopyForXML.Shop
{
    public partial class Store : System.Web.UI.Page
    {
        ControlProject ControlProject = new ControlProject();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["SaveSuccess"].ToString() == "1")
                    Success.Text = "Produkt został dodany";
            }
            catch 
            {
                
            }
            Session["SaveSuccess"]= "0";
            ControlProject.CheckSession(this);
            ControlProject.VisibleMenu(true, this);
            Status.Text = "";
            DataTable dt = GetData();
            Loader(dt);
            if (!IsPostBack)
            {
            }
        }

        private void Loader(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {

            }
            try
            {
                image1.Width = 200;
                image1.Height = 200;
                image1.BorderColor = Color.Black;
                image1.BorderWidth = 1;
                image1.ImageUrl = "~/Images/" + dt.Rows[0]["prod_path"] + ".jpg";
                l1.Text = dt.Rows[0]["prod_name"].ToString();
                desc1.Text = dt.Rows[0]["prod_desc"].ToString();
                price1.Text = dt.Rows[0]["prod_price"].ToString() + " PLN";

                image2.Width = 200;
                image2.Height = 200;
                image2.BorderColor = Color.Black;
                image2.BorderWidth = 1;
                image2.ImageUrl = "~/Images/" + dt.Rows[1]["prod_path"] + ".jpg";
                l2.Text = dt.Rows[1]["prod_name"].ToString();
                desc2.Text = dt.Rows[1]["prod_desc"].ToString();
                price2.Text = dt.Rows[1]["prod_price"].ToString() + " PLN";

                image3.Width = 200;
                image3.Height = 200;
                image3.BorderColor = Color.Black;
                image3.BorderWidth = 1;
                image3.ImageUrl = "~/Images/" + dt.Rows[2]["prod_path"] + ".jpg";
                l3.Text = dt.Rows[2]["prod_name"].ToString();
                desc3.Text = dt.Rows[2]["prod_desc"].ToString();
                price3.Text = dt.Rows[2]["prod_price"].ToString() + " PLN";

                image4.Width = 200;
                image4.Height = 200;
                image4.BorderColor = Color.Black;
                image4.BorderWidth = 1;
                image4.ImageUrl = "~/Images/" + dt.Rows[3]["prod_path"] + ".jpg";
                l4.Text = dt.Rows[3]["prod_name"].ToString();
                desc4.Text = dt.Rows[3]["prod_desc"].ToString();
                price4.Text = dt.Rows[3]["prod_price"].ToString() + " PLN";

                image5.Width = 200;
                image5.Height = 200;
                image5.BorderColor = Color.Black;
                image5.BorderWidth = 1;
                image5.ImageUrl = "~/Images/" + dt.Rows[4]["prod_path"] + ".jpg";
                l5.Text = dt.Rows[4]["prod_name"].ToString();
                desc5.Text = dt.Rows[4]["prod_desc"].ToString();
                price5.Text = dt.Rows[4]["prod_price"].ToString() + " PLN";
            }
            catch { }


        }

        private  DataTable GetData()
        {
            ADOHelper helper = new ADOHelper("categories_get");
            DataTable dt = helper.Execute();


            //ddlCategories.DataTextField = "cate_name";
            //ddlCategories.DataValueField = "cate_id";
            //ddlCategories.DataSource = dt;
            //ddlCategories.DataBind();
            Session["categories"] = dt;
            //dt.Columns.Remove("user_password");
            //dt.Columns.Remove("user_date_pass");
            helper = new ADOHelper("products_get");
            helper.AddParameter("@banner", 1);
            helper.AddParameter("@prod_quantity", 0);
            dt = helper.Execute();
            //gvMessageInfo.DataSource = dt;
            //gvMessageInfo.DataBind();
            return dt;
        }

        protected void gvMessageInfo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //Success.Text = "";
            ////lbtnData.BorderWidth = 1;
            ////lbtnPassword.BorderWidth = 0;
            //dData.Visible = true;
            ////dPassword.Visible = false;
            //GridViewRow row = gvMessageInfo.Rows[e.NewSelectedIndex];
            //pProduct.Visible = true;
            //tbxNazwa.Text = row.Cells[2].Text;
            //tbxIlosc.Text = row.Cells[3].Text;
            //tbxPrice.Text = row.Cells[5].Text;
            //tbxOpis.Text = row.Cells[6].Text;
            //if (row.Cells[7].Text == "1") cbBanner.Checked = true;
            //else cbBanner.Checked = false;
            //tbxPath.Text = row.Cells[8].Text;
            //image.Width = 220;
            //image.Height = 200;
            //image.BorderColor = Color.Black;
            //image.BorderWidth = 1;
            //image.ImageUrl = "~/Images/" + row.Cells[8].Text + ".jpg";
            ////tbxLogin.Text = row.Cells[6].Text;
            //ddlCategories.SelectedItem.Text = row.Cells[4].Text;
            //setSelectedRow(row.Cells[1].Text);


            //ADOHelper helper = new ADOHelper("product_history_get");
            //helper.AddParameter("@prod_id", row.Cells[1].Text);
            //DataTable dt = helper.Execute();
            //gvHistory.DataSource = dt;
            //gvHistory.DataBind();

            //ChangeColumns(sender);
        }

        private void setSelectedRow(string ProductId)//,string LoginToChange)
        {
            Session["ProductId"] = ProductId;
            //Session["LoginToChange"] = LoginToChange;
        }

        protected void gvMessageInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string img = ((System.Web.UI.WebControls.Image)e.Row.FindControl("Image1")).ImageUrl;
                //string[] ext = img.Split('.');
                //if (ext.Length == 1)
                //{
                //    ((System.Web.UI.WebControls.Image)e.Row.FindControl("Image1")).Visible = false;
                //}
                //else
                //{
                //    ((System.Web.UI.WebControls.Image)e.Row.FindControl("Image1")).Visible = true;
                //}


                //System.Web.UI.WebControls.Image img = e.Row.Cells[0].Controls[0] as System.Web.UI.WebControls.Image;
                //img.ImageUrl = "~/Images/" + DataBinder.Eval(e.Row.DataItem, "prod_path") + ".jpg";
            }
            //gvMessageInfo.DataBind();

        }

        protected void gvMessageInfo_DataBound(object sender, EventArgs e)
        {
            //if(gvMessageInfo.Columns.Count>1)
            //{
            //    gvMessageInfo.Columns[0].Visible = false;
            //}
            ChangeColumns(sender);

            //foreach (GridViewRow row in gvMessageInfo.Rows)
            //{
            //    row.Cells[1].Visible = false;
            //    //row.Cells[5].Visible = false;
            //    row.Cells[6].Visible = false;
            //    row.Cells[7].Visible = false;
            //    row.Cells[8].Visible = false;
            //    row.Cells[4].Text = ddlCategories.Items.FindByValue(row.Cells[4].Text).Text;


            //    //if (row.Cells[5].Text == "1")
            //    //    row.Cells[5].Text = "Tak";
            //}
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateProduct(int.Parse(Session["ProductId"].ToString()), tbxNazwa.Text, tbxIlosc.Text, decimal.Parse(tbxPrice.Text), cbBanner.Checked, tbxOpis.Text, tbxPath.Text, int.Parse(ddlCategories.SelectedValue), int.Parse(Session["IdUser"].ToString()));
                //SaveUserData(tbxImię.Text, tbxLogin.Text, tbxNazwisko.Text, cbBanner.Checked, ddlGroup.SelectedValue, int.Parse(Session["UserIdToChange"].ToString()), int.Parse(Session["IdUser"].ToString()));
                SwitchDiv(true);
                Status.Text = "Dane zostały zmienione";
                GetData();
            }
            catch 
            {
                FailureText.Text = "Błąd wystąpił w procedurze";
            }
            
        }


        private void UpdateProduct(int ProductId, string tbxNazwa, string tbxIlosc, decimal tbxPrice, bool cbBanner, string tbxOpis, string tbxPath, int ddlCategories, int IdUser)
        {
            ADOHelper helper = new ADOHelper("product_update");
            helper.AddParameter("@prod_id", ProductId);
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

        private void SaveUserData(string tbxImię, string tbxLogin, string tbxNazwisko, bool cbActive, string ddlGroup,int UserIdtoChange, int UserId)
        {
            ADOHelper helper = new ADOHelper("user_data_save");
            helper.AddParameter("@user_fname", tbxImię);
            helper.AddParameter("@user_login", tbxLogin);
            helper.AddParameter("@user_lname", tbxNazwisko);
            helper.AddParameter("@user_active", cbActive ? 1 : 0);
            helper.AddParameter("@user_grou_id", ddlGroup);
            helper.AddParameter("@user_id", UserIdtoChange);
            helper.AddParameter("@user_user_mod", UserId);
            helper.ExecuteNonQuery();
        }

       
        protected void lbtnData_Click(object sender, EventArgs e)
        {
            //SwitchDiv(true);
        }

        private  void SwitchDiv( bool switchDiv)
        {
            //ChangeColumns(gvMessageInfo);
            //lbtnData.BorderWidth = (switchDiv) ? 1 : 0;//.Style.Add("", "");
            //lbtnPassword.BorderWidth = (!switchDiv) ? 1 : 0;
            //dData.Visible = switchDiv;
            //dPassword.Visible = !switchDiv;
        }

        protected void lbtnPassword_Click(object sender, EventArgs e)
        {
            SwitchDiv(false);
        }

        protected void btnSavePswd_Click(object sender, EventArgs e)
        {
            ADOHelper helper = new ADOHelper("users_get");
            DataTable dt = helper.Execute();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["user_login"].ToString() == Session["LoginToChange"].ToString())
                {
                    if (Decode.Decrypt(dt.Rows[i]["user_password"].ToString(), CommonConst.MilaKey) != tbxPasswordNew.Text)
                    {
                        try
                        {
                            ControlProject.SavePassword(tbxPasswordNew.Text, int.Parse(Session["UserIdToChange"].ToString()), int.Parse(Session["IdUser"].ToString()));
                            SwitchDiv(true);
                            Status.Text = "Hasło zostało zmienione";
                            GetData();
                            //return true;
                        }
                        catch
                        {
                            FailureText.Text= "Błąd wystąpił w procedurze";
                        }
                    }
                    else FailureText.Text = "Nowe hasło nie różni się od starego!";
                }
            }
            //return retVal;
        }

        protected void gvMessageInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Success.Text = "";
            //gvMessageInfo.PageIndex = e.NewPageIndex;
            //pProduct.Visible = false;
            ////gvMessageInfo.SelectRow(0);
            //gvMessageInfo.DataBind();
        }


        protected void gvHistory_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void gvHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvHistory_DataBound(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["categories"];
            GridView gridView = (GridView)sender;

            if (gridView.HeaderRow != null && gridView.HeaderRow.Cells.Count > 0)
            {
                gridView.HeaderRow.Cells[0].Text = "Akcja";
                gridView.HeaderRow.Cells[1].Text = "Wersja";
                gridView.HeaderRow.Cells[2].Visible = false;
                gridView.HeaderRow.Cells[3].Text = "Nazwa";
                gridView.HeaderRow.Cells[4].Text = "Ilość";
                gridView.HeaderRow.Cells[5].Text = "Kategoria";
                gridView.HeaderRow.Cells[6].Text = "Cena";
                gridView.HeaderRow.Cells[7].Text = "Opis";
                gridView.HeaderRow.Cells[7].Visible = false;
                gridView.HeaderRow.Cells[8].Text = "Banner";
                //gridView.HeaderRow.Cells[8].Visible = false;
                gridView.HeaderRow.Cells[9].Text = "Lokalizacja";
                gridView.HeaderRow.Cells[10].Text = "Modyfikujący";
                gridView.HeaderRow.Cells[11].Text = "Zmodyfikowany";
            }
            foreach (GridViewRow row in gridView.Rows)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (row.Cells[5].Text == dt.Rows[i]["cate_id"].ToString()) row.Cells[5].Text = dt.Rows[i]["cate_name"].ToString();
                }
                if (row.Cells[0].Text == "ins") row.Cells[0].Text = "Dodanie";
                else if (row.Cells[0].Text == "upd") row.Cells[0].Text = "Aktualizacja";
                else row.Cells[0].Text = "Usunięcie";
                row.Cells[2].Visible = false;
                row.Cells[7].Visible = false;
                if (row.Cells[8].Text == "1")
                    row.Cells[8].Text = "Tak";
                else row.Cells[8].Text = "Nie";
            }
        }

    }

}