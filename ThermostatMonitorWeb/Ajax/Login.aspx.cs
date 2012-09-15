using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Ajax_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string email = Request["Email"];
        string password = Request["Password"];
        ThermostatMonitorLib.User user = ThermostatMonitorLib.User.LoadUser(email, password);
        if (user != null) AppUser.Current.Login(user);
    }
}
