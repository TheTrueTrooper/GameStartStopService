using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServicePipeLine
{
    public interface IPipeJSONResponse<T> : IPipeJSONResponse
    {
        T ActionData { get; set; }
    }

    public class PipeJSONResponse<T> : PipeJSONResponseRoot, IPipeJSONResponse, IPipeJSONResponse<T>
    {
        [JsonProperty("ActionData")]
        public T ActionData { get; set; }

        [JsonIgnore]
        public override object ActionDataObj { get { return ActionData; }  set { ActionData = (T)ActionData; }  }

        public override string ToString()
        {
            string Return = JsonConvert.SerializeObject(this, Formatting.None);
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public static PipeJSONResponse<T> FromString(string Data)
        {

            return JsonConvert.DeserializeObject<PipeJSONResponse<T>>(Data);
        }
    }

    public static class PipeJSONResponseDynamicExt
    {
        public static PipeJSONResponse<T> DynamicAutoCast<T>(this PipeJSONResponse<dynamic> This)
        {
            try
            {
                return new PipeJSONResponse<T>() { ActionData = (T)JsonConvert.DeserializeObject<T>(This.ActionData.ToString()), RequestStatus = This.RequestStatus, ActionName = This.ActionName, Message = This.Message };
            }
            catch
            {
                return new PipeJSONResponse<T>() { ActionData = This.ActionData, RequestStatus = This.RequestStatus, ActionName = This.ActionName, Message = This.Message };
            }
        }

        public static object ResponseDynamicAutoCast(this PipeJSONResponse<dynamic> This, Type ReturnsSubType)
        {
            Type ReturnType = typeof(PipeJSONAction<>).MakeGenericType(ReturnsSubType);

            MethodInfo GenericMethod = typeof(PipeJSONActionDynamicExt).GetMethod("DynamicAutoCast");
            MethodInfo Method = GenericMethod.MakeGenericMethod(ReturnsSubType);
            object ReturnObj = Method.Invoke(null, new object[] { This });

            return ReturnObj;
        }

        public static PipeJSONResponse<dynamic> ToDynamic<T>(this PipeJSONResponse<T> This)
        {
            return new PipeJSONResponse<dynamic>() { ActionName = This.ActionName, RequestStatus = This.RequestStatus, ActionData = This.ActionData, Message = This.Message };
        }
    }
}
