﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace WebMonitor
{
    class Send
    {
        // Define other methods and classes here
        public static Task<string> MakeAsyncRequest(string url, string data)
        {
            //http://stackoverflow.com/questions/3279888/how-to-add-parameters-into-a-webrequest
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("localhost:9000/" + url);
            request.Method = "POST";
            request.Proxy = null;
            request.ContentType = "application/x-www-form-urlencoded";

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

        private static string ReadStreamFromResponse(WebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader sr = new StreamReader(responseStream))
            {
                //Need to return this response 
                string strContent = sr.ReadToEnd();
                return strContent;
            }
        }

        public static string MakeRequest(string url, string data)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:9000/" + url);
                request.Method = "POST";
                request.Proxy = null;
                request.ContentType = "application/x-www-form-urlencoded";

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
