namespace WeatherCollectorDesktop
{
    class ConstructRequestUrl
    {
        public static string ForecastUrl(string apiKey, string cordsLat, string cordsLong, string units, string language)
        {
            //Reference URL
            //https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&appid={YOUR API KEY}

            string baseUrl = "https://api.openweathermap.org/data/2.5/onecall?";

            string forecastUrl = baseUrl + "lat=" + cordsLat + "&lon=" + cordsLong;

            //If no units are selected from the settings the API will use the default
            if (units != null)
            {
                forecastUrl = forecastUrl + "&units=" + units;
            }
            
            //If no language is selected, we won't set it, the API will use the default
            if(language != null)
            {
                forecastUrl = forecastUrl + "&lang=" + language;
            }
                
            forecastUrl = forecastUrl +  "&appid=" + apiKey;           

            return forecastUrl;
        }
    }
}
