<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StoreDashboard.aspx.cs" Inherits="Shop.StoreDashboard" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Centrum dowodzenia.</h2>
    </hgroup>

    <article>
        <p>Nowe zamówienia: </p>  
        <p>        
            
        <asp:GridView ID="gvMessageInfo" runat="server" AllowPaging="True" AutoGenerateColumns="true"
            AllowSorting="true" PagerSettings-Mode="NumericFirstLast"
            CssClass="grid" PageSize="5" DataKeyNames="orde_id" OnDataBound="gvMessageInfo_DataBound" >
	        <AlternatingRowStyle CssClass="altrow" />
            <HeaderStyle CssClass="headerstyle" />
            <RowStyle CssClass="row" HorizontalAlign="Center" />
            <SelectedRowStyle CssClass="selectedRow" />
            <PagerStyle CssClass="foot" />
            <EmptyDataTemplate>Brak danych</EmptyDataTemplate>    
        </asp:GridView>
                        <asp:DropDownList ID="ddlStatuses" runat="server" Enabled="false" Visible="false"></asp:DropDownList>
            <a id="A1" runat="server" href="~/Storehouse/OrderReport.aspx" style="width:500px">Przejdź do zamówień ></a>
            <br />
        </p>
        <hr />
            <h3>Terminarz</h3>
        <table><tr>
            <td>
            <asp:Calendar ID="calendar" runat="server"
                BackColor="White" BorderColor="black"  CellPadding="1" DayNameFormat="Shortest"
    Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" 
    Width="250px">
    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
    <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
    <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
    <WeekendDayStyle BackColor="#CCCCFF" />
    <OtherMonthDayStyle ForeColor="#999999" />
    <NextPrevStyle Font-Size="8pt" ForeColor="black" />
    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
    <TitleStyle BackColor="#BDBDBD" BorderColor="black" 
        BorderWidth="1px" Font-Bold="True"
        Font-Size="10pt" ForeColor="black" Height="25px" /></asp:Calendar> 
                </td>
            <td> 
                15 Luty 2016 - Dostawa <br />
                23 Luty 2016 - Szkolenie BHP <br />
                22 Luty 2016 - Dostawa <br />
                24 Luty 2016 - Wysyłka gwarancji <br />
                26 Luty 2016 - Komisja <br />
                29 Luty 2016 - Dostawa <br />
            </td>
            </tr>
            </table>
    </article>
        
        

    <aside>
        <h3>Dashboard</h3>
        <p>        
           
        </p>
        <ul>
           <%-- <li><a runat="server" href="~/Accounts/Manage.aspx">Zarządzaj</a></li>
            <li><a runat="server" href="~/Products/ProductsReport.aspx">Produkty</a></li>--%>
            <li><a runat="server" href="~/Storehouse/OrderReport.aspx">Magazyn</a></li>
        </ul>
    </aside>
</asp:Content>