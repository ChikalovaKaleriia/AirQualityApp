using System;
using System.Collections.Generic;
using System.Text;

namespace AirQualittyApp
{
    internal class AirQualityApiResponse
    {
        public string status { get; set; }
        public object data { get; set; }
    }

    internal class AirQualityData
    {

        public int idx { get; set; }

        public int aqi { get; set; }

        public AirQualityForecast forecast { get; set; }
    }

    internal class AirQualityForecast
    {
        public AirQualityDailyForecast daily { get; set; }
    }

    internal class AirQualityDailyForecast
    {
        public AirQualityInfo[] pm25 { get; set; }

        public AirQualityInfo[] pm10 { get; set; }

        public AirQualityInfo[] o3 { get; set; }

        public AirQualityInfo[] uvi { get; set; }
    }

    internal class AirQualityInfo
    {
        public int avg { get; set; }
        public DateTime day { get; set; }
        public int max { get; set; }
        public int min { get; set; }
    }
}
