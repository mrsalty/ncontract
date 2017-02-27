using System.Net.Http;

namespace NContract.Core.RestApi
{
    public class HttpClientFactory
    {
        public HttpClient Create()
        {
            return new HttpClient();
        }
    }
}