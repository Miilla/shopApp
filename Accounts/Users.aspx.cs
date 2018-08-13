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
using System.Xml;
using System.Xml.Linq;

namespace ShopCopyForXML.Accounts
{
    public partial class Users : System.Web.UI.Page
    {
        ControlProject ControlProject = new ControlProject();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            ControlProject.CheckSession(this);
            ControlProject.VisibleMenu(true, this);
            Status.Text = "";            

            if (!IsPostBack)
            {
                GetData();
            }
            //ADOHelper helper = new ADOHelper("users_get");

            XmlNodeList xnList = CommonConst.GetDataXML("Users");
            DataTable dt = CommonConst.ConvertXmlNodeListToDataTable(xnList);

            dt.Columns.Remove("user_password");
            dt.Columns.Remove("user_date_pass");
            gvMessageInfo.DataSource = dt;
            gvMessageInfo.DataBind();
            try
            {
                if (int.Parse(Session["GroupID"].ToString()) > 5)
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
        

        private  void GetData()
        {
            XmlNodeList xnList = CommonConst.GetDataXML("Groups");
            DataTable dt = CommonConst.ConvertXmlNodeListToDataTable(xnList);
            //ADOHelper helper  = new ADOHelper("groups_get");
            //DataTable dt = helper.Execute();
            Session["groups"] = dt;
            ddlGroup.DataTextField = "grou_name";
            ddlGroup.DataValueField = "grou_id";
            ddlGroup.DataSource = dt;
            ddlGroup.DataBind();
        }

        protected void gvMessageInfo_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
           
            lbtnData.BorderWidth = 1;
            lbtnPassword.BorderWidth = 0;
            dData.Visible = true;
            dPassword.Visible = false;
            GridViewRow row = gvMessageInfo.Rows[e.NewSelectedIndex];
            pUser.Visible = true;
            tbxImię.Text = row.Cells[3].Text;
            tbxNazwisko.Text = row.Cells[4].Text;
            if (row.Cells[5].Text == "Tak") cbActive.Checked = true;
            else cbActive.Checked = false;
            tbxLogin.Text = row.Cells[6].Text;
            ddlGroup.SelectedValue = row.Cells[2].Text;
            setSelectedRow(row.Cells[1].Text, row.Cells[6].Text);


            ADOHelper helper = new ADOHelper("user_history_get");
            helper.AddParameter("@user_id", row.Cells[1].Text);
            DataTable dt = helper.Execute();
            gvHistory.DataSource = dt;
            gvHistory.DataBind();

            ChangeColumns(sender);
        }

        private void setSelectedRow(string UserIdToChange,string LoginToChange)
        {
            Session["UserIdToChange"] = UserIdToChange;
            Session["LoginToChange"] = LoginToChange;
        }

