using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMonitor
{
    public interface IConvertJson
    {
         T ConvertJSON<T>(string Json);
    }
}
