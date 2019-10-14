using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicePipeLine;
using Newtonsoft.Json;

namespace TestClientConsole
{
    class SlaveInfo
    {
        public string MachineName;
        public string IP;
        public string Port;
        [JsonIgnore]
        public JSONServerSocket Connection;

        public override string ToString()
        {
            return $"{MachineName}:{IP}:{Port}";
        }
    }
}
