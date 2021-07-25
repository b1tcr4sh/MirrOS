using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MirrOS.Config
{
    class ConfigFile
    {
        public enum orientation
        {
            horizontal,
            vertical
        }
        public string path;

        public ConfigFile (string filePath)
        {
            path = filePath;
        }

        public void initialize()
        {
            ConfigFileDataModel model = new ConfigFileDataModel();

            List<ConfigFileDataModel> data = new List<ConfigFileDataModel>();
            data.Add(new ConfigFileDataModel()
            {
                GESTURE_CONTROLS_ENABLED = false,
                LOCATION = "dallas",
                SCREEN_ORIENTATION = orientation.horizontal
            });

            string jsonString = JsonSerializer.Serialize(data);
            File.Create(path);
            File.WriteAllText(path, jsonString);
        }
        public ConfigFileDataModel readConfig()
        {
            string jsonString = File.ReadAllText(path);
            return JsonSerializer.Deserialize<ConfigFileDataModel>(jsonString);
        }
    }
}
