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
    public interface IJSONAction<T> : IJSONAction
    {
        T ActionData { get; set; }
    }

    public class JSONAction<T> : JSONActionRoot, IJSONAction, IJSONAction<T>
    {
        [JsonProperty("ActionData")]
        public T ActionData { get; set; }

        [JsonIgnore]
        public override object ActionDataObj { get { return ActionData; } set { ActionData = ActionData; } }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }

        public static JSONAction<T> FromString(string Data)
        {
            return JsonConvert.DeserializeObject<JSONAction<T>>(Data);
        }
    }

    public static class JSONActionDynamicExt
    {
        public static JSONAction<T> DynamicAutoCast<T>(this JSONAction<dynamic> This)
        {
            return new JSONAction<T>() { ActionData = (T)JsonConvert.DeserializeObject<T>(This.ActionData.ToString()), ActionName = This.ActionName };
        }

        public static object ActionDynamicAutoCast(this JSONAction<dynamic> This, Type ReturnsSubType)
        {
            Type ReturnType = typeof(JSONAction<>).MakeGenericType(ReturnsSubType);

            MethodInfo GenericMethod = typeof(JSONActionDynamicExt).GetMethod("DynamicAutoCast");
            MethodInfo Method = GenericMethod.MakeGenericMethod(ReturnsSubType);
            object ReturnObj = Method.Invoke(null, new object[]{ This });

            return ReturnObj;
        }

        public static JSONAction<dynamic> ToDynamic<T>(this JSONAction<T> This)
        {
            return new JSONAction<dynamic>() { ActionName = This.ActionName, ActionData = This.ActionData };
        }
    }
}
