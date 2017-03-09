using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NContract.RestApi
{
    public class RestApiClient
    {
        private readonly RestApiClientConfiguration _clientConfiguration;
        private readonly ContractTest _contractTest;
        private readonly HttpClientFactory _httpClientFactory;

        internal RestApiClient(
            RestApiClientConfiguration clientConfiguration,
            ContractTest contractTest,
            HttpClientFactory httpClientFactory)
        {
            _clientConfiguration = clientConfiguration;
            _contractTest = contractTest;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<InvocationResult> Invoke()
        {
            if (_clientConfiguration.BaseUri == null)
            {
                throw new ApplicationException("BaseUri is null");
            }

            try
            {
                BeforeInvoke(_clientConfiguration.HttpMethod);

                var apiInvocation = new RestApiInvocation(_httpClientFactory, _clientConfiguration);

                _contractTest.ApiInvocations.Add(apiInvocation);

                InvocationResult invocationResult;

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

                AfterInvoke();

                return invocationResult;
            }
            catch (Exception ex)
            {
                //_nContractConfiguration.CustomStringBuilder.AppendLine(ex.ToString());
                throw;
            }
        }

        private void BeforeInvoke(HttpMethod httpMethod)
        {
        }

        private void AfterInvoke()
        {
        }
    }
}