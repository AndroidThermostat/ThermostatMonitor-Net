<%@ Page Title="Thermostat Monitor - Track usage of WiFi Enabled Thermostats" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"  %>
<%@ Register src="Controls/SlideShow.ascx" tagname="SlideShow" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="/slides/agile_carousel.css" type='text/css'>
    <script src="/slides/agile_carousel.a1.1.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uc1:SlideShow ID="SlideShow1" runat="server" />
    <br />
    <h1>Introducing Thermostat Monitor</h1>    <img src="/images/desktopapp.gif" align="right" style="margin:0px 0px 10px 10px;" />    <p>Thermostat Montior is an open source project for tracking thermostat usage patterns.  It currently supports WiFi enabled thermostats made by <a href="http://radiothermostat.com/">Radio Thermostat</a>.</p>    <h2>How it Works</h2>    <p>Thermostat Monitor is a free, open source solution that you can use with our hosted website or a copy running on your own server.  To get started <a href="/cp/register.aspx">register</a> an account and tell us about your thermostats.  Then download the desktop application that will periodically ping your thermostat to receive data and ping our server to publish that data.  You can then log in and view your thermostat usage history.</p>
    
    <h2>Completely Open</h2>
    <p>Thermostat Monitor is completely open.  You can use the website for free or you can download the source code and host your own copy.  You can also export your data in CSV format.</p>
    
</asp:Content>

