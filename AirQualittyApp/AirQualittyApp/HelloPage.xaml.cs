using AirQualittyApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private void Button_Start(object sender, RoutedEventArgs e)
       {
            MainPageViewModel mainPageViewModel = new MainPageViewModel();
            MainPage main = new MainPage();
            main.DataContext = mainPageViewModel;
            main.Show();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
