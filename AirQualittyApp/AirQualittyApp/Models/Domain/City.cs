using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AirQualittyApp.Models
{
    // Model for city
    public class City : INotifyPropertyChanged
    {
        // Name of the city
        private string name;
        public string Name { get { return name; } set { name = value; } }

        // Did user select city for analyzing statistic 
        private bool isSelected;
        public bool IsSelected { get { return isSelected; } set { isSelected = value; } }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
