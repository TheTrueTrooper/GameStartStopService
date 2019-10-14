using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServerConsole
{
    /// <summary>
    /// A class to load the Game configs from XML
    /// </summary>
    public sealed class ServiceJSONGamesConfig
    {
        public static string ConfigFile = "GameInstancerConfig.json";
        const string NotSetError = "On either one of the optional exes or primary game you have left the Path, PlayTime, or Delay empty in the XML config.\nMinimal configuration requires these to be set on each game and their extra exes.\nNote that a value of 0 will be infinite play time or no start delay.";

        public List<Games> GamesRaw { private set; get; } = new List<Games>();


        public ServiceJSONGamesConfig(Stream Stream = null)
        {
            if (Stream == null)
                Stream = new FileStream(ConfigFile, FileMode.Open);
            using (Stream)
            using (StreamReader sr = new StreamReader(Stream))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                List<Games> ScopedList = serializer.Deserialize<List<Games>>(reader).ToList();
                //Temp for a fast generate
                //using (StreamWriter SW = new StreamWriter($"n{ConfigFile}"))
                //    serializer.Serialize(SW, ScopedList);
                // read the json from a stream
                // json size doesn't matter because only a small piece is read at a time from the HTTP request
                foreach (Games CGM in ScopedList)
                    GamesRaw.Add(CGM);
            }
        }

    }
}
