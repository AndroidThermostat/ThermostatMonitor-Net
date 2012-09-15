using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Utils
/// </summary>
public class Utils
{
    public Utils()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void Do301(string url)
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        context.Response.Status = "301 Moved Permanently";
        context.Response.AddHeader("Location", url);
        context.Response.End();
    }
}
