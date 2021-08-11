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

        public int temp { get; private set; }
        public int feelsLike { get; private set; }
        public int pressure { get; private set; }
        public int humidity { get; private set; }
        public int tempMin { get; private set; }
        public int tempMax { get; private set; }
        public string main { get; private set; }
        public string desc { get; private set; }


        public WeatherElement()
        {
            Initialize();

            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);

            timer.Interval = TimeSpan.FromHours(1);
            timer.IsEnabled = true;
            timer.Tick += (s, e) =>
            {
                UpdateWeather();
            };
        }
        async Task Initialize()
        {
            await PullParamsFromConfig();
            await UpdateWeather();
        }
        async Task PullParamsFromConfig()
        {
            ConfigFile config = new ConfigFile(@"../config/config.json");
            var configObject = await config.readConfigAsync();
            location = configObject.LOCATION;
            apiKey = configObject.OPENWEATHERAPI_KEY;
            units = configObject.UNITS;
        }

        public async Task UpdateWeather()
        {
            var response = await RequestWeatherData();

            ProcessResponse(response);
        }

        async Task<WeatherResponseModel> RequestWeatherData()
        {
            HttpClient client = new HttpClient();
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={location}&appid={apiKey}&units={units}";
            Console.WriteLine(url);

            var responseTask = client.GetStreamAsync(url);

            var deserializedResponse = await JsonSerializer.DeserializeAsync<WeatherResponseModel>(await responseTask) ?? new WeatherResponseModel
            {
                cod = -1,                
            }; 

            return deserializedResponse; 
        }
        void ProcessResponse(WeatherResponseModel response)
        {
            string errorMessage = String.Empty;

            switch (response.cod)
            {
                case 200:
                    errorMessage = "200 Okay, request resolved successfully";
                    break;
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
#if DEBUG
            Console.WriteLine(errorMessage);
#endif

            temp = response?.main.temp;
            feelsLike = response?.main.feels_like;
            pressure = response?.main.pressure;
            humidity = response?.main.humidity;
            tempMin = response?.main.temp_min;
            tempMax = response?.main.temp_max;
            main = response.weather[0].main;
            desc = response.weather[0].description;
        }
    }
}
