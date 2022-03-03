using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirQualittyApp.Models
{
    public interface IAirQualityProvider
    {
        Task<AirQualityResponse> GetCurrentQualityAsync(string city);

    }
}
