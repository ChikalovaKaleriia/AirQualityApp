using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AirQualittyApp.Models
{
    /// <summary>
    /// Model of city
    /// </summary>
    public class City 
    {
        /// <summary>
        ///  Id of the city
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the city
        /// </summary>
        public string Name { get; set; }
    }
}
