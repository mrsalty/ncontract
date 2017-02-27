using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NContract.Core.RestApi
{
    public class HttpClientWrapper : IDisposable
    {
        private readonly HttpClientFactory _httpClientFactory;
        private HttpClient _client;

        public HttpClientWrapper(HttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<HttpResponseMessage> PostAsync(
            StringContent content,
            string baseUrl,
            string requestUri,
            string contentType)
        {
            using (_client = _httpClientFactory.Create())
            {
                _client.BaseAddress = new Uri(baseUrl);

                return await _client.PostAsync(requestUri, content);
            }
        }

        public async Task<HttpResponseMessage> PutAsync(
            StringContent content, 
            string baseUrl, 
            string requestUri, 
            string contentType)
        {
            using (_client = _httpClientFactory.Create())
            {
                _client.BaseAddress = new Uri(baseUrl);

                return await _client.PutAsync(requestUri, content);
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string baseUrl, string requestUri)
        {
            using (var client = _httpClientFactory.Create())
            {
                client.BaseAddress = new Uri(baseUrl);

                return await client.DeleteAsync(requestUri);
            }
        }

        public async Task<HttpResponseMessage> GetAsync(
            string baseUrl, 
            string requestUri, 
            string contentType)
        {
            using (var client = _httpClientFactory.Create())
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"{baseUrl}/{requestUri}"),
                    Method = HttpMethod.Get,
                };

                return await client.SendAsync(request);
            }
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}