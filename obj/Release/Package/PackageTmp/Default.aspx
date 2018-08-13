<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Shop._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >
    
    <h2 style="padding-left:350px;">Zaloguj się!</h2>
                <p class="validation-summary-errors" style="padding-left:350px;">
                    <asp:Literal runat="server" ID="FailureText" />
                </p>
                <fieldset style="padding-left:350px;">
                    <legend>Log in Form</legend>
                    <ol>
                        <li>
                            <asp:Label ID="Label12" runat="server" AssociatedControlID="tbxLogin">Nazwa użytkownika</asp:Label>
                            <asp:TextBox runat="server" ID="tbxLogin"></asp:TextBox>
                        </li>
                        <li>
                            <asp:Label ID="Label22" runat="server" AssociatedControlID="tbPass">Hasło</asp:Label>
                            <asp:TextBox runat="server" ID="tbPass" TextMode="Password"></asp:TextBox>
                            </li>
                        
                    </ol>
                    <asp:Button ID="Button1" runat="server" OnClick="LoginClick" Text="Zaloguj" />
                </fieldset>
</asp:Content>