        protected void gvMessageInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.Cells.Count > 2)
            {
                
            }
        }

        protected void gvMessageInfo_DataBound(object sender, EventArgs e)
        {

            
            if(gvMessageInfo.Columns.Count>1)
            {
                gvMessageInfo.Columns[0].Visible = false;
            }
            ChangeColumns(sender);

            foreach (GridViewRow row in gvMessageInfo.Rows)
            {
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                
                if (row.Cells[5].Text == "1")
                    row.Cells[5].Text = "Tak";
                else row.Cells[5].Text = "Nie";
            }
        }

        protected void ChangeColumns(object sender)
        {
            GridView gridView = (GridView)sender;

            if (gridView.HeaderRow != null && gridView.HeaderRow.Cells.Count > 0)
            {
                gridView.HeaderRow.Cells[1].Visible = false;
                gridView.HeaderRow.Cells[2].Visible = false;
                gridView.HeaderRow.Cells[3].Text = "Imię";
                gridView.HeaderRow.Cells[4].Text = "Nazwisko";
                gridView.HeaderRow.Cells[5].Text = "Aktywny";
                gridView.HeaderRow.Cells[6].Text = "Login";
                gridView.HeaderRow.Cells[7].Text = "Modyfikujący";
                gridView.HeaderRow.Cells[8].Text = "Zmodyfikowany";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveUserData(tbxImię.Text, tbxLogin.Text, tbxNazwisko.Text, cbActive.Checked, ddlGroup.SelectedValue, int.Parse(Session["UserIdToChange"].ToString()), int.Parse(Session["IdUser"].ToString()));
                SwitchDiv(true);
                Status.Text = "Dane zostały zmienione";
                GetData();
            }
            catch 
            {
                FailureText.Text = "Błąd wystąpił w procedurze";
            }
            
        }

        private void SaveUserData(string tbxImię, string tbxLogin, string tbxNazwisko, bool cbActive
            , string ddlGroup,int UserIdtoChange, int UserId)
        {           

            XmlNodeList xnList = XMLHelper.GetDataXML("Users");
            DataTable dt = XMLHelper.ConvertXmlNodeListToDataTable(xnList);
            
            foreach (DataRow item in dt.Rows)
            {
                if (item[0].ToString() == UserIdtoChange.ToString())
                {
                    item[1] = ddlGroup;
                    item[2] = tbxImię;
                    item[3] = tbxNazwisko;
                    item[4] = cbActive ? 1 : 0;
                    item[5] = tbxLogin;
                    item[8] = Session["UserLogin"];
                    item[9] = DateTime.Now;
                }
            }
            dt.TableName = "users";
            XDocument xd  = XMLHelper.ToXmlDocument(dt);
            xd.Save(CommonConst.XmlPathTable+"Users.xml");
        }

       
        protected void lbtnData_Click(object sender, EventArgs e)
        {
            SwitchDiv(true);
        }

        private  void SwitchDiv( bool switchDiv)
        {
            ChangeColumns(gvMessageInfo);
            lbtnData.BorderWidth = (switchDiv) ? 1 : 0;//.Style.Add("", "");
            lbtnPassword.BorderWidth = (!switchDiv) ? 1 : 0;
            dData.Visible = switchDiv;
            dPassword.Visible = !switchDiv;
        }

        protected void lbtnPassword_Click(object sender, EventArgs e)
        {
            SwitchDiv(false);
            //lbtnData.BorderWidth =0;//.Style.Add("", "");
            //lbtnPassword.BorderWidth = 1;
            //dData.Visible = false;
            //dPassword.Visible = true;
        }

        //public static JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        //public struct Result
        //{
        //    public string errorMessage;
        //    public object value;
        //    public bool success;
        //    public bool finished;

        //    public override string ToString()
        //    {
        //        return javaScriptSerializer.Serialize(this);
        //    }
        //}

        //[WebMethod(true)]
        //public static Result SavePass(string tbxPasswordNew)
        //{
        //    Result retVal = new Result { errorMessage = "" };
        //    String KeyString = Decode.GenerateAPassKey("MilasKey");
        //    ADOHelper helper = new ADOHelper("users_get");
        //    DataTable dt = helper.Execute();

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        if (dt.Rows[i]["user_login"].ToString() == HttpContext.Current.Session["LoginToChange"].ToString())
        //        {
        //            if (Decode.Decrypt(dt.Rows[i]["user_password"].ToString(), KeyString) != tbxPasswordNew)
        //            {
        //                try
        //                {
        //                    ControlProject.SavePassword(tbxPasswordNew, int.Parse(HttpContext.Current.Session["UserIdToChange"].ToString()), int.Parse(HttpContext.Current.Session["IdUser"].ToString()));
        //                    retVal.success = true;
        //                }
        //                catch
        //                {
        //                    retVal.errorMessage = "Błąd wystąpił w procedurze";
        //                }
        //            }
        //            else retVal.errorMessage = "Nowe hasło nie różni się od starego!";
        //        }
        //    }
        //    return retVal;
        //}
        //[WebMethod(true)]
        //public static void AfterSave()
        //{
        //    SwitchDiv(true);
        //    frm.Status.Text = "Hasło zostało zmienione";
        //    GetData();
        //}

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

        protected void gvHistory_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void gvHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvHistory_DataBound(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["groups"]; 
            GridView gridView = (GridView)sender;

            if (gridView.HeaderRow != null && gridView.HeaderRow.Cells.Count > 0)
            {
                //gridView.HeaderRow.Cells[1].Visible = false;
                //gridView.HeaderRow.Cells[2].Visible = false;
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
                if (row.Cells[0].Text == "ins") row.Cells[0].Text = "Dodanie";
                else if (row.Cells[0].Text == "upd") row.Cells[0].Text = "Aktualizacja";
                else row.Cells[0].Text = "Usunięcie";
                row.Cells[2].Visible = false;
                row.Cells[8].Visible = false;
                if (row.Cells[6].Text == "1")
                    row.Cells[6].Text = "Tak";
                else row.Cells[6].Text = "Nie";
                //if (row.Cells[5].Text == "1")
                //    row.Cells[5].Text = "Tak";
                //else row.Cells[5].Text = "Nie";
            }
        }

        protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvHistory.PageIndex = e.NewPageIndex;
            gvHistory.DataBind();
        }

        protected void gvMessageInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FailureText.Text = "";
            Status.Text = "";
            gvMessageInfo.PageIndex = e.NewPageIndex;
            pUser.Visible = false;
            //gvMessageInfo.SelectRow(0);
            gvMessageInfo.DataBind();
        }

    }

}