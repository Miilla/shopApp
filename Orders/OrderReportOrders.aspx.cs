using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace ShopCopyForXML.Orders
{
    public partial class OrderReportOrders : System.Web.UI.Page
    {
        ControlProject ControlProject = new ControlProject();
        string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
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
            Session["SaveSuccess"] = "0";
            ControlProject.CheckSession(this);
            ControlProject.VisibleMenu(true, this);
            Status.Text = "";

            if (!IsPostBack)
            {
                GetData();
            }
            GetOrders();
        }

        private void GetOrders()
        {
            ADOHelper helper = new ADOHelper("orders_for_client_get");
            helper.AddParameter("@clie_id", 1);
            DataTable dt = helper.Execute();
            gvMessageInfo.DataSource = dt;
            gvMessageInfo.DataBind();
        }

        private void GetData()
        {
            ADOHelper helper = new ADOHelper("statuses_get");
            DataTable dt = helper.Execute();


            ddlStatuses.DataTextField = "stat_name";
            ddlStatuses.DataValueField = "stat_id";
            ddlStatuses.DataSource = dt;
            ddlStatuses.DataBind();
            Session["statuses"] = dt;
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
            lbtnData.BorderWidth = 1;
            lbtnProducts.BorderWidth = 0;
            lbtnFaktura.BorderWidth = 0;
            dData.Visible = true;
            dProducts.Visible = false;
            dDocuments.Visible = false;
            GridViewRow row = gvMessageInfo.Rows[e.NewSelectedIndex];
            pProduct.Visible = true;
            tbxImie.Text = row.Cells[4].Text;
            tbxNazwisko.Text = row.Cells[5].Text;
            tbxCity.Text = row.Cells[6].Text;
            tbxZip.Text = row.Cells[9].Text;
            // if (row.Cells[7].Text == "1") cbBanner.Checked = true;
            //else cbBanner.Checked = false;
            tbxStreet.Text = row.Cells[7].Text;
            tbxHome.Text = row.Cells[8].Text;
            string value = ddlStatuses.Items.FindByText(row.Cells[10].Text).Value;
            ddlStatuses.SelectedValue = value;

            tbxDate.Text = row.Cells[11].Text;
            setSelectedRow(row.Cells[1].Text);

            ADOHelper helper = new ADOHelper("order_history_get");
            helper.AddParameter("@orde_id", row.Cells[1].Text);
            DataTable dt = helper.Execute();
            gvHistory.DataSource = dt;
            gvHistory.DataBind();

            ChangeColumns(sender);
        }

        private void setSelectedRow(string OrderId)
        {
            Session["OrderId"] = OrderId;
            ADOHelper helper = new ADOHelper("order_product_get");
            helper.AddParameter("@orpr_orde_id", OrderId);
            DataTable dt = helper.Execute();
            gvProducts.DataSource = dt;
            gvProducts.DataBind();
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
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[8].Visible = false;
                row.Cells[9].Visible = false;
                row.Cells[10].Text = ddlStatuses.Items.FindByValue(row.Cells[10].Text).Text;
            }
        }

        protected void ChangeColumns(object sender)
        {
            GridView gridView = (GridView)sender;

            if (gridView.HeaderRow != null && gridView.HeaderRow.Cells.Count > 0)
            {
                gridView.HeaderRow.Cells[1].Visible = false;
                gridView.HeaderRow.Cells[2].Visible = false;
                gridView.HeaderRow.Cells[3].Visible = false;
                gridView.HeaderRow.Cells[4].Visible = false;
                gridView.HeaderRow.Cells[8].Visible = false;
                gridView.HeaderRow.Cells[9].Visible = false;
                gridView.HeaderRow.Cells[4].Text = "Imię";
                gridView.HeaderRow.Cells[5].Text = "Nazwisko";
                gridView.HeaderRow.Cells[6].Text = "Miasto";
                gridView.HeaderRow.Cells[7].Text = "Ulica";
                gridView.HeaderRow.Cells[8].Text = "Nr domu";
                gridView.HeaderRow.Cells[9].Text = "Kod pocztowy";
                gridView.HeaderRow.Cells[10].Text = "Status";
                gridView.HeaderRow.Cells[11].Text = "Data Zamówienia";
                gridView.HeaderRow.Cells[12].Text = "Modyfikujący";
                gridView.HeaderRow.Cells[13].Text = "Zmodyfikowany";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateOrderStatus(int.Parse(Session["OrderId"].ToString()), int.Parse(ddlStatuses.SelectedValue), int.Parse(Session["IdUser"].ToString()));
                SwitchDiv(1);
                Success.Text = "Dane zostały zmienione";
                GetOrders();
                pProduct.Visible = false;
            }
            catch
            {
                FailureText2.Text = "Błąd wystąpił w procedurze";
            }

        }

        private void UpdateOrderStatus(int OrderId, int ddlStatuses, int IdUser)
        {
            ADOHelper helper = new ADOHelper("order_update");
            helper.AddParameter("@orde_id", OrderId);
            helper.AddParameter("@orde_stat_id", ddlStatuses);
            helper.AddParameter("@user_id", IdUser);
            helper.ExecuteNonQuery();
        }

        protected void lbtnData_Click(object sender, EventArgs e)
        {
            //SwitchDiv(true);
            SwitchDiv(1);
        }

        private void SwitchDiv(int switchDiv)//bool switchDiv)
        {
            ChangeColumns(gvMessageInfo);
            ChangeColumns2(gvProducts);
            ChangeColumnsHistory(gvHistory);
            dStatus.Visible = false;
            lbtnData.BorderWidth = (switchDiv==1) ? 1 : 0;//.Style.Add("", "");
            lbtnProducts.BorderWidth = (switchDiv == 0) ? 1 : 0;
            lbtnFaktura.BorderWidth = (switchDiv == 2) ? 1 : 0;
            if (switchDiv==2)
            {
                dData.Visible = false;
                dProducts.Visible = false;
                dDocuments.Visible = true;
            }
            else if (switchDiv == 0)
            {
                dData.Visible = false;
                dProducts.Visible = true;
                dDocuments.Visible = false;
            }
            else
            {
                dData.Visible = true;
                dProducts.Visible = false;
                dDocuments.Visible = false;
            }
        }

        protected void lbtnProducts_Click(object sender, EventArgs e)
        {
            SwitchDiv(0);
        }

        protected void btnSaveProductStatus_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateOrderProductStatus(int.Parse(Session["OrderProductId"].ToString()), int.Parse(ddlOrderStatus.SelectedValue), int.Parse(Session["IdUser"].ToString()));
                setSelectedRow(Session["OrderId"].ToString());
                SwitchDiv(0);
                Status.Text = "Dane zostały zmienione";
            }
            catch
            {
                FailureText.Text = "Błąd wystąpił w procedurze";
            }
        }

        private void UpdateOrderProductStatus(int OrderProductId, int status, int IdUser)
        {
            ADOHelper helper = new ADOHelper("order_product_update");
            helper.AddParameter("@orpr_id", OrderProductId);
            helper.AddParameter("@orpr_status", status);
            helper.AddParameter("@orpr_user_mod", IdUser);
            helper.ExecuteNonQuery();
        }

        protected void gvMessageInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Success.Text = "";
            pProduct.Visible = false;
            gvMessageInfo.PageIndex = e.NewPageIndex;
            gvMessageInfo.DataBind();
        }

        protected void gvProducts_DataBound(object sender, EventArgs e)
        {
            ChangeColumns2(sender);

            foreach (GridViewRow row in gvProducts.Rows)
            {
                row.Cells[1].Visible = false;
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[6].Visible = false;
                row.Cells[7].Text = ddlOrderStatus.Items.FindByValue(row.Cells[7].Text).Text;
            }
        }

        protected void ChangeColumns2(object sender)
        {
            GridView gridView = (GridView)sender;

            if (gridView.HeaderRow != null && gridView.HeaderRow.Cells.Count > 0)
            {
                gridView.HeaderRow.Cells[1].Visible = false;
                gridView.HeaderRow.Cells[2].Visible = false;
                gridView.HeaderRow.Cells[3].Visible = false;
                gridView.HeaderRow.Cells[6].Visible = false;
                gridView.HeaderRow.Cells[4].Text = "Produkt";
                gridView.HeaderRow.Cells[5].Text = "Cena";
                gridView.HeaderRow.Cells[7].Text = "Status";
                gridView.HeaderRow.Cells[8].Text = "Modyfikujący";
                gridView.HeaderRow.Cells[9].Text = "Zmodyfikowany";
            }
        }

        protected void gvProducts_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            ChangeColumns2(sender);
            ChangeColumnsHistory(gvHistory);
            GridViewRow row = gvProducts.Rows[e.NewSelectedIndex];
            dStatus.Visible = true;
            string value = ddlOrderStatus.Items.FindByText(row.Cells[7].Text).Value;
            ddlOrderStatus.SelectedValue = value;
            tbxProductName.Text = row.Cells[4].Text;
            setSelectedRow2(row.Cells[1].Text);
            image.Width = 200;
            image.Height = 200;
            image.BorderColor = Color.Black;
            image.BorderWidth = 1;
            image.ImageUrl = "~/Images/" + row.Cells[6].Text + ".jpg";


            ADOHelper helper = new ADOHelper("order_product_history_get");
            helper.AddParameter("@orpr_orde_id", row.Cells[2].Text);
            DataTable dt = helper.Execute();
            gvHistoryOrderProduct.DataSource = dt;
            gvHistoryOrderProduct.DataBind();
        }

        private void setSelectedRow2(string OrderProductId)
        {
            Session["OrderProductId"] = OrderProductId;
        }

        protected void gvHistory_DataBound(object sender, EventArgs e)
        {
            ChangeColumnsHistory(sender);

            DataTable dt = (DataTable)Session["statuses"];
            foreach (GridViewRow row in gvHistory.Rows)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (row.Cells[4].Text == dt.Rows[i]["stat_id"].ToString()) row.Cells[4].Text = dt.Rows[i]["stat_name"].ToString();
                }
                if (row.Cells[0].Text == "ins") row.Cells[0].Text = "Dodanie";
                else if (row.Cells[0].Text == "upd") row.Cells[0].Text = "Aktualizacja";
                else row.Cells[0].Text = "Usunięcie";
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
            }
        }

        private void ChangeColumnsHistory(object sender)
        {
            GridView gridView = (GridView)sender;

            if (gridView.HeaderRow != null && gridView.HeaderRow.Cells.Count > 0)
            {
                gridView.HeaderRow.Cells[0].Text = "Akcja";
                gridView.HeaderRow.Cells[1].Text = "Wersja";
                gridView.HeaderRow.Cells[2].Visible = false;
                gridView.HeaderRow.Cells[3].Visible = false;
                gridView.HeaderRow.Cells[4].Text = "Status";
                //gridView.HeaderRow.Cells[5].Visible = false;
                gridView.HeaderRow.Cells[5].Text = "Data zamówienia";
                //gridView.HeaderRow.Cells[4].Text = "Ilość";
                //gridView.HeaderRow.Cells[5].Text = "Kategoria";
                //gridView.HeaderRow.Cells[6].Text = "Cena";
                //gridView.HeaderRow.Cells[7].Text = "Opis";
                //gridView.HeaderRow.Cells[7].Visible = false;
                //gridView.HeaderRow.Cells[8].Text = "Banner";
                //gridView.HeaderRow.Cells[8].Visible = false;
                //gridView.HeaderRow.Cells[9].Text = "Lokalizacja";
                gridView.HeaderRow.Cells[6].Text = "Modyfikujący";
                gridView.HeaderRow.Cells[7].Text = "Zmodyfikowany";
            }
        }

        protected void gvHistoryOrderProduct_DataBound(object sender, EventArgs e)
        {
            ChangeColumnsHistoryOrderProduct(sender);

            //DataTable dt = (DataTable)Session["statuses"];
            foreach (GridViewRow row in gvHistoryOrderProduct.Rows)
            {

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    if (row.Cells[4].Text == dt.Rows[i]["stat_id"].ToString()) row.Cells[4].Text = dt.Rows[i]["stat_name"].ToString();
                //}
                if (row.Cells[7].Text == "0") row.Cells[7].Text = "Nie spakowny";
                else if (row.Cells[7].Text == "1") row.Cells[7].Text = "Spakowany";
                else if (row.Cells[7].Text == "2") row.Cells[7].Text = "Brak";

                if (row.Cells[0].Text == "ins") row.Cells[0].Text = "Dodanie";
                else if (row.Cells[0].Text == "upd") row.Cells[0].Text = "Aktualizacja";
                else row.Cells[0].Text = "Usunięcie";
                row.Cells[2].Visible = false;
                row.Cells[3].Visible = false;
                row.Cells[4].Visible = false;
                row.Cells[6].Visible = false;
            }
        }

        private void ChangeColumnsHistoryOrderProduct(object sender)
        {
            GridView gridView = (GridView)sender;

            if (gridView.HeaderRow != null && gridView.HeaderRow.Cells.Count > 0)
            {
                gridView.HeaderRow.Cells[0].Text = "Akcja";
                gridView.HeaderRow.Cells[1].Text = "Wersja";
                gridView.HeaderRow.Cells[2].Visible = false;
                gridView.HeaderRow.Cells[3].Visible = false;
                gridView.HeaderRow.Cells[4].Visible = false;
                gridView.HeaderRow.Cells[5].Text = "Produkt";
                gridView.HeaderRow.Cells[6].Visible = false;
                gridView.HeaderRow.Cells[7].Text = "Status";
                gridView.HeaderRow.Cells[8].Text = "Modyfikujący";
                gridView.HeaderRow.Cells[9].Text = "Zmodyfikowany";
            }
        }

        protected void lbtnFaktura_Click(object sender, EventArgs e)
        {
            SwitchDiv(2);
        }

        protected void btnInvoice_Click(object sender, EventArgs e)
        {
           // string pathhtml = path + "\\inz\\list2.html"; 
           // WebClient wc = new WebClient();
          //  string htmlText = wc.DownloadString(new Uri(pathhtml).LocalPath);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", "OpenPopupWithHtml('" + htmlText + "', '" + "asd" + "');");
            ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}', '{1}', 'width=400,height=200,scrollbars=yes');</script>", "f.html", "lol"));

            //PdfDocument pdf = new PdfDocument();
            //pdf.Info.Title = "My First PDF";
            //PdfPage pdfPage = pdf.AddPage();
            //XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            //XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
            //graph.DrawString("This is my first PDF document", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.Center);
            //string pdfFilename = "firstpage.pdf";
            //pdf.Save(pdfFilename);
            //Process.Start(pdfFilename);
            //Bitmap bitmap = new Bitmap(1200, 1800);
            //Graphics g = Graphics.FromImage(bitmap);
            //TheArtOfDev.HtmlRenderer.WPF.HtmlContainer c = new TheArtOfDev.HtmlRenderer.WPF.HtmlContainer();
            //c.SetHtml("<html><body style='font-size:20px'>Whatever</body></html>");
            ////rect
            
            //c.PerformPaint(g);
            //PdfDocument doc = new PdfDocument();
            //PdfPage page = new PdfPage();
            //XImage img = XImage.FromGdiPlusImage(bitmap);
            //doc.Pages.Add(page);
            //XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[0]);
            //xgr.DrawImage(img, 0, 0);
            //doc.Save(@"C:\test.pdf");
            //doc.Close();
            

            
            //string pathpdf = path + "\\my.pdf";
            //////////////string pathhtml = path + "\\inz\\f.html";
            //////////////WebClient wc = new WebClient();
            //////////////string htmlText = wc.DownloadString(new Uri(pathhtml).LocalPath);
            ////////////////wc.Encoding = Encoding.;
            //////////////htmlText=htmlText.Replace("x:path:x", path + "\\inz\\");
            //////////////createPDF(htmlText);

            //Spire.Doc.Document doc = new Spire.Doc.Document("my document.doc/docx")
            //doc.SaveToFile("pdf document.pdf", Spire.Doc.FileFormat.PDF);

           // newcreatepdf();
            //Document document = new Document();
            //try
            //{
            //    Response.ContentType = "Application/pdf";
            //    Response.AppendHeader( "Content-Disposition",  "attachment; filename=test.pdf");
            //    //MemoryStream myMemoryStream = new MemoryStream();
            //    //FileStream ms = new FileStream(new Uri(pathpdf).LocalPath, FileMode.Create);
            //    //ms = new FileStream(new Uri(pathpdf).LocalPath, FileMode.Create);
            //    PdfWriter.GetInstance(document, Response.OutputStream);//myMemoryStream);
            //    document.Open();
            //    WebClient wc = new WebClient();
            //    string htmlText = wc.DownloadString(new Uri(pathhtml).LocalPath);
            //    //Response.Write(htmlText);
            //    List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StringReader(htmlText), null);
            //    for (int k = 0; k < htmlarraylist.Count; k++)
            //    {
            //        document.Add((IElement)htmlarraylist[k]);
            //    }
            //    document.Close();
            //    Response.End();

            //    //byte[] content = myMemoryStream.ToArray();
            //    //myMemoryStream.CopyTo(Response.OutputStream);
            //}
            //catch
            //{
            //}
        }

        private void newcreatepdf()
        {
            using (var client = new WebClient())
            {
                //Console.WriteLine("Please choose a Word document to convert to PDF.");

                //var openFileDialog = new OpenFileDialog
                //{
                //    Filter =
                //        "Word document(*.doc;*.docx)|*.doc;*.docx"
                //};
                //if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                string pathdoc = path + "\\inz\\x.docx";
                var fileToConvert = pathdoc;

                //Console.WriteLine(string.Format("Converting the file {0} Please wait.", fileToConvert));

                var data = new System.Collections.Specialized.NameValueCollection();
                data.Add("OutputFileName", "MyFile.pdf");

                try
                {
                    client.QueryString.Add(data);
                    var response =
                    client.UploadFile("http://do.convertapi.com/word2pdf", fileToConvert);
                    var responseHeaders = client.ResponseHeaders;
                    var web2PdfOutputFileName = responseHeaders["OutputFileName"];
                    var path2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), web2PdfOutputFileName);
                    File.WriteAllBytes(path2, response);
                    //Console.WriteLine("The conversion was successful! The word file {0} converted to PDF and saved at {1}", fileToConvert, path2);
                }
                catch (WebException e)
                {
                //    Console.WriteLine("Exception Message :" + e.Message);
                //    if (e.Status == WebExceptionStatus.ProtocolError)
                //    {
                //        Console.WriteLine("Status Code : {0}", ((HttpWebResponse)e.Response).StatusCode);
                //        Console.WriteLine("Status Description : {0}", ((HttpWebResponse)e.Response).StatusDescription);
                //    }

                }


            }
        }
        private void createPDF(string html)
        {
            //MemoryStream msOutput = new MemoryStream();
            TextReader reader = new StringReader(html);// step 1: creation of a document-object
            Document document = new Document(PageSize.A4, 30, 30, 30, 30);
            Response.ContentType = "Application/pdf";
            // step 2:
            // we create a writer that listens to the document
            // and directs a XML-stream to a file
            PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);//new FileStream("Test.pdf", FileMode.Create));

            // step 3: we create a worker parse the document
            HTMLWorker worker = new HTMLWorker(document);

            // step 4: we open document and start the worker on the document
            document.Open();

            // step 4.1: register a unicode font and assign it an allias
            FontFactory.Register("C:\\Windows\\Fonts\\ARIALUNI.TTF", "arial unicode ms");

            // step 4.2: create a style sheet and set the encoding to Identity-H
            iTextSharp.text.html.simpleparser.StyleSheet ST = new iTextSharp.text.html.simpleparser.StyleSheet();
            ST.LoadTagStyle("body", "encoding", "Identity-H");

            // step 4.3: assign the style sheet to the html parser
            worker.SetStyleSheet(ST);

            worker.StartDocument();

            // step 5: parse the html into the document
            worker.Parse(reader);

            // step 6: close the document and the worker
            worker.EndDocument();
            worker.Close();
            document.Close();
            Response.End();
        }

        protected void btnLetter_Click(object sender, EventArgs e)
        {
            //string pathpdf = path + "\\my.pdf";
            //string pathhtml = path + "\\inz\\list.html";
            //WebClient wc = new WebClient();
            //string htmlText = wc.DownloadString(new Uri(pathhtml).LocalPath);
            ////wc.Encoding = Encoding.;
            //htmlText = htmlText.Replace("x:path:x", path + "\\inz\\");
            //createPDF(htmlText);
            ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}', '{1}', 'width=400,height=200,scrollbars=yes');</script>", "list2ssss.html", "lol"));
        }

    }

}