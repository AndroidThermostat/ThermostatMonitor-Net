using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_LoginBox : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (AppUser.Current.IsAuthenticated)
        {
            LoggedInHolder.Visible = true;
            LoggedOutHolder.Visible = false;
            EmailLit.Text = "<a href=\"mailto:" + AppUser.Current.UserData.EmailAddress + "\">" + AppUser.Current.UserData.EmailAddress + "</a>";
        }
    }
}
