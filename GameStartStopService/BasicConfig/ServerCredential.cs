using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStartStopService.BasicConfig
{
    internal class ServerCredential
    {
        [JsonProperty("Password")]
        internal string Password { get; set; }
        [JsonProperty("UserName")]
        internal string UserName { get; set; }
    }
}
