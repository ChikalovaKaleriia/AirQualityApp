using AirQualittyApp.Models;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AirQualittyApp.ViewModels
{
    public class StatisticPageViewModel : INotifyPropertyChanged
    {

        public static ObservableCollection<City> SelectedCitys { get; set; }  // The collection of selected cities
        public StatisticPageViewModel()
        {
            Messenger.Default.Register<ObservableCollection<City>>(this, MakeAList); // Registreting of the collection with selected cities
        }

         // Method for registreting collection
        public void MakeAList(ObservableCollection<City> citys)
        {
            SelectedCitys = new ObservableCollection<City>(); // Initialization of SelectedCities collection
            foreach(var c in citys)
            {
                SelectedCitys.Add(c); // copy collection
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
