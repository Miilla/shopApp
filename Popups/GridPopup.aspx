<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridPopup.aspx.cs" Inherits="ShopCopyForXML.Popups.GridPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="popup" runat="server" style="display:none">
         <asp:GridView ID="gvMessageInfo" runat="server" AllowPaging="True" AutoGenerateColumns="true"
                AllowSorting="true" PagerSettings-Mode="NumericFirstLast" AutoGenerateSelectButton="true"
                CssClass="grid" PageSize="10"
                        OnSelectedIndexChanging="gvMessageInfo_SelectedIndexChanging" DataKeyNames="user_id"
                        OnRowDataBound="gvMessageInfo_RowDataBound" OnDataBound="gvMessageInfo_DataBound">
   
    
	            <AlternatingRowStyle CssClass="altrow" />
                <HeaderStyle CssClass="headerstyle" />
                <RowStyle CssClass="row" HorizontalAlign="Center" />
                <SelectedRowStyle CssClass="selectedRow" />
                <PagerStyle CssClass="foot" />
    
    
                <EmptyDataTemplate>Brak danych</EmptyDataTemplate>
    
            </asp:GridView>
    </div>
    </form>
</body>
</html>
