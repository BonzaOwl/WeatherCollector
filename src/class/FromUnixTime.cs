﻿using System;

namespace WeatherCollectorDesktop
{
    class FromUnixTime
    {
        public static DateTime Convert(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }
    }
}
