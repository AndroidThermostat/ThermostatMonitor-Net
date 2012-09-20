<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        Exception ex = context.Server.GetLastError();
        string url = context.Request.Url.PathAndQuery;
        string additionalInfo = context.Request.ServerVariables.ToString();

        ThermostatMonitorLib.Error error = new ThermostatMonitorLib.Error();
        error.ErrorMessage = ex.ToString() + "\n\n" + additionalInfo;
        error.Url = url;
        error.LogDate = DateTime.Now;
        //error.UserId = 0;
        try
        {
            if (AppUser.Current.IsAuthenticated) error.UserId = AppUser.Current.UserData.Id;
        }
        catch { }
        ThermostatMonitorLib.Error.SaveError(error);
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
