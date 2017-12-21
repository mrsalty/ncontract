using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NContract
{
    public class HttpClientWrapper
    {
        private readonly HttpClient _client;

        public HttpClientWrapper()
        {
            _client = new HttpClient();
        }

        public Encoding DefaultEncoding => Encoding.UTF8;

        public async Task<HttpResponseMessage> PostAsync(RestApiClientConfiguration configuration)
        {
            SetupHeaders(configuration.Headers);

            _client.BaseAddress = new Uri(configuration.BaseUri);

            if (!string.IsNullOrEmpty(configuration.ContentType))
            {
                configuration.Content.Headers.ContentType.MediaType = configuration.ContentType;
            }

            return await _client.PostAsync(configuration.RequestUri, configuration.Content);
        }

        public async Task<HttpResponseMessage> PutAsync(RestApiClientConfiguration configuration)
        {
            SetupHeaders(configuration.Headers);

            _client.BaseAddress = new Uri(configuration.BaseUri);

            return await _client.PutAsync(configuration.RequestUri, configuration.Content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(RestApiClientConfiguration configuration)
        {
            SetupHeaders(configuration.Headers);

            _client.BaseAddress = new Uri(configuration.BaseUri);

            return await _client.DeleteAsync(configuration.RequestUri);
        }

        public async Task<HttpResponseMessage> GetAsync(RestApiClientConfiguration configuration)
        {
            SetupHeaders(configuration.Headers);

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri($"{configuration.BaseUri}/{configuration.RequestUri}"),
                Method = HttpMethod.Get,
                Content = configuration.Content
            };

            return await _client.SendAsync(request);
        }

        private void SetupHeaders(RequestHeadersContainer requestHeaderContainer)
        {
            if (requestHeaderContainer == null) return;

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.AcceptCharset.Clear();
            _client.DefaultRequestHeaders.AcceptEncoding.Clear();

            requestHeaderContainer.Accept?.ToList().ForEach(item => _client.DefaultRequestHeaders.Accept.Add(item));
            requestHeaderContainer.AcceptCharset?.ToList().ForEach(item => _client.DefaultRequestHeaders.AcceptCharset.Add(item));
            requestHeaderContainer.AcceptEncoding?.ToList().ForEach(item => _client.DefaultRequestHeaders.AcceptEncoding.Add(item));
            //TODO Add all props
        }
    }
}