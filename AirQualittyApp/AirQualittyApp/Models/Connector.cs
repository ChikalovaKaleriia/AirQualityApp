using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace AirQualittyApp.Models
{
    /// <summary>
    /// Connect to API
    /// </summary>
    public class Connector
    {
        /// <summary>
        /// Connection string to API 
        /// </summary>
        public static string ApiConnectionString = ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString;
    }
}
