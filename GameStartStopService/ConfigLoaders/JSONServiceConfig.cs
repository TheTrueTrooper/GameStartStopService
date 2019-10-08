using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStartStopService.BasicConfig;
using Newtonsoft.Json;

namespace GameStartStopService.ConfigLoaders
{


    /// <summary>
    /// A class to load the Game configs from XML
    /// </summary>
    public static class JSONServiceConfig
    {
        internal static string ConfigFile = "GameServiceConfig.json";
        const string NotSetError = "Warrning incomplete config file.";

        internal static GameServiceConfig GetJSONServiceConfig()
        {
            GameServiceConfig Settings;
            using (Stream Stream = new FileStream(ConfigFile, FileMode.Open))
            using (StreamReader SR = new StreamReader(Stream))
            using (JsonReader Reader = new JsonTextReader(SR))
            {
                JsonSerializer Serializer = new JsonSerializer();
                Settings = Serializer.Deserialize<GameServiceConfig>(Reader);
            }

            Dictionary<string, List<string>> Errors;
            if (!ConfigHelpers.ValidateConfig(Settings, out Errors))
            {
                ConfigEditor Editor = ConfigEditor.ConfigEditorFactoryFromConfig(true);
                System.Windows.Forms.Application.Run(Editor);
                GetJSONServiceConfig();
            }

            return Settings;
        }

        internal static void SaveJSONServiceConfig(GameServiceConfig Settings)
        {
            using (Stream Stream = new FileStream(ConfigFile, FileMode.Create))
            using (StreamWriter sr = new StreamWriter(Stream))
            {
                sr.Write(JsonConvert.SerializeObject(Settings, Formatting.Indented));
                sr.Close();
            }
        }

    }
}
