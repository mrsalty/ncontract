using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace NContract.FluentRestApi
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
            TryGetContent(httpResponseMessage, responseContentType);
            InvocationEndedUtc = DateTime.UtcNow;
            Exceptions = new List<Exception>();
        }

        private void TryGetContent(HttpResponseMessage httpResponseMessage, ResponseContentType responseContentType)
        {
            try
            {
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
                    default:
                        //TODO implement all
                        throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                Exceptions.Add(ex);
                throw;
            }
        }

        public DateTime InvocationEndedUtc { get; private set; }

        public HttpResponseMessage HttpResponseMessage { get; private set; }

        public DateTime InvocationStartedUtc { get; }

        public RestApiClientConfiguration RestApiClientConfiguration { get; private set; }

        public dynamic StringContent { get; set; }

        public byte[] ByteContent { get; set; }

        public TimeSpan InvocationTime => InvocationEndedUtc.Subtract(InvocationStartedUtc).Duration();

        public IList<Exception> Exceptions { get; set; }
    }
}