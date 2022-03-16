using AirQualittyApp.Models;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AirQualittyApp.ViewModels
{
    // View Model class for working with MainPage
    public class MainPageViewModel : INotifyPropertyChanged
    {
        static HttpClient httpClient = new HttpClient(); // Http Client for connection to API
        public static ObservableCollection<City> Cities { get; set; } // The collection of cities. User can select city from this collection for analizing statistic 
        public ObservableCollection<City> SelectedCitys { get; set; } // The collection of selected cities
        public string CityName { get; set; } // Property for city`s name

        private string quality; 
        public string Quality { get { return quality; } set { quality = value; OnPropertyChanged("Quality"); } }

        private string colorForCanvas; 
        public string ColorForCanvas { get { return colorForCanvas; } set { colorForCanvas = value; OnPropertyChanged("ColorForCanvas"); } }  // Property for color of canvas 



        private readonly ICommand searchCommand; // Command for air quality
        public ICommand SearchCommand => searchCommand; 

        private readonly ICommand statisticCommand; // Command for going to StatisticPage
        public ICommand StatisticCommand => statisticCommand;

        public MainPageViewModel()
        {
            // Initialization for Cities collection
            Cities = new ObservableCollection<City> 
            {
            new City{Name = "Athens", IsSelected = false}, new City{Name = "Barcelona", IsSelected = false}, new City{Name = "Berlin", IsSelected = false},
            new City{Name = "Budapest", IsSelected = true}, new City{Name = "Cairo", IsSelected = false}, new City{Name = "Chicago", IsSelected = false},
            new City{Name = "Dublin", IsSelected = true}, new City{Name = "Dnipro", IsSelected = true}, new City{Name = "Istanbul", IsSelected = false},
            new City{Name = "Geneva", IsSelected = false}, new City{Name = "Kharkiv", IsSelected = false}, new City{Name = "Kiev", IsSelected = false},
            new City{Name = "London", IsSelected = false},  new City{Name = "Lisbon", IsSelected = false}, new City{Name = "Madrid", IsSelected = false},
            new City{Name = "Milan", IsSelected = false}, new City{Name = "Sydney", IsSelected = false}, new City{Name = "Warsaw", IsSelected = false}
            };
            searchCommand = new RelayCommand(async() => await SearchCity()); // Binding searchCommand to SearchCity method 
            statisticCommand = new RelayCommand(() => GoToStatistic()); // Binding statisticCommand to GoToStatistic method 
        }

        public async Task SearchCity()
        {
            string url = "https://localhost:44387/airquality/" + CityName; // url for connecting to API
            HttpResponseMessage responseMessage = await httpClient.GetAsync(url); // Getting the response from API
            if(responseMessage.IsSuccessStatusCode)
            {
                Quality = await responseMessage.Content.ReadAsStringAsync(); // Binding the response to Quality
            }

            //Changing the color of canvas depending on the Quality
            if (Convert.ToInt32(Quality) <= 50) ColorForCanvas = "#35cc38";
            else if (Convert.ToInt32(Quality) >= 50 && Convert.ToInt32(Quality) <= 100) ColorForCanvas = "#f7ed60";
            else if (Convert.ToInt32(Quality) >= 101 && Convert.ToInt32(Quality) <= 150) ColorForCanvas = "#f7a960";
            else if (Convert.ToInt32(Quality) >= 151 && Convert.ToInt32(Quality) <= 200) ColorForCanvas = "#fa5252";
            else if (Convert.ToInt32(Quality) >= 201 && Convert.ToInt32(Quality) <= 300) ColorForCanvas = "#c252fa";
            else if (Convert.ToInt32(Quality) >= 301) ColorForCanvas = "#590d11";
        }

        public void GoToStatistic()
        {
            SelectedCitys = new ObservableCollection<City>(); // Initialization of collection
            foreach(var c in Cities)
            {
                if(c.IsSelected == true)
                {
                    SelectedCitys.Add(c); //checking the IsSelected property of the city
                }
            }
            StatisticPage statisticPage = new StatisticPage(); // Initialization of the Statistic Page
            statisticPage.Show(); // Showing the Statistic Page
            Messenger.Default.Send<ObservableCollection<City>>(SelectedCitys); // Sending the SelectedCities collection 

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
