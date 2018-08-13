<%@ Page Title="Zmiana hasła" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Shop.Accounts.ChangePassword" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >
    <hgroup class="title">
        <h1><%: Title %>.</h1>
    </hgroup>
    <aside>
        <h3>Zarządzaj</h3>
        <ul>
            <li><a id="aChangePassword" runat="server" href="~/Accounts/ChangePassword.aspx">Zmień hasło</a></li>
            <li><a id="aChangeData" runat="server" href="~/Accounts/ChangeData.aspx">Zmień dane</a></li>
            <li><a id="aUsers" runat="server" href="~/Accounts/Users.aspx">Użytkownicy</a></li>
            <li><a id="aGroups" runat="server" href="~/Accounts/AddGroup.aspx">Dodaj grupe</a></li>
        </ul>
    </aside>    
    <table>
        <tr>
            <td><asp:Label ID="Label1" runat="server" Text="Stare hasło:"></asp:Label></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="tbxPasswordOld" runat="server" TextMode="Password" onchange="confirm_pass()"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label2" runat="server" Text="Nowe hasło:"></asp:Label></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="tbxPasswordNew" runat="server" TextMode="Password" onchange="confirm_pass()"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label3" runat="server" Text="Potwierdź hasło:"></asp:Label></td>
        </tr>
        <tr>
            <td><asp:TextBox ID="tbxPasswordConfirm" runat="server" TextMode="Password" onchange="confirm_pass()"></asp:TextBox></td><td><p  class="validation-summary-errors" >
            <asp:Label runat="server" ID="FailureText" />
                </p></td>
        </tr>
        <tr>
            <td>
                <input id="btnSave" type="button" value="Zatwierdź"  onclick="Save();" /> 
            </td>
        </tr>
    </table>
    
<script type="text/javascript" >
    
    function confirm_pass() {
        var tbxPasswordNew = document.getElementById('<%=tbxPasswordNew.ClientID%>').value;
        var tbxPasswordConfirm = document.getElementById('<%=tbxPasswordConfirm.ClientID%>').value;
        var tbxPasswordOld = document.getElementById('<%=tbxPasswordOld.ClientID%>').value;
        if (tbxPasswordConfirm == "") {
            document.getElementById('<%=FailureText.ClientID%>').innerHTML = "Wypełnij!";
            return false;
        }
        else if (tbxPasswordNew != tbxPasswordConfirm)
        {
            document.getElementById('<%=FailureText.ClientID%>').innerHTML = "Hasła nie są takie same!";
            return false;
        }
        else if (tbxPasswordNew == tbxPasswordOld) {
            document.getElementById('<%=FailureText.ClientID%>').innerHTML = "Nowe hasło musi się różnić od starego!";
                return false;
        }
        else if (tbxPasswordNew.length<7) {
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
                document.getElementById("<%=tbxPasswordOld.ClientID%>").value,
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
            location.href = "Manage.aspx?succes=1";
        }
    }
    function onError() {
    }
</script> 

    
</asp:Content>

