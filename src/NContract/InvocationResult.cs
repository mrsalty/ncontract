using System;
using System.Net.Http;
using NContract.RestApi;
using Newtonsoft.Json;

namespace NContract
{
    public class InvocationResult
    {
        public InvocationResult(
            RestApiClientConfiguration restApiClientConfiguration,
            HttpResponseMessage httpResponseMessage, 
            ResponseContentType responseContentType,
            DateTime invocationStartedUtc)
        {
            RestApiClientConfiguration = restApiClientConfiguration;
            HttpResponseMessage = httpResponseMessage;
            InvocationStartedUtc = invocationStartedUtc;
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
            InvocationEndedUtc = DateTime.UtcNow;
        }

        public DateTime InvocationEndedUtc { get; private set; }

        public HttpResponseMessage HttpResponseMessage { get; private set; }

        public DateTime InvocationStartedUtc { get; }

        public RestApiClientConfiguration RestApiClientConfiguration { get; private set; }

        public dynamic StringContent { get; set; }

        public byte[] ByteContent { get; set; }

        public TimeSpan InvocationTime => InvocationEndedUtc.Subtract(InvocationStartedUtc).Duration();
    }
}