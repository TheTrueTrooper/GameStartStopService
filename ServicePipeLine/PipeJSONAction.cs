using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ServicePipeLine
{
    public interface IPipeJSONAction<T> : IPipeJSONAction
    {
        T ActionData { get; set; }
    }

    public class PipeJSONAction<T> : PipeJSONActionRoot, IPipeJSONAction, IPipeJSONAction<T>
    {
        [JsonProperty("ActionData")]
        public T ActionData { get; set; }

        [JsonIgnore]
        public override object ActionDataObj { get { return ActionData; } set { ActionData = ActionData; } }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public static PipeJSONAction<T> FromString(string Data)
        {
            return JsonConvert.DeserializeObject<PipeJSONAction<T>>(Data);
        }
    }

    public static class PipeJSONActionDynamicExt
    {
        public static PipeJSONAction<T> DynamicAutoCast<T>(this PipeJSONAction<dynamic> This)
        {
            return new PipeJSONAction<T>() { ActionData = (T)JsonConvert.DeserializeObject<T>(This.ActionData.ToString()), ActionName = This.ActionName };
        }

        public static object ActionDynamicAutoCast(this PipeJSONAction<dynamic> This, Type ReturnsSubType)
        {
            Type ReturnType = typeof(PipeJSONAction<>).MakeGenericType(ReturnsSubType);

            MethodInfo GenericMethod = typeof(PipeJSONActionDynamicExt).GetMethod("DynamicAutoCast");
            MethodInfo Method = GenericMethod.MakeGenericMethod(ReturnsSubType);
            object ReturnObj = Method.Invoke(null, new object[]{ This });

            return ReturnObj;
        }

        public static PipeJSONAction<dynamic> ToDynamic<T>(this PipeJSONAction<T> This)
        {
            return new PipeJSONAction<dynamic>() { ActionName = This.ActionName, ActionData = This.ActionData };
        }
    }
}
