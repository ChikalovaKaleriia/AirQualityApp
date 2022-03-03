using AirQualittyApp.Models;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AirQualittyApp.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<City> Citys { get; set; }
        public  ObservableCollection<City> SelectedCitys { get; set; }
        public string CityName { get; set; }
        


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
            var airQualityProvider = new AirQualityProvider();
            var response =  await airQualityProvider.GetCurrentQualityAsync(CityName);
            var qua = response.AirQuality.Quality;
            
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
