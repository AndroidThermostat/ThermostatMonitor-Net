using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ThermostatMonitorLib
{
    public class Weather
    {


        private static double[] GetCoordinates(string location)
        {
            double[] result = new double[2];
            string url = "http://where.yahooapis.com/geocode?q=" + location + "&appid=4m2kmJ3V34Gpe6hGVj8ORcMio3s44DgMDmyS8nKHNnf6PlZwjMcoluUBzwAVXGDG";
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(url);
            result[0] = Convert.ToDouble(doc.SelectSingleNode("/ResultSet/Result/latitude").InnerText);
            result[1] = Convert.ToDouble(doc.SelectSingleNode("/ResultSet/Result/longitude").InnerText);
            return result;
        }

        public static int GetCityId(string zipcode)
        {
            double[] coordinates = GetCoordinates(zipcode);
            return GetCityId(coordinates[0], coordinates[1]);
        }

        private static int GetCityId(double latitude, double longitude)
        {
            string url = "http://openweathermap.org/data/2.0/find/city?lat=" + latitude.ToString() + "&lon=" + longitude.ToString() + "&cnt=1";
            string contents = ThermostatMonitorLib.Utils.GetUrlContents(url);
            Hashtable ht = (Hashtable)ThermostatMonitorLib.JSON.JsonDecode(contents);
            ArrayList list = (ArrayList)ht["list"];
            Hashtable entry = (Hashtable)list[0];
            int cityId = Convert.ToInt32(entry["id"]);
            return cityId;
        }


        public static int GetTemperature(int cityId)
        {
            string url = "http://openweathermap.org/data/2.0/weather/city/" + cityId.ToString();
            string contents = ThermostatMonitorLib.Utils.GetUrlContents(url);
            Hashtable ht = (Hashtable)ThermostatMonitorLib.JSON.JsonDecode(contents);
            Hashtable main = (Hashtable)ht["main"];
            double kelvin = Convert.ToInt32(main["temp"]);
            double celcius = kelvin - 272.15;
            int fahrenheit = (int)System.Math.Round(celcius * 9.0 / 5.0 + 32, 0);
            return fahrenheit;
        }

        
        //public static int GetTemperature(string zipCode)
        //{
        //    string url = "http://www.google.com/ig/api?weather=" + zipCode;
        //    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        //    doc.Load(url);
        //    System.Xml.XmlNodeList nodes = doc.SelectNodes("/xml_api_reply/weather/current_conditions/temp_f");
        //    return Convert.ToInt32(nodes[0].Attributes["data"].Value);
        //}


    }
}
