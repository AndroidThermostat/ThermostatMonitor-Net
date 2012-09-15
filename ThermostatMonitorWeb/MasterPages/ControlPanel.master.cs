using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_ControlPanel : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!AppUser.Current.IsAuthenticated)
        {
            string authCode = Request["authCode"];
            if (authCode != null && authCode != "")
            {
                ThermostatMonitorLib.User user = ThermostatMonitorLib.User.LoadByAuthCode(authCode);
                if (user != null)
                {
                    AppUser.Current.Login(user);
                }
            }

            if (!AppUser.Current.IsAuthenticated) Response.Redirect("/cp/login.aspx");
        }
    }
}
