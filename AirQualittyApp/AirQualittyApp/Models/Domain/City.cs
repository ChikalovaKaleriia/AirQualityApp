using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AirQualittyApp.Models
{
    /// <summary>
    /// Model of city
    /// </summary>
    public class City : INotifyPropertyChanged
    {

        private string id;
        private string name;

        /// <summary>
        ///  Id of the city
        /// </summary>
        public string Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }

        /// <summary>
        /// Name of the city
        /// </summary>
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }

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
