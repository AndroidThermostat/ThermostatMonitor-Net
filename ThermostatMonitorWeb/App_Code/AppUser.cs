using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AppUser
/// </summary>
public class AppUser
{
    public ThermostatMonitorLib.User UserData;
    public bool IsAuthenticated = false;

    public AppUser()
    {
    }

    public static int TimezoneDifference(int timezone, bool daylightSavings)
    {
        int systemTimezone = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Timezone"]);
        int result = timezone - systemTimezone;

        if (System.TimeZone.CurrentTimeZone.IsDaylightSavingTime(DateTime.Now)) result = result - 1;  //The system is observing daylight savings.
        if (daylightSavings) result = result + 1;  //The user is observing daylight savings


        return result;
    }

    public void Login(ThermostatMonitorLib.User user)
    {
        UserData = user;
        IsAuthenticated = true;
    }

    public void Logout()
    {
        UserData = null;
        IsAuthenticated = false;
    }

    public static AppUser Current
    {
        get
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["AppUser"] == null)
            {
                AppUser appUser = new AppUser();
                Current = appUser;
            }
            return (AppUser)HttpContext.Current.Session["AppUser"];
        }
        set { HttpContext.Current.Session["AppUser"] = value; }
    }

}
