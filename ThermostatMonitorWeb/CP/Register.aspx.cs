using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cp_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) TimezoneList.SelectedValue = "-6";
    }


    private bool Validate()
    {
        List<string> errors = new List<string>();
        ThermostatMonitorLib.User user=ThermostatMonitorLib.User.LoadUser(EmailText.Text);
        if (!AgreeCheck.Checked) errors.Add("You must agree to the terms of use.");
        if (!EmailText.Text.Contains("@")) errors.Add("Invalid email address.");
        if (ZipCodeText.Text.Length != 5) errors.Add("Please enter a five digit zip code.");
        if (PriceText.Text == "") errors.Add("Plese list a price.  You can change it at any time.");
        double price;
        if (!Double.TryParse(PriceText.Text, out price)) errors.Add("Please list electricity price as cents per kilowatt hour.");

        double heatPrice;
        if (!Double.TryParse(HeatPriceText.Text.Replace("$","").Replace(",",""), out heatPrice)) errors.Add("Please list heat price as dollars per Dekatherm.");


        if (user != null) errors.Add("An account is already registered with this email address.");
        if (Password2Text.Text != PasswordText.Text) errors.Add("Passwords do not match.");
        if (errors.Count == 0) ErrorLit.Text = ""; else ErrorLit.Text = "<div class=\"error\">" + String.Join(" ", errors.ToArray()) + "</div>";
        return errors.Count == 0;
    }

    protected void RegisterButton_Click(object sender, EventArgs e)
    {
        if (Validate())
        {
            ThermostatMonitorLib.User user = new ThermostatMonitorLib.User();
            user.EmailAddress = EmailText.Text;
            user.Password = ThermostatMonitorLib.Utils.HashPassword(PasswordText.Text);
            user.AuthCode = Guid.NewGuid().ToString();
            ThermostatMonitorLib.User.SaveUser(user);

            ThermostatMonitorLib.Location location = new ThermostatMonitorLib.Location();
            location.ApiKey = Guid.NewGuid();
            location.ElectricityPrice = Convert.ToDouble(PriceText.Text);
            location.HeatFuelPrice = Convert.ToDouble(HeatPriceText.Text.Replace("$", "").Replace(",", ""));
            location.Name = "Home";
            location.UserId = user.Id;
            location.ZipCode = ZipCodeText.Text;
            location.ShareData = ShareCheck.Checked;
            location.Timezone = Convert.ToInt32(TimezoneList.SelectedValue);
            location.DaylightSavings = DaylightSavings.Checked;
            //location.DaylightSavings = DaylightSavings.Checked;


            location.OpenWeatherCityId = ThermostatMonitorLib.Weather.GetCityId(location.ZipCode);

            ThermostatMonitorLib.Location.SaveLocation(location);

            //ThermostatMonitorLib.UserSetting setting = new ThermostatMonitorLib.UserSetting();
            //setting.ZipCode = ZipCodeText.Text;
            //setting.UserId = user.Id;
            //setting.FilterChangeDate = DateTime.Today.AddDays(90);
            //ThermostatMonitorLib.UserSetting.SaveUserSetting(setting);


            AppUser.Current.Login(user);
            Response.Redirect("/cp/");
        }
    }
}
