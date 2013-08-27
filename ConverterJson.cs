using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WebMonitor
{
    class ConverterJson : IConvertJson
    {
        public T ConvertJSON<T>(string Json)
        {

            return JsonConvert.DeserializeObject<T>(Json);

        }
    }
}
