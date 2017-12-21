using System.Net.Http;
using System.Text;

namespace NContract
{
    public class RestApiClientConfiguration
    {
        public string BaseUri { get; set; }

        public StringContent Content { get; set; }

        public object Model { get; set; }

        public RequestHeadersContainer Headers { get; set; }

        public string RequestUri { get; set; }

        public string ContentType { get; set; }

        public ResponseContentType ResponseContentType { get; set; }

        public Encoding Encoding { get; set; }

        public HttpMethod HttpMethod { get; set; }
    }
}