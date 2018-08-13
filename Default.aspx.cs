using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace ShopCopyForXML
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["IdUser"] = "";
            Session["GroupID"] = "";
            Session["UserLogin"] = "";
            Session["UserFirstName"] = "";
            Session["UserLastName"] = "";
           // DataSet1.

        }

        //private void LoadUserTable(string xmlPath)
        //{
        //    //Jak jest DataSet to działa jak nie to nie wypełnia DatasSetu
        //    DataSet1 dataSet = new DataSet1();
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(xmlPath);
        //    XmlNodeReader reader = new XmlNodeReader(doc);
        //    dataSet.ReadXml(reader);


        //    //XmlDataDocument xmlDatadoc = new XmlDataDocument();
        //    //xmlDatadoc.DataSet.ReadXml(xmlPath);

        //    DataSet1 ds = new DataSet1();
        //    //ds = xmlDatadoc.DataSet1;
        //}

        protected void LoginClick(object sender, EventArgs e)
        {
            //ADOHelper helper = new ADOHelper("users_get");
            //DataTable dt = helper.Execute();


            int start = DateTime.Now.Millisecond;
            XmlNodeList xnList = CommonConst.GetDataXML("Users");
            int end = DateTime.Now.Millisecond;
            int supply = end - start;

            foreach (XmlNode xn in xnList)
            {
                string login = xn["user_login"].InnerText;
                string pass = xn["user_password"].InnerText;
                if (login == tbxLogin.Text)
                {
                    if (Decode.Decrypt(pass, CommonConst.MilaKey) == tbPass.Text)
                    {
                        CreateSession(xn);
                        if (Session["GroupID"].ToString() == "6") Response.Redirect("~/ClientDashboard.aspx");
                        if (Session["GroupID"].ToString() == "3") Response.Redirect("~/WorkerDashboard.aspx");
                        if (Session["GroupID"].ToString() == "4") Response.Redirect("~/StoreDashboard.aspx");
                        else Response.Redirect("~/About.aspx");
                        return;
                    }
                    else { FailureText.Text = "Błedne hasło!"; return; }
                }
            }

            //DataSet a = new data

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (dt.Rows[i]["user_login"].ToString() == tbxLogin.Text)
            //    {
            //        if (Decode.Decrypt(dt.Rows[i]["user_password"].ToString(), CommonConst.MilaKey) == tbPass.Text)
            //            {
            //                CreateSession(dt , i);
            //                if (Session["GroupID"].ToString() == "6") Response.Redirect("~/ClientDashboard.aspx");
            //                if (Session["GroupID"].ToString() == "3") Response.Redirect("~/WorkerDashboard.aspx");
            //                if (Session["GroupID"].ToString() == "4") Response.Redirect("~/StoreDashboard.aspx");
            //                else Response.Redirect("~/About.aspx");
            //                return;
            //            }
            //            else { FailureText.Text = "Błedne hasło!"; return; }
            //    }
            //}
            FailureText.Text = "Błedny login lub hasło!";
        }

        public void CreateSession(XmlNode dt)
        {
            Session["IdUser"] = dt["user_id"].InnerText;
            Session["GroupID"] = dt["user_grou_id"].InnerText;
            Session["UserLogin"] = dt["user_login"].InnerText;
            Session["UserFirstName"] = dt["user_fname"].InnerText;
            Session["UserLastName"] = dt["user_lname"].InnerText; 
        }
    }
}