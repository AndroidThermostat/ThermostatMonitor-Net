<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="cp_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<form runat="server">
<h1>Login</h1>
<table align="center">
    <asp:Literal ID="ErrorLit" runat="server" />
    <tr><td>Email</td><td><asp:TextBox ID="EmailText" runat="server" /></td></tr>
    <tr><td>Password</td><td><asp:TextBox ID="PasswordText" TextMode="Password" runat="server" /></td></tr>
    <tr><td colspan="2" align="center"><asp:Button ID="LoginButton" runat="server" Text="Login" onclick="LoginButton_Click" /><br /><a href="/cp/forgotpassword.aspx">Forgot Password?</a></td></tr>
</table>
</form>
</asp:Content>

