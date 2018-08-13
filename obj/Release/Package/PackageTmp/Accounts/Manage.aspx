<%@ Page Title="Zarządzaj" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="Shop.Accounts.Manage" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <%--<h2>Your app description page.</h2>--%>
    </hgroup>
    <article>
        <p  class="validation-summary-success" ><asp:Label runat="server" ID="Status" ></asp:Label> </p>
        <p>
                    
            Tutaj możesz zmienić swoje podstawowe dane.
        </p>

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
           <%-- <li><a id="aChangeData" runat="server" href="~/Accounts/ChangeData.aspx">Zmień dane</a></li>--%>
            <li><a id="aUsers" runat="server" href="~/Accounts/Users.aspx">Użytkownicy</a></li>
            <li><a id="aGroups" runat="server" href="~/Accounts/AddGroup.aspx">Dodaj grupe</a></li>
            <%--<li><a id="A3" runat="server" href="~/Contact.aspx">Contact</a></li>--%>
        </ul>
    </aside>
</asp:Content>