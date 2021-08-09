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
        public enum units
        {
            standard,
            metric,
            imperial
        }
        public enum windowState
        {
            fullscreen,
            maximized,
            minimized
        }
        public string path;

        public ConfigFile (string filePath)
        {
            path = filePath;
        }

        public async Task initializeAsync()
        {
            ConfigFileDataModel model = new ConfigFileDataModel()
            {
                GESTURE_CONTROLS_ENABLED = false,
                SCREEN_ORIENTATION = orientation.horizontal,
                WINDOWSTATE = windowState.fullscreen,
                TWELVEHOURTIME = true,
                OPENWEATHERAPI_KEY = "Your API Key Here (Visit docs for instructions)",
                LOCATION = "Your City Here (Visit docs for instructions)",
                UNITS = units.standard
            };

            string current = File.ReadAllText(path);
            if (current == null)
            {
                // Create and serialize ConfigFileDataModel to path
                using FileStream createStream = File.Create(path);
                await JsonSerializer.SerializeAsync(createStream, model);
                await createStream.DisposeAsync();
            }   
        }
        public async Task<ConfigFileDataModel> readConfigAsync()
        {
            using FileStream openStream = File.OpenRead(path);
            return await JsonSerializer.DeserializeAsync<ConfigFileDataModel>(openStream);
        }
    }
}
