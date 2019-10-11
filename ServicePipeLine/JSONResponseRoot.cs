using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePipeLine
{
    public interface IJSONResponse
    {
        string ActionName { get; set; }
        string Message { get; set; }
        JSONResponseStatus RequestStatus { get; set; }
        object ActionDataObj { get; set; }
    }

    public enum JSONResponseStatus
    {
        NotPresent,// 0 defualt
        UnknownFailure,
        ActionNotFound,
        InternalFailure,
        ActionNotAllowed,
        Success,
        TimeOut
    }

    public abstract class JSONResponseRoot : IJSONResponse
    {
        [JsonProperty("ActionName")]
        public string ActionName { get; set; }

        [JsonProperty("RequestStatus"), JsonConverter(typeof(StringEnumConverter))]
        public JSONResponseStatus RequestStatus { get; set; }

        public abstract object ActionDataObj { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
