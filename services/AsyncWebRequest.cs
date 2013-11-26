using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace WebMonitor.services
{

    public class AsyncWebRequest
    {
        public void MakeWebRequestAsync(string url, string data, method method)
        {
            try
            {
                Util.WriteLog("Entrou no MakeWebRequestAsync: " + DateTime.Now.ToString());
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Strings.HOST + url + "?apiKey=" + WMGlobalSettings.Instance.Key);
                request.Method = method.ToString();
                request.Proxy = null;
                request.ContentType = "application/json";


                byte[] dataStream = Encoding.UTF8.GetBytes(data);
                Stream newStream = request.GetRequestStream();
                newStream.Write(dataStream, 0, dataStream.Length);
                newStream.Close();

                RequestState state = new RequestState();
                state.Request = request;

                IAsyncResult result = request.BeginGetResponse(new AsyncCallback(ResponseCallback), state);
            }
            catch (Exception ex)
            {
                // Error handling
            }
        }

        private void ResponseCallback(IAsyncResult result)
        {
            try
            {
                // Get and fill the RequestState
                RequestState state = (RequestState)result.AsyncState;
                HttpWebRequest request = state.Request;
                // End the Asynchronous response and get the actual resonse object
                state.Response = (HttpWebResponse)request.EndGetResponse(result);
                Stream responseStream = state.Response.GetResponseStream();
                state.ResponseStream = responseStream;

                // Begin async reading of the contents
                IAsyncResult readResult = responseStream.BeginRead(state.BufferRead,
                        0, state.BufferSize, new System.AsyncCallback(ReadCallback), state);
            }
            catch (Exception ex)
            {
                // Error handling
                RequestState state = (RequestState)result.AsyncState;
                if (state.Response != null)
                    state.Response.Close();
            }
        }

        private void ReadCallback(IAsyncResult result)
        {
            try
            {
                // Get RequestState
                RequestState state = (RequestState)result.AsyncState;
                // determine how many bytes have been read
                int bytesRead = state.ResponseStream.EndRead(result);

                if (bytesRead > 0) // stream has not reached the end yet
                {
                    // append the read data to the ResponseContent and...
                    state.ResponseContent.Append(Encoding.ASCII.GetString(state.BufferRead, 0, bytesRead));
                    // ...read the next piece of data from the stream
                    state.ResponseStream.BeginRead(state.BufferRead, 0, state.BufferSize,
                        new System.AsyncCallback(ReadCallback), state);
                }
                else // end of the stream reached
                {
                    if (state.ResponseContent.Length > 0)
                    {
                        // do something with the response content, e.g. fill a property or fire an event
                        // AsyncResponseContent = state.ResponseContent.ToString();
                        // close the stream and the response
                        state.ResponseStream.Close();
                        state.Response.Close();
                        //OnAsyncResponseArrived(AsyncResponseContent);
                    }
                }
            }
            catch (Exception ex)
            {
                // Error handling
                RequestState state = (RequestState)result.AsyncState;
                if (state.Response != null)
                    state.Response.Close();
            }
        }
    }
}
