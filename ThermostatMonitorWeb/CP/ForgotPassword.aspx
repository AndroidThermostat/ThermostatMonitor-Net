<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="cp_ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<form id="Form1" runat="server">
    <h1>Forgot Password</h1>
    <table align="center">
        <asp:Literal ID="ErrorLit" runat="server" />
        <tr><td>Email</td><td><asp:TextBox ID="EmailText" runat="server" /></td></tr>
        <tr><td colspan="2" align="center"><asp:Button ID="ResetButton" runat="server" Text="Reset" onclick="ResetButton_Click" /></td></tr>
    </table>
</form>
</asp:Content>
