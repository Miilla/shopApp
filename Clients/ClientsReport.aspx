<%@ Page Title="Klienci" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientsReport.aspx.cs" Inherits="ShopCopyForXML.Clients.ClientsReport" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
      <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>
      <script src="//code.jquery.com/jquery-1.10.2.js"></script>
      <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
      <link rel="stylesheet" href="/resources/demos/style.css"/>
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <%--<h2>Your app description page.</h2>--%>
    </hgroup>

    <article>
        <p>        
            
        </p>

        <asp:GridView ID="gvMessageInfo" runat="server" AllowPaging="True" AutoGenerateColumns="true"
            AllowSorting="true" PagerSettings-Mode="NumericFirstLast" AutoGenerateSelectButton="true"
            CssClass="grid" PageSize="5"
                    OnSelectedIndexChanging="gvMessageInfo_SelectedIndexChanging" DataKeyNames="clie_id"
                    OnRowDataBound="gvMessageInfo_RowDataBound" OnDataBound="gvMessageInfo_DataBound" OnPageIndexChanging="gvMessageInfo_PageIndexChanging">
   
    
	        <AlternatingRowStyle CssClass="altrow" />
            <HeaderStyle CssClass="headerstyle" />
            <RowStyle CssClass="row" HorizontalAlign="Center" />
            <SelectedRowStyle CssClass="selectedRow" />
            <PagerStyle CssClass="foot" />
    
    
            <EmptyDataTemplate>Brak danych</EmptyDataTemplate>
    
        </asp:GridView>
            <p  class="validation-summary-success" ><asp:Label runat="server" ID="Success" ></asp:Label> </p>
        
        
        <asp:Panel ID="pClient" runat="server" Visible="false">
            <p  class="validation-summary-success" ><asp:Label runat="server" ID="Status" ></asp:Label> </p>
            <br />
            <asp:LinkButton ID="lbtnData" runat="server" OnClick="lbtnData_Click" ><b>Dane Klienta</b></asp:LinkButton> | <asp:LinkButton ID="lbtnUser" runat="server" OnClick="lbtnUser_Click"><b>Konto użytkownika</b></asp:LinkButton>
            
            <hr />
            <div id="dData" runat="server">
                <table>
                    <tr>
                        <td><b>Imię:</b><asp:TextBox ID="tbxImię" runat="server"  ></asp:TextBox></td>
                        <td><b>Nazwisko:</b><asp:TextBox ID="tbxNazwisko" runat="server"  ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <%--<asp:TextBox ID="tbxAktywny" runat="server" enabled="false" ></asp:TextBox>--%>
                        
                       <%-- <td><b>Główna:  </b><asp:CheckBox ID="cbBanner" runat="server" /></td> --%>
                        <td><b>Saldo:</b><asp:TextBox ID="tbxSaldo" runat="server"  Enabled="false"></asp:TextBox></td>
                        <td><b>Płeć:  </b><asp:DropDownList ID="ddlPlec" runat="server">
                                        <asp:ListItem Text="Meżczyzna" Value="m"></asp:ListItem>
                                        <asp:ListItem Text="Kobieta" Value="k"></asp:ListItem>
                                        </asp:DropDownList></td>
                    </tr>  
                                  
                    <tr>
                        <td><b>Data urodzenia:</b><asp:TextBox ID="tbxBDate" runat="server" TextMode="Date"></asp:TextBox></td>
                        <td><b>Nip:</b><asp:TextBox ID="tbxNip" runat="server" ></asp:TextBox></td>
                    </tr>             
                    <tr>
                        <td><b>Ulica:</b><asp:TextBox ID="tbxStreet" runat="server" ></asp:TextBox></td>
                        <td><b>Nr domu:</b><asp:TextBox ID="tbxHome" runat="server" ></asp:TextBox></td>                    
                    </tr>           
                    <tr>
                        <td><b>Miasto:</b><asp:TextBox ID="tbxCity" runat="server" ></asp:TextBox></td>
                        <td><b>Kod pocztowy:</b><asp:TextBox ID="tbxZip" runat="server" ></asp:TextBox></td>                    
                    </tr>
                </table>
                <hr />
                <table>
                    <tr>
                        <td style="width:100%">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Zapisz"/>
                        </td>
                        <td>
                            <input id="btnHistory" type="button" value="Historia"  onclick="History();" /> 
                        </td>
                    </tr>
                </table>
            </div>
            <div id="dUser" runat="server" visible="false">
                <table>
                    <tr>
                        <%--<asp:TextBox ID="tbxAktywny" runat="server" enabled="false" ></asp:TextBox>--%>
                        <td><b>Login:</b><asp:TextBox ID="tbxLogin" runat="server"  ></asp:TextBox></td>
                        <td><b>Aktywny:  </b><asp:CheckBox ID="cbActive" runat="server" /></td> 
                    </tr>                
                    <tr>
                        <%--<asp:TextBox ID="tbxAktywny" runat="server" enabled="false" ></asp:TextBox>--%>
                        <td><b>Grupa użytkowników:  </b><asp:DropDownList ID="ddlGroup" runat="server"></asp:DropDownList></td>
                    
                    </tr>
                </table>
                <hr />
                <table>
                    <tr>
                        <td style="width:100%">
                            <asp:Button ID="tbnSaveUser" runat="server" OnClick="tbnSaveUser_Click" Text="Zapisz"/>
                        </td>
                        <td>
                            <%--<asp:Button ClientIDMode="Static" runat="server" ID="btnHistory" onclick="History();" Text="Historia" />--%>
                            <input id="btnHistoryUser" type="button" value="Historia"  onclick="HistoryUser();" /> 
                        </td>
                    </tr>
                </table>
            </div>
            <div id="popup"  style="display:none;width:100%" title="Historia klienta">
                <asp:GridView ID="gvHistory" runat="server" AllowPaging="True" AutoGenerateColumns="true"
                    AllowSorting="true" PagerSettings-Mode="NumericFirstLast" 
                    CssClass="grid" PageSize="100"
                            OnSelectedIndexChanging="gvHistory_SelectedIndexChanging" DataKeyNames="clie_id"
                            OnRowDataBound="gvHistory_RowDataBound" OnDataBound="gvHistory_DataBound" >
   
    
	                <AlternatingRowStyle CssClass="altrow" />
                    <HeaderStyle CssClass="headerstyle" />
                    <RowStyle CssClass="row" HorizontalAlign="Center" />
                    <SelectedRowStyle CssClass="selectedRow" />
                    <PagerStyle CssClass="foot" />  
    
                    <EmptyDataTemplate>Brak danych</EmptyDataTemplate>
    
                </asp:GridView>
            </div>
            <div id="popupUser"  style="display:none;width:100%" title="Historia Użytkownika">
                <asp:GridView ID="gvHistoryUser" runat="server" AllowPaging="True" AutoGenerateColumns="true"
                    AllowSorting="true" PagerSettings-Mode="NumericFirstLast" 
                    CssClass="grid" PageSize="100"
                            OnSelectedIndexChanging="gvHistory_SelectedIndexChanging" DataKeyNames="user_id"
                            OnRowDataBound="gvHistoryUser_RowDataBound" OnDataBound="gvHistoryUser_DataBound" >
   
    
	                <AlternatingRowStyle CssClass="altrow" />
                    <HeaderStyle CssClass="headerstyle" />
                    <RowStyle CssClass="row" HorizontalAlign="Center" />
                    <SelectedRowStyle CssClass="selectedRow" />
                    <PagerStyle CssClass="foot" />  
    
                    <EmptyDataTemplate>Brak danych</EmptyDataTemplate>
    
                </asp:GridView>
            </div>
        </asp:Panel>
        <%--<p>        
            Use this area to provide additional information.
        </p>

        <p>        
            Use this area to provide additional information.
        </p>--%>
    </article>

    <aside>
        <h3>Klienci</h3>
       <%-- <p>        
            Use this area to provide additional information.
        </p>--%>
        <ul>
            <li><a id="aClientsReport" runat="server" href="~/Clients/ClientsReport.aspx">Edytor klientów</a></li>
            <li><a id="aAddClient" runat="server" href="~/Clients/AddClient.aspx">Dodaj klienta</a></li>
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
    function HistoryUser() {
        $("#popupUser").dialog({
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