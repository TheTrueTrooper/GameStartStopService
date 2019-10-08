using GameInstancerNS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStartAndStopPipeClientNS
{
    public class GetMachineGamesReturn : IGameConfig
    {
        public string ID { get; set; }
        public string MachineName { get; set; }
        public string Description { get; set; }
        public bool IsAttendantRequired { get; set; }
        public string VRStoreID { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<Games> VRMachineGames { get; set; } = new List<Games>();

        [JsonIgnore]
        public IGameModel this[string GUID]
        {
            get
            {
                return Games.Where(x => x.GUID == GUID).FirstOrDefault();
            }
        }

        [JsonIgnore]
        public List<IGameModel> Games { get => VRMachineGames.Cast<IGameModel>().ToList(); set => VRMachineGames = value.Cast<Games>().ToList(); }

        public IGameModel GetGameByName(string GameName)
        {
            return Games.Where(x => x.Name == GameName).FirstOrDefault();
        }
    }

    public class Games : IGameModel
    {
        //name change
        [JsonProperty("VRGameID")]
        public string GUID { get; set; }
        public byte? MinNumberShares { get; set; }
        public byte? MaxNumberShares { get; set; }
        //originaly a decimal from server + name change
        [JsonProperty("Price")]
        public double? CostToPlay { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string Name { get; set; }
        public string StartOptions { get; set; }
        public string ImagePath { get; set; }
        public string Path { get; set; }
        //oringally an int from server
        public ulong? PlayTime { get; set; }
        public string Description { get; set; }

        public List<OptionalExeView> OptionalExes { get; set; } = new List<OptionalExeView>();

        [JsonIgnore]
        public List<IOptionalAddtionalExeStartsModel> IOptionalAddtionalExeStarts { get => OptionalExes.Cast<IOptionalAddtionalExeStartsModel>().ToList(); set => OptionalExes = value.Cast<OptionalExeView>().ToList(); }
    }

    public class OptionalExeView : IOptionalAddtionalExeStartsModel
    {
        public string ID { get; set; }
        public int? Delay { get; set; }
        public string Path { get; set; }
        public string StartOptions { get; set; }
        public bool IsDeleted { get; set; }
    }
}
