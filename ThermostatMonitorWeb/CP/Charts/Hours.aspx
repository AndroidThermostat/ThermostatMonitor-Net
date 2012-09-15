<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Blank.master" AutoEventWireup="true" CodeFile="Hours.aspx.cs" Inherits="cp_Charts_Hours" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

<body style="margin:0px;">
<script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
      google.load("visualization", "1", {packages:["corechart"]});

      google.setOnLoadCallback(drawChart);
      function drawChart() {
      
        var data = google.visualization.arrayToDataTable([
            <%=Data %>
        ]);
        var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));
        chart.draw(data, {width: 970, height: 200, scaleType:'maximized', legend:'none', hAxis: { showTextEvery:2 }, chartArea: { width:935 }, colors: ['#4c78d1', '#dc3912', '#97acd5', '#dc8773'] });
      }

    </script>
    <div id="chart_div" style="width: 970px; height: 200px;"></div>
</body>

</asp:Content>

