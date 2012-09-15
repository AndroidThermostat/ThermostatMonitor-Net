using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cp_Location : System.Web.UI.Page
{
    ThermostatMonitorLib.Location location;

    protected void Page_Load(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(Request["id"]);
        if (id > 0)
        {
            location = ThermostatMonitorLib.Location.LoadLocation(id);
            if (location.UserId != AppUser.Current.UserData.Id) Response.Redirect("/cp/login.aspx");
        }
        else
        {
            location = new ThermostatMonitorLib.Location();
            location.UserId = AppUser.Current.UserData.Id;
            location.ElectricityPrice = 10;
            location.Name = "Home";
        }

        if (!IsPostBack) Populate();
    }

    private void Populate()
    {
        DisplayNameText.Text = location.Name;
        ZipCodeText.Text = location.ZipCode;
        PriceText.Text = location.ElectricityPrice.ToString("###,###,###.#0");
        HeatPriceText.Text = location.HeatFuelPrice.ToString("###,###,###.#0");
        ShareCheck.Checked = location.ShareData;
        TimezoneList.SelectedValue = location.Timezone.ToString();
        DaylightSavings.Checked = location.DaylightSavings;
    }

    private bool Validate()
    {
        List<string> errors = new List<string>();
        if (ZipCodeText.Text.Length != 5) errors.Add("Please enter a five digit zip code.");
        if (PriceText.Text == "") errors.Add("Plese list a price.  You can change it at any time.");
        double price;
        if (!Double.TryParse(PriceText.Text, out price)) errors.Add("Please list electricity price as cents per kilowatt hour.");
        if (!Double.TryParse(HeatPriceText.Text.Replace("$","").Replace(",",""), out price)) errors.Add("Please list a heat price as dollars per kilowatt hour.");

        if (errors.Count == 0) ErrorLit.Text = ""; else ErrorLit.Text = "<div class=\"error\">" + String.Join(" ", errors.ToArray()) + "</div>";
        return errors.Count == 0;
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        if (Validate())
        {
            location.ElectricityPrice = Convert.ToDouble(PriceText.Text);
            location.HeatFuelPrice = Convert.ToDouble(HeatPriceText.Text.Replace("$", "").Replace(",", ""));
            location.Name = DisplayNameText.Text;
            location.UserId = AppUser.Current.UserData.Id;
            location.ZipCode = ZipCodeText.Text;
            location.ShareData = ShareCheck.Checked;
            location.Timezone = Convert.ToInt32(TimezoneList.SelectedValue);
            location.DaylightSavings = DaylightSavings.Checked;
            location.OpenWeatherCityId = ThermostatMonitorLib.Weather.GetCityId(location.ZipCode);
            ThermostatMonitorLib.Location.SaveLocation(location);

            Response.Redirect("/cp/");
        }
    }

}
