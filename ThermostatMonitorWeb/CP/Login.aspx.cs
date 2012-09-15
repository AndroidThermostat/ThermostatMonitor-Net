using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cp_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["ApiKey"] != null)
            {
                System.Guid guid = new Guid(Request["ApiKey"]);
                ThermostatMonitorLib.Location location = ThermostatMonitorLib.Location.LoadLocation(guid);
                if (location != null)
                {
                    ThermostatMonitorLib.User user = ThermostatMonitorLib.User.LoadUser(location.UserId);
                    if (user != null)
                    {
                        AppUser.Current.Login(user);
                        Response.Redirect("/cp/");
                    }
                }
            }
            else if (Request["action"] == "logout")
            {
                AppUser.Current.Logout();
                Response.Redirect("/");
            }
        }

    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {

        ThermostatMonitorLib.User user = null;
        List<string> errors = new List<string>();
        if (EmailText.Text=="") errors.Add("Email is blank.");
        if (PasswordText.Text == "") errors.Add("Password is blank.");
        if (errors.Count == 0)
        {
            user = ThermostatMonitorLib.User.LoadUser(EmailText.Text, PasswordText.Text);
            if (user == null) errors.Add("Invalid email or password");
        }
        if (errors.Count != 0)
        {
            ErrorLit.Text = "<tr><td colspan=\"2\"><div class=\"error\">" + String.Join(" ", errors.ToArray()) + "</div></td></tr>";
        }
        else
        {
            AppUser.Current.Login(user);
            Response.Redirect("/cp/");
        }
        

    }
}
