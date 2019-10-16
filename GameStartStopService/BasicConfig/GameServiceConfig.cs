using GameStartStopService.ConfigEnums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStartStopService.BasicConfig
{
    internal class GameServiceConfig
    {
        [JsonProperty("MachineGUID")]
        internal string MachineGUID { get; set; }
        [JsonProperty("GameStarterMode"), JsonConverter(typeof(StringEnumConverter))]
        internal GameStartMode? StarterMode { get; set; }
        [JsonProperty("ServerMode"), JsonConverter(typeof(StringEnumConverter))]
        internal ServerMode? ServerMode { get; set; }
        [JsonProperty("CardModeMode"), JsonConverter(typeof(StringEnumConverter))]
        internal CardModeMode? CardModeMode { get; set; }
        [JsonProperty("MasterServerURL")]
        internal string MasterServerURL { get; set; }
        [JsonProperty("MasterStarterMasterLoc")]
        internal string MasterStarterMasterLoc { get; set; }
        [JsonProperty("LocalLogOutput")]
        internal string LocalLogOutput { get; set; }
        [JsonProperty("ServerLogOutput")]
        internal string ServerLogOutput { get; set; }
        [JsonProperty("ServerCredential")]
        internal ServerCredential ServerCredential { get; set; } = new ServerCredential();
        [JsonProperty("MachineName")]
        internal string MachineName { get; set; }
        [JsonProperty("PortNumber")]
        internal int PortNumber { get; set; }
        //depreciated with game selector
        //[JsonProperty("DefualtGameGUID")]
        //internal string DefualtGameGUID { get; set; }
    }
}
