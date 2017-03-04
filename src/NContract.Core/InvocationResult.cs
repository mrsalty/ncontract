using System.Net.Http;
using NContract.Core.RestApi;
using Newtonsoft.Json;

namespace NContract.Core
{
    public class InvocationResult
    {
        public InvocationResult(
            RestApiClientConfiguration restApiClientConfiguration,
            HttpResponseMessage httpResponseMessage, 
            ResponseContentType responseContentType)
        {
            RestApiClientConfiguration = restApiClientConfiguration;
            HttpResponseMessage = httpResponseMessage;
            switch (responseContentType)
            {
                case ResponseContentType.String:
                    StringContent =
                        JsonConvert.DeserializeObject<dynamic>(
                            httpResponseMessage.Content.ReadAsStringAsync().Result);
                    break;
                case ResponseContentType.Byte:
                    ByteContent =
                        httpResponseMessage.Content.ReadAsByteArrayAsync().Result;
                    break;
            }
        }
        
        public HttpResponseMessage HttpResponseMessage { get; private set; }

        public RestApiClientConfiguration RestApiClientConfiguration { get; private set; }

        public dynamic StringContent { get; set; }

        public byte[] ByteContent { get; set; }
    }
}