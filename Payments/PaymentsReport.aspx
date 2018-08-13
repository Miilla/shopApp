<%@ Page Title="Płatności" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaymentsReport.aspx.cs" Inherits="ShopCopyForXML.Payments.PaymentsReport" %>

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
            AllowSorting="true" PagerSettings-Mode="NumericFirstLast" 
            CssClass="grid" PageSize="5" DataKeyNames="paym_id" OnDataBound="gvMessageInfo_DataBound"
             OnPageIndexChanging="gvMessageInfo_PageIndexChanging">
   
    
	        <AlternatingRowStyle CssClass="altrow" />
            <HeaderStyle CssClass="headerstyle" />
            <RowStyle CssClass="row" HorizontalAlign="Center" />
            <SelectedRowStyle CssClass="selectedRow" />
            <PagerStyle CssClass="foot" />
    
    
            <EmptyDataTemplate>Brak danych</EmptyDataTemplate>
    
        </asp:GridView>
        
    </article>

    <aside>
        <h3>Płatności</h3>
        <ul>
            <li><a id="aPaymentsReport" runat="server" href="~/Payments/PaymentsReport.aspx">Płatności</a></li>
        </ul>
    </aside>    

</asp:Content>