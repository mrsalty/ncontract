using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NContract.Core.RestApi
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
                        invocationResult = await apiInvocation.Invoke(new Request(
                            HttpMethod.Get,
                            _clientConfiguration.BaseUri,
                            _clientConfiguration.RequestUri,
                            _clientConfiguration?.Model?.ToString(),
                            _clientConfiguration.ContentType,
                            null,
                            null));
                        break;
                    case "POST":
                        invocationResult = await apiInvocation.Invoke(
                            new Request(
                                HttpMethod.Post,
                                _clientConfiguration.BaseUri,
                                _clientConfiguration.RequestUri,
                                string.Empty,
                                _clientConfiguration.ContentType,
                                _clientConfiguration.Content,
                                null));
                        break;
                    case "PUT":
                        invocationResult = await apiInvocation.Invoke(
                            new Request(
                                HttpMethod.Put, 
                                _clientConfiguration.BaseUri, 
                                _clientConfiguration.RequestUri,
                                string.Empty, 
                                _clientConfiguration.ContentType, 
                                _clientConfiguration.Content, 
                                null));
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
            //_nContractConfiguration.CustomStringBuilder.AppendLine(string.Empty);
            //_nContractConfiguration.CustomStringBuilder.AppendLine("RESPONSE");
            //_nContractConfiguration.CustomStringBuilder.AppendLine(new string('-', 8));
            //_nContractConfiguration.CustomStringBuilder.AppendLine($"Result status code : {InvocationResult.HttpResponseMessage.StatusCode}");
            //_nContractConfiguration.CustomStringBuilder.AppendLine($"Result string content: {InvocationResult.StringContent ?? "NULL"}");
            //_nContractConfiguration.CustomStringBuilder.AppendLine(string.Empty);
        }
    }
}