using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cp_User : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    private bool Validate()
    {
        List<string> errors = new List<string>();
        if (Password2Text.Text != PasswordText.Text) errors.Add("Passwords do not match.");
        
        if (errors.Count == 0) ErrorLit.Text = ""; else ErrorLit.Text = "<div class=\"error\">" + String.Join(" ", errors.ToArray()) + "</div>";
        return errors.Count == 0;
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        if (Validate())
        {
            ThermostatMonitorLib.User user = ThermostatMonitorLib.User.LoadUser(AppUser.Current.UserData.Id);
            if (PasswordText.Text.Length>0) user.Password = ThermostatMonitorLib.Utils.HashPassword(PasswordText.Text);
            ThermostatMonitorLib.User.SaveUser(user);
            AppUser.Current.UserData = user;
            int systemTimezone = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Timezone"]);
            Response.Redirect("/cp/");
        }
    }
}
