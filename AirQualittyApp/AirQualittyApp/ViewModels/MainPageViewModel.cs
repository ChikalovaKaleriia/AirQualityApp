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
using System.Linq;
using AirQualittyApp.Models.Domain;

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
        private string responAllCities = "";

        /// <summary>
        /// Respons from Db
        /// </summary>
        private string responSelectedCities = "";

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
        public ObservableCollection<CityViewModel> Cities { get; set; }

        /// <summary>
        /// The collection of selected cities
        /// </summary>
        public ObservableCollection<string> SelectedCities { get; set; }

        public ICommand SearchCommand => searchCommand;

        public ICommand SelectCityCommand => selectCityCommand;

        public ICommand StatisticCommand => statisticCommand;
        #endregion

        #region Initialization
        public MainPageViewModel()
        {
            #region Initialization of Cities
            //Connecting to API
            string urlAllCities = Connector.ApiConnectionString + "/cities";
            // Getting the response from API
            HttpResponseMessage responseMessageAllCities = _httpClient.GetAsync(urlAllCities).Result;
            if (responseMessageAllCities.IsSuccessStatusCode)
            {
                // Binding the response to responAllCities
                responAllCities = responseMessageAllCities.Content.ReadAsStringAsync().Result;
            }
            var cities = JsonConvert.DeserializeObject<ObservableCollection<City>>(responAllCities);
            #endregion

            #region Initialization of SelectedCities
            //Connecting to API
            string urlSelectedCities = Connector.ApiConnectionString + "/userselect";
            // Getting the response from API
            HttpResponseMessage responseMessageSelectedCities = _httpClient.GetAsync(urlSelectedCities).Result;
            if (responseMessageSelectedCities.IsSuccessStatusCode)
            {
                // Binding the response to responSelectedCities
                responSelectedCities = responseMessageSelectedCities.Content.ReadAsStringAsync().Result;
            }
            //Initialization of the SelectedCities collection
            SelectedCities = JsonConvert.DeserializeObject<ObservableCollection<string>>(responSelectedCities);
            #endregion

            // Action of saving changes
            Action<string, bool> saveAction = (id, isSelected) =>
            {
                if (isSelected)
                {
                    SelectedCities.Add(id);
                    Select(id);
                }

                else
                {
                    SelectedCities.Remove(id);
                    Unselect(id);
                }
            };

            // Initialization for Cities collection
            Cities = new ObservableCollection<CityViewModel>(cities.Select(x =>new CityViewModel(x, saveAction)
            {
                IsChecked = SelectedCities.Contains(x.Id)
            }));

            #region Initialization of Commands
            //Binding searchCommand to SearchCity method
            searchCommand = new RelayCommand(async() => await SearchCity());

            //Binding statisticCommand to GoToStatistic method
            statisticCommand = new RelayCommand(() => GoToStatistic());
            #endregion
        }
        #endregion

        #region Methods
        /// <summary>
        ///  Searching the city Air Quality
        /// </summary>
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

        /// <summary>
        /// Select city and go to Api
        /// </summary>
        public void Select(string id)
        {
            //Content for Post Api
            var content = new StringContent(SerializeObject("post"), Encoding.UTF8, "application/json");

            // url for connecting to API
            string urlPost = Connector.ApiConnectionString + "/userselect/" + id;

            //Connection
            _httpClient.PostAsync(urlPost, content);
        }
 
        /// <summary>
        /// Unselect city and go to Api
        /// </summary>
        public void Unselect(string id)
        {

            // url for connecting to API
            string urlDelete = Connector.ApiConnectionString + "/userselect/" + id;

            //Connecting
            _httpClient.DeleteAsync(urlDelete);
        }

        public void GoToStatistic()
        {
            StatisticPageViewModel statisticPageViewModel = new StatisticPageViewModel();
            // Initialization of the Statistic Page
            StatisticPage statisticPage = new StatisticPage();
            statisticPage.DataContext = statisticPageViewModel;

            // Showing the Statistic Page
            statisticPage.Show();
        }

        /// <summary>
        /// Serialization for Post Api in Select() method
        /// </summary>
        private string SerializeObject<T>(T value, bool handleTimeZone = true)
        {
            var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            if (handleTimeZone) jsonSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            return JsonConvert.SerializeObject(value, jsonSettings);
        }
        #endregion

        #region Comments
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
