using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using ShopCopyForXML;
using System.IO;
using System.Xml;
using System.Data;
using System.Xml.Linq;

/// <summary>
/// Summary description for CommonConst
/// </summary>
public class CommonConst
{
    //public static readonly string ConnectionString = "server=shopeedatabase.mssql.somee.com;database=shopeedatabase;user id=milaxx92_SQLLogin_1;password=vjdsp35295";
    //public static readonly string ConnectionString = "server=MILA-PC;database=shopeedatabase;user id=mila;password=a";
    //public static readonly string ConnectionString = "server=SQL5003.myASP.NET;database=DB_9F3C3A_xmilax;user id=DB_9F3C3A_xmilax_admin;password=zaq12wsx";
   // public static readonly string ConnectionString = "server=SQL5018.myASP.NET;database=DB_9FCEAF_xmilax;user id=DB_9FCEAF_xmilax_admin;password=zaq12wsx";
    //ata Source=MILA-PC;Initial Catalog=shopeedatabase;Integrated Security=True
    //private static readonly string ConnectionStringBase = "server=shopeedatabase.mssql.somee.com;database=shopeedatabase;user id=milaxx92_SQLLogin_1;password=vjdsp35295";
    public static readonly string MilaKey = Decode.GenerateAPassKey("MilasKey");

    //komp
    public static readonly string XmlPathTable = "E:\\Drive\\mgr\\projekty\\ShopCopyForXML\\ShopCopyForXML\\XML\\Tables\\";
    public static readonly string ConnectionString = "server=MILA-PC\\MILA;database=DB_9FCEAF_xmilax;user id=DB_9FCEAF_xmilax_admin;password=zaq12wsx";

    //laptop
    //public static readonly string XmlPathTable = "G:\\google\\mgr\\projekty\\ShopCopyForXML\\ShopCopyForXML\\XML\\Tables\\";
    //public static readonly string ConnectionString = "server=DESKTOP-0S4HTCH;database=DB_9FCEAF_xmilax;user id=DB_9FCEAF_xmilax_admin;password=zaq12wsx";



    //public static readonly string XmlPathUser = "E:\\Dropbox\\mgr\\ShopCopyForXML\\ShopCopyForXML\\XML\\XML_F52E2B61-18A1-11d1-B105-00805F49916B2.xml";
    //public static readonly string xmlPath = "E:\\Dropbox\\mgr\\TesowaApkaXML2\\TesowaApkaXML2\\XML\\XML_F52E2B61-18A1-11d1-B105-00805F49916B2.xml";
    public static string GetConnectionString(string dbname)
    {
        //return CommonConst.ConnectionStringBase.Replace("dbname", dbname);
        
        return CommonConst.ConnectionString.Replace("dbname", dbname);
    }

    public static XmlNodeList GetDataXML(string table)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(XmlPathTable + table + ".xml");
        XmlNodeList xnList = doc.SelectNodes("/xmlDS/"+table.ToLower());

        return xnList;
    }
    public static DataTable ConvertXmlNodeListToDataTable(XmlNodeList xnl)
    {
        DataTable dt = new DataTable();
        int TempColumn = 0;

        foreach (XmlNode node in xnl.Item(0).ChildNodes)
        {
            TempColumn++;
            DataColumn dc = new DataColumn(node.Name, System.Type.GetType("System.String"));
            if (dt.Columns.Contains(node.Name))
            {
                dt.Columns.Add(dc.ColumnName = dc.ColumnName + TempColumn.ToString());
            }
            else
            {
                dt.Columns.Add(dc);
            }
        }

        int ColumnsCount = dt.Columns.Count;
        for (int i = 0; i < xnl.Count; i++)
        {
            DataRow dr = dt.NewRow();
            for (int j = 0; j < ColumnsCount; j++)
            {
                dr[j] = xnl.Item(i).ChildNodes[j].InnerText;
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }
    public static XDocument ToXmlDocument(DataTable dataTable)
    {
        string rootName = "xmlDS";
        var XmlDocument = new XDocument
        {
            Declaration = new XDeclaration("1.0", "utf-8", "")
        };
        XmlDocument.Add(new XElement(rootName));
        foreach (DataRow row in dataTable.Rows)
        {
            XElement element = null;
            if (dataTable.TableName != null)
            {
                element = new XElement(dataTable.TableName);
            }
            foreach (DataColumn column in dataTable.Columns)
            {
                element.Add(new
            XElement(column.ColumnName, row[column].ToString().Trim(' ')));
            }
            if (XmlDocument.Root != null) XmlDocument.Root.Add(element);
        }

        return XmlDocument;
    }

    public static readonly string[] Separators = new string[] { ";" };
    


}