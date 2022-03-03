using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AirQualittyApp.Models
{
    public class City : INotifyPropertyChanged
    {
        private string name;
        private bool isSelected;
        public string Name { get { return name; } set { name = value; } }
        public bool IsSelected { get { return isSelected; } set { isSelected = value; } }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
