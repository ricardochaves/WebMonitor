using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace WebMonitor.services
{
    public class Send : ISend
    {

        // Define other methods and classes here
        public Task<string> MakeAsyncRequest(string url, string data, string method) 
        {
            try
            {

                Util.WriteLog(Strings.HOST + url + "?apiKey=" + WMGlobalSettings.Instance.Key);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Strings.HOST + url + "?apiKey=" + WMGlobalSettings.Instance.Key);
                request.Method = method;
                request.Proxy = null;
                request.ContentType = "application/json";

                byte[] dataStream = Encoding.UTF8.GetBytes(data);
                Stream newStream = request.GetRequestStream();
                newStream.Write(dataStream, 0, dataStream.Length);
                newStream.Close();

                Task<WebResponse> task = Task.Factory.FromAsync(
                    request.BeginGetResponse,
                    asyncResult => request.EndGetResponse(asyncResult),
                    (object)null);

                return task.ContinueWith(t => ReadStreamFromResponse(t.Result));
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }

        private string ReadStreamFromResponse(WebResponse response)
        {
            try
            {
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader sr = new StreamReader(responseStream))
                {
                    //Need to return this response 
                    string strContent = sr.ReadToEnd();
                    return strContent;
                }
            }
            catch (Exception)
            {
                return "";
                //throw ex;
            }

        }

        public string MakeRequest(string url, string data, string method)
        {
            try
            {

                Util.WriteLog(Strings.HOST + url + "?apiKey=" + WMGlobalSettings.Instance.Key);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Strings.HOST + url + "?apiKey=" + WMGlobalSettings.Instance.Key);
                request.Method = method;
                request.Proxy = null;
                request.ContentType = "application/json";

                byte[] dataStream = Encoding.UTF8.GetBytes(data);
                Stream newStream = request.GetRequestStream();
                newStream.Write(dataStream, 0, dataStream.Length);
                newStream.Close();

                WebResponse teste  =  request.GetResponse();

                using (Stream responseStream = teste.GetResponseStream())
                using (StreamReader sr = new StreamReader(responseStream))
                {
                    //Need to return this response 
                    string strContent = sr.ReadToEnd();
                    return strContent;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
