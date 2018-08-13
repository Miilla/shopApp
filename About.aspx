<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="ShopCopyForXML.About" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Centrum dowodzenia.</h2>
    </hgroup>

    <article>
        <p>Pięć ostatnich zrealizowanych zamówienień: </p>  
        <p>        
            
        <asp:GridView ID="gvMessageInfo" runat="server" AllowPaging="True" AutoGenerateColumns="true"
            AllowSorting="true" PagerSettings-Mode="NumericFirstLast"
            CssClass="mGrid" PageSize="5" DataKeyNames="orde_id" OnDataBound="gvMessageInfo_DataBound" >
	        <AlternatingRowStyle CssClass="altrow" />
            <HeaderStyle CssClass="headerstyle" />
            <RowStyle CssClass="row" HorizontalAlign="Center" />
            <SelectedRowStyle CssClass="selectedRow" />
            <PagerStyle CssClass="foot" />
            <EmptyDataTemplate>Brak danych</EmptyDataTemplate>    
        </asp:GridView>
                        <asp:DropDownList ID="ddlStatuses" runat="server" Enabled="false" Visible="false"></asp:DropDownList>
            <a id="A1" runat="server" href="~/Sale/OrderReportSale.aspx" style="width:500px">Przejdź do zamówień ></a>
            <br />
        </p>
    </article>
        
        

    <aside>
        <h3>Dashboard</h3>
        <p>        
           
        </p>
        <ul>
            <li><a runat="server" href="~/Accounts/Manage.aspx">Zarządzaj</a></li>
            <li><a runat="server" href="~/Products/ProductsReport.aspx">Produkty</a></li>
            <li><a runat="server" href="~/Storehouse/ProductsReport.aspx">Magazyn</a></li>
        </ul>
    </aside>
</asp:Content>