<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NavBar.ascx.cs" Inherits="Controls_NavBar" %>
<div id="navBar">
    <a href="/">Home</a>
    <a href="/aboutus/">About</a>
    <asp:PlaceHolder ID="LoggedInHolder" runat="server" Visible="false">
        <a href="/cp/">My Thermostats</a>
        <a href="/cp/user.aspx">Account Settings</a>
    </asp:PlaceHolder>
</div>