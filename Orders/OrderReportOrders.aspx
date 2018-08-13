<%@ Page Title="Twoje Zamówienia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderReportOrders.aspx.cs" Inherits="ShopCopyForXML.Orders.OrderReportOrders" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
      <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>
      <script src="//code.jquery.com/jquery-1.10.2.js"></script>
      <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
      <link rel="stylesheet" href="/resources/demos/style.css"/>
    <hgroup class="title">
        <h1><%: Title %>.</h1>
    </hgroup>
    <article>
        <asp:GridView ID="gvMessageInfo" runat="server" AllowPaging="True" AutoGenerateColumns="true"
            AllowSorting="true" PagerSettings-Mode="NumericFirstLast" AutoGenerateSelectButton="true"
            CssClass="grid" PageSize="5"
                    OnSelectedIndexChanging="gvMessageInfo_SelectedIndexChanging" DataKeyNames="orde_id"
                    OnRowDataBound="gvMessageInfo_RowDataBound" OnDataBound="gvMessageInfo_DataBound" OnPageIndexChanging="gvMessageInfo_PageIndexChanging">
	        <AlternatingRowStyle CssClass="altrow" />
            <HeaderStyle CssClass="headerstyle" />
            <RowStyle CssClass="row" HorizontalAlign="Center" />
            <SelectedRowStyle CssClass="selectedRow" />
            <PagerStyle CssClass="foot" />
            <EmptyDataTemplate>Brak danych</EmptyDataTemplate>    
        </asp:GridView>
       <p  class="validation-summary-success" >
           <asp:Label runat="server" ID="Success" ></asp:Label> 
       </p>
        <asp:Panel ID="pProduct" runat="server" Visible="false">
            <p  class="validation-summary-success" ><asp:Label runat="server" ID="Status" ></asp:Label> </p>
            <br />
            <asp:LinkButton ID="lbtnData" runat="server" OnClick="lbtnData_Click" ><b>Dane Zamawiającego</b></asp:LinkButton> 
            | <asp:LinkButton ID="lbtnProducts" runat="server" OnClick="lbtnProducts_Click"><b>Produkty zamówienia</b></asp:LinkButton>
            | <asp:LinkButton ID="lbtnFaktura" runat="server" OnClick="lbtnFaktura_Click"><b>Dokumenty</b></asp:LinkButton>
            <hr />
            <div id="dData" runat="server">
                <table>
                    <tr>
                        <td><b>Imię:</b><asp:TextBox ID="tbxImie" runat="server"  Enabled="false"></asp:TextBox></td>
                        <td><b>Nazwisko:</b><asp:TextBox ID="tbxNazwisko"  Enabled="false" runat="server"  ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><b>Miasto:</b><asp:TextBox ID="tbxCity" runat="server" Enabled="false" ></asp:TextBox></td>
                        <td><b>Zip:  </b><asp:TextBox ID="tbxZip" runat="server" Enabled="false"></asp:TextBox></td> 
                    </tr>                     
                    <tr>
                        <td><b>Ulica:</b><asp:TextBox ID="tbxStreet" runat="server" Enabled="false" ></asp:TextBox></td>
                        <td><b>Nr Domu:  </b><asp:TextBox ID="tbxHome" runat="server" Enabled="false" ></asp:TextBox></td>
                    </tr>        
                    <tr>
                        <td><b>Data zamówienia:</b><asp:TextBox ID="tbxDate" runat="server" Enabled="false" ></asp:TextBox></td>
                        <td><b>Status całego zamówienia:  </b><asp:DropDownList ID="ddlStatuses" runat="server" Enabled="false"></asp:DropDownList></td>                    
                    </tr>
                </table>
                <hr />
                <table >
                    <tr>
                        <td style="width:100%;display:none">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Zapisz status" Visible="false"/>
                        </td>
                        <td>
                            <p  class="validation-summary-errors" >
                                <asp:Label runat="server" ID="FailureText2" />
                            </p>
                        </td>                           
                        <td>
                            <input id="btnHistory" type="button" value="Historia"  onclick="History();" /> 
                        </td>
                    </tr>
                </table>
            </div>
            <div id="dProducts" runat="server" visible="false">
                <asp:GridView ID="gvProducts" runat="server" AllowPaging="True" AutoGenerateColumns="true"
                    AllowSorting="true" PagerSettings-Mode="NumericFirstLast" AutoGenerateSelectButton="true"
                    CssClass="grid" PageSize="50"
                            OnSelectedIndexChanging="gvProducts_SelectedIndexChanging" DataKeyNames="orpr_orde_id"
                             OnDataBound="gvProducts_DataBound">
	                <AlternatingRowStyle CssClass="altrow" />
                    <HeaderStyle CssClass="headerstyle" />
                    <RowStyle CssClass="row" HorizontalAlign="Center" />
                    <SelectedRowStyle CssClass="selectedRow" />
                    <PagerStyle CssClass="foot" />
                    <EmptyDataTemplate>Brak danych</EmptyDataTemplate>    
                </asp:GridView>
                <hr />
                <div id="dStatus" runat="server">
                    <table>
                        <tr style="width:100%">
                            <td ><b>Produkt: </b><asp:TextBox ID="tbxProductName" runat="server"  Enabled="false"></asp:TextBox></td>
                            <td colspan="2" rowspan="2"><asp:Image id="image" runat="server" onError="this.onerror=null;this.src='../Images/brak.jpg';"/></td>
                        </tr>
                        <tr>
                            <td><b>Status produktu:    </b>
                                <asp:DropDownList ID="ddlOrderStatus" runat="server" Enabled="false">
                                    <asp:ListItem Value="0" Text="Nie spakowny"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Spakowany"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Brak"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:100%">
                                <asp:Button runat="server" ID="btnSaveProductStatus" Text="Zatwierdź" OnClick="btnSaveProductStatus_Click" Visible="false"/>
                            </td>
                            <td>
                                <p  class="validation-summary-errors" >
                                  <asp:Label runat="server" ID="FailureText" />
                                </p>
                            </td>                         
                            <td>
                                <input id="btnOrderProductHistory" type="button" value="Historia"  onclick="HistoryOrderProduct();" /> 
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="dDocuments" runat="server" visible="false">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnInvoice" runat="server" OnClick="btnInvoice_Click" Text="Faktura"/>
                        </td>
                        
                        <td style="display:none">
                            <asp:Button ID="btnLetter" runat="server" OnClick="btnLetter_Click" Text="List przewozowy"/>
                        </td>
                    </tr>
                </table>                
            </div>
            <div id="popup"  style="display:none;width:100%" title="Historia zamówienia">
                <asp:GridView ID="gvHistory" runat="server" AllowPaging="True" AutoGenerateColumns="true"
                    AllowSorting="true" PagerSettings-Mode="NumericFirstLast" 
                    CssClass="grid" PageSize="100" DataKeyNames="orde_id" OnDataBound="gvHistory_DataBound" >
	                <AlternatingRowStyle CssClass="altrow" />
                    <HeaderStyle CssClass="headerstyle" />
                    <RowStyle CssClass="row" HorizontalAlign="Center" />
                    <SelectedRowStyle CssClass="selectedRow" />
                    <PagerStyle CssClass="foot" />  
    
                    <EmptyDataTemplate>Brak danych</EmptyDataTemplate>
    
                </asp:GridView>
            </div>
            <div id="popupOrderProduct"  style="display:none;width:100%" title="Historia produktów zamówienia">
                <asp:GridView ID="gvHistoryOrderProduct" runat="server" AllowPaging="True" AutoGenerateColumns="true"
                    AllowSorting="true" PagerSettings-Mode="NumericFirstLast" 
                    CssClass="grid" PageSize="100" DataKeyNames="orpr_id" OnDataBound="gvHistoryOrderProduct_DataBound" >
	                <AlternatingRowStyle CssClass="altrow" />
                    <HeaderStyle CssClass="headerstyle" />
                    <RowStyle CssClass="row" HorizontalAlign="Center" />
                    <SelectedRowStyle CssClass="selectedRow" />
                    <PagerStyle CssClass="foot" />  
    
                    <EmptyDataTemplate>Brak danych</EmptyDataTemplate>
    
                </asp:GridView>
            </div>
        </asp:Panel>
    </article>

    <aside style="display:none">
        <h3>Sprzedaż</h3>
        <ul >
            <%--<li><a id="aProductsReport" runat="server" href="~/Storehouse/ProductsReport.aspx">Magazyn</a></li>
            <li><a id="aAddProduct" runat="server" href="~/Products/AddProduct.aspx">Dodaj produkt</a></li>--%>
            <li><a id="aOrderReportSale" runat="server" href="~/Sale/OrderReportSale.aspx">Zamówienia</a></li>
            <li><a id="aAddCategory" runat="server" href="~/Sale/AddOrder.aspx">Dodaj zamówienie</a></li>
            <%--<li><a id="aUsers" runat="server" href="~/Accounts/Users.aspx">Użytkownicy</a></li>--%>
            <%--<li><a id="A3" runat="server" href="~/Contact.aspx">Contact</a></li>--%>
        </ul>
    </aside>

 <script type="text/javascript" >

     function History() {
         $("#popup").dialog({
             //show: {
             //    effect: "blind",
             //    duration: 200
             //},
             modal: true,
             minWidth: 1000,
             minHeight: 200,
             width: 1200,
             height: 400,
             buttons: {
                 Zamknij: function () {
                     $(this).dialog("close");
                 }
             }
         });
     }

     function HistoryOrderProduct() {
         $("#popupOrderProduct").dialog({
             //show: {
             //    effect: "blind",
             //    duration: 200
             //},
             modal: true,
             minWidth: 1000,
             minHeight: 200,
             width: 1200,
             height: 400,
             buttons: {
                 Zamknij: function () {
                     $(this).dialog("close");
                 }
             }
         });
     }
 
</script> 

</asp:Content>