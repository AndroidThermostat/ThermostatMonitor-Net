<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ControlPanel.master" AutoEventWireup="true" CodeFile="EditThermostat.aspx.cs" Inherits="cp_EditThermostat" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<form id="Form1" runat="server">
    <h1>Edit Thermostat</h1>
    <p>These settings can be changed at any time.  SEER and Tons are used to calculate A/C power consumption for energy usage reports.  <a target="_blank" href="http://www.inspectapedia.com/aircond/aircond06.htm">Here</a> an article on how to find the tons of your unit from the model number.  The SEER rating can using be found from the manufactuers website.</p>
    
    <asp:Literal ID="ErrorLit" runat="server" />
    <table>
        <tr><td>Location</td><td><asp:DropDownList ID="LocationList" runat="server" DataTextField="Name" DataValueField="Id" /></td></tr>
        <tr><td>Display Name</td><td><asp:TextBox ID="DisplayNameText" runat="server" /></td></tr>
        <tr><td>Brand</td><td><asp:DropDownList ID="BrandList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="BrandList_SelectedIndexChanged" >
            <asp:ListItem Value="RTCOA" Text="Radio Thermostat" />
            <asp:ListItem Value="Android Thermostat" Text="Android Thermostat" />
        </asp:DropDownList></td></tr>
        <asp:PlaceHolder ID="RadioThermostatHolder" runat="server">
            <tr><td>IP Address</td><td><asp:TextBox ID="IpAddressText" runat="server" /> (Interal IP - ex. 192.168.1.106)</td><td></td></tr>
            <tr><td colspan="3"><br />
                <i style="font-size:8pt;">* With Radio Thermostat models it is necessary to download and run the desktop application in order to track usage (<a href="/downloads/thermostatmonitor.zip">Windows</a> | <a href="/downloads/thermostatmonitor_mac.zip">Mac</a>).  The desktop application will ask you for an API key.  Enter the API key listed on the <a href="/cp/" target="_blank">My Thermostats</a> page, next to the location name.  This application must remain running in order to continue to monitor your thermostat usage.</i>
            </td></tr>
        </asp:PlaceHolder>
        <asp:Literal ID="AndroidThermostatLit" runat="server" />
        
        <tr><td colspan="2"><br /><b>A/C Information</b></td></tr>
        <tr><td>Tons</td><td><asp:TextBox ID="TonsText" runat="server" Text="3.5" /></td></tr>
        <tr><td>SEER</td><td><asp:TextBox ID="SeerText" runat="server" Text="16" /></td></tr>
        <tr><td>Energy Used</td><td><asp:Label ID="KilowattsLabel" runat="server" Text="2.625kw" /></td></tr>
        
        <tr><td colspan="2"><br /><b>Heater Information</b></td></tr>
        <tr><td>Input BTU/hour</td><td><asp:TextBox ID="HeatBtuPerHourText" runat="server" Text="100,000" /></td></tr>
        
        <tr><td colspan="2"><br /><b>Blower Fan Information</b></td></tr>
        <tr><td>Energy Used</td><td><asp:TextBox ID="FanKilowattsText" runat="server" Text="0.5" />kw</td></tr>
        <tr><td colspan="2" align="center"><asp:Button ID="SaveButton" runat="server" Text="Save" onclick="SaveButton_Click" />
        <asp:Button ID="DeleteButton" runat="server" Text="Delete" 
                onclick="DeleteButton_Click" />
        </td></tr>
    </table>
</form>
</asp:Content>

