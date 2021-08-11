using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Reactive.Subjects;
using System;
using System.Threading.Tasks;
using Avalonia.Threading;
using MirrOS.Config;
using MirrOS.Models;
using MirrOS.UIElements.Weather;

namespace MirrOS
{
    public partial class MainWindow : Window
    {
        bool _displayHorizontal;
        public MainWindow()
        {
            InitializeConfigFile();
            InitializeComponent();
            
#if DEBUG
            this.AttachDevTools();
#endif
            UpdateTime();
            var weatherElement = new WeatherElement();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.DataContext = new MainWindowModel()
            {
                Time = DateTime.Now.ToString("hh:mm tt"),
                Date = DateTime.Now.ToString("d"),
                DisplayHorizontal = _displayHorizontal
            };
        }
        public void UpdateTime()
        {
            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.IsEnabled = true;
            timer.Tick += (s, e) =>
            {
                var context = this.DataContext as MainWindowModel;
                context.Time = DateTime.Now.ToString("hh:mm tt");
                context.Date = DateTime.Now.ToString("d");
            };
        }
        private async Task InitializeConfigFile()
        {
            ConfigFile defaultConfig = new ConfigFile(@"../config/config.json");
            await defaultConfig.initializeAsync();
            InitializeWindowSetttings(defaultConfig);
        }
        private async Task InitializeWindowSetttings(ConfigFile defaultConfig)
        {
            var config = await defaultConfig.readConfigAsync();

            if (config.SCREEN_ORIENTATION == ConfigFile.orientation.horizontal)
            {
                _displayHorizontal = true;
            }
            else
            {
                _displayHorizontal = false;
            }
        }
    }
}
