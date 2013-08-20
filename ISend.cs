using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace WebMonitor
{
    public interface ISend
    {
        Task<string> MakeAsyncRequest(string url, string data);
        string ReadStreamFromResponse(WebResponse response);
        string MakeRequest(string url, string data);
    }
}
