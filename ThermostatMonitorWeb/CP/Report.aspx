<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ControlPanel.master" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="CP_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <form id="Form1" runat="server">
        <div style="width:250px;float:right;padding-left:20px;">
            <b>Date Range</b><br />
            <asp:TextBox ID="StartDateText" runat="server" style="width:80px;" /> - <asp:TextBox ID="EndDateText" runat="server" style="width:80px;" /><br />
            <asp:CheckBox ID="IncludePrevious" Text="Compare to past" runat="server" /><br />
            <asp:TextBox ID="PrevStartDateText" runat="server" style="width:80px;" /> - <asp:TextBox ID="PrevEndDateText" runat="server" style="width:80px;" /><br />
            <asp:Button ID="ApplyButton" runat="server" Text="Apply" OnClick="ApplyButton_Click" />
        </div>
        <h1>Report for Thermostat: <asp:Literal ID="ThermostatName" runat="server" /></h1>
        <p><i>These reports allow you to compare performance over different date ranges.  You can use these reports to see how changes like changing air filters
        or adjusting the swing on your thermostat affect system performance.  The accuracy of these reports improve with wider date ranges.</i></p>
        
        <div class="clear"></div>
        
        <h2>Average Run Time by Time of Day</h2>
        The perecent of each hour that the A/C or furnace has run.
        <iframe src="<%=TimeOfDayUrl%>" width="980" height="210" frameborder="0" marginheight="0" marginwidth="0"></iframe>
    
        
        <h2>Average Run Time by Temperature Delta</h2>
        <p>The percentage of time the A/C or furnace runs for each degree of temperature delta.  An outside temperatue of 90° and inside of 75° would be a 15° temperature delta.</p>
        <iframe src="<%=DeltaUrl%>" width="980" height="210" frameborder="0" marginheight="0" marginwidth="0"></iframe>
        
    </form>
</asp:Content>