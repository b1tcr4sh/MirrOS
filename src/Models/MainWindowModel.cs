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
        private string clockLayout;

        private string _time;
        private string _date;
        private bool _twelveHourTime;

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

        public MainWindowModel()
        {
            InitializeConfigFile();
            UpdateTime();
            var weatherElement = new WeatherElement();
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

            InitializeClock(config);
        }
        private void InitializeClock(ConfigFileDataModel config)
        {
            TwelveHourTime = config.TWELVEHOURTIME;

            clockLayout = TwelveHourTime ? "hh:mm tt" : "HH:mm tt";
        }
    }
}
