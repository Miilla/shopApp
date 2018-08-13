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
public class XMLHelper
{
    public static readonly string XmlPathTable = "E:\\Drive\\mgr\\projekty\\ShopCopyForXML\\ShopCopyForXML\\XML\\Tables\\";
   

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
}