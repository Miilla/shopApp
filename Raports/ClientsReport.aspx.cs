using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
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


namespace ShopCopyForXML.Raports
{
    public partial class ClientsReport : System.Web.UI.Page
    {
        ControlProject ControlProject = new ControlProject();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["SaveSuccess"].ToString() == "1")
                    Success.Text = "Klient został dodany";
            }
            catch
            {

            }
            Session["SaveSuccess"] = "0";
            ControlProject.CheckSession(this);
            ControlProject.VisibleMenu(true, this);
            Status.Text = "";
            GetData();
            LoadCharts();
            if (!IsPostBack)
            {
            }
        }

        private void LoadCharts()
        {
            Series seriesClients2011 = new Series();
            seriesClients2011.Data = new Data(new object[]
                        {
                            41, 33, 38, 29, 81, 76, 96, 87, 68, 37,
                            36.7, 32.2
                        });

            seriesClients2011.Name = "2011";
            seriesClients2011.Type = ChartTypes.Column;

            Series seriesClients2012 = new Series();
            seriesClients2012.Data = new Data(new object[]
                        {
                            42, 33, 38, 29, 81, 76, 96, 87, 68, 37,
                            36, 32
                        });

            seriesClients2012.Name = "2012";
            seriesClients2012.Type = ChartTypes.Column;


            Series[] series = new Series[] { seriesClients2011, seriesClients2012 };

            Highcharts chart = new Highcharts("bar")
                .SetTitle(new Title
                {
                    Text = "Dodani klienci względem miesięcy"
                })
                .SetSubtitle(new Subtitle
                {
                    Text = "Rok 2011 i 2012"
                })
                .SetXAxis(new XAxis
                {
                    Categories =
                        new[]
                            {
                                "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpien", "Wrzesień", "Pażdziernik", "Listopad",
                                "Grudzień"
                            }
                })

                .SetSeries(series);

            ltChart.Text = chart.ToHtmlString();
        }

        private void GetData()
        {
            ADOHelper helper = new ADOHelper("categories_get");
            DataTable dt = helper.Execute();
            helper = new ADOHelper("clients_get");
            dt = helper.Execute();

            gvMessageInfo.DataSource = dt;
            gvMessageInfo.DataBind();

            helper = new ADOHelper("groups_get");
            dt = helper.Execute();
            Session["groups"] = dt;
            ddlGroup.DataTextField = "grou_name";
            ddlGroup.DataValueField = "grou_id";
            ddlGroup.DataSource = dt;
            ddlGroup.DataBind();
        }

        protected void gvMessageInfo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Success.Text = "";
            dData.Visible = true;
            GridViewRow row = gvMessageInfo.Rows[e.NewSelectedIndex];
            pClient.Visible = true;
            tbxBDate.Text = row.Cells[3].Text.Substring(0, 10);
            tbxImię.Text = row.Cells[4].Text;
            tbxNazwisko.Text = row.Cells[5].Text;
            tbxSaldo.Text = row.Cells[7].Text;
            tbxNip.Text = row.Cells[8].Text;
            tbxStreet.Text = row.Cells[9].Text;
            tbxHome.Text = row.Cells[10].Text;
            tbxCity.Text = row.Cells[11].Text;
            tbxZip.Text = row.Cells[12].Text;
            if (row.Cells[6].Text == "Mężczyzna") ddlPlec.SelectedValue = "m";
            else ddlPlec.SelectedValue = "k";
            ddlGroup.SelectedValue = row.Cells[15].Text;
            cbActive.Checked = row.Cells[16].Text=="1" ? true : false;
            tbxLogin.Text = row.Cells[17].Text;
            Session["ClieId"] = row.Cells[1].Text;
            ADOHelper helper = new ADOHelper("client_history_get");
            helper.AddParameter("@clie_id", row.Cells[1].Text);
            DataTable dt = helper.Execute();
            gvHistory.DataSource = dt;
            gvHistory.DataBind();
            SwitchDiv(true);

            helper = new ADOHelper("user_history_get");
            helper.AddParameter("@user_id", row.Cells[2].Text);
             dt = helper.Execute();
            gvHistoryUser.DataSource = dt;
            gvHistoryUser.DataBind();

            ChangeColumns(sender);
        }

        private void setSelectedRow(string ProductId)
        {
            Session["ProductId"] = ProductId;
        }

        protected void gvMessageInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells.Count > 2)
            {

            }

        }

        protected void gvMessageInfo_DataBound(object sender, EventArgs e)
        {
            ChangeColumns(sender);

            foreach (GridViewRow row in gvMessageInfo.Rows)
            {
                row.Cells[0].Visible = false;
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[13].Visible = false;
                row.Cells[14].Visible = false;
                row.Cells[15].Visible = false;
                row.Cells[16].Visible = false;
                row.Cells[17].Visible = false;
                if (row.Cells[6].Text == "m")
                    row.Cells[6].Text = "Mężczyzna";
                else row.Cells[6].Text = "Kobieta";
            }
        }

        protected void ChangeColumns(object sender)
        {
            GridView gridView = (GridView)sender;

            if (gridView.HeaderRow != null && gridView.HeaderRow.Cells.Count > 0)
            {
                gridView.HeaderRow.Cells[0].Visible = false;
                gridView.HeaderRow.Cells[1].Visible = false;
                gridView.HeaderRow.Cells[2].Visible = false;
                gridView.HeaderRow.Cells[3].Visible = false;
                gridView.HeaderRow.Cells[4].Text = "Imię";
                gridView.HeaderRow.Cells[5].Text = "Nazwisko";
                gridView.HeaderRow.Cells[6].Text = "Płeć";
                gridView.HeaderRow.Cells[7].Text = "Saldo";
                gridView.HeaderRow.Cells[8].Text = "Nip";

                gridView.HeaderRow.Cells[9].Text = "Ulica";
                gridView.HeaderRow.Cells[10].Text = "Nr domu";
                gridView.HeaderRow.Cells[11].Text = "Misto";
                gridView.HeaderRow.Cells[12].Text = "Kod pocztowy";
                
                gridView.HeaderRow.Cells[13].Text = "Modyfikujący";
                gridView.HeaderRow.Cells[14].Text = "Zmodyfikowany";
                gridView.HeaderRow.Cells[13].Visible = false;
                gridView.HeaderRow.Cells[14].Visible = false;
                gridView.HeaderRow.Cells[15].Visible = false;
                gridView.HeaderRow.Cells[16].Visible = false;
                gridView.HeaderRow.Cells[17].Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateClient(tbxBDate.Text, tbxImię.Text, tbxNazwisko.Text, tbxNip.Text, tbxStreet.Text, tbxHome.Text, tbxCity.Text, tbxZip.Text, ddlPlec.SelectedValue, int.Parse(Session["ClieId"].ToString()),int.Parse(Session["IdUser"].ToString()));
                SwitchDiv(true);
                Status.Text = "Dane zostały zmienione";
                GetData();
            }
            catch
            {
                //FailureText.Text = "Błąd wystąpił w procedurze";
            }

        }

        private void UpdateClient(string tbxBDate, string tbxImię, string tbxNazwisko, string tbxNip, string tbxStreet, string tbxHome, string tbxCity, string tbxZip, string ddlPlec,int ClieId, int UserId)
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

        private void SaveUserData(string tbxImię, string tbxLogin, string tbxNazwisko, bool cbActive, string ddlGroup, int UserIdtoChange, int UserId)
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
            SwitchDiv(true);
        }

        private void SwitchDiv(bool switchDiv)
        {
            ChangeColumns(gvMessageInfo);
            ChangeColumnsFoUser(gvHistoryUser);
            ChangeColumnsForClients(gvHistory);
            lbtnData.BorderWidth = (switchDiv) ? 1 : 0;//.Style.Add("", "");
            lbtnUser.BorderWidth = (!switchDiv) ? 1 : 0;
            dData.Visible = switchDiv;
            dUser.Visible = !switchDiv;
        }

        protected void lbtnUser_Click(object sender, EventArgs e)
        {
            SwitchDiv(false);
        }

        protected void gvMessageInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Success.Text = "";
            gvMessageInfo.PageIndex = e.NewPageIndex;
            pClient.Visible = false;
            gvMessageInfo.DataBind();
        }


        protected void gvHistory_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void gvHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvHistory_DataBound(object sender, EventArgs e)
        {
            ChangeColumnsForClients(sender);
        }

        protected void ChangeColumnsForClients(object sender)
        {
            GridView gridView = (GridView)sender;

            if (gridView.HeaderRow != null && gridView.HeaderRow.Cells.Count > 0)
            {
                gridView.HeaderRow.Cells[0].Text = "Akcja";
                gridView.HeaderRow.Cells[1].Text = "Wersja";
                gridView.HeaderRow.Cells[2].Visible = false;
                gridView.HeaderRow.Cells[3].Visible = false;
                gridView.HeaderRow.Cells[4].Text = "Data ur.";
                gridView.HeaderRow.Cells[5].Text = "Płeć";
                gridView.HeaderRow.Cells[6].Text = "Saldo";
                gridView.HeaderRow.Cells[7].Text = "Nip";
                gridView.HeaderRow.Cells[8].Text = "Ulica";
                gridView.HeaderRow.Cells[9].Text = "Dom";
                gridView.HeaderRow.Cells[10].Text = "Miasto";
                gridView.HeaderRow.Cells[11].Text = "Zip";
                gridView.HeaderRow.Cells[12].Text = "User";
                gridView.HeaderRow.Cells[13].Text = "Zmodyfikowany";
            }
            foreach (GridViewRow row in gridView.Rows)
            {
                if (row.Cells[0].Text == "ins" || row.Cells[0].Text == "Dodanie") row.Cells[0].Text = "Dodanie";
                else if (row.Cells[0].Text == "upd" || row.Cells[0].Text == "Aktualizacja") row.Cells[0].Text = "Aktualizacja";
                else row.Cells[0].Text = "Usunięcie";
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                string data = row.Cells[4].Text.Substring(0, 10);
                row.Cells[4].Text = data;
            }
        }

        protected void tbnSaveUser_Click(object sender, EventArgs e)
        {

        }

        protected void gvHistoryUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvHistoryUser_DataBound(object sender, EventArgs e)
        {
            ChangeColumnsFoUser(sender);
        }
        protected void ChangeColumnsFoUser(object sender)
        {
            DataTable dt = (DataTable)Session["groups"];
            GridView gridView = (GridView)sender;

            if (gridView.HeaderRow != null && gridView.HeaderRow.Cells.Count > 0)
            {
                gridView.HeaderRow.Cells[0].Text = "Akcja";
                gridView.HeaderRow.Cells[1].Text = "Wersja";
                gridView.HeaderRow.Cells[2].Visible = false;
                gridView.HeaderRow.Cells[3].Text = "Grupa";
                gridView.HeaderRow.Cells[4].Text = "Imię";
                gridView.HeaderRow.Cells[5].Text = "Nazwisko";
                gridView.HeaderRow.Cells[6].Text = "Aktywny";
                gridView.HeaderRow.Cells[7].Text = "Login";
                gridView.HeaderRow.Cells[8].Visible = false;
                gridView.HeaderRow.Cells[9].Text = "Data hasła";
                gridView.HeaderRow.Cells[10].Text = "Modyfikujący";
                gridView.HeaderRow.Cells[11].Text = "Zmodyfikowany";
            }
            foreach (GridViewRow row in gridView.Rows)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (row.Cells[3].Text == dt.Rows[i]["grou_id"].ToString()) row.Cells[3].Text = dt.Rows[i]["grou_name"].ToString();
                }
                if (row.Cells[0].Text == "ins" || row.Cells[0].Text == "Dodanie") row.Cells[0].Text = "Dodanie";
                else if (row.Cells[0].Text == "upd" || row.Cells[0].Text == "Aktualizacja") row.Cells[0].Text = "Aktualizacja";
                else row.Cells[0].Text = "Usunięcie";
                row.Cells[2].Visible = false;
                row.Cells[8].Visible = false;
                if (row.Cells[6].Text == "1")
                    row.Cells[6].Text = "Tak";
                else row.Cells[6].Text = "Nie";
            }
        }


    }

}