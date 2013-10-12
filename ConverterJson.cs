using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//!CompilerOption:AddRef:System.Web.Extensions.dll
using System.Web.Script.Serialization;

namespace WebMonitor
{
    public class ConverterJson : IConvertJson
    {
        public T ConvertJSON<T>(string Json)
        {
            try
            {
                JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                T routes_list = json_serializer.Deserialize<T>(Json);
                return routes_list;
            }
            catch (Exception ex )
            {
                
                throw ex;
            }


        }

        public string ConvertTOJson(Object o)
        {

            try
            {
                String s;
                JavaScriptSerializer json_serializer = new JavaScriptSerializer();

                s = json_serializer.Serialize(o);
                s = s.Replace(@"\/Date(", "");
                s = s.Replace(@")\/", "");
                return s;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }

    }
}
