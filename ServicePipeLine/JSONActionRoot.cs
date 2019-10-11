using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePipeLine
{
    public interface IJSONAction
    {
        string ActionName { get; set; }
        object ActionDataObj { get; set; }
    }

    public abstract class JSONActionRoot : IJSONAction
    {
        [JsonProperty("ActionName")]
        public string ActionName { get; set; }
        public abstract object ActionDataObj { get; set; }
    }
}
