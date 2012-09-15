using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CP_Report : System.Web.UI.Page
{
    ThermostatMonitorLib.Thermostat thermostat;
    DateTime startDate = DateTime.Today.AddDays(-7);
    DateTime endDate = DateTime.Today.AddDays(-1);
    DateTime prevStartDate = DateTime.Today.AddDays(-14);
    DateTime prevEndDate = DateTime.Today.AddDays(-8);
    public string DeltaUrl = "";
    public string TimeOfDayUrl = "";
    int timezoneDifference = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        int thermostatId = Convert.ToInt32(Request["thermostatId"]);
        thermostat = ThermostatMonitorLib.Thermostat.LoadThermostat(thermostatId);
        ThermostatMonitorLib.Location location = ThermostatMonitorLib.Location.LoadLocation(thermostat.LocationId);

        timezoneDifference = AppUser.TimezoneDifference(location.Timezone, location.DaylightSavings);


        if (!IsPostBack)
        {
            ThermostatName.Text = thermostat.DisplayName;
            StartDateText.Text = startDate.ToString("MM/dd/yyyy");
            EndDateText.Text = endDate.ToString("MM/dd/yyyy");
            PrevStartDateText.Text = prevStartDate.ToString("MM/dd/yyyy");
            PrevEndDateText.Text = prevEndDate.ToString("MM/dd/yyyy");
            IncludePrevious.Checked = true;
        }

    }

    public void ApplyButton_Click(object sender, EventArgs e)
    {
        startDate = Convert.ToDateTime(StartDateText.Text);
        endDate = Convert.ToDateTime(EndDateText.Text);
        prevStartDate = Convert.ToDateTime(PrevStartDateText.Text);
        prevEndDate = Convert.ToDateTime(PrevEndDateText.Text);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {

        DeltaUrl = "/cp/charts/delta.aspx?thermostatId=" + thermostat.Id.ToString() + "&timezoneDifference=" + timezoneDifference.ToString() + "&startDate=" + startDate.ToString("M/d/yyyy") + "&endDate=" + endDate.ToString("M/d/yyyy");
        if (IncludePrevious.Checked) DeltaUrl += "&prevStartDate=" + prevStartDate.ToString("M/d/yyyy") + "&prevEndDate=" + prevEndDate.ToString("M/d/yyyy");

        TimeOfDayUrl = "/cp/charts/hours.aspx?thermostatId=" + thermostat.Id.ToString() + "&timezoneDifference=" + timezoneDifference.ToString() + "&startDate=" + startDate.ToString("M/d/yyyy") + "&endDate=" + endDate.ToString("M/d/yyyy");
        if (IncludePrevious.Checked) TimeOfDayUrl += "&prevStartDate=" + prevStartDate.ToString("M/d/yyyy") + "&prevEndDate=" + prevEndDate.ToString("M/d/yyyy");
    }

}
