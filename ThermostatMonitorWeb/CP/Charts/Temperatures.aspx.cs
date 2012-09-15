using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class cp_Charts_Temperatures : System.Web.UI.Page
{
    public string Data = "";
    public int TimezoneDifference = 0;
    DateTime startTime = DateTime.Now.AddDays(-1);
    DateTime endTime = DateTime.Now;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        int thermostatId = Convert.ToInt32(Request["thermostatId"]);
        TimezoneDifference = Convert.ToInt32(Request["TimezoneDifference"]);
        if (Request["date"] != null)
        {
            startTime = Convert.ToDateTime(Request["Date"]);
            endTime = startTime.AddDays(1).AddMilliseconds(-1);
            startTime=startTime.AddHours(-TimezoneDifference);
            endTime = endTime.AddHours(-TimezoneDifference);
            if (endTime > DateTime.Now) endTime = DateTime.Now;
            
        }
        Populate(thermostatId,startTime,endTime);
    }


    public void OutputData(DataTable dt)
    {
        dt.DefaultView.Sort = "LogDate";
        //FillInDataTable(dt);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("data.addRows(" + dt.Rows.Count + ");\n");
        for (int i = 0; i < dt.DefaultView.Count; i++)
        {
            DataRowView row = dt.DefaultView[i];//.Rows[i];
            DateTime adjustedDate = Convert.ToDateTime(row[0]).AddHours(TimezoneDifference);
            sb.Append("data.setValue(" + i.ToString() + ",0,new Date(" + adjustedDate.ToString("yyyy,") + Convert.ToString(adjustedDate.Month - 1) + adjustedDate.ToString(",d,H,m") + "));\n");
            if (!Convert.IsDBNull(row[1])) sb.Append("data.setValue(" + i.ToString() + ",1," + row[1].ToString() + ");\n");
            if (!Convert.IsDBNull(row[2])) sb.Append("data.setValue(" + i.ToString() + ",2," + row[2].ToString() + ");\n");
        }
        Data = sb.ToString();


        DateTime adjustedStartTime = startTime.AddHours(TimezoneDifference);
        DateTime adjustedEndTime = endTime.AddHours(TimezoneDifference);

        Data += "var startTime=new Date(" + adjustedStartTime.ToString("yyyy,") + Convert.ToString(adjustedStartTime.Month - 1) + adjustedStartTime.ToString(",d,H,m") + ");";
        Data += "var endTime=new Date(" + adjustedEndTime.ToString("yyyy,") + Convert.ToString(adjustedEndTime.Month - 1) + adjustedEndTime.ToString(",d,H,m") + ");";
        Data += "var chart = new google.visualization.AnnotatedTimeLine(document.getElementById('chart_div'));\n";
        Data += "chart.draw(data, {width: 970, height: 200, interpolateNulls: true, pointSize: 1,  displayRangeSelector:false, displayZoomButtons:false, dateFormat:'h:mm', scaleType:'maximized', zoomStartTime:startTime });";
        ChartDiv.Text = "<div id=\"chart_div\" style=\"width: 970px; height: 200px;\"></div>";
    }


    public void Populate(int thermostatId,DateTime startTime, DateTime endTime)
    {
        ThermostatMonitorLib.Thermostat thermostat = ThermostatMonitorLib.Thermostat.LoadThermostat(thermostatId);

        ThermostatMonitorLib.Temperatures temps = ThermostatMonitorLib.Temperatures.LoadRange(thermostatId, startTime, endTime);
        ThermostatMonitorLib.OutsideConditions conditions = ThermostatMonitorLib.OutsideConditions.LoadRange(thermostat.LocationId, startTime, endTime);



        DataTable dt = new DataTable();
        dt.Columns.Add("LogDate", typeof(DateTime));
        dt.Columns.Add("InsideTemperature", typeof(double));
        //dt.Columns.Add("AC_On", typeof(double));
        dt.Columns.Add("OutsideTemperature", typeof(double));


        for (int i = 0; i < temps.Count; i++)
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
        if (conditions.Count > 0 && temps.Count > 0)
        {
            DataRow row = dt.NewRow();
            row[0] = startTime;
            row[1] = temps[0].Degrees;
            row[2] = conditions[0].Degrees;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[0] = endTime;
            row[1] = temps[temps.Count - 1].Degrees;
            row[2] = conditions[conditions.Count - 1].Degrees;
            dt.Rows.Add(row);
        }



        OutputData(dt);
        return;


    }

}
