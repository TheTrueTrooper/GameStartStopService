using GameInstancerNS;
using GameStartStopService.TheServerClient.ClientModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStartStopService.ConfigLoaders
{
    /// <summary>
    /// A class to load the Game configs from XML
    /// </summary>
    public sealed class ServiceJSONGamesConfig : IGameConfig
    {
        public static string ConfigFile = "GameInstancerConfig.json";
        const string NotSetError = "On either one of the optional exes or primary game you have left the Path, PlayTime, or Delay empty in the XML config.\nMinimal configuration requires these to be set on each game and their extra exes.\nNote that a value of 0 will be infinite play time or no start delay.";

        public List<IGameModel> Games { get => GamesRaw.Cast<IGameModel>().ToList(); set => GamesRaw = value.Cast<Games>().ToList(); }

        public List<Games> GamesRaw { private set; get; } = new List<Games>();


        public IGameModel this[string GUID]
        {
            get
            {
                return Games.Where(x => x.GUID == GUID).FirstOrDefault();
            }
        }

        public IGameModel GetGameByName(string StartGameName)
        {
            return Games.Where(x => x.Name == StartGameName).FirstOrDefault();
        }

        public ServiceJSONGamesConfig(GetMachineGamesReturn GamesData)
        {
            foreach (Games CGM in GamesData.Games)
                GamesRaw.Add(CGM);
        }

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

            if (Games.Any(x => x.GUID == null || x.Path == null || x.PlayTime == null || x.IOptionalAddtionalExeStarts.Any(j => j.Path == null || j.Delay == null)))
                throw new Exception(NotSetError);
        }

    }
}
