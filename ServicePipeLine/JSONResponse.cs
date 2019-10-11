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
    public interface IJSONResponse<T> : IJSONResponse
    {
        T ActionData { get; set; }
    }

    public class JSONResponse<T> : JSONResponseRoot, IJSONResponse, IJSONResponse<T>
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

        public static JSONResponse<T> FromString(string Data)
        {
            return JsonConvert.DeserializeObject<JSONResponse<T>>(Data);
        }
    }

    public static class JSONResponseDynamicExt
    {
        public static JSONResponse<T> DynamicAutoCast<T>(this JSONResponse<dynamic> This)
        {
            try
            {
                return new JSONResponse<T>() { ActionData = (T)JsonConvert.DeserializeObject<T>(This.ActionData.ToString()), RequestStatus = This.RequestStatus, ActionName = This.ActionName, Message = This.Message };
            }
            catch
            {
                return new JSONResponse<T>() { ActionData = This.ActionData, RequestStatus = This.RequestStatus, ActionName = This.ActionName, Message = This.Message };
            }
        }

        public static object ResponseDynamicAutoCast(this JSONResponse<dynamic> This, Type ReturnsSubType)
        {
            Type ReturnType = typeof(JSONResponse<>).MakeGenericType(ReturnsSubType);

            MethodInfo GenericMethod = typeof(JSONResponseDynamicExt).GetMethod("DynamicAutoCast");
            MethodInfo Method = GenericMethod.MakeGenericMethod(ReturnsSubType);
            object ReturnObj = Method.Invoke(null, new object[] { This });

            return ReturnObj;
        }

        public static JSONResponse<dynamic> ToDynamic<T>(this JSONResponse<T> This)
        {
            return new JSONResponse<dynamic>() { ActionName = This.ActionName, RequestStatus = This.RequestStatus, ActionData = This.ActionData, Message = This.Message };
        }
    }
}
