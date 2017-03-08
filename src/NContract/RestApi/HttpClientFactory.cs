using System.Linq;
using System.Net.Http;

namespace NContract.RestApi
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
}