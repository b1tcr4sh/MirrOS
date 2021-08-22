using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MirrOS.UIElements.Weather
{
    class OpenWeatherApiResponseModelDynamic
    {
        public dynamic coord { get; set; }
        public dynamic[] weather { get; set; }
        public dynamic main { get; set; }
        public int cod { get; set; }
    }
    class OpenWeatherApiResponseModel
    {
        public Coord coord { get; set; }
        public WeatherArray[] weather { get; set; }
        public Main main { get; set; }
        public int cod { get; set; }
    }
    class Coord
    {
        public int lon { get; set; }
        public int lat { get; set; }
    }
    class WeatherArray
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
    class Main
    {
        public int temp { get; set; }
        public int feels_like { get; set; }
        public int temp_min { get; set; }
        public int temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }
}
