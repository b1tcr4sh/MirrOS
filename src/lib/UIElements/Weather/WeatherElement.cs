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
using System.Diagnostics;

namespace MirrOS.UIElements.Weather
{
    public class WeatherElement
    {
        private string location { get; set; }
        private string apiKey { get; set; }
        private ConfigFile.units units { get; set; }

        private int _temp;
        private int _feelsLike;
        private int _pressure;
        private int _humidity;
        private int _tempMin;
        private int _tempMax;
        private string _main;
        private string _desc;


        public WeatherElement()
        {
            PullParamsFromConfig();
            UpdateWeather();

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
            var response = await RequestWeatherData();

            Console.WriteLine(response);

            ProcessResponse(response);
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
                cod = -1,                
            };

            return deserializedResponse;
        }
        void ProcessResponse(WeatherResponseModel response)
        {
            string errorMessage;

            switch (response.cod)
            {
                case -1:
                    errorMessage = "An error occurred: Unable to resolve response from API host.";
                    break;
                case 404:
                    errorMessage = "An error occurred: Invalid location, please refer to documentation for help.";
                    break;
                case 401:
                    errorMessage = "An error occurred: Invalid API key, pleease refer to documentation for help.";
                    break;
                case 429:
                    errorMessage = "Fatal exception: API request limit reached, this is not supposed to happen and is likely a bug.";
                    break;
                case 500:
                case 502:
                case 503:
                case 504:
                    errorMessage = "An external server error occurred, please try again later";
                    break;
            }

            _temp = response.main.temp;
            _feelsLike = response.main.feels_like;
            _pressure = response.main.pressure;
            _humidity = response.main.humidity;
            _tempMin = response.main.temp_min;
            _tempMax = response.main.temp_max;
            _main = response.weather[0].main;
            _desc = response.weather[0].description;
        }
    }
}
