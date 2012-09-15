<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ControlPanel.master" AutoEventWireup="true" CodeFile="Day.aspx.cs" Inherits="cp_Day" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

<h1><asp:Literal ID="ThermostatName" runat="server" /> - Summary for <asp:Literal ID="DateLit" runat="server" /></h1>

<h2>Temperature</h2>
<iframe src="/cp/charts/temperatures.aspx?thermostatId=<%=Request["thermostatid"]%>&date=<%=Request["date"]%>&timezoneDifference=<%=TimezoneDifference %>" width="980" height="210" frameborder="0" marginheight="0" marginwidth="0"></iframe>
<br /><br />
<h2>Cycles</h2>
<iframe src="/cp/charts/cycles.aspx?thermostatId=<%=Request["thermostatid"]%>&date=<%=Request["date"]%>&mode=cycles&timezoneDifference=<%=TimezoneDifference %>" width="980" height="110" frameborder="0" marginheight="0" marginwidth="0"></iframe>
<br /><br />
<h2>Percentage Run Time</h2>
<iframe src="/cp/charts/hours.aspx?thermostatId=<%=Request["thermostatid"]%>&startDate=<%=Request["date"]%>&endDate=<%=Request["date"]%>&&timezoneDifference=<%=TimezoneDifference %>" width="980" height="210" frameborder="0" marginheight="0" marginwidth="0"></iframe>

</asp:Content>

