<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ControlPanel.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="cp_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

<form id="Form1" runat="server">
    <h1>Account Details</h1>
    <asp:Literal ID="ErrorLit" runat="server" />
    <table>
        <tr><td>Password</td><td><asp:TextBox ID="PasswordText" TextMode="Password" runat="server" /> (Leave blank if you do not wish to change)</td></tr>
        <tr><td>Verify Password</td><td><asp:TextBox ID="Password2Text" TextMode="Password" runat="server" /></td></tr>
        <tr><td colspan="2" align="center"><asp:Button ID="SaveButton" runat="server" 
                Text="Save" onclick="SaveButton_Click" /></td></tr>
    </table>
</form>
</asp:Content>

