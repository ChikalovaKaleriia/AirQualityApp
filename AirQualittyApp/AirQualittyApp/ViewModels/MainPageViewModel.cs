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
    public class MainPageViewModel : INotifyPropertyChanged
    {
        static HttpClient httpClient = new HttpClient();
        public static ObservableCollection<City> Citys { get; set; }
        public ObservableCollection<City> SelectedCitys { get; set; }
        public string CityName { get; set; }

        private string quality;
        public string Quality { get { return quality; } set { quality = value; OnPropertyChanged("Quality"); } }

        private string colorForCanvas;
        public string ColorForCanvas { get { return colorForCanvas; } set { colorForCanvas = value; OnPropertyChanged("ColorForCanvas"); } }



        private readonly ICommand searchCommand;
        public ICommand SearchCommand => searchCommand;

        private readonly ICommand statisticCommand;
        public ICommand StatisticCommand => statisticCommand;

        public MainPageViewModel()
        {
            Citys = new ObservableCollection<City>
            {
            new City{Name = "Athens", IsSelected = false}, new City{Name = "Barcelona", IsSelected = false}, new City{Name = "Berlin", IsSelected = false},
            new City{Name = "Budapest", IsSelected = true}, new City{Name = "Cairo", IsSelected = false}, new City{Name = "Chicago", IsSelected = false},
            new City{Name = "Dublin", IsSelected = true}, new City{Name = "Dnipro", IsSelected = true}, new City{Name = "Istanbul", IsSelected = false},
            new City{Name = "Geneva", IsSelected = false}, new City{Name = "Kharkiv", IsSelected = false}, new City{Name = "Kiev", IsSelected = false},
            new City{Name = "London", IsSelected = false},  new City{Name = "Lisbon", IsSelected = false}, new City{Name = "Madrid", IsSelected = false},
            new City{Name = "Milan", IsSelected = false}, new City{Name = "Sydney", IsSelected = false}, new City{Name = "Warsaw", IsSelected = false}
            };
            searchCommand = new RelayCommand(async() => await SearchCity());
            statisticCommand = new RelayCommand(() => GoToStatistic());
        }

        public async Task SearchCity()
        {
            string url = "https://localhost:44387/airquality/" + CityName;
            HttpResponseMessage responseMessage = await httpClient.GetAsync(url);
            if(responseMessage.IsSuccessStatusCode)
            {
                Quality = await responseMessage.Content.ReadAsStringAsync();
            }

            //var airQualityProvider = new AirQualityProvider();
            //try
            //{
            //    var response = await airQualityProvider.GetCurrentQualityAsync(CityName);
            //    Quality = response.AirQuality.Quality.ToString();
            //}
            //catch(Exception e)
            //{
            //    MessageBox.Show(e.Message);
            //}

            //if(Convert.ToInt32(Quality) <= 50) ColorForCanvas = "#35cc38";
            //else if (Convert.ToInt32(Quality) >= 50 && Convert.ToInt32(Quality) <= 100) ColorForCanvas = "#f7ed60";
            //else if (Convert.ToInt32(Quality) >= 101 && Convert.ToInt32(Quality) <= 150) ColorForCanvas = "#f7a960";
            //else if (Convert.ToInt32(Quality) >= 151 && Convert.ToInt32(Quality) <= 200) ColorForCanvas = "#fa5252";
            //else if (Convert.ToInt32(Quality) >= 201 && Convert.ToInt32(Quality) <= 300) ColorForCanvas = "#c252fa";
            //else if (Convert.ToInt32(Quality) >= 301) ColorForCanvas = "#590d11";
        }

        public void GoToStatistic()
        {
            SelectedCitys = new ObservableCollection<City>();
            foreach(var c in Citys)
            {
                if(c.IsSelected == true)
                {
                    SelectedCitys.Add(c);
                }
            }
            StatisticPage statisticPage = new StatisticPage();
            statisticPage.Show();
            Messenger.Default.Send<ObservableCollection<City>>(SelectedCitys);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
