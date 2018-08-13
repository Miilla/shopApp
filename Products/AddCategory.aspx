<%@ Page Title="Dodaj kategorie" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddCategory.aspx.cs" Inherits="ShopCopyForXML.Products.AddCategory" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >
    <%--<asp:ScriptManager ID="scriptManager" runat="server" EnablePageMethods="true" ScriptMode="Release"
       
        EnablePartialRendering="false">
        <Scripts>
            <asp:ScriptReference Path="Scripts/jquery-1.7.1.min.js" />
        </Scripts>
    </asp:ScriptManager>--%>
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <%--<h2>Your app description page.</h2>--%>
    </hgroup>
    <aside>
        <h3>Magazyn</h3>
       <%-- <p>        
            Use this area to provide additional information.
        </p>--%>
        <ul>
            <li><a id="aProductsReport" runat="server" href="~/Storehouse/ProductsReport.aspx">Magazyn</a></li>
            <li><a id="aAddProduct" runat="server" href="~/Products/AddProduct.aspx">Dodaj produkt</a></li>
            <li><a id="aAddCategory" runat="server" href="~/Products/AddCategory.aspx">Dodaj kategorie </a></li>
            <li><a id="aOrderReport" runat="server" href="~/Storehouse/OrderReport.aspx">Zamówienia</a></li>
            <%--<li><a id="A3" runat="server" href="~/Contact.aspx">Contact</a></li>--%>
        </ul>
    </aside>
    
        <table>
                    <tr>
                        <td><b>Podaj Nazwe Kategori:</b><asp:TextBox ID="tbxNazwa" runat="server"  ></asp:TextBox></td>
                        <%--<td><b>Ilość:</b><asp:TextBox ID="tbxIlosc" runat="server"  TextMode="Number" Enabled="false"></asp:TextBox></td>--%>
                    </tr>
                    <tr>
                        <%--<asp:TextBox ID="tbxAktywny" runat="server" enabled="false" ></asp:TextBox>--%>
                        <%--<td><b>Cena:</b><asp:TextBox ID="tbxPrice" runat="server"  ></asp:TextBox></td>
                        <td><b>Główna:  </b><asp:CheckBox ID="cbBanner" runat="server" /></td> --%>
                    </tr>  
                    <%--<tr><td ><b>Opis:</b></td></tr>
                    <tr>
                        
                        <td colspan="2"><asp:TextBox ID="tbxOpis" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>
                        
                    </tr> --%>             
                    <tr >
                        <%--<td><b>Lokalizacja zdjęcia:</b><asp:TextBox ID="tbxPath" runat="server"  ></asp:TextBox></td>
                        <td><b>Kategoria:  </b><asp:DropDownList ID="ddlCategories" runat="server"></asp:DropDownList></td>--%>
                    
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
    
    <%--<article>aa
        <p>        
            Tutaj możesz zmienić swoje podstawowe dane.
        </p>
    </article>--%>
    

    
</asp:Content>

