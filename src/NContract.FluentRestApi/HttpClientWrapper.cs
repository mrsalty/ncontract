using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NContract.FluentRestApi
{
    public class HttpClientWrapper : IDisposable
    {
        private readonly HttpClientFactory _httpClientFactory;
        private HttpClient _client;

        public HttpClientWrapper(HttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Encoding DefaultEncoding => Encoding.UTF8;
        
        public async Task<HttpResponseMessage> PostAsync(RestApiClientConfiguration configuration)
        {
            using (_client = _httpClientFactory.Create(configuration.Headers))
            {
                _client.BaseAddress = new Uri(configuration.BaseUri);
                if (!string.IsNullOrEmpty(configuration.ContentType))
                {
                    configuration.Content.Headers.ContentType.MediaType = configuration.ContentType;
                }

                return await _client.PostAsync(configuration.RequestUri, configuration.Content);
            }
        }

        public async Task<HttpResponseMessage> PutAsync(RestApiClientConfiguration configuration)
        {
            using (_client = _httpClientFactory.Create(configuration.Headers))
            {
                _client.BaseAddress = new Uri(configuration.BaseUri);

                return await _client.PutAsync(configuration.RequestUri, configuration.Content);
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(RestApiClientConfiguration configuration)
        {
            using (var client = _httpClientFactory.Create(configuration.Headers))
            {
                client.BaseAddress = new Uri(configuration.BaseUri);

                return await client.DeleteAsync(configuration.RequestUri);
            }
        }

        public async Task<HttpResponseMessage> GetAsync(RestApiClientConfiguration configuration)
        {
            using (var client = _httpClientFactory.Create(configuration.Headers))
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"{configuration.BaseUri}/{configuration.RequestUri}"),
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