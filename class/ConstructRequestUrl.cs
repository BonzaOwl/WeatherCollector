using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkSkyCollectorDesktop
{
    class ConstructRequestUrl
    {
        public static string ForecastUrl(string apiKey, string cordsLat, string cordsLong, string exclude, string units)
        {
            string baseUrl = "https://api.darksky.net/forecast/";
            string forecastUrl = baseUrl + apiKey;

            forecastUrl = forecastUrl + "/" + cordsLat + "," + cordsLong + "?" + exclude + "&" + units;

            return forecastUrl;
        }
    }
}
