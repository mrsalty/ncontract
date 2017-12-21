using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NContract
{
    public class RestApiInvocation
    {
        private readonly RestApiClientConfiguration _restApiClientConfiguration;
        private readonly HttpClientWrapper _httpClientWrapper;

        public RestApiInvocation(RestApiClientConfiguration restApiClientConfiguration)
        {
            _restApiClientConfiguration = restApiClientConfiguration;
            _httpClientWrapper = new HttpClientWrapper();
        }
        
        public InvocationResult InvocationResult { get; private set; }

        public async Task<InvocationResult> Invoke(HttpMethod httpMethod)
        {
            var invationStartedUtc = DateTime.UtcNow;
            try
            {
                HttpResponseMessage httpResponseMessage;
                switch (httpMethod.ToString().ToUpper())
                {
                    case "GET":
                        httpResponseMessage = await _httpClientWrapper.GetAsync(_restApiClientConfiguration);
                        break;
                    case "POST":
                        httpResponseMessage = await _httpClientWrapper.PostAsync(_restApiClientConfiguration);
                        break;
                    case "PUT":
                        httpResponseMessage = await _httpClientWrapper.PutAsync(_restApiClientConfiguration);
                        break;
                    case "DELETE":
                        httpResponseMessage = await _httpClientWrapper.DeleteAsync(_restApiClientConfiguration);
                        break;
                    default:
                        throw new NotImplementedException();
                }

                InvocationResult = new InvocationResult(_restApiClientConfiguration, httpResponseMessage,
                    ResponseContentType.String, invationStartedUtc);

                return InvocationResult;
            }
            catch (AggregateException aex)
            {
                aex.InnerExceptions.ToList().ForEach(ex => InvocationResult.Exceptions.Add(ex));
                throw;
            }
        }
    }
}