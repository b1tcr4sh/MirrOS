using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MirrOS.Models
{
    class MainWindowModel : INotifyPropertyChanged
    {

        private string _time;
        private string _date;

        public string Time
        {
            get => _time;
            set
            {
                if (value != _time)
                {
                    _time = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Date
        {
            get => _date;
            set
            {
                if (value != _date)
                {
                    _date = value;
                    OnPropertyChanged();
                }      
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
