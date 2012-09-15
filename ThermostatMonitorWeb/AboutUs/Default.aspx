<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="AboutUs_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>About Thermostat Monitor</h1>
    <p>This site was just created in June of 2011 and I'm still working on many of the features.  Please feel free to sign up and use the site.  Please <a href="mailto:jeremy@thermostatmonitor.com">email me</a> any suggestions or bug reports you may hvae..</p>
        
    <h2>Public Data</h2>
    <p>Some of our users have opted in to sharing their usage thermostat usage patterns in order to help researchers.  You can download this data <a href="/export.aspx">here</a>.
    </p>
    
   
    
    <h2>Open Source</h2>
    <p>If you would like to contribute to this project or host your own copy, you can download the latest source code <a href="https://code.google.com/p/thermostat-monitor/">here</a>.  The website is written in .NET and the desktop application is written in Javascript using Titanium.  I would love any help you're able to provide.  Some items currently on the to do list are:</p>
    <ul>
        <li><b>Design</b> - I've done the best I can, but website design is not my specialty.  I would love to find someone who can give this site a more professional look.</li>
        <li><b>Desktop Client</b> - The desktop client is writting in HTML/Javascript, using <a href="http://www.appcelerator.com/products/titanium-desktop-application-development/">Titanium</a>.  I have compile Windows and Mac clients but am having issues compiling a Linux client that I could use some help with from someone with more Linux experience.  The desktop client could also use a better UI.</li>
        <li><b>Additional Charts and Data</b> - I'm sure there are more uses for the data being gathered, especially the aggregate data available from the public export of those who have opted-in.   I'd love any ideas you have for charts.  Even better if you're able to make them.</li>
        <li><b>Support for Other Thermostat Brands</b> - So far the site supports RadioThermostat and RCS Technologies thermostats.  If you have access to other brands I'd love for you to extend the desktop client to support them.</li>
        <li><b>Winter Support</b> - In order to get things set up as quickly as possible, most parts of the site are only tracking A/C usage for now.  We need to come up with ways to simply display heat usage as well before fall in North America.</li>
    </ul>
    <a name="terms"></a>
    <h2>Terms of Use</h2>    
    <p>Copyright (C) 2012 Trilitech, LLC</p>
    <p>Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:</p>
    <p>The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.</p>
    <p>THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.</p>
    
</asp:Content>

