<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="cp_Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <form runat="server">
    <h1>Register</h1>
    <asp:Literal ID="ErrorLit" runat="server" />
    <table>
        <tr><td>Email</td><td><asp:TextBox ID="EmailText" runat="server" /></td></tr>
        <tr><td>Password</td><td><asp:TextBox ID="PasswordText" TextMode="Password" runat="server" /></td></tr>
        <tr><td>Verify Password</td><td><asp:TextBox ID="Password2Text" TextMode="Password" runat="server" /></td></tr>
        <tr><td>Zip Code</td><td><asp:TextBox ID="ZipCodeText" runat="server" /></td><td>(to pull weather conditions)</td></tr>
        <tr><td>Time Zone</td><td colspan="2">
        <asp:DropDownList ID="TimezoneList" runat="server">
              <asp:ListItem Value="-12" Text="(GMT -12:00) Eniwetok, Kwajalein" />
              <asp:ListItem Value="-11" Text="(GMT -11:00) Midway Island, Samoa" />
              <asp:ListItem Value="-10" Text="(GMT -10:00) Hawaii" />
              <asp:ListItem Value="-9" Text="(GMT -9:00) Alaska" />
              <asp:ListItem Value="-8" Text="(GMT -8:00) Pacific Time (US &amp; Canada)" />
              <asp:ListItem Value="-7" Text="(GMT -7:00) Mountain Time (US &amp; Canada)" />
              <asp:ListItem Value="-6" Text="(GMT -6:00) Central Time (US &amp; Canada), Mexico City" />
              <asp:ListItem Value="-5" Text="(GMT -5:00) Eastern Time (US &amp; Canada), Bogota, Lima" />
              <asp:ListItem Value="-4" Text="(GMT -4:00) Atlantic Time (Canada), Caracas, La Paz" />
              <asp:ListItem Value="-3" Text="(GMT -3:00) Brazil, Buenos Aires, Georgetown" />
              <asp:ListItem Value="-2" Text="(GMT -2:00) Mid-Atlantic" />
              <asp:ListItem Value="-1" Text="(GMT -1:00 hour) Azores, Cape Verde Islands" />
              <asp:ListItem Value="0" Text="(GMT) Western Europe Time, London, Lisbon, Casablanca" />
              <asp:ListItem Value="1" Text="(GMT +1:00 hour) Brussels, Copenhagen, Madrid, Paris" />
              <asp:ListItem Value="2" Text="(GMT +2:00) Kaliningrad, South Africa" />
              <asp:ListItem Value="3" Text="(GMT +3:00) Baghdad, Riyadh, Moscow, St. Petersburg" />
              <asp:ListItem Value="4" Text="(GMT +4:00) Abu Dhabi, Muscat, Baku, Tbilisi" />
              <asp:ListItem Value="5" Text="(GMT +5:00) Ekaterinburg, Islamabad, Karachi, Tashkent" />
              <asp:ListItem Value="6" Text="(GMT +6:00) Almaty, Dhaka, Colombo" />
              <asp:ListItem Value="7" Text="(GMT +7:00) Bangkok, Hanoi, Jakarta" />
              <asp:ListItem Value="8" Text="(GMT +8:00) Beijing, Perth, Singapore, Hong Kong" />
              <asp:ListItem Value="9" Text="(GMT +9:00) Tokyo, Seoul, Osaka, Sapporo, Yakutsk" />
              <asp:ListItem Value="10" Text="(GMT +10:00) Eastern Australia, Guam, Vladivostok" />
              <asp:ListItem Value="11" Text="(GMT +11:00) Magadan, Solomon Islands, New Caledonia" />
              <asp:ListItem Value="12" Text="(GMT +12:00) Auckland, Wellington, Fiji, Kamchatka" />
        </asp:DropDownList> <asp:CheckBox ID="DaylightSavings" Text="Observe Daylight Savings" runat="server" Checked="true" />
        </td></tr>
        <tr><td>Electricity Price</td><td><asp:TextBox ID="PriceText" runat="server" Text="10.00"/></td><td>(in <u>cents</u> per kilowatt hour.  You can change this later.)</td></tr>
        <tr><td>Heat Fuel Price</td><td><asp:TextBox ID="HeatPriceText" runat="server" Text="10.00"/></td><td>(in <u>dollars</u> per Dekatherm.  You can change this later.)</td></tr>
        <tr><td colspan="3"><asp:CheckBox ID="ShareCheck" runat="server" Checked="true" /> May we share your anonymous thermostat data with others? *</td></tr>
        <tr><td colspan="3"><asp:CheckBox ID="AgreeCheck" runat="server" Checked="true" /> I agree with the <a href="/aboutus/#terms" target="_blank">terms of use</a>.</td></tr>
        <tr><td colspan="2" align="center"><asp:Button ID="RegisterButton" runat="server" Text="Register" onclick="RegisterButton_Click" /></td></tr>
    </table>
    
    <br /><br />
    <p><i>* We provide a <a href="/export.aspx" target="_blank">public export file</a> containing the usage stats of those who have opted to share their data.  This export is file doesn't contain any personally identifying information and is is available for download by anyone.
    Having usage statistics from hundreds of thermostats is very useful for those looking for ways to make heating and cooling more efficient.  I encourage you to contribute by sharing your usage, but it is optional.</i></p>
    </form>
</asp:Content>

