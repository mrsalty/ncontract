using System.Collections.Generic;
using System.Net.Http.Headers;

namespace NContract.Core.RestApi
{
    public class RequestHeadersContainer
    {
        public List<MediaTypeWithQualityHeaderValue> Accept { get; set; }
        public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptCharset { get; set; }
        public HttpHeaderValueCollection<StringWithQualityHeaderValue> AcceptEncoding { get; set; }
        //TODO all props
    }
}