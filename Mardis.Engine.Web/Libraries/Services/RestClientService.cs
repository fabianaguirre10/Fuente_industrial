using System;
using System.Net;

namespace Mardis.Engine.Web.Libraries.Services
{
    public class RestClientService
    {
        public RestClientService(string value, string url)
        {
            Value = value;
            Url = url;
        }

        public string Url { get; }

        public string Value { get; }

        public string RestUrl => Url + "?" + Value;

        public string GetRequest()
        {
            if (string.IsNullOrEmpty(RestUrl))
            {
                return null;
            }
            try
            {
                var responseObject = "";
                HttpWebRequest request = WebRequest.Create(RestUrl) as HttpWebRequest;
                if (request != null)
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response != null && response.StatusCode != HttpStatusCode.OK)
                        {
                            throw new Exception(
                                $"Server error (HTTP {response.StatusCode}: {response.StatusDescription}).");
                        }
                    }
                return responseObject;
            }
            catch (Exception e)
            {
                // catch exception and log it
                return null;
            }

        }
    }
}
