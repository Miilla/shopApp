using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace ShopCopyForXML.Storehouse
{
    public partial class ProductsReport : System.Web.UI.Page
    {
        ControlProject ControlProject = new ControlProject();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["SaveSuccess"].ToString() == "1")
                    Success.Text = "Element został dodany";
            }
            catch 
            {
                
            }
            Session["SaveSuccess"]= "0";
            ControlProject.CheckSession(this);
            ControlProject.VisibleMenu(true, this);
            Status.Text = "";


            

            if (!IsPostBack)
            {
                GetData();
            }
            ADOHelper helper = new ADOHelper("products_get");
            helper.AddParameter("@banner", 0);
            helper.AddParameter("@prod_quantity", -1);
            DataTable dt = helper.Execute();
            gvMessageInfo.DataSource = dt;
            gvMessageInfo.DataBind();
        }

        private  void GetData()
        {
            ADOHelper helper = new ADOHelper("categories_get");
            DataTable dt = helper.Execute();


            ddlCategories.DataTextField = "cate_name";
            ddlCategories.DataValueField = "cate_id";
            ddlCategories.DataSource = dt;
            ddlCategories.DataBind();
            //dt.Columns.Remove("user_password");
            //dt.Columns.Remove("user_date_pass");
        }
        private void GetData2()
        {
            ADOHelper helper = new ADOHelper("products_get");
            helper.AddParameter("@banner", 0);
            helper.AddParameter("@prod_quantity", -1);
            DataTable dt = helper.Execute();
            gvMessageInfo.DataSource = dt;
            gvMessageInfo.DataBind();
        }

        protected void gvMessageInfo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Success.Text = "";
            //lbtnData.BorderWidth = 1;
            //lbtnPassword.BorderWidth = 0;
            dData.Visible = true;
            //dPassword.Visible = false;
            GridViewRow row = gvMessageInfo.Rows[e.NewSelectedIndex];
            pProduct.Visible = true;
            tbxNazwa.Text = row.Cells[2].Text;
            tbxIlosc.Text = row.Cells[3].Text;
            tbxPrice.Text = row.Cells[5].Text;
            tbxOpis.Text = row.Cells[6].Text;
            if (row.Cells[7].Text == "1") cbBanner.Checked = true;
            else cbBanner.Checked = false;
            tbxPath.Text = row.Cells[8].Text;
            //tbxLogin.Text = row.Cells[6].Text;
            string value = ddlCategories.Items.FindByText(row.Cells[4].Text).Value;
            ddlCategories.SelectedValue= value;
            setSelectedRow(row.Cells[1].Text);//, row.Cells[6].Text);

            ChangeColumns(sender);
        }

        private void setSelectedRow(string ProductId)//,string LoginToChange)
        {
            Session["ProductId"] = ProductId;
            //Session["LoginToChange"] = LoginToChange;
        }

        protected void gvMessageInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells.Count > 2)
            {
                
            }

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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateProduct(int.Parse(Session["ProductId"].ToString()), tbxNazwa.Text, tbxIlosc.Text, decimal.Parse(tbxPrice.Text), cbBanner.Checked, tbxOpis.Text, tbxPath.Text, int.Parse(ddlCategories.SelectedValue), int.Parse(Session["IdUser"].ToString()));
                //SaveUserData(tbxImię.Text, tbxLogin.Text, tbxNazwisko.Text, cbBanner.Checked, ddlGroup.SelectedValue, int.Parse(Session["UserIdToChange"].ToString()), int.Parse(Session["IdUser"].ToString()));
                SwitchDiv(true);
                Status.Text = "Dane zostały zmienione";
                GetData2();
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
            Success.Text = "";
            pProduct.Visible=false;
            gvMessageInfo.PageIndex = e.NewPageIndex;
            gvMessageInfo.DataBind();
        }

    }

}