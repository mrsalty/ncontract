using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NContract.Core.RestApi
{
    public class RestApiInvocation
    {
        private readonly HttpClientWrapper _httpClientHelper;

        public RestApiInvocation(HttpClientFactory httpClientFactory,
            RestApiClientConfiguration clientConfiguration)
        {
            _httpClientHelper = new HttpClientWrapper(httpClientFactory);
        }

        public Request Request { get; private set; }

        public InvocationResult InvocationResult { get; private set; }

        public async Task<InvocationResult> Invoke(Request request)
        {
            Request = request;
            HttpResponseMessage httpResponseMessage;

            switch (request.HttpMethod.ToString().ToUpper())
            {
                case "GET":
                    httpResponseMessage = await _httpClientHelper.GetAsync(request.BaseUri, request.RequestUri, request.ContentType);
                    break;
                case "POST":
                    httpResponseMessage = await _httpClientHelper.PostAsync(request.StringContent, request.BaseUri, request.RequestUri, request.ContentType);
                    break;
                case "PUT":
                    httpResponseMessage = await _httpClientHelper.PutAsync(request.StringContent, request.BaseUri, request.RequestUri, request.ContentType);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return new InvocationResult(httpResponseMessage, ResponseContentType.String);
        }
    }
}