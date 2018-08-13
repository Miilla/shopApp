<%@ Page Title="Dodaj zamówienie" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddOrder.aspx.cs" Inherits="Shop.Sale.AddOrder" %>

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
        <h3>Sprzedaż</h3>
       <%-- <p>        
            Use this area to provide additional information.
        </p>--%>
        <ul>
            <li><a id="aOrderRaportSale" runat="server" href="~/Sale/OrderReportSale.aspx">Zamówienia</a></li>
            <li><a id="aAddOrder" runat="server" href="~/Sale/AddOrder.aspx">Dodaj Zamówienie</a></li>
        </ul>
    </aside>
    
        <table>           
                    <tr >
                        <td><b>Wybierz klienta:  </b><asp:DropDownList ID="ddlClient" runat="server"></asp:DropDownList></td>
                    
                    </tr>
            <tr><td style="width:100%"><hr /></td></tr>
          </table>
    
    <table>
        <tr>
            
                         <td><b>Kategoria:  </b><asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem Text="Myszki"></asp:ListItem>
                                                </asp:DropDownList></td>
                        <td><b>Cena od: </b><asp:TextBox ID="TextBox3" runat="server"  Width="110px"></asp:TextBox></td>
                        <td><b>Cena do: </b><asp:TextBox ID="TextBox4" runat="server"  Width="110px"></asp:TextBox></td>
                        <%--<td><b>Ilość:</b><asp:TextBox ID="tbxIlosc" runat="server"  TextMode="Number" Enabled="false"></asp:TextBox></td>--%>
        </tr>
        <tr>
            <td colspan="2"><b>Nazwa produktu: </b><asp:TextBox ID="tbxNazwa" runat="server"  Width="200px"></asp:TextBox></td>
                        <%--<td><b>Ilość:</b><asp:TextBox ID="tbxIlosc" runat="server"  TextMode="Number" Enabled="false"></asp:TextBox></td>--%>
                        <td ><b>Data Dodania: </b><asp:TextBox ID="TextBox5" runat="server"  Width="100px"></asp:TextBox></td>
            <td><asp:Button runat="server" Text="Szukaj" /></td>
                        </tr>
    </table>
    <table> 
                    <tr>
                        <td style="width:100%">
                            <asp:GridView ID="gvMessageInfo" runat="server" AllowPaging="True" AutoGenerateColumns="true"
                            AllowSorting="true" PagerSettings-Mode="NumericFirstLast" AutoGenerateSelectButton="true"
                            CssClass="grid" PageSize="5"
                                    OnSelectedIndexChanging="gvMessageInfo_SelectedIndexChanging" DataKeyNames="prod_id"
                                     OnDataBound="gvMessageInfo_DataBound" OnPageIndexChanging="gvMessageInfo_PageIndexChanging">
   
    
	                        <AlternatingRowStyle CssClass="altrow" />
                            <HeaderStyle CssClass="headerstyle" />
                            <RowStyle CssClass="row" HorizontalAlign="Center" />
                            <SelectedRowStyle CssClass="selectedRow" />
                            <PagerStyle CssClass="foot" />
    
    
                            <EmptyDataTemplate>Brak danych</EmptyDataTemplate>
    
                        </asp:GridView>

                        </td>
                        <%--<asp:TextBox ID="tbxAktywny" runat="server" enabled="false" ></asp:TextBox>--%>
                        <%--<td><b>Cena:</b><asp:TextBox ID="tbxPrice" runat="server"  ></asp:TextBox></td>
                        <td><b>Główna:  </b><asp:CheckBox ID="cbBanner" runat="server" /></td> --%>
                    </tr>  
                    <tr><td ><b>Produkty:</b></td></tr>
                    <tr>
                        <td><asp:ListBox ID="lbProducts" runat="server" Width="100%"></asp:ListBox></td>
                        <td><asp:Button ID="btnRemove" runat="server" Text="Usuń produkt z listy" OnClick="btnRemove_Click" UseSubmitBehavior="false"/></td>
                        <%--<td colspan="2"><asp:TextBox ID="tbxProducts" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox></td>--%>
                        
                    </tr>          
            <tr>
                
                        <td><b>Suma:</b><asp:TextBox ID="tbxSum" runat="server" Enabled="false" ></asp:TextBox></td>
            </tr>    
                    <tr style="display:none">
                        <%--<td><b>Suma:</b><asp:TextBox ID="tbxSum" runat="server" Enabled="false" ></asp:TextBox></td>--%>
                        <td><b>Kategoria:  </b><asp:DropDownList ID="ddlCategories" runat="server" Visible="false"></asp:DropDownList></td>
                    
                    </tr>
                </table>
                <hr />
                <table>
                    <tr>
                        <td >
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Zapisz"/>
                        </td>
                        <td >
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

