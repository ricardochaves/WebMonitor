using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//!CompilerOption:AddRef:System.Web.Extensions.dll
using System.Web.Script.Serialization;

namespace WebMonitor
{
    class ConverterJson : IConvertJson
    {
        public T ConvertJSON<T>(string Json)
        {
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            T routes_list = (T)json_serializer.DeserializeObject(Json);
            return routes_list;

        }
    }
}
