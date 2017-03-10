using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NContract.FluentRestApi
{
    public class RestApiInvocation
    {
        private readonly RestApiClientConfiguration _clientConfiguration;
        private readonly HttpClientWrapper _httpClientWrapper;

        public RestApiInvocation(HttpClientFactory httpClientFactory,
            RestApiClientConfiguration clientConfiguration)
        {
            _clientConfiguration = clientConfiguration;
            _httpClientWrapper = new HttpClientWrapper(httpClientFactory);
        }
        
        public InvocationResult InvocationResult { get; private set; }

        public async Task<InvocationResult> Invoke(HttpMethod httpMethod)
        {
            DateTime invationStartedUtc = DateTime.UtcNow;
            HttpResponseMessage httpResponseMessage;
            switch (httpMethod.ToString().ToUpper())
            {
                case "GET":
                    httpResponseMessage = await _httpClientWrapper.GetAsync(_clientConfiguration);
                    break;
                case "POST":
                    httpResponseMessage = await _httpClientWrapper.PostAsync(_clientConfiguration);
                    break;
                case "PUT":
                    httpResponseMessage = await _httpClientWrapper.PutAsync(_clientConfiguration);
                    break;
                case "DELETE":
                    httpResponseMessage = await _httpClientWrapper.DeleteAsync(_clientConfiguration);
                    break;
                default:
                    throw new NotImplementedException();
            }

            InvocationResult = new InvocationResult(_clientConfiguration, httpResponseMessage, ResponseContentType.String, invationStartedUtc);

            return InvocationResult;
        }
    }
}