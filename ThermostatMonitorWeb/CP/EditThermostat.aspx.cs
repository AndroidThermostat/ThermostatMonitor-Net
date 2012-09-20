using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cp_EditThermostat : System.Web.UI.Page
{
    ThermostatMonitorLib.Thermostat thermostat;
    private string key
    {
        get
        {
            if (ViewState["key"] == null)
            {
                String result = "";
                String encodedResult = null;
                while (result != encodedResult)
                {
                    long ticks = DateTime.Now.Ticks;
                    byte[] bytes = BitConverter.GetBytes(ticks);
                    result = Convert.ToBase64String(bytes).Replace("=", "");
                    encodedResult = Server.UrlEncode(result);
                }
                key = result;
            }
            return ViewState["key"].ToString();
        }
        set { ViewState["key"] = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        int id = Convert.ToInt32(Request["id"]);
        if (id > 0)
        {
            thermostat = ThermostatMonitorLib.Thermostat.LoadThermostat(id);
            if (thermostat.KeyName!=null && thermostat.KeyName!="") key = thermostat.KeyName;
            ThermostatMonitorLib.Location location = ThermostatMonitorLib.Location.LoadLocation(thermostat.LocationId);
            if (location.UserId != AppUser.Current.UserData.Id) Response.Redirect("/cp/login.aspx");
        }
        else
        {
            thermostat = new ThermostatMonitorLib.Thermostat();
            thermostat.AcSeer = 16;
            thermostat.AcTons = 3.5;
            thermostat.AcKilowatts = 2.625;
            thermostat.DisplayName = "New Thermostat";
            thermostat.FanKilowatts = 0.5;
            thermostat.IpAddress = "192.168.1.106";
        }
        if (!IsPostBack) Populate();
    }

    public void BrandList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ToggleNotes();
    }

    private void ToggleNotes()
    {
        if (BrandList.SelectedIndex == 0)
        {
            RadioThermostatHolder.Visible = true;
            AndroidThermostatLit.Text = "";
        }
        else
        {
            RadioThermostatHolder.Visible = false;
            AndroidThermostatLit.Text = "<tr><td colspan=\"3\"><br>Enter the following urls into your Android Thermostat app:<br/><ul>";

            AndroidThermostatLit.Text += "<li><b>Base Url:</b> http://api.thermostatmonitor.com/v2/?k=" + Server.UrlEncode(key) + "</li>";
            AndroidThermostatLit.Text += "<li><b>Cycle Complete Params:</b> &a=cycle&m=[mode]&d=[duration]</li>";
            AndroidThermostatLit.Text += "<li><b>Inside Temp Change Params:</b> &a=temp&t=[insideTemp]</li>";
            AndroidThermostatLit.Text += "<li><b>Outside Temp Change Params:</b> &a=conditions&t=[outsideTemp]</li>";

            AndroidThermostatLit.Text += "</ul></td></tr>";
        }
    }

    private void Populate()
    {
        LocationList.DataSource = ThermostatMonitorLib.Locations.LoadLocationsByUserId(AppUser.Current.UserData.Id);
        LocationList.DataBind();

        LocationList.SelectedValue = thermostat.LocationId.ToString();
        DisplayNameText.Text = thermostat.DisplayName;
        IpAddressText.Text = thermostat.IpAddress;
        TonsText.Text = thermostat.AcTons.ToString();
        SeerText.Text = thermostat.AcSeer.ToString();
        KilowattsLabel.Text = thermostat.AcKilowatts.ToString() + "kw";
        BrandList.SelectedValue = thermostat.Brand;
        HeatBtuPerHourText.Text = thermostat.HeatBtuPerHour.ToString("###,###,###,###,###.##");
        FanKilowattsText.Text = thermostat.FanKilowatts.ToString();

        if (thermostat.Id == 0) DeleteButton.Visible = false; else DeleteButton.Attributes.Add("onclick", "return confirm('Are you sure you wish to permanently delete this thermostat?  All data will be lost.');");
        ToggleNotes();
    }

    private bool Validate()
    {
        List<string> errors = new List<string>();
        if (DisplayNameText.Text == "") errors.Add("Display name is required");
        if (IpAddressText.Text == "") errors.Add("IP address is required");
        double tons;
        int seer;
        if (!Double.TryParse(TonsText.Text, out tons)) errors.Add("Please enter a numeric number for A/C tons.");
        if (!Int32.TryParse(SeerText.Text, out seer)) errors.Add("Please enter a numeric number for A/C SEER rating.");
        double fanKilo;
        if (!Double.TryParse(FanKilowattsText.Text, out fanKilo)) errors.Add("Please enter a numeric number for fan kilowatts.  0.5 is average.");
        double heatBtu;
        if (!Double.TryParse(HeatBtuPerHourText.Text, out heatBtu)) errors.Add("Please enter a numeric number for heater BTU/h.  100,000 is average.");

        if (errors.Count == 0) ErrorLit.Text = ""; else ErrorLit.Text = "<div class=\"error\">" + String.Join(" ", errors.ToArray()) + "</div>";
        return errors.Count == 0;
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        if (Validate())
        {
            thermostat.LocationId = Convert.ToInt32(LocationList.SelectedValue);
            thermostat.DisplayName = DisplayNameText.Text;
            thermostat.IpAddress = IpAddressText.Text;
            thermostat.AcTons = Convert.ToDouble(TonsText.Text);
            thermostat.AcSeer = Convert.ToInt32(SeerText.Text);
            thermostat.AcKilowatts = (thermostat.AcTons * 12.0) / Convert.ToDouble(thermostat.AcSeer);
            thermostat.FanKilowatts = Convert.ToDouble(FanKilowattsText.Text);
            thermostat.HeatBtuPerHour = Convert.ToDouble(HeatBtuPerHourText.Text.Replace(",", ""));
            thermostat.Brand = BrandList.SelectedValue;
            thermostat.KeyName = key;

            ThermostatMonitorLib.Thermostat.SaveThermostat(thermostat);
            Response.Redirect("/cp/");
        }
    }
    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        ThermostatMonitorLib.Thermostat.DeleteThermostat(thermostat.Id);
        Response.Redirect("/cp/");
    }
}
