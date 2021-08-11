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
        public MainWindow()
        {
            InitializeComponent();
            
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            this.DataContext = new MainWindowModel();
        }
        
    }
}
