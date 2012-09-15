using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ThermostatMonitorWebApi
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            Exception ex = context.Server.GetLastError();
            string url = context.Request.Url.PathAndQuery;
            string additionalInfo = context.Request.ServerVariables.ToString();

            ThermostatMonitorLib.Error error = new ThermostatMonitorLib.Error();
            error.ErrorMessage = ex.ToString() + "\n\n" + additionalInfo;
            error.Url = url;
            error.LogDate = DateTime.Now;
            error.UserId = 0;
            ThermostatMonitorLib.Error.SaveError(error);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}