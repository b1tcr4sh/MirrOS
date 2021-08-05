using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MirrOS.UIElements.Weather
{
    class WeatherResponseModel
    {
        public dynamic coord { get; set; }
        public dynamic[] weather { get; set; }
        public dynamic main { get; set; }
        public int cod { get; set; }
    }
}
