using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cp_ForgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private string GeneratePassword()
    {
        Random rnd = new Random();
        string[] characters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        string result = "";
        for (int i = 0; i < 8; i++) { result += characters[rnd.Next(0,25)]; }
        return result;
    }

    protected void ResetButton_Click(object sender, EventArgs e)
    {
        ThermostatMonitorLib.User user = ThermostatMonitorLib.User.LoadUser(EmailText.Text);
        if (user == null)
        {
            ErrorLit.Text = "<div class=\"error\">Could not find a user with this email address</div>";
            return;
        }
        else
        {
            string password=GeneratePassword();
            user.Password = ThermostatMonitorLib.Utils.HashPassword(password);
            ThermostatMonitorLib.User.SaveUser(user);
            SendEmail(user.EmailAddress, password);
            ErrorLit.Text = "<div>Password reset email has been sent.</div>";

        }
    }

    private void SendEmail(string address, string password)
    {
        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
        msg.From = new System.Net.Mail.MailAddress("support@thermostatmonitor.com", "Thermostat Monitor");
        msg.To.Add(address);
        msg.Subject = "ThermostatMonitor.com Password Reset";
        msg.Body = "Your ThermostatMonitor.com password has been reset to <B>" + password + "</B>.  You can change your password from the account settings screen once you log in.";
        msg.IsBodyHtml = true;
        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("localhost");
        smtp.Send(msg);
    }
}
