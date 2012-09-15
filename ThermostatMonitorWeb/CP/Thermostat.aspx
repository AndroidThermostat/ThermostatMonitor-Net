<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ControlPanel.master" AutoEventWireup="true" CodeFile="Thermostat.aspx.cs" Inherits="cp_Thermostat" %>

<%@ Register src="Controls/HistorySummary.ascx" tagname="HistorySummary" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <h1>Thermostat: <asp:Literal ID="ThermostatName" runat="server" /></h1>
    <b><a href="report.aspx?thermostatId=<%=Request["id"]%>">Run Reports</a></b></br><br />
    
    <div><b><asp:Literal ID="CurrentConditionsLit" runat="server" /></b></div>
    <div><b>CSV Downloads: </b><a href="csvdownload.aspx?reporttype=summary&thermostatid=<%=Request["id"]%>">Summary</a> | <a href="csvdownload.aspx?reporttype=cycles&thermostatid=<%=Request["id"]%>">Cycles</a></div><br />
    
    <h2>Recent History</h2>
    <uc1:HistorySummary ID="HistorySummary1" runat="server" />
    <br />
    
    <p><i>*Chart data becomes more accurate over time</i></p>
    
    <h2>Average Run Time by Time of Day</h2>
    The perecent of each hour that the A/C or furnace has run.
    <iframe src="/cp/charts/hours.aspx?thermostatId=<%=Request["id"]%>&timezoneDifference=<%=TimezoneDifference %>" width="980" height="210" frameborder="0" marginheight="0" marginwidth="0"></iframe>
    
    <h2>Average Run Time by Temperature Delta</h2>
    <p>The percentage of time the A/C or furnace runs for each degree of temperature delta.  An outside temperatue of 90° and inside of 75° would be a 15° temperature delta.</p>
    <iframe src="/cp/charts/delta.aspx?thermostatId=<%=Request["id"]%>" width="980" height="210" frameborder="0" marginheight="0" marginwidth="0"></iframe>
    
    
    

</asp:Content>

