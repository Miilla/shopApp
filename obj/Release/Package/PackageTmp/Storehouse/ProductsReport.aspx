<%@ Page Title="Magazyn" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductsReport.aspx.cs" Inherits="Shop.Storehouse.ProductsReport" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
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
                    OnSelectedIndexChanging="gvMessageInfo_SelectedIndexChanging" DataKeyNames="prod_id"
                    OnRowDataBound="gvMessageInfo_RowDataBound" OnDataBound="gvMessageInfo_DataBound" OnPageIndexChanging="gvMessageInfo_PageIndexChanging">
   
    
	        <AlternatingRowStyle CssClass="altrow" />
            <HeaderStyle CssClass="headerstyle" />
            <RowStyle CssClass="row" HorizontalAlign="Center" />
            <SelectedRowStyle CssClass="selectedRow" />
            <PagerStyle CssClass="foot" />
    
    
            <EmptyDataTemplate>Brak danych</EmptyDataTemplate>
    
        </asp:GridView>
            <p  class="validation-summary-success" ><asp:Label runat="server" ID="Success" ></asp:Label> </p>
        
        
        <asp:Panel ID="pProduct" runat="server" Visible="false">
            <p  class="validation-summary-success" ><asp:Label runat="server" ID="Status" ></asp:Label> </p>
            <br />
            <asp:LinkButton ID="lbtnData" runat="server" OnClick="lbtnData_Click" ><b>Dane Produktu</b></asp:LinkButton> <%--| <asp:LinkButton ID="lbtnPassword" runat="server" OnClick="lbtnPassword_Click"><b>Zmień hasło</b></asp:LinkButton>--%>
            
            <hr />
            <div id="dData" runat="server">
                <table>
                    <tr>
                        <td><b>Nazwa produktu:</b><asp:TextBox ID="tbxNazwa" runat="server"  Enabled="false"></asp:TextBox></td>
                        <td><b>Ilość:</b><asp:TextBox ID="tbxIlosc" runat="server"  TextMode="Number"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <%--<asp:TextBox ID="tbxAktywny" runat="server" enabled="false" ></asp:TextBox>--%>
                        <td><b>Cena:</b><asp:TextBox ID="tbxPrice" runat="server" Enabled="false" ></asp:TextBox></td>
                        <td><b>Sklep:  </b><asp:CheckBox ID="cbBanner" runat="server" Enabled="false" /></td> 
                    </tr>  
                    <tr><td ><b>Opis:</b></td></tr>
                    <tr>
                        
                        <td colspan="2"><asp:TextBox ID="tbxOpis" runat="server" TextMode="MultiLine" Width="100%" Enabled="false"></asp:TextBox></td>
                        
                    </tr>              
                    <tr >
                        <td><b>Lokalizacja zdjęcia:</b><asp:TextBox ID="tbxPath" runat="server" Enabled="false" ></asp:TextBox></td>
                        <td><b>Kategoria:  </b><asp:DropDownList ID="ddlCategories" runat="server"></asp:DropDownList></td>
                    
                    </tr>
                </table>
                <hr />
                <table>
                    <tr>
                        <td style="width:100%">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Zapisz"/>
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
                            <asp:Button runat="server" ID="btnSavePswd" Text="Zatwierdź" OnClick="btnSavePswd_Click"/>
                            <%--<input id="Button1" type="button" value="Zatwierdź"  onclick="Save();" /> --%>
                        </td>
                    </tr>
                </table>
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
        <h3>Magazyn</h3>
       <%-- <p>        
            Use this area to provide additional information.
        </p>--%>
        <ul>
            <li><a id="aProductsReport" runat="server" href="~/Storehouse/ProductsReport.aspx">Magazyn</a></li>
            <li><a id="aAddProduct" runat="server" href="~/Products/AddProduct.aspx">Dodaj produkt</a></li>
            <li><a id="aAddCategory" runat="server" href="~/Products/AddCategory.aspx">Dodaj kategorie</a></li>
            <li><a id="aOrderReport" runat="server" href="~/Storehouse/OrderReport.aspx">Zamówienia</a></li>
            <%--<li><a id="aUsers" runat="server" href="~/Accounts/Users.aspx">Użytkownicy</a></li>--%>
            <%--<li><a id="A3" runat="server" href="~/Contact.aspx">Contact</a></li>--%>
        </ul>
    </aside>

    
<script type="text/javascript" >

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