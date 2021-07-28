using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using MirrOS.Config;

namespace MirrOS.UIElements
{
    class WeatherElement
    {
        private string location { get; set; }
        private string apiKey { get; set; }
        private ConfigFile.units units { get; set; }


        WeatherElement()
        {
            PullParamsFromConfig();

            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);

            timer.Interval = TimeSpan.FromHours(1);
            timer.IsEnabled = true;
            timer.Tick += (s, e) =>
            {
                RequestWeatherData();
            };
        }
        async Task PullParamsFromConfig()
        {
            ConfigFile config = new ConfigFile(@"../config/config.json");
            var configObject = await config.readConfigAsync();
            location = configObject.LOCATION;
            apiKey = configObject.OPENWEATHERAPI_KEY;
            units = configObject.UNITS;
        }

        void RequestWeatherData()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8"));
            client.DefaultRequestHeaders.Add("User-Agent", )
        }
    }
}
