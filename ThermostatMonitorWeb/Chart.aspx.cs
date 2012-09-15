using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Chart : System.Web.UI.Page
{
    public string Data = "";
    public int TimezoneDifference = 0;

    public void FillInDataTable(DataTable dt)
    {
        double column1 = 0;
        double column2 = 0;
        
        foreach (DataRowView row in dt.DefaultView)
        {
            if (column1 == 0 && !Convert.IsDBNull(row[1])) column1 = Convert.ToDouble(row[1]);
            if (column2 == 0 && !Convert.IsDBNull(row[2])) column2 = Convert.ToDouble(row[2]);
        }
        

        foreach (DataRowView row in dt.DefaultView)
        {
            if (Convert.IsDBNull(row[1]))
            {
                row[1] = column1;
            } else {
                column1 = Convert.ToDouble(row[1]);
            }
            if (Convert.IsDBNull(row[2])) { 
                row[2] = column2; 
            } else { 
                column2 = Convert.ToDouble(row[2]); 
            }

        }

    }

    public void OutputData(DataTable dt)
    {
        dt.DefaultView.Sort = "LogDate";
        //FillInDataTable(dt);
        
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("data.addRows(" + dt.Rows.Count + ");\n");
        for (int i=0;i<dt.DefaultView.Count;i++)
        {
            DataRowView row = dt.DefaultView[i];//.Rows[i];
            DateTime adjustedDate=Convert.ToDateTime(row[0]).AddHours(TimezoneDifference);
            sb.Append("data.setValue(" + i.ToString() + ",0,new Date(" + adjustedDate.ToString("yyyy,") + Convert.ToString(adjustedDate.Month-1) + adjustedDate.ToString(",d,H,m") + "));\n");
            if (!Convert.IsDBNull(row[1])) sb.Append("data.setValue(" + i.ToString() + ",1," + row[1].ToString() + ");\n");
            if (!Convert.IsDBNull(row[2])) sb.Append("data.setValue(" + i.ToString() + ",2," + row[2].ToString() + ");\n");
        }
        Data = sb.ToString();

        DateTime startDate=DateTime.Now.AddDays(-1).AddHours(TimezoneDifference);
        DateTime endDate=DateTime.Now.AddHours(TimezoneDifference);

        Data += "var startTime=new Date(" + startDate.ToString("yyyy,") + Convert.ToString(startDate.Month-1) + startDate.ToString(",d,H,m") + ");";
        Data += "var endTime=new Date(" + endDate.ToString("yyyy,") + Convert.ToString(endDate.Month-1) + endDate.ToString(",d,H,m") + ");";
        Data += "var chart = new google.visualization.AnnotatedTimeLine(document.getElementById('chart_div'));\n";
        Data += "chart.draw(data, {width: 700, height: 200, interpolateNulls: true, pointSize: 1,  displayRangeSelector:false, displayZoomButtons:false, dateFormat:'h:mm', scaleType:'maximized', zoomStartTime:startTime });";
        ChartDiv.Text = "<div id=\"chart_div\" style=\"width: 700px; height: 200px;\"></div>";
    }


    public void Populate(int thermostatId)
    {
        ThermostatMonitorLib.Thermostat thermostat = ThermostatMonitorLib.Thermostat.LoadThermostat(thermostatId);

        ThermostatMonitorLib.Temperatures temps = ThermostatMonitorLib.Temperatures.LoadRange(thermostatId, DateTime.Now.AddDays(-1), DateTime.Now);
        //ThermostatMonitorLib.Temperatures outdoorTemps = ThermostatMonitorLib.Temperatures.LoadRange(0, DateTime.Now.AddDays(-1), DateTime.Now);
        ThermostatMonitorLib.OutsideConditions conditions = ThermostatMonitorLib.OutsideConditions.LoadRange(thermostat.LocationId, DateTime.Now.AddDays(-1), DateTime.Now);
        


        DataTable dt = new DataTable();
        dt.Columns.Add("LogDate", typeof(DateTime));
        dt.Columns.Add("InsideTemperature", typeof(double));
        //dt.Columns.Add("AC_On", typeof(double));
        dt.Columns.Add("OutsideTemperature", typeof(double));

        
        for (int i=0;i<temps.Count;i++)
        {
            ThermostatMonitorLib.Temperature temp = temps[i];
            if (temp.Degrees == Convert.ToInt32(temp.Degrees))
            {
                if (i > 0)
                {
                    DataRow pRow = dt.NewRow();
                    pRow[0] = temp.LogDate.AddSeconds(-1);
                    pRow[1] = temps[i - 1].Degrees;
                    dt.Rows.Add(pRow);
                }
                DataRow row = dt.NewRow();
                row[0] = temp.LogDate;
                row[1] = temp.Degrees;
                dt.Rows.Add(row);
            }
        }
      

        
        foreach (ThermostatMonitorLib.OutsideCondition condition in conditions)
        {
            DataRow row = dt.NewRow();
            row[0] = condition.LogDate;
            row[2] = condition.Degrees;
            dt.Rows.Add(row);
        }
        if (conditions.Count > 0 && temps.Count>0)
        {
            DataRow row = dt.NewRow();
            row[0] = DateTime.Now.AddDays(-1);
            row[1] = temps[0].Degrees;
            row[2] = conditions[0].Degrees;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[0] = DateTime.Now;
            row[1] = temps[temps.Count - 1].Degrees;
            row[2] = conditions[conditions.Count-1].Degrees;
            dt.Rows.Add(row);
        }
        


        OutputData(dt);
        return;

        
    }



    private void PopulateCycles(int thermostatId)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("LogDate", typeof(DateTime));
        dt.Columns.Add("AC_On", typeof(int));
        int days=Convert.ToInt32(Request["days"]);
        ThermostatMonitorLib.Cycles cycles = ThermostatMonitorLib.Cycles.LoadRange(thermostatId, DateTime.Now.AddDays(-1).AddDays(days), DateTime.Now.AddDays(days));

        if (cycles.Count > 0)
        {
            DataRow row = dt.NewRow();
            row[0] = DateTime.Now.AddDays(-1);
            row[1] = 0;
            dt.Rows.Add(row);
        }
        foreach (ThermostatMonitorLib.Cycle cycle in cycles)
        {
            if (cycle.IsEndDateNull) cycle.EndDate = DateTime.Now;
            DataRow row = dt.NewRow();
            row[0] = cycle.StartDate.AddSeconds(-1);
            row[1] = 0;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[0] = cycle.StartDate;
            row[1] = 1;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[0] = cycle.EndDate.AddSeconds(-1);
            row[1] = 1;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[0] = cycle.EndDate;
            row[1] = 0;
            dt.Rows.Add(row);
        }

        if (cycles.Count > 0)
        {
            DataRow row = dt.NewRow();
            row[0] = DateTime.Now;
            row[1] = 0;
            dt.Rows.Add(row);
        }

        OutputCycles(dt);
    }

    public void OutputCycles(DataTable dt)
    {
        dt.DefaultView.Sort = "LogDate";
        //FillInDataTable(dt);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("data.addRows(" + dt.Rows.Count + ");\n");
        for (int i = 0; i < dt.DefaultView.Count; i++)
        {
            DataRowView row = dt.DefaultView[i];//.Rows[i];
            DateTime adjustedDate=Convert.ToDateTime(row[0]).AddHours(TimezoneDifference);
            sb.Append("data.setValue(" + i.ToString() + ",0,new Date(" + adjustedDate.ToString("yyyy,") + Convert.ToString(adjustedDate.Month - 1) + adjustedDate.ToString(",d,H,m") + "));\n");
            if (!Convert.IsDBNull(row[1])) sb.Append("data.setValue(" + i.ToString() + ",1," + row[1].ToString() + ");\n");
        }

        DateTime startDate = DateTime.Now.AddDays(-1).AddHours(TimezoneDifference);
        DateTime endDate = DateTime.Now.AddHours(TimezoneDifference);
        Data = sb.ToString();
        Data += "var chart = new google.visualization.AnnotatedTimeLine(document.getElementById('chart_div'));\n";
        Data += "var startTime=new Date(" + startDate.ToString("yyyy,") + Convert.ToString(startDate.Month - 1) + startDate.ToString(",d,H,m") + ");";
        Data += "var endTime=new Date(" + endDate.ToString("yyyy,") + Convert.ToString(endDate.Month - 1) + endDate.ToString(",d,H,m") + ");";
        Data += "chart.draw(data, {width: 700, height: 100, interpolateNulls: true, pointSize: 1, displayLegendDots:false, displayLegendValues:false, displayRangeSelector:false, displayZoomButtons:false, fill:100, dateFormat:'h:mm', scaleType:'maximized', zoomStartTime:startTime });";
        ChartDiv.Text = "<div id=\"chart_div\" style=\"width: 700px; height: 100px;\"></div>";
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request["thermostat"]);
        TimezoneDifference = Convert.ToInt32(Request["TimezoneDifference"]);
        if (Request["mode"] == "cycles") PopulateCycles(id); else Populate(id);
    }
}
