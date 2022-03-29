using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AirQualittyApp.Models
{
    public class CityViewModel : INotifyPropertyChanged
    {
        #region Private
        private readonly City _city;
        private readonly Action<string, bool> _saveChanges;
        private bool _isChecked ;
        #endregion

        #region Public
        /// <summary>
        /// Name of the selected sity
        /// </summary>
        public string Name => _city.Name;

        /// <summary>
        /// Property for Checkbox
        /// </summary>
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                _saveChanges(_city.Id, IsChecked);
            }
        }
        #endregion

        #region Initialization
        public CityViewModel(City city, Action<string, bool> saveChanges)
        {
            _city = city;
            _saveChanges = saveChanges;
        }
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
