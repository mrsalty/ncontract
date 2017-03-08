using System.Text;

namespace NContract.RestApi
{
    public static class RestApiClientDefaultConfiguration
    {
        public static ResponseContentType ContentType => ResponseContentType.String;

        public static Encoding Encoding => Encoding.UTF8;
    }
}