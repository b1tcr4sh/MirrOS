using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using MirrOS.Config;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace MirrOS.UIElements.Weather
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
                UpdateWeather();
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

        async Task UpdateWeather()
        {
            
        }

        async Task<WeatherResponseModel> RequestWeatherData()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8"));
            client.DefaultRequestHeaders.Add("User-Agent", "MirrOS Weather Data Collector");

            // string response = await client.GetStringAsync($@"https://api.openweathermap.org/data/2.5/weather?q={location}&appid={apiKey}%units={units}");

            var responseTask = client.GetStreamAsync($@"https://api.openweathermap.org/data/2.5/weather?q={location}&appid={apiKey}%units={units}");
            var deserializedResponse = await JsonSerializer.DeserializeAsync<WeatherResponseModel>(await responseTask) ?? new WeatherResponseModel
            {
                coord = "Response Was Unable to be Deserialized",
                Weather = String.Empty
            };

            return deserializedResponse;
        }
    }
}
