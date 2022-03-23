using AirQualittyApp.Models;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AirQualittyApp.ViewModels
{
    /// <summary>
    /// View Model class for working with MainPage
    /// </summary>
    public class MainPageViewModel : INotifyPropertyChanged
    {
        #region Private

        /// <summary>
        /// Http Client for connection to API
        /// </summary>
        private static HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Respons from Db
        /// </summary>
        private string respon = "";

        /// <summary>
        /// Quality of air
        /// </summary>
        private string _quality;

        /// <summary>
        /// Color of canvas
        /// </summary>
        private string colorForCanvas;

        /// <summary>
        /// Command for air quality
        /// </summary>
        private readonly ICommand searchCommand;

        /// <summary>
        /// Command for selecting the city
        /// </summary>
        private readonly ICommand selectCityCommand;

        /// <summary>
        /// Command for going to StatisticPage
        /// </summary>
        private readonly ICommand statisticCommand;
        #endregion

        #region Public

        /// <summary>
        /// Property for city`s name, that user search
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Property for city`s name
        /// </summary>
        public string NameOfSelectedCity { get; set; }

        /// <summary>
        /// Property for air quality
        /// </summary>
        public string Quality { get { return _quality; } set { _quality = value; OnPropertyChanged("Quality"); } }

        /// <summary>
        /// Property for color of canvas 
        /// </summary>
        public string ColorForCanvas { get { return colorForCanvas; } set { colorForCanvas = value; OnPropertyChanged("ColorForCanvas"); } } 
        
        /// <summary>
        /// The collection of cities. User can select city from this collection for analizing statistic
        /// </summary>
        public static ObservableCollection<City> Cities { get; set; }

        /// <summary>
        /// The collection of selected cities
        /// </summary>
        public ObservableCollection<City> SelectedCities { get; set; } 

        public ICommand SearchCommand => searchCommand;

        public ICommand SelectCityCommand => selectCityCommand;

        public ICommand StatisticCommand => statisticCommand;
        #endregion

        #region Initialization
        public MainPageViewModel()
        {
            //Connecting to API
            string url = Connector.ApiConnectionString + "/cities";
            // Getting the response from API
            HttpResponseMessage responseMessage = _httpClient.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                // Binding the response to respon
                respon = responseMessage.Content.ReadAsStringAsync().Result;
            }
            // Initialization for Cities collection
            Cities = JsonConvert.DeserializeObject<ObservableCollection<City>>(respon);

            //Binding searchCommand to SearchCity method
            searchCommand = new RelayCommand(async() => await SearchCity());
            // Binding selectCityCommand to SelectCity method 
            selectCityCommand = new RelayCommand(async () => await SelectCity());
            // Binding statisticCommand to GoToStatistic method 
            //statisticCommand = new RelayCommand(() => GoToStatistic()); 
        }
        #endregion

        #region Methods
        public async Task SearchCity()
        {
            // url for connecting to API
            string url = Connector.ApiConnectionString + "/airquality/" + CityName;
            // Getting the response from API
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(url); 

            if(responseMessage.IsSuccessStatusCode)
            {
                // Binding the response to Quality
                Quality = await responseMessage.Content.ReadAsStringAsync(); 
            }

            //Changing the color of canvas depending on the Quality
            if (Convert.ToInt32(Quality) <= 50) ColorForCanvas = "#35cc38";
            else if (Convert.ToInt32(Quality) >= 50 && Convert.ToInt32(Quality) <= 100) ColorForCanvas = "#f7ed60";
            else if (Convert.ToInt32(Quality) >= 101 && Convert.ToInt32(Quality) <= 150) ColorForCanvas = "#f7a960";
            else if (Convert.ToInt32(Quality) >= 151 && Convert.ToInt32(Quality) <= 200) ColorForCanvas = "#fa5252";
            else if (Convert.ToInt32(Quality) >= 201 && Convert.ToInt32(Quality) <= 300) ColorForCanvas = "#c252fa";
            else if (Convert.ToInt32(Quality) >= 301) ColorForCanvas = "#590d11";
        }

        public async Task SelectCity()
        {
            //NameOfSelectedCity
        }

        //public void GoToStatistic()
        //{
        //    File.WriteAllText("Cities.json", JsonSerializer.Serialize<ObservableCollection<City>>(Cities));

        //    // Initialization of collection
        //    SelectedCities = new ObservableCollection<City>();

        //    foreach (var c in Cities)
        //    {
        //        if (c.IsSelected == true)
        //        {
        //            //Checking the IsSelected property of the city
        //            SelectedCities.Add(c);
        //        }
        //    }
        //    StatisticPageViewModel statisticPageViewModel = new StatisticPageViewModel(SelectedCities);
        //    // Initialization of the Statistic Page
        //    StatisticPage statisticPage = new StatisticPage();
        //    statisticPage.DataContext = statisticPageViewModel;

        //    // Showing the Statistic Page
        //    statisticPage.Show(); 
        //}
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
