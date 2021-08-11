using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MirrOS.Config
{
    class ConfigFileDataModel
    {
        public bool GESTURE_CONTROLS_ENABLED { get; set; }
        public bool TWELVEHOURTIME { get; set; }
        public string LOCATION { get; set; }
        public string OPENWEATHERAPI_KEY { get; set; }
        public ConfigFile.units UNITS { get; set; }
    }
}
