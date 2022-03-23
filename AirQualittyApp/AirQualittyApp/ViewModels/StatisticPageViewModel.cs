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

namespace AirQualittyApp.ViewModels
{
    public class StatisticPageViewModel : INotifyPropertyChanged
    {
        #region Private
        

       
        #endregion

        #region Public
        /// <summary>
        /// The collection of selected cities
        /// </summary>
        public static ObservableCollection<City> SelectedCities { get; set; }

        /// <summary>
        /// Http client for connection to API
        /// </summary>
        HttpClient _httpClient = new HttpClient();
        
 
        #endregion

        #region Initialization
        public StatisticPageViewModel(ObservableCollection<City> selectedCities)
        {
            //Initialization of SelectedCities colloction
            SelectedCities = selectedCities;
            
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
