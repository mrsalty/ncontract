using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace NContract
{
    public class RequestHeadersContainer
    {
        public List<MediaTypeWithQualityHeaderValue> Accept { get; set; }
        public List<StringWithQualityHeaderValue> AcceptCharset { get; set; }
        public List<StringWithQualityHeaderValue> AcceptEncoding { get; set; }
        //TODO all props

        public override string ToString()
        {
            var headers = new StringBuilder();
            headers.AppendLine("Headers:");
            //Accept
            if (Accept != null)
            {
                foreach (var item in Accept)
                {
                    headers.Append($"-{item.Display()}{Environment.NewLine}");
                }
            }
            //AcceptCharset
            if (AcceptCharset != null)
            {
                foreach (var item in this.AcceptCharset)
                {
                    headers.Append($"-AcceptCharset:{item}{Environment.NewLine}");
                }
            }
            //AcceptEncoding
            if (AcceptEncoding != null)
            {
                foreach (var item in AcceptEncoding)
                {
                    headers.Append($"-AcceptEncoding:{item}{Environment.NewLine}");
                }
            }
            return headers.ToString();
        }
    }
}