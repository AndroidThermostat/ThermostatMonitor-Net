<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="cp_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Thermostats</h1>
    <table>
        <asp:Literal ID="ThermostatsLit" runat="server" />
        <tr><td></td><td> - <a href="editthermostat.aspx">(Add a new thermostat)</a></td></tr>
    </table>
    
    <br /><br /><br /><br />
    
</asp:Content>

