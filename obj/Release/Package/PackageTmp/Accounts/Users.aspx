<%@ Page Title="Użytkownicy" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Shop.Accounts.Users" %>
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
                    OnSelectedIndexChanging="gvMessageInfo_SelectedIndexChanging" DataKeyNames="user_id"
                    OnRowDataBound="gvMessageInfo_RowDataBound" OnDataBound="gvMessageInfo_DataBound" OnPageIndexChanging="gvMessageInfo_PageIndexChanging">
   
    
	        <AlternatingRowStyle CssClass="altrow" />
            <HeaderStyle CssClass="headerstyle" />
            <RowStyle CssClass="row" HorizontalAlign="Center" />
            <SelectedRowStyle CssClass="selectedRow" />
            <PagerStyle CssClass="foot" />
    
    
            <EmptyDataTemplate>Brak danych</EmptyDataTemplate>
    
        </asp:GridView>
        
        
        <asp:Panel ID="pUser" runat="server" Visible="false">
            <p  class="validation-summary-success" ><asp:Label runat="server" ID="Status" ></asp:Label> </p>
            <br />
            <asp:LinkButton ID="lbtnData" runat="server" OnClick="lbtnData_Click" ><b>Dane Użytkownika</b></asp:LinkButton> | <asp:LinkButton ID="lbtnPassword" runat="server" OnClick="lbtnPassword_Click"><b>Zmień hasło</b></asp:LinkButton>
            
            <hr />
            <div id="dData" runat="server">
                <table>
                    <tr>
                        <td><b>Imię:</b><asp:TextBox ID="tbxImię" runat="server"  ></asp:TextBox></td>
                        <td><b>Nazwisko:</b><asp:TextBox ID="tbxNazwisko" runat="server"  ></asp:TextBox></td>
                    </tr>
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
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Zapisz"/>
                        </td>
                        <td>
                            <%--<asp:Button ClientIDMode="Static" runat="server" ID="btnHistory" onclick="History();" Text="Historia" />--%>
                            <input id="btnHistory" type="button" value="Historia"  onclick="History();" /> 
                        </td>
                    </tr>
                </table>
            </div>
            <div id="dPassword" runat="server" visible="false">
                <table>
                    <tr>
                        <td><b>Nowe hasło:</b><asp:TextBox ID="tbxPasswordNew" runat="server" TextMode="Password" onchange="confirm_pass()"></asp:TextBox></td>
                    
                        <td><b>Potwierdź hasło:</b><asp:TextBox ID="tbxPasswordConfirm" runat="server" TextMode="Password" onchange="confirm_pass()"></asp:TextBox></td><td><p  class="validation-summary-errors" >
                        <asp:Label runat="server" ID="FailureText" />
                            </p></td>
                    </tr>
                </table>
                <hr />
                <table>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnSavePswd" Text="Zatwierdź" OnClick="btnSavePswd_Click" Width="100%"/>
                            <%--<input id="Button1" type="button" value="Zatwierdź"  onclick="Save();" /> --%>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
        
        <div id="popup"  style="display:none;width:100%" title="Historia użytkownika">
                <asp:GridView ID="gvHistory" runat="server" AllowPaging="True" AutoGenerateColumns="true"
                    AllowSorting="true" PagerSettings-Mode="NumericFirstLast" 
                    CssClass="grid" PageSize="100"
                            OnSelectedIndexChanging="gvHistory_SelectedIndexChanging" DataKeyNames="user_id"
                            OnRowDataBound="gvHistory_RowDataBound" OnDataBound="gvHistory_DataBound" OnPageIndexChanging="gvHistory_PageIndexChanging">
   
    
	                <AlternatingRowStyle CssClass="altrow" />
                    <HeaderStyle CssClass="headerstyle" />
                    <RowStyle CssClass="row" HorizontalAlign="Center" />
                    <SelectedRowStyle CssClass="selectedRow" />
                    <PagerStyle CssClass="foot" />  
    
                    <EmptyDataTemplate>Brak danych</EmptyDataTemplate>
    
                </asp:GridView>
        </div>
        <%--<p>        
            Use this area to provide additional information.
        </p>

        <p>        
            Use this area to provide additional information.
        </p>--%>
    </article>

    <aside>
        <h3>Zarządzaj</h3>
       <%-- <p>        
            Use this area to provide additional information.
        </p>--%>
        <ul>
            <li><a id="aChangePassword" runat="server" href="~/Accounts/ChangePassword.aspx">Zmień hasło</a></li>
            <li><a id="aChangeData" runat="server" href="~/Accounts/ChangeData.aspx">Zmień dane</a></li>
            <%--<li><a id="aChangeData" runat="server" href="~/Accounts/ChangeData.aspx">Zmień dane</a></li>--%>
            <li><a id="aUsers" runat="server" href="~/Accounts/Users.aspx">Użytkownicy</a></li>
            <li><a id="aGroups" runat="server" href="~/Accounts/AddGroup.aspx">Dodaj grupe</a></li>
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
            width:1200,
            height:400,
            buttons: {
                Zamknij: function () {
                    $(this).dialog("close");
                }
            }
        });
    }
    //$("#btnHistory").live("click", function () {
    //    $("#popup").dialog({
    //        title: "History", width: 600
    //    })
    //    return false;
    //});

    function confirm_pass() {
        var tbxPasswordNew = document.getElementById('<%=tbxPasswordNew.ClientID%>').value;
        var tbxPasswordConfirm = document.getElementById('<%=tbxPasswordConfirm.ClientID%>').value;
        if (tbxPasswordConfirm == "") {
            document.getElementById('<%=FailureText.ClientID%>').innerHTML = "Wypełnij!";
            return false;
        }
        else if (tbxPasswordNew != tbxPasswordConfirm) {
            document.getElementById('<%=FailureText.ClientID%>').innerHTML = "Hasła nie są takie same!";
            return false;
        }
        else if (tbxPasswordNew.length < 7) {
            document.getElementById('<%=FailureText.ClientID%>').innerHTML = "Hasło powinno składać się z przynajmniej 8 znaków";
            return false;
        }
        else {
            document.getElementById('<%=FailureText.ClientID%>').innerHTML = "";
            return true;
        }

    }
    function Save() {
        if (confirm_pass()) {
            PageMethods.SavePass(
                document.getElementById("<%=tbxPasswordNew.ClientID%>").value,               
                dbAction_Callback
                );
        }
    }
    function dbAction_Callback(result) {
        if (result.errorMessage != null && result.errorMessage != '') {
            document.getElementById('<%=FailureText.ClientID%>').innerHTML = result.errorMessage;
        }
        else {
            alert(1);
            PageMethods.AfterSave(
                AfterSave_Callback
                );
        }
    }
    function AfterSave_Callback() {
        var o;
    }
</script> 

</asp:Content>