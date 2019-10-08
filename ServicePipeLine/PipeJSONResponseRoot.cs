using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePipeLine
{
    public interface IPipeJSONResponse
    {
        string ActionName { get; set; }
        string Message { get; set; }
        PipeJSONResponseStatus RequestStatus { get; set; }
        object ActionDataObj { get; set; }
    }

    public enum PipeJSONResponseStatus
    {
        UnknownFailure, // 0 defualt
        ActionNotFound,
        InternalFailure,
        ActionNotAllowed,
        Success,
        TimeOut
    }

    public abstract class PipeJSONResponseRoot : IPipeJSONResponse
    {
        [JsonProperty("ActionName")]
        public string ActionName { get; set; }

        [JsonProperty("RequestStatus"), JsonConverter(typeof(StringEnumConverter))]
        public PipeJSONResponseStatus RequestStatus { get; set; }

        public abstract object ActionDataObj { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
