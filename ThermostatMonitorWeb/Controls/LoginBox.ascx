<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginBox.ascx.cs" Inherits="Controls_LoginBox" %>
<div id="loginBox">
<asp:PlaceHolder ID="LoggedOutHolder" runat="server">
    <table cellpadding="2" cellspacing="0">
        <tr><td>Email:</td><td><input type="text" id="loginEmail" /></td></tr>
        <tr><td>Password:</td><td><input id="loginPassword" type="password" /></td></tr>
        <tr><td colspan="2" align="center"><a href="/cp/register.aspx">Register</a> | <a href="/cp/forgotpassword.aspx">Forgot Password</a></td></tr>
    </table>
</asp:PlaceHolder>
<asp:PlaceHolder ID="LoggedInHolder" runat="server" Visible="false">
    <b>Welcome back:</b> <asp:Literal ID="EmailLit" runat="server" /><br />
    <a href="/cp/login.aspx?action=logout">Log out</a>
</asp:PlaceHolder>
</div>