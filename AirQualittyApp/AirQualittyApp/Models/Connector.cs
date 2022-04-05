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
        private static Connector instance;
        /// <summary>
        /// Connection string to API 
        /// </summary>
        public string ApiConnectionString { get; private set; }
        private Connector()
        {
            ApiConnectionString = ConfigurationManager.ConnectionStrings["ApiConnectionString"].ConnectionString;
        }
        public static Connector GetInstance()
        {
            if (instance == null)
                instance = new Connector();
            return instance;
        }
    }
}
