using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Reactive.Subjects;
using System;
using MirrOS.Models;

namespace MirrOS
{
    public partial class MainWindow : Window
    {
        public string currentTime = DateTime.Now.ToString("hh:mm tt");
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowModel() { Time = currentTime };
#if DEBUG
            this.AttachDevTools();
#endif
            
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private void UpdateTime()
        {

            if (currentTime != DateTime.Now.ToString("hh:mm tt"))
            {
                currentTime = DateTime.Now.ToString("hh:mm tt");

                var context = this.DataContext as MainWindowModel;
                context.Time = DateTime.Now.ToString("hh:mm tt");
            }
        }
    }
}
