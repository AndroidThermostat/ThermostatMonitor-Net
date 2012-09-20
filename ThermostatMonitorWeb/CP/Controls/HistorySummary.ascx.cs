using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class cp_Controls_HistorySummary : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void Populate(DataTable dt,ThermostatMonitorLib.Thermostat thermostat)
    {
        ThermostatMonitorLib.Location location = ThermostatMonitorLib.Location.LoadLocation(thermostat.LocationId);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        bool cool = false;
        bool heat = false;
        bool fan = false;

        foreach (DataRow row in dt.Rows)
        {
            if (dt.Columns.Contains("Cool_CycleCount")) cool = true;
            if (dt.Columns.Contains("Heat_CycleCount")) heat = true;
            if (dt.Columns.Contains("Fan_CycleCount")) fan = true;
        }

        sb.Append("<table cellpadding=\"4\" cellspacing=\"0\" style=\"border:1px solid black;\"><tr><td></td>");
        if (cool) sb.Append("<td colspan=\"4\" class=\"cool\"><b>Cool</b></td>");
        if (heat) sb.Append("<td colspan=\"4\" class=\"heat\"><b>Heat</b></td>");
        if (fan) sb.Append("<td colspan=\"4\" class=\"fan\"><b>Fan</b></td>");
        sb.Append("<td colspan=\"2\" class=\"outside\"><b>Outside</b></td></tr>");

        sb.Append("<tr><td><b>Day</b></td>");
        if (cool) sb.Append("<td class=\"cool\"><b>Cycles</b></td><td class=\"cool\"><b>Average</b></td><td class=\"cool\"><b>Total</b></td><td class=\"cool\"><b>Cost</b></td>");
        if (heat) sb.Append("<td class=\"heat\"><b>Cycles</b></td><td class=\"heat\"><b>Average</b></td><td class=\"heat\"><b>Total</b></td><td class=\"heat\"><b>Cost</b></td>");
        if (fan) sb.Append("<td class=\"fan\"><b>Cycles</b></td><td class=\"fan\"><b>Average</b></td><td class=\"fan\"><b>Total</b></td><td class=\"fan\"><b>Cost</b></td>");
        sb.Append("<td class=\"outside\"><b>Min</b></td><td class=\"outside\"><b>Max</b></td></tr>");


        dt.DefaultView.Sort = "LogDate desc";
        //foreach (DataRowView row in dt.DefaultView)
        for (int i=0;i<7;i++)
        {
            DateTime logDate=DateTime.Today.AddDays(-i);
            DataRow row = GetRow(dt, logDate);

            DataRow[] rows = dt.Select("logdate='" + logDate.ToString("MM/dd/yyyy") + "'");
            if (rows.Length > 0) row=rows[0];
            

            sb.Append("<tr><td><a href=\"/cp/day.aspx?thermostatId=" + thermostat.Id + "&date=" + Convert.ToDateTime(row["LogDate"]).ToString("MM/dd/yyyy") + "\">" + Convert.ToDateTime(row["LogDate"]).ToString("dddd") + "</a></td>");
            if (cool)
            {
                if (Convert.IsDBNull(row["Cool_CycleCount"])) sb.Append("<td class=\"cool\" colspan=\"4\">&nbsp;</td>");
                else
                {
                    double coolCost = (thermostat.AcKilowatts + thermostat.FanKilowatts) * Convert.ToDouble(row["Cool_TotalSeconds"]) / 60.0 / 60.0 * location.ElectricityPrice / 100;
                    sb.Append("<td class=\"cool\">" + row["Cool_CycleCount"].ToString() + "</td><td class=\"cool\">" + (Convert.ToDouble(row["Cool_AverageSeconds"]) / 60).ToString("#,##0.#") + " min</td><td class=\"cool\">" + (Convert.ToDouble(row["Cool_TotalSeconds"]) / 60).ToString("###,##0") + " min</td><td class=\"cool\">" + coolCost.ToString("C") + "</td>");
                }
            }
            if (heat)
            {
                if (Convert.IsDBNull(row["Heat_CycleCount"])) sb.Append("<td class=\"heat\" colspan=\"4\">&nbsp;</td>");
                else
                {
                    double heatCost = thermostat.HeatBtuPerHour * Convert.ToDouble(row["Heat_TotalSeconds"]) / 60.0 / 60.0 * location.HeatFuelPrice / 1000000;
                    double fanCost = (thermostat.FanKilowatts) * Convert.ToDouble(row["Heat_TotalSeconds"]) / 60.0 / 60.0 * location.ElectricityPrice / 100;
                    heatCost += fanCost;

                    sb.Append("<td class=\"heat\">" + row["Heat_CycleCount"].ToString() + "</td><td class=\"heat\">" + (Convert.ToDouble(row["Heat_AverageSeconds"]) / 60).ToString("#,##0.#") + " min</td><td class=\"heat\">" + (Convert.ToDouble(row["Heat_TotalSeconds"]) / 60).ToString("###,##0") + " min</td><td class=\"heat\">" + heatCost.ToString("C") + "</td>");
                }
            }
            if (fan)
            {
                if (Convert.IsDBNull(row["Fan_CycleCount"])) sb.Append("<td class=\"fan\" colspan=\"4\">&nbsp;</td>");
                else
                {
                    sb.Append("<td class=\"fan\">" + row["Fan_CycleCount"].ToString() + "</td><td class=\"fan\">" + (Convert.ToDouble(row["Fan_AverageSeconds"]) / 60).ToString("#,##0.#") + " min</td><td class=\"fan\">" + (Convert.ToDouble(row["Fan_TotalSeconds"]) / 60).ToString("###,##0") + " min</td><td class=\"fan\"></td>");
                }
            }
            sb.Append("<td class=\"outside\">" + row["OutsideMin"].ToString() + "</td><td class=\"outside\">" + row["OutsideMax"].ToString() + "</td></tr>");
            
        }
        sb.Append("</table>");
        OutputLit.Text=sb.ToString();
    }

    public DataRow GetRow(DataTable dt, DateTime logDate)
    {
        DataRow row = dt.NewRow();
        row["LogDate"] = logDate;
        if (dt.Columns.Contains("Cool_CycleCount"))
        {
            row["Cool_TotalSeconds"] = 0;
            row["Cool_CycleCount"] = 0;
            row["Cool_AverageSeconds"] = 0;
        }
        return row;
    }

}
