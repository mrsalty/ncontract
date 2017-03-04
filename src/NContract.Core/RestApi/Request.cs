using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace NContract.Core.RestApi
{
    public class Request
    {
        public Request(HttpMethod httpMethod,
            string baseUri,
            string requestUri,
            string body,
        string contentType,
        StringContent stringContent,   
        IEnumerable<KeyValuePair<string, string>> headers)
        {
            HttpMethod = httpMethod;
            BaseUri = baseUri;
            RequestUri = requestUri;
            Body = body;
            ContentType = contentType;
            StringContent = stringContent;
            Headers = headers?.ToList();
        }

        public HttpMethod HttpMethod { get; private set; }

        public string BaseUri { get; private set; }

        public string RequestUri { get; private set; }

        public string Uri => $"{BaseUri}{RequestUri}";

        public string Body { get; private set; }

        public IList<KeyValuePair<string, string>> Headers { get; private set; }

        public string ContentType { get; private set; }

        public StringContent StringContent { get; private set; }
    }
}