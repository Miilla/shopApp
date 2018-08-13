<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkerDashboard.aspx.cs" Inherits="Shop.WorkerDashboard" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Centrum dowodzenia.</h2>
    </hgroup>

    <article>
        <p>Nowe produkty: </p>  
        <p>        
            
        <asp:GridView ID="gvMessageInfo" runat="server" AllowPaging="false" AutoGenerateColumns="true"
            AllowSorting="true" PagerSettings-Mode="NumericFirstLast"
            CssClass="grid" PageSize="5" DataKeyNames="prod_id" OnDataBound="gvMessageInfo_DataBound" >
	        <AlternatingRowStyle CssClass="altrow" />
            <HeaderStyle CssClass="headerstyle" />
            <RowStyle CssClass="row" HorizontalAlign="Center" />
            <SelectedRowStyle CssClass="selectedRow" />
            <PagerStyle CssClass="foot" />
            <EmptyDataTemplate>Brak danych</EmptyDataTemplate>    
        </asp:GridView>
                        <asp:DropDownList ID="ddlStatuses" runat="server" Enabled="false" Visible="false"></asp:DropDownList>
            <a id="A1" runat="server" href="~/Products/ProductsReport.aspx" style="width:500px">Przejdź do produktów ></a>
            <br />
        </p>
    </article>
        
        

    <aside>
        <h3>Dashboard</h3>
        <p>        
           
        </p>
        <ul>
            <li><a runat="server" href="~/Products/ProductsReport.aspx">Produkty</a></li>
            <li><a runat="server" href="~/Shop/Sale.aspx">Sklep</a></li>
            <li><a runat="server" href="~/Payments/PaymentsReport.aspx">Zarządzanie płatnościami</a></li>
<%--            <li><a id="A2" runat="server" href="~/Storehouse/OrderReport.aspx">Twoje Zamówienia</a></li>--%>
        </ul>
    </aside>
</asp:Content>