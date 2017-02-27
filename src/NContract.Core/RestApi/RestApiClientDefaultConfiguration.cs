using System.Text;

namespace NContract.Core.RestApi
{
    public static class RestApiClientDefaultConfiguration
    {
        public static ResponseContentType ContentType => ResponseContentType.String;

        public static Encoding Encoding => Encoding.UTF8;
    }
}