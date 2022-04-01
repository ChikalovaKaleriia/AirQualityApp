using AirQualittyApp.Models;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using MongoDB.Driver;
using AirQualittyApp.Models.Domain;
using System.Net.Http;
using System.Windows.Threading;
using Newtonsoft.Json;

namespace AirQualittyApp.ViewModels
{
    public class StatisticPageViewModel : INotifyPropertyChanged
    {
        #region Private

        private SelectedCitiesAndStatistic selectedCity;

        #endregion

        #region Public
        /// <summary>
        /// The collection of selected cities
        /// </summary>
        public static ObservableCollection<SelectedCitiesAndStatistic> SelectedCities { get; set; }

        /// <summary>
        /// Http client for connection to API
        /// </summary>
        HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Respons from Db
        /// </summary>
        private string response = "";

        /// <summary>
        /// Property for Selected city
        /// </summary>
        public SelectedCitiesAndStatistic SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
            }
        }

        #endregion

        #region Initialization
        public StatisticPageViewModel()
        {
            //Connecting to API
            string urlAllCities = Connector.ApiConnectionString + "/statistic";
            // Getting the response from API
            HttpResponseMessage responseMessageAllCities = _httpClient.GetAsync(urlAllCities).Result;
            if (responseMessageAllCities.IsSuccessStatusCode)
            {
                // Binding the response to responAllCities
                response = responseMessageAllCities.Content.ReadAsStringAsync().Result;
            }

            //Initialization of SelectedCities collection
            SelectedCities = JsonConvert.DeserializeObject<ObservableCollection<SelectedCitiesAndStatistic>>(response);
        }
        #endregion

        #region INotifyPropertyChanched
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion

        #region Comments
        //private DbCity selectedCity;
        //public DbCity SelectedCity {
        //    get { return selectedCity; }
        //    set
        //    {
        //        selectedCity = value;
        //        OnPropertyChanged("SelectedCity");
        //    }
        //}
        //public static ObservableCollection<string> CollectionOfQuality { get; set; }
        #endregion
    }
}
