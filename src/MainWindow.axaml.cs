using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Reactive.Subjects;
using System;
using System.Threading.Tasks;
using MirrOS.Models;
using Avalonia.Threading;
using MirrOS.Config;
using System.IO;

namespace MirrOS
{
    public partial class MainWindow : Window
    {   
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowModel() 
            { 
                Time = DateTime.Now.ToString("hh:mm tt"), 
                Date = DateTime.Now.ToString("d") 
            };
#if DEBUG
            this.AttachDevTools();
#endif
            initializeConfigFile();

            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.IsEnabled = true;
            timer.Tick += (s, e) =>
            {
                UpdateTime();
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            
        }
        public void UpdateTime()
        {
            var context = this.DataContext as MainWindowModel;
            context.Time = DateTime.Now.ToString("hh:mm tt");
            context.Date = DateTime.Now.ToString("d");
        }
        private async Task initializeConfigFile()
        {
            ConfigFile defaultConfig = new ConfigFile(@"../config/config.json");
            await defaultConfig.initializeAsync();
            Console.WriteLine(await defaultConfig.readConfigAsync());
        }
    }
}
