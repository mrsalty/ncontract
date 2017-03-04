using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NContract.Core.RestApi
{
    public class HttpClientFactory
    {
        public HttpClient Create(RequestHeadersContainer requestHeaderContainer)
        {
            var client = new HttpClient();

            if (requestHeaderContainer != null)
            {
                requestHeaderContainer.Accept?.ToList().ForEach(item => client.DefaultRequestHeaders.Accept.Add(item));
                requestHeaderContainer.AcceptCharset?.ToList().ForEach(item => client.DefaultRequestHeaders.AcceptCharset.Add(item));
                requestHeaderContainer.AcceptEncoding?.ToList().ForEach(item => client.DefaultRequestHeaders.AcceptEncoding.Add(item));
            }

            return client;
        }
    }

    public class RequestHeader : HttpHeaders
    { }
}