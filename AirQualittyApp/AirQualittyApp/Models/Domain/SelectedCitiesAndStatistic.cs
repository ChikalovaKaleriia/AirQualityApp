using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AirQualittyApp.Models.Domain
{
    public class SelectedCitiesAndStatistic : INotifyPropertyChanged
    {
        #region Private
        private string id;
        private string name;
        private string stringStatistic;
        private string average;
        #endregion

        #region Public
        public string Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        public string StringStatistic { get { return stringStatistic; } set { stringStatistic = value; OnPropertyChanged("StringStatistic"); } }
        public string Average { get { return average; } set { average = value; OnPropertyChanged("Average"); } }
        #endregion

        #region INotifyPropertyChanched
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
