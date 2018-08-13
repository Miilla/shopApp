<%@ Page Title="Dodaj produkt" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="Shop.Products.AddProduct" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >
        <hgroup class="title">
            <h1><%: Title %>.</h1>
        </hgroup>
        <aside>
            <h3>Magazyn</h3>
            <ul>
                <li><a id="aProductsReport" runat="server" href="~/Storehouse/ProductsReport.aspx">Magazyn</a></li>
                <li><a id="aAddProduct" runat="server" href="~/Products/AddProduct.aspx">Dodaj produkt</a></li>
                <li><a id="aAddCategory" runat="server" href="~/Products/AddCategory.aspx">Dodaj kategorie</a></li>
                <li><a id="aOrderReport" runat="server" href="~/Products/OrderReport.aspx">Zamówienia</a></li>
            </ul>
        </aside>    
        <table>
            <tr>
                <td><b>Nazwa produktu:</b><asp:TextBox ID="tbxNazwa" runat="server"  ></asp:TextBox></td>
                <td><b>Ilość:</b><asp:TextBox ID="tbxIlosc" runat="server"  TextMode="Number" ></asp:TextBox></td>
            </tr>
            <tr>
                <td><b>Cena:</b><asp:TextBox ID="tbxPrice" runat="server"  ></asp:TextBox></td>
                <td><b>Sklep:  </b><asp:CheckBox ID="cbBanner" runat="server" Enabled="false" /></td> 
            </tr>  
            <tr><td ><b>Opis:</b></td></tr>
            <tr>
                        
                <td colspan="2"><asp:TextBox ID="tbxOpis" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                        
            </tr>              
            <tr >
                <td><b>Lokalizacja zdjęcia:</b><asp:TextBox ID="tbxPath" runat="server"  ></asp:TextBox></td>
                <td><b>Kategoria:  </b><asp:DropDownList ID="ddlCategories" runat="server"></asp:DropDownList></td>
                    
            </tr>
        </table>
        <hr />
        <table>
            <tr>
                <td >
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Zapisz"/>
                </td>
                <td>
                    <p  class="validation-summary-errors" >
                    <asp:Label runat="server" ID="FailureText" />
                    </p>
                </td>
            </tr>
            <tr>        
            </tr>
        </table>    
</asp:Content>

