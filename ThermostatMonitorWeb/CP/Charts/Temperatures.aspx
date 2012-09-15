<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Blank.master" AutoEventWireup="true" CodeFile="Temperatures.aspx.cs" Inherits="cp_Charts_Temperatures" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<body style="margin:0px;">
<script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
      google.load('visualization', '1', {'packages':['annotatedtimeline']});

      google.setOnLoadCallback(drawChart);
      function drawChart() {
        var data = new google.visualization.DataTable();
        data.addColumn('date', 'Time');
        data.addColumn('number', 'Indoors');
        data.addColumn('number', 'Outdoors');
        <%=Data %>
      }
      
      function addData(data, times, degrees)
      {
        for (i=0;i<times.length;i++)
        {
            data.setValue(i,0,times[i]);
            data.setValue(i,1,degrees[i]);
        }
      }
    </script>
    <asp:Literal id="ChartDiv" runat="server" />
</body>
</asp:Content>

