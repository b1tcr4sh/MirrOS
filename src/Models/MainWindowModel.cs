using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MirrOS.Config;
using MirrOS.UIElements;
using Avalonia.Threading;

namespace MirrOS.Models
{
    class MainWindowModel : INotifyPropertyChanged
    {
        private string clockLayout;

        private string _time;
        private string _date;
        private bool _twelveHourTime;
        private string _location;

        private string _temp;
        private string _feelsLike;
        private string _pressure;
        private string _humidity;
        private string _maxTemp;
        private string _minTemp;
        private string _description;
        private string _main;

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
        public bool TwelveHourTime
        {
            get => _twelveHourTime;
            set
            {
                if (value != _twelveHourTime) _twelveHourTime = value;
                OnPropertyChanged();
            }
        }


        public string Temp {
            get => $"Temperature | {_temp}";
            set 
            {
                if (value != _temp) _temp = value;
                OnPropertyChanged();
            }
        } 
        public string FeelsLike 
        {
            get => $"Feels Like | {_feelsLike}";
            set 
            {
                if (value != _feelsLike) _feelsLike = value;
                OnPropertyChanged();
            }
        }
        public string Pressure 
        {
            get => $"Pressure | {_pressure}";
            set 
            {
                if (value != _pressure) _pressure = value;
                OnPropertyChanged();
            }
        }
        public string Humidity 
        {
            get => $"Humidity | {_humidity}%";
            set 
            {
                if (value != _humidity) _humidity = value;
                OnPropertyChanged();
            }
        }
        public string MaxTemp 
        {
            get => $"Max Temperature | {_maxTemp}";
            set 
            {
                if (value != _maxTemp) _maxTemp = value;
                OnPropertyChanged();
            }
        }
        public string MinTemp
        {
            get => $"Min Temperature | {_minTemp}";
            set
            {
                if (value != _minTemp) _minTemp = value;
                OnPropertyChanged();
            }
        }
        public string Description {
            get => _description;
            set
            {
                if (value != _description) _description = value;
                OnPropertyChanged();
            }
        }
        public string MainDesc
        {
            get => _main;
            set
            {
                if (value != _main) _main = value;
                OnPropertyChanged();
            }
        }
        public string LocationWarning 
        {
            get => $"Results for {_location}*";
            set
            {
                if (value != _location) _location = value;
                OnPropertyChanged();
            }
        }
        
        public MainWindowModel()
        {
            InitializeConfigFile();
            UpdateTime();
            BindWeatherElement();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateTime()
        {
            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.IsEnabled = true;
            timer.Tick += (s, e) =>
            {
                Time = DateTime.Now.ToString(clockLayout);
                Date = DateTime.Now.ToString("d");
            };
        }
        private async Task InitializeConfigFile()
        {
            ConfigFile defaultConfig = new ConfigFile(@"../config/config.json");
            await defaultConfig.initializeAsync();
            ConfigFileDataModel config = await defaultConfig.readConfigAsync();

            _location = config.LOCATION;
            Conosle.WriteLine(config.LOCATION);

            InitializeClock(config);
        }
        private void InitializeClock(ConfigFileDataModel config)
        {
            TwelveHourTime = config.TWELVEHOURTIME;

            clockLayout = TwelveHourTime ? "h:mm tt" : "HH:mm tt";
        }
        private async Task BindWeatherElement() {            
            var weatherElement = new WeatherElement();

            Temp = Convert.ToString(weatherElement.temp);
            FeelsLike = Convert.ToString(weatherElement.feelsLike);
            Pressure = Convert.ToString(weatherElement.pressure);
            Humidity = Convert.ToString(weatherElement.humidity);
            MaxTemp = Convert.ToString(weatherElement.tempMax);
            MinTemp = Convert.ToString(weatherElement.tempMin);
            Description = weatherElement.desc;
            MainDesc = weatherElement.main;
        }
    }
}
