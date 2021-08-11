using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MirrOS.Config;
using MirrOS.UIElements.Weather;
using Avalonia.Threading;

namespace MirrOS.Models
{
    class MainWindowModel : INotifyPropertyChanged
    {

        private string _time;
        private string _date;
        private bool _displayHorizontal;
        private int _height;
        private int _width;

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


        public int Width 
        { 
            get => _width;
            private set
            {
                if (_width != value) _width = value;
                OnPropertyChanged();
            } 
        }
        public int Height { 
            get => _height; 
            private set
            {
                if (_height != value) _height = value;
                OnPropertyChanged();
            }
        }
        public bool DisplayHorizontal 
        { 
            get => _displayHorizontal; 
            set
            {
                ChangeDisplayOrientation(value);    
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ChangeDisplayOrientation(bool isHorizontal)
        {
            if (isHorizontal)
            {
                Width = 1920;
                Height = 1080;
                _displayHorizontal = true;
            }
            else
            {
                Width = 1080;
                Height = 1920;
                _displayHorizontal = false;
            }
        }
    }
}
