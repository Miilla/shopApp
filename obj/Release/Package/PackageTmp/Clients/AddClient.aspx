<%@ Page Title="Dodaj Klienta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddClient.aspx.cs" Inherits="Shop.Clients.AddClient" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent" >
      <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css"/>
  <script src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
  <link rel="stylesheet" href="/resources/demos/style.css"/>  <script src="datepicker-fr.js"></script>
    <hgroup class="title">
        <h1><%: Title %>.</h1>
    </hgroup>
    <aside>
        <h3>Klienci</h3>
        <ul>
            <li><a id="aClientsReport" runat="server" href="~/Clients/ClientsReport.aspx">Klienci</a></li>
            <li><a id="aAddClient" runat="server" href="~/Clients/AddClient.aspx">Dodaj produkt</a></li>
        </ul>
        <asp:HiddenField id="hdnDate" runat="server" />
    </aside>    
        <table>
                    <tr>
                        <td><b>Imię:</b><asp:TextBox ID="tbxFname" runat="server"  ></asp:TextBox></td>
                        <td><b>Nazwisko:</b><asp:TextBox ID="tbxLname" runat="server"  ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><b>Login:</b><asp:TextBox ID="tbxLogin" runat="server"  ></asp:TextBox></td>
<%--                        <td><b>Data urodzenia:</b><asp:Calendar ID="cDate" runat="server" ></asp:Calendar></td>--%>
                        <td><b>Data urodzenia:</b> <p> <input type="text" id="datepicker"/></p></td>
                        
                    </tr>  
                    <tr>
                        <td><b>Nip:</b><asp:TextBox ID="tbxNip" runat="server"  ></asp:TextBox></td>
                        <td><b>Płeć:</b><asp:DropDownList ID="ddlPlec" runat="server">
                                             <asp:ListItem Text="Kobieta" Value="k"></asp:ListItem>
                                             <asp:ListItem Text="Mężczyzna" Value="m"></asp:ListItem>
                                        </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td><b>Ulica:</b><asp:TextBox ID="tbxStreet" runat="server"  ></asp:TextBox></td>
                        <td><b>Nr domu:</b><asp:TextBox ID="tbxHome" runat="server"  ></asp:TextBox></td>
                    </tr>             
                    <tr >
                        <td><b>Miasto:</b><asp:TextBox ID="tbxCity" runat="server"  ></asp:TextBox></td>
                        <td><b>Kod pocztowy:</b><asp:TextBox ID="tbxZip" runat="server" MaxLength="6" ></asp:TextBox></td>
                    
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
                </table> 

      <script>
          $(function () {
              $.datepicker.regional['pl'] = {
                  	                closeText: 'Zamknij',
                  	                prevText: '&#x3c;Poprzedni',
                  	                nextText: 'NastÄpny&#x3e;',
                  	                currentText: 'Dzień',
                  	                monthNames: ['StyczeÅ','Luty','Marzec','KwiecieÅ','Maj','Czerwiec',
                  	                'Lipiec','SierpieÅ','WrzesieÅ','PaÅºdziernik','Listopad','GrudzieÅ'],
                                    monthNamesShort: ['Sty','Lu','Mar','Kw','Maj','Cze',
                  	                'Lip','Sie','Wrz','Pa','Lis','Gru'],
                  	                dayNames: ['Niedziela','PoniedziaÅek','Wtorek','Åroda','Czwartek','PiÄtek','Sobota'],
                  	                dayNamesShort: ['Nie','Pn','Wt','År','Czw','Pt','So'],
                  	                dayNamesMin: ['N','Pn','Wt','Śr','Cz','Pt','So'],
                  	                weekHeader: 'Tydz',
                  	                dateFormat: 'yy.mm.dd',
                  	                firstDay: 1,
                  	                isRTL: false,
                  	                showMonthAfterYear: false,
                  	                yearSuffix: ''};
          	  $.datepicker.setDefaults($.datepicker.regional['pl']);
              $("#datepicker").datepicker({
                  changeMonth: true,
                  changeYear: true
              });

              $('#datepicker').change(function () {
                  $("#<%=hdnDate.ClientID%>").val($('#datepicker').val());
              });


              <%--dtString = $("#<%=hdnDate.ClientID%>").val();--%>
              //dtString = dtString.split(',');
              //var defaultDate = new Date(dtString[0], dtString[1], dtString[2]);
              //$("#datepicker").datepicker("setDate", defaultDate);
          });
  </script>
</asp:Content>

