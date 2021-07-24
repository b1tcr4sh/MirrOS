using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Reactive.Subjects;
using System;
using MirrOS.Models;
using Avalonia.Threading;

namespace MirrOS
{
    public partial class MainWindow : Window
    {
        private string currentTime = DateTime.Now.ToString("hh:mm tt");
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowModel() { Time = currentTime };
#if DEBUG
            this.AttachDevTools();
#endif
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
    }
}
