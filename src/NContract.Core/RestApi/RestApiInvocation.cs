using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NContract.Core.RestApi
{
    public class RestApiInvocation
    {
        private readonly RestApiClientConfiguration _clientConfiguration;
        private readonly HttpClientWrapper _httpClientHelper;

        public RestApiInvocation(HttpClientFactory httpClientFactory,
            RestApiClientConfiguration clientConfiguration)
        {
            _clientConfiguration = clientConfiguration;
            _httpClientHelper = new HttpClientWrapper(httpClientFactory);
        }
        
        public InvocationResult InvocationResult { get; private set; }

        public async Task<InvocationResult> Invoke(HttpMethod httpMethod)
        {
            HttpResponseMessage httpResponseMessage;

            switch (httpMethod.ToString().ToUpper())
            {
                case "GET":
                    httpResponseMessage = await _httpClientHelper.GetAsync(_clientConfiguration);
                    break;
                case "POST":
                    httpResponseMessage = await _httpClientHelper.PostAsync(_clientConfiguration);
                    break;
                case "PUT":
                    httpResponseMessage = await _httpClientHelper.PutAsync(_clientConfiguration);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return new InvocationResult(httpResponseMessage, ResponseContentType.String);
        }
    }
}