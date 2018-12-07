using System;
using System.IO;
using System.Net;
using System.Text;

namespace Daily.Common
{
    public class HttpWebRequest
    {
        protected string Post(string fullUrl, string data)
        {
            WebRequest request = WebRequest.Create(fullUrl);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        protected string Get(string fullUrl)
        {
            WebRequest request = WebRequest.Create(fullUrl);
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";

            WebResponse response = request.GetResponse();
            Stream respPostStream = response.GetResponseStream();
            StreamReader readerPost = new StreamReader(respPostStream, Encoding.UTF8, true);

            string responseFromServer = readerPost.ReadToEnd();

            return responseFromServer;
        }
    }
}
