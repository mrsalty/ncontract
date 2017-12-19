using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NContract.FluentRestApi
{
    public class RestApiClient
    {
        private readonly RestApiClientConfiguration _clientConfiguration;

        public RestApiClient(RestApiClientConfiguration clientConfiguration)
        {
            _clientConfiguration = clientConfiguration;
        }

        public async Task<InvocationResult> Invoke(RestApiInvocation apiInvocation)
        {
            InvocationResult invocationResult;

            if (_clientConfiguration.BaseUri == null)
            {
                throw new ApplicationException("BaseUri is null");
            }

            switch (_clientConfiguration.HttpMethod.Method.ToUpper())
            {
                case "GET":
                    invocationResult = await apiInvocation.Invoke(HttpMethod.Get);
                    break;
                case "POST":
                    invocationResult = await apiInvocation.Invoke(HttpMethod.Post);
                    break;
                case "PUT":
                    invocationResult = await apiInvocation.Invoke(HttpMethod.Put);
                    break;
                case "DELETE":
                    invocationResult = await apiInvocation.Invoke(HttpMethod.Delete);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return invocationResult;
        }
    }
}