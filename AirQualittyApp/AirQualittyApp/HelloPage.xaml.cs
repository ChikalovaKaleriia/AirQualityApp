using AirQualittyApp.Models;
using AirQualittyApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirQualittyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class HelloPage : Window
    {
        public HelloPage()
        {
            InitializeComponent();
        }
        private static HttpClient _httpClient = new HttpClient();

        private void Button_Start(object sender, RoutedEventArgs e)
       {
            MainPageViewModel mainPageViewModel = new MainPageViewModel();
            MainPage main = new MainPage();
            main.DataContext = mainPageViewModel;
            main.Show();


            ////Content for Post Api
            //var content = new StringContent(SerializeObject("post"), Encoding.UTF8, "application/json");

            //// url for connecting to API
            //string urlPost = Connector.ApiConnectionString + "/airquality";

            ////Connection
            //_httpClient.PostAsync(urlPost, content);
        }
        private string SerializeObject<T>(T value, bool handleTimeZone = true)
        {
            var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            if (handleTimeZone) jsonSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            return JsonConvert.SerializeObject(value, jsonSettings);
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
