using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace NContract.Core.RestApi
{
    public class RequestHeadersContainer
    {
        public List<MediaTypeWithQualityHeaderValue> Accept { get; set; }
        public List<StringWithQualityHeaderValue> AcceptCharset { get; set; }
        public List<StringWithQualityHeaderValue> AcceptEncoding { get; set; }
        //TODO all props

        public override string ToString()
        {
            var headers= new StringBuilder();
            foreach (var item in this.Accept)
            {
                headers.Append(item.Display());
            }
            return headers.ToString();
        }
    }
